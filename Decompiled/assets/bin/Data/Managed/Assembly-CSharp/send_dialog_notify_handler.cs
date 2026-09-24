using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x020002B7 RID: 695
public class send_dialog_notify_handler
{
	// Token: 0x060013F5 RID: 5109 RVA: 0x000819C8 File Offset: 0x0007FBC8
	public static SprotoTypeBase send_dialog_notify_request(SprotoTypeBase req)
	{
		send_dialog_notify.request request = req as send_dialog_notify.request;
		if (request != null)
		{
			long type = request.type;
			int num = Random.Range(10, 15);
			if (request.HasParm)
			{
				MessageBoxLogic.OpenCancelWaitBox(StrDictionary.GetDictionaryString(request.key, new object[]
				{
					request.parm
				}), StrDictionary.GetDictionaryString("#{100127}", new object[0]), (float)num, null, null);
			}
			else
			{
				MessageBoxLogic.OpenCancelWaitBox(StrDictionary.GetDictionaryString(request.key, new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), (float)num, null, null);
			}
		}
		return null;
	}
}
