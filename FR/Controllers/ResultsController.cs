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
        //private TeamService _teamService;

        public ResultsController(GroupService groupService, ResultsService resultsService/*, TeamService teamService*/)
        {
            _groupService = groupService;
            _resultsService = resultsService;
            //_teamService = teamService;
        }

        // GET: api/Results
        [HttpGet]
        public IEnumerable<ResultViewModel> Get()
        {
            return _resultsService.Get();
        }

        // GET: api/Results/id
        [HttpGet("{id}", Name = "Get")]
        public ResultViewModel Get(int id)
        {
            return _resultsService.Get(id);
        }

        // POST: api/Results
        [HttpPost]
        public IEnumerable<GroupViewModel> Post(List<ResultViewModel> resultsVM)
        {
            foreach (var resultVM in resultsVM)
            {
                int groupId = _groupService.AddGroup(resultVM.Group, resultVM.LeagueTitle);
                //int homeTeamId = _teamService.AddTeam(resultVM.HomeTeam);
                //int awayTeamId = _teamService.AddTeam(resultVM.AwayTeam);

                _resultsService.AddResult(groupId, resultVM);
            }

            return _groupService.GetTables();
        }

        // PUT: api/Results/5
        [HttpPut("{id}")]
        public void Put(int id, ResultViewModel resultVM)
        {
            _resultsService.Update(id, resultVM);
        }

        // POST: api/Results/
        [HttpPost]
        public void PutRange(List<int> ids, List<ResultViewModel> resultsVM)
        {
            _resultsService.Update(ids, resultsVM);
        }

        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _resultsService.Delete(id);
        }

        // POST: api/Results/Filter
        [HttpPost]
        public IEnumerable<GroupViewModel> Filter(FilterViewModel filter)
        {
            return _resultsService.Filter(filter);
        }
    }
}