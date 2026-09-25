using System;
using UnityEngine;

// Token: 0x02000183 RID: 387
public class MapAreaInfoData
{
	// Token: 0x06000F80 RID: 3968 RVA: 0x0006371C File Offset: 0x0006191C
	public void Init()
	{
		this.p1x.value = this.Point1X;
		this.p1z.value = this.Point1Z;
		this.p2x.value = this.Point2X;
		this.p2z.value = this.Point2Z;
		this.p3x.value = this.Point3X;
		this.p3z.value = this.Point3Z;
		this.p4x.value = this.Point4X;
		this.p4z.value = this.Point4Z;
		this.rx.value = this.Range;
	}

	// Token: 0x17000315 RID: 789
	// (get) Token: 0x06000F81 RID: 3969 RVA: 0x000637C4 File Offset: 0x000619C4
	public Vector2[] PointList
	{
		get
		{
			this.mPointList[0] = new Vector2((float)this.p1x.value / 100f, (float)this.p1z.value / 100f);
			this.mPointList[1] = new Vector2((float)this.p2x.value / 100f, (float)this.p2z.value / 100f);
			this.mPointList[2] = new Vector2((float)this.p3x.value / 100f, (float)this.p3z.value / 100f);
			this.mPointList[3] = new Vector2((float)this.p4x.value / 100f, (float)this.p4z.value / 100f);
			return this.mPointList;
		}
	}

	// Token: 0x17000316 RID: 790
	// (get) Token: 0x06000F82 RID: 3970 RVA: 0x000638C0 File Offset: 0x00061AC0
	public float CircleRange
	{
		get
		{
			return (float)this.rx.value / 100f;
		}
	}

	// Token: 0x04000FEC RID: 4076
	public string ID = string.Empty;

	// Token: 0x04000FED RID: 4077
	public int Type;

	// Token: 0x04000FEE RID: 4078
	public int Point1X;

	// Token: 0x04000FEF RID: 4079
	public int Point1Z;

	// Token: 0x04000FF0 RID: 4080
	public int Point2X;

	// Token: 0x04000FF1 RID: 4081
	public int Point2Z;

	// Token: 0x04000FF2 RID: 4082
	public int Point3X;

	// Token: 0x04000FF3 RID: 4083
	public int Point3Z;

	// Token: 0x04000FF4 RID: 4084
	public int Point4X;

	// Token: 0x04000FF5 RID: 4085
	public int Point4Z;

	// Token: 0x04000FF6 RID: 4086
	public int Range;

	// Token: 0x04000FF7 RID: 4087
	private XorInt p1x = new XorInt();

	// Token: 0x04000FF8 RID: 4088
	private XorInt p1z = new XorInt();

	// Token: 0x04000FF9 RID: 4089
	private XorInt p2x = new XorInt();

	// Token: 0x04000FFA RID: 4090
	private XorInt p2z = new XorInt();

	// Token: 0x04000FFB RID: 4091
	private XorInt p3x = new XorInt();

	// Token: 0x04000FFC RID: 4092
	private XorInt p3z = new XorInt();

	// Token: 0x04000FFD RID: 4093
	private XorInt p4x = new XorInt();

	// Token: 0x04000FFE RID: 4094
	private XorInt p4z = new XorInt();

	// Token: 0x04000FFF RID: 4095
	private XorInt rx = new XorInt();

	// Token: 0x04001000 RID: 4096
	private Vector2[] mPointList = new Vector2[4];
}
