using System;
using UnityEngine;

// Token: 0x02000037 RID: 55
[AddComponentMenu("NGUI/Examples/Spin")]
public class Spin : MonoBehaviour
{
	// Token: 0x060000D3 RID: 211 RVA: 0x00006440 File Offset: 0x00004640
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRb = base.rigidbody;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x0000645C File Offset: 0x0000465C
	private void Update()
	{
		if (this.mRb == null)
		{
			this.ApplyDelta((!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime);
		}
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00006490 File Offset: 0x00004690
	private void FixedUpdate()
	{
		if (this.mRb != null)
		{
			this.ApplyDelta(Time.deltaTime);
		}
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x000064B0 File Offset: 0x000046B0
	public void ApplyDelta(float delta)
	{
		delta *= 360f;
		Quaternion quaternion = Quaternion.Euler(this.rotationsPerSecond * delta);
		if (this.mRb == null)
		{
			this.mTrans.rotation = this.mTrans.rotation * quaternion;
		}
		else
		{
			this.mRb.MoveRotation(this.mRb.rotation * quaternion);
		}
	}

	// Token: 0x040000EE RID: 238
	public Vector3 rotationsPerSecond = new Vector3(0f, 0.1f, 0f);

	// Token: 0x040000EF RID: 239
	public bool ignoreTimeScale;

	// Token: 0x040000F0 RID: 240
	private Rigidbody mRb;

	// Token: 0x040000F1 RID: 241
	private Transform mTrans;
}
