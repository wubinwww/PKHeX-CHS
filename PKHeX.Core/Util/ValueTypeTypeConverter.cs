using System;
using System.Collections;
using System.ComponentModel;

namespace PKHeX.Core;

/// <summary>
/// Used for allowing a struct to be mutated in a PropertyGrid.
/// </summary>
public sealed class ValueTypeTypeConverter : ExpandableObjectConverter
{
    public override bool GetCreateInstanceSupported(ITypeDescriptorContext? context) => true;

    public override object? CreateInstance(ITypeDescriptorContext? context, IDictionary propertyValues)
    {
        if (context?.PropertyDescriptor is not { } pd)
            return null;

        var boxed = pd.GetValue(context.Instance);
        foreach (DictionaryEntry entry in propertyValues)
        {
            if (entry.Key.ToString() is not { } propName)
                continue;
            var pi = pd.PropertyType.GetProperty(propName);
            if (pi?.CanWrite == true)
                pi.SetValue(boxed, Convert.ChangeType(entry.Value, pi.PropertyType), null);
        }
        return boxed;
    }
}

/// <summary>
/// Used for converting a <see cref="uint"/> to an uppercase hex string and back.
/// </summary>
/// <remarks>
/// When converting from a string, it accepts both "0x" prefixed and non-prefixed hex strings, with case insensitivity.
/// </remarks>
public sealed class TypeConverterU32 : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is uint u)
            return $"{u:X8}"; // no 0x prefix
        return base.ConvertTo(context, culture, value, destinationType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
    {
        if (value is not string input)
            return base.ConvertFrom(context, culture, value);

        var span = input.AsSpan();
        if (span.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            span = span[2..];
        return uint.TryParse(span, System.Globalization.NumberStyles.HexNumber, culture, out var result) ? result : 0u;
    }
}

/// <summary>
/// Used for converting a <see cref="ulong"/> to an uppercase hex string and back.
/// </summary>
/// <remarks>
/// When converting from a string, it accepts both "0x" prefixed and non-prefixed hex strings, with case insensitivity.
/// </remarks>
public sealed class TypeConverterU64 : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is ulong u)
            return $"{u:X16}"; // no 0x prefix
        return base.ConvertTo(context, culture, value, destinationType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
    {
        if (value is not string input)
            return base.ConvertFrom(context, culture, value);
        var span = input.AsSpan();
        if (span.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            span = span[2..];
        return ulong.TryParse(span, System.Globalization.NumberStyles.HexNumber, culture, out var result) ? result : 0ul;
    }
}
