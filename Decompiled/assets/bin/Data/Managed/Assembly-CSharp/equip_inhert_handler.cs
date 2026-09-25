using System;
using Sproto;
using SprotoType;

// Token: 0x0200022F RID: 559
public class equip_inhert_handler
{
	// Token: 0x060012DF RID: 4831 RVA: 0x0007B460 File Offset: 0x00079660
	public static void equip_inhert_response(SprotoTypeBase rep)
	{
		equip_inhert.response response = rep as equip_inhert.response;
		if (rep != null)
		{
		}
	}
}
