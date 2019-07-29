using System.Threading.Tasks;
using ei_core.Entities;

namespace ei_core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, IIdentifiable
    {
        Task<T> FindByIdAsync(int id);
    }
}