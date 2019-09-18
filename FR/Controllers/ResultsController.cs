using System.Collections.Generic;
using FR.Api.Services;
using FR.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private GroupService _groupService;
        private ResultsService _resultsService;
        private TeamService _teamService;

        public ResultsController(GroupService groupService, ResultsService resultsService, TeamService teamService)
        {
            _groupService = groupService;
            _resultsService = resultsService;
            _teamService = teamService;
        }

        // GET: api/Results
        [HttpGet]
        public IEnumerable<GroupViewModel> Get()
        {
            return _groupService.GetTables();
        }

        // GET: api/Results/A
        [HttpGet("{groupName}", Name = "Get")]
        public GroupViewModel Get(string groupName)
        {
            return _groupService.GetTable(groupName);
        }

        // POST: api/Results
        [HttpPost]
        public IEnumerable<GroupViewModel> Post(List<ResultViewModel> resultsVM)
        {
            foreach (var resultVM in resultsVM)
            {
                int groupId = _groupService.AddGroup(resultVM.Group, resultVM.LeagueTitle);
                int homeTeamId = _teamService.AddTeam(resultVM.HomeTeam);
                int awayTeamId = _teamService.AddTeam(resultVM.AwayTeam);

                _resultsService.AddResult(groupId, homeTeamId, awayTeamId, resultVM);
            }

            return _groupService.GetTables();
        }

        // PUT: api/Results/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET: api/Results/A
        [HttpGet("{startDate/endDate/groupName/teamName}", Name = "Filter")]
        public IEnumerable<GroupViewModel> Filter(string startDate, string endDate, string groupName, string teamName)
        {
            //return _groupService.GetTable(groupName);
            return null;
        }
    }
}
