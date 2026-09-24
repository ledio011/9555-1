using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200018D RID: 397
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum BindingFlags
	{
		// Token: 0x0400060E RID: 1550
		Default = 0,
		// Token: 0x0400060F RID: 1551
		IgnoreCase = 1,
		// Token: 0x04000610 RID: 1552
		DeclaredOnly = 2,
		// Token: 0x04000611 RID: 1553
		Instance = 4,
		// Token: 0x04000612 RID: 1554
		Static = 8,
		// Token: 0x04000613 RID: 1555
		Public = 16,
		// Token: 0x04000614 RID: 1556
		NonPublic = 32,
		// Token: 0x04000615 RID: 1557
		FlattenHierarchy = 64,
		// Token: 0x04000616 RID: 1558
		InvokeMethod = 256,
		// Token: 0x04000617 RID: 1559
		CreateInstance = 512,
		// Token: 0x04000618 RID: 1560
		GetField = 1024,
		// Token: 0x04000619 RID: 1561
		SetField = 2048,
		// Token: 0x0400061A RID: 1562
		GetProperty = 4096,
		// Token: 0x0400061B RID: 1563
		SetProperty = 8192,
		// Token: 0x0400061C RID: 1564
		PutDispProperty = 16384,
		// Token: 0x0400061D RID: 1565
		PutRefDispProperty = 32768,
		// Token: 0x0400061E RID: 1566
		ExactBinding = 65536,
		// Token: 0x0400061F RID: 1567
		SuppressChangeType = 131072,
		// Token: 0x04000620 RID: 1568
		OptionalParamBinding = 262144,
		// Token: 0x04000621 RID: 1569
		IgnoreReturn = 16777216
	}
}
