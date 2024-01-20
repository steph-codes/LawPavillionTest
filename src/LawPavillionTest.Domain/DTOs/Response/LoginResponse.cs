using LawPavillionTest.Domain.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawPavillionTest.Domain.DTOs.Response
{
    public class LoginResponse : RegisterModel
    {
        public string Token { get; set; }
        public Response Response { get; set; } 
    }
}
