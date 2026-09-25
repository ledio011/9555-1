using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009B2 RID: 2482
public class StrongerLineLogic : MonoBehaviour
{
	// Token: 0x0600467C RID: 18044 RVA: 0x00164FA0 File Offset: 0x001631A0
	public void Reset(StrongerData curstrongerdata)
	{
		this.curData = curstrongerdata;
		this.btnSp.spriteName = GameDefine.BtnIconNew[0];
		this.IconFlag.spriteName = curstrongerdata.Icon;
		this.IconFlag.SetDimensions(this.curData.IconWidth, this.curData.IconHeight);
		this.TipsFlag.enabled = false;
		this.InfoLabel.text = StrDictionary.GetDictionaryString(curstrongerdata.Desc, new object[0]);
		if (this.curData.Star == 0)
		{
			UnityVersionUtil.SetActiveRecursive(this.starObj, false);
			UnityVersionUtil.SetActiveRecursive(this.sliderObj, true);
			this.StateLabel.enabled = true;
			GameDefine.STRONGER_ACTIVITY type = (GameDefine.STRONGER_ACTIVITY)this.curData.Type;
			GameDefine.STRONGER_ACTIVITY stronger_ACTIVITY = type;
			switch (stronger_ACTIVITY)
			{
			case GameDefine.STRONGER_ACTIVITY.SKILL:
				this.ProgressValue.value = this.GetSkillProgress();
				break;
			case GameDefine.STRONGER_ACTIVITY.ENHANCE_CUS:
				this.ProgressValue.value = this.GetEnhanceCusProgress();
				break;
			case GameDefine.STRONGER_ACTIVITY.HONOR:
				this.ProgressValue.value = this.GetHonorProgress();
				break;
			case GameDefine.STRONGER_ACTIVITY.ENHANCE_STR:
				this.ProgressValue.value = this.GetEnhanceStarProgress();
				break;
			case GameDefine.STRONGER_ACTIVITY.ENHANCE_FUSE:
				this.ProgressValue.value = this.GetBadgeProgress();
				break;
			default:
				switch (stronger_ACTIVITY)
				{
				case GameDefine.STRONGER_ACTIVITY.BIGSALE:
				{
					PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
					if (!playerCommonData.Big_PackFlag)
					{
						this.ProgressValue.value = 1f;
					}
					else
					{
						this.ProgressValue.value = 0f;
					}
					break;
				}
				case GameDefine.STRONGER_ACTIVITY.FIRSTBUY:
				{
					PlayerCommonData playerCommonData2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
					if (!playerCommonData2.First_PackFlag)
					{
						this.ProgressValue.value = 1f;
					}
					else
					{
						this.ProgressValue.value = 0f;
					}
					break;
				}
				case GameDefine.STRONGER_ACTIVITY.GUILDSKILL:
					this.ProgressValue.value = this.GetGuildSkillProgress();
					break;
				case GameDefine.STRONGER_ACTIVITY.EQUIPMORE:
					this.ProgressValue.value = this.GetEquipLevelProgress();
					break;
				case GameDefine.STRONGER_ACTIVITY.EQUIPBEST:
					this.ProgressValue.value = this.GetEquipLevelProgress();
					break;
				}
				break;
			}
			if (this.ProgressValue.value < 0.5f)
			{
				this.StateLabel.text = StrDictionary.GetDictionaryString("#{800105}", new object[0]);
			}
			else if (this.ProgressValue.value < 0.75f)
			{
				this.StateLabel.text = StrDictionary.GetDictionaryString("#{800106}", new object[0]);
			}
			else if (this.ProgressValue.value < 0.9f)
			{
				this.StateLabel.text = StrDictionary.GetDictionaryString("#{800107}", new object[0]);
			}
			else
			{
				this.StateLabel.text = StrDictionary.GetDictionaryString("#{800108}", new object[0]);
			}
			if (this.ProgressValue.value < 1E-45f)
			{
				this.sliderFore.enabled = false;
			}
			else
			{
				this.sliderFore.enabled = true;
			}
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.starObj, true);
			UnityVersionUtil.SetActiveRecursive(this.sliderObj, false);
			this.StateLabel.enabled = false;
			for (int i = 0; i < this.starList.Length; i++)
			{
				if (i < this.curData.Star)
				{
					this.starList[i].enabled = true;
				}
				else
				{
					this.starList[i].enabled = false;
				}
			}
			GameDefine.STRONGER_ACTIVITY type2 = (GameDefine.STRONGER_ACTIVITY)this.curData.Type;
			if (type2 == GameDefine.STRONGER_ACTIVITY.MAIN_LINE)
			{
				if (SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetCurMissionByClassType(MISSION_CLASS_TYPE.MAIN) == null)
				{
					this.btnSp.spriteName = GameDefine.BtnIconNew[1];
				}
			}
			else if (type2 == GameDefine.STRONGER_ACTIVITY.DAILY_LINE && !SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.IsHaveDailyMission())
			{
				this.btnSp.spriteName = GameDefine.BtnIconNew[1];
			}
		}
		this.SetTipsInfo();
	}

	// Token: 0x0600467D RID: 18045 RVA: 0x001653AC File Offset: 0x001635AC
	public void SetTipsInfo()
	{
		this.TipsFlag.enabled = false;
		if (this.curData != null && this.curData.StrongerType == 2)
		{
			int type = this.curData.Type;
			switch (type)
			{
			case 6:
				if (SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.IsHaveDailyMission())
				{
					this.TipsFlag.enabled = true;
				}
				break;
			case 7:
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.IsDailyCopyCanPlay(MAPTYPE.EQUIP_COPY))
				{
					this.TipsFlag.enabled = true;
				}
				break;
			case 8:
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.IsDailyCopyCanPlay(MAPTYPE.EXP_DAILY_COPY))
				{
					this.TipsFlag.enabled = true;
				}
				break;
			case 9:
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.ESCORT) || SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT))
				{
					this.TipsFlag.enabled = true;
				}
				break;
			case 10:
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.CITY_DANCE))
				{
					this.TipsFlag.enabled = true;
				}
				break;
			case 11:
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.BAR_FIGHT))
				{
					this.TipsFlag.enabled = true;
				}
				break;
			case 12:
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.IsDailyCopyCanPlay(MAPTYPE.SCUFFLE_AREA_1))
				{
					this.TipsFlag.enabled = true;
				}
				break;
			default:
				if (type == 34)
				{
					if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.IsDailyCopyCanPlay(MAPTYPE.BIG_WORLD))
					{
						this.TipsFlag.enabled = true;
					}
				}
				break;
			}
		}
	}

	// Token: 0x0600467E RID: 18046 RVA: 0x00165590 File Offset: 0x00163790
	public float GetGuildSkillProgress()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetGuildSkillProgress();
	}

	// Token: 0x0600467F RID: 18047 RVA: 0x001655A4 File Offset: 0x001637A4
	public float GetEquipLevelProgress()
	{
		float num = 0f;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ItemContainer equipPack = playerData.EquipPack;
		for (int i = 0; i < equipPack.ContainerSize; i++)
		{
			if (!equipPack.ItemList[i].IsEmpty() && playerData.Level - equipPack.ItemList[i].ItemData.Level < 10)
			{
				num += 1f;
			}
		}
		return num / 6f;
	}

	// Token: 0x06004680 RID: 18048 RVA: 0x0016562C File Offset: 0x0016382C
	public float GetBadgeProgress()
	{
		float num = 0f;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		RefineData[] array = new RefineData[5];
		List<GameItem> itemList = playerData.BadgeEquipPack.ItemList;
		for (int i = 0; i < itemList.Count; i++)
		{
			GameItem gameItem = itemList[i];
			if (!gameItem.IsEmpty())
			{
				ItemData itemData = gameItem.ItemData;
				BadgeData badgeDataById = DataManager.GetBadgeDataById(gameItem.ItemId);
				num += (float)badgeDataById.Lv;
			}
		}
		return num / 40f;
	}

	// Token: 0x06004681 RID: 18049 RVA: 0x001656C0 File Offset: 0x001638C0
	public float GetEnhanceStarProgress()
	{
		float num = 0f;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		RefineData[] array = new RefineData[5];
		for (int i = 1; i < 5; i++)
		{
			array[i] = DataManager.GetRefineDataByPartLevelPRO(i, playerData.MainPlayerAttrData.GetTargetRefinePartLevel((REFINE_PART)i), (int)playerData.Profession);
			num += (float)array[i].Lv;
		}
		return num / 40f;
	}

	// Token: 0x06004682 RID: 18050 RVA: 0x00165728 File Offset: 0x00163928
	public float GetHonorProgress()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		int curTitleLevel = playerData.MainPlayerAttrData.CurTitleLevel;
		return (float)curTitleLevel / 10f;
	}

	// Token: 0x06004683 RID: 18051 RVA: 0x0016575C File Offset: 0x0016395C
	public float GetEnhanceCusProgress()
	{
		float num = 0f;
		ItemContainer equipPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.EquipPack;
		for (int i = 0; i < equipPack.ContainerSize; i++)
		{
			if (!equipPack.ItemList[i].IsEmpty())
			{
				num += (float)equipPack.ItemList[i].ItemLevel;
			}
		}
		return num / (float)(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level * 6);
	}

	// Token: 0x06004684 RID: 18052 RVA: 0x001657D8 File Offset: 0x001639D8
	public float GetSkillProgress()
	{
		float num = 0f;
		int num2 = 0;
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		for (int i = 0; i < mainPlayer.CharacterSkillData.Count; i++)
		{
			CharacterSkillData characterSkillData = mainPlayer.CharacterSkillData[i];
			if (characterSkillData != null && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(characterSkillData.UnlockLevel))
			{
				int index = characterSkillData.Index;
				if (index >= 4 && index <= 6)
				{
					SkillData skillDataById = DataManager.GetSkillDataById(characterSkillData.ID);
					if (skillDataById != null && skillDataById.IsUpgrade == 1)
					{
						num2 += SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
						num += (float)(characterSkillData.Level + 1);
					}
				}
			}
		}
		return num / (float)num2;
	}

	// Token: 0x06004685 RID: 18053 RVA: 0x001658AC File Offset: 0x00163AAC
	public void OnClickGoToBtn()
	{
		switch (this.curData.Type)
		{
		case 0:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.SKILL))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
			{
				SingletonUnity<EquipStrengthenUIRootLogic>.Instance.OnClickSkillBtn();
			}, null);
			break;
		case 1:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ENHANCE_EQUIP))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
			{
				SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipEnhance(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
			}, null);
			break;
		case 2:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.TITLE))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
			{
				SingletonUnity<EquipStrengthenUIRootLogic>.Instance.OnClickPlayerTitleBtn();
			}, null);
			break;
		case 3:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ENHANCE_STAR))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
			{
				SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipRefine();
			}, null);
			break;
		case 4:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ENHANCE_BADGE))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
			{
				SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowBadgeMerge(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
			}, null);
			break;
		case 5:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.MAIN_MISSION))
			{
				return;
			}
			SingletonUnity<MissionTeamTipLogic>.Instance.ResetMissionTip();
			if (!SingletonUnity<MissionTeamTipLogic>.Instance.MissionTipRoot.IsHaveMainLineMission())
			{
				NoticeLogic.AddNotifyData("#{800501}", true, false);
			}
			else
			{
				TutorialManager.ShowTutorial(TUTORIAL_STEP.MAIN_MISSION_CLICK_START);
			}
			break;
		case 6:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.DAILY_MISSION))
			{
				return;
			}
			SingletonUnity<MissionTeamTipLogic>.Instance.ResetMissionTip();
			if (!SingletonUnity<MissionTeamTipLogic>.Instance.MissionTipRoot.IsHaveDailyMission())
			{
				NoticeLogic.AddNotifyData("#{800502}", true, false);
			}
			else
			{
				TutorialManager.ShowTutorial(TUTORIAL_STEP.DAILY_MISSION_CLICK_START);
			}
			break;
		case 7:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.EQUIP_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
			}, null);
			break;
		case 8:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.EXP_DAILY_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
			}, null);
			break;
		case 9:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.ESCORT))
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.ESCORT, null, false);
				}
				else if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.IsTimeActivityCanPlay(GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT))
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT, null, false);
				}
				else
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.ESCORT, null, false);
				}
			}, null);
			break;
		case 10:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.CITY_DANCE, null, false);
			}, null);
			break;
		case 11:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.BAR_FIGHT, null, false);
			}, null);
			break;
		case 12:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.SCUFFLE_AREA_1, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
			}, null);
			break;
		case 13:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.CASH_DAILY_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
			}, null);
			break;
		case 14:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.CAR_CHASE_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
			}, null);
			break;
		case 15:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_CHALLENGE))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.INVALID, null, null, GameDefine.ACTIVITY_TYPE.TOWER);
			}, null);
			break;
		case 16:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_WORLDBOSS))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.WILD_BOSS, null, false);
			}, null);
			break;
		case 17:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.RANK_PVP))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickRankBtn();
			}, null);
			break;
		case 18:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.SHOP_BUY))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
			{
				SingletonUnity<ShopUIRootLogic>.Instance.OnClickBuyDiamondBtn();
			}, null);
			break;
		case 19:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE, null, false);
			}, null);
			break;
		case 20:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.SHOP_GUILD))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
			{
				SingletonUnity<ShopUIRootLogic>.Instance.Reset();
				SingletonUnity<ShopUIRootLogic>.Instance.OnClickGuildBtn((GameDefine.SHOP_TAB_TYPE)this.curData.SubType, GameDefine.UIBACKTYPE.NOTHINTG, this.curData.shopitemid);
			}, null);
			break;
		case 21:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.SHOP_TOOL))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
			{
				SingletonUnity<ShopUIRootLogic>.Instance.OnClickToolsBtn((GameDefine.SHOP_TAB_TYPE)this.curData.SubType, GameDefine.UIBACKTYPE.NOTHINTG, this.curData.shopitemid);
			}, null);
			break;
		case 22:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.SHOP_EQUIP))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
			{
				SingletonUnity<ShopUIRootLogic>.Instance.OnClickEquipBtn((GameDefine.SHOP_TAB_TYPE)this.curData.SubType, GameDefine.UIBACKTYPE.NOTHINTG, this.curData.shopitemid);
			}, null);
			break;
		case 23:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.LOTTO))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SlotUIRoot, delegate
			{
				SingletonUnity<SlotUIRootLogic>.Instance.EnableReset();
				WaitResponseUIRootLogic.OpenWaitBox(242, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.request_slot_info>(null, null);
			}, null);
			break;
		case 24:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.GUILD))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewGuildUIRootLogic, delegate
			{
				SingletonUnity<NewGuildUIRootLogic>.Instance.targetTabType = 0;
			}, null);
			break;
		case 25:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.GUILD_ACTIVITY))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.Reset();
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.GUILD_BOSS, null, false);
			}, null);
			break;
		case 26:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.SHOP_BIGSALE))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
			{
				SingletonUnity<ShopUIRootLogic>.Instance.OnClickBigSaleBtn((GameDefine.SHOP_TAB_TYPE)this.curData.SubType, GameDefine.UIBACKTYPE.NOTHINTG, this.curData.shopitemid);
			}, null);
			break;
		case 27:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.SEX_MINI, null, false);
			}, null);
			break;
		case 29:
		{
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.BIGSALES))
			{
				return;
			}
			PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			if (!playerCommonData.Big_PackFlag)
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BigPackRoot, delegate
			{
				SingletonUnity<BigPackRootLogic>.Instance.EnableReset();
				WaitResponseUIRootLogic.OpenWaitBox(260, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.request_big_pack>(null, null);
			}, null);
			break;
		}
		case 30:
		{
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.BIGSALES))
			{
				return;
			}
			PlayerCommonData playerCommonData2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			if (!playerCommonData2.First_PackFlag)
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.FirstBuyRoot, delegate
			{
				SingletonUnity<FirstBuyRootLogic>.Instance.EnableReset();
				WaitResponseUIRootLogic.OpenWaitBox(259, 10f, 0f, null);
				NetLogic.GetInstance().Send<Protocol.request_first_buy>(null, null);
			}, null);
			break;
		}
		case 31:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.GUILD_SKILL))
			{
				return;
			}
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewGuildUIRootLogic, delegate
				{
					SingletonUnity<NewGuildUIRootLogic>.Instance.targetTabType = 2;
				}, null);
			}
			else
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewGuildUIRootLogic, null, null);
			}
			break;
		case 32:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.EQUIP_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
			}, null);
			break;
		case 33:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.BAR_FIGHT, null, false);
			}, null);
			break;
		case 34:
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn();
			}, null);
			break;
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("BeStronger", "BeStronger", string.Format("clicktype_{0}", this.curData.Type));
	}

	// Token: 0x06004686 RID: 18054 RVA: 0x00166294 File Offset: 0x00164494
	public bool CheckUnlockFunction(FUNCTION_TYPE curFunction)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (playerCommonData.IsFunctionUnlock(curFunction))
		{
			this.OnClickCloseBtn();
			return true;
		}
		NoticeLogic.AddNotifyData("#{101539}", true, false);
		return false;
	}

	// Token: 0x06004687 RID: 18055 RVA: 0x001662D0 File Offset: 0x001644D0
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.StrongerRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
	}

	// Token: 0x04003386 RID: 13190
	public UISprite btnSp;

	// Token: 0x04003387 RID: 13191
	public UISprite IconFlag;

	// Token: 0x04003388 RID: 13192
	public UILabel InfoLabel;

	// Token: 0x04003389 RID: 13193
	public GameObject sliderObj;

	// Token: 0x0400338A RID: 13194
	public UISlider ProgressValue;

	// Token: 0x0400338B RID: 13195
	public UILabel StateLabel;

	// Token: 0x0400338C RID: 13196
	public UISprite sliderFore;

	// Token: 0x0400338D RID: 13197
	public GameObject starObj;

	// Token: 0x0400338E RID: 13198
	public UISprite[] starList;

	// Token: 0x0400338F RID: 13199
	private StrongerData curData;

	// Token: 0x04003390 RID: 13200
	public UISprite TipsFlag;
}
