using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200005D RID: 93
	[StructLayout(3)]
	public struct ArgIterator
	{
		// Token: 0x0600025C RID: 604 RVA: 0x0000E970 File Offset: 0x0000CB70
		public ArgIterator(RuntimeArgumentHandle arglist)
		{
			this.sig = IntPtr.Zero;
			this.args = IntPtr.Zero;
			this.next_arg = (this.num_args = 0);
			this.Setup(arglist.args, IntPtr.Zero);
		}

		// Token: 0x0600025D RID: 605
		[MethodImpl(4096)]
		private extern void Setup(IntPtr argsp, IntPtr start);

		// Token: 0x0600025E RID: 606 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
		public override bool Equals(object o)
		{
			throw new NotSupportedException(Locale.GetText("ArgIterator does not support Equals."));
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000E9CC File Offset: 0x0000CBCC
		public override int GetHashCode()
		{
			return this.sig.GetHashCode();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000E9DC File Offset: 0x0000CBDC
		[CLSCompliant(false)]
		public TypedReference GetNextArg()
		{
			if (this.num_args == this.next_arg)
			{
				throw new InvalidOperationException(Locale.GetText("Invalid iterator position."));
			}
			return this.IntGetNextArg();
		}

		// Token: 0x06000261 RID: 609
		[MethodImpl(4096)]
		private extern TypedReference IntGetNextArg();

		// Token: 0x06000262 RID: 610 RVA: 0x0000EA08 File Offset: 0x0000CC08
		public int GetRemainingCount()
		{
			return this.num_args - this.next_arg;
		}

		// Token: 0x04000183 RID: 387
		private IntPtr sig;

		// Token: 0x04000184 RID: 388
		private IntPtr args;

		// Token: 0x04000185 RID: 389
		private int next_arg;

		// Token: 0x04000186 RID: 390
		private int num_args;
	}
}
