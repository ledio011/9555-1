using System;
using UnityEngine;

// Token: 0x02000173 RID: 371
public class FxEffInfoData
{
	// Token: 0x170002FF RID: 767
	// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00062D2C File Offset: 0x00060F2C
	public bool AutoMoveFlag
	{
		get
		{
			return this.AutoMove == 1;
		}
	}

	// Token: 0x17000300 RID: 768
	// (get) Token: 0x06000F52 RID: 3922 RVA: 0x00062D38 File Offset: 0x00060F38
	public float EffDurationTimeSeconds
	{
		get
		{
			return (float)this.EffDurationTime / 1000f;
		}
	}

	// Token: 0x17000301 RID: 769
	// (get) Token: 0x06000F53 RID: 3923 RVA: 0x00062D48 File Offset: 0x00060F48
	public float fEffDelayTimeSeconds
	{
		get
		{
			return (float)this.EffDelayTime / 1000f;
		}
	}

	// Token: 0x17000302 RID: 770
	// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00062D58 File Offset: 0x00060F58
	public Vector3 Position
	{
		get
		{
			return new Vector3(this.PX, this.PY, this.PZ);
		}
	}

	// Token: 0x17000303 RID: 771
	// (get) Token: 0x06000F55 RID: 3925 RVA: 0x00062D74 File Offset: 0x00060F74
	public Vector3 Angel
	{
		get
		{
			return new Vector3(this.AX, this.AY, this.AZ);
		}
	}

	// Token: 0x04000EF4 RID: 3828
	public string ID;

	// Token: 0x04000EF5 RID: 3829
	public string EffName = string.Empty;

	// Token: 0x04000EF6 RID: 3830
	public string EffFilePath = string.Empty;

	// Token: 0x04000EF7 RID: 3831
	public int EffDurationTime;

	// Token: 0x04000EF8 RID: 3832
	public int EffDelayTime;

	// Token: 0x04000EF9 RID: 3833
	public string EffLinkNode = string.Empty;

	// Token: 0x04000EFA RID: 3834
	public float PX;

	// Token: 0x04000EFB RID: 3835
	public float PY;

	// Token: 0x04000EFC RID: 3836
	public float PZ;

	// Token: 0x04000EFD RID: 3837
	public float AX;

	// Token: 0x04000EFE RID: 3838
	public float AY;

	// Token: 0x04000EFF RID: 3839
	public float AZ;

	// Token: 0x04000F00 RID: 3840
	public int AutoMove;
}
