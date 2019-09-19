using System.Collections.Generic;
using FR.Api.Services;
using FR.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly GroupService _groupsService;

        public GroupsController(GroupService groupService)
        {
            _groupsService = groupService;
        }

        // GET: api/Groups
        [HttpGet]
        public ActionResult<IEnumerable<GroupViewModel>> Get()
        {
            return Ok(_groupsService.Get());
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public ActionResult<GroupViewModel> Get(string id)
        {
            var groupVM = _groupsService.Get(id);
            if (groupVM == null) return NotFound("Record not found!");

            return  Ok(groupVM);
        }
    }
}