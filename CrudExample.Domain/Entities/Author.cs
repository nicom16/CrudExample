using CrudExample.Domain.Entities.Base;

namespace CrudExample.Domain.Entities;

public class Author : EntityBase
{
    public string Name { get; protected set; }
    public string Surname { get; protected set; }
    public string Pseudonym { get; protected set; }
}