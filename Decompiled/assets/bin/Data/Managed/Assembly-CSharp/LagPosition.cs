using System;
using UnityEngine;

// Token: 0x0200002E RID: 46
[AddComponentMenu("NGUI/Examples/Lag Position")]
public class LagPosition : MonoBehaviour
{
	// Token: 0x060000BC RID: 188 RVA: 0x00005B6C File Offset: 0x00003D6C
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mAbsolute = this.mTrans.position;
		this.mRelative = this.mTrans.localPosition;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00005BA8 File Offset: 0x00003DA8
	private void Update()
	{
		Transform parent = this.mTrans.parent;
		if (parent != null)
		{
			float num = (!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime;
			Vector3 vector = parent.position + parent.rotation * this.mRelative;
			this.mAbsolute.x = Mathf.Lerp(this.mAbsolute.x, vector.x, Mathf.Clamp01(num * this.speed.x));
			this.mAbsolute.y = Mathf.Lerp(this.mAbsolute.y, vector.y, Mathf.Clamp01(num * this.speed.y));
			this.mAbsolute.z = Mathf.Lerp(this.mAbsolute.z, vector.z, Mathf.Clamp01(num * this.speed.z));
			this.mTrans.position = this.mAbsolute;
		}
	}

	// Token: 0x040000D0 RID: 208
	public int updateOrder;

	// Token: 0x040000D1 RID: 209
	public Vector3 speed = new Vector3(10f, 10f, 10f);

	// Token: 0x040000D2 RID: 210
	public bool ignoreTimeScale;

	// Token: 0x040000D3 RID: 211
	private Transform mTrans;

	// Token: 0x040000D4 RID: 212
	private Vector3 mRelative;

	// Token: 0x040000D5 RID: 213
	private Vector3 mAbsolute;
}
