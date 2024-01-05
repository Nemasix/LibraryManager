namespace WebAppLibraryManager.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly Lazy<ILoanService> _lazyLoanService;
        private readonly Lazy<IBookService> _lazyBookService;
        private readonly Lazy<ISearchService> _lazySearchService;
        public ServiceManager(IConfiguration configuration)
        {
            _lazyUserService = new Lazy<IUserService>(() => new UserService(configuration));
            _lazyLoanService = new Lazy<ILoanService>(() => new LoanService(configuration));
            _lazyBookService = new Lazy<IBookService>(() => new BookService(configuration));
            _lazySearchService = new Lazy<ISearchService>(() => new SearchService(configuration));
        }

        public IUserService UserService => _lazyUserService.Value;
        public ILoanService LoanService => _lazyLoanService.Value;
        public IBookService BookService => _lazyBookService.Value;
        public ISearchService SearchService => _lazySearchService.Value;
    }
}
