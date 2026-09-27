using System;
using Sproto;
using SprotoType;

// Token: 0x02000239 RID: 569
public class heart_beat_handler
{
	// Token: 0x060012F3 RID: 4851 RVA: 0x0007BACC File Offset: 0x00079CCC
	public static void heart_beat_response(SprotoTypeBase rep)
	{
		heart_beat.response response = rep as heart_beat.response;
		if (rep != null)
		{
		}
	}
}
