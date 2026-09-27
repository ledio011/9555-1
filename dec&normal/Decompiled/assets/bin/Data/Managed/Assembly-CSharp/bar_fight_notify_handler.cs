using System;
using Sproto;
using SprotoType;

// Token: 0x02000223 RID: 547
public class bar_fight_notify_handler
{
	// Token: 0x060012C3 RID: 4803 RVA: 0x0007ADCC File Offset: 0x00078FCC
	public static SprotoTypeBase bar_fight_notify_request(SprotoTypeBase req)
	{
		bar_fight_notify.request request = req as bar_fight_notify.request;
		if (request != null)
		{
			PopTipsRoot.ShowPop(0, request.id);
		}
		return null;
	}
}
