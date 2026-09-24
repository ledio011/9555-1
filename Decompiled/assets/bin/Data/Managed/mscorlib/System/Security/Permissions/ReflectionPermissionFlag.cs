using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000347 RID: 839
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum ReflectionPermissionFlag
	{
		// Token: 0x04000D9D RID: 3485
		NoFlags = 0,
		// Token: 0x04000D9E RID: 3486
		[Obsolete("not used anymore")]
		TypeInformation = 1,
		// Token: 0x04000D9F RID: 3487
		MemberAccess = 2,
		// Token: 0x04000DA0 RID: 3488
		ReflectionEmit = 4,
		// Token: 0x04000DA1 RID: 3489
		AllFlags = 7,
		// Token: 0x04000DA2 RID: 3490
		[ComVisible(false)]
		RestrictedMemberAccess = 8
	}
}
