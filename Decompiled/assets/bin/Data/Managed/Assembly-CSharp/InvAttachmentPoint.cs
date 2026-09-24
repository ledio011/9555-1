using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
[AddComponentMenu("NGUI/Examples/Item Attachment Point")]
public class InvAttachmentPoint : MonoBehaviour
{
	// Token: 0x0600008D RID: 141 RVA: 0x00004C38 File Offset: 0x00002E38
	public GameObject Attach(GameObject prefab)
	{
		if (this.mPrefab != prefab)
		{
			this.mPrefab = prefab;
			if (this.mChild != null)
			{
				Object.Destroy(this.mChild);
			}
			if (this.mPrefab != null)
			{
				Transform transform = base.transform;
				this.mChild = (Object.Instantiate(this.mPrefab, transform.position, transform.rotation) as GameObject);
				Transform transform2 = this.mChild.transform;
				transform2.parent = transform;
				transform2.localPosition = Vector3.zero;
				transform2.localRotation = Quaternion.identity;
				transform2.localScale = Vector3.one;
			}
		}
		return this.mChild;
	}

	// Token: 0x04000082 RID: 130
	public InvBaseItem.Slot slot;

	// Token: 0x04000083 RID: 131
	private GameObject mPrefab;

	// Token: 0x04000084 RID: 132
	private GameObject mChild;
}
