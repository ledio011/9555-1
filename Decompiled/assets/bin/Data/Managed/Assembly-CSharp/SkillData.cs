using System;
using System.Collections.Generic;

// Token: 0x020001B0 RID: 432
[Serializable]
public class SkillData
{
	// Token: 0x1700036C RID: 876
	// (get) Token: 0x06001008 RID: 4104 RVA: 0x00065B50 File Offset: 0x00063D50
	public string MName
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Name, new object[0]);
		}
	}

	// Token: 0x1700036D RID: 877
	// (get) Token: 0x06001009 RID: 4105 RVA: 0x00065B64 File Offset: 0x00063D64
	public string MDescription
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Description, new object[0]);
		}
	}

	// Token: 0x1700036E RID: 878
	// (get) Token: 0x0600100A RID: 4106 RVA: 0x00065B78 File Offset: 0x00063D78
	public string[] CamRockIDList
	{
		get
		{
			if (this.mCamRockIDList == null && !string.IsNullOrEmpty(this.CamRockID))
			{
				this.mCamRockIDList = this.CamRockID.Split(new char[]
				{
					'#'
				});
			}
			return this.mCamRockIDList;
		}
	}

	// Token: 0x1700036F RID: 879
	// (get) Token: 0x0600100B RID: 4107 RVA: 0x00065BB8 File Offset: 0x00063DB8
	public float CastTimeSecond
	{
		get
		{
			return (float)this.CastTime / 1000f;
		}
	}

	// Token: 0x17000370 RID: 880
	// (get) Token: 0x0600100C RID: 4108 RVA: 0x00065BC8 File Offset: 0x00063DC8
	public float MoveTimeSecond
	{
		get
		{
			return this.movtx.value;
		}
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x00065BD8 File Offset: 0x00063DD8
	public void Init()
	{
		this.cdx.value = (float)this.CD / 1000f;
		this.hdx.value = (float)this.HoldTime / 1000f;
		this.Edx01.value = (float)this.EffTime_0 / 1000f;
		this.Edx02.value = (float)this.EffTime_1 / 1000f;
		this.Edx03.value = (float)this.EffTime_2 / 1000f;
		this.traceX.value = (float)this.TraceDistance / 100f;
		this.movx.value = (float)this.MoveDistance / 100f;
		this.autox.value = (float)this.AutoAttackDistance / 100f;
		this.movtx.value = (float)this.MoveTime / 1000f;
	}

	// Token: 0x17000371 RID: 881
	// (get) Token: 0x0600100E RID: 4110 RVA: 0x00065CC0 File Offset: 0x00063EC0
	public float CDSecond
	{
		get
		{
			return this.cdx.value;
		}
	}

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x0600100F RID: 4111 RVA: 0x00065CD0 File Offset: 0x00063ED0
	public float TraceDistanceMeter
	{
		get
		{
			return this.traceX.value;
		}
	}

	// Token: 0x17000373 RID: 883
	// (get) Token: 0x06001010 RID: 4112 RVA: 0x00065CE0 File Offset: 0x00063EE0
	public float AutoAttackDistanceMeter
	{
		get
		{
			return this.autox.value;
		}
	}

	// Token: 0x17000374 RID: 884
	// (get) Token: 0x06001011 RID: 4113 RVA: 0x00065CF0 File Offset: 0x00063EF0
	public float ComboValidTimeSecond
	{
		get
		{
			return (float)this.ComboValidTime / 1000f;
		}
	}

	// Token: 0x17000375 RID: 885
	// (get) Token: 0x06001012 RID: 4114 RVA: 0x00065D00 File Offset: 0x00063F00
	public float HoldTimeSecond
	{
		get
		{
			return this.hdx.value;
		}
	}

	// Token: 0x17000376 RID: 886
	// (get) Token: 0x06001013 RID: 4115 RVA: 0x00065D10 File Offset: 0x00063F10
	public float MoveDistanceMeter
	{
		get
		{
			return this.movx.value;
		}
	}

	// Token: 0x17000377 RID: 887
	// (get) Token: 0x06001014 RID: 4116 RVA: 0x00065D20 File Offset: 0x00063F20
	public float EffTime_0Second
	{
		get
		{
			return this.Edx01.value;
		}
	}

	// Token: 0x17000378 RID: 888
	// (get) Token: 0x06001015 RID: 4117 RVA: 0x00065D30 File Offset: 0x00063F30
	public float EffTime_1Second
	{
		get
		{
			return this.Edx02.value;
		}
	}

	// Token: 0x17000379 RID: 889
	// (get) Token: 0x06001016 RID: 4118 RVA: 0x00065D40 File Offset: 0x00063F40
	public float EffTime_2Second
	{
		get
		{
			return this.Edx03.value;
		}
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x00065D50 File Offset: 0x00063F50
	public bool IsFirstComboSkill()
	{
		return !string.IsNullOrEmpty(this.ComboStart) && this.ID.Equals(this.ComboStart);
	}

	// Token: 0x06001018 RID: 4120 RVA: 0x00065D78 File Offset: 0x00063F78
	public bool IsComboSkill()
	{
		return !string.IsNullOrEmpty(this.NextSkill);
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x00065D88 File Offset: 0x00063F88
	public bool IsComboLastSkill()
	{
		return !string.IsNullOrEmpty(this.ComboStart) && this.NextSkill.Equals(this.ComboStart);
	}

	// Token: 0x1700037A RID: 890
	// (get) Token: 0x0600101A RID: 4122 RVA: 0x00065DB0 File Offset: 0x00063FB0
	public List<string> LabelIdList
	{
		get
		{
			if (this.mLabelIdList == null)
			{
				this.mLabelIdList = new List<string>();
				if (!string.IsNullOrEmpty(this.LabelID))
				{
					string[] array = this.LabelID.Split(new char[]
					{
						'#'
					});
					for (int i = 0; i < array.Length; i++)
					{
						this.mLabelIdList.Add(array[i]);
					}
				}
			}
			return this.mLabelIdList;
		}
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x00065E24 File Offset: 0x00064024
	public int GetSkillDamageVal(int level)
	{
		if (level < 0)
		{
			level = 0;
		}
		if (this.IsUpgrade == 0)
		{
			level = 0;
		}
		int num = 0;
		if (!string.IsNullOrEmpty(this.EffId_0))
		{
			EffInfoData effInfoDataById = DataManager.GetEffInfoDataById(this.EffId_0);
			num += effInfoDataById.Damage + effInfoDataById.DamageAdd * level;
		}
		if (!string.IsNullOrEmpty(this.EffId_1))
		{
			EffInfoData effInfoDataById2 = DataManager.GetEffInfoDataById(this.EffId_1);
			num += effInfoDataById2.Damage + effInfoDataById2.DamageAdd * level;
		}
		if (!string.IsNullOrEmpty(this.EffId_1))
		{
			EffInfoData effInfoDataById3 = DataManager.GetEffInfoDataById(this.EffId_1);
			num += effInfoDataById3.Damage + effInfoDataById3.DamageAdd * level;
		}
		return num;
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x00065ED8 File Offset: 0x000640D8
	public float GetSkillDamageMultiVal(int level)
	{
		if (level < 0)
		{
			level = 0;
		}
		if (this.IsUpgrade == 0)
		{
			level = 0;
		}
		int num = 0;
		if (!string.IsNullOrEmpty(this.EffId_0))
		{
			EffInfoData effInfoDataById = DataManager.GetEffInfoDataById(this.EffId_0);
			num += effInfoDataById.DamageMulti + effInfoDataById.DamageMultiAdd * level;
		}
		if (!string.IsNullOrEmpty(this.EffId_1))
		{
			EffInfoData effInfoDataById2 = DataManager.GetEffInfoDataById(this.EffId_1);
			num += effInfoDataById2.DamageMulti + effInfoDataById2.DamageMultiAdd * level;
		}
		if (!string.IsNullOrEmpty(this.EffId_1))
		{
			EffInfoData effInfoDataById3 = DataManager.GetEffInfoDataById(this.EffId_1);
			num += effInfoDataById3.DamageMulti + effInfoDataById3.DamageMultiAdd * level;
		}
		return (float)num / 10000f;
	}

	// Token: 0x04001293 RID: 4755
	public string ID = string.Empty;

	// Token: 0x04001294 RID: 4756
	public string Name = string.Empty;

	// Token: 0x04001295 RID: 4757
	public string Icon = string.Empty;

	// Token: 0x04001296 RID: 4758
	public string Description = string.Empty;

	// Token: 0x04001297 RID: 4759
	public string ActionName = string.Empty;

	// Token: 0x04001298 RID: 4760
	public int CastTime;

	// Token: 0x04001299 RID: 4761
	public string CastAction = string.Empty;

	// Token: 0x0400129A RID: 4762
	public int CD;

	// Token: 0x0400129B RID: 4763
	public int TraceDistance;

	// Token: 0x0400129C RID: 4764
	public int ComboValidTime;

	// Token: 0x0400129D RID: 4765
	public string ComboStart = string.Empty;

	// Token: 0x0400129E RID: 4766
	public string NextSkill = string.Empty;

	// Token: 0x0400129F RID: 4767
	public int HoldTime;

	// Token: 0x040012A0 RID: 4768
	public int AutoMoveFlag;

	// Token: 0x040012A1 RID: 4769
	public int MoveTime;

	// Token: 0x040012A2 RID: 4770
	public int MoveDistance;

	// Token: 0x040012A3 RID: 4771
	public int MoveAngle;

	// Token: 0x040012A4 RID: 4772
	public string EffId_0 = string.Empty;

	// Token: 0x040012A5 RID: 4773
	public int EffTime_0;

	// Token: 0x040012A6 RID: 4774
	public string EffId_1 = string.Empty;

	// Token: 0x040012A7 RID: 4775
	public int EffTime_1;

	// Token: 0x040012A8 RID: 4776
	public string EffId_2 = string.Empty;

	// Token: 0x040012A9 RID: 4777
	public int EffTime_2;

	// Token: 0x040012AA RID: 4778
	public int CanBeBreak;

	// Token: 0x040012AB RID: 4779
	public int MaxAttackCount = 1;

	// Token: 0x040012AC RID: 4780
	public int PriorityAutoCombat;

	// Token: 0x040012AD RID: 4781
	public int AutoAttackDistance;

	// Token: 0x040012AE RID: 4782
	public string Template = string.Empty;

	// Token: 0x040012AF RID: 4783
	public int Locklevel = 1;

	// Token: 0x040012B0 RID: 4784
	public int IsUpgrade;

	// Token: 0x040012B1 RID: 4785
	public int fvalue;

	// Token: 0x040012B2 RID: 4786
	public int fvalueAdd;

	// Token: 0x040012B3 RID: 4787
	public int Job;

	// Token: 0x040012B4 RID: 4788
	public int TeamID;

	// Token: 0x040012B5 RID: 4789
	public string CamRockID = string.Empty;

	// Token: 0x040012B6 RID: 4790
	private string[] mCamRockIDList;

	// Token: 0x040012B7 RID: 4791
	private XorFloat cdx = new XorFloat();

	// Token: 0x040012B8 RID: 4792
	private XorFloat hdx = new XorFloat();

	// Token: 0x040012B9 RID: 4793
	private XorFloat Edx01 = new XorFloat();

	// Token: 0x040012BA RID: 4794
	private XorFloat Edx02 = new XorFloat();

	// Token: 0x040012BB RID: 4795
	private XorFloat Edx03 = new XorFloat();

	// Token: 0x040012BC RID: 4796
	private XorFloat traceX = new XorFloat();

	// Token: 0x040012BD RID: 4797
	private XorFloat movx = new XorFloat();

	// Token: 0x040012BE RID: 4798
	private XorFloat autox = new XorFloat();

	// Token: 0x040012BF RID: 4799
	private XorFloat movtx = new XorFloat();

	// Token: 0x040012C0 RID: 4800
	public string LabelID;

	// Token: 0x040012C1 RID: 4801
	private List<string> mLabelIdList;
}
