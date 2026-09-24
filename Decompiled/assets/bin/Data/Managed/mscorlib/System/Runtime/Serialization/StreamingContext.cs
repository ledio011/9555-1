using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000319 RID: 793
	[ComVisible(true)]
	[Serializable]
	public struct StreamingContext
	{
		// Token: 0x0600182E RID: 6190 RVA: 0x0005801C File Offset: 0x0005621C
		public StreamingContext(StreamingContextStates state)
		{
			this.state = state;
			this.additional = null;
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0005802C File Offset: 0x0005622C
		public StreamingContext(StreamingContextStates state, object additional)
		{
			this.state = state;
			this.additional = additional;
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x0005803C File Offset: 0x0005623C
		public object Context
		{
			get
			{
				return this.additional;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x00058044 File Offset: 0x00056244
		public StreamingContextStates State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0005804C File Offset: 0x0005624C
		public override bool Equals(object obj)
		{
			if (!(obj is StreamingContext))
			{
				return false;
			}
			StreamingContext streamingContext = (StreamingContext)obj;
			return streamingContext.state == this.state && streamingContext.additional == this.additional;
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00058094 File Offset: 0x00056294
		public override int GetHashCode()
		{
			return (int)this.state;
		}

		// Token: 0x04000C90 RID: 3216
		private StreamingContextStates state;

		// Token: 0x04000C91 RID: 3217
		private object additional;
	}
}
