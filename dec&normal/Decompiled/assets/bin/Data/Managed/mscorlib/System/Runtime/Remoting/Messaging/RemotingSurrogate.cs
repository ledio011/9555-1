using System;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002C0 RID: 704
	internal class RemotingSurrogate : ISerializationSurrogate
	{
		// Token: 0x0600162D RID: 5677 RVA: 0x0004DD90 File Offset: 0x0004BF90
		public virtual void GetObjectData(object obj, SerializationInfo si, StreamingContext sc)
		{
			if (obj == null || si == null)
			{
				throw new ArgumentNullException();
			}
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				realProxy.GetObjectData(si, sc);
			}
			else
			{
				RemotingServices.GetObjectData(obj, si, sc);
			}
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0004DDD8 File Offset: 0x0004BFD8
		public virtual object SetObjectData(object obj, SerializationInfo si, StreamingContext sc, ISurrogateSelector selector)
		{
			throw new NotSupportedException();
		}
	}
}
