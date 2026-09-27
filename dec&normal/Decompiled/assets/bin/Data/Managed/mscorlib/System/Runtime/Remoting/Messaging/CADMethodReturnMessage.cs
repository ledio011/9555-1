using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200029E RID: 670
	internal class CADMethodReturnMessage : CADMessageBase
	{
		// Token: 0x06001541 RID: 5441 RVA: 0x0004B1F0 File Offset: 0x000493F0
		internal CADMethodReturnMessage(IMethodReturnMessage retMsg)
		{
			ArrayList arrayList = null;
			this._propertyCount = CADMessageBase.MarshalProperties(retMsg.Properties, ref arrayList);
			this._returnValue = base.MarshalArgument(retMsg.ReturnValue, ref arrayList);
			this._args = base.MarshalArguments(retMsg.Args, ref arrayList);
			if (retMsg.Exception != null)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._exception = new CADArgHolder(arrayList.Count);
				arrayList.Add(retMsg.Exception);
			}
			base.SaveLogicalCallContext(retMsg, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CADSerializer.SerializeObject(arrayList.ToArray());
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0004B29C File Offset: 0x0004949C
		internal static CADMethodReturnMessage Create(IMessage callMsg)
		{
			IMethodReturnMessage methodReturnMessage = callMsg as IMethodReturnMessage;
			if (methodReturnMessage == null)
			{
				return null;
			}
			return new CADMethodReturnMessage(methodReturnMessage);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0004B2C0 File Offset: 0x000494C0
		internal ArrayList GetArguments()
		{
			ArrayList result = null;
			if (this._serializedArgs != null)
			{
				object[] c = (object[])CADSerializer.DeserializeObject(new MemoryStream(this._serializedArgs));
				result = new ArrayList(c);
				this._serializedArgs = null;
			}
			return result;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0004B300 File Offset: 0x00049500
		internal object[] GetArgs(ArrayList args)
		{
			return base.UnmarshalArguments(this._args, args);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0004B310 File Offset: 0x00049510
		internal object GetReturnValue(ArrayList args)
		{
			return base.UnmarshalArgument(this._returnValue, args);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0004B320 File Offset: 0x00049520
		internal Exception GetException(ArrayList args)
		{
			if (this._exception == null)
			{
				return null;
			}
			return (Exception)args[this._exception.index];
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x0004B348 File Offset: 0x00049548
		internal int PropertiesCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x04000B01 RID: 2817
		private object _returnValue;

		// Token: 0x04000B02 RID: 2818
		private CADArgHolder _exception;
	}
}
