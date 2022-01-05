using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockManagment.Authentication.Configuration;
using StockManagment.Authentication.Models.DTOs.Generic;
using StockManagment.Authentication.Models.DTOs.Incoming;
using StockManagment.Authentication.Models.DTOs.Outgoing;
using StockManagment.DataServices.IConfiguration;
using StockManagment.Entities.DbSet;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockManagment.Api.Controllers.v1
{

    public class AccountsController : BaseController
    {

        private readonly JwtConfig _jwtConfig;

        private readonly TokenValidationParameters _tokenValidationParameters;
        public AccountsController(
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            TokenValidationParameters tokenValidationParameters)
            : base(unitOfWork, userManager)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenValidationParameters = tokenValidationParameters;
        }


        // Registr Action
        [HttpPost]
        [Route("Registr")]
        public async Task<IActionResult> Registr([FromBody] UserRegistrationRequestDTO userRegistrationRequestDTO)
        {
            if (ModelState.IsValid)
            {
                var userexit = await _userManager.FindByEmailAsync(userRegistrationRequestDTO.Email);
                if (userexit != null)
                {
                    return BadRequest(new UserRegistrationResponsDTO()
                    {
                        Succes = false,
                        Errors = new List<string>(){
                            "Email  already in use"
                        }
                    });
                }

                var newUser = new IdentityUser()
                {
                    Email = userRegistrationRequestDTO.Email,
                    UserName = userRegistrationRequestDTO.Email,
                    EmailConfirmed = true
                };

                var isCreated = await _userManager.CreateAsync(newUser, userRegistrationRequestDTO.Password);

                if (!isCreated.Succeeded)
                {
                    return BadRequest(new UserRegistrationResponsDTO()
                    {
                        Succes = isCreated.Succeeded,
                        Errors = isCreated.Errors.Select(x => x.Description).ToList()
                    });
                }

                var _user = new User();
                _user.Identity = new Guid(newUser.Id);
                _user.FirstName = userRegistrationRequestDTO.FirstName;
                _user.LastName = userRegistrationRequestDTO.LastName;
                _user.Phone = "";
                _user.Email = userRegistrationRequestDTO.Email;
                _user.DateOfBirth = DateTime.UtcNow;
                _user.Status = 1;

                await _iUnitOfWork.UserRepository.Add(_user);
                await _iUnitOfWork.CompleteAsync();


                var Token = await GenerateJwtToken(newUser);

                return Ok(new UserRegistrationResponsDTO()
                {
                    Succes = true,
                    Token = Token.JwtToken,
                    RefreshToken = Token.RefreshToken
                });

            }
            else
            {
                return BadRequest(new UserRegistrationResponsDTO()
                {
                    Succes = false,
                    Errors = new List<string>()
                    {
                         "Invalid payload"
                    }
                });
            }
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDTO userLoginRequest)
        {
            if (ModelState.IsValid)
            {
                var userExies = await _userManager.FindByEmailAsync(userLoginRequest.Email);

                if (userExies == null)
                {
                    return BadRequest(new UserLoginResponseDTO()
                    {
                        Succes = false,
                        Errors = new List<string>(){
                        "Invali authentication request"
                        }
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(userExies, userLoginRequest.Password);
                if (isCorrect)
                {
                    var jwtToken = await GenerateJwtToken(userExies);
                    return Ok(new UserLoginResponseDTO()
                    {
                        Succes = true,
                        Token = jwtToken.JwtToken,
                        RefreshToken = jwtToken.RefreshToken

                    });
                } else
                {
                    return BadRequest(new UserLoginResponseDTO()
                    {
                        Succes = false,
                        Errors = new List<string>(){
                        "Invali authentication request"
                        }
                    });
                }


            } else
            {

                return BadRequest(new UserLoginResponseDTO()
                {
                    Succes = false,
                    Errors = new List<string>(){
                        "Invalid payload"
                    }
                });
            }
        }


        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken( [FromBody] TokenRequestDTO tokenRequestDTO  )
        {
            if(ModelState.IsValid)
            {
                var result = await VerifyToken(tokenRequestDTO);

                if (result == null)
                {
                    return BadRequest(new UserRegistrationResponsDTO()
                    {
                        Succes = false,
                        Errors = new List<string>(){
                        "Token validation Faild"
                    }
                    });
                }

                return Ok(result);

            }
            else
            {
                return BadRequest(new UserRegistrationResponsDTO()
                {
                    Succes = false,
                    Errors = new List<string>(){
                        "Invalid payload"
                    }
                });
            }

        }

        private async Task<AuthResult> VerifyToken(TokenRequestDTO tokenRequest)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                //we need to check the validety of the token
                var principal = tokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToekn);

                // we need to valida result that has been generated for us

                if (validatedToekn is JwtSecurityToken jwtSecurityToken)
                {
                    // check if the jwtToken is created with the same algorithm as out jwt token
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (!result)
                        return null;

                }

                // we need to check expiary date of token 

                var utcExpiaryDate = long.Parse(principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                // convert to date to check
                var expdate = UnixTimeStampToDateTime(utcExpiaryDate);

                // checking if the jwt token has expired
                if (expdate > DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Succes = false,
                        Errors = new List<string>(){
                                        "Jwt token has not expired"
                                }
                    };
                }

                // check if the refresh token exist

                var refreshTokenExit = await _iUnitOfWork.RefreshTokenReposiroty.GetByRefreshToken(tokenRequest.RefreshToken);

                if (refreshTokenExit == null)
                {
                    return new AuthResult()
                    {
                        Succes = false,
                        Errors = new List<string>(){
                                        "Invalid refresh token"
                                }
                    };
                }

                // check the expriary date of refreshToken
                if (refreshTokenExit.ExpiryDate < DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Succes = false,
                        Errors = new List<string>(){
                                        "Refresh token has expired plase log in again"
                                }
                    };
                }

                // chack the refresh token has been user or not
                if (refreshTokenExit.isUsed)
                {
                    return new AuthResult()
                    {
                        Succes = false,
                        Errors = new List<string>(){
                                        "Refresh token has used"
                                }
                    };
                }

                // chack if refresh token has been revoked
                if (refreshTokenExit.isRevoked)
                {
                    return new AuthResult()
                    {
                        Succes = false,
                        Errors = new List<string>(){
                                        "Refresh token has been revoked, it cannot be used"
                                }
                    };
                }

                var jti = principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (refreshTokenExit.JwtId != jti)
                {
                    return new AuthResult()
                    {
                        Succes = false,
                        Errors = new List<string>(){
                                        "Refresh token reference dose not match the jwtToken"
                                }
                    };
                }

                // Start processing and get a new token
                refreshTokenExit.isUsed = true;
                var updateResult = await _iUnitOfWork.RefreshTokenReposiroty.MarkRefreshTokenAsUsed(refreshTokenExit);

                if (updateResult)
                {
                    await _iUnitOfWork.CompleteAsync();

                    // get the user to generate a new jwt Token
                    var dbUser = await _userManager.FindByIdAsync(refreshTokenExit.UserId);

                    if (dbUser == null)
                    {
                        return new AuthResult()
                        {
                            Succes = false,
                            Errors = new List<string>(){
                                        "Error processing request"
                                }
                        };

                    }
                    // Genrate jwt token
                    var tokens = await GenerateJwtToken(dbUser);
                    return new AuthResult()
                    {
                        Succes = true,
                        Token = tokens.JwtToken,
                        RefreshToken = tokens.RefreshToken
                    };

                }


                return new AuthResult()
                {
                    Succes = false,
                    Errors = new List<string>(){
                                        "Error processing request"
                                }
                };

            }
            catch (Exception ex)
            {
                // todo : Add better error handling, add a logger

                return null;
            }
        }

        private DateTime UnixTimeStampToDateTime(long unixDate)
        {
            // Sets the time to 1, Jan, 1970
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            // Add the number of seconds from 1 Jan 1970
            dateTime = dateTime.AddSeconds(unixDate).ToUniversalTime();

            return dateTime;

        }

        private async Task<TokenData> GenerateJwtToken(IdentityUser newUser)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", newUser.Id),
                    new Claim(ClaimTypes.NameIdentifier, newUser.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTimeFrame),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                 )
            };
            // generate Security Token 
            var token = jwtHandler.CreateToken(tokenDecriptor);

            var jwtToken = jwtHandler.WriteToken(token);


            //Generate Refresh Token

            var refreshToken = new RefreshToken
            {
                AddedDate = DateTime.UtcNow,
                Token = $"{RandomSrtingGenerator(25)}_{Guid.NewGuid().ToString()}", // Create Method to generate Random string and attach a create guid
                UserId = newUser.Id,
                isRevoked = false,
                isUsed = false,
                Status = 1,
                JwtId = token.Id,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _iUnitOfWork.RefreshTokenReposiroty.Add(refreshToken);
            await _iUnitOfWork.CompleteAsync();

            var tokenData = new TokenData()
            {
                JwtToken = jwtToken,
                RefreshToken = refreshToken.Token
            };

            return tokenData;
        }

        private string RandomSrtingGenerator(int length)
        {
            var random = new Random();
            const string charts = "ABCDEFGHJKLQWOWYDNS12321144536";

            return new string(Enumerable.Repeat(charts, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
