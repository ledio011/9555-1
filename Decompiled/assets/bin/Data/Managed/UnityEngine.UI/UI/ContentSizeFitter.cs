using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000040 RID: 64
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("Layout/Content Size Fitter", 141)]
	public class ContentSizeFitter : UIBehaviour, ILayoutController, ILayoutSelfController
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x000062BC File Offset: 0x000044BC
		protected ContentSizeFitter()
		{
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000062C4 File Offset: 0x000044C4
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x000062CC File Offset: 0x000044CC
		public ContentSizeFitter.FitMode horizontalFit
		{
			get
			{
				return this.m_HorizontalFit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ContentSizeFitter.FitMode>(ref this.m_HorizontalFit, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000062E8 File Offset: 0x000044E8
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x000062F0 File Offset: 0x000044F0
		public ContentSizeFitter.FitMode verticalFit
		{
			get
			{
				return this.m_VerticalFit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ContentSizeFitter.FitMode>(ref this.m_VerticalFit, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000630C File Offset: 0x0000450C
		private RectTransform rectTransform
		{
			get
			{
				if (this.m_Rect == null)
				{
					this.m_Rect = base.GetComponent<RectTransform>();
				}
				return this.m_Rect;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006334 File Offset: 0x00004534
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006344 File Offset: 0x00004544
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006364 File Offset: 0x00004564
		protected override void OnRectTransformDimensionsChange()
		{
			this.SetDirty();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000636C File Offset: 0x0000456C
		private void HandleSelfFittingAlongAxis(int axis)
		{
			ContentSizeFitter.FitMode fitMode = (axis != 0) ? this.verticalFit : this.horizontalFit;
			if (fitMode == ContentSizeFitter.FitMode.Unconstrained)
			{
				return;
			}
			this.m_Tracker.Add(this, this.rectTransform, (axis != 0) ? DrivenTransformProperties.SizeDeltaY : DrivenTransformProperties.SizeDeltaX);
			if (fitMode == ContentSizeFitter.FitMode.MinSize)
			{
				this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetMinSize(this.m_Rect, axis));
			}
			else
			{
				this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetPreferredSize(this.m_Rect, axis));
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000063FC File Offset: 0x000045FC
		public virtual void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
			this.HandleSelfFittingAlongAxis(0);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00006410 File Offset: 0x00004610
		public virtual void SetLayoutVertical()
		{
			this.HandleSelfFittingAlongAxis(1);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000641C File Offset: 0x0000461C
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x040000D8 RID: 216
		[SerializeField]
		protected ContentSizeFitter.FitMode m_HorizontalFit;

		// Token: 0x040000D9 RID: 217
		[SerializeField]
		protected ContentSizeFitter.FitMode m_VerticalFit;

		// Token: 0x040000DA RID: 218
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x040000DB RID: 219
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x02000041 RID: 65
		public enum FitMode
		{
			// Token: 0x040000DD RID: 221
			Unconstrained,
			// Token: 0x040000DE RID: 222
			MinSize,
			// Token: 0x040000DF RID: 223
			PreferredSize
		}
	}
}
