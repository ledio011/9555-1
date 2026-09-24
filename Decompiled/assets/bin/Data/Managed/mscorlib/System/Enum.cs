using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000E1 RID: 225
	[ComVisible(true)]
	[Serializable]
	public abstract class Enum : ValueType, IComparable, IConvertible, IFormattable
	{
		// Token: 0x060008C7 RID: 2247 RVA: 0x00021FA8 File Offset: 0x000201A8
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this.Value, provider);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00021FB8 File Offset: 0x000201B8
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this.Value, provider);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00021FC8 File Offset: 0x000201C8
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this.Value, provider);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00021FD8 File Offset: 0x000201D8
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this.Value, provider);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00021FE8 File Offset: 0x000201E8
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this.Value, provider);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00021FF8 File Offset: 0x000201F8
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this.Value, provider);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00022008 File Offset: 0x00020208
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this.Value, provider);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00022018 File Offset: 0x00020218
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this.Value, provider);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00022028 File Offset: 0x00020228
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this.Value, provider);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00022038 File Offset: 0x00020238
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this.Value, provider);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00022048 File Offset: 0x00020248
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this.Value, provider);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00022058 File Offset: 0x00020258
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (targetType == typeof(string))
			{
				return this.ToString(provider);
			}
			return Convert.ToType(this.Value, targetType, provider, false);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00022094 File Offset: 0x00020294
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this.Value, provider);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x000220A4 File Offset: 0x000202A4
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this.Value, provider);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000220B4 File Offset: 0x000202B4
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this.Value, provider);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x000220C4 File Offset: 0x000202C4
		public TypeCode GetTypeCode()
		{
			return Type.GetTypeCode(Enum.GetUnderlyingType(base.GetType()));
		}

		// Token: 0x060008D7 RID: 2263
		[MethodImpl(4096)]
		private extern object get_value();

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x000220D8 File Offset: 0x000202D8
		private object Value
		{
			get
			{
				return this.get_value();
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000220E0 File Offset: 0x000202E0
		private static int FindPosition(object value, Array values)
		{
			if (!(values is byte[]) && !(values is ushort[]) && !(values is uint[]) && !(values is ulong[]))
			{
				if (values is int[])
				{
					return Array.BinarySearch(values, value, MonoEnumInfo.int_comparer);
				}
				if (values is short[])
				{
					return Array.BinarySearch(values, value, MonoEnumInfo.short_comparer);
				}
				if (values is sbyte[])
				{
					return Array.BinarySearch(values, value, MonoEnumInfo.sbyte_comparer);
				}
				if (values is long[])
				{
					return Array.BinarySearch(values, value, MonoEnumInfo.long_comparer);
				}
			}
			return Array.BinarySearch(values, value);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00022184 File Offset: 0x00020384
		[ComVisible(true)]
		public static string GetName(Type enumType, object value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("enumType is not an Enum type.", "enumType");
			}
			value = Enum.ToObject(enumType, value);
			MonoEnumInfo monoEnumInfo;
			MonoEnumInfo.GetInfo(enumType, out monoEnumInfo);
			int num = Enum.FindPosition(value, monoEnumInfo.values);
			return (num < 0) ? null : monoEnumInfo.names[num];
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00022204 File Offset: 0x00020404
		[ComVisible(true)]
		public static bool IsDefined(Type enumType, object value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("enumType is not an Enum type.", "enumType");
			}
			MonoEnumInfo monoEnumInfo;
			MonoEnumInfo.GetInfo(enumType, out monoEnumInfo);
			Type type = value.GetType();
			if (type == typeof(string))
			{
				return ((IList)monoEnumInfo.names).Contains(value);
			}
			if (type == monoEnumInfo.utype || type == enumType)
			{
				value = Enum.ToObject(enumType, value);
				MonoEnumInfo.GetInfo(enumType, out monoEnumInfo);
				return Enum.FindPosition(value, monoEnumInfo.values) >= 0;
			}
			throw new ArgumentException("The value parameter is not the correct type.It must be type String or the same type as the underlying typeof the Enum.");
		}

		// Token: 0x060008DC RID: 2268
		[MethodImpl(4096)]
		private static extern Type get_underlying_type(Type enumType);

		// Token: 0x060008DD RID: 2269 RVA: 0x000222C0 File Offset: 0x000204C0
		[ComVisible(true)]
		public static Type GetUnderlyingType(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("enumType is not an Enum type.", "enumType");
			}
			return Enum.get_underlying_type(enumType);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x000222F4 File Offset: 0x000204F4
		[ComVisible(true)]
		public static object Parse(Type enumType, string value)
		{
			return Enum.Parse(enumType, value, false);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00022300 File Offset: 0x00020500
		private static int FindName(Hashtable name_hash, string[] names, string name, bool ignoreCase)
		{
			if (!ignoreCase)
			{
				if (name_hash != null)
				{
					object obj = name_hash[name];
					if (obj != null)
					{
						return (int)obj;
					}
				}
				else
				{
					for (int i = 0; i < names.Length; i++)
					{
						if (name == names[i])
						{
							return i;
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < names.Length; j++)
				{
					if (string.Compare(name, names[j], ignoreCase, CultureInfo.InvariantCulture) == 0)
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00022388 File Offset: 0x00020588
		private static ulong GetValue(object value, TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.SByte:
				return (ulong)((byte)((sbyte)value));
			case TypeCode.Byte:
				return (ulong)((byte)value);
			case TypeCode.Int16:
				return (ulong)((ushort)((short)value));
			case TypeCode.UInt16:
				return (ulong)((ushort)value);
			case TypeCode.Int32:
				return (ulong)((int)value);
			case TypeCode.UInt32:
				return (ulong)((uint)value);
			case TypeCode.Int64:
				return (ulong)((long)value);
			case TypeCode.UInt64:
				return (ulong)value;
			default:
				throw new ArgumentException("typeCode is not a valid type code for an Enum");
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00022410 File Offset: 0x00020610
		[ComVisible(true)]
		public static object Parse(Type enumType, string value, bool ignoreCase)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("enumType is not an Enum type.", "enumType");
			}
			value = value.Trim();
			if (value.Length == 0)
			{
				throw new ArgumentException("An empty string is not considered a valid value.");
			}
			MonoEnumInfo monoEnumInfo;
			MonoEnumInfo.GetInfo(enumType, out monoEnumInfo);
			int num = Enum.FindName(monoEnumInfo.name_hash, monoEnumInfo.names, value, ignoreCase);
			if (num >= 0)
			{
				return monoEnumInfo.values.GetValue(num);
			}
			TypeCode typeCode = ((Enum)monoEnumInfo.values.GetValue(0)).GetTypeCode();
			if (value.IndexOf(',') != -1)
			{
				string[] array = value.Split(Enum.split_char);
				ulong num2 = 0UL;
				for (int i = 0; i < array.Length; i++)
				{
					num = Enum.FindName(monoEnumInfo.name_hash, monoEnumInfo.names, array[i].Trim(), ignoreCase);
					if (num < 0)
					{
						throw new ArgumentException("The requested value was not found.");
					}
					num2 |= Enum.GetValue(monoEnumInfo.values.GetValue(num), typeCode);
				}
				return Enum.ToObject(enumType, num2);
			}
			switch (typeCode)
			{
			case TypeCode.SByte:
			{
				sbyte value2;
				if (sbyte.TryParse(value, out value2))
				{
					return Enum.ToObject(enumType, value2);
				}
				break;
			}
			case TypeCode.Byte:
			{
				byte value3;
				if (byte.TryParse(value, out value3))
				{
					return Enum.ToObject(enumType, value3);
				}
				break;
			}
			case TypeCode.Int16:
			{
				short value4;
				if (short.TryParse(value, out value4))
				{
					return Enum.ToObject(enumType, value4);
				}
				break;
			}
			case TypeCode.UInt16:
			{
				ushort value5;
				if (ushort.TryParse(value, out value5))
				{
					return Enum.ToObject(enumType, value5);
				}
				break;
			}
			case TypeCode.Int32:
			{
				int value6;
				if (int.TryParse(value, out value6))
				{
					return Enum.ToObject(enumType, value6);
				}
				break;
			}
			case TypeCode.UInt32:
			{
				uint value7;
				if (uint.TryParse(value, out value7))
				{
					return Enum.ToObject(enumType, value7);
				}
				break;
			}
			case TypeCode.Int64:
			{
				long value8;
				if (long.TryParse(value, out value8))
				{
					return Enum.ToObject(enumType, value8);
				}
				break;
			}
			case TypeCode.UInt64:
			{
				ulong value9;
				if (ulong.TryParse(value, out value9))
				{
					return Enum.ToObject(enumType, value9);
				}
				break;
			}
			}
			throw new ArgumentException(string.Format("The requested value '{0}' was not found.", value));
		}

		// Token: 0x060008E2 RID: 2274
		[MethodImpl(4096)]
		private extern int compare_value_to(object other);

		// Token: 0x060008E3 RID: 2275 RVA: 0x00022668 File Offset: 0x00020868
		public int CompareTo(object target)
		{
			if (target == null)
			{
				return 1;
			}
			Type type = base.GetType();
			if (target.GetType() != type)
			{
				throw new ArgumentException(string.Format("Object must be the same type as the enum. The type passed in was {0}; the enum type was {1}.", target.GetType(), type));
			}
			return this.compare_value_to(target);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x000226B0 File Offset: 0x000208B0
		public override string ToString()
		{
			return this.ToString("G");
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x000226C0 File Offset: 0x000208C0
		[Obsolete("Provider is ignored, just use ToString")]
		public string ToString(IFormatProvider provider)
		{
			return this.ToString("G", provider);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000226D0 File Offset: 0x000208D0
		public string ToString(string format)
		{
			if (format == string.Empty || format == null)
			{
				format = "G";
			}
			return Enum.Format(base.GetType(), this.Value, format);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00022704 File Offset: 0x00020904
		[Obsolete("Provider is ignored, just use ToString")]
		public string ToString(string format, IFormatProvider provider)
		{
			if (format == string.Empty || format == null)
			{
				format = "G";
			}
			return Enum.Format(base.GetType(), this.Value, format);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00022738 File Offset: 0x00020938
		[ComVisible(true)]
		public static object ToObject(Type enumType, byte value)
		{
			return Enum.ToObject(enumType, value);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00022748 File Offset: 0x00020948
		[ComVisible(true)]
		public static object ToObject(Type enumType, short value)
		{
			return Enum.ToObject(enumType, value);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00022758 File Offset: 0x00020958
		[ComVisible(true)]
		public static object ToObject(Type enumType, int value)
		{
			return Enum.ToObject(enumType, value);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00022768 File Offset: 0x00020968
		[ComVisible(true)]
		public static object ToObject(Type enumType, long value)
		{
			return Enum.ToObject(enumType, value);
		}

		// Token: 0x060008EC RID: 2284
		[ComVisible(true)]
		[MethodImpl(4096)]
		public static extern object ToObject(Type enumType, object value);

		// Token: 0x060008ED RID: 2285 RVA: 0x00022778 File Offset: 0x00020978
		[CLSCompliant(false)]
		[ComVisible(true)]
		public static object ToObject(Type enumType, sbyte value)
		{
			return Enum.ToObject(enumType, value);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00022788 File Offset: 0x00020988
		[CLSCompliant(false)]
		[ComVisible(true)]
		public static object ToObject(Type enumType, ushort value)
		{
			return Enum.ToObject(enumType, value);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00022798 File Offset: 0x00020998
		[CLSCompliant(false)]
		[ComVisible(true)]
		public static object ToObject(Type enumType, uint value)
		{
			return Enum.ToObject(enumType, value);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x000227A8 File Offset: 0x000209A8
		[CLSCompliant(false)]
		[ComVisible(true)]
		public static object ToObject(Type enumType, ulong value)
		{
			return Enum.ToObject(enumType, value);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x000227B8 File Offset: 0x000209B8
		public override bool Equals(object obj)
		{
			return ValueType.DefaultEquals(this, obj);
		}

		// Token: 0x060008F2 RID: 2290
		[MethodImpl(4096)]
		private extern int get_hashcode();

		// Token: 0x060008F3 RID: 2291 RVA: 0x000227C4 File Offset: 0x000209C4
		public override int GetHashCode()
		{
			return this.get_hashcode();
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000227CC File Offset: 0x000209CC
		private static string FormatSpecifier_X(Type enumType, object value, bool upper)
		{
			switch (Type.GetTypeCode(enumType))
			{
			case TypeCode.SByte:
				return ((sbyte)value).ToString((!upper) ? "x2" : "X2");
			case TypeCode.Byte:
				return ((byte)value).ToString((!upper) ? "x2" : "X2");
			case TypeCode.Int16:
				return ((short)value).ToString((!upper) ? "x4" : "X4");
			case TypeCode.UInt16:
				return ((ushort)value).ToString((!upper) ? "x4" : "X4");
			case TypeCode.Int32:
				return ((int)value).ToString((!upper) ? "x8" : "X8");
			case TypeCode.UInt32:
				return ((uint)value).ToString((!upper) ? "x8" : "X8");
			case TypeCode.Int64:
				return ((long)value).ToString((!upper) ? "x16" : "X16");
			case TypeCode.UInt64:
				return ((ulong)value).ToString((!upper) ? "x16" : "X16");
			default:
				throw new Exception("Invalid type code for enumeration.");
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002293C File Offset: 0x00020B3C
		private static string FormatFlags(Type enumType, object value)
		{
			string text = string.Empty;
			MonoEnumInfo monoEnumInfo;
			MonoEnumInfo.GetInfo(enumType, out monoEnumInfo);
			string text2 = value.ToString();
			if (text2 == "0")
			{
				text = Enum.GetName(enumType, value);
				if (text == null)
				{
					text = text2;
				}
				return text;
			}
			switch (((Enum)monoEnumInfo.values.GetValue(0)).GetTypeCode())
			{
			case TypeCode.SByte:
			{
				sbyte b = (sbyte)value;
				for (int i = monoEnumInfo.values.Length - 1; i >= 0; i--)
				{
					sbyte b2 = (sbyte)monoEnumInfo.values.GetValue(i);
					if ((int)b2 != 0)
					{
						if (((int)b & (int)b2) == (int)b2)
						{
							text = monoEnumInfo.names[i] + ((!(text == string.Empty)) ? ", " : string.Empty) + text;
							b = (sbyte)((int)b - (int)b2);
						}
					}
				}
				if ((int)b != 0)
				{
					return text2;
				}
				break;
			}
			case TypeCode.Byte:
			{
				byte b3 = (byte)value;
				for (int j = monoEnumInfo.values.Length - 1; j >= 0; j--)
				{
					byte b4 = (byte)monoEnumInfo.values.GetValue(j);
					if (b4 != 0)
					{
						if ((b3 & b4) == b4)
						{
							text = monoEnumInfo.names[j] + ((!(text == string.Empty)) ? ", " : string.Empty) + text;
							b3 -= b4;
						}
					}
				}
				if (b3 != 0)
				{
					return text2;
				}
				break;
			}
			case TypeCode.Int16:
			{
				short num = (short)value;
				for (int k = monoEnumInfo.values.Length - 1; k >= 0; k--)
				{
					short num2 = (short)monoEnumInfo.values.GetValue(k);
					if (num2 != 0)
					{
						if ((num & num2) == num2)
						{
							text = monoEnumInfo.names[k] + ((!(text == string.Empty)) ? ", " : string.Empty) + text;
							num -= num2;
						}
					}
				}
				if (num != 0)
				{
					return text2;
				}
				break;
			}
			case TypeCode.UInt16:
			{
				ushort num3 = (ushort)value;
				for (int l = monoEnumInfo.values.Length - 1; l >= 0; l--)
				{
					ushort num4 = (ushort)monoEnumInfo.values.GetValue(l);
					if (num4 != 0)
					{
						if ((num3 & num4) == num4)
						{
							text = monoEnumInfo.names[l] + ((!(text == string.Empty)) ? ", " : string.Empty) + text;
							num3 -= num4;
						}
					}
				}
				if (num3 != 0)
				{
					return text2;
				}
				break;
			}
			case TypeCode.Int32:
			{
				int num5 = (int)value;
				for (int m = monoEnumInfo.values.Length - 1; m >= 0; m--)
				{
					int num6 = (int)monoEnumInfo.values.GetValue(m);
					if (num6 != 0)
					{
						if ((num5 & num6) == num6)
						{
							text = monoEnumInfo.names[m] + ((!(text == string.Empty)) ? ", " : string.Empty) + text;
							num5 -= num6;
						}
					}
				}
				if (num5 != 0)
				{
					return text2;
				}
				break;
			}
			case TypeCode.UInt32:
			{
				uint num7 = (uint)value;
				for (int n = monoEnumInfo.values.Length - 1; n >= 0; n--)
				{
					uint num8 = (uint)monoEnumInfo.values.GetValue(n);
					if (num8 != 0U)
					{
						if ((num7 & num8) == num8)
						{
							text = monoEnumInfo.names[n] + ((!(text == string.Empty)) ? ", " : string.Empty) + text;
							num7 -= num8;
						}
					}
				}
				if (num7 != 0U)
				{
					return text2;
				}
				break;
			}
			case TypeCode.Int64:
			{
				long num9 = (long)value;
				for (int num10 = monoEnumInfo.values.Length - 1; num10 >= 0; num10--)
				{
					long num11 = (long)monoEnumInfo.values.GetValue(num10);
					if (num11 != 0L)
					{
						if ((num9 & num11) == num11)
						{
							text = monoEnumInfo.names[num10] + ((!(text == string.Empty)) ? ", " : string.Empty) + text;
							num9 -= num11;
						}
					}
				}
				if (num9 != 0L)
				{
					return text2;
				}
				break;
			}
			case TypeCode.UInt64:
			{
				ulong num12 = (ulong)value;
				for (int num13 = monoEnumInfo.values.Length - 1; num13 >= 0; num13--)
				{
					ulong num14 = (ulong)monoEnumInfo.values.GetValue(num13);
					if (num14 != 0UL)
					{
						if ((num12 & num14) == num14)
						{
							text = monoEnumInfo.names[num13] + ((!(text == string.Empty)) ? ", " : string.Empty) + text;
							num12 -= num14;
						}
					}
				}
				if (num12 != 0UL)
				{
					return text2;
				}
				break;
			}
			}
			if (text == string.Empty)
			{
				return text2;
			}
			return text;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00022EC8 File Offset: 0x000210C8
		[ComVisible(true)]
		public static string Format(Type enumType, object value, string format)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("enumType is not an Enum type.", "enumType");
			}
			Type type = value.GetType();
			Type underlyingType = Enum.GetUnderlyingType(enumType);
			if (type.IsEnum)
			{
				if (type != enumType)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Object must be the same type as the enum. The type passed in was {0}; the enum type was {1}.", new object[]
					{
						type.FullName,
						enumType.FullName
					}));
				}
			}
			else if (type != underlyingType)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Enum underlying type and the object must be the same type or object. Type passed in was {0}; the enum underlying type was {1}.", new object[]
				{
					type.FullName,
					underlyingType.FullName
				}));
			}
			if (format.Length != 1)
			{
				throw new FormatException("Format String can be only \"G\",\"g\",\"X\",\"x\",\"F\",\"f\",\"D\" or \"d\".");
			}
			char c = format[0];
			string text;
			if (c == 'G' || c == 'g')
			{
				if (!enumType.IsDefined(typeof(FlagsAttribute), false))
				{
					text = Enum.GetName(enumType, value);
					if (text == null)
					{
						text = value.ToString();
					}
					return text;
				}
				c = 'f';
			}
			if (c == 'f' || c == 'F')
			{
				return Enum.FormatFlags(enumType, value);
			}
			text = string.Empty;
			char c2 = c;
			if (c2 != 'D')
			{
				if (c2 == 'X')
				{
					return Enum.FormatSpecifier_X(enumType, value, true);
				}
				if (c2 != 'd')
				{
					if (c2 != 'x')
					{
						throw new FormatException("Format String can be only \"G\",\"g\",\"X\",\"x\",\"F\",\"f\",\"D\" or \"d\".");
					}
					return Enum.FormatSpecifier_X(enumType, value, false);
				}
			}
			if (underlyingType == typeof(ulong))
			{
				text = Convert.ToUInt64(value).ToString();
			}
			else
			{
				text = Convert.ToInt64(value).ToString();
			}
			return text;
		}

		// Token: 0x040002FB RID: 763
		private static char[] split_char = new char[]
		{
			','
		};
	}
}
