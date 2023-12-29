namespace Application.Abstractions;

public interface IServiceManager
{
    IBookService BookService { get; }
    ILoanService LoanService { get; }
    IUserService UserService { get; }
}
