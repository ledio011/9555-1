using System;
using UnityEngine;

// Token: 0x0200005B RID: 91
[AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton : MonoBehaviour
{
	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000198 RID: 408 RVA: 0x0000AD4C File Offset: 0x00008F4C
	// (set) Token: 0x06000199 RID: 409 RVA: 0x0000AD74 File Offset: 0x00008F74
	public bool isEnabled
	{
		get
		{
			Collider collider = base.collider;
			return collider && collider.enabled;
		}
		set
		{
			Collider collider = base.collider;
			if (!collider)
			{
				return;
			}
			if (collider.enabled != value)
			{
				collider.enabled = value;
				this.UpdateImage();
			}
		}
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0000ADB0 File Offset: 0x00008FB0
	private void OnEnable()
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<UISprite>();
		}
		this.UpdateImage();
	}

	// Token: 0x0600019B RID: 411 RVA: 0x0000ADD8 File Offset: 0x00008FD8
	private void OnValidate()
	{
		if (this.target != null)
		{
			if (string.IsNullOrEmpty(this.normalSprite))
			{
				this.normalSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.hoverSprite))
			{
				this.hoverSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.pressedSprite))
			{
				this.pressedSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.disabledSprite))
			{
				this.disabledSprite = this.target.spriteName;
			}
		}
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000AE7C File Offset: 0x0000907C
	private void UpdateImage()
	{
		if (this.target != null)
		{
			if (this.isEnabled)
			{
				this.SetSprite((!UICamera.IsHighlighted(base.gameObject)) ? this.normalSprite : this.hoverSprite);
			}
			else
			{
				this.SetSprite(this.disabledSprite);
			}
		}
	}

	// Token: 0x0600019D RID: 413 RVA: 0x0000AEE0 File Offset: 0x000090E0
	private void OnHover(bool isOver)
	{
		if (this.isEnabled && this.target != null)
		{
			this.SetSprite((!isOver) ? this.normalSprite : this.hoverSprite);
		}
	}

	// Token: 0x0600019E RID: 414 RVA: 0x0000AF1C File Offset: 0x0000911C
	private void OnPress(bool pressed)
	{
		if (pressed)
		{
			this.SetSprite(this.pressedSprite);
		}
		else
		{
			this.UpdateImage();
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x0000AF3C File Offset: 0x0000913C
	private void SetSprite(string sprite)
	{
		if (this.target.atlas == null || this.target.atlas.GetSprite(sprite) == null)
		{
			return;
		}
		this.target.spriteName = sprite;
		if (this.pixelSnap)
		{
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x040001CD RID: 461
	public UISprite target;

	// Token: 0x040001CE RID: 462
	public string normalSprite;

	// Token: 0x040001CF RID: 463
	public string hoverSprite;

	// Token: 0x040001D0 RID: 464
	public string pressedSprite;

	// Token: 0x040001D1 RID: 465
	public string disabledSprite;

	// Token: 0x040001D2 RID: 466
	public bool pixelSnap = true;
}
