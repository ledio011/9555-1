using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A77 RID: 2679
[ExecuteInEditMode]
public class TransDicts : MonoBehaviour
{
	// Token: 0x06004DFE RID: 19966 RVA: 0x001AA5C8 File Offset: 0x001A87C8
	private void Awake()
	{
		if (Application.isEditor && this.boneList.Count == 0)
		{
			Transform[] componentsInChildren = base.transform.gameObject.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this.boneList.Add(componentsInChildren[i]);
			}
		}
		else if (this.TransDict.Count == 0)
		{
			for (int j = 0; j < this.boneList.Count; j++)
			{
				this.TransDict.Add(this.boneList[j].name, this.boneList[j]);
			}
		}
	}

	// Token: 0x04003C90 RID: 15504
	public List<Transform> boneList = new List<Transform>();

	// Token: 0x04003C91 RID: 15505
	public Dictionary<string, Transform> TransDict = new Dictionary<string, Transform>();
}
