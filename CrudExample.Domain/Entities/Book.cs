using CrudExample.Domain.Entities.Base;

namespace CrudExample.Domain.Entities;

public class Book : EntityBase
{
    public string Title { get; protected set; }
    public Guid AuthorId { get; protected set; }
    public Author Author { get; protected set; }
}