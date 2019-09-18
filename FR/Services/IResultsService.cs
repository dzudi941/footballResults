using FR.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FR.Api.Services
{
    public interface IResultsService
    {
        void AddRange(IEnumerable<ResultViewModel> results);
    }
}
