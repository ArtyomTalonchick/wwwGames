using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wwwGames.Models;

namespace wwwGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        ContextDb db;

        public UserApiController(ContextDb context)
        {
            db = context;
        }

        [HttpGet]
        public ICollection<User> GetAll(int? teamId)
        {
            ICollection<User> users;
            if (teamId == null)
            {
                users = db.Users.ToList();
            }
            else
            {
                users = db.Teams.Include(t => t.Users).FirstOrDefault(t => t.Id == teamId).Users;
            }

            return users;
        }
    }
}