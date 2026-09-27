using System;
using System.Collections.Generic;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x0200090D RID: 2317
public class InhertUIRootLogic : SingletonUnity<InhertUIRootLogic>
{
	// Token: 0x06003F95 RID: 16277 RVA: 0x00128658 File Offset: 0x00126858
	protected override void Awake()
	{
		base.Awake();
		this.BackPackRootLogicScript.onClickItem = new BackPackRootLogic.OnClickItemDelegate(this.OnClickItem);
	}

	// Token: 0x06003F96 RID: 16278 RVA: 0x00128678 File Offset: 0x00126878
	public void ResetEnable()
	{
		this.DirTipsParticle.Play();
		this.leftEffect.resetOnPlay = true;
		this.rightEffect.resetOnPlay = true;
		this.leftEffect.Play(true);
		this.rightEffect.Play(true);
		this.InhertIng = false;
	}

	// Token: 0x06003F97 RID: 16279 RVA: 0x001286C8 File Offset: 0x001268C8
	private void OnEnable()
	{
		UIUpdateEvent.UpdateMoneyEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.UpdateMoneyEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateMoney));
	}

	// Token: 0x06003F98 RID: 16280 RVA: 0x001286F8 File Offset: 0x001268F8
	private void OnDisable()
	{
		UIUpdateEvent.UpdateMoneyEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.UpdateMoneyEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateMoney));
		this.levelUpItem = null;
		this.levelDownItem = null;
		if (this.DirTipsParticle != null)
		{
			this.DirTipsParticle.Clear();
			this.DirTipsParticle.Stop();
		}
	}

	// Token: 0x06003F99 RID: 16281 RVA: 0x0012875C File Offset: 0x0012695C
	public void UpdateMoney()
	{
		if (this.levelUpItem != null && this.levelDownItem != null)
		{
			EquipData equipDataById = DataManager.GetEquipDataById(this.levelUpItem.ItemId);
			int exp = this.levelDownItem.Parm[0];
			int num;
			int upgradeLevelByExp = equipDataById.GetUpgradeLevelByExp(this.levelUpItem.ItemLevel, exp, out num);
			if (num > 0)
			{
				this.InhertBtn.spriteName = GameDefine.BtnIcon[1];
				this.isCaninhert = true;
				NGUITools.SetActive(this.CostObj, true);
			}
			else
			{
				this.InhertBtn.spriteName = GameDefine.BtnIcon[2];
				this.isCaninhert = false;
				NGUITools.SetActive(this.CostObj, false);
			}
			this.currentCostCoin = (int)(this.costCoinsN * (float)num);
			this.CoinsTxt.text = this.currentCostCoin.ToString();
			if ((long)this.currentCostCoin <= GameMoneyHelper.GetMoneyNum(0))
			{
				this.CoinsTxt.color = Color.white;
			}
			else
			{
				this.CoinsTxt.color = Color.red;
			}
		}
	}

	// Token: 0x06003F9A RID: 16282 RVA: 0x00128868 File Offset: 0x00126A68
	public void OnClickItem(GameItem item)
	{
		if (this.levelDownItem == null)
		{
			this.ShowLevelDownItem(item);
		}
		else if (this.levelUpItem == null)
		{
			this.ShowLevelUpItem(item);
		}
		this.UpdateInfo();
	}

	// Token: 0x06003F9B RID: 16283 RVA: 0x0012889C File Offset: 0x00126A9C
	public void OnClickMoneyInfo()
	{
		ItemData moneyItemData = GameMoneyHelper.GetMoneyItemData(GameDefine.MONEY_TYPE.CASH);
		if (moneyItemData != null)
		{
			ItemInfoRootLogicNew.ShowItemTips(moneyItemData, false, UI_PAGE_TYPE.INVALID);
		}
	}

	// Token: 0x06003F9C RID: 16284 RVA: 0x001288C0 File Offset: 0x00126AC0
	public void ClickLevelUpBtn()
	{
		if (this.levelUpItem != null)
		{
			this.HideLevelUpItem();
			this.levelUpItem = null;
			this.UpdateInfo();
		}
	}

	// Token: 0x06003F9D RID: 16285 RVA: 0x001288E0 File Offset: 0x00126AE0
	public void ClickLevelDownBtn()
	{
		if (this.levelDownItem != null)
		{
			this.HideLevelDownItem();
			this.levelDownItem = null;
			this.UpdateInfo();
		}
	}

	// Token: 0x06003F9E RID: 16286 RVA: 0x00128900 File Offset: 0x00126B00
	public void ShowLevelUpItem(GameItem item)
	{
		if (item != null)
		{
			this.levelUpItem = item;
			ItemData itemDataByID = DataManager.GetItemDataByID(item.ItemId);
			this.LevelUpIcon.spriteName = itemDataByID.BackPackIcon;
			this.LevelUpQuality.spriteName = item.GetItemQuality().ToString();
			this.LevelUpUnloadFlag.enabled = true;
			this.LevelUpNameLabel.text = itemDataByID.MName;
			this.LevelUpNameLabel.color = GameDefine.GetColorByQuality(item.GetItemQuality());
			NGUITools.SetActive(this.LevelUpObj, false);
		}
	}

	// Token: 0x06003F9F RID: 16287 RVA: 0x00128994 File Offset: 0x00126B94
	public void ShowLevelUpLevel(int nextLevel)
	{
		NGUITools.SetActive(this.LevelUpObj, true);
		this.LevelUpCurLevel.text = string.Format("Lv.{0}", this.levelUpItem.ItemLevel);
		this.LevelUpNextLevel.text = string.Format("Lv.{0}", nextLevel);
	}

	// Token: 0x06003FA0 RID: 16288 RVA: 0x001289F0 File Offset: 0x00126BF0
	public void HideLevelUpItem()
	{
		this.LevelUpIcon.spriteName = "CZ_zhuangBeiCao";
		this.LevelUpQuality.spriteName = string.Empty;
		this.LevelUpUnloadFlag.enabled = false;
		this.LevelUpNameLabel.text = string.Empty;
		NGUITools.SetActive(this.LevelUpObj, false);
	}

	// Token: 0x06003FA1 RID: 16289 RVA: 0x00128A48 File Offset: 0x00126C48
	public void ShowLevelDownItem(GameItem item)
	{
		if (item != null)
		{
			this.levelDownItem = item;
			ItemData itemDataByID = DataManager.GetItemDataByID(item.ItemId);
			this.LevelDownIcon.spriteName = itemDataByID.BackPackIcon;
			this.LevelDownQuality.spriteName = item.GetItemQuality().ToString();
			this.LevelDownUnloadFlag.enabled = true;
			this.LevelDownNameLabel.text = itemDataByID.MName;
			this.LevelDownNameLabel.color = GameDefine.GetColorByQuality(item.GetItemQuality());
			NGUITools.SetActive(this.LevelDownObj, true);
			this.LevelDownCurLevel.text = string.Format("Lv.{0}", item.ItemLevel.ToString());
			this.LevelDownNextLevel.text = "Lv.0";
		}
	}

	// Token: 0x06003FA2 RID: 16290 RVA: 0x00128B0C File Offset: 0x00126D0C
	public void HideLevelDownItem()
	{
		this.LevelDownIcon.spriteName = "CZ_zhuangBeiCao";
		this.LevelDownQuality.spriteName = string.Empty;
		this.LevelDownUnloadFlag.enabled = false;
		this.LevelDownNameLabel.text = string.Empty;
		NGUITools.SetActive(this.LevelDownObj, false);
	}

	// Token: 0x06003FA3 RID: 16291 RVA: 0x00128B64 File Offset: 0x00126D64
	public void Show(GameItem item = null, bool isShowUp = false)
	{
		this.ResetEnable();
		if (this.playerData == null)
		{
			this.playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		}
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
		if (item != null)
		{
			if (isShowUp)
			{
				this.levelUpItem = item;
				this.AutoSelectDownItem();
			}
			else
			{
				this.levelDownItem = item;
			}
		}
		this.UpdateInfo();
	}

	// Token: 0x06003FA4 RID: 16292 RVA: 0x00128BCC File Offset: 0x00126DCC
	public void AutoSelectDownItem()
	{
		List<GameItem> list = new List<GameItem>();
		list.Add(this.levelUpItem);
		ItemData itemData = this.levelUpItem.ItemData;
		List<GameItem> subItem = ItemContainerTool.GetSubItem(this.playerData.EquipPack, GameDefine.ITEM_TYPE.EQUIP, itemData.SubType, list, PROFESSION_TYPE.INVALID);
		List<GameItem> subItem2 = ItemContainerTool.GetSubItem(this.playerData.EquipBackPack, GameDefine.ITEM_TYPE.EQUIP, itemData.SubType, list, PROFESSION_TYPE.INVALID);
		subItem.AddRange(subItem2);
		GameItem gameItem = null;
		for (int i = 0; i < subItem.Count; i++)
		{
			if (gameItem == null || gameItem.ItemLevel < subItem[i].ItemLevel)
			{
				gameItem = subItem[i];
			}
		}
		this.levelDownItem = gameItem;
	}

	// Token: 0x06003FA5 RID: 16293 RVA: 0x00128C84 File Offset: 0x00126E84
	public void Hide()
	{
		NGUITools.SetActive(base.gameObject, false);
	}

	// Token: 0x06003FA6 RID: 16294 RVA: 0x00128C94 File Offset: 0x00126E94
	public void UpdateInfo()
	{
		if (this.levelUpItem == null && this.levelDownItem == null)
		{
			List<GameItem> targetTypeItem = ItemContainerTool.GetTargetTypeItem(this.playerData.EquipPack, true, GameDefine.ITEM_TYPE.EQUIP, false, PROFESSION_TYPE.INVALID);
			List<GameItem> targetTypeItem2 = ItemContainerTool.GetTargetTypeItem(this.playerData.EquipBackPack, false, GameDefine.ITEM_TYPE.EQUIP, false, this.playerData.Profession);
			targetTypeItem.AddRange(targetTypeItem2);
			this.ShowItemList(targetTypeItem, true);
			this.HideLevelDownItem();
			this.HideLevelUpItem();
			this.InhertBtn.spriteName = GameDefine.BtnIcon[2];
			this.isCaninhert = false;
			this.CoinsTxt.text = string.Empty;
			NGUITools.SetActive(this.CostObj, false);
		}
		else if (this.levelDownItem == null)
		{
			List<GameItem> list = new List<GameItem>();
			list.Add(this.levelUpItem);
			ItemData itemData = this.levelUpItem.ItemData;
			List<GameItem> subItem = ItemContainerTool.GetSubItem(this.playerData.EquipPack, GameDefine.ITEM_TYPE.EQUIP, itemData.SubType, list, PROFESSION_TYPE.INVALID);
			List<GameItem> subItem2 = ItemContainerTool.GetSubItem(this.playerData.EquipBackPack, GameDefine.ITEM_TYPE.EQUIP, itemData.SubType, list, this.playerData.Profession);
			subItem.AddRange(subItem2);
			this.ShowItemList(subItem, true);
			this.ShowLevelUpItem(this.levelUpItem);
			this.HideLevelDownItem();
			this.InhertBtn.spriteName = GameDefine.BtnIcon[2];
			this.isCaninhert = false;
			this.CoinsTxt.text = string.Empty;
			NGUITools.SetActive(this.CostObj, false);
		}
		else if (this.levelUpItem == null)
		{
			List<GameItem> list2 = new List<GameItem>();
			list2.Add(this.levelDownItem);
			ItemData itemData2 = this.levelDownItem.ItemData;
			List<GameItem> subItem3 = ItemContainerTool.GetSubItem(this.playerData.EquipPack, GameDefine.ITEM_TYPE.EQUIP, itemData2.SubType, list2, PROFESSION_TYPE.INVALID);
			List<GameItem> subItem4 = ItemContainerTool.GetSubItem(this.playerData.EquipBackPack, GameDefine.ITEM_TYPE.EQUIP, itemData2.SubType, list2, this.playerData.Profession);
			subItem3.AddRange(subItem4);
			this.ShowItemList(subItem3, true);
			this.ShowLevelDownItem(this.levelDownItem);
			this.HideLevelUpItem();
			this.InhertBtn.spriteName = GameDefine.BtnIcon[2];
			this.isCaninhert = false;
			this.CoinsTxt.text = string.Empty;
			NGUITools.SetActive(this.CostObj, false);
		}
		else
		{
			List<GameItem> list3 = new List<GameItem>();
			list3.Add(this.levelDownItem);
			list3.Add(this.levelUpItem);
			ItemData itemData3 = this.levelDownItem.ItemData;
			List<GameItem> subItem5 = ItemContainerTool.GetSubItem(this.playerData.EquipPack, GameDefine.ITEM_TYPE.EQUIP, itemData3.SubType, list3, PROFESSION_TYPE.INVALID);
			List<GameItem> subItem6 = ItemContainerTool.GetSubItem(this.playerData.EquipBackPack, GameDefine.ITEM_TYPE.EQUIP, itemData3.SubType, list3, this.playerData.Profession);
			subItem5.AddRange(subItem6);
			this.ShowItemList(subItem5, true);
			this.ShowLevelUpItem(this.levelUpItem);
			this.ShowLevelDownItem(this.levelDownItem);
			EquipData equipDataById = DataManager.GetEquipDataById(this.levelUpItem.ItemId);
			int exp = this.levelDownItem.Parm[0];
			int num;
			int upgradeLevelByExp = equipDataById.GetUpgradeLevelByExp(this.levelUpItem.ItemLevel, exp, out num);
			this.ShowLevelUpLevel(upgradeLevelByExp);
			if (num > 0)
			{
				this.InhertBtn.spriteName = GameDefine.BtnIcon[1];
				this.isCaninhert = true;
				NGUITools.SetActive(this.CostObj, true);
			}
			else
			{
				this.InhertBtn.spriteName = GameDefine.BtnIcon[2];
				this.isCaninhert = false;
				NGUITools.SetActive(this.CostObj, false);
			}
			this.currentCostCoin = (int)(this.costCoinsN * (float)num);
			this.CoinsTxt.text = this.currentCostCoin.ToString();
			if ((long)this.currentCostCoin <= GameMoneyHelper.GetMoneyNum(0))
			{
				this.CoinsTxt.color = Color.white;
			}
			else
			{
				this.CoinsTxt.color = Color.red;
			}
		}
	}

	// Token: 0x06003FA7 RID: 16295 RVA: 0x00129068 File Offset: 0x00127268
	public void ShowItemList(List<GameItem> list, bool needResetPos = true)
	{
		this.BackPackRootLogicScript.ShowList(list, needResetPos);
	}

	// Token: 0x06003FA8 RID: 16296 RVA: 0x00129078 File Offset: 0x00127278
	public void ClickInhertBtn()
	{
		if (this.InhertIng)
		{
			return;
		}
		if (!this.isCaninhert)
		{
			return;
		}
		if (GameMoneyHelper.BeforeCheckBuy(GameDefine.MONEY_TYPE.CASH, this.currentCostCoin))
		{
			equip_inhert.request request = new equip_inhert.request();
			request.containertype1 = (long)this.levelDownItem.ContainerType;
			request.indexId1 = this.levelDownItem.IndexId;
			request.containertype2 = (long)this.levelUpItem.ContainerType;
			request.indexId2 = this.levelUpItem.IndexId;
			this.InhertIng = true;
			NetLogic.GetInstance().Send<Protocol.equip_inhert>(request, new RpcRspHandler(this.EquipInhertResponse));
		}
	}

	// Token: 0x06003FA9 RID: 16297 RVA: 0x00129118 File Offset: 0x00127318
	public void EquipInhertResponse(SprotoTypeBase sp)
	{
		this.InhertIng = false;
		equip_inhert.response response = sp as equip_inhert.response;
		if (response != null)
		{
			if (response.state != 0L)
			{
				NoticeLogic.AddNotifyData("#{100922}", true, false);
				return;
			}
			ITEM_CONTAINER_TYPE type = (ITEM_CONTAINER_TYPE)response.containertype1;
			ItemContainer itemContainer = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetItemContainer(type);
			if (itemContainer != null)
			{
				GameItem itemByIndexId = itemContainer.GetItemByIndexId(response.item1.indexId);
				itemByIndexId.UpdateItem(response.item1);
			}
			ITEM_CONTAINER_TYPE type2 = (ITEM_CONTAINER_TYPE)response.containertype2;
			ItemContainer itemContainer2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetItemContainer(type2);
			if (itemContainer2 != null)
			{
				GameItem itemByIndexId2 = itemContainer2.GetItemByIndexId(response.item2.indexId);
				itemByIndexId2.UpdateItem(response.item2);
			}
			NoticeLogic.AddNotifyData("#{100921}", true, false);
			if (this == null || !UnityVersionUtil.IsActive(base.gameObject))
			{
				return;
			}
			this.UpdateInfo();
			this.PlayerEffect();
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Inherit", "inherit", "inherit_times");
		}
	}

	// Token: 0x06003FAA RID: 16298 RVA: 0x00129220 File Offset: 0x00127420
	public void PlayerEffect()
	{
		SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(19, 1f, null);
		this.upgradeEffect.resetOnPlay = true;
		this.upgradeEffect.Play(true);
	}

	// Token: 0x04002B67 RID: 11111
	public int lineCount = 6;

	// Token: 0x04002B68 RID: 11112
	public int ShowLineCount = 4;

	// Token: 0x04002B69 RID: 11113
	private PlayerData playerData;

	// Token: 0x04002B6A RID: 11114
	public BackPackRootLogic BackPackRootLogicScript;

	// Token: 0x04002B6B RID: 11115
	private List<GameItem> mCurItemList;

	// Token: 0x04002B6C RID: 11116
	private GameItem levelUpItem;

	// Token: 0x04002B6D RID: 11117
	private GameItem levelDownItem;

	// Token: 0x04002B6E RID: 11118
	private bool mInitFlag;

	// Token: 0x04002B6F RID: 11119
	public UISprite LevelUpIcon;

	// Token: 0x04002B70 RID: 11120
	public UISprite LevelUpQuality;

	// Token: 0x04002B71 RID: 11121
	public UILabel LevelUpNameLabel;

	// Token: 0x04002B72 RID: 11122
	public UISprite LevelUpUnloadFlag;

	// Token: 0x04002B73 RID: 11123
	public UISprite LevelDownIcon;

	// Token: 0x04002B74 RID: 11124
	public UISprite LevelDownQuality;

	// Token: 0x04002B75 RID: 11125
	public UILabel LevelDownNameLabel;

	// Token: 0x04002B76 RID: 11126
	public UISprite LevelDownUnloadFlag;

	// Token: 0x04002B77 RID: 11127
	public GameObject LevelDownObj;

	// Token: 0x04002B78 RID: 11128
	public UILabel LevelDownCurLevel;

	// Token: 0x04002B79 RID: 11129
	public UILabel LevelDownNextLevel;

	// Token: 0x04002B7A RID: 11130
	public GameObject LevelUpObj;

	// Token: 0x04002B7B RID: 11131
	public UILabel LevelUpCurLevel;

	// Token: 0x04002B7C RID: 11132
	public UILabel LevelUpNextLevel;

	// Token: 0x04002B7D RID: 11133
	public ParticleSystem DirTipsParticle;

	// Token: 0x04002B7E RID: 11134
	public GameObject CostObj;

	// Token: 0x04002B7F RID: 11135
	public UISprite InhertBtn;

	// Token: 0x04002B80 RID: 11136
	public UILabel CoinsTxt;

	// Token: 0x04002B81 RID: 11137
	private float costCoinsN = 1f;

	// Token: 0x04002B82 RID: 11138
	private int currentCostCoin;

	// Token: 0x04002B83 RID: 11139
	public UIPlayTween leftEffect;

	// Token: 0x04002B84 RID: 11140
	public UIPlayTween rightEffect;

	// Token: 0x04002B85 RID: 11141
	private bool isCaninhert;

	// Token: 0x04002B86 RID: 11142
	private bool InhertIng;

	// Token: 0x04002B87 RID: 11143
	public UIPlayTween upgradeEffect;
}
