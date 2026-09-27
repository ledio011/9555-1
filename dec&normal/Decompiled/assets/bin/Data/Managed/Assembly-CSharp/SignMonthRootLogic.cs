using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008E8 RID: 2280
public class SignMonthRootLogic : SingletonUnity<SignMonthRootLogic>
{
	// Token: 0x06003DA8 RID: 15784 RVA: 0x0011544C File Offset: 0x0011364C
	public void EnableReset()
	{
		this.curSignState = false;
		this.ReplenishState = false;
		UnityVersionUtil.SetActiveRecursive(this.PriceLabel.gameObject, false);
		this.SignDataList = DataManager.GetSignInMonthDataList();
		int num = this.SignDataList.Count - this.DayItems.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.DayItems[0].gameObject) as GameObject;
				DayRewardItem component = gameObject.GetComponent<DayRewardItem>();
				gameObject.name = string.Format("dayitem{0:D2}", this.DayItems.Count);
				gameObject.transform.parent = this.ParentGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				this.DayItems.Add(component);
			}
		}
		this.ParentGrid.Reposition();
		for (int j = 0; j < this.DayItems.Count; j++)
		{
			this.DayItems[j].ResetPos(j);
			if (j < this.SignDataList.Count)
			{
				this.DayItems[j].UpdateItem(this.SignDataList[j].ItemID, this.SignDataList[j].ItemCount);
			}
			UnityVersionUtil.SetActiveRecursive(this.DayItems[j].gameObject, false);
		}
		UnityVersionUtil.SetActiveRecursive(this.DayEffect.gameObject, false);
	}

	// Token: 0x06003DA9 RID: 15785 RVA: 0x001155DC File Offset: 0x001137DC
	public void UpdateInfo(ret_request_30_day_info.request request)
	{
		this.curSign = (int)request.cur_sign;
		this.sysSign = (int)request.sys_sign;
		this.ReplenishTimes = (int)request.replenish;
		this.curSignState = request.cur_sign_state;
		this.ReplenishState = request.replenish_sign_state;
		this.MonthDays = (int)request.count;
		if (request.HasStr)
		{
			this.ReplenishStr = request.str;
		}
		else
		{
			this.ReplenishStr = string.Empty;
		}
		this.RefershUI();
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "Sign30", "open");
	}

	// Token: 0x06003DAA RID: 15786 RVA: 0x0011567C File Offset: 0x0011387C
	public void UpdateInfo(ret_sign_30_day.request request)
	{
		if (this.curSignState)
		{
			LocalDataSaveManager.SetRewardFlag();
		}
		this.curSign = (int)request.cur_sign;
		this.sysSign = (int)request.sys_sign;
		this.ReplenishTimes = (int)request.replenish;
		this.curSignState = request.cur_sign_state;
		this.ReplenishState = request.replenish_sign_state;
		this.MonthDays = (int)request.count;
		if (request.HasStr)
		{
			this.ReplenishStr = request.str;
		}
		else
		{
			this.ReplenishStr = string.Empty;
		}
		this.RefershUI();
		int num = this.curSign - 1;
		ItemData itemDataByID = DataManager.GetItemDataByID(this.SignDataList[num].ItemID);
		SimpleRewardRootLogic.AddReward(itemDataByID, this.SignDataList[num].ItemCount, itemDataByID.Quality);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "Sign30", string.Format("sign_{0}", num + 1));
	}

	// Token: 0x06003DAB RID: 15787 RVA: 0x00115778 File Offset: 0x00113978
	public void RefershUI()
	{
		this.DayEffect.transform.parent = this.ParentGrid.transform.parent;
		UnityVersionUtil.SetActiveRecursive(this.DayEffect.gameObject, false);
		string[] array = this.ReplenishStr.Split(new char[]
		{
			'#'
		});
		for (int i = 0; i < this.DayItems.Count; i++)
		{
			if (i < this.MonthDays)
			{
				UnityVersionUtil.SetActiveRecursive(this.DayItems[i].gameObject, true);
				if (i > this.sysSign - 1)
				{
					this.DayItems[i].UpdataStateInfo(DayItemState.NONE, false);
				}
				else if (i > this.curSign - 1 && i <= this.sysSign - 1)
				{
					this.DayItems[i].UpdataStateInfo(DayItemState.CAN_REPLENISH, false);
				}
				else if (i < this.curSign - 1)
				{
					bool isRepget = false;
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].Equals((i + 1).ToString()))
						{
							isRepget = true;
							break;
						}
					}
					this.DayItems[i].UpdataStateInfo(DayItemState.ISGET, isRepget);
				}
				else if (this.curSignState)
				{
					this.DayItems[i].UpdataStateInfo(DayItemState.CURSIGN, false);
					this.DayEffect.transform.parent = this.DayItems[i].transform;
					this.DayEffect.localPosition = Vector3.zero;
					UnityVersionUtil.SetActiveRecursive(this.DayEffect.gameObject, true);
				}
				else
				{
					bool isRepget2 = false;
					for (int k = 0; k < array.Length; k++)
					{
						if (array[k].Equals((i + 1).ToString()))
						{
							isRepget2 = true;
							break;
						}
					}
					this.DayItems[i].UpdataStateInfo(DayItemState.ISGET, isRepget2);
				}
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.DayItems[i].gameObject, false);
			}
		}
		this.ParentGrid.Reposition();
		if (this.curSignState)
		{
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{300201}", new object[0]);
			this.BtnSp.spriteName = GameDefine.BtnIconNew[0];
			UnityVersionUtil.SetActiveRecursive(this.PriceLabel.gameObject, false);
		}
		else if (this.ReplenishState)
		{
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{300202}", new object[0]);
			this.BtnSp.spriteName = GameDefine.BtnIconNew[0];
			SignInMonthData signInMonthData = this.SignDataList[this.ReplenishTimes];
			this.PriceFlag.spriteName = GameMoneyHelper.GetMoneyIcon((long)signInMonthData.PriceType);
			this.PriceLabel.text = string.Format("{0}", signInMonthData.PriceCost);
			UnityVersionUtil.SetActiveRecursive(this.PriceLabel.gameObject, true);
		}
		else
		{
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{300403}", new object[0]);
			this.BtnSp.spriteName = GameDefine.BtnIconNew[1];
			UnityVersionUtil.SetActiveRecursive(this.PriceLabel.gameObject, false);
		}
	}

	// Token: 0x06003DAC RID: 15788 RVA: 0x00115AC8 File Offset: 0x00113CC8
	public void OnClickSignBtn()
	{
		if (this.curSignState || this.ReplenishState)
		{
			if (!this.curSignState)
			{
				SignInMonthData signInMonthData = this.SignDataList[this.ReplenishTimes];
				if (!GameMoneyHelper.BeforeCheckBuy(signInMonthData.PriceType, signInMonthData.PriceCost))
				{
					return;
				}
			}
			WaitResponseUIRootLogic.OpenWaitBox(254, 10f, 0f, null);
			sign_30_day.request request = new sign_30_day.request();
			request.day = (long)this.curSign;
			NetLogic.GetInstance().Send<Protocol.sign_30_day>(null, null);
		}
	}

	// Token: 0x04002936 RID: 10550
	public UISprite BtnSp;

	// Token: 0x04002937 RID: 10551
	public UILabel BtnLabel;

	// Token: 0x04002938 RID: 10552
	public List<DayRewardItem> DayItems;

	// Token: 0x04002939 RID: 10553
	private List<SignInMonthData> SignDataList;

	// Token: 0x0400293A RID: 10554
	public UIGrid ParentGrid;

	// Token: 0x0400293B RID: 10555
	public UISprite PriceFlag;

	// Token: 0x0400293C RID: 10556
	public UILabel PriceLabel;

	// Token: 0x0400293D RID: 10557
	private int curSign;

	// Token: 0x0400293E RID: 10558
	private int sysSign;

	// Token: 0x0400293F RID: 10559
	private int ReplenishTimes;

	// Token: 0x04002940 RID: 10560
	private bool curSignState;

	// Token: 0x04002941 RID: 10561
	private bool ReplenishState;

	// Token: 0x04002942 RID: 10562
	private int MonthDays;

	// Token: 0x04002943 RID: 10563
	private string ReplenishStr = string.Empty;

	// Token: 0x04002944 RID: 10564
	public Transform DayEffect;
}
