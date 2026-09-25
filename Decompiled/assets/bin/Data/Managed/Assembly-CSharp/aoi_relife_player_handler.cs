using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x02000217 RID: 535
public class aoi_relife_player_handler
{
	// Token: 0x060012A8 RID: 4776 RVA: 0x00079D08 File Offset: 0x00077F08
	public static SprotoTypeBase aoi_relife_player_request(SprotoTypeBase req)
	{
		aoi_relife_player.request request = req as aoi_relife_player.request;
		if (request != null)
		{
			long id = request.character.id;
			ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(id);
			if (objCharacter != null)
			{
				if (objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER)
				{
					ObjMainPlayer objMainPlayer = objCharacter as ObjMainPlayer;
					Vector3 pos;
					pos..ctor((float)request.character.movement.pos.x / 100f, 0f, (float)request.character.movement.pos.z / 100f);
					pos.y = SceneManager.GetHitHeight(pos);
					objMainPlayer.DisactiveTargetArriveFinish();
					if (objMainPlayer.IsLocalDrivingCar)
					{
						ObjPlayerCar curPlayerCar = objMainPlayer.CurPlayerCar;
						objMainPlayer.CurPlayerCar.DisableCar();
						curPlayerCar.rigidbody.useGravity = true;
						curPlayerCar.rigidbody.isKinematic = false;
						if (SingletonUnity<CitySimController>.Exists)
						{
							CitySimController instance = SingletonUnity<CitySimController>.Instance;
							instance.ChangeToPlayerCtl();
							instance.PlayerCar = null;
							instance.getOnCarHandle.Cancel();
							instance.getOnCarAnimaHandle.Cancel();
							if (instance.RobNpcFakeObj != null)
							{
								instance.RobNpcFakeObj.DestroyNpcFakeObj();
								instance.RobNpcFakeObj = null;
							}
						}
						objMainPlayer.EnableMainPlayer();
						curPlayerCar.enabled = false;
					}
					objMainPlayer.OnRelife(objMainPlayer.AttributeData.MaxHP, pos);
				}
				else if (objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER)
				{
					Vector3 pos2;
					pos2..ctor((float)request.character.movement.pos.x / 100f, 0f, (float)request.character.movement.pos.z / 100f);
					pos2.y = SceneManager.GetHitHeight(pos2);
					objCharacter.OnRelife(objCharacter.AttributeData.MaxHP, pos2);
				}
			}
		}
		return null;
	}
}
