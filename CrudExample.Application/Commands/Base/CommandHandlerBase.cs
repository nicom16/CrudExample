using CrudExample.Dal.Context;
using Microsoft.EntityFrameworkCore;

namespace CrudExample.Application.Commands.Base;

public abstract class CommandHandlerBase(IDbContextFactory<MyDbContext> factory)
{
    protected readonly IDbContextFactory<MyDbContext> Factory = factory;
}