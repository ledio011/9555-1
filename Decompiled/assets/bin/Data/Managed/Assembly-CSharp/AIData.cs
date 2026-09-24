using System;
using UnityEngine;

// Token: 0x02000143 RID: 323
public class AIData
{
	// Token: 0x1700029F RID: 671
	// (get) Token: 0x06000E98 RID: 3736 RVA: 0x000600A8 File Offset: 0x0005E2A8
	public float LockDistanceMeter
	{
		get
		{
			return (float)this.LockDistance / 100f;
		}
	}

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x06000E99 RID: 3737 RVA: 0x000600B8 File Offset: 0x0005E2B8
	public float SqrtLockDistanceMeter
	{
		get
		{
			return (float)(this.LockDistance * this.LockDistance) / 10000f;
		}
	}

	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x06000E9A RID: 3738 RVA: 0x000600D0 File Offset: 0x0005E2D0
	public float AroundTimeSecond
	{
		get
		{
			return (float)Random.Range(this.AroundTimeMin, this.AroundTimeMax) / 1000f;
		}
	}

	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x06000E9B RID: 3739 RVA: 0x000600EC File Offset: 0x0005E2EC
	public int AroundAngle
	{
		get
		{
			return Random.Range(this.AroundMinAngel, this.AroundMaxAngel);
		}
	}

	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00060100 File Offset: 0x0005E300
	public float AroundDistanceMeter
	{
		get
		{
			return (float)Random.Range(this.AroundMinDistance, this.AroundMaxDistance) / 100f;
		}
	}

	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x06000E9D RID: 3741 RVA: 0x0006011C File Offset: 0x0005E31C
	public float ActionTimeSecond
	{
		get
		{
			return (float)Random.Range(this.ActionMinTime, this.ActionMaxTime) / 1000f;
		}
	}

	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00060138 File Offset: 0x0005E338
	public float ReturnDistanceMeter
	{
		get
		{
			return (float)this.ReturnDistance / 100f;
		}
	}

	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x06000E9F RID: 3743 RVA: 0x00060148 File Offset: 0x0005E348
	public float PatrolDistanceMeter
	{
		get
		{
			return (float)this.PatrolDistance / 100f;
		}
	}

	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x00060158 File Offset: 0x0005E358
	public PATROL_TYPE PATROL_Type
	{
		get
		{
			return (PATROL_TYPE)this.PatrolType;
		}
	}

	// Token: 0x04000BF5 RID: 3061
	public string ID;

	// Token: 0x04000BF6 RID: 3062
	public int LockType;

	// Token: 0x04000BF7 RID: 3063
	public int LockDistance;

	// Token: 0x04000BF8 RID: 3064
	public int AroundProb;

	// Token: 0x04000BF9 RID: 3065
	public int AroundTimeMin;

	// Token: 0x04000BFA RID: 3066
	public int AroundTimeMax;

	// Token: 0x04000BFB RID: 3067
	public int AroundMaxAngel;

	// Token: 0x04000BFC RID: 3068
	public int AroundMinAngel;

	// Token: 0x04000BFD RID: 3069
	public int AroundMinDistance;

	// Token: 0x04000BFE RID: 3070
	public int AroundMaxDistance;

	// Token: 0x04000BFF RID: 3071
	public int ActionMinTime;

	// Token: 0x04000C00 RID: 3072
	public int ActionMaxTime;

	// Token: 0x04000C01 RID: 3073
	public int SkillCDMin;

	// Token: 0x04000C02 RID: 3074
	public int SkillCDMax;

	// Token: 0x04000C03 RID: 3075
	public int ReturnDistance;

	// Token: 0x04000C04 RID: 3076
	public int ChangeTargetCD;

	// Token: 0x04000C05 RID: 3077
	public int PatrolType;

	// Token: 0x04000C06 RID: 3078
	public int PatrolDistance;

	// Token: 0x04000C07 RID: 3079
	public int WaitTime;
}
