using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawPavillionTest.Domain.DTOs.Request
{
    public class RegisterModel
    {
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "EnterPhone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Username is required")]

        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is Invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(45, ErrorMessage = "Maximum Password Length Exceeded! only up to 45 characters expected!")]
        public string Password { get; set; }


    }
}
