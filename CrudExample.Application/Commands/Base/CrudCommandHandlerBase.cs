using CrudExample.Application.Dtos.Base;
using CrudExample.Dal.Context;
using CrudExample.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace CrudExample.Application.Commands.Base;

public abstract class CrudCommandHandlerBase<TDto, TEntity>(IDbContextFactory<MyDbContext> factory) : CommandHandlerBase(factory) 
    where TDto : DtoBase
    where TEntity : EntityBase
{
    
}