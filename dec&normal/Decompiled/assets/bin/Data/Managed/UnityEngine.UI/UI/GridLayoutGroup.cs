using System;

namespace UnityEngine.UI
{
	// Token: 0x0200004E RID: 78
	[AddComponentMenu("Layout/Grid Layout Group", 152)]
	public class GridLayoutGroup : LayoutGroup
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00007BF4 File Offset: 0x00005DF4
		protected GridLayoutGroup()
		{
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00007C24 File Offset: 0x00005E24
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00007C2C File Offset: 0x00005E2C
		public GridLayoutGroup.Corner startCorner
		{
			get
			{
				return this.m_StartCorner;
			}
			set
			{
				base.SetProperty<GridLayoutGroup.Corner>(ref this.m_StartCorner, value);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00007C3C File Offset: 0x00005E3C
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00007C44 File Offset: 0x00005E44
		public GridLayoutGroup.Axis startAxis
		{
			get
			{
				return this.m_StartAxis;
			}
			set
			{
				base.SetProperty<GridLayoutGroup.Axis>(ref this.m_StartAxis, value);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00007C54 File Offset: 0x00005E54
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00007C5C File Offset: 0x00005E5C
		public Vector2 cellSize
		{
			get
			{
				return this.m_CellSize;
			}
			set
			{
				base.SetProperty<Vector2>(ref this.m_CellSize, value);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00007C6C File Offset: 0x00005E6C
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00007C74 File Offset: 0x00005E74
		public Vector2 spacing
		{
			get
			{
				return this.m_Spacing;
			}
			set
			{
				base.SetProperty<Vector2>(ref this.m_Spacing, value);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00007C84 File Offset: 0x00005E84
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00007C8C File Offset: 0x00005E8C
		public GridLayoutGroup.Constraint constraint
		{
			get
			{
				return this.m_Constraint;
			}
			set
			{
				base.SetProperty<GridLayoutGroup.Constraint>(ref this.m_Constraint, value);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00007C9C File Offset: 0x00005E9C
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00007CA4 File Offset: 0x00005EA4
		public int constraintCount
		{
			get
			{
				return this.m_ConstraintCount;
			}
			set
			{
				base.SetProperty<int>(ref this.m_ConstraintCount, Mathf.Max(1, value));
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007CBC File Offset: 0x00005EBC
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			int num2;
			int num;
			if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = (num2 = this.m_ConstraintCount);
			}
			else if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount)
			{
				num = (num2 = Mathf.CeilToInt((float)base.rectChildren.Count / (float)this.m_ConstraintCount - 0.001f));
			}
			else
			{
				num2 = 1;
				num = Mathf.CeilToInt(Mathf.Sqrt((float)base.rectChildren.Count));
			}
			base.SetLayoutInputForAxis((float)base.padding.horizontal + (this.cellSize.x + this.spacing.x) * (float)num2 - this.spacing.x, (float)base.padding.horizontal + (this.cellSize.x + this.spacing.x) * (float)num - this.spacing.x, -1f, 0);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007DC4 File Offset: 0x00005FC4
		public override void CalculateLayoutInputVertical()
		{
			int num;
			if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = Mathf.CeilToInt((float)base.rectChildren.Count / (float)this.m_ConstraintCount - 0.001f);
			}
			else if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount)
			{
				num = this.m_ConstraintCount;
			}
			else
			{
				float x = base.rectTransform.rect.size.x;
				int num2 = Mathf.Max(1, Mathf.FloorToInt((x - (float)base.padding.horizontal + this.spacing.x + 0.001f) / (this.cellSize.x + this.spacing.x)));
				num = Mathf.CeilToInt((float)base.rectChildren.Count / (float)num2);
			}
			float num3 = (float)base.padding.vertical + (this.cellSize.y + this.spacing.y) * (float)num - this.spacing.y;
			base.SetLayoutInputForAxis(num3, num3, -1f, 1);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007EF0 File Offset: 0x000060F0
		public override void SetLayoutHorizontal()
		{
			this.SetCellsAlongAxis(0);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007EFC File Offset: 0x000060FC
		public override void SetLayoutVertical()
		{
			this.SetCellsAlongAxis(1);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007F08 File Offset: 0x00006108
		private void SetCellsAlongAxis(int axis)
		{
			if (axis == 0)
			{
				for (int i = 0; i < base.rectChildren.Count; i++)
				{
					RectTransform rectTransform = base.rectChildren[i];
					this.m_Tracker.Add(this, rectTransform, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
					rectTransform.anchorMin = Vector2.up;
					rectTransform.anchorMax = Vector2.up;
					rectTransform.sizeDelta = this.cellSize;
				}
				return;
			}
			float x = base.rectTransform.rect.size.x;
			float y = base.rectTransform.rect.size.y;
			int num;
			int num2;
			if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = this.m_ConstraintCount;
				num2 = Mathf.CeilToInt((float)base.rectChildren.Count / (float)num - 0.001f);
			}
			else if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount)
			{
				num2 = this.m_ConstraintCount;
				num = Mathf.CeilToInt((float)base.rectChildren.Count / (float)num2 - 0.001f);
			}
			else
			{
				if (this.cellSize.x + this.spacing.x <= 0f)
				{
					num = int.MaxValue;
				}
				else
				{
					num = Mathf.Max(1, Mathf.FloorToInt((x - (float)base.padding.horizontal + this.spacing.x + 0.001f) / (this.cellSize.x + this.spacing.x)));
				}
				if (this.cellSize.y + this.spacing.y <= 0f)
				{
					num2 = int.MaxValue;
				}
				else
				{
					num2 = Mathf.Max(1, Mathf.FloorToInt((y - (float)base.padding.vertical + this.spacing.y + 0.001f) / (this.cellSize.y + this.spacing.y)));
				}
			}
			int num3 = (int)(this.startCorner % GridLayoutGroup.Corner.LowerLeft);
			int num4 = (int)(this.startCorner / GridLayoutGroup.Corner.LowerLeft);
			int num5;
			int num6;
			int num7;
			if (this.startAxis == GridLayoutGroup.Axis.Horizontal)
			{
				num5 = num;
				num6 = Mathf.Clamp(num, 1, base.rectChildren.Count);
				num7 = Mathf.Clamp(num2, 1, Mathf.CeilToInt((float)base.rectChildren.Count / (float)num5));
			}
			else
			{
				num5 = num2;
				num7 = Mathf.Clamp(num2, 1, base.rectChildren.Count);
				num6 = Mathf.Clamp(num, 1, Mathf.CeilToInt((float)base.rectChildren.Count / (float)num5));
			}
			Vector2 vector = new Vector2((float)num6 * this.cellSize.x + (float)(num6 - 1) * this.spacing.x, (float)num7 * this.cellSize.y + (float)(num7 - 1) * this.spacing.y);
			Vector2 vector2 = new Vector2(base.GetStartOffset(0, vector.x), base.GetStartOffset(1, vector.y));
			for (int j = 0; j < base.rectChildren.Count; j++)
			{
				int num8;
				int num9;
				if (this.startAxis == GridLayoutGroup.Axis.Horizontal)
				{
					num8 = j % num5;
					num9 = j / num5;
				}
				else
				{
					num8 = j / num5;
					num9 = j % num5;
				}
				if (num3 == 1)
				{
					num8 = num6 - 1 - num8;
				}
				if (num4 == 1)
				{
					num9 = num7 - 1 - num9;
				}
				base.SetChildAlongAxis(base.rectChildren[j], 0, vector2.x + (this.cellSize[0] + this.spacing[0]) * (float)num8, this.cellSize[0]);
				base.SetChildAlongAxis(base.rectChildren[j], 1, vector2.y + (this.cellSize[1] + this.spacing[1]) * (float)num9, this.cellSize[1]);
			}
		}

		// Token: 0x0400011E RID: 286
		[SerializeField]
		protected GridLayoutGroup.Corner m_StartCorner;

		// Token: 0x0400011F RID: 287
		[SerializeField]
		protected GridLayoutGroup.Axis m_StartAxis;

		// Token: 0x04000120 RID: 288
		[SerializeField]
		protected Vector2 m_CellSize = new Vector2(100f, 100f);

		// Token: 0x04000121 RID: 289
		[SerializeField]
		protected Vector2 m_Spacing = Vector2.zero;

		// Token: 0x04000122 RID: 290
		[SerializeField]
		protected GridLayoutGroup.Constraint m_Constraint;

		// Token: 0x04000123 RID: 291
		[SerializeField]
		protected int m_ConstraintCount = 2;

		// Token: 0x0200004F RID: 79
		public enum Axis
		{
			// Token: 0x04000125 RID: 293
			Horizontal,
			// Token: 0x04000126 RID: 294
			Vertical
		}

		// Token: 0x02000050 RID: 80
		public enum Constraint
		{
			// Token: 0x04000128 RID: 296
			Flexible,
			// Token: 0x04000129 RID: 297
			FixedColumnCount,
			// Token: 0x0400012A RID: 298
			FixedRowCount
		}

		// Token: 0x02000051 RID: 81
		public enum Corner
		{
			// Token: 0x0400012C RID: 300
			UpperLeft,
			// Token: 0x0400012D RID: 301
			UpperRight,
			// Token: 0x0400012E RID: 302
			LowerLeft,
			// Token: 0x0400012F RID: 303
			LowerRight
		}
	}
}
