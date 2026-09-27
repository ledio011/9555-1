using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001B7 RID: 439
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ParameterBuilder))]
	[ComVisible(true)]
	public class ParameterBuilder : _ParameterBuilder
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0004044C File Offset: 0x0003E64C
		public virtual int Attributes
		{
			get
			{
				return (int)this.attrs;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x00040454 File Offset: 0x0003E654
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x0004045C File Offset: 0x0003E65C
		public virtual int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x04000825 RID: 2085
		private MethodBase methodb;

		// Token: 0x04000826 RID: 2086
		private string name;

		// Token: 0x04000827 RID: 2087
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000828 RID: 2088
		private UnmanagedMarshal marshal_info;

		// Token: 0x04000829 RID: 2089
		private ParameterAttributes attrs;

		// Token: 0x0400082A RID: 2090
		private int position;

		// Token: 0x0400082B RID: 2091
		private int table_idx;

		// Token: 0x0400082C RID: 2092
		private object def_value;
	}
}
