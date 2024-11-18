using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Services.Interface
{
    public interface IJWTSettingsService
    {
        string GenerateToken(User user);
        Task<User?> GetUserById(Guid id);

    }
}