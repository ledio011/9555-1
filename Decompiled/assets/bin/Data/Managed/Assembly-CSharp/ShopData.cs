using System;

// Token: 0x020001AA RID: 426
public class ShopData
{
	// Token: 0x17000364 RID: 868
	// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00065500 File Offset: 0x00063700
	public int[] StartTimes
	{
		get
		{
			if ((this.mStartTimes == null || this.mStartTimes.Length == 0) && !string.IsNullOrEmpty(this.StartTime))
			{
				string[] array = this.StartTime.Split(new char[]
				{
					'-'
				});
				this.mStartTimes = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mStartTimes[i] = int.Parse(array[i]);
				}
			}
			return this.mStartTimes;
		}
	}

	// Token: 0x17000365 RID: 869
	// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x00065584 File Offset: 0x00063784
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

	// Token: 0x04001231 RID: 4657
	public string ID = string.Empty;

	// Token: 0x04001232 RID: 4658
	public int Shop;

	// Token: 0x04001233 RID: 4659
	public string ItemID = string.Empty;

	// Token: 0x04001234 RID: 4660
	public string Name = string.Empty;

	// Token: 0x04001235 RID: 4661
	public int ProfessionType = -1;

	// Token: 0x04001236 RID: 4662
	public int ItemType;

	// Token: 0x04001237 RID: 4663
	public int Quality;

	// Token: 0x04001238 RID: 4664
	public int PriceType;

	// Token: 0x04001239 RID: 4665
	public int Price;

	// Token: 0x0400123A RID: 4666
	public int Limit;

	// Token: 0x0400123B RID: 4667
	public int SingleLimit = 30;

	// Token: 0x0400123C RID: 4668
	public int Discount = 100;

	// Token: 0x0400123D RID: 4669
	public int Class;

	// Token: 0x0400123E RID: 4670
	public int Weight = int.MaxValue;

	// Token: 0x0400123F RID: 4671
	public string StartTime = string.Empty;

	// Token: 0x04001240 RID: 4672
	public string EndTime = string.Empty;

	// Token: 0x04001241 RID: 4673
	public int OnlyBuyCount = -1;

	// Token: 0x04001242 RID: 4674
	public int MinLevel;

	// Token: 0x04001243 RID: 4675
	public int MaxLevel = int.MaxValue;

	// Token: 0x04001244 RID: 4676
	private int[] mStartTimes;

	// Token: 0x04001245 RID: 4677
	private int[] mEndTimes;
}
