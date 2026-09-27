using System;
using UnityEngine;

// Token: 0x0200002F RID: 47
[AddComponentMenu("NGUI/Examples/Lag Rotation")]
public class LagRotation : MonoBehaviour
{
	// Token: 0x060000BF RID: 191 RVA: 0x00005CC8 File Offset: 0x00003EC8
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mRelative = this.mTrans.localRotation;
		this.mAbsolute = this.mTrans.rotation;
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00005D04 File Offset: 0x00003F04
	private void Update()
	{
		Transform parent = this.mTrans.parent;
		if (parent != null)
		{
			float num = (!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime;
			this.mAbsolute = Quaternion.Slerp(this.mAbsolute, parent.rotation * this.mRelative, num * this.speed);
			this.mTrans.rotation = this.mAbsolute;
		}
	}

	// Token: 0x040000D6 RID: 214
	public int updateOrder;

	// Token: 0x040000D7 RID: 215
	public float speed = 10f;

	// Token: 0x040000D8 RID: 216
	public bool ignoreTimeScale;

	// Token: 0x040000D9 RID: 217
	private Transform mTrans;

	// Token: 0x040000DA RID: 218
	private Quaternion mRelative;

	// Token: 0x040000DB RID: 219
	private Quaternion mAbsolute;
}
