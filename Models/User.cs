﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaveIBeenPwnedAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
