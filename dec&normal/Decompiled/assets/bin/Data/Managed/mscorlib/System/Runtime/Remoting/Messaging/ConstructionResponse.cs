using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002A7 RID: 679
	[ComVisible(true)]
	[CLSCompliant(false)]
	[Serializable]
	public class ConstructionResponse : MethodResponse, IConstructionReturnMessage, IMessage, IMethodMessage, IMethodReturnMessage
	{
		// Token: 0x0600156E RID: 5486 RVA: 0x0004BB70 File Offset: 0x00049D70
		internal ConstructionResponse(object resultObject, LogicalCallContext callCtx, IMethodCallMessage msg) : base(resultObject, null, callCtx, msg)
		{
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0004BB7C File Offset: 0x00049D7C
		internal ConstructionResponse(Exception e, IMethodCallMessage msg) : base(e, msg)
		{
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0004BB88 File Offset: 0x00049D88
		internal ConstructionResponse(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0004BB94 File Offset: 0x00049D94
		public override IDictionary Properties
		{
			get
			{
				return base.Properties;
			}
		}
	}
}
