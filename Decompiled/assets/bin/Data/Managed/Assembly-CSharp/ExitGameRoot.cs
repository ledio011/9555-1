using System;
using UnityEngine;

// Token: 0x02000910 RID: 2320
public class ExitGameRoot : SingletonUnity<ExitGameRoot>
{
	// Token: 0x06003FD5 RID: 16341 RVA: 0x0012ACA4 File Offset: 0x00128EA4
	public void Reset()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.IsFullScreenSmallReady() && !LocalDataSaveManager.AdFree)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.ShowFullScreenExitSmall();
			this.upTra.localPosition = new Vector3(0f, 167f, 0f);
			this.downTra.localPosition = new Vector3(0f, -182f, 0f);
		}
		else
		{
			this.upTra.localPosition = new Vector3(0f, 40f, 0f);
			this.downTra.localPosition = new Vector3(0f, -30f, 0f);
		}
	}

	// Token: 0x06003FD6 RID: 16342 RVA: 0x0012AD58 File Offset: 0x00128F58
	public void CloseAd()
	{
		this.upTra.localPosition = new Vector3(0f, 40f, 0f);
		this.downTra.localPosition = new Vector3(0f, -30f, 0f);
	}

	// Token: 0x06003FD7 RID: 16343 RVA: 0x0012ADA4 File Offset: 0x00128FA4
	public void OnClickExitBtn()
	{
		SingletonDontDestoryUnity<NetManager>.Instance.LeaveGame();
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ExitGameRoot);
		Application.Quit();
	}

	// Token: 0x06003FD8 RID: 16344 RVA: 0x0012ADC4 File Offset: 0x00128FC4
	public void OnClickMoreGame()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.ShowMoreGames();
	}

	// Token: 0x06003FD9 RID: 16345 RVA: 0x0012ADD0 File Offset: 0x00128FD0
	public void OnClickNoBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ExitGameRoot);
		if (SingletonDontDestoryUnity<GameManager>.Instance.IsFullScreenSmallShowing())
		{
			SingletonDontDestoryUnity<GameManager>.Instance.HideFullScreenSmall();
		}
	}

	// Token: 0x04002BBA RID: 11194
	public Transform upTra;

	// Token: 0x04002BBB RID: 11195
	public Transform downTra;
}
