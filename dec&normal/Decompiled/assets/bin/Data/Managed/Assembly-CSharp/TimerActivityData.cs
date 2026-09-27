using System;

// Token: 0x020001C1 RID: 449
public class TimerActivityData
{
	// Token: 0x17000389 RID: 905
	// (get) Token: 0x06001040 RID: 4160 RVA: 0x000665AC File Offset: 0x000647AC
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

	// Token: 0x1700038A RID: 906
	// (get) Token: 0x06001041 RID: 4161 RVA: 0x00066630 File Offset: 0x00064830
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

	// Token: 0x0400135F RID: 4959
	public string ID;

	// Token: 0x04001360 RID: 4960
	public string EventName;

	// Token: 0x04001361 RID: 4961
	public string EventDialog;

	// Token: 0x04001362 RID: 4962
	public int EventType;

	// Token: 0x04001363 RID: 4963
	public string StartTime;

	// Token: 0x04001364 RID: 4964
	public string EndTime;

	// Token: 0x04001365 RID: 4965
	private int[] mStartTimeList;

	// Token: 0x04001366 RID: 4966
	private int[] mEndTimeList;
}
