using System;

namespace UnityEngine.Internal
{
	// Token: 0x02000096 RID: 150
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.GenericParameter)]
	[Serializable]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x000112E0 File Offset: 0x0000F4E0
		public DefaultValueAttribute(string value)
		{
			this.DefaultValue = value;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x000112F0 File Offset: 0x0000F4F0
		public object Value
		{
			get
			{
				return this.DefaultValue;
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000112F8 File Offset: 0x0000F4F8
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

		// Token: 0x06000700 RID: 1792 RVA: 0x0001133C File Offset: 0x0000F53C
		public override int GetHashCode()
		{
			if (this.DefaultValue == null)
			{
				return base.GetHashCode();
			}
			return this.DefaultValue.GetHashCode();
		}

		// Token: 0x040001B6 RID: 438
		private object DefaultValue;
	}
}
