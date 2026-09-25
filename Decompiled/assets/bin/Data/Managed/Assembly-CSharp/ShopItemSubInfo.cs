using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A20 RID: 2592
public class ShopItemSubInfo : MonoBehaviour
{
	// Token: 0x06004AC4 RID: 19140 RVA: 0x0018A160 File Offset: 0x00188360
	public void UpdateSelectItem(shop_item curSelectItem)
	{
		ItemData itemDataByID = DataManager.GetItemDataByID(curSelectItem.ItemID);
		int num = (int)curSelectItem.Quality;
		if (itemDataByID != null)
		{
			this.IconSprite.spriteName = itemDataByID.BackPackIcon;
			this.ItemNameLabel.text = itemDataByID.MName;
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				this.QualitySprite.spriteName = ((EQUIP_QUALITY)curSelectItem.Quality).ToString();
				this.ItemNameLabel.color = GameDefine.GetColorByQuality((EQUIP_QUALITY)curSelectItem.Quality);
			}
			else
			{
				this.QualitySprite.spriteName = itemDataByID.QualityType.ToString();
				this.ItemNameLabel.color = GameDefine.GetColorByQuality(itemDataByID.QualityType);
			}
			if (itemDataByID.UseHour > 0)
			{
				NGUITools.SetActive(this.TimeLimitObj, true);
				this.TimeLimitLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100643}", new object[0]), TimeTools.GetFormateTime((long)(itemDataByID.UseHour * 3600)));
			}
			else
			{
				NGUITools.SetActive(this.TimeLimitObj, false);
				this.TimeLimitLabel.text = string.Empty;
			}
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP || itemDataByID.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
			{
				this.PowerLabel.enabled = true;
				UnityVersionUtil.SetActiveRecursive(this.BaseRoot, true);
				UnityVersionUtil.SetActiveRecursive(this.ItemDesObj, false);
				UnityVersionUtil.SetActiveRecursive(this.RandomRoot, true);
				this.RandomLabel.text = itemDataByID.MDescription;
				string text = string.Format("[FFFF00] ({0})[-]", StrDictionary.GetDictionaryString("#{101213}", new object[0]));
				if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					text = string.Format("[FFFF00] ({0})[-]", StrDictionary.GetDictionaryString("#{101213}", new object[0]));
					num = (int)curSelectItem.Quality;
					GameItem gameItem = new GameItem(itemDataByID.ID, (EQUIP_QUALITY)num, 1);
					gameItem.ItemLevel = 80;
					this.PowerLabel.text = string.Format("[FFFF00]{0} +{1}[-]\nlv.80[FFFF00] ({2})[-]", StrDictionary.GetDictionaryString("#{100421}", new object[0]), gameItem.GetItemCombatVal(), StrDictionary.GetDictionaryString("#{101213}", new object[0]));
				}
				else if (itemDataByID.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
				{
					text = string.Empty;
					num = itemDataByID.Quality;
					GameItem gameItem2 = new GameItem(itemDataByID.ID, (EQUIP_QUALITY)num, 1);
					this.PowerLabel.text = string.Format("[FFFF00]{0} +{1}[-]", StrDictionary.GetDictionaryString("#{100421}", new object[0]), gameItem2.GetItemCombatVal());
				}
				EquipData equipDataById = DataManager.GetEquipDataById(itemDataByID.ID);
				int num2 = equipDataById.GetBaseAttCount() - this.BaseList.Count;
				if (num2 > 0)
				{
					for (int i = 0; i < num2; i++)
					{
						GameObject gameObject = Object.Instantiate(this.BaseList[0].gameObject) as GameObject;
						InfoLineItemLogic component = gameObject.GetComponent<InfoLineItemLogic>();
						gameObject.name = string.Format("{0:D2}", this.BaseList.Count);
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
					string name = GameDefine.GetAttributeName_S((int)equipDataById.BaseStatusType) + text;
					if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
					{
						array[0] = equipDataById.GetAttrValByQualityAndLevel(0, num, 80);
					}
					else
					{
						array[0] = equipDataById.GetAttrValByQualityAndLevel(0, num, 0);
					}
					string attributeValueStr = GameDefine.GetAttributeValueStr((int)equipDataById.BaseStatusType, array[0]);
					string empty = string.Empty;
					this.BaseList[0].ResetBase(attributeIcon, name, attributeValueStr, empty);
				}
				if (equipDataById.Status1 != 0)
				{
					string attributeIcon2 = GameDefine.GetAttributeIcon(equipDataById.Status1);
					string name2 = GameDefine.GetAttributeName_S(equipDataById.Status1) + text;
					if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
					{
						array[1] = equipDataById.GetAttrValByQualityAndLevel(1, num, 80);
					}
					else
					{
						array[1] = equipDataById.GetAttrValByQualityAndLevel(1, num, 0);
					}
					string attributeValueStr2 = GameDefine.GetAttributeValueStr(equipDataById.Status1, array[1]);
					string empty2 = string.Empty;
					this.BaseList[1].ResetBase(attributeIcon2, name2, attributeValueStr2, empty2);
				}
				if (equipDataById.Status2 != 0)
				{
					string attributeIcon3 = GameDefine.GetAttributeIcon(equipDataById.Status2);
					string name3 = GameDefine.GetAttributeName_S(equipDataById.Status2) + text;
					if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
					{
						array[2] = equipDataById.GetAttrValByQualityAndLevel(2, num, 80);
					}
					else
					{
						array[2] = equipDataById.GetAttrValByQualityAndLevel(2, num, 0);
					}
					string attributeValueStr3 = GameDefine.GetAttributeValueStr(equipDataById.Status2, array[2]);
					string empty3 = string.Empty;
					this.BaseList[2].ResetBase(attributeIcon3, name3, attributeValueStr3, empty3);
				}
				if (equipDataById.ExStatus != 0)
				{
					string attributeIcon4 = GameDefine.GetAttributeIcon(equipDataById.ExStatus);
					string name4 = GameDefine.GetAttributeName_S(equipDataById.ExStatus) + text;
					if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
					{
						array[3] = equipDataById.GetAttrValByQualityAndLevel(3, num, 80);
					}
					else
					{
						array[3] = equipDataById.GetAttrValByQualityAndLevel(3, num, 0);
					}
					string attributeValueStr4 = GameDefine.GetAttributeValueStr(equipDataById.ExStatus, array[3]);
					string empty4 = string.Empty;
					this.BaseList[3].ResetBase(attributeIcon4, name4, attributeValueStr4, empty4);
				}
				this.BaseAttributeGrid.Reposition();
			}
			else if (itemDataByID.Type == GameDefine.ITEM_TYPE.BADGE)
			{
				this.PowerLabel.enabled = false;
				UnityVersionUtil.SetActiveRecursive(this.BaseRoot, true);
				UnityVersionUtil.SetActiveRecursive(this.ItemDesObj, false);
				UnityVersionUtil.SetActiveRecursive(this.RandomRoot, false);
				BadgeData badgeDataById = DataManager.GetBadgeDataById(itemDataByID.ID);
				int num3 = badgeDataById.GetBaseAttCount() - this.BaseList.Count;
				if (num3 > 0)
				{
					for (int k = 0; k < num3; k++)
					{
						GameObject gameObject2 = Object.Instantiate(this.BaseList[0].gameObject) as GameObject;
						InfoLineItemLogic component2 = gameObject2.GetComponent<InfoLineItemLogic>();
						gameObject2.name = string.Format("{0:D2}", this.BaseList.Count);
						gameObject2.transform.parent = this.BaseAttributeGrid.transform;
						gameObject2.transform.localScale = Vector3.one;
						gameObject2.transform.localPosition = Vector3.zero;
						this.BaseList.Add(component2);
					}
				}
				for (int l = 0; l < this.BaseList.Count; l++)
				{
					if (l < badgeDataById.GetBaseAttCount())
					{
						UnityVersionUtil.SetActiveRecursive(this.BaseList[l].gameObject, true);
					}
					else
					{
						UnityVersionUtil.SetActiveRecursive(this.BaseList[l].gameObject, false);
					}
				}
				if (badgeDataById.Status1 != -1)
				{
					string attributeName_S = GameDefine.GetAttributeName_S(badgeDataById.Status1);
					string attributeValueStr5 = GameDefine.GetAttributeValueStr(badgeDataById.Status1, badgeDataById.Value1);
					string attributeIcon5 = GameDefine.GetAttributeIcon(badgeDataById.Status1);
					this.BaseList[0].ResetBase(attributeIcon5, attributeName_S, attributeValueStr5, string.Empty);
				}
				if (badgeDataById.Status2 != -1)
				{
					string attributeName_S2 = GameDefine.GetAttributeName_S(badgeDataById.Status2);
					string attributeValueStr6 = GameDefine.GetAttributeValueStr(badgeDataById.Status2, badgeDataById.Value2);
					string attributeIcon6 = GameDefine.GetAttributeIcon(badgeDataById.Status2);
					this.BaseList[0].ResetBase(attributeIcon6, attributeName_S2, attributeValueStr6, string.Empty);
				}
			}
			else if (itemDataByID.Type == GameDefine.ITEM_TYPE.EXCHANGE)
			{
				UnityVersionUtil.SetActiveRecursive(this.BaseRoot, false);
				UnityVersionUtil.SetActiveRecursive(this.ItemDesObj, true);
				UnityVersionUtil.SetActiveRecursive(this.RandomRoot, false);
				this.ItemDesLabel.text = itemDataByID.MDescription;
				this.PowerLabel.enabled = false;
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.BaseRoot, false);
				UnityVersionUtil.SetActiveRecursive(this.ItemDesObj, true);
				UnityVersionUtil.SetActiveRecursive(this.RandomRoot, false);
				this.ItemDesLabel.text = itemDataByID.MDescription;
				this.PowerLabel.enabled = false;
			}
		}
	}

	// Token: 0x04003865 RID: 14437
	public UISprite IconSprite;

	// Token: 0x04003866 RID: 14438
	public UISprite QualitySprite;

	// Token: 0x04003867 RID: 14439
	public GameObject TimeLimitObj;

	// Token: 0x04003868 RID: 14440
	public UILabel TimeLimitLabel;

	// Token: 0x04003869 RID: 14441
	public UILabel RemainLabel;

	// Token: 0x0400386A RID: 14442
	public UILabel ItemNameLabel;

	// Token: 0x0400386B RID: 14443
	public UILabel PowerLabel;

	// Token: 0x0400386C RID: 14444
	public GameObject ItemDesObj;

	// Token: 0x0400386D RID: 14445
	public UILabel ItemDesLabel;

	// Token: 0x0400386E RID: 14446
	public GameObject BaseRoot;

	// Token: 0x0400386F RID: 14447
	public UIGrid BaseAttributeGrid;

	// Token: 0x04003870 RID: 14448
	public List<InfoLineItemLogic> BaseList;

	// Token: 0x04003871 RID: 14449
	public GameObject RandomRoot;

	// Token: 0x04003872 RID: 14450
	public UILabel RandomLabel;
}
