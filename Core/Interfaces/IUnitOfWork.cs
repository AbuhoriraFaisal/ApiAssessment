namespace Core.Interfaces
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenericRepositroy<T> Entity { get; }
        Task<bool>CompeleteAsync();

    }
}
