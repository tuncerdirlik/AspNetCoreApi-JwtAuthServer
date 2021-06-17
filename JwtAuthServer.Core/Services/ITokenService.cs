using JwtAuthServer.Core.Configuration;
using JwtAuthServer.Core.Dtos;
using JwtAuthServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtAuthServer.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp userApp);
        ClientTokenDto CreateTokenByClient(Client client);
    }
}
