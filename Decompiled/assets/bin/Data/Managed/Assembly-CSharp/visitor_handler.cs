using System;
using Sproto;
using SprotoType;

// Token: 0x020002D9 RID: 729
public class visitor_handler
{
	// Token: 0x0600143A RID: 5178 RVA: 0x000833C4 File Offset: 0x000815C4
	public static void visitor_response(SprotoTypeBase rep)
	{
		visitor.response response = rep as visitor.response;
		if (rep != null)
		{
		}
	}
}
