using System;
using UnityEngine;

// Token: 0x020008C3 RID: 2243
public class UIGridNew : UIWidgetContainer
{
	// Token: 0x17000F81 RID: 3969
	// (set) Token: 0x06003C71 RID: 15473 RVA: 0x00109238 File Offset: 0x00107438
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

	// Token: 0x06003C72 RID: 15474 RVA: 0x00109250 File Offset: 0x00107450
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

	// Token: 0x06003C73 RID: 15475 RVA: 0x001092B8 File Offset: 0x001074B8
	public Transform GetChild(int index)
	{
		BetterList<Transform> childList = this.GetChildList();
		return (index >= childList.size) ? null : childList[index];
	}

	// Token: 0x06003C74 RID: 15476 RVA: 0x001092E8 File Offset: 0x001074E8
	public int GetIndex(Transform trans)
	{
		return this.GetChildList().IndexOf(trans);
	}

	// Token: 0x06003C75 RID: 15477 RVA: 0x001092F8 File Offset: 0x001074F8
	public void AddChild(Transform trans)
	{
		this.AddChild(trans, true);
	}

	// Token: 0x06003C76 RID: 15478 RVA: 0x00109304 File Offset: 0x00107504
	public void AddChild(Transform trans, bool sort)
	{
		if (trans != null)
		{
			BetterList<Transform> childList = this.GetChildList();
			childList.Add(trans);
			this.ResetPosition(childList);
		}
	}

	// Token: 0x06003C77 RID: 15479 RVA: 0x00109334 File Offset: 0x00107534
	public void AddChild(Transform trans, int index)
	{
		if (trans != null)
		{
			if (this.sorting != UIGridNew.Sorting.None)
			{
				Debug.LogWarning("The Grid has sorting enabled, so AddChild at index may not work as expected.", this);
			}
			BetterList<Transform> childList = this.GetChildList();
			childList.Insert(index, trans);
			this.ResetPosition(childList);
		}
	}

	// Token: 0x06003C78 RID: 15480 RVA: 0x0010937C File Offset: 0x0010757C
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

	// Token: 0x06003C79 RID: 15481 RVA: 0x001093B8 File Offset: 0x001075B8
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

	// Token: 0x06003C7A RID: 15482 RVA: 0x001093E4 File Offset: 0x001075E4
	protected virtual void Init()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	// Token: 0x06003C7B RID: 15483 RVA: 0x00109400 File Offset: 0x00107600
	public void InitSelf()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	// Token: 0x06003C7C RID: 15484 RVA: 0x0010941C File Offset: 0x0010761C
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

	// Token: 0x06003C7D RID: 15485 RVA: 0x0010945C File Offset: 0x0010765C
	protected virtual void Update()
	{
		if (this.mReposition)
		{
			this.Reposition();
		}
		base.enabled = false;
	}

	// Token: 0x06003C7E RID: 15486 RVA: 0x00109478 File Offset: 0x00107678
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x06003C7F RID: 15487 RVA: 0x0010948C File Offset: 0x0010768C
	public static int SortHorizontal(Transform a, Transform b)
	{
		return a.localPosition.x.CompareTo(b.localPosition.x);
	}

	// Token: 0x06003C80 RID: 15488 RVA: 0x001094BC File Offset: 0x001076BC
	public static int SortVertical(Transform a, Transform b)
	{
		return b.localPosition.y.CompareTo(a.localPosition.y);
	}

	// Token: 0x06003C81 RID: 15489 RVA: 0x001094EC File Offset: 0x001076EC
	protected virtual void Sort(BetterList<Transform> list)
	{
	}

	// Token: 0x06003C82 RID: 15490 RVA: 0x001094F0 File Offset: 0x001076F0
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
			if (this.sorting == UIGridNew.Sorting.None)
			{
				this.sorting = UIGridNew.Sorting.Alphabetic;
			}
			NGUITools.SetDirty(this);
		}
		if (!this.mInitDone)
		{
			this.Init();
		}
		BetterList<Transform> childList = this.GetChildList();
		if (this.sorting != UIGridNew.Sorting.None)
		{
			if (this.sorting == UIGridNew.Sorting.Alphabetic)
			{
				childList.Sort(new BetterList<Transform>.CompareFunc(UIGridNew.SortByName));
			}
			else if (this.sorting == UIGridNew.Sorting.Horizontal)
			{
				childList.Sort(new BetterList<Transform>.CompareFunc(UIGridNew.SortHorizontal));
			}
			else if (this.sorting == UIGridNew.Sorting.Vertical)
			{
				childList.Sort(new BetterList<Transform>.CompareFunc(UIGridNew.SortVertical));
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

	// Token: 0x06003C83 RID: 15491 RVA: 0x0010962C File Offset: 0x0010782C
	public void NowReposition()
	{
		if (Application.isPlaying && !this.mInitDone && NGUITools.GetActive(this))
		{
			this.mReposition = true;
		}
		if (this.sorted)
		{
			this.sorted = false;
			if (this.sorting == UIGridNew.Sorting.None)
			{
				this.sorting = UIGridNew.Sorting.Alphabetic;
			}
			NGUITools.SetDirty(this);
		}
		if (!this.mInitDone)
		{
			this.Init();
		}
		BetterList<Transform> childList = this.GetChildList();
		if (this.sorting != UIGridNew.Sorting.None)
		{
			if (this.sorting == UIGridNew.Sorting.Alphabetic)
			{
				childList.Sort(new BetterList<Transform>.CompareFunc(UIGridNew.SortByName));
			}
			else if (this.sorting == UIGridNew.Sorting.Horizontal)
			{
				childList.Sort(new BetterList<Transform>.CompareFunc(UIGridNew.SortHorizontal));
			}
			else if (this.sorting == UIGridNew.Sorting.Vertical)
			{
				childList.Sort(new BetterList<Transform>.CompareFunc(UIGridNew.SortVertical));
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

	// Token: 0x06003C84 RID: 15492 RVA: 0x00109768 File Offset: 0x00107968
	public void ConstrainWithinPanel()
	{
		if (this.mPanel != null)
		{
			this.mPanel.ConstrainTargetToBounds(base.transform, true);
		}
	}

	// Token: 0x06003C85 RID: 15493 RVA: 0x0010979C File Offset: 0x0010799C
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
			Vector3 vector = (this.arrangement != UIGridNew.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z) : new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z);
			if (this.animateSmoothly && Application.isPlaying)
			{
				SpringPosition.Begin(transform2.gameObject, vector, 15f).updateScrollView = true;
			}
			else
			{
				transform2.localPosition = vector;
				UIWidget component = transform2.GetComponent<UIWidget>();
				if (component != null && component.alpha == 0f)
				{
					component.alpha = 1f;
				}
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
			if (this.arrangement == UIGridNew.Arrangement.Horizontal)
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
				SpringPosition component2 = child.GetComponent<SpringPosition>();
				if (component2 != null)
				{
					SpringPosition springPosition = component2;
					springPosition.target.x = springPosition.target.x - num5;
					SpringPosition springPosition2 = component2;
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

	// Token: 0x06003C86 RID: 15494 RVA: 0x00109A54 File Offset: 0x00107C54
	public void OpenGrid()
	{
		BetterList<Transform> childList = this.GetChildList();
		this.ResetPositionAnima(childList, true);
	}

	// Token: 0x06003C87 RID: 15495 RVA: 0x00109A70 File Offset: 0x00107C70
	public void CloseGrid()
	{
		BetterList<Transform> childList = this.GetChildList();
		this.ResetPositionAnima(childList, false);
	}

	// Token: 0x06003C88 RID: 15496 RVA: 0x00109A8C File Offset: 0x00107C8C
	protected void ResetPositionAnima(BetterList<Transform> list, bool isOpen)
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
			Vector3 pos = Vector3.zero;
			float alpha = 0f;
			if (isOpen)
			{
				transform2.localPosition = Vector3.zero;
				float z = transform2.localPosition.z;
				pos = ((this.arrangement != UIGridNew.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z) : new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z));
				alpha = 1f;
			}
			if (Application.isPlaying)
			{
				UITweener uitweener = TweenPosition.Begin(transform2.gameObject, this.Duration, pos);
				uitweener.animationCurve = this.PosAnimationCurve;
				UITweener uitweener2 = TweenAlpha.Begin(transform2.gameObject, this.Duration, alpha);
				uitweener2.animationCurve = this.AlphaAnimationCurve;
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
		if (this.pivot != UIWidget.Pivot.TopLeft && isOpen)
		{
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.pivot);
			float num5;
			float num6;
			if (this.arrangement == UIGridNew.Arrangement.Horizontal)
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
				TweenPosition component = child.GetComponent<TweenPosition>();
				if (component != null)
				{
					TweenPosition tweenPosition = component;
					tweenPosition.to.x = tweenPosition.to.x - num5;
					TweenPosition tweenPosition2 = component;
					tweenPosition2.to.y = tweenPosition2.to.y - num6;
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

	// Token: 0x040027AE RID: 10158
	public UIGridNew.Arrangement arrangement;

	// Token: 0x040027AF RID: 10159
	public UIGridNew.Sorting sorting;

	// Token: 0x040027B0 RID: 10160
	public UIWidget.Pivot pivot;

	// Token: 0x040027B1 RID: 10161
	public int maxPerLine;

	// Token: 0x040027B2 RID: 10162
	public float cellWidth = 200f;

	// Token: 0x040027B3 RID: 10163
	public float cellHeight = 200f;

	// Token: 0x040027B4 RID: 10164
	public bool animateSmoothly;

	// Token: 0x040027B5 RID: 10165
	public bool hideInactive = true;

	// Token: 0x040027B6 RID: 10166
	public bool keepWithinPanel;

	// Token: 0x040027B7 RID: 10167
	public UIGridNew.OnReposition onReposition;

	// Token: 0x040027B8 RID: 10168
	public BetterList<Transform>.CompareFunc onCustomSort;

	// Token: 0x040027B9 RID: 10169
	[SerializeField]
	[HideInInspector]
	private bool sorted;

	// Token: 0x040027BA RID: 10170
	protected bool mReposition;

	// Token: 0x040027BB RID: 10171
	protected UIPanel mPanel;

	// Token: 0x040027BC RID: 10172
	protected bool mInitDone;

	// Token: 0x040027BD RID: 10173
	public AnimationCurve PosAnimationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x040027BE RID: 10174
	public AnimationCurve AlphaAnimationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x040027BF RID: 10175
	public float Duration = 0.3f;

	// Token: 0x020008C4 RID: 2244
	public enum Arrangement
	{
		// Token: 0x040027C1 RID: 10177
		Horizontal,
		// Token: 0x040027C2 RID: 10178
		Vertical
	}

	// Token: 0x020008C5 RID: 2245
	public enum Sorting
	{
		// Token: 0x040027C4 RID: 10180
		None,
		// Token: 0x040027C5 RID: 10181
		Alphabetic,
		// Token: 0x040027C6 RID: 10182
		Horizontal,
		// Token: 0x040027C7 RID: 10183
		Vertical,
		// Token: 0x040027C8 RID: 10184
		Custom
	}

	// Token: 0x02000AE9 RID: 2793
	// (Invoke) Token: 0x0600502D RID: 20525
	public delegate void OnReposition();
}
