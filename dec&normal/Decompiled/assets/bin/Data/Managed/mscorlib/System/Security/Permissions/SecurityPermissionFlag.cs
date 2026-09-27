using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200034C RID: 844
	[ComVisible(true)]
	[Obsolete("CAS support is not available with Silverlight applications.")]
	[Flags]
	[Serializable]
	public enum SecurityPermissionFlag
	{
		// Token: 0x04000DBB RID: 3515
		NoFlags = 0,
		// Token: 0x04000DBC RID: 3516
		Assertion = 1,
		// Token: 0x04000DBD RID: 3517
		UnmanagedCode = 2,
		// Token: 0x04000DBE RID: 3518
		SkipVerification = 4,
		// Token: 0x04000DBF RID: 3519
		Execution = 8,
		// Token: 0x04000DC0 RID: 3520
		ControlThread = 16,
		// Token: 0x04000DC1 RID: 3521
		ControlEvidence = 32,
		// Token: 0x04000DC2 RID: 3522
		ControlPolicy = 64,
		// Token: 0x04000DC3 RID: 3523
		SerializationFormatter = 128,
		// Token: 0x04000DC4 RID: 3524
		ControlDomainPolicy = 256,
		// Token: 0x04000DC5 RID: 3525
		ControlPrincipal = 512,
		// Token: 0x04000DC6 RID: 3526
		ControlAppDomain = 1024,
		// Token: 0x04000DC7 RID: 3527
		RemotingConfiguration = 2048,
		// Token: 0x04000DC8 RID: 3528
		Infrastructure = 4096,
		// Token: 0x04000DC9 RID: 3529
		BindingRedirects = 8192,
		// Token: 0x04000DCA RID: 3530
		AllFlags = 16383
	}
}
