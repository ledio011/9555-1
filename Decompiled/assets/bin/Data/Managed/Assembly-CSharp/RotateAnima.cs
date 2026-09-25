using System;
using UnityEngine;

// Token: 0x020008B9 RID: 2233
public class RotateAnima : MonoBehaviour
{
	// Token: 0x06003C34 RID: 15412 RVA: 0x00107358 File Offset: 0x00105558
	private void Awake()
	{
		this.mTra = base.transform;
		this.isPlay = false;
	}

	// Token: 0x06003C35 RID: 15413 RVA: 0x00107370 File Offset: 0x00105570
	private void Update()
	{
		if (this.isPlay)
		{
			this.mTra.localEulerAngles -= new Vector3(0f, this.RotateSpeed * Time.deltaTime, 0f);
		}
	}

	// Token: 0x06003C36 RID: 15414 RVA: 0x001073BC File Offset: 0x001055BC
	public void PlayAnima()
	{
		this.isPlay = true;
	}

	// Token: 0x06003C37 RID: 15415 RVA: 0x001073C8 File Offset: 0x001055C8
	public void StopAnima()
	{
		this.isPlay = false;
	}

	// Token: 0x04002767 RID: 10087
	private Transform mTra;

	// Token: 0x04002768 RID: 10088
	private float RotateSpeed = 10f;

	// Token: 0x04002769 RID: 10089
	private bool isPlay = true;
}
