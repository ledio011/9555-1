using System;
using System.Collections.Generic;

// Token: 0x0200019D RID: 413
public class PoliceLevelData
{
	// Token: 0x17000354 RID: 852
	// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00064CF8 File Offset: 0x00062EF8
	public List<int> PoliceScoresList
	{
		get
		{
			if (this.policeScores == null)
			{
				this.policeScores = new List<int>();
				this.policeScores.Add(this.Star1Scores);
				this.policeScores.Add(this.Star2Scores);
				this.policeScores.Add(this.Star3Scores);
			}
			return this.policeScores;
		}
	}

	// Token: 0x17000355 RID: 853
	// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00064D54 File Offset: 0x00062F54
	public List<float> PoliceFlashTimeList
	{
		get
		{
			if (this.policeFlashTime == null)
			{
				this.policeFlashTime = new List<float>();
				this.policeFlashTime.Add((float)this.Star1FlashTime / 1000f);
				this.policeFlashTime.Add((float)this.Star2FlashTime / 1000f);
				this.policeFlashTime.Add((float)this.Star3FlashTime / 1000f);
			}
			return this.policeFlashTime;
		}
	}

	// Token: 0x17000356 RID: 854
	// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00064DC8 File Offset: 0x00062FC8
	public List<int> PoliceNumList
	{
		get
		{
			if (this.policeNum == null)
			{
				this.policeNum = new List<int>();
				this.policeNum.Add(this.Star1Num);
				this.policeNum.Add(this.Star2Num);
				this.policeNum.Add(this.Star3Num);
			}
			return this.policeNum;
		}
	}

	// Token: 0x17000357 RID: 855
	// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00064E24 File Offset: 0x00063024
	public List<string>[] PoliceIdList
	{
		get
		{
			if (this.policeIdList == null)
			{
				string[] array = this.Star1PoliceId.Split(new char[]
				{
					'|'
				});
				string[] array2 = this.Star2PoliceId.Split(new char[]
				{
					'|'
				});
				string[] array3 = this.Star3PoliceId.Split(new char[]
				{
					'|'
				});
				List<string> list = new List<string>();
				for (int i = 0; i < array.Length; i++)
				{
					list.Add(array[i]);
				}
				List<string> list2 = new List<string>();
				for (int j = 0; j < array2.Length; j++)
				{
					list2.Add(array2[j]);
				}
				List<string> list3 = new List<string>();
				for (int k = 0; k < array3.Length; k++)
				{
					list3.Add(array3[k]);
				}
				this.policeIdList = new List<string>[]
				{
					list,
					list2,
					list3
				};
			}
			return this.policeIdList;
		}
	}

	// Token: 0x040011A2 RID: 4514
	public string ID = string.Empty;

	// Token: 0x040011A3 RID: 4515
	public int Star1Scores;

	// Token: 0x040011A4 RID: 4516
	public int Star2Scores;

	// Token: 0x040011A5 RID: 4517
	public int Star3Scores;

	// Token: 0x040011A6 RID: 4518
	public int MaxScores;

	// Token: 0x040011A7 RID: 4519
	public int Star1FlashTime;

	// Token: 0x040011A8 RID: 4520
	public int Star2FlashTime;

	// Token: 0x040011A9 RID: 4521
	public int Star3FlashTime;

	// Token: 0x040011AA RID: 4522
	public int Star1Num;

	// Token: 0x040011AB RID: 4523
	public int Star2Num;

	// Token: 0x040011AC RID: 4524
	public int Star3Num;

	// Token: 0x040011AD RID: 4525
	public string Star1PoliceId = string.Empty;

	// Token: 0x040011AE RID: 4526
	public string Star2PoliceId = string.Empty;

	// Token: 0x040011AF RID: 4527
	public string Star3PoliceId = string.Empty;

	// Token: 0x040011B0 RID: 4528
	private List<int> policeScores;

	// Token: 0x040011B1 RID: 4529
	private List<float> policeFlashTime;

	// Token: 0x040011B2 RID: 4530
	private List<int> policeNum;

	// Token: 0x040011B3 RID: 4531
	private List<string>[] policeIdList;
}
