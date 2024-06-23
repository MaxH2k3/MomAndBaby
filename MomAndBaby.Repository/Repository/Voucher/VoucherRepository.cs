using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly MomAndBabyContext _context;

        public VoucherRepository(MomAndBabyContext context)
        {
            _context = context;
        }
    }
}
