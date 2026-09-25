using System;
using UnityEngine;

// Token: 0x02000A8C RID: 2700
internal class StopWatch : MonoBehaviour
{
	// Token: 0x17000FE4 RID: 4068
	// (get) Token: 0x06004E83 RID: 20099 RVA: 0x001ADD98 File Offset: 0x001ABF98
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

	// Token: 0x06004E84 RID: 20100 RVA: 0x001ADDC4 File Offset: 0x001ABFC4
	private void OnGUI()
	{
		if (this.DrawDigitalWatch)
		{
			this.DrawDigital();
		}
		if (this.DrawButtons)
		{
			this.DrawDemoButtons();
		}
		if (this.DrawAnalogWatch)
		{
			this.DrawAnalog();
		}
	}

	// Token: 0x06004E85 RID: 20101 RVA: 0x001ADDFC File Offset: 0x001ABFFC
	private void DrawDemoButtons()
	{
		if (GUI.Button(new Rect((float)Screen.width * 0.5f + this.ButtonsPosition.x - this.ButtonsScale.x / 2f, (float)Screen.height * 0.5f + this.ButtonsPosition.y - this.ButtonsScale.y, this.ButtonsScale.x, this.ButtonsScale.y), (!this.m_Timer.Active) ? this.ImageButtonPlay : this.ImageButtonStop))
		{
			if (!this.m_Timer.Active)
			{
				this.Run();
			}
			else
			{
				this.Stop();
			}
		}
		if (!this.m_Timer.Active)
		{
			GUI.enabled = false;
		}
		if (GUI.Button(new Rect((float)Screen.width * 0.5f + this.ButtonsPosition.x - this.ButtonsScale.x / 2f, (float)Screen.height * 0.5f + this.ButtonsPosition.y + 5f, this.ButtonsScale.x, this.ButtonsScale.y), (!this.m_Timer.Paused) ? this.ImageButtonPause : this.ImageButtonPlay))
		{
			this.Pause();
		}
		GUI.enabled = true;
	}

	// Token: 0x06004E86 RID: 20102 RVA: 0x001ADF70 File Offset: 0x001AC170
	private void DrawDigital()
	{
		string text = string.Empty;
		text = vp_TimeUtility.TimeToString(this.m_Timer.Duration, false, true, true, false, true, false, ':');
		Rect rect;
		rect..ctor((float)Screen.width * 0.5f + this.DigitalPosition.x - (float)this.ImageDigitalDisplay.width * 0.5f, (float)Screen.height * 0.5f + this.DigitalPosition.y - (float)this.ImageDigitalDisplay.height * 0.5f, (float)this.ImageDigitalDisplay.width + this.FontBackSizeOffset, (float)this.ImageDigitalDisplay.height + this.FontBackSizeOffset);
		GUI.color = Color.black;
		GUI.DrawTexture(new Rect(rect.x - this.FontBackMargin + this.FontPosOffset.x, rect.y - this.FontBackMargin + this.FontPosOffset.y, rect.width - 1f + this.FontBackMargin * 2f + 1f, rect.height - 1f + this.FontBackMargin * 2f + 1f), this.ImagePixel);
		GUI.color = this.DigitalNumbersColor;
		GUI.DrawTexture(new Rect(rect.x + 1f + this.FontPosOffset.x, rect.y + 2f + this.FontPosOffset.y, rect.width - 1f, rect.height - 1f), this.ImagePixel);
		GUI.color = Color.black;
		GUI.Label(new Rect(rect.x + this.FontPosOffset.x, rect.y + this.FontPosOffset.y, rect.width, rect.height), text, this.DigitalDisplayStyle);
		GUI.color = Color.white;
		GUI.DrawTexture(new Rect(rect.x, rect.y, (float)this.ImageDigitalDisplay.width, (float)this.ImageDigitalDisplay.height), this.ImageDigitalDisplay);
	}

	// Token: 0x06004E87 RID: 20103 RVA: 0x001AE1A4 File Offset: 0x001AC3A4
	private void DrawAnalog()
	{
		GUI.color = Color.white;
		Vector2 vector;
		vector..ctor((float)Screen.width * 0.5f + this.AnalogPosition.x - (float)(this.ImageStopWatch.width / 2), (float)Screen.height * 0.5f + this.AnalogPosition.y - (float)(this.ImageStopWatch.height / 2));
		Vector3 zero = Vector3.zero;
		zero.x = 0f;
		zero.y = vp_TimeUtility.TimeToDegrees(this.m_Timer.Duration, false, false, false, true);
		zero.z = vp_TimeUtility.TimeToDegrees(this.m_Timer.Duration, false, false, true, true);
		GUIUtility.RotateAroundPivot(zero.z, vector + new Vector2((float)(this.ImageStopWatch.width / 2) + this.BigHandPos.x, (float)(this.ImageStopWatch.height / 2) + this.BigHandPos.y));
		GUI.DrawTexture(new Rect(vector.x + this.BigHandPos.x, vector.y + this.BigHandPos.y, (float)this.ImageStopWatch.width, (float)this.ImageStopWatch.height), this.ImageHandBig);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
		GUIUtility.RotateAroundPivot(zero.y, vector + new Vector2((float)(this.ImageStopWatch.width / 2) + this.SmallHandPos.x, (float)(this.ImageStopWatch.height / 2) + this.SmallHandPos.y));
		GUI.DrawTexture(new Rect(vector.x + this.SmallHandPos.x, vector.y + this.SmallHandPos.y, (float)this.ImageStopWatch.width, (float)this.ImageStopWatch.height), this.ImageHandSmall);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
		GUI.color = Color.white;
		GUI.DrawTexture(new Rect(vector.x, vector.y, (float)this.ImageStopWatch.width, (float)this.ImageStopWatch.height), this.ImageStopWatch);
	}

	// Token: 0x06004E88 RID: 20104 RVA: 0x001AE3FC File Offset: 0x001AC5FC
	public void Run()
	{
		vp_Timer.Start(this.m_Timer);
		if (!base.audio.isPlaying)
		{
			base.audio.Play();
		}
	}

	// Token: 0x06004E89 RID: 20105 RVA: 0x001AE430 File Offset: 0x001AC630
	public void Stop()
	{
		this.m_Timer.Cancel();
		if (base.audio.isPlaying)
		{
			base.audio.Stop();
		}
	}

	// Token: 0x06004E8A RID: 20106 RVA: 0x001AE464 File Offset: 0x001AC664
	public void Pause()
	{
		this.m_Timer.Paused = !this.m_Timer.Paused;
		if (!base.audio.isPlaying && this.m_Timer.Active && !this.m_Timer.Paused)
		{
			base.audio.Play();
		}
		else if (base.audio.isPlaying && (!this.m_Timer.Active || this.m_Timer.Paused))
		{
			base.audio.Stop();
		}
	}

	// Token: 0x04003D02 RID: 15618
	public bool DrawButtons = true;

	// Token: 0x04003D03 RID: 15619
	public bool DrawDigitalWatch = true;

	// Token: 0x04003D04 RID: 15620
	public bool DrawAnalogWatch = true;

	// Token: 0x04003D05 RID: 15621
	public float FontBackMargin = 8f;

	// Token: 0x04003D06 RID: 15622
	public float FontBackSizeOffset = -29f;

	// Token: 0x04003D07 RID: 15623
	public Vector2 FontPosOffset = new Vector2(14f, 13f);

	// Token: 0x04003D08 RID: 15624
	public Vector2 ButtonsPosition = new Vector2(0f, 0f);

	// Token: 0x04003D09 RID: 15625
	public Vector2 ButtonsScale = new Vector2(80f, 40f);

	// Token: 0x04003D0A RID: 15626
	public Vector2 DigitalPosition = new Vector2(255f, -230f);

	// Token: 0x04003D0B RID: 15627
	public Vector2 AnalogPosition = new Vector2(-240f, 140f);

	// Token: 0x04003D0C RID: 15628
	public Vector2 BigHandPos = new Vector2(0f, 0f);

	// Token: 0x04003D0D RID: 15629
	public Vector2 SmallHandPos = new Vector2(0f, -53f);

	// Token: 0x04003D0E RID: 15630
	public Color DigitalNumbersColor = Color.yellow;

	// Token: 0x04003D0F RID: 15631
	private vp_Timer.Handle m_Timer = new vp_Timer.Handle();

	// Token: 0x04003D10 RID: 15632
	public Texture ImagePixel;

	// Token: 0x04003D11 RID: 15633
	public Texture ImageDigitalDisplay;

	// Token: 0x04003D12 RID: 15634
	public Texture ImageStopWatch;

	// Token: 0x04003D13 RID: 15635
	public Texture ImageHandBig;

	// Token: 0x04003D14 RID: 15636
	public Texture ImageHandSmall;

	// Token: 0x04003D15 RID: 15637
	public Texture ImageButtonPlay;

	// Token: 0x04003D16 RID: 15638
	public Texture ImageButtonPause;

	// Token: 0x04003D17 RID: 15639
	public Texture ImageButtonStop;

	// Token: 0x04003D18 RID: 15640
	public GUISkin Skin;

	// Token: 0x04003D19 RID: 15641
	private GUIStyle m_DigitalDisplayStyle;
}
