using MomAndBaby.Repository;

namespace MomAndBaby.Repository.Uow
{
    public interface IUnitOfWork
    {
        ICommonRepository CommonRepository { get; }
        IArticleRepository ArticleRepository { get; }
        IGiftRepository GiftRepository { get; }
        IMessageRepository MessageRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IOrderTrackingRepository OrderTrackingRepository { get; }
        IProductRepository ProductRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IUserRepository UserRepository { get; }
        IVoucherRepository VoucherRepository { get; }

        Task<bool> SaveChangesAsync();
    }
}
