using System;
using Sproto;
using SprotoType;

// Token: 0x02000218 RID: 536
public class aoi_remove_handler
{
	// Token: 0x060012AA RID: 4778 RVA: 0x00079EE4 File Offset: 0x000780E4
	public static SprotoTypeBase aoi_remove_request(SprotoTypeBase req)
	{
		aoi_remove.request request = req as aoi_remove.request;
		if (request != null)
		{
			ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(request.character);
			if (objCharacter != null)
			{
				if (objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER)
				{
					Singleton<ObjManager>.Instance.RecycleOtherPlayer(objCharacter as ObjOtherPlayer);
				}
				else if (objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
				{
					if (objCharacter.AttributeData.Camp == GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC)
					{
						CurMission escortMission = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetEscortMission();
						if (escortMission != null && escortMission.GetParam(1) == objCharacter.ServerId)
						{
							escortMission.SetParam(4, (long)(objCharacter.Position.x * 100f));
							escortMission.SetParam(5, (long)(objCharacter.Position.z * 100f));
						}
					}
					Singleton<ObjManager>.Instance.RecycleNpc(objCharacter as ObjNPC);
				}
			}
			else
			{
				Singleton<ObjManager>.Instance.RemoveNoLogicOtherPlayerData(request.character);
			}
		}
		return null;
	}
}
