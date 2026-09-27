using System;
using Sproto;
using SprotoType;

// Token: 0x020002B3 RID: 691
public class retrieve_account_handler
{
	// Token: 0x060013ED RID: 5101 RVA: 0x0008183C File Offset: 0x0007FA3C
	public static SprotoTypeBase retrieve_account_request(SprotoTypeBase req)
	{
		retrieve_account.request request = req as retrieve_account.request;
		if (request != null)
		{
			long id = request.id;
			PlayerData.SavePlayerAccountId(id.ToString());
			PlayerData.SavePlayerAccountKey(id.ToString());
		}
		return null;
	}
}
