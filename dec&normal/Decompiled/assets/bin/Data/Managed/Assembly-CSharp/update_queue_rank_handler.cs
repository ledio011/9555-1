using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x020002D5 RID: 725
public class update_queue_rank_handler
{
	// Token: 0x06001432 RID: 5170 RVA: 0x0008326C File Offset: 0x0008146C
	public static SprotoTypeBase update_queue_rank_request(SprotoTypeBase req)
	{
		update_queue_rank.request request = req as update_queue_rank.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			MessageBoxLogic.OpenWaitBox(StrDictionary.GetDictionaryString("#{100158}", new object[]
			{
				request.rank
			}), "#{100244}", 0f, 0f, null);
			Debug.Log(request.rank.ToString());
		}
		return null;
	}
}
