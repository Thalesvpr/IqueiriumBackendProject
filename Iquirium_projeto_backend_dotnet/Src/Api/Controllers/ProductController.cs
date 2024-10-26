using Microsoft.AspNetCore.Mvc;

namespace Iquirium_projeto_backend_dotnet.Src.Api.Controllers
{
    public class ProductController : Controller
    {

        // GET: ProductController/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
        }

        // POST: ProductController/Create
        [HttpPost]
        public IActionResult Create(IFormCollection collection)
        {
        }


        // POST: ProductController/Edit/5
        [HttpPut]
        public IActionResult Edit(int id, IFormCollection collection)
        {
        }


        // POST: ProductController/Delete/5
        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
        }
    }
}
