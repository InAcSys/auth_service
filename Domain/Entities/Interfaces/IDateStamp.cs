namespace AuthService.Domain.Entities.Interfaces
{
    public interface IDateStamp
    {
        DateTime Created { get; set; }
        DateTime? Updated { get; set; }
        DateTime? Deleted { get; set; }
    }
}
