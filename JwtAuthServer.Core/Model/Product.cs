﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JwtAuthServer.Core.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string UserId { get; set; }
    }
}
