using System;
using Sproto;
using SprotoType;

// Token: 0x0200024C RID: 588
public class npc_create_handler
{
	// Token: 0x0600131D RID: 4893 RVA: 0x0007C784 File Offset: 0x0007A984
	public static SprotoTypeBase npc_create_request(SprotoTypeBase req)
	{
		npc_create.request request = req as npc_create.request;
		if (request != null)
		{
			ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(request.npc_attribute.id);
			if (objCharacter != null && objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
			{
				Singleton<ObjManager>.Instance.RecycleNpc(objCharacter as ObjNPC);
			}
			ObjInitNpcData objInitNpcData = new ObjInitNpcData();
			objInitNpcData.InitData(request.npc_attribute);
			Singleton<ObjManager>.Instance.CreateNPC(objInitNpcData, null, null);
		}
		return null;
	}
}
