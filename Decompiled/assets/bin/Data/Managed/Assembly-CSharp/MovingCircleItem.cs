using System;
using UnityEngine;

// Token: 0x02000204 RID: 516
public class MovingCircleItem : SingletonUnity<MovingCircleItem>
{
	// Token: 0x0600117B RID: 4475 RVA: 0x000713B4 File Offset: 0x0006F5B4
	public void ActiveMovingCircle(Vector3 pos)
	{
		base.transform.position = pos + Vector3.up * 0.2f;
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x000713F0 File Offset: 0x0006F5F0
	public void DisactiveMovingCircle()
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
		}
	}
}
