using System;
using SprotoType;

// Token: 0x02000939 RID: 2361
public class EquipInhertRootLogic : SingletonUnity<EquipInhertRootLogic>
{
	// Token: 0x060041AE RID: 16814 RVA: 0x00139738 File Offset: 0x00137938
	public void ShowInfo(GameItem gameItem, GameItem equipedItem)
	{
		this.leftitem = equipedItem;
		this.rightitem = gameItem;
		if (gameItem.ItemData.SubType == 0)
		{
			this.IsWeaponFlag = true;
			this.LeftInfoLabel.text = StrDictionary.GetDictionaryString("#{100672}", new object[0]);
			this.RightInfoLabel.text = StrDictionary.GetDictionaryString("#{100673}", new object[0]);
		}
		else
		{
			this.IsWeaponFlag = false;
			this.LeftInfoLabel.text = StrDictionary.GetDictionaryString("#{100679}", new object[0]);
			this.RightInfoLabel.text = StrDictionary.GetDictionaryString("#{100680}", new object[0]);
		}
		this.leftInhert.Reset(equipedItem, true, null, new EquipInertItemLogic.AutoSelectDelegate(this.AutoSelect));
		this.rightInhert.Reset(gameItem, false, new DelegateDefine.OneIntParamDelegate(this.UpdatePrice), new EquipInertItemLogic.AutoSelectDelegate(this.AutoSelect));
		this.PriceLabel.text = GameMoneyHelper.GetMoneyValStr(0, GameDefine.MONEY_TYPE.GOLD);
		this.NeedPrice = 0;
	}

	// Token: 0x060041AF RID: 16815 RVA: 0x0013983C File Offset: 0x00137A3C
	public void OnClickInhertBtn()
	{
		if (this.leftInhert.CurSelectIndex != -1 && this.rightInhert.CurSelectIndex != -1)
		{
			if (!GameMoneyHelper.BeforeCheckBuy(GameDefine.MONEY_TYPE.GOLD, this.NeedPrice))
			{
				return;
			}
			if (this.IsWeaponFlag)
			{
				weapon_inhert.request request = new weapon_inhert.request();
				request.index1 = this.leftitem.IndexId;
				request.index2 = this.rightitem.IndexId;
				request.attribute_index1 = (long)(this.leftInhert.CurSelectIndex + 1);
				request.attribute_index2 = (long)(this.rightInhert.CurSelectIndex + 1);
				NetLogic.GetInstance().Send<Protocol.weapon_inhert>(request, null);
			}
			else
			{
				attribute_inhert.request request2 = new attribute_inhert.request();
				request2.index1 = this.leftitem.IndexId;
				request2.index2 = this.rightitem.IndexId;
				request2.attribute_index1 = (long)(this.leftInhert.CurSelectIndex + 1);
				request2.attribute_index2 = (long)(this.rightInhert.CurSelectIndex + 1);
				NetLogic.GetInstance().Send<Protocol.attribute_inhert>(request2, null);
			}
		}
	}

	// Token: 0x060041B0 RID: 16816 RVA: 0x00139948 File Offset: 0x00137B48
	public void AutoSelect(bool isequiped, string key)
	{
		if (string.IsNullOrEmpty(key))
		{
			if (isequiped)
			{
				if (!string.IsNullOrEmpty(this.rightInhert.GetSelectKey) && this.leftInhert.IsHaveKey(this.rightInhert.GetSelectKey))
				{
					this.rightInhert.ClearSelect();
				}
			}
			else if (!string.IsNullOrEmpty(this.leftInhert.GetSelectKey) && this.rightInhert.IsHaveKey(this.leftInhert.GetSelectKey))
			{
				this.leftInhert.ClearSelect();
			}
			return;
		}
		if (isequiped)
		{
			if (!string.IsNullOrEmpty(this.rightInhert.GetSelectKey) && this.leftInhert.IsHaveKey(this.rightInhert.GetSelectKey) && !key.Equals(this.rightInhert.GetSelectKey))
			{
				this.rightInhert.ClearSelect();
			}
			this.rightInhert.SelectNeedKey(key, false);
		}
		else
		{
			this.leftInhert.SelectNeedKey(key, true);
		}
	}

	// Token: 0x060041B1 RID: 16817 RVA: 0x00139A58 File Offset: 0x00137C58
	public void UpdateInhertItem(GameItem newitem)
	{
		if (newitem != null && this.leftitem.IndexId == newitem.IndexId)
		{
			this.leftitem = newitem;
			this.leftInhert.Reset(this.leftitem, true, null, new EquipInertItemLogic.AutoSelectDelegate(this.AutoSelect));
		}
		if (newitem != null && this.rightitem.IndexId == newitem.IndexId)
		{
			this.rightitem = newitem;
			this.rightInhert.Reset(this.rightitem, false, new DelegateDefine.OneIntParamDelegate(this.UpdatePrice), new EquipInertItemLogic.AutoSelectDelegate(this.AutoSelect));
		}
	}

	// Token: 0x060041B2 RID: 16818 RVA: 0x00139AF4 File Offset: 0x00137CF4
	public void UpdatePrice(int quality)
	{
		if (GameManager.IsSupportCurDataVersion167())
		{
			this.NeedPrice = this.rightitem.GetInhertPrice(quality, this.IsWeaponFlag);
			this.PriceLabel.text = GameMoneyHelper.GetMoneyValStr(this.NeedPrice, GameDefine.MONEY_TYPE.GOLD);
		}
		else if (this.IsWeaponFlag)
		{
			if (GameDefine.WeaponInherited.ContainsKey(quality))
			{
				ConfigData configDataByKey = DataManager.GetConfigDataByKey(GameDefine.WeaponInherited[quality]);
				if (configDataByKey != null)
				{
					this.NeedPrice = configDataByKey.Valuei;
					this.PriceLabel.text = GameMoneyHelper.GetMoneyValStr(this.NeedPrice, GameDefine.MONEY_TYPE.GOLD);
				}
			}
		}
		else if (GameDefine.EquipInherited.ContainsKey(quality))
		{
			ConfigData configDataByKey2 = DataManager.GetConfigDataByKey(GameDefine.EquipInherited[quality]);
			if (configDataByKey2 != null)
			{
				this.NeedPrice = configDataByKey2.Valuei;
				this.PriceLabel.text = GameMoneyHelper.GetMoneyValStr(this.NeedPrice, GameDefine.MONEY_TYPE.GOLD);
			}
		}
	}

	// Token: 0x060041B3 RID: 16819 RVA: 0x00139BE4 File Offset: 0x00137DE4
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.EquipInhertRoot);
	}

	// Token: 0x04002D8E RID: 11662
	public EquipInertItemLogic leftInhert;

	// Token: 0x04002D8F RID: 11663
	public EquipInertItemLogic rightInhert;

	// Token: 0x04002D90 RID: 11664
	public UILabel PriceLabel;

	// Token: 0x04002D91 RID: 11665
	public GameItem leftitem;

	// Token: 0x04002D92 RID: 11666
	public GameItem rightitem;

	// Token: 0x04002D93 RID: 11667
	public bool IsWeaponFlag;

	// Token: 0x04002D94 RID: 11668
	private int NeedPrice;

	// Token: 0x04002D95 RID: 11669
	public UILabel LeftInfoLabel;

	// Token: 0x04002D96 RID: 11670
	public UILabel RightInfoLabel;
}
