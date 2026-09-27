using System;
using Sproto;
using SprotoType;

// Token: 0x02000238 RID: 568
public class guild_invite_accept_handler
{
	// Token: 0x060012F1 RID: 4849 RVA: 0x0007BA50 File Offset: 0x00079C50
	public static SprotoTypeBase guild_invite_accept_request(SprotoTypeBase req)
	{
		guild_invite_accept.request request = req as guild_invite_accept.request;
		if (request != null)
		{
			string name = request.name;
			string guildName = request.guildName;
			long guildId = request.guildId;
			MessageBoxLogic.OpenOKCancelWaitBox(StrDictionary.GetDictionaryString("#{100776}", new object[]
			{
				name,
				guildName
			}), "#{100127}", 10f, delegate
			{
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GUILD))
				{
					guild_join.request request2 = new guild_join.request();
					request2.guildId = guildId;
					NetLogic.GetInstance().Send<Protocol.guild_join>(request2, null);
				}
				else
				{
					NoticeLogic.AddNotifyData("#{100288}", true, false);
				}
			}, null, null, null, null);
		}
		return null;
	}
}
