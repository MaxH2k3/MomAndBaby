using Microsoft.EntityFrameworkCore;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.Repository.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.Service
{
    public class VoucherService : IVoucherService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VoucherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Voucher>> GetVouchers()
        {
            return await _unitOfWork.VoucherRepository.GetVouchers();
        }

        public async Task<IEnumerable<Gift>> GetGifts()
        {
            return await _unitOfWork.VoucherRepository.GetGifts();
        }

        public async Task<bool> DeleteVoucher(int id)
        {
            await _unitOfWork.VoucherRepository.DeleteVoucher(id);
            return await _unitOfWork.SaveChangesAsync();
        }

    }
}
