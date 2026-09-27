using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	// Token: 0x02000024 RID: 36
	[ComVisible(true)]
	public static class Registry
	{
		// Token: 0x0400004E RID: 78
		public static readonly RegistryKey ClassesRoot = new RegistryKey(RegistryHive.ClassesRoot);

		// Token: 0x0400004F RID: 79
		public static readonly RegistryKey CurrentConfig = new RegistryKey(RegistryHive.CurrentConfig);

		// Token: 0x04000050 RID: 80
		public static readonly RegistryKey CurrentUser = new RegistryKey(RegistryHive.CurrentUser);

		// Token: 0x04000051 RID: 81
		public static readonly RegistryKey DynData = new RegistryKey(RegistryHive.DynData);

		// Token: 0x04000052 RID: 82
		public static readonly RegistryKey LocalMachine = new RegistryKey(RegistryHive.LocalMachine);

		// Token: 0x04000053 RID: 83
		public static readonly RegistryKey PerformanceData = new RegistryKey(RegistryHive.PerformanceData);

		// Token: 0x04000054 RID: 84
		public static readonly RegistryKey Users = new RegistryKey(RegistryHive.Users);
	}
}
