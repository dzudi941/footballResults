using HTEC.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTEC.Api.Services
{
    public interface IGroupService
    {
        IEnumerable<GroupViewModel> GetTables();
        GroupViewModel GetTable(string groupName);
    }
}
