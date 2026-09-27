using System;

namespace System.ComponentModel
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.All)]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x06000034 RID: 52 RVA: 0x0000269C File Offset: 0x0000089C
		public DefaultValueAttribute(bool value)
		{
			this.DefaultValue = value;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026B0 File Offset: 0x000008B0
		public DefaultValueAttribute(string value)
		{
			this.DefaultValue = value;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000026C0 File Offset: 0x000008C0
		public virtual object Value
		{
			get
			{
				return this.DefaultValue;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026C8 File Offset: 0x000008C8
		public override bool Equals(object obj)
		{
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			if (defaultValueAttribute == null)
			{
				return false;
			}
			if (this.DefaultValue == null)
			{
				return defaultValueAttribute.Value == null;
			}
			return this.DefaultValue.Equals(defaultValueAttribute.Value);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000270C File Offset: 0x0000090C
		public override int GetHashCode()
		{
			if (this.DefaultValue == null)
			{
				return base.GetHashCode();
			}
			return this.DefaultValue.GetHashCode();
		}

		// Token: 0x04000024 RID: 36
		private object DefaultValue;
	}
}
