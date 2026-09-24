using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	// Token: 0x0200028F RID: 655
	[ComVisible(true)]
	public interface IRemotingTypeInfo
	{
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060014F1 RID: 5361
		// (set) Token: 0x060014F2 RID: 5362
		string TypeName { get; set; }

		// Token: 0x060014F3 RID: 5363
		bool CanCastTo(Type fromType, object o);
	}
}
