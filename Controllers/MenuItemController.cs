using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuItemController : ControllerBase
    {
        private readonly ILogger<MenuItemController> _logger;
        private readonly AppDbContext _dbc = new AppDbContext();

        public MenuItemController(ILogger<MenuItemController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<MenuItem> Get()
        {
            return _dbc.MenuItems.ToList();
        }

        [HttpGet("{id}")]
        public MenuItem Get(int id)
        {
            return _dbc.MenuItems.Find(id);
        }

        [HttpPost]
        public void Post([FromBody] MenuItem value)
        {
            int id = _dbc.MenuItems.AsEnumerable().Last().Id + 1;

            MenuItem mi = new MenuItem { 
                Id = id,
                Name = value.Name,
                Price = value.Price,
                PhotoPath = value.PhotoPath
            };

            _dbc.MenuItems.Add(mi);
            _dbc.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] MenuItem value)
        {
            MenuItem mi = _dbc.MenuItems.Where(m => m.Id == id)
                                       .FirstOrDefault();

            if (mi != null)
            {
                mi.Name = value.Name;
                mi.Price = value.Price;

                _dbc.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            MenuItem mi = _dbc.MenuItems.Find(id);
            _dbc.MenuItems.Remove(mi);
            _dbc.SaveChanges();
        }
    }
}
