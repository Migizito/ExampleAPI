using ExampleAGAPI.Models;

namespace ExampleAGAPI.Repositorio.IRepositorio
{
    public interface INumeroProductoRepository : IRepositorio<NumeroProducto>
    {
        Task<NumeroProducto> Update(NumeroProducto entidad);
    }
}
