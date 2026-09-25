using System;
using Sproto;
using SprotoType;

// Token: 0x020002D0 RID: 720
public class update_copyscene_info_handler
{
	// Token: 0x06001428 RID: 5160 RVA: 0x00082D1C File Offset: 0x00080F1C
	public static SprotoTypeBase update_copyscene_info_request(SprotoTypeBase req)
	{
		update_copyscene_info.request request = req as update_copyscene_info.request;
		if (request != null)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopyInfoData.SyncCopyInfo(request.copyscene);
		}
		return null;
	}
}
