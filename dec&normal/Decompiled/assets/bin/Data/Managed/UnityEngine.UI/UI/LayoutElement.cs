using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200006D RID: 109
	[AddComponentMenu("Layout/Layout Element", 140)]
	[RequireComponent(typeof(RectTransform))]
	[ExecuteInEditMode]
	public class LayoutElement : UIBehaviour, ILayoutElement, ILayoutIgnorer
	{
		// Token: 0x0600031B RID: 795 RVA: 0x0000E120 File Offset: 0x0000C320
		protected LayoutElement()
		{
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000E178 File Offset: 0x0000C378
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000E180 File Offset: 0x0000C380
		public virtual bool ignoreLayout
		{
			get
			{
				return this.m_IgnoreLayout;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_IgnoreLayout, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000E19C File Offset: 0x0000C39C
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000E1AC File Offset: 0x0000C3AC
		public virtual float minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000E1D0 File Offset: 0x0000C3D0
		public virtual float minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000E1EC File Offset: 0x0000C3EC
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		public virtual float preferredWidth
		{
			get
			{
				return this.m_PreferredWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000E210 File Offset: 0x0000C410
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000E218 File Offset: 0x0000C418
		public virtual float preferredHeight
		{
			get
			{
				return this.m_PreferredHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000E234 File Offset: 0x0000C434
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000E23C File Offset: 0x0000C43C
		public virtual float flexibleWidth
		{
			get
			{
				return this.m_FlexibleWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FlexibleWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000E258 File Offset: 0x0000C458
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000E260 File Offset: 0x0000C460
		public virtual float flexibleHeight
		{
			get
			{
				return this.m_FlexibleHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FlexibleHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000E27C File Offset: 0x0000C47C
		public virtual int layoutPriority
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000E280 File Offset: 0x0000C480
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000E290 File Offset: 0x0000C490
		protected override void OnTransformParentChanged()
		{
			this.SetDirty();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000E298 File Offset: 0x0000C498
		protected override void OnDisable()
		{
			this.SetDirty();
			base.OnDisable();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000E2A8 File Offset: 0x0000C4A8
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000E2B0 File Offset: 0x0000C4B0
		protected override void OnBeforeTransformParentChanged()
		{
			this.SetDirty();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(base.transform as RectTransform);
		}

		// Token: 0x040001A1 RID: 417
		[SerializeField]
		private bool m_IgnoreLayout;

		// Token: 0x040001A2 RID: 418
		[SerializeField]
		private float m_MinWidth = -1f;

		// Token: 0x040001A3 RID: 419
		[SerializeField]
		private float m_MinHeight = -1f;

		// Token: 0x040001A4 RID: 420
		[SerializeField]
		private float m_PreferredWidth = -1f;

		// Token: 0x040001A5 RID: 421
		[SerializeField]
		private float m_PreferredHeight = -1f;

		// Token: 0x040001A6 RID: 422
		[SerializeField]
		private float m_FlexibleWidth = -1f;

		// Token: 0x040001A7 RID: 423
		[SerializeField]
		private float m_FlexibleHeight = -1f;
	}
}
