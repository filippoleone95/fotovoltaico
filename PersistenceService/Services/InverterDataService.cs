using PersistenceService.Models;
using PersistenceService.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersistenceService.Services
{
    public class InverterDataService
    {
        private readonly InverterDataContext _dbContext;

        public InverterDataService(InverterDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Inserisce un nuovo record nel DB.
        /// </summary>
        public async Task InsertDataAsync(InverterData data)
        {
            _dbContext.InverterDatas.Add(data);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Restituisce tutti i record, in ordine discendente per data di creazione.
        /// </summary>
        public async Task<List<InverterData>> GetAllDataAsync()
        {
            return await _dbContext.InverterDatas
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// Restituisce il record più recente (ordinando per CreatedAt).
        /// </summary>
        public async Task<InverterData?> GetLatestDataAsync()
        {
            return await _dbContext.InverterDatas
                .OrderByDescending(d => d.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}