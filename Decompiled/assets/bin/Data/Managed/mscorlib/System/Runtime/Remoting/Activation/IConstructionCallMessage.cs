using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200025B RID: 603
	[ComVisible(true)]
	public interface IConstructionCallMessage : IMessage, IMethodCallMessage, IMethodMessage
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600142A RID: 5162
		Type ActivationType { get; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600142B RID: 5163
		string ActivationTypeName { get; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600142C RID: 5164
		// (set) Token: 0x0600142D RID: 5165
		IActivator Activator { get; set; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600142E RID: 5166
		object[] CallSiteActivationAttributes { get; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600142F RID: 5167
		IList ContextProperties { get; }
	}
}
