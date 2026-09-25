using System;
using Sproto;
using SprotoType;

// Token: 0x020002C5 RID: 709
public class sync_common_data_handler
{
	// Token: 0x06001412 RID: 5138 RVA: 0x00082438 File Offset: 0x00080638
	public static SprotoTypeBase sync_common_data_request(SprotoTypeBase req)
	{
		sync_common_data.request request = req as sync_common_data.request;
		if (request != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SyncCommonData(request);
		}
		return null;
	}
}
