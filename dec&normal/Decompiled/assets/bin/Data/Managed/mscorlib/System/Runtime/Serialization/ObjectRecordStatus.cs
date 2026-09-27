using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200030B RID: 779
	internal enum ObjectRecordStatus : byte
	{
		// Token: 0x04000C77 RID: 3191
		Unregistered,
		// Token: 0x04000C78 RID: 3192
		ReferenceUnsolved,
		// Token: 0x04000C79 RID: 3193
		ReferenceSolvingDelayed,
		// Token: 0x04000C7A RID: 3194
		ReferenceSolved
	}
}
