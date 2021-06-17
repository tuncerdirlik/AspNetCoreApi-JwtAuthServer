using System;
using System.Collections.Generic;
using System.Text;

namespace JwtAuthServer.Core.Dtos
{
    public class ClientTokenDto
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
    }
}
