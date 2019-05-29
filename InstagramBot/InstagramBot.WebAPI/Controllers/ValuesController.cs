using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramBot.DB;
using Microsoft.AspNetCore.Mvc;

namespace InstagramBot.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public ValuesController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return _apiContext.Users.FirstOrDefault().Login;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
