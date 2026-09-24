using System;
using Sproto;
using SprotoType;

// Token: 0x02000287 RID: 647
public class ret_open_guild_shop_handler
{
	// Token: 0x06001395 RID: 5013 RVA: 0x0007FF20 File Offset: 0x0007E120
	public static SprotoTypeBase ret_open_guild_shop_request(SprotoTypeBase req)
	{
		ret_open_guild_shop.request request = req as ret_open_guild_shop.request;
		if (request == null || request.HasShop_list)
		{
		}
		return null;
	}
}
