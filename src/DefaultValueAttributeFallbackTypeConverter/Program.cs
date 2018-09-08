using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Security.Principal;
using System.Threading;

namespace DefaultValueAttributeFallbackTypeConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            DefaultValueAttribute.s_convertFromInvariantString = new object();

            //TypeDescriptor.AddAttributes(typeof(MyType), new TypeConverterAttribute(typeof(MyConverter)));

            //var converters = TypeDescriptor.GetConverter(typeof(MyType));

            //Console.WriteLine(object.ReferenceEquals(TypeDescriptor.GetConverter(typeof(MyType)), TypeDescriptor.GetConverter(typeof(MyType))));

            Console.WriteLine(new DefaultValueAttribute(typeof(int), "42").Value);

            //var r = new DefaultValueAttribute(typeof(DayOfWeek), "Monday").Value;
            //var r2 = new DefaultValueAttribute(typeof(TimeSpan), "1:00:00").Value;

            //Console.WriteLine(DayOfWeek.Monday.Equals(new DefaultValueAttribute(typeof(DayOfWeek), "Monday").Value));

            //DayOfWeek.Monday, new DefaultValueAttribute(typeof(DayOfWeek), "Monday").Value            
        }
    }

    public class DefaultValueAttribute : Attribute
    {
        /// <devdoc>
        ///     This is the default value.
        /// </devdoc>
        private object _value;

        // Delegate ad hoc created 'TypeDescriptor.ConvertFromInvariantString' reflection object cache
        public static object s_convertFromInvariantString;

        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/> class, converting the
        ///    specified value to the
        ///    specified type, and using the U.S. English culture as the
        ///    translation
        ///    context.</para>
        /// </devdoc>
        public DefaultValueAttribute(Type type, string value)
        {
            // The try/catch here is because attributes should never throw exceptions.  We would fail to
            // load an otherwise normal class.
            try
            {
                if (TryConvertFromInvariantString(type, value, out object convertedValue))
                {
                    _value = convertedValue;
                }
                else if (type.IsSubclassOf(typeof(Enum)))
                {
                    _value = Enum.Parse(type, value, true);
                }
                else if (type == typeof(TimeSpan))
                {
                    _value = TimeSpan.Parse(value);
                }
                else
                {
                    _value = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
                }

                return;

                // Looking for ad hoc created TypeDescriptor.ConvertFromInvariantString(Type, string)
                bool TryConvertFromInvariantString(Type typeToConvert, string stringValue, out object conversionResult)
                {
                    conversionResult = null;

                    // lazy init reflection objects
                    if (s_convertFromInvariantString == null)
                    {
                        Type typeDescriptorType = Type.GetType("System.ComponentModel.TypeDescriptor, System.ComponentModel.TypeConverter, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", throwOnError: false);
                        Volatile.Write(ref s_convertFromInvariantString, typeDescriptorType == null ? new object() : Delegate.CreateDelegate(typeof(Func<Type, string, object>), typeDescriptorType, "ConvertFromInvariantString", ignoreCase: false));
                    }

                    if (!(s_convertFromInvariantString is Func<Type, string, object> convertFromInvariantString))
                        return false;

                    conversionResult = convertFromInvariantString(typeToConvert, stringValue);

                    return true;
                }
            }
            catch
            {

            }
        }

        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/> class using a Unicode
        ///    character.</para>
        /// </devdoc>
        public DefaultValueAttribute(char value)
        {
            _value = value;
        }
        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/> class using an 8-bit unsigned
        ///    integer.</para>
        /// </devdoc>
        public DefaultValueAttribute(byte value)
        {
            _value = value;
        }
        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/> class using a 16-bit signed
        ///    integer.</para>
        /// </devdoc>
        public DefaultValueAttribute(short value)
        {
            _value = value;
        }
        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/> class using a 32-bit signed
        ///    integer.</para>
        /// </devdoc>
        public DefaultValueAttribute(int value)
        {
            _value = value;
        }
        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/> class using a 64-bit signed
        ///    integer.</para>
        /// </devdoc>
        public DefaultValueAttribute(long value)
        {
            _value = value;
        }
        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/> class using a
        ///    single-precision floating point
        ///    number.</para>
        /// </devdoc>
        public DefaultValueAttribute(float value)
        {
            _value = value;
        }
        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/> class using a
        ///    double-precision floating point
        ///    number.</para>
        /// </devdoc>
        public DefaultValueAttribute(double value)
        {
            _value = value;
        }
        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/> class using a <see cref='System.Boolean'/>
        /// value.</para>
        /// </devdoc>
        public DefaultValueAttribute(bool value)
        {
            _value = value;
        }
        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/> class using a <see cref='System.String'/>.</para>
        /// </devdoc>
        public DefaultValueAttribute(string value)
        {
            _value = value;
        }

        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/>
        /// class.</para>
        /// </devdoc>
        public DefaultValueAttribute(object value)
        {
            _value = value;
        }

        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/> class using a <see cref='System.SByte'/>
        /// value.</para>
        /// </devdoc>
        [CLSCompliant(false)]
        public DefaultValueAttribute(sbyte value)
        {
            _value = value;
        }

        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/> class using a <see cref='System.UInt16'/>
        /// value.</para>
        /// </devdoc>
        [CLSCompliant(false)]
        public DefaultValueAttribute(ushort value)
        {
            _value = value;
        }

        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/> class using a <see cref='System.UInt32'/>
        /// value.</para>
        /// </devdoc>
        [CLSCompliant(false)]
        public DefaultValueAttribute(uint value)
        {
            _value = value;
        }

        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.DefaultValueAttribute'/> class using a <see cref='System.UInt64'/>
        /// value.</para>
        /// </devdoc>
        [CLSCompliant(false)]
        public DefaultValueAttribute(ulong value)
        {
            _value = value;
        }

        /// <devdoc>
        ///    <para>
        ///       Gets the default value of the property this
        ///       attribute is
        ///       bound to.
        ///    </para>
        /// </devdoc>
        public virtual object Value
        {
            get
            {
                return _value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            DefaultValueAttribute other = obj as DefaultValueAttribute;

            if (other != null)
            {
                if (Value != null)
                {
                    return Value.Equals(other.Value);
                }
                else
                {
                    return (other.Value == null);
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected void SetValue(object value)
        {
            _value = value;
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
