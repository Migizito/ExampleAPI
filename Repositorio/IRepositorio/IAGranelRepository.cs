using ExampleAGAPI.Models;

namespace ExampleAGAPI.Repositorio.IRepositorio
{
    public interface IAGranelRepository : IRepositorio<AGranel>
    {
        Task<AGranel> Update(AGranel entidad);
    }
}
