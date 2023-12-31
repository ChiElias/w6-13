﻿using System;
using System.ComponentModel.DataAnnotations;

namespace API.DIOs;

#nullable disable
public class RegisterDto
{
    [Required(ErrorMessage = "Please enter a username")]
    [MinLength(3, ErrorMessage = "Please enter a username at least 3 characters")]
    public string UserName { get; set; }
    [MinLength(3, ErrorMessage = "Please enter a password at least 3 characters")]
    public string password { get; set; }
}
