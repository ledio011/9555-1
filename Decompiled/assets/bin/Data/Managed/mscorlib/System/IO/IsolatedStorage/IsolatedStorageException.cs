using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO.IsolatedStorage
{
	// Token: 0x0200012C RID: 300
	[ComVisible(true)]
	[Serializable]
	public class IsolatedStorageException : Exception
	{
		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002D8A0 File Offset: 0x0002BAA0
		public IsolatedStorageException() : base(Locale.GetText("An Isolated storage operation failed."))
		{
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002D8B4 File Offset: 0x0002BAB4
		public IsolatedStorageException(string message) : base(message)
		{
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002D8C0 File Offset: 0x0002BAC0
		protected IsolatedStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
