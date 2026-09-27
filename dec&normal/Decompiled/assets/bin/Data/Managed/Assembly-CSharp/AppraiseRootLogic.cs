using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200091D RID: 2333
public class AppraiseRootLogic : MonoBehaviour
{
	// Token: 0x060040CC RID: 16588 RVA: 0x001327A0 File Offset: 0x001309A0
	public void UpdateInfo(GameItem item, ITEM_SHOW_TYPE showType)
	{
		this.CurItem = item;
		ItemData itemData = this.CurItem.ItemData;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.ItemIcon.spriteName = itemData.BackPackIcon;
		this.ItemQualityIcon.spriteName = this.CurItem.GetItemQuality().ToString();
		this.ItemNameLabel.text = itemData.MName;
		this.ItemNameLabel.color = GameDefine.GetColorByQuality(this.CurItem.GetItemQuality());
		EquipData equipDataById = DataManager.GetEquipDataById(this.CurItem.ItemId);
		if (this.CurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			this.LevelNameLabel.text = StrDictionary.GetDictionaryString("#{100682}", new object[0]);
			this.LevelLabel.text = equipDataById.Class.ToString();
			this.SetLabelWarning(this.LevelLabel, false);
		}
		else
		{
			this.LevelNameLabel.text = StrDictionary.GetDictionaryString("#{100606}", new object[0]);
			this.LevelLabel.text = itemData.Level.ToString();
			this.SetLabelWarning(this.LevelLabel, playerData.Level < itemData.Level);
		}
		if (this.CurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP && this.CurItem.ItemData.SubType == 0)
		{
			this.ProfessionLabel.text = StrDictionary.GetDictionaryString(GameDefine.WeaponName[equipDataById.WeaponType], new object[0]);
			this.SetLabelWarning(this.ProfessionLabel, false);
			this.ProfessionNameLabel.text = StrDictionary.GetDictionaryString("#{101219}", new object[0]);
		}
		else
		{
			this.ProfessionLabel.text = StrDictionary.GetDictionaryString(GameDefine.ProfessionName[equipDataById.Job], new object[0]);
			this.SetLabelWarning(this.ProfessionLabel, playerData.Profession != equipDataById.profession);
			this.ProfessionNameLabel.text = StrDictionary.GetDictionaryString("#{100607}", new object[0]);
		}
		this.ShowBaseAtt(this.CurItem);
		if (itemData.CanSell())
		{
			this.RecycleLabel.text = GameMoneyHelper.GetMoneyValStr(itemData.GetSellPrice(this.CurItem.GetItemQuality()), itemData.PriceType);
			UnityVersionUtil.SetActiveRecursive(this.RecycleLabel.gameObject, true);
		}
		else
		{
			this.RecycleLabel.text = string.Empty;
			UnityVersionUtil.SetActiveRecursive(this.RecycleLabel.gameObject, false);
		}
		this.DescLabel.text = StrDictionary.GetDictionaryString("#{100666}", new object[0]);
		if (showType == ITEM_SHOW_TYPE.BACKPACK)
		{
			NGUITools.SetActive(this.AppraiseBtnObj, true);
			this.AppraisePrice = equipDataById.GetAppraisePrice(this.CurItem.GetItemQuality());
			if (this.AppraisePrice != 0)
			{
				this.AppraisePriceLabel.text = GameMoneyHelper.GetMoneyValStr(this.AppraisePrice, GameDefine.MONEY_TYPE.GOLD);
			}
			else
			{
				this.AppraisePriceLabel.text = string.Empty;
			}
			NGUITools.SetActive(this.recycleBtn, true);
		}
		else
		{
			NGUITools.SetActive(this.AppraiseBtnObj, false);
			NGUITools.SetActive(this.recycleBtn, false);
		}
	}

	// Token: 0x060040CD RID: 16589 RVA: 0x00132ACC File Offset: 0x00130CCC
	public void ShowBaseAtt(GameItem mCurItem)
	{
		EquipData equipDataById = DataManager.GetEquipDataById(mCurItem.ItemId);
		int num = equipDataById.GetBaseAttCount() - this.BaseList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.BaseList[0].gameObject) as GameObject;
				InfoLineItemLogic component = gameObject.GetComponent<InfoLineItemLogic>();
				gameObject.name = string.Format("ItemInfoNameLabel{0:D2}", this.BaseList.Count);
				gameObject.transform.parent = this.BaseAttributeGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				this.BaseList.Add(component);
			}
		}
		for (int j = 0; j < this.BaseList.Count; j++)
		{
			if (j < equipDataById.GetBaseAttCount())
			{
				UnityVersionUtil.SetActiveRecursive(this.BaseList[j].gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.BaseList[j].gameObject, false);
			}
		}
		bool flag = true;
		if (mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
		{
			flag = false;
		}
		else if (mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			flag = true;
		}
		int[] array = new int[]
		{
			-1,
			-1,
			-1,
			-1
		};
		if (equipDataById.BaseStatusType != (ATTRIBUTE_TYPE)0)
		{
			string attributeIcon = GameDefine.GetAttributeIcon((int)equipDataById.BaseStatusType);
			string attributeName_S = GameDefine.GetAttributeName_S((int)equipDataById.BaseStatusType);
			array[0] = equipDataById.GetAttrValueByQuality(0, (int)mCurItem.GetItemQuality());
			string attributeValueStr = GameDefine.GetAttributeValueStr((int)equipDataById.BaseStatusType, array[0]);
			string enhancestr = string.Empty;
			if (flag)
			{
				enhancestr = string.Format("({0} +{1})", StrDictionary.GetDictionaryString("#{100934}", new object[0]), equipDataById.GetAttrEnhanceVal(0, (int)mCurItem.GetItemQuality(), mCurItem.ItemLevel));
			}
			this.BaseList[0].ResetBase(attributeIcon, attributeName_S, attributeValueStr, enhancestr);
		}
		if (equipDataById.Status1 != 0)
		{
			string attributeIcon2 = GameDefine.GetAttributeIcon(equipDataById.Status1);
			string attributeName_S2 = GameDefine.GetAttributeName_S(equipDataById.Status1);
			array[1] = equipDataById.GetAttrValueByQuality(1, (int)mCurItem.GetItemQuality());
			string attributeValueStr2 = GameDefine.GetAttributeValueStr(equipDataById.Status1, array[1]);
			string enhancestr2 = string.Empty;
			if (flag)
			{
				enhancestr2 = string.Format("({0} +{1})", StrDictionary.GetDictionaryString("#{100934}", new object[0]), equipDataById.GetAttrEnhanceVal(1, (int)mCurItem.GetItemQuality(), mCurItem.ItemLevel));
			}
			this.BaseList[1].ResetBase(attributeIcon2, attributeName_S2, attributeValueStr2, enhancestr2);
		}
		if (equipDataById.Status2 != 0)
		{
			string attributeIcon3 = GameDefine.GetAttributeIcon(equipDataById.Status2);
			string attributeName_S3 = GameDefine.GetAttributeName_S(equipDataById.Status2);
			array[2] = equipDataById.GetAttrValueByQuality(2, (int)mCurItem.GetItemQuality());
			string attributeValueStr3 = GameDefine.GetAttributeValueStr(equipDataById.Status2, array[2]);
			string enhancestr3 = string.Empty;
			if (flag)
			{
				enhancestr3 = string.Format("({0} +{1})", StrDictionary.GetDictionaryString("#{100934}", new object[0]), equipDataById.GetAttrEnhanceVal(2, (int)mCurItem.GetItemQuality(), mCurItem.ItemLevel));
			}
			this.BaseList[2].ResetBase(attributeIcon3, attributeName_S3, attributeValueStr3, enhancestr3);
		}
		if (equipDataById.ExStatus != 0)
		{
			string attributeIcon4 = GameDefine.GetAttributeIcon(equipDataById.ExStatus);
			string attributeName_S4 = GameDefine.GetAttributeName_S(equipDataById.ExStatus);
			array[3] = equipDataById.GetAttrValueByQuality(3, (int)mCurItem.GetItemQuality());
			string attributeValueStr4 = GameDefine.GetAttributeValueStr(equipDataById.ExStatus, array[3]);
			string enhancestr4 = string.Empty;
			if (flag)
			{
				enhancestr4 = string.Format("({0} +{1})", StrDictionary.GetDictionaryString("#{100934}", new object[0]), equipDataById.GetAttrEnhanceVal(3, (int)mCurItem.GetItemQuality(), mCurItem.ItemLevel));
			}
			this.BaseList[3].ResetBase(attributeIcon4, attributeName_S4, attributeValueStr4, enhancestr4);
		}
		this.BaseAttributeGrid.Reposition();
	}

	// Token: 0x060040CE RID: 16590 RVA: 0x00132ED4 File Offset: 0x001310D4
	public void OnClickRecycleBtn()
	{
		int num = (int)((float)(this.CurItem.ItemData.GetSellPrice(this.CurItem.GetItemQuality()) * this.CurItem.StackNum) * 1f);
		MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{100636}", new object[]
		{
			num
		}), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
		{
			if (this.CurItem.ItemData.Type != GameDefine.ITEM_TYPE.BADGE)
			{
				sell_item.request request = new sell_item.request();
				request.indexId = this.CurItem.IndexId;
				request.itemCount = (long)this.CurItem.StackNum;
				request.type = (long)this.CurItem.ContainerType;
				NetLogic.GetInstance().Send<Protocol.sell_item>(request, null);
			}
		}, null, null, null);
		SingletonUnity<ItemInfoRootLogicNew>.Instance.OnClickCloseBtn();
	}

	// Token: 0x060040CF RID: 16591 RVA: 0x00132F58 File Offset: 0x00131158
	public void OnClickAppraiseBtn()
	{
		EquipData equipDataById = DataManager.GetEquipDataById(this.CurItem.ItemId);
		if (GameMoneyHelper.BeforeCheckBuy(GameDefine.MONEY_TYPE.GOLD, this.AppraisePrice))
		{
			equip_appraise.request request = new equip_appraise.request();
			request.index = this.CurItem.IndexId;
			NetLogic.GetInstance().Send<Protocol.equip_appraise>(request, null);
		}
		SingletonUnity<ItemInfoRootLogicNew>.Instance.OnClickCloseBtn();
	}

	// Token: 0x060040D0 RID: 16592 RVA: 0x00132FB4 File Offset: 0x001311B4
	public void SetLabelWarning(UILabel curlabel, bool istrue)
	{
		if (istrue)
		{
			curlabel.color = Color.red;
		}
		else
		{
			curlabel.color = Color.white;
		}
	}

	// Token: 0x04002C65 RID: 11365
	private GameItem CurItem;

	// Token: 0x04002C66 RID: 11366
	public UISprite ItemIcon;

	// Token: 0x04002C67 RID: 11367
	public UISprite ItemQualityIcon;

	// Token: 0x04002C68 RID: 11368
	public UILabel ItemNameLabel;

	// Token: 0x04002C69 RID: 11369
	public UILabel LevelLabel;

	// Token: 0x04002C6A RID: 11370
	public UILabel LevelNameLabel;

	// Token: 0x04002C6B RID: 11371
	public UILabel ProfessionNameLabel;

	// Token: 0x04002C6C RID: 11372
	public UILabel ProfessionLabel;

	// Token: 0x04002C6D RID: 11373
	public UILabel DescLabel;

	// Token: 0x04002C6E RID: 11374
	public UILabel AppraisePriceLabel;

	// Token: 0x04002C6F RID: 11375
	public UILabel RecycleLabel;

	// Token: 0x04002C70 RID: 11376
	public GameObject AppraiseBtnObj;

	// Token: 0x04002C71 RID: 11377
	public GameObject recycleBtn;

	// Token: 0x04002C72 RID: 11378
	public GameObject BaseRoot;

	// Token: 0x04002C73 RID: 11379
	public UIGrid BaseAttributeGrid;

	// Token: 0x04002C74 RID: 11380
	public List<InfoLineItemLogic> BaseList;

	// Token: 0x04002C75 RID: 11381
	private int AppraisePrice;
}
