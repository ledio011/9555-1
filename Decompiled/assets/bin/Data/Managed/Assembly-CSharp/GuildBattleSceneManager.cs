using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200087A RID: 2170
public class GuildBattleSceneManager : SceneManager
{
	// Token: 0x060039BA RID: 14778 RVA: 0x000F7760 File Offset: 0x000F5960
	public override void Init(string id)
	{
		base.Init(id);
		this.CheckInterTime = 2f;
		this.curTemptime = 0f;
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildBattleInfoRoot, delegate
		{
			SingletonUnity<GuildBattleInfoRoot>.Instance.EnableReset();
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleInfoRoot);
		}, null);
		List<MapAreaInfoData> mapAreaInfoDataListById = DataManager.GetMapAreaInfoDataListById(this.mapInfoData.GatherAreaId);
		if (mapAreaInfoDataListById != null && mapAreaInfoDataListById.Count > 0)
		{
			GameObject gameObject = ResourcesManager.LoadAndInstantiate("Items/GuildBattlePoint") as GameObject;
			this.mBattlePointList.Add(gameObject.GetComponent<GuildBattlePoint>());
			for (int i = 1; i < mapAreaInfoDataListById.Count; i++)
			{
				this.mBattlePointList.Add((Object.Instantiate(gameObject) as GameObject).GetComponent<GuildBattlePoint>());
			}
			for (int j = 0; j < mapAreaInfoDataListById.Count; j++)
			{
				this.mBattlePointList[j].Reset((long)j, new Vector3(mapAreaInfoDataListById[j].PointList[0].x, SceneManager.GetHitHeight(mapAreaInfoDataListById[j].PointList[0].x, mapAreaInfoDataListById[j].PointList[0].y), mapAreaInfoDataListById[j].PointList[0].y), mapAreaInfoDataListById[j].CircleRange, Color.white);
			}
		}
	}

	// Token: 0x060039BB RID: 14779 RVA: 0x000F78D0 File Offset: 0x000F5AD0
	public override void OnLoadingOver()
	{
		base.OnLoadingOver();
		this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		this.LockControl();
	}

	// Token: 0x060039BC RID: 14780 RVA: 0x000F78F0 File Offset: 0x000F5AF0
	private void LockControl()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.JueseJiNengQuUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.YiDongKongZhiUI);
	}

	// Token: 0x060039BD RID: 14781 RVA: 0x000F7910 File Offset: 0x000F5B10
	public void OpenControl()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.JueseJiNengQuUI, null, null);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.YiDongKongZhiUI, null, null);
	}

	// Token: 0x060039BE RID: 14782 RVA: 0x000F7940 File Offset: 0x000F5B40
	~GuildBattleSceneManager()
	{
	}

	// Token: 0x060039BF RID: 14783 RVA: 0x000F7978 File Offset: 0x000F5B78
	public void UpdatePointColor(long id, Color col)
	{
		for (int i = 0; i < this.mBattlePointList.Count; i++)
		{
			if ((long)this.mBattlePointList[i].Id == id)
			{
				this.mBattlePointList[i].UpdateColor(col);
			}
		}
	}

	// Token: 0x060039C0 RID: 14784 RVA: 0x000F79CC File Offset: 0x000F5BCC
	public override void Update()
	{
		base.Update();
		this.curTemptime += Time.deltaTime;
		if (this.curTemptime > this.CheckInterTime)
		{
			this.curTemptime = 0f;
			NetLogic.GetInstance().Send<Protocol.req_guild_score_info>(null, null);
		}
	}

	// Token: 0x040025D3 RID: 9683
	private float CheckInterTime;

	// Token: 0x040025D4 RID: 9684
	private float curTemptime;

	// Token: 0x040025D5 RID: 9685
	private ObjMainPlayer mMainPlayer;

	// Token: 0x040025D6 RID: 9686
	private List<GuildBattlePoint> mBattlePointList = new List<GuildBattlePoint>();
}
