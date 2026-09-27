using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200024D RID: 589
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	public sealed class TypeLibImportClassAttribute : Attribute
	{
		// Token: 0x0600140D RID: 5133 RVA: 0x000464F4 File Offset: 0x000446F4
		public TypeLibImportClassAttribute(Type importClass)
		{
			this._importClass = importClass.ToString();
		}

		// Token: 0x04000A16 RID: 2582
		private string _importClass;
	}
}
