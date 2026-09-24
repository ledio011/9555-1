using System;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000057 RID: 87
	[ComVisible(true)]
	public class AppDomainManager : MarshalByRefObject
	{
		// Token: 0x04000164 RID: 356
		private ApplicationActivator _activator;

		// Token: 0x04000165 RID: 357
		private AppDomainManagerInitializationOptions _flags;
	}
}
