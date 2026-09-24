using System;

// Token: 0x0200014F RID: 335
public class BigPackageTimeListData
{
	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00061410 File Offset: 0x0005F610
	public int[] EndTimes
	{
		get
		{
			if ((this.mEndTimes == null || this.mEndTimes.Length == 0) && !string.IsNullOrEmpty(this.EndTime))
			{
				string[] array = this.EndTime.Split(new char[]
				{
					'-'
				});
				this.mEndTimes = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mEndTimes[i] = int.Parse(array[i]);
				}
			}
			return this.mEndTimes;
		}
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00061494 File Offset: 0x0005F694
	public int[] Starttimes
	{
		get
		{
			if ((this.mStarttimes == null || this.mStarttimes.Length == 0) && !string.IsNullOrEmpty(this.StartTime))
			{
				string[] array = this.StartTime.Split(new char[]
				{
					'-'
				});
				this.mStarttimes = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mStarttimes[i] = int.Parse(array[i]);
				}
			}
			return this.mStarttimes;
		}
	}

	// Token: 0x04000D0B RID: 3339
	public string ID;

	// Token: 0x04000D0C RID: 3340
	public string StartTime;

	// Token: 0x04000D0D RID: 3341
	public string EndTime;

	// Token: 0x04000D0E RID: 3342
	private int[] mEndTimes;

	// Token: 0x04000D0F RID: 3343
	private int[] mStarttimes;
}
