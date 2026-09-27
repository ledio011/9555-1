using System;
using UnityEngine;

// Token: 0x02000A88 RID: 2696
internal class Clock : MonoBehaviour
{
	// Token: 0x17000FE3 RID: 4067
	// (get) Token: 0x06004E67 RID: 20071 RVA: 0x001ACCEC File Offset: 0x001AAEEC
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

	// Token: 0x06004E68 RID: 20072 RVA: 0x001ACD18 File Offset: 0x001AAF18
	private void Start()
	{
		this.m_LastSecond = vp_TimeUtility.SystemTimeToUnits().seconds;
	}

	// Token: 0x06004E69 RID: 20073 RVA: 0x001ACD38 File Offset: 0x001AAF38
	private void OnGUI()
	{
		if (this.DrawDigitalClock)
		{
			this.DrawDigital();
		}
		if (this.DrawAnalogClock)
		{
			this.DrawAnalog();
		}
		if (this.m_LastSecond != vp_TimeUtility.SystemTimeToUnits().seconds)
		{
			base.audio.Play();
			this.m_LastSecond = vp_TimeUtility.SystemTimeToUnits().seconds;
		}
	}

	// Token: 0x06004E6A RID: 20074 RVA: 0x001ACDA0 File Offset: 0x001AAFA0
	private void DrawDigital()
	{
		string text = string.Empty;
		text = vp_TimeUtility.SystemTimeToString(true, true, true, false, false, false, ':');
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

	// Token: 0x06004E6B RID: 20075 RVA: 0x001ACFC8 File Offset: 0x001AB1C8
	private void DrawAnalog()
	{
		GUI.color = Color.white;
		Vector2 vector;
		vector..ctor((float)Screen.width * 0.5f + this.AnalogPosition.x - (float)this.ImageClockFace.width * 0.5f, (float)Screen.height * 0.5f + this.AnalogPosition.y - (float)this.ImageClockFace.height * 0.5f);
		Vector3 vector2 = Vector3.zero;
		vector2 = vp_TimeUtility.SystemTimeToDegrees(false);
		vector2.x = vp_TimeUtility.SystemTimeToDegrees(true).x;
		GUIUtility.RotateAroundPivot(vector2.z, vector + new Vector2((float)this.ImageClockFace.width * 0.5f + this.HandPos.x, (float)this.ImageClockFace.height * 0.5f + this.HandPos.y));
		GUI.DrawTexture(new Rect(vector.x + this.HandPos.x, vector.y + this.HandPos.y, (float)this.ImageClockFace.width, (float)this.ImageClockFace.height), this.ImageClockHandSecond);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
		GUIUtility.RotateAroundPivot(vector2.x, vector + new Vector2((float)this.ImageClockFace.width * 0.5f + this.HandPos.x, (float)this.ImageClockFace.height * 0.5f + this.HandPos.y));
		GUI.DrawTexture(new Rect(vector.x + this.HandPos.x, vector.y + this.HandPos.y, (float)this.ImageClockFace.width, (float)this.ImageClockFace.height), this.ImageClockHandHour);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
		GUIUtility.RotateAroundPivot(vector2.y, vector + new Vector2((float)this.ImageClockFace.width * 0.5f + this.HandPos.x, (float)this.ImageClockFace.height * 0.5f + this.HandPos.y));
		GUI.DrawTexture(new Rect(vector.x + this.HandPos.x, vector.y + this.HandPos.y, (float)this.ImageClockFace.width, (float)this.ImageClockFace.height), this.ImageClockHandMinute);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
		GUI.color = Color.white;
		GUI.DrawTexture(new Rect(vector.x, vector.y, (float)this.ImageClockFace.width, (float)this.ImageClockFace.height), this.ImageClockFace);
	}

	// Token: 0x04003CE6 RID: 15590
	public bool DrawDigitalClock = true;

	// Token: 0x04003CE7 RID: 15591
	public bool DrawAnalogClock = true;

	// Token: 0x04003CE8 RID: 15592
	public Vector2 FontPosOffset = new Vector2(14f, 13f);

	// Token: 0x04003CE9 RID: 15593
	public float FontBackMargin = 8f;

	// Token: 0x04003CEA RID: 15594
	public float FontBackSizeOffset = -29f;

	// Token: 0x04003CEB RID: 15595
	public Vector2 DigitalPosition = new Vector2(-255f, -230f);

	// Token: 0x04003CEC RID: 15596
	public Vector2 AnalogPosition = new Vector2(240f, 140f);

	// Token: 0x04003CED RID: 15597
	public Vector2 HandPos = new Vector2(0f, 0f);

	// Token: 0x04003CEE RID: 15598
	public Color DigitalNumbersColor = new Color(0.3f, 0.3f, 1f, 1f);

	// Token: 0x04003CEF RID: 15599
	private int m_LastSecond;

	// Token: 0x04003CF0 RID: 15600
	public Texture ImagePixel;

	// Token: 0x04003CF1 RID: 15601
	public Texture ImagePixelBlack;

	// Token: 0x04003CF2 RID: 15602
	public Texture ImageDigitalDisplay;

	// Token: 0x04003CF3 RID: 15603
	public Texture ImageClockFace;

	// Token: 0x04003CF4 RID: 15604
	public Texture ImageClockHandHour;

	// Token: 0x04003CF5 RID: 15605
	public Texture ImageClockHandMinute;

	// Token: 0x04003CF6 RID: 15606
	public Texture ImageClockHandSecond;

	// Token: 0x04003CF7 RID: 15607
	public GUISkin Skin;

	// Token: 0x04003CF8 RID: 15608
	private GUIStyle m_DigitalDisplayStyle;
}
