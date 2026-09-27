using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000950 RID: 2384
public class LevelRewardGetLogic : SingletonUnity<LevelRewardGetLogic>
{
	// Token: 0x060042B3 RID: 17075 RVA: 0x00146700 File Offset: 0x00144900
	private void OnEnable()
	{
		this.showuiflag = true;
		this.ShowList.Clear();
		this.starttime = Time.time;
		this.mCurSecond = this.WaitTime;
	}

	// Token: 0x060042B4 RID: 17076 RVA: 0x0014672C File Offset: 0x0014492C
	public void EnableReset()
	{
		for (int i = 0; i < this.rewardItems.Count; i++)
		{
			UnityVersionUtil.SetActiveRecursive(this.rewardItems[i].gameObject, false);
		}
	}

	// Token: 0x060042B5 RID: 17077 RVA: 0x0014676C File Offset: 0x0014496C
	public void ShowRewardList(List<LevelPackageData> list)
	{
		this.ShowList = list;
		this.Level_Dic = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.LevelPackDic;
		this.ShowNextLevelreward();
	}

	// Token: 0x060042B6 RID: 17078 RVA: 0x001467A0 File Offset: 0x001449A0
	private void Update()
	{
		if (this.showuiflag)
		{
			this.mTimeCount = Time.time - this.starttime;
			if ((int)((float)this.WaitTime - this.mTimeCount) < this.mCurSecond)
			{
				this.mCurSecond = (int)((float)this.WaitTime - this.mTimeCount);
				this.TimeLabel.text = string.Format("{0}s", this.mCurSecond);
			}
			if (this.mTimeCount >= (float)this.WaitTime)
			{
				this.OnClickBtn();
			}
		}
	}

	// Token: 0x060042B7 RID: 17079 RVA: 0x00146834 File Offset: 0x00144A34
	private void ShowNextLevelreward()
	{
		if (this.ShowList.Count > 0)
		{
			this.showdata = this.ShowList[0];
			this.ShowList.RemoveAt(0);
			if (this.Level_Dic.Count > 0 && this.Level_Dic.ContainsKey(this.showdata.ID))
			{
				this.UpdateInfo(this.Level_Dic[this.showdata.ID], this.showdata);
				this.starttime = Time.time;
				this.mCurSecond = this.WaitTime;
			}
			else
			{
				this.ShowNextLevelreward();
			}
		}
		else
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LevelRewardGetRoot);
		}
	}

	// Token: 0x060042B8 RID: 17080 RVA: 0x001468F4 File Offset: 0x00144AF4
	public void UpdateInfo(level_pack curinfo, LevelPackageData curdata)
	{
		this.curInfo = curinfo;
		this.curData = curdata;
		if (GameManager.IsSupportCurDataVersion47())
		{
			this.InfoLabel.text = StrDictionary.GetDictionaryString("#{301118}", new object[]
			{
				this.curData.LvTarget
			});
		}
		else
		{
			this.InfoLabel.text = string.Format("Congratulations to level {0}", this.curData.LvTarget);
		}
		this.ItemList.Clear();
		if (!string.IsNullOrEmpty(this.curData.ItemID1))
		{
			this.ItemList.Add(new GameItem(this.curData.ItemID1, (EQUIP_QUALITY)this.curData.Quality1, this.curData.ItemCount1));
		}
		if (!string.IsNullOrEmpty(this.curData.ItemID2))
		{
			this.ItemList.Add(new GameItem(this.curData.ItemID2, (EQUIP_QUALITY)this.curData.Quality2, this.curData.ItemCount2));
		}
		if (!string.IsNullOrEmpty(this.curData.ItemID3))
		{
			this.ItemList.Add(new GameItem(this.curData.ItemID3, (EQUIP_QUALITY)this.curData.Quality3, this.curData.ItemCount3));
		}
		if (!string.IsNullOrEmpty(this.curData.ItemID4))
		{
			this.ItemList.Add(new GameItem(this.curData.ItemID4, (EQUIP_QUALITY)this.curData.Quality4, this.curData.ItemCount4));
		}
		if (!string.IsNullOrEmpty(this.curData.ItemID5))
		{
			this.ItemList.Add(new GameItem(this.curData.ItemID5, (EQUIP_QUALITY)this.curData.Quality5, this.curData.ItemCount5));
		}
		int num = this.ItemList.Count - this.rewardItems.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.rewardItems[0].gameObject) as GameObject;
				RewardItem component = gameObject.GetComponent<RewardItem>();
				gameObject.name = string.Format("reward{0:D2}", this.rewardItems.Count);
				gameObject.transform.parent = this.parentGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				this.rewardItems.Add(component);
			}
		}
		for (int j = 0; j < this.rewardItems.Count; j++)
		{
			if (j < this.ItemList.Count)
			{
				int level = 0;
				if (this.ItemList[j].ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(this.ItemList[j].ItemData.SubType);
				}
				this.rewardItems[j].UpdateItem(this.ItemList[j], level, false);
				UnityVersionUtil.SetActiveRecursive(this.rewardItems[j].gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.rewardItems[j].gameObject, false);
			}
		}
		this.parentGrid.Reposition();
	}

	// Token: 0x060042B9 RID: 17081 RVA: 0x00146C64 File Offset: 0x00144E64
	public void OnClickBtn()
	{
		if (this.curInfo.state == 1L)
		{
			require_level_reward.request request = new require_level_reward.request();
			request.ID = this.curInfo.ID;
			NetLogic.GetInstance().Send<Protocol.require_level_reward>(request, null);
		}
		this.ShowNextLevelreward();
	}

	// Token: 0x04002EFB RID: 12027
	public UILabel InfoLabel;

	// Token: 0x04002EFC RID: 12028
	public List<RewardItem> rewardItems;

	// Token: 0x04002EFD RID: 12029
	private level_pack curInfo;

	// Token: 0x04002EFE RID: 12030
	private LevelPackageData curData;

	// Token: 0x04002EFF RID: 12031
	public UIGrid parentGrid;

	// Token: 0x04002F00 RID: 12032
	private List<GameItem> ItemList = new List<GameItem>();

	// Token: 0x04002F01 RID: 12033
	private List<level_pack> Level_list;

	// Token: 0x04002F02 RID: 12034
	private List<LevelPackageData> LevelDataList = new List<LevelPackageData>();

	// Token: 0x04002F03 RID: 12035
	private Dictionary<string, level_pack> Level_Dic = new Dictionary<string, level_pack>();

	// Token: 0x04002F04 RID: 12036
	private List<LevelPackageData> ShowList = new List<LevelPackageData>();

	// Token: 0x04002F05 RID: 12037
	private LevelPackageData showdata;

	// Token: 0x04002F06 RID: 12038
	private bool showuiflag;

	// Token: 0x04002F07 RID: 12039
	private float starttime;

	// Token: 0x04002F08 RID: 12040
	private int WaitTime = 20;

	// Token: 0x04002F09 RID: 12041
	public UILabel TimeLabel;

	// Token: 0x04002F0A RID: 12042
	private int mCurSecond;

	// Token: 0x04002F0B RID: 12043
	private float mTimeCount;
}
