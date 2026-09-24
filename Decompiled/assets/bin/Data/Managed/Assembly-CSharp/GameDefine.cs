using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001E3 RID: 483
public class GameDefine
{
	// Token: 0x0600114B RID: 4427 RVA: 0x0006EFAC File Offset: 0x0006D1AC
	// Note: this type is marked as 'beforefieldinit'.
	static GameDefine()
	{
		Dictionary<int, string> dictionary = new Dictionary<int, string>();
		dictionary.Add(1, "#{800001}");
		dictionary.Add(2, "#{800002}");
		dictionary.Add(3, "#{800003}");
		dictionary.Add(4, "#{800004}");
		dictionary.Add(5, "#{800005}");
		dictionary.Add(6, "#{800006}");
		dictionary.Add(7, "#{800007}");
		dictionary.Add(8, "#{800008}");
		dictionary.Add(9, "#{800009}");
		dictionary.Add(10, "#{800010}");
		dictionary.Add(11, "#{800011}");
		dictionary.Add(12, "#{800012}");
		GameDefine.STRONGER_TYPE_NAME = dictionary;
		dictionary = new Dictionary<int, string>();
		dictionary.Add(1, "Activity_Point_1");
		dictionary.Add(2, "Activity_Point_2");
		dictionary.Add(3, "Activity_Point_3");
		dictionary.Add(4, "Activity_Point_4");
		dictionary.Add(5, "Activity_Point_5");
		GameDefine.ACTIVITYPOINTNAME = dictionary;
		GameDefine.SingleDanceToolItem = "6001";
		GameDefine.GangDanceToolItem = "6002";
		GameDefine.EnemyWarpToolItem = "5025";
		Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
		dictionary2.Add("6001", "CZ_gaWuDaoJu_geRen");
		dictionary2.Add("6002", "CZ_gaWuDaoJu_gongHui");
		GameDefine.DanceToolName = dictionary2;
		GameDefine.AtkBuffItemId = "9501";
		GameDefine.DefBuffItemId = "9503";
		GameDefine.MovBuffItemId = "9502";
		GameDefine.XD_DefaultModel = new string[]
		{
			"XD_A_WQ",
			"XD_0_T",
			"XD_0_S",
			"XD_0_X"
		};
		GameDefine.QJ_DefaultModel = new string[]
		{
			"QJ_A_WQ",
			"QJ_0_T",
			"QJ_0_S",
			"QJ_0_X"
		};
		GameDefine.NQS_DefaultModel = new string[]
		{
			"NQS_A_WQ",
			"NQS_0_T",
			"NQS_0_S",
			"NQS_0_X"
		};
		GameDefine.XD_NormalModel = new string[]
		{
			"XD_A_WQ",
			"XD_A_T",
			"XD_A_S",
			"XD_A_X"
		};
		GameDefine.QJ_NormalModel = new string[]
		{
			"QJ_A_WQ",
			"QJ_A_T",
			"QJ_A_S",
			"QJ_A_X"
		};
		GameDefine.NQS_NormalModel = new string[]
		{
			"NQS_A_WQ",
			"NQS_A_T",
			"NQS_A_S",
			"NQS_A_X"
		};
		GameDefine.Player_Icon_Pic = new string[]
		{
			"CZ_touXiang_1",
			"CZ_touXiang_2",
			"CZ_touXiang_3"
		};
		GameDefine.Game_Player_Icon_pic = new string[]
		{
			"CZ_xieDouTouXiang",
			"CZ_quanJiTouXiang",
			"CZ_nvQiangTouXiang"
		};
		GameDefine.Player_Icon_Small_Pic = new string[]
		{
			"CZ_touXiang_yuan1",
			"CZ_touXiang_yuan2",
			"CZ_touXiang_yuan3",
			"CZ_touXiang_XiTong"
		};
		GameDefine.Player_Profession_Pic = new string[]
		{
			"CZ_xieDouTouXiang",
			"CZ_quanJiTouXiang",
			"CZ_nvQiangTouXiang"
		};
		GameDefine.TianTi_TuBiao = new string[]
		{
			"CZ_A_tianTi_tuBiao_1",
			"CZ_A_tianTi_tuBiao_2",
			"CZ_A_tianTi_tuBiao_3",
			"CZ_A_tianTi_tuBiao_4",
			"CZ_A_tianTi_tuBiao_5",
			"CZ_A_tianTi_tuBiao_6",
			"CZ_A_tianTi_tuBiao_7",
			"CZ_A_tianTi_tuBiao_8",
			"CZ_A_tianTi_tuBiao_9",
			"CZ_A_tianTi_tuBiao_10"
		};
		GameDefine.MailIcon = new string[]
		{
			"CZ_sheJiao_youJianWeiDuTB",
			"CZ_sheJiao_youJianTB"
		};
		GameDefine.TianTi_AnNiu = new string[]
		{
			"CZ_A_tianTi_sehngJiAnNiuLiang",
			"CZ_A_tianTi_sehngJiAnNiu"
		};
		GameDefine.GuildJobStr = new string[]
		{
			"#{100720}",
			"#{100721}",
			"#{100722}",
			"#{100703}"
		};
		GameDefine.GuildJOb = new string[]
		{
			"BOSS",
			"VICEBOSS",
			"ELDER",
			"MEMBER"
		};
		GameDefine.GuildIcon = new string[]
		{
			"CZ_gongHui_TuBiao1",
			"CZ_gongHui_TuBiao2",
			"CZ_gongHui_TuBiao3",
			"CZ_gongHui_TuBiao4",
			"CZ_gongHui_TuBiao5",
			"CZ_gongHui_TuBiao6",
			"CZ_gongHui_TuBiao7",
			"CZ_gongHui_TuBiao8"
		};
		GameDefine.DonateTipsTB = new string[]
		{
			"CZ_A_HSXZG",
			"CZ_A_jinZhi"
		};
		GameDefine.RefinePartName = new string[]
		{
			"1",
			"#{100908}",
			"#{100909}",
			"#{100910}",
			"#{100911}"
		};
		GameDefine.BtnIcon = new string[]
		{
			"CZ_anNiu_1",
			"CZ_anNiu_2",
			"CZ_anNiu_2+"
		};
		GameDefine.BtnIconNew = new string[]
		{
			"CZ_SY_AnNiu_1",
			"CZ_SY_AnNiu_2"
		};
		GameDefine.RoundName = new string[]
		{
			"#{105002}",
			"#{105003}",
			"#{105004}"
		};
		GameDefine.MISSION_NPC_GROUP_VAL = 9999;
		GameDefine.CopyMissionBestGrade = 2;
		GameDefine.CHANNEL_PRE_WORD = new string[]
		{
			"#{100282}",
			"#{100283}",
			"#{100284}",
			"#{100285}",
			"#{100286}",
			"#{100287}"
		};
		dictionary = new Dictionary<int, string>();
		dictionary.Add(0, "EquipQuality_White");
		dictionary.Add(1, "EquipQuality_Green");
		dictionary.Add(2, "EquipQuality_Blue");
		dictionary.Add(3, "EquipQuality_Purple");
		dictionary.Add(4, "EquipQuality_Orange");
		dictionary.Add(5, "EquipQuality_Red");
		GameDefine.EquipQualityValAddName = dictionary;
		dictionary = new Dictionary<int, string>();
		dictionary.Add(0, "XD");
		dictionary.Add(1, "QJ");
		dictionary.Add(2, "QS");
		GameDefine.WeaponTypeName = dictionary;
		Dictionary<string, int> dictionary3 = new Dictionary<string, int>();
		dictionary3.Add("XD", 0);
		dictionary3.Add("QJ", 1);
		dictionary3.Add("QS", 2);
		GameDefine.NameWeaponType = dictionary3;
		GameDefine.MAX_REFINE_LEVEL = 10;
		GameDefine.MAX_BADGE_LEVEL = 8;
		GameDefine.PLAYER_MAX_LEVEL = 80;
		GameDefine.MAX_FRIENT_COUNT = 100;
		GameDefine.MAX_APPLY_FRIEND_COUNT = 100;
		GameDefine.MAX_ENEMY_COUNT = 30;
		GameDefine.IdleSelectAnimaName = "idle_select";
		GameDefine.ShowSelectAnimaName = "show_select";
		GameDefine.RoleIdleSelectAnimaName = new string[]
		{
			"idle_select",
			"idle",
			"idle_attack"
		};
		GameDefine.RoleShowSelectAnimaName = new string[]
		{
			"show_select",
			"show_select",
			"show_select"
		};
		GameDefine.DailyMissionBoardMapId = "101";
		GameDefine.DailyMissionBoardPos = new Vector3(-25f, 0f, -34.73339f);
		GameDefine.SKILL_MAX_LEVEL = 80;
		GameDefine.RANK_PICNAME = new string[]
		{
			"CZ_A_paiHang_diYiMing",
			"CZ_A_paiHang_diErMing",
			"CZ_A_paiHang_diSanMing"
		};
		GameDefine.BATTLE_WIN_PIC = new string[]
		{
			"CZ_gongHui_HuoShengYiCi",
			"CZ_gongHui_HuoShengErCi",
			"CZ_gongHui_GuanJun"
		};
		GameDefine.Profession_PicName = new string[]
		{
			"CZ_zhiYeTuBiao_1",
			"CZ_zhiYeTuBiao_2",
			"CZ_zhiYeTuBiao_3"
		};
		GameDefine.GrayColor = new Color(0.11764706f, 0.11764706f, 0.11764706f, 1f);
		Dictionary<GameDefine.MONEY_TYPE, string> dictionary4 = new Dictionary<GameDefine.MONEY_TYPE, string>();
		dictionary4.Add(GameDefine.MONEY_TYPE.CASH, "1001");
		dictionary4.Add(GameDefine.MONEY_TYPE.GOLD, "1002");
		dictionary4.Add(GameDefine.MONEY_TYPE.DIAMOND, "1003");
		dictionary4.Add(GameDefine.MONEY_TYPE.GUILD_CONTRIBUTE, "1004");
		GameDefine.ITEM_ID = dictionary4;
		Dictionary<string, GameDefine.MONEY_TYPE> dictionary5 = new Dictionary<string, GameDefine.MONEY_TYPE>();
		dictionary5.Add("1001", GameDefine.MONEY_TYPE.CASH);
		dictionary5.Add("1002", GameDefine.MONEY_TYPE.GOLD);
		dictionary5.Add("1003", GameDefine.MONEY_TYPE.DIAMOND);
		dictionary5.Add("1004", GameDefine.MONEY_TYPE.GUILD_CONTRIBUTE);
		GameDefine.ITEM_ID_MONEYTYPR = dictionary5;
		dictionary = new Dictionary<int, string>();
		dictionary.Add(1001, "CZ_renWuShuXingTuBiao_1");
		dictionary.Add(1002, "CZ_renWuShuXingTuBiao_2");
		dictionary.Add(1003, "CZ_renWuShuXingTuBiao_3");
		dictionary.Add(1004, "CZ_renWuShuXingTuBiao_4");
		dictionary.Add(1005, "CZ_renWuShuXingTuBiao_5");
		dictionary.Add(1006, "CZ_renWuShuXingTuBiao_6");
		dictionary.Add(1007, "CZ_renWuShuXingTuBiao_7");
		dictionary.Add(1008, "CZ_renWuShuXingTuBiao_8");
		dictionary.Add(1009, "CZ_renWuShuXingTuBiao_9");
		dictionary.Add(1010, "CZ_renWuShuXingTuBiao_10");
		dictionary.Add(1011, "CZ_renWuShuXingTuBiao_11");
		dictionary.Add(1012, "CZ_renWuShuXingTuBiao_12");
		dictionary.Add(1013, "CZ_renWuShuXingTuBiao_13");
		GameDefine.ATTRIBUTE_ICON = dictionary;
		Dictionary<int, Color> dictionary6 = new Dictionary<int, Color>();
		dictionary6.Add(0, new Color(0.5882353f, 0.5882353f, 0.5882353f, 1f));
		dictionary6.Add(1, new Color(0.007843138f, 0.6666667f, 0f, 1f));
		dictionary6.Add(2, new Color(0f, 0.5176471f, 1f, 1f));
		dictionary6.Add(3, new Color(0.6784314f, 0f, 1f, 1f));
		dictionary6.Add(4, new Color(1f, 0.7058824f, 0f, 1f));
		dictionary6.Add(5, new Color(1f, 0f, 0f, 1f));
		GameDefine.QUALITY_COLOR = dictionary6;
		dictionary = new Dictionary<int, string>();
		dictionary.Add(0, "StarAttIntegral_White");
		dictionary.Add(1, "StarAttIntegral_Green");
		dictionary.Add(2, "StarAttIntegral_Blue");
		dictionary.Add(3, "StarAttIntegral_Purple");
		dictionary.Add(4, "StarAttIntegral_Orange");
		dictionary.Add(5, "StarAttIntegral_Red");
		GameDefine.StarAttIntegral = dictionary;
		GameDefine.EquipQualityScoreName = new string[]
		{
			"EquipQuality0",
			"EquipQuality1",
			"EquipQuality2",
			"EquipQuality3",
			"EquipQuality4",
			"EquipQuality5"
		};
		GameDefine.EquipStarScoreName = new string[]
		{
			"EquipStar1",
			"EquipStar2",
			"EquipStar3",
			"EquipStar4",
			"EquipStar5",
			"EquipStar6",
			"EquipStar7"
		};
		dictionary = new Dictionary<int, string>();
		dictionary.Add(0, "EquipInherited_White");
		dictionary.Add(1, "EquipInherited_Green");
		dictionary.Add(2, "EquipInherited_Blue");
		dictionary.Add(3, "EquipInherited_Purple");
		dictionary.Add(4, "EquipInherited_Orange");
		dictionary.Add(5, "EquipInherited_Red");
		GameDefine.EquipInherited = dictionary;
		dictionary = new Dictionary<int, string>();
		dictionary.Add(0, "WeaponInherited_White");
		dictionary.Add(1, "WeaponInherited_Green");
		dictionary.Add(2, "WeaponInherited_Blue");
		dictionary.Add(3, "WeaponInherited_Purple");
		dictionary.Add(4, "WeaponInherited_Orange");
		dictionary.Add(5, "WeaponInherited_Red");
		GameDefine.WeaponInherited = dictionary;
		dictionary = new Dictionary<int, string>();
		dictionary.Add(0, "#{100625}");
		dictionary.Add(1, "#{100626}");
		dictionary.Add(2, "#{100627}");
		dictionary.Add(3, "#{100628}");
		dictionary.Add(4, "#{100628}");
		GameDefine.QUALITY_NAME = dictionary;
		dictionary = new Dictionary<int, string>();
		dictionary.Add(0, "#{106019}");
		dictionary.Add(1, "#{106020}");
		dictionary.Add(2, "#{106021}");
		dictionary.Add(3, "#{106022}");
		dictionary.Add(4, "#{106023}");
		dictionary.Add(5, "#{106024}");
		dictionary.Add(6, "#{106025}");
		GameDefine.WEEK_NAME = dictionary;
		dictionary = new Dictionary<int, string>();
		dictionary.Add(1001, "#{100402}");
		dictionary.Add(1002, "#{100403}");
		dictionary.Add(1003, "#{100404}");
		dictionary.Add(1004, "#{100405}");
		dictionary.Add(1005, "#{100406}");
		dictionary.Add(1006, "#{100407}");
		dictionary.Add(1007, "#{100408}");
		dictionary.Add(1008, "#{100409}");
		dictionary.Add(1009, "#{100410}");
		dictionary.Add(1010, "#{100422}");
		dictionary.Add(1011, "#{100420}");
		dictionary.Add(1012, "#{100423}");
		dictionary.Add(1013, "#{100424}");
		GameDefine.ATTRIBUTE_NAME = dictionary;
		dictionary = new Dictionary<int, string>();
		dictionary.Add(2001, "#{100411}");
		dictionary.Add(2002, "#{100412}");
		dictionary.Add(2003, "#{100413}");
		dictionary.Add(2004, "#{100414}");
		dictionary.Add(2005, "#{100415}");
		dictionary.Add(2006, "#{100416}");
		dictionary.Add(2007, "#{100417}");
		dictionary.Add(2008, "#{100418}");
		dictionary.Add(2009, "#{100419}");
		dictionary.Add(2010, "#{100422}");
		dictionary.Add(2011, "#{100420}");
		dictionary.Add(2012, "#{100437}");
		dictionary.Add(2013, "#{100438}");
		GameDefine.ATTRIBUTE_NAME_S = dictionary;
		Dictionary<int, float> dictionary7 = new Dictionary<int, float>();
		dictionary7.Add(1001, 16f);
		dictionary7.Add(1002, 1f);
		dictionary7.Add(1003, 11f);
		dictionary7.Add(1004, 2f);
		dictionary7.Add(1005, 5.5f);
		dictionary7.Add(1006, 10f);
		dictionary7.Add(1007, 10f);
		dictionary7.Add(1008, 5f);
		dictionary7.Add(1009, 5f);
		dictionary7.Add(1012, 5f);
		dictionary7.Add(1013, 5f);
		dictionary7.Add(2001, 5f);
		dictionary7.Add(2002, 5f);
		dictionary7.Add(2003, 5f);
		dictionary7.Add(2004, 5f);
		dictionary7.Add(2005, 5f);
		dictionary7.Add(2006, 5f);
		dictionary7.Add(2007, 5f);
		dictionary7.Add(2008, 5f);
		dictionary7.Add(2009, 5f);
		dictionary7.Add(2012, 5f);
		dictionary7.Add(2013, 5f);
		GameDefine.ATTRIBUTE_COMBAT_VAL_DIC_XD = dictionary7;
		dictionary7 = new Dictionary<int, float>();
		dictionary7.Add(1001, 20f);
		dictionary7.Add(1002, 1f);
		dictionary7.Add(1003, 12f);
		dictionary7.Add(1004, 1f);
		dictionary7.Add(1005, 6f);
		dictionary7.Add(1006, 5f);
		dictionary7.Add(1007, 10f);
		dictionary7.Add(1008, 5f);
		dictionary7.Add(1009, 5f);
		dictionary7.Add(1012, 5f);
		dictionary7.Add(1013, 5f);
		dictionary7.Add(2001, 5f);
		dictionary7.Add(2002, 5f);
		dictionary7.Add(2003, 5f);
		dictionary7.Add(2004, 5f);
		dictionary7.Add(2005, 5f);
		dictionary7.Add(2006, 5f);
		dictionary7.Add(2007, 5f);
		dictionary7.Add(2008, 5f);
		dictionary7.Add(2009, 5f);
		dictionary7.Add(2012, 5f);
		dictionary7.Add(2013, 5f);
		GameDefine.ATTRIBUTE_COMBAT_VAL_DIC_QJ = dictionary7;
		dictionary7 = new Dictionary<int, float>();
		dictionary7.Add(1001, 7f);
		dictionary7.Add(1002, 1f);
		dictionary7.Add(1003, 7.4f);
		dictionary7.Add(1004, 3f);
		dictionary7.Add(1005, 3.7f);
		dictionary7.Add(1006, 15f);
		dictionary7.Add(1007, 10f);
		dictionary7.Add(1008, 5f);
		dictionary7.Add(1009, 5f);
		dictionary7.Add(1012, 5f);
		dictionary7.Add(1013, 5f);
		dictionary7.Add(2001, 5f);
		dictionary7.Add(2002, 5f);
		dictionary7.Add(2003, 5f);
		dictionary7.Add(2004, 5f);
		dictionary7.Add(2005, 5f);
		dictionary7.Add(2006, 5f);
		dictionary7.Add(2007, 5f);
		dictionary7.Add(2008, 5f);
		dictionary7.Add(2009, 5f);
		dictionary7.Add(2012, 5f);
		dictionary7.Add(2013, 5f);
		GameDefine.ATTRIBUTE_COMBAT_VAL_DIC_NQS = dictionary7;
		GameDefine.ProfessionName = new string[]
		{
			"#{100128}",
			"#{100129}",
			"#{100130}"
		};
		GameDefine.WeaponName = new string[]
		{
			"#{100676}",
			"#{100678}",
			"#{100677}"
		};
		dictionary = new Dictionary<int, string>();
		dictionary.Add(-1, "#{200084}");
		dictionary.Add(0, "#{200079}");
		dictionary.Add(1, "#{200081}");
		dictionary.Add(2, "#{200082}");
		GameDefine.SERVER_AREA_NAME = dictionary;
		dictionary6 = new Dictionary<int, Color>();
		dictionary6.Add(0, Color.green);
		dictionary6.Add(1, Color.yellow);
		dictionary6.Add(2, Color.red);
		dictionary6.Add(3, Color.white);
		GameDefine.SERVER_STATE_COLOR = dictionary6;
		dictionary = new Dictionary<int, string>();
		dictionary.Add(1, "#{301107}");
		dictionary.Add(2, "#{301108}");
		dictionary.Add(3, "#{301109}");
		dictionary.Add(4, "#{301110}");
		dictionary.Add(5, "#{301111}");
		dictionary.Add(6, "#{301121}");
		dictionary.Add(7, "#{301122}");
		dictionary.Add(8, "#{301123}");
		GameDefine.SHOP_TAB_NAME = dictionary;
		GameDefine.SHOP_TAB_SORT = new int[]
		{
			3,
			1,
			6,
			7,
			8,
			2,
			4,
			5
		};
		GameDefine.TowerIconName = "CZ_paTa_PVE";
		GameDefine.EmptyBadgeIconName = "CZ_zhuangBeiCao_jia";
		GameDefine.EmptyItemIconName = "CZ_zhuangBeiCao";
		GameDefine.EmptyEquipIconName = "CZ_zhuangBeiCao_jia";
		GameDefine.EmptyAddItemID = "9900";
		GameDefine.GuildStarBG = "CZ_xingPan_BeiJing";
		GameDefine.GuildXingpanDi = "CZ_xingPanDi";
		GameDefine.TextureBannerDaily = "banner_Daily";
		GameDefine.TextureBannerLevel = "banner_Level";
		GameDefine.TextrueBannerInvest = "banner_TouZiJiHua";
		GameDefine.TextureBannerRetrieve = "banner_zhaoHui";
		GameDefine.TestureBannerStronger = "banner_bianQiang";
		GameDefine.TextureBigSale = "CZ_SY_BIG_SALE";
		GameDefine.TextureBigSaleBG = "CZ_SY_BIG_SALE_BG";
		GameDefine.TextureFirstBuy = "CZ_SY_First_RR";
		GameDefine.TextureFirstBuyBG = "CZ_SY_First_RR_BG";
		GameDefine.WorldMap = "CZ_diTu_ShiJie";
		GameDefine.SlotBigWin = "CZ_solt_bigWin";
		GameDefine.SlotBigWinBian = "CZ_solt_bigWinCaiDai";
		GameDefine.LevelRewardBg = "FuBenTuBiao_DANCE";
		GameDefine.CopyBGNameDefault = "DENGLUTU";
		GameDefine.LoadingTips = new string[]
		{
			"#{700001}",
			"#{700002}",
			"#{700003}",
			"#{700004}",
			"#{700005}",
			"#{700006}",
			"#{700007}",
			"#{700008}",
			"#{700009}",
			"#{700010}",
			"#{700011}",
			"#{700012}",
			"#{700001}",
			"#{700006}",
			"#{700015}",
			"#{700016}",
			"#{700017}",
			"#{700018}"
		};
		GameDefine.CASH_ITEM_ID = "1001";
		GameDefine.GOLD_ITEM_ID = "1002";
		GameDefine.DIAMOND_ITEM_ID = "1003";
		GameDefine.XD_INVINCIBLE_SKILL_ID = "199";
		GameDefine.QJ_INVINCIBLE_SKILL_ID = "299";
		GameDefine.NQS_INVINCIBLE_SKILL_ID = "399";
		GameDefine.NPC_SERVER_WAIT_RELIFE_NUM = 5;
		GameDefine.NPC_HP_LINE_SHOW_TIME = 10f;
		GameDefine.SEX_MINI_GAME_ID = "1301";
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x0007055C File Offset: 0x0006E75C
	public static string GetMissionStateIcon(MISSION_STATE state)
	{
		if (state == MISSION_STATE.INVALID)
		{
			return "CZ_renWu_tanHao";
		}
		if (state == MISSION_STATE.ACCEPTED)
		{
			return "CZ_renWu_wenHao_hui";
		}
		if (state == MISSION_STATE.COMPLETE)
		{
			return "CZ_renWu_wenHao";
		}
		return string.Empty;
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x00070598 File Offset: 0x0006E798
	public static string GetObjTypeSpriteName(ObjNPC obj)
	{
		string result = "CZ_diTu_TuBiao_hongDian";
		switch (obj.NPCType)
		{
		case GameDefine.NPC_TYPE.BOSS:
			if (obj.IsCityCaptureNpc())
			{
				result = "CZ_GangDomain_paiMing";
			}
			break;
		case GameDefine.NPC_TYPE.MISSION:
			result = "CZ_diTu_TuBiao_lvDian";
			break;
		case GameDefine.NPC_TYPE.ESCORT:
			if (Singleton<ObjManager>.Instance.IsMyEscortNpc(obj))
			{
				result = "CZ_diTu_TuBiao_lvDian";
			}
			break;
		}
		return result;
	}

	// Token: 0x0600114E RID: 4430 RVA: 0x00070614 File Offset: 0x0006E814
	public static string GetYellowColor(string str)
	{
		return string.Format("[[ffff00]{0}[-]]", str);
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x00070624 File Offset: 0x0006E824
	public static string GetYellowColor(int value)
	{
		return string.Format("[[ffff00]{0}[-]]", value);
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x00070638 File Offset: 0x0006E838
	public static string GetBoldStr(string str)
	{
		return string.Format("[b]{0}[-]", str);
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x00070648 File Offset: 0x0006E848
	public static string GetWeaponName(string WeaponModelName)
	{
		if (!string.IsNullOrEmpty(WeaponModelName))
		{
			string[] array = WeaponModelName.Split(new char[]
			{
				'_'
			});
			if (array != null && array.Length > 0)
			{
				if (array[0].Equals("XD"))
				{
					return "XD";
				}
				if (array[0].Equals("QJ"))
				{
					return "QJ";
				}
				if (array[0].Equals("NQS"))
				{
					return "QS";
				}
			}
		}
		return null;
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x000706CC File Offset: 0x0006E8CC
	public static int GetWeaponType(string WeaponModelName)
	{
		if (!string.IsNullOrEmpty(WeaponModelName))
		{
			string[] array = WeaponModelName.Split(new char[]
			{
				'_'
			});
			if (array != null && array.Length > 0)
			{
				if (array[0].Equals("XD"))
				{
					return 0;
				}
				if (array[0].Equals("QJ"))
				{
					return 1;
				}
				if (array[0].Equals("NQS"))
				{
					return 2;
				}
			}
		}
		return -1;
	}

	// Token: 0x06001153 RID: 4435 RVA: 0x00070744 File Offset: 0x0006E944
	public static string GetRoleIdelSelectName(int per, int weapon)
	{
		switch (weapon)
		{
		case 0:
			return GameDefine.RoleIdleSelectAnimaName[0];
		case 1:
			return GameDefine.RoleIdleSelectAnimaName[2];
		case 2:
			if (per == 2)
			{
				return GameDefine.RoleIdleSelectAnimaName[1];
			}
			return GameDefine.RoleIdleSelectAnimaName[2];
		default:
			return GameDefine.RoleIdleSelectAnimaName[1];
		}
	}

	// Token: 0x06001154 RID: 4436 RVA: 0x00070798 File Offset: 0x0006E998
	public static string GetAttributeName(int type)
	{
		if (type > 2000)
		{
			type -= 1000;
		}
		if (GameDefine.ATTRIBUTE_NAME.ContainsKey(type))
		{
			return StrDictionary.GetDictionaryString(GameDefine.ATTRIBUTE_NAME[type], new object[0]);
		}
		return string.Empty;
	}

	// Token: 0x06001155 RID: 4437 RVA: 0x000707E8 File Offset: 0x0006E9E8
	public static string GetAttributeName_S(int type)
	{
		if (type > 3000)
		{
			type -= 1000;
		}
		if (type < 2000)
		{
			type += 1000;
		}
		if (GameDefine.ATTRIBUTE_NAME_S.ContainsKey(type))
		{
			return StrDictionary.GetDictionaryString(GameDefine.ATTRIBUTE_NAME_S[type], new object[0]);
		}
		return string.Empty;
	}

	// Token: 0x06001156 RID: 4438 RVA: 0x0007084C File Offset: 0x0006EA4C
	public static string GetAttributeIcon(int type)
	{
		if (type > 2000)
		{
			type -= 1000;
		}
		if (GameDefine.ATTRIBUTE_ICON.ContainsKey(type))
		{
			return GameDefine.ATTRIBUTE_ICON[type];
		}
		return string.Empty;
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x00070884 File Offset: 0x0006EA84
	public static string GetAttributeValueStr(int type, int value)
	{
		int num = type;
		if (num > 2000)
		{
			num -= 1000;
		}
		if (!GameDefine.ATTRIBUTE_NAME.ContainsKey(num))
		{
			return string.Empty;
		}
		if (type > 2000 || type == 1008 || type == 1009 || type == 1012 || type == 1013)
		{
			return string.Format("+{0:P1}", (float)value / 10000f);
		}
		return string.Format("+{0}", value);
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x0007091C File Offset: 0x0006EB1C
	public static string GetAttributeValueStr2(int type, int value)
	{
		int num = type;
		if (num > 2000)
		{
			num -= 1000;
		}
		if (!GameDefine.ATTRIBUTE_NAME.ContainsKey(num))
		{
			return string.Empty;
		}
		if (type > 2000 || type == 1008 || type == 1009 || type == 1012 || type == 1013)
		{
			return string.Format("{0:P1}", (float)value / 10000f);
		}
		return string.Format("{0}", value);
	}

	// Token: 0x06001159 RID: 4441 RVA: 0x000709B4 File Offset: 0x0006EBB4
	public static Color GetColorByQuality(EQUIP_QUALITY quality)
	{
		return GameDefine.GetColorByQuality((int)quality);
	}

	// Token: 0x0600115A RID: 4442 RVA: 0x000709CC File Offset: 0x0006EBCC
	public static string GetStrByQuality(EQUIP_QUALITY quality)
	{
		return GameDefine.GetStrByQuality((int)quality);
	}

	// Token: 0x0600115B RID: 4443 RVA: 0x000709E4 File Offset: 0x0006EBE4
	public static string GetStrByQuality(int quality)
	{
		if (GameDefine.QUALITY_NAME.ContainsKey(quality))
		{
			return StrDictionary.GetDictionaryString(GameDefine.QUALITY_NAME[quality], new object[0]);
		}
		return string.Empty;
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x00070A20 File Offset: 0x0006EC20
	public static Color GetColorByQuality(int quality)
	{
		if (GameDefine.QUALITY_COLOR.ContainsKey(quality))
		{
			return GameDefine.QUALITY_COLOR[quality];
		}
		return Color.white;
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x00070A44 File Offset: 0x0006EC44
	public static float GET_ATTRIBUTE_COMBAT_VAL(int attid)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		switch (playerData.Profession)
		{
		case PROFESSION_TYPE.XD:
			if (GameDefine.ATTRIBUTE_COMBAT_VAL_DIC_XD.ContainsKey(attid))
			{
				return GameDefine.ATTRIBUTE_COMBAT_VAL_DIC_XD[attid];
			}
			break;
		case PROFESSION_TYPE.QJ:
			if (GameDefine.ATTRIBUTE_COMBAT_VAL_DIC_QJ.ContainsKey(attid))
			{
				return GameDefine.ATTRIBUTE_COMBAT_VAL_DIC_QJ[attid];
			}
			break;
		case PROFESSION_TYPE.NQS:
			if (GameDefine.ATTRIBUTE_COMBAT_VAL_DIC_NQS.ContainsKey(attid))
			{
				return GameDefine.ATTRIBUTE_COMBAT_VAL_DIC_NQS[attid];
			}
			break;
		}
		return 0f;
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x00070AE4 File Offset: 0x0006ECE4
	public static string GetActKey_FirstClick(GameDefine.ACTIVITY_TYPE type)
	{
		return string.Format("ACTIVITY{0}", (int)type);
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x00070AF8 File Offset: 0x0006ECF8
	public static string GetCopyKey_FirstClick(MAPTYPE type)
	{
		return string.Format("COPY{0}", (int)type);
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x00070B0C File Offset: 0x0006ED0C
	public static float GetSkillTypeValue(EffInfoData data, GameDefine.OBJ_TYPE objType, SKILL_ADD_TYPE type)
	{
		if (objType == GameDefine.OBJ_TYPE.OBJ_NPC)
		{
			return 0f;
		}
		float num = 0f;
		if (data.AddType1 == (int)type)
		{
			num += (float)data.AddValue1;
		}
		if (data.AddType2 == (int)type)
		{
			num += (float)data.AddValue2;
		}
		if (data.AddType3 == (int)type)
		{
			num += (float)data.AddValue3;
		}
		if (data.AddType4 == (int)type)
		{
			num += (float)data.AddValue4;
		}
		return num / 10000f;
	}

	// Token: 0x040014C4 RID: 5316
	public const int SKILL_DRAG_LEVEL = 25;

	// Token: 0x040014C5 RID: 5317
	public const int MAX_TEAM_MEMBER = 4;

	// Token: 0x040014C6 RID: 5318
	public const float SELL_ITEM_MUTI_NUM = 1f;

	// Token: 0x040014C7 RID: 5319
	public const string POINTLOCKNAME = "Activity_Point_6";

	// Token: 0x040014C8 RID: 5320
	public const string LOCKFLAGNAME = "CZ_effect_suo";

	// Token: 0x040014C9 RID: 5321
	public const string DOORITEMNAME = "City_jinRu";

	// Token: 0x040014CA RID: 5322
	public const string ICON_EMPTY_NAME = "CZ_zhuangBeiCao";

	// Token: 0x040014CB RID: 5323
	public const float CONSIGN_SELL_LOW = 0.5f;

	// Token: 0x040014CC RID: 5324
	public const float CONSIGN_SELL_HEIGHT = 1.5f;

	// Token: 0x040014CD RID: 5325
	public const string NORMAL_BUTTON_SPRITE_NAME = "CZ_anNiu_2";

	// Token: 0x040014CE RID: 5326
	public const string GRAY_BUTTON_SPRITE_NAME = "CZ_anNiu_2+";

	// Token: 0x040014CF RID: 5327
	public const string CASH_GET_ITEMID = "5001";

	// Token: 0x040014D0 RID: 5328
	public const string CASH_GET_ITEMID1 = "5002";

	// Token: 0x040014D1 RID: 5329
	public const string CASH_GET_ITEMID2 = "5003";

	// Token: 0x040014D2 RID: 5330
	public const string GOLD_GET_ITEMID = "5006";

	// Token: 0x040014D3 RID: 5331
	public const string PVP_NAME = "#{100125}";

	// Token: 0x040014D4 RID: 5332
	public const string PVE_NAME = "#{100124}";

	// Token: 0x040014D5 RID: 5333
	public const string GANG_NAME = "#{100114}";

	// Token: 0x040014D6 RID: 5334
	public const string ENHANCE_ID = "3001";

	// Token: 0x040014D7 RID: 5335
	public const string REFINE_ID1 = "4001";

	// Token: 0x040014D8 RID: 5336
	public const string REFINE_ID2 = "4002";

	// Token: 0x040014D9 RID: 5337
	public const string REFINE_Safty = "4003";

	// Token: 0x040014DA RID: 5338
	public const string HOUNR_ID = "2002";

	// Token: 0x040014DB RID: 5339
	public const string POTION_ITEMID1 = "9001";

	// Token: 0x040014DC RID: 5340
	public const string POTION_ITEMID2 = "9002";

	// Token: 0x040014DD RID: 5341
	public const string POTION_ITEMID3 = "9003";

	// Token: 0x040014DE RID: 5342
	public const string EXP_ITEM3 = "5013";

	// Token: 0x040014DF RID: 5343
	public const string SecondCarId = "1002";

	// Token: 0x040014E0 RID: 5344
	public const string WeaponPackID = "4";

	// Token: 0x040014E1 RID: 5345
	public const string Badge_Pack_ITEM = "9602";

	// Token: 0x040014E2 RID: 5346
	public const string Speaker_ITEM = "5026";

	// Token: 0x040014E3 RID: 5347
	public const string Buy_Badge_Tutorial_Id = "9999";

	// Token: 0x040014E4 RID: 5348
	public const string CashCopyTicket = "9203";

	// Token: 0x040014E5 RID: 5349
	public const string ExpCopuTicket = "9202";

	// Token: 0x040014E6 RID: 5350
	public const string EQUIPCOPYID = "901";

	// Token: 0x040014E7 RID: 5351
	public const string EXPCOPYID = "223";

	// Token: 0x040014E8 RID: 5352
	public const string CASHCOPYID = "201";

	// Token: 0x040014E9 RID: 5353
	public const string CARCOPYID = "211";

	// Token: 0x040014EA RID: 5354
	public const string SCUFFLE_AREA_ID = "1201";

	// Token: 0x040014EB RID: 5355
	public const string ATTACKESCORTID = "30002";

	// Token: 0x040014EC RID: 5356
	public const string ESCORTID = "30001";

	// Token: 0x040014ED RID: 5357
	public const string WILDBOSSID = "801";

	// Token: 0x040014EE RID: 5358
	public const string SURVIVE = "1101";

	// Token: 0x040014EF RID: 5359
	public const string BigSaleCarID = "2";

	// Token: 0x040014F0 RID: 5360
	public const string ReNameCardID = "5024";

	// Token: 0x040014F1 RID: 5361
	public static int BUF_USE_SUCCESS = -2;

	// Token: 0x040014F2 RID: 5362
	public static int BUF_USE_FAIL = -1;

	// Token: 0x040014F3 RID: 5363
	public static int AutoReDownloadTimes = 3;

	// Token: 0x040014F4 RID: 5364
	public static int DayCentiSecond = 8640000;

	// Token: 0x040014F5 RID: 5365
	public static int NotifyTime1 = 600;

	// Token: 0x040014F6 RID: 5366
	public static string GameName = "#{200405}";

	// Token: 0x040014F7 RID: 5367
	public static string[] MAP_ACTIVITY_ICON = new string[]
	{
		string.Empty,
		"CZ_xianShiRenWu",
		"CZ_xianShiRenWu",
		"CZ_xianShiRenWu",
		"CZ_xianShiRenWu",
		"CZ_xianShiRenWu",
		"CZ_xianShiRenWu",
		"CZ_xianShiRenWu",
		"CZ_riChangRenWu",
		"CZ_riChangRenWu",
		"CZ_riChangRenWu",
		"CZ_riChangRenWu"
	};

	// Token: 0x040014F8 RID: 5368
	public static string[] MAP_ACTIVITY_MISSION_ICON = new string[]
	{
		"CZ_riHuanRenWu",
		"CZ_zhuXianRenWu",
		"CZ_zhiXianRenWu",
		"CZ_riHuanRenWu",
		"CZ_xianShiRenWu",
		"CZ_xianShiRenWu",
		"CZ_riHuanRenWu",
		"CZ_riHuanRenWu",
		"CZ_lianXianRenWu"
	};

	// Token: 0x040014F9 RID: 5369
	public static string LOGIN_SCENE_NAME = "Login";

	// Token: 0x040014FA RID: 5370
	public static Dictionary<int, string> STRONGER_TYPE_NAME;

	// Token: 0x040014FB RID: 5371
	public static Dictionary<int, string> ACTIVITYPOINTNAME;

	// Token: 0x040014FC RID: 5372
	public static string SingleDanceToolItem;

	// Token: 0x040014FD RID: 5373
	public static string GangDanceToolItem;

	// Token: 0x040014FE RID: 5374
	public static string EnemyWarpToolItem;

	// Token: 0x040014FF RID: 5375
	public static Dictionary<string, string> DanceToolName;

	// Token: 0x04001500 RID: 5376
	public static string AtkBuffItemId;

	// Token: 0x04001501 RID: 5377
	public static string DefBuffItemId;

	// Token: 0x04001502 RID: 5378
	public static string MovBuffItemId;

	// Token: 0x04001503 RID: 5379
	public static string[] XD_DefaultModel;

	// Token: 0x04001504 RID: 5380
	public static string[] QJ_DefaultModel;

	// Token: 0x04001505 RID: 5381
	public static string[] NQS_DefaultModel;

	// Token: 0x04001506 RID: 5382
	public static string[] XD_NormalModel;

	// Token: 0x04001507 RID: 5383
	public static string[] QJ_NormalModel;

	// Token: 0x04001508 RID: 5384
	public static string[] NQS_NormalModel;

	// Token: 0x04001509 RID: 5385
	public static string[] Player_Icon_Pic;

	// Token: 0x0400150A RID: 5386
	public static string[] Game_Player_Icon_pic;

	// Token: 0x0400150B RID: 5387
	public static string[] Player_Icon_Small_Pic;

	// Token: 0x0400150C RID: 5388
	public static string[] Player_Profession_Pic;

	// Token: 0x0400150D RID: 5389
	public static string[] TianTi_TuBiao;

	// Token: 0x0400150E RID: 5390
	public static string[] MailIcon;

	// Token: 0x0400150F RID: 5391
	public static string[] TianTi_AnNiu;

	// Token: 0x04001510 RID: 5392
	public static string[] GuildJobStr;

	// Token: 0x04001511 RID: 5393
	public static string[] GuildJOb;

	// Token: 0x04001512 RID: 5394
	public static string[] GuildIcon;

	// Token: 0x04001513 RID: 5395
	public static string[] DonateTipsTB;

	// Token: 0x04001514 RID: 5396
	public static string[] RefinePartName;

	// Token: 0x04001515 RID: 5397
	public static string[] BtnIcon;

	// Token: 0x04001516 RID: 5398
	public static string[] BtnIconNew;

	// Token: 0x04001517 RID: 5399
	public static string[] RoundName;

	// Token: 0x04001518 RID: 5400
	public static int MISSION_NPC_GROUP_VAL;

	// Token: 0x04001519 RID: 5401
	public static int CopyMissionBestGrade;

	// Token: 0x0400151A RID: 5402
	public static string[] CHANNEL_PRE_WORD;

	// Token: 0x0400151B RID: 5403
	public static Dictionary<int, string> EquipQualityValAddName;

	// Token: 0x0400151C RID: 5404
	public static Dictionary<int, string> WeaponTypeName;

	// Token: 0x0400151D RID: 5405
	public static Dictionary<string, int> NameWeaponType;

	// Token: 0x0400151E RID: 5406
	public static int MAX_REFINE_LEVEL;

	// Token: 0x0400151F RID: 5407
	public static int MAX_BADGE_LEVEL;

	// Token: 0x04001520 RID: 5408
	public static int PLAYER_MAX_LEVEL;

	// Token: 0x04001521 RID: 5409
	public static int MAX_FRIENT_COUNT;

	// Token: 0x04001522 RID: 5410
	public static int MAX_APPLY_FRIEND_COUNT;

	// Token: 0x04001523 RID: 5411
	public static int MAX_ENEMY_COUNT;

	// Token: 0x04001524 RID: 5412
	public static string IdleSelectAnimaName;

	// Token: 0x04001525 RID: 5413
	public static string ShowSelectAnimaName;

	// Token: 0x04001526 RID: 5414
	public static string[] RoleIdleSelectAnimaName;

	// Token: 0x04001527 RID: 5415
	public static string[] RoleShowSelectAnimaName;

	// Token: 0x04001528 RID: 5416
	public static string DailyMissionBoardMapId;

	// Token: 0x04001529 RID: 5417
	public static Vector3 DailyMissionBoardPos;

	// Token: 0x0400152A RID: 5418
	public static int SKILL_MAX_LEVEL;

	// Token: 0x0400152B RID: 5419
	public static string[] RANK_PICNAME;

	// Token: 0x0400152C RID: 5420
	public static string[] BATTLE_WIN_PIC;

	// Token: 0x0400152D RID: 5421
	public static string[] Profession_PicName;

	// Token: 0x0400152E RID: 5422
	public static Color GrayColor;

	// Token: 0x0400152F RID: 5423
	public static Dictionary<GameDefine.MONEY_TYPE, string> ITEM_ID;

	// Token: 0x04001530 RID: 5424
	public static Dictionary<string, GameDefine.MONEY_TYPE> ITEM_ID_MONEYTYPR;

	// Token: 0x04001531 RID: 5425
	public static Dictionary<int, string> ATTRIBUTE_ICON;

	// Token: 0x04001532 RID: 5426
	public static Dictionary<int, Color> QUALITY_COLOR;

	// Token: 0x04001533 RID: 5427
	public static Dictionary<int, string> StarAttIntegral;

	// Token: 0x04001534 RID: 5428
	public static string[] EquipQualityScoreName;

	// Token: 0x04001535 RID: 5429
	public static string[] EquipStarScoreName;

	// Token: 0x04001536 RID: 5430
	public static Dictionary<int, string> EquipInherited;

	// Token: 0x04001537 RID: 5431
	public static Dictionary<int, string> WeaponInherited;

	// Token: 0x04001538 RID: 5432
	public static Dictionary<int, string> QUALITY_NAME;

	// Token: 0x04001539 RID: 5433
	public static Dictionary<int, string> WEEK_NAME;

	// Token: 0x0400153A RID: 5434
	public static Dictionary<int, string> ATTRIBUTE_NAME;

	// Token: 0x0400153B RID: 5435
	public static Dictionary<int, string> ATTRIBUTE_NAME_S;

	// Token: 0x0400153C RID: 5436
	public static Dictionary<int, float> ATTRIBUTE_COMBAT_VAL_DIC_XD;

	// Token: 0x0400153D RID: 5437
	public static Dictionary<int, float> ATTRIBUTE_COMBAT_VAL_DIC_QJ;

	// Token: 0x0400153E RID: 5438
	public static Dictionary<int, float> ATTRIBUTE_COMBAT_VAL_DIC_NQS;

	// Token: 0x0400153F RID: 5439
	public static string[] ProfessionName;

	// Token: 0x04001540 RID: 5440
	public static string[] WeaponName;

	// Token: 0x04001541 RID: 5441
	public static Dictionary<int, string> SERVER_AREA_NAME;

	// Token: 0x04001542 RID: 5442
	public static Dictionary<int, Color> SERVER_STATE_COLOR;

	// Token: 0x04001543 RID: 5443
	public static Dictionary<int, string> SHOP_TAB_NAME;

	// Token: 0x04001544 RID: 5444
	public static int[] SHOP_TAB_SORT;

	// Token: 0x04001545 RID: 5445
	public static string TowerIconName;

	// Token: 0x04001546 RID: 5446
	public static string EmptyBadgeIconName;

	// Token: 0x04001547 RID: 5447
	public static string EmptyItemIconName;

	// Token: 0x04001548 RID: 5448
	public static string EmptyEquipIconName;

	// Token: 0x04001549 RID: 5449
	public static string EmptyAddItemID;

	// Token: 0x0400154A RID: 5450
	public static string GuildStarBG;

	// Token: 0x0400154B RID: 5451
	public static string GuildXingpanDi;

	// Token: 0x0400154C RID: 5452
	public static string TextureBannerDaily;

	// Token: 0x0400154D RID: 5453
	public static string TextureBannerLevel;

	// Token: 0x0400154E RID: 5454
	public static string TextrueBannerInvest;

	// Token: 0x0400154F RID: 5455
	public static string TextureBannerRetrieve;

	// Token: 0x04001550 RID: 5456
	public static string TestureBannerStronger;

	// Token: 0x04001551 RID: 5457
	public static string TextureBigSale;

	// Token: 0x04001552 RID: 5458
	public static string TextureBigSaleBG;

	// Token: 0x04001553 RID: 5459
	public static string TextureFirstBuy;

	// Token: 0x04001554 RID: 5460
	public static string TextureFirstBuyBG;

	// Token: 0x04001555 RID: 5461
	public static string WorldMap;

	// Token: 0x04001556 RID: 5462
	public static string SlotBigWin;

	// Token: 0x04001557 RID: 5463
	public static string SlotBigWinBian;

	// Token: 0x04001558 RID: 5464
	public static string LevelRewardBg;

	// Token: 0x04001559 RID: 5465
	public static string CopyBGNameDefault;

	// Token: 0x0400155A RID: 5466
	public static string[] LoadingTips;

	// Token: 0x0400155B RID: 5467
	public static string CASH_ITEM_ID;

	// Token: 0x0400155C RID: 5468
	public static string GOLD_ITEM_ID;

	// Token: 0x0400155D RID: 5469
	public static string DIAMOND_ITEM_ID;

	// Token: 0x0400155E RID: 5470
	public static string XD_INVINCIBLE_SKILL_ID;

	// Token: 0x0400155F RID: 5471
	public static string QJ_INVINCIBLE_SKILL_ID;

	// Token: 0x04001560 RID: 5472
	public static string NQS_INVINCIBLE_SKILL_ID;

	// Token: 0x04001561 RID: 5473
	public static int NPC_SERVER_WAIT_RELIFE_NUM;

	// Token: 0x04001562 RID: 5474
	public static float NPC_HP_LINE_SHOW_TIME;

	// Token: 0x04001563 RID: 5475
	public static string SEX_MINI_GAME_ID;

	// Token: 0x020001E4 RID: 484
	public enum OBJ_TYPE
	{
		// Token: 0x04001565 RID: 5477
		OBJ_NPC,
		// Token: 0x04001566 RID: 5478
		OBJ_MAIN_PLAYER,
		// Token: 0x04001567 RID: 5479
		OBJ_OTHER_PLAYER,
		// Token: 0x04001568 RID: 5480
		OBJ_ZOMBIE_PLAYER,
		// Token: 0x04001569 RID: 5481
		OBJ_DROP_ITEM,
		// Token: 0x0400156A RID: 5482
		OBJ_COLLECT_ITEM,
		// Token: 0x0400156B RID: 5483
		OBJ_PLAYER_CAR,
		// Token: 0x0400156C RID: 5484
		OBJ_NPC_CAR,
		// Token: 0x0400156D RID: 5485
		OBJ_ZOMBIE_RAGDOLL
	}

	// Token: 0x020001E5 RID: 485
	public enum ITEM_TYPE
	{
		// Token: 0x0400156F RID: 5487
		INVALID = -1,
		// Token: 0x04001570 RID: 5488
		ITEM,
		// Token: 0x04001571 RID: 5489
		EQUIP = 2,
		// Token: 0x04001572 RID: 5490
		POTION,
		// Token: 0x04001573 RID: 5491
		ENHANCE_ITEM,
		// Token: 0x04001574 RID: 5492
		MISSION_ITEM,
		// Token: 0x04001575 RID: 5493
		BADGE,
		// Token: 0x04001576 RID: 5494
		ADD_COIN,
		// Token: 0x04001577 RID: 5495
		ADD_GOLD,
		// Token: 0x04001578 RID: 5496
		ADD_DIAMOND,
		// Token: 0x04001579 RID: 5497
		ADD_HONOR,
		// Token: 0x0400157A RID: 5498
		ADD_EXP,
		// Token: 0x0400157B RID: 5499
		DKP,
		// Token: 0x0400157C RID: 5500
		SHOW,
		// Token: 0x0400157D RID: 5501
		RES,
		// Token: 0x0400157E RID: 5502
		BOX,
		// Token: 0x0400157F RID: 5503
		FASHION_EQUIP,
		// Token: 0x04001580 RID: 5504
		SWIPE,
		// Token: 0x04001581 RID: 5505
		REMAIN,
		// Token: 0x04001582 RID: 5506
		EXCHANGE,
		// Token: 0x04001583 RID: 5507
		LOCK1,
		// Token: 0x04001584 RID: 5508
		LOCK2,
		// Token: 0x04001585 RID: 5509
		BUFF,
		// Token: 0x04001586 RID: 5510
		SCORE,
		// Token: 0x04001587 RID: 5511
		RENAME = 26,
		// Token: 0x04001588 RID: 5512
		POTION_2,
		// Token: 0x04001589 RID: 5513
		DANCE_TOOL,
		// Token: 0x0400158A RID: 5514
		ENEMYWARP_TOOL,
		// Token: 0x0400158B RID: 5515
		WORLDSPEAK
	}

	// Token: 0x020001E6 RID: 486
	public enum MONEY_TYPE
	{
		// Token: 0x0400158D RID: 5517
		CASH,
		// Token: 0x0400158E RID: 5518
		GOLD,
		// Token: 0x0400158F RID: 5519
		DIAMOND,
		// Token: 0x04001590 RID: 5520
		GUILD_CONTRIBUTE,
		// Token: 0x04001591 RID: 5521
		BATTLECOIN,
		// Token: 0x04001592 RID: 5522
		ACTIVITYCOIN
	}

	// Token: 0x020001E7 RID: 487
	public enum SHOP_TYPE
	{
		// Token: 0x04001594 RID: 5524
		TOOL_SHOP,
		// Token: 0x04001595 RID: 5525
		EQUIP_SHOP,
		// Token: 0x04001596 RID: 5526
		BIGSALE_SHOP,
		// Token: 0x04001597 RID: 5527
		GUILD_SHOP,
		// Token: 0x04001598 RID: 5528
		DOLLAR_SHOP,
		// Token: 0x04001599 RID: 5529
		VIP_SHOP,
		// Token: 0x0400159A RID: 5530
		BATTLECOIN_SHOP,
		// Token: 0x0400159B RID: 5531
		ACTIVITY_SHOP
	}

	// Token: 0x020001E8 RID: 488
	public enum ANIMATIONSTATE
	{
		// Token: 0x0400159D RID: 5533
		IDLE,
		// Token: 0x0400159E RID: 5534
		RUN,
		// Token: 0x0400159F RID: 5535
		WALK,
		// Token: 0x040015A0 RID: 5536
		DIE,
		// Token: 0x040015A1 RID: 5537
		IDLE_ATTACK,
		// Token: 0x040015A2 RID: 5538
		DRIVING
	}

	// Token: 0x020001E9 RID: 489
	public enum ACTIVITY_TYPE
	{
		// Token: 0x040015A4 RID: 5540
		INVALID = -1,
		// Token: 0x040015A5 RID: 5541
		ALL,
		// Token: 0x040015A6 RID: 5542
		ESCORT,
		// Token: 0x040015A7 RID: 5543
		ATTACK_ESCORT,
		// Token: 0x040015A8 RID: 5544
		CITY_DANCE,
		// Token: 0x040015A9 RID: 5545
		BAR_FIGHT,
		// Token: 0x040015AA RID: 5546
		WILD_BOSS,
		// Token: 0x040015AB RID: 5547
		GUILD_BOSS,
		// Token: 0x040015AC RID: 5548
		SURVIVE_BATTLE,
		// Token: 0x040015AD RID: 5549
		SEX_MINI,
		// Token: 0x040015AE RID: 5550
		GUILD_BATTLE,
		// Token: 0x040015AF RID: 5551
		DAILY_COPY,
		// Token: 0x040015B0 RID: 5552
		MISSION,
		// Token: 0x040015B1 RID: 5553
		TOWER,
		// Token: 0x040015B2 RID: 5554
		RANKPVP,
		// Token: 0x040015B3 RID: 5555
		DOMIN,
		// Token: 0x040015B4 RID: 5556
		FIRST_GUILD_DANCE,
		// Token: 0x040015B5 RID: 5557
		GUILD_DANCE,
		// Token: 0x040015B6 RID: 5558
		MISSION_TIMEOUT,
		// Token: 0x040015B7 RID: 5559
		GUILD_DONMINE,
		// Token: 0x040015B8 RID: 5560
		GUILD_DONMINE_RES,
		// Token: 0x040015B9 RID: 5561
		KILL_PLAYER,
		// Token: 0x040015BA RID: 5562
		SHOP_GATE,
		// Token: 0x040015BB RID: 5563
		COUNT
	}

	// Token: 0x020001EA RID: 490
	public enum DANCE_TYPE
	{
		// Token: 0x040015BD RID: 5565
		SINGLE,
		// Token: 0x040015BE RID: 5566
		SINGLE_TOOL,
		// Token: 0x040015BF RID: 5567
		GANG,
		// Token: 0x040015C0 RID: 5568
		GANG_TOOL
	}

	// Token: 0x020001EB RID: 491
	public enum ACTIVITY_STATE
	{
		// Token: 0x040015C2 RID: 5570
		NORMAL,
		// Token: 0x040015C3 RID: 5571
		LEVEL_LIMIT,
		// Token: 0x040015C4 RID: 5572
		TIME_LIMIT,
		// Token: 0x040015C5 RID: 5573
		TIMESNUM_LIMIT,
		// Token: 0x040015C6 RID: 5574
		BARFIGHT_NOSIGN,
		// Token: 0x040015C7 RID: 5575
		TIME_SIGN_LIMIT,
		// Token: 0x040015C8 RID: 5576
		LEVEL_BELOW_LIMIT,
		// Token: 0x040015C9 RID: 5577
		LEVEL_HEIGH_LIMIT
	}

	// Token: 0x020001EC RID: 492
	public enum CAMP_TYPE
	{
		// Token: 0x040015CB RID: 5579
		PLAYER_1,
		// Token: 0x040015CC RID: 5580
		PLAYER_2,
		// Token: 0x040015CD RID: 5581
		NORMAL_NPC,
		// Token: 0x040015CE RID: 5582
		NPC_ATTACK_NPC,
		// Token: 0x040015CF RID: 5583
		STATIC_NPC,
		// Token: 0x040015D0 RID: 5584
		FUNCTION_NPC,
		// Token: 0x040015D1 RID: 5585
		PLAYER_FRIEND_NPC,
		// Token: 0x040015D2 RID: 5586
		COUNT
	}

	// Token: 0x020001ED RID: 493
	public enum NPC_FUNCTION_TYPE
	{
		// Token: 0x040015D4 RID: 5588
		NORMAL,
		// Token: 0x040015D5 RID: 5589
		SHIFT,
		// Token: 0x040015D6 RID: 5590
		CITIZEN_NPC,
		// Token: 0x040015D7 RID: 5591
		SOUND_BOX,
		// Token: 0x040015D8 RID: 5592
		RECHARGE,
		// Token: 0x040015D9 RID: 5593
		TOOL_SHOP,
		// Token: 0x040015DA RID: 5594
		EQUIP_SHOP,
		// Token: 0x040015DB RID: 5595
		BIGSALE_SHOP,
		// Token: 0x040015DC RID: 5596
		GANG_SHOP,
		// Token: 0x040015DD RID: 5597
		MONTH_CARD,
		// Token: 0x040015DE RID: 5598
		GANGCITY_NPC
	}

	// Token: 0x020001EE RID: 494
	public enum NPC_TYPE
	{
		// Token: 0x040015E0 RID: 5600
		NORMAL,
		// Token: 0x040015E1 RID: 5601
		ELITE,
		// Token: 0x040015E2 RID: 5602
		BOSS,
		// Token: 0x040015E3 RID: 5603
		MISSION,
		// Token: 0x040015E4 RID: 5604
		ESCORT
	}

	// Token: 0x020001EF RID: 495
	public enum Mail_Type
	{
		// Token: 0x040015E6 RID: 5606
		OPEN,
		// Token: 0x040015E7 RID: 5607
		DELETE,
		// Token: 0x040015E8 RID: 5608
		GET,
		// Token: 0x040015E9 RID: 5609
		GET_ALL,
		// Token: 0x040015EA RID: 5610
		DELETE_ALL
	}

	// Token: 0x020001F0 RID: 496
	public enum SCENE_DEFINE
	{
		// Token: 0x040015EC RID: 5612
		SCENE_LOGIN,
		// Token: 0x040015ED RID: 5613
		SCENE_LODING,
		// Token: 0x040015EE RID: 5614
		SCENE_ANIMAEDITOR = 9,
		// Token: 0x040015EF RID: 5615
		SCENE_TUTORIAL_CAR = 11,
		// Token: 0x040015F0 RID: 5616
		SCENE_TUTORIAL_GAMBLING,
		// Token: 0x040015F1 RID: 5617
		SCENE_MAIN_CITY = 101,
		// Token: 0x040015F2 RID: 5618
		SCENE_SLUM_CITY,
		// Token: 0x040015F3 RID: 5619
		SCENE_TUTORIAL,
		// Token: 0x040015F4 RID: 5620
		SCENE_GAMBLING_CITY,
		// Token: 0x040015F5 RID: 5621
		SCENE_BUSINESS_CITY,
		// Token: 0x040015F6 RID: 5622
		SCENE_BEACH_CITY,
		// Token: 0x040015F7 RID: 5623
		SCENE_CHINA_TOWN,
		// Token: 0x040015F8 RID: 5624
		SCENE_RICH_CITY,
		// Token: 0x040015F9 RID: 5625
		SCENE_ESCORT_TEST,
		// Token: 0x040015FA RID: 5626
		SCENE_PVP_1,
		// Token: 0x040015FB RID: 5627
		SCENE_PVP_2,
		// Token: 0x040015FC RID: 5628
		SCENE_GARAGE = 201,
		// Token: 0x040015FD RID: 5629
		SCENE_CAR_CHASE = 401,
		// Token: 0x040015FE RID: 5630
		SCENE_RANK_PVP_1 = 501,
		// Token: 0x040015FF RID: 5631
		SCENE_REAL_PVP_1,
		// Token: 0x04001600 RID: 5632
		GUILD_GARAGE = 601,
		// Token: 0x04001601 RID: 5633
		CLAMBING_TOWER = 701,
		// Token: 0x04001602 RID: 5634
		LOW_PHONE_SCENE = 3001,
		// Token: 0x04001603 RID: 5635
		COUNT
	}

	// Token: 0x020001F1 RID: 497
	public enum UIBACKTYPE
	{
		// Token: 0x04001605 RID: 5637
		NOTHINTG,
		// Token: 0x04001606 RID: 5638
		FASHION,
		// Token: 0x04001607 RID: 5639
		EQUIP,
		// Token: 0x04001608 RID: 5640
		BADGE,
		// Token: 0x04001609 RID: 5641
		ITEM,
		// Token: 0x0400160A RID: 5642
		ENHANCE
	}

	// Token: 0x020001F2 RID: 498
	public enum TIPS_TYPE
	{
		// Token: 0x0400160C RID: 5644
		ACTIVITY = 1,
		// Token: 0x0400160D RID: 5645
		ACTIVITY_WILDBOSS,
		// Token: 0x0400160E RID: 5646
		SOCIAL,
		// Token: 0x0400160F RID: 5647
		GUILD,
		// Token: 0x04001610 RID: 5648
		SLOT,
		// Token: 0x04001611 RID: 5649
		VEHICLE,
		// Token: 0x04001612 RID: 5650
		ACHIEVEMNT,
		// Token: 0x04001613 RID: 5651
		CHARACTER,
		// Token: 0x04001614 RID: 5652
		ITEMS,
		// Token: 0x04001615 RID: 5653
		ENHANCE,
		// Token: 0x04001616 RID: 5654
		SKILL,
		// Token: 0x04001617 RID: 5655
		WELFARE,
		// Token: 0x04001618 RID: 5656
		DAILYACT,
		// Token: 0x04001619 RID: 5657
		SEVENDAY,
		// Token: 0x0400161A RID: 5658
		MAIL,
		// Token: 0x0400161B RID: 5659
		SHOP
	}

	// Token: 0x020001F3 RID: 499
	public enum CommercailTYPE
	{
		// Token: 0x0400161D RID: 5661
		LEVEL,
		// Token: 0x0400161E RID: 5662
		INVEST,
		// Token: 0x0400161F RID: 5663
		FIRSTBUY,
		// Token: 0x04001620 RID: 5664
		BIGSALE,
		// Token: 0x04001621 RID: 5665
		DAILYBUY,
		// Token: 0x04001622 RID: 5666
		DAILYACTIVE,
		// Token: 0x04001623 RID: 5667
		SHOP,
		// Token: 0x04001624 RID: 5668
		VIP
	}

	// Token: 0x020001F4 RID: 500
	public enum AUTOPOPTYPE
	{
		// Token: 0x04001626 RID: 5670
		NOTHING,
		// Token: 0x04001627 RID: 5671
		RATE,
		// Token: 0x04001628 RID: 5672
		SIGNMONTH,
		// Token: 0x04001629 RID: 5673
		SIGNWEEK,
		// Token: 0x0400162A RID: 5674
		BIGSALE,
		// Token: 0x0400162B RID: 5675
		FIRSTBUY,
		// Token: 0x0400162C RID: 5676
		MYSTERYSHOP,
		// Token: 0x0400162D RID: 5677
		RETRIEVE,
		// Token: 0x0400162E RID: 5678
		TIME_ACTIVITY_TIPS
	}

	// Token: 0x020001F5 RID: 501
	public enum STRONGER_ACTIVITY
	{
		// Token: 0x04001630 RID: 5680
		SKILL,
		// Token: 0x04001631 RID: 5681
		ENHANCE_CUS,
		// Token: 0x04001632 RID: 5682
		HONOR,
		// Token: 0x04001633 RID: 5683
		ENHANCE_STR,
		// Token: 0x04001634 RID: 5684
		ENHANCE_FUSE,
		// Token: 0x04001635 RID: 5685
		MAIN_LINE,
		// Token: 0x04001636 RID: 5686
		DAILY_LINE,
		// Token: 0x04001637 RID: 5687
		EQUIP_COPY,
		// Token: 0x04001638 RID: 5688
		EXP_COPY,
		// Token: 0x04001639 RID: 5689
		ESCORT,
		// Token: 0x0400163A RID: 5690
		DANCE,
		// Token: 0x0400163B RID: 5691
		BARFIGHT,
		// Token: 0x0400163C RID: 5692
		SCUFFLE_AREA,
		// Token: 0x0400163D RID: 5693
		CASH_COPY,
		// Token: 0x0400163E RID: 5694
		CAR_COPY,
		// Token: 0x0400163F RID: 5695
		TOWER,
		// Token: 0x04001640 RID: 5696
		WILD_BOSS,
		// Token: 0x04001641 RID: 5697
		PVP,
		// Token: 0x04001642 RID: 5698
		DIAMOND_BUY,
		// Token: 0x04001643 RID: 5699
		SURVIVE_BATTLE,
		// Token: 0x04001644 RID: 5700
		GUILD_SHOP,
		// Token: 0x04001645 RID: 5701
		DIAMOND_SHOP,
		// Token: 0x04001646 RID: 5702
		GOLD_SHOP,
		// Token: 0x04001647 RID: 5703
		SLOT,
		// Token: 0x04001648 RID: 5704
		GUILD_DONATE,
		// Token: 0x04001649 RID: 5705
		GUILD_BOSS,
		// Token: 0x0400164A RID: 5706
		CASH_SHOP,
		// Token: 0x0400164B RID: 5707
		SEX_GAME,
		// Token: 0x0400164C RID: 5708
		TRADE,
		// Token: 0x0400164D RID: 5709
		BIGSALE,
		// Token: 0x0400164E RID: 5710
		FIRSTBUY,
		// Token: 0x0400164F RID: 5711
		GUILDSKILL,
		// Token: 0x04001650 RID: 5712
		EQUIPMORE,
		// Token: 0x04001651 RID: 5713
		EQUIPBEST,
		// Token: 0x04001652 RID: 5714
		PKMAP
	}

	// Token: 0x020001F6 RID: 502
	public enum CHAT_CHANNEL_TYPE
	{
		// Token: 0x04001654 RID: 5716
		INVALID = -1,
		// Token: 0x04001655 RID: 5717
		SYSTEM,
		// Token: 0x04001656 RID: 5718
		NORMAL,
		// Token: 0x04001657 RID: 5719
		WORLD,
		// Token: 0x04001658 RID: 5720
		TEAM,
		// Token: 0x04001659 RID: 5721
		GUILD,
		// Token: 0x0400165A RID: 5722
		PRIVATE,
		// Token: 0x0400165B RID: 5723
		NOTIFY,
		// Token: 0x0400165C RID: 5724
		BROAT_CAST,
		// Token: 0x0400165D RID: 5725
		COUNT
	}

	// Token: 0x020001F7 RID: 503
	public enum CHAT_LINK_TYPE
	{
		// Token: 0x0400165F RID: 5727
		INVALID = -1,
		// Token: 0x04001660 RID: 5728
		ITEM,
		// Token: 0x04001661 RID: 5729
		EQUIP,
		// Token: 0x04001662 RID: 5730
		TEAM,
		// Token: 0x04001663 RID: 5731
		GUILD,
		// Token: 0x04001664 RID: 5732
		GUILD_BOSS,
		// Token: 0x04001665 RID: 5733
		ESCORT,
		// Token: 0x04001666 RID: 5734
		DANCE,
		// Token: 0x04001667 RID: 5735
		BAR_FIGHT,
		// Token: 0x04001668 RID: 5736
		WILD_BOSS,
		// Token: 0x04001669 RID: 5737
		SURVIVE,
		// Token: 0x0400166A RID: 5738
		ATTACK_ESCORT,
		// Token: 0x0400166B RID: 5739
		GUILD_DANCE,
		// Token: 0x0400166C RID: 5740
		FIRST_GUILD_DANCE,
		// Token: 0x0400166D RID: 5741
		GUILD_DONMINE,
		// Token: 0x0400166E RID: 5742
		GUILD_DONMINE_RES
	}

	// Token: 0x020001F8 RID: 504
	public enum DAILY_ACTIVE_TYPE
	{
		// Token: 0x04001670 RID: 5744
		EQUIP_COPY,
		// Token: 0x04001671 RID: 5745
		EXP_COPY,
		// Token: 0x04001672 RID: 5746
		CASH_COPY,
		// Token: 0x04001673 RID: 5747
		CAR_COPY,
		// Token: 0x04001674 RID: 5748
		ATTACK_ESCORT,
		// Token: 0x04001675 RID: 5749
		TOWER_COPY,
		// Token: 0x04001676 RID: 5750
		WILD_BOSS,
		// Token: 0x04001677 RID: 5751
		RANK_PVP,
		// Token: 0x04001678 RID: 5752
		EQUIP_UPGRADE,
		// Token: 0x04001679 RID: 5753
		REFINE_UPGRADE,
		// Token: 0x0400167A RID: 5754
		SKILL_UPGRADE,
		// Token: 0x0400167B RID: 5755
		GOLD_BUY,
		// Token: 0x0400167C RID: 5756
		DIAMOND_BUY,
		// Token: 0x0400167D RID: 5757
		DAILY_BUY,
		// Token: 0x0400167E RID: 5758
		ESCORT,
		// Token: 0x0400167F RID: 5759
		DOMIN,
		// Token: 0x04001680 RID: 5760
		SCUFFLE_AREA,
		// Token: 0x04001681 RID: 5761
		SINGLE_DANCE,
		// Token: 0x04001682 RID: 5762
		SURVIVE,
		// Token: 0x04001683 RID: 5763
		GUILD_BOSS,
		// Token: 0x04001684 RID: 5764
		GUILD_DANCE
	}

	// Token: 0x020001F9 RID: 505
	public enum DAMAGEBOARD_TYPE
	{
		// Token: 0x04001686 RID: 5766
		PLAYER_HP_DOWN = 1,
		// Token: 0x04001687 RID: 5767
		TARGET_HPDOWN_PARTNER,
		// Token: 0x04001688 RID: 5768
		TARGET_HPDOWN_PLAYER,
		// Token: 0x04001689 RID: 5769
		PLAYER_ATTACK_MISS,
		// Token: 0x0400168A RID: 5770
		TARGET_ATTACK_MISS,
		// Token: 0x0400168B RID: 5771
		SKILL_NAME,
		// Token: 0x0400168C RID: 5772
		PLAYER_ATTACK_CRITICAL,
		// Token: 0x0400168D RID: 5773
		TARGET_ATTACK_CRITICAL,
		// Token: 0x0400168E RID: 5774
		SKILL_NAME_NPC,
		// Token: 0x0400168F RID: 5775
		PLAYER_HP_UP,
		// Token: 0x04001690 RID: 5776
		COUNT
	}

	// Token: 0x020001FA RID: 506
	public enum BROAD_CAST_TYPE
	{
		// Token: 0x04001692 RID: 5778
		SLOT,
		// Token: 0x04001693 RID: 5779
		WILD_BOSS,
		// Token: 0x04001694 RID: 5780
		PACK_ITEM,
		// Token: 0x04001695 RID: 5781
		RANK_PVP,
		// Token: 0x04001696 RID: 5782
		REFINE,
		// Token: 0x04001697 RID: 5783
		BADGE,
		// Token: 0x04001698 RID: 5784
		SURVIVE,
		// Token: 0x04001699 RID: 5785
		TITLE,
		// Token: 0x0400169A RID: 5786
		GUILD_LEVEL,
		// Token: 0x0400169B RID: 5787
		FUNC_LEVEL,
		// Token: 0x0400169C RID: 5788
		REFINE_2,
		// Token: 0x0400169D RID: 5789
		GUILD_BOSS,
		// Token: 0x0400169E RID: 5790
		CAR_RANK,
		// Token: 0x0400169F RID: 5791
		BADGE_2,
		// Token: 0x040016A0 RID: 5792
		BAR_FIGHT,
		// Token: 0x040016A1 RID: 5793
		SURVIVE_2,
		// Token: 0x040016A2 RID: 5794
		WILD_BOSS_2,
		// Token: 0x040016A3 RID: 5795
		DANCE,
		// Token: 0x040016A4 RID: 5796
		RAID_BOSS,
		// Token: 0x040016A5 RID: 5797
		GUILD_BOSS_2,
		// Token: 0x040016A6 RID: 5798
		GUILD_BATTLE,
		// Token: 0x040016A7 RID: 5799
		FIRST_GUILD_DANCE,
		// Token: 0x040016A8 RID: 5800
		RARE_ITEM
	}

	// Token: 0x020001FB RID: 507
	public enum SHOP_TAB_TYPE
	{
		// Token: 0x040016AA RID: 5802
		INVALID,
		// Token: 0x040016AB RID: 5803
		FASHION,
		// Token: 0x040016AC RID: 5804
		ITEM,
		// Token: 0x040016AD RID: 5805
		EXCHANGE,
		// Token: 0x040016AE RID: 5806
		TICKET,
		// Token: 0x040016AF RID: 5807
		CASE,
		// Token: 0x040016B0 RID: 5808
		EQUIP,
		// Token: 0x040016B1 RID: 5809
		PETANIMAL,
		// Token: 0x040016B2 RID: 5810
		PIECE
	}
}
