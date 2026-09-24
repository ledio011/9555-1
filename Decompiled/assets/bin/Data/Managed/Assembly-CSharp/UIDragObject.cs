using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Object")]
public class UIDragObject : MonoBehaviour
{
	// Token: 0x17000017 RID: 23
	// (get) Token: 0x0600014D RID: 333 RVA: 0x00008ED4 File Offset: 0x000070D4
	// (set) Token: 0x0600014E RID: 334 RVA: 0x00008EDC File Offset: 0x000070DC
	public Vector3 dragMovement
	{
		get
		{
			return this.scale;
		}
		set
		{
			this.scale = value;
		}
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00008EE8 File Offset: 0x000070E8
	private void OnEnable()
	{
		if (this.scrollWheelFactor != 0f)
		{
			this.scrollMomentum = this.scale * this.scrollWheelFactor;
			this.scrollWheelFactor = 0f;
		}
		if (this.contentRect == null && this.target != null && Application.isPlaying)
		{
			UIWidget component = this.target.GetComponent<UIWidget>();
			if (component != null)
			{
				this.contentRect = component;
			}
		}
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00008F74 File Offset: 0x00007174
	private void OnDisable()
	{
		this.mStarted = false;
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00008F80 File Offset: 0x00007180
	private void FindPanel()
	{
		this.mPanel = ((!(this.target != null)) ? null : UIPanel.Find(this.target.transform.parent));
		if (this.mPanel == null)
		{
			this.restrictWithinPanel = false;
		}
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00008FD8 File Offset: 0x000071D8
	private void UpdateBounds()
	{
		if (this.contentRect)
		{
			Transform cachedTransform = this.mPanel.cachedTransform;
			Matrix4x4 worldToLocalMatrix = cachedTransform.worldToLocalMatrix;
			Vector3[] worldCorners = this.contentRect.worldCorners;
			for (int i = 0; i < 4; i++)
			{
				worldCorners[i] = worldToLocalMatrix.MultiplyPoint3x4(worldCorners[i]);
			}
			this.mBounds = new Bounds(worldCorners[0], Vector3.zero);
			for (int j = 1; j < 4; j++)
			{
				this.mBounds.Encapsulate(worldCorners[j]);
			}
		}
		else
		{
			this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mPanel.cachedTransform, this.target);
		}
	}

	// Token: 0x06000153 RID: 339 RVA: 0x000090B0 File Offset: 0x000072B0
	private void OnPress(bool pressed)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.target != null)
		{
			if (pressed)
			{
				if (!this.mPressed)
				{
					this.mTouchID = UICamera.currentTouchID;
					this.mPressed = true;
					this.mStarted = false;
					this.CancelMovement();
					if (this.restrictWithinPanel && this.mPanel == null)
					{
						this.FindPanel();
					}
					if (this.restrictWithinPanel)
					{
						this.UpdateBounds();
					}
					this.CancelSpring();
					Transform transform = UICamera.currentCamera.transform;
					this.mPlane = new Plane(((!(this.mPanel != null)) ? transform.rotation : this.mPanel.cachedTransform.rotation) * Vector3.back, UICamera.lastHit.point);
				}
			}
			else if (this.mPressed && this.mTouchID == UICamera.currentTouchID)
			{
				this.mPressed = false;
				if (this.restrictWithinPanel && this.dragEffect == UIDragObject.DragEffect.MomentumAndSpring && this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, false))
				{
					this.CancelMovement();
				}
			}
		}
	}

	// Token: 0x06000154 RID: 340 RVA: 0x0000920C File Offset: 0x0000740C
	private void OnDrag(Vector2 delta)
	{
		if (this.mPressed && this.mTouchID == UICamera.currentTouchID && base.enabled && NGUITools.GetActive(base.gameObject) && this.target != null)
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
			float num = 0f;
			if (this.mPlane.Raycast(ray, ref num))
			{
				Vector3 point = ray.GetPoint(num);
				Vector3 vector = point - this.mLastPos;
				this.mLastPos = point;
				if (!this.mStarted)
				{
					this.mStarted = true;
					vector = Vector3.zero;
				}
				if (vector.x != 0f || vector.y != 0f)
				{
					vector = this.target.InverseTransformDirection(vector);
					vector.Scale(this.scale);
					vector = this.target.TransformDirection(vector);
				}
				if (this.dragEffect != UIDragObject.DragEffect.None)
				{
					this.mMomentum = Vector3.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
				}
				Vector3 localPosition = this.target.localPosition;
				this.Move(vector);
				if (this.restrictWithinPanel)
				{
					this.mBounds.center = this.mBounds.center + (this.target.localPosition - localPosition);
					if (this.dragEffect != UIDragObject.DragEffect.MomentumAndSpring && this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, true))
					{
						this.CancelMovement();
					}
				}
			}
		}
	}

	// Token: 0x06000155 RID: 341 RVA: 0x000093D8 File Offset: 0x000075D8
	private void Move(Vector3 worldDelta)
	{
		if (this.mPanel != null)
		{
			this.mTargetPos += worldDelta;
			this.target.position = this.mTargetPos;
			Vector3 localPosition = this.target.localPosition;
			localPosition.x = Mathf.Round(localPosition.x);
			localPosition.y = Mathf.Round(localPosition.y);
			this.target.localPosition = localPosition;
			UIScrollView component = this.mPanel.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
			}
		}
		else
		{
			this.target.position += worldDelta;
		}
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00009494 File Offset: 0x00007694
	private void LateUpdate()
	{
		if (this.target == null)
		{
			return;
		}
		float deltaTime = RealTime.deltaTime;
		this.mMomentum -= this.mScroll;
		this.mScroll = NGUIMath.SpringLerp(this.mScroll, Vector3.zero, 20f, deltaTime);
		if (!this.mPressed)
		{
			if (this.mMomentum.magnitude < 0.0001f)
			{
				return;
			}
			if (this.mPanel == null)
			{
				this.FindPanel();
			}
			this.Move(NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime));
			if (this.restrictWithinPanel && this.mPanel != null)
			{
				this.UpdateBounds();
				if (this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, this.dragEffect == UIDragObject.DragEffect.None))
				{
					this.CancelMovement();
				}
				else
				{
					this.CancelSpring();
				}
			}
		}
		else
		{
			this.mTargetPos = ((!(this.target != null)) ? Vector3.zero : this.target.position);
		}
		NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
	}

	// Token: 0x06000157 RID: 343 RVA: 0x000095D8 File Offset: 0x000077D8
	public void CancelMovement()
	{
		this.mTargetPos = ((!(this.target != null)) ? Vector3.zero : this.target.position);
		this.mMomentum = Vector3.zero;
		this.mScroll = Vector3.zero;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00009628 File Offset: 0x00007828
	public void CancelSpring()
	{
		SpringPosition component = this.target.GetComponent<SpringPosition>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00009654 File Offset: 0x00007854
	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			this.mScroll -= this.scrollMomentum * (delta * 0.05f);
		}
	}

	// Token: 0x04000168 RID: 360
	public Transform target;

	// Token: 0x04000169 RID: 361
	public Vector3 scrollMomentum = Vector3.zero;

	// Token: 0x0400016A RID: 362
	public bool restrictWithinPanel;

	// Token: 0x0400016B RID: 363
	public UIRect contentRect;

	// Token: 0x0400016C RID: 364
	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	// Token: 0x0400016D RID: 365
	public float momentumAmount = 35f;

	// Token: 0x0400016E RID: 366
	[SerializeField]
	protected Vector3 scale = new Vector3(1f, 1f, 0f);

	// Token: 0x0400016F RID: 367
	[HideInInspector]
	[SerializeField]
	private float scrollWheelFactor;

	// Token: 0x04000170 RID: 368
	private Plane mPlane;

	// Token: 0x04000171 RID: 369
	private Vector3 mTargetPos;

	// Token: 0x04000172 RID: 370
	private Vector3 mLastPos;

	// Token: 0x04000173 RID: 371
	private UIPanel mPanel;

	// Token: 0x04000174 RID: 372
	private Vector3 mMomentum = Vector3.zero;

	// Token: 0x04000175 RID: 373
	private Vector3 mScroll = Vector3.zero;

	// Token: 0x04000176 RID: 374
	private Bounds mBounds;

	// Token: 0x04000177 RID: 375
	private int mTouchID;

	// Token: 0x04000178 RID: 376
	private bool mStarted;

	// Token: 0x04000179 RID: 377
	private bool mPressed;

	// Token: 0x02000052 RID: 82
	public enum DragEffect
	{
		// Token: 0x0400017B RID: 379
		None,
		// Token: 0x0400017C RID: 380
		Momentum,
		// Token: 0x0400017D RID: 381
		MomentumAndSpring
	}
}
