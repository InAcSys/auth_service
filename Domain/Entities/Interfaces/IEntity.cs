namespace AuthService.Domain.Entities.Interfaces
{
    public interface IEntity<TKey> : IDateStamp, ITenant
    {
        TKey? Id { get; set; }
        bool IsActive { get; set; }

    }
}
