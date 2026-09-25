using System;
using UnityEngine;

// Token: 0x020008C0 RID: 2240
public class UIDragItemEx : MonoBehaviour
{
	// Token: 0x06003C5F RID: 15455 RVA: 0x00108748 File Offset: 0x00106948
	protected virtual void Start()
	{
		this.mTrans = base.transform;
		this.mCollider = base.collider;
		this.mButton = base.GetComponent<UIButton>();
		this.mDragScrollView = base.GetComponent<UIDragScrollView>();
	}

	// Token: 0x06003C60 RID: 15456 RVA: 0x00108788 File Offset: 0x00106988
	private void OnPress(bool isPressed)
	{
		if (isPressed)
		{
			this.mPressTime = RealTime.time;
		}
	}

	// Token: 0x06003C61 RID: 15457 RVA: 0x0010879C File Offset: 0x0010699C
	private void OnDragStart()
	{
		if (!base.enabled || this.mTouchID != -2147483648)
		{
			return;
		}
		if (this.restriction != UIDragItemEx.Restriction.None)
		{
			if (this.restriction == UIDragItemEx.Restriction.Horizontal)
			{
				Vector2 totalDelta = UICamera.currentTouch.totalDelta;
				if (Mathf.Abs(totalDelta.x) < Mathf.Abs(totalDelta.y))
				{
					return;
				}
			}
			else if (this.restriction == UIDragItemEx.Restriction.Vertical)
			{
				Vector2 totalDelta2 = UICamera.currentTouch.totalDelta;
				if (Mathf.Abs(totalDelta2.x) > Mathf.Abs(totalDelta2.y))
				{
					return;
				}
			}
			else if (this.restriction == UIDragItemEx.Restriction.PressAndHold && this.mPressTime + this.pressAndHoldDelay > RealTime.time)
			{
				return;
			}
		}
		if (this.cloneOnDrag)
		{
			GameObject gameObject = NGUITools.AddChild(base.transform.parent.gameObject, base.gameObject);
			gameObject.transform.localPosition = base.transform.localPosition;
			gameObject.transform.localRotation = base.transform.localRotation;
			gameObject.transform.localScale = base.transform.localScale;
			UIButtonColor component = gameObject.GetComponent<UIButtonColor>();
			if (component != null)
			{
				component.defaultColor = base.GetComponent<UIButtonColor>().defaultColor;
			}
			UICamera.currentTouch.dragged = gameObject;
			UIDragItemEx component2 = gameObject.GetComponent<UIDragItemEx>();
			component2.Start();
			component2.OnDragDropStart(base.gameObject);
		}
		else
		{
			this.OnDragDropStart(base.gameObject);
		}
	}

	// Token: 0x06003C62 RID: 15458 RVA: 0x00108930 File Offset: 0x00106B30
	private void OnDrag(Vector2 delta)
	{
		if (!base.enabled || this.mTouchID != UICamera.currentTouchID)
		{
			return;
		}
		this.OnDragDropMove(delta * this.mRoot.pixelSizeAdjustment);
	}

	// Token: 0x06003C63 RID: 15459 RVA: 0x00108978 File Offset: 0x00106B78
	private void OnDragEnd()
	{
		if (!base.enabled || this.mTouchID != UICamera.currentTouchID)
		{
			return;
		}
		this.OnDragDropRelease(UICamera.hoveredObject);
	}

	// Token: 0x06003C64 RID: 15460 RVA: 0x001089A4 File Offset: 0x00106BA4
	protected virtual void OnDragDropStart(GameObject OrginDrag = null)
	{
		if (this.mDragScrollView != null)
		{
			this.mDragScrollView.enabled = false;
		}
		if (this.mButton != null)
		{
			this.mButton.isEnabled = false;
		}
		else if (this.mCollider != null)
		{
			this.mCollider.enabled = false;
		}
		this.mTouchID = UICamera.currentTouchID;
		this.mParent = this.mTrans.parent;
		this.mRoot = NGUITools.FindInParents<UIRoot>(this.mParent);
		this.mGrid = NGUITools.FindInParents<UIGrid>(this.mParent);
		this.mTable = NGUITools.FindInParents<UITable>(this.mParent);
		if (UIDragDropRoot.root != null)
		{
			this.mTrans.parent = UIDragDropRoot.root;
		}
		Vector3 localPosition = this.mTrans.localPosition;
		localPosition.z = 0f;
		this.mTrans.localPosition = localPosition;
		TweenPosition component = base.GetComponent<TweenPosition>();
		if (component != null)
		{
			component.enabled = false;
		}
		SpringPosition component2 = base.GetComponent<SpringPosition>();
		if (component2 != null)
		{
			component2.enabled = false;
		}
		NGUITools.MarkParentAsChanged(base.gameObject);
		if (this.mTable != null)
		{
			this.mTable.repositionNow = true;
		}
		if (this.mGrid != null)
		{
			this.mGrid.repositionNow = true;
		}
	}

	// Token: 0x06003C65 RID: 15461 RVA: 0x00108B1C File Offset: 0x00106D1C
	protected virtual void OnDragDropMove(Vector3 delta)
	{
		this.mTrans.localPosition += delta;
	}

	// Token: 0x06003C66 RID: 15462 RVA: 0x00108B38 File Offset: 0x00106D38
	protected virtual void OnDragDropRelease(GameObject surface)
	{
		if (!this.cloneOnDrag)
		{
			this.mTouchID = int.MinValue;
			if (this.mButton != null)
			{
				this.mButton.isEnabled = true;
			}
			else if (this.mCollider != null)
			{
				this.mCollider.enabled = true;
			}
			UIDragDropContainer uidragDropContainer = (!surface) ? null : NGUITools.FindInParents<UIDragDropContainer>(surface);
			if (uidragDropContainer != null)
			{
				this.mTrans.parent = ((!(uidragDropContainer.reparentTarget != null)) ? uidragDropContainer.transform : uidragDropContainer.reparentTarget);
				Vector3 localPosition = this.mTrans.localPosition;
				localPosition.z = 0f;
				this.mTrans.localPosition = localPosition;
			}
			else
			{
				this.mTrans.parent = this.mParent;
			}
			this.mParent = this.mTrans.parent;
			this.mGrid = NGUITools.FindInParents<UIGrid>(this.mParent);
			this.mTable = NGUITools.FindInParents<UITable>(this.mParent);
			if (this.mDragScrollView != null)
			{
				this.mDragScrollView.enabled = true;
			}
			NGUITools.MarkParentAsChanged(base.gameObject);
			if (this.mTable != null)
			{
				this.mTable.repositionNow = true;
			}
			if (this.mGrid != null)
			{
				this.mGrid.repositionNow = true;
			}
		}
		else
		{
			NGUITools.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400278B RID: 10123
	public UIDragItemEx.Restriction restriction;

	// Token: 0x0400278C RID: 10124
	public bool cloneOnDrag;

	// Token: 0x0400278D RID: 10125
	[HideInInspector]
	public float pressAndHoldDelay = 1f;

	// Token: 0x0400278E RID: 10126
	protected Transform mTrans;

	// Token: 0x0400278F RID: 10127
	protected Transform mParent;

	// Token: 0x04002790 RID: 10128
	protected Collider mCollider;

	// Token: 0x04002791 RID: 10129
	protected UIButton mButton;

	// Token: 0x04002792 RID: 10130
	protected UIRoot mRoot;

	// Token: 0x04002793 RID: 10131
	protected UIGrid mGrid;

	// Token: 0x04002794 RID: 10132
	protected UITable mTable;

	// Token: 0x04002795 RID: 10133
	protected int mTouchID = int.MinValue;

	// Token: 0x04002796 RID: 10134
	protected float mPressTime;

	// Token: 0x04002797 RID: 10135
	protected UIDragScrollView mDragScrollView;

	// Token: 0x020008C1 RID: 2241
	public enum Restriction
	{
		// Token: 0x04002799 RID: 10137
		None,
		// Token: 0x0400279A RID: 10138
		Horizontal,
		// Token: 0x0400279B RID: 10139
		Vertical,
		// Token: 0x0400279C RID: 10140
		PressAndHold
	}
}
