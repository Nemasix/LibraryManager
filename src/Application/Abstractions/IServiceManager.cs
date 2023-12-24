namespace Application.Abstractions;

public interface IServiceManager
{
    IBooksService BooksService { get; }
}
