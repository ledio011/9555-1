using System;
using Sproto;
using SprotoType;

// Token: 0x020002BC RID: 700
public class show_player_damage_board_handler
{
	// Token: 0x060013FF RID: 5119 RVA: 0x00081CB4 File Offset: 0x0007FEB4
	public static SprotoTypeBase show_player_damage_board_request(SprotoTypeBase req)
	{
		show_player_damage_board.request request = req as show_player_damage_board.request;
		if (request != null)
		{
			ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(request.id);
			if (objCharacter != null)
			{
				objCharacter.UpdateDamgeBoard(GameDefine.DAMAGEBOARD_TYPE.PLAYER_HP_UP, request.hp);
			}
		}
		return null;
	}
}
