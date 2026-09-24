using System;
using UnityEngine;

// Token: 0x02000898 RID: 2200
public class ChangeWayBtn : MonoBehaviour
{
	// Token: 0x06003B7E RID: 15230 RVA: 0x00104098 File Offset: 0x00102298
	private void Start()
	{
	}

	// Token: 0x06003B7F RID: 15231 RVA: 0x0010409C File Offset: 0x0010229C
	private void Update()
	{
	}

	// Token: 0x06003B80 RID: 15232 RVA: 0x001040A0 File Offset: 0x001022A0
	public void OnClick()
	{
		foreach (Obj obj in Singleton<ObjManager>.Instance.ObjDict.Values)
		{
			ObjCharacter objCharacter = (ObjCharacter)obj;
			Vector3 vector;
			vector..ctor((float)Random.Range(-30, 30), 0f, (float)Random.Range(-30, 30));
			NavMeshHit navMeshHit;
			if (NavMesh.SamplePosition(vector, ref navMeshHit, 0.1f, 15))
			{
				objCharacter.MoveTo(vector, 1f, null);
			}
		}
	}
}
