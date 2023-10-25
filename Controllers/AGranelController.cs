using ExampleAGAPI.Datos;
using ExampleAGAPI.Models;
using ExampleAGAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleAGAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AGranelController : ControllerBase
    {
        private readonly ILogger<AGranelController> _logger;
        private readonly ApplicationDbContext _context;
        public AGranelController(ILogger<AGranelController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<AGranelDTO>> GetAllProductos() 
        {
            _logger.LogInformation("Obtener los productos");
            return Ok(_context.Productos.ToList());
        }

        [HttpGet ("id:int", Name = "GetAGranel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AGranelDTO> GetProducto(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer producto con el id" + id);
                return BadRequest();
            }

            //var agranel = AGranelStore.aGranelList.FirstOrDefault(a => a.Id == id);
            var product = _context.Productos.FirstOrDefault(p=> p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AGranelDTO> CrearProducto([FromBody] AGranelDTO agranelDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(_context.Productos.FirstOrDefault(p=>p.Nombre.ToLower()==agranelDTO.Nombre.ToLower())!=null)
            {
                ModelState.AddModelError("Nombre Existe", "El producto con ese nombre ya existe");
                return BadRequest(ModelState);
            }
            if(agranelDTO == null)
            {
                return BadRequest(agranelDTO);
            }
            
            AGranel model = new()
            {
                Nombre = agranelDTO.Nombre,
                Detalle = agranelDTO.Detalle,
                ImageUrl = agranelDTO.ImageUrl,
                Ocupantes = agranelDTO.Ocupantes,
                Metros = agranelDTO.Metros,
                Tarifa = agranelDTO.Tarifa,
                Amenidad = agranelDTO.Amenidad,
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now,
            };
            _context.Productos.Add(model);
            _context.SaveChanges();
            return CreatedAtRoute("GetAGranel", new {id=model.Id}, agranelDTO);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProducto(int id) 
        { 
            if(id == 0)
            {
                return BadRequest();
            }
            var agranel = _context.Productos.FirstOrDefault(a => a.Id == id);
            if(agranel == null)
            {
                return NotFound();
            }
            _context.Productos.Remove(agranel);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProducto(int id, [FromBody] AGranelDTO agranelDTO)
        {
            if(agranelDTO == null)
            {
                return BadRequest();
            }
            var agranel = _context.Productos.FirstOrDefault(a => a.Id == id);
            if (agranel != null)
            {
                agranel.Nombre = agranelDTO.Nombre;
                agranel.Detalle = agranelDTO.Detalle;
                agranel.ImageUrl = agranelDTO.ImageUrl;
                agranel.Ocupantes = agranelDTO.Ocupantes;
                agranel.Metros = agranelDTO.Metros;
                agranel.Tarifa = agranelDTO.Tarifa;
                agranel.Amenidad = agranelDTO.Amenidad;
                agranel.FechaActualizacion = DateTime.Now;
                _context.Productos.Update(agranel);
                _context.SaveChanges();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
