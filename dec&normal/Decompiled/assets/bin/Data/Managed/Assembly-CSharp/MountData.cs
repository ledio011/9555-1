using System;
using UnityEngine;

// Token: 0x02000191 RID: 401
public class MountData
{
	// Token: 0x17000330 RID: 816
	// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x000644D8 File Offset: 0x000626D8
	public string MCarName
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.CarName, new object[0]);
		}
	}

	// Token: 0x17000331 RID: 817
	// (get) Token: 0x06000FAA RID: 4010 RVA: 0x000644EC File Offset: 0x000626EC
	public float ATKValue
	{
		get
		{
			return (float)this.ATK / 10000f;
		}
	}

	// Token: 0x17000332 RID: 818
	// (get) Token: 0x06000FAB RID: 4011 RVA: 0x000644FC File Offset: 0x000626FC
	public bool IsMotorBool
	{
		get
		{
			return this.IsMotor == 1;
		}
	}

	// Token: 0x17000333 RID: 819
	// (get) Token: 0x06000FAC RID: 4012 RVA: 0x00064508 File Offset: 0x00062708
	public float CamDisMeter
	{
		get
		{
			return (float)this.CamDis / 100f;
		}
	}

	// Token: 0x17000334 RID: 820
	// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00064518 File Offset: 0x00062718
	public float CamHeightMeter
	{
		get
		{
			return (float)this.CamHeight / 100f;
		}
	}

	// Token: 0x17000335 RID: 821
	// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00064528 File Offset: 0x00062728
	public int[] StartTimeList
	{
		get
		{
			if ((this.mStartTimeList == null || this.mStartTimeList.Length == 0) && !string.IsNullOrEmpty(this.StartTime))
			{
				string[] array = this.StartTime.Split(new char[]
				{
					'-'
				});
				this.mStartTimeList = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mStartTimeList[i] = int.Parse(array[i]);
				}
			}
			return this.mStartTimeList;
		}
	}

	// Token: 0x17000336 RID: 822
	// (get) Token: 0x06000FAF RID: 4015 RVA: 0x000645AC File Offset: 0x000627AC
	public int[] EndTimeList
	{
		get
		{
			if ((this.mEndTimeList == null || this.mEndTimeList.Length == 0) && !string.IsNullOrEmpty(this.EndTime))
			{
				string[] array = this.EndTime.Split(new char[]
				{
					'-'
				});
				this.mEndTimeList = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mEndTimeList[i] = int.Parse(array[i]);
				}
			}
			return this.mEndTimeList;
		}
	}

	// Token: 0x17000337 RID: 823
	// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00064630 File Offset: 0x00062830
	public string[] ColorIDs
	{
		get
		{
			if (string.IsNullOrEmpty(this.ColorStr))
			{
				return null;
			}
			return this.ColorStr.Split(new char[]
			{
				'#'
			});
		}
	}

	// Token: 0x17000338 RID: 824
	// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00064668 File Offset: 0x00062868
	public float MaxSp
	{
		get
		{
			return (float)this.MaxSpeed / 100f;
		}
	}

	// Token: 0x17000339 RID: 825
	// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00064678 File Offset: 0x00062878
	public float MaxAcce
	{
		get
		{
			return (float)this.MaxAcceleration / 100f;
		}
	}

	// Token: 0x1700033A RID: 826
	// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00064688 File Offset: 0x00062888
	public float BrakeAcce
	{
		get
		{
			return (float)this.BrakeAcceleration / 100f;
		}
	}

	// Token: 0x1700033B RID: 827
	// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x00064698 File Offset: 0x00062898
	public Vector3 ModelPos
	{
		get
		{
			return new Vector3(this.ModelPosX, this.ModelPosY, this.ModelPosZ);
		}
	}

	// Token: 0x1700033C RID: 828
	// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x000646B4 File Offset: 0x000628B4
	public Vector3 LightPosR
	{
		get
		{
			return new Vector3(this.LightPosX, this.LightPosY, this.LightPosZ);
		}
	}

	// Token: 0x1700033D RID: 829
	// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x000646D0 File Offset: 0x000628D0
	public Vector3 LightPosL
	{
		get
		{
			return new Vector3(-this.LightPosX, this.LightPosY, this.LightPosZ);
		}
	}

	// Token: 0x040010FE RID: 4350
	public string ID;

	// Token: 0x040010FF RID: 4351
	public string CarName;

	// Token: 0x04001100 RID: 4352
	public string ItemID;

	// Token: 0x04001101 RID: 4353
	public string Desc;

	// Token: 0x04001102 RID: 4354
	public int Quality;

	// Token: 0x04001103 RID: 4355
	public int Lv;

	// Token: 0x04001104 RID: 4356
	public int Status1;

	// Token: 0x04001105 RID: 4357
	public int Value1;

	// Token: 0x04001106 RID: 4358
	public int Status2;

	// Token: 0x04001107 RID: 4359
	public int Value2;

	// Token: 0x04001108 RID: 4360
	public int Status3;

	// Token: 0x04001109 RID: 4361
	public int Value3;

	// Token: 0x0400110A RID: 4362
	public int Status4;

	// Token: 0x0400110B RID: 4363
	public int Value4;

	// Token: 0x0400110C RID: 4364
	public string ModelId;

	// Token: 0x0400110D RID: 4365
	public int GetPath;

	// Token: 0x0400110E RID: 4366
	public string GetDesc;

	// Token: 0x0400110F RID: 4367
	public string CarIcon;

	// Token: 0x04001110 RID: 4368
	public float ModelPosX;

	// Token: 0x04001111 RID: 4369
	public float ModelPosY;

	// Token: 0x04001112 RID: 4370
	public float ModelPosZ;

	// Token: 0x04001113 RID: 4371
	public int MaxSpeed = 4000;

	// Token: 0x04001114 RID: 4372
	public float MaxSteerAngle = 10f;

	// Token: 0x04001115 RID: 4373
	public int MaxAcceleration = 2000;

	// Token: 0x04001116 RID: 4374
	public int BrakeAcceleration = 2000;

	// Token: 0x04001117 RID: 4375
	public float LightPosX;

	// Token: 0x04001118 RID: 4376
	public float LightPosY;

	// Token: 0x04001119 RID: 4377
	public float LightPosZ;

	// Token: 0x0400111A RID: 4378
	public string ColorStr;

	// Token: 0x0400111B RID: 4379
	public string DefaultColorId;

	// Token: 0x0400111C RID: 4380
	public float NameHeight = 2f;

	// Token: 0x0400111D RID: 4381
	public float ShadowHeight = 0.1f;

	// Token: 0x0400111E RID: 4382
	public string StartTime = string.Empty;

	// Token: 0x0400111F RID: 4383
	public string EndTime = string.Empty;

	// Token: 0x04001120 RID: 4384
	public int NeedShow;

	// Token: 0x04001121 RID: 4385
	public int MaxHP;

	// Token: 0x04001122 RID: 4386
	public int IsShowPlayer;

	// Token: 0x04001123 RID: 4387
	public string GTALinkCarId = string.Empty;

	// Token: 0x04001124 RID: 4388
	public int CamDis = 650;

	// Token: 0x04001125 RID: 4389
	public int CamHeight = 200;

	// Token: 0x04001126 RID: 4390
	public int PriceType = -1;

	// Token: 0x04001127 RID: 4391
	public int Price;

	// Token: 0x04001128 RID: 4392
	public int ATK;

	// Token: 0x04001129 RID: 4393
	public int IsMotor;

	// Token: 0x0400112A RID: 4394
	private int[] mStartTimeList;

	// Token: 0x0400112B RID: 4395
	private int[] mEndTimeList;
}
