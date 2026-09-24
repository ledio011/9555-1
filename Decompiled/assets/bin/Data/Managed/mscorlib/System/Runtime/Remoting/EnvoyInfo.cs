using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000288 RID: 648
	[Serializable]
	internal class EnvoyInfo : IEnvoyInfo
	{
		// Token: 0x060014DC RID: 5340 RVA: 0x00049C8C File Offset: 0x00047E8C
		public EnvoyInfo(IMessageSink sinks)
		{
			this.envoySinks = sinks;
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x00049C9C File Offset: 0x00047E9C
		public IMessageSink EnvoySinks
		{
			get
			{
				return this.envoySinks;
			}
		}

		// Token: 0x04000AC5 RID: 2757
		private IMessageSink envoySinks;
	}
}
