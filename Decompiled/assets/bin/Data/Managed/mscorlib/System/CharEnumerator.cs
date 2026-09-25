using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000075 RID: 117
	[ComVisible(true)]
	[Serializable]
	public sealed class CharEnumerator : IEnumerator<char>, IEnumerator, ICloneable, IDisposable
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x00012874 File Offset: 0x00010A74
		internal CharEnumerator(string s)
		{
			this.str = s;
			this.index = -1;
			this.length = s.Length;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00012898 File Offset: 0x00010A98
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000128A8 File Offset: 0x00010AA8
		void IDisposable.Dispose()
		{
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x000128AC File Offset: 0x00010AAC
		public char Current
		{
			get
			{
				if (this.index == -1 || this.index >= this.length)
				{
					throw new InvalidOperationException(Locale.GetText("The position is not valid."));
				}
				return this.str[this.index];
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000128EC File Offset: 0x00010AEC
		public object Clone()
		{
			return new CharEnumerator(this.str)
			{
				index = this.index
			};
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00012914 File Offset: 0x00010B14
		public bool MoveNext()
		{
			this.index++;
			if (this.index >= this.length)
			{
				this.index = this.length;
				return false;
			}
			return true;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00012944 File Offset: 0x00010B44
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x040001C3 RID: 451
		private string str;

		// Token: 0x040001C4 RID: 452
		private int index;

		// Token: 0x040001C5 RID: 453
		private int length;
	}
}
