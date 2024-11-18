using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Enums;
using WardrobeOrganizerApp.Repositories.Interface;
using WardrobeOrganizerApp.Services.Interface;

namespace WardrobeOrganizerApp.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductInterface _productInterface;
        private readonly IUnitOfWork _unitofwork;
        private readonly ICurrentUser _currentUser;
        private readonly IUserInterface _userInterface;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(IProductInterface productInterface, IUnitOfWork unitofwork, IWebHostEnvironment webHostEnvironment, ICurrentUser currentUser, IUserInterface userInterface)
        {
            _productInterface = productInterface;
            _unitofwork = unitofwork;
            _currentUser = currentUser;
            _webHostEnvironment = webHostEnvironment;
            _userInterface = userInterface;
        }
        public async Task<Response<ProductResponseModel>> CreateProduct(ProductRequestModel model)
        {
            var exist = await _productInterface.GetBy(m => m.Name == model.Name);
            if (exist != null)
            {
                // exist.Quantity += model.Quantity;
                // _productInterface.Update(exist);
                _unitofwork.SaveChanges();
            }

            var current = _userInterface.GetUserAsync(x => x.Email == _currentUser.GetCurrentUser());
            var createProduct = new Product
            {
                Name = model.Name,
                Quantity = model.Quantity,
                Price = model.Price,
                Picture = SaveImage(model.Picture),
                Category = model.Category,
            };
            await _productInterface.Create(createProduct);
            _unitofwork.SaveChanges();
            return new Response<ProductResponseModel>
            {
                Message = "Product Created Successfully",
                Status = true,
            };
        }


        public async Task<Response<ProductResponseModel>> Delete(Guid id)
        {
            var pro = await _productInterface.GetById(id);
            if (pro == null)
            {
                return new Response<ProductResponseModel>
                {
                    Message = "Product not found",
                    Status = false,
                };
            }
            _productInterface.Delete(pro);
            _unitofwork.SaveChanges();
            return new Response<ProductResponseModel>
            {
                Message = "Product deleted Successfully",
                Status = true,
            };
        }

        public async Task<Response<ProductResponseModel>> Get(Guid id)
        {
            var product = await _productInterface.GetById(id);
            if (product == null)
            {
                return new Response<ProductResponseModel>
                {
                    Message = "Product not found",
                    Status = false,
                };
            }
            var getProduct = new ProductResponseModel
            {
                Quantity = product.Quantity,
                Name = product.Name,
                Id = product.Id,
                Price = product.Price,
                Picture = product.Picture,
                Category = product.Category,
            };
            return new Response<ProductResponseModel>
            {
                Message = "Product Exist",
                Status = true,
                Value = getProduct,
            };
        }

        public async Task<Response<ICollection<ProductResponseModel>>> GetAllAccessories()
        {
            var access = await _productInterface.GetAllAccessories(x => x.Category == Category.Accessories);
            if(access == null){
                return new Response<ICollection<ProductResponseModel>>{
                    Message = "No accessories items found",
                    Status = false,
                };
            }
            var getAccess = access.Select(x => new ProductResponseModel
            {
                Quantity = x.Quantity,
                Name = x.Name,
                Id = x.Id,
                Price = x.Price,
                Picture = x.Picture,
                Category = x.Category,
            }).ToList();

            return new Response<ICollection<ProductResponseModel>>
            {
                Message = "List of accessories",
                Status = true,
                Value = getAccess,
            };
        }

        public async Task<Response<ICollection<ProductResponseModel>>> GetAllClothingItems()
        {
            var items = await _productInterface.GetAllClothingItems(x => x.Category == Category.ClothingItems);
            if(items == null){
                return new Response<ICollection<ProductResponseModel>>{
                    Message = "No clothing items found",
                    Status = false,
                };
            }
            var getItem = items.Select(x => new ProductResponseModel
            {
                Quantity = x.Quantity,
                Name = x.Name,
                Id = x.Id,
                Price = x.Price,
                Picture = x.Picture,
                Category = x.Category,
            }).ToList();

            return new Response<ICollection<ProductResponseModel>>
            {
                Message = "List of Items",
                Status = true,
                Value = getItem,
            };
        }

        public async Task<Response<ICollection<ProductResponseModel>>> GetAllClothings()
        {
            var cloth = await _productInterface.GetAllClothings(x => x.Category == Category.Clothing);
            if(cloth == null){
                return new Response<ICollection<ProductResponseModel>>{
                    Message = "No clothing items found",
                    Status = false,
                };
            }
            var getClothes = cloth.Select(x => new ProductResponseModel
            {
                Quantity = x.Quantity,
                Name = x.Name,
                Id = x.Id,
                Price = x.Price,
                Picture = x.Picture,
                Category = x.Category,
            }).ToList();

            return new Response<ICollection<ProductResponseModel>>
            {
                Message = "List of clothing",
                Status = true,
                Value = getClothes,
            };
        }

        public async Task<Response<ICollection<ProductResponseModel>>> GetAllFootWears()
        {
            var footwear = await _productInterface.GetAllFootWears(x => x.Category == Category.FootWear);
            var getFootWear = footwear.Select(x => new ProductResponseModel
            {
                Quantity = x.Quantity,
                Name = x.Name,
                Id = x.Id,
                Price = x.Price,
                Picture = x.Picture,
            }).ToList();

            return new Response<ICollection<ProductResponseModel>>
            {
                Message = "List of FootWear",
                Status = true,
                Value = getFootWear,
            };
        }

        public async Task<Response<ICollection<ProductResponseModel>>> GetAllOutfits()
        {
            var outfits = await _productInterface.GetAllOutfits(x => x.Category == Category.Outfits);
            var getOutfits = outfits.Select(x => new ProductResponseModel
            {
                Quantity = x.Quantity,
                Name = x.Name,
                Id = x.Id,
                Price = x.Price,
                Picture = x.Picture,
            }).ToList();

            return new Response<ICollection<ProductResponseModel>>
            {
                Message = "List of OutFits",
                Status = true,
                Value = getOutfits,
            };
        }

        public async Task<Response<ICollection<ProductResponseModel>>> GetAllProducts()
        {
            var product = await _productInterface.GetAllProducts();
            var getProducts = product.Select(x => new ProductResponseModel
            {
                Quantity = x.Quantity,
                Name = x.Name,
                Id = x.Id,
                Price = x.Price,
                Picture = x.Picture,
                Category = x.Category,
            }).ToList();

            return new Response<ICollection<ProductResponseModel>>
            {
                Message = "List of Products",
                Status = true,
                Value = getProducts,
            };
        }

        public async Task<Response<ProductResponseModel>> Update(ProductRequestModel model, Guid id)
        {
            var prod = await _productInterface.GetById(id);
            if (prod == null)
            {
                return new Response<ProductResponseModel>
                {
                    Message = "Product not found",
                    Status = false,
                };
            }
            var updateProduct = new Product();
            prod.Quantity = model.Quantity;
            prod.Name = model.Name;
            prod.Price = model.Price;
            prod.Category = model.Category;
            prod.Picture = SaveImage(model.Picture);
            _productInterface.Update(updateProduct);
            _unitofwork.SaveChanges();
            return new Response<ProductResponseModel>
            {
                Message = "Product Updated",
                Status = true,
            };
        }

        private string SaveImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File cannot be null or empty", nameof(file));

            var baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            if (!Directory.Exists(baseDirectory))
            {
                Directory.CreateDirectory(baseDirectory);
            }

            var filePath = Path.Combine(baseDirectory, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            return filePath;
        }



    }
}