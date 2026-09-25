using System;

// Token: 0x02000882 RID: 2178
public class RealPVPSceneManager : SceneManager
{
	// Token: 0x06003A46 RID: 14918 RVA: 0x000FB848 File Offset: 0x000F9A48
	public override void Init(string id)
	{
		base.Init(id);
		this.LockControl();
	}

	// Token: 0x06003A47 RID: 14919 RVA: 0x000FB858 File Offset: 0x000F9A58
	public void LockControl()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.JueseJiNengQuUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.YiDongKongZhiUI);
	}

	// Token: 0x06003A48 RID: 14920 RVA: 0x000FB878 File Offset: 0x000F9A78
	private void OpenControl()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.JueseJiNengQuUI, null, null);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.YiDongKongZhiUI, null, null);
	}

	// Token: 0x06003A49 RID: 14921 RVA: 0x000FB8A8 File Offset: 0x000F9AA8
	public override void StartGame()
	{
		this.OpenControl();
	}

	// Token: 0x06003A4A RID: 14922 RVA: 0x000FB8B0 File Offset: 0x000F9AB0
	public override void SuccessMission()
	{
	}
}
