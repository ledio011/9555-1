using System;
using Sproto;
using SprotoType;

// Token: 0x0200021F RID: 543
public class ask_character_info_handler
{
	// Token: 0x060012B8 RID: 4792 RVA: 0x0007AAC8 File Offset: 0x00078CC8
	public static void ask_character_info_response(SprotoTypeBase rep)
	{
		ask_character_info.response response = rep as ask_character_info.response;
		if (rep != null)
		{
		}
	}
}
