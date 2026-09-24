using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000068 RID: 104
	[ComVisible(true)]
	[Serializable]
	public class ArrayTypeMismatchException : SystemException
	{
		// Token: 0x06000337 RID: 823 RVA: 0x00011A5C File Offset: 0x0000FC5C
		public ArrayTypeMismatchException() : base(Locale.GetText("Source array type cannot be assigned to destination array type."))
		{
			base.HResult = -2146233085;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00011A7C File Offset: 0x0000FC7C
		public ArrayTypeMismatchException(string message) : base(message)
		{
			base.HResult = -2146233085;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00011A90 File Offset: 0x0000FC90
		protected ArrayTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000199 RID: 409
		private const int Result = -2146233085;
	}
}
