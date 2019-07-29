using System.Threading.Tasks;

namespace ei_core.Interfaces
{
    public interface IRepository
    {
        Task<IUserAccountDto> FindUserAccountByIdAsync(int id);
    }
}