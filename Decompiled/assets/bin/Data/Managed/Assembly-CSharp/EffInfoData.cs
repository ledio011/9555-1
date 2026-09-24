using System;

// Token: 0x0200016A RID: 362
public class EffInfoData
{
	// Token: 0x170002DC RID: 732
	// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00062030 File Offset: 0x00060230
	public float DamageMultiAdd_f
	{
		get
		{
			return (float)this.DamageMultiAdd / 10000f;
		}
	}

	// Token: 0x170002DD RID: 733
	// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00062040 File Offset: 0x00060240
	public float BuffProbFloat
	{
		get
		{
			return (float)this.BuffProb / 10000f;
		}
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x00062050 File Offset: 0x00060250
	public void Init()
	{
		this.dmx.value = (float)this.DamageMulti / 10000f;
		this.movxt.value = (float)this.MoveTime / 1000f;
		this.movx.value = (float)this.MoveDistance / 100f;
		this.bufx.value = (float)this.BuffDuration / 1000f;
		this.parm1x.value = (float)this.Param1 / 100f;
		this.parm2x.value = (float)this.Param2 / 100f;
		this.parm3x.value = (float)this.Param3 / 100f;
		this.parm4x.value = (float)this.Param4 / 100f;
		this.parm5x.value = (float)this.Param5 / 100f;
		this.damagex.value = this.Damage;
	}

	// Token: 0x170002DE RID: 734
	// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00062148 File Offset: 0x00060348
	public int Damagex
	{
		get
		{
			return this.damagex.value;
		}
	}

	// Token: 0x170002DF RID: 735
	// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00062158 File Offset: 0x00060358
	public float DamageMulti_100f
	{
		get
		{
			return this.dmx.value;
		}
	}

	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00062168 File Offset: 0x00060368
	public float MoveTimeSecond
	{
		get
		{
			return this.movxt.value;
		}
	}

	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00062178 File Offset: 0x00060378
	public float MoveDistanceMeter
	{
		get
		{
			return this.movx.value;
		}
	}

	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00062188 File Offset: 0x00060388
	public float BuffDurationSecond
	{
		get
		{
			return this.bufx.value;
		}
	}

	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00062198 File Offset: 0x00060398
	public float Param1Meter
	{
		get
		{
			return this.parm1x.value;
		}
	}

	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x06000F1E RID: 3870 RVA: 0x000621A8 File Offset: 0x000603A8
	public float Param2Meter
	{
		get
		{
			return this.parm2x.value;
		}
	}

	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x06000F1F RID: 3871 RVA: 0x000621B8 File Offset: 0x000603B8
	public float Param3Meter
	{
		get
		{
			return this.parm3x.value;
		}
	}

	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x06000F20 RID: 3872 RVA: 0x000621C8 File Offset: 0x000603C8
	public float Param4Meter
	{
		get
		{
			return this.parm4x.value;
		}
	}

	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x06000F21 RID: 3873 RVA: 0x000621D8 File Offset: 0x000603D8
	public float Param5Meter
	{
		get
		{
			return this.parm5x.value;
		}
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x000621E8 File Offset: 0x000603E8
	public bool IsHaveDamage()
	{
		return this.DamageMulti_100f != 0f || this.Damage != 0;
	}

	// Token: 0x04000E47 RID: 3655
	public string ID = string.Empty;

	// Token: 0x04000E48 RID: 3656
	public string Name = string.Empty;

	// Token: 0x04000E49 RID: 3657
	public string HitAction = string.Empty;

	// Token: 0x04000E4A RID: 3658
	public int Target;

	// Token: 0x04000E4B RID: 3659
	public int ForceMove;

	// Token: 0x04000E4C RID: 3660
	public int MoveTime;

	// Token: 0x04000E4D RID: 3661
	public int MoveAngle;

	// Token: 0x04000E4E RID: 3662
	public int MoveDistance;

	// Token: 0x04000E4F RID: 3663
	public string BuffID = string.Empty;

	// Token: 0x04000E50 RID: 3664
	public int BuffDuration;

	// Token: 0x04000E51 RID: 3665
	public int Damage;

	// Token: 0x04000E52 RID: 3666
	public int DamageMulti;

	// Token: 0x04000E53 RID: 3667
	public int PvPDamage;

	// Token: 0x04000E54 RID: 3668
	public int PvPDamageMulti = 1;

	// Token: 0x04000E55 RID: 3669
	public int AreaType;

	// Token: 0x04000E56 RID: 3670
	public int Param1;

	// Token: 0x04000E57 RID: 3671
	public int Param2;

	// Token: 0x04000E58 RID: 3672
	public int Param3;

	// Token: 0x04000E59 RID: 3673
	public int Param4;

	// Token: 0x04000E5A RID: 3674
	public int Param5;

	// Token: 0x04000E5B RID: 3675
	public int DamageAdd;

	// Token: 0x04000E5C RID: 3676
	public int DamageMultiAdd;

	// Token: 0x04000E5D RID: 3677
	public int AddType1;

	// Token: 0x04000E5E RID: 3678
	public int AddValue1;

	// Token: 0x04000E5F RID: 3679
	public int AddType2;

	// Token: 0x04000E60 RID: 3680
	public int AddValue2;

	// Token: 0x04000E61 RID: 3681
	public int AddType3;

	// Token: 0x04000E62 RID: 3682
	public int AddValue3;

	// Token: 0x04000E63 RID: 3683
	public int AddType4;

	// Token: 0x04000E64 RID: 3684
	public int AddValue4;

	// Token: 0x04000E65 RID: 3685
	public int BuffProb;

	// Token: 0x04000E66 RID: 3686
	private XorFloat dmx = new XorFloat();

	// Token: 0x04000E67 RID: 3687
	private XorFloat movxt = new XorFloat();

	// Token: 0x04000E68 RID: 3688
	private XorFloat movx = new XorFloat();

	// Token: 0x04000E69 RID: 3689
	private XorFloat bufx = new XorFloat();

	// Token: 0x04000E6A RID: 3690
	private XorFloat parm1x = new XorFloat();

	// Token: 0x04000E6B RID: 3691
	private XorFloat parm2x = new XorFloat();

	// Token: 0x04000E6C RID: 3692
	private XorFloat parm3x = new XorFloat();

	// Token: 0x04000E6D RID: 3693
	private XorFloat parm4x = new XorFloat();

	// Token: 0x04000E6E RID: 3694
	private XorFloat parm5x = new XorFloat();

	// Token: 0x04000E6F RID: 3695
	private XorInt damagex = new XorInt();
}
