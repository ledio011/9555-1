using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002B5 RID: 693
	[ComVisible(true)]
	[Serializable]
	public sealed class LogicalCallContext : ICloneable, ISerializable
	{
		// Token: 0x0600159B RID: 5531 RVA: 0x0004BC18 File Offset: 0x00049E18
		internal LogicalCallContext()
		{
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0004BC2C File Offset: 0x00049E2C
		internal LogicalCallContext(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "__RemotingData")
				{
					this._remotingData = (CallContextRemotingData)serializationEntry.Value;
				}
				else
				{
					this.SetData(serializationEntry.Name, serializationEntry.Value);
				}
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x0004BCAC File Offset: 0x00049EAC
		public bool HasInfo
		{
			get
			{
				return this._data != null && this._data.Count > 0;
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0004BCCC File Offset: 0x00049ECC
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("__RemotingData", this._remotingData);
			if (this._data != null)
			{
				foreach (object obj in this._data)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					info.AddValue((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0004BD60 File Offset: 0x00049F60
		public void SetData(string name, object data)
		{
			if (this._data == null)
			{
				this._data = new Hashtable();
			}
			this._data[name] = data;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0004BD88 File Offset: 0x00049F88
		public object Clone()
		{
			LogicalCallContext logicalCallContext = new LogicalCallContext();
			logicalCallContext._remotingData = (CallContextRemotingData)this._remotingData.Clone();
			if (this._data != null)
			{
				logicalCallContext._data = new Hashtable();
				foreach (object obj in this._data)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					logicalCallContext._data[dictionaryEntry.Key] = dictionaryEntry.Value;
				}
			}
			return logicalCallContext;
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x0004BE30 File Offset: 0x0004A030
		internal Hashtable Datastore
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x04000B20 RID: 2848
		private Hashtable _data;

		// Token: 0x04000B21 RID: 2849
		private CallContextRemotingData _remotingData = new CallContextRemotingData();
	}
}
