using System;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting
{
	// Token: 0x02000261 RID: 609
	[Serializable]
	internal class ChannelInfo : IChannelInfo
	{
		// Token: 0x06001440 RID: 5184 RVA: 0x00046F58 File Offset: 0x00045158
		public ChannelInfo()
		{
			this.channelData = ChannelServices.GetCurrentChannelInfo();
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00046F6C File Offset: 0x0004516C
		public ChannelInfo(object remoteChannelData)
		{
			this.channelData = new object[]
			{
				remoteChannelData
			};
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00046F84 File Offset: 0x00045184
		public object[] ChannelData
		{
			get
			{
				return this.channelData;
			}
		}

		// Token: 0x04000A7B RID: 2683
		private object[] channelData;
	}
}
