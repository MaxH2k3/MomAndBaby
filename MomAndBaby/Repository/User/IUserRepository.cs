namespace MomAndBaby.Repository
{
    public interface IUserRepository
    {
		Task<bool> IsStaff(Guid userId);

	}
}
