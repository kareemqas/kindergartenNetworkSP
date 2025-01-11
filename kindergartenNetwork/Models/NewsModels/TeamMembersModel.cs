using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO.News;

namespace kindergartenNetwork.Models.NewsModels
{
    public class TeamMembersModel
    {
        public TeamMembersModel()
        {
            OTeamMember = new TeamMembers();
            LstTeamMembers = new List<TeamMembers>();
        }
        public TeamMembers OTeamMember { get; set; }
        public List<TeamMembers> LstTeamMembers { get; set; }
        public int Count { get; set; }
    }
}