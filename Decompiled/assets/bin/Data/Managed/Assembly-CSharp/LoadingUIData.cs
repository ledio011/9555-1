using System;

// Token: 0x02000182 RID: 386
public class LoadingUIData
{
	// Token: 0x17000313 RID: 787
	// (get) Token: 0x06000F7D RID: 3965 RVA: 0x00063584 File Offset: 0x00061784
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

	// Token: 0x17000314 RID: 788
	// (get) Token: 0x06000F7E RID: 3966 RVA: 0x00063608 File Offset: 0x00061808
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

	// Token: 0x04000FE3 RID: 4067
	public string ID;

	// Token: 0x04000FE4 RID: 4068
	public string Name;

	// Token: 0x04000FE5 RID: 4069
	public int ShowFlag;

	// Token: 0x04000FE6 RID: 4070
	public int MinLevel;

	// Token: 0x04000FE7 RID: 4071
	public int MaxLevel;

	// Token: 0x04000FE8 RID: 4072
	public string StartTime = string.Empty;

	// Token: 0x04000FE9 RID: 4073
	public string EndTime = string.Empty;

	// Token: 0x04000FEA RID: 4074
	private int[] mStartTimeList;

	// Token: 0x04000FEB RID: 4075
	private int[] mEndTimeList;
}
