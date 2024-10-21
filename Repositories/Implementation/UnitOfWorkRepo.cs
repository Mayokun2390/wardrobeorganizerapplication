using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Context;
using WardrobeOrganizerApp.Repositories.Interface;

namespace WardrobeOrganizerApp.Repositories.Implementation
{
    public class UnitOfWorkRepo : IUnitOfWork
    {
        private readonly StoreContext _context;
        public UnitOfWorkRepo(StoreContext context)
        {
            _context = context;
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();

        }
    }
}