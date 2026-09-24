using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A67 RID: 2663
[ExecuteInEditMode]
public class BoneSave : MonoBehaviour
{
	// Token: 0x06004DAD RID: 19885 RVA: 0x001A8C98 File Offset: 0x001A6E98
	private void Start()
	{
		if (Application.isEditor && this.list.Count == 0)
		{
			SkinnedMeshRenderer component = base.GetComponent<SkinnedMeshRenderer>();
			Transform[] bones = component.bones;
			for (int i = 0; i < bones.Length; i++)
			{
				this.list.Add(bones[i].name);
			}
		}
	}

	// Token: 0x04003C67 RID: 15463
	public List<string> list = new List<string>();
}
