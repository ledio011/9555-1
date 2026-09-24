using System;
using UnityEngine;

// Token: 0x0200083C RID: 2108
[Serializable]
public class CamRockInfo
{
	// Token: 0x06003621 RID: 13857 RVA: 0x000DE4A8 File Offset: 0x000DC6A8
	public void Init()
	{
		this.CamRockId = string.Empty;
		this.RockTime = 0f;
		this.NeedRockTime = 0f;
		this.DelayTime = 0f;
		this.XPosCurve = null;
		this.YPosCurve = null;
		this.ZPosCurve = null;
		this.XRotCurve = null;
		this.YRotCurve = null;
		this.ZRotCurve = null;
		this.WRotCurve = null;
	}

	// Token: 0x06003622 RID: 13858 RVA: 0x000DE514 File Offset: 0x000DC714
	public bool IsValid()
	{
		return !string.IsNullOrEmpty(this.CamRockId);
	}

	// Token: 0x04002386 RID: 9094
	public string CamRockId = string.Empty;

	// Token: 0x04002387 RID: 9095
	public float RockTime;

	// Token: 0x04002388 RID: 9096
	public float NeedRockTime;

	// Token: 0x04002389 RID: 9097
	public float DelayTime;

	// Token: 0x0400238A RID: 9098
	public AnimationCurve XPosCurve;

	// Token: 0x0400238B RID: 9099
	public AnimationCurve YPosCurve;

	// Token: 0x0400238C RID: 9100
	public AnimationCurve ZPosCurve;

	// Token: 0x0400238D RID: 9101
	public AnimationCurve XRotCurve;

	// Token: 0x0400238E RID: 9102
	public AnimationCurve YRotCurve;

	// Token: 0x0400238F RID: 9103
	public AnimationCurve ZRotCurve;

	// Token: 0x04002390 RID: 9104
	public AnimationCurve WRotCurve;
}
