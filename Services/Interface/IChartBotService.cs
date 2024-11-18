using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;

namespace WardrobeOrganizerApp.Services.Interface
{
    public interface IChartBotService
    {
        Task<Response<ChartBotResponseModel>> CreateChart (ChartBotRequestModel model);
        Task<Response<ICollection<ChartBotResponseModel>>> GetAll ();
        Task<Response<ChartBotResponseModel>> Update (ChartBotRequestModel model, Guid id);
        Task<Response<ChartBotResponseModel>> Delete (Guid id);
    }
}