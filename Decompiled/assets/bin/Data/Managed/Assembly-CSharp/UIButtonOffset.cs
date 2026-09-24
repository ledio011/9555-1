using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset : MonoBehaviour
{
	// Token: 0x0600011A RID: 282 RVA: 0x0000789C File Offset: 0x00005A9C
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mPos = this.tweenTarget.localPosition;
		}
	}

	// Token: 0x0600011B RID: 283 RVA: 0x000078EC File Offset: 0x00005AEC
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0000790C File Offset: 0x00005B0C
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenPosition component = this.tweenTarget.GetComponent<TweenPosition>();
			if (component != null)
			{
				component.value = this.mPos;
				component.enabled = false;
			}
		}
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00007960 File Offset: 0x00005B60
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mPos : (this.mPos + this.hover)) : (this.mPos + this.pressed)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x0600011E RID: 286 RVA: 0x000079F0 File Offset: 0x00005BF0
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mPos : (this.mPos + this.hover)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00007A58 File Offset: 0x00005C58
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x04000133 RID: 307
	public Transform tweenTarget;

	// Token: 0x04000134 RID: 308
	public Vector3 hover = Vector3.zero;

	// Token: 0x04000135 RID: 309
	public Vector3 pressed = new Vector3(2f, -2f);

	// Token: 0x04000136 RID: 310
	public float duration = 0.2f;

	// Token: 0x04000137 RID: 311
	private Vector3 mPos;

	// Token: 0x04000138 RID: 312
	private bool mStarted;
}
