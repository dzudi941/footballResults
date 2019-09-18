using FR.Domain.Interfaces;
using FR.Domain.Models;
using FR.Infrastructure.Repositories;
using FR.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FR.Api.Services
{
    //public class TeamService
    //{
    //    private readonly IRepository<Team> _teamRepository;

    //    public TeamService(IRepository<Team> teamRepository)
    //    {
    //        _teamRepository = teamRepository;
    //    }

    //    public int AddTeam(string name)
    //    {
    //        Team team = _teamRepository.Find(new TeamSpecification(name)).FirstOrDefault();
    //        if (team != null) return team.Id;

    //        Team newTeam = new Team
    //        {
    //            Name = name
    //        };

    //        _teamRepository.Add(newTeam);
    //        return _teamRepository.Find(new TeamSpecification(name)).First().Id;
    //    }
    //}
}
