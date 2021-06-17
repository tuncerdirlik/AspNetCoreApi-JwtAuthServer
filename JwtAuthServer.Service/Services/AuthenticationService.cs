using JwtAuthServer.Core.Configuration;
using JwtAuthServer.Core.Dtos;
using JwtAuthServer.Core.Model;
using JwtAuthServer.Core.Repositories;
using JwtAuthServer.Core.Services;
using JwtAuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthServer.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

        public AuthenticationService(IOptions<List<Client>> optionsClient,
                                     ITokenService tokenService,
                                     UserManager<UserApp> userManager,
                                     IUnitOfWork unitOfWork,
                                     IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
        }


        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Response<TokenDto>.Failed("Email or Password is wrong", 400, true);
            }

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Response<TokenDto>.Failed("Email or Password is wrong", 400, true);
            }

            var token = _tokenService.CreateToken(user);
            var userRefreshToken = await _userRefreshTokenService.Where(k => k.UserId == user.Id).SingleOrDefaultAsync();
            if (userRefreshToken == null)
            {
                userRefreshToken = new UserRefreshToken
                {
                    UserId = user.Id,
                    Code = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration
                };

                await _userRefreshTokenService.AddAsync(userRefreshToken);
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;

                
            }

            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(token, 200);
        }

        public async Task<Response<ClientTokenDto>> CreateTokenByClientAsync(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(k => k.Id == clientLoginDto.ClientId && k.Secret == clientLoginDto.ClientSecret);
            if (client == null)
            {
                return Response<ClientTokenDto>.Failed("ClientId ya da ClientSecret not found", 404, true);
            }

            var token = _tokenService.CreateTokenByClient(client);
            return Response<ClientTokenDto>.Success(token, 200);
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var dbRefreshToken = await _userRefreshTokenService.Where(k => k.Code == refreshToken).SingleOrDefaultAsync();
            if (dbRefreshToken == null)
            {
                return Response<TokenDto>.Failed("Refresh token not found", 404, true);
            }

            var user = await _userManager.FindByIdAsync(dbRefreshToken.UserId);
            if (user == null)
            {
                return Response<TokenDto>.Failed("User Id not found", 404, true);
            }

            var tokenDto = _tokenService.CreateToken(user);
            dbRefreshToken.Code = tokenDto.RefreshToken;
            dbRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(tokenDto, 200);
        }

        public async Task<Response<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken)
        {
            var dbRefreshToken = await _userRefreshTokenService.Where(k => k.Code == refreshToken).SingleOrDefaultAsync();
            if (dbRefreshToken == null)
            {
                return Response<NoDataDto>.Failed("Refresh token not found", 404, true);
            }

            _userRefreshTokenService.Remove(dbRefreshToken);
            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(200);
        }
    }
}
