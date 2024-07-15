using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.Repository.Repository;

namespace MomAndBaby.Repository.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MomAndBabyContext _context;

        private readonly IArticleRepository _articleRepository = null!;
        private readonly IGiftRepository _giftRepository = null!;
        private readonly IMessageRepository _messageRepository = null!;
        private readonly IProductRepository _productRepository = null!;
        private readonly ICommonRepository _commonRepository = null!;
        private readonly IOrderRepository _orderRepository = null!;
        private readonly IOrderDetailRepository _orderDetailRepository = null!;
        private readonly IOrderTrackingRepository _orderTrackingRepository = null!;
        private readonly IReviewRepository _reviewRepository = null!;
        private readonly IUserRepository _userRepository = null!;
        private readonly IVoucherRepository _voucherRepository = null!;
        private readonly ICategoryRepository _categoryRepository = null!;
        private readonly IUserValidationRepository _userValidationRepository = null!;

        public UnitOfWork(MomAndBabyContext context)
        {
            _context = context;
        }

        public UnitOfWork()
        {
            _context = new MomAndBabyContext();
        }

        public IArticleRepository ArticleRepository => _articleRepository ?? new ArticleRepository(_context);
        public IGiftRepository GiftRepository => _giftRepository ?? new GiftRepository(_context);
        public IMessageRepository MessageRepository => _messageRepository ?? new MessageRepository(_context);
        public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_context);
        public ICommonRepository CommonRepository => _commonRepository ?? new CommonRepository(_context);
        public IOrderRepository OrderRepository => _orderRepository ?? new OrderRepository(_context);
        public IOrderDetailRepository OrderDetailRepository => _orderDetailRepository ?? new OrderDetailRepository(_context);
        public IOrderTrackingRepository OrderTrackingRepository => _orderTrackingRepository ?? new OrderTrackingRepository(_context);
        public IReviewRepository ReviewRepository => _reviewRepository ?? new ReviewRepository(_context);
        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
        public IVoucherRepository VoucherRepository => _voucherRepository ?? new VoucherRepository(_context);
        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_context);
        public IUserValidationRepository UserValidationRepository => _userValidationRepository ?? new UserValidationRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
