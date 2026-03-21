using CrudExample.Domain.Entities.Base;

namespace CrudExample.Domain.Entities;

public class User : EntityBase
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}