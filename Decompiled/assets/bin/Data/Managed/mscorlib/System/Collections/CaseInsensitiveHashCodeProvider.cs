using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200007E RID: 126
	[ComVisible(true)]
	[Obsolete("Please use StringComparer instead.")]
	[Serializable]
	public class CaseInsensitiveHashCodeProvider : IHashCodeProvider
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x000140F8 File Offset: 0x000122F8
		public CaseInsensitiveHashCodeProvider()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			if (!CaseInsensitiveHashCodeProvider.AreEqual(currentCulture, CultureInfo.InvariantCulture))
			{
				this.m_text = CultureInfo.CurrentCulture.TextInfo;
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00014134 File Offset: 0x00012334
		public CaseInsensitiveHashCodeProvider(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (!CaseInsensitiveHashCodeProvider.AreEqual(culture, CultureInfo.InvariantCulture))
			{
				this.m_text = culture.TextInfo;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00014188 File Offset: 0x00012388
		public static CaseInsensitiveHashCodeProvider Default
		{
			get
			{
				object obj = CaseInsensitiveHashCodeProvider.sync;
				CaseInsensitiveHashCodeProvider result;
				lock (obj)
				{
					if (CaseInsensitiveHashCodeProvider.singleton == null)
					{
						CaseInsensitiveHashCodeProvider.singleton = new CaseInsensitiveHashCodeProvider();
					}
					else if (CaseInsensitiveHashCodeProvider.singleton.m_text == null)
					{
						if (!CaseInsensitiveHashCodeProvider.AreEqual(CultureInfo.CurrentCulture, CultureInfo.InvariantCulture))
						{
							CaseInsensitiveHashCodeProvider.singleton = new CaseInsensitiveHashCodeProvider();
						}
					}
					else if (!CaseInsensitiveHashCodeProvider.AreEqual(CaseInsensitiveHashCodeProvider.singleton.m_text, CultureInfo.CurrentCulture))
					{
						CaseInsensitiveHashCodeProvider.singleton = new CaseInsensitiveHashCodeProvider();
					}
					result = CaseInsensitiveHashCodeProvider.singleton;
				}
				return result;
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00014238 File Offset: 0x00012438
		private static bool AreEqual(CultureInfo a, CultureInfo b)
		{
			return a.Name == b.Name;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001424C File Offset: 0x0001244C
		private static bool AreEqual(TextInfo info, CultureInfo culture)
		{
			return info.CultureName == culture.Name;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00014260 File Offset: 0x00012460
		public static CaseInsensitiveHashCodeProvider DefaultInvariant
		{
			get
			{
				return CaseInsensitiveHashCodeProvider.singletonInvariant;
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00014268 File Offset: 0x00012468
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text == null)
			{
				return obj.GetHashCode();
			}
			int num = 0;
			if (this.m_text != null && !CaseInsensitiveHashCodeProvider.AreEqual(this.m_text, CultureInfo.InvariantCulture))
			{
				foreach (char c in this.m_text.ToLower(text))
				{
					num = num * 31 + (int)c;
				}
			}
			else
			{
				for (int j = 0; j < text.Length; j++)
				{
					char c = char.ToLower(text[j], CultureInfo.InvariantCulture);
					num = num * 31 + (int)c;
				}
			}
			return num;
		}

		// Token: 0x040001DD RID: 477
		private static readonly CaseInsensitiveHashCodeProvider singletonInvariant = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);

		// Token: 0x040001DE RID: 478
		private static CaseInsensitiveHashCodeProvider singleton;

		// Token: 0x040001DF RID: 479
		private static readonly object sync = new object();

		// Token: 0x040001E0 RID: 480
		private TextInfo m_text;
	}
}
