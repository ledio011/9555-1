using System;
using Sproto;
using SprotoType;

// Token: 0x02000241 RID: 577
public class move_handler
{
	// Token: 0x06001307 RID: 4871 RVA: 0x0007C250 File Offset: 0x0007A450
	public static void move_response(SprotoTypeBase rep)
	{
		move.response response = rep as move.response;
		if (rep != null)
		{
		}
	}
}
