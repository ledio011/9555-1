using System;
using UnityEngine;

// Token: 0x02000145 RID: 325
public class ActionData
{
	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x000601B4 File Offset: 0x0005E3B4
	public float CrossInTimeSecond
	{
		get
		{
			return (float)this.CrossInTime / 1000f;
		}
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x000601C4 File Offset: 0x0005E3C4
	public float CrossOutTimeSecond
	{
		get
		{
			return (float)this.CrossOutTime / 1000f;
		}
	}

	// Token: 0x170002AA RID: 682
	// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x000601D4 File Offset: 0x0005E3D4
	public float AnimDurationTimeSecond
	{
		get
		{
			return (float)this.AnimDurationTime / 1000f;
		}
	}

	// Token: 0x170002AB RID: 683
	// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x000601E4 File Offset: 0x0005E3E4
	public WrapMode AnimationWrapMode
	{
		get
		{
			switch (this.AnimWrapMode)
			{
			case 0:
				return 1;
			case 1:
				return 2;
			case 2:
				return 8;
			default:
				return 1;
			}
		}
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x00060218 File Offset: 0x0005E418
	public string GetAnimaFileName()
	{
		string[] array = this.ID.Split(new char[]
		{
			'_'
		});
		if (array.Length > 2)
		{
			return string.Format("{0}_{1}", array[0], array[1]);
		}
		return null;
	}

	// Token: 0x04000C0C RID: 3084
	public string ID = string.Empty;

	// Token: 0x04000C0D RID: 3085
	[ServerExclude("ServerNoUse")]
	public string AnimName = string.Empty;

	// Token: 0x04000C0E RID: 3086
	public int AnimWrapMode;

	// Token: 0x04000C0F RID: 3087
	public int AnimDurationTime;

	// Token: 0x04000C10 RID: 3088
	public int AnimCanBeBreak;

	// Token: 0x04000C11 RID: 3089
	[ServerExclude("ServerNoUse")]
	public string NextActionName = string.Empty;

	// Token: 0x04000C12 RID: 3090
	public string FxEffID = string.Empty;

	// Token: 0x04000C13 RID: 3091
	public int CrossInTime = -1;

	// Token: 0x04000C14 RID: 3092
	public int CrossOutTime = -1;

	// Token: 0x04000C15 RID: 3093
	public int SoundID = -1;
}
