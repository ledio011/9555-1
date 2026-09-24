using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000313 RID: 787
	[ComVisible(true)]
	public struct SerializationEntry
	{
		// Token: 0x06001805 RID: 6149 RVA: 0x00057A44 File Offset: 0x00055C44
		internal SerializationEntry(string name, Type type, object value)
		{
			this.name = name;
			this.objectType = type;
			this.value = value;
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00057A5C File Offset: 0x00055C5C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x00057A64 File Offset: 0x00055C64
		public Type ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x00057A6C File Offset: 0x00055C6C
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000C82 RID: 3202
		private string name;

		// Token: 0x04000C83 RID: 3203
		private Type objectType;

		// Token: 0x04000C84 RID: 3204
		private object value;
	}
}
