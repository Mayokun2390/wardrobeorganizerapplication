using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WardrobeOrganizerApp.Context;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Repositories.Interface;

namespace WardrobeOrganizerApp.Repositories.Implementation
{
    public class ChartBotRepo : IChartBotInterface
    {
        private readonly StoreContext _context;
        public ChartBotRepo(StoreContext context)
        {
            _context = context;
        }
        public async Task<ChartBot> Create(ChartBot chartBot)
        {
            await _context.ChartBots.AddAsync(chartBot);
            return chartBot;
        }

        public bool Delete(ChartBot chartBot)
        {
            _context.ChartBots.Remove(chartBot);
            return true;
        }

        public async Task<ICollection<ChartBot>> GetAllChart()
        {
            var getCharts = await _context.ChartBots.ToListAsync();
            return getCharts;
        }

        public async Task<ChartBot> GetById(Guid id)
        {
            var getId = await _context.ChartBots.Include(c => c.Customer).FirstOrDefaultAsync(c => c.Id == id);
            return getId;
        }

        public ChartBot Update(ChartBot chartBot)
        {
            _context.ChartBots.Update(chartBot);
            return chartBot;
        }
    }
}