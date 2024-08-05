namespace OneTheater.Modules.Shows.PublicApi;

public interface ICustomersApi
{
    Task<Guid?> PostAsync(CustomerCreateRequest request, CancellationToken cancellationToken = default);
}

public sealed class CustomerCreateRequest
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
