using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace owntrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwntrackController : ControllerBase
    {
        [HttpGet]
        public string Get() {
            return "Test erfolgreich";
        }
        
        [HttpPost]
        public void Post([FromBody] string value) {

        }        
    }
}
