using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200094A RID: 2378
public class ItemUILogic : MonoBehaviour
{
	// Token: 0x06004286 RID: 17030 RVA: 0x00144914 File Offset: 0x00142B14
	public void UpdateItemUI(GameItem item, bool isPowerful = false)
	{
		if (item != null)
		{
			if (!item.IsEmpty())
			{
				this.curItemData = DataManager.GetItemDataByID(item.ItemId);
				this.curItem = item;
				this.SetDefaultShow();
				if (this.NewflagSprite != null)
				{
					this.NewflagSprite.enabled = (this.curItem.Parm[5] == 0);
				}
				if (this.TimeLimitSp != null)
				{
					this.TimeLimitSp.enabled = (this.curItem.Parm[4] > 0 || this.curItem.ItemData.UseHour > 0);
					if (this.curItemData.Type == GameDefine.ITEM_TYPE.DANCE_TOOL || this.curItemData.Type == GameDefine.ITEM_TYPE.WORLDSPEAK || this.curItemData.Type == GameDefine.ITEM_TYPE.ENEMYWARP_TOOL)
					{
						this.TimeLimitSp.transform.localPosition = new Vector3(-16f, -15f, 0f);
					}
					else
					{
						this.TimeLimitSp.transform.localPosition = new Vector3(16f, -15f, 0f);
					}
				}
				if (this.UpArrowObj != null)
				{
					NGUITools.SetActive(this.UpArrowObj, isPowerful);
				}
				if (this.curItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					if (this.ItemNumLabel != null)
					{
						UnityVersionUtil.SetActiveRecursive(this.ItemNumLabel.gameObject, false);
					}
					if (this.curItem.GetItemQuality() != EQUIP_QUALITY.INVALID)
					{
						UnityVersionUtil.SetActiveRecursive(this.ItemQualityIcon.gameObject, true);
						this.ItemQualityIcon.spriteName = this.curItem.GetItemQuality().ToString();
					}
					else
					{
						this.ItemQualityIcon.spriteName = this.curItemData.QualityType.ToString();
					}
					if (item.ContainerType == ITEM_CONTAINER_TYPE.EQUIPPACK)
					{
						if (this.equipFlagSprite != null)
						{
							this.equipFlagSprite.spriteName = "CZ_yiZhuangBei_tuBiao";
						}
					}
					else if (this.equipFlagSprite != null)
					{
						this.equipFlagSprite.spriteName = string.Empty;
					}
					if (this.curItem.IsAppraise)
					{
						if (this.AppraiseObj != null)
						{
							NGUITools.SetActive(this.AppraiseObj, false);
						}
					}
					else if (this.AppraiseObj != null)
					{
						NGUITools.SetActive(this.AppraiseObj, true);
					}
					if (this.StarObj != null)
					{
						NGUITools.SetActive(this.StarObj, true);
						int starByScore = this.curItem.GetStarByScore();
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
					if (this.AddObj != null)
					{
						NGUITools.SetActive(this.AddObj, true);
						this.AddLabel.text = string.Format("+{0}", this.curItem.ItemLevel);
					}
				}
				else if (this.curItemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
				{
					if (this.ItemNumLabel != null)
					{
						UnityVersionUtil.SetActiveRecursive(this.ItemNumLabel.gameObject, false);
					}
					this.ItemQualityIcon.spriteName = this.curItemData.QualityType.ToString();
					if (item.ContainerType == ITEM_CONTAINER_TYPE.EQUIPPACK)
					{
						if (this.equipFlagSprite != null)
						{
							this.equipFlagSprite.spriteName = "CZ_yiZhuangBei_tuBiao";
						}
					}
					else if (this.equipFlagSprite != null)
					{
						this.equipFlagSprite.spriteName = string.Empty;
					}
				}
				else if (this.curItemData.Type == GameDefine.ITEM_TYPE.ENHANCE_ITEM)
				{
					if (this.ItemNumLabel != null)
					{
						UnityVersionUtil.SetActiveRecursive(this.ItemNumLabel.gameObject, true);
						if (this.curItem.StackNum > 1)
						{
							this.ItemNumLabel.text = string.Format("{0}", this.curItem.StackNum);
						}
						else
						{
							this.ItemNumLabel.text = string.Empty;
						}
						this.ItemNumLabel.color = Color.white;
					}
					this.ItemQualityIcon.spriteName = this.curItemData.QualityType.ToString();
					if (this.equipFlagSprite != null)
					{
						this.equipFlagSprite.spriteName = string.Empty;
					}
				}
				else if (this.curItemData.Type == GameDefine.ITEM_TYPE.BADGE)
				{
					if (this.ItemNumLabel != null)
					{
						UnityVersionUtil.SetActiveRecursive(this.ItemNumLabel.gameObject, true);
						if (this.curItem.StackNum > 1)
						{
							this.ItemNumLabel.text = string.Format("{0}", this.curItem.StackNum);
						}
						else
						{
							this.ItemNumLabel.text = string.Empty;
						}
						this.ItemNumLabel.color = Color.white;
					}
					this.ItemQualityIcon.spriteName = this.curItemData.QualityType.ToString();
					if (this.equipFlagSprite != null)
					{
						this.equipFlagSprite.spriteName = string.Empty;
					}
				}
				else
				{
					if (this.ItemNumLabel != null)
					{
						UnityVersionUtil.SetActiveRecursive(this.ItemNumLabel.gameObject, true);
						if (this.curItem.StackNum > 1)
						{
							this.ItemNumLabel.text = string.Format("{0}", this.curItem.StackNum);
						}
						else
						{
							this.ItemNumLabel.text = string.Empty;
						}
						this.ItemNumLabel.color = Color.white;
					}
					this.ItemQualityIcon.spriteName = this.curItemData.QualityType.ToString();
					if (this.equipFlagSprite != null)
					{
						this.equipFlagSprite.spriteName = string.Empty;
					}
				}
				this.ItemIcon.spriteName = this.curItemData.BackPackIcon;
			}
			else
			{
				this.SetItemEmpty(item.ContainerType, item.EquipType);
			}
		}
	}

	// Token: 0x06004287 RID: 17031 RVA: 0x00144F9C File Offset: 0x0014319C
	public void SetSellChoose(bool active)
	{
		if (this.SellChoosePic != null)
		{
			NGUITools.SetActive(this.SellChoosePic.gameObject, active);
		}
	}

	// Token: 0x06004288 RID: 17032 RVA: 0x00144FCC File Offset: 0x001431CC
	public void SetItemEmpty(ITEM_CONTAINER_TYPE containerType, EQUIP_BACKPACK_TYPE type = EQUIP_BACKPACK_TYPE.COUNT)
	{
		this.curItemData = null;
		this.curItem = null;
		if (this.ItemNumLabel != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ItemNumLabel.gameObject, false);
		}
		if (containerType == ITEM_CONTAINER_TYPE.BADGE_EQUIPPACK)
		{
			this.ItemIcon.spriteName = GameDefine.EmptyBadgeIconName;
		}
		else if (containerType == ITEM_CONTAINER_TYPE.EQUIPPACK)
		{
			if (type == EQUIP_BACKPACK_TYPE.WEAPON)
			{
				this.ItemIcon.spriteName = "ZhuangBeiCao_wuQi";
			}
			else if (type == EQUIP_BACKPACK_TYPE.HEAD)
			{
				this.ItemIcon.spriteName = "ZhuangBeiCao_tou";
			}
			else if (type == EQUIP_BACKPACK_TYPE.BODY)
			{
				this.ItemIcon.spriteName = "ZhuangBeiCao_shangYi";
			}
			else if (type == EQUIP_BACKPACK_TYPE.LEG)
			{
				this.ItemIcon.spriteName = "ZhuangBeiCao_xiaYi";
			}
			else if (type == EQUIP_BACKPACK_TYPE.BELT)
			{
				this.ItemIcon.spriteName = "ZhuangBeiCao_yaoDai";
			}
			else if (type == EQUIP_BACKPACK_TYPE.NECKLACE)
			{
				this.ItemIcon.spriteName = "ZhuangBeiCao_xiangLian";
			}
			else
			{
				this.ItemIcon.spriteName = GameDefine.EmptyEquipIconName;
			}
		}
		else
		{
			this.ItemIcon.spriteName = GameDefine.EmptyItemIconName;
		}
		this.SetDefaultShow();
		this.ItemQualityIcon.spriteName = EQUIP_QUALITY.KUANG_BLACK.ToString();
	}

	// Token: 0x06004289 RID: 17033 RVA: 0x00145118 File Offset: 0x00143318
	public void SetDefaultShow()
	{
		if (this.NewflagSprite != null)
		{
			this.NewflagSprite.enabled = false;
		}
		if (this.TimeLimitSp != null)
		{
			this.TimeLimitSp.enabled = false;
		}
		this.SetSellChoose(false);
		if (this.UpArrowObj != null)
		{
			NGUITools.SetActive(this.UpArrowObj, false);
		}
		if (this.AppraiseObj != null)
		{
			NGUITools.SetActive(this.AppraiseObj, false);
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

	// Token: 0x0600428A RID: 17034 RVA: 0x001451DC File Offset: 0x001433DC
	public void SetItemLock()
	{
	}

	// Token: 0x0600428B RID: 17035 RVA: 0x001451E0 File Offset: 0x001433E0
	public void OnClickItem()
	{
		if (this.curItem == null)
		{
			if (!this.ItemIcon.spriteName.Equals(GameDefine.EmptyItemIconName) && this.onClickItem != null)
			{
				this.onClickItem(this.curItem, this);
			}
			return;
		}
		if (this.NewflagSprite != null && this.curItem.Parm[5] == 0)
		{
			this.curItem.Parm[5] = 1;
			this.NewflagSprite.enabled = false;
			change_item_state.request request = new change_item_state.request();
			request.indexId = this.curItem.IndexId;
			request.type = (long)this.curItemData.GetContainerType();
			NetLogic.GetInstance().Send<Protocol.change_item_state>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.UpdateEquipsTips();
		}
		if (this.onClickItem != null)
		{
			this.onClickItem(this.curItem, this);
			SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(4, 1f, null);
		}
	}

	// Token: 0x0600428C RID: 17036 RVA: 0x001452E0 File Offset: 0x001434E0
	public void UpdateNewItemUI(GameItem item)
	{
		if (item != null)
		{
			if (!item.IsEmpty())
			{
				this.curItemData = DataManager.GetItemDataByID(item.ItemId);
				this.curItem = item;
				if (this.NewflagSprite != null)
				{
					this.NewflagSprite.enabled = (this.curItem.Parm[5] == 0);
				}
				this.ItemNumLabel.enabled = false;
				if (this.curItemData.Type == GameDefine.ITEM_TYPE.EQUIP || this.curItemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
				{
					if (this.curItem.GetItemQuality() != EQUIP_QUALITY.INVALID)
					{
						UnityVersionUtil.SetActiveRecursive(this.ItemQualityIcon.gameObject, true);
						this.ItemQualityIcon.spriteName = this.curItem.GetItemQuality().ToString();
					}
					else
					{
						this.ItemQualityIcon.spriteName = this.curItemData.QualityType.ToString();
					}
					if (item.ContainerType == ITEM_CONTAINER_TYPE.EQUIPPACK)
					{
						if (this.equipFlagSprite != null)
						{
							this.equipFlagSprite.spriteName = "CZ_yiZhuangBei_tuBiao";
						}
					}
					else if (this.equipFlagSprite != null)
					{
						this.equipFlagSprite.spriteName = string.Empty;
					}
				}
				else if (this.curItemData.Type == GameDefine.ITEM_TYPE.ENHANCE_ITEM)
				{
					this.ItemQualityIcon.spriteName = this.curItemData.QualityType.ToString();
					if (this.equipFlagSprite != null)
					{
						this.equipFlagSprite.spriteName = string.Empty;
					}
				}
				else if (this.curItemData.Type == GameDefine.ITEM_TYPE.BADGE)
				{
					this.ItemQualityIcon.spriteName = this.curItemData.QualityType.ToString();
					if (this.equipFlagSprite != null)
					{
						this.equipFlagSprite.spriteName = string.Empty;
					}
				}
				else
				{
					this.ItemQualityIcon.spriteName = this.curItemData.QualityType.ToString();
					if (this.equipFlagSprite != null)
					{
						this.equipFlagSprite.spriteName = string.Empty;
					}
				}
				this.ItemIcon.spriteName = this.curItemData.BackPackIcon;
			}
			else
			{
				this.SetItemEmpty(ITEM_CONTAINER_TYPE.ITEM_BACKPACK, EQUIP_BACKPACK_TYPE.COUNT);
			}
		}
	}

	// Token: 0x04002E8E RID: 11918
	public ItemUILogic.OnClickItemDelegate onClickItem;

	// Token: 0x04002E8F RID: 11919
	public ItemData curItemData;

	// Token: 0x04002E90 RID: 11920
	public GameItem curItem;

	// Token: 0x04002E91 RID: 11921
	public UISprite ItemIcon;

	// Token: 0x04002E92 RID: 11922
	public UILabel ItemNumLabel;

	// Token: 0x04002E93 RID: 11923
	public UISprite equipFlagSprite;

	// Token: 0x04002E94 RID: 11924
	public UISprite ItemQualityIcon;

	// Token: 0x04002E95 RID: 11925
	public UIWidget RootWidget;

	// Token: 0x04002E96 RID: 11926
	public UISprite NewflagSprite;

	// Token: 0x04002E97 RID: 11927
	public UISprite TimeLimitSp;

	// Token: 0x04002E98 RID: 11928
	public UISprite SellChoosePic;

	// Token: 0x04002E99 RID: 11929
	public GameObject UpArrowObj;

	// Token: 0x04002E9A RID: 11930
	public GameObject AppraiseObj;

	// Token: 0x04002E9B RID: 11931
	public GameObject StarObj;

	// Token: 0x04002E9C RID: 11932
	public List<UISprite> StarList;

	// Token: 0x04002E9D RID: 11933
	public GameObject AddObj;

	// Token: 0x04002E9E RID: 11934
	public UILabel AddLabel;

	// Token: 0x04002E9F RID: 11935
	public bool BadgeItemFlag;

	// Token: 0x02000AF3 RID: 2803
	// (Invoke) Token: 0x06005055 RID: 20565
	public delegate void OnClickItemDelegate(GameItem item, ItemUILogic itemUILogic);
}
