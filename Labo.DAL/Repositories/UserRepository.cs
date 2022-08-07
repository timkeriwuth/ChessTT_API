using Labo.DAL.Contexts;
using Labo.DL.Entities;
using ToolBox.EF.Repository;

namespace Labo.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(TournamentContext context) : base(context)
        {
        }
    }
}
