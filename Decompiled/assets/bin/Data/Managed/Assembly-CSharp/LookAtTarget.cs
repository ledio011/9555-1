using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
[AddComponentMenu("NGUI/Examples/Look At Target")]
public class LookAtTarget : MonoBehaviour
{
	// Token: 0x060000C4 RID: 196 RVA: 0x00005DBC File Offset: 0x00003FBC
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00005DCC File Offset: 0x00003FCC
	private void LateUpdate()
	{
		if (this.target != null)
		{
			Vector3 vector = this.target.position - this.mTrans.position;
			float magnitude = vector.magnitude;
			if (magnitude > 0.001f)
			{
				Quaternion quaternion = Quaternion.LookRotation(vector);
				this.mTrans.rotation = Quaternion.Slerp(this.mTrans.rotation, quaternion, Mathf.Clamp01(this.speed * Time.deltaTime));
			}
		}
	}

	// Token: 0x040000DD RID: 221
	public int level;

	// Token: 0x040000DE RID: 222
	public Transform target;

	// Token: 0x040000DF RID: 223
	public float speed = 8f;

	// Token: 0x040000E0 RID: 224
	private Transform mTrans;
}
