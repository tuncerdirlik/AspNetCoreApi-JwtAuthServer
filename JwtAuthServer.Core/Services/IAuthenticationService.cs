using JwtAuthServer.Core.Dtos;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthServer.Core.Services
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<Response<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken);
        
        /// <summary>
        /// RefreshToken'ı siler
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<Response<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken);

        Task<Response<ClientTokenDto>> CreateTokenByClientAsync(ClientLoginDto clientLoginDto);
    }
}
