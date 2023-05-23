using Demo.Data.Data;
using Demo.Data.Models;
using FuzzySharp;
using Microsoft.EntityFrameworkCore;

namespace Demo.Api.Services
{
    public class MunicipalityService : IMunicipalityService
    {
        private readonly DataContext _dataContext;

        public MunicipalityService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Municipality>> SearchByNameAsync(string name , CancellationToken cancellationToken)
        {

            
        }




    }
}
