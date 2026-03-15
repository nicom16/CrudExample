using CrudExample.Domain.ValueObjects;

namespace CrudExample.Domain.Entities.Base;

public abstract class EntityBase
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public Guid CreatedBy { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
    public Guid UpdatedBy { get; protected set; }
    public bool IsDeleted { get; protected set; }
    public DateTime DeletedAt { get; protected set; }
    public Guid DeletedBy { get; protected set; }

    protected EntityBase() { }
    
    public static TEntity Create<TEntity>(Guid userId, NamedValuesCollection values) where TEntity : EntityBase, new()
    {
        var entity = new TEntity
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId
        };

        entity.Update(userId, values);
        
        return entity;
    }

    public virtual void Update(Guid userId, NamedValuesCollection values)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = userId;
    }

    public virtual void Delete(Guid userId)
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeletedBy = userId;
    }
}