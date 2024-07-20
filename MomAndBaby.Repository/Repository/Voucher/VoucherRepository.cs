using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Voucher>> GetVouchers()
        {
            return await _context.Vouchers.Include(v => v.CreatedByNavigation).ToListAsync();
        }

        public async Task<IEnumerable<Gift>> GetGifts()
        {
            return await _context.Gifts.ToListAsync();
        }

        public async Task DeleteVoucher(int id)
        {
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.Id == id);
            _context.Vouchers.Remove(voucher!);
        }
    }
}
