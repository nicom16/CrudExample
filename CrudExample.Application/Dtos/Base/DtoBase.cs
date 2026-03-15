namespace CrudExample.Application.Dtos.Base;

public abstract record DtoBase(Guid Id)
{ 
    public bool IsNew => Id == Guid.Empty;
}