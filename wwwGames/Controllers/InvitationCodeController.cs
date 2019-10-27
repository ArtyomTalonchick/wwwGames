using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wwwGames.Models;

namespace wwwGames.Controllers
{
    [Authorize(Roles = "teamLead")]
    public class InvitationCodeController : Controller
    {
        ContextDb db;

        public InvitationCodeController(ContextDb context)
        {
            db = context;
        }

        [HttpGet]
        public List<InvitationCode> All()
        {
            return db.InvitationCodes.ToList();
        }

        [HttpPost]
        public InvitationCode Create(int teamId, string code)
        {
            InvitationCode duplicationCode = db.InvitationCodes.FirstOrDefault(c => c.Code == code);
            if (code != "" && duplicationCode == null)
            {
                Team team = db.Teams.FirstOrDefault(t => t.Id == teamId);
                InvitationCode invitationCode = new InvitationCode {Team = team, Code = code };
                db.InvitationCodes.Add(invitationCode);
                db.SaveChanges();
                return invitationCode;
            } else
            {
                return null;
            }
        }


        [HttpGet]
        public bool Remove(int id)
        {
            InvitationCode invitationCode = db.InvitationCodes.FirstOrDefault(c => c.Id == id);
            if (invitationCode != null)
            {
                db.InvitationCodes.Remove(invitationCode);
                db.SaveChanges();
                return true;
            } else
            {
                return false;
            }
        }
    }
}