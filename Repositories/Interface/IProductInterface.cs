using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Repositories.Interface
{
    public interface IProductInterface
    {
        Task<Product> Create(Product product);
        Task<Product> GetById(Guid id);
        Task<Product> GetByName(string name);
        Task<ICollection<Product>> GetAllOutfits(Expression<Func<Product, bool>> predicate);
        Task<ICollection<Product>> GetAllClothingItems(Expression<Func<Product, bool>> predicate);
        Task<ICollection<Product>> GetAllClothings(Expression<Func<Product, bool>> predicate);
        Task<ICollection<Product>> GetAllAccessories(Expression<Func<Product, bool>> predicate);
        Task<ICollection<Product>> GetAllFootWears(Expression<Func<Product, bool>> predicate);
        Task<ICollection<Product>> GetAllProducts();
        Product Update(Product product);
        bool Delete(Product product);

        Task<ICollection<Product>> GetBy(Expression<Func<Product, bool>> predicate);

    }
}