using System;
using System.Collections.Generic;
using Sproto;
using SprotoType;

// Token: 0x02000285 RID: 645
public class ret_offline_chat_handler
{
	// Token: 0x06001391 RID: 5009 RVA: 0x0007FE48 File Offset: 0x0007E048
	public static SprotoTypeBase ret_offline_chat_request(SprotoTypeBase req)
	{
		ret_offline_chat.request request = req as ret_offline_chat.request;
		if (request != null && request.HasChat_list)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			List<chat_item> chat_list = request.chat_list;
			for (int i = 0; i < chat_list.Count; i++)
			{
				playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
			}
		}
		return null;
	}
}
