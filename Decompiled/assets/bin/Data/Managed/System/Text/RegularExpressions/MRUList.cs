using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000078 RID: 120
	internal class MRUList
	{
		// Token: 0x06000240 RID: 576 RVA: 0x0000AC6C File Offset: 0x00008E6C
		public MRUList()
		{
			this.head = (this.tail = null);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000AC90 File Offset: 0x00008E90
		public void Use(object o)
		{
			MRUList.Node node;
			if (this.head == null)
			{
				node = new MRUList.Node(o);
				this.head = (this.tail = node);
				return;
			}
			node = this.head;
			while (node != null && !o.Equals(node.value))
			{
				node = node.previous;
			}
			if (node == null)
			{
				node = new MRUList.Node(o);
			}
			else
			{
				if (node == this.head)
				{
					return;
				}
				if (node == this.tail)
				{
					this.tail = node.next;
				}
				else
				{
					node.previous.next = node.next;
				}
				node.next.previous = node.previous;
			}
			this.head.next = node;
			node.previous = this.head;
			node.next = null;
			this.head = node;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000AD70 File Offset: 0x00008F70
		public object Evict()
		{
			if (this.tail == null)
			{
				return null;
			}
			object value = this.tail.value;
			this.tail = this.tail.next;
			if (this.tail == null)
			{
				this.head = null;
			}
			else
			{
				this.tail.previous = null;
			}
			return value;
		}

		// Token: 0x040009B7 RID: 2487
		private MRUList.Node head;

		// Token: 0x040009B8 RID: 2488
		private MRUList.Node tail;

		// Token: 0x02000079 RID: 121
		private class Node
		{
			// Token: 0x06000243 RID: 579 RVA: 0x0000ADCC File Offset: 0x00008FCC
			public Node(object value)
			{
				this.value = value;
			}

			// Token: 0x040009B9 RID: 2489
			public object value;

			// Token: 0x040009BA RID: 2490
			public MRUList.Node previous;

			// Token: 0x040009BB RID: 2491
			public MRUList.Node next;
		}
	}
}
