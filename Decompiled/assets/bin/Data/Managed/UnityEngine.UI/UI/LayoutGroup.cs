using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200006E RID: 110
	[RequireComponent(typeof(RectTransform))]
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	public abstract class LayoutGroup : UIBehaviour, ILayoutController, ILayoutElement, ILayoutGroup
	{
		// Token: 0x06000333 RID: 819 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
		protected LayoutGroup()
		{
			if (this.m_Padding == null)
			{
				this.m_Padding = new RectOffset();
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000E338 File Offset: 0x0000C538
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000E340 File Offset: 0x0000C540
		public RectOffset padding
		{
			get
			{
				return this.m_Padding;
			}
			set
			{
				this.SetProperty<RectOffset>(ref this.m_Padding, value);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000E350 File Offset: 0x0000C550
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000E358 File Offset: 0x0000C558
		public TextAnchor childAlignment
		{
			get
			{
				return this.m_ChildAlignment;
			}
			set
			{
				this.SetProperty<TextAnchor>(ref this.m_ChildAlignment, value);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000E368 File Offset: 0x0000C568
		protected RectTransform rectTransform
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000E390 File Offset: 0x0000C590
		protected List<RectTransform> rectChildren
		{
			get
			{
				return this.m_RectChildren;
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000E398 File Offset: 0x0000C598
		public virtual void CalculateLayoutInputHorizontal()
		{
			this.m_RectChildren.Clear();
			for (int i = 0; i < this.rectTransform.childCount; i++)
			{
				RectTransform rectTransform = this.rectTransform.GetChild(i) as RectTransform;
				if (!(rectTransform == null))
				{
					ILayoutIgnorer layoutIgnorer = rectTransform.GetComponent(typeof(ILayoutIgnorer)) as ILayoutIgnorer;
					if (rectTransform.gameObject.activeInHierarchy && (layoutIgnorer == null || !layoutIgnorer.ignoreLayout))
					{
						this.m_RectChildren.Add(rectTransform);
					}
				}
			}
			this.m_Tracker.Clear();
		}

		// Token: 0x0600033B RID: 827
		public abstract void CalculateLayoutInputVertical();

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000E440 File Offset: 0x0000C640
		public virtual float minWidth
		{
			get
			{
				return this.GetTotalMinSize(0);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000E44C File Offset: 0x0000C64C
		public virtual float preferredWidth
		{
			get
			{
				return this.GetTotalPreferredSize(0);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000E458 File Offset: 0x0000C658
		public virtual float flexibleWidth
		{
			get
			{
				return this.GetTotalFlexibleSize(0);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000E464 File Offset: 0x0000C664
		public virtual float minHeight
		{
			get
			{
				return this.GetTotalMinSize(1);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000E470 File Offset: 0x0000C670
		public virtual float preferredHeight
		{
			get
			{
				return this.GetTotalPreferredSize(1);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000E47C File Offset: 0x0000C67C
		public virtual float flexibleHeight
		{
			get
			{
				return this.GetTotalFlexibleSize(1);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000E488 File Offset: 0x0000C688
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000343 RID: 835
		public abstract void SetLayoutHorizontal();

		// Token: 0x06000344 RID: 836
		public abstract void SetLayoutVertical();

		// Token: 0x06000345 RID: 837 RVA: 0x0000E48C File Offset: 0x0000C68C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000E49C File Offset: 0x0000C69C
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		protected float GetTotalMinSize(int axis)
		{
			return this.m_TotalMinSize[axis];
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		protected float GetTotalPreferredSize(int axis)
		{
			return this.m_TotalPreferredSize[axis];
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000E4E4 File Offset: 0x0000C6E4
		protected float GetTotalFlexibleSize(int axis)
		{
			return this.m_TotalFlexibleSize[axis];
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		protected float GetStartOffset(int axis, float requiredSpaceWithoutPadding)
		{
			float num = requiredSpaceWithoutPadding + (float)((axis != 0) ? this.padding.vertical : this.padding.horizontal);
			float num2 = this.rectTransform.rect.size[axis];
			float num3 = num2 - num;
			float num4;
			if (axis == 0)
			{
				num4 = (float)(this.childAlignment % TextAnchor.MiddleLeft) * 0.5f;
			}
			else
			{
				num4 = (float)(this.childAlignment / TextAnchor.MiddleLeft) * 0.5f;
			}
			return (float)((axis != 0) ? this.padding.top : this.padding.left) + num3 * num4;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000E5A0 File Offset: 0x0000C7A0
		protected void SetLayoutInputForAxis(float totalMin, float totalPreferred, float totalFlexible, int axis)
		{
			this.m_TotalMinSize[axis] = totalMin;
			this.m_TotalPreferredSize[axis] = totalPreferred;
			this.m_TotalFlexibleSize[axis] = totalFlexible;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		protected void SetChildAlongAxis(RectTransform rect, int axis, float pos, float size)
		{
			if (rect == null)
			{
				return;
			}
			this.m_Tracker.Add(this, rect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
			rect.SetInsetAndSizeFromParentEdge((axis != 0) ? RectTransform.Edge.Top : RectTransform.Edge.Left, pos, size);
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000E604 File Offset: 0x0000C804
		private bool isRootLayoutGroup
		{
			get
			{
				Transform parent = base.transform.parent;
				return parent == null || base.transform.parent.GetComponent(typeof(ILayoutGroup)) == null;
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000E64C File Offset: 0x0000C84C
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (this.isRootLayoutGroup)
			{
				this.SetDirty();
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000E668 File Offset: 0x0000C868
		protected virtual void OnTransformChildrenChanged()
		{
			this.SetDirty();
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000E670 File Offset: 0x0000C870
		protected void SetProperty<T>(ref T currentValue, T newValue)
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return;
			}
			currentValue = newValue;
			this.SetDirty();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000E6D0 File Offset: 0x0000C8D0
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x040001A8 RID: 424
		[SerializeField]
		protected RectOffset m_Padding = new RectOffset();

		// Token: 0x040001A9 RID: 425
		[SerializeField]
		[FormerlySerializedAs("m_Alignment")]
		protected TextAnchor m_ChildAlignment;

		// Token: 0x040001AA RID: 426
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x040001AB RID: 427
		protected DrivenRectTransformTracker m_Tracker;

		// Token: 0x040001AC RID: 428
		private Vector2 m_TotalMinSize = Vector2.zero;

		// Token: 0x040001AD RID: 429
		private Vector2 m_TotalPreferredSize = Vector2.zero;

		// Token: 0x040001AE RID: 430
		private Vector2 m_TotalFlexibleSize = Vector2.zero;

		// Token: 0x040001AF RID: 431
		[NonSerialized]
		private List<RectTransform> m_RectChildren = new List<RectTransform>();
	}
}
