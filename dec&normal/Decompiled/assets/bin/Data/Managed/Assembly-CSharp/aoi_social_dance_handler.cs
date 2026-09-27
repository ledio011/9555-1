using System;
using Sproto;
using SprotoType;

// Token: 0x02000219 RID: 537
public class aoi_social_dance_handler
{
	// Token: 0x060012AC RID: 4780 RVA: 0x00079FE8 File Offset: 0x000781E8
	public static SprotoTypeBase aoi_social_dance_request(SprotoTypeBase req)
	{
		aoi_social_dance.request request = req as aoi_social_dance.request;
		if (request != null)
		{
			long id = request.id;
			string danceId = request.danceId;
			ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(id);
			if (objCharacter != null && objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER)
			{
				ObjOtherPlayer objOtherPlayer = objCharacter as ObjOtherPlayer;
				if (objOtherPlayer != null && objOtherPlayer.IsVisible())
				{
					objOtherPlayer.PlayeSocialDance(danceId);
				}
			}
		}
		return null;
	}
}
