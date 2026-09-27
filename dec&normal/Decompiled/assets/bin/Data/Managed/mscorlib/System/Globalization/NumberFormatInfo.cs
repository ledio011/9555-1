using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Globalization
{
	// Token: 0x020000FF RID: 255
	[ComVisible(true)]
	[Serializable]
	public sealed class NumberFormatInfo : ICloneable, IFormatProvider
	{
		// Token: 0x06000A16 RID: 2582 RVA: 0x00026C68 File Offset: 0x00024E68
		internal NumberFormatInfo(int lcid, bool read_only)
		{
			this.isReadOnly = read_only;
			if (lcid != 127)
			{
				lcid = 127;
			}
			int num = lcid;
			if (num == 127)
			{
				this.isReadOnly = false;
				this.currencyDecimalDigits = 2;
				this.currencyDecimalSeparator = ".";
				this.currencyGroupSeparator = ",";
				this.currencyGroupSizes = new int[]
				{
					3
				};
				this.currencyNegativePattern = 0;
				this.currencyPositivePattern = 0;
				this.currencySymbol = "$";
				this.nanSymbol = "NaN";
				this.negativeInfinitySymbol = "-Infinity";
				this.negativeSign = "-";
				this.numberDecimalDigits = 2;
				this.numberDecimalSeparator = ".";
				this.numberGroupSeparator = ",";
				this.numberGroupSizes = new int[]
				{
					3
				};
				this.numberNegativePattern = 1;
				this.percentDecimalDigits = 2;
				this.percentDecimalSeparator = ".";
				this.percentGroupSeparator = ",";
				this.percentGroupSizes = new int[]
				{
					3
				};
				this.percentNegativePattern = 0;
				this.percentPositivePattern = 0;
				this.percentSymbol = "%";
				this.perMilleSymbol = "‰";
				this.positiveInfinitySymbol = "Infinity";
				this.positiveSign = "+";
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00026DD4 File Offset: 0x00024FD4
		internal NumberFormatInfo(bool read_only) : this(127, read_only)
		{
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00026DE0 File Offset: 0x00024FE0
		public NumberFormatInfo() : this(false)
		{
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00026E58 File Offset: 0x00025058
		public int CurrencyDecimalDigits
		{
			get
			{
				return this.currencyDecimalDigits;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00026E60 File Offset: 0x00025060
		public string CurrencyDecimalSeparator
		{
			get
			{
				return this.currencyDecimalSeparator;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x00026E68 File Offset: 0x00025068
		public string CurrencyGroupSeparator
		{
			get
			{
				return this.currencyGroupSeparator;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00026E70 File Offset: 0x00025070
		internal int[] RawCurrencyGroupSizes
		{
			get
			{
				return this.currencyGroupSizes;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00026E78 File Offset: 0x00025078
		public int CurrencyNegativePattern
		{
			get
			{
				return this.currencyNegativePattern;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00026E80 File Offset: 0x00025080
		public int CurrencyPositivePattern
		{
			get
			{
				return this.currencyPositivePattern;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00026E88 File Offset: 0x00025088
		public string CurrencySymbol
		{
			get
			{
				return this.currencySymbol;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00026E90 File Offset: 0x00025090
		public static NumberFormatInfo CurrentInfo
		{
			get
			{
				NumberFormatInfo numberFormat = Thread.CurrentThread.CurrentCulture.NumberFormat;
				numberFormat.isReadOnly = true;
				return numberFormat;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00026EB8 File Offset: 0x000250B8
		public static NumberFormatInfo InvariantInfo
		{
			get
			{
				return new NumberFormatInfo
				{
					NumberNegativePattern = 1,
					isReadOnly = true
				};
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00026EDC File Offset: 0x000250DC
		public string NaNSymbol
		{
			get
			{
				return this.nanSymbol;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00026EE4 File Offset: 0x000250E4
		public string NegativeInfinitySymbol
		{
			get
			{
				return this.negativeInfinitySymbol;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00026EEC File Offset: 0x000250EC
		public string NegativeSign
		{
			get
			{
				return this.negativeSign;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00026EF4 File Offset: 0x000250F4
		public int NumberDecimalDigits
		{
			get
			{
				return this.numberDecimalDigits;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x00026EFC File Offset: 0x000250FC
		public string NumberDecimalSeparator
		{
			get
			{
				return this.numberDecimalSeparator;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x00026F04 File Offset: 0x00025104
		public string NumberGroupSeparator
		{
			get
			{
				return this.numberGroupSeparator;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00026F0C File Offset: 0x0002510C
		internal int[] RawNumberGroupSizes
		{
			get
			{
				return this.numberGroupSizes;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00026F14 File Offset: 0x00025114
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x00026F1C File Offset: 0x0002511C
		public int NumberNegativePattern
		{
			get
			{
				return this.numberNegativePattern;
			}
			set
			{
				if (value < 0 || value > 4)
				{
					throw new ArgumentOutOfRangeException("The value specified for the property is less than 0 or greater than 15");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.numberNegativePattern = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00026F54 File Offset: 0x00025154
		public int PercentDecimalDigits
		{
			get
			{
				return this.percentDecimalDigits;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00026F5C File Offset: 0x0002515C
		public string PercentDecimalSeparator
		{
			get
			{
				return this.percentDecimalSeparator;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00026F64 File Offset: 0x00025164
		public string PercentGroupSeparator
		{
			get
			{
				return this.percentGroupSeparator;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00026F6C File Offset: 0x0002516C
		internal int[] RawPercentGroupSizes
		{
			get
			{
				return this.percentGroupSizes;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00026F74 File Offset: 0x00025174
		public int PercentNegativePattern
		{
			get
			{
				return this.percentNegativePattern;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00026F7C File Offset: 0x0002517C
		public int PercentPositivePattern
		{
			get
			{
				return this.percentPositivePattern;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00026F84 File Offset: 0x00025184
		public string PercentSymbol
		{
			get
			{
				return this.percentSymbol;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00026F8C File Offset: 0x0002518C
		public string PerMilleSymbol
		{
			get
			{
				return this.perMilleSymbol;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x00026F94 File Offset: 0x00025194
		public string PositiveInfinitySymbol
		{
			get
			{
				return this.positiveInfinitySymbol;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00026F9C File Offset: 0x0002519C
		public string PositiveSign
		{
			get
			{
				return this.positiveSign;
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00026FA4 File Offset: 0x000251A4
		public object GetFormat(Type formatType)
		{
			return (formatType != typeof(NumberFormatInfo)) ? null : this;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00026FC0 File Offset: 0x000251C0
		public object Clone()
		{
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)base.MemberwiseClone();
			numberFormatInfo.isReadOnly = false;
			return numberFormatInfo;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00026FE4 File Offset: 0x000251E4
		public static NumberFormatInfo ReadOnly(NumberFormatInfo nfi)
		{
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)nfi.Clone();
			numberFormatInfo.isReadOnly = true;
			return numberFormatInfo;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00027008 File Offset: 0x00025208
		public static NumberFormatInfo GetInstance(IFormatProvider formatProvider)
		{
			if (formatProvider != null)
			{
				NumberFormatInfo numberFormatInfo = (NumberFormatInfo)formatProvider.GetFormat(typeof(NumberFormatInfo));
				if (numberFormatInfo != null)
				{
					return numberFormatInfo;
				}
			}
			return NumberFormatInfo.CurrentInfo;
		}

		// Token: 0x040003DB RID: 987
		private bool isReadOnly;

		// Token: 0x040003DC RID: 988
		private string decimalFormats;

		// Token: 0x040003DD RID: 989
		private string currencyFormats;

		// Token: 0x040003DE RID: 990
		private string percentFormats;

		// Token: 0x040003DF RID: 991
		private string digitPattern = "#";

		// Token: 0x040003E0 RID: 992
		private string zeroPattern = "0";

		// Token: 0x040003E1 RID: 993
		private int currencyDecimalDigits;

		// Token: 0x040003E2 RID: 994
		private string currencyDecimalSeparator;

		// Token: 0x040003E3 RID: 995
		private string currencyGroupSeparator;

		// Token: 0x040003E4 RID: 996
		private int[] currencyGroupSizes;

		// Token: 0x040003E5 RID: 997
		private int currencyNegativePattern;

		// Token: 0x040003E6 RID: 998
		private int currencyPositivePattern;

		// Token: 0x040003E7 RID: 999
		private string currencySymbol;

		// Token: 0x040003E8 RID: 1000
		private string nanSymbol;

		// Token: 0x040003E9 RID: 1001
		private string negativeInfinitySymbol;

		// Token: 0x040003EA RID: 1002
		private string negativeSign;

		// Token: 0x040003EB RID: 1003
		private int numberDecimalDigits;

		// Token: 0x040003EC RID: 1004
		private string numberDecimalSeparator;

		// Token: 0x040003ED RID: 1005
		private string numberGroupSeparator;

		// Token: 0x040003EE RID: 1006
		private int[] numberGroupSizes;

		// Token: 0x040003EF RID: 1007
		private int numberNegativePattern;

		// Token: 0x040003F0 RID: 1008
		private int percentDecimalDigits;

		// Token: 0x040003F1 RID: 1009
		private string percentDecimalSeparator;

		// Token: 0x040003F2 RID: 1010
		private string percentGroupSeparator;

		// Token: 0x040003F3 RID: 1011
		private int[] percentGroupSizes;

		// Token: 0x040003F4 RID: 1012
		private int percentNegativePattern;

		// Token: 0x040003F5 RID: 1013
		private int percentPositivePattern;

		// Token: 0x040003F6 RID: 1014
		private string percentSymbol;

		// Token: 0x040003F7 RID: 1015
		private string perMilleSymbol;

		// Token: 0x040003F8 RID: 1016
		private string positiveInfinitySymbol;

		// Token: 0x040003F9 RID: 1017
		private string positiveSign;

		// Token: 0x040003FA RID: 1018
		private string ansiCurrencySymbol;

		// Token: 0x040003FB RID: 1019
		private int m_dataItem;

		// Token: 0x040003FC RID: 1020
		private bool m_useUserOverride;

		// Token: 0x040003FD RID: 1021
		private bool validForParseAsNumber;

		// Token: 0x040003FE RID: 1022
		private bool validForParseAsCurrency;

		// Token: 0x040003FF RID: 1023
		private string[] nativeDigits = NumberFormatInfo.invariantNativeDigits;

		// Token: 0x04000400 RID: 1024
		private int digitSubstitution = 1;

		// Token: 0x04000401 RID: 1025
		private static readonly string[] invariantNativeDigits = new string[]
		{
			"0",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9"
		};
	}
}
