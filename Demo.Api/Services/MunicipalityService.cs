using Demo.Api.Extension_Methods;
using Demo.Data.Data;
using Demo.Data.Models;
using FuzzySharp;
using FuzzySharp.SimilarityRatio.Scorer;
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

            if(string.IsNullOrEmpty(name))
            {
                return Enumerable.Empty<Municipality>();
            }


            IEnumerable<string> choices = Enumerable.Empty<string>();
            bool isInEnglsih = name.IsEnglish();

            if (isInEnglsih)
            {
                choices = await _dataContext.Municipalities.Select(x => x.EnglishName).ToArrayAsync(cancellationToken);
            }

            //Arabic
            else
            {
                choices = await _dataContext.Municipalities.Select(x => x.ArabicName).ToArrayAsync(cancellationToken);
            }

            var result = Process.ExtractTop(name, choices, null, null, 15, 75);
            var matchedOptions = result.Select(r => r.Value).ToArray();
            var resultQuery = Array.Empty<Municipality>();


            if(isInEnglsih)
            {
               resultQuery = _dataContext.Municipalities
                                         .Where(e => matchedOptions.Contains(e.EnglishName))
                                         .ToArray();
            }

            else
            {
              resultQuery = _dataContext.Municipalities
                                        .Where(e => matchedOptions.Contains(e.ArabicName))
                                        .ToArray();
            }


            return resultQuery;

        }




    }
}
