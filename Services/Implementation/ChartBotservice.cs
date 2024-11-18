using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Repositories.Interface;
using WardrobeOrganizerApp.Services.Interface;
using ZstdSharp.Unsafe;

namespace WardrobeOrganizerApp.Services.Implementation
{
    public class ChartBotservice : IChartBotService
    {
        private readonly IChartBotInterface _chartBotInterface;
        private readonly ICustomerInterface _customerInterface;
        private readonly IUnitOfWork _unitofwork;
        public ChartBotservice(IChartBotInterface chartBotInterface, ICustomerInterface customerInterface, IUnitOfWork unitofwork)
        {
            _chartBotInterface = chartBotInterface;
            _customerInterface = customerInterface;
            _unitofwork = unitofwork;
        }
        public Task<Response<ChartBotResponseModel>> CreateChart(ChartBotRequestModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<ChartBotResponseModel>> Delete(Guid id)
        {
            var chart = await _chartBotInterface.GetById(id);
            if (chart == null)
            {
                return new Response<ChartBotResponseModel>
                {
                    Message = "Chart not found",
                    Status = false,
                };
            }
            _chartBotInterface.Delete(chart);
            _unitofwork.SaveChanges();
            return new Response<ChartBotResponseModel>
            {
                Message = "Chart deleted Successfully",
                Status = true,
            };
        }

        public async Task<Response<ICollection<ChartBotResponseModel>>> GetAll()
        {
            var charts = await _chartBotInterface.GetAllChart();
            var getCharts = charts.Select(x => new ChartBotResponseModel{  
                MessageText = x.MessageText,
                ResponseText = x.ResponseText,
                // DateTime = x.DateTime.UtcNow,
            }).ToList();

            return new Response<ICollection<ChartBotResponseModel>>
            {
                Value = getCharts,
                Message = "List of Charts",
                Status = true,
            };
        }
        public async Task<Response<ChartBotResponseModel>> Update(ChartBotRequestModel model, Guid id)
        {
            var chartbot = await _chartBotInterface.GetById(id);
            if (chartbot == null)
            {
                return new Response<ChartBotResponseModel>
                {
                    Message = "Chart not found",
                    Status = false,
                };
            }
            var updateChart = new ChartBot();
            chartbot.MessageText = model.MessageText;
            chartbot.ResponseText = model.ResponseText;
            _chartBotInterface.Update(updateChart);
            _unitofwork.SaveChanges();
            return new Response<ChartBotResponseModel>
            {
                Message = "Chart Updated",
                Status = true,
            };
        }
    }
}