using System;
using System.Collections.Generic;
using Sproto;
using SprotoType;

// Token: 0x020002BB RID: 699
public class show_damage_board_handler
{
	// Token: 0x060013FD RID: 5117 RVA: 0x00081BCC File Offset: 0x0007FDCC
	public static SprotoTypeBase show_damage_board_request(SprotoTypeBase req)
	{
		show_damage_board.request request = req as show_damage_board.request;
		if (request != null)
		{
			if (!request.HasDamges)
			{
				return null;
			}
			List<acceptdamge> damges = request.damges;
			for (int i = 0; i < damges.Count; i++)
			{
				long id = damges[i].id;
				ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(id);
				if (objCharacter != null)
				{
					if (damges[i].damage > 0L)
					{
						if (damges[i].cri)
						{
							objCharacter.UpdateDamgeBoard(GameDefine.DAMAGEBOARD_TYPE.TARGET_ATTACK_CRITICAL, (long)((int)damges[i].damage));
						}
						else
						{
							objCharacter.UpdateDamgeBoard(GameDefine.DAMAGEBOARD_TYPE.PLAYER_HP_DOWN, (long)((int)damges[i].damage));
						}
					}
					else
					{
						objCharacter.UpdateDamgeBoard(GameDefine.DAMAGEBOARD_TYPE.TARGET_ATTACK_MISS, (long)((int)damges[i].damage));
					}
				}
			}
		}
		return null;
	}
}
