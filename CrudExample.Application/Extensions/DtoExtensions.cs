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
}