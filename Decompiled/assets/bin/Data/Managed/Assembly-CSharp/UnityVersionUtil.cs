using System;
using UnityEngine;

// Token: 0x02000A7D RID: 2685
public class UnityVersionUtil
{
	// Token: 0x06004E13 RID: 19987 RVA: 0x001AADA4 File Offset: 0x001A8FA4
	public static void SetActiveRecursive(GameObject go, bool state)
	{
		if (go == null)
		{
			return;
		}
		go.SetActive(state);
	}

	// Token: 0x06004E14 RID: 19988 RVA: 0x001AADBC File Offset: 0x001A8FBC
	public static bool IsActive(GameObject go)
	{
		return !(go == null) && go.activeInHierarchy;
	}

	// Token: 0x06004E15 RID: 19989 RVA: 0x001AADD4 File Offset: 0x001A8FD4
	public static bool IsactiveInHierarchy(GameObject go)
	{
		return !(go == null) && go.activeInHierarchy;
	}
}
