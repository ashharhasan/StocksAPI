using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace api.Dtos.AccountDto
{
    public class RegisterDto
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [HiddenInput]
        public string? Password { get; set; }
    }
    public class NewUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Token { get; set; }
    }
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}