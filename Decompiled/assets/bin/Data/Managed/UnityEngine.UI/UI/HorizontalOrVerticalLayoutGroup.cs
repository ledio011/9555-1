using System;

namespace UnityEngine.UI
{
	// Token: 0x02000053 RID: 83
	public abstract class HorizontalOrVerticalLayoutGroup : LayoutGroup
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000083A8 File Offset: 0x000065A8
		// (set) Token: 0x06000240 RID: 576 RVA: 0x000083B0 File Offset: 0x000065B0
		public float spacing
		{
			get
			{
				return this.m_Spacing;
			}
			set
			{
				base.SetProperty<float>(ref this.m_Spacing, value);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000241 RID: 577 RVA: 0x000083C0 File Offset: 0x000065C0
		// (set) Token: 0x06000242 RID: 578 RVA: 0x000083C8 File Offset: 0x000065C8
		public bool childForceExpandWidth
		{
			get
			{
				return this.m_ChildForceExpandWidth;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildForceExpandWidth, value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000083D8 File Offset: 0x000065D8
		// (set) Token: 0x06000244 RID: 580 RVA: 0x000083E0 File Offset: 0x000065E0
		public bool childForceExpandHeight
		{
			get
			{
				return this.m_ChildForceExpandHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildForceExpandHeight, value);
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000083F0 File Offset: 0x000065F0
		protected void CalcAlongAxis(int axis, bool isVertical)
		{
			float num = (float)((axis != 0) ? base.padding.vertical : base.padding.horizontal);
			float num2 = num;
			float num3 = num;
			float num4 = 0f;
			bool flag = isVertical ^ axis == 1;
			for (int i = 0; i < base.rectChildren.Count; i++)
			{
				RectTransform rect = base.rectChildren[i];
				float minSize = LayoutUtility.GetMinSize(rect, axis);
				float preferredSize = LayoutUtility.GetPreferredSize(rect, axis);
				float num5 = LayoutUtility.GetFlexibleSize(rect, axis);
				if ((axis != 0) ? this.childForceExpandHeight : this.childForceExpandWidth)
				{
					num5 = Mathf.Max(num5, 1f);
				}
				if (flag)
				{
					num2 = Mathf.Max(minSize + num, num2);
					num3 = Mathf.Max(preferredSize + num, num3);
					num4 = Mathf.Max(num5, num4);
				}
				else
				{
					num2 += minSize + this.spacing;
					num3 += preferredSize + this.spacing;
					num4 += num5;
				}
			}
			if (!flag && base.rectChildren.Count > 0)
			{
				num2 -= this.spacing;
				num3 -= this.spacing;
			}
			num3 = Mathf.Max(num2, num3);
			base.SetLayoutInputForAxis(num2, num3, num4, axis);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00008530 File Offset: 0x00006730
		protected void SetChildrenAlongAxis(int axis, bool isVertical)
		{
			float num = base.rectTransform.rect.size[axis];
			bool flag = isVertical ^ axis == 1;
			if (flag)
			{
				float value = num - (float)((axis != 0) ? base.padding.vertical : base.padding.horizontal);
				for (int i = 0; i < base.rectChildren.Count; i++)
				{
					RectTransform rect = base.rectChildren[i];
					float minSize = LayoutUtility.GetMinSize(rect, axis);
					float preferredSize = LayoutUtility.GetPreferredSize(rect, axis);
					float num2 = LayoutUtility.GetFlexibleSize(rect, axis);
					if ((axis != 0) ? this.childForceExpandHeight : this.childForceExpandWidth)
					{
						num2 = Mathf.Max(num2, 1f);
					}
					float num3 = Mathf.Clamp(value, minSize, (num2 <= 0f) ? preferredSize : num);
					float startOffset = base.GetStartOffset(axis, num3);
					base.SetChildAlongAxis(rect, axis, startOffset, num3);
				}
			}
			else
			{
				float num4 = (float)((axis != 0) ? base.padding.top : base.padding.left);
				if (base.GetTotalFlexibleSize(axis) == 0f && base.GetTotalPreferredSize(axis) < num)
				{
					num4 = base.GetStartOffset(axis, base.GetTotalPreferredSize(axis) - (float)((axis != 0) ? base.padding.vertical : base.padding.horizontal));
				}
				float t = 0f;
				if (base.GetTotalMinSize(axis) != base.GetTotalPreferredSize(axis))
				{
					t = Mathf.Clamp01((num - base.GetTotalMinSize(axis)) / (base.GetTotalPreferredSize(axis) - base.GetTotalMinSize(axis)));
				}
				float num5 = 0f;
				if (num > base.GetTotalPreferredSize(axis) && base.GetTotalFlexibleSize(axis) > 0f)
				{
					num5 = (num - base.GetTotalPreferredSize(axis)) / base.GetTotalFlexibleSize(axis);
				}
				for (int j = 0; j < base.rectChildren.Count; j++)
				{
					RectTransform rect2 = base.rectChildren[j];
					float minSize2 = LayoutUtility.GetMinSize(rect2, axis);
					float preferredSize2 = LayoutUtility.GetPreferredSize(rect2, axis);
					float num6 = LayoutUtility.GetFlexibleSize(rect2, axis);
					if ((axis != 0) ? this.childForceExpandHeight : this.childForceExpandWidth)
					{
						num6 = Mathf.Max(num6, 1f);
					}
					float num7 = Mathf.Lerp(minSize2, preferredSize2, t);
					num7 += num6 * num5;
					base.SetChildAlongAxis(rect2, axis, num4, num7);
					num4 += num7 + this.spacing;
				}
			}
		}

		// Token: 0x04000130 RID: 304
		[SerializeField]
		protected float m_Spacing;

		// Token: 0x04000131 RID: 305
		[SerializeField]
		protected bool m_ChildForceExpandWidth = true;

		// Token: 0x04000132 RID: 306
		[SerializeField]
		protected bool m_ChildForceExpandHeight = true;
	}
}
