using System;
using UnityEngine;

// Token: 0x02000890 RID: 2192
public class WildBossSceneManager : SceneManager
{
	// Token: 0x06003B4F RID: 15183 RVA: 0x00102D2C File Offset: 0x00100F2C
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

	// Token: 0x06003B50 RID: 15184 RVA: 0x00102D84 File Offset: 0x00100F84
	~WildBossSceneManager()
	{
	}

	// Token: 0x06003B51 RID: 15185 RVA: 0x00102DBC File Offset: 0x00100FBC
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

	// Token: 0x040026D1 RID: 9937
	private float CheckInterTime;

	// Token: 0x040026D2 RID: 9938
	private float curTemptime;
}
