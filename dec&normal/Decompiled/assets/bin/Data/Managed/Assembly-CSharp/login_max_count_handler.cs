using System;
using Sproto;
using SprotoType;

// Token: 0x0200023D RID: 573
public class login_max_count_handler
{
	// Token: 0x060012FE RID: 4862 RVA: 0x0007BD00 File Offset: 0x00079F00
	public static SprotoTypeBase login_max_count_request(SprotoTypeBase req)
	{
		login_max_count.request request = req as login_max_count.request;
		if (request != null)
		{
			NoticeLogic.AddNotifyData("#{200063}", true, false);
		}
		return null;
	}
}
