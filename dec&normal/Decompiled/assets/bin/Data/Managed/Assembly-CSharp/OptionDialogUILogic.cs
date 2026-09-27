using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000A3B RID: 2619
public class OptionDialogUILogic : SingletonUnity<OptionDialogUILogic>
{
	// Token: 0x06004C55 RID: 19541 RVA: 0x0019DDF4 File Offset: 0x0019BFF4
	private void Reset()
	{
		this.TitleLabel.text = string.Empty;
		this.TextLabel.text = string.Empty;
		this.Option1Label.text = string.Empty;
		this.Option2Label.text = string.Empty;
		this.mOnClickOk = null;
		this.mParam = string.Empty;
	}

	// Token: 0x06004C56 RID: 19542 RVA: 0x0019DE54 File Offset: 0x0019C054
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.OptionDialogUI);
		Singleton<DialogManager>.Instance.OnCloseDialog();
		SingletonUnity<UIManager>.Instance.CheckReShowUI(UIInfo.OptionDialogUI);
	}

	// Token: 0x06004C57 RID: 19543 RVA: 0x0019DE8C File Offset: 0x0019C08C
	public void OnClickOkBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.OptionDialogUI);
		Singleton<DialogManager>.Instance.OnCloseDialog();
		if (this.mOnClickOk != null)
		{
			this.mOnClickOk(this.mParam);
			return;
		}
		if (this.mCloseOptionFlag)
		{
			return;
		}
		switch (this.optionData.Type)
		{
		case OPTION_TYPE.STORAGE:
			this.OpenStorage();
			break;
		case OPTION_TYPE.WEAPONSHOP:
			this.OpenWeaponShop();
			break;
		case OPTION_TYPE.ENTERCOPY:
			this.EnterCopy();
			break;
		case OPTION_TYPE.SEX_NPC_MAN:
			this.OpenSexManDialog();
			break;
		case OPTION_TYPE.SEX_NPC_WOMEN:
			this.OpenSexWomenDialog();
			break;
		case OPTION_TYPE.OPEN_SHOP:
			this.OpenShop();
			break;
		}
	}

	// Token: 0x06004C58 RID: 19544 RVA: 0x0019DF50 File Offset: 0x0019C150
	private void OpenShop()
	{
		switch (this.mCurNpcData.FunctionType)
		{
		case 4:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
			{
				SingletonUnity<ShopUIRootLogic>.Instance.OnClickBuyDiamondBtn();
			}, null);
			break;
		case 5:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
			{
				SingletonUnity<ShopUIRootLogic>.Instance.OnClickToolsBtn();
			}, null);
			break;
		case 6:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
			{
				SingletonUnity<ShopUIRootLogic>.Instance.OnClickEquipBtn();
			}, null);
			break;
		case 7:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
			{
				SingletonUnity<ShopUIRootLogic>.Instance.OnClickBigSaleBtn();
			}, null);
			break;
		case 8:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
			{
				SingletonUnity<ShopUIRootLogic>.Instance.Reset();
				SingletonUnity<ShopUIRootLogic>.Instance.OnClickGuildBtn();
			}, null);
			break;
		case 9:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
			{
				SingletonUnity<ShopUIRootLogic>.Instance.OnClickMonthlyCardBtn();
			}, null);
			break;
		}
	}

	// Token: 0x06004C59 RID: 19545 RVA: 0x0019E0BC File Offset: 0x0019C2BC
	private void OpenSexManDialog()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SexGameUIRoot, delegate
		{
			SingletonUnity<SexGameUIRootLogic>.Instance.Reset(true, this.optionData.OptionParam, this.mCurNpcData);
		}, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", "activity_8", "accept_man");
	}

	// Token: 0x06004C5A RID: 19546 RVA: 0x0019E100 File Offset: 0x0019C300
	private void OpenSexWomenDialog()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SexGameUIRoot, delegate
		{
			SingletonUnity<SexGameUIRootLogic>.Instance.Reset(false, this.optionData.OptionParam, this.mCurNpcData);
		}, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", "activity_8", "accept_women");
	}

	// Token: 0x06004C5B RID: 19547 RVA: 0x0019E144 File Offset: 0x0019C344
	private void OpenStorage()
	{
	}

	// Token: 0x06004C5C RID: 19548 RVA: 0x0019E148 File Offset: 0x0019C348
	private void OnOpenStorageRoot(bool isSuccess, object param)
	{
	}

	// Token: 0x06004C5D RID: 19549 RVA: 0x0019E14C File Offset: 0x0019C34C
	private void OpenWeaponShop()
	{
	}

	// Token: 0x06004C5E RID: 19550 RVA: 0x0019E150 File Offset: 0x0019C350
	private void OnOpenWeaponShopPage(bool isSuccess, object param)
	{
	}

	// Token: 0x06004C5F RID: 19551 RVA: 0x0019E154 File Offset: 0x0019C354
	private void EnterCopy()
	{
		Debug.Log("EnterCopy");
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
		enter_copy_scene.request request = new enter_copy_scene.request();
		request.mapInfoId = "1";
		NetLogic.GetInstance().Send<Protocol.enter_copy_scene>(request, null);
	}

	// Token: 0x06004C60 RID: 19552 RVA: 0x0019E198 File Offset: 0x0019C398
	public void ResetOptionDialog(string optDiaID, NpcData npcData, bool showCash = false)
	{
		this.Reset();
		if (showCash)
		{
			NGUITools.SetActive(this.CashRoot, true);
			this.CashLabel.text = string.Format("{0:N0}", GameMoneyHelper.GetCash());
		}
		else
		{
			NGUITools.SetActive(this.CashRoot, false);
		}
		this.optionData = DataManager.GetNpcOptionDialogDataByID(optDiaID);
		if (this.optionData != null)
		{
			if (this.optionData.Type == OPTION_TYPE.SEX_NPC_MAN || this.optionData.Type == OPTION_TYPE.SEX_NPC_WOMEN)
			{
				activity_info activity_info = null;
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.CurActivityDataDic.ContainsKey(this.optionData.OptionParam))
				{
					activity_info = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.CurActivityDataDic[this.optionData.OptionParam];
				}
				if (activity_info != null)
				{
					int num = (int)activity_info.CurNum;
					SexMiniData sexMiniDataById = DataManager.GetSexMiniDataById(this.optionData.OptionParam);
					if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.Level < sexMiniDataById.UnlockLevel)
					{
						UnityVersionUtil.SetActiveRecursive(this.NoBtnRoot, false);
						this.TextLabel.text = this.optionData.MOptionFailDialog;
						this.Option1Label.text = this.optionData.MFailOption;
						this.mCloseOptionFlag = true;
					}
					else
					{
						int num2 = sexMiniDataById.CostStart + sexMiniDataById.CostAdd * num;
						if (num2 > sexMiniDataById.CostMax)
						{
							num2 = sexMiniDataById.CostMax;
						}
						this.CashPic.spriteName = GameMoneyHelper.GetMoneyPicName((GameDefine.MONEY_TYPE)sexMiniDataById.CostType);
						this.CashLabel.text = string.Format("{0:N0}", GameMoneyHelper.GetMoneyNum(sexMiniDataById.CostType));
						if (GameMoneyHelper.GetMoneyNum(sexMiniDataById.CostType) < (long)num2)
						{
							UnityVersionUtil.SetActiveRecursive(this.NoBtnRoot, false);
							this.TextLabel.text = StrDictionary.GetDictionaryString(this.optionData.OptionFailDialog, new object[0]);
							this.Option1Label.text = StrDictionary.GetDictionaryString(this.optionData.MFailOption, new object[0]);
							this.mCloseOptionFlag = true;
						}
						else
						{
							this.TextLabel.text = StrDictionary.GetDictionaryString(this.optionData.CenterDialog, new object[]
							{
								GameMoneyHelper.GetMoneyValStr(num2, (GameDefine.MONEY_TYPE)sexMiniDataById.CostType)
							});
							this.Option1Label.text = StrDictionary.GetDictionaryString(this.optionData.MOption1, new object[0]);
							this.Option2Label.text = StrDictionary.GetDictionaryString(this.optionData.MOption2, new object[0]);
						}
					}
				}
				else
				{
					Debug.Log("No Activity Data Error!!!!!!!!!!!!!!!!!!!!!!!!!!");
				}
			}
			else if (this.optionData.Type == OPTION_TYPE.OPEN_SHOP)
			{
				bool flag = false;
				switch (npcData.FunctionType)
				{
				case 4:
					if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SHOP_BUY))
					{
						flag = true;
					}
					break;
				case 5:
					if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SHOP_TOOL))
					{
						flag = true;
					}
					break;
				case 6:
					if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SHOP_EQUIP))
					{
						flag = true;
					}
					break;
				case 7:
					if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SHOP_BIGSALE))
					{
						flag = true;
					}
					break;
				case 8:
					if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SHOP_GUILD))
					{
						flag = true;
					}
					break;
				case 9:
					if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SHOP_VIP))
					{
						flag = true;
					}
					break;
				}
				if (!flag)
				{
					UnityVersionUtil.SetActiveRecursive(this.NoBtnRoot, false);
					this.TextLabel.text = this.optionData.MOptionFailDialog;
					this.Option1Label.text = this.optionData.MFailOption;
					this.mCloseOptionFlag = true;
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(this.NoBtnRoot, true);
					this.TextLabel.text = StrDictionary.GetDictionaryString(this.optionData.CenterDialog, new object[0]);
					this.Option1Label.text = StrDictionary.GetDictionaryString(this.optionData.Option1, new object[0]);
					this.Option2Label.text = StrDictionary.GetDictionaryString(this.optionData.Option2, new object[0]);
					this.mCloseOptionFlag = false;
				}
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.NoBtnRoot, true);
				this.TextLabel.text = StrDictionary.GetDictionaryString(this.optionData.CenterDialog, new object[0]);
				this.Option1Label.text = StrDictionary.GetDictionaryString(this.optionData.Option1, new object[0]);
				this.Option2Label.text = StrDictionary.GetDictionaryString(this.optionData.Option2, new object[0]);
				this.mCloseOptionFlag = false;
			}
		}
		this.NpcFakeObjRoot.EnableFakeObjRoot();
		this.NpcPic.mainTexture = this.NpcFakeObjRoot.ModelPic;
		this.NpcFakeObj.InitFakeNpcObj(npcData.Model, this.NpcFakeObjRoot.MeshRoot, null);
		this.mCurNpcData = npcData;
	}

	// Token: 0x06004C61 RID: 19553 RVA: 0x0019E6D4 File Offset: 0x0019C8D4
	public void ResetOptionDialog(NpcData npcData, string textStr, string option1Str, string option2Str, string param, DelegateDefine.OneStringParamDelegate func, bool showCash = false)
	{
		this.Reset();
		this.TextLabel.text = StrDictionary.GetDictionaryString(textStr, new object[0]);
		this.Option1Label.text = StrDictionary.GetDictionaryString(option1Str, new object[0]);
		this.Option2Label.text = StrDictionary.GetDictionaryString(option2Str, new object[0]);
		this.mOnClickOk = func;
		this.mParam = param;
		this.NpcFakeObjRoot.EnableFakeObjRoot();
		this.NpcPic.mainTexture = this.NpcFakeObjRoot.ModelPic;
		this.NpcFakeObj.InitFakeNpcObj(npcData.Model, this.NpcFakeObjRoot.MeshRoot, null);
		if (showCash)
		{
			NGUITools.SetActive(this.CashRoot, true);
			this.CashLabel.text = string.Format("{0:N0}", GameMoneyHelper.GetCash());
		}
		else
		{
			NGUITools.SetActive(this.CashRoot, false);
		}
	}

	// Token: 0x06004C62 RID: 19554 RVA: 0x0019E7C0 File Offset: 0x0019C9C0
	public void ResetOptionDialog(string weaponId, string headId, string bodyId, string legId, string textStr, string option1Str, string option2Str, string param, DelegateDefine.OneStringParamDelegate func, bool showCash = false)
	{
		this.Reset();
		this.TextLabel.text = StrDictionary.GetDictionaryString(textStr, new object[0]);
		this.Option1Label.text = StrDictionary.GetDictionaryString(option1Str, new object[0]);
		this.Option2Label.text = StrDictionary.GetDictionaryString(option2Str, new object[0]);
		this.mOnClickOk = func;
		this.mParam = param;
		this.NpcFakeObjRoot.EnableFakeObjRoot();
		this.NpcPic.mainTexture = this.NpcFakeObjRoot.ModelPic;
		this.NpcFakeObj.InitFakeObject(weaponId, headId, bodyId, legId, this.NpcFakeObjRoot.MeshRoot, null, "FakeObj");
		if (showCash)
		{
			NGUITools.SetActive(this.CashRoot, true);
			this.CashLabel.text = string.Format("{0:N0}", GameMoneyHelper.GetCash());
		}
		else
		{
			NGUITools.SetActive(this.CashRoot, false);
		}
	}

	// Token: 0x06004C63 RID: 19555 RVA: 0x0019E8B0 File Offset: 0x0019CAB0
	private void OnEnable()
	{
		Singleton<ObjManager>.Instance.MainPlayer.IsTalking = true;
	}

	// Token: 0x06004C64 RID: 19556 RVA: 0x0019E8C4 File Offset: 0x0019CAC4
	private void OnDisable()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = false;
		}
		this.NpcFakeObj.DisableNpcAnimaHandle();
	}

	// Token: 0x06004C65 RID: 19557 RVA: 0x0019E904 File Offset: 0x0019CB04
	protected new virtual void OnDestroy()
	{
		this.NpcFakeObj.DestroyNpcFakeObj();
		base.OnDestroy();
	}

	// Token: 0x04003A0C RID: 14860
	public UILabel TitleLabel;

	// Token: 0x04003A0D RID: 14861
	public UILabel TextLabel;

	// Token: 0x04003A0E RID: 14862
	public UILabel Option1Label;

	// Token: 0x04003A0F RID: 14863
	public UILabel Option2Label;

	// Token: 0x04003A10 RID: 14864
	public NpcOptionDialogData optionData;

	// Token: 0x04003A11 RID: 14865
	public TeamFakeObjPicRootLogic NpcFakeObjRoot;

	// Token: 0x04003A12 RID: 14866
	public FakeObjLogic NpcFakeObj = new FakeObjLogic();

	// Token: 0x04003A13 RID: 14867
	public UITexture NpcPic;

	// Token: 0x04003A14 RID: 14868
	public GameObject CashRoot;

	// Token: 0x04003A15 RID: 14869
	public UILabel CashLabel;

	// Token: 0x04003A16 RID: 14870
	public UISprite CashPic;

	// Token: 0x04003A17 RID: 14871
	public GameObject NoBtnRoot;

	// Token: 0x04003A18 RID: 14872
	private DelegateDefine.OneStringParamDelegate mOnClickOk;

	// Token: 0x04003A19 RID: 14873
	private string mParam;

	// Token: 0x04003A1A RID: 14874
	private bool mCloseOptionFlag;

	// Token: 0x04003A1B RID: 14875
	private NpcData mCurNpcData;
}
