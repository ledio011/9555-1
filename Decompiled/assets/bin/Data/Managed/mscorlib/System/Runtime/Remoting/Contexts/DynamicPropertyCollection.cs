using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000279 RID: 633
	internal class DynamicPropertyCollection
	{
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x00049640 File Offset: 0x00047840
		public bool HasProperties
		{
			get
			{
				return this._properties.Count > 0;
			}
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00049650 File Offset: 0x00047850
		public void NotifyMessage(bool start, IMessage msg, bool client_site, bool async)
		{
			ArrayList properties = this._properties;
			if (start)
			{
				foreach (object obj in properties)
				{
					DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg = (DynamicPropertyCollection.DynamicPropertyReg)obj;
					if (dynamicPropertyReg.Sink != null)
					{
						dynamicPropertyReg.Sink.ProcessMessageStart(msg, client_site, async);
					}
				}
			}
			else
			{
				foreach (object obj2 in properties)
				{
					DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg2 = (DynamicPropertyCollection.DynamicPropertyReg)obj2;
					if (dynamicPropertyReg2.Sink != null)
					{
						dynamicPropertyReg2.Sink.ProcessMessageFinish(msg, client_site, async);
					}
				}
			}
		}

		// Token: 0x04000AAF RID: 2735
		private ArrayList _properties;

		// Token: 0x0200027A RID: 634
		private class DynamicPropertyReg
		{
			// Token: 0x04000AB0 RID: 2736
			public IDynamicProperty Property;

			// Token: 0x04000AB1 RID: 2737
			public IDynamicMessageSink Sink;
		}
	}
}
