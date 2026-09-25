using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008F7 RID: 2295
public class DailyRewardNewLogic : SingletonUnity<DailyRewardNewLogic>
{
	// Token: 0x06003E77 RID: 15991 RVA: 0x0011E404 File Offset: 0x0011C604
	public void EnableReset()
	{
		UnityVersionUtil.SetActiveRecursive(this.rewardObj, false);
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (GameManager.IsSupportCurDataVersion145())
		{
			this.totalNameLabel.text = StrDictionary.GetDictionaryString("#{301125}", new object[0]);
			this.infolabel.text = StrDictionary.GetDictionaryString("#{300817}", new object[]
			{
				TimeTools.GetLocalShowTime_HM((long)playerCommonData.ResetTime, playerCommonData.TimeOffset)
			});
		}
		else
		{
			this.totalNameLabel.text = "Today Score";
			this.infolabel.text = string.Format("The score will be reset at {0} everyday.", TimeTools.GetLocalShowTime_HM((long)playerCommonData.ResetTime, playerCommonData.TimeOffset));
		}
	}

	// Token: 0x06003E78 RID: 15992 RVA: 0x0011E4BC File Offset: 0x0011C6BC
	public void Reset(ret_request_daily_active.request request)
	{
		this.rewardList = new List<daily_reward>(request.daily_rewards.Values);
		this.rewardList.Sort(delegate(daily_reward x, daily_reward y)
		{
			if (x.ID.Length == y.ID.Length)
			{
				return x.ID.CompareTo(y.ID);
			}
			return x.ID.Length - y.ID.Length;
		});
		this.RewardDataList.Clear();
		for (int i = 0; i < this.rewardList.Count; i++)
		{
			this.RewardDataList.Add(DataManager.GetDailyActiveRewardDataById(this.rewardList[i].ID));
		}
		this.curScore = (int)request.score;
		UnityVersionUtil.SetActiveRecursive(this.rewardObj, true);
		for (int j = 0; j < this.rewardItemsList.Count; j++)
		{
			if (j < this.rewardList.Count)
			{
				NGUITools.SetActive(this.rewardItemsList[j].gameObject, true);
				this.rewardItemsList[j].UpdateInfo(this.rewardList[j]);
			}
			else
			{
				NGUITools.SetActive(this.rewardItemsList[j].gameObject, false);
			}
		}
		if (this.RewardDataList.Count > 0)
		{
			this.MaxActive = this.RewardDataList[this.RewardDataList.Count - 1].Score;
		}
		this.SetActiveSlider((int)request.score);
		this.ActiveLabel.text = string.Format("{0}/{1}", (int)request.score, this.MaxActive);
		if (request.score >= 100L)
		{
			NGUITools.SetActive(this.AnimaObj, false);
		}
		else
		{
			NGUITools.SetActive(this.AnimaObj, true);
		}
	}

	// Token: 0x06003E79 RID: 15993 RVA: 0x0011E680 File Offset: 0x0011C880
	private void SetActiveSlider(int score)
	{
		int num = -1;
		for (int i = 0; i < this.RewardDataList.Count; i++)
		{
			if (score <= this.RewardDataList[i].Score)
			{
				num = i;
				break;
			}
		}
		if (score > this.RewardDataList[this.RewardDataList.Count - 1].Score)
		{
			this.ActiveSlider.value = 1f;
		}
		else if (num > 0)
		{
			this.ActiveSlider.value = this.sliderValue[num - 1] + (this.sliderValue[num] - this.sliderValue[num - 1]) * ((float)(score - this.RewardDataList[num - 1].Score) / (float)(this.RewardDataList[num].Score - this.RewardDataList[num - 1].Score));
		}
		else
		{
			this.ActiveSlider.value = this.sliderValue[0] * ((float)score / (float)this.RewardDataList[0].Score);
		}
	}

	// Token: 0x06003E7A RID: 15994 RVA: 0x0011E7A0 File Offset: 0x0011C9A0
	public void UpdateInfo(string id)
	{
		for (int i = 0; i < this.rewardList.Count; i++)
		{
			if (this.rewardList[i].ID.Equals(id))
			{
				this.rewardList[i].state = 2L;
			}
		}
		for (int j = 0; j < this.rewardItemsList.Count; j++)
		{
			this.rewardItemsList[j].UpdateInfo(this.rewardList[j]);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "DailyActive", string.Format("require_{0}", id));
	}

	// Token: 0x04002A1A RID: 10778
	public List<DailyRewardNewItem> rewardItemsList;

	// Token: 0x04002A1B RID: 10779
	public GameObject rewardObj;

	// Token: 0x04002A1C RID: 10780
	public UISlider ActiveSlider;

	// Token: 0x04002A1D RID: 10781
	public UILabel ActiveLabel;

	// Token: 0x04002A1E RID: 10782
	private int MaxActive = 100;

	// Token: 0x04002A1F RID: 10783
	private List<daily_reward> rewardList;

	// Token: 0x04002A20 RID: 10784
	private List<DailyActiveRewardData> RewardDataList = new List<DailyActiveRewardData>();

	// Token: 0x04002A21 RID: 10785
	private int curScore;

	// Token: 0x04002A22 RID: 10786
	public GameObject AnimaObj;

	// Token: 0x04002A23 RID: 10787
	public UILabel infolabel;

	// Token: 0x04002A24 RID: 10788
	public UILabel totalNameLabel;

	// Token: 0x04002A25 RID: 10789
	private float[] sliderValue = new float[]
	{
		0.27f,
		0.517f,
		0.75f,
		1f
	};
}
