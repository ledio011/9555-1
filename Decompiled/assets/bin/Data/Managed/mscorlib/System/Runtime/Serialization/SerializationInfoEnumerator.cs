using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000316 RID: 790
	[ComVisible(true)]
	public sealed class SerializationInfoEnumerator : IEnumerator
	{
		// Token: 0x06001821 RID: 6177 RVA: 0x00057E70 File Offset: 0x00056070
		internal SerializationInfoEnumerator(ArrayList list)
		{
			this.enumerator = list.GetEnumerator();
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x00057E84 File Offset: 0x00056084
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x00057E94 File Offset: 0x00056094
		public SerializationEntry Current
		{
			get
			{
				return (SerializationEntry)this.enumerator.Current;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x00057EA8 File Offset: 0x000560A8
		public string Name
		{
			get
			{
				SerializationEntry serializationEntry = this.Current;
				return serializationEntry.Name;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x00057EC4 File Offset: 0x000560C4
		public Type ObjectType
		{
			get
			{
				SerializationEntry serializationEntry = this.Current;
				return serializationEntry.ObjectType;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x00057EE0 File Offset: 0x000560E0
		public object Value
		{
			get
			{
				SerializationEntry serializationEntry = this.Current;
				return serializationEntry.Value;
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00057EFC File Offset: 0x000560FC
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00057F0C File Offset: 0x0005610C
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x04000C8A RID: 3210
		private IEnumerator enumerator;
	}
}
