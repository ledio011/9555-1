using System;

namespace System.ComponentModel
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultPropertyAttribute : Attribute
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002644 File Offset: 0x00000844
		public DefaultPropertyAttribute(string name)
		{
			this.property_name = name;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002664 File Offset: 0x00000864
		public string Name
		{
			get
			{
				return this.property_name;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000266C File Offset: 0x0000086C
		public override bool Equals(object o)
		{
			return o is DefaultPropertyAttribute && ((DefaultPropertyAttribute)o).Name == this.property_name;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002694 File Offset: 0x00000894
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000022 RID: 34
		private string property_name;

		// Token: 0x04000023 RID: 35
		public static readonly DefaultPropertyAttribute Default = new DefaultPropertyAttribute(null);
	}
}
