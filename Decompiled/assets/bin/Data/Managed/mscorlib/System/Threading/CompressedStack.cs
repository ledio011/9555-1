using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	// Token: 0x020003AF RID: 943
	[Serializable]
	public sealed class CompressedStack : ISerializable
	{
		// Token: 0x06001C7E RID: 7294 RVA: 0x0006D61C File Offset: 0x0006B81C
		internal CompressedStack(int length)
		{
			if (length > 0)
			{
				this._list = new ArrayList(length);
			}
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x0006D638 File Offset: 0x0006B838
		internal CompressedStack(CompressedStack cs)
		{
			if (cs != null && cs._list != null)
			{
				this._list = (ArrayList)cs._list.Clone();
			}
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0006D668 File Offset: 0x0006B868
		[ComVisible(false)]
		public CompressedStack CreateCopy()
		{
			return new CompressedStack(this);
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x0006D670 File Offset: 0x0006B870
		public static CompressedStack Capture()
		{
			CompressedStack compressedStack = new CompressedStack(0);
			compressedStack._list = SecurityFrame.GetStack(1);
			CompressedStack compressedStack2 = Thread.CurrentThread.GetCompressedStack();
			if (compressedStack2 != null)
			{
				for (int i = 0; i < compressedStack2._list.Count; i++)
				{
					compressedStack._list.Add(compressedStack2._list[i]);
				}
			}
			return compressedStack;
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x0006D6D8 File Offset: 0x0006B8D8
		[MonoTODO("incomplete")]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x0006D6EC File Offset: 0x0006B8EC
		internal bool IsEmpty()
		{
			return this._list == null || this._list.Count == 0;
		}

		// Token: 0x04000F14 RID: 3860
		private ArrayList _list;
	}
}
