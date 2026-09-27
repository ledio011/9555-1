using System;
using SprotoType;
using UnityEngine;

// Token: 0x020008D8 RID: 2264
public class DailyActiveLineLogic : MonoBehaviour
{
	// Token: 0x06003D1A RID: 15642 RVA: 0x0010F59C File Offset: 0x0010D79C
	public void UpdateInfo(daily_active curinfo)
	{
		this.curActive = curinfo;
		this.curData = DataManager.GetDailyActiveDataById(curinfo.ID);
		this.TitleLabel.text = StrDictionary.GetDictionaryString(this.curData.Title, new object[0]);
		this.InfoLabel.text = StrDictionary.GetDictionaryString(this.curData.Desc, new object[]
		{
			this.curData.Count
		});
		if (this.curActive.count < (long)this.curData.Count)
		{
			this.TimesLabel.text = string.Format("{0}/{1}", this.curActive.count, this.curData.Count);
		}
		else
		{
			this.TimesLabel.text = string.Format("{0}/{1}", this.curData.Count, this.curData.Count);
		}
		this.ScoreLabel.text = string.Format("��{0}", this.curData.Score * this.curData.Count);
		if (this.curActive.count >= (long)this.curData.Count)
		{
			this.btnsp.spriteName = GameDefine.BtnIconNew[1];
			this.btnLabel.text = StrDictionary.GetDictionaryString("#{300802}", new object[0]);
		}
		else
		{
			this.btnsp.spriteName = GameDefine.BtnIconNew[0];
			this.btnLabel.text = StrDictionary.GetDictionaryString("#{300801}", new object[0]);
		}
	}

	// Token: 0x06003D1B RID: 15643 RVA: 0x0010F750 File Offset: 0x0010D950
	public void OnClickGoBtn()
	{
		if (this.curActive.count >= (long)this.curData.Count)
		{
			return;
		}
		switch (this.curData.Type)
		{
		case 0:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				return;
			}
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.EQUIP_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.EQUIP_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
				}, null);
			}
			break;
		case 1:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				return;
			}
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.EXP_DAILY_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.EXP_DAILY_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
				}, null);
			}
			break;
		case 2:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				return;
			}
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.CASH_DAILY_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.CASH_DAILY_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
				}, null);
			}
			break;
		case 3:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				return;
			}
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.CAR_CHASE_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.CAR_CHASE_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
				}, null);
			}
			break;
		case 4:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
			{
				return;
			}
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT, null, false);
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT, null, false);
				}, null);
			}
			break;
		case 5:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_CHALLENGE))
			{
				return;
			}
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.INVALID, null, null, GameDefine.ACTIVITY_TYPE.TOWER);
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.INVALID, null, null, GameDefine.ACTIVITY_TYPE.TOWER);
				}, null);
			}
			break;
		case 6:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_WORLDBOSS))
			{
				return;
			}
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.WILD_BOSS, null, false);
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.WILD_BOSS, null, false);
				}, null);
			}
			break;
		case 7:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.RANK_PVP))
			{
				return;
			}
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickRankBtn();
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickRankBtn();
				}, null);
			}
			break;
		case 8:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ENHANCE_EQUIP))
			{
				return;
			}
			SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
			{
				SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipEnhance(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
			}, null);
			break;
		case 9:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ENHANCE_STAR))
			{
				return;
			}
			SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
			{
				SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipRefine();
			}, null);
			break;
		case 10:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.SKILL))
			{
				return;
			}
			SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
			{
				SingletonUnity<EquipStrengthenUIRootLogic>.Instance.OnClickSkillBtn();
			}, null);
			break;
		case 11:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.SHOP_EQUIP))
			{
				return;
			}
			SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
			{
				SingletonUnity<ShopUIRootLogic>.Instance.OnClickEquipBtn();
			}, null);
			break;
		case 12:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.SHOP_TOOL))
			{
				return;
			}
			SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
			{
				SingletonUnity<ShopUIRootLogic>.Instance.OnClickToolsBtn();
			}, null);
			break;
		case 13:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.GIFT_DAILY))
			{
				return;
			}
			SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CommercialUIRoot, delegate
			{
				SingletonUnity<CommercialUIRootLogic>.Instance.OnClickDailyBuyBtn();
			}, null);
			break;
		case 14:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
			{
				return;
			}
			SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.ESCORT, null, false);
			}, null);
			break;
		case 15:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.DOMIN))
			{
				return;
			}
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDominBtn();
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDominBtn();
				}, null);
			}
			break;
		case 16:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				return;
			}
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.SCUFFLE_AREA_1, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.SCUFFLE_AREA_1, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
				}, null);
			}
			break;
		case 17:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				return;
			}
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.SINGLE_DANCE, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.SINGLE_DANCE, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
				}, null);
			}
			break;
		case 18:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
			{
				return;
			}
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE, null, false);
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE, null, false);
				}, null);
			}
			break;
		case 19:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
			{
				return;
			}
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.GUILD_BOSS, null, false);
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.GUILD_BOSS, null, false);
				}, null);
			}
			break;
		case 20:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
			{
				return;
			}
			if (SingletonUnity<NewActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewActivityUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.GUILD_DANCE, null, false);
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.GUILD_DANCE, null, false);
				}, null);
			}
			break;
		}
	}

	// Token: 0x06003D1C RID: 15644 RVA: 0x001100E0 File Offset: 0x0010E2E0
	public bool CheckUnlockFunction(FUNCTION_TYPE curFunction)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (playerCommonData.IsFunctionUnlock(curFunction))
		{
			return true;
		}
		NoticeLogic.AddNotifyData("#{101539}", true, false);
		return false;
	}

	// Token: 0x06003D1D RID: 15645 RVA: 0x00110114 File Offset: 0x0010E314
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActiveRewardRoot);
	}

	// Token: 0x0400287B RID: 10363
	public UILabel TimesLabel;

	// Token: 0x0400287C RID: 10364
	public UILabel TitleLabel;

	// Token: 0x0400287D RID: 10365
	public UILabel InfoLabel;

	// Token: 0x0400287E RID: 10366
	public UILabel ScoreLabel;

	// Token: 0x0400287F RID: 10367
	public UILabel btnLabel;

	// Token: 0x04002880 RID: 10368
	public UISprite btnsp;

	// Token: 0x04002881 RID: 10369
	private daily_active curActive;

	// Token: 0x04002882 RID: 10370
	private DailyActiveData curData;
}
