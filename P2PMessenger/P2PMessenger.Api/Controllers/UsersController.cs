using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace P2PMessenger.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        public static readonly List<string> Users = new List<string>();

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Users;
        }

        [HttpPost]
        public void Post(string value)
        {
            if(value != null)
                Users.Add(value);
        }

        [HttpDelete]
        public void Delete()
        {
            Users.Clear();
        }
    }
}
