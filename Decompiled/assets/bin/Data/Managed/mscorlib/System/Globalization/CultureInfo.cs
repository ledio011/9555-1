using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Globalization
{
	// Token: 0x020000F6 RID: 246
	[ComVisible(true)]
	[Serializable]
	public class CultureInfo : ICloneable, IFormatProvider
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x00024F90 File Offset: 0x00023190
		public CultureInfo(int culture) : this(culture, true)
		{
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00024F9C File Offset: 0x0002319C
		public CultureInfo(int culture, bool useUserOverride) : this(culture, useUserOverride, false)
		{
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00024FA8 File Offset: 0x000231A8
		private CultureInfo(int culture, bool useUserOverride, bool read_only)
		{
			if (culture < 0)
			{
				throw new ArgumentOutOfRangeException("culture", "Positive number required.");
			}
			this.constructed = true;
			this.m_isReadOnly = read_only;
			this.m_useUserOverride = useUserOverride;
			if (culture == 127)
			{
				this.ConstructInvariant(read_only);
				return;
			}
			if (!this.ConstructInternalLocaleFromLcid(culture))
			{
				throw new ArgumentException(string.Format("Culture ID {0} (0x{0:X4}) is not a supported culture.", culture), "culture");
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00025020 File Offset: 0x00023220
		public CultureInfo(string name) : this(name, true)
		{
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0002502C File Offset: 0x0002322C
		public CultureInfo(string name, bool useUserOverride) : this(name, useUserOverride, false)
		{
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00025038 File Offset: 0x00023238
		private CultureInfo(string name, bool useUserOverride, bool read_only)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.constructed = true;
			this.m_isReadOnly = read_only;
			this.m_useUserOverride = useUserOverride;
			if (name.Length == 0)
			{
				this.ConstructInvariant(read_only);
				return;
			}
			if (!this.ConstructInternalLocaleFromName(name.ToLowerInvariant()))
			{
				throw new ArgumentException("Culture name " + name + " is not supported.", "name");
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x000250B0 File Offset: 0x000232B0
		private CultureInfo()
		{
			this.constructed = true;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x000250C0 File Offset: 0x000232C0
		static CultureInfo()
		{
			CultureInfo.invariant_culture_info = new CultureInfo(127, false, true);
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x000250E8 File Offset: 0x000232E8
		public static CultureInfo InvariantCulture
		{
			get
			{
				return CultureInfo.invariant_culture_info;
			}
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000250F4 File Offset: 0x000232F4
		public static CultureInfo CreateSpecificCulture(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				return CultureInfo.InvariantCulture;
			}
			CultureInfo cultureInfo = new CultureInfo();
			if (!CultureInfo.ConstructInternalLocaleFromSpecificName(cultureInfo, name.ToLowerInvariant()))
			{
				throw new ArgumentException("Culture name " + name + " is not supported.", name);
			}
			return cultureInfo;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00025158 File Offset: 0x00023358
		public static CultureInfo CurrentCulture
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00025164 File Offset: 0x00023364
		public static CultureInfo CurrentUICulture
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture;
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00025170 File Offset: 0x00023370
		internal static CultureInfo ConstructCurrentCulture()
		{
			CultureInfo cultureInfo = new CultureInfo();
			if (!CultureInfo.ConstructInternalLocaleFromCurrentLocale(cultureInfo))
			{
				cultureInfo = CultureInfo.InvariantCulture;
			}
			CultureInfo.BootstrapCultureID = cultureInfo.cultureID;
			return cultureInfo;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x000251A0 File Offset: 0x000233A0
		internal static CultureInfo ConstructCurrentUICulture()
		{
			return CultureInfo.ConstructCurrentCulture();
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x000251A8 File Offset: 0x000233A8
		internal string Territory
		{
			get
			{
				return this.territory;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x000251B0 File Offset: 0x000233B0
		public virtual int LCID
		{
			get
			{
				return this.cultureID;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x000251B8 File Offset: 0x000233B8
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x000251C0 File Offset: 0x000233C0
		public virtual string NativeName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.nativename;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x000251DC File Offset: 0x000233DC
		public virtual Calendar Calendar
		{
			get
			{
				return this.DateTimeFormat.Calendar;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x000251EC File Offset: 0x000233EC
		public virtual Calendar[] OptionalCalendars
		{
			get
			{
				if (this.optional_calendars == null)
				{
					lock (this)
					{
						if (this.optional_calendars == null)
						{
							this.ConstructCalendars();
						}
					}
				}
				return this.optional_calendars;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00025240 File Offset: 0x00023440
		public virtual CultureInfo Parent
		{
			get
			{
				if (this.parent_culture == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					if (this.parent_lcid == this.cultureID)
					{
						return null;
					}
					if (this.parent_lcid == 127)
					{
						this.parent_culture = CultureInfo.InvariantCulture;
					}
					else if (this.cultureID == 127)
					{
						this.parent_culture = this;
					}
					else
					{
						this.parent_culture = new CultureInfo(this.parent_lcid);
					}
				}
				return this.parent_culture;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x000252CC File Offset: 0x000234CC
		public virtual TextInfo TextInfo
		{
			get
			{
				if (this.textInfo == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					lock (this)
					{
						if (this.textInfo == null)
						{
							this.textInfo = this.CreateTextInfo(this.m_isReadOnly);
						}
					}
				}
				return this.textInfo;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00025344 File Offset: 0x00023544
		public virtual string ThreeLetterISOLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.iso3lang;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00025360 File Offset: 0x00023560
		public virtual string ThreeLetterWindowsLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.win3lang;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0002537C File Offset: 0x0002357C
		public virtual string TwoLetterISOLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.iso2lang;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00025398 File Offset: 0x00023598
		public bool UseUserOverride
		{
			get
			{
				return this.m_useUserOverride;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x000253A0 File Offset: 0x000235A0
		internal string IcuName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.icu_name;
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x000253BC File Offset: 0x000235BC
		public void ClearCachedData()
		{
			Thread.CurrentThread.CurrentCulture = null;
			Thread.CurrentThread.CurrentUICulture = null;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x000253D4 File Offset: 0x000235D4
		public virtual object Clone()
		{
			if (!this.constructed)
			{
				this.Construct();
			}
			CultureInfo cultureInfo = (CultureInfo)base.MemberwiseClone();
			cultureInfo.m_isReadOnly = false;
			cultureInfo.cached_serialized_form = null;
			if (!this.IsNeutralCulture)
			{
				cultureInfo.NumberFormat = (NumberFormatInfo)this.NumberFormat.Clone();
				cultureInfo.DateTimeFormat = (DateTimeFormatInfo)this.DateTimeFormat.Clone();
			}
			return cultureInfo;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00025444 File Offset: 0x00023644
		public override bool Equals(object value)
		{
			CultureInfo cultureInfo = value as CultureInfo;
			return cultureInfo != null && cultureInfo.cultureID == this.cultureID;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00025470 File Offset: 0x00023670
		public static CultureInfo[] GetCultures(CultureTypes types)
		{
			bool flag = (types & CultureTypes.NeutralCultures) != (CultureTypes)0;
			bool specific = (types & CultureTypes.SpecificCultures) != (CultureTypes)0;
			bool installed = (types & CultureTypes.InstalledWin32Cultures) != (CultureTypes)0;
			CultureInfo[] array = CultureInfo.internal_get_cultures(flag, specific, installed);
			if (flag && array.Length > 0 && array[0] == null)
			{
				array[0] = (CultureInfo)CultureInfo.InvariantCulture.Clone();
			}
			return array;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000254D0 File Offset: 0x000236D0
		public override int GetHashCode()
		{
			return this.cultureID;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000254D8 File Offset: 0x000236D8
		public static CultureInfo ReadOnly(CultureInfo ci)
		{
			if (ci == null)
			{
				throw new ArgumentNullException("ci");
			}
			if (ci.m_isReadOnly)
			{
				return ci;
			}
			CultureInfo cultureInfo = (CultureInfo)ci.Clone();
			cultureInfo.m_isReadOnly = true;
			if (cultureInfo.numInfo != null)
			{
				cultureInfo.numInfo = NumberFormatInfo.ReadOnly(cultureInfo.numInfo);
			}
			if (cultureInfo.dateTimeInfo != null)
			{
				cultureInfo.dateTimeInfo = DateTimeFormatInfo.ReadOnly(cultureInfo.dateTimeInfo);
			}
			if (cultureInfo.textInfo != null)
			{
				cultureInfo.textInfo = TextInfo.ReadOnly(cultureInfo.textInfo);
			}
			return cultureInfo;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00025580 File Offset: 0x00023780
		public override string ToString()
		{
			return this.m_name;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00025588 File Offset: 0x00023788
		public virtual CompareInfo CompareInfo
		{
			get
			{
				if (this.compareInfo == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					lock (this)
					{
						if (this.compareInfo == null)
						{
							this.compareInfo = new CompareInfo(this);
						}
					}
				}
				return this.compareInfo;
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000255FC File Offset: 0x000237FC
		internal static bool IsIDNeutralCulture(int lcid)
		{
			bool result;
			if (!CultureInfo.internal_is_lcid_neutral(lcid, out result))
			{
				throw new ArgumentException(string.Format("Culture id 0x{:x4} is not supported.", lcid));
			}
			return result;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00025630 File Offset: 0x00023830
		public virtual bool IsNeutralCulture
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.cultureID != 127 && ((this.cultureID & 65280) == 0 || this.specific_lcid == 0);
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00025670 File Offset: 0x00023870
		internal void CheckNeutral()
		{
			if (this.IsNeutralCulture)
			{
				throw new NotSupportedException("Culture \"" + this.m_name + "\" is a neutral culture. It can not be used in formatting and parsing and therefore cannot be set as the thread's current culture.");
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00025698 File Offset: 0x00023898
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x0002571C File Offset: 0x0002391C
		public virtual NumberFormatInfo NumberFormat
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				this.CheckNeutral();
				if (this.numInfo == null)
				{
					lock (this)
					{
						if (this.numInfo == null)
						{
							this.numInfo = new NumberFormatInfo(this.m_isReadOnly);
							this.construct_number_format();
						}
					}
				}
				return this.numInfo;
			}
			set
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				if (this.m_isReadOnly)
				{
					throw new InvalidOperationException(CultureInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException("NumberFormat");
				}
				this.numInfo = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0002576C File Offset: 0x0002396C
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x00025810 File Offset: 0x00023A10
		public virtual DateTimeFormatInfo DateTimeFormat
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				this.CheckNeutral();
				if (this.dateTimeInfo == null)
				{
					lock (this)
					{
						if (this.dateTimeInfo == null)
						{
							this.dateTimeInfo = new DateTimeFormatInfo(this.m_isReadOnly);
							this.construct_datetime_format();
							if (this.optional_calendars != null)
							{
								this.dateTimeInfo.Calendar = this.optional_calendars[0];
							}
						}
					}
				}
				return this.dateTimeInfo;
			}
			set
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				if (this.m_isReadOnly)
				{
					throw new InvalidOperationException(CultureInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException("DateTimeFormat");
				}
				this.dateTimeInfo = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00025860 File Offset: 0x00023A60
		public virtual string DisplayName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.displayname;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0002587C File Offset: 0x00023A7C
		public virtual string EnglishName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.englishname;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00025898 File Offset: 0x00023A98
		public static CultureInfo InstalledUICulture
		{
			get
			{
				return CultureInfo.GetCultureInfo(CultureInfo.BootstrapCultureID);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x000258A4 File Offset: 0x00023AA4
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x000258AC File Offset: 0x00023AAC
		public virtual object GetFormat(Type formatType)
		{
			object result = null;
			if (formatType == typeof(NumberFormatInfo))
			{
				result = this.NumberFormat;
			}
			else if (formatType == typeof(DateTimeFormatInfo))
			{
				result = this.DateTimeFormat;
			}
			return result;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x000258F0 File Offset: 0x00023AF0
		private void Construct()
		{
			this.construct_internal_locale_from_lcid(this.cultureID);
			this.constructed = true;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00025908 File Offset: 0x00023B08
		private bool ConstructInternalLocaleFromName(string locale)
		{
			string text = locale;
			if (text != null)
			{
				if (CultureInfo.<>f__switch$map19 == null)
				{
					CultureInfo.<>f__switch$map19 = new Dictionary<string, int>(2)
					{
						{
							"zh-hans",
							0
						},
						{
							"zh-hant",
							1
						}
					};
				}
				int num;
				if (CultureInfo.<>f__switch$map19.TryGetValue(text, out num))
				{
					if (num != 0)
					{
						if (num == 1)
						{
							locale = "zh-cht";
						}
					}
					else
					{
						locale = "zh-chs";
					}
				}
			}
			return this.construct_internal_locale_from_name(locale);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00025998 File Offset: 0x00023B98
		private bool ConstructInternalLocaleFromLcid(int lcid)
		{
			return this.construct_internal_locale_from_lcid(lcid);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000259AC File Offset: 0x00023BAC
		private static bool ConstructInternalLocaleFromSpecificName(CultureInfo ci, string name)
		{
			return CultureInfo.construct_internal_locale_from_specific_name(ci, name);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x000259C0 File Offset: 0x00023BC0
		private static bool ConstructInternalLocaleFromCurrentLocale(CultureInfo ci)
		{
			return CultureInfo.construct_internal_locale_from_current_locale(ci);
		}

		// Token: 0x060009C3 RID: 2499
		[MethodImpl(4096)]
		private extern bool construct_internal_locale_from_lcid(int lcid);

		// Token: 0x060009C4 RID: 2500
		[MethodImpl(4096)]
		private extern bool construct_internal_locale_from_name(string name);

		// Token: 0x060009C5 RID: 2501
		[MethodImpl(4096)]
		private static extern bool construct_internal_locale_from_specific_name(CultureInfo ci, string name);

		// Token: 0x060009C6 RID: 2502
		[MethodImpl(4096)]
		private static extern bool construct_internal_locale_from_current_locale(CultureInfo ci);

		// Token: 0x060009C7 RID: 2503
		[MethodImpl(4096)]
		private static extern CultureInfo[] internal_get_cultures(bool neutral, bool specific, bool installed);

		// Token: 0x060009C8 RID: 2504
		[MethodImpl(4096)]
		private extern void construct_datetime_format();

		// Token: 0x060009C9 RID: 2505
		[MethodImpl(4096)]
		private extern void construct_number_format();

		// Token: 0x060009CA RID: 2506
		[MethodImpl(4096)]
		private static extern bool internal_is_lcid_neutral(int lcid, out bool is_neutral);

		// Token: 0x060009CB RID: 2507 RVA: 0x000259D0 File Offset: 0x00023BD0
		private void ConstructInvariant(bool read_only)
		{
			this.cultureID = 127;
			this.numInfo = NumberFormatInfo.InvariantInfo;
			this.dateTimeInfo = DateTimeFormatInfo.InvariantInfo;
			if (!read_only)
			{
				this.numInfo = (NumberFormatInfo)this.numInfo.Clone();
				this.dateTimeInfo = (DateTimeFormatInfo)this.dateTimeInfo.Clone();
			}
			this.textInfo = this.CreateTextInfo(read_only);
			this.m_name = string.Empty;
			this.displayname = (this.englishname = (this.nativename = "Invariant Language (Invariant Country)"));
			this.iso3lang = "IVL";
			this.iso2lang = "iv";
			this.icu_name = "en_US_POSIX";
			this.win3lang = "IVL";
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00025A9C File Offset: 0x00023C9C
		private TextInfo CreateTextInfo(bool readOnly)
		{
			return new TextInfo(this, this.cultureID, this.textinfo_data, readOnly);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00025AB4 File Offset: 0x00023CB4
		private static void insert_into_shared_tables(CultureInfo c)
		{
			if (CultureInfo.shared_by_number == null)
			{
				CultureInfo.shared_by_number = new Hashtable();
				CultureInfo.shared_by_name = new Hashtable();
			}
			CultureInfo.shared_by_number[c.cultureID] = c;
			CultureInfo.shared_by_name[c.m_name] = c;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00025B08 File Offset: 0x00023D08
		public static CultureInfo GetCultureInfo(int culture)
		{
			object obj = CultureInfo.shared_table_lock;
			CultureInfo result;
			lock (obj)
			{
				CultureInfo cultureInfo;
				if (CultureInfo.shared_by_number != null)
				{
					cultureInfo = (CultureInfo.shared_by_number[culture] as CultureInfo);
					if (cultureInfo != null)
					{
						return cultureInfo;
					}
				}
				cultureInfo = new CultureInfo(culture, false, true);
				CultureInfo.insert_into_shared_tables(cultureInfo);
				result = cultureInfo;
			}
			return result;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00025B84 File Offset: 0x00023D84
		public static CultureInfo GetCultureInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = CultureInfo.shared_table_lock;
			CultureInfo result;
			lock (obj)
			{
				CultureInfo cultureInfo;
				if (CultureInfo.shared_by_name != null)
				{
					cultureInfo = (CultureInfo.shared_by_name[name] as CultureInfo);
					if (cultureInfo != null)
					{
						return cultureInfo;
					}
				}
				cultureInfo = new CultureInfo(name, false, true);
				CultureInfo.insert_into_shared_tables(cultureInfo);
				result = cultureInfo;
			}
			return result;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00025C0C File Offset: 0x00023E0C
		[MonoTODO("Currently it ignores the altName parameter")]
		public static CultureInfo GetCultureInfo(string name, string altName)
		{
			if (name == null)
			{
				throw new ArgumentNullException("null");
			}
			if (altName == null)
			{
				throw new ArgumentNullException("null");
			}
			return CultureInfo.GetCultureInfo(name);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00025C38 File Offset: 0x00023E38
		public static CultureInfo GetCultureInfoByIetfLanguageTag(string name)
		{
			if (name != null)
			{
				if (CultureInfo.<>f__switch$map1A == null)
				{
					CultureInfo.<>f__switch$map1A = new Dictionary<string, int>(2)
					{
						{
							"zh-Hans",
							0
						},
						{
							"zh-Hant",
							1
						}
					};
				}
				int num;
				if (CultureInfo.<>f__switch$map1A.TryGetValue(name, out num))
				{
					if (num == 0)
					{
						return CultureInfo.GetCultureInfo("zh-CHS");
					}
					if (num == 1)
					{
						return CultureInfo.GetCultureInfo("zh-CHT");
					}
				}
			}
			return CultureInfo.GetCultureInfo(name);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00025CBC File Offset: 0x00023EBC
		internal static CultureInfo CreateCulture(string name, bool reference)
		{
			bool flag = name.Length == 0;
			bool useUserOverride;
			bool read_only;
			if (reference)
			{
				useUserOverride = !flag;
				read_only = false;
			}
			else
			{
				read_only = false;
				useUserOverride = !flag;
			}
			return new CultureInfo(name, useUserOverride, read_only);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00025D08 File Offset: 0x00023F08
		internal unsafe void ConstructCalendars()
		{
			if (this.calendar_data == null)
			{
				this.optional_calendars = new Calendar[]
				{
					new GregorianCalendar(GregorianCalendarTypes.Localized)
				};
				return;
			}
			this.optional_calendars = new Calendar[5];
			for (int i = 0; i < 5; i++)
			{
				int num = this.calendar_data[i];
				Calendar calendar;
				switch (num >> 24)
				{
				case 0:
				{
					GregorianCalendarTypes type = (GregorianCalendarTypes)(num & 16777215);
					calendar = new GregorianCalendar(type);
					break;
				}
				case 1:
					calendar = new HijriCalendar();
					break;
				case 2:
					calendar = new ThaiBuddhistCalendar();
					break;
				default:
					throw new Exception("invalid calendar type:  " + num);
				}
				this.optional_calendars[i] = calendar;
			}
		}

		// Token: 0x0400034B RID: 843
		private const int NumOptionalCalendars = 5;

		// Token: 0x0400034C RID: 844
		private const int GregorianTypeMask = 16777215;

		// Token: 0x0400034D RID: 845
		private const int CalendarTypeBits = 24;

		// Token: 0x0400034E RID: 846
		private const int InvariantCultureId = 127;

		// Token: 0x0400034F RID: 847
		private static volatile CultureInfo invariant_culture_info;

		// Token: 0x04000350 RID: 848
		private static object shared_table_lock = new object();

		// Token: 0x04000351 RID: 849
		internal static int BootstrapCultureID;

		// Token: 0x04000352 RID: 850
		private bool m_isReadOnly;

		// Token: 0x04000353 RID: 851
		private int cultureID;

		// Token: 0x04000354 RID: 852
		[NonSerialized]
		private int parent_lcid;

		// Token: 0x04000355 RID: 853
		[NonSerialized]
		private int specific_lcid;

		// Token: 0x04000356 RID: 854
		[NonSerialized]
		private int datetime_index;

		// Token: 0x04000357 RID: 855
		[NonSerialized]
		private int number_index;

		// Token: 0x04000358 RID: 856
		private bool m_useUserOverride;

		// Token: 0x04000359 RID: 857
		[NonSerialized]
		private volatile NumberFormatInfo numInfo;

		// Token: 0x0400035A RID: 858
		private volatile DateTimeFormatInfo dateTimeInfo;

		// Token: 0x0400035B RID: 859
		private volatile TextInfo textInfo;

		// Token: 0x0400035C RID: 860
		private string m_name;

		// Token: 0x0400035D RID: 861
		[NonSerialized]
		private string displayname;

		// Token: 0x0400035E RID: 862
		[NonSerialized]
		private string englishname;

		// Token: 0x0400035F RID: 863
		[NonSerialized]
		private string nativename;

		// Token: 0x04000360 RID: 864
		[NonSerialized]
		private string iso3lang;

		// Token: 0x04000361 RID: 865
		[NonSerialized]
		private string iso2lang;

		// Token: 0x04000362 RID: 866
		[NonSerialized]
		private string icu_name;

		// Token: 0x04000363 RID: 867
		[NonSerialized]
		private string win3lang;

		// Token: 0x04000364 RID: 868
		[NonSerialized]
		private string territory;

		// Token: 0x04000365 RID: 869
		private volatile CompareInfo compareInfo;

		// Token: 0x04000366 RID: 870
		[NonSerialized]
		private unsafe readonly int* calendar_data;

		// Token: 0x04000367 RID: 871
		[NonSerialized]
		private unsafe readonly void* textinfo_data;

		// Token: 0x04000368 RID: 872
		[NonSerialized]
		private Calendar[] optional_calendars;

		// Token: 0x04000369 RID: 873
		[NonSerialized]
		private CultureInfo parent_culture;

		// Token: 0x0400036A RID: 874
		private int m_dataItem;

		// Token: 0x0400036B RID: 875
		private Calendar calendar;

		// Token: 0x0400036C RID: 876
		[NonSerialized]
		private bool constructed;

		// Token: 0x0400036D RID: 877
		[NonSerialized]
		internal byte[] cached_serialized_form;

		// Token: 0x0400036E RID: 878
		private static readonly string MSG_READONLY = "This instance is read only";

		// Token: 0x0400036F RID: 879
		private static Hashtable shared_by_number;

		// Token: 0x04000370 RID: 880
		private static Hashtable shared_by_name;
	}
}
