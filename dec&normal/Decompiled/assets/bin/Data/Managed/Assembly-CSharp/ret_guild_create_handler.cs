using System;
using Sproto;
using SprotoType;

// Token: 0x02000272 RID: 626
public class ret_guild_create_handler
{
	// Token: 0x0600136B RID: 4971 RVA: 0x0007F1F8 File Offset: 0x0007D3F8
	public static SprotoTypeBase ret_guild_create_request(SprotoTypeBase req)
	{
		ret_guild_create.request request = req as ret_guild_create.request;
		if (request == null || request.state == 0L)
		{
		}
		return null;
	}
}
