using ExampleAGAPI.Datos;
using ExampleAGAPI.Models;

namespace ExampleAGAPI.Repositorio.IRepositorio
{
    public class NumeroProductoRepository : Repositorio<NumeroProducto>, INumeroProductoRepository
    {
        private readonly ApplicationDbContext _context;
        public NumeroProductoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<NumeroProducto> Update(NumeroProducto entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _context.NumeroProductos.Update(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }
    }
}
