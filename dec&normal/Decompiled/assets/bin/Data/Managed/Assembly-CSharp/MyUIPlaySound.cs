using System;
using UnityEngine;

// Token: 0x020008B7 RID: 2231
public class MyUIPlaySound : MonoBehaviour
{
	// Token: 0x06003C2A RID: 15402 RVA: 0x00107294 File Offset: 0x00105494
	private void Awake()
	{
		if (this.IsAwakePlay)
		{
			this.Play();
		}
	}

	// Token: 0x06003C2B RID: 15403 RVA: 0x001072A8 File Offset: 0x001054A8
	private void OnHover(bool isOver)
	{
	}

	// Token: 0x06003C2C RID: 15404 RVA: 0x001072AC File Offset: 0x001054AC
	private void OnPress(bool isPressed)
	{
	}

	// Token: 0x06003C2D RID: 15405 RVA: 0x001072B0 File Offset: 0x001054B0
	private void OnClick()
	{
		if (this.trigger == MyUIPlaySound.Trigger.OnClick)
		{
			this.Play();
		}
	}

	// Token: 0x06003C2E RID: 15406 RVA: 0x001072C4 File Offset: 0x001054C4
	private void OnSelect(bool isSelected)
	{
	}

	// Token: 0x06003C2F RID: 15407 RVA: 0x001072C8 File Offset: 0x001054C8
	private void OnEnable()
	{
	}

	// Token: 0x06003C30 RID: 15408 RVA: 0x001072CC File Offset: 0x001054CC
	private void OnDisable()
	{
		this.timer.Cancel();
	}

	// Token: 0x06003C31 RID: 15409 RVA: 0x001072DC File Offset: 0x001054DC
	private void PlaySound()
	{
		SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(this.AudioId, 1f, null);
	}

	// Token: 0x06003C32 RID: 15410 RVA: 0x001072F4 File Offset: 0x001054F4
	public void Play()
	{
		if (this.delay > 0f)
		{
			vp_Timer.In(this.delay, new vp_Timer.Callback(this.PlaySound), this.timer);
		}
		else
		{
			this.PlaySound();
		}
	}

	// Token: 0x0400275B RID: 10075
	public int AudioId = 4;

	// Token: 0x0400275C RID: 10076
	public MyUIPlaySound.Trigger trigger;

	// Token: 0x0400275D RID: 10077
	public float delay = -1f;

	// Token: 0x0400275E RID: 10078
	public bool IsAwakePlay;

	// Token: 0x0400275F RID: 10079
	private vp_Timer.Handle timer = new vp_Timer.Handle();

	// Token: 0x020008B8 RID: 2232
	public enum Trigger
	{
		// Token: 0x04002761 RID: 10081
		OnClick,
		// Token: 0x04002762 RID: 10082
		OnMouseOver,
		// Token: 0x04002763 RID: 10083
		OnMouseOut,
		// Token: 0x04002764 RID: 10084
		OnPress,
		// Token: 0x04002765 RID: 10085
		OnRelease,
		// Token: 0x04002766 RID: 10086
		Custom
	}
}
