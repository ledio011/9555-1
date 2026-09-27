using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x020002DC RID: 732
	internal class SingleCallIdentity : ServerIdentity
	{
		// Token: 0x06001706 RID: 5894 RVA: 0x00050F74 File Offset: 0x0004F174
		public SingleCallIdentity(string objectUri, Context context, Type objectType) : base(objectUri, context, objectType)
		{
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00050F80 File Offset: 0x0004F180
		public override IMessage SyncObjectProcessMessage(IMessage msg)
		{
			MarshalByRefObject marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(this._objectType, true);
			if (marshalByRefObject.ObjectIdentity == null)
			{
				marshalByRefObject.ObjectIdentity = this;
			}
			IMessageSink messageSink = this._context.CreateServerObjectSinkChain(marshalByRefObject, false);
			IMessage result = messageSink.SyncProcessMessage(msg);
			if (marshalByRefObject is IDisposable)
			{
				((IDisposable)marshalByRefObject).Dispose();
			}
			return result;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00050FE0 File Offset: 0x0004F1E0
		public override IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink)
		{
			MarshalByRefObject marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(this._objectType, true);
			IMessageSink messageSink = this._context.CreateServerObjectSinkChain(marshalByRefObject, false);
			if (marshalByRefObject is IDisposable)
			{
				replySink = new DisposerReplySink(replySink, (IDisposable)marshalByRefObject);
			}
			return messageSink.AsyncProcessMessage(msg, replySink);
		}
	}
}
