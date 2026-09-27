using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Globalization.Unicode;

namespace System
{
	// Token: 0x0200038B RID: 907
	[ComVisible(true)]
	[Serializable]
	public sealed class String : IEnumerable<char>, IComparable<string>, IEquatable<string>, IEnumerable, ICloneable, IComparable, IConvertible
	{
		// Token: 0x06001A94 RID: 6804
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public unsafe extern String(char* value);

		// Token: 0x06001A95 RID: 6805
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public unsafe extern String(char* value, int startIndex, int length);

		// Token: 0x06001A96 RID: 6806
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public unsafe extern String(sbyte* value);

		// Token: 0x06001A97 RID: 6807
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public unsafe extern String(sbyte* value, int startIndex, int length);

		// Token: 0x06001A98 RID: 6808
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public unsafe extern String(sbyte* value, int startIndex, int length, Encoding enc);

		// Token: 0x06001A99 RID: 6809
		[MethodImpl(4096)]
		public extern String(char[] value, int startIndex, int length);

		// Token: 0x06001A9A RID: 6810
		[MethodImpl(4096)]
		public extern String(char[] value);

		// Token: 0x06001A9B RID: 6811
		[MethodImpl(4096)]
		public extern String(char c, int count);

		// Token: 0x06001A9D RID: 6813 RVA: 0x00062760 File Offset: 0x00060960
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this, provider);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0006276C File Offset: 0x0006096C
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this, provider);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00062778 File Offset: 0x00060978
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this, provider);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00062784 File Offset: 0x00060984
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this, provider);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00062790 File Offset: 0x00060990
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this, provider);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x0006279C File Offset: 0x0006099C
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this, provider);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x000627A8 File Offset: 0x000609A8
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this, provider);
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x000627B4 File Offset: 0x000609B4
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this, provider);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x000627C0 File Offset: 0x000609C0
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this, provider);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x000627CC File Offset: 0x000609CC
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this, provider);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x000627D8 File Offset: 0x000609D8
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this, provider);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x000627E4 File Offset: 0x000609E4
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("type");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00062800 File Offset: 0x00060A00
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this, provider);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0006280C File Offset: 0x00060A0C
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this, provider);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00062818 File Offset: 0x00060A18
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this, provider);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00062824 File Offset: 0x00060A24
		IEnumerator<char> IEnumerable<char>.GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x0006282C File Offset: 0x00060A2C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x00062834 File Offset: 0x00060A34
		public unsafe static bool Equals(string a, string b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			int i = a.length;
			if (i != b.length)
			{
				return false;
			}
			char* ptr = &a.start_char;
			char* ptr2 = &b.start_char;
			while (i >= 8)
			{
				if (*(int*)ptr != *(int*)ptr2 || *(int*)(ptr + 2) != *(int*)(ptr2 + 2) || *(int*)(ptr + 4) != *(int*)(ptr2 + 4) || *(int*)(ptr + 6) != *(int*)(ptr2 + 6))
				{
					return false;
				}
				ptr += 8;
				ptr2 += 8;
				i -= 8;
			}
			if (i >= 4)
			{
				if (*(int*)ptr != *(int*)ptr2 || *(int*)(ptr + 2) != *(int*)(ptr2 + 2))
				{
					return false;
				}
				ptr += 4;
				ptr2 += 4;
				i -= 4;
			}
			if (i > 1)
			{
				if (*(int*)ptr != *(int*)ptr2)
				{
					return false;
				}
				ptr += 2;
				ptr2 += 2;
				i -= 2;
			}
			return i == 0 || *ptr == *ptr2;
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00062934 File Offset: 0x00060B34
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public override bool Equals(object obj)
		{
			return string.Equals(this, obj as string);
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x00062944 File Offset: 0x00060B44
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public bool Equals(string value)
		{
			return string.Equals(this, value);
		}

		// Token: 0x170004C5 RID: 1221
		[IndexerName("Chars")]
		public unsafe char this[int index]
		{
			get
			{
				if (index < 0 || index >= this.length)
				{
					throw new IndexOutOfRangeException();
				}
				return *(ref this.start_char + index * 2);
			}
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x00062984 File Offset: 0x00060B84
		public object Clone()
		{
			return this;
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00062988 File Offset: 0x00060B88
		public TypeCode GetTypeCode()
		{
			return TypeCode.String;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0006298C File Offset: 0x00060B8C
		public unsafe void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (sourceIndex < 0)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", "Cannot be negative");
			}
			if (destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", "Cannot be negative.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative.");
			}
			if (sourceIndex > this.Length - count)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", "sourceIndex + count > Length");
			}
			if (destinationIndex > destination.Length - count)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", "destinationIndex + count > destination.Length");
			}
			fixed (char* ptr = ref (destination != null && destination.Length != 0) ? ref destination[0] : ref *null)
			{
				fixed (string text = this)
				{
					fixed (char* ptr2 = text + RuntimeHelpers.OffsetToStringData / 2)
					{
						string.CharCopy(ptr + destinationIndex, ptr2 + sourceIndex, count);
						ptr = null;
						text = null;
						return;
					}
				}
			}
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00062A6C File Offset: 0x00060C6C
		public char[] ToCharArray()
		{
			return this.ToCharArray(0, this.length);
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00062A7C File Offset: 0x00060C7C
		public unsafe char[] ToCharArray(int startIndex, int length)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "< 0");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "< 0");
			}
			if (startIndex > this.length - length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Must be greater than the length of the string.");
			}
			char[] array = new char[length];
			fixed (char* dest = ref (array != null && array.Length != 0) ? ref array[0] : ref *null)
			{
				fixed (string text = this)
				{
					fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
					{
						string.CharCopy(dest, ptr + startIndex, length);
						dest = null;
						text = null;
						return array;
					}
				}
			}
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x00062B18 File Offset: 0x00060D18
		public string[] Split(params char[] separator)
		{
			return this.Split(separator, int.MaxValue);
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00062B28 File Offset: 0x00060D28
		public string[] Split(char[] separator, int count)
		{
			if (separator == null || separator.Length == 0)
			{
				separator = string.WhiteChars;
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count == 0)
			{
				return new string[0];
			}
			if (count == 1)
			{
				return new string[]
				{
					this
				};
			}
			return this.InternalSplit(separator, count, 0);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00062B84 File Offset: 0x00060D84
		[MonoDocumentationNote("code should be moved to managed")]
		[ComVisible(false)]
		public string[] Split(char[] separator, int count, StringSplitOptions options)
		{
			if (separator == null || separator.Length == 0)
			{
				return this.Split(string.WhiteChars, count, options);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than zero.");
			}
			if (options != StringSplitOptions.None && options != StringSplitOptions.RemoveEmptyEntries)
			{
				throw new ArgumentException("Illegal enum value: " + options + ".");
			}
			if (count == 0)
			{
				return new string[0];
			}
			return this.InternalSplit(separator, count, (int)options);
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x00062C04 File Offset: 0x00060E04
		[ComVisible(false)]
		public string[] Split(string[] separator, int count, StringSplitOptions options)
		{
			if (separator == null || separator.Length == 0)
			{
				return this.Split(string.WhiteChars, count, options);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than zero.");
			}
			if (options != StringSplitOptions.None && options != StringSplitOptions.RemoveEmptyEntries)
			{
				throw new ArgumentException("Illegal enum value: " + options + ".");
			}
			if (count == 1)
			{
				return new string[]
				{
					this
				};
			}
			bool flag = (options & StringSplitOptions.RemoveEmptyEntries) == StringSplitOptions.RemoveEmptyEntries;
			if (count == 0 || (this == string.Empty && flag))
			{
				return new string[0];
			}
			List<string> list = new List<string>();
			int i = 0;
			int num = 0;
			while (i < this.Length)
			{
				int num2 = -1;
				int num3 = int.MaxValue;
				for (int j = 0; j < separator.Length; j++)
				{
					string text = separator[j];
					if (text != null && !(text == string.Empty))
					{
						int num4 = this.IndexOf(text, i);
						if (num4 > -1 && num4 < num3)
						{
							num2 = j;
							num3 = num4;
						}
					}
				}
				if (num2 == -1)
				{
					break;
				}
				if (num3 != i || !flag)
				{
					if (list.Count == count - 1)
					{
						break;
					}
					list.Add(this.Substring(i, num3 - i));
				}
				i = num3 + separator[num2].Length;
				num++;
			}
			if (num == 0)
			{
				return new string[]
				{
					this
				};
			}
			if (flag && num != 0 && i == this.Length && list.Count == 0)
			{
				return new string[0];
			}
			if (!flag || i != this.Length)
			{
				list.Add(this.Substring(i));
			}
			return list.ToArray();
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00062DDC File Offset: 0x00060FDC
		[ComVisible(false)]
		public string[] Split(char[] separator, StringSplitOptions options)
		{
			return this.Split(separator, int.MaxValue, options);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00062DEC File Offset: 0x00060FEC
		[ComVisible(false)]
		public string[] Split(string[] separator, StringSplitOptions options)
		{
			return this.Split(separator, int.MaxValue, options);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00062DFC File Offset: 0x00060FFC
		public string Substring(int startIndex)
		{
			if (startIndex == 0)
			{
				return this;
			}
			if (startIndex < 0 || startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			return this.SubstringUnchecked(startIndex, this.length - startIndex);
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00062E34 File Offset: 0x00061034
		public string Substring(int startIndex, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Cannot be negative.");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative.");
			}
			if (startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot exceed length of string.");
			}
			if (startIndex > this.length - length)
			{
				throw new ArgumentOutOfRangeException("length", "startIndex + length > this.length");
			}
			if (startIndex == 0 && length == this.length)
			{
				return this;
			}
			return this.SubstringUnchecked(startIndex, length);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00062EC8 File Offset: 0x000610C8
		internal unsafe string SubstringUnchecked(int startIndex, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(length);
			fixed (string text2 = text)
			{
				fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text3 = this)
					{
						fixed (char* ptr = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(dest, ptr + startIndex, length);
							text2 = null;
							text3 = null;
							return text;
						}
					}
				}
			}
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00062F14 File Offset: 0x00061114
		public string Trim()
		{
			if (this.length == 0)
			{
				return string.Empty;
			}
			int num = this.FindNotWhiteSpace(0, this.length, 1);
			if (num == this.length)
			{
				return string.Empty;
			}
			int num2 = this.FindNotWhiteSpace(this.length - 1, num, -1);
			int num3 = num2 - num + 1;
			if (num3 == this.length)
			{
				return this;
			}
			return this.SubstringUnchecked(num, num3);
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00062F80 File Offset: 0x00061180
		public string Trim(params char[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
			{
				return this.Trim();
			}
			if (this.length == 0)
			{
				return string.Empty;
			}
			int num = this.FindNotInTable(0, this.length, 1, trimChars);
			if (num == this.length)
			{
				return string.Empty;
			}
			int num2 = this.FindNotInTable(this.length - 1, num, -1, trimChars);
			int num3 = num2 - num + 1;
			if (num3 == this.length)
			{
				return this;
			}
			return this.SubstringUnchecked(num, num3);
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00063004 File Offset: 0x00061204
		public string TrimStart(params char[] trimChars)
		{
			if (this.length == 0)
			{
				return string.Empty;
			}
			int num;
			if (trimChars == null || trimChars.Length == 0)
			{
				num = this.FindNotWhiteSpace(0, this.length, 1);
			}
			else
			{
				num = this.FindNotInTable(0, this.length, 1, trimChars);
			}
			if (num == 0)
			{
				return this;
			}
			return this.SubstringUnchecked(num, this.length - num);
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0006306C File Offset: 0x0006126C
		public string TrimEnd(params char[] trimChars)
		{
			if (this.length == 0)
			{
				return string.Empty;
			}
			int num;
			if (trimChars == null || trimChars.Length == 0)
			{
				num = this.FindNotWhiteSpace(this.length - 1, -1, -1);
			}
			else
			{
				num = this.FindNotInTable(this.length - 1, -1, -1, trimChars);
			}
			num++;
			if (num == this.length)
			{
				return this;
			}
			return this.SubstringUnchecked(0, num);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000630DC File Offset: 0x000612DC
		private int FindNotWhiteSpace(int pos, int target, int change)
		{
			while (pos != target)
			{
				char c = this[pos];
				if (c < '\u0085')
				{
					if (c != ' ' && (c < '\t' || c > '\r'))
					{
						return pos;
					}
				}
				else if (c != '\u00a0' && c != '﻿' && c != '\u3000' && c != '\u0085' && c != '\u1680' && c != '\u2028' && c != '\u2029' && c != '\u202f' && c != '\u205f' && (c < '\u2000' || c > '​'))
				{
					return pos;
				}
				pos += change;
			}
			return pos;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000631A8 File Offset: 0x000613A8
		private unsafe int FindNotInTable(int pos, int target, int change, char[] table)
		{
			fixed (char* ptr = ref (table != null && table.Length != 0) ? ref table[0] : ref *null)
			{
				fixed (string text = this)
				{
					fixed (char* ptr2 = text + RuntimeHelpers.OffsetToStringData / 2)
					{
						while (pos != target)
						{
							char c = ptr2[pos];
							int i;
							for (i = 0; i < table.Length; i++)
							{
								if (c == ptr[i])
								{
									break;
								}
							}
							if (i == table.Length)
							{
								return pos;
							}
							pos += change;
						}
						ptr = null;
						text = null;
						return pos;
					}
				}
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00063234 File Offset: 0x00061434
		public static int Compare(string strA, string strB)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00063248 File Offset: 0x00061448
		public static int Compare(string strA, string strB, bool ignoreCase)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, (!ignoreCase) ? CompareOptions.None : CompareOptions.IgnoreCase);
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00063268 File Offset: 0x00061468
		public static int Compare(string strA, string strB, bool ignoreCase, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.CompareInfo.Compare(strA, strB, (!ignoreCase) ? CompareOptions.None : CompareOptions.IgnoreCase);
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00063298 File Offset: 0x00061498
		public static int Compare(string strA, int indexA, string strB, int indexB, int length)
		{
			return string.Compare(strA, indexA, strB, indexB, length, false, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000632AC File Offset: 0x000614AC
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase)
		{
			return string.Compare(strA, indexA, strB, indexB, length, ignoreCase, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000632C0 File Offset: 0x000614C0
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (indexA > strA.Length || indexB > strB.Length || indexA < 0 || indexB < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (length == 0)
			{
				return 0;
			}
			if (strA == null)
			{
				if (strB == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (strB == null)
				{
					return 1;
				}
				CompareOptions options;
				if (ignoreCase)
				{
					options = CompareOptions.IgnoreCase;
				}
				else
				{
					options = CompareOptions.None;
				}
				int length2 = length;
				int length3 = length;
				if (length > strA.Length - indexA)
				{
					length2 = strA.Length - indexA;
				}
				if (length > strB.Length - indexB)
				{
					length3 = strB.Length - indexB;
				}
				return culture.CompareInfo.Compare(strA, indexA, length2, strB, indexB, length3, options);
			}
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x00063390 File Offset: 0x00061590
		public static int Compare(string strA, string strB, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return string.Compare(strA, strB, false, CultureInfo.CurrentCulture);
			case StringComparison.CurrentCultureIgnoreCase:
				return string.Compare(strA, strB, true, CultureInfo.CurrentCulture);
			case StringComparison.InvariantCulture:
				return string.Compare(strA, strB, false, CultureInfo.InvariantCulture);
			case StringComparison.InvariantCultureIgnoreCase:
				return string.Compare(strA, strB, true, CultureInfo.InvariantCulture);
			case StringComparison.Ordinal:
				return string.CompareOrdinalUnchecked(strA, 0, int.MaxValue, strB, 0, int.MaxValue);
			case StringComparison.OrdinalIgnoreCase:
				return string.CompareOrdinalCaseInsensitiveUnchecked(strA, 0, int.MaxValue, strB, 0, int.MaxValue);
			default:
			{
				string text = Locale.GetText("Invalid value '{0}' for StringComparison", new object[]
				{
					comparisonType
				});
				throw new ArgumentException(text, "comparisonType");
			}
			}
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00063448 File Offset: 0x00061648
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return string.Compare(strA, indexA, strB, indexB, length, false, CultureInfo.CurrentCulture);
			case StringComparison.CurrentCultureIgnoreCase:
				return string.Compare(strA, indexA, strB, indexB, length, true, CultureInfo.CurrentCulture);
			case StringComparison.InvariantCulture:
				return string.Compare(strA, indexA, strB, indexB, length, false, CultureInfo.InvariantCulture);
			case StringComparison.InvariantCultureIgnoreCase:
				return string.Compare(strA, indexA, strB, indexB, length, true, CultureInfo.InvariantCulture);
			case StringComparison.Ordinal:
				return string.CompareOrdinal(strA, indexA, strB, indexB, length);
			case StringComparison.OrdinalIgnoreCase:
				return string.CompareOrdinalCaseInsensitive(strA, indexA, strB, indexB, length);
			default:
			{
				string text = Locale.GetText("Invalid value '{0}' for StringComparison", new object[]
				{
					comparisonType
				});
				throw new ArgumentException(text, "comparisonType");
			}
			}
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00063504 File Offset: 0x00061704
		public static bool Equals(string a, string b, StringComparison comparisonType)
		{
			return string.Compare(a, b, comparisonType) == 0;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x00063514 File Offset: 0x00061714
		public bool Equals(string value, StringComparison comparisonType)
		{
			return string.Compare(value, this, comparisonType) == 0;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00063524 File Offset: 0x00061724
		public static int Compare(string strA, string strB, CultureInfo culture, CompareOptions options)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.CompareInfo.Compare(strA, strB, options);
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00063548 File Offset: 0x00061748
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, CultureInfo culture, CompareOptions options)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			int length2 = length;
			int length3 = length;
			if (length > strA.Length - indexA)
			{
				length2 = strA.Length - indexA;
			}
			if (length > strB.Length - indexB)
			{
				length3 = strB.Length - indexB;
			}
			return culture.CompareInfo.Compare(strA, indexA, length2, strB, indexB, length3, options);
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000635B4 File Offset: 0x000617B4
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is string))
			{
				throw new ArgumentException();
			}
			return string.Compare(this, (string)value);
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x000635DC File Offset: 0x000617DC
		public int CompareTo(string strB)
		{
			if (strB == null)
			{
				return 1;
			}
			return string.Compare(this, strB);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x000635F0 File Offset: 0x000617F0
		public static int CompareOrdinal(string strA, string strB)
		{
			return string.CompareOrdinalUnchecked(strA, 0, int.MaxValue, strB, 0, int.MaxValue);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00063608 File Offset: 0x00061808
		public static int CompareOrdinal(string strA, int indexA, string strB, int indexB, int length)
		{
			if (indexA > strA.Length || indexB > strB.Length || indexA < 0 || indexB < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return string.CompareOrdinalUnchecked(strA, indexA, length, strB, indexB, length);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00063658 File Offset: 0x00061858
		internal static int CompareOrdinalCaseInsensitive(string strA, int indexA, string strB, int indexB, int length)
		{
			if (indexA > strA.Length || indexB > strB.Length || indexA < 0 || indexB < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return string.CompareOrdinalCaseInsensitiveUnchecked(strA, indexA, length, strB, indexB, length);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x000636A8 File Offset: 0x000618A8
		internal unsafe static int CompareOrdinalUnchecked(string strA, int indexA, int lenA, string strB, int indexB, int lenB)
		{
			if (strA == null)
			{
				if (strB == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (strB == null)
				{
					return 1;
				}
				int num = Math.Min(lenA, strA.Length - indexA);
				int num2 = Math.Min(lenB, strB.Length - indexB);
				if (num == num2 && object.ReferenceEquals(strA, strB))
				{
					return 0;
				}
				fixed (char* ptr = strA + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (char* ptr2 = strB + RuntimeHelpers.OffsetToStringData / 2)
					{
						char* ptr3 = ptr + indexA;
						char* ptr4 = ptr3 + Math.Min(num, num2);
						char* ptr5 = ptr2 + indexB;
						while (ptr3 < ptr4)
						{
							if (*ptr3 != *ptr5)
							{
								return (int)(*ptr3 - *ptr5);
							}
							ptr3++;
							ptr5++;
						}
						return num - num2;
					}
				}
			}
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0006376C File Offset: 0x0006196C
		internal unsafe static int CompareOrdinalCaseInsensitiveUnchecked(string strA, int indexA, int lenA, string strB, int indexB, int lenB)
		{
			if (strA == null)
			{
				if (strB == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (strB == null)
				{
					return 1;
				}
				int num = Math.Min(lenA, strA.Length - indexA);
				int num2 = Math.Min(lenB, strB.Length - indexB);
				if (num == num2 && object.ReferenceEquals(strA, strB))
				{
					return 0;
				}
				fixed (char* ptr = strA + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (char* ptr2 = strB + RuntimeHelpers.OffsetToStringData / 2)
					{
						char* ptr3 = ptr + indexA;
						char* ptr4 = ptr3 + Math.Min(num, num2);
						char* ptr5 = ptr2 + indexB;
						while (ptr3 < ptr4)
						{
							if (*ptr3 != *ptr5)
							{
								char c = char.ToUpperInvariant(*ptr3);
								char c2 = char.ToUpperInvariant(*ptr5);
								if (c != c2)
								{
									return (int)(c - c2);
								}
							}
							ptr3++;
							ptr5++;
						}
						return num - num2;
					}
				}
			}
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0006384C File Offset: 0x00061A4C
		public bool EndsWith(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00063874 File Offset: 0x00061A74
		public bool EndsWith(string value, bool ignoreCase, CultureInfo culture)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			return culture.CompareInfo.IsSuffix(this, value, (!ignoreCase) ? CompareOptions.None : CompareOptions.IgnoreCase);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x000638B0 File Offset: 0x00061AB0
		public int IndexOfAny(char[] anyOf)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException();
			}
			if (this.length == 0)
			{
				return -1;
			}
			return this.IndexOfAnyUnchecked(anyOf, 0, this.length);
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x000638DC File Offset: 0x00061ADC
		public int IndexOfAny(char[] anyOf, int startIndex)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException();
			}
			if (startIndex < 0 || startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.IndexOfAnyUnchecked(anyOf, startIndex, this.length - startIndex);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00063914 File Offset: 0x00061B14
		public int IndexOfAny(char[] anyOf, int startIndex, int count)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException();
			}
			if (startIndex < 0 || startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (count < 0 || startIndex > this.length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be negative, and startIndex + count must be less than length of the string.");
			}
			return this.IndexOfAnyUnchecked(anyOf, startIndex, count);
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00063974 File Offset: 0x00061B74
		private unsafe int IndexOfAnyUnchecked(char[] anyOf, int startIndex, int count)
		{
			if (anyOf.Length == 0)
			{
				return -1;
			}
			if (anyOf.Length == 1)
			{
				return this.IndexOfUnchecked(anyOf[0], startIndex, count);
			}
			fixed (char* ptr = ref (anyOf != null && anyOf.Length != 0) ? ref anyOf[0] : ref *null)
			{
				int num = (int)(*ptr);
				int num2 = (int)(*ptr);
				char* ptr2 = ptr + anyOf.Length;
				char* ptr3 = ptr;
				while (++ptr3 != ptr2)
				{
					if ((int)(*ptr3) > num)
					{
						num = (int)(*ptr3);
					}
					else if ((int)(*ptr3) < num2)
					{
						num2 = (int)(*ptr3);
					}
				}
				fixed (char* ptr4 = &this.start_char)
				{
					char* ptr5 = ptr4 + startIndex;
					char* ptr6 = ptr5 + count;
					while (ptr5 != ptr6)
					{
						if ((int)(*ptr5) > num || (int)(*ptr5) < num2)
						{
							ptr5++;
						}
						else
						{
							if (*ptr5 == *ptr)
							{
								return (int)((long)(ptr5 - ptr4));
							}
							ptr3 = ptr;
							while (++ptr3 != ptr2)
							{
								if (*ptr5 == *ptr3)
								{
									return (int)((long)(ptr5 - ptr4));
								}
							}
							ptr5++;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00063A94 File Offset: 0x00061C94
		public int IndexOf(string value, StringComparison comparisonType)
		{
			return this.IndexOf(value, 0, this.Length, comparisonType);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00063AA8 File Offset: 0x00061CA8
		public int IndexOf(string value, int startIndex, StringComparison comparisonType)
		{
			return this.IndexOf(value, startIndex, this.Length - startIndex, comparisonType);
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00063ABC File Offset: 0x00061CBC
		public int IndexOf(string value, int startIndex, int count, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return this.IndexOfOrdinal(value, startIndex, count, CompareOptions.Ordinal);
			case StringComparison.OrdinalIgnoreCase:
				return this.IndexOfOrdinal(value, startIndex, count, CompareOptions.OrdinalIgnoreCase);
			default:
			{
				string text = Locale.GetText("Invalid value '{0}' for StringComparison", new object[]
				{
					comparisonType
				});
				throw new ArgumentException(text, "comparisonType");
			}
			}
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00063B88 File Offset: 0x00061D88
		internal int IndexOfOrdinal(string value, int startIndex, int count, CompareOptions options)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (count < 0 || this.length - startIndex < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (options == CompareOptions.Ordinal)
			{
				return this.IndexOfOrdinalUnchecked(value, startIndex, count);
			}
			return this.IndexOfOrdinalIgnoreCaseUnchecked(value, startIndex, count);
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x00063BF8 File Offset: 0x00061DF8
		internal unsafe int IndexOfOrdinalUnchecked(string value, int startIndex, int count)
		{
			int num = value.Length;
			if (count < num)
			{
				return -1;
			}
			if (num > 1)
			{
				fixed (string text = this)
				{
					fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text2 = value)
						{
							fixed (char* ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
							{
								char* ptr3 = ptr + startIndex;
								char* ptr4 = ptr3 + count - num + 1;
								while (ptr3 != ptr4)
								{
									if (*ptr3 == *ptr2)
									{
										for (int i = 1; i < num; i++)
										{
											if (ptr3[i] != ptr2[i])
											{
												goto IL_A1;
											}
										}
										return (int)((long)(ptr3 - ptr));
									}
									IL_A1:
									ptr3++;
								}
								text = null;
								text2 = null;
								return -1;
							}
						}
					}
				}
			}
			if (num == 1)
			{
				return this.IndexOfUnchecked(value[0], startIndex, count);
			}
			return startIndex;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00063CBC File Offset: 0x00061EBC
		internal unsafe int IndexOfOrdinalIgnoreCaseUnchecked(string value, int startIndex, int count)
		{
			int num = value.Length;
			if (count < num)
			{
				return -1;
			}
			if (num == 0)
			{
				return startIndex;
			}
			fixed (string text = this)
			{
				fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text2 = value)
					{
						fixed (char* ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
						{
							char* ptr3 = ptr + startIndex;
							char* ptr4 = ptr3 + count - num + 1;
							IL_8F:
							while (ptr3 != ptr4)
							{
								for (int i = 0; i < num; i++)
								{
									if (char.ToUpperInvariant(ptr3[i]) != char.ToUpperInvariant(ptr2[i]))
									{
										ptr3++;
										goto IL_8F;
									}
								}
								return (int)((long)(ptr3 - ptr));
							}
							text = null;
							text2 = null;
							return -1;
						}
					}
				}
			}
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00063D68 File Offset: 0x00061F68
		public int LastIndexOf(string value, StringComparison comparisonType)
		{
			if (this.Length == 0)
			{
				return (!(value == string.Empty)) ? -1 : 0;
			}
			return this.LastIndexOf(value, this.Length - 1, this.Length, comparisonType);
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00063DA4 File Offset: 0x00061FA4
		public int LastIndexOf(string value, int startIndex, StringComparison comparisonType)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1, comparisonType);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00063DB4 File Offset: 0x00061FB4
		public int LastIndexOf(string value, int startIndex, int count, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return this.LastIndexOfOrdinal(value, startIndex, count, CompareOptions.Ordinal);
			case StringComparison.OrdinalIgnoreCase:
				return this.LastIndexOfOrdinal(value, startIndex, count, CompareOptions.OrdinalIgnoreCase);
			default:
			{
				string text = Locale.GetText("Invalid value '{0}' for StringComparison", new object[]
				{
					comparisonType
				});
				throw new ArgumentException(text, "comparisonType");
			}
			}
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00063E80 File Offset: 0x00062080
		internal int LastIndexOfOrdinal(string value, int startIndex, int count, CompareOptions options)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0 || startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (count < 0 || startIndex < count - 1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (options == CompareOptions.Ordinal)
			{
				return this.LastIndexOfOrdinalUnchecked(value, startIndex, count);
			}
			return this.LastIndexOfOrdinalIgnoreCaseUnchecked(value, startIndex, count);
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00063EF8 File Offset: 0x000620F8
		internal unsafe int LastIndexOfOrdinalUnchecked(string value, int startIndex, int count)
		{
			int num = value.Length;
			if (count < num)
			{
				return -1;
			}
			if (num > 1)
			{
				fixed (string text = this)
				{
					fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text2 = value)
						{
							fixed (char* ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
							{
								char* ptr3 = ptr + startIndex - num + 1;
								char* ptr4 = ptr3 - count + num - 1;
								while (ptr3 != ptr4)
								{
									if (*ptr3 == *ptr2)
									{
										for (int i = 1; i < num; i++)
										{
											if (ptr3[i] != ptr2[i])
											{
												goto IL_A7;
											}
										}
										return (int)((long)(ptr3 - ptr));
									}
									IL_A7:
									ptr3--;
								}
								text = null;
								text2 = null;
								return -1;
							}
						}
					}
				}
			}
			if (num == 1)
			{
				return this.LastIndexOfUnchecked(value[0], startIndex, count);
			}
			return startIndex;
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00063FC0 File Offset: 0x000621C0
		internal unsafe int LastIndexOfOrdinalIgnoreCaseUnchecked(string value, int startIndex, int count)
		{
			int num = value.Length;
			if (count < num)
			{
				return -1;
			}
			if (num == 0)
			{
				return startIndex;
			}
			fixed (string text = this)
			{
				fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text2 = value)
					{
						fixed (char* ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
						{
							char* ptr3 = ptr + startIndex - num + 1;
							char* ptr4 = ptr3 - count + num - 1;
							IL_95:
							while (ptr3 != ptr4)
							{
								for (int i = 0; i < num; i++)
								{
									if (char.ToUpperInvariant(ptr3[i]) != char.ToUpperInvariant(ptr2[i]))
									{
										ptr3--;
										goto IL_95;
									}
								}
								return (int)((long)(ptr3 - ptr));
							}
							text = null;
							text2 = null;
							return -1;
						}
					}
				}
			}
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00064074 File Offset: 0x00062274
		public int IndexOf(char value)
		{
			if (this.length == 0)
			{
				return -1;
			}
			return this.IndexOfUnchecked(value, 0, this.length);
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00064094 File Offset: 0x00062294
		public int IndexOf(char value, int startIndex)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "< 0");
			}
			if (startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "startIndex > this.length");
			}
			if ((startIndex == 0 && this.length == 0) || startIndex == this.length)
			{
				return -1;
			}
			return this.IndexOfUnchecked(value, startIndex, this.length - startIndex);
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00064104 File Offset: 0x00062304
		public int IndexOf(char value, int startIndex, int count)
		{
			if (startIndex < 0 || startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative and must be< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (startIndex > this.length - count)
			{
				throw new ArgumentOutOfRangeException("count", "startIndex + count > this.length");
			}
			if ((startIndex == 0 && this.length == 0) || startIndex == this.length || count == 0)
			{
				return -1;
			}
			return this.IndexOfUnchecked(value, startIndex, count);
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00064198 File Offset: 0x00062398
		internal unsafe int IndexOfUnchecked(char value, int startIndex, int count)
		{
			char* ptr = ref this.start_char + startIndex * 2;
			char* ptr2 = ptr + (count >> 3 << 3);
			while (ptr != ptr2)
			{
				if (*ptr == value)
				{
					return (ptr - ref this.start_char / 2) / 2;
				}
				if (ptr[1] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 + 1L / 2L;
				}
				if (ptr[2] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 + 2L / 2L;
				}
				if (ptr[3] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 + 3L / 2L;
				}
				if (ptr[4] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 + 4L / 2L;
				}
				if (ptr[5] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 + 5L / 2L;
				}
				if (ptr[6] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 + 6L / 2L;
				}
				if (ptr[7] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 + 7L / 2L;
				}
				ptr += 8;
			}
			ptr2 += (count & 7);
			while (ptr != ptr2)
			{
				if (*ptr == value)
				{
					return (ptr - ref this.start_char / 2) / 2;
				}
				ptr++;
			}
			return -1;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x000642A0 File Offset: 0x000624A0
		internal unsafe int IndexOfOrdinalIgnoreCase(char value, int startIndex, int count)
		{
			if (this.length == 0)
			{
				return -1;
			}
			int num = startIndex + count;
			char c = char.ToUpperInvariant(value);
			fixed (char* ptr = &this.start_char)
			{
				for (int i = startIndex; i < num; i++)
				{
					if (char.ToUpperInvariant(ptr[i]) == c)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000642F8 File Offset: 0x000624F8
		public int IndexOf(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.length == 0)
			{
				return 0;
			}
			if (this.length == 0)
			{
				return -1;
			}
			return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, 0, this.length, CompareOptions.Ordinal);
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x00064350 File Offset: 0x00062550
		public int IndexOf(string value, int startIndex)
		{
			return this.IndexOf(value, startIndex, this.length - startIndex);
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00064364 File Offset: 0x00062564
		public int IndexOf(string value, int startIndex, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0 || startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative, and should not exceed length of string.");
			}
			if (count < 0 || startIndex > this.length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative, and should point to location in string.");
			}
			if (value.length == 0)
			{
				return startIndex;
			}
			if (startIndex == 0 && this.length == 0)
			{
				return -1;
			}
			if (count == 0)
			{
				return -1;
			}
			return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00064408 File Offset: 0x00062608
		public int LastIndexOfAny(char[] anyOf)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException();
			}
			return this.LastIndexOfAnyUnchecked(anyOf, this.length - 1, this.length);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0006442C File Offset: 0x0006262C
		public int LastIndexOfAny(char[] anyOf, int startIndex)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException();
			}
			if (startIndex < 0 || startIndex >= this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative, and should be less than length of string.");
			}
			if (this.length == 0)
			{
				return -1;
			}
			return this.LastIndexOfAnyUnchecked(anyOf, startIndex, startIndex + 1);
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x00064480 File Offset: 0x00062680
		public int LastIndexOfAny(char[] anyOf, int startIndex, int count)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException();
			}
			if (startIndex < 0 || startIndex >= this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "< 0 || > this.Length");
			}
			if (count < 0 || count > this.Length)
			{
				throw new ArgumentOutOfRangeException("count", "< 0 || > this.Length");
			}
			if (startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex - count + 1 < 0");
			}
			if (this.length == 0)
			{
				return -1;
			}
			return this.LastIndexOfAnyUnchecked(anyOf, startIndex, count);
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0006450C File Offset: 0x0006270C
		private unsafe int LastIndexOfAnyUnchecked(char[] anyOf, int startIndex, int count)
		{
			if (anyOf.Length == 1)
			{
				return this.LastIndexOfUnchecked(anyOf[0], startIndex, count);
			}
			fixed (char* ptr = this + RuntimeHelpers.OffsetToStringData / 2)
			{
				fixed (char* ptr2 = ref (anyOf != null && anyOf.Length != 0) ? ref anyOf[0] : ref *null)
				{
					char* ptr3 = ptr + startIndex;
					char* ptr4 = ptr3 - count;
					char* ptr5 = ptr2 + anyOf.Length;
					while (ptr3 != ptr4)
					{
						for (char* ptr6 = ptr2; ptr6 != ptr5; ptr6++)
						{
							if (*ptr6 == *ptr3)
							{
								return (int)((long)(ptr3 - ptr));
							}
						}
						ptr3--;
					}
					return -1;
				}
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x000645AC File Offset: 0x000627AC
		public int LastIndexOf(char value)
		{
			if (this.length == 0)
			{
				return -1;
			}
			return this.LastIndexOfUnchecked(value, this.length - 1, this.length);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x000645D0 File Offset: 0x000627D0
		public int LastIndexOf(char value, int startIndex)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x000645E0 File Offset: 0x000627E0
		public int LastIndexOf(char value, int startIndex, int count)
		{
			if (startIndex == 0 && this.length == 0)
			{
				return -1;
			}
			if (startIndex < 0 || startIndex >= this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "< 0 || >= this.Length");
			}
			if (count < 0 || count > this.Length)
			{
				throw new ArgumentOutOfRangeException("count", "< 0 || > this.Length");
			}
			if (startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex - count + 1 < 0");
			}
			return this.LastIndexOfUnchecked(value, startIndex, count);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00064668 File Offset: 0x00062868
		internal unsafe int LastIndexOfUnchecked(char value, int startIndex, int count)
		{
			char* ptr = ref this.start_char + startIndex * 2;
			char* ptr2 = ptr - (count >> 3 << 3);
			while (ptr != ptr2)
			{
				if (*ptr == value)
				{
					return (ptr - ref this.start_char / 2) / 2;
				}
				if (ptr[-1] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 - 1;
				}
				if (ptr[-2] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 - 2;
				}
				if (ptr[-3] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 - 3;
				}
				if (ptr[-4] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 - 4;
				}
				if (ptr[-5] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 - 5;
				}
				if (ptr[-6] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 - 6;
				}
				if (ptr[-7] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 - 7;
				}
				ptr -= 8;
			}
			ptr2 -= (count & 7);
			while (ptr != ptr2)
			{
				if (*ptr == value)
				{
					return (ptr - ref this.start_char / 2) / 2;
				}
				ptr--;
			}
			return -1;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0006476C File Offset: 0x0006296C
		internal unsafe int LastIndexOfOrdinalIgnoreCase(char value, int startIndex, int count)
		{
			if (this.length == 0)
			{
				return -1;
			}
			int num = startIndex - count;
			char c = char.ToUpperInvariant(value);
			fixed (char* ptr = &this.start_char)
			{
				for (int i = startIndex; i > num; i--)
				{
					if (char.ToUpperInvariant(ptr[i]) == c)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x000647C4 File Offset: 0x000629C4
		public int LastIndexOf(string value)
		{
			if (this.length == 0)
			{
				return this.LastIndexOf(value, 0, 0);
			}
			return this.LastIndexOf(value, this.length - 1, this.length);
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x000647F0 File Offset: 0x000629F0
		public int LastIndexOf(string value, int startIndex)
		{
			int num = startIndex;
			if (num < this.Length)
			{
				num++;
			}
			return this.LastIndexOf(value, startIndex, num);
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x00064818 File Offset: 0x00062A18
		public int LastIndexOf(string value, int startIndex, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < -1 || startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "< 0 || > this.Length");
			}
			if (count < 0 || count > this.Length)
			{
				throw new ArgumentOutOfRangeException("count", "< 0 || > this.Length");
			}
			if (startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex - count + 1 < 0");
			}
			if (value.Length == 0)
			{
				return startIndex;
			}
			if (startIndex == 0 && this.length == 0)
			{
				return -1;
			}
			if (this.length == 0 && value.length > 0)
			{
				return -1;
			}
			if (count == 0)
			{
				return -1;
			}
			if (startIndex == this.Length)
			{
				startIndex--;
			}
			return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count);
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x000648F8 File Offset: 0x00062AF8
		public bool Contains(string value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x00064908 File Offset: 0x00062B08
		public static bool IsNullOrEmpty(string value)
		{
			return value == null || value.Length == 0;
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0006491C File Offset: 0x00062B1C
		public string Normalize()
		{
			return Normalization.Normalize(this, 0);
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00064928 File Offset: 0x00062B28
		public string Normalize(NormalizationForm normalizationForm)
		{
			switch (normalizationForm)
			{
			case NormalizationForm.FormD:
				return Normalization.Normalize(this, 1);
			default:
				return Normalization.Normalize(this, 0);
			case NormalizationForm.FormKC:
				return Normalization.Normalize(this, 2);
			case NormalizationForm.FormKD:
				return Normalization.Normalize(this, 3);
			}
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x00064978 File Offset: 0x00062B78
		public bool IsNormalized()
		{
			return Normalization.IsNormalized(this, 0);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00064984 File Offset: 0x00062B84
		public bool IsNormalized(NormalizationForm normalizationForm)
		{
			switch (normalizationForm)
			{
			case NormalizationForm.FormD:
				return Normalization.IsNormalized(this, 1);
			default:
				return Normalization.IsNormalized(this, 0);
			case NormalizationForm.FormKC:
				return Normalization.IsNormalized(this, 2);
			case NormalizationForm.FormKD:
				return Normalization.IsNormalized(this, 3);
			}
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x000649D4 File Offset: 0x00062BD4
		public string Remove(int startIndex)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex can not be less than zero");
			}
			if (startIndex >= this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex must be less than the length of the string");
			}
			return this.Remove(startIndex, this.length - startIndex);
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x00064A24 File Offset: 0x00062C24
		public string PadLeft(int totalWidth)
		{
			return this.PadLeft(totalWidth, ' ');
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00064A30 File Offset: 0x00062C30
		public unsafe string PadLeft(int totalWidth, char paddingChar)
		{
			if (totalWidth < 0)
			{
				throw new ArgumentOutOfRangeException("totalWidth", "< 0");
			}
			if (totalWidth < this.length)
			{
				return this;
			}
			string text = string.InternalAllocateStr(totalWidth);
			fixed (string text2 = text)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text3 = this)
					{
						fixed (char* src = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							char* ptr2 = ptr;
							char* ptr3 = ptr + (totalWidth - this.length);
							while (ptr2 != ptr3)
							{
								*(ptr2++) = paddingChar;
							}
							string.CharCopy(ptr3, src, this.length);
							text2 = null;
							text3 = null;
							return text;
						}
					}
				}
			}
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00064AC0 File Offset: 0x00062CC0
		public string PadRight(int totalWidth)
		{
			return this.PadRight(totalWidth, ' ');
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x00064ACC File Offset: 0x00062CCC
		public unsafe string PadRight(int totalWidth, char paddingChar)
		{
			if (totalWidth < 0)
			{
				throw new ArgumentOutOfRangeException("totalWidth", "< 0");
			}
			if (totalWidth < this.length)
			{
				return this;
			}
			if (totalWidth == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(totalWidth);
			fixed (string text2 = text)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text3 = this)
					{
						fixed (char* src = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(ptr, src, this.length);
							char* ptr2 = ptr + this.length;
							char* ptr3 = ptr + totalWidth;
							while (ptr2 != ptr3)
							{
								*(ptr2++) = paddingChar;
							}
							text2 = null;
							text3 = null;
							return text;
						}
					}
				}
			}
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00064B68 File Offset: 0x00062D68
		public bool StartsWith(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x00064B90 File Offset: 0x00062D90
		[ComVisible(false)]
		public bool StartsWith(string value, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.Ordinal);
			case StringComparison.OrdinalIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.OrdinalIgnoreCase);
			default:
			{
				string text = Locale.GetText("Invalid value '{0}' for StringComparison", new object[]
				{
					comparisonType
				});
				throw new ArgumentException(text, "comparisonType");
			}
			}
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x00064C74 File Offset: 0x00062E74
		[ComVisible(false)]
		public bool EndsWith(string value, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.Ordinal);
			case StringComparison.OrdinalIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.OrdinalIgnoreCase);
			default:
			{
				string text = Locale.GetText("Invalid value '{0}' for StringComparison", new object[]
				{
					comparisonType
				});
				throw new ArgumentException(text, "comparisonType");
			}
			}
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x00064D58 File Offset: 0x00062F58
		public bool StartsWith(string value, bool ignoreCase, CultureInfo culture)
		{
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			return culture.CompareInfo.IsPrefix(this, value, (!ignoreCase) ? CompareOptions.None : CompareOptions.IgnoreCase);
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x00064D84 File Offset: 0x00062F84
		public unsafe string Replace(char oldChar, char newChar)
		{
			if (this.length == 0 || oldChar == newChar)
			{
				return this;
			}
			int num = this.IndexOfUnchecked(oldChar, 0, this.length);
			if (num == -1)
			{
				return this;
			}
			if (num < 4)
			{
				num = 0;
			}
			string text = string.InternalAllocateStr(this.length);
			fixed (string text2 = text)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (char* ptr2 = &this.start_char)
					{
						if (num != 0)
						{
							string.CharCopy(ptr, ptr2, num);
						}
						char* ptr3 = ptr + this.length;
						char* ptr4 = ptr + num;
						char* ptr5 = ptr2 + num;
						while (ptr4 != ptr3)
						{
							if (*ptr5 == oldChar)
							{
								*ptr4 = newChar;
							}
							else
							{
								*ptr4 = *ptr5;
							}
							ptr5++;
							ptr4++;
						}
						text2 = null;
					}
					return text;
				}
			}
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x00064E4C File Offset: 0x0006304C
		public string Replace(string oldValue, string newValue)
		{
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException("oldValue is the empty string.");
			}
			if (this.Length == 0)
			{
				return this;
			}
			if (newValue == null)
			{
				newValue = string.Empty;
			}
			return this.ReplaceUnchecked(oldValue, newValue);
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00064EA4 File Offset: 0x000630A4
		private unsafe string ReplaceUnchecked(string oldValue, string newValue)
		{
			if (oldValue.length > this.length)
			{
				return this;
			}
			if (oldValue.length == 1 && newValue.length == 1)
			{
				return this.Replace(oldValue[0], newValue[0]);
			}
			int* ptr = stackalloc int[checked(200 * 4)];
			fixed (char* ptr2 = this + RuntimeHelpers.OffsetToStringData / 2)
			{
				fixed (char* src = newValue + RuntimeHelpers.OffsetToStringData / 2)
				{
					int i = 0;
					int num = 0;
					while (i < this.length)
					{
						int num2 = this.IndexOfOrdinalUnchecked(oldValue, i, this.length - i);
						if (num2 < 0)
						{
							break;
						}
						if (num >= 200)
						{
							return this.ReplaceFallback(oldValue, newValue, 200);
						}
						ptr[num++ * 4] = num2;
						i = num2 + oldValue.length;
					}
					if (num == 0)
					{
						return this;
					}
					int num3 = this.length + (newValue.length - oldValue.length) * num;
					string text = string.InternalAllocateStr(num3);
					int num4 = 0;
					int num5 = 0;
					fixed (string text2 = text)
					{
						fixed (char* ptr3 = text2 + RuntimeHelpers.OffsetToStringData / 2)
						{
							for (int j = 0; j < num; j++)
							{
								int num6 = ptr[j] - num5;
								string.CharCopy(ptr3 + num4, ptr2 + num5, num6);
								num4 += num6;
								num5 = ptr[j] + oldValue.length;
								string.CharCopy(ptr3 + num4, src, newValue.length);
								num4 += newValue.length;
							}
							string.CharCopy(ptr3 + num4, ptr2 + num5, this.length - num5);
							text2 = null;
							return text;
						}
					}
				}
			}
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x00065054 File Offset: 0x00063254
		private string ReplaceFallback(string oldValue, string newValue, int testedCount)
		{
			int capacity = this.length + (newValue.length - oldValue.length) * testedCount;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			int num;
			for (int i = 0; i < this.length; i = num + oldValue.Length)
			{
				num = this.IndexOfOrdinalUnchecked(oldValue, i, this.length - i);
				if (num < 0)
				{
					stringBuilder.Append(this.SubstringUnchecked(i, this.length - i));
					break;
				}
				stringBuilder.Append(this.SubstringUnchecked(i, num - i));
				stringBuilder.Append(newValue);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x000650F0 File Offset: 0x000632F0
		public unsafe string Remove(int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative.");
			}
			if (startIndex > this.length - count)
			{
				throw new ArgumentOutOfRangeException("count", "startIndex + count > this.length");
			}
			string text = string.InternalAllocateStr(this.length - count);
			fixed (string text2 = text)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text3 = this)
					{
						fixed (char* ptr2 = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							char* ptr3 = ptr;
							string.CharCopy(ptr3, ptr2, startIndex);
							int num = startIndex + count;
							ptr3 += startIndex;
							string.CharCopy(ptr3, ptr2 + num, this.length - num);
							text2 = null;
							text3 = null;
							return text;
						}
					}
				}
			}
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x000651A4 File Offset: 0x000633A4
		public string ToLower()
		{
			return this.ToLower(CultureInfo.CurrentCulture);
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000651B4 File Offset: 0x000633B4
		public string ToLower(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (culture.LCID == 127)
			{
				return this.ToLowerInvariant();
			}
			return culture.TextInfo.ToLower(this);
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000651E8 File Offset: 0x000633E8
		public unsafe string ToLowerInvariant()
		{
			if (this.length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(this.length);
			fixed (char* ptr = &this.start_char)
			{
				fixed (string text2 = text)
				{
					fixed (char* ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						char* ptr3 = ptr2;
						char* ptr4 = ptr;
						for (int i = 0; i < this.length; i++)
						{
							*ptr3 = char.ToLowerInvariant(*ptr4);
							ptr4++;
							ptr3++;
						}
						ptr = null;
						text2 = null;
						return text;
					}
				}
			}
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00065264 File Offset: 0x00063464
		public string ToUpper()
		{
			return this.ToUpper(CultureInfo.CurrentCulture);
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00065274 File Offset: 0x00063474
		public string ToUpper(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (culture.LCID == 127)
			{
				return this.ToUpperInvariant();
			}
			return culture.TextInfo.ToUpper(this);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x000652A8 File Offset: 0x000634A8
		public unsafe string ToUpperInvariant()
		{
			if (this.length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(this.length);
			fixed (char* ptr = &this.start_char)
			{
				fixed (string text2 = text)
				{
					fixed (char* ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						char* ptr3 = ptr2;
						char* ptr4 = ptr;
						for (int i = 0; i < this.length; i++)
						{
							*ptr3 = char.ToUpperInvariant(*ptr4);
							ptr4++;
							ptr3++;
						}
						ptr = null;
						text2 = null;
						return text;
					}
				}
			}
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x00065324 File Offset: 0x00063524
		public override string ToString()
		{
			return this;
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00065328 File Offset: 0x00063528
		public string ToString(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0006532C File Offset: 0x0006352C
		public static string Format(string format, object arg0)
		{
			return string.Format(null, format, new object[]
			{
				arg0
			});
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00065340 File Offset: 0x00063540
		public static string Format(string format, object arg0, object arg1)
		{
			return string.Format(null, format, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00065358 File Offset: 0x00063558
		public static string Format(string format, object arg0, object arg1, object arg2)
		{
			return string.Format(null, format, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00065374 File Offset: 0x00063574
		public static string Format(string format, params object[] args)
		{
			return string.Format(null, format, args);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x00065380 File Offset: 0x00063580
		public static string Format(IFormatProvider provider, string format, params object[] args)
		{
			StringBuilder stringBuilder = string.FormatHelper(null, provider, format, args);
			return stringBuilder.ToString();
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000653A0 File Offset: 0x000635A0
		internal static StringBuilder FormatHelper(StringBuilder result, IFormatProvider provider, string format, params object[] args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (result == null)
			{
				int num = 0;
				int i;
				for (i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text == null)
					{
						break;
					}
					num += text.length;
				}
				if (i == args.Length)
				{
					result = new StringBuilder(num + format.length);
				}
				else
				{
					result = new StringBuilder();
				}
			}
			int j = 0;
			int num2 = j;
			while (j < format.length)
			{
				char c = format[j++];
				if (c == '{')
				{
					result.Append(format, num2, j - num2 - 1);
					if (format[j] == '{')
					{
						num2 = j++;
					}
					else
					{
						int num3;
						int num4;
						bool flag;
						string format2;
						string.ParseFormatSpecifier(format, ref j, out num3, out num4, out flag, out format2);
						if (num3 >= args.Length)
						{
							throw new FormatException("Index (zero based) must be greater than or equal to zero and less than the size of the argument list.");
						}
						object obj = args[num3];
						ICustomFormatter customFormatter = null;
						if (provider != null)
						{
							customFormatter = (provider.GetFormat(typeof(ICustomFormatter)) as ICustomFormatter);
						}
						string text2;
						if (obj == null)
						{
							text2 = string.Empty;
						}
						else if (customFormatter != null)
						{
							text2 = customFormatter.Format(format2, obj, provider);
						}
						else if (obj is IFormattable)
						{
							text2 = ((IFormattable)obj).ToString(format2, provider);
						}
						else
						{
							text2 = obj.ToString();
						}
						if (num4 > text2.length)
						{
							int repeatCount = num4 - text2.length;
							if (flag)
							{
								result.Append(text2);
								result.Append(' ', repeatCount);
							}
							else
							{
								result.Append(' ', repeatCount);
								result.Append(text2);
							}
						}
						else
						{
							result.Append(text2);
						}
						num2 = j;
					}
				}
				else if (c == '}' && j < format.length && format[j] == '}')
				{
					result.Append(format, num2, j - num2 - 1);
					num2 = j++;
				}
				else if (c == '}')
				{
					throw new FormatException("Input string was not in a correct format.");
				}
			}
			if (num2 < format.length)
			{
				result.Append(format, num2, format.Length - num2);
			}
			return result;
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00065604 File Offset: 0x00063804
		public unsafe static string Copy(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int num = str.length;
			string text = string.InternalAllocateStr(num);
			if (num != 0)
			{
				fixed (string text2 = text)
				{
					fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text3 = str, src = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(dest, src, num);
							text2 = null;
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x00065660 File Offset: 0x00063860
		public static string Concat(object arg0)
		{
			if (arg0 == null)
			{
				return string.Empty;
			}
			return arg0.ToString();
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x00065674 File Offset: 0x00063874
		public static string Concat(object arg0, object arg1)
		{
			return ((arg0 == null) ? null : arg0.ToString()) + ((arg1 == null) ? null : arg1.ToString());
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000656A0 File Offset: 0x000638A0
		public static string Concat(object arg0, object arg1, object arg2)
		{
			string str;
			if (arg0 == null)
			{
				str = string.Empty;
			}
			else
			{
				str = arg0.ToString();
			}
			string str2;
			if (arg1 == null)
			{
				str2 = string.Empty;
			}
			else
			{
				str2 = arg1.ToString();
			}
			string str3;
			if (arg2 == null)
			{
				str3 = string.Empty;
			}
			else
			{
				str3 = arg2.ToString();
			}
			return str + str2 + str3;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00065700 File Offset: 0x00063900
		[CLSCompliant(false)]
		public static string Concat(object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			string str;
			if (arg0 == null)
			{
				str = string.Empty;
			}
			else
			{
				str = arg0.ToString();
			}
			string str2;
			if (arg1 == null)
			{
				str2 = string.Empty;
			}
			else
			{
				str2 = arg1.ToString();
			}
			string str3;
			if (arg2 == null)
			{
				str3 = string.Empty;
			}
			else
			{
				str3 = arg2.ToString();
			}
			ArgIterator argIterator = new ArgIterator(__arglist);
			int remainingCount = argIterator.GetRemainingCount();
			StringBuilder stringBuilder = new StringBuilder();
			if (arg3 != null)
			{
				stringBuilder.Append(arg3.ToString());
			}
			for (int i = 0; i < remainingCount; i++)
			{
				TypedReference nextArg = argIterator.GetNextArg();
				stringBuilder.Append(TypedReference.ToObject(nextArg));
			}
			string str4 = stringBuilder.ToString();
			return str + str2 + str3 + str4;
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x000657C4 File Offset: 0x000639C4
		public unsafe static string Concat(string str0, string str1)
		{
			if (str0 == null || str0.Length == 0)
			{
				if (str1 == null || str1.Length == 0)
				{
					return string.Empty;
				}
				return str1;
			}
			else
			{
				if (str1 == null || str1.Length == 0)
				{
					return str0;
				}
				string text = string.InternalAllocateStr(str0.length + str1.length);
				fixed (string text2 = text)
				{
					fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text3 = str0)
						{
							fixed (char* src = text3 + RuntimeHelpers.OffsetToStringData / 2)
							{
								string.CharCopy(dest, src, str0.length);
								text2 = null;
								text3 = null;
								fixed (string text4 = text)
								{
									fixed (char* ptr = text4 + RuntimeHelpers.OffsetToStringData / 2)
									{
										fixed (string text5 = str1)
										{
											fixed (char* src2 = text5 + RuntimeHelpers.OffsetToStringData / 2)
											{
												string.CharCopy(ptr + str0.Length, src2, str1.length);
												text4 = null;
												text5 = null;
												return text;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00065888 File Offset: 0x00063A88
		public unsafe static string Concat(string str0, string str1, string str2)
		{
			if (str0 == null || str0.Length == 0)
			{
				if (str1 == null || str1.Length == 0)
				{
					if (str2 == null || str2.Length == 0)
					{
						return string.Empty;
					}
					return str2;
				}
				else
				{
					if (str2 == null || str2.Length == 0)
					{
						return str1;
					}
					str0 = string.Empty;
				}
			}
			else if (str1 == null || str1.Length == 0)
			{
				if (str2 == null || str2.Length == 0)
				{
					return str0;
				}
				str1 = string.Empty;
			}
			else if (str2 == null || str2.Length == 0)
			{
				str2 = string.Empty;
			}
			string text = string.InternalAllocateStr(str0.length + str1.length + str2.length);
			if (str0.Length != 0)
			{
				fixed (string text2 = text)
				{
					fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text3 = str0, src = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(dest, src, str0.length);
							text2 = null;
						}
					}
				}
			}
			if (str1.Length != 0)
			{
				fixed (string text4 = text)
				{
					fixed (char* ptr = text4 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text5 = str1, src2 = text5 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(ptr + str0.Length, src2, str1.length);
							text4 = null;
						}
					}
				}
			}
			if (str2.Length != 0)
			{
				fixed (string text6 = text)
				{
					fixed (char* ptr2 = text6 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text7 = str2, src3 = text7 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(ptr2 + str0.Length + str1.Length, src3, str2.length);
							text6 = null;
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x00065A1C File Offset: 0x00063C1C
		public unsafe static string Concat(string str0, string str1, string str2, string str3)
		{
			if (str0 == null && str1 == null && str2 == null && str3 == null)
			{
				return string.Empty;
			}
			if (str0 == null)
			{
				str0 = string.Empty;
			}
			if (str1 == null)
			{
				str1 = string.Empty;
			}
			if (str2 == null)
			{
				str2 = string.Empty;
			}
			if (str3 == null)
			{
				str3 = string.Empty;
			}
			string text = string.InternalAllocateStr(str0.length + str1.length + str2.length + str3.length);
			if (str0.Length != 0)
			{
				fixed (string text2 = text)
				{
					fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text3 = str0, src = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(dest, src, str0.length);
							text2 = null;
						}
					}
				}
			}
			if (str1.Length != 0)
			{
				fixed (string text4 = text)
				{
					fixed (char* ptr = text4 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text5 = str1, src2 = text5 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(ptr + str0.Length, src2, str1.length);
							text4 = null;
						}
					}
				}
			}
			if (str2.Length != 0)
			{
				fixed (string text6 = text)
				{
					fixed (char* ptr2 = text6 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text7 = str2, src3 = text7 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(ptr2 + str0.Length + str1.Length, src3, str2.length);
							text6 = null;
						}
					}
				}
			}
			if (str3.Length != 0)
			{
				fixed (string text8 = text)
				{
					fixed (char* ptr3 = text8 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text9 = str3, src4 = text9 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(ptr3 + str0.Length + str1.Length + str2.Length, src4, str3.length);
							text8 = null;
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00065BC0 File Offset: 0x00063DC0
		public static string Concat(params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			int num = args.Length;
			if (num == 0)
			{
				return string.Empty;
			}
			string[] array = new string[num];
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				if (args[i] != null)
				{
					array[i] = args[i].ToString();
					num2 += array[i].length;
				}
			}
			return string.ConcatInternal(array, num2);
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00065C30 File Offset: 0x00063E30
		public static string Concat(params string[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			int num = 0;
			foreach (string text in values)
			{
				if (text != null)
				{
					num += text.length;
				}
			}
			return string.ConcatInternal(values, num);
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x00065C80 File Offset: 0x00063E80
		private unsafe static string ConcatInternal(string[] values, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(length);
			fixed (string text2 = text)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					int num = 0;
					foreach (string text3 in values)
					{
						if (text3 != null)
						{
							fixed (string text4 = text3)
							{
								fixed (char* src = text4 + RuntimeHelpers.OffsetToStringData / 2)
								{
									string.CharCopy(ptr + num, src, text3.length);
									text4 = null;
									num += text3.Length;
								}
							}
						}
					}
					text2 = null;
					return text;
				}
			}
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x00065D04 File Offset: 0x00063F04
		public unsafe string Insert(int startIndex, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0 || startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative and must be less than or equal to length of string.");
			}
			if (value.Length == 0)
			{
				return this;
			}
			if (this.Length == 0)
			{
				return value;
			}
			string text = string.InternalAllocateStr(this.length + value.length);
			fixed (string text2 = text)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text3 = this)
					{
						fixed (char* ptr2 = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							fixed (string text4 = value)
							{
								fixed (char* src = text4 + RuntimeHelpers.OffsetToStringData / 2)
								{
									char* ptr3 = ptr;
									string.CharCopy(ptr3, ptr2, startIndex);
									ptr3 += startIndex;
									string.CharCopy(ptr3, src, value.length);
									ptr3 += value.length;
									string.CharCopy(ptr3, ptr2 + startIndex, this.length - startIndex);
									text2 = null;
									text3 = null;
									text4 = null;
									return text;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x00065DE8 File Offset: 0x00063FE8
		public static string Intern(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return string.InternalIntern(str);
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x00065E04 File Offset: 0x00064004
		public static string IsInterned(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return string.InternalIsInterned(str);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x00065E20 File Offset: 0x00064020
		public static string Join(string separator, string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			return string.JoinUnchecked(separator, value, 0, value.Length);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x00065E4C File Offset: 0x0006404C
		public static string Join(string separator, string[] value, int startIndex, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (startIndex > value.Length - count)
			{
				throw new ArgumentOutOfRangeException("startIndex", "startIndex + count > value.length");
			}
			if (startIndex == value.Length)
			{
				return string.Empty;
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			return string.JoinUnchecked(separator, value, startIndex, count);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x00065ED8 File Offset: 0x000640D8
		private unsafe static string JoinUnchecked(string separator, string[] value, int startIndex, int count)
		{
			int num = 0;
			int num2 = startIndex + count;
			for (int i = startIndex; i < num2; i++)
			{
				string text = value[i];
				if (text != null)
				{
					num += text.length;
				}
			}
			num += separator.length * (count - 1);
			if (num <= 0)
			{
				return string.Empty;
			}
			string text2 = string.InternalAllocateStr(num);
			num2--;
			fixed (string text3 = text2)
			{
				fixed (char* ptr = text3 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text4 = separator)
					{
						fixed (char* src = text4 + RuntimeHelpers.OffsetToStringData / 2)
						{
							int num3 = 0;
							for (int j = startIndex; j < num2; j++)
							{
								string text5 = value[j];
								if (text5 != null && text5.Length > 0)
								{
									fixed (string text6 = text5)
									{
										fixed (char* src2 = text6 + RuntimeHelpers.OffsetToStringData / 2)
										{
											string.CharCopy(ptr + num3, src2, text5.Length);
											text6 = null;
											num3 += text5.Length;
										}
									}
								}
								if (separator.Length > 0)
								{
									string.CharCopy(ptr + num3, src, separator.Length);
									num3 += separator.Length;
								}
							}
							string text7 = value[num2];
							if (text7 != null && text7.Length > 0)
							{
								fixed (string text8 = text7, src3 = text8 + RuntimeHelpers.OffsetToStringData / 2)
								{
									string.CharCopy(ptr + num3, src3, text7.Length);
								}
							}
							text3 = null;
							text4 = null;
							return text2;
						}
					}
				}
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x0006602C File Offset: 0x0006422C
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x00066034 File Offset: 0x00064234
		public CharEnumerator GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0006603C File Offset: 0x0006423C
		private static void ParseFormatSpecifier(string str, ref int ptr, out int n, out int width, out bool left_align, out string format)
		{
			try
			{
				n = string.ParseDecimal(str, ref ptr);
				if (n < 0)
				{
					throw new FormatException("Input string was not in a correct format.");
				}
				if (str[ptr] == ',')
				{
					ptr++;
					while (char.IsWhiteSpace(str[ptr]))
					{
						ptr++;
					}
					int num = ptr;
					format = str.Substring(num, ptr - num);
					left_align = (str[ptr] == '-');
					if (left_align)
					{
						ptr++;
					}
					width = string.ParseDecimal(str, ref ptr);
					if (width < 0)
					{
						throw new FormatException("Input string was not in a correct format.");
					}
				}
				else
				{
					width = 0;
					left_align = false;
					format = string.Empty;
				}
				if (str[ptr] == ':')
				{
					int num2 = ++ptr;
					while (str[ptr] != '}')
					{
						ptr++;
					}
					format += str.Substring(num2, ptr - num2);
				}
				else
				{
					format = null;
				}
				if (str[ptr++] != '}')
				{
					throw new FormatException("Input string was not in a correct format.");
				}
			}
			catch (IndexOutOfRangeException)
			{
				throw new FormatException("Input string was not in a correct format.");
			}
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0006619C File Offset: 0x0006439C
		private static int ParseDecimal(string str, ref int ptr)
		{
			int num = ptr;
			int num2 = 0;
			for (;;)
			{
				char c = str[num];
				if (c < '0' || '9' < c)
				{
					break;
				}
				num2 = num2 * 10 + (int)c - 48;
				num++;
			}
			if (num == ptr)
			{
				return -1;
			}
			ptr = num;
			return num2;
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x000661EC File Offset: 0x000643EC
		internal unsafe void InternalSetChar(int idx, char val)
		{
			if (idx >= this.Length)
			{
				throw new ArgumentOutOfRangeException("idx");
			}
			fixed (char* ptr = &this.start_char)
			{
				ptr[idx] = val;
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00066224 File Offset: 0x00064424
		internal unsafe void InternalSetLength(int newLength)
		{
			if (newLength > this.length)
			{
				throw new ArgumentOutOfRangeException("newLength", "newLength as to be <= length");
			}
			fixed (char* ptr = &this.start_char)
			{
				char* ptr2 = ptr + newLength;
				char* ptr3 = ptr + this.length;
				while (ptr2 < ptr3)
				{
					*ptr2 = '\0';
					ptr2++;
				}
			}
			this.length = newLength;
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x00066284 File Offset: 0x00064484
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe override int GetHashCode()
		{
			fixed (char* ptr = this + RuntimeHelpers.OffsetToStringData / 2)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + this.length - 1;
				int num = 0;
				while (ptr2 < ptr3)
				{
					num = (num << 5) - num + (int)(*ptr2);
					num = (num << 5) - num + (int)ptr2[1];
					ptr2 += 2;
				}
				ptr3++;
				if (ptr2 < ptr3)
				{
					num = (num << 5) - num + (int)(*ptr2);
				}
				return num;
			}
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x000662EC File Offset: 0x000644EC
		internal unsafe int GetCaseInsensitiveHashCode()
		{
			fixed (char* ptr = this + RuntimeHelpers.OffsetToStringData / 2)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + this.length - 1;
				int num = 0;
				while (ptr2 < ptr3)
				{
					num = (num << 5) - num + (int)char.ToUpperInvariant(*ptr2);
					num = (num << 5) - num + (int)char.ToUpperInvariant(ptr2[1]);
					ptr2 += 2;
				}
				ptr3++;
				if (ptr2 < ptr3)
				{
					num = (num << 5) - num + (int)char.ToUpperInvariant(*ptr2);
				}
				return num;
			}
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00066360 File Offset: 0x00064560
		private unsafe string CreateString(sbyte* value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			byte* ptr = (byte*)value;
			int num = 0;
			try
			{
				while (*(ptr++) != 0)
				{
					num++;
				}
			}
			catch (NullReferenceException)
			{
				throw new ArgumentOutOfRangeException("ptr", "Value does not refer to a valid string.");
			}
			catch (AccessViolationException)
			{
				throw new ArgumentOutOfRangeException("ptr", "Value does not refer to a valid string.");
			}
			return this.CreateString(value, 0, num, null);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x000663EC File Offset: 0x000645EC
		private unsafe string CreateString(sbyte* value, int startIndex, int length)
		{
			return this.CreateString(value, startIndex, length, null);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x000663F8 File Offset: 0x000645F8
		private unsafe string CreateString(sbyte* value, int startIndex, int length, Encoding enc)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Non-negative number required.");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Non-negative number required.");
			}
			if (value + startIndex < value)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Value, startIndex and length do not refer to a valid string.");
			}
			bool flag;
			if (flag = (enc == null))
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (length == 0)
				{
					return string.Empty;
				}
				enc = Encoding.Default;
			}
			byte[] array = new byte[length];
			if (length != 0)
			{
				fixed (byte* dest = ref (array != null && array.Length != 0) ? ref array[0] : ref *null)
				{
					try
					{
						string.memcpy(dest, (byte*)(value + startIndex), length);
					}
					catch (NullReferenceException)
					{
						throw new ArgumentOutOfRangeException("ptr", "Value, startIndex and length do not refer to a valid string.");
					}
					catch (AccessViolationException)
					{
						if (!flag)
						{
							throw;
						}
						throw new ArgumentOutOfRangeException("value", "Value, startIndex and length do not refer to a valid string.");
					}
				}
			}
			return enc.GetString(array);
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x00066510 File Offset: 0x00064710
		private unsafe string CreateString(char* value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			char* ptr = value;
			int num = 0;
			while (*ptr != '\0')
			{
				num++;
				ptr++;
			}
			string text = string.InternalAllocateStr(num);
			if (num != 0)
			{
				fixed (string text2 = text, dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					string.CharCopy(dest, value, num);
				}
			}
			return text;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x00066568 File Offset: 0x00064768
		private unsafe string CreateString(char* value, int startIndex, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			string text = string.InternalAllocateStr(length);
			fixed (string text2 = text)
			{
				fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					string.CharCopy(dest, value + startIndex, length);
					text2 = null;
					return text;
				}
			}
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x000665D8 File Offset: 0x000647D8
		private unsafe string CreateString(char[] val, int startIndex, int length)
		{
			if (val == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative.");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Cannot be negative.");
			}
			if (startIndex > val.Length - length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative, and should be less than length of string.");
			}
			if (length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(length);
			fixed (string text2 = text)
			{
				fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (char* ptr = ref (val != null && val.Length != 0) ? ref val[0] : ref *null)
					{
						string.CharCopy(dest, ptr + startIndex, length);
						text2 = null;
					}
					return text;
				}
			}
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x0006668C File Offset: 0x0006488C
		private unsafe string CreateString(char[] val)
		{
			if (val == null)
			{
				return string.Empty;
			}
			if (val.Length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(val.Length);
			fixed (string text2 = text)
			{
				fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (char* src = ref (val != null && val.Length != 0) ? ref val[0] : ref *null)
					{
						string.CharCopy(dest, src, val.Length);
						text2 = null;
					}
					return text;
				}
			}
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x000666F4 File Offset: 0x000648F4
		private unsafe string CreateString(char c, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(count);
			fixed (string text2 = text)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					char* ptr2 = ptr;
					char* ptr3 = ptr2 + count;
					while (ptr2 < ptr3)
					{
						*ptr2 = c;
						ptr2++;
					}
					text2 = null;
					return text;
				}
			}
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x00066754 File Offset: 0x00064954
		internal unsafe static void memset(byte* dest, int val, int len)
		{
			if (len < 8)
			{
				while (len != 0)
				{
					*dest = (byte)val;
					dest++;
					len--;
				}
				return;
			}
			if (val != 0)
			{
				val |= val << 8;
				val |= val << 16;
			}
			int num = dest & 3;
			if (num != 0)
			{
				num = 4 - num;
				len -= num;
				do
				{
					*dest = (byte)val;
					dest++;
					num--;
				}
				while (num != 0);
			}
			while (len >= 16)
			{
				*(int*)dest = val;
				*(int*)(dest + 4) = val;
				*(int*)(dest + 8) = val;
				*(int*)(dest + 12) = val;
				dest += 16;
				len -= 16;
			}
			while (len >= 4)
			{
				*(int*)dest = val;
				dest += 4;
				len -= 4;
			}
			while (len > 0)
			{
				*dest = (byte)val;
				dest++;
				len--;
			}
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x00066820 File Offset: 0x00064A20
		private unsafe static void memcpy4(byte* dest, byte* src, int size)
		{
			while (size >= 16)
			{
				*(int*)dest = *(int*)src;
				*(int*)(dest + 4) = *(int*)(src + 4);
				*(int*)(dest + 8) = *(int*)(src + 8);
				*(int*)(dest + 12) = *(int*)(src + 12);
				dest += 16;
				src += 16;
				size -= 16;
			}
			while (size >= 4)
			{
				*(int*)dest = *(int*)src;
				dest += 4;
				src += 4;
				size -= 4;
			}
			while (size > 0)
			{
				*dest = *src;
				dest++;
				src++;
				size--;
			}
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000668A8 File Offset: 0x00064AA8
		private unsafe static void memcpy2(byte* dest, byte* src, int size)
		{
			while (size >= 8)
			{
				*(short*)dest = *(short*)src;
				*(short*)(dest + 2) = *(short*)(src + 2);
				*(short*)(dest + 4) = *(short*)(src + 4);
				*(short*)(dest + 6) = *(short*)(src + 6);
				dest += 8;
				src += 8;
				size -= 8;
			}
			while (size >= 2)
			{
				*(short*)dest = *(short*)src;
				dest += 2;
				src += 2;
				size -= 2;
			}
			if (size > 0)
			{
				*dest = *src;
			}
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x00066918 File Offset: 0x00064B18
		private unsafe static void memcpy1(byte* dest, byte* src, int size)
		{
			while (size >= 8)
			{
				*dest = *src;
				dest[1] = src[1];
				dest[2] = src[2];
				dest[3] = src[3];
				dest[4] = src[4];
				dest[5] = src[5];
				dest[6] = src[6];
				dest[7] = src[7];
				dest += 8;
				src += 8;
				size -= 8;
			}
			while (size >= 2)
			{
				*dest = *src;
				dest[1] = src[1];
				dest += 2;
				src += 2;
				size -= 2;
			}
			if (size > 0)
			{
				*dest = *src;
			}
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x000669B0 File Offset: 0x00064BB0
		internal unsafe static void memcpy(byte* dest, byte* src, int size)
		{
			if (((dest | src) & 3) != 0)
			{
				if ((dest & 1) != 0 && (src & 1) != 0 && size >= 1)
				{
					*dest = *src;
					dest++;
					src++;
					size--;
				}
				if ((dest & 2) != 0 && (src & 2) != 0 && size >= 2)
				{
					*(short*)dest = *(short*)src;
					dest += 2;
					src += 2;
					size -= 2;
				}
				if (((dest | src) & 1) != 0)
				{
					string.memcpy1(dest, src, size);
					return;
				}
				if (((dest | src) & 2) != 0)
				{
					string.memcpy2(dest, src, size);
					return;
				}
			}
			string.memcpy4(dest, src, size);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x00066A58 File Offset: 0x00064C58
		internal unsafe static void CharCopy(char* dest, char* src, int count)
		{
			if (((dest | src) & 3) != 0)
			{
				if ((dest & 2) != 0 && (src & 2) != 0 && count > 0)
				{
					*dest = (char)(*(short*)src);
					dest++;
					src++;
					count--;
				}
				if (((dest | src) & 2) != 0)
				{
					string.memcpy2((byte*)dest, (byte*)src, count * 2);
					return;
				}
			}
			string.memcpy4((byte*)dest, (byte*)src, count * 2);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x00066AC0 File Offset: 0x00064CC0
		internal unsafe static void CharCopyReverse(char* dest, char* src, int count)
		{
			dest += count;
			src += count;
			for (int i = count; i > 0; i--)
			{
				dest--;
				src--;
				*dest = *src;
			}
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x00066B00 File Offset: 0x00064D00
		internal unsafe static void CharCopy(string target, int targetIndex, string source, int sourceIndex, int count)
		{
			fixed (string text = target)
			{
				fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text2 = source, ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						string.CharCopy(ptr + targetIndex, ptr2 + sourceIndex, count);
						text = null;
					}
				}
			}
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00066B38 File Offset: 0x00064D38
		internal unsafe static void CharCopy(string target, int targetIndex, char[] source, int sourceIndex, int count)
		{
			fixed (string text = target)
			{
				fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (char* ptr2 = ref (source != null && source.Length != 0) ? ref source[0] : ref *null)
					{
						string.CharCopy(ptr + targetIndex, ptr2 + sourceIndex, count);
						text = null;
					}
				}
			}
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00066B84 File Offset: 0x00064D84
		internal unsafe static void CharCopyReverse(string target, int targetIndex, string source, int sourceIndex, int count)
		{
			fixed (string text = target)
			{
				fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text2 = source, ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						string.CharCopyReverse(ptr + targetIndex, ptr2 + sourceIndex, count);
						text = null;
					}
				}
			}
		}

		// Token: 0x06001B4C RID: 6988
		[MethodImpl(4096)]
		private extern string[] InternalSplit(char[] separator, int count, int options);

		// Token: 0x06001B4D RID: 6989
		[MethodImpl(4096)]
		internal static extern string InternalAllocateStr(int length);

		// Token: 0x06001B4E RID: 6990
		[MethodImpl(4096)]
		private static extern string InternalIntern(string str);

		// Token: 0x06001B4F RID: 6991
		[MethodImpl(4096)]
		private static extern string InternalIsInterned(string str);

		// Token: 0x06001B50 RID: 6992 RVA: 0x00066BBC File Offset: 0x00064DBC
		public static bool operator ==(string a, string b)
		{
			return string.Equals(a, b);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x00066BC8 File Offset: 0x00064DC8
		public static bool operator !=(string a, string b)
		{
			return !string.Equals(a, b);
		}

		// Token: 0x04000EA1 RID: 3745
		[NonSerialized]
		private int length;

		// Token: 0x04000EA2 RID: 3746
		[NonSerialized]
		private char start_char;

		// Token: 0x04000EA3 RID: 3747
		public static readonly string Empty = "";

		// Token: 0x04000EA4 RID: 3748
		private static readonly char[] WhiteChars = new char[]
		{
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			'\u0085',
			'\u1680',
			'\u2028',
			'\u2029',
			' ',
			'\u00a0',
			'\u2000',
			'\u2001',
			'\u2002',
			'\u2003',
			'\u2004',
			'\u2005',
			'\u2006',
			'\u2007',
			'\u2008',
			'\u2009',
			'\u200a',
			'​',
			'\u3000',
			'﻿',
			'\u202f',
			'\u205f'
		};
	}
}
