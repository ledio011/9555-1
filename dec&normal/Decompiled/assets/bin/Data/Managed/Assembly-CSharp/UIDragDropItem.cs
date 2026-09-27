using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
[AddComponentMenu("NGUI/Interaction/Drag and Drop Item")]
public class UIDragDropItem : MonoBehaviour
{
	// Token: 0x06000141 RID: 321 RVA: 0x000088C4 File Offset: 0x00006AC4
	protected virtual void Start()
	{
		this.mTrans = base.transform;
		this.mCollider = base.collider;
		this.mButton = base.GetComponent<UIButton>();
		this.mDragScrollView = base.GetComponent<UIDragScrollView>();
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00008904 File Offset: 0x00006B04
	private void OnPress(bool isPressed)
	{
		if (isPressed)
		{
			this.mPressTime = RealTime.time;
		}
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00008918 File Offset: 0x00006B18
	private void OnDragStart()
	{
		if (!base.enabled || this.mTouchID != -2147483648)
		{
			return;
		}
		if (this.restriction != UIDragDropItem.Restriction.None)
		{
			if (this.restriction == UIDragDropItem.Restriction.Horizontal)
			{
				Vector2 totalDelta = UICamera.currentTouch.totalDelta;
				if (Mathf.Abs(totalDelta.x) < Mathf.Abs(totalDelta.y))
				{
					return;
				}
			}
			else if (this.restriction == UIDragDropItem.Restriction.Vertical)
			{
				Vector2 totalDelta2 = UICamera.currentTouch.totalDelta;
				if (Mathf.Abs(totalDelta2.x) > Mathf.Abs(totalDelta2.y))
				{
					return;
				}
			}
			else if (this.restriction == UIDragDropItem.Restriction.PressAndHold && this.mPressTime + this.pressAndHoldDelay > RealTime.time)
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
			UIDragDropItem component2 = gameObject.GetComponent<UIDragDropItem>();
			component2.Start();
			component2.OnDragDropStart();
		}
		else
		{
			this.OnDragDropStart();
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00008AA0 File Offset: 0x00006CA0
	private void OnDrag(Vector2 delta)
	{
		if (!base.enabled || this.mTouchID != UICamera.currentTouchID)
		{
			return;
		}
		this.OnDragDropMove(delta * this.mRoot.pixelSizeAdjustment);
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00008AE8 File Offset: 0x00006CE8
	private void OnDragEnd()
	{
		if (!base.enabled || this.mTouchID != UICamera.currentTouchID)
		{
			return;
		}
		this.OnDragDropRelease(UICamera.hoveredObject);
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00008B14 File Offset: 0x00006D14
	protected virtual void OnDragDropStart()
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

	// Token: 0x06000147 RID: 327 RVA: 0x00008C8C File Offset: 0x00006E8C
	protected virtual void OnDragDropMove(Vector3 delta)
	{
		this.mTrans.localPosition += delta;
	}

	// Token: 0x06000148 RID: 328 RVA: 0x00008CA8 File Offset: 0x00006EA8
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

	// Token: 0x04000155 RID: 341
	public UIDragDropItem.Restriction restriction;

	// Token: 0x04000156 RID: 342
	public bool cloneOnDrag;

	// Token: 0x04000157 RID: 343
	[HideInInspector]
	public float pressAndHoldDelay = 1f;

	// Token: 0x04000158 RID: 344
	protected Transform mTrans;

	// Token: 0x04000159 RID: 345
	protected Transform mParent;

	// Token: 0x0400015A RID: 346
	protected Collider mCollider;

	// Token: 0x0400015B RID: 347
	protected UIButton mButton;

	// Token: 0x0400015C RID: 348
	protected UIRoot mRoot;

	// Token: 0x0400015D RID: 349
	protected UIGrid mGrid;

	// Token: 0x0400015E RID: 350
	protected UITable mTable;

	// Token: 0x0400015F RID: 351
	protected int mTouchID = int.MinValue;

	// Token: 0x04000160 RID: 352
	protected float mPressTime;

	// Token: 0x04000161 RID: 353
	protected UIDragScrollView mDragScrollView;

	// Token: 0x0200004F RID: 79
	public enum Restriction
	{
		// Token: 0x04000163 RID: 355
		None,
		// Token: 0x04000164 RID: 356
		Horizontal,
		// Token: 0x04000165 RID: 357
		Vertical,
		// Token: 0x04000166 RID: 358
		PressAndHold
	}
}
