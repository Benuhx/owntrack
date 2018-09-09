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
        // Just for testing
        [HttpGet]
        public string Get() {
            return "Server is active";
        }
        
        // Owntracks sends HTTP Post Request with the schema defined in OwntrackData.cs
        // Owntrack Docs: https://owntracks.org/booklet/tech/json/
        [HttpPost]
        public void Post([FromBody] OwntrackData owntrackData) {
            
        }        
    }
}