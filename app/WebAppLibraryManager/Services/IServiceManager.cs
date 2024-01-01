namespace WebAppLibraryManager.Services
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        ILoanService LoanService { get; }
    }
}
