using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Repositories.Interface;
using WardrobeOrganizerApp.Services.Interface;

namespace WardrobeOrganizerApp.Services.Implementation
{
    public class OutfitsService : IOutfitsService
    {
        private readonly IOutfitsInterface _outfitsInterface;
        private readonly IUnitOfWork _unitofwork;        
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OutfitsService(IOutfitsInterface outfitsInterface, IUnitOfWork unitofwork, IWebHostEnvironment webHostEnvironment)
        {
            _outfitsInterface = outfitsInterface;
            _unitofwork = unitofwork;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Response<OutfitsResponseModel>> Create(OutfitsRequestModel model)
        {
            var create = await _outfitsInterface.GetById(model.Id);
            if (create == null)
            {
                return new Response<OutfitsResponseModel>
                {
                    Message = "Outfits not found",
                    Status = false,
                };
            }
            var outfit = new Outfits
            {
                Id = model.Id,
                Occasion = model.Occasion,
               ImageUrl = SaveImage(model.ImageUrl),
            };
           await  _outfitsInterface.CreateOutfit(outfit);
           _unitofwork.SaveChanges();
           return new Response<OutfitsResponseModel>
           {
              Message = "Outfits created successfully",
              Status = true,
           };
        }

        public async Task<Response<OutfitsResponseModel>> Delete(Guid id)
        {
            var outfits = await _outfitsInterface.GetById(id);
            if (outfits == null)
            {
                return new Response<OutfitsResponseModel>
                {
                    Message = "Outfits not found",
                    Status = false,
                };
            }
            _outfitsInterface.Delete(outfits);
            _unitofwork.SaveChanges();
            return new Response<OutfitsResponseModel>
            {
                Message = "Outfits deleted Successfully",
                Status = true,
            };
        }

        public async Task<Response<OutfitsResponseModel>> Get(Guid id)
        {
            var outfit = await _outfitsInterface.GetById(id);
            if (outfit == null)
            {
                return new  Response<OutfitsResponseModel>
                {
                    Message = "Outfits not found",
                    Status = false,
                };
            }
            var getOutfits = new OutfitsResponseModel
            {
                Occasion = outfit.Occasion,
                ImageUrl = outfit.ImageUrl,
            };
            return new Response<OutfitsResponseModel>
            {
                Message = "Outfits Exist",
                Status = true,
                Value = getOutfits,
            };
        }

        public async Task<Response<ICollection<OutfitsResponseModel>>> GetAllOutfits()
        {
            var outf = await _outfitsInterface.GetAllOutfit();
            var outfitss = outf.Select(x => new OutfitsResponseModel{  
                Occasion = x.Occasion,
                ImageUrl = x.ImageUrl,
            }).ToList();

            return new Response<ICollection<OutfitsResponseModel>>
            {
                Value = outfitss,
                Message = "List of Outfits",
                Status = true,
            };
        }

        public async Task<Response<OutfitsResponseModel>> Update(OutfitsRequestModel model, Guid id)
        {
            var fits = await _outfitsInterface.GetById(id);
            if (fits == null)
            {
                return new Response<OutfitsResponseModel>
                {
                    Message = "Outfit not found",
                    Status = false,
                };
            }
            var updateOutfit = new Outfits();
            fits.Occasion = model.Occasion;
            fits.ImageUrl = SaveImage(model.ImageUrl);

            _outfitsInterface.Update(updateOutfit);
            _unitofwork.SaveChanges();
            return new Response<OutfitsResponseModel>
            {
                Message = "Outfits Updated",
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