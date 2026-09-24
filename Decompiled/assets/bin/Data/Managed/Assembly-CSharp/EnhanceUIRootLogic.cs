using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000908 RID: 2312
public class EnhanceUIRootLogic : SingletonUnity<EnhanceUIRootLogic>
{
	// Token: 0x06003F56 RID: 16214 RVA: 0x00126860 File Offset: 0x00124A60
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mClickUpgradeBtnCount = 0;
		this.mOnClickTutorialBtn = tutorialEvent;
		if (TutorialManager.CurStep == TUTORIAL_STEP.ENHANCE_CLICK)
		{
			this.clickHandle = new vp_Timer.Handle();
		}
	}

	// Token: 0x06003F57 RID: 16215 RVA: 0x00126888 File Offset: 0x00124A88
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x06003F58 RID: 16216 RVA: 0x001268B8 File Offset: 0x00124AB8
	private void UpgradeBtnTutorialCheck()
	{
		if (TutorialManager.CurStep != TUTORIAL_STEP.ENHANCE_CLICK)
		{
			return;
		}
		this.mClickUpgradeBtnCount++;
		NGUITools.SetActive(SingletonUnity<TutorialUIRootLogic>.Instance.HandTipTweenS.gameObject, false);
		NGUITools.SetActive(SingletonUnity<TutorialUIRootLogic>.Instance.TipCircleSprite.gameObject, false);
		if (this.clickHandle != null)
		{
			this.clickHandle.Cancel();
		}
		vp_Timer.In(0.5f, delegate()
		{
			NGUITools.SetActive(SingletonUnity<TutorialUIRootLogic>.Instance.HandTipTweenS.gameObject, true);
			NGUITools.SetActive(SingletonUnity<TutorialUIRootLogic>.Instance.TipCircleSprite.gameObject, true);
		}, this.clickHandle);
		if (this.mClickUpgradeBtnCount >= 3)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06003F59 RID: 16217 RVA: 0x00126960 File Offset: 0x00124B60
	private void CloseTutorial()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.CloseTutorial();
			if (TutorialManager.CurStep == TUTORIAL_STEP.ENHANCE_CLICK)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.ENHANCE);
			}
		}
	}

	// Token: 0x17000F8E RID: 3982
	// (get) Token: 0x06003F5A RID: 16218 RVA: 0x001269A0 File Offset: 0x00124BA0
	// (set) Token: 0x06003F5B RID: 16219 RVA: 0x001269A8 File Offset: 0x00124BA8
	public GameItem CurItem
	{
		get
		{
			return this.mCurItem;
		}
		set
		{
			this.mCurItem = value;
		}
	}

	// Token: 0x17000F8F RID: 3983
	// (get) Token: 0x06003F5C RID: 16220 RVA: 0x001269B4 File Offset: 0x00124BB4
	// (set) Token: 0x06003F5D RID: 16221 RVA: 0x001269BC File Offset: 0x00124BBC
	public EquipData CurEquipData
	{
		get
		{
			return this.mCurEquipData;
		}
		set
		{
			this.mCurEquipData = value;
		}
	}

	// Token: 0x17000F90 RID: 3984
	// (get) Token: 0x06003F5E RID: 16222 RVA: 0x001269C8 File Offset: 0x00124BC8
	// (set) Token: 0x06003F5F RID: 16223 RVA: 0x001269D0 File Offset: 0x00124BD0
	public GameItem CurCaiLiao
	{
		get
		{
			return this.mCurCaiLiao;
		}
		set
		{
			this.mCurCaiLiao = value;
		}
	}

	// Token: 0x06003F60 RID: 16224 RVA: 0x001269DC File Offset: 0x00124BDC
	protected override void Awake()
	{
		base.Awake();
		this.Init();
	}

	// Token: 0x06003F61 RID: 16225 RVA: 0x001269EC File Offset: 0x00124BEC
	public void Init()
	{
		this.QiangHuaEquipList.Init();
		QiangHuaListLogic qiangHuaEquipList = this.QiangHuaEquipList;
		qiangHuaEquipList.onClickEquipItem = (EquipItemLogic.OnClickItem)Delegate.Combine(qiangHuaEquipList.onClickEquipItem, new EquipItemLogic.OnClickItem(this.OnClickEquipItem));
	}

	// Token: 0x06003F62 RID: 16226 RVA: 0x00126A2C File Offset: 0x00124C2C
	private void OnEnable()
	{
		UIUpdateEvent.UpdateMoneyEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.UpdateMoneyEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateMoney));
	}

	// Token: 0x06003F63 RID: 16227 RVA: 0x00126A5C File Offset: 0x00124C5C
	private void OnDisable()
	{
		UIUpdateEvent.UpdateMoneyEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.UpdateMoneyEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateMoney));
	}

	// Token: 0x06003F64 RID: 16228 RVA: 0x00126A8C File Offset: 0x00124C8C
	public void UpdateMoney()
	{
		if (this.CurItem != null)
		{
			this.mCurEquipData = DataManager.GetEquipDataById(this.CurItem.ItemId);
			this.NeedMoney = this.CurEquipData.GetUpgradeMoneyByQualityAndLevel((int)this.CurItem.GetItemQuality(), this.GetCurItemLevel());
			this.NeedMoneyLab.text = this.NeedMoney.ToString();
			if ((long)this.NeedMoney <= GameMoneyHelper.GetMoneyNum(0))
			{
				this.NeedMoneyLab.color = Color.white;
			}
			else
			{
				this.NeedMoneyLab.color = Color.red;
			}
		}
	}

	// Token: 0x06003F65 RID: 16229 RVA: 0x00126B2C File Offset: 0x00124D2C
	private int GetCurItemLevel()
	{
		if (this.CurItem == null || this.CurItem.IsEmpty())
		{
			return 0;
		}
		if (GameManager.IsSupportCurDataVersion137())
		{
			if (this.mplayerdata == null)
			{
				this.mplayerdata = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			}
			return this.mplayerdata.MainPlayerAttrData.EquipEnhanceList[this.CurItem.ItemData.SubType];
		}
		return this.CurItem.ItemLevel;
	}

	// Token: 0x06003F66 RID: 16230 RVA: 0x00126BA8 File Offset: 0x00124DA8
	private void OnClickEquipItem(GameItem item)
	{
		if (item == null || item.IndexId == -1L)
		{
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100659}", new object[0]), true, false);
			return;
		}
		if (item != null && this.mCurItem != null && this.mCurItem.IndexId == item.IndexId)
		{
			int itemLevel = 0;
			if (item.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				itemLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(item.ItemData.SubType);
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
			{
				item.ItemLevel = itemLevel;
				SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(item, ITEM_SHOW_TYPE.REWARD_TIPS, false, UI_PAGE_TYPE.INVALID, false, false);
			}, null);
		}
		this.QiangHuaEquipList.UpdateEquipInfo(item);
		this.UpdateEquipUpdateInfoPage(item);
	}

	// Token: 0x06003F67 RID: 16231 RVA: 0x00126CB8 File Offset: 0x00124EB8
	public void Show(GameItem item)
	{
		this.Reset(item, true);
	}

	// Token: 0x06003F68 RID: 16232 RVA: 0x00126CC4 File Offset: 0x00124EC4
	public void OnClickSelctItem()
	{
		if (this.mCurItem == null)
		{
			return;
		}
		int itemLevel = 0;
		if (this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			itemLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(this.mCurItem.ItemData.SubType);
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
		{
			this.mCurItem.ItemLevel = itemLevel;
			SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(this.mCurItem, ITEM_SHOW_TYPE.REWARD_TIPS, false, UI_PAGE_TYPE.INVALID, false, false);
		}, null);
	}

	// Token: 0x06003F69 RID: 16233 RVA: 0x00126D50 File Offset: 0x00124F50
	public void OnClickCaoLiao()
	{
		if (this.mCurCaiLiao == null)
		{
			return;
		}
		ItemInfoRootLogicNew.ShowItemTips(this.mCurCaiLiao, 0, true, UI_PAGE_TYPE.ENHANCE_EQUIP);
	}

	// Token: 0x06003F6A RID: 16234 RVA: 0x00126D6C File Offset: 0x00124F6C
	public void OnClikcHuoBi()
	{
		ItemData moneyItemData = GameMoneyHelper.GetMoneyItemData(GameDefine.MONEY_TYPE.CASH);
		if (moneyItemData != null)
		{
			ItemInfoRootLogicNew.ShowItemTips(moneyItemData, true, UI_PAGE_TYPE.ENHANCE_EQUIP);
		}
	}

	// Token: 0x06003F6B RID: 16235 RVA: 0x00126D90 File Offset: 0x00124F90
	public void RefershUI()
	{
		this.Reset(this.CurItem, false);
	}

	// Token: 0x06003F6C RID: 16236 RVA: 0x00126DA0 File Offset: 0x00124FA0
	public void Reset(GameItem item, bool resetpos = true)
	{
		this.mplayerdata = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		List<GameItem> list = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.EquipPack.ItemList;
		list = ItemContainerTool.SortItemList(list);
		List<GameItem> list2 = new List<GameItem>();
		for (int i = 0; i < 6; i++)
		{
			list2.Add(new GameItem());
			list2[i].IndexId = -1L;
		}
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j] != null && !list[j].IsEmpty())
			{
				list2[list[j].ItemData.SubType] = new GameItem(list[j].IndexId, list[j].ContainerType, list[j].ItemId, list[j].BindFlag, list[j].StackNum, list[j].Quality);
				list2[list[j].ItemData.SubType].SetAttInfo(list[j].Appraise, list[j].Random_AttriDic, list[j].InlayDic);
			}
		}
		if (GameManager.IsSupportCurDataVersion137())
		{
			for (int k = 0; k < list2.Count; k++)
			{
				if (list2[k].IndexId == -1L)
				{
					list2[k].ItemId = string.Format("{0}", 60001 + k);
					list2[k].Quality = EQUIP_QUALITY.KUANG_PURPLE;
				}
				list2[k].ItemLevel = this.mplayerdata.MainPlayerAttrData.GetEquipEnhanceLevel(list2[k].ItemData.SubType);
			}
		}
		this.mEnhancePartList = list2;
		if (GameManager.IsSupportCurDataVersion137())
		{
			this.QiangHuaEquipList.ResetEuipListInfo(list2, list2[item.ItemData.SubType], resetpos);
			this.UpdateEquipUpdateInfoPage(list2[item.ItemData.SubType]);
		}
		else
		{
			this.QiangHuaEquipList.ResetEuipListInfo(list, item, resetpos);
			this.UpdateEquipUpdateInfoPage(item);
		}
	}

	// Token: 0x06003F6D RID: 16237 RVA: 0x00126FDC File Offset: 0x001251DC
	public void UpdateEquipUpdateInfoPage(GameItem item)
	{
		if (this.mEnhancePartList == null || this.mEnhancePartList.Count == 0)
		{
			return;
		}
		this.mplayerdata = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.CurItem = item;
		this.CurCaiLiao = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ItemBackPack.GetEnhanceItem();
		if (this.CurCaiLiao == null)
		{
			this.CurStack = 0;
		}
		else
		{
			this.CurStack = this.CurCaiLiao.StackNum;
		}
		ItemData itemDataByID = DataManager.GetItemDataByID("3001");
		this.CurCaiLiao = new GameItem(itemDataByID.ID, itemDataByID.QualityType, 1);
		this.CaiLiaoSP.spriteName = itemDataByID.BackPackIcon;
		this.CaiLiaoQualitySprite.spriteName = itemDataByID.QualityType.ToString();
		if (this.CurItem != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.Cost, true);
			NGUITools.SetActive(this.UpLevelBtn.gameObject, true);
			NGUITools.SetActive(this.UpLevelAllBtn.gameObject, true);
			this.mCurEquipData = DataManager.GetEquipDataById(this.CurItem.ItemId);
			ItemData itemDataByID2 = DataManager.GetItemDataByID(this.CurItem.ItemId);
			this.EquipNameLab.text = itemDataByID2.MName;
			this.EquipNameLab.color = GameDefine.GetColorByQuality(this.CurItem.GetItemQuality());
			this.CurItemIcon.spriteName = itemDataByID2.BackPackIcon;
			float num = (float)this.GetCurItemLevel() / (float)this.mplayerdata.Level;
			if (num > 1f)
			{
				num = 1f;
			}
			this.LevelProgress.fillAmount = 0.09f + num * 0.9f;
			if (this.GetCurItemLevel() < this.mplayerdata.Level)
			{
				this.MaxLevelFlag = false;
				if (GameManager.IsSupportCurDataVersion137())
				{
					this.CurLevelLab.text = string.Format("LV.{0}", this.mplayerdata.MainPlayerAttrData.EquipEnhanceList[this.CurItem.ItemData.SubType]);
				}
				else
				{
					this.CurLevelLab.text = string.Format("LV.{0}", this.GetCurItemLevel());
				}
				this.NextLevelLab.text = string.Format("LV.{0}", this.GetCurItemLevel() + 1);
			}
			else
			{
				this.MaxLevelFlag = true;
				if (GameManager.IsSupportCurDataVersion137())
				{
					this.CurLevelLab.text = string.Format("LV.{0}", this.mplayerdata.MainPlayerAttrData.EquipEnhanceList[this.CurItem.ItemData.SubType]);
				}
				else
				{
					this.CurLevelLab.text = string.Format("LV.{0}", this.GetCurItemLevel());
				}
				this.NextLevelLab.text = "Lv.Max";
			}
			if (itemDataByID2.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				UnityVersionUtil.SetActiveRecursive(this.ItemQualityIcon.gameObject, true);
				this.ItemQualityIcon.spriteName = this.CurItem.GetItemQuality().ToString();
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.ItemQualityIcon.gameObject, true);
				this.ItemQualityIcon.spriteName = itemDataByID2.QualityType.ToString();
			}
			for (int i = 0; i < this.EquipDataLab.Count; i++)
			{
				if (this.CurItem.IndexId != -1L && i < this.mCurEquipData.GetBaseAttCount())
				{
					NGUITools.SetActive(this.EquipLabs[i].gameObject, true);
					this.EquipDataNameLab[i].text = GameDefine.GetAttributeName_S(this.mCurEquipData.GetAttrIDByQuality(i));
					this.EquipIconSprite[i].spriteName = GameDefine.GetAttributeIcon(this.mCurEquipData.GetAttrIDByQuality(i));
					this.EquipDataLab[i].text = this.mCurEquipData.GetAttrValByQualityAndLevel(i, (int)this.CurItem.GetItemQuality(), this.GetCurItemLevel()).ToString();
					if (this.MaxLevelFlag)
					{
						if (this.mplayerdata.Level < GameDefine.PLAYER_MAX_LEVEL)
						{
							this.NextLevelEquipDataLab[i].text = this.mCurEquipData.GetAttrValByQualityAndLevel(i, (int)this.CurItem.GetItemQuality(), this.GetCurItemLevel() + 1).ToString();
						}
						else
						{
							this.NextLevelEquipDataLab[i].text = this.mCurEquipData.GetAttrValByQualityAndLevel(i, (int)this.CurItem.GetItemQuality(), this.GetCurItemLevel()).ToString();
						}
					}
					else
					{
						this.NextLevelEquipDataLab[i].text = this.mCurEquipData.GetAttrValByQualityAndLevel(i, (int)this.CurItem.GetItemQuality(), this.GetCurItemLevel() + 1).ToString();
					}
				}
				else
				{
					NGUITools.SetActive(this.EquipLabs[i].gameObject, false);
				}
			}
			this.NeedStack = this.CurEquipData.GetUpgradeExpValByLevel(this.GetCurItemLevel());
			this.NeedStackLab.text = string.Format("{0}/{1}", this.NeedStack, this.CurStack);
			if (this.NeedStack > this.CurStack)
			{
				this.NeedStackLab.color = Color.red;
			}
			else
			{
				this.NeedStackLab.color = Color.white;
			}
			this.NeedMoney = this.CurEquipData.GetUpgradeMoneyByQualityAndLevel((int)this.CurItem.GetItemQuality(), this.GetCurItemLevel());
			this.NeedMoneyLab.text = this.NeedMoney.ToString();
			if ((long)this.NeedMoney <= GameMoneyHelper.GetMoneyNum(0))
			{
				this.NeedMoneyLab.color = Color.white;
			}
			else
			{
				this.NeedMoneyLab.color = Color.red;
			}
			this.ItemGrid.Reposition();
			this.ItemGrid.repositionNow = true;
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.Cost, false);
			NGUITools.SetActive(this.UpLevelBtn.gameObject, false);
			NGUITools.SetActive(this.UpLevelAllBtn.gameObject, false);
			this.EquipNameLab.text = string.Empty;
			this.CurItemIcon.spriteName = string.Empty;
			this.CurLevelLab.text = "--";
			this.NextLevelLab.text = "--";
			UnityVersionUtil.SetActiveRecursive(this.ItemGrid.gameObject, false);
			this.ItemGrid.Reposition();
			this.LevelProgress.fillAmount = 0f;
			UnityVersionUtil.SetActiveRecursive(this.ItemQualityIcon.gameObject, false);
		}
	}

	// Token: 0x06003F6E RID: 16238 RVA: 0x00127694 File Offset: 0x00125894
	public void UpdateQHInfo(GameItem item)
	{
		if (this.CurCaiLiao != null && item.IndexId == this.CurCaiLiao.IndexId)
		{
			this.UpdataCaiLiaoStack(item);
		}
		else if (item.IndexId == this.CurItem.IndexId)
		{
			this.UpdateEquipUpdateInfoPage(item);
			this.QiangHuaEquipList.UpdateEquipInfo(item);
			NoticeLogic.AddNotifyData("#{100917}", true, false);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Enhance", string.Format("part_{0}", item.ItemData.SubType), string.Format("level_{0}", item.ItemLevel));
		}
	}

	// Token: 0x06003F6F RID: 16239 RVA: 0x00127744 File Offset: 0x00125944
	public void UpdataCaiLiaoStack(GameItem item)
	{
		this.CurStack = item.StackNum;
	}

	// Token: 0x06003F70 RID: 16240 RVA: 0x00127754 File Offset: 0x00125954
	public void OnClickAllLevelBtn()
	{
		if (this.CurItem == null || this.CurEquipData == null || this.CurItem.IndexId == -1L)
		{
			NoticeLogic.AddNotifyData("#{100923}", true, false);
			this.CloseTutorial();
			return;
		}
		if (this.GetCurItemLevel() >= this.mplayerdata.Level)
		{
			NoticeLogic.AddNotifyData("#{100918}", true, false);
			this.CloseTutorial();
			return;
		}
		if (!GameMoneyHelper.BeforeCheckBuy(GameDefine.MONEY_TYPE.CASH, this.NeedMoney))
		{
			this.CloseTutorial();
			return;
		}
		if (this.CurStack < this.NeedStack)
		{
			GameMoneyHelper.ShowItemProduct("3001", GameDefine.SHOP_TYPE.TOOL_SHOP);
			this.CloseTutorial();
			return;
		}
		int i = this.GetCurItemLevel();
		int num = 0;
		int num2 = 0;
		long cash = GameMoneyHelper.GetCash();
		while (i < this.mplayerdata.Level)
		{
			num += this.CurEquipData.GetUpgradeExpValByLevel(i);
			num2 += this.CurEquipData.GetUpgradeMoneyByQualityAndLevel((int)this.CurItem.GetItemQuality(), i);
			if (num > this.CurStack || (long)num2 > cash)
			{
				break;
			}
			i++;
		}
		if (i > this.GetCurItemLevel())
		{
			this.PlayEffect();
			equip_enhance.request request = new equip_enhance.request();
			request.indexId = this.CurItem.IndexId;
			request.level = (long)i;
			NetLogic.GetInstance().Send<Protocol.equip_enhance>(request, null);
			if (TutorialManager.CurStep == TUTORIAL_STEP.ENHANCE_ALL_CLICK || TutorialManager.CurStep == TUTORIAL_STEP.ENHANCE_CLICK_ALL)
			{
				this.CheckTutorialEvent();
			}
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Enhance", "click_enhanceall", "times");
		}
	}

	// Token: 0x06003F71 RID: 16241 RVA: 0x001278E8 File Offset: 0x00125AE8
	public void PlayEffect()
	{
		this.EquipEffect.Clear();
		this.EquipEffect.Play(true);
		for (int i = 0; i < this.EquipDataLab.Count; i++)
		{
			if (UnityVersionUtil.IsActive(this.EquipDataLab[i].gameObject))
			{
				this.lineEffect[i].resetOnPlay = true;
				this.lineEffect[i].Play(true);
			}
		}
		SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(19, 1f, null);
	}

	// Token: 0x06003F72 RID: 16242 RVA: 0x00127974 File Offset: 0x00125B74
	public void OnClickUpLevelBtn()
	{
		if (this.CurItem == null || this.CurEquipData == null || this.CurItem.IndexId == -1L)
		{
			NoticeLogic.AddNotifyData("#{100923}", true, false);
			this.CloseTutorial();
			return;
		}
		if (this.GetCurItemLevel() >= this.mplayerdata.Level)
		{
			NoticeLogic.AddNotifyData("#{100918}", true, false);
			this.CloseTutorial();
			return;
		}
		if (!GameMoneyHelper.BeforeCheckBuy(GameDefine.MONEY_TYPE.CASH, this.NeedMoney))
		{
			this.CloseTutorial();
			return;
		}
		if (this.CurStack < this.NeedStack)
		{
			GameMoneyHelper.ShowItemProduct("3001", GameDefine.SHOP_TYPE.TOOL_SHOP);
			this.CloseTutorial();
			return;
		}
		if (this.CurItem != null)
		{
			this.PlayEffect();
			equip_enhance.request request = new equip_enhance.request();
			request.indexId = this.CurItem.IndexId;
			NetLogic.GetInstance().Send<Protocol.equip_enhance>(request, null);
			if (TutorialManager.CurStep == TUTORIAL_STEP.ENHANCE_CLICK)
			{
				this.CheckTutorialEvent();
			}
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Enhance", "click_enhance", "times");
		}
	}

	// Token: 0x04002B2B RID: 11051
	private int mClickUpgradeBtnCount;

	// Token: 0x04002B2C RID: 11052
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04002B2D RID: 11053
	private vp_Timer.Handle clickHandle;

	// Token: 0x04002B2E RID: 11054
	private GameItem mCurItem;

	// Token: 0x04002B2F RID: 11055
	private EquipData mCurEquipData;

	// Token: 0x04002B30 RID: 11056
	private GameItem mCurCaiLiao;

	// Token: 0x04002B31 RID: 11057
	public QiangHuaListLogic QiangHuaEquipList;

	// Token: 0x04002B32 RID: 11058
	public UISprite CaiLiaoSP;

	// Token: 0x04002B33 RID: 11059
	public UISprite CaiLiaoQualitySprite;

	// Token: 0x04002B34 RID: 11060
	public UISprite CurItemIcon;

	// Token: 0x04002B35 RID: 11061
	public UILabel EquipNameLab;

	// Token: 0x04002B36 RID: 11062
	public UILabel CurLevelLab;

	// Token: 0x04002B37 RID: 11063
	public UILabel NextLevelLab;

	// Token: 0x04002B38 RID: 11064
	public UISprite ItemQualityIcon;

	// Token: 0x04002B39 RID: 11065
	public UIGrid ItemGrid;

	// Token: 0x04002B3A RID: 11066
	public List<UILabel> EquipDataLab;

	// Token: 0x04002B3B RID: 11067
	public List<UILabel> NextLevelEquipDataLab;

	// Token: 0x04002B3C RID: 11068
	public List<GameObject> EquipLabs;

	// Token: 0x04002B3D RID: 11069
	public List<UILabel> EquipDataNameLab;

	// Token: 0x04002B3E RID: 11070
	public List<UISprite> EquipIconSprite;

	// Token: 0x04002B3F RID: 11071
	public GameObject Cost;

	// Token: 0x04002B40 RID: 11072
	public UIWidget UpLevelBtn;

	// Token: 0x04002B41 RID: 11073
	public UIWidget UpLevelAllBtn;

	// Token: 0x04002B42 RID: 11074
	public UILabel NeedStackLab;

	// Token: 0x04002B43 RID: 11075
	private int CurStack = -1;

	// Token: 0x04002B44 RID: 11076
	private int NeedStack;

	// Token: 0x04002B45 RID: 11077
	public UILabel NeedMoneyLab;

	// Token: 0x04002B46 RID: 11078
	public UISprite LevelProgress;

	// Token: 0x04002B47 RID: 11079
	private int NeedMoney;

	// Token: 0x04002B48 RID: 11080
	private bool MaxLevelFlag;

	// Token: 0x04002B49 RID: 11081
	private PlayerData mplayerdata;

	// Token: 0x04002B4A RID: 11082
	public ParticleSystem EquipEffect;

	// Token: 0x04002B4B RID: 11083
	public UIPlayTween[] lineEffect;

	// Token: 0x04002B4C RID: 11084
	private List<GameItem> mEnhancePartList = new List<GameItem>();
}
