using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Infrastructure.SeedWork;

public class TypedIdValueConverter<TTypedIdValue>(ConverterMappingHints mappingHints = null) : ValueConverter<TTypedIdValue, Guid>(id => id.Value, value => Create(value), mappingHints)
    where TTypedIdValue : TypedIdValueBase
{
    private static TTypedIdValue Create(Guid id) => Activator.CreateInstance(typeof(TTypedIdValue), id) as TTypedIdValue;
}