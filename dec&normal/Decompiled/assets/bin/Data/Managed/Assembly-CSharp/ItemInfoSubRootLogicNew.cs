using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000948 RID: 2376
public class ItemInfoSubRootLogicNew : MonoBehaviour
{
	// Token: 0x06004276 RID: 17014 RVA: 0x00143788 File Offset: 0x00141988
	public void ShowBaseAtt(GameItem mCurItem, bool isEquiped)
	{
		NGUITools.SetActive(this.BaseRoot, true);
		if (this.BadgeRoot != null)
		{
			NGUITools.SetActive(this.BadgeRoot, false);
		}
		if (this.TitleLabel != null)
		{
			NGUITools.SetActive(this.TitleLabel.gameObject, false);
		}
		if (this.ItemLevelLabel != null)
		{
			if (isEquiped && mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				this.ItemLevelLabel.text = string.Format("+{0}", mCurItem.ItemLevel);
			}
			else
			{
				this.ItemLevelLabel.text = string.Empty;
			}
		}
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

	// Token: 0x06004277 RID: 17015 RVA: 0x00143C38 File Offset: 0x00141E38
	public void ShowOtherEquipInfo(GameItem mCurItem)
	{
		if (mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			NGUITools.SetActive(this.StarObj, true);
			this.ShowStar(mCurItem.GetStarByScore());
			if (mCurItem.ItemData.SubType == 0)
			{
				NGUITools.SetActive(this.SkillRoot, true);
				NGUITools.SetActive(this.AttRoot, false);
				if (mCurItem.IsHaveRandomAtt)
				{
					int num = mCurItem.Random_AttriDic.Count - this.SkillList.Count;
					if (num > 0)
					{
						for (int i = 0; i < num; i++)
						{
							GameObject gameObject = Object.Instantiate(this.SkillList[0].gameObject) as GameObject;
							InfoLineItemLogic component = gameObject.GetComponent<InfoLineItemLogic>();
							gameObject.name = string.Format("ItemInfoNameLabel{0:D2}", this.SkillList.Count);
							gameObject.transform.parent = this.SkillGrid.transform;
							gameObject.transform.localScale = Vector3.one;
							gameObject.transform.localPosition = Vector3.zero;
							this.SkillList.Add(component);
						}
					}
					List<random_attri> list = new List<random_attri>(mCurItem.Random_AttriDic.Values);
					for (int j = list.Count - 1; j >= 0; j--)
					{
						if (string.IsNullOrEmpty(list[j].skillId))
						{
							list.RemoveAt(j);
						}
					}
					list.Sort((random_attri x, random_attri y) => (int)x.index - (int)y.index);
					for (int k = 0; k < this.SkillList.Count; k++)
					{
						if (k < list.Count)
						{
							NGUITools.SetActive(this.SkillList[k].gameObject, true);
							this.SkillList[k].ResetSkill(list[k], mCurItem);
						}
						else
						{
							NGUITools.SetActive(this.SkillList[k].gameObject, false);
						}
					}
					this.SkillGrid.Reposition();
				}
				else
				{
					for (int l = 0; l < this.SkillList.Count; l++)
					{
						NGUITools.SetActive(this.SkillList[l].gameObject, false);
					}
				}
			}
			else
			{
				NGUITools.SetActive(this.SkillRoot, false);
				NGUITools.SetActive(this.AttRoot, true);
				if (mCurItem.IsHaveRandomAtt)
				{
					int num2 = mCurItem.Random_AttriDic.Count - this.RandomList.Count;
					if (num2 > 0)
					{
						for (int m = 0; m < num2; m++)
						{
							GameObject gameObject2 = Object.Instantiate(this.RandomList[0].gameObject) as GameObject;
							InfoLineItemLogic component2 = gameObject2.GetComponent<InfoLineItemLogic>();
							gameObject2.name = string.Format("ItemInfoNameLabel{0:D2}", this.RandomList.Count);
							gameObject2.transform.parent = this.RandomAttGrid.transform;
							gameObject2.transform.localScale = Vector3.one;
							gameObject2.transform.localPosition = Vector3.zero;
							this.RandomList.Add(component2);
						}
					}
					List<random_attri> list2 = new List<random_attri>(mCurItem.Random_AttriDic.Values);
					for (int n = list2.Count - 1; n >= 0; n--)
					{
						if (list2[n].id == 0L)
						{
							list2.RemoveAt(n);
						}
					}
					list2.Sort((random_attri x, random_attri y) => (int)x.index - (int)y.index);
					for (int num3 = 0; num3 < this.RandomList.Count; num3++)
					{
						if (num3 < list2.Count)
						{
							NGUITools.SetActive(this.RandomList[num3].gameObject, true);
							this.RandomList[num3].ResetAtt(list2[num3], mCurItem);
						}
						else
						{
							NGUITools.SetActive(this.RandomList[num3].gameObject, false);
						}
					}
					this.RandomAttGrid.Reposition();
				}
				else
				{
					for (int num4 = 0; num4 < this.RandomList.Count; num4++)
					{
						NGUITools.SetActive(this.RandomList[num4].gameObject, false);
					}
				}
				if (mCurItem.IsHaveInlay)
				{
					NGUITools.SetActive(this.InlayObj.gameObject, true);
					int num5 = mCurItem.InlayDic.Count - this.InlayList.Count;
					if (num5 > 0)
					{
						for (int num6 = 0; num6 < num5; num6++)
						{
							GameObject gameObject3 = Object.Instantiate(this.InlayList[0].gameObject) as GameObject;
							InfoLineItemLogic component3 = gameObject3.GetComponent<InfoLineItemLogic>();
							gameObject3.name = string.Format("ItemInfoNameLabel{0:D2}", this.InlayList.Count);
							gameObject3.transform.parent = this.InlayGrid.transform;
							gameObject3.transform.localScale = Vector3.one;
							gameObject3.transform.localPosition = Vector3.zero;
							this.InlayList.Add(component3);
						}
					}
					List<inlay> list3 = new List<inlay>(mCurItem.InlayDic.Values);
					list3.Sort((inlay x, inlay y) => (int)x.index - (int)y.index);
					for (int num7 = 0; num7 < this.InlayList.Count; num7++)
					{
						if (num7 < list3.Count)
						{
							NGUITools.SetActive(this.InlayList[num7].gameObject, true);
							this.InlayList[num7].ResetInlay(list3[num7]);
						}
						else
						{
							NGUITools.SetActive(this.InlayList[num7].gameObject, false);
						}
					}
					this.InlayGrid.Reposition();
				}
				else
				{
					NGUITools.SetActive(this.InlayObj.gameObject, false);
				}
			}
			return;
		}
		NGUITools.SetActive(this.StarObj, false);
		NGUITools.SetActive(this.SkillRoot, false);
		NGUITools.SetActive(this.AttRoot, false);
	}

	// Token: 0x06004278 RID: 17016 RVA: 0x00144298 File Offset: 0x00142498
	public void ShowBadgeInfo(GameItem mCurItem)
	{
		NGUITools.SetActive(this.BadgeRoot, true);
		NGUITools.SetActive(this.StarObj, false);
		NGUITools.SetActive(this.SkillRoot, false);
		NGUITools.SetActive(this.AttRoot, false);
		NGUITools.SetActive(this.BaseRoot, false);
		if (this.TitleLabel != null)
		{
			NGUITools.SetActive(this.TitleLabel.gameObject, false);
		}
		BadgeData badgeDataById = DataManager.GetBadgeDataById(mCurItem.ItemId);
		for (int i = 0; i < this.LeftAttrLabelList.Length; i++)
		{
			NGUITools.SetActive(this.LeftAttrLabelList[i].gameObject, false);
		}
		if (badgeDataById.Status1 != -1)
		{
			NGUITools.SetActive(this.LeftAttrLabelList[0].gameObject, true);
			this.LeftAttrLabelList[0].text = GameDefine.GetAttributeName_S(badgeDataById.Status1);
			this.RightAttrLabelList[0].text = GameDefine.GetAttributeValueStr(badgeDataById.Status1, badgeDataById.Value1);
			this.LefeAttrIconList[0].spriteName = GameDefine.GetAttributeIcon(badgeDataById.Status1);
		}
		if (badgeDataById.Status2 != -1)
		{
			NGUITools.SetActive(this.LeftAttrLabelList[1].gameObject, true);
			this.LeftAttrLabelList[1].text = GameDefine.GetAttributeName_S(badgeDataById.Status2);
			this.RightAttrLabelList[1].text = GameDefine.GetAttributeValueStr(badgeDataById.Status2, badgeDataById.Value2);
			this.LefeAttrIconList[1].spriteName = GameDefine.GetAttributeIcon(badgeDataById.Status2);
		}
	}

	// Token: 0x06004279 RID: 17017 RVA: 0x00144418 File Offset: 0x00142618
	public void ShowDesInfo(GameItem mCurItem)
	{
		if (this.BadgeRoot != null)
		{
			NGUITools.SetActive(this.BadgeRoot, false);
		}
		NGUITools.SetActive(this.StarObj, false);
		NGUITools.SetActive(this.SkillRoot, false);
		NGUITools.SetActive(this.AttRoot, false);
		NGUITools.SetActive(this.BaseRoot, false);
		if (this.TitleLabel != null)
		{
			NGUITools.SetActive(this.TitleLabel.gameObject, true);
			this.TitleLabel.text = StrDictionary.GetDictionaryString("#{100611}", new object[0]);
			this.DescLabel.text = mCurItem.ItemData.MDescription;
		}
	}

	// Token: 0x0600427A RID: 17018 RVA: 0x001444C8 File Offset: 0x001426C8
	public void ShowStar(int star)
	{
		for (int i = 0; i < this.StarList.Count; i++)
		{
			if (i < star)
			{
				this.StarList[i].color = Color.white;
			}
			else
			{
				this.StarList[i].color = Color.black;
			}
		}
	}

	// Token: 0x0600427B RID: 17019 RVA: 0x0014452C File Offset: 0x0014272C
	public void ClearItemLevel()
	{
		if (this.ItemLevelLabel != null)
		{
			this.ItemLevelLabel.text = string.Empty;
		}
	}

	// Token: 0x04002E61 RID: 11873
	public UISprite ItemIcon;

	// Token: 0x04002E62 RID: 11874
	public UISprite ItemQualityIcon;

	// Token: 0x04002E63 RID: 11875
	public UILabel ItemNameLabel;

	// Token: 0x04002E64 RID: 11876
	public UILabel ItemEnhanceLabel;

	// Token: 0x04002E65 RID: 11877
	public UILabel LevelLabel;

	// Token: 0x04002E66 RID: 11878
	public UILabel LevelNameLabel;

	// Token: 0x04002E67 RID: 11879
	public UILabel ProfessionNameLabel;

	// Token: 0x04002E68 RID: 11880
	public UILabel ProfessionLabel;

	// Token: 0x04002E69 RID: 11881
	public UILabel PriceLabel;

	// Token: 0x04002E6A RID: 11882
	public UILabel ItemLevelLabel;

	// Token: 0x04002E6B RID: 11883
	public UISprite IsEquipedSprite;

	// Token: 0x04002E6C RID: 11884
	public GameObject StarObj;

	// Token: 0x04002E6D RID: 11885
	public List<UISprite> StarList;

	// Token: 0x04002E6E RID: 11886
	public GameObject AttRoot;

	// Token: 0x04002E6F RID: 11887
	public GameObject BaseRoot;

	// Token: 0x04002E70 RID: 11888
	public UIGrid BaseAttributeGrid;

	// Token: 0x04002E71 RID: 11889
	public List<InfoLineItemLogic> BaseList;

	// Token: 0x04002E72 RID: 11890
	public UIGrid RandomAttGrid;

	// Token: 0x04002E73 RID: 11891
	public List<InfoLineItemLogic> RandomList;

	// Token: 0x04002E74 RID: 11892
	public GameObject InlayObj;

	// Token: 0x04002E75 RID: 11893
	public UIGrid InlayGrid;

	// Token: 0x04002E76 RID: 11894
	public List<InfoLineItemLogic> InlayList;

	// Token: 0x04002E77 RID: 11895
	public GameObject SkillRoot;

	// Token: 0x04002E78 RID: 11896
	public UIGrid SkillGrid;

	// Token: 0x04002E79 RID: 11897
	public List<InfoLineItemLogic> SkillList;

	// Token: 0x04002E7A RID: 11898
	public GameObject[] JumpBtnRoot;

	// Token: 0x04002E7B RID: 11899
	public UILabel[] JumpBtnLabel;

	// Token: 0x04002E7C RID: 11900
	public UIGrid JumpBtnRootGrid;

	// Token: 0x04002E7D RID: 11901
	public GameObject BadgeRoot;

	// Token: 0x04002E7E RID: 11902
	public UILabel TitleLabel;

	// Token: 0x04002E7F RID: 11903
	public UILabel DescLabel;

	// Token: 0x04002E80 RID: 11904
	public UILabel[] LeftAttrLabelList;

	// Token: 0x04002E81 RID: 11905
	public UILabel[] RightAttrLabelList;

	// Token: 0x04002E82 RID: 11906
	public UISprite[] LefeAttrIconList;

	// Token: 0x04002E83 RID: 11907
	public GameObject PowerUpArrow;

	// Token: 0x04002E84 RID: 11908
	public GameObject PowerDownArrow;
}
