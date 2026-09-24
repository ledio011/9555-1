using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting
{
	// Token: 0x020002D0 RID: 720
	internal class ProviderData
	{
		// Token: 0x06001684 RID: 5764 RVA: 0x0004EA98 File Offset: 0x0004CC98
		public void CopyFrom(ProviderData other)
		{
			if (this.Ref == null)
			{
				this.Ref = other.Ref;
			}
			if (this.Id == null)
			{
				this.Id = other.Id;
			}
			if (this.Type == null)
			{
				this.Type = other.Type;
			}
			foreach (object obj in other.CustomProperties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!this.CustomProperties.ContainsKey(dictionaryEntry.Key))
				{
					this.CustomProperties[dictionaryEntry.Key] = dictionaryEntry.Value;
				}
			}
			if (other.CustomData != null)
			{
				if (this.CustomData == null)
				{
					this.CustomData = new ArrayList();
				}
				foreach (object obj2 in other.CustomData)
				{
					SinkProviderData value = (SinkProviderData)obj2;
					this.CustomData.Add(value);
				}
			}
		}

		// Token: 0x04000B9C RID: 2972
		internal string Ref;

		// Token: 0x04000B9D RID: 2973
		internal string Type;

		// Token: 0x04000B9E RID: 2974
		internal string Id;

		// Token: 0x04000B9F RID: 2975
		internal Hashtable CustomProperties = new Hashtable();

		// Token: 0x04000BA0 RID: 2976
		internal IList CustomData;
	}
}
