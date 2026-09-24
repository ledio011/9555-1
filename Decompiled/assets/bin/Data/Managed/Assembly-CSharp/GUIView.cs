using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class GUIView : MonoBehaviour
{
	// Token: 0x06000002 RID: 2 RVA: 0x00002100 File Offset: 0x00000300
	private void Start()
	{
		this.RefreshData();
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002108 File Offset: 0x00000308
	private void Update()
	{
		if (Input.anyKeyDown)
		{
			this.RefreshData();
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000211C File Offset: 0x0000031C
	private void OnGUI()
	{
		int num = 450;
		int num2 = 400;
		GUILayout.BeginArea(new Rect((float)(Screen.width / 2 - num / 2), (float)(Screen.height / 2 - num2 / 2), (float)num, (float)num2));
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		string text = "Unfortunately, this will take a a few seconds. This is due Unity working different on a Mac :(";
		GUILayout.TextArea(string.Format("Instructions: {0} 1) Open the Advanced PlayerPrefs Window and dock it somewhere. {0} 2) Change the values in the scene using the gui widgets. {0} 3) Go back to the Advanced PlayerPrefs Window and click the refresh button. " + ((Application.platform != null) ? string.Empty : text) + " {0} 4) Observe that the values in the Advanced PlayerPrefs Window has changed to your scene input. {0}{0} 5) Now in the Advanced PlayerPrefs Window, change the values and save those changes {0} 6) Go give the scene focus by clicking in the sceneview. {0} 7) Watch the gui values update to your changes", Environment.NewLine), new GUILayoutOption[0]);
		GUILayout.Space(12f);
		GUILayout.Label("Progress: " + (int)this.progress + "%", new GUILayoutOption[0]);
		float num3 = GUILayout.HorizontalSlider(this.progress, 0f, 100f, new GUILayoutOption[0]);
		if (!Mathf.Approximately(num3, this.progress))
		{
			this.progress = num3;
			this.SaveData();
		}
		GUILayout.Space(12f);
		bool flag = GUILayout.Toggle(this.muted, "Is Audio Muted?", new GUILayoutOption[0]);
		if (flag != this.muted)
		{
			this.muted = flag;
			this.SaveData();
		}
		GUILayout.Space(12f);
		GUILayout.Label("Highscore: " + this.score, new GUILayoutOption[0]);
		GUILayout.Space(12f);
		GUILayout.Label("Playername", new GUILayoutOption[0]);
		string text2 = GUILayout.TextField(this.playername, new GUILayoutOption[0]);
		if (text2 != this.playername)
		{
			this.playername = text2;
			this.SaveData();
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000022D0 File Offset: 0x000004D0
	public void RefreshData()
	{
		this.progress = PlayerPrefs.GetFloat("Progress", 100f);
		this.muted = (PlayerPrefs.GetString("IsSoundMuted", "true") == "true");
		this.score = PlayerPrefs.GetInt("Highscore", 123);
		this.playername = PlayerPrefs.GetString("PlayerName", "Noname");
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002338 File Offset: 0x00000538
	public void SaveData()
	{
		PlayerPrefs.SetFloat("Progress", this.progress);
		PlayerPrefs.SetString("IsSoundMuted", (!this.muted) ? "false" : "true");
		PlayerPrefs.SetInt("Highscore", this.score);
		PlayerPrefs.SetString("PlayerName", this.playername);
	}

	// Token: 0x04000001 RID: 1
	private const string PROGRESS_KEY = "Progress";

	// Token: 0x04000002 RID: 2
	private const string MUTED_KEY = "IsSoundMuted";

	// Token: 0x04000003 RID: 3
	private const string SCORE_KEY = "Highscore";

	// Token: 0x04000004 RID: 4
	private const string PLAYERNAME_KEY = "PlayerName";

	// Token: 0x04000005 RID: 5
	private float progress;

	// Token: 0x04000006 RID: 6
	private bool muted;

	// Token: 0x04000007 RID: 7
	private int score;

	// Token: 0x04000008 RID: 8
	private string playername = "Noname";
}
