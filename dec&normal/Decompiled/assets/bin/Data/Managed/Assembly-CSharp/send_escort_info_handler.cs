using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x020002B8 RID: 696
public class send_escort_info_handler
{
	// Token: 0x060013F7 RID: 5111 RVA: 0x00081A6C File Offset: 0x0007FC6C
	public static SprotoTypeBase send_escort_info_request(SprotoTypeBase req)
	{
		send_escort_info.request request = req as send_escort_info.request;
		if (request != null)
		{
			MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			CurMission escortMission = missionManager.GetEscortMission();
			if (escortMission != null)
			{
				escortMission.SetParam(1, request.npcid);
				escortMission.SetParam(2, request.lineIndex);
				escortMission.SetParam(3, long.Parse(request.mapInfoId));
			}
			else
			{
				Debug.Log("No Escort Mission!!!!!!!!!!!!!!!!!!!!");
			}
			Debug.Log("send_escort_info_handler :: " + request.npcid);
			SingletonUnity<FunctionBtnRootLogic>.Instance.refershBtn();
			ObjManager instance = Singleton<ObjManager>.Instance;
			ObjCharacter objCharacter = instance.FindObjInScene(request.npcid);
			if (objCharacter != null)
			{
				instance.RemoveFromTargetCampList(objCharacter);
				objCharacter.AttributeData.Camp = GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC;
			}
		}
		return null;
	}
}
