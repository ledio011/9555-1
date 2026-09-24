using System;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002BE RID: 702
	internal class ObjRefSurrogate : ISerializationSurrogate
	{
		// Token: 0x0600162A RID: 5674 RVA: 0x0004DD4C File Offset: 0x0004BF4C
		public virtual void GetObjectData(object obj, SerializationInfo si, StreamingContext sc)
		{
			if (obj == null || si == null)
			{
				throw new ArgumentNullException();
			}
			((ObjRef)obj).GetObjectData(si, sc);
			si.AddValue("fIsMarshalled", 0);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x0004DD7C File Offset: 0x0004BF7C
		public virtual object SetObjectData(object obj, SerializationInfo si, StreamingContext sc, ISurrogateSelector selector)
		{
			throw new NotSupportedException("Do not use RemotingSurrogateSelector when deserializating");
		}
	}
}
