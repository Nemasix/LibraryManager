namespace Application.Abstractions;

public interface IServiceManager
{
    IBookService BookService { get; }
}
