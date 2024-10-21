using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Entities;
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

        public ProductService(IProductInterface productInterface, IUnitOfWork unitofwork, ICurrentUser currentUser, IWebHostEnvironment webHostEnvironment)
        {
            _productInterface = productInterface;
            _unitofwork = unitofwork;
            _currentUser = currentUser;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Response<ProductResponseModel>> CreateProduct(ProductRequestModel model)
        {
            var current = _userInterface.GetUserAsync(x => x.Email == _currentUser.GetCurrentUser());
            var createProduct = new Product{
                Name = model.Name,
                Quantity = model.Quantity,
                Price = model.Price,
               ImageUrl = SaveImage(model.ImageUrl),
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
                return new  Response<ProductResponseModel>
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
                ImageUrl = product.ImageUrl,
                Category = product.Category,
            };
            return new Response<ProductResponseModel>
            {
                Message = "Product Exist",
                Status = true,
                Value = getProduct,
            };
        }

        public async Task<Response<ICollection<ProductResponseModel>>> GetAllProducts()
        {
            var product = await _productInterface.GetAllProducts();
            var getProducts = product.Select(x => new ProductResponseModel{  
                Quantity = x.Quantity,
                Name = x.Name,
                Id = x.Id,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
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
            prod.ImageUrl = SaveImage(model.ImageUrl);
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
                return string.Empty;

            var uploadDir = "uploads";
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, uploadDir);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var uniqueFileName = Guid.NewGuid().ToString().Substring(0, 5) + "_" + file.FileName;
            var fullPath = Path.Combine(filePath, uniqueFileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return $"/{uploadDir}/{uniqueFileName}";
        }
    }
}