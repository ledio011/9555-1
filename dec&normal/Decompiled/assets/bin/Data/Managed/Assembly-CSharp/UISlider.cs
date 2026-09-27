using System;
using UnityEngine;

// Token: 0x02000070 RID: 112
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/NGUI Slider")]
public class UISlider : UIProgressBar
{
	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000243 RID: 579 RVA: 0x000101EC File Offset: 0x0000E3EC
	// (set) Token: 0x06000244 RID: 580 RVA: 0x000101F4 File Offset: 0x0000E3F4
	[Obsolete("Use 'value' instead")]
	public float sliderValue
	{
		get
		{
			return base.value;
		}
		set
		{
			base.value = value;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000245 RID: 581 RVA: 0x00010200 File Offset: 0x0000E400
	// (set) Token: 0x06000246 RID: 582 RVA: 0x00010208 File Offset: 0x0000E408
	[Obsolete("Use 'fillDirection' instead")]
	public bool inverted
	{
		get
		{
			return base.isInverted;
		}
		set
		{
		}
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0001020C File Offset: 0x0000E40C
	protected override void Upgrade()
	{
		if (this.direction != UISlider.Direction.Upgraded)
		{
			this.mValue = this.rawValue;
			if (this.foreground != null)
			{
				this.mFG = this.foreground.GetComponent<UIWidget>();
			}
			if (this.direction == UISlider.Direction.Horizontal)
			{
				this.mFill = ((!this.mInverted) ? UIProgressBar.FillDirection.LeftToRight : UIProgressBar.FillDirection.RightToLeft);
			}
			else
			{
				this.mFill = ((!this.mInverted) ? UIProgressBar.FillDirection.BottomToTop : UIProgressBar.FillDirection.TopToBottom);
			}
			this.direction = UISlider.Direction.Upgraded;
		}
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0001029C File Offset: 0x0000E49C
	protected override void OnStart()
	{
		GameObject go = (!(this.mBG != null) || !(this.mBG.collider != null)) ? base.gameObject : this.mBG.gameObject;
		UIEventListener uieventListener = UIEventListener.Get(go);
		UIEventListener uieventListener2 = uieventListener;
		uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(this.OnPressBackground));
		UIEventListener uieventListener3 = uieventListener;
		uieventListener3.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener3.onDrag, new UIEventListener.VectorDelegate(this.OnDragBackground));
		if (this.thumb != null && this.thumb.collider != null && (this.mFG == null || this.thumb != this.mFG.cachedTransform))
		{
			UIEventListener uieventListener4 = UIEventListener.Get(this.thumb.gameObject);
			UIEventListener uieventListener5 = uieventListener4;
			uieventListener5.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener5.onPress, new UIEventListener.BoolDelegate(this.OnPressForeground));
			UIEventListener uieventListener6 = uieventListener4;
			uieventListener6.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener6.onDrag, new UIEventListener.VectorDelegate(this.OnDragForeground));
		}
	}

	// Token: 0x06000249 RID: 585 RVA: 0x000103DC File Offset: 0x0000E5DC
	protected void OnPressBackground(GameObject go, bool isPressed)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		this.mCam = UICamera.currentCamera;
		base.value = base.ScreenToValue(UICamera.lastTouchPosition);
		if (!isPressed && this.onDragFinished != null)
		{
			this.onDragFinished();
		}
	}

	// Token: 0x0600024A RID: 586 RVA: 0x00010430 File Offset: 0x0000E630
	protected void OnDragBackground(GameObject go, Vector2 delta)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		this.mCam = UICamera.currentCamera;
		base.value = base.ScreenToValue(UICamera.lastTouchPosition);
	}

	// Token: 0x0600024B RID: 587 RVA: 0x00010468 File Offset: 0x0000E668
	protected void OnPressForeground(GameObject go, bool isPressed)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		if (isPressed)
		{
			this.mOffset = ((!(this.mFG == null)) ? (base.value - base.ScreenToValue(UICamera.lastTouchPosition)) : 0f);
		}
		else if (this.onDragFinished != null)
		{
			this.onDragFinished();
		}
	}

	// Token: 0x0600024C RID: 588 RVA: 0x000104D8 File Offset: 0x0000E6D8
	protected void OnDragForeground(GameObject go, Vector2 delta)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		this.mCam = UICamera.currentCamera;
		base.value = this.mOffset + base.ScreenToValue(UICamera.lastTouchPosition);
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0001050C File Offset: 0x0000E70C
	protected void OnKey(KeyCode key)
	{
		if (base.enabled)
		{
			float num = ((float)this.numberOfSteps <= 1f) ? 0.125f : (1f / (float)(this.numberOfSteps - 1));
			if (base.fillDirection == UIProgressBar.FillDirection.LeftToRight || base.fillDirection == UIProgressBar.FillDirection.RightToLeft)
			{
				if (key == 276)
				{
					base.value = this.mValue - num;
				}
				else if (key == 275)
				{
					base.value = this.mValue + num;
				}
			}
			else if (key == 274)
			{
				base.value = this.mValue - num;
			}
			else if (key == 273)
			{
				base.value = this.mValue + num;
			}
		}
	}

	// Token: 0x04000287 RID: 647
	[HideInInspector]
	[SerializeField]
	private Transform foreground;

	// Token: 0x04000288 RID: 648
	[HideInInspector]
	[SerializeField]
	private float rawValue = 1f;

	// Token: 0x04000289 RID: 649
	[HideInInspector]
	[SerializeField]
	private UISlider.Direction direction = UISlider.Direction.Upgraded;

	// Token: 0x0400028A RID: 650
	[HideInInspector]
	[SerializeField]
	protected bool mInverted;

	// Token: 0x02000071 RID: 113
	private enum Direction
	{
		// Token: 0x0400028C RID: 652
		Horizontal,
		// Token: 0x0400028D RID: 653
		Vertical,
		// Token: 0x0400028E RID: 654
		Upgraded
	}
}
