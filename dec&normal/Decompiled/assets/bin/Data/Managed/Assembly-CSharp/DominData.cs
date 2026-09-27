using System;
using UnityEngine;

// Token: 0x02000168 RID: 360
public class DominData
{
	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x06000F0F RID: 3855 RVA: 0x00061EFC File Offset: 0x000600FC
	public float Speed1_Second
	{
		get
		{
			return (float)this.Speed1 / 1000f;
		}
	}

	// Token: 0x170002DA RID: 730
	// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00061F0C File Offset: 0x0006010C
	public float Speed2_Second
	{
		get
		{
			return (float)this.Speed2 / 1000f;
		}
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x00061F1C File Offset: 0x0006011C
	public Vector3 GetPos()
	{
		float num = (float)this.PosX / 100f;
		float num2 = (float)this.PosZ / 100f;
		return new Vector3(num, SceneManager.GetHitHeight(new Vector3(num, 0f, num2)), num2);
	}

	// Token: 0x170002DB RID: 731
	// (get) Token: 0x06000F12 RID: 3858 RVA: 0x00061F60 File Offset: 0x00060160
	public string GetName
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.ZoneName, new object[0]);
		}
	}

	// Token: 0x04000E22 RID: 3618
	public string ID = string.Empty;

	// Token: 0x04000E23 RID: 3619
	public string ZoneName = string.Empty;

	// Token: 0x04000E24 RID: 3620
	public string BuildingName = string.Empty;

	// Token: 0x04000E25 RID: 3621
	public string Resources1 = string.Empty;

	// Token: 0x04000E26 RID: 3622
	public int Total1;

	// Token: 0x04000E27 RID: 3623
	public int Speed1 = 1;

	// Token: 0x04000E28 RID: 3624
	public string PriceType1 = string.Empty;

	// Token: 0x04000E29 RID: 3625
	public int Price1;

	// Token: 0x04000E2A RID: 3626
	public string Resources2 = string.Empty;

	// Token: 0x04000E2B RID: 3627
	public int Total2;

	// Token: 0x04000E2C RID: 3628
	public int Speed2 = 1;

	// Token: 0x04000E2D RID: 3629
	public string PriceType2 = string.Empty;

	// Token: 0x04000E2E RID: 3630
	public int Price2;

	// Token: 0x04000E2F RID: 3631
	public int DominantMin;

	// Token: 0x04000E30 RID: 3632
	public int DominantMax;

	// Token: 0x04000E31 RID: 3633
	public int Lossspeed;

	// Token: 0x04000E32 RID: 3634
	public int matchpower;

	// Token: 0x04000E33 RID: 3635
	public int LevelMin;

	// Token: 0x04000E34 RID: 3636
	public string MapID = string.Empty;

	// Token: 0x04000E35 RID: 3637
	public int IsOpen = 1;

	// Token: 0x04000E36 RID: 3638
	public int Entrytype;

	// Token: 0x04000E37 RID: 3639
	public string AcceptMapID;

	// Token: 0x04000E38 RID: 3640
	public int PosX;

	// Token: 0x04000E39 RID: 3641
	public int PosZ;
}
