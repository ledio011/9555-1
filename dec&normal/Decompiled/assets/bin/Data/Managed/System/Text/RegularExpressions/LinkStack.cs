using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000074 RID: 116
	internal abstract class LinkStack : LinkRef
	{
		// Token: 0x0600022C RID: 556 RVA: 0x0000AA48 File Offset: 0x00008C48
		public LinkStack()
		{
			this.stack = new Stack();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000AA5C File Offset: 0x00008C5C
		public void Push()
		{
			this.stack.Push(this.GetCurrent());
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000AA70 File Offset: 0x00008C70
		public bool Pop()
		{
			if (this.stack.Count > 0)
			{
				this.SetCurrent(this.stack.Pop());
				return true;
			}
			return false;
		}

		// Token: 0x0600022F RID: 559
		protected abstract object GetCurrent();

		// Token: 0x06000230 RID: 560
		protected abstract void SetCurrent(object l);

		// Token: 0x040009AE RID: 2478
		private Stack stack;
	}
}
