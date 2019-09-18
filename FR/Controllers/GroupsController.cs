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
        private TeamService _teamsService;

        public GroupsController(GroupService groupService, ResultsService resultsService, TeamService teamsService)
        {
            _groupsService = groupService;
            _resultsService = resultsService;
            _teamsService = teamsService;
        }

        // GET: api/Groups
        [HttpGet]
        public IEnumerable<GroupViewModel> Get()
        {
            return _groupsService.GetTables();
        }

        // GET: api/Groups/A
        [HttpGet("{groupName}", Name = "Get")]
        public GroupViewModel Get(string groupName)
        {
            return _groupsService.GetTable(groupName);
        }
    }
}