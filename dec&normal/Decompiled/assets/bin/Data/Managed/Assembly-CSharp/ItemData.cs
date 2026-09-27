using System;
using System.Collections.Generic;

// Token: 0x0200017C RID: 380
public class ItemData
{
	// Token: 0x06000F63 RID: 3939 RVA: 0x00063078 File Offset: 0x00061278
	public int GetScore(EQUIP_QUALITY quality = EQUIP_QUALITY.INVALID)
	{
		if (this.mScore == null)
		{
			string[] array = this.Score.Split(new char[]
			{
				'#'
			});
			this.mScore = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				this.mScore[i] = int.Parse(array[i]);
			}
		}
		if (this.Type == GameDefine.ITEM_TYPE.EQUIP && quality < (EQUIP_QUALITY)this.mScore.Length)
		{
			return this.mScore[(int)quality];
		}
		if (this.mScore.Length > 0)
		{
			return this.mScore[0];
		}
		return -1;
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x00063118 File Offset: 0x00061318
	public int GetSellPrice(EQUIP_QUALITY quality = EQUIP_QUALITY.INVALID)
	{
		if (this.Type == GameDefine.ITEM_TYPE.ADD_COIN || this.Type == GameDefine.ITEM_TYPE.ADD_DIAMOND || this.Type == GameDefine.ITEM_TYPE.ADD_GOLD)
		{
			return 0;
		}
		if (this.mPrice == null && !string.IsNullOrEmpty(this.Price))
		{
			string[] array = this.Price.Split(new char[]
			{
				'#'
			});
			this.mPrice = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				this.mPrice[i] = int.Parse(array[i]);
			}
		}
		if (this.Type == GameDefine.ITEM_TYPE.EQUIP && quality < (EQUIP_QUALITY)this.mPrice.Length)
		{
			return this.mPrice[(int)quality];
		}
		if (this.mPrice.Length > 0)
		{
			return this.mPrice[0];
		}
		return -1;
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x000631F0 File Offset: 0x000613F0
	public bool CanSell()
	{
		return this.Type == GameDefine.ITEM_TYPE.EQUIP || this.Type == GameDefine.ITEM_TYPE.ENHANCE_ITEM || this.Type == GameDefine.ITEM_TYPE.POTION || this.Type == GameDefine.ITEM_TYPE.BOX || this.Type == GameDefine.ITEM_TYPE.LOCK1 || this.Type == GameDefine.ITEM_TYPE.LOCK2;
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x0006324C File Offset: 0x0006144C
	public bool CanConsign()
	{
		return this.ConsignPrice > 0;
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x00063260 File Offset: 0x00061460
	public GameDefine.SHOP_TYPE GetItemShopType()
	{
		List<ShopData> shpTabClassList = this.GetShpTabClassList(0);
		for (int i = 0; i < shpTabClassList.Count; i++)
		{
			if (shpTabClassList[i].ItemID.Equals(this.ID))
			{
				return GameDefine.SHOP_TYPE.TOOL_SHOP;
			}
		}
		shpTabClassList = this.GetShpTabClassList(1);
		for (int j = 0; j < shpTabClassList.Count; j++)
		{
			if (shpTabClassList[j].ItemID.Equals(this.ID))
			{
				return GameDefine.SHOP_TYPE.EQUIP_SHOP;
			}
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			shpTabClassList = this.GetShpTabClassList(3);
			for (int k = 0; k < shpTabClassList.Count; k++)
			{
				if (shpTabClassList[k].ItemID.Equals(this.ID))
				{
					return GameDefine.SHOP_TYPE.GUILD_SHOP;
				}
			}
		}
		return GameDefine.SHOP_TYPE.TOOL_SHOP;
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x0006333C File Offset: 0x0006153C
	public int GetItemClass(GameDefine.SHOP_TYPE shoptype)
	{
		List<ShopData> shpTabClassList = this.GetShpTabClassList((int)shoptype);
		for (int i = 0; i < shpTabClassList.Count; i++)
		{
			if (shpTabClassList[i].ItemID.Equals(this.ID))
			{
				return shpTabClassList[i].Class;
			}
		}
		return -1;
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x00063394 File Offset: 0x00061594
	public List<ShopData> GetShpTabClassList(int curType)
	{
		List<ShopData> list = new List<ShopData>();
		List<ShopData> shopDataList = DataManager.GetShopDataList();
		for (int i = 0; i < shopDataList.Count; i++)
		{
			if (curType == shopDataList[i].Shop)
			{
				list.Add(shopDataList[i]);
			}
		}
		return list;
	}

	// Token: 0x17000307 RID: 775
	// (get) Token: 0x06000F6A RID: 3946 RVA: 0x000633E4 File Offset: 0x000615E4
	public string MName
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Name, new object[0]);
		}
	}

	// Token: 0x17000308 RID: 776
	// (get) Token: 0x06000F6B RID: 3947 RVA: 0x000633F8 File Offset: 0x000615F8
	public string MDescription
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Description, new object[0]);
		}
	}

	// Token: 0x17000309 RID: 777
	// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0006340C File Offset: 0x0006160C
	public GameDefine.ITEM_TYPE Type
	{
		get
		{
			return (GameDefine.ITEM_TYPE)this.ItemType;
		}
	}

	// Token: 0x1700030A RID: 778
	// (get) Token: 0x06000F6D RID: 3949 RVA: 0x00063414 File Offset: 0x00061614
	public bool CanShowModel
	{
		get
		{
			return this.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP || (this.Type == GameDefine.ITEM_TYPE.EQUIP && this.SubType < 4);
		}
	}

	// Token: 0x1700030B RID: 779
	// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0006344C File Offset: 0x0006164C
	public GameDefine.MONEY_TYPE SellType
	{
		get
		{
			return (GameDefine.MONEY_TYPE)this.PriceType;
		}
	}

	// Token: 0x1700030C RID: 780
	// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00063454 File Offset: 0x00061654
	public EQUIP_QUALITY QualityType
	{
		get
		{
			return (EQUIP_QUALITY)this.Quality;
		}
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x0006345C File Offset: 0x0006165C
	public ITEM_CONTAINER_TYPE GetContainerType()
	{
		if (this.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			return ITEM_CONTAINER_TYPE.EQUIP_BACKPACK;
		}
		if (this.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
		{
			return ITEM_CONTAINER_TYPE.FASHION_BACKPACK;
		}
		if (this.Type == GameDefine.ITEM_TYPE.BADGE)
		{
			return ITEM_CONTAINER_TYPE.BADGE_BACKPACK;
		}
		return ITEM_CONTAINER_TYPE.ITEM_BACKPACK;
	}

	// Token: 0x04000F81 RID: 3969
	public string ID;

	// Token: 0x04000F82 RID: 3970
	public string Name;

	// Token: 0x04000F83 RID: 3971
	[ServerExclude("ServerNoUse")]
	public string Description;

	// Token: 0x04000F84 RID: 3972
	public int Level;

	// Token: 0x04000F85 RID: 3973
	public int Flags;

	// Token: 0x04000F86 RID: 3974
	public int Quality;

	// Token: 0x04000F87 RID: 3975
	public int ItemType;

	// Token: 0x04000F88 RID: 3976
	public int SubType;

	// Token: 0x04000F89 RID: 3977
	public int PriceType;

	// Token: 0x04000F8A RID: 3978
	public string Price = string.Empty;

	// Token: 0x04000F8B RID: 3979
	public int ConsignPrice;

	// Token: 0x04000F8C RID: 3980
	public int Stack;

	// Token: 0x04000F8D RID: 3981
	public int Function;

	// Token: 0x04000F8E RID: 3982
	public string FunLock;

	// Token: 0x04000F8F RID: 3983
	[ServerExclude("ServerNoUse")]
	public string DropIcon;

	// Token: 0x04000F90 RID: 3984
	[ServerExclude("ServerNoUse")]
	public string BackPackIcon;

	// Token: 0x04000F91 RID: 3985
	public string Score;

	// Token: 0x04000F92 RID: 3986
	public string Tvshow;

	// Token: 0x04000F93 RID: 3987
	public int UseHour = -1;

	// Token: 0x04000F94 RID: 3988
	public string JumpPath = string.Empty;

	// Token: 0x04000F95 RID: 3989
	private int[] mScore;

	// Token: 0x04000F96 RID: 3990
	private int[] mPrice;
}
