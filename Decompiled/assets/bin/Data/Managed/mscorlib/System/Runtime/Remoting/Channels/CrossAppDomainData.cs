using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000266 RID: 614
	[Serializable]
	internal class CrossAppDomainData
	{
		// Token: 0x0600145D RID: 5213 RVA: 0x000479E8 File Offset: 0x00045BE8
		internal CrossAppDomainData(int domainId)
		{
			this._ContextID = 0;
			this._DomainID = domainId;
			this._processGuid = RemotingConfiguration.ProcessId;
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x00047A10 File Offset: 0x00045C10
		internal int DomainID
		{
			get
			{
				return this._DomainID;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x00047A18 File Offset: 0x00045C18
		internal string ProcessID
		{
			get
			{
				return this._processGuid;
			}
		}

		// Token: 0x04000A86 RID: 2694
		private object _ContextID;

		// Token: 0x04000A87 RID: 2695
		private int _DomainID;

		// Token: 0x04000A88 RID: 2696
		private string _processGuid;
	}
}
