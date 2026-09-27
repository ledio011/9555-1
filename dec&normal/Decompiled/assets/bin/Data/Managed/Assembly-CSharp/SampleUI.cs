using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class SampleUI : MonoBehaviour
{
	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600005E RID: 94 RVA: 0x00003A58 File Offset: 0x00001C58
	// (set) Token: 0x0600005F RID: 95 RVA: 0x00003A60 File Offset: 0x00001C60
	public string StatusText
	{
		get
		{
			return this.statusText;
		}
		set
		{
			this.statusText = value;
		}
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00003A6C File Offset: 0x00001C6C
	private void Awake()
	{
		this.titleStyle = new GUIStyle(this.skin.label);
		this.titleStyle.alignment = 4;
		this.titleStyle.normal.textColor = this.titleColor;
		this.statusStyle = new GUIStyle(this.skin.label);
		this.statusStyle.alignment = 7;
		this.helpStyle = new GUIStyle(this.skin.label);
		this.helpStyle.alignment = 0;
		this.helpStyle.padding.left = 5;
		this.helpStyle.padding.right = 5;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00003B18 File Offset: 0x00001D18
	public static void ApplyVirtualScreen()
	{
		GUI.matrix = Matrix4x4.Scale(new Vector3((float)Screen.width / SampleUI.VirtualScreenWidth, (float)Screen.height / SampleUI.VirtualScreenHeight, 1f));
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00003B54 File Offset: 0x00001D54
	protected virtual void OnGUI()
	{
		if (this.skin != null)
		{
			GUI.skin = this.skin;
		}
		SampleUI.ApplyVirtualScreen();
		GUI.Box(this.topBarRect, string.Empty);
		if (GUI.Button(this.backButtonRect, "Back"))
		{
			Application.LoadLevel(0);
		}
		GUI.Label(this.titleRect, "FingerGestures - " + base.name, this.titleStyle);
		if (this.showStatusText)
		{
			GUI.Label(this.statusTextRect, this.statusText, this.statusStyle);
		}
		if (this.helpText.Length > 0 && this.showHelpButton && !this.showHelp && GUI.Button(this.helpButtonRect, "Help"))
		{
			this.showHelp = true;
		}
		if (this.showHelp)
		{
			GUI.Box(this.helpRect, "Help");
			GUILayout.BeginArea(this.helpRect);
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			GUILayout.Space(25f);
			GUILayout.Label(this.helpText, this.helpStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Close", new GUILayoutOption[]
			{
				GUILayout.Height(40f)
			}))
			{
				this.showHelp = false;
			}
			GUILayout.EndVertical();
			GUILayout.EndArea();
		}
	}

	// Token: 0x04000048 RID: 72
	public GUISkin skin;

	// Token: 0x04000049 RID: 73
	public Color titleColor = Color.white;

	// Token: 0x0400004A RID: 74
	private GUIStyle titleStyle;

	// Token: 0x0400004B RID: 75
	private GUIStyle statusStyle;

	// Token: 0x0400004C RID: 76
	private GUIStyle helpStyle;

	// Token: 0x0400004D RID: 77
	private Rect topBarRect = new Rect(0f, -4f, 600f, 56f);

	// Token: 0x0400004E RID: 78
	private Rect backButtonRect = new Rect(5f, 2f, 80f, 46f);

	// Token: 0x0400004F RID: 79
	private Rect titleRect = new Rect(100f, 2f, 400f, 46f);

	// Token: 0x04000050 RID: 80
	private Rect helpButtonRect = new Rect(515f, 2f, 80f, 46f);

	// Token: 0x04000051 RID: 81
	private Rect statusTextRect = new Rect(30f, 336f, 540f, 60f);

	// Token: 0x04000052 RID: 82
	private Rect helpRect = new Rect(50f, 60f, 500f, 300f);

	// Token: 0x04000053 RID: 83
	private string statusText = string.Empty;

	// Token: 0x04000054 RID: 84
	public bool showStatusText = true;

	// Token: 0x04000055 RID: 85
	public string helpText = string.Empty;

	// Token: 0x04000056 RID: 86
	public bool showHelpButton = true;

	// Token: 0x04000057 RID: 87
	public bool showHelp;

	// Token: 0x04000058 RID: 88
	public static readonly float VirtualScreenWidth = 600f;

	// Token: 0x04000059 RID: 89
	public static readonly float VirtualScreenHeight = 400f;
}
