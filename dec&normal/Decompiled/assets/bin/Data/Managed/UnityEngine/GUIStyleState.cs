using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000084 RID: 132
	[Serializable]
	[StructLayout(0)]
	public sealed class GUIStyleState
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x00010C78 File Offset: 0x0000EE78
		public GUIStyleState()
		{
			this.Init();
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00010C88 File Offset: 0x0000EE88
		internal GUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
			this.RefreshAssetReference();
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00010CA4 File Offset: 0x0000EEA4
		internal void RefreshAssetReference()
		{
			this.m_BackgroundInternal = this.GetBackgroundInternal();
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00010CB4 File Offset: 0x0000EEB4
		~GUIStyleState()
		{
			if (this.m_SourceStyle == null)
			{
				this.Cleanup();
			}
		}

		// Token: 0x06000696 RID: 1686
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init();

		// Token: 0x06000697 RID: 1687
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x06000698 RID: 1688
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Texture2D GetBackgroundInternal();

		// Token: 0x1700015F RID: 351
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x00010CF0 File Offset: 0x0000EEF0
		public Color textColor
		{
			set
			{
				this.INTERNAL_set_textColor(ref value);
			}
		}

		// Token: 0x0600069A RID: 1690
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_textColor(ref Color value);

		// Token: 0x0400018D RID: 397
		[NotRenamed]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x0400018E RID: 398
		private GUIStyle m_SourceStyle;

		// Token: 0x0400018F RID: 399
		[NonSerialized]
		private Texture2D m_BackgroundInternal;
	}
}
