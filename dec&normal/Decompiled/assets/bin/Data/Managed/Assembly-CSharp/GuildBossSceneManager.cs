using System;
using UnityEngine;

// Token: 0x0200087B RID: 2171
public class GuildBossSceneManager : SceneManager
{
	// Token: 0x060039C3 RID: 14787 RVA: 0x000F7A40 File Offset: 0x000F5C40
	public override void Init(string id)
	{
		base.Init(id);
		this.CheckInterTime = 2f;
		this.curTemptime = 0f;
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MultiRankSmallRootUI, delegate
		{
			SingletonUnity<MultiRankSmallRootLogic>.Instance.EnableReset();
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MultiRankSmallRootUI);
		}, null);
	}

	// Token: 0x060039C4 RID: 14788 RVA: 0x000F7A98 File Offset: 0x000F5C98
	~GuildBossSceneManager()
	{
	}

	// Token: 0x060039C5 RID: 14789 RVA: 0x000F7AD0 File Offset: 0x000F5CD0
	public override void Update()
	{
		base.Update();
		this.curTemptime += Time.deltaTime;
		if (this.curTemptime > this.CheckInterTime)
		{
			this.curTemptime = 0f;
			NetLogic.GetInstance().Send<Protocol.request_battle_info>(null, null);
		}
	}

	// Token: 0x040025D8 RID: 9688
	private float CheckInterTime;

	// Token: 0x040025D9 RID: 9689
	private float curTemptime;
}
