using System;
using UnityEngine;

// Token: 0x02000839 RID: 2105
public class BossCameraController : MonoBehaviour
{
	// Token: 0x060035E1 RID: 13793 RVA: 0x000DC438 File Offset: 0x000DA638
	public void Init(ObjCharacter objCharacter)
	{
		this.mObjCharacter = objCharacter;
		this.isStart = false;
	}

	// Token: 0x060035E2 RID: 13794 RVA: 0x000DC448 File Offset: 0x000DA648
	private void CaluParm()
	{
		this.mLookOffset = this.mObjCharacter.transform.position - base.transform.position;
		this.mLookYaw = MathUtil.WrapDegrees(Quaternion.LookRotation(this.mLookOffset).eulerAngles.y);
		this.mCurrentYaw = 0f;
		this.isStart = true;
	}

	// Token: 0x060035E3 RID: 13795 RVA: 0x000DC4B4 File Offset: 0x000DA6B4
	private void UpdatePosRot()
	{
		Vector3 position = this.mObjCharacter.transform.position;
		this.mCurrentYaw += Time.deltaTime * 60f * 6f;
		Quaternion quaternion = Quaternion.Euler(0f, -this.mCurrentYaw, 0f);
		Vector3 vector = position - quaternion * this.mLookOffset;
		Quaternion rotation = Quaternion.LookRotation(position - vector);
		base.transform.position = vector;
		base.transform.rotation = rotation;
		if (this.mCurrentYaw >= 270f)
		{
			Singleton<ObjManager>.Instance.MainPlayer.CameraController.LerpBackToPlayer(2f, null);
			Object.Destroy(this);
		}
	}

	// Token: 0x060035E4 RID: 13796 RVA: 0x000DC570 File Offset: 0x000DA770
	private void Start()
	{
	}

	// Token: 0x060035E5 RID: 13797 RVA: 0x000DC574 File Offset: 0x000DA774
	private void Update()
	{
		if (this.isStart)
		{
			this.UpdatePosRot();
		}
	}

	// Token: 0x060035E6 RID: 13798 RVA: 0x000DC588 File Offset: 0x000DA788
	public void CameraArrived()
	{
		this.CaluParm();
	}

	// Token: 0x0400232D RID: 9005
	private ObjCharacter mObjCharacter;

	// Token: 0x0400232E RID: 9006
	private bool isStart;

	// Token: 0x0400232F RID: 9007
	private float distance;

	// Token: 0x04002330 RID: 9008
	private Vector3 mLookOffset;

	// Token: 0x04002331 RID: 9009
	private float mLookYaw;

	// Token: 0x04002332 RID: 9010
	private float mCurrentYaw;

	// Token: 0x04002333 RID: 9011
	private float durationTime;

	// Token: 0x04002334 RID: 9012
	private float velocity;
}
