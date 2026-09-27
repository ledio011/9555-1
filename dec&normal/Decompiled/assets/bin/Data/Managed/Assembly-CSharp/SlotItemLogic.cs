using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200099A RID: 2458
public class SlotItemLogic : MonoBehaviour
{
	// Token: 0x060045AB RID: 17835 RVA: 0x0015EDF8 File Offset: 0x0015CFF8
	public void Reset(slot_data curinfo)
	{
		ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(curinfo.ShowRewardID);
		if (showRewardDataByID != null)
		{
			this.rewardshow.ShowRewards(new List<string>(showRewardDataByID.ItemIdList), new List<EQUIP_QUALITY>(showRewardDataByID.QualityList), new List<int>(showRewardDataByID.CountList));
		}
		string rewardMap = curinfo.RewardMap;
		if (string.IsNullOrEmpty(rewardMap) && curinfo.ID.Equals("9999"))
		{
			this.SpList[0].spriteName = "CZ_slot_Star";
			this.SpList[0].SetDimensions(30, 30);
			this.SpList[0].enabled = true;
			this.SpList[1].enabled = false;
			this.SpList[2].enabled = false;
		}
		else
		{
			this.resultStr[0] = rewardMap.Substring(0, 1);
			this.resultStr[1] = rewardMap.Substring(1, 1);
			this.resultStr[2] = rewardMap.Substring(2, 1);
			List<string> list = new List<string>();
			for (int i = 0; i < this.resultStr.Length; i++)
			{
				if (this.resultStr[i].Equals("a") || this.resultStr[i].Equals("b") || this.resultStr[i].Equals("c"))
				{
					list.Add("a");
				}
				else
				{
					list.Add(this.resultStr[i]);
				}
			}
			for (int j = 0; j < this.SpList.Length; j++)
			{
				if (j < list.Count)
				{
					if (list[j].Equals("a"))
					{
						this.SpList[j].spriteName = "CZ_renWu_wenHao";
					}
					else
					{
						SlotIconData slotIconDataById = DataManager.GetSlotIconDataById(list[j]);
						this.SpList[j].spriteName = slotIconDataById.IconName;
					}
					if (list[j].Equals("1") || list[j].Equals("2"))
					{
						this.SpList[j].SetDimensions(30, 25);
					}
					else if (list[j].Equals("a"))
					{
						this.SpList[j].SetDimensions(20, 28);
					}
					else
					{
						this.SpList[j].SetDimensions(24, 26);
					}
					this.SpList[j].enabled = true;
				}
				else
				{
					this.SpList[j].enabled = false;
				}
			}
		}
	}

	// Token: 0x040032A3 RID: 12963
	public UISprite[] SpList;

	// Token: 0x040032A4 RID: 12964
	public ShowRewardItems rewardshow;

	// Token: 0x040032A5 RID: 12965
	public string[] resultStr;
}
