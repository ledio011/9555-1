using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Collections
{
	// Token: 0x02000081 RID: 129
	[ComVisible(true)]
	[Serializable]
	public sealed class Comparer : IComparer, ISerializable
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x00014630 File Offset: 0x00012830
		private Comparer()
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00014638 File Offset: 0x00012838
		public Comparer(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this.m_compareInfo = culture.CompareInfo;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001467C File Offset: 0x0001287C
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			if (this.m_compareInfo != null)
			{
				string text = a as string;
				string text2 = b as string;
				if (text != null && text2 != null)
				{
					return this.m_compareInfo.Compare(text, text2);
				}
			}
			if (a is IComparable)
			{
				return (a as IComparable).CompareTo(b);
			}
			if (b is IComparable)
			{
				return -(b as IComparable).CompareTo(a);
			}
			throw new ArgumentException(Locale.GetText("Neither 'a' nor 'b' implements IComparable."));
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00014718 File Offset: 0x00012918
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("CompareInfo", this.m_compareInfo, typeof(CompareInfo));
		}

		// Token: 0x040001E3 RID: 483
		public static readonly Comparer Default = new Comparer();

		// Token: 0x040001E4 RID: 484
		public static readonly Comparer DefaultInvariant = new Comparer(CultureInfo.InvariantCulture);

		// Token: 0x040001E5 RID: 485
		private CompareInfo m_compareInfo;
	}
}
