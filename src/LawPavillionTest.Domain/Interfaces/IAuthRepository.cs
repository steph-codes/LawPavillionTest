using LawPavillionTest.Domain.DTOs.Request;
using LawPavillionTest.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawPavillionTest.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<Response> RegisterAsync(RegisterModel registerModel);
        Task<LoginResponse> LoginAsync(LoginModel loginmodel);
    }
}