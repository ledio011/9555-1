using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Mono.Security;

namespace System
{
	// Token: 0x02000106 RID: 262
	[ComVisible(true)]
	[Serializable]
	public struct Guid : IComparable<Guid>, IEquatable<Guid>, IComparable, IFormattable
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x0002798C File Offset: 0x00025B8C
		public Guid(byte[] b)
		{
			Guid.CheckArray(b, 16);
			this._a = BitConverterLE.ToInt32(b, 0);
			this._b = BitConverterLE.ToInt16(b, 4);
			this._c = BitConverterLE.ToInt16(b, 6);
			this._d = b[8];
			this._e = b[9];
			this._f = b[10];
			this._g = b[11];
			this._h = b[12];
			this._i = b[13];
			this._j = b[14];
			this._k = b[15];
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00027A18 File Offset: 0x00025C18
		public Guid(string g)
		{
			Guid.CheckNull(g);
			g = g.Trim();
			Guid.GuidParser guidParser = new Guid.GuidParser(g);
			Guid guid = guidParser.Parse();
			this = guid;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00027A48 File Offset: 0x00025C48
		public Guid(int a, short b, short c, byte[] d)
		{
			Guid.CheckArray(d, 8);
			this._a = a;
			this._b = b;
			this._c = c;
			this._d = d[0];
			this._e = d[1];
			this._f = d[2];
			this._g = d[3];
			this._h = d[4];
			this._i = d[5];
			this._j = d[6];
			this._k = d[7];
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00027AC4 File Offset: 0x00025CC4
		public Guid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this._a = a;
			this._b = b;
			this._c = c;
			this._d = d;
			this._e = e;
			this._f = f;
			this._g = g;
			this._h = h;
			this._i = i;
			this._j = j;
			this._k = k;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00027B28 File Offset: 0x00025D28
		[CLSCompliant(false)]
		public Guid(uint a, ushort b, ushort c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this = new Guid((int)a, (short)b, (short)c, d, e, f, g, h, i, j, k);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00027B50 File Offset: 0x00025D50
		static Guid()
		{
			if (MonoTouchAOTHelper.FalseFlag)
			{
				GenericComparer<Guid> genericComparer = new GenericComparer<Guid>();
				GenericEqualityComparer<Guid> genericEqualityComparer = new GenericEqualityComparer<Guid>();
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00027B94 File Offset: 0x00025D94
		private static void CheckNull(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException(Locale.GetText("Value cannot be null."));
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00027BAC File Offset: 0x00025DAC
		private static void CheckLength(byte[] o, int l)
		{
			if (o.Length != l)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Array should be exactly {0} bytes long."), l));
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00027BD4 File Offset: 0x00025DD4
		private static void CheckArray(byte[] o, int l)
		{
			Guid.CheckNull(o);
			Guid.CheckLength(o, l);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00027BE4 File Offset: 0x00025DE4
		private static int Compare(int x, int y)
		{
			if (x < y)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00027BF0 File Offset: 0x00025DF0
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is Guid))
			{
				throw new ArgumentException("value", Locale.GetText("Argument of System.Guid.CompareTo should be a Guid."));
			}
			return this.CompareTo((Guid)value);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00027C28 File Offset: 0x00025E28
		public override bool Equals(object o)
		{
			return o is Guid && this.CompareTo((Guid)o) == 0;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00027C48 File Offset: 0x00025E48
		public int CompareTo(Guid value)
		{
			if (this._a != value._a)
			{
				return Guid.Compare(this._a, value._a);
			}
			if (this._b != value._b)
			{
				return Guid.Compare((int)this._b, (int)value._b);
			}
			if (this._c != value._c)
			{
				return Guid.Compare((int)this._c, (int)value._c);
			}
			if (this._d != value._d)
			{
				return Guid.Compare((int)this._d, (int)value._d);
			}
			if (this._e != value._e)
			{
				return Guid.Compare((int)this._e, (int)value._e);
			}
			if (this._f != value._f)
			{
				return Guid.Compare((int)this._f, (int)value._f);
			}
			if (this._g != value._g)
			{
				return Guid.Compare((int)this._g, (int)value._g);
			}
			if (this._h != value._h)
			{
				return Guid.Compare((int)this._h, (int)value._h);
			}
			if (this._i != value._i)
			{
				return Guid.Compare((int)this._i, (int)value._i);
			}
			if (this._j != value._j)
			{
				return Guid.Compare((int)this._j, (int)value._j);
			}
			if (this._k != value._k)
			{
				return Guid.Compare((int)this._k, (int)value._k);
			}
			return 0;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00027DF0 File Offset: 0x00025FF0
		public bool Equals(Guid g)
		{
			return this.CompareTo(g) == 0;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00027DFC File Offset: 0x00025FFC
		public override int GetHashCode()
		{
			int num = this._a;
			num ^= ((int)this._b << 16 | (int)this._c);
			num ^= (int)this._d << 24;
			num ^= (int)this._e << 16;
			num ^= (int)this._f << 8;
			num ^= (int)this._g;
			num ^= (int)this._h << 24;
			num ^= (int)this._i << 16;
			num ^= (int)this._j << 8;
			return num ^ (int)this._k;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00027E7C File Offset: 0x0002607C
		private static char ToHex(int b)
		{
			return (char)((b >= 10) ? (97 + b - 10) : (48 + b));
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00027E98 File Offset: 0x00026098
		public static Guid NewGuid()
		{
			byte[] array = new byte[16];
			object rngAccess = Guid._rngAccess;
			lock (rngAccess)
			{
				if (Guid._rng == null)
				{
					Guid._rng = RandomNumberGenerator.Create();
				}
				Guid._rng.GetBytes(array);
			}
			Guid result = new Guid(array);
			result._d = ((result._d & 63) | 128);
			result._c = (short)(((long)result._c & 4095L) | 16384L);
			return result;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00027F34 File Offset: 0x00026134
		internal static byte[] FastNewGuidArray()
		{
			byte[] array = new byte[16];
			object rngAccess = Guid._rngAccess;
			lock (rngAccess)
			{
				if (Guid._rng != null)
				{
					Guid._fastRng = Guid._rng;
				}
				if (Guid._fastRng == null)
				{
					Guid._fastRng = new RNGCryptoServiceProvider();
				}
				Guid._fastRng.GetBytes(array);
			}
			array[8] = ((array[8] & 63) | 128);
			array[7] = ((array[7] & 15) | 64);
			return array;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00027FC4 File Offset: 0x000261C4
		public byte[] ToByteArray()
		{
			byte[] array = new byte[16];
			int num = 0;
			byte[] bytes = BitConverterLE.GetBytes(this._a);
			for (int i = 0; i < 4; i++)
			{
				array[num++] = bytes[i];
			}
			bytes = BitConverterLE.GetBytes(this._b);
			for (int i = 0; i < 2; i++)
			{
				array[num++] = bytes[i];
			}
			bytes = BitConverterLE.GetBytes(this._c);
			for (int i = 0; i < 2; i++)
			{
				array[num++] = bytes[i];
			}
			array[8] = this._d;
			array[9] = this._e;
			array[10] = this._f;
			array[11] = this._g;
			array[12] = this._h;
			array[13] = this._i;
			array[14] = this._j;
			array[15] = this._k;
			return array;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000280A4 File Offset: 0x000262A4
		private static void AppendInt(StringBuilder builder, int value)
		{
			builder.Append(Guid.ToHex(value >> 28 & 15));
			builder.Append(Guid.ToHex(value >> 24 & 15));
			builder.Append(Guid.ToHex(value >> 20 & 15));
			builder.Append(Guid.ToHex(value >> 16 & 15));
			builder.Append(Guid.ToHex(value >> 12 & 15));
			builder.Append(Guid.ToHex(value >> 8 & 15));
			builder.Append(Guid.ToHex(value >> 4 & 15));
			builder.Append(Guid.ToHex(value & 15));
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00028144 File Offset: 0x00026344
		private static void AppendShort(StringBuilder builder, short value)
		{
			builder.Append(Guid.ToHex(value >> 12 & 15));
			builder.Append(Guid.ToHex(value >> 8 & 15));
			builder.Append(Guid.ToHex(value >> 4 & 15));
			builder.Append(Guid.ToHex((int)(value & 15)));
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00028198 File Offset: 0x00026398
		private static void AppendByte(StringBuilder builder, byte value)
		{
			builder.Append(Guid.ToHex(value >> 4 & 15));
			builder.Append(Guid.ToHex((int)(value & 15)));
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000281BC File Offset: 0x000263BC
		private string BaseToString(bool h, bool p, bool b)
		{
			StringBuilder stringBuilder = new StringBuilder(40);
			if (p)
			{
				stringBuilder.Append('(');
			}
			else if (b)
			{
				stringBuilder.Append('{');
			}
			Guid.AppendInt(stringBuilder, this._a);
			if (h)
			{
				stringBuilder.Append('-');
			}
			Guid.AppendShort(stringBuilder, this._b);
			if (h)
			{
				stringBuilder.Append('-');
			}
			Guid.AppendShort(stringBuilder, this._c);
			if (h)
			{
				stringBuilder.Append('-');
			}
			Guid.AppendByte(stringBuilder, this._d);
			Guid.AppendByte(stringBuilder, this._e);
			if (h)
			{
				stringBuilder.Append('-');
			}
			Guid.AppendByte(stringBuilder, this._f);
			Guid.AppendByte(stringBuilder, this._g);
			Guid.AppendByte(stringBuilder, this._h);
			Guid.AppendByte(stringBuilder, this._i);
			Guid.AppendByte(stringBuilder, this._j);
			Guid.AppendByte(stringBuilder, this._k);
			if (p)
			{
				stringBuilder.Append(')');
			}
			else if (b)
			{
				stringBuilder.Append('}');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x000282E0 File Offset: 0x000264E0
		public override string ToString()
		{
			return this.BaseToString(true, false, false);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x000282EC File Offset: 0x000264EC
		public string ToString(string format)
		{
			bool h = true;
			bool p = false;
			bool b = false;
			if (format != null)
			{
				string a = format.ToLowerInvariant();
				if (a == "b")
				{
					b = true;
				}
				else if (a == "p")
				{
					p = true;
				}
				else if (a == "n")
				{
					h = false;
				}
				else if (a != "d" && a != string.Empty)
				{
					throw new FormatException(Locale.GetText("Argument to Guid.ToString(string format) should be \"b\", \"B\", \"d\", \"D\", \"n\", \"N\", \"p\" or \"P\""));
				}
			}
			return this.BaseToString(h, p, b);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002838C File Offset: 0x0002658C
		public string ToString(string format, IFormatProvider provider)
		{
			return this.ToString(format);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00028398 File Offset: 0x00026598
		public static bool operator ==(Guid a, Guid b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000283A4 File Offset: 0x000265A4
		public static bool operator !=(Guid a, Guid b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04000449 RID: 1097
		private int _a;

		// Token: 0x0400044A RID: 1098
		private short _b;

		// Token: 0x0400044B RID: 1099
		private short _c;

		// Token: 0x0400044C RID: 1100
		private byte _d;

		// Token: 0x0400044D RID: 1101
		private byte _e;

		// Token: 0x0400044E RID: 1102
		private byte _f;

		// Token: 0x0400044F RID: 1103
		private byte _g;

		// Token: 0x04000450 RID: 1104
		private byte _h;

		// Token: 0x04000451 RID: 1105
		private byte _i;

		// Token: 0x04000452 RID: 1106
		private byte _j;

		// Token: 0x04000453 RID: 1107
		private byte _k;

		// Token: 0x04000454 RID: 1108
		public static readonly Guid Empty = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

		// Token: 0x04000455 RID: 1109
		private static object _rngAccess = new object();

		// Token: 0x04000456 RID: 1110
		private static RandomNumberGenerator _rng;

		// Token: 0x04000457 RID: 1111
		private static RandomNumberGenerator _fastRng;

		// Token: 0x02000107 RID: 263
		internal class GuidParser
		{
			// Token: 0x06000A74 RID: 2676 RVA: 0x000283B4 File Offset: 0x000265B4
			public GuidParser(string src)
			{
				this._src = src;
				this.Reset();
			}

			// Token: 0x06000A75 RID: 2677 RVA: 0x000283CC File Offset: 0x000265CC
			private void Reset()
			{
				this._cur = 0;
				this._length = this._src.Length;
			}

			// Token: 0x06000A76 RID: 2678 RVA: 0x000283E8 File Offset: 0x000265E8
			private bool AtEnd()
			{
				return this._cur >= this._length;
			}

			// Token: 0x06000A77 RID: 2679 RVA: 0x000283FC File Offset: 0x000265FC
			private void ThrowFormatException()
			{
				throw new FormatException(Locale.GetText("Invalid format for Guid.Guid(string)."));
			}

			// Token: 0x06000A78 RID: 2680 RVA: 0x00028410 File Offset: 0x00026610
			private ulong ParseHex(int length, bool strictLength)
			{
				ulong num = 0UL;
				bool flag = false;
				int num2 = 0;
				while (!flag && num2 < length)
				{
					if (this.AtEnd())
					{
						if (strictLength || num2 == 0)
						{
							this.ThrowFormatException();
						}
						else
						{
							flag = true;
						}
					}
					else
					{
						char c = char.ToLowerInvariant(this._src[this._cur]);
						if (char.IsDigit(c))
						{
							num = num * 16UL + (ulong)c - 48UL;
							this._cur++;
						}
						else if (c >= 'a' && c <= 'f')
						{
							num = num * 16UL + (ulong)c - 97UL + 10UL;
							this._cur++;
						}
						else if (strictLength || num2 == 0)
						{
							this.ThrowFormatException();
						}
						else
						{
							flag = true;
						}
					}
					num2++;
				}
				return num;
			}

			// Token: 0x06000A79 RID: 2681 RVA: 0x000284F4 File Offset: 0x000266F4
			private bool ParseOptChar(char c)
			{
				if (!this.AtEnd() && this._src[this._cur] == c)
				{
					this._cur++;
					return true;
				}
				return false;
			}

			// Token: 0x06000A7A RID: 2682 RVA: 0x0002852C File Offset: 0x0002672C
			private void ParseChar(char c)
			{
				if (!this.ParseOptChar(c))
				{
					this.ThrowFormatException();
				}
			}

			// Token: 0x06000A7B RID: 2683 RVA: 0x00028550 File Offset: 0x00026750
			private Guid ParseGuid1()
			{
				bool flag = true;
				char c = '}';
				byte[] array = new byte[8];
				bool flag2 = this.ParseOptChar('{');
				if (!flag2)
				{
					flag2 = this.ParseOptChar('(');
					if (flag2)
					{
						c = ')';
					}
				}
				int a = (int)this.ParseHex(8, true);
				if (flag2)
				{
					this.ParseChar('-');
				}
				else
				{
					flag = this.ParseOptChar('-');
				}
				short b = (short)this.ParseHex(4, true);
				if (flag)
				{
					this.ParseChar('-');
				}
				short c2 = (short)this.ParseHex(4, true);
				if (flag)
				{
					this.ParseChar('-');
				}
				for (int i = 0; i < 8; i++)
				{
					array[i] = (byte)this.ParseHex(2, true);
					if (i == 1 && flag)
					{
						this.ParseChar('-');
					}
				}
				if (flag2 && !this.ParseOptChar(c))
				{
					this.ThrowFormatException();
				}
				return new Guid(a, b, c2, array);
			}

			// Token: 0x06000A7C RID: 2684 RVA: 0x00028644 File Offset: 0x00026844
			private void ParseHexPrefix()
			{
				this.ParseChar('0');
				this.ParseChar('x');
			}

			// Token: 0x06000A7D RID: 2685 RVA: 0x00028658 File Offset: 0x00026858
			private Guid ParseGuid2()
			{
				byte[] array = new byte[8];
				this.ParseChar('{');
				this.ParseHexPrefix();
				int a = (int)this.ParseHex(8, false);
				this.ParseChar(',');
				this.ParseHexPrefix();
				short b = (short)this.ParseHex(4, false);
				this.ParseChar(',');
				this.ParseHexPrefix();
				short c = (short)this.ParseHex(4, false);
				this.ParseChar(',');
				this.ParseChar('{');
				for (int i = 0; i < 8; i++)
				{
					this.ParseHexPrefix();
					array[i] = (byte)this.ParseHex(2, false);
					if (i != 7)
					{
						this.ParseChar(',');
					}
				}
				this.ParseChar('}');
				this.ParseChar('}');
				return new Guid(a, b, c, array);
			}

			// Token: 0x06000A7E RID: 2686 RVA: 0x00028718 File Offset: 0x00026918
			public Guid Parse()
			{
				Guid result;
				try
				{
					result = this.ParseGuid1();
				}
				catch (FormatException)
				{
					this.Reset();
					result = this.ParseGuid2();
				}
				if (!this.AtEnd())
				{
					this.ThrowFormatException();
				}
				return result;
			}

			// Token: 0x04000458 RID: 1112
			private string _src;

			// Token: 0x04000459 RID: 1113
			private int _length;

			// Token: 0x0400045A RID: 1114
			private int _cur;
		}
	}
}
