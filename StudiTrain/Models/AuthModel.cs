﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace StudiTrain.Models
{
    public class AuthModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Passhash { get; set; }    
    }
}
