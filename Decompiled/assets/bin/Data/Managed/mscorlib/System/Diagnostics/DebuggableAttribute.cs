using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020000D3 RID: 211
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module)]
	[ComVisible(true)]
	public sealed class DebuggableAttribute : Attribute
	{
		// Token: 0x0600087B RID: 2171 RVA: 0x00020DE8 File Offset: 0x0001EFE8
		public DebuggableAttribute(DebuggableAttribute.DebuggingModes modes)
		{
			this.debuggingModes = modes;
			this.JITTrackingEnabledFlag = ((this.debuggingModes & DebuggableAttribute.DebuggingModes.Default) != DebuggableAttribute.DebuggingModes.None);
			this.JITOptimizerDisabledFlag = ((this.debuggingModes & DebuggableAttribute.DebuggingModes.DisableOptimizations) != DebuggableAttribute.DebuggingModes.None);
		}

		// Token: 0x040002CE RID: 718
		private bool JITTrackingEnabledFlag;

		// Token: 0x040002CF RID: 719
		private bool JITOptimizerDisabledFlag;

		// Token: 0x040002D0 RID: 720
		private DebuggableAttribute.DebuggingModes debuggingModes;

		// Token: 0x020000D4 RID: 212
		[Flags]
		[ComVisible(true)]
		public enum DebuggingModes
		{
			// Token: 0x040002D2 RID: 722
			None = 0,
			// Token: 0x040002D3 RID: 723
			Default = 1,
			// Token: 0x040002D4 RID: 724
			IgnoreSymbolStoreSequencePoints = 2,
			// Token: 0x040002D5 RID: 725
			EnableEditAndContinue = 4,
			// Token: 0x040002D6 RID: 726
			DisableOptimizations = 256
		}
	}
}
