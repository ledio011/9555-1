using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002A0 RID: 672
	[ComVisible(true)]
	[Serializable]
	public sealed class CallContext
	{
		// Token: 0x0600154B RID: 5451 RVA: 0x0004B38C File Offset: 0x0004958C
		private CallContext()
		{
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0004B394 File Offset: 0x00049594
		public static void SetData(string name, object data)
		{
			CallContext.Datastore[name] = data;
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0004B3A4 File Offset: 0x000495A4
		internal static LogicalCallContext CreateLogicalCallContext(bool createEmpty)
		{
			LogicalCallContext logicalCallContext = null;
			if (CallContext.datastore != null)
			{
				foreach (object obj in CallContext.datastore)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (dictionaryEntry.Value is ILogicalThreadAffinative)
					{
						if (logicalCallContext == null)
						{
							logicalCallContext = new LogicalCallContext();
						}
						logicalCallContext.SetData((string)dictionaryEntry.Key, dictionaryEntry.Value);
					}
				}
			}
			if (logicalCallContext == null && createEmpty)
			{
				return new LogicalCallContext();
			}
			return logicalCallContext;
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0004B454 File Offset: 0x00049654
		internal static object SetCurrentCallContext(LogicalCallContext ctx)
		{
			object result = CallContext.datastore;
			if (ctx != null && ctx.HasInfo)
			{
				CallContext.datastore = (Hashtable)ctx.Datastore.Clone();
			}
			else
			{
				CallContext.datastore = null;
			}
			return result;
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0004B49C File Offset: 0x0004969C
		internal static void UpdateCurrentCallContext(LogicalCallContext ctx)
		{
			Hashtable hashtable = ctx.Datastore;
			foreach (object obj in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				CallContext.SetData((string)dictionaryEntry.Key, dictionaryEntry.Value);
			}
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0004B514 File Offset: 0x00049714
		internal static void RestoreCallContext(object oldContext)
		{
			CallContext.datastore = (Hashtable)oldContext;
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x0004B524 File Offset: 0x00049724
		private static Hashtable Datastore
		{
			get
			{
				Hashtable hashtable = CallContext.datastore;
				if (hashtable == null)
				{
					return CallContext.datastore = new Hashtable();
				}
				return hashtable;
			}
		}

		// Token: 0x04000B05 RID: 2821
		[ThreadStatic]
		private static Header[] Headers;

		// Token: 0x04000B06 RID: 2822
		[ThreadStatic]
		private static Hashtable datastore;
	}
}
