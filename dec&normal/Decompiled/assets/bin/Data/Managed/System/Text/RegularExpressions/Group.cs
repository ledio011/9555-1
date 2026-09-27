using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	public class Group : Capture
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000850C File Offset: 0x0000670C
		internal Group(string text, int index, int length, int n_caps) : base(text, index, length)
		{
			this.success = true;
			this.captures = new CaptureCollection(n_caps);
			this.captures.SetValue(this, n_caps - 1);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000853C File Offset: 0x0000673C
		internal Group(string text, int index, int length) : base(text, index, length)
		{
			this.success = true;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008550 File Offset: 0x00006750
		internal Group() : base(string.Empty)
		{
			this.success = false;
			this.captures = new CaptureCollection(0);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000857C File Offset: 0x0000677C
		public CaptureCollection Captures
		{
			get
			{
				return this.captures;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00008584 File Offset: 0x00006784
		public bool Success
		{
			get
			{
				return this.success;
			}
		}

		// Token: 0x04000982 RID: 2434
		internal static Group Fail = new Group();

		// Token: 0x04000983 RID: 2435
		private bool success;

		// Token: 0x04000984 RID: 2436
		private CaptureCollection captures;
	}
}
