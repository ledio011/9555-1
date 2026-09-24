using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
[AddComponentMenu("NGUI/Interaction/Grid")]
public class UIGrid : UIWidgetContainer
{
	// Token: 0x17000019 RID: 25
	// (set) Token: 0x06000183 RID: 387 RVA: 0x0000A570 File Offset: 0x00008770
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

	// Token: 0x06000184 RID: 388 RVA: 0x0000A588 File Offset: 0x00008788
	public BetterList<Transform> GetChildList()
	{
		Transform transform = base.transform;
		BetterList<Transform> betterList = new BetterList<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (!this.hideInactive || (child && NGUITools.GetActive(child.gameObject)))
			{
				betterList.Add(child);
			}
		}
		return betterList;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000A5F0 File Offset: 0x000087F0
	public Transform GetChild(int index)
	{
		BetterList<Transform> childList = this.GetChildList();
		return (index >= childList.size) ? null : childList[index];
	}

	// Token: 0x06000186 RID: 390 RVA: 0x0000A620 File Offset: 0x00008820
	public int GetIndex(Transform trans)
	{
		return this.GetChildList().IndexOf(trans);
	}

	// Token: 0x06000187 RID: 391 RVA: 0x0000A630 File Offset: 0x00008830
	public void AddChild(Transform trans)
	{
		this.AddChild(trans, true);
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0000A63C File Offset: 0x0000883C
	public void AddChild(Transform trans, bool sort)
	{
		if (trans != null)
		{
			BetterList<Transform> childList = this.GetChildList();
			childList.Add(trans);
			this.ResetPosition(childList);
		}
	}

	// Token: 0x06000189 RID: 393 RVA: 0x0000A66C File Offset: 0x0000886C
	public void AddChild(Transform trans, int index)
	{
		if (trans != null)
		{
			if (this.sorting != UIGrid.Sorting.None)
			{
				Debug.LogWarning("The Grid has sorting enabled, so AddChild at index may not work as expected.", this);
			}
			BetterList<Transform> childList = this.GetChildList();
			childList.Insert(index, trans);
			this.ResetPosition(childList);
		}
	}

	// Token: 0x0600018A RID: 394 RVA: 0x0000A6B4 File Offset: 0x000088B4
	public Transform RemoveChild(int index)
	{
		BetterList<Transform> childList = this.GetChildList();
		if (index < childList.size)
		{
			Transform result = childList[index];
			childList.RemoveAt(index);
			this.ResetPosition(childList);
			return result;
		}
		return null;
	}

	// Token: 0x0600018B RID: 395 RVA: 0x0000A6F0 File Offset: 0x000088F0
	public bool RemoveChild(Transform t)
	{
		BetterList<Transform> childList = this.GetChildList();
		if (childList.Remove(t))
		{
			this.ResetPosition(childList);
			return true;
		}
		return false;
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0000A71C File Offset: 0x0000891C
	protected virtual void Init()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0000A738 File Offset: 0x00008938
	protected virtual void Start()
	{
		if (!this.mInitDone)
		{
			this.Init();
		}
		bool flag = this.animateSmoothly;
		this.animateSmoothly = false;
		this.Reposition();
		this.animateSmoothly = flag;
		base.enabled = false;
	}

	// Token: 0x0600018E RID: 398 RVA: 0x0000A778 File Offset: 0x00008978
	protected virtual void Update()
	{
		if (this.mReposition)
		{
			this.Reposition();
		}
		base.enabled = false;
	}

	// Token: 0x0600018F RID: 399 RVA: 0x0000A794 File Offset: 0x00008994
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x06000190 RID: 400 RVA: 0x0000A7A8 File Offset: 0x000089A8
	public static int SortHorizontal(Transform a, Transform b)
	{
		return a.localPosition.x.CompareTo(b.localPosition.x);
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0000A7D8 File Offset: 0x000089D8
	public static int SortVertical(Transform a, Transform b)
	{
		return b.localPosition.y.CompareTo(a.localPosition.y);
	}

	// Token: 0x06000192 RID: 402 RVA: 0x0000A808 File Offset: 0x00008A08
	protected virtual void Sort(BetterList<Transform> list)
	{
	}

	// Token: 0x06000193 RID: 403 RVA: 0x0000A80C File Offset: 0x00008A0C
	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !this.mInitDone && NGUITools.GetActive(this))
		{
			this.mReposition = true;
			return;
		}
		if (this.sorted)
		{
			this.sorted = false;
			if (this.sorting == UIGrid.Sorting.None)
			{
				this.sorting = UIGrid.Sorting.Alphabetic;
			}
			NGUITools.SetDirty(this);
		}
		if (!this.mInitDone)
		{
			this.Init();
		}
		BetterList<Transform> childList = this.GetChildList();
		if (this.sorting != UIGrid.Sorting.None)
		{
			if (this.sorting == UIGrid.Sorting.Alphabetic)
			{
				childList.Sort(new BetterList<Transform>.CompareFunc(UIGrid.SortByName));
			}
			else if (this.sorting == UIGrid.Sorting.Horizontal)
			{
				childList.Sort(new BetterList<Transform>.CompareFunc(UIGrid.SortHorizontal));
			}
			else if (this.sorting == UIGrid.Sorting.Vertical)
			{
				childList.Sort(new BetterList<Transform>.CompareFunc(UIGrid.SortVertical));
			}
			else if (this.onCustomSort != null)
			{
				childList.Sort(this.onCustomSort);
			}
			else
			{
				this.Sort(childList);
			}
		}
		this.ResetPosition(childList);
		if (this.keepWithinPanel)
		{
			this.ConstrainWithinPanel();
		}
		if (this.onReposition != null)
		{
			this.onReposition();
		}
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0000A948 File Offset: 0x00008B48
	public void NowReposition()
	{
		if (Application.isPlaying && !this.mInitDone && NGUITools.GetActive(this))
		{
			this.mReposition = true;
		}
		if (this.sorted)
		{
			this.sorted = false;
			if (this.sorting == UIGrid.Sorting.None)
			{
				this.sorting = UIGrid.Sorting.Alphabetic;
			}
			NGUITools.SetDirty(this);
		}
		if (!this.mInitDone)
		{
			this.Init();
		}
		BetterList<Transform> childList = this.GetChildList();
		if (this.sorting != UIGrid.Sorting.None)
		{
			if (this.sorting == UIGrid.Sorting.Alphabetic)
			{
				childList.Sort(new BetterList<Transform>.CompareFunc(UIGrid.SortByName));
			}
			else if (this.sorting == UIGrid.Sorting.Horizontal)
			{
				childList.Sort(new BetterList<Transform>.CompareFunc(UIGrid.SortHorizontal));
			}
			else if (this.sorting == UIGrid.Sorting.Vertical)
			{
				childList.Sort(new BetterList<Transform>.CompareFunc(UIGrid.SortVertical));
			}
			else if (this.onCustomSort != null)
			{
				childList.Sort(this.onCustomSort);
			}
			else
			{
				this.Sort(childList);
			}
		}
		this.ResetPosition(childList);
		if (this.keepWithinPanel)
		{
			this.ConstrainWithinPanel();
		}
		if (this.onReposition != null)
		{
			this.onReposition();
		}
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0000AA84 File Offset: 0x00008C84
	public void ConstrainWithinPanel()
	{
		if (this.mPanel != null)
		{
			this.mPanel.ConstrainTargetToBounds(base.transform, true);
		}
	}

	// Token: 0x06000196 RID: 406 RVA: 0x0000AAB8 File Offset: 0x00008CB8
	protected void ResetPosition(BetterList<Transform> list)
	{
		this.mReposition = false;
		int i = 0;
		int size = list.size;
		while (i < size)
		{
			list[i].parent = null;
			i++;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		Transform transform = base.transform;
		int j = 0;
		int size2 = list.size;
		while (j < size2)
		{
			Transform transform2 = list[j];
			transform2.parent = transform;
			float z = transform2.localPosition.z;
			Vector3 vector = (this.arrangement != UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z) : new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z);
			if (this.animateSmoothly && Application.isPlaying)
			{
				SpringPosition.Begin(transform2.gameObject, vector, 15f).updateScrollView = true;
			}
			else
			{
				transform2.localPosition = vector;
			}
			num3 = Mathf.Max(num3, num);
			num4 = Mathf.Max(num4, num2);
			if (++num >= this.maxPerLine && this.maxPerLine > 0)
			{
				num = 0;
				num2++;
			}
			j++;
		}
		if (this.pivot != UIWidget.Pivot.TopLeft)
		{
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.pivot);
			float num5;
			float num6;
			if (this.arrangement == UIGrid.Arrangement.Horizontal)
			{
				num5 = Mathf.Lerp(0f, (float)num3 * this.cellWidth, pivotOffset.x);
				num6 = Mathf.Lerp((float)(-(float)num4) * this.cellHeight, 0f, pivotOffset.y);
			}
			else
			{
				num5 = Mathf.Lerp(0f, (float)num4 * this.cellWidth, pivotOffset.x);
				num6 = Mathf.Lerp((float)(-(float)num3) * this.cellHeight, 0f, pivotOffset.y);
			}
			for (int k = 0; k < transform.childCount; k++)
			{
				Transform child = transform.GetChild(k);
				SpringPosition component = child.GetComponent<SpringPosition>();
				if (component != null)
				{
					SpringPosition springPosition = component;
					springPosition.target.x = springPosition.target.x - num5;
					SpringPosition springPosition2 = component;
					springPosition2.target.y = springPosition2.target.y - num6;
				}
				else
				{
					Vector3 localPosition = child.localPosition;
					localPosition.x -= num5;
					localPosition.y -= num6;
					child.localPosition = localPosition;
				}
			}
		}
	}

	// Token: 0x040001B3 RID: 435
	public UIGrid.Arrangement arrangement;

	// Token: 0x040001B4 RID: 436
	public UIGrid.Sorting sorting;

	// Token: 0x040001B5 RID: 437
	public UIWidget.Pivot pivot;

	// Token: 0x040001B6 RID: 438
	public int maxPerLine;

	// Token: 0x040001B7 RID: 439
	public float cellWidth = 200f;

	// Token: 0x040001B8 RID: 440
	public float cellHeight = 200f;

	// Token: 0x040001B9 RID: 441
	public bool animateSmoothly;

	// Token: 0x040001BA RID: 442
	public bool hideInactive = true;

	// Token: 0x040001BB RID: 443
	public bool keepWithinPanel;

	// Token: 0x040001BC RID: 444
	public UIGrid.OnReposition onReposition;

	// Token: 0x040001BD RID: 445
	public BetterList<Transform>.CompareFunc onCustomSort;

	// Token: 0x040001BE RID: 446
	[SerializeField]
	[HideInInspector]
	private bool sorted;

	// Token: 0x040001BF RID: 447
	protected bool mReposition;

	// Token: 0x040001C0 RID: 448
	protected UIPanel mPanel;

	// Token: 0x040001C1 RID: 449
	protected bool mInitDone;

	// Token: 0x040001C2 RID: 450
	public AnimationCurve PosAnimationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x040001C3 RID: 451
	public AnimationCurve AlphaAnimationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x02000059 RID: 89
	public enum Arrangement
	{
		// Token: 0x040001C5 RID: 453
		Horizontal,
		// Token: 0x040001C6 RID: 454
		Vertical
	}

	// Token: 0x0200005A RID: 90
	public enum Sorting
	{
		// Token: 0x040001C8 RID: 456
		None,
		// Token: 0x040001C9 RID: 457
		Alphabetic,
		// Token: 0x040001CA RID: 458
		Horizontal,
		// Token: 0x040001CB RID: 459
		Vertical,
		// Token: 0x040001CC RID: 460
		Custom
	}

	// Token: 0x02000A95 RID: 2709
	// (Invoke) Token: 0x06004EDD RID: 20189
	public delegate void OnReposition();
}
