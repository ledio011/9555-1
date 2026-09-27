using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Interaction/Draggable Camera")]
public class UIDraggableCamera : MonoBehaviour
{
	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000166 RID: 358 RVA: 0x00009AE4 File Offset: 0x00007CE4
	// (set) Token: 0x06000167 RID: 359 RVA: 0x00009AEC File Offset: 0x00007CEC
	public Vector2 currentMomentum
	{
		get
		{
			return this.mMomentum;
		}
		set
		{
			this.mMomentum = value;
		}
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00009AF8 File Offset: 0x00007CF8
	private void Awake()
	{
		this.mCam = base.camera;
		this.mTrans = base.transform;
		if (this.rootForBounds == null)
		{
			Debug.LogError(NGUITools.GetHierarchy(base.gameObject) + " needs the 'Root For Bounds' parameter to be set", this);
			base.enabled = false;
		}
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00009B50 File Offset: 0x00007D50
	private void Start()
	{
		this.mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00009B64 File Offset: 0x00007D64
	private Vector3 CalculateConstrainOffset()
	{
		if (this.rootForBounds == null || this.rootForBounds.childCount == 0)
		{
			return Vector3.zero;
		}
		Vector3 vector;
		vector..ctor(this.mCam.rect.xMin * (float)Screen.width, this.mCam.rect.yMin * (float)Screen.height, 0f);
		Vector3 vector2;
		vector2..ctor(this.mCam.rect.xMax * (float)Screen.width, this.mCam.rect.yMax * (float)Screen.height, 0f);
		vector = this.mCam.ScreenToWorldPoint(vector);
		vector2 = this.mCam.ScreenToWorldPoint(vector2);
		Vector2 minRect;
		minRect..ctor(this.mBounds.min.x, this.mBounds.min.y);
		Vector2 maxRect;
		maxRect..ctor(this.mBounds.max.x, this.mBounds.max.y);
		return NGUIMath.ConstrainRect(minRect, maxRect, vector, vector2);
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00009CAC File Offset: 0x00007EAC
	public bool ConstrainToBounds(bool immediate)
	{
		if (this.mTrans != null && this.rootForBounds != null)
		{
			Vector3 vector = this.CalculateConstrainOffset();
			if (vector.sqrMagnitude > 0f)
			{
				if (immediate)
				{
					this.mTrans.position -= vector;
				}
				else
				{
					SpringPosition springPosition = SpringPosition.Begin(base.gameObject, this.mTrans.position - vector, 13f);
					springPosition.ignoreTimeScale = true;
					springPosition.worldSpace = true;
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600016C RID: 364 RVA: 0x00009D48 File Offset: 0x00007F48
	public void Press(bool isPressed)
	{
		if (isPressed)
		{
			this.mDragStarted = false;
		}
		if (this.rootForBounds != null)
		{
			this.mPressed = isPressed;
			if (isPressed)
			{
				this.mBounds = NGUIMath.CalculateAbsoluteWidgetBounds(this.rootForBounds);
				this.mMomentum = Vector2.zero;
				this.mScroll = 0f;
				SpringPosition component = base.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			else if (this.dragEffect == UIDragObject.DragEffect.MomentumAndSpring)
			{
				this.ConstrainToBounds(false);
			}
		}
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00009DDC File Offset: 0x00007FDC
	public void Drag(Vector2 delta)
	{
		if (this.smoothDragStart && !this.mDragStarted)
		{
			this.mDragStarted = true;
			return;
		}
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
		if (this.mRoot != null)
		{
			delta *= this.mRoot.pixelSizeAdjustment;
		}
		Vector2 vector = Vector2.Scale(delta, -this.scale);
		this.mTrans.localPosition += vector;
		this.mMomentum = Vector2.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
		if (this.dragEffect != UIDragObject.DragEffect.MomentumAndSpring && this.ConstrainToBounds(true))
		{
			this.mMomentum = Vector2.zero;
			this.mScroll = 0f;
		}
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00009EC8 File Offset: 0x000080C8
	public void Scroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			if (Mathf.Sign(this.mScroll) != Mathf.Sign(delta))
			{
				this.mScroll = 0f;
			}
			this.mScroll += delta * this.scrollWheelFactor;
		}
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00009F28 File Offset: 0x00008128
	private void Update()
	{
		float deltaTime = RealTime.deltaTime;
		if (this.mPressed)
		{
			SpringPosition component = base.GetComponent<SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
			}
			this.mScroll = 0f;
		}
		else
		{
			this.mMomentum += this.scale * (this.mScroll * 20f);
			this.mScroll = NGUIMath.SpringLerp(this.mScroll, 0f, 20f, deltaTime);
			if (this.mMomentum.magnitude > 0.01f)
			{
				this.mTrans.localPosition += NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
				this.mBounds = NGUIMath.CalculateAbsoluteWidgetBounds(this.rootForBounds);
				if (!this.ConstrainToBounds(this.dragEffect == UIDragObject.DragEffect.None))
				{
					SpringPosition component2 = base.GetComponent<SpringPosition>();
					if (component2 != null)
					{
						component2.enabled = false;
					}
				}
				return;
			}
			this.mScroll = 0f;
		}
		NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
	}

	// Token: 0x04000190 RID: 400
	public Transform rootForBounds;

	// Token: 0x04000191 RID: 401
	public Vector2 scale = Vector2.one;

	// Token: 0x04000192 RID: 402
	public float scrollWheelFactor;

	// Token: 0x04000193 RID: 403
	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	// Token: 0x04000194 RID: 404
	public bool smoothDragStart = true;

	// Token: 0x04000195 RID: 405
	public float momentumAmount = 35f;

	// Token: 0x04000196 RID: 406
	private Camera mCam;

	// Token: 0x04000197 RID: 407
	private Transform mTrans;

	// Token: 0x04000198 RID: 408
	private bool mPressed;

	// Token: 0x04000199 RID: 409
	private Vector2 mMomentum = Vector2.zero;

	// Token: 0x0400019A RID: 410
	private Bounds mBounds;

	// Token: 0x0400019B RID: 411
	private float mScroll;

	// Token: 0x0400019C RID: 412
	private UIRoot mRoot;

	// Token: 0x0400019D RID: 413
	private bool mDragStarted;
}
