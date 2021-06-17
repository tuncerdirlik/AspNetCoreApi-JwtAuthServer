﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JwtAuthServer.Core.Configuration
{
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public List<string> Audiences { get; set; }
    }
}
