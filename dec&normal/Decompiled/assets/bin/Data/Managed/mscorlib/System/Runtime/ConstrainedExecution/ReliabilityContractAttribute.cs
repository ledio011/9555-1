using System;

namespace System.Runtime.ConstrainedExecution
{
	// Token: 0x0200020D RID: 525
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Interface, Inherited = false)]
	public sealed class ReliabilityContractAttribute : Attribute
	{
		// Token: 0x06001297 RID: 4759 RVA: 0x0004533C File Offset: 0x0004353C
		public ReliabilityContractAttribute(Consistency consistencyGuarantee, Cer cer)
		{
			this.consistency = consistencyGuarantee;
			this.cer = cer;
		}

		// Token: 0x040009D0 RID: 2512
		private Consistency consistency;

		// Token: 0x040009D1 RID: 2513
		private Cer cer;
	}
}
