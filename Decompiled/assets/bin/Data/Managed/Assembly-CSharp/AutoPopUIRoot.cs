using System;
using System.Collections.Generic;

// Token: 0x0200091E RID: 2334
public class AutoPopUIRoot : SingletonUnity<AutoPopUIRoot>
{
	// Token: 0x060040D4 RID: 16596 RVA: 0x00133058 File Offset: 0x00131258
	public void Reset()
	{
		AutoPopUIRoot.PopUIList.Clear();
		if (LocalDataSaveManager.GetRateFlag() == 1)
		{
			AutoPopUIRoot.PopUIList.Add(GameDefine.AUTOPOPTYPE.RATE);
		}
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		long push = playerCommonData.Push;
		if (push != -1L)
		{
			if ((push & 1L) != 0L && playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GIFT_CHECK))
			{
				AutoPopUIRoot.PopUIList.Add(GameDefine.AUTOPOPTYPE.SIGNMONTH);
			}
			if ((push & 2L) != 0L && playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GIFT_7DAY))
			{
				AutoPopUIRoot.PopUIList.Add(GameDefine.AUTOPOPTYPE.SIGNWEEK);
			}
			if ((push & 4L) != 0L && playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GIFT_RETRIEVE))
			{
				AutoPopUIRoot.PopUIList.Add(GameDefine.AUTOPOPTYPE.RETRIEVE);
			}
		}
		if (playerCommonData.Big_PackFlag && playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.BIGSALES))
		{
			AutoPopUIRoot.PopUIList.Add(GameDefine.AUTOPOPTYPE.BIGSALE);
		}
		if (DataManager.GetTimerActivityTipsDataList().Count > 0)
		{
			AutoPopUIRoot.PopUIList.Add(GameDefine.AUTOPOPTYPE.TIME_ACTIVITY_TIPS);
		}
		if (push != -1L && (push & 16L) != 0L && playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.MYSTERYSHOP))
		{
			AutoPopUIRoot.PopUIList.Add(GameDefine.AUTOPOPTYPE.MYSTERYSHOP);
		}
		if (AutoPopUIRoot.PopUIList.Count > 0)
		{
			this.ShowPopUI();
		}
		else
		{
			this.Close(false);
		}
	}

	// Token: 0x060040D5 RID: 16597 RVA: 0x0013319C File Offset: 0x0013139C
	public void RemoveUI(GameDefine.AUTOPOPTYPE type)
	{
		AutoPopUIRoot.PopUIList.Remove(type);
	}

	// Token: 0x060040D6 RID: 16598 RVA: 0x001331AC File Offset: 0x001313AC
	public void ShowPopUI()
	{
		this.curShowUI = AutoPopUIRoot.PopUIList[0];
		AutoPopUIRoot.PopUIList.RemoveAt(0);
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		switch (this.curShowUI)
		{
		case GameDefine.AUTOPOPTYPE.RATE:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.RateRoot, null, null);
			break;
		case GameDefine.AUTOPOPTYPE.SIGNMONTH:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CommercialUIRoot, delegate
			{
				SingletonUnity<CommercialUIRootLogic>.Instance.OnClickMonthBtn();
			}, null);
			break;
		case GameDefine.AUTOPOPTYPE.SIGNWEEK:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CommercialUIRoot, delegate
			{
				SingletonUnity<CommercialUIRootLogic>.Instance.OnClickWeekBtn();
			}, null);
			break;
		case GameDefine.AUTOPOPTYPE.BIGSALE:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BigPackRoot, delegate
			{
				SingletonUnity<BigPackRootLogic>.Instance.EnableReset();
				WaitResponseUIRootLogic.OpenWaitBox(260, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.request_big_pack>(null, null);
				int diedFlag = LocalDataSaveManager.GetDiedFlag();
				if (diedFlag == 1)
				{
					LocalDataSaveManager.SetDiedFlag(2);
				}
			}, null);
			break;
		case GameDefine.AUTOPOPTYPE.MYSTERYSHOP:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MysteryShopRoot, delegate
			{
				SingletonUnity<MysteryShopRootLogic>.Instance.EnableReset();
				WaitResponseUIRootLogic.OpenWaitBox(274, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.request_special_big_pack>(null, null);
			}, null);
			break;
		case GameDefine.AUTOPOPTYPE.RETRIEVE:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CommercialUIRoot, delegate
			{
				SingletonUnity<CommercialUIRootLogic>.Instance.OnClickRetrieveBtn();
			}, null);
			break;
		case GameDefine.AUTOPOPTYPE.TIME_ACTIVITY_TIPS:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TimerActivityTipsRoot, delegate
			{
				SingletonUnity<TimerActivityTipsRootLogic>.Instance.Reset();
			}, null);
			break;
		}
	}

	// Token: 0x060040D7 RID: 16599 RVA: 0x00133358 File Offset: 0x00131558
	public void NextPop(bool isUIJump = false)
	{
		if (this.checkCurShowUI())
		{
			return;
		}
		if (AutoPopUIRoot.PopUIList.Count > 0)
		{
			if (!isUIJump)
			{
				this.ShowPopUI();
			}
		}
		else
		{
			this.Close(isUIJump);
		}
	}

	// Token: 0x060040D8 RID: 16600 RVA: 0x0013339C File Offset: 0x0013159C
	public void Close(bool isUIJump = false)
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.PushShowFlag = true;
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.AutoPopUIRoot);
		if (!isUIJump)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.CheckShowUnlockFunction();
		}
	}

	// Token: 0x060040D9 RID: 16601 RVA: 0x001333E0 File Offset: 0x001315E0
	public bool checkCurShowUI()
	{
		switch (this.curShowUI)
		{
		case GameDefine.AUTOPOPTYPE.RATE:
			if (SingletonUnity<UIManager>.Instance.CheckUIExit(UIInfo.RateRoot))
			{
				return true;
			}
			break;
		case GameDefine.AUTOPOPTYPE.SIGNMONTH:
			if (SingletonUnity<UIManager>.Instance.CheckUIExit(UIInfo.CommercialUIRoot))
			{
				return true;
			}
			break;
		case GameDefine.AUTOPOPTYPE.SIGNWEEK:
			if (SingletonUnity<UIManager>.Instance.CheckUIExit(UIInfo.CommercialUIRoot))
			{
				return true;
			}
			break;
		case GameDefine.AUTOPOPTYPE.BIGSALE:
			if (SingletonUnity<UIManager>.Instance.CheckUIExit(UIInfo.BigPackRoot))
			{
				return true;
			}
			break;
		case GameDefine.AUTOPOPTYPE.MYSTERYSHOP:
			if (SingletonUnity<UIManager>.Instance.CheckUIExit(UIInfo.MysteryShopRoot))
			{
				return true;
			}
			break;
		case GameDefine.AUTOPOPTYPE.RETRIEVE:
			if (SingletonUnity<UIManager>.Instance.CheckUIExit(UIInfo.CommercialUIRoot))
			{
				return true;
			}
			break;
		case GameDefine.AUTOPOPTYPE.TIME_ACTIVITY_TIPS:
			if (SingletonUnity<UIManager>.Instance.CheckUIExit(UIInfo.TimerActivityTipsRoot))
			{
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x04002C76 RID: 11382
	public static List<GameDefine.AUTOPOPTYPE> PopUIList = new List<GameDefine.AUTOPOPTYPE>();

	// Token: 0x04002C77 RID: 11383
	private GameDefine.AUTOPOPTYPE curShowUI;
}
