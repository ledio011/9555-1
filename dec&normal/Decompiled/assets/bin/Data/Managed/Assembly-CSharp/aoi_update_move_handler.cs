using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x0200021C RID: 540
public class aoi_update_move_handler
{
	// Token: 0x060012B2 RID: 4786 RVA: 0x0007A7B8 File Offset: 0x000789B8
	public static SprotoTypeBase aoi_update_move_request(SprotoTypeBase req)
	{
		aoi_update_move.request request = req as aoi_update_move.request;
		if (request != null)
		{
			if (Singleton<ObjManager>.Instance.MainPlayer == null)
			{
				return null;
			}
			if (request.character.id != Singleton<ObjManager>.Instance.MainPlayer.ServerId)
			{
				ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(request.character.id);
				if (objCharacter != null)
				{
					if (objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER)
					{
						ObjOtherPlayer objOtherPlayer = objCharacter as ObjOtherPlayer;
						if (objOtherPlayer != null)
						{
							MovePoint point = default(MovePoint);
							point.Index = 0;
							point.xPos = (float)request.character.movement.pos.x / 100f;
							point.zPos = (float)request.character.movement.pos.z / 100f;
							point.O = (float)request.character.movement.pos.o / 100f;
							objOtherPlayer.AutoMoveLogic.AddMovePoint(point);
						}
					}
					else if (objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
					{
						ObjNPC objNPC = objCharacter as ObjNPC;
						MovePoint point2 = default(MovePoint);
						point2.Index = 0;
						point2.xPos = (float)request.character.movement.pos.x / 100f;
						point2.zPos = (float)request.character.movement.pos.z / 100f;
						point2.walk = true;
						if (request.character.HasWalk)
						{
							point2.walk = request.character.walk;
						}
						objNPC.AutoMoveLogic.AddMovePoint(point2);
					}
				}
				else
				{
					ObjInitPlayerData noLogicOtherPlayerData = Singleton<ObjManager>.Instance.GetNoLogicOtherPlayerData(request.character.id);
					if (noLogicOtherPlayerData != null)
					{
						noLogicOtherPlayerData.mPos = new Vector3((float)request.character.movement.pos.x / 100f, (float)request.character.movement.pos.y / 100f, (float)request.character.movement.pos.z / 100f);
						noLogicOtherPlayerData.mDir = MathUtil.HeadingToVector3((float)request.character.movement.pos.o);
					}
				}
			}
		}
		return null;
	}
}
