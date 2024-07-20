using MomAndBaby.BusinessObject.Entity;
using System.Threading.Tasks;

namespace MomAndBaby.Repository
{
    public interface IVoucherRepository
    {
        Task<IEnumerable<Voucher>> GetVouchers();
        Task<IEnumerable<Gift>> GetGifts();
    }
}
