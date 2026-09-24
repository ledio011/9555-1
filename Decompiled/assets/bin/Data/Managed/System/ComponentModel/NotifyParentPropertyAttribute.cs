using System;

namespace System.ComponentModel
{
	// Token: 0x02000021 RID: 33
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NotifyParentPropertyAttribute : Attribute
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public NotifyParentPropertyAttribute(bool notifyParent)
		{
			this.notifyParent = notifyParent;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002B0C File Offset: 0x00000D0C
		public bool NotifyParent
		{
			get
			{
				return this.notifyParent;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002B14 File Offset: 0x00000D14
		public override bool Equals(object obj)
		{
			return obj is NotifyParentPropertyAttribute && (obj == this || ((NotifyParentPropertyAttribute)obj).NotifyParent == this.notifyParent);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002B40 File Offset: 0x00000D40
		public override int GetHashCode()
		{
			return this.notifyParent.GetHashCode();
		}

		// Token: 0x04000046 RID: 70
		private bool notifyParent;

		// Token: 0x04000047 RID: 71
		public static readonly NotifyParentPropertyAttribute Default = new NotifyParentPropertyAttribute(false);

		// Token: 0x04000048 RID: 72
		public static readonly NotifyParentPropertyAttribute No = new NotifyParentPropertyAttribute(false);

		// Token: 0x04000049 RID: 73
		public static readonly NotifyParentPropertyAttribute Yes = new NotifyParentPropertyAttribute(true);
	}
}
