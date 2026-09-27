using System;
using UnityEngine;

// Token: 0x02000A6F RID: 2671
public class Log
{
	// Token: 0x06004DCF RID: 19919 RVA: 0x001A9BE0 File Offset: 0x001A7DE0
	public static string getTitle()
	{
		return string.Empty;
	}

	// Token: 0x06004DD0 RID: 19920 RVA: 0x001A9BE8 File Offset: 0x001A7DE8
	public static void DEBUG_MSG(object s)
	{
		if (DEBUGLEVEL.DEBUG >= Log.debugLevel)
		{
			Debug.Log(Log.getTitle() + s);
		}
	}

	// Token: 0x06004DD1 RID: 19921 RVA: 0x001A9C08 File Offset: 0x001A7E08
	public static void WARING_MSG(object s)
	{
		if (DEBUGLEVEL.WARING >= Log.debugLevel)
		{
			Debug.LogWarning(Log.getTitle() + s);
		}
	}

	// Token: 0x06004DD2 RID: 19922 RVA: 0x001A9C28 File Offset: 0x001A7E28
	public static void ERROR_MSG(object s)
	{
		if (DEBUGLEVEL.ERRO >= Log.debugLevel)
		{
			Debug.LogError(Log.getTitle() + s);
		}
	}

	// Token: 0x04003C7F RID: 15487
	public static DEBUGLEVEL debugLevel;
}
