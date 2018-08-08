using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace DefaultValueAttributeFallbackTypeConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            TypeDescriptor.AddAttributes(typeof(MyType), new TypeConverterAttribute(typeof(MyConverter)));
            var converter = TypeDescriptor.GetConverter(typeof(MyType));
            var d = new DefaultValueAttribute(typeof(MyType), "10");
            Console.WriteLine(d.Value);
        }
    }

    public class DefaultValueAttribute : Attribute
    {
        /// <devdoc>
        ///     This is the default value.
        /// </devdoc>
        private object _value;
        static Type s_typeDescriptorTypeCached;
        static MethodInfo s_typeConverterConvertFromInvariantStringMethodCached;

        static DefaultValueAttribute()
        {
            //cache "reflection" types for conversion fallback
            s_typeDescriptorTypeCached = Type.GetType("System.ComponentModel.TypeDescriptor, System, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", throwOnError: false);
            Type typeConverterType = Type.GetType("System.ComponentModel.TypeConverter, System, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", throwOnError: false);
            if (typeConverterType != null)
            {
                s_typeConverterConvertFromInvariantStringMethodCached = typeConverterType.GetMethod("ConvertFromInvariantString", new Type[] { typeof(string) });
            }
        }

        public DefaultValueAttribute(Type type, string value)
        {
            // The try/catch here is because attributes should never throw exceptions.  We would fail to
            // load an otherwise normal class.
            try
            {
                if (type.IsSubclassOf(typeof(Enum)))
                {
                    _value = Enum.Parse(type, value, true);
                }
                else if (type == typeof(TimeSpan))
                {
                    _value = TimeSpan.Parse(value);
                }
                else if (type.Module == typeof(string).Module)
                {
                    _value = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
                }
                else
                {
                    _value = ConvertFromInvariantString(type, value);
                }
            }
            catch
            {
                //removed on codebase
                throw;
            }

            object ConvertFromInvariantString(Type typeToConvert, string stringValue)
            {
                if (s_typeDescriptorTypeCached == null || s_typeConverterConvertFromInvariantStringMethodCached == null)
                    return null;

                var typeDescriptorTypeGetConverter = s_typeDescriptorTypeCached.GetMethod("GetConverter", new Type[] { typeToConvert });

                // typeConverter cannot be null GetConverter return default converter
                var typeConverter = typeDescriptorTypeGetConverter.Invoke(null, new[] { typeToConvert });

                return s_typeConverterConvertFromInvariantStringMethodCached.Invoke(typeConverter, new[] { stringValue });
            }
        }

        public virtual object Value
        {
            get
            {
                return _value;
            }
        }
    }

    public class MyType
    {

    }

    public class MyConverter : System.ComponentModel.TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value;
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return base.CanConvertFrom(context, sourceType);
        }
    }

}
