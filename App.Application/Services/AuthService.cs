using App.Application.IExternalServices;
using App.Domain.DTOs.Requests;
using App.Domain.DTOs.Responses;
using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Application.Services
{
    public class AuthService(IUserRepository userRepository, IConfiguration configuration,
        IUnitOfWork unitOfWork, IEmailService emailService, IValidator<SignInDto> signInValidator,
        IValidator<SignUpDto> signUpValidator, IFileRepository fileRepository) : IAuthService
    {
        public async Task<ApiResponse<UserResponseDto>> ConfirmEmailAsync(string email, string token)
        {
            var user = await userRepository.GetUserAsync(u => u.Email == email);
            if (user == null) return new ApiResponse<UserResponseDto>
            {
                IsSuccessful = false,
                Message = "User Not Found"
            };

            if (!ValidateToken(token, user.Email)) return new ApiResponse<UserResponseDto>
            {
                IsSuccessful = false,
                Message = "Token not valid"
            };

            // Mark email as confirmed
            user.EmailConfirmed = true;
            userRepository.Update(user);
            await unitOfWork.SaveAsync();

            return new ApiResponse<UserResponseDto>
            {
                IsSuccessful = true,
                Message = "Email Confirmed"
            };
        }

        public async Task<ApiResponse<string>> SignInAsync(SignInDto request)
        {
            var validationResult = await signInValidator.ValidateAsync(request);
            if (!validationResult.IsValid) return new ApiResponse<string>
            {
                IsSuccessful = false,
                Message = "Invalid Credentials"
            };

            var user = await userRepository.GetUserAsync(u => u.Email == request.Email);
            if (user == null) return new ApiResponse<string>
            {
                IsSuccessful = false,
                Message = "Unrecognized Email"
            };

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid) return new ApiResponse<string>
            {
                IsSuccessful = false,
                Message = "Invalid Email or Password"
            };

            return new ApiResponse<string>
            {
                IsSuccessful = true,
                Message = "Sign in successful",
                Data = GenerateToken(user)
            };

        }

        public async Task<ApiResponse<UserResponseDto>> SignUpAsync(SignUpDto request)
        {
            var validationResult = await signUpValidator.ValidateAsync(request);
            if (!validationResult.IsValid) return new ApiResponse<UserResponseDto>
            {
                IsSuccessful = false,
                Message = "Invalid Inputs"
            };

            var existingUser = await userRepository.GetUserAsync(u => u.Email == request.Email);
            if (existingUser != null) return new ApiResponse<UserResponseDto>
            {
                IsSuccessful = false,
                Message = $"User with email {request.Email} already exist"
            };

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedAt = DateTime.Now,
                CreatedBy = request.Email
            };


            if (request.ProfilePic != null)
            {
                var imageUpload = await fileRepository.UploadAsync(request.ProfilePic);
                if (imageUpload != null)
                {
                    user.ProfilePic = imageUpload;
                }
            }

            await userRepository.CreateAsync(user);
            var result = await unitOfWork.SaveAsync();
            if (result > 0)
            {
                var token = GenerateToken(user);
                SendConfirmationEmail(user, token);
                return new ApiResponse<UserResponseDto>
                {
                    IsSuccessful = true,
                    Message = $"Registration Successfull, a confirmation email has been sent to you"
                };
            }

            return new ApiResponse<UserResponseDto>
            {
                IsSuccessful = false,
                Message = "Registration Failed"
            };
        }

        private string GenerateToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim("Surname", user.LastName),
                new Claim("GivenName", user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var tokenLifetime = int.Parse(configuration["Jwt:TokenLifetime"]);
            var tokenExpiration = DateTime.UtcNow.AddDays(tokenLifetime);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:ValidIssuer"],
                audience: configuration["Jwt:ValidAudience"],
                claims: claims,
                expires: tokenExpiration,
                signingCredentials: credential);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        private bool ValidateToken(string token, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = secretKey,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                ClockSkew = TimeSpan.Zero
            };

            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var tokenEmail = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;

            return tokenEmail == email;
        }

        private void SendConfirmationEmail(User user, string token)
        {
            string url = $"{configuration["AppUrl"]}/api/auth/confirmEmail?email={user.Email}&token={token}";
            string userFullName = $"{user.FirstName} {user.LastName}";

            var mailRequestDto = new MailRequestDto
            {
                ToEmail = user.Email,
                Subject = "Confirm Your Email",
                Body = emailService.CreateBody(userFullName, url)
            };
            emailService.SendEmail(emailService.CreateMailMessage(mailRequestDto));
        }
    }
}
