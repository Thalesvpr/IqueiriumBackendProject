using Microsoft.AspNetCore.Mvc;


namespace IqueiriumBackendProject.Src.Api.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/product
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/product/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/product
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/product/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
