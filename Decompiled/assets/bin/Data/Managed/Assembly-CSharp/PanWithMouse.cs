using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
[AddComponentMenu("NGUI/Examples/Pan With Mouse")]
public class PanWithMouse : MonoBehaviour
{
	// Token: 0x060000C9 RID: 201 RVA: 0x00005ED0 File Offset: 0x000040D0
	private void Start()
	{
		this.mTrans = base.transform;
		this.mStart = this.mTrans.localRotation;
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00005EF0 File Offset: 0x000040F0
	private void Update()
	{
		float deltaTime = RealTime.deltaTime;
		Vector3 mousePosition = Input.mousePosition;
		float num = (float)Screen.width * 0.5f;
		float num2 = (float)Screen.height * 0.5f;
		if (this.range < 0.1f)
		{
			this.range = 0.1f;
		}
		float num3 = Mathf.Clamp((mousePosition.x - num) / num / this.range, -1f, 1f);
		float num4 = Mathf.Clamp((mousePosition.y - num2) / num2 / this.range, -1f, 1f);
		this.mRot = Vector2.Lerp(this.mRot, new Vector2(num3, num4), deltaTime * 5f);
		this.mTrans.localRotation = this.mStart * Quaternion.Euler(-this.mRot.y * this.degrees.y, this.mRot.x * this.degrees.x, 0f);
	}

	// Token: 0x040000E1 RID: 225
	public Vector2 degrees = new Vector2(5f, 3f);

	// Token: 0x040000E2 RID: 226
	public float range = 1f;

	// Token: 0x040000E3 RID: 227
	private Transform mTrans;

	// Token: 0x040000E4 RID: 228
	private Quaternion mStart;

	// Token: 0x040000E5 RID: 229
	private Vector2 mRot = Vector2.zero;
}
