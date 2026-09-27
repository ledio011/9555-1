using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x020008B2 RID: 2226
public class GameMoneyHelper
{
	// Token: 0x06003BEF RID: 15343 RVA: 0x0010592C File Offset: 0x00103B2C
	public static bool BeforeCheckBuy(int type, int cost)
	{
		return GameMoneyHelper.BeforeCheckBuy((GameDefine.MONEY_TYPE)type, cost);
	}

	// Token: 0x06003BF0 RID: 15344 RVA: 0x00105938 File Offset: 0x00103B38
	public static bool BeforeCheckBuy(GameDefine.MONEY_TYPE type, int cost)
	{
		if (type == GameDefine.MONEY_TYPE.CASH)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Cash >= (long)cost)
			{
				return true;
			}
		}
		else if (type == GameDefine.MONEY_TYPE.GOLD)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Gold >= (long)cost)
			{
				return true;
			}
		}
		else if (type == GameDefine.MONEY_TYPE.GUILD_CONTRIBUTE)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GuildContribute >= (long)cost)
			{
				return true;
			}
		}
		else if (type == GameDefine.MONEY_TYPE.DIAMOND)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Diamond >= (long)cost)
			{
				return true;
			}
		}
		else if (type == GameDefine.MONEY_TYPE.BATTLECOIN)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.BattleCoin >= (long)cost)
			{
				return true;
			}
		}
		else if (type == GameDefine.MONEY_TYPE.ACTIVITYCOIN && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityCoin >= (long)cost)
		{
			return true;
		}
		if (type == GameDefine.MONEY_TYPE.DIAMOND)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopShopRoot);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PopDiamondBuyRoot, delegate
			{
				SingletonUnity<PopDiamondBuyRootLogic>.Instance.EnableReset();
				ask_shop_list.request request = new ask_shop_list.request();
				request.type = 4L;
				request.subType = 1L;
				NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
				WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
			}, null);
		}
		else if (type == GameDefine.MONEY_TYPE.CASH)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopDiamondBuyRoot);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PopShopRoot, delegate
			{
				SingletonUnity<PopShopRootLogic>.Instance.EnableReset();
				ask_shop_list.request request = new ask_shop_list.request();
				string text = GameMoneyHelper.GetShopMoneyItemID(GameDefine.MONEY_TYPE.CASH);
				if (string.IsNullOrEmpty(text))
				{
					text = "5001";
				}
				ItemData itemDataByID = DataManager.GetItemDataByID(text);
				GameDefine.SHOP_TYPE itemShopType = itemDataByID.GetItemShopType();
				request.type = (long)itemShopType;
				request.itemId = text;
				request.subType = 1L;
				SingletonUnity<PopShopRootLogic>.Instance.ShowItemProdect(request.itemId);
				NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
				WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
			}, null);
		}
		else if (type == GameDefine.MONEY_TYPE.GOLD)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopDiamondBuyRoot);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PopShopRoot, delegate
			{
				SingletonUnity<PopShopRootLogic>.Instance.EnableReset();
				ask_shop_list.request request = new ask_shop_list.request();
				ItemData itemDataByID = DataManager.GetItemDataByID("5006");
				GameDefine.SHOP_TYPE itemShopType = itemDataByID.GetItemShopType();
				request.type = (long)itemShopType;
				request.itemId = "5006";
				request.subType = 1L;
				SingletonUnity<PopShopRootLogic>.Instance.ShowItemProdect(request.itemId);
				NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
				WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
			}, null);
		}
		else if (type == GameDefine.MONEY_TYPE.GUILD_CONTRIBUTE)
		{
			NoticeLogic.AddNotifyData("#{100639}", true, false);
		}
		else if (type == GameDefine.MONEY_TYPE.BATTLECOIN)
		{
			if (GameManager.IsSupportCurDataVersion56())
			{
				NoticeLogic.AddNotifyData("#{100655}", true, false);
			}
			else
			{
				NoticeLogic.AddNotifyData("Battle Coin is not enough!", true, false);
			}
		}
		else if (type == GameDefine.MONEY_TYPE.ACTIVITYCOIN)
		{
			if (GameManager.IsSupportCurDataVersion56())
			{
				MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{100658}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), null);
			}
			else
			{
				MessageBoxLogic.OpenOKBox("The Event coin is not enough#rYou can get more coin in [ffff00]Event Enemy[-].", StrDictionary.GetDictionaryString("#{100127}", new object[0]), null);
			}
		}
		return false;
	}

	// Token: 0x06003BF1 RID: 15345 RVA: 0x00105B94 File Offset: 0x00103D94
	public static void ShowItemProduct(string itemid, GameDefine.SHOP_TYPE shoptype = GameDefine.SHOP_TYPE.TOOL_SHOP)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (!playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SHOP))
		{
			int condition = DataManager.GetFunctionDataById(3006.ToString()).Condition;
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100649}", new object[]
			{
				condition
			}), true, false);
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopDiamondBuyRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PopShopRoot, delegate
		{
			SingletonUnity<PopShopRootLogic>.Instance.EnableReset();
			ask_shop_list.request request = new ask_shop_list.request();
			ItemData itemDataByID = DataManager.GetItemDataByID(itemid);
			shoptype = itemDataByID.GetItemShopType();
			request.type = (long)shoptype;
			request.itemId = itemid;
			request.subType = 1L;
			SingletonUnity<PopShopRootLogic>.Instance.ShowItemProdect(request.itemId);
			NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
			WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
		}, null);
	}

	// Token: 0x06003BF2 RID: 15346 RVA: 0x00105C34 File Offset: 0x00103E34
	public static void ShowBuyPotion()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ItemData itemDataByID = DataManager.GetItemDataByID("9003");
		if (playerData.CheckLevel(itemDataByID.Level))
		{
			GameMoneyHelper.ShowItemProduct("9003", GameDefine.SHOP_TYPE.TOOL_SHOP);
		}
		else
		{
			itemDataByID = DataManager.GetItemDataByID("9002");
			if (playerData.CheckLevel(itemDataByID.Level))
			{
				GameMoneyHelper.ShowItemProduct("9002", GameDefine.SHOP_TYPE.TOOL_SHOP);
			}
			else
			{
				GameMoneyHelper.ShowItemProduct("9001", GameDefine.SHOP_TYPE.TOOL_SHOP);
			}
		}
	}

	// Token: 0x06003BF3 RID: 15347 RVA: 0x00105CB0 File Offset: 0x00103EB0
	public static bool BeforeCheckBuyTop(string itemId, int cost)
	{
		ItemData itemDataByID = DataManager.GetItemDataByID(itemId);
		if (itemDataByID != null)
		{
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.ADD_COIN)
			{
				return GameMoneyHelper.BeforeCheckBuyTop(GameDefine.MONEY_TYPE.CASH, cost);
			}
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.ADD_GOLD)
			{
				return GameMoneyHelper.BeforeCheckBuyTop(GameDefine.MONEY_TYPE.GOLD, cost);
			}
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.ADD_DIAMOND)
			{
				return GameMoneyHelper.BeforeCheckBuyTop(GameDefine.MONEY_TYPE.CASH, cost);
			}
		}
		return false;
	}

	// Token: 0x06003BF4 RID: 15348 RVA: 0x00105D08 File Offset: 0x00103F08
	public static bool BeforeCheckBuyTop(int type, int cost)
	{
		return GameMoneyHelper.BeforeCheckBuyTop((GameDefine.MONEY_TYPE)type, cost);
	}

	// Token: 0x06003BF5 RID: 15349 RVA: 0x00105D14 File Offset: 0x00103F14
	public static bool BeforeCheckBuyTop(GameDefine.MONEY_TYPE type, int cost)
	{
		if (type == GameDefine.MONEY_TYPE.CASH)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Cash >= (long)cost)
			{
				return true;
			}
		}
		else if (type == GameDefine.MONEY_TYPE.GOLD)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Gold >= (long)cost)
			{
				return true;
			}
		}
		else if (type == GameDefine.MONEY_TYPE.GUILD_CONTRIBUTE)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GuildContribute >= (long)cost)
			{
				return true;
			}
		}
		else if (type == GameDefine.MONEY_TYPE.DIAMOND)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Diamond >= (long)cost)
			{
				return true;
			}
		}
		else if (type == GameDefine.MONEY_TYPE.BATTLECOIN)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.BattleCoin >= (long)cost)
			{
				return true;
			}
		}
		else if (type == GameDefine.MONEY_TYPE.ACTIVITYCOIN && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityCoin >= (long)cost)
		{
			return true;
		}
		if (type == GameDefine.MONEY_TYPE.DIAMOND)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopTopShopRoot);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PopTopDiamondBuyRoot, delegate
			{
				SingletonUnity<PopTopDiamondBuyRootLogic>.Instance.EnableReset();
				ask_shop_list.request request = new ask_shop_list.request();
				request.type = 4L;
				request.subType = 1L;
				NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
				WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
			}, null);
		}
		else if (type == GameDefine.MONEY_TYPE.CASH)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopTopDiamondBuyRoot);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PopTopShopRoot, delegate
			{
				SingletonUnity<PopTopShopRootLogic>.Instance.EnableReset();
				ask_shop_list.request request = new ask_shop_list.request();
				string text = GameMoneyHelper.GetShopMoneyItemID(GameDefine.MONEY_TYPE.CASH);
				if (string.IsNullOrEmpty(text))
				{
					text = "5001";
				}
				ItemData itemDataByID = DataManager.GetItemDataByID(text);
				GameDefine.SHOP_TYPE itemShopType = itemDataByID.GetItemShopType();
				request.type = (long)itemShopType;
				request.itemId = text;
				request.subType = 1L;
				SingletonUnity<PopTopShopRootLogic>.Instance.ShowItemProdect(request.itemId);
				NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
				WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
			}, null);
		}
		else if (type == GameDefine.MONEY_TYPE.GOLD)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopTopDiamondBuyRoot);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PopTopShopRoot, delegate
			{
				SingletonUnity<PopTopShopRootLogic>.Instance.EnableReset();
				ask_shop_list.request request = new ask_shop_list.request();
				ItemData itemDataByID = DataManager.GetItemDataByID("5006");
				GameDefine.SHOP_TYPE itemShopType = itemDataByID.GetItemShopType();
				request.type = (long)itemShopType;
				request.itemId = "5006";
				request.subType = 1L;
				SingletonUnity<PopTopShopRootLogic>.Instance.ShowItemProdect(request.itemId);
				NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
				WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
			}, null);
		}
		if (type == GameDefine.MONEY_TYPE.GUILD_CONTRIBUTE)
		{
			NoticeLogic.AddNotifyData("#{100639}", true, false);
		}
		else if (type == GameDefine.MONEY_TYPE.BATTLECOIN)
		{
			if (GameManager.IsSupportCurDataVersion56())
			{
				NoticeLogic.AddNotifyData("#{100655}", true, false);
			}
			else
			{
				NoticeLogic.AddNotifyData("Battle Coin is not enough!", true, false);
			}
		}
		return false;
	}

	// Token: 0x06003BF6 RID: 15350 RVA: 0x00105F0C File Offset: 0x0010410C
	public static void ShowItemProductTop(string itemid, GameDefine.SHOP_TYPE shoptype = GameDefine.SHOP_TYPE.TOOL_SHOP)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (!playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SHOP))
		{
			int condition = DataManager.GetFunctionDataById(3006.ToString()).Condition;
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100649}", new object[]
			{
				condition
			}), true, false);
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PopTopDiamondBuyRoot);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PopTopShopRoot, delegate
		{
			SingletonUnity<PopTopShopRootLogic>.Instance.EnableReset();
			ask_shop_list.request request = new ask_shop_list.request();
			ItemData itemDataByID = DataManager.GetItemDataByID(itemid);
			shoptype = itemDataByID.GetItemShopType();
			request.type = (long)shoptype;
			request.itemId = itemid;
			request.subType = 1L;
			SingletonUnity<PopTopShopRootLogic>.Instance.ShowItemProdect(request.itemId);
			NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
			WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
		}, null);
	}

	// Token: 0x06003BF7 RID: 15351 RVA: 0x00105FAC File Offset: 0x001041AC
	public static long GetMoneyNum(int curtype)
	{
		if (curtype == 0)
		{
			return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Cash;
		}
		if (curtype == 1)
		{
			return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Gold;
		}
		if (curtype == 3)
		{
			return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GuildContribute;
		}
		if (curtype == 2)
		{
			return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Diamond;
		}
		if (curtype == 4)
		{
			return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.BattleCoin;
		}
		if (curtype == 5)
		{
			return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityCoin;
		}
		return 0L;
	}

	// Token: 0x06003BF8 RID: 15352 RVA: 0x00106048 File Offset: 0x00104248
	public static long GetGold()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Gold;
	}

	// Token: 0x06003BF9 RID: 15353 RVA: 0x0010605C File Offset: 0x0010425C
	public static long GetCash()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Cash;
	}

	// Token: 0x06003BFA RID: 15354 RVA: 0x00106070 File Offset: 0x00104270
	public static long GetDiamond()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Diamond;
	}

	// Token: 0x06003BFB RID: 15355 RVA: 0x00106084 File Offset: 0x00104284
	public static long GetBattleCoin()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.BattleCoin;
	}

	// Token: 0x06003BFC RID: 15356 RVA: 0x00106098 File Offset: 0x00104298
	public static void UpdateMoney(long money1, long money2, long money3, long money4, long money5 = 0L, long money6 = 0L)
	{
		bool flag = false;
		if (GameMoneyHelper.GetCash() != money1 || GameMoneyHelper.GetGold() != money2 || GameMoneyHelper.GetDiamond() != money3)
		{
			flag = true;
		}
		GameMoneyHelper.SetCash(money1);
		GameMoneyHelper.SetGold(money2);
		GameMoneyHelper.SetDiamond(money3);
		GameMoneyHelper.SetGuildContribute(money4);
		GameMoneyHelper.SetBattleCoin(money5);
		GameMoneyHelper.SetActivityCoin(money6);
		if (flag)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.UpdateEnhanceTips();
			if (SingletonUnity<FunctionBtnRootLogic>.Exists)
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateSkillTips();
			}
		}
	}

	// Token: 0x06003BFD RID: 15357 RVA: 0x0010611C File Offset: 0x0010431C
	public static void SetGold(long val)
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SetGold(val);
	}

	// Token: 0x06003BFE RID: 15358 RVA: 0x00106130 File Offset: 0x00104330
	public static void SetCash(long val)
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SetCash(val);
	}

	// Token: 0x06003BFF RID: 15359 RVA: 0x00106144 File Offset: 0x00104344
	public static void SetDiamond(long val)
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SetDiamond(val);
	}

	// Token: 0x06003C00 RID: 15360 RVA: 0x00106158 File Offset: 0x00104358
	public static void SetBattleCoin(long val)
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SetBattleCoin(val);
		if (SingletonUnity<ShopTabRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ShopTabRootLogic>.Instance.gameObject))
		{
			SingletonUnity<ShopTabRootLogic>.Instance.UpdateMoneyLabel();
		}
	}

	// Token: 0x06003C01 RID: 15361 RVA: 0x001061A0 File Offset: 0x001043A0
	public static void SetActivityCoin(long val)
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SetActivityCoin(val);
		if (SingletonUnity<ShopTabRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ShopTabRootLogic>.Instance.gameObject))
		{
			SingletonUnity<ShopTabRootLogic>.Instance.UpdateMoneyLabel();
		}
	}

	// Token: 0x06003C02 RID: 15362 RVA: 0x001061E8 File Offset: 0x001043E8
	public static long GetGuildContribute()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GuildContribute;
	}

	// Token: 0x06003C03 RID: 15363 RVA: 0x001061FC File Offset: 0x001043FC
	public static void SetGuildContribute(long val)
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GuildContribute = val;
		if (SingletonUnity<ShopTabRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ShopTabRootLogic>.Instance.gameObject))
		{
			SingletonUnity<ShopTabRootLogic>.Instance.UpdateMoneyLabel();
		}
		if (SingletonUnity<GuildStarRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildStarRootLogic>.Instance.gameObject))
		{
			SingletonUnity<GuildStarRootLogic>.Instance.UpdateMoneyLabel();
		}
	}

	// Token: 0x06003C04 RID: 15364 RVA: 0x0010626C File Offset: 0x0010446C
	public static string GetMoneyValStr(long val, long type)
	{
		return GameMoneyHelper.GetMoneyValStr((int)val, (GameDefine.MONEY_TYPE)type);
	}

	// Token: 0x06003C05 RID: 15365 RVA: 0x00106278 File Offset: 0x00104478
	public static string GetMoneyValStr(int val, int type)
	{
		return GameMoneyHelper.GetMoneyValStr(val, (GameDefine.MONEY_TYPE)type);
	}

	// Token: 0x06003C06 RID: 15366 RVA: 0x00106284 File Offset: 0x00104484
	public static string GetMoneyValStr(int val, GameDefine.MONEY_TYPE type)
	{
		switch (type)
		{
		case GameDefine.MONEY_TYPE.CASH:
			return string.Format(":$ {0}", val);
		case GameDefine.MONEY_TYPE.GOLD:
			return string.Format(":% {0}", val);
		case GameDefine.MONEY_TYPE.DIAMOND:
			return string.Format(":@ {0}", val);
		case GameDefine.MONEY_TYPE.GUILD_CONTRIBUTE:
			return string.Format(":& {0}", val);
		case GameDefine.MONEY_TYPE.BATTLECOIN:
			return string.Format(":^ {0}", val);
		case GameDefine.MONEY_TYPE.ACTIVITYCOIN:
			return string.Format(":~ {0}", val);
		default:
			return string.Format(":${0}", val);
		}
	}

	// Token: 0x06003C07 RID: 15367 RVA: 0x0010632C File Offset: 0x0010452C
	public static string GetMoneyValStr(int val, string itemId)
	{
		ItemData itemDataByID = DataManager.GetItemDataByID(itemId);
		if (itemDataByID != null)
		{
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.ADD_COIN)
			{
				return GameMoneyHelper.GetMoneyValStr(val, GameDefine.MONEY_TYPE.CASH);
			}
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.ADD_GOLD)
			{
				return GameMoneyHelper.GetMoneyValStr(val, GameDefine.MONEY_TYPE.GOLD);
			}
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.ADD_DIAMOND)
			{
				return GameMoneyHelper.GetMoneyValStr(val, GameDefine.MONEY_TYPE.DIAMOND);
			}
		}
		return string.Empty;
	}

	// Token: 0x06003C08 RID: 15368 RVA: 0x00106388 File Offset: 0x00104588
	public static string GetMoneyIcon(GameDefine.MONEY_TYPE type)
	{
		switch (type)
		{
		case GameDefine.MONEY_TYPE.CASH:
			return "CZ_tuBiao_money";
		case GameDefine.MONEY_TYPE.GOLD:
			return "CZ_tuBiao_Gold";
		case GameDefine.MONEY_TYPE.DIAMOND:
			return "CZ_tuBiao_zuanShi";
		case GameDefine.MONEY_TYPE.GUILD_CONTRIBUTE:
			return "CZ_gongHui_GongHuiHuoBi";
		case GameDefine.MONEY_TYPE.BATTLECOIN:
			return "CZ_duiZhanHuoBi";
		case GameDefine.MONEY_TYPE.ACTIVITYCOIN:
			return "CZ_huoDongHuoBi";
		default:
			return string.Empty;
		}
	}

	// Token: 0x06003C09 RID: 15369 RVA: 0x001063E4 File Offset: 0x001045E4
	public static string GetMoneyIcon(long type)
	{
		GameDefine.MONEY_TYPE type2 = (GameDefine.MONEY_TYPE)type;
		return GameMoneyHelper.GetMoneyIcon(type2);
	}

	// Token: 0x06003C0A RID: 15370 RVA: 0x001063FC File Offset: 0x001045FC
	public static ItemData GetMoneyItemData(GameDefine.MONEY_TYPE type)
	{
		if (GameDefine.ITEM_ID.ContainsKey(type))
		{
			string id = GameDefine.ITEM_ID[type];
			return DataManager.GetItemDataByID(id);
		}
		return null;
	}

	// Token: 0x06003C0B RID: 15371 RVA: 0x00106430 File Offset: 0x00104630
	public static string GetMoneyPre(GameDefine.MONEY_TYPE type)
	{
		switch (type)
		{
		case GameDefine.MONEY_TYPE.CASH:
			return ":$";
		case GameDefine.MONEY_TYPE.GOLD:
			return ":%";
		case GameDefine.MONEY_TYPE.DIAMOND:
			return ":@";
		case GameDefine.MONEY_TYPE.GUILD_CONTRIBUTE:
			return ":&";
		case GameDefine.MONEY_TYPE.BATTLECOIN:
			return ":^";
		case GameDefine.MONEY_TYPE.ACTIVITYCOIN:
			return ":~";
		default:
			return ":$";
		}
	}

	// Token: 0x06003C0C RID: 15372 RVA: 0x0010648C File Offset: 0x0010468C
	public static string GetMoneyPicName(GameDefine.MONEY_TYPE type)
	{
		if (type == GameDefine.MONEY_TYPE.CASH)
		{
			return string.Format("CZ_qian", new object[0]);
		}
		if (type != GameDefine.MONEY_TYPE.GOLD)
		{
			return string.Format("CZ_qian", new object[0]);
		}
		return string.Format("CZ_jinBi", new object[0]);
	}

	// Token: 0x06003C0D RID: 15373 RVA: 0x001064E0 File Offset: 0x001046E0
	public static string GetShopMoneyItemID(GameDefine.MONEY_TYPE type)
	{
		List<ShopData> list = new List<ShopData>();
		List<ShopData> shopDataList = DataManager.GetShopDataList();
		for (int i = 0; i < shopDataList.Count; i++)
		{
			if (shopDataList[i].Shop == 0)
			{
				list.Add(shopDataList[i]);
			}
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (type != GameDefine.MONEY_TYPE.CASH)
		{
			return null;
		}
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j].ItemID.Equals("5001") && playerData.CheckLevel(list[j].MinLevel, list[j].MaxLevel))
			{
				return "5001";
			}
			if (list[j].ItemID.Equals("5002") && playerData.CheckLevel(list[j].MinLevel, list[j].MaxLevel))
			{
				return "5002";
			}
			if (list[j].ItemID.Equals("5003") && playerData.CheckLevel(list[j].MinLevel, list[j].MaxLevel))
			{
				return "5003";
			}
		}
		return "5001";
	}

	// Token: 0x04002733 RID: 10035
	public const int GuildCashCount = 499;

	// Token: 0x04002734 RID: 10036
	public const int GuildDiamondCount = 49;
}
