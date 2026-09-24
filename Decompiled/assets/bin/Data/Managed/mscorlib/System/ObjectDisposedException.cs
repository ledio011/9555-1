using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200016D RID: 365
	[ComVisible(true)]
	[Serializable]
	public class ObjectDisposedException : InvalidOperationException
	{
		// Token: 0x06000DFA RID: 3578 RVA: 0x00037C78 File Offset: 0x00035E78
		public ObjectDisposedException(string objectName) : base(Locale.GetText("The object was used after being disposed."))
		{
			this.obj_name = objectName;
			this.msg = Locale.GetText("The object was used after being disposed.");
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00037CA4 File Offset: 0x00035EA4
		public ObjectDisposedException(string objectName, string message) : base(message)
		{
			this.obj_name = objectName;
			this.msg = message;
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00037CBC File Offset: 0x00035EBC
		protected ObjectDisposedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.obj_name = info.GetString("ObjectName");
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00037CD8 File Offset: 0x00035ED8
		public override string Message
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00037CE0 File Offset: 0x00035EE0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ObjectName", this.obj_name);
		}

		// Token: 0x040005C5 RID: 1477
		private string obj_name;

		// Token: 0x040005C6 RID: 1478
		private string msg;
	}
}
