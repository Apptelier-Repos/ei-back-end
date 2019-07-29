using System.Threading.Tasks;
using ei_core.Entities.UserAccountAggregate;

namespace ei_core.Interfaces
{
    public interface IRepository
    {
        Task<UserAccount> FindUserAccountByIdAsync(int id);
    }
}