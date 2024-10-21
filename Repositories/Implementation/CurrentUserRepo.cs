using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Repositories.Interface;

namespace WardrobeOrganizerApp.Repositories.Implementation
{
    public class CurrentUserRepo : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserRepo(HttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetCurrentUser()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                var email = httpContext.User.FindFirst(ClaimTypes.Email);
                return email.Value;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Email claim not found");
            }
        }
    }
}