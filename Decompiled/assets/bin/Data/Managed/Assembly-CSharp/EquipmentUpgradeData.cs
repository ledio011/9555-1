using System;

// Token: 0x0200016E RID: 366
public class EquipmentUpgradeData
{
	// Token: 0x170002EF RID: 751
	// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00062A68 File Offset: 0x00060C68
	public float EquipeWhiteK
	{
		get
		{
			return (float)this.ParameterK1 / 100f;
		}
	}

	// Token: 0x170002F0 RID: 752
	// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00062A78 File Offset: 0x00060C78
	public float EquipeGreenK
	{
		get
		{
			return (float)this.ParameterK2 / 100f;
		}
	}

	// Token: 0x170002F1 RID: 753
	// (get) Token: 0x06000F3B RID: 3899 RVA: 0x00062A88 File Offset: 0x00060C88
	public float EquipeBlueK
	{
		get
		{
			return (float)this.ParameterK3 / 100f;
		}
	}

	// Token: 0x170002F2 RID: 754
	// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00062A98 File Offset: 0x00060C98
	public float EquipePurpleK
	{
		get
		{
			return (float)this.ParameterK4 / 100f;
		}
	}

	// Token: 0x170002F3 RID: 755
	// (get) Token: 0x06000F3D RID: 3901 RVA: 0x00062AA8 File Offset: 0x00060CA8
	public float MoneyWhitK
	{
		get
		{
			return (float)this.MoneyParameterK1 / 100f;
		}
	}

	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00062AB8 File Offset: 0x00060CB8
	public float MoneyWhitB
	{
		get
		{
			return (float)this.MoneyParameterB1;
		}
	}

	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00062AC4 File Offset: 0x00060CC4
	public float MoneyGreenK
	{
		get
		{
			return (float)this.MoneyParameterK2 / 100f;
		}
	}

	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00062AD4 File Offset: 0x00060CD4
	public float MoneyGreenB
	{
		get
		{
			return (float)this.MoneyParameterB2;
		}
	}

	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00062AE0 File Offset: 0x00060CE0
	public float MoneyBlueK
	{
		get
		{
			return (float)this.MoneyParameterK3 / 100f;
		}
	}

	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x06000F42 RID: 3906 RVA: 0x00062AF0 File Offset: 0x00060CF0
	public float MoneyBlueB
	{
		get
		{
			return (float)this.MoneyParameterB3;
		}
	}

	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x06000F43 RID: 3907 RVA: 0x00062AFC File Offset: 0x00060CFC
	public float MoneyPurpleK
	{
		get
		{
			return (float)this.MoneyParameterK4 / 100f;
		}
	}

	// Token: 0x170002FA RID: 762
	// (get) Token: 0x06000F44 RID: 3908 RVA: 0x00062B0C File Offset: 0x00060D0C
	public float MoneyPurpleB
	{
		get
		{
			return (float)this.MoneyParameterB4;
		}
	}

	// Token: 0x170002FB RID: 763
	// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00062B18 File Offset: 0x00060D18
	public float LevelK
	{
		get
		{
			return (float)this.LevelupParameterK / 100f;
		}
	}

	// Token: 0x170002FC RID: 764
	// (get) Token: 0x06000F46 RID: 3910 RVA: 0x00062B28 File Offset: 0x00060D28
	public float LevelB
	{
		get
		{
			return (float)this.LevelupParameterB;
		}
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x00062B34 File Offset: 0x00060D34
	public float GetEquipKByQuality(int quality, int job)
	{
		if (job == 0 || !GameManager.IsSupportCurDataVersion145())
		{
			switch (quality)
			{
			case 0:
				return this.EquipeWhiteK;
			case 1:
				return this.EquipeGreenK;
			case 2:
				return this.EquipeBlueK;
			case 3:
				return this.EquipePurpleK;
			default:
				return this.EquipeWhiteK;
			}
		}
		else if (job == 1)
		{
			switch (quality)
			{
			case 0:
				return (float)this.QJParameterK1 / 100f;
			case 1:
				return (float)this.QJParameterK2 / 100f;
			case 2:
				return (float)this.QJParameterK3 / 100f;
			case 3:
				return (float)this.QJParameterK4 / 100f;
			default:
				return (float)this.QJParameterK1 / 100f;
			}
		}
		else
		{
			switch (quality)
			{
			case 0:
				return (float)this.NQParameterK1 / 100f;
			case 1:
				return (float)this.NQParameterK2 / 100f;
			case 2:
				return (float)this.NQParameterK3 / 100f;
			case 3:
				return (float)this.NQParameterK4 / 100f;
			default:
				return (float)this.NQParameterK1 / 100f;
			}
		}
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x00062C60 File Offset: 0x00060E60
	public float GetMoneyKByQuality(int quality)
	{
		return this.MoneyWhitK;
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x00062C68 File Offset: 0x00060E68
	public float GetMoneyBByQuality(int quality)
	{
		return this.MoneyWhitB;
	}

	// Token: 0x04000E99 RID: 3737
	public int PartID = -1;

	// Token: 0x04000E9A RID: 3738
	public int Level;

	// Token: 0x04000E9B RID: 3739
	public int ParameterK1 = 1;

	// Token: 0x04000E9C RID: 3740
	public int ParameterK2 = 1;

	// Token: 0x04000E9D RID: 3741
	public int ParameterK3 = 1;

	// Token: 0x04000E9E RID: 3742
	public int ParameterK4 = 1;

	// Token: 0x04000E9F RID: 3743
	public int QJParameterK1 = 1;

	// Token: 0x04000EA0 RID: 3744
	public int QJParameterK2 = 1;

	// Token: 0x04000EA1 RID: 3745
	public int QJParameterK3 = 1;

	// Token: 0x04000EA2 RID: 3746
	public int QJParameterK4 = 1;

	// Token: 0x04000EA3 RID: 3747
	public int NQParameterK1 = 1;

	// Token: 0x04000EA4 RID: 3748
	public int NQParameterK2 = 1;

	// Token: 0x04000EA5 RID: 3749
	public int NQParameterK3 = 1;

	// Token: 0x04000EA6 RID: 3750
	public int NQParameterK4 = 1;

	// Token: 0x04000EA7 RID: 3751
	public int WeaponParameterK = 1;

	// Token: 0x04000EA8 RID: 3752
	public int MoneyParameterK1;

	// Token: 0x04000EA9 RID: 3753
	public int MoneyParameterB1 = 1;

	// Token: 0x04000EAA RID: 3754
	public int MoneyParameterK2;

	// Token: 0x04000EAB RID: 3755
	public int MoneyParameterB2 = 1;

	// Token: 0x04000EAC RID: 3756
	public int MoneyParameterK3;

	// Token: 0x04000EAD RID: 3757
	public int MoneyParameterB3 = 1;

	// Token: 0x04000EAE RID: 3758
	public int MoneyParameterK4;

	// Token: 0x04000EAF RID: 3759
	public int MoneyParameterB4 = 1;

	// Token: 0x04000EB0 RID: 3760
	public int LevelupParameterK = 1;

	// Token: 0x04000EB1 RID: 3761
	public int LevelupParameterB;
}
