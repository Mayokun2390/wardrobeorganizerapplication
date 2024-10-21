using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WardrobeOrganizerApp.Repositories.Interface
{
    public interface IUnitOfWork
    {
        int SaveChanges();

    }
}