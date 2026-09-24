using System;
using Sproto;
using SprotoType;

// Token: 0x020002A7 RID: 679
public class ret_skill_use_handler
{
	// Token: 0x060013D5 RID: 5077 RVA: 0x00080FB0 File Offset: 0x0007F1B0
	public static SprotoTypeBase ret_skill_use_request(SprotoTypeBase req)
	{
		ret_skill_use.request request = req as ret_skill_use.request;
		if (request != null)
		{
			ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(request.sendderId);
			if (objCharacter != null)
			{
				if (objCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER && !(objCharacter as ObjOtherPlayer).IsVisible())
				{
					return null;
				}
				objCharacter.SkillLogic.ServerUseSkill(request.skillId.ToString(), request.sendderId, request.targetId, request.attack_list);
			}
		}
		return null;
	}
}
