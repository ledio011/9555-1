using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002A1 RID: 673
	[Serializable]
	internal class CallContextRemotingData : ICloneable
	{
		// Token: 0x06001553 RID: 5459 RVA: 0x0004B554 File Offset: 0x00049754
		public object Clone()
		{
			return new CallContextRemotingData
			{
				_logicalCallID = this._logicalCallID
			};
		}

		// Token: 0x04000B07 RID: 2823
		private string _logicalCallID;
	}
}
