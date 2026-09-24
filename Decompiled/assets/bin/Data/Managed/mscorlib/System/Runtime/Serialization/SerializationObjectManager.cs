using System;
using System.Collections;

namespace System.Runtime.Serialization
{
	// Token: 0x02000317 RID: 791
	public sealed class SerializationObjectManager
	{
		// Token: 0x06001829 RID: 6185 RVA: 0x00057F1C File Offset: 0x0005611C
		public SerializationObjectManager(StreamingContext context)
		{
			this.context = context;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00057F38 File Offset: 0x00056138
		public void RegisterObject(object obj)
		{
			if (this.seen.Contains(obj))
			{
				return;
			}
			SerializationCallbacks sc = SerializationCallbacks.GetSerializationCallbacks(obj.GetType());
			this.seen[obj] = 1;
			sc.RaiseOnSerializing(obj, this.context);
			if (sc.HasSerializedCallbacks)
			{
				this.callbacks = (SerializationCallbacks.CallbackHandler)Delegate.Combine(this.callbacks, new SerializationCallbacks.CallbackHandler(delegate(StreamingContext ctx)
				{
					sc.RaiseOnSerialized(obj, ctx);
				}));
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00057FE0 File Offset: 0x000561E0
		public void RaiseOnSerializedEvent()
		{
			if (this.callbacks != null)
			{
				this.callbacks(this.context);
			}
		}

		// Token: 0x04000C8B RID: 3211
		private readonly StreamingContext context;

		// Token: 0x04000C8C RID: 3212
		private readonly Hashtable seen = new Hashtable();

		// Token: 0x04000C8D RID: 3213
		private SerializationCallbacks.CallbackHandler callbacks;
	}
}
