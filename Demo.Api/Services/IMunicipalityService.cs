using Demo.Data.Models;

namespace Demo.Api.Services
{
    public interface IMunicipalityService
    {
        Task<IEnumerable<Municipality>> SearchByNameAsync(string name , CancellationToken cancellationToken);
    }
}
