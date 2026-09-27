using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000265 RID: 613
	[Serializable]
	internal class CrossAppDomainChannel : IChannel, IChannelReceiver, IChannelSender
	{
		// Token: 0x06001457 RID: 5207 RVA: 0x00047920 File Offset: 0x00045B20
		internal static void RegisterCrossAppDomainChannel()
		{
			object obj = CrossAppDomainChannel.s_lock;
			lock (obj)
			{
				CrossAppDomainChannel chnl = new CrossAppDomainChannel();
				ChannelServices.RegisterChannel(chnl);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00047964 File Offset: 0x00045B64
		public virtual string ChannelName
		{
			get
			{
				return "MONOCAD";
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0004796C File Offset: 0x00045B6C
		public virtual int ChannelPriority
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x00047970 File Offset: 0x00045B70
		public virtual object ChannelData
		{
			get
			{
				return new CrossAppDomainData(Thread.GetDomainID());
			}
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0004797C File Offset: 0x00045B7C
		public virtual void StartListening(object data)
		{
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00047980 File Offset: 0x00045B80
		public virtual IMessageSink CreateMessageSink(string url, object data, out string uri)
		{
			uri = null;
			if (data != null)
			{
				CrossAppDomainData crossAppDomainData = data as CrossAppDomainData;
				if (crossAppDomainData != null && crossAppDomainData.ProcessID == RemotingConfiguration.ProcessId)
				{
					return CrossAppDomainSink.GetSink(crossAppDomainData.DomainID);
				}
			}
			if (url != null && url.StartsWith("MONOCAD"))
			{
				throw new NotSupportedException("Can't create a named channel via crossappdomain");
			}
			return null;
		}

		// Token: 0x04000A83 RID: 2691
		private const string _strName = "MONOCAD";

		// Token: 0x04000A84 RID: 2692
		private const string _strBaseURI = "MONOCADURI";

		// Token: 0x04000A85 RID: 2693
		private static object s_lock = new object();
	}
}
