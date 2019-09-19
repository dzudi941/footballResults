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

        public ResultsController(GroupService groupService, ResultsService resultsService)
        {
            _groupService = groupService;
            _resultsService = resultsService;
        }

        // GET: api/Results
        [HttpGet]
        public ActionResult<IEnumerable<ResultViewModel>> Get()
        {
            return Ok(_resultsService.Get());
        }

        // GET: api/Results/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<ResultViewModel> Get(int id)
        {
            var resultVM = _resultsService.Get(id);
            if (resultVM == null) return NotFound("Record not found!");

            return Ok(resultVM);
        }

        // POST: api/Results
        [HttpPost]
        public ActionResult<IEnumerable<GroupViewModel>> Post(ResultViewModel resultVM)
        {
            int groupId = _groupService.AddGroup(resultVM.Group, resultVM.LeagueTitle);
            bool successfullyAdded = _resultsService.AddResult(groupId, resultVM) > 0;
            var groupsVM = _groupService.Get();

            return successfullyAdded ? (ActionResult)Ok(groupsVM) : BadRequest();
        }

        // POST: api/Results/Multiple
        [HttpPost("Multiple")]
        public ActionResult<IEnumerable<GroupViewModel>> Post(List<ResultViewModel> resultsVM)
        {
            int totalAddedResults = 0;
            foreach (var resultVM in resultsVM)
            {
                int groupId = _groupService.AddGroup(resultVM.Group, resultVM.LeagueTitle);
                totalAddedResults += _resultsService.AddResult(groupId, resultVM);
            }

            var groupsVM = _groupService.Get();
            bool allAdded = totalAddedResults == resultsVM.Count;

            return allAdded ? (ActionResult)Ok(groupsVM) : BadRequest();
        }

        // PUT: api/Results/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, ResultViewModel resultVM)
        {
            bool updated = _resultsService.Update(id, resultVM);

            return updated ? (ActionResult)Ok() : BadRequest();
        }

        // POST: api/Results/PutMultiple
        [HttpPost("PutMultiple")]
        public ActionResult PutMultiple(List<ResultViewModel> resultsVM)
        {
            bool updated = _resultsService.Update(resultsVM);

            return updated ? (ActionResult)Ok() : BadRequest();
        }

        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (_resultsService.Delete(id))
                return Ok();
            else
                return BadRequest();
        }

        // POST: api/Results/Filter
        [HttpPost("Filter")]
        public ActionResult<IEnumerable<ResultViewModel>> Filter(FilterViewModel filter)
        {
            return Ok(_resultsService.Filter(filter));
        }
    }
}