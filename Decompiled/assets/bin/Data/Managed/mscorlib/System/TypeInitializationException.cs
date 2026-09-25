using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020003D0 RID: 976
	[ComVisible(true)]
	[Serializable]
	public sealed class TypeInitializationException : SystemException
	{
		// Token: 0x06001E0B RID: 7691 RVA: 0x00070760 File Offset: 0x0006E960
		public TypeInitializationException(string fullTypeName, Exception innerException) : base(Locale.GetText("An exception was thrown by the type initializer for ") + fullTypeName, innerException)
		{
			this.type_name = fullTypeName;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x00070780 File Offset: 0x0006E980
		internal TypeInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type_name = info.GetString("TypeName");
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x0007079C File Offset: 0x0006E99C
		public string TypeName
		{
			get
			{
				return this.type_name;
			}
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x000707A4 File Offset: 0x0006E9A4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("TypeName", this.type_name);
		}

		// Token: 0x04000FA1 RID: 4001
		private string type_name;
	}
}
