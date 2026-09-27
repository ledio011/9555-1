using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x02000102 RID: 258
	[ComVisible(true)]
	[MonoTODO("IDeserializationCallback isn't implemented.")]
	[Serializable]
	public class TextInfo : ICloneable, IDeserializationCallback
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x0002725C File Offset: 0x0002545C
		internal unsafe TextInfo(CultureInfo ci, int lcid, void* data, bool read_only)
		{
			this.m_isReadOnly = read_only;
			this.m_win32LangID = lcid;
			this.ci = ci;
			if (data != null)
			{
				this.data = *(TextInfo.Data*)data;
			}
			else
			{
				this.data = default(TextInfo.Data);
				this.data.list_sep = 44;
			}
			CultureInfo cultureInfo = ci;
			while (cultureInfo.Parent != null && cultureInfo.Parent.LCID != 127 && cultureInfo.Parent != cultureInfo)
			{
				cultureInfo = cultureInfo.Parent;
			}
			if (cultureInfo != null)
			{
				int lcid2 = cultureInfo.LCID;
				if (lcid2 == 31 || lcid2 == 44)
				{
					this.handleDotI = true;
				}
			}
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00027320 File Offset: 0x00025520
		private TextInfo(TextInfo textInfo)
		{
			this.m_win32LangID = textInfo.m_win32LangID;
			this.m_nDataItem = textInfo.m_nDataItem;
			this.m_useUserOverride = textInfo.m_useUserOverride;
			this.m_listSeparator = textInfo.ListSeparator;
			this.customCultureName = textInfo.CultureName;
			this.ci = textInfo.ci;
			this.handleDotI = textInfo.handleDotI;
			this.data = textInfo.data;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00027394 File Offset: 0x00025594
		[MonoTODO]
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00027398 File Offset: 0x00025598
		public virtual string ListSeparator
		{
			get
			{
				if (this.m_listSeparator == null)
				{
					this.m_listSeparator = ((char)this.data.list_sep).ToString();
				}
				return this.m_listSeparator;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x000273D4 File Offset: 0x000255D4
		[ComVisible(false)]
		public string CultureName
		{
			get
			{
				if (this.customCultureName == null)
				{
					this.customCultureName = this.ci.Name;
				}
				return this.customCultureName;
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000273F8 File Offset: 0x000255F8
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			TextInfo textInfo = obj as TextInfo;
			return textInfo != null && textInfo.m_win32LangID == this.m_win32LangID && textInfo.ci == this.ci;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00027444 File Offset: 0x00025644
		public override int GetHashCode()
		{
			return this.m_win32LangID;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002744C File Offset: 0x0002564C
		public override string ToString()
		{
			return "TextInfo - " + this.m_win32LangID;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00027464 File Offset: 0x00025664
		public virtual char ToLower(char c)
		{
			if (c < '@' || ('`' < c && c < '\u0080'))
			{
				return c;
			}
			if ('A' <= c && c <= 'Z' && (!this.handleDotI || c != 'I'))
			{
				return c + ' ';
			}
			if (this.ci == null || this.ci.LCID == 127)
			{
				return char.ToLowerInvariant(c);
			}
			switch (c)
			{
			case 'ǅ':
				return 'ǆ';
			default:
				switch (c)
				{
				case 'ϒ':
					return 'υ';
				case 'ϓ':
					return 'ύ';
				case 'ϔ':
					return 'ϋ';
				default:
					if (c != 'I')
					{
						if (c == 'İ')
						{
							return 'i';
						}
						if (c == 'ǋ')
						{
							return 'ǌ';
						}
						if (c == 'ǲ')
						{
							return 'ǳ';
						}
					}
					else if (this.handleDotI)
					{
						return 'ı';
					}
					return char.ToLowerInvariant(c);
				}
				break;
			case 'ǈ':
				return 'ǉ';
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00027588 File Offset: 0x00025788
		public virtual char ToUpper(char c)
		{
			if (c < '`')
			{
				return c;
			}
			if ('a' <= c && c <= 'z' && (!this.handleDotI || c != 'i'))
			{
				return c - ' ';
			}
			if (this.ci == null || this.ci.LCID == 127)
			{
				return char.ToUpperInvariant(c);
			}
			switch (c)
			{
			case 'ϐ':
				return 'Β';
			case 'ϑ':
				return 'Θ';
			default:
				switch (c)
				{
				case 'ǅ':
					return 'Ǆ';
				default:
					if (c == 'ϰ')
					{
						return 'Κ';
					}
					if (c != 'ϱ')
					{
						if (c != 'i')
						{
							if (c == 'ı')
							{
								return 'I';
							}
							if (c == 'ǋ')
							{
								return 'Ǌ';
							}
							if (c == 'ǲ')
							{
								return 'Ǳ';
							}
							if (c == 'ΐ')
							{
								return 'Ϊ';
							}
							if (c == 'ΰ')
							{
								return 'Ϋ';
							}
						}
						else if (this.handleDotI)
						{
							return 'İ';
						}
						return char.ToUpperInvariant(c);
					}
					return 'Ρ';
				case 'ǈ':
					return 'Ǉ';
				}
				break;
			case 'ϕ':
				return 'Φ';
			case 'ϖ':
				return 'Π';
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000276F4 File Offset: 0x000258F4
		public unsafe virtual string ToLower(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(str.Length);
			fixed (string text2 = str)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text3 = text)
					{
						fixed (char* ptr2 = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							char* ptr3 = ptr2;
							char* ptr4 = ptr;
							for (int i = 0; i < str.Length; i++)
							{
								*ptr3 = this.ToLower(*ptr4);
								ptr4++;
								ptr3++;
							}
							text2 = null;
							text3 = null;
							return text;
						}
					}
				}
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00027788 File Offset: 0x00025988
		public unsafe virtual string ToUpper(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(str.Length);
			fixed (string text2 = str)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text3 = text)
					{
						fixed (char* ptr2 = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							char* ptr3 = ptr2;
							char* ptr4 = ptr;
							for (int i = 0; i < str.Length; i++)
							{
								*ptr3 = this.ToUpper(*ptr4);
								ptr4++;
								ptr3++;
							}
							text2 = null;
							text3 = null;
							return text;
						}
					}
				}
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002781C File Offset: 0x00025A1C
		[ComVisible(false)]
		public static TextInfo ReadOnly(TextInfo textInfo)
		{
			if (textInfo == null)
			{
				throw new ArgumentNullException("textInfo");
			}
			return new TextInfo(textInfo)
			{
				m_isReadOnly = true
			};
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002784C File Offset: 0x00025A4C
		[ComVisible(false)]
		public virtual object Clone()
		{
			return new TextInfo(this);
		}

		// Token: 0x04000418 RID: 1048
		private string m_listSeparator;

		// Token: 0x04000419 RID: 1049
		private bool m_isReadOnly;

		// Token: 0x0400041A RID: 1050
		private string customCultureName;

		// Token: 0x0400041B RID: 1051
		[NonSerialized]
		private int m_nDataItem;

		// Token: 0x0400041C RID: 1052
		private bool m_useUserOverride;

		// Token: 0x0400041D RID: 1053
		private int m_win32LangID;

		// Token: 0x0400041E RID: 1054
		[NonSerialized]
		private readonly CultureInfo ci;

		// Token: 0x0400041F RID: 1055
		[NonSerialized]
		private readonly bool handleDotI;

		// Token: 0x04000420 RID: 1056
		[NonSerialized]
		private readonly TextInfo.Data data;

		// Token: 0x02000103 RID: 259
		private struct Data
		{
			// Token: 0x04000421 RID: 1057
			public int ansi;

			// Token: 0x04000422 RID: 1058
			public int ebcdic;

			// Token: 0x04000423 RID: 1059
			public int mac;

			// Token: 0x04000424 RID: 1060
			public int oem;

			// Token: 0x04000425 RID: 1061
			public byte list_sep;
		}
	}
}
