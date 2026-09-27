using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001CD RID: 461
	[ComVisible(true)]
	public class ManifestResourceInfo
	{
		// Token: 0x06001137 RID: 4407 RVA: 0x0004226C File Offset: 0x0004046C
		internal ManifestResourceInfo()
		{
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x00042274 File Offset: 0x00040474
		public virtual string FileName
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0004227C File Offset: 0x0004047C
		public virtual Assembly ReferencedAssembly
		{
			get
			{
				return this._assembly;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x00042284 File Offset: 0x00040484
		public virtual ResourceLocation ResourceLocation
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x040008B9 RID: 2233
		private Assembly _assembly;

		// Token: 0x040008BA RID: 2234
		private string _filename;

		// Token: 0x040008BB RID: 2235
		private ResourceLocation _location;
	}
}
