using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
[AddComponentMenu("NGUI/Interaction/Button Color")]
[ExecuteInEditMode]
public class UIButtonColor : UIWidgetContainer
{
	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060000FB RID: 251 RVA: 0x00006FD4 File Offset: 0x000051D4
	// (set) Token: 0x060000FC RID: 252 RVA: 0x00006FDC File Offset: 0x000051DC
	public UIButtonColor.State state
	{
		get
		{
			return this.mState;
		}
		set
		{
			this.SetState(value, false);
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x060000FD RID: 253 RVA: 0x00006FE8 File Offset: 0x000051E8
	// (set) Token: 0x060000FE RID: 254 RVA: 0x00007004 File Offset: 0x00005204
	public Color defaultColor
	{
		get
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			return this.mDefaultColor;
		}
		set
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			this.mDefaultColor = value;
			UIButtonColor.State state = this.mState;
			this.mState = UIButtonColor.State.Disabled;
			this.SetState(state, false);
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x060000FF RID: 255 RVA: 0x00007040 File Offset: 0x00005240
	// (set) Token: 0x06000100 RID: 256 RVA: 0x00007048 File Offset: 0x00005248
	public virtual bool isEnabled
	{
		get
		{
			return base.enabled;
		}
		set
		{
			base.enabled = value;
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00007054 File Offset: 0x00005254
	public void ResetDefaultColor()
	{
		this.defaultColor = this.mStartingColor;
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00007064 File Offset: 0x00005264
	private void Awake()
	{
		if (!this.mInitDone)
		{
			this.OnInit();
		}
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00007078 File Offset: 0x00005278
	private void Start()
	{
		if (!this.isEnabled)
		{
			this.SetState(UIButtonColor.State.Disabled, true);
		}
	}

	// Token: 0x06000104 RID: 260 RVA: 0x00007090 File Offset: 0x00005290
	protected virtual void OnInit()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.gameObject;
		}
		this.mWidget = this.tweenTarget.GetComponent<UIWidget>();
		if (this.mWidget != null)
		{
			this.mDefaultColor = this.mWidget.color;
			this.mStartingColor = this.mDefaultColor;
		}
		else
		{
			Renderer renderer = this.tweenTarget.renderer;
			if (renderer != null)
			{
				this.mDefaultColor = ((!Application.isPlaying) ? renderer.sharedMaterial.color : renderer.material.color);
				this.mStartingColor = this.mDefaultColor;
			}
			else
			{
				Light light = this.tweenTarget.light;
				if (light != null)
				{
					this.mDefaultColor = light.color;
					this.mStartingColor = this.mDefaultColor;
				}
				else
				{
					this.tweenTarget = null;
					this.mInitDone = false;
				}
			}
		}
	}

	// Token: 0x06000105 RID: 261 RVA: 0x0000719C File Offset: 0x0000539C
	protected virtual void OnEnable()
	{
		if (this.mInitDone)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
		if (UICamera.currentTouch != null)
		{
			if (UICamera.currentTouch.pressed == base.gameObject)
			{
				this.OnPress(true);
			}
			else if (UICamera.currentTouch.current == base.gameObject)
			{
				this.OnHover(true);
			}
		}
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00007218 File Offset: 0x00005418
	protected virtual void OnDisable()
	{
		if (this.mInitDone && this.tweenTarget != null)
		{
			this.SetState(UIButtonColor.State.Normal, true);
			TweenColor component = this.tweenTarget.GetComponent<TweenColor>();
			if (component != null)
			{
				component.value = this.mDefaultColor;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00007274 File Offset: 0x00005474
	protected virtual void OnHover(bool isOver)
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState((!isOver) ? UIButtonColor.State.Normal : UIButtonColor.State.Hover, false);
			}
		}
	}

	// Token: 0x06000108 RID: 264 RVA: 0x000072C4 File Offset: 0x000054C4
	protected virtual void OnPress(bool isPressed)
	{
		if (this.isEnabled && UICamera.currentTouch != null)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				if (isPressed)
				{
					this.SetState(UIButtonColor.State.Pressed, false);
				}
				else if (UICamera.currentTouch.current == base.gameObject)
				{
					if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
					{
						this.SetState(UIButtonColor.State.Hover, false);
					}
					else if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && UICamera.hoveredObject == base.gameObject)
					{
						this.SetState(UIButtonColor.State.Hover, false);
					}
					else
					{
						this.SetState(UIButtonColor.State.Normal, false);
					}
				}
				else
				{
					this.SetState(UIButtonColor.State.Normal, false);
				}
			}
		}
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00007390 File Offset: 0x00005590
	protected virtual void OnDragOver()
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState(UIButtonColor.State.Pressed, false);
			}
		}
	}

	// Token: 0x0600010A RID: 266 RVA: 0x000073D4 File Offset: 0x000055D4
	protected virtual void OnDragOut()
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState(UIButtonColor.State.Normal, false);
			}
		}
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00007418 File Offset: 0x00005618
	protected virtual void OnSelect(bool isSelected)
	{
		if (this.isEnabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller) && this.tweenTarget != null)
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x0600010C RID: 268 RVA: 0x0000745C File Offset: 0x0000565C
	public virtual void SetState(UIButtonColor.State state, bool instant)
	{
		if (!this.mInitDone)
		{
			this.mInitDone = true;
			this.OnInit();
		}
		if (this.mState != state)
		{
			this.mState = state;
			TweenColor tweenColor;
			switch (this.mState)
			{
			case UIButtonColor.State.Hover:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.hover);
				break;
			case UIButtonColor.State.Pressed:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.pressed);
				break;
			case UIButtonColor.State.Disabled:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.disabledColor);
				break;
			default:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.mDefaultColor);
				break;
			}
			if (instant && tweenColor != null)
			{
				tweenColor.value = tweenColor.to;
				tweenColor.enabled = false;
			}
		}
	}

	// Token: 0x04000113 RID: 275
	public GameObject tweenTarget;

	// Token: 0x04000114 RID: 276
	public Color hover = new Color(0.88235295f, 0.78431374f, 0.5882353f, 1f);

	// Token: 0x04000115 RID: 277
	public Color pressed = new Color(0.7176471f, 0.6392157f, 0.48235294f, 1f);

	// Token: 0x04000116 RID: 278
	public Color disabledColor = Color.grey;

	// Token: 0x04000117 RID: 279
	public float duration = 0.2f;

	// Token: 0x04000118 RID: 280
	protected Color mStartingColor;

	// Token: 0x04000119 RID: 281
	protected Color mDefaultColor;

	// Token: 0x0400011A RID: 282
	protected bool mInitDone;

	// Token: 0x0400011B RID: 283
	protected UIWidget mWidget;

	// Token: 0x0400011C RID: 284
	protected UIButtonColor.State mState;

	// Token: 0x02000042 RID: 66
	public enum State
	{
		// Token: 0x0400011E RID: 286
		Normal,
		// Token: 0x0400011F RID: 287
		Hover,
		// Token: 0x04000120 RID: 288
		Pressed,
		// Token: 0x04000121 RID: 289
		Disabled
	}
}
