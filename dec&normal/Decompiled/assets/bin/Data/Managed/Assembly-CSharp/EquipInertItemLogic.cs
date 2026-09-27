using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000938 RID: 2360
public class EquipInertItemLogic : MonoBehaviour
{
	// Token: 0x17000F9F RID: 3999
	// (get) Token: 0x0600419F RID: 16799 RVA: 0x00138908 File Offset: 0x00136B08
	public string GetSelectKey
	{
		get
		{
			return this.CurSelectkey;
		}
	}

	// Token: 0x060041A0 RID: 16800 RVA: 0x00138910 File Offset: 0x00136B10
	public void Reset(GameItem mCurItem, bool isEquiped, DelegateDefine.OneIntParamDelegate updatefun = null, EquipInertItemLogic.AutoSelectDelegate selectfun = null)
	{
		this.UpdatePriceFun = updatefun;
		this.AutoSelectFun = selectfun;
		this.IsEquipped = isEquiped;
		ItemData itemData = mCurItem.ItemData;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.CurSelectIndex = -1;
		this.CurSelectkey = string.Empty;
		this.ItemNameLabel.text = itemData.MName;
		this.ItemIcon.spriteName = itemData.BackPackIcon;
		this.LevelLabel.text = itemData.Level.ToString();
		this.SetLabelWarning(this.LevelLabel, playerData.Level < itemData.Level);
		this.ItemQualityIcon.spriteName = mCurItem.GetItemQuality().ToString();
		int itemCombatVal = mCurItem.GetItemCombatVal();
		this.ItemEnhanceLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100664}", new object[0]), itemCombatVal);
		this.ItemNameLabel.color = GameDefine.GetColorByQuality(mCurItem.GetItemQuality());
		EquipData equipDataById = DataManager.GetEquipDataById(mCurItem.ItemId);
		if (mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP && mCurItem.ItemData.SubType == 0)
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
		if (this.IsEquipped)
		{
			this.IsEquipedSprite.alpha = 1f;
		}
		else
		{
			this.IsEquipedSprite.alpha = 0f;
		}
		if (mCurItem.ItemData.SubType == 0)
		{
			this.IsWeaponFlag = true;
			this.ShowSkillInfo(mCurItem, this.IsEquipped);
		}
		else
		{
			this.IsWeaponFlag = false;
			this.ShowAttInfo(mCurItem, this.IsEquipped);
		}
		this.ShowStar(mCurItem.GetStarByScore());
	}

	// Token: 0x060041A1 RID: 16801 RVA: 0x00138B5C File Offset: 0x00136D5C
	public void ShowAttInfo(GameItem mCurItem, bool isEquiped)
	{
		NGUITools.SetActive(this.AttParentGrid.gameObject, true);
		NGUITools.SetActive(this.SkillParentGrid.gameObject, false);
		if (isEquiped)
		{
			int num = 6 - this.SelectItemList.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = Object.Instantiate(this.SelectItemList[0].gameObject) as GameObject;
					InfoLineSelectItemLogic component = gameObject.GetComponent<InfoLineSelectItemLogic>();
					gameObject.name = string.Format("ItemInfoNameLabel{0:D2}", this.SelectItemList.Count);
					gameObject.transform.parent = this.AttParentGrid.transform;
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.localPosition = Vector3.zero;
					this.SelectItemList.Add(component);
				}
			}
			for (int j = 0; j < this.SelectItemList.Count; j++)
			{
				if (j < 6)
				{
					NGUITools.SetActive(this.SelectItemList[j].gameObject, true);
				}
				else
				{
					NGUITools.SetActive(this.SelectItemList[j].gameObject, false);
				}
			}
			for (int k = 0; k < this.SelectItemList.Count; k++)
			{
				this.SelectItemList[k].ResetAtt(this.GetindexAtt(mCurItem, k), k, mCurItem, new DelegateDefine.ThreeParamDelegate(this.OnClickSelectBtn));
			}
			this.AttParentGrid.Reposition();
		}
		else if (mCurItem.IsHaveRandomAtt)
		{
			int num2 = mCurItem.Random_AttriDic.Count - this.SelectItemList.Count;
			if (num2 > 0)
			{
				for (int l = 0; l < num2; l++)
				{
					GameObject gameObject2 = Object.Instantiate(this.SelectItemList[0].gameObject) as GameObject;
					InfoLineSelectItemLogic component2 = gameObject2.GetComponent<InfoLineSelectItemLogic>();
					gameObject2.name = string.Format("ItemInfoNameLabel{0:D2}", this.SelectItemList.Count);
					gameObject2.transform.parent = this.AttParentGrid.transform;
					gameObject2.transform.localScale = Vector3.one;
					gameObject2.transform.localPosition = Vector3.zero;
					this.SelectItemList.Add(component2);
				}
			}
			List<random_attri> list = new List<random_attri>(mCurItem.Random_AttriDic.Values);
			for (int m = list.Count - 1; m >= 0; m--)
			{
				if (list[m].id == 0L)
				{
					list.RemoveAt(m);
				}
			}
			list.Sort((random_attri x, random_attri y) => (int)x.index - (int)y.index);
			for (int n = 0; n < this.SelectItemList.Count; n++)
			{
				if (n < list.Count)
				{
					NGUITools.SetActive(this.SelectItemList[n].gameObject, true);
					this.SelectItemList[n].ResetAtt(list[n], (int)list[n].index - 1, mCurItem, new DelegateDefine.ThreeParamDelegate(this.OnClickSelectBtn));
				}
				else
				{
					NGUITools.SetActive(this.SelectItemList[n].gameObject, false);
				}
			}
			this.AttParentGrid.Reposition();
		}
		else
		{
			for (int num3 = 0; num3 < this.SelectItemList.Count; num3++)
			{
				NGUITools.SetActive(this.SelectItemList[num3].gameObject, false);
			}
		}
	}

	// Token: 0x060041A2 RID: 16802 RVA: 0x00138F18 File Offset: 0x00137118
	public void ShowSkillInfo(GameItem mCurItem, bool isEquiped)
	{
		NGUITools.SetActive(this.AttParentGrid.gameObject, false);
		NGUITools.SetActive(this.SkillParentGrid.gameObject, true);
		if (isEquiped)
		{
			int num = 6 - this.SelectSkillItemList.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = Object.Instantiate(this.SelectSkillItemList[0].gameObject) as GameObject;
					InfoLineSelectItemLogic component = gameObject.GetComponent<InfoLineSelectItemLogic>();
					gameObject.name = string.Format("ItemInfoNameLabel{0:D2}", this.SelectSkillItemList.Count);
					gameObject.transform.parent = this.SkillParentGrid.transform;
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.localPosition = Vector3.zero;
					this.SelectSkillItemList.Add(component);
				}
			}
			for (int j = 0; j < this.SelectSkillItemList.Count; j++)
			{
				if (j < 6)
				{
					NGUITools.SetActive(this.SelectSkillItemList[j].gameObject, true);
				}
				else
				{
					NGUITools.SetActive(this.SelectSkillItemList[j].gameObject, false);
				}
			}
			for (int k = 0; k < this.SelectSkillItemList.Count; k++)
			{
				this.SelectSkillItemList[k].ResetSkill(this.GetindexAtt(mCurItem, k), k, mCurItem, new DelegateDefine.ThreeParamDelegate(this.OnClickSelectBtn));
			}
			this.SkillParentGrid.Reposition();
		}
		else if (mCurItem.IsHaveRandomAtt)
		{
			int num2 = mCurItem.Random_AttriDic.Count - this.SelectSkillItemList.Count;
			if (num2 > 0)
			{
				for (int l = 0; l < num2; l++)
				{
					GameObject gameObject2 = Object.Instantiate(this.SelectSkillItemList[0].gameObject) as GameObject;
					InfoLineSelectItemLogic component2 = gameObject2.GetComponent<InfoLineSelectItemLogic>();
					gameObject2.name = string.Format("ItemInfoNameLabel{0:D2}", this.SelectSkillItemList.Count);
					gameObject2.transform.parent = this.SkillParentGrid.transform;
					gameObject2.transform.localScale = Vector3.one;
					gameObject2.transform.localPosition = Vector3.zero;
					this.SelectSkillItemList.Add(component2);
				}
			}
			List<random_attri> list = new List<random_attri>(mCurItem.Random_AttriDic.Values);
			for (int m = list.Count - 1; m >= 0; m--)
			{
				if (string.IsNullOrEmpty(list[m].skillId))
				{
					list.RemoveAt(m);
				}
			}
			list.Sort((random_attri x, random_attri y) => (int)x.index - (int)y.index);
			for (int n = 0; n < this.SelectSkillItemList.Count; n++)
			{
				if (n < list.Count)
				{
					NGUITools.SetActive(this.SelectSkillItemList[n].gameObject, true);
					this.SelectSkillItemList[n].ResetSkill(list[n], (int)list[n].index - 1, mCurItem, new DelegateDefine.ThreeParamDelegate(this.OnClickSelectBtn));
				}
				else
				{
					NGUITools.SetActive(this.SelectSkillItemList[n].gameObject, false);
				}
			}
			this.SkillParentGrid.Reposition();
		}
		else
		{
			for (int num3 = 0; num3 < this.SelectSkillItemList.Count; num3++)
			{
				NGUITools.SetActive(this.SelectSkillItemList[num3].gameObject, false);
			}
		}
	}

	// Token: 0x060041A3 RID: 16803 RVA: 0x001392DC File Offset: 0x001374DC
	public void OnClickSelectBtn(int index, int quality, string selectkey, bool isAutoSelect)
	{
		if (this.CurSelectIndex == index)
		{
			return;
		}
		this.CurSelectIndex = index;
		this.CurSelectkey = selectkey;
		this.UpdateSelectIndex();
		if (this.UpdatePriceFun != null)
		{
			this.UpdatePriceFun(quality);
		}
		if (isAutoSelect && this.AutoSelectFun != null)
		{
			this.AutoSelectFun(this.IsEquipped, selectkey);
		}
	}

	// Token: 0x060041A4 RID: 16804 RVA: 0x00139348 File Offset: 0x00137548
	public void ClearSelect()
	{
		this.CurSelectIndex = -1;
		this.CurSelectkey = string.Empty;
		this.UpdateSelectIndex();
	}

	// Token: 0x060041A5 RID: 16805 RVA: 0x00139364 File Offset: 0x00137564
	public void SelectNeedKey(string key, bool needselect = false)
	{
		if (this.IsWeaponFlag)
		{
			if (needselect)
			{
				if (this.IsHaveKey(key))
				{
					for (int i = 0; i < this.SelectSkillItemList.Count; i++)
					{
						if (this.SelectSkillItemList[i].AutoSelectKey(key))
						{
							return;
						}
					}
				}
				else if (this.CurSelectIndex == -1)
				{
					for (int j = 0; j < this.SelectSkillItemList.Count; j++)
					{
						if (this.SelectSkillItemList[j].SelectEmpty())
						{
							return;
						}
					}
				}
			}
			else
			{
				for (int k = 0; k < this.SelectSkillItemList.Count; k++)
				{
					if (this.SelectSkillItemList[k].AutoSelectKey(key))
					{
						return;
					}
				}
			}
		}
		else if (needselect)
		{
			if (this.IsHaveKey(key))
			{
				for (int l = 0; l < this.SelectItemList.Count; l++)
				{
					if (this.SelectItemList[l].AutoSelectKey(key))
					{
						return;
					}
				}
			}
			else if (this.CurSelectIndex == -1)
			{
				for (int m = 0; m < this.SelectItemList.Count; m++)
				{
					if (this.SelectItemList[m].SelectEmpty())
					{
						return;
					}
				}
			}
		}
		else
		{
			for (int n = 0; n < this.SelectItemList.Count; n++)
			{
				if (this.SelectItemList[n].AutoSelectKey(key))
				{
					return;
				}
			}
		}
	}

	// Token: 0x060041A6 RID: 16806 RVA: 0x00139514 File Offset: 0x00137714
	public bool IsHaveKey(string key)
	{
		if (this.IsWeaponFlag)
		{
			for (int i = 0; i < this.SelectSkillItemList.Count; i++)
			{
				if (this.SelectSkillItemList[i].IsEqualsKey(key))
				{
					return true;
				}
			}
		}
		else
		{
			for (int j = 0; j < this.SelectItemList.Count; j++)
			{
				if (this.SelectItemList[j].IsEqualsKey(key))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060041A7 RID: 16807 RVA: 0x0013959C File Offset: 0x0013779C
	public void UpdateSelectIndex()
	{
		if (this.IsWeaponFlag)
		{
			for (int i = 0; i < this.SelectSkillItemList.Count; i++)
			{
				this.SelectSkillItemList[i].UpdateSelect(this.CurSelectIndex);
			}
		}
		else
		{
			for (int j = 0; j < this.SelectItemList.Count; j++)
			{
				this.SelectItemList[j].UpdateSelect(this.CurSelectIndex);
			}
		}
	}

	// Token: 0x060041A8 RID: 16808 RVA: 0x00139620 File Offset: 0x00137820
	public random_attri GetindexAtt(GameItem mCurItem, int index)
	{
		if (mCurItem.IsHaveRandomAtt)
		{
			List<random_attri> list = new List<random_attri>(mCurItem.Random_AttriDic.Values);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].index - 1L == (long)index)
				{
					return list[i];
				}
			}
		}
		return null;
	}

	// Token: 0x060041A9 RID: 16809 RVA: 0x00139680 File Offset: 0x00137880
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

	// Token: 0x060041AA RID: 16810 RVA: 0x001396E4 File Offset: 0x001378E4
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

	// Token: 0x04002D79 RID: 11641
	public UISprite ItemIcon;

	// Token: 0x04002D7A RID: 11642
	public UISprite ItemQualityIcon;

	// Token: 0x04002D7B RID: 11643
	public UILabel ItemNameLabel;

	// Token: 0x04002D7C RID: 11644
	public UILabel ItemEnhanceLabel;

	// Token: 0x04002D7D RID: 11645
	public UILabel LevelLabel;

	// Token: 0x04002D7E RID: 11646
	public UILabel ProfessionNameLabel;

	// Token: 0x04002D7F RID: 11647
	public UILabel ProfessionLabel;

	// Token: 0x04002D80 RID: 11648
	public UISprite IsEquipedSprite;

	// Token: 0x04002D81 RID: 11649
	public List<UISprite> StarList;

	// Token: 0x04002D82 RID: 11650
	public UIGrid AttParentGrid;

	// Token: 0x04002D83 RID: 11651
	public List<InfoLineSelectItemLogic> SelectItemList;

	// Token: 0x04002D84 RID: 11652
	public UIGrid SkillParentGrid;

	// Token: 0x04002D85 RID: 11653
	public List<InfoLineSelectItemLogic> SelectSkillItemList;

	// Token: 0x04002D86 RID: 11654
	public int CurSelectIndex = -1;

	// Token: 0x04002D87 RID: 11655
	private string CurSelectkey = string.Empty;

	// Token: 0x04002D88 RID: 11656
	public bool IsWeaponFlag;

	// Token: 0x04002D89 RID: 11657
	private bool IsEquipped;

	// Token: 0x04002D8A RID: 11658
	private DelegateDefine.OneIntParamDelegate UpdatePriceFun;

	// Token: 0x04002D8B RID: 11659
	private EquipInertItemLogic.AutoSelectDelegate AutoSelectFun;

	// Token: 0x02000AF2 RID: 2802
	// (Invoke) Token: 0x06005051 RID: 20561
	public delegate void AutoSelectDelegate(bool isequiped, string key);
}
