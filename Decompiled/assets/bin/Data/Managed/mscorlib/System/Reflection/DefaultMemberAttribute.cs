using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000190 RID: 400
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	[Serializable]
	public sealed class DefaultMemberAttribute : Attribute
	{
		// Token: 0x06000ECE RID: 3790 RVA: 0x0003A6B4 File Offset: 0x000388B4
		public DefaultMemberAttribute(string memberName)
		{
			this.member_name = memberName;
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0003A6C4 File Offset: 0x000388C4
		public string MemberName
		{
			get
			{
				return this.member_name;
			}
		}

		// Token: 0x0400062A RID: 1578
		private string member_name;
	}
}
