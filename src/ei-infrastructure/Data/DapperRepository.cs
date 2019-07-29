using System.Threading.Tasks;
using ei_core.Entities;
using ei_core.Interfaces;

namespace ei_infrastructure.Data
{
    public class DapperRepository<T> : IRepository<T> where T : BaseEntity, IIdentifiable
    {
        public Task<T> FindByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}