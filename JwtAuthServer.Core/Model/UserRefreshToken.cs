﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JwtAuthServer.Core.Model
{
    public class UserRefreshToken
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public DateTime Expiration { get; set; }
    }
}