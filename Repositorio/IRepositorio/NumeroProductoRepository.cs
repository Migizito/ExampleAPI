using ExampleAGAPI.Datos;
using ExampleAGAPI.Models;

namespace ExampleAGAPI.Repositorio.IRepositorio
{
    public class AGranelRepository : Repositorio<AGranel>, IAGranelRepository
    {
        private readonly ApplicationDbContext _context;
        public AGranelRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<AGranel> Update(AGranel entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _context.Productos.Update(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }
    }
}
