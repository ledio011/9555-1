using System;
using System.Collections;

namespace System.Runtime.Remoting
{
	// Token: 0x02000260 RID: 608
	internal class ChannelData
	{
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00046CF8 File Offset: 0x00044EF8
		internal ArrayList ServerProviders
		{
			get
			{
				if (this._serverProviders == null)
				{
					this._serverProviders = new ArrayList();
				}
				return this._serverProviders;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00046D18 File Offset: 0x00044F18
		public ArrayList ClientProviders
		{
			get
			{
				if (this._clientProviders == null)
				{
					this._clientProviders = new ArrayList();
				}
				return this._clientProviders;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00046D38 File Offset: 0x00044F38
		public Hashtable CustomProperties
		{
			get
			{
				if (this._customProperties == null)
				{
					this._customProperties = new Hashtable();
				}
				return this._customProperties;
			}
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00046D58 File Offset: 0x00044F58
		public void CopyFrom(ChannelData other)
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
			if (this.DelayLoadAsClientChannel == null)
			{
				this.DelayLoadAsClientChannel = other.DelayLoadAsClientChannel;
			}
			if (other._customProperties != null)
			{
				foreach (object obj in other._customProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (!this.CustomProperties.ContainsKey(dictionaryEntry.Key))
					{
						this.CustomProperties[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
			}
			if (this._serverProviders == null && other._serverProviders != null)
			{
				foreach (object obj2 in other._serverProviders)
				{
					ProviderData other2 = (ProviderData)obj2;
					ProviderData providerData = new ProviderData();
					providerData.CopyFrom(other2);
					this.ServerProviders.Add(providerData);
				}
			}
			if (this._clientProviders == null && other._clientProviders != null)
			{
				foreach (object obj3 in other._clientProviders)
				{
					ProviderData other3 = (ProviderData)obj3;
					ProviderData providerData2 = new ProviderData();
					providerData2.CopyFrom(other3);
					this.ClientProviders.Add(providerData2);
				}
			}
		}

		// Token: 0x04000A74 RID: 2676
		internal string Ref;

		// Token: 0x04000A75 RID: 2677
		internal string Type;

		// Token: 0x04000A76 RID: 2678
		internal string Id;

		// Token: 0x04000A77 RID: 2679
		internal string DelayLoadAsClientChannel;

		// Token: 0x04000A78 RID: 2680
		private ArrayList _serverProviders = new ArrayList();

		// Token: 0x04000A79 RID: 2681
		private ArrayList _clientProviders = new ArrayList();

		// Token: 0x04000A7A RID: 2682
		private Hashtable _customProperties = new Hashtable();
	}
}
