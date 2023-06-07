namespace DogApp.Application.Repositories
{
    public interface IRepositoryWrapper
    {
        IDogRepository Dogs { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}