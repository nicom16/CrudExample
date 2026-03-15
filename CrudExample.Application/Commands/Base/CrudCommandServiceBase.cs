using CrudExample.Application.Dtos.Base;
using CrudExample.Application.Extensions;
using CrudExample.Dal.Context;
using CrudExample.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace CrudExample.Application.Commands.Base;

public abstract class CrudCommandServiceBase<TDto, TEntity>(IDbContextFactory<MyDbContext> factory) : CommandHandlerBase(factory) 
    where TDto : DtoBase
    where TEntity : EntityBase, new()
{
    public async Task<Guid> CreateOrUpdateAsync(Guid userId, TDto dto)
    {
        await using var context = await factory.CreateDbContextAsync();

        Guid entityId =
            dto.IsNew 
                ? await CreateAsync(context, userId, dto) 
                : await UpdateAsync(context, userId, dto);
        
        await context.SaveChangesAsync();
        
        return entityId;
    }

    private async Task<Guid> CreateAsync(MyDbContext context, Guid userId, TDto dto)
    {
        TEntity entity = EntityBase.Create<TEntity>(userId, dto.ToNamedValuesCollection());
        await context.Set<TEntity>().AddAsync(entity);
        return entity.Id;
    }

    private async Task<Guid> UpdateAsync(MyDbContext context, Guid userId, TDto dto)
    {
        TEntity? entity = await context.FindAsync<TEntity>(dto.Id);
        if (entity == null)
            throw new InvalidOperationException("Impossibile aggiornare un elemento non salvato!");
        entity.Update(userId, dto.ToNamedValuesCollection());
        return entity.Id;
    }

    public async Task<int> DeleteAsync(Guid userId, Guid id)
    {
        await using var context = await factory.CreateDbContextAsync();
        
        TEntity? entity = await context.FindAsync<TEntity>(id);
        if (entity == null)
            throw new InvalidOperationException("Impossibile eliminare un elemento non salvato!");
        entity.Delete(userId);
        
        return await context.SaveChangesAsync();
    } 
}