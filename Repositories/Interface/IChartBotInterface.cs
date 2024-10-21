using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Repositories.Interface
{
    public interface IChartBotInterface
    {
        Task<ChartBot> Create(ChartBot chartBot);
        Task<ChartBot> GetById(Guid id);
        Task<ICollection<ChartBot>> GetAllChart();
        ChartBot Update(ChartBot chartBot);
        bool Delete(ChartBot chartBot);
    }
}