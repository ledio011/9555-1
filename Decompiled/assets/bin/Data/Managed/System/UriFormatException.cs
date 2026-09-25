using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200009B RID: 155
	[Serializable]
	public class UriFormatException : FormatException, ISerializable
	{
		// Token: 0x06000364 RID: 868 RVA: 0x000119C0 File Offset: 0x0000FBC0
		public UriFormatException() : base(Locale.GetText("Invalid URI format"))
		{
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000119D4 File Offset: 0x0000FBD4
		public UriFormatException(string message) : base(message)
		{
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000119E0 File Offset: 0x0000FBE0
		protected UriFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000119EC File Offset: 0x0000FBEC
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
