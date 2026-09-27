using System;

namespace System
{
	// Token: 0x02000156 RID: 342
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000D12 RID: 3346 RVA: 0x00032A4C File Offset: 0x00030C4C
		public MonoDocumentationNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
