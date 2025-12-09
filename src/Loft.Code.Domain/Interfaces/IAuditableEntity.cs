namespace Loft.Code.Domain.Interfaces;

public interface IAuditableEntity
{
    DateTime? CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
    long? CreatedBy { get; set; }
    long? UpdatedBy { get; set; }
}