namespace Services.Interfaces
{
    public interface ILinkEntity
    {
        int EntityId { get; set; }
        string EntityIdField { get; set; }
        string EntityDisplayValueField { get; set; }
    }
}