using Demo.Api.Extension_Methods;
using Demo.Data.Data;
using Demo.Data.Models;
using FuzzySharp;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

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
            name = name.Trim();

            if(string.IsNullOrEmpty(name))
            {
                return Enumerable.Empty<Municipality>();
            }


            bool isInEnglsih = name.IsEnglish();

            if (isInEnglsih)
            {
                //get all the municipalities
                IEnumerable<Municipality> municipalities = await _dataContext.Municipalities
                                                                             .AsNoTracking()
                                                                             .ToArrayAsync(cancellationToken);
                
                IEnumerable<string> choices = municipalities.Select(x => x.EnglishName).ToArray();
                var result = Process.ExtractTop(name, choices, cutoff: 66);

                //get the matched strings from result
                var matchedOptions = result.Select(r => r.Value)
                                           .ToHashSet();
                //fetch the result
                var resultQuery = municipalities.Where(e => matchedOptions.Contains(e.EnglishName))
                                                .ToArray();
                return resultQuery;
            }
            //Arabic
             return _dataContext.Municipalities
                                .AsNoTracking()
                                .AsEnumerable()
                                .Where(x => Levenshtein.GetRatio(name, x.ArabicName) >= 0.6666)
                                .ToArray();
            //test test test
            //another final test
            //fsjrjpigerpgijp
            //grgeojjejeoerjerj
            //day 1
            //day 2
        }




    }
}
