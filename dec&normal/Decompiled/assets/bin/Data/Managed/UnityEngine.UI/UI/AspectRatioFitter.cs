using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000030 RID: 48
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("Layout/Aspect Ratio Fitter", 142)]
	[ExecuteInEditMode]
	public class AspectRatioFitter : UIBehaviour, ILayoutController, ILayoutSelfController
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00005140 File Offset: 0x00003340
		protected AspectRatioFitter()
		{
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005154 File Offset: 0x00003354
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000515C File Offset: 0x0000335C
		public AspectRatioFitter.AspectMode aspectMode
		{
			get
			{
				return this.m_AspectMode;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<AspectRatioFitter.AspectMode>(ref this.m_AspectMode, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00005178 File Offset: 0x00003378
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00005180 File Offset: 0x00003380
		public float aspectRatio
		{
			get
			{
				return this.m_AspectRatio;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_AspectRatio, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000519C File Offset: 0x0000339C
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

		// Token: 0x06000131 RID: 305 RVA: 0x000051C4 File Offset: 0x000033C4
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000051D4 File Offset: 0x000033D4
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000051F4 File Offset: 0x000033F4
		protected override void OnRectTransformDimensionsChange()
		{
			this.UpdateRect();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000051FC File Offset: 0x000033FC
		private void UpdateRect()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_Tracker.Clear();
			switch (this.m_AspectMode)
			{
			case AspectRatioFitter.AspectMode.WidthControlsHeight:
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.SizeDeltaY);
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.rectTransform.rect.width / this.m_AspectRatio);
				break;
			case AspectRatioFitter.AspectMode.HeightControlsWidth:
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.SizeDeltaX);
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.rectTransform.rect.height * this.m_AspectRatio);
				break;
			case AspectRatioFitter.AspectMode.FitInParent:
			case AspectRatioFitter.AspectMode.EnvelopeParent:
			{
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
				this.rectTransform.anchorMin = Vector2.zero;
				this.rectTransform.anchorMax = Vector2.one;
				this.rectTransform.anchoredPosition = Vector2.zero;
				Vector2 zero = Vector2.zero;
				Vector2 parentSize = this.GetParentSize();
				if (parentSize.y * this.aspectRatio < parentSize.x ^ this.m_AspectMode == AspectRatioFitter.AspectMode.FitInParent)
				{
					zero.y = this.GetSizeDeltaToProduceSize(parentSize.x / this.aspectRatio, 1);
				}
				else
				{
					zero.x = this.GetSizeDeltaToProduceSize(parentSize.y * this.aspectRatio, 0);
				}
				this.rectTransform.sizeDelta = zero;
				break;
			}
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005394 File Offset: 0x00003594
		private float GetSizeDeltaToProduceSize(float size, int axis)
		{
			return size - this.GetParentSize()[axis] * (this.rectTransform.anchorMax[axis] - this.rectTransform.anchorMin[axis]);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000053DC File Offset: 0x000035DC
		private Vector2 GetParentSize()
		{
			RectTransform rectTransform = this.rectTransform.parent as RectTransform;
			if (!rectTransform)
			{
				return Vector2.zero;
			}
			return rectTransform.rect.size;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000541C File Offset: 0x0000361C
		public virtual void SetLayoutHorizontal()
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005420 File Offset: 0x00003620
		public virtual void SetLayoutVertical()
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005424 File Offset: 0x00003624
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateRect();
		}

		// Token: 0x04000090 RID: 144
		[SerializeField]
		private AspectRatioFitter.AspectMode m_AspectMode;

		// Token: 0x04000091 RID: 145
		[SerializeField]
		private float m_AspectRatio = 1f;

		// Token: 0x04000092 RID: 146
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x04000093 RID: 147
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x02000031 RID: 49
		public enum AspectMode
		{
			// Token: 0x04000095 RID: 149
			None,
			// Token: 0x04000096 RID: 150
			WidthControlsHeight,
			// Token: 0x04000097 RID: 151
			HeightControlsWidth,
			// Token: 0x04000098 RID: 152
			FitInParent,
			// Token: 0x04000099 RID: 153
			EnvelopeParent
		}
	}
}
