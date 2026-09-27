using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002C1 RID: 705
	[ComVisible(true)]
	public class RemotingSurrogateSelector : ISurrogateSelector
	{
		// Token: 0x06001631 RID: 5681 RVA: 0x0004DE10 File Offset: 0x0004C010
		public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector ssout)
		{
			if (type.IsMarshalByRef)
			{
				ssout = this;
				return RemotingSurrogateSelector._objRemotingSurrogate;
			}
			if (RemotingSurrogateSelector.s_cachedTypeObjRef.IsAssignableFrom(type))
			{
				ssout = this;
				return RemotingSurrogateSelector._objRefSurrogate;
			}
			if (this._next != null)
			{
				return this._next.GetSurrogate(type, context, out ssout);
			}
			ssout = null;
			return null;
		}

		// Token: 0x04000B58 RID: 2904
		private static Type s_cachedTypeObjRef = typeof(ObjRef);

		// Token: 0x04000B59 RID: 2905
		private static ObjRefSurrogate _objRefSurrogate = new ObjRefSurrogate();

		// Token: 0x04000B5A RID: 2906
		private static RemotingSurrogate _objRemotingSurrogate = new RemotingSurrogate();

		// Token: 0x04000B5B RID: 2907
		private object _rootObj;

		// Token: 0x04000B5C RID: 2908
		private MessageSurrogateFilter _filter;

		// Token: 0x04000B5D RID: 2909
		private ISurrogateSelector _next;
	}
}
