using System;
using Sproto;
using SprotoType;

// Token: 0x02000216 RID: 534
public class aoi_add_handler
{
	// Token: 0x060012A6 RID: 4774 RVA: 0x00079C64 File Offset: 0x00077E64
	public static SprotoTypeBase aoi_add_request(SprotoTypeBase req)
	{
		aoi_add.request request = req as aoi_add.request;
		if (request != null)
		{
			ObjInitPlayerData objInitPlayerData = new ObjInitPlayerData();
			objInitPlayerData.InitData(request.character);
			ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(objInitPlayerData.mServerID);
			if (objCharacter != null)
			{
				if (objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER)
				{
					Singleton<ObjManager>.Instance.RecycleOtherPlayer(objCharacter as ObjOtherPlayer);
				}
			}
			else if (Singleton<ObjManager>.Instance.GetNoLogicOtherPlayerData(objInitPlayerData.mServerID) != null)
			{
				Singleton<ObjManager>.Instance.RemoveNoLogicOtherPlayerData(objInitPlayerData.mServerID);
			}
			Singleton<ObjManager>.Instance.CreateOtherPlayer(objInitPlayerData);
		}
		return null;
	}
}
