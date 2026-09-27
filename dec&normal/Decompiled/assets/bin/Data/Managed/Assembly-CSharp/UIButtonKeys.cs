using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
[AddComponentMenu("NGUI/Interaction/Button Keys (Legacy)")]
[ExecuteInEditMode]
public class UIButtonKeys : UIKeyNavigation
{
	// Token: 0x0600010E RID: 270 RVA: 0x00007558 File Offset: 0x00005758
	protected override void OnEnable()
	{
		this.Upgrade();
		base.OnEnable();
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00007568 File Offset: 0x00005768
	public void Upgrade()
	{
		if (this.onClick == null && this.selectOnClick != null)
		{
			this.onClick = this.selectOnClick.gameObject;
			this.selectOnClick = null;
			NGUITools.SetDirty(this);
		}
		if (this.onLeft == null && this.selectOnLeft != null)
		{
			this.onLeft = this.selectOnLeft.gameObject;
			this.selectOnLeft = null;
			NGUITools.SetDirty(this);
		}
		if (this.onRight == null && this.selectOnRight != null)
		{
			this.onRight = this.selectOnRight.gameObject;
			this.selectOnRight = null;
			NGUITools.SetDirty(this);
		}
		if (this.onUp == null && this.selectOnUp != null)
		{
			this.onUp = this.selectOnUp.gameObject;
			this.selectOnUp = null;
			NGUITools.SetDirty(this);
		}
		if (this.onDown == null && this.selectOnDown != null)
		{
			this.onDown = this.selectOnDown.gameObject;
			this.selectOnDown = null;
			NGUITools.SetDirty(this);
		}
	}

	// Token: 0x04000122 RID: 290
	public UIButtonKeys selectOnClick;

	// Token: 0x04000123 RID: 291
	public UIButtonKeys selectOnUp;

	// Token: 0x04000124 RID: 292
	public UIButtonKeys selectOnDown;

	// Token: 0x04000125 RID: 293
	public UIButtonKeys selectOnLeft;

	// Token: 0x04000126 RID: 294
	public UIButtonKeys selectOnRight;
}
