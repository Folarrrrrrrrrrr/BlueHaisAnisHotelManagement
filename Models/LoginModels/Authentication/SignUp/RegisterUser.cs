﻿
namespace BlueHaisAnisHotelManagement.Models.LoginModels.Authentication.SignUp
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

         [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
    }
}
