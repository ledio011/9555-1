using System;
using Sproto;
using SprotoType;

// Token: 0x02000228 RID: 552
public class character_list_handler
{
	// Token: 0x060012CD RID: 4813 RVA: 0x0007AF14 File Offset: 0x00079114
	public static void character_list_response(SprotoTypeBase rep)
	{
		character_list.response response = rep as character_list.response;
		if (rep != null)
		{
		}
	}
}
