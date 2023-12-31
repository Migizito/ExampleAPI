﻿using AutoMapper;
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
    public class AGranelController : ControllerBase
    {
        private readonly ILogger<AGranelController> _logger;
        private readonly IAGranelRepository _agranelRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public AGranelController(ILogger<AGranelController> logger, IAGranelRepository agranelRepo, IMapper mapper)
        {
            _logger = logger;
            _agranelRepo = agranelRepo;
            _mapper = mapper;
            _response = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllProductos() 
        {
            try
            {
                _logger.LogInformation("Obtener los productos");

                IEnumerable<AGranel> agranelList = await _agranelRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<AGranelDTO>>(agranelList);
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

        [HttpGet ("id:int", Name = "GetAGranel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetProducto(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer producto con el id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                //var agranel = AGranelStore.aGranelList.FirstOrDefault(a => a.Id == id);
                var product = await _agranelRepo.Obtener(p => p.Id == id);

                if (product == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<AGranelDTO>(product);
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
        public async Task<ActionResult<APIResponse>> CrearProducto([FromBody] AGranelCreateDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _agranelRepo.Obtener(p => p.Nombre.ToLower() == createDTO.Nombre.ToLower()) != null)
                {
                    ModelState.AddModelError("Nombre Existe", "El producto con ese nombre ya existe");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                AGranel model = _mapper.Map<AGranel>(createDTO);
                model.FechaCreacion = DateTime.Now;
                model.FechaActualizacion = DateTime.Now;                
                await _agranelRepo.Crear(model);
                _response.Resultado = model;
                _response.statusCode = HttpStatusCode.Created;
                _response.IsExitoso = true;

                return CreatedAtRoute("GetAGranel", new { id = model.Id }, _response);
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
        public async Task<IActionResult> DeleteProducto(int id) 
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var agranel = await _agranelRepo.Obtener(a => a.Id == id);
                if (agranel == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _agranelRepo.Remover(agranel);
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
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] AGranelUpdateDTO updateDTO)
        {
            if(updateDTO == null)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var agranel = await _agranelRepo.Obtener(a => a.Id == id);

            AGranel model = _mapper.Map<AGranel>(updateDTO);
            model.FechaCreacion = agranel.FechaCreacion;
            model.FechaActualizacion = DateTime.Now;
            await _agranelRepo.Update(model);
            _response.statusCode = HttpStatusCode.NoContent;
            _response.IsExitoso = true;
            return Ok(_response);
        }
    }
}
