using System.Collections;
using System.Runtime.CompilerServices;
using CrudExample.Application.Dtos.Base;
using CrudExample.Domain.ValueObjects;

namespace CrudExample.Application.Extensions;

public static class DtoExtensions
{
    public static NamedValuesCollection ToNamedValuesCollection(this DtoBase dto)
    {
        Dictionary<string, object?> properties = 
            dto.GetType()
                .GetProperties()
                .Where(p => 
                    p.GetSetMethod() is not null && 
                    !p.GetSetMethod()!.ReturnParameter
                        .GetRequiredCustomModifiers()
                        .Contains(typeof(IsExternalInit)))
                .ToDictionary(p => p.Name, p => p.GetValue(dto));
        return new NamedValuesCollection(properties);
    }

    public static TEntity ToEntity<TEntity>(this DtoBase dto)
    {
        var entity = Activator.CreateInstance<TEntity>();

        foreach (var dtoProperty in dto.GetType().GetProperties())
        {
            var entityProperty = entity!.GetType().GetProperty(dtoProperty.Name);
            if (entityProperty == null)
            {
                continue;
            }

            if (dtoProperty.PropertyType == entityProperty.PropertyType)
            {
                entityProperty.SetValue(entity, dtoProperty.GetValue(dto));
                continue;
            }

            if (dtoProperty.PropertyType.IsSubclassOf(typeof(DtoBase)))
            {
                var propertyValue = dtoProperty.GetValue(dto)!;
                var toEntityMethod = propertyValue.GetType().GetMethod("ToEntity");
                entityProperty.SetValue(entity, toEntityMethod!.Invoke(propertyValue, [ ]));
                continue;
            }

            if (typeof(IEnumerable).IsAssignableFrom(dtoProperty.PropertyType))
            {
                foreach (var item in (IEnumerable)dtoProperty.GetValue(dto)!)
                {
                    
                }
            }
        }
        
        return entity;
    }
}