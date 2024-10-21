using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Repositories.Interface
{
    public interface IProductInterface
    {
        Task<Product> Create(Product product);
        Task<Product> GetById(Guid id);
        Task<Product> GetByName(string name);
        Task<ICollection<Product>> GetAllProducts();
        Product Update(Product product);
        bool Delete(Product product);
    }
}