using System;
using Sproto;
using SprotoType;

// Token: 0x0200022C RID: 556
public class count_down_handler
{
	// Token: 0x060012D9 RID: 4825 RVA: 0x0007B2AC File Offset: 0x000794AC
	public static SprotoTypeBase count_down_request(SprotoTypeBase req)
	{
		count_down.request request = req as count_down.request;
		if (request != null)
		{
			long type = request.type;
			if (type != 1L)
			{
				if (type != 2L)
				{
				}
			}
		}
		return null;
	}
}
