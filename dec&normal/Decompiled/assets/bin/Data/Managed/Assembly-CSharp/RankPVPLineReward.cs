using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200097D RID: 2429
public class RankPVPLineReward : MonoBehaviour
{
	// Token: 0x060044A4 RID: 17572 RVA: 0x00156EE0 File Offset: 0x001550E0
	public void ShowRewardInfo(LadderRewardData curdata)
	{
		ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(curdata.ShowRewardID);
		if (showRewardDataByID != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.showrewarditem.gameObject, true);
			this.showrewarditem.ShowRewards(new List<string>(showRewardDataByID.ItemIdList), new List<EQUIP_QUALITY>(showRewardDataByID.QualityList), new List<int>(showRewardDataByID.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.showrewarditem.gameObject, false);
		}
		if (curdata.Rankdown == curdata.Rankup)
		{
			this.infoLabel.text = string.Format("Rank {0}", curdata.Rankdown);
		}
		else
		{
			this.infoLabel.text = string.Format("Rank {0}-{1}", curdata.Rankdown, curdata.Rankup);
		}
	}

	// Token: 0x04003155 RID: 12629
	public UILabel infoLabel;

	// Token: 0x04003156 RID: 12630
	public ShowRewardItems showrewarditem;
}
