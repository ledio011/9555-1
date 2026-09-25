using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000073 RID: 115
[AddComponentMenu("NGUI/Interaction/Table")]
public class UITable : UIWidgetContainer
{
	// Token: 0x1700003D RID: 61
	// (set) Token: 0x06000252 RID: 594 RVA: 0x00010668 File Offset: 0x0000E868
	public bool repositionNow
	{
		set
		{
			if (value)
			{
				this.mReposition = true;
				base.enabled = true;
			}
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000253 RID: 595 RVA: 0x00010680 File Offset: 0x0000E880
	public List<Transform> children
	{
		get
		{
			if (this.mChildren.Count == 0)
			{
				Transform transform = base.transform;
				this.mChildren.Clear();
				for (int i = 0; i < transform.childCount; i++)
				{
					Transform child = transform.GetChild(i);
					if (child && child.gameObject && (!this.hideInactive || NGUITools.GetActive(child.gameObject)))
					{
						this.mChildren.Add(child);
					}
				}
				if (this.sorting != UITable.Sorting.None || this.sorted)
				{
					if (this.sorting == UITable.Sorting.Alphabetic)
					{
						this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortByName));
					}
					else if (this.sorting == UITable.Sorting.Horizontal)
					{
						this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortHorizontal));
					}
					else if (this.sorting == UITable.Sorting.Vertical)
					{
						this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortVertical));
					}
					else
					{
						this.Sort(this.mChildren);
					}
				}
			}
			return this.mChildren;
		}
	}

	// Token: 0x06000254 RID: 596 RVA: 0x000107B0 File Offset: 0x0000E9B0
	protected virtual void Sort(List<Transform> list)
	{
		list.Sort(new Comparison<Transform>(UIGrid.SortByName));
	}

	// Token: 0x06000255 RID: 597 RVA: 0x000107C4 File Offset: 0x0000E9C4
	protected void RepositionVariableSize(List<Transform> children)
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = (this.columns <= 0) ? 1 : (children.Count / this.columns + 1);
		int num4 = (this.columns <= 0) ? children.Count : this.columns;
		Bounds[,] array = new Bounds[num3, num4];
		Bounds[] array2 = new Bounds[num4];
		Bounds[] array3 = new Bounds[num3];
		int num5 = 0;
		int num6 = 0;
		int i = 0;
		int count = children.Count;
		while (i < count)
		{
			Transform transform = children[i];
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform, !this.hideInactive);
			Vector3 localScale = transform.localScale;
			bounds.min = Vector3.Scale(bounds.min, localScale);
			bounds.max = Vector3.Scale(bounds.max, localScale);
			array[num6, num5] = bounds;
			array2[num5].Encapsulate(bounds);
			array3[num6].Encapsulate(bounds);
			if (++num5 >= this.columns && this.columns > 0)
			{
				num5 = 0;
				num6++;
			}
			i++;
		}
		num5 = 0;
		num6 = 0;
		int j = 0;
		int count2 = children.Count;
		while (j < count2)
		{
			Transform transform2 = children[j];
			Bounds bounds2 = array[num6, num5];
			Bounds bounds3 = array2[num5];
			Bounds bounds4 = array3[num6];
			Vector3 localPosition = transform2.localPosition;
			localPosition.x = num + bounds2.extents.x - bounds2.center.x;
			localPosition.x += bounds2.min.x - bounds3.min.x + this.padding.x;
			if (this.direction == UITable.Direction.Down)
			{
				localPosition.y = -num2 - bounds2.extents.y - bounds2.center.y;
				localPosition.y += (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - this.padding.y;
			}
			else
			{
				localPosition.y = num2 + (bounds2.extents.y - bounds2.center.y);
				localPosition.y -= (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - this.padding.y;
			}
			num += bounds3.max.x - bounds3.min.x + this.padding.x * 2f;
			transform2.localPosition = localPosition;
			if (++num5 >= this.columns && this.columns > 0)
			{
				num5 = 0;
				num6++;
				num = 0f;
				num2 += bounds4.size.y + this.padding.y * 2f;
			}
			j++;
		}
	}

	// Token: 0x06000256 RID: 598 RVA: 0x00010B84 File Offset: 0x0000ED84
	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !this.mInitDone && NGUITools.GetActive(this))
		{
			this.mReposition = true;
			return;
		}
		if (!this.mInitDone)
		{
			this.Init();
		}
		this.mReposition = false;
		Transform transform = base.transform;
		this.mChildren.Clear();
		List<Transform> children = this.children;
		if (children.Count > 0)
		{
			this.RepositionVariableSize(children);
		}
		if (this.keepWithinPanel && this.mPanel != null)
		{
			this.mPanel.ConstrainTargetToBounds(transform, true);
			UIScrollView component = this.mPanel.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
			}
		}
		if (this.onReposition != null)
		{
			this.onReposition();
		}
	}

	// Token: 0x06000257 RID: 599 RVA: 0x00010C5C File Offset: 0x0000EE5C
	protected virtual void Start()
	{
		this.Init();
		base.enabled = false;
	}

	// Token: 0x06000258 RID: 600 RVA: 0x00010C6C File Offset: 0x0000EE6C
	protected virtual void Init()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	// Token: 0x06000259 RID: 601 RVA: 0x00010C88 File Offset: 0x0000EE88
	protected virtual void LateUpdate()
	{
		if (this.mReposition)
		{
			this.Reposition();
		}
		base.enabled = false;
	}

	// Token: 0x04000290 RID: 656
	public int columns;

	// Token: 0x04000291 RID: 657
	public UITable.Direction direction;

	// Token: 0x04000292 RID: 658
	public UITable.Sorting sorting;

	// Token: 0x04000293 RID: 659
	public bool hideInactive = true;

	// Token: 0x04000294 RID: 660
	public bool keepWithinPanel;

	// Token: 0x04000295 RID: 661
	public Vector2 padding = Vector2.zero;

	// Token: 0x04000296 RID: 662
	public UITable.OnReposition onReposition;

	// Token: 0x04000297 RID: 663
	protected UIPanel mPanel;

	// Token: 0x04000298 RID: 664
	protected bool mInitDone;

	// Token: 0x04000299 RID: 665
	protected bool mReposition;

	// Token: 0x0400029A RID: 666
	protected List<Transform> mChildren = new List<Transform>();

	// Token: 0x0400029B RID: 667
	[HideInInspector]
	[SerializeField]
	private bool sorted;

	// Token: 0x02000074 RID: 116
	public enum Direction
	{
		// Token: 0x0400029D RID: 669
		Down,
		// Token: 0x0400029E RID: 670
		Up
	}

	// Token: 0x02000075 RID: 117
	public enum Sorting
	{
		// Token: 0x040002A0 RID: 672
		None,
		// Token: 0x040002A1 RID: 673
		Alphabetic,
		// Token: 0x040002A2 RID: 674
		Horizontal,
		// Token: 0x040002A3 RID: 675
		Vertical,
		// Token: 0x040002A4 RID: 676
		Custom
	}

	// Token: 0x02000A99 RID: 2713
	// (Invoke) Token: 0x06004EED RID: 20205
	public delegate void OnReposition();
}
