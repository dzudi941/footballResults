using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FR.Api.Services;
using FR.Api.ViewModels;
using FR.Domain.Interfaces;
using FR.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private GroupService _groupsService;
        private ResultsService _resultsService;
        //private TeamService _teamsService;

        public GroupsController(GroupService groupService, ResultsService resultsService/*, TeamService teamsService*/)
        {
            _groupsService = groupService;
            _resultsService = resultsService;
            //_teamsService = teamsService;
        }

        // GET: api/Groups
        [HttpGet]
        public ActionResult<IEnumerable<GroupViewModel>> Get()
        {
            return Ok(_groupsService.Get());
        }


        [HttpGet("{id}")]
        public ActionResult<GroupViewModel> Get(string id)
        {
            var groupVM = _groupsService.Get(id);
            if (groupVM == null) return NotFound("Record not found!");

            return  Ok(groupVM);
        }
    }
}