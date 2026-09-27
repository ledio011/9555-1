using System;
using Sproto;
using SprotoType;

// Token: 0x0200023C RID: 572
public class login_handler
{
	// Token: 0x060012FC RID: 4860 RVA: 0x0007BCDC File Offset: 0x00079EDC
	public static void login_response(SprotoTypeBase rep)
	{
		login.response response = rep as login.response;
		if (rep != null)
		{
		}
	}
}
