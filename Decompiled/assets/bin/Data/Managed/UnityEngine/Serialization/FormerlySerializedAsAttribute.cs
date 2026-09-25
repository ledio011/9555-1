using System;

namespace UnityEngine.Serialization
{
	// Token: 0x020000FC RID: 252
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	public class FormerlySerializedAsAttribute : Attribute
	{
		// Token: 0x06000984 RID: 2436 RVA: 0x000153BC File Offset: 0x000135BC
		public FormerlySerializedAsAttribute(string oldName)
		{
			this.m_oldName = oldName;
		}

		// Token: 0x040003B7 RID: 951
		private string m_oldName;
	}
}
