using System;
using SprotoType;

// Token: 0x02000963 RID: 2403
public class ServerToClientTools
{
	// Token: 0x0600438F RID: 17295 RVA: 0x0014E4CC File Offset: 0x0014C6CC
	public static GameItem ServerGameItemToClientGameItem(gameitem netItem)
	{
		GameItem gameItem = new GameItem();
		if (netItem != null)
		{
			gameItem.ItemId = netItem.itemId;
			if (netItem.HasBindflag)
			{
				gameItem.BindFlag = netItem.bindflag;
			}
			else
			{
				gameItem.BindFlag = false;
			}
			if (netItem.HasStack)
			{
				gameItem.StackNum = (int)netItem.stack;
			}
			else
			{
				gameItem.StackNum = 1;
			}
			if (netItem.HasIndexId)
			{
				gameItem.IndexId = netItem.indexId;
			}
			else
			{
				gameItem.IndexId = -1L;
			}
			if (netItem.HasQuality)
			{
				gameItem.Quality = (EQUIP_QUALITY)netItem.quality;
			}
			else
			{
				gameItem.Quality = EQUIP_QUALITY.INVALID;
			}
			if (netItem.HasLevel)
			{
				gameItem.ItemLevel = (int)netItem.level;
			}
			else
			{
				gameItem.ItemLevel = 0;
			}
			if (netItem.HasAppraise)
			{
				gameItem.Appraise = (int)netItem.appraise;
			}
			else
			{
				gameItem.Appraise = 0;
			}
			if (netItem.HasRandom_attri)
			{
				gameItem.Random_AttriDic = netItem.random_attri;
			}
			else
			{
				gameItem.Random_AttriDic = null;
			}
			if (netItem.HasInlay)
			{
				gameItem.InlayDic = netItem.inlay;
			}
			else
			{
				gameItem.InlayDic = null;
			}
			if (netItem.HasParm)
			{
				gameItem.SetParm(netItem.parm);
			}
			return gameItem;
		}
		gameItem.Reset();
		return gameItem;
	}

	// Token: 0x06004390 RID: 17296 RVA: 0x0014E630 File Offset: 0x0014C830
	public static CharacterAttributeData attributeToCharacterAttributeData(character_look character)
	{
		CharacterAttributeData otherPlayerAttriData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.OtherPlayerAttriData;
		if (character != null)
		{
			otherPlayerAttriData.MaxHP = character.attribute.max_hp;
			otherPlayerAttriData.CurEXP = character.attribute_other.exp;
			otherPlayerAttriData.Level = (int)character.attribute_other.level;
			otherPlayerAttriData.ATK = (int)character.attribute.atk;
			otherPlayerAttriData.DEF = (int)character.attribute.def;
			otherPlayerAttriData.HIT = (int)character.attribute.hit;
			otherPlayerAttriData.DGE = (int)character.attribute.eva;
			otherPlayerAttriData.CRI = (int)character.attribute.cri;
			otherPlayerAttriData.EXD = (float)character.attribute.exd / 10000f;
			otherPlayerAttriData.EXR = (float)character.attribute.exr / 10000f;
			otherPlayerAttriData.CRD = (float)character.attribute.crd / 10000f;
			otherPlayerAttriData.CRR = (float)character.attribute.crr / 10000f;
			otherPlayerAttriData.DEFA = (int)character.attribute.defa;
			otherPlayerAttriData.RES = (float)((int)character.attribute.res);
			otherPlayerAttriData.Speed = (float)character.attribute.mov / 100f;
			otherPlayerAttriData.Rec = (int)character.attribute.rec;
			otherPlayerAttriData.CurTitleLevel = (int)character.attribute_other.title_level;
			otherPlayerAttriData.CurTitleExp = (int)character.attribute_other.title_exp;
			otherPlayerAttriData.ComboValue = (int)character.attribute_other.combValue;
			if (character.HasEquip_enhance)
			{
				for (int i = 0; i < otherPlayerAttriData.EquipEnhanceList.Length; i++)
				{
					if (character.equip_enhance.ContainsKey((long)i))
					{
						otherPlayerAttriData.EquipEnhanceList[i] = (int)character.equip_enhance[(long)i].level;
					}
					else
					{
						otherPlayerAttriData.EquipEnhanceList[i] = 0;
					}
				}
			}
			else
			{
				for (int j = 0; j < otherPlayerAttriData.EquipEnhanceList.Length; j++)
				{
					otherPlayerAttriData.EquipEnhanceList[j] = 0;
				}
			}
		}
		return otherPlayerAttriData;
	}

	// Token: 0x06004391 RID: 17297 RVA: 0x0014E850 File Offset: 0x0014CA50
	public static string GetModeName(string headId)
	{
		string text = headId.Substring(0, headId.IndexOf('_'));
		string result = string.Empty;
		if (text.Equals("XD"))
		{
			result = "baiRen";
		}
		else if (text.Equals("QJ"))
		{
			result = "heiRen";
		}
		else
		{
			result = "nvRen";
		}
		return result;
	}
}
