using System;
using UnityEngine;

// Token: 0x02000872 RID: 2162
public class BarFightSceneManager : SceneManager
{
	// Token: 0x0600395A RID: 14682 RVA: 0x000F51D0 File Offset: 0x000F33D0
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

	// Token: 0x0600395B RID: 14683 RVA: 0x000F5228 File Offset: 0x000F3428
	~BarFightSceneManager()
	{
	}

	// Token: 0x0600395C RID: 14684 RVA: 0x000F5260 File Offset: 0x000F3460
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

	// Token: 0x04002586 RID: 9606
	private float CheckInterTime;

	// Token: 0x04002587 RID: 9607
	private float curTemptime;
}
