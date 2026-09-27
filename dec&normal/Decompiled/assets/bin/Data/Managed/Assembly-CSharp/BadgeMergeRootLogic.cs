using System;
using System.Collections.Generic;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x0200091F RID: 2335
public class BadgeMergeRootLogic : SingletonUnity<BadgeMergeRootLogic>
{
	// Token: 0x060040E1 RID: 16609 RVA: 0x001335AC File Offset: 0x001317AC
	protected override void Awake()
	{
		base.Awake();
		this.BackPackRootLogicScript.onClickItem = new BackPackRootLogic.OnClickItemDelegate(this.OnClickItem);
	}

	// Token: 0x060040E2 RID: 16610 RVA: 0x001335CC File Offset: 0x001317CC
	public void ResetEnable()
	{
		this.DirTipsParticle.Play();
		this.leftEffect.resetOnPlay = true;
		this.rightEffect.resetOnPlay = true;
		this.leftEffect.Play(true);
		this.rightEffect.Play(true);
		this.mergIng = 0;
	}

	// Token: 0x060040E3 RID: 16611 RVA: 0x0013361C File Offset: 0x0013181C
	private void OnEnable()
	{
		UIUpdateEvent.UpdateMoneyEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.UpdateMoneyEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateMoney));
	}

	// Token: 0x060040E4 RID: 16612 RVA: 0x0013364C File Offset: 0x0013184C
	private void OnDisable()
	{
		UIUpdateEvent.UpdateMoneyEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.UpdateMoneyEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateMoney));
		this.levelUpItem = null;
		if (this.DirTipsParticle != null)
		{
			this.DirTipsParticle.Clear();
			this.DirTipsParticle.Stop();
		}
	}

	// Token: 0x060040E5 RID: 16613 RVA: 0x001336A8 File Offset: 0x001318A8
	public void UpdateMoney()
	{
		if (this.levelUpItem != null)
		{
			this.mLevelUpBadgeData = DataManager.GetBadgeDataById(this.levelUpItem.ItemId);
			if (this.mLevelUpBadgeData == null)
			{
				return;
			}
			this.mMaxMergeNum = this.levelUpItem.StackNum / this.mLevelUpBadgeData.UpgradeCount;
			this.CoinsTxt.text = this.mLevelUpBadgeData.UpgradeCost.ToString();
			this.CoinsTxt2.text = (this.mLevelUpBadgeData.UpgradeCost * Mathf.Max(1, this.mMaxMergeNum)).ToString();
			long moneyNum = GameMoneyHelper.GetMoneyNum(0);
			if (moneyNum >= (long)this.mLevelUpBadgeData.UpgradeCost)
			{
				this.CoinsTxt.color = Color.white;
			}
			else
			{
				this.CoinsTxt.color = Color.red;
			}
			if (moneyNum >= (long)(this.mLevelUpBadgeData.UpgradeCost * Mathf.Max(1, this.mMaxMergeNum)))
			{
				this.CoinsTxt2.color = Color.white;
			}
			else
			{
				this.CoinsTxt2.color = Color.red;
			}
		}
	}

	// Token: 0x060040E6 RID: 16614 RVA: 0x001337C8 File Offset: 0x001319C8
	public void OnClickItem(GameItem item)
	{
		if (this.mergIng > 0)
		{
			return;
		}
		if (item != null && item == this.levelUpItem)
		{
			ItemInfoRootLogicNew.ShowItemTips(item, 0, false, UI_PAGE_TYPE.INVALID);
			return;
		}
		this.ShowLevelUpItem(item);
	}

	// Token: 0x060040E7 RID: 16615 RVA: 0x00133808 File Offset: 0x00131A08
	public void OnClickMoneyInfo()
	{
		ItemData moneyItemData = GameMoneyHelper.GetMoneyItemData(GameDefine.MONEY_TYPE.CASH);
		if (moneyItemData != null)
		{
			ItemInfoRootLogicNew.ShowItemTips(moneyItemData, false, UI_PAGE_TYPE.INVALID);
		}
	}

	// Token: 0x060040E8 RID: 16616 RVA: 0x0013382C File Offset: 0x00131A2C
	public void ClickLevelUpBtn()
	{
		if (this.levelUpItem != null)
		{
			this.HideLevelUpItem();
			this.levelUpItem = null;
			this.UpdateInfo(true);
		}
	}

	// Token: 0x060040E9 RID: 16617 RVA: 0x00133850 File Offset: 0x00131A50
	public void ShowLevelUpItem(GameItem item)
	{
		this.isCanMerge = false;
		this.mergIng = 0;
		if (item != null)
		{
			this.levelUpItem = item;
			this.mLevelUpBadgeData = DataManager.GetBadgeDataById(item.ItemId);
			string id = (int.Parse(item.ItemId) + 1).ToString();
			this.mNextBadgeData = DataManager.GetBadgeDataById(id);
			if (this.mNextBadgeData == null)
			{
				this.HideLevelUpItem();
				NoticeLogic.AddNotifyData("#{100933}", true, false);
				return;
			}
			ItemData itemDataByID = DataManager.GetItemDataByID(item.ItemId);
			this.LevelUpIcon.spriteName = itemDataByID.BackPackIcon;
			this.LevelUpQuality.spriteName = itemDataByID.QualityType.ToString();
			this.LevelUpUnloadFlag.enabled = true;
			this.LevelUpNameLabel.text = itemDataByID.MName;
			this.LevelUpNameLabel.color = GameDefine.GetColorByQuality(itemDataByID.QualityType);
			this.LevelUpNumLabel.text = item.StackNum.ToString();
			this.upCount = item.StackNum;
			this.mMaxMergeNum = item.StackNum / this.mLevelUpBadgeData.UpgradeCount;
			ItemData itemDataByID2 = DataManager.GetItemDataByID(id);
			this.LevelDownIcon.spriteName = itemDataByID2.BackPackIcon;
			this.LevelDownQuality.spriteName = itemDataByID2.QualityType.ToString();
			this.LevelDownNameLabel.text = itemDataByID2.MName;
			this.LevelDownNameLabel.color = GameDefine.GetColorByQuality(itemDataByID2.QualityType);
			this.LevelDownNumLabel.text = this.mMaxMergeNum.ToString();
			this.CoinsTxt.text = this.mLevelUpBadgeData.UpgradeCost.ToString();
			this.CoinsTxt2.text = (this.mLevelUpBadgeData.UpgradeCost * Mathf.Max(1, this.mMaxMergeNum)).ToString();
			long moneyNum = GameMoneyHelper.GetMoneyNum(0);
			if (moneyNum >= (long)this.mLevelUpBadgeData.UpgradeCost)
			{
				this.CoinsTxt.color = Color.white;
			}
			else
			{
				this.CoinsTxt.color = Color.red;
			}
			if (moneyNum >= (long)(this.mLevelUpBadgeData.UpgradeCost * Mathf.Max(1, this.mMaxMergeNum)))
			{
				this.CoinsTxt2.color = Color.white;
			}
			else
			{
				this.CoinsTxt2.color = Color.red;
			}
			this.isCanMerge = (this.mMaxMergeNum > 0);
			this.oneCost = this.mLevelUpBadgeData.UpgradeCost;
			this.allCost = this.mLevelUpBadgeData.UpgradeCost * Mathf.Max(1, this.mMaxMergeNum);
			UnityVersionUtil.SetActiveRecursive(this.CostObj, true);
		}
		else
		{
			this.HideLevelUpItem();
		}
	}

	// Token: 0x060040EA RID: 16618 RVA: 0x00133B04 File Offset: 0x00131D04
	public void HideLevelUpItem()
	{
		this.LevelUpIcon.spriteName = "CZ_zhuangBeiCao";
		this.LevelUpQuality.spriteName = string.Empty;
		this.LevelUpUnloadFlag.enabled = false;
		this.LevelUpNameLabel.text = string.Empty;
		this.LevelUpNumLabel.text = string.Empty;
		this.LevelDownIcon.spriteName = "CZ_zhuangBeiCao";
		this.LevelDownQuality.spriteName = string.Empty;
		this.LevelDownNameLabel.text = string.Empty;
		this.LevelDownNumLabel.text = string.Empty;
		UnityVersionUtil.SetActiveRecursive(this.CostObj, false);
		this.levelUpItem = null;
	}

	// Token: 0x060040EB RID: 16619 RVA: 0x00133BB0 File Offset: 0x00131DB0
	public void ClickShowNextItem()
	{
		if (this.mNextBadgeData == null)
		{
			return;
		}
		ItemData itemDataByID = DataManager.GetItemDataByID(this.mNextBadgeData.ID);
		if (itemDataByID != null)
		{
			ItemInfoRootLogicNew.ShowItemTips(itemDataByID, false, UI_PAGE_TYPE.INVALID);
		}
	}

	// Token: 0x060040EC RID: 16620 RVA: 0x00133BE8 File Offset: 0x00131DE8
	public void Show(GameItem item = null)
	{
		this.ResetEnable();
		if (this.playerData == null)
		{
			this.playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		}
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
		this.ShowLevelUpItem(item);
		this.UpdateInfo(true);
	}

	// Token: 0x060040ED RID: 16621 RVA: 0x00133C30 File Offset: 0x00131E30
	private void Refresh()
	{
		if (this && UnityVersionUtil.IsActive(base.gameObject))
		{
			this.UpdateInfo(false);
		}
	}

	// Token: 0x060040EE RID: 16622 RVA: 0x00133C60 File Offset: 0x00131E60
	public void UpdateInfo(bool needResetPos = true)
	{
		List<GameItem> targetTypeItem = ItemContainerTool.GetTargetTypeItem(this.playerData.BadgeBackPack, true, GameDefine.ITEM_TYPE.BADGE, false, PROFESSION_TYPE.INVALID);
		this.ShowItemList(targetTypeItem, needResetPos);
	}

	// Token: 0x060040EF RID: 16623 RVA: 0x00133C8C File Offset: 0x00131E8C
	public void ShowItemList(List<GameItem> list, bool needResetPos = true)
	{
		this.BackPackRootLogicScript.ShowList(list, needResetPos);
	}

	// Token: 0x060040F0 RID: 16624 RVA: 0x00133C9C File Offset: 0x00131E9C
	public void ClickMergeOne()
	{
		if (this.mergIng > 0)
		{
			return;
		}
		if (!this.isCanMerge)
		{
			NoticeLogic.AddNotifyData("#{100924}", true, false);
			return;
		}
		if (GameMoneyHelper.BeforeCheckBuy(GameDefine.MONEY_TYPE.CASH, this.oneCost))
		{
			this.PlayerEffect();
			badge_merge.request request = new badge_merge.request();
			request.indexId = this.levelUpItem.IndexId;
			request.nextItemId = this.mNextBadgeData.ID;
			request.count = 1L;
			NetLogic.GetInstance().Send<Protocol.badge_merge>(request, new RpcRspHandler(this.BadgeMergeResponse));
		}
	}

	// Token: 0x060040F1 RID: 16625 RVA: 0x00133D2C File Offset: 0x00131F2C
	public void ClickMergeAll()
	{
		if (this.mergIng > 0)
		{
			return;
		}
		if (!this.isCanMerge)
		{
			NoticeLogic.AddNotifyData("#{100924}", true, false);
			return;
		}
		if (GameMoneyHelper.BeforeCheckBuy(GameDefine.MONEY_TYPE.CASH, this.allCost))
		{
			this.PlayerEffect();
			badge_merge.request request = new badge_merge.request();
			request.indexId = this.levelUpItem.IndexId;
			request.nextItemId = this.mNextBadgeData.ID;
			request.count = (long)this.mMaxMergeNum;
			NetLogic.GetInstance().Send<Protocol.badge_merge>(request, new RpcRspHandler(this.BadgeMergeResponse));
		}
	}

	// Token: 0x060040F2 RID: 16626 RVA: 0x00133DC4 File Offset: 0x00131FC4
	public void BadgeMergeResponse(SprotoTypeBase sp)
	{
		this.mergIng++;
		badge_merge.response response = sp as badge_merge.response;
		if (response != null && response.HasState && response.state == 0L)
		{
			if (this.mergIng >= 3)
			{
				if (this.levelUpItem.IsEmpty())
				{
					this.levelUpItem = null;
				}
				this.ShowLevelUpItem(this.levelUpItem);
			}
			this.Refresh();
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("BadgeMerger", "merger", "merger_times");
		}
		if (this.mergIng >= 3)
		{
			this.mergIng = 0;
		}
	}

	// Token: 0x060040F3 RID: 16627 RVA: 0x00133E64 File Offset: 0x00132064
	public void EffectFinish()
	{
		this.mergIng++;
		if (this && UnityVersionUtil.IsActive(base.gameObject) && this.mergIng >= 3)
		{
			if (this.levelUpItem.IsEmpty())
			{
				this.levelUpItem = null;
			}
			this.ShowLevelUpItem(this.levelUpItem);
		}
		if (this.mergIng >= 3)
		{
			this.mergIng = 0;
		}
	}

	// Token: 0x060040F4 RID: 16628 RVA: 0x00133EDC File Offset: 0x001320DC
	public void PlayerEffect()
	{
		this.mergIng++;
		SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(19, 1f, null);
		this.upgradeEffect.resetOnPlay = true;
		this.upgradeEffect.Play(true);
	}

	// Token: 0x04002C7E RID: 11390
	public int lineCount = 6;

	// Token: 0x04002C7F RID: 11391
	public int ShowLineCount = 4;

	// Token: 0x04002C80 RID: 11392
	private PlayerData playerData;

	// Token: 0x04002C81 RID: 11393
	public BackPackRootLogic BackPackRootLogicScript;

	// Token: 0x04002C82 RID: 11394
	private GameItem levelUpItem;

	// Token: 0x04002C83 RID: 11395
	public UISprite LevelUpIcon;

	// Token: 0x04002C84 RID: 11396
	public UISprite LevelUpQuality;

	// Token: 0x04002C85 RID: 11397
	public UILabel LevelUpNameLabel;

	// Token: 0x04002C86 RID: 11398
	public UISprite LevelUpUnloadFlag;

	// Token: 0x04002C87 RID: 11399
	public UILabel LevelUpNumLabel;

	// Token: 0x04002C88 RID: 11400
	public UISprite LevelDownIcon;

	// Token: 0x04002C89 RID: 11401
	public UISprite LevelDownQuality;

	// Token: 0x04002C8A RID: 11402
	public UILabel LevelDownNameLabel;

	// Token: 0x04002C8B RID: 11403
	public UILabel LevelDownNumLabel;

	// Token: 0x04002C8C RID: 11404
	public ParticleSystem DirTipsParticle;

	// Token: 0x04002C8D RID: 11405
	public GameObject CostObj;

	// Token: 0x04002C8E RID: 11406
	public UILabel CoinsTxt;

	// Token: 0x04002C8F RID: 11407
	public UILabel CoinsTxt2;

	// Token: 0x04002C90 RID: 11408
	public UIPlayTween leftEffect;

	// Token: 0x04002C91 RID: 11409
	public UIPlayTween rightEffect;

	// Token: 0x04002C92 RID: 11410
	private bool isCanMerge;

	// Token: 0x04002C93 RID: 11411
	private BadgeData mNextBadgeData;

	// Token: 0x04002C94 RID: 11412
	private BadgeData mLevelUpBadgeData;

	// Token: 0x04002C95 RID: 11413
	private GameItem mCurItem;

	// Token: 0x04002C96 RID: 11414
	private int mMaxMergeNum;

	// Token: 0x04002C97 RID: 11415
	private int oneCost;

	// Token: 0x04002C98 RID: 11416
	private int allCost;

	// Token: 0x04002C99 RID: 11417
	private int upCount;

	// Token: 0x04002C9A RID: 11418
	private int mergIng;

	// Token: 0x04002C9B RID: 11419
	public UIPlayTween upgradeEffect;
}
