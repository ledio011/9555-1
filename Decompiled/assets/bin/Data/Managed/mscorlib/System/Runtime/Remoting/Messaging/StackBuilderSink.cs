using System;
using System.Reflection;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002C6 RID: 710
	internal class StackBuilderSink : IMessageSink
	{
		// Token: 0x06001654 RID: 5716 RVA: 0x0004E320 File Offset: 0x0004C520
		public StackBuilderSink(MarshalByRefObject obj, bool forceInternalExecute)
		{
			this._target = obj;
			if (!forceInternalExecute && RemotingServices.IsTransparentProxy(obj))
			{
				this._rp = RemotingServices.GetRealProxy(obj);
			}
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0004E34C File Offset: 0x0004C54C
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.CheckParameters(msg);
			if (this._rp != null)
			{
				return this._rp.Invoke(msg);
			}
			return RemotingServices.InternalExecuteMessage(this._target, (IMethodCallMessage)msg);
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0004E380 File Offset: 0x0004C580
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			object[] state = new object[]
			{
				msg,
				replySink
			};
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.ExecuteAsyncMessage), state);
			return null;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0004E3B0 File Offset: 0x0004C5B0
		private void ExecuteAsyncMessage(object ob)
		{
			object[] array = (object[])ob;
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)array[0];
			IMessageSink messageSink = (IMessageSink)array[1];
			this.CheckParameters(methodCallMessage);
			IMessage msg;
			if (this._rp != null)
			{
				msg = this._rp.Invoke(methodCallMessage);
			}
			else
			{
				msg = RemotingServices.InternalExecuteMessage(this._target, methodCallMessage);
			}
			messageSink.SyncProcessMessage(msg);
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0004E410 File Offset: 0x0004C610
		private void CheckParameters(IMessage msg)
		{
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)msg;
			ParameterInfo[] parameters = methodCallMessage.MethodBase.GetParameters();
			int num = 0;
			foreach (ParameterInfo parameterInfo in parameters)
			{
				object arg = methodCallMessage.GetArg(num++);
				Type type = parameterInfo.ParameterType;
				if (type.IsByRef)
				{
					type = type.GetElementType();
				}
				if (arg != null && !type.IsInstanceOfType(arg))
				{
					throw new RemotingException(string.Concat(new object[]
					{
						"Cannot cast argument ",
						parameterInfo.Position,
						" of type '",
						arg.GetType().AssemblyQualifiedName,
						"' to type '",
						type.AssemblyQualifiedName,
						"'"
					}));
				}
			}
		}

		// Token: 0x04000B6F RID: 2927
		private MarshalByRefObject _target;

		// Token: 0x04000B70 RID: 2928
		private RealProxy _rp;
	}
}
