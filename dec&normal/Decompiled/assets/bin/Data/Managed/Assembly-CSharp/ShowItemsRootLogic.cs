using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000991 RID: 2449
public class ShowItemsRootLogic : SingletonUnity<ShowItemsRootLogic>
{
	// Token: 0x06004559 RID: 17753 RVA: 0x0015C154 File Offset: 0x0015A354
	public void Clear()
	{
		this.onClickClose = null;
		this.onClickNo = null;
		this.onClickYes = null;
	}

	// Token: 0x0600455A RID: 17754 RVA: 0x0015C16C File Offset: 0x0015A36C
	public void ResetNoBtn(string rewardId, string titlestr, string infostr, DelegateDefine.NoParamDelegate closefun = null, params object[] args)
	{
		ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(rewardId);
		if (showRewardDataByID != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.showrewarditem.gameObject, true);
			this.showrewarditem.ShowRewards(new List<string>(showRewardDataByID.ItemIdList), new List<EQUIP_QUALITY>(showRewardDataByID.QualityList), new List<int>(showRewardDataByID.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.showrewarditem.gameObject, false);
		}
		this.titlelabel.text = StrDictionary.GetDictionaryString(titlestr, new object[0]);
		this.infoLabel.text = StrDictionary.GetDictionaryString(infostr, args);
		this.Clear();
		this.onClickClose = closefun;
		NGUITools.SetActive(this.YESobj, false);
		NGUITools.SetActive(this.NOobj, false);
	}

	// Token: 0x0600455B RID: 17755 RVA: 0x0015C22C File Offset: 0x0015A42C
	public static void ShowYestOrNoBtn(ItemData itemData, int count, string title, string infostr, DelegateDefine.NoParamDelegate yesfun = null, DelegateDefine.NoParamDelegate nofun = null, params object[] args)
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShowItemsRoot, delegate
		{
			SingletonUnity<ShowItemsRootLogic>.Instance.ResetYesNoBtn(itemData, count, title, infostr, yesfun, nofun, args);
		}, null);
	}

	// Token: 0x0600455C RID: 17756 RVA: 0x0015C290 File Offset: 0x0015A490
	public void ResetYesNoBtn(ItemData itemData, int count, string titlestr, string infostr, DelegateDefine.NoParamDelegate yesfun, DelegateDefine.NoParamDelegate nofun, params object[] args)
	{
		this.showrewarditem.ShowRewards(itemData, count);
		this.titlelabel.text = StrDictionary.GetDictionaryString(titlestr, new object[0]);
		this.infoLabel.text = StrDictionary.GetDictionaryString(infostr, args);
		this.Clear();
		this.onClickYes = yesfun;
		this.onClickNo = nofun;
		this.onClickClose = nofun;
		this.YESobj.transform.localPosition = new Vector3(106f, -115f, 0f);
		this.NOobj.transform.localPosition = new Vector3(-106f, -115f, 0f);
	}

	// Token: 0x0600455D RID: 17757 RVA: 0x0015C33C File Offset: 0x0015A53C
	public void ResetYesNoBtn(List<item> items, string titlestr, string infostr, DelegateDefine.NoParamDelegate yesfun, DelegateDefine.NoParamDelegate nofun, params object[] args)
	{
		if (items.Count != 0)
		{
			UnityVersionUtil.SetActiveRecursive(this.showrewarditem.gameObject, true);
			this.showrewarditem.ShowRewards(items);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.showrewarditem.gameObject, false);
		}
		this.titlelabel.text = StrDictionary.GetDictionaryString(titlestr, new object[0]);
		this.infoLabel.text = StrDictionary.GetDictionaryString(infostr, args);
		this.Clear();
		this.onClickYes = yesfun;
		this.onClickNo = nofun;
		this.onClickClose = nofun;
		this.YESobj.transform.localPosition = new Vector3(106f, -115f, 0f);
		this.NOobj.transform.localPosition = new Vector3(-106f, -115f, 0f);
	}

	// Token: 0x0600455E RID: 17758 RVA: 0x0015C418 File Offset: 0x0015A618
	public static void ShowYesBtn(ItemData itemData, string title, string infostr, DelegateDefine.NoParamDelegate yesfun = null, params object[] args)
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShowItemsRoot, delegate
		{
			SingletonUnity<ShowItemsRootLogic>.Instance.ResetYesBtn(itemData, title, infostr, yesfun, args);
		}, null);
	}

	// Token: 0x0600455F RID: 17759 RVA: 0x0015C46C File Offset: 0x0015A66C
	public void ResetYesBtn(ItemData itemData, string titlestr, string infostr, DelegateDefine.NoParamDelegate yesfun = null, params object[] args)
	{
		this.showrewarditem.ShowRewards(itemData, 1);
		this.titlelabel.text = StrDictionary.GetDictionaryString(titlestr, new object[0]);
		this.infoLabel.text = StrDictionary.GetDictionaryString(infostr, args);
		this.Clear();
		this.onClickYes = yesfun;
		NGUITools.SetActive(this.YESobj, true);
		NGUITools.SetActive(this.NOobj, false);
		this.YESobj.transform.localPosition = new Vector3(0f, -115f, 0f);
	}

	// Token: 0x06004560 RID: 17760 RVA: 0x0015C4FC File Offset: 0x0015A6FC
	public void OnClickcloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShowItemsRoot);
		if (this.onClickClose != null)
		{
			this.onClickClose();
		}
	}

	// Token: 0x06004561 RID: 17761 RVA: 0x0015C524 File Offset: 0x0015A724
	public void OnClickYesBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShowItemsRoot);
		if (this.onClickYes != null)
		{
			this.onClickYes();
		}
	}

	// Token: 0x06004562 RID: 17762 RVA: 0x0015C54C File Offset: 0x0015A74C
	public void OnClickNoBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ShowItemsRoot);
		if (this.onClickNo != null)
		{
			this.onClickNo();
		}
	}

	// Token: 0x0400323A RID: 12858
	public UILabel titlelabel;

	// Token: 0x0400323B RID: 12859
	public UILabel infoLabel;

	// Token: 0x0400323C RID: 12860
	public ShowRewardItems showrewarditem;

	// Token: 0x0400323D RID: 12861
	public UILabel yesLabel;

	// Token: 0x0400323E RID: 12862
	public UILabel noLabel;

	// Token: 0x0400323F RID: 12863
	public GameObject YESobj;

	// Token: 0x04003240 RID: 12864
	public GameObject NOobj;

	// Token: 0x04003241 RID: 12865
	private DelegateDefine.NoParamDelegate onClickClose;

	// Token: 0x04003242 RID: 12866
	private DelegateDefine.NoParamDelegate onClickNo;

	// Token: 0x04003243 RID: 12867
	private DelegateDefine.NoParamDelegate onClickYes;
}
