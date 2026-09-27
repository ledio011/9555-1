using System;

namespace UnityEngineInternal
{
	// Token: 0x02000143 RID: 323
	[AttributeUsage(AttributeTargets.Method)]
	[Serializable]
	public class TypeInferenceRuleAttribute : Attribute
	{
		// Token: 0x06000B97 RID: 2967 RVA: 0x0001B8E0 File Offset: 0x00019AE0
		public TypeInferenceRuleAttribute(TypeInferenceRules rule) : this(rule.ToString())
		{
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0001B8F4 File Offset: 0x00019AF4
		public TypeInferenceRuleAttribute(string rule)
		{
			this._rule = rule;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0001B904 File Offset: 0x00019B04
		public override string ToString()
		{
			return this._rule;
		}

		// Token: 0x0400050D RID: 1293
		private readonly string _rule;
	}
}
