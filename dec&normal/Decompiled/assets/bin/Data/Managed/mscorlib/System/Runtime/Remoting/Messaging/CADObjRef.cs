using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200029F RID: 671
	internal class CADObjRef
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x0004B350 File Offset: 0x00049550
		public CADObjRef(ObjRef o, int sourceDomain)
		{
			this.objref = o;
			this.SourceDomain = sourceDomain;
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x0004B368 File Offset: 0x00049568
		public string TypeName
		{
			get
			{
				return this.objref.TypeInfo.TypeName;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0004B37C File Offset: 0x0004957C
		public string URI
		{
			get
			{
				return this.objref.URI;
			}
		}

		// Token: 0x04000B03 RID: 2819
		private ObjRef objref;

		// Token: 0x04000B04 RID: 2820
		public int SourceDomain;
	}
}
