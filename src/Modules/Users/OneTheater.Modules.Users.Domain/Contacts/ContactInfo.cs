using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Modules.Users.Domain.Contacts;

public sealed class ContactInfo : Entity
{
    private ContactInfo()
    { }

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Email { get; private set; }
    public string? Phone { get; private set; }
    public string? Address { get; private set; }

    public static Result<ContactInfo> Create(Guid userId, string email, string? phone, string? address)
    {
        var contact = new ContactInfo
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Email = email,
            Phone = phone,
            Address = address
        };

        return contact;
    }

    public void Update(string email, string? phone, string? address)
    {
        Email = email;
        Phone = phone;
        Address = address;
    }
}

public interface IContactInfoRepository
{
    Task<ContactInfo?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    void Insert(ContactInfo entity);
}





