using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x0200028B RID: 651
	internal abstract class Identity
	{
		// Token: 0x060014E0 RID: 5344 RVA: 0x00049CAC File Offset: 0x00047EAC
		public Identity(string objectUri)
		{
			this._objectUri = objectUri;
		}

		// Token: 0x060014E1 RID: 5345
		public abstract ObjRef CreateObjRef(Type requestedType);

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00049CBC File Offset: 0x00047EBC
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x00049CC4 File Offset: 0x00047EC4
		public IMessageSink ChannelSink
		{
			get
			{
				return this._channelSink;
			}
			set
			{
				this._channelSink = value;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00049CD0 File Offset: 0x00047ED0
		public IMessageSink EnvoySink
		{
			get
			{
				return this._envoySink;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00049CD8 File Offset: 0x00047ED8
		// (set) Token: 0x060014E6 RID: 5350 RVA: 0x00049CE0 File Offset: 0x00047EE0
		public string ObjectUri
		{
			get
			{
				return this._objectUri;
			}
			set
			{
				this._objectUri = value;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00049CEC File Offset: 0x00047EEC
		public bool IsConnected
		{
			get
			{
				return this._objectUri != null;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00049CFC File Offset: 0x00047EFC
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x00049D04 File Offset: 0x00047F04
		public bool Disposed
		{
			get
			{
				return this._disposed;
			}
			set
			{
				this._disposed = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00049D10 File Offset: 0x00047F10
		public bool HasServerDynamicSinks
		{
			get
			{
				return this._serverDynamicProperties != null && this._serverDynamicProperties.HasProperties;
			}
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00049D2C File Offset: 0x00047F2C
		public void NotifyClientDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this._clientDynamicProperties != null && this._clientDynamicProperties.HasProperties)
			{
				this._clientDynamicProperties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00049D5C File Offset: 0x00047F5C
		public void NotifyServerDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this._serverDynamicProperties != null && this._serverDynamicProperties.HasProperties)
			{
				this._serverDynamicProperties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x04000AC6 RID: 2758
		protected string _objectUri;

		// Token: 0x04000AC7 RID: 2759
		protected IMessageSink _channelSink;

		// Token: 0x04000AC8 RID: 2760
		protected IMessageSink _envoySink;

		// Token: 0x04000AC9 RID: 2761
		private DynamicPropertyCollection _clientDynamicProperties;

		// Token: 0x04000ACA RID: 2762
		private DynamicPropertyCollection _serverDynamicProperties;

		// Token: 0x04000ACB RID: 2763
		protected ObjRef _objRef;

		// Token: 0x04000ACC RID: 2764
		private bool _disposed;
	}
}
