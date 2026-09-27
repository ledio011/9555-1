using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
[AddComponentMenu("NGUI/Interaction/Play Sound")]
public class UIPlaySound : MonoBehaviour
{
	// Token: 0x1700001C RID: 28
	// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000BD34 File Offset: 0x00009F34
	private bool canPlay
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			UIButton component = base.GetComponent<UIButton>();
			return component == null || component.isEnabled;
		}
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000BD6C File Offset: 0x00009F6C
	private void OnHover(bool isOver)
	{
		if (this.trigger == UIPlaySound.Trigger.OnMouseOver)
		{
			if (this.mIsOver == isOver)
			{
				return;
			}
			this.mIsOver = isOver;
		}
		if (this.canPlay && ((isOver && this.trigger == UIPlaySound.Trigger.OnMouseOver) || (!isOver && this.trigger == UIPlaySound.Trigger.OnMouseOut)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000BDE0 File Offset: 0x00009FE0
	private void OnPress(bool isPressed)
	{
		if (this.trigger == UIPlaySound.Trigger.OnPress)
		{
			if (this.mIsOver == isPressed)
			{
				return;
			}
			this.mIsOver = isPressed;
		}
		if (this.canPlay && ((isPressed && this.trigger == UIPlaySound.Trigger.OnPress) || (!isPressed && this.trigger == UIPlaySound.Trigger.OnRelease)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0000BE54 File Offset: 0x0000A054
	private void OnClick()
	{
		if (this.canPlay && this.trigger == UIPlaySound.Trigger.OnClick)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0000BE90 File Offset: 0x0000A090
	private void OnSelect(bool isSelected)
	{
		if (this.canPlay && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
	public void Play()
	{
		NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
	}

	// Token: 0x040001FD RID: 509
	public AudioClip audioClip;

	// Token: 0x040001FE RID: 510
	public UIPlaySound.Trigger trigger;

	// Token: 0x040001FF RID: 511
	private bool mIsOver;

	// Token: 0x04000200 RID: 512
	[Range(0f, 1f)]
	public float volume = 1f;

	// Token: 0x04000201 RID: 513
	[Range(0f, 2f)]
	public float pitch = 1f;

	// Token: 0x02000063 RID: 99
	public enum Trigger
	{
		// Token: 0x04000203 RID: 515
		OnClick,
		// Token: 0x04000204 RID: 516
		OnMouseOver,
		// Token: 0x04000205 RID: 517
		OnMouseOut,
		// Token: 0x04000206 RID: 518
		OnPress,
		// Token: 0x04000207 RID: 519
		OnRelease,
		// Token: 0x04000208 RID: 520
		Custom
	}
}
