using System;
using UnityEngine;

// Token: 0x02000A8D RID: 2701
internal class TimeBomb : MonoBehaviour
{
	// Token: 0x17000FE5 RID: 4069
	// (get) Token: 0x06004E8C RID: 20108 RVA: 0x001AE5B4 File Offset: 0x001AC7B4
	private GUIStyle DigitalDisplayStyle
	{
		get
		{
			if (this.m_DigitalDisplayStyle == null)
			{
				this.m_DigitalDisplayStyle = this.Skin.GetStyle("DigitalDisplay");
			}
			return this.m_DigitalDisplayStyle;
		}
	}

	// Token: 0x06004E8D RID: 20109 RVA: 0x001AE5E0 File Offset: 0x001AC7E0
	private void Start()
	{
		this.m_StartPosition = this.DigitalPosition;
		this.ScheduleBomb(this.BombTime);
	}

	// Token: 0x06004E8E RID: 20110 RVA: 0x001AE5FC File Offset: 0x001AC7FC
	private void OnGUI()
	{
		if (this.m_Timer.Active || this.m_NumberBlinkTimer.Active || this.m_ExplosionDelayTimer.Active)
		{
			this.DrawDigital();
		}
		if (this.m_TimeBombFlashAlpha > 0f)
		{
			this.m_TimeBombFlashAlpha -= Time.deltaTime * 0.33f;
			GUI.color = new Color(1f, 1f, 1f, this.m_TimeBombFlashAlpha);
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.ImagePixel);
		}
		if (this.m_Timer.Active)
		{
			if (!base.audio.isPlaying)
			{
				base.audio.Play();
			}
			if (this.m_LastSecond != vp_TimeUtility.TimeToUnits(this.m_Timer.Duration).seconds)
			{
				base.audio.PlayOneShot(this.m_BlipSound);
				this.m_LastSecond = vp_TimeUtility.TimeToUnits(this.m_Timer.Duration).seconds;
			}
		}
		if (this.m_ExplosionDelayTimer.Active)
		{
			this.DigitalPosition = this.m_StartPosition;
			float num = Mathf.Max(0f, this.m_ExplosionDelayTimer.Duration * 5f);
			Vector2 vector;
			vector..ctor(Random.value * num, Random.value * num);
			if (Random.value < 0.5f)
			{
				vector.x = -vector.x;
			}
			if (Random.value < 0.5f)
			{
				vector.y = -vector.y;
			}
			this.DigitalPosition += vector;
		}
	}

	// Token: 0x06004E8F RID: 20111 RVA: 0x001AE7C4 File Offset: 0x001AC9C4
	private void DrawDigital()
	{
		Rect rect;
		rect..ctor((float)Screen.width * 0.5f + this.DigitalPosition.x - (float)this.ImageDigitalDisplay.width * 0.5f, (float)Screen.height * 0.5f + this.DigitalPosition.y - (float)this.ImageDigitalDisplay.height * 0.5f, (float)this.ImageDigitalDisplay.width + this.FontBackSizeOffset, (float)this.ImageDigitalDisplay.height + this.FontBackSizeOffset);
		GUI.color = Color.black;
		GUI.DrawTexture(new Rect(rect.x - this.FontBackMargin + this.FontPosOffset.x, rect.y - this.FontBackMargin + this.FontPosOffset.y, rect.width - 1f + this.FontBackMargin * 2f + 1f, rect.height - 1f + this.FontBackMargin * 2f + 1f), this.ImagePixel);
		Color digitalNumbersColor = this.DigitalNumbersColor;
		digitalNumbersColor.a = this.m_TimeBombNumberAlpha;
		GUI.color = digitalNumbersColor;
		GUI.DrawTexture(new Rect(rect.x + 1f + this.FontPosOffset.x, rect.y + 2f + this.FontPosOffset.y, rect.width - 1f, rect.height - 1f), this.ImagePixel);
		GUI.color = Color.black;
		GUI.Label(new Rect(rect.x + this.FontPosOffset.x, rect.y + this.FontPosOffset.y, rect.width, rect.height), vp_TimeUtility.TimeToString(this.m_Timer.DurationLeft, false, true, true, false, true, false, ':'), this.DigitalDisplayStyle);
		GUI.color = Color.white;
		GUI.DrawTexture(new Rect(rect.x, rect.y, (float)this.ImageDigitalDisplay.width, (float)this.ImageDigitalDisplay.height), this.ImageDigitalDisplay);
	}

	// Token: 0x06004E90 RID: 20112 RVA: 0x001AEA00 File Offset: 0x001ACC00
	private void DoTimeBombFlash()
	{
		if (this.m_TimeBombFlashAlpha > 0f)
		{
			this.m_TimeBombFlashAlpha -= Time.deltaTime * 0.33f;
			GUI.color = new Color(1f, 1f, 1f, this.m_TimeBombFlashAlpha);
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.ImagePixel);
		}
	}

	// Token: 0x06004E91 RID: 20113 RVA: 0x001AEA7C File Offset: 0x001ACC7C
	private void ScheduleBomb(float time)
	{
		vp_Timer.In(time, delegate()
		{
			base.audio.Stop();
			base.audio.PlayOneShot(this.m_RumbleFadeInSound);
			vp_Timer.In(0.33f, delegate()
			{
				if (this.m_TimeBombNumberAlpha == 0f)
				{
					this.m_TimeBombNumberAlpha = 1f;
				}
				else
				{
					this.m_TimeBombNumberAlpha = 0f;
				}
			}, 7, this.m_NumberBlinkTimer);
			vp_Timer.In(2.66f, delegate()
			{
				base.audio.Stop();
				base.audio.PlayOneShot(this.m_ExplosionSound);
				this.m_TimeBombFlashAlpha = 2.5f;
				this.m_TimeBombNumberAlpha = 1f;
				this.DigitalPosition = this.m_StartPosition;
			}, this.m_ExplosionDelayTimer);
		}, this.m_Timer);
	}

	// Token: 0x06004E92 RID: 20114 RVA: 0x001AEA98 File Offset: 0x001ACC98
	private void CancelBomb()
	{
		base.audio.Stop();
		this.DigitalPosition = this.m_StartPosition;
		this.m_Timer.Cancel();
		this.m_TimeBombNumberAlpha = 1f;
		this.m_NumberBlinkTimer.Cancel();
		this.m_ExplosionDelayTimer.Cancel();
	}

	// Token: 0x04003D1A RID: 15642
	public Vector2 FontPosOffset = new Vector2(14f, 13f);

	// Token: 0x04003D1B RID: 15643
	public float FontBackMargin = 8f;

	// Token: 0x04003D1C RID: 15644
	public float FontBackSizeOffset = -29f;

	// Token: 0x04003D1D RID: 15645
	public Vector2 DigitalPosition = new Vector2(0f, 0f);

	// Token: 0x04003D1E RID: 15646
	private Vector2 m_StartPosition = new Vector2(0f, 0f);

	// Token: 0x04003D1F RID: 15647
	public Color DigitalNumbersColor = Color.red;

	// Token: 0x04003D20 RID: 15648
	private float m_TimeBombNumberAlpha = 1f;

	// Token: 0x04003D21 RID: 15649
	private float m_TimeBombFlashAlpha;

	// Token: 0x04003D22 RID: 15650
	public float BombTime = 11f;

	// Token: 0x04003D23 RID: 15651
	private int m_LastSecond;

	// Token: 0x04003D24 RID: 15652
	private vp_Timer.Handle m_Timer = new vp_Timer.Handle();

	// Token: 0x04003D25 RID: 15653
	private vp_Timer.Handle m_NumberBlinkTimer = new vp_Timer.Handle();

	// Token: 0x04003D26 RID: 15654
	private vp_Timer.Handle m_ExplosionDelayTimer = new vp_Timer.Handle();

	// Token: 0x04003D27 RID: 15655
	public Texture ImagePixel;

	// Token: 0x04003D28 RID: 15656
	public Texture ImageDigitalDisplay;

	// Token: 0x04003D29 RID: 15657
	public AudioClip m_ExplosionSound;

	// Token: 0x04003D2A RID: 15658
	public AudioClip m_BlipSound;

	// Token: 0x04003D2B RID: 15659
	public AudioClip m_RumbleFadeInSound;

	// Token: 0x04003D2C RID: 15660
	public AudioClip m_SoftTickLoopSound;

	// Token: 0x04003D2D RID: 15661
	public GUISkin Skin;

	// Token: 0x04003D2E RID: 15662
	private GUIStyle m_DigitalDisplayStyle;
}
