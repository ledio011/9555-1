using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000198 RID: 408
public class NpcData
{
	// Token: 0x17000343 RID: 835
	// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00064984 File Offset: 0x00062B84
	public int[] ShiftHP
	{
		get
		{
			if (this.mShiftHP == null && !string.IsNullOrEmpty(this.TabShiftHP))
			{
				string[] array = this.TabShiftHP.Split(new char[]
				{
					'#'
				});
				this.mShiftHP = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					if (!int.TryParse(array[i], ref this.mShiftHP[i]))
					{
						Debug.Log("TabShiftHP Input Wrong!!!!!!!!!!");
					}
				}
			}
			return this.mShiftHP;
		}
	}

	// Token: 0x17000344 RID: 836
	// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00064A10 File Offset: 0x00062C10
	public string[] ShiftSkill
	{
		get
		{
			if (this.mShiftSkill == null && !string.IsNullOrEmpty(this.TabShiftSkill))
			{
				string[] array = this.TabShiftSkill.Split(new char[]
				{
					'#'
				});
				this.mShiftSkill = array;
			}
			return this.mShiftSkill;
		}
	}

	// Token: 0x17000345 RID: 837
	// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00064A5C File Offset: 0x00062C5C
	public string MName
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Name, new object[0]);
		}
	}

	// Token: 0x17000346 RID: 838
	// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00064A70 File Offset: 0x00062C70
	public List<string[]> ShiftSkillGroup
	{
		get
		{
			if (this.mShiftSkillGroup == null && !string.IsNullOrEmpty(this.TabShiftSkillGroup))
			{
				this.mShiftSkillGroup = new List<string[]>();
				string[] array = this.TabShiftSkillGroup.Split(new char[]
				{
					'#'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						';'
					});
					this.mShiftSkillGroup.Add(array2);
				}
			}
			return this.mShiftSkillGroup;
		}
	}

	// Token: 0x17000347 RID: 839
	// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00064AF4 File Offset: 0x00062CF4
	public int ShiftStateCount
	{
		get
		{
			if (this.mShiftHP != null)
			{
				return this.mShiftHP.Length;
			}
			return 0;
		}
	}

	// Token: 0x17000348 RID: 840
	// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00064B0C File Offset: 0x00062D0C
	public float WalkSpeedMeter
	{
		get
		{
			return (float)this.WalkSpeed / 10f;
		}
	}

	// Token: 0x17000349 RID: 841
	// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00064B1C File Offset: 0x00062D1C
	public float MoveSpeedMeter
	{
		get
		{
			return (float)this.MoveSpeed / 10f;
		}
	}

	// Token: 0x1700034A RID: 842
	// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00064B2C File Offset: 0x00062D2C
	public float ModelScale
	{
		get
		{
			return (float)this.Size / 100f;
		}
	}

	// Token: 0x1700034B RID: 843
	// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00064B3C File Offset: 0x00062D3C
	public string[] SkillList
	{
		get
		{
			if (this.SkillGroupID == null && !string.IsNullOrEmpty(this.SkillGroup))
			{
				string[] skillGroupID = this.SkillGroup.Split(new char[]
				{
					';'
				});
				this.SkillGroupID = skillGroupID;
			}
			return this.SkillGroupID;
		}
	}

	// Token: 0x1700034C RID: 844
	// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00064B88 File Offset: 0x00062D88
	public float PatrolRadius
	{
		get
		{
			return (float)this.Patrol / 100f;
		}
	}

	// Token: 0x1700034D RID: 845
	// (get) Token: 0x06000FCE RID: 4046 RVA: 0x00064B98 File Offset: 0x00062D98
	public float SearchRadius
	{
		get
		{
			return (float)this.Search / 100f;
		}
	}

	// Token: 0x0400114A RID: 4426
	public string ID;

	// Token: 0x0400114B RID: 4427
	public string Name;

	// Token: 0x0400114C RID: 4428
	[ServerExclude("ServerNoUse")]
	public int IsPlayerModel;

	// Token: 0x0400114D RID: 4429
	public string Model;

	// Token: 0x0400114E RID: 4430
	public int Size = 100;

	// Token: 0x0400114F RID: 4431
	public int Lv;

	// Token: 0x04001150 RID: 4432
	public int Group = 1;

	// Token: 0x04001151 RID: 4433
	public string TalkGroup = string.Empty;

	// Token: 0x04001152 RID: 4434
	public int FunctionType;

	// Token: 0x04001153 RID: 4435
	public int Type;

	// Token: 0x04001154 RID: 4436
	public long Hp;

	// Token: 0x04001155 RID: 4437
	public int Atk;

	// Token: 0x04001156 RID: 4438
	public int Def;

	// Token: 0x04001157 RID: 4439
	public int HIT;

	// Token: 0x04001158 RID: 4440
	public int DGE;

	// Token: 0x04001159 RID: 4441
	public int CRI;

	// Token: 0x0400115A RID: 4442
	public int RES;

	// Token: 0x0400115B RID: 4443
	public int EXD;

	// Token: 0x0400115C RID: 4444
	public int EXR;

	// Token: 0x0400115D RID: 4445
	public int CRD;

	// Token: 0x0400115E RID: 4446
	public int CRR;

	// Token: 0x0400115F RID: 4447
	public int DEFA;

	// Token: 0x04001160 RID: 4448
	public int DGEA;

	// Token: 0x04001161 RID: 4449
	public int RESA;

	// Token: 0x04001162 RID: 4450
	public int HITA;

	// Token: 0x04001163 RID: 4451
	public int CRIA;

	// Token: 0x04001164 RID: 4452
	public int AntiStun;

	// Token: 0x04001165 RID: 4453
	public int AntiKnockDown;

	// Token: 0x04001166 RID: 4454
	public int HpCoe;

	// Token: 0x04001167 RID: 4455
	public int AtkCoe;

	// Token: 0x04001168 RID: 4456
	public int DefCoe;

	// Token: 0x04001169 RID: 4457
	public int HITCoe;

	// Token: 0x0400116A RID: 4458
	public int DGECoe;

	// Token: 0x0400116B RID: 4459
	public int CRICoe;

	// Token: 0x0400116C RID: 4460
	public int RESCoe;

	// Token: 0x0400116D RID: 4461
	public int EXDCoe;

	// Token: 0x0400116E RID: 4462
	public int EXRCoe;

	// Token: 0x0400116F RID: 4463
	public int CRDCoe;

	// Token: 0x04001170 RID: 4464
	public int CRRCoe;

	// Token: 0x04001171 RID: 4465
	public int DEFACoe;

	// Token: 0x04001172 RID: 4466
	public int DGEACoe;

	// Token: 0x04001173 RID: 4467
	public int RESACoe;

	// Token: 0x04001174 RID: 4468
	public int HITACoe;

	// Token: 0x04001175 RID: 4469
	public int CRIACoe;

	// Token: 0x04001176 RID: 4470
	public int AntiStunCoe;

	// Token: 0x04001177 RID: 4471
	public int AntiKnockDownCoe;

	// Token: 0x04001178 RID: 4472
	public string SkillGroup;

	// Token: 0x04001179 RID: 4473
	public int Buff;

	// Token: 0x0400117A RID: 4474
	public int Patrol;

	// Token: 0x0400117B RID: 4475
	public int Search;

	// Token: 0x0400117C RID: 4476
	public string AI = string.Empty;

	// Token: 0x0400117D RID: 4477
	public string DropID = string.Empty;

	// Token: 0x0400117E RID: 4478
	public string AIID = "1001";

	// Token: 0x0400117F RID: 4479
	public int WalkSpeed = 10;

	// Token: 0x04001180 RID: 4480
	public int MoveSpeed = 50;

	// Token: 0x04001181 RID: 4481
	public string TabShiftHP;

	// Token: 0x04001182 RID: 4482
	public string TabShiftSkill;

	// Token: 0x04001183 RID: 4483
	public string TabShiftSkillGroup;

	// Token: 0x04001184 RID: 4484
	private int[] mShiftHP;

	// Token: 0x04001185 RID: 4485
	private string[] mShiftSkill;

	// Token: 0x04001186 RID: 4486
	private List<string[]> mShiftSkillGroup;

	// Token: 0x04001187 RID: 4487
	private string[] SkillGroupID;
}
