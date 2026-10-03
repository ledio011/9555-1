using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x0200021B RID: 539
public class aoi_update_attribute_handler
{
	// Token: 0x060012B0 RID: 4784 RVA: 0x0007A2A0 File Offset: 0x000784A0
	public static SprotoTypeBase aoi_update_attribute_request(SprotoTypeBase req)
	{
		aoi_update_attribute.request request = req as aoi_update_attribute.request;
		if (request != null)
		{
			long id = request.character.id;
			ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(id);
			if (objCharacter != null)
			{
				if (objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
				{
					// NPCs created locally by SceneManager (for example Mission 1001 Hulk)
					// must keep the existing object.  Apply TAG 510 values directly;
					// do NOT route them through npc_create (509), which recycles/recreates.
					CharacterAttributeData npcAttr = objCharacter.AttributeData;

					if (request.character.HasAttribute)
					{
						attribute attr = request.character.attribute;
						if (attr.HasMax_hp) npcAttr.MaxHP = attr.max_hp;
						if (attr.HasAtk) npcAttr.ATK = (int)attr.atk;
						if (attr.HasDef) npcAttr.DEF = (int)attr.def;
						if (attr.HasHit) npcAttr.HIT = (int)attr.hit;
						if (attr.HasEva) npcAttr.DGE = (int)attr.eva;
						if (attr.HasCri) npcAttr.CRI = (int)attr.cri;
						if (attr.HasRes) npcAttr.RES = (float)attr.res;
						if (attr.HasExd) npcAttr.EXD = (float)attr.exd / 10000f;
						if (attr.HasExr) npcAttr.EXR = (float)attr.exr / 10000f;
						if (attr.HasCrd) npcAttr.CRD = (float)attr.crd / 10000f;
						if (attr.HasCrr) npcAttr.CRR = (float)attr.crr / 10000f;
						if (attr.HasDefa) npcAttr.DEFA = (int)attr.defa;
						if (attr.HasDgea) npcAttr.DGEA = (int)attr.dgea;
						if (attr.HasResa) npcAttr.RESA = (int)attr.resa;
						if (attr.HasHita) npcAttr.HITA = (int)attr.hita;
						if (attr.HasCria) npcAttr.CRIA = (int)attr.cria;
					}
					if (request.character.HasAttribute_all)
					{
						attribute all = request.character.attribute_all;
						if (all.HasAtk) npcAttr.CurATK = (float)all.atk;
						if (all.HasDef) npcAttr.CurDEF = (float)all.def;
						if (all.HasHit) npcAttr.CurHIT = (float)all.hit;
						if (all.HasEva) npcAttr.CurDGE = (float)all.eva;
						if (all.HasCri) npcAttr.CurCRI = (float)all.cri;
						if (all.HasRes) npcAttr.CurRES = (float)all.res;
						if (all.HasExd) npcAttr.CurEXD = (float)all.exd / 10000f;
						if (all.HasExr) npcAttr.CurEXR = (float)all.exr / 10000f;
						if (all.HasCrd) npcAttr.CurCRD = (float)all.crd / 10000f;
						if (all.HasCrr) npcAttr.CurCRR = (float)all.crr / 10000f;
						if (all.HasDefa) npcAttr.CurDEFA = (int)all.defa;
						if (all.HasDgea) npcAttr.CurDGEA = (int)all.dgea;
						if (all.HasResa) npcAttr.CurRESA = (int)all.resa;
						if (all.HasHita) npcAttr.CurHITA = (int)all.hita;
						if (all.HasCria) npcAttr.CurCRIA = (int)all.cria;
					}
					if (request.character.HasAttribute_other)
					{
						long hp = request.character.attribute_other.hp;
						npcAttr.Level = (int)request.character.attribute_other.level;
						objCharacter.ChangeHPVal(hp);
						long guildId = (!request.character.attribute_other.HasGuildId) ? -1L : request.character.attribute_other.guildId;
						long teamId = (!request.character.attribute_other.HasGuildJob) ? -1L : request.character.attribute_other.guildJob;
						ObjNPC objNPC = objCharacter as ObjNPC;
						objNPC.UpdateEscortNpcCamp(guildId, teamId);
					}
					return null;
				}
				if (request.character.HasVisual)
				{
					SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
					if (objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER)
					{
						ObjOtherPlayer objOtherPlayer = objCharacter as ObjOtherPlayer;
						if (objOtherPlayer != null)
						{
							characterVisual visual = request.character.visual;
							objOtherPlayer.IsServerRidingMount = (visual.mount_state == 1L);
							objOtherPlayer.ReLoadPlayerVisual(visual);
							if (visual.mount_state == 0L)
							{
								objOtherPlayer.MountId = visual.MountId;
								objOtherPlayer.MountColor = visual.mount_color;
								if (sceneManager.IsBigWorld())
								{
									objOtherPlayer.DisMountCar();
								}
							}
							else if (sceneManager.IsBigWorld())
							{
								objOtherPlayer.MountCar(visual.MountId, visual.mount_color);
							}
						}
					}
					else if (objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
					{
						ObjMainPlayer objMainPlayer = objCharacter as ObjMainPlayer;
						if (objMainPlayer != null)
						{
							characterVisual visual2 = request.character.visual;
							objMainPlayer.IsServerRidingMount = (visual2.mount_state == 1L);
							objMainPlayer.UpdateMainPlayerVisual(visual2);
							objMainPlayer.ReLoadPlayerVisual(visual2);
							objMainPlayer.MountId = visual2.MountId;
							objMainPlayer.MountColor = visual2.mount_color;
						}
					}
				}
				if (request.character.HasAttribute || request.character.HasAttribute_other || request.character.HasAttribute_all)
				{
					CharacterAttributeData attributeData = objCharacter.AttributeData;
					if (request.character.HasAttribute && request.character.attribute.HasMax_hp)
					{
						objCharacter.AttributeData.MaxHP = request.character.attribute.max_hp;
						objCharacter.ChangeHPVal(attributeData.HP);
					}
					if (request.character.HasAttribute_other)
					{
						long hp2 = request.character.attribute_other.hp;
						objCharacter.ChangeHPVal(hp2);
						objCharacter.ChangeLevel((int)request.character.attribute_other.level, (int)request.character.attribute_other.combValue);
					}
					attributeData.InitData(request.character, objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER);
					objCharacter.UpdatePlayerSpeed();
					if (objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
					{
						SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SetPKModeState(attributeData.PkMode);
					}
					if (request.character.HasAttribute_other)
					{
						objCharacter.RefreshHeadInfo();
						if (objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
						{
							ExpLineRootLogic.UpdateExp();
							PlayerModelPageRootLogic.UpdateCombo(attributeData.ComboValue);
							if (request.character.attribute_other.dance_state == 0L)
							{
								ObjMainPlayer objMainPlayer2 = objCharacter as ObjMainPlayer;
								objMainPlayer2.StopDance();
							}
						}
						else if (objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER)
						{
							if (request.character.attribute_other.dance_state == 1L)
							{
								ObjOtherPlayer objOtherPlayer2 = objCharacter as ObjOtherPlayer;
								objOtherPlayer2.StartDance(request.character.attribute_other.dance_id);
							}
							else
							{
								ObjOtherPlayer objOtherPlayer3 = objCharacter as ObjOtherPlayer;
								objOtherPlayer3.StopDance();
							}
						}
					}
					if (SingletonUnity<JSSXKuangRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<JSSXKuangRootLogic>.Instance.gameObject))
					{
						SingletonUnity<JSSXKuangRootLogic>.Instance.Reset(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData);
					}
				}
				if (request.character.HasProperty && objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
				{
					GameMoneyHelper.UpdateMoney(request.character.property.money1, request.character.property.money2, request.character.property.money3, request.character.property.money4, request.character.property.money5, request.character.property.money6);
					if (UIUpdateEvent.UpdateMoneyEvent != null)
					{
						UIUpdateEvent.UpdateMoneyEvent();
					}
				}
			}
			else
			{
				ObjInitPlayerData noLogicOtherPlayerData = Singleton<ObjManager>.Instance.GetNoLogicOtherPlayerData(id);
				if (noLogicOtherPlayerData != null)
				{
					noLogicOtherPlayerData.InitData(request.character);
				}
				else
				{
					Debug.Log("No PlayerData In Scene!!!!!!!!!!!!!!!");
				}
			}
		}
		return null;
	}
}
