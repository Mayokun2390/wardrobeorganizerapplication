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
    public class ClothingItemsService : IClothingItemService
    {
        private readonly IClothingItemInterface _clothingItemInterface;
        private readonly IUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClothingItemsService(IClothingItemInterface clothingItemInterface, IUnitOfWork unitofwork,IWebHostEnvironment webHostEnvironment)
        {
            _clothingItemInterface = clothingItemInterface;
            _unitofwork = unitofwork;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Response<ClothingItemResponseModel>> Create(ClothingItemRequestModel model)
        {
            var create = await _clothingItemInterface.GetById(model.Id);
            if (create == null)
            {
                return new Response<ClothingItemResponseModel>
                {
                    Message = "ClothingItem not found",
                    Status = false,
                };
            }
            var clothing = new ClothingItems
            {
                Id = model.Id,
               ImageUrl = SaveImage(model.ImageUrl),
                Brand = model.Brand,
                Name = model.Name,
                Price = model.Price,
                Category = model.Category,
                Season = model.Season,
            };
            await  _clothingItemInterface.Create(clothing);
           _unitofwork.SaveChanges();
           return new Response<ClothingItemResponseModel>
           {
              Message = "ClothingItems created successfully",
              Status = true,
           };
        }

        public async Task<Response<ClothingItemResponseModel>> Delete(Guid id)
        {
            var clothes = await _clothingItemInterface.GetById(id);
            if (clothes == null)
            {
                return new Response<ClothingItemResponseModel>
                {
                    Message = "ClothingItems not found",
                    Status = false,
                };
            }
            _clothingItemInterface.Delete(clothes);
            _unitofwork.SaveChanges();
            return new Response<ClothingItemResponseModel>
            {
                Message = "ClothingItems deleted Successfully",
                Status = true,
            };
        }

        public async Task<Response<ClothingItemResponseModel>> Get(Guid id)
        {
            var items = await _clothingItemInterface.GetById(id);
            if (items == null)
            {
                return new  Response<ClothingItemResponseModel>
                {
                    Message = "ClothingItems not found",
                    Status = false,
                };
            }
            var getitems = new ClothingItemResponseModel
            {
                Name = items.Name,
                Brand = items.Brand,
                Price = items.Price,
                Season = items.Season,
                ImageUrl = items.ImageUrl,
            };
            return new Response<ClothingItemResponseModel>
            {
                Message = "ClothingItems Exist",
                Status = true,
                Value = getitems,
            };
        }

        public async Task<Response<ICollection<ClothingItemResponseModel>>> GetAllClothings()
        {
            var outf = await _clothingItemInterface.GetAllClothingItems();
            var outfitss = outf.Select(x => new ClothingItemResponseModel{  
                Name = x.Name,
                Brand = x.Brand,
                Price = x.Price,
                Season = x.Season,
                ImageUrl = x.ImageUrl,
            }).ToList();

            return new Response<ICollection<ClothingItemResponseModel>>
            {
                Value = outfitss,
                Message = "List of ClothingItems",
                Status = true,
            };
        }

        public async Task<Response<ClothingItemResponseModel>> Update(ClothingItemRequestModel model, Guid id)
        {
           var item = await _clothingItemInterface.GetById(id);
            if (item == null)
            {
                return new Response<ClothingItemResponseModel>
                {
                    Message = "ClothingItems not found",
                    Status = false,
                };
            }
            var updateItems = new ClothingItems();
            item.Brand = model.Brand;
            item.Name = model.Name;
            item.Price = model.Price;
            item.Category = model.Category;
            item.ImageUrl = SaveImage(model.ImageUrl);
            _clothingItemInterface.Update(updateItems);
            _unitofwork.SaveChanges();
            return new Response<ClothingItemResponseModel>
            {
                Message = "ClothingItems Updated",
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