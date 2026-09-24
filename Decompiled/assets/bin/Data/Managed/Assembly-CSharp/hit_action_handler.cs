using System;
using Sproto;
using SprotoType;

// Token: 0x0200023A RID: 570
public class hit_action_handler
{
	// Token: 0x060012F5 RID: 4853 RVA: 0x0007BAF0 File Offset: 0x00079CF0
	public static SprotoTypeBase hit_action_request(SprotoTypeBase req)
	{
		hit_action.request request = req as hit_action.request;
		if (request != null)
		{
			ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(request.targetid);
			ObjCharacter objCharacter2 = Singleton<ObjManager>.Instance.FindObjInScene(request.senderId);
			if (objCharacter != null && objCharacter2 != null)
			{
				EffInfoData effInfoDataById = DataManager.GetEffInfoDataById(request.effinfoId);
			}
		}
		return null;
	}
}
