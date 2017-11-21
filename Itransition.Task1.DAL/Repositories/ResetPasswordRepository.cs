using Itransition.Task1.DAL.Interfaces;
using Itransition.Task1.Domain;

namespace Itransition.Task1.DAL.Repositories
{
    public class ResetPasswordRepository : BaseRepository<ResetPassword>, IResetPasswordRepository
    {
        public ResetPasswordRepository(AppDbContext context)
            : base(context)
        { }
    }
}
