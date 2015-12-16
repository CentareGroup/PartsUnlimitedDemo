using System.Data.Entity;

namespace PartsUnlimited.Models
{
    public interface IPartsUnlimitedUserContext : IPartsUnlimitedContext
    {
        IDbSet<ApplicationUser> Users { get; }
    }
}
