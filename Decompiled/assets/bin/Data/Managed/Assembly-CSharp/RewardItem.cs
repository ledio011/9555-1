using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A41 RID: 2625
[ExecuteInEditMode]
public class RewardItem : MonoBehaviour
{
	// Token: 0x06004C91 RID: 19601 RVA: 0x0019F324 File Offset: 0x0019D524
	private void Awake()
	{
		this.AddTriger();
	}

	// Token: 0x06004C92 RID: 19602 RVA: 0x0019F32C File Offset: 0x0019D52C
	private void AddTriger()
	{
		if (base.gameObject.GetComponent<UIEventTrigger>() != null)
		{
			return;
		}
		base.gameObject.AddComponent<BoxCollider>();
		base.GetComponent<UIWidget>().autoResizeBoxCollider = true;
		base.gameObject.AddComponent<UIButtonColor>();
		UIEventTrigger uieventTrigger = base.gameObject.AddComponent<UIEventTrigger>();
		uieventTrigger.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.OnClickShowItems)));
	}

	// Token: 0x06004C93 RID: 19603 RVA: 0x0019F39C File Offset: 0x0019D59C
	public void UpdateItem(string itemId, int quality, int count, int other = 0)
	{
		this.UpdateItem(itemId, (EQUIP_QUALITY)quality, count, other);
	}

	// Token: 0x06004C94 RID: 19604 RVA: 0x0019F3AC File Offset: 0x0019D5AC
	public void OnClickShowItems()
	{
		if (this.curItemData == null)
		{
			return;
		}
		SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(4, 1f, null);
		if (this.item.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP || this.item.ItemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
		{
			if (this.IsEquipedTips)
			{
				ItemInfoRootLogicNew.ShowEquipFullTips(this.item, this.itemLevel);
			}
			else
			{
				ItemInfoRootLogicNew.ShowEquipTips(this.item, this.itemLevel);
			}
		}
		else
		{
			ItemInfoRootLogicNew.ShowItemTips(this.item, this.itemLevel, false, UI_PAGE_TYPE.INVALID);
		}
	}

	// Token: 0x06004C95 RID: 19605 RVA: 0x0019F450 File Offset: 0x0019D650
	public void UpdateItem(string itemId, EQUIP_QUALITY quality, int count, int other = 0)
	{
		this.IsEquipedTips = false;
		this.itemLevel = 0;
		ItemData itemDataByID = DataManager.GetItemDataByID(itemId);
		this.curItemData = itemDataByID;
		this.item = new GameItem(itemId, quality, count);
		if (this.OtherLabel != null)
		{
			this.OtherLabel.text = string.Empty;
		}
		if (itemDataByID != null)
		{
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				this.qualitySprite.spriteName = quality.ToString();
			}
			else
			{
				this.qualitySprite.spriteName = itemDataByID.QualityType.ToString();
			}
			this.iconSprite.spriteName = itemDataByID.BackPackIcon;
			this.iconSprite.enabled = true;
			if (count > 1)
			{
				this.itemCountLabel.text = string.Format("x{0}", count);
			}
			else
			{
				this.itemCountLabel.text = string.Empty;
			}
			if (this.OtherLabel != null)
			{
				if (other > 0)
				{
					this.OtherLabel.text = string.Format("{0}+ {1}", StrDictionary.GetDictionaryString("#{102044}", new object[0]), other);
				}
				else
				{
					this.OtherLabel.text = string.Empty;
				}
			}
			if (this.TimeLimitSp != null)
			{
				this.TimeLimitSp.enabled = false;
			}
			if (this.StarObj != null)
			{
				NGUITools.SetActive(this.StarObj, false);
			}
			if (this.AddObj != null)
			{
				NGUITools.SetActive(this.AddObj, false);
			}
		}
		else
		{
			NGUITools.SetActive(base.gameObject, false);
		}
	}

	// Token: 0x06004C96 RID: 19606 RVA: 0x0019F608 File Offset: 0x0019D808
	public void UpdateItem(GameItem curItem, int level, bool isEquipTips = false)
	{
		this.IsEquipedTips = isEquipTips;
		this.itemLevel = level;
		ItemData itemData = curItem.ItemData;
		this.curItemData = curItem.ItemData;
		this.item = curItem;
		if (this.OtherLabel != null)
		{
			this.OtherLabel.text = string.Empty;
		}
		if (this.AddObj != null)
		{
			NGUITools.SetActive(this.AddObj, false);
		}
		if (this.StarObj != null)
		{
			NGUITools.SetActive(this.StarObj, false);
		}
		if (this.TimeLimitSp != null)
		{
			this.TimeLimitSp.enabled = false;
		}
		if (this.curItemData != null)
		{
			if (this.curItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				this.qualitySprite.spriteName = curItem.GetItemQuality().ToString();
				if (this.AddObj != null)
				{
					NGUITools.SetActive(this.AddObj, true);
					this.AddLabel.text = string.Format("+{0}", this.itemLevel);
				}
				if (this.StarObj != null)
				{
					NGUITools.SetActive(this.StarObj, true);
					int starByScore = curItem.GetStarByScore();
					for (int i = 0; i < this.StarList.Count; i++)
					{
						if (i < starByScore)
						{
							this.StarList[i].enabled = true;
						}
						else
						{
							this.StarList[i].enabled = false;
						}
					}
				}
			}
			else if (this.curItemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
			{
				this.qualitySprite.spriteName = itemData.QualityType.ToString();
				if (this.TimeLimitSp != null)
				{
					this.TimeLimitSp.enabled = (curItem.Parm[4] > 0 || curItem.ItemData.UseHour > 0);
				}
			}
			else
			{
				this.qualitySprite.spriteName = itemData.QualityType.ToString();
			}
			this.iconSprite.spriteName = itemData.BackPackIcon;
			this.iconSprite.enabled = true;
			if (this.itemCountLabel != null)
			{
				if (curItem.StackNum > 1)
				{
					this.itemCountLabel.text = string.Format("x{0}", curItem.StackNum);
				}
				else
				{
					this.itemCountLabel.text = string.Empty;
				}
			}
		}
		else
		{
			NGUITools.SetActive(base.gameObject, false);
		}
	}

	// Token: 0x06004C97 RID: 19607 RVA: 0x0019F8A8 File Offset: 0x0019DAA8
	public void SetItemEmpty()
	{
		this.itemLevel = 0;
		this.curItemData = null;
		this.item = null;
		this.IsEquipedTips = false;
		if (this.itemCountLabel != null)
		{
			this.itemCountLabel.text = string.Empty;
		}
		this.iconSprite.spriteName = GameDefine.EmptyItemIconName;
		this.qualitySprite.spriteName = EQUIP_QUALITY.KUANG_BLACK.ToString();
		if (this.TimeLimitSp != null)
		{
			this.TimeLimitSp.enabled = false;
		}
		if (this.StarObj != null)
		{
			NGUITools.SetActive(this.StarObj, false);
		}
		if (this.AddObj != null)
		{
			NGUITools.SetActive(this.AddObj, false);
		}
	}

	// Token: 0x04003A3B RID: 14907
	public UISprite iconSprite;

	// Token: 0x04003A3C RID: 14908
	public UISprite qualitySprite;

	// Token: 0x04003A3D RID: 14909
	public UILabel itemCountLabel;

	// Token: 0x04003A3E RID: 14910
	public UILabel OtherLabel;

	// Token: 0x04003A3F RID: 14911
	private ItemData curItemData;

	// Token: 0x04003A40 RID: 14912
	private GameItem item;

	// Token: 0x04003A41 RID: 14913
	private int itemLevel;

	// Token: 0x04003A42 RID: 14914
	private bool IsEquipedTips;

	// Token: 0x04003A43 RID: 14915
	public GameObject StarObj;

	// Token: 0x04003A44 RID: 14916
	public List<UISprite> StarList;

	// Token: 0x04003A45 RID: 14917
	public GameObject AddObj;

	// Token: 0x04003A46 RID: 14918
	public UILabel AddLabel;

	// Token: 0x04003A47 RID: 14919
	public UISprite TimeLimitSp;
}
