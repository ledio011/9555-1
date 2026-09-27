using System;
using System.Threading;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000258 RID: 600
	[Serializable]
	internal class ConstructionLevelActivator : IActivator
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00046A5C File Offset: 0x00044C5C
		public IActivator NextActivator
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00046A60 File Offset: 0x00044C60
		public IConstructionReturnMessage Activate(IConstructionCallMessage msg)
		{
			return (IConstructionReturnMessage)Thread.CurrentContext.GetServerContextSinkChain().SyncProcessMessage(msg);
		}
	}
}
