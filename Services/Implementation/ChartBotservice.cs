using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Repositories.Interface;
using WardrobeOrganizerApp.Services.Interface;

namespace WardrobeOrganizerApp.Services.Implementation
{
    public class ChartBotservice : IChartBotService
    {
        private readonly IChartBotInterface _chartBotInterface;
        private readonly ICustomerInterface _customerInterface;
        public ChartBotservice(IChartBotInterface chartBotInterface, ICustomerInterface _customerInterface)
        {
            _chartBotInterface = chartBotInterface;
            _customerInterface = _customerInterface;
        }
        public Task<Response<ChartBotResponseModel>> CreateChart(ChartBotRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ChartBotResponseModel>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ICollection<ChartBotResponseModel>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Response<ChartBotResponseModel>> Update(ChartBotRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}