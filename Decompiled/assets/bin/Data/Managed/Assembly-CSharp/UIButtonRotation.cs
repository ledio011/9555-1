using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	// Token: 0x06000121 RID: 289 RVA: 0x00007AAC File Offset: 0x00005CAC
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mRot = this.tweenTarget.localRotation;
		}
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00007AFC File Offset: 0x00005CFC
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00007B1C File Offset: 0x00005D1C
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenRotation component = this.tweenTarget.GetComponent<TweenRotation>();
			if (component != null)
			{
				component.value = this.mRot;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00007B70 File Offset: 0x00005D70
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))) : (this.mRot * Quaternion.Euler(this.pressed))).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00007C08 File Offset: 0x00005E08
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00007C74 File Offset: 0x00005E74
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x04000139 RID: 313
	public Transform tweenTarget;

	// Token: 0x0400013A RID: 314
	public Vector3 hover = Vector3.zero;

	// Token: 0x0400013B RID: 315
	public Vector3 pressed = Vector3.zero;

	// Token: 0x0400013C RID: 316
	public float duration = 0.2f;

	// Token: 0x0400013D RID: 317
	private Quaternion mRot;

	// Token: 0x0400013E RID: 318
	private bool mStarted;
}
