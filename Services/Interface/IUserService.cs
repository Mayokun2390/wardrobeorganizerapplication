using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;

namespace WardrobeOrganizerApp.Services.Interface
{
    public interface IUserService
    {
        Task<Response<UserResponseModel>> Login(LoginRequestModel model);

    }
}