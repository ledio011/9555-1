using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000990 RID: 2448
public class ShopItemBtnLogic : MonoBehaviour
{
	// Token: 0x06004550 RID: 17744 RVA: 0x0015BAE4 File Offset: 0x00159CE4
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004551 RID: 17745 RVA: 0x0015BAF0 File Offset: 0x00159CF0
	private void CheckTutorialEvent()
	{
		if (SingletonUnity<TutorialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.CloseCheck();
		}
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x06004552 RID: 17746 RVA: 0x0015BB48 File Offset: 0x00159D48
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x06004553 RID: 17747 RVA: 0x0015BB54 File Offset: 0x00159D54
	public void Reset(shop_item item, GameDefine.SHOP_TYPE type, ShopItemBtnLogic.OnClickShopItemDelegate clickfun)
	{
		this.onClickShopItem = clickfun;
		this.curType = type;
		this.mCurShopItem = item;
		ItemData itemDataByID = DataManager.GetItemDataByID(item.ItemID);
		if (itemDataByID != null)
		{
			this.Icon.spriteName = itemDataByID.BackPackIcon;
			this.NameLabel.text = itemDataByID.MName;
			this.PriceLabel.text = GameMoneyHelper.GetMoneyValStr(item.Price, item.PriceType);
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				UnityVersionUtil.SetActiveRecursive(this.ItemQualitySprite.gameObject, true);
				this.ItemQualitySprite.spriteName = ((EQUIP_QUALITY)item.Quality).ToString();
				this.NameLabel.color = GameDefine.GetColorByQuality((EQUIP_QUALITY)item.Quality);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.ItemQualitySprite.gameObject, true);
				this.ItemQualitySprite.spriteName = itemDataByID.QualityType.ToString();
				this.NameLabel.color = GameDefine.GetColorByQuality(itemDataByID.QualityType);
			}
			if (itemDataByID.UseHour > 0)
			{
				if (this.ExpireFlag != null)
				{
					this.ExpireFlag.enabled = true;
				}
			}
			else if (this.ExpireFlag != null)
			{
				this.ExpireFlag.enabled = false;
			}
		}
		if (this.mCurShopItem.HasDiscount && this.mCurShopItem.Discount < 100L)
		{
			this.saleValueLabel.text = string.Format("{0}", this.mCurShopItem.Discount);
			UnityVersionUtil.SetActiveRecursive(this.ForSaleObj, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ForSaleObj, false);
		}
		if (this.mCurShopItem.Limit > 0L)
		{
			int num = (int)(this.mCurShopItem.Limit - this.mCurShopItem.curNum);
			this.NumLimitLabel.text = StrDictionary.GetDictionaryString("#{100746}", new object[]
			{
				num
			});
			this.NumLimitLabel.enabled = true;
		}
		else
		{
			this.NumLimitLabel.enabled = false;
		}
		this.isHaveLimittime = false;
		ShopData shopDataByID = DataManager.GetShopDataByID(item.ID);
		if (shopDataByID != null)
		{
			if (string.IsNullOrEmpty(shopDataByID.EndTime))
			{
				UnityVersionUtil.SetActiveRecursive(this.limittimeobj, false);
			}
			else
			{
				TimeSpan shopItemTime = TimeTools.GetShopItemTime(shopDataByID.EndTimes);
				this.limitTime = (float)shopItemTime.TotalSeconds;
				this.isHaveLimittime = true;
				this.starttime = Time.time;
				if (shopItemTime.Days > 0)
				{
					this.limittimelabel.text = string.Format("{0}D", shopItemTime.Days + 1);
				}
				else if (shopItemTime.Hours > 0)
				{
					this.limittimelabel.text = string.Format("{0}H", shopItemTime.Hours + 1);
				}
				else if (shopItemTime.Minutes > 0)
				{
					this.limittimelabel.text = string.Format("{0}M", shopItemTime.Minutes + 1);
				}
				else if (shopItemTime.Seconds > 0)
				{
					this.limittimelabel.text = string.Format("{0}S", shopItemTime.Seconds);
				}
				if (shopItemTime.TotalSeconds <= 0.0)
				{
					this.limittimelabel.text = "0S";
				}
				UnityVersionUtil.SetActiveRecursive(this.limittimeobj, true);
			}
		}
	}

	// Token: 0x06004554 RID: 17748 RVA: 0x0015BEE0 File Offset: 0x0015A0E0
	private void Update()
	{
		if (this.isHaveLimittime)
		{
			this.limitTime -= Time.deltaTime;
			TimeSpan timeSpan;
			timeSpan..ctor(0, 0, (int)this.limitTime);
			if (timeSpan.Days > 0)
			{
				this.limittimelabel.text = string.Format("{0}D", timeSpan.Days + 1);
			}
			else if (timeSpan.Hours > 0)
			{
				this.limittimelabel.text = string.Format("{0}H", timeSpan.Hours + 1);
			}
			else if (timeSpan.Minutes > 0)
			{
				this.limittimelabel.text = string.Format("{0}M", timeSpan.Minutes + 1);
			}
			else if (timeSpan.Seconds > 0)
			{
				this.limittimelabel.text = string.Format("{0}S", timeSpan.Seconds);
			}
			if (timeSpan.TotalSeconds <= 0.0)
			{
				this.limittimelabel.text = "0S";
				this.isHaveLimittime = false;
			}
		}
	}

	// Token: 0x06004555 RID: 17749 RVA: 0x0015C014 File Offset: 0x0015A214
	public void UpdateSelect(shop_item item)
	{
		if (item != null && this.mCurShopItem.ID == item.ID)
		{
			this.BkSprite.spriteName = "CZ_huaDongBG_1";
		}
		else
		{
			this.BkSprite.spriteName = "CZ_huaDongBG";
		}
	}

	// Token: 0x06004556 RID: 17750 RVA: 0x0015C068 File Offset: 0x0015A268
	public void OnClickBtn()
	{
		if (this.onClickShopItem != null)
		{
			this.onClickShopItem(this.mCurShopItem, base.gameObject);
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.BUY_BADGE_CHOOSE_BADGE)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004557 RID: 17751 RVA: 0x0015C0A4 File Offset: 0x0015A2A4
	public void UpdateInfo(shop_item newitem)
	{
		if (newitem.ID == this.mCurShopItem.ID)
		{
			this.mCurShopItem.curNum = newitem.curNum;
		}
		if (this.mCurShopItem.Limit > 0L)
		{
			int num = (int)(this.mCurShopItem.Limit - this.mCurShopItem.curNum);
			this.NumLimitLabel.text = StrDictionary.GetDictionaryString("#{100746}", new object[]
			{
				num
			});
			this.NumLimitLabel.enabled = true;
		}
		else
		{
			this.NumLimitLabel.enabled = false;
		}
	}

	// Token: 0x04003228 RID: 12840
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04003229 RID: 12841
	public ShopItemBtnLogic.OnClickShopItemDelegate onClickShopItem;

	// Token: 0x0400322A RID: 12842
	public UISprite Icon;

	// Token: 0x0400322B RID: 12843
	public UILabel NameLabel;

	// Token: 0x0400322C RID: 12844
	public UILabel PriceLabel;

	// Token: 0x0400322D RID: 12845
	public UISprite ItemQualitySprite;

	// Token: 0x0400322E RID: 12846
	public UISprite BkSprite;

	// Token: 0x0400322F RID: 12847
	private GameDefine.SHOP_TYPE curType;

	// Token: 0x04003230 RID: 12848
	private shop_item mCurShopItem;

	// Token: 0x04003231 RID: 12849
	public GameObject ForSaleObj;

	// Token: 0x04003232 RID: 12850
	public UILabel saleValueLabel;

	// Token: 0x04003233 RID: 12851
	public UISprite ExpireFlag;

	// Token: 0x04003234 RID: 12852
	public UILabel limittimelabel;

	// Token: 0x04003235 RID: 12853
	public GameObject limittimeobj;

	// Token: 0x04003236 RID: 12854
	private bool isHaveLimittime;

	// Token: 0x04003237 RID: 12855
	private float starttime;

	// Token: 0x04003238 RID: 12856
	private float limitTime;

	// Token: 0x04003239 RID: 12857
	public UILabel NumLimitLabel;

	// Token: 0x02000AF9 RID: 2809
	// (Invoke) Token: 0x0600506D RID: 20589
	public delegate void OnClickShopItemDelegate(shop_item itemKey, GameObject itemobj);
}
