using System;
using UnityEngine;

// Token: 0x02000A78 RID: 2680
public class TransformUtil
{
	// Token: 0x06004E00 RID: 19968 RVA: 0x001AA684 File Offset: 0x001A8884
	public static GameObject FindChildGameObject(GameObject gameObject, string name)
	{
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].gameObject.name.CompareTo(name) == 0)
			{
				return componentsInChildren[i].gameObject;
			}
		}
		return null;
	}

	// Token: 0x06004E01 RID: 19969 RVA: 0x001AA6D0 File Offset: 0x001A88D0
	public static Transform FindChildTransform(Transform[] trans, string name)
	{
		for (int i = 0; i < trans.Length; i++)
		{
			if (trans[i].name.CompareTo(name) == 0)
			{
				return trans[i];
			}
		}
		return null;
	}

	// Token: 0x06004E02 RID: 19970 RVA: 0x001AA70C File Offset: 0x001A890C
	public static Transform FindChildTransform(Transform transform, string name, bool active = false)
	{
		Transform[] componentsInChildren = transform.gameObject.GetComponentsInChildren<Transform>(active);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].name.CompareTo(name) == 0)
			{
				return componentsInChildren[i];
			}
		}
		return null;
	}
}
