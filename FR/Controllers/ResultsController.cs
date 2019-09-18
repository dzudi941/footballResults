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
        public ActionResult<IEnumerable<ResultViewModel>> Get()
        {
            return Ok(_resultsService.Get());
        }

        [HttpGet("{id}", Name = "Get")]
        public ActionResult<ResultViewModel> Get(int id)
        {
            var resultVM = _resultsService.Get(id);
            if (resultVM == null) return NotFound("Record not found!");

            return Ok(resultVM);
        }

        // POST: api/Results
        [HttpPost]
        public ActionResult<IEnumerable<GroupViewModel>> Post(List<ResultViewModel> resultsVM)
        {
            foreach (var resultVM in resultsVM)
            {
                int groupId = _groupService.AddGroup(resultVM.Group, resultVM.LeagueTitle);

                _resultsService.AddResult(groupId, resultVM);
            }

            return Ok(_groupService.Get());
        }

        // PUT: api/Results/5
        [HttpPut("{id}")]
        public void Put(int id, ResultViewModel resultVM)
        {
            _resultsService.Update(id, resultVM);
        }

        //// POST: api/Results/
        //[HttpPost]
        //public void PutRange(List<int> ids, List<ResultViewModel> resultsVM)
        //{
        //    _resultsService.Update(ids, resultsVM);
        //}

        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _resultsService.Delete(id);
        }

        // POST: api/Results/Filter
        //[Route("api/[controller]/Filter")]
        [HttpPost("Filter")]
        public ActionResult<IEnumerable<ResultViewModel>> Filter(FilterViewModel filter)
        {
            return Ok(_resultsService.Filter(filter));
        }
    }
}