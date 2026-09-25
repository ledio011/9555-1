using System;
using UnityEngine;

// Token: 0x020001A3 RID: 419
public class SceneComponentData
{
	// Token: 0x1700035C RID: 860
	// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x00065178 File Offset: 0x00063378
	public bool CloseQiqiuFlag
	{
		get
		{
			return this.CloseQiqiuren == 1;
		}
	}

	// Token: 0x1700035D RID: 861
	// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00065184 File Offset: 0x00063384
	public bool StaticCombine
	{
		get
		{
			return this.IsCombinStatic == 1;
		}
	}

	// Token: 0x1700035E RID: 862
	// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00065190 File Offset: 0x00063390
	public int[] EndTimes
	{
		get
		{
			if ((this.mEndTimes == null || this.mEndTimes.Length == 0) && !string.IsNullOrEmpty(this.EndTime))
			{
				string[] array = this.EndTime.Split(new char[]
				{
					'-'
				});
				this.mEndTimes = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mEndTimes[i] = int.Parse(array[i]);
				}
			}
			return this.mEndTimes;
		}
	}

	// Token: 0x1700035F RID: 863
	// (get) Token: 0x06000FEB RID: 4075 RVA: 0x00065214 File Offset: 0x00063414
	public int[] Starttimes
	{
		get
		{
			if ((this.mStarttimes == null || this.mStarttimes.Length == 0) && !string.IsNullOrEmpty(this.Starttime))
			{
				string[] array = this.Starttime.Split(new char[]
				{
					'-'
				});
				this.mStarttimes = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mStarttimes[i] = int.Parse(array[i]);
				}
			}
			return this.mStarttimes;
		}
	}

	// Token: 0x17000360 RID: 864
	// (get) Token: 0x06000FEC RID: 4076 RVA: 0x00065298 File Offset: 0x00063498
	public Vector3 FPos
	{
		get
		{
			if (this.mPos == null)
			{
				string[] array = this.POS.Split(new char[]
				{
					'#'
				});
				this.mPos = new float[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mPos[i] = (float)int.Parse(array[i]) / 100f;
				}
			}
			return new Vector3(this.mPos[0], this.mPos[1], this.mPos[2]);
		}
	}

	// Token: 0x17000361 RID: 865
	// (get) Token: 0x06000FED RID: 4077 RVA: 0x00065320 File Offset: 0x00063520
	public Vector3 FAngle
	{
		get
		{
			if (this.mAngle == null)
			{
				string[] array = this.Angle.Split(new char[]
				{
					'#'
				});
				this.mAngle = new float[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mAngle[i] = (float)int.Parse(array[i]) / 100f;
				}
			}
			return new Vector3(this.mAngle[0], this.mAngle[1], this.mAngle[2]);
		}
	}

	// Token: 0x17000362 RID: 866
	// (get) Token: 0x06000FEE RID: 4078 RVA: 0x000653A8 File Offset: 0x000635A8
	public Vector3 FScale
	{
		get
		{
			if (this.mScale == null)
			{
				string[] array = this.Scale.Split(new char[]
				{
					'#'
				});
				this.mScale = new float[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mScale[i] = (float)int.Parse(array[i]) / 100f;
				}
			}
			return new Vector3(this.mScale[0], this.mScale[1], this.mScale[2]);
		}
	}

	// Token: 0x040011FB RID: 4603
	public string MapID;

	// Token: 0x040011FC RID: 4604
	public string POS;

	// Token: 0x040011FD RID: 4605
	public string Angle;

	// Token: 0x040011FE RID: 4606
	public string Scale;

	// Token: 0x040011FF RID: 4607
	public string PrefabName = string.Empty;

	// Token: 0x04001200 RID: 4608
	public int IsDanceShow;

	// Token: 0x04001201 RID: 4609
	public int CloseQiqiuren;

	// Token: 0x04001202 RID: 4610
	public int IsCombinStatic = 1;

	// Token: 0x04001203 RID: 4611
	public string DependName = string.Empty;

	// Token: 0x04001204 RID: 4612
	public string Starttime = string.Empty;

	// Token: 0x04001205 RID: 4613
	public string EndTime = string.Empty;

	// Token: 0x04001206 RID: 4614
	private int[] mEndTimes;

	// Token: 0x04001207 RID: 4615
	private int[] mStarttimes;

	// Token: 0x04001208 RID: 4616
	private float[] mPos;

	// Token: 0x04001209 RID: 4617
	private float[] mAngle;

	// Token: 0x0400120A RID: 4618
	private float[] mScale;
}
