using System;

// Token: 0x020001C2 RID: 450
public class TimerActivityTipsData
{
	// Token: 0x1700038B RID: 907
	// (get) Token: 0x06001043 RID: 4163 RVA: 0x0006671C File Offset: 0x0006491C
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

	// Token: 0x1700038C RID: 908
	// (get) Token: 0x06001044 RID: 4164 RVA: 0x000667A0 File Offset: 0x000649A0
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

	// Token: 0x04001367 RID: 4967
	public string ID = string.Empty;

	// Token: 0x04001368 RID: 4968
	public string EventName = string.Empty;

	// Token: 0x04001369 RID: 4969
	public string StartTime = string.Empty;

	// Token: 0x0400136A RID: 4970
	public string EndTime = string.Empty;

	// Token: 0x0400136B RID: 4971
	public string PicName = string.Empty;

	// Token: 0x0400136C RID: 4972
	public int Weight = int.MaxValue;

	// Token: 0x0400136D RID: 4973
	public int JumpType = -1;

	// Token: 0x0400136E RID: 4974
	public string JumpLabel = string.Empty;

	// Token: 0x0400136F RID: 4975
	private int[] mEndTimes;

	// Token: 0x04001370 RID: 4976
	private int[] mStarttimes;
}
