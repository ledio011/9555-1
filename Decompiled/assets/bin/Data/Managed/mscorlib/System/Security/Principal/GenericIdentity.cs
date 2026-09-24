using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x02000371 RID: 881
	[ComVisible(true)]
	[Serializable]
	public class GenericIdentity : IIdentity
	{
		// Token: 0x06001A11 RID: 6673 RVA: 0x00060B90 File Offset: 0x0005ED90
		public GenericIdentity(string name, string type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.m_name = name;
			this.m_type = type;
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x00060BC8 File Offset: 0x0005EDC8
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x04000E45 RID: 3653
		private string m_name;

		// Token: 0x04000E46 RID: 3654
		private string m_type;
	}
}
