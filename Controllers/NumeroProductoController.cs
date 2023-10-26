using AutoMapper;
using ExampleAGAPI.Datos;
using ExampleAGAPI.Models;
using ExampleAGAPI.Models.DTO;
using ExampleAGAPI.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ExampleAGAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroProductoController : ControllerBase
    {
        private readonly ILogger<NumeroProductoController> _logger;
        private readonly IAGranelRepository _agranelRepo;
        private readonly INumeroProductoRepository _numeroProductoRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public NumeroProductoController(ILogger<NumeroProductoController> logger, IAGranelRepository agranelRepo, INumeroProductoRepository numeroProductoRepo, IMapper mapper)
        {
            _logger = logger;
            _agranelRepo = agranelRepo;
            _numeroProductoRepo = numeroProductoRepo;
            _mapper = mapper;
            _response = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroProductos() 
        {
            try
            {
                _logger.LogInformation("Obtener los numeros productos");

                IEnumerable<NumeroProducto> numeroProductoList = await _numeroProductoRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<NumeroProductoDTO>>(numeroProductoList);
                _response.statusCode = HttpStatusCode.OK;
                _response.IsExitoso = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
            
        }

        [HttpGet ("id:int", Name = "GetNumeroProducto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroProducto(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer numero producto con el id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                //var agranel = AGranelStore.aGranelList.FirstOrDefault(a => a.Id == id);
                var numeroProduct = await _numeroProductoRepo.Obtener(p => p.NroProducto == id);

                if (numeroProduct == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<NumeroProductoDTO>(numeroProduct);
                _response.statusCode = HttpStatusCode.OK;
                _response.IsExitoso = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearNumeroProducto([FromBody] NumeroProductoCreateDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _numeroProductoRepo.Obtener(p => p.NroProducto == createDTO.NroProducto) != null)
                {
                    ModelState.AddModelError("Numero Existe", "El numero de producto ya existe");
                    return BadRequest(ModelState);
                }
                if(await _agranelRepo.Obtener(a=>a.Id==createDTO.IdProducto)==null)
                {
                    ModelState.AddModelError("Foreign Key", "El Id de producto no existe");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                NumeroProducto model = _mapper.Map<NumeroProducto>(createDTO);
                model.FechaCreacion = DateTime.Now;
                model.FechaActualizacion = DateTime.Now;                
                await _numeroProductoRepo.Crear(model);
                _response.Resultado = model;
                _response.statusCode = HttpStatusCode.Created;
                _response.IsExitoso = true;

                return CreatedAtRoute("GetNumeroProducto", new { id = model.NroProducto }, _response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumeroProducto(int id) 
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var numeroProducto = await _numeroProductoRepo.Obtener(a => a.NroProducto == id);
                if (numeroProducto == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _numeroProductoRepo.Remover(numeroProducto);
                _response.statusCode = HttpStatusCode.NoContent;
                _response.IsExitoso = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumeroProducto(int id, [FromBody] NumeroProductoUpdateDTO updateDTO)
        {
            if(updateDTO == null)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            if(await _agranelRepo.Obtener(a=>a.Id == updateDTO.IdProducto)==null)
            {
                ModelState.AddModelError("Foreign Key", "El Id de producto no existe");
                return BadRequest(ModelState);
            }

            //var numeroProduct = await _numeroProductoRepo.Obtener(p => p.NroProducto == id);


            NumeroProducto model = _mapper.Map<NumeroProducto>(updateDTO);
            model.FechaCreacion = DateTime.Now;
            model.FechaActualizacion = DateTime.Now;
            await _numeroProductoRepo.Update(model);
            _response.statusCode = HttpStatusCode.NoContent;
            _response.IsExitoso = true;
            return Ok(_response);
        }
    }
}
