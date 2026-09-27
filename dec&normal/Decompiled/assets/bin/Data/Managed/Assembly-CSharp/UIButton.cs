using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003F RID: 63
[AddComponentMenu("NGUI/Interaction/Button")]
public class UIButton : UIButtonColor
{
	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060000ED RID: 237 RVA: 0x00006B58 File Offset: 0x00004D58
	// (set) Token: 0x060000EE RID: 238 RVA: 0x00006BAC File Offset: 0x00004DAC
	public override bool isEnabled
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			Collider collider = base.collider;
			if (collider && collider.enabled)
			{
				return true;
			}
			Collider2D component = base.GetComponent<Collider2D>();
			return component && component.enabled;
		}
		set
		{
			if (this.isEnabled != value)
			{
				Collider collider = base.collider;
				if (collider != null)
				{
					collider.enabled = value;
					this.SetState((!value) ? UIButtonColor.State.Disabled : UIButtonColor.State.Normal, false);
				}
				else
				{
					Collider2D component = base.GetComponent<Collider2D>();
					if (component != null)
					{
						component.enabled = value;
						this.SetState((!value) ? UIButtonColor.State.Disabled : UIButtonColor.State.Normal, false);
					}
					else
					{
						base.enabled = value;
					}
				}
			}
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060000EF RID: 239 RVA: 0x00006C34 File Offset: 0x00004E34
	// (set) Token: 0x060000F0 RID: 240 RVA: 0x00006C50 File Offset: 0x00004E50
	public string normalSprite
	{
		get
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			return this.mNormalSprite;
		}
		set
		{
			if (this.mSprite != null && !string.IsNullOrEmpty(this.mNormalSprite) && this.mNormalSprite == this.mSprite.spriteName)
			{
				this.mNormalSprite = value;
				this.SetSprite(value);
			}
			else
			{
				this.mNormalSprite = value;
				if (this.mState == UIButtonColor.State.Normal)
				{
					this.SetSprite(value);
				}
			}
		}
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00006CC8 File Offset: 0x00004EC8
	protected override void OnInit()
	{
		base.OnInit();
		this.mSprite = (this.mWidget as UISprite);
		if (this.mSprite != null)
		{
			this.mNormalSprite = this.mSprite.spriteName;
		}
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00006D04 File Offset: 0x00004F04
	protected override void OnEnable()
	{
		if (this.isEnabled)
		{
			if (this.mInitDone)
			{
				if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
				{
					this.OnHover(UICamera.selectedObject == base.gameObject);
				}
				else if (UICamera.currentScheme == UICamera.ControlScheme.Mouse)
				{
					this.OnHover(UICamera.hoveredObject == base.gameObject);
				}
				else
				{
					this.SetState(UIButtonColor.State.Normal, false);
				}
			}
		}
		else
		{
			this.SetState(UIButtonColor.State.Disabled, true);
		}
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00006D88 File Offset: 0x00004F88
	protected override void OnDragOver()
	{
		if (this.isEnabled && (this.dragHighlight || UICamera.currentTouch.pressed == base.gameObject))
		{
			base.OnDragOver();
		}
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00006DCC File Offset: 0x00004FCC
	protected override void OnDragOut()
	{
		if (this.isEnabled && (this.dragHighlight || UICamera.currentTouch.pressed == base.gameObject))
		{
			base.OnDragOut();
		}
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00006E10 File Offset: 0x00005010
	protected virtual void OnClick()
	{
		if (UIButton.current == null && this.isEnabled)
		{
			UIButton.current = this;
			EventDelegate.Execute(this.onClick);
			UIButton.current = null;
		}
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00006E50 File Offset: 0x00005050
	public override void SetState(UIButtonColor.State state, bool immediate)
	{
		base.SetState(state, immediate);
		switch (state)
		{
		case UIButtonColor.State.Normal:
			this.SetSprite(this.mNormalSprite);
			break;
		case UIButtonColor.State.Hover:
			this.SetSprite(this.hoverSprite);
			break;
		case UIButtonColor.State.Pressed:
			this.SetSprite(this.pressedSprite);
			break;
		case UIButtonColor.State.Disabled:
			this.SetSprite(this.disabledSprite);
			break;
		}
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00006EC8 File Offset: 0x000050C8
	protected void SetSprite(string sp)
	{
		if (this.mSprite != null && !string.IsNullOrEmpty(sp) && this.mSprite.spriteName != sp)
		{
			this.mSprite.spriteName = sp;
			if (this.pixelSnap)
			{
				this.mSprite.MakePixelPerfect();
			}
		}
	}

	// Token: 0x04000108 RID: 264
	public static UIButton current;

	// Token: 0x04000109 RID: 265
	public bool dragHighlight;

	// Token: 0x0400010A RID: 266
	public string hoverSprite;

	// Token: 0x0400010B RID: 267
	public string pressedSprite;

	// Token: 0x0400010C RID: 268
	public string disabledSprite;

	// Token: 0x0400010D RID: 269
	public bool pixelSnap;

	// Token: 0x0400010E RID: 270
	public List<EventDelegate> onClick = new List<EventDelegate>();

	// Token: 0x0400010F RID: 271
	[NonSerialized]
	private string mNormalSprite;

	// Token: 0x04000110 RID: 272
	[NonSerialized]
	private UISprite mSprite;
}
