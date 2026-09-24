using System;
using Sproto;
using SprotoType;

// Token: 0x02000288 RID: 648
public class ret_open_item_package_handler
{
	// Token: 0x06001397 RID: 5015 RVA: 0x0007FF50 File Offset: 0x0007E150
	public static SprotoTypeBase ret_open_item_package_request(SprotoTypeBase req)
	{
		ret_open_item_package.request request = req as ret_open_item_package.request;
		if (request != null && SingletonUnity<OpenBoxRootLogic>.Exists)
		{
			SingletonUnity<OpenBoxRootLogic>.Instance.ShowTipPage(request);
		}
		return null;
	}
}
