using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Globalization
{
	// Token: 0x020000F9 RID: 249
	[ComVisible(true)]
	[Serializable]
	public sealed class DateTimeFormatInfo : ICloneable, IFormatProvider
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x00025DD0 File Offset: 0x00023FD0
		internal DateTimeFormatInfo(bool read_only)
		{
			this.m_isReadOnly = read_only;
			this.amDesignator = "AM";
			this.pmDesignator = "PM";
			this.dateSeparator = "/";
			this.timeSeparator = ":";
			this.shortDatePattern = "MM/dd/yyyy";
			this.longDatePattern = "dddd, dd MMMM yyyy";
			this.shortTimePattern = "HH:mm";
			this.longTimePattern = "HH:mm:ss";
			this.monthDayPattern = "MMMM dd";
			this.yearMonthPattern = "yyyy MMMM";
			this.fullDateTimePattern = "dddd, dd MMMM yyyy HH:mm:ss";
			this._RFC1123Pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
			this._SortableDateTimePattern = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
			this._UniversalSortableDateTimePattern = "yyyy'-'MM'-'dd HH':'mm':'ss'Z'";
			this.firstDayOfWeek = 0;
			this.calendar = new GregorianCalendar();
			this.calendarWeekRule = 0;
			this.abbreviatedDayNames = DateTimeFormatInfo.INVARIANT_ABBREVIATED_DAY_NAMES;
			this.dayNames = DateTimeFormatInfo.INVARIANT_DAY_NAMES;
			this.abbreviatedMonthNames = DateTimeFormatInfo.INVARIANT_ABBREVIATED_MONTH_NAMES;
			this.monthNames = DateTimeFormatInfo.INVARIANT_MONTH_NAMES;
			this.m_genitiveAbbreviatedMonthNames = DateTimeFormatInfo.INVARIANT_ABBREVIATED_MONTH_NAMES;
			this.genitiveMonthNames = DateTimeFormatInfo.INVARIANT_MONTH_NAMES;
			this.shortDayNames = DateTimeFormatInfo.INVARIANT_SHORT_DAY_NAMES;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00025EEC File Offset: 0x000240EC
		public DateTimeFormatInfo() : this(false)
		{
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x000260DC File Offset: 0x000242DC
		public static DateTimeFormatInfo GetInstance(IFormatProvider provider)
		{
			if (provider != null)
			{
				DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)provider.GetFormat(typeof(DateTimeFormatInfo));
				if (dateTimeFormatInfo != null)
				{
					return dateTimeFormatInfo;
				}
			}
			return DateTimeFormatInfo.CurrentInfo;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00026114 File Offset: 0x00024314
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002611C File Offset: 0x0002431C
		public static DateTimeFormatInfo ReadOnly(DateTimeFormatInfo dtfi)
		{
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)dtfi.Clone();
			dateTimeFormatInfo.m_isReadOnly = true;
			return dateTimeFormatInfo;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00026140 File Offset: 0x00024340
		public object Clone()
		{
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)base.MemberwiseClone();
			dateTimeFormatInfo.m_isReadOnly = false;
			return dateTimeFormatInfo;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00026164 File Offset: 0x00024364
		public object GetFormat(Type formatType)
		{
			return (formatType != base.GetType()) ? null : this;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002617C File Offset: 0x0002437C
		public string GetAbbreviatedMonthName(int month)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.abbreviatedMonthNames[month - 1];
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x000261A0 File Offset: 0x000243A0
		public string GetEraName(int era)
		{
			if (era < 0 || era > this.calendar.EraNames.Length)
			{
				throw new ArgumentOutOfRangeException("era", era.ToString());
			}
			return this.calendar.EraNames[era - 1];
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000261E0 File Offset: 0x000243E0
		public string GetMonthName(int month)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.monthNames[month - 1];
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00026204 File Offset: 0x00024404
		internal string[] RawAbbreviatedDayNames
		{
			get
			{
				return this.abbreviatedDayNames;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0002620C File Offset: 0x0002440C
		internal string[] RawAbbreviatedMonthNames
		{
			get
			{
				return this.abbreviatedMonthNames;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00026214 File Offset: 0x00024414
		internal string[] RawDayNames
		{
			get
			{
				return this.dayNames;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0002621C File Offset: 0x0002441C
		internal string[] RawMonthNames
		{
			get
			{
				return this.monthNames;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00026224 File Offset: 0x00024424
		public string AMDesignator
		{
			get
			{
				return this.amDesignator;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0002622C File Offset: 0x0002442C
		public string PMDesignator
		{
			get
			{
				return this.pmDesignator;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00026234 File Offset: 0x00024434
		public string DateSeparator
		{
			get
			{
				return this.dateSeparator;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002623C File Offset: 0x0002443C
		public string TimeSeparator
		{
			get
			{
				return this.timeSeparator;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00026244 File Offset: 0x00024444
		public string LongDatePattern
		{
			get
			{
				return this.longDatePattern;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0002624C File Offset: 0x0002444C
		public string ShortDatePattern
		{
			get
			{
				return this.shortDatePattern;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00026254 File Offset: 0x00024454
		public string ShortTimePattern
		{
			get
			{
				return this.shortTimePattern;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0002625C File Offset: 0x0002445C
		public string LongTimePattern
		{
			get
			{
				return this.longTimePattern;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00026264 File Offset: 0x00024464
		public string MonthDayPattern
		{
			get
			{
				return this.monthDayPattern;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0002626C File Offset: 0x0002446C
		public string YearMonthPattern
		{
			get
			{
				return this.yearMonthPattern;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00026274 File Offset: 0x00024474
		public string FullDateTimePattern
		{
			get
			{
				if (this.fullDateTimePattern != null)
				{
					return this.fullDateTimePattern;
				}
				return this.longDatePattern + " " + this.longTimePattern;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x000262A0 File Offset: 0x000244A0
		public static DateTimeFormatInfo CurrentInfo
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture.DateTimeFormat;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x000262B4 File Offset: 0x000244B4
		public static DateTimeFormatInfo InvariantInfo
		{
			get
			{
				if (DateTimeFormatInfo.theInvariantDateTimeFormatInfo == null)
				{
					DateTimeFormatInfo.theInvariantDateTimeFormatInfo = DateTimeFormatInfo.ReadOnly(new DateTimeFormatInfo());
					DateTimeFormatInfo.theInvariantDateTimeFormatInfo.FillInvariantPatterns();
				}
				return DateTimeFormatInfo.theInvariantDateTimeFormatInfo;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x000262E0 File Offset: 0x000244E0
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x000262E8 File Offset: 0x000244E8
		public Calendar Calendar
		{
			get
			{
				return this.calendar;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(DateTimeFormatInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.calendar = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00026314 File Offset: 0x00024514
		public string RFC1123Pattern
		{
			get
			{
				return this._RFC1123Pattern;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0002631C File Offset: 0x0002451C
		internal string RoundtripPattern
		{
			get
			{
				return "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00026324 File Offset: 0x00024524
		public string SortableDateTimePattern
		{
			get
			{
				return this._SortableDateTimePattern;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0002632C File Offset: 0x0002452C
		public string UniversalSortableDateTimePattern
		{
			get
			{
				return this._UniversalSortableDateTimePattern;
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00026334 File Offset: 0x00024534
		internal string[] GetAllDateTimePatternsInternal()
		{
			this.FillAllDateTimePatterns();
			return this.all_date_time_patterns;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00026344 File Offset: 0x00024544
		private void FillAllDateTimePatterns()
		{
			if (this.all_date_time_patterns != null)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			arrayList.AddRange(this.GetAllRawDateTimePatterns('d'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('D'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('g'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('G'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('f'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('F'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('m'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('M'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('r'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('R'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('s'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('t'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('T'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('u'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('U'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('y'));
			arrayList.AddRange(this.GetAllRawDateTimePatterns('Y'));
			this.all_date_time_patterns = (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00026470 File Offset: 0x00024670
		internal string[] GetAllRawDateTimePatterns(char format)
		{
			string[] array;
			switch (format)
			{
			case 'R':
				goto IL_2CB;
			default:
				switch (format)
				{
				case 'r':
					goto IL_2CB;
				case 's':
					return new string[]
					{
						this.SortableDateTimePattern
					};
				case 't':
					if (this.allShortTimePatterns != null && this.allShortTimePatterns.Length > 0)
					{
						return this.allShortTimePatterns;
					}
					return new string[]
					{
						this.ShortTimePattern
					};
				case 'u':
					return new string[]
					{
						this.UniversalSortableDateTimePattern
					};
				default:
					switch (format)
					{
					case 'D':
						if (this.allLongDatePatterns != null && this.allLongDatePatterns.Length > 0)
						{
							return this.allLongDatePatterns;
						}
						return new string[]
						{
							this.LongDatePattern
						};
					default:
						switch (format)
						{
						case 'd':
							if (this.allShortDatePatterns != null && this.allShortDatePatterns.Length > 0)
							{
								return this.allShortDatePatterns;
							}
							return new string[]
							{
								this.ShortDatePattern
							};
						default:
							if (format != 'M' && format != 'm')
							{
								throw new ArgumentException("Format specifier was invalid.");
							}
							if (this.monthDayPatterns != null && this.monthDayPatterns.Length > 0)
							{
								return this.monthDayPatterns;
							}
							return new string[]
							{
								this.MonthDayPattern
							};
						case 'f':
							array = this.PopulateCombinedList(this.allLongDatePatterns, this.allShortTimePatterns);
							if (array != null && array.Length > 0)
							{
								return array;
							}
							return new string[]
							{
								this.LongDatePattern + " " + this.ShortTimePattern
							};
						case 'g':
							array = this.PopulateCombinedList(this.allShortDatePatterns, this.allShortTimePatterns);
							if (array != null && array.Length > 0)
							{
								return array;
							}
							return new string[]
							{
								this.ShortDatePattern + " " + this.ShortTimePattern
							};
						}
						break;
					case 'F':
						break;
					case 'G':
						array = this.PopulateCombinedList(this.allShortDatePatterns, this.allLongTimePatterns);
						if (array != null && array.Length > 0)
						{
							return array;
						}
						return new string[]
						{
							this.ShortDatePattern + " " + this.LongTimePattern
						};
					}
					break;
				case 'y':
					goto IL_29B;
				}
				break;
			case 'T':
				if (this.allLongTimePatterns != null && this.allLongTimePatterns.Length > 0)
				{
					return this.allLongTimePatterns;
				}
				return new string[]
				{
					this.LongTimePattern
				};
			case 'U':
				break;
			case 'Y':
				goto IL_29B;
			}
			array = this.PopulateCombinedList(this.allLongDatePatterns, this.allLongTimePatterns);
			if (array != null && array.Length > 0)
			{
				return array;
			}
			return new string[]
			{
				this.LongDatePattern + " " + this.LongTimePattern
			};
			IL_29B:
			if (this.yearMonthPatterns != null && this.yearMonthPatterns.Length > 0)
			{
				return this.yearMonthPatterns;
			}
			return new string[]
			{
				this.YearMonthPattern
			};
			IL_2CB:
			return new string[]
			{
				this.RFC1123Pattern
			};
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00026784 File Offset: 0x00024984
		public string GetDayName(DayOfWeek dayofweek)
		{
			if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.dayNames[(int)dayofweek];
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000267B0 File Offset: 0x000249B0
		public string GetAbbreviatedDayName(DayOfWeek dayofweek)
		{
			if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.abbreviatedDayNames[(int)dayofweek];
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x000267DC File Offset: 0x000249DC
		private void FillInvariantPatterns()
		{
			this.allShortDatePatterns = new string[]
			{
				"MM/dd/yyyy"
			};
			this.allLongDatePatterns = new string[]
			{
				"dddd, dd MMMM yyyy"
			};
			this.allLongTimePatterns = new string[]
			{
				"HH:mm:ss"
			};
			this.allShortTimePatterns = new string[]
			{
				"HH:mm",
				"hh:mm tt",
				"H:mm",
				"h:mm tt"
			};
			this.monthDayPatterns = new string[]
			{
				"MMMM dd"
			};
			this.yearMonthPatterns = new string[]
			{
				"yyyy MMMM"
			};
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002687C File Offset: 0x00024A7C
		private string[] PopulateCombinedList(string[] dates, string[] times)
		{
			if (dates != null && times != null)
			{
				string[] array = new string[dates.Length * times.Length];
				int num = 0;
				foreach (string str in dates)
				{
					foreach (string str2 in times)
					{
						array[num++] = str + " " + str2;
					}
				}
				return array;
			}
			return null;
		}

		// Token: 0x04000382 RID: 898
		private const string _RoundtripPattern = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";

		// Token: 0x04000383 RID: 899
		private static readonly string MSG_READONLY = "This instance is read only";

		// Token: 0x04000384 RID: 900
		private static readonly string MSG_ARRAYSIZE_MONTH = "An array with exactly 13 elements is needed";

		// Token: 0x04000385 RID: 901
		private static readonly string MSG_ARRAYSIZE_DAY = "An array with exactly 7 elements is needed";

		// Token: 0x04000386 RID: 902
		private static readonly string[] INVARIANT_ABBREVIATED_DAY_NAMES = new string[]
		{
			"Sun",
			"Mon",
			"Tue",
			"Wed",
			"Thu",
			"Fri",
			"Sat"
		};

		// Token: 0x04000387 RID: 903
		private static readonly string[] INVARIANT_DAY_NAMES = new string[]
		{
			"Sunday",
			"Monday",
			"Tuesday",
			"Wednesday",
			"Thursday",
			"Friday",
			"Saturday"
		};

		// Token: 0x04000388 RID: 904
		private static readonly string[] INVARIANT_ABBREVIATED_MONTH_NAMES = new string[]
		{
			"Jan",
			"Feb",
			"Mar",
			"Apr",
			"May",
			"Jun",
			"Jul",
			"Aug",
			"Sep",
			"Oct",
			"Nov",
			"Dec",
			string.Empty
		};

		// Token: 0x04000389 RID: 905
		private static readonly string[] INVARIANT_MONTH_NAMES = new string[]
		{
			"January",
			"February",
			"March",
			"April",
			"May",
			"June",
			"July",
			"August",
			"September",
			"October",
			"November",
			"December",
			string.Empty
		};

		// Token: 0x0400038A RID: 906
		private static readonly string[] INVARIANT_SHORT_DAY_NAMES = new string[]
		{
			"Su",
			"Mo",
			"Tu",
			"We",
			"Th",
			"Fr",
			"Sa"
		};

		// Token: 0x0400038B RID: 907
		private static DateTimeFormatInfo theInvariantDateTimeFormatInfo;

		// Token: 0x0400038C RID: 908
		private bool m_isReadOnly;

		// Token: 0x0400038D RID: 909
		private string amDesignator;

		// Token: 0x0400038E RID: 910
		private string pmDesignator;

		// Token: 0x0400038F RID: 911
		private string dateSeparator;

		// Token: 0x04000390 RID: 912
		private string timeSeparator;

		// Token: 0x04000391 RID: 913
		private string shortDatePattern;

		// Token: 0x04000392 RID: 914
		private string longDatePattern;

		// Token: 0x04000393 RID: 915
		private string shortTimePattern;

		// Token: 0x04000394 RID: 916
		private string longTimePattern;

		// Token: 0x04000395 RID: 917
		private string monthDayPattern;

		// Token: 0x04000396 RID: 918
		private string yearMonthPattern;

		// Token: 0x04000397 RID: 919
		private string fullDateTimePattern;

		// Token: 0x04000398 RID: 920
		private string _RFC1123Pattern;

		// Token: 0x04000399 RID: 921
		private string _SortableDateTimePattern;

		// Token: 0x0400039A RID: 922
		private string _UniversalSortableDateTimePattern;

		// Token: 0x0400039B RID: 923
		private int firstDayOfWeek;

		// Token: 0x0400039C RID: 924
		private Calendar calendar;

		// Token: 0x0400039D RID: 925
		private int calendarWeekRule;

		// Token: 0x0400039E RID: 926
		private string[] abbreviatedDayNames;

		// Token: 0x0400039F RID: 927
		private string[] dayNames;

		// Token: 0x040003A0 RID: 928
		private string[] monthNames;

		// Token: 0x040003A1 RID: 929
		private string[] abbreviatedMonthNames;

		// Token: 0x040003A2 RID: 930
		private string[] allShortDatePatterns;

		// Token: 0x040003A3 RID: 931
		private string[] allLongDatePatterns;

		// Token: 0x040003A4 RID: 932
		private string[] allShortTimePatterns;

		// Token: 0x040003A5 RID: 933
		private string[] allLongTimePatterns;

		// Token: 0x040003A6 RID: 934
		private string[] monthDayPatterns;

		// Token: 0x040003A7 RID: 935
		private string[] yearMonthPatterns;

		// Token: 0x040003A8 RID: 936
		private string[] shortDayNames;

		// Token: 0x040003A9 RID: 937
		private int nDataItem;

		// Token: 0x040003AA RID: 938
		private bool m_useUserOverride;

		// Token: 0x040003AB RID: 939
		private bool m_isDefaultCalendar;

		// Token: 0x040003AC RID: 940
		private int CultureID;

		// Token: 0x040003AD RID: 941
		private bool bUseCalendarInfo;

		// Token: 0x040003AE RID: 942
		private string generalShortTimePattern;

		// Token: 0x040003AF RID: 943
		private string generalLongTimePattern;

		// Token: 0x040003B0 RID: 944
		private string[] m_eraNames;

		// Token: 0x040003B1 RID: 945
		private string[] m_abbrevEraNames;

		// Token: 0x040003B2 RID: 946
		private string[] m_abbrevEnglishEraNames;

		// Token: 0x040003B3 RID: 947
		private string[] m_dateWords;

		// Token: 0x040003B4 RID: 948
		private int[] optionalCalendars;

		// Token: 0x040003B5 RID: 949
		private string[] m_superShortDayNames;

		// Token: 0x040003B6 RID: 950
		private string[] genitiveMonthNames;

		// Token: 0x040003B7 RID: 951
		private string[] m_genitiveAbbreviatedMonthNames;

		// Token: 0x040003B8 RID: 952
		private string[] leapYearMonthNames;

		// Token: 0x040003B9 RID: 953
		private DateTimeFormatFlags formatFlags;

		// Token: 0x040003BA RID: 954
		private string m_name;

		// Token: 0x040003BB RID: 955
		private volatile string[] all_date_time_patterns;
	}
}
