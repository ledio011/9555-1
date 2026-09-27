using System;
using System.Collections.Generic;

// Token: 0x0200019F RID: 415
public class QualityData
{
	// Token: 0x17000358 RID: 856
	// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00064F50 File Offset: 0x00063150
	public List<int> EquipStarList
	{
		get
		{
			if (this.starlist == null || this.starlist.Count == 0)
			{
				this.starlist.Add(this.EquipStar0);
				this.starlist.Add(this.EquipStar1);
				this.starlist.Add(this.EquipStar2);
				this.starlist.Add(this.EquipStar3);
				this.starlist.Add(this.EquipStar4);
				this.starlist.Add(this.EquipStar5);
				this.starlist.Add(this.EquipStar6);
				this.starlist.Add(this.EquipStar7);
			}
			return this.starlist;
		}
	}

	// Token: 0x17000359 RID: 857
	// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00065008 File Offset: 0x00063208
	public float ModulusVal
	{
		get
		{
			return (float)this.Modulus / 10000f;
		}
	}

	// Token: 0x040011C1 RID: 4545
	public string ID;

	// Token: 0x040011C2 RID: 4546
	public int EquipQualityScore;

	// Token: 0x040011C3 RID: 4547
	public int AttQualityScore;

	// Token: 0x040011C4 RID: 4548
	public int AttInitialScore;

	// Token: 0x040011C5 RID: 4549
	public int Modulus;

	// Token: 0x040011C6 RID: 4550
	public int EquipInherit;

	// Token: 0x040011C7 RID: 4551
	public int WeaponInherit;

	// Token: 0x040011C8 RID: 4552
	public int EquipStar0;

	// Token: 0x040011C9 RID: 4553
	public int EquipStar1;

	// Token: 0x040011CA RID: 4554
	public int EquipStar2;

	// Token: 0x040011CB RID: 4555
	public int EquipStar3;

	// Token: 0x040011CC RID: 4556
	public int EquipStar4;

	// Token: 0x040011CD RID: 4557
	public int EquipStar5;

	// Token: 0x040011CE RID: 4558
	public int EquipStar6;

	// Token: 0x040011CF RID: 4559
	public int EquipStar7;

	// Token: 0x040011D0 RID: 4560
	private List<int> starlist = new List<int>();
}
