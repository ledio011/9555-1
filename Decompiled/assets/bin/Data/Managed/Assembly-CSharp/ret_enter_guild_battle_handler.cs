using System;
using Sproto;
using SprotoType;

// Token: 0x02000269 RID: 617
public class ret_enter_guild_battle_handler
{
	// Token: 0x06001359 RID: 4953 RVA: 0x0007EECC File Offset: 0x0007D0CC
	public static SprotoTypeBase ret_enter_guild_battle_request(SprotoTypeBase req)
	{
		ret_enter_guild_battle.request request = req as ret_enter_guild_battle.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			NoticeLogic.AddNotifyData("#{105086}", true, false);
		}
		return null;
	}
}
