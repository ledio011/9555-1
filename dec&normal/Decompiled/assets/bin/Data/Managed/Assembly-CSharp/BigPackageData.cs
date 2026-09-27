using System;
using System.Collections.Generic;

// Token: 0x0200014E RID: 334
public class BigPackageData
{
	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x06000ECA RID: 3786 RVA: 0x000612C4 File Offset: 0x0005F4C4
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

	// Token: 0x06000ECB RID: 3787 RVA: 0x00061348 File Offset: 0x0005F548
	public int[] GetCurTimeEnd()
	{
		if (!string.IsNullOrEmpty(this.TimeList))
		{
			if (this.mTimeList == null || this.mTimeList.Length == 0)
			{
				this.mTimeList = this.TimeList.Split(new char[]
				{
					'#'
				});
			}
			if (this.mTimeList != null && this.mTimeList.Length > 0)
			{
				List<BigPackageTimeListData> bigPackageTimeListDataListById = DataManager.GetBigPackageTimeListDataListById(this.mTimeList);
				for (int i = 0; i < bigPackageTimeListDataListById.Count; i++)
				{
					if (TimeTools.IsTimeRange(bigPackageTimeListDataListById[i].Starttimes, bigPackageTimeListDataListById[i].EndTimes))
					{
						return bigPackageTimeListDataListById[i].EndTimes;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x04000CC9 RID: 3273
	public string ID;

	// Token: 0x04000CCA RID: 3274
	public string XDItemID1;

	// Token: 0x04000CCB RID: 3275
	public int XDItemCount1;

	// Token: 0x04000CCC RID: 3276
	public int XDQuality1;

	// Token: 0x04000CCD RID: 3277
	public string XDItemID2;

	// Token: 0x04000CCE RID: 3278
	public int XDItemCount2;

	// Token: 0x04000CCF RID: 3279
	public int XDQuality2;

	// Token: 0x04000CD0 RID: 3280
	public string XDItemID3;

	// Token: 0x04000CD1 RID: 3281
	public int XDItemCount3;

	// Token: 0x04000CD2 RID: 3282
	public int XDQuality3;

	// Token: 0x04000CD3 RID: 3283
	public string XDItemID4;

	// Token: 0x04000CD4 RID: 3284
	public int XDItemCount4;

	// Token: 0x04000CD5 RID: 3285
	public int XDQuality4;

	// Token: 0x04000CD6 RID: 3286
	public string QJItemID1;

	// Token: 0x04000CD7 RID: 3287
	public int QJItemCount1;

	// Token: 0x04000CD8 RID: 3288
	public int QJQuality1;

	// Token: 0x04000CD9 RID: 3289
	public string QJItemID2;

	// Token: 0x04000CDA RID: 3290
	public int QJItemCount2;

	// Token: 0x04000CDB RID: 3291
	public int QJQuality2;

	// Token: 0x04000CDC RID: 3292
	public string QJItemID3;

	// Token: 0x04000CDD RID: 3293
	public int QJItemCount3;

	// Token: 0x04000CDE RID: 3294
	public int QJQuality3;

	// Token: 0x04000CDF RID: 3295
	public string QJItemID4;

	// Token: 0x04000CE0 RID: 3296
	public int QJItemCount4;

	// Token: 0x04000CE1 RID: 3297
	public int QJQuality4;

	// Token: 0x04000CE2 RID: 3298
	public string NQItemID1;

	// Token: 0x04000CE3 RID: 3299
	public int NQItemCount1;

	// Token: 0x04000CE4 RID: 3300
	public int NQQuality1;

	// Token: 0x04000CE5 RID: 3301
	public string NQItemID2;

	// Token: 0x04000CE6 RID: 3302
	public int NQItemCount2;

	// Token: 0x04000CE7 RID: 3303
	public int NQQuality2;

	// Token: 0x04000CE8 RID: 3304
	public string NQItemID3;

	// Token: 0x04000CE9 RID: 3305
	public int NQItemCount3;

	// Token: 0x04000CEA RID: 3306
	public int NQQuality3;

	// Token: 0x04000CEB RID: 3307
	public string NQItemID4;

	// Token: 0x04000CEC RID: 3308
	public int NQItemCount4;

	// Token: 0x04000CED RID: 3309
	public int NQQuality4;

	// Token: 0x04000CEE RID: 3310
	public string ItemID5;

	// Token: 0x04000CEF RID: 3311
	public int ItemCount5;

	// Token: 0x04000CF0 RID: 3312
	public int Quality5;

	// Token: 0x04000CF1 RID: 3313
	public int PriceType = -1;

	// Token: 0x04000CF2 RID: 3314
	public int PriceCost;

	// Token: 0x04000CF3 RID: 3315
	public string ProductId = string.Empty;

	// Token: 0x04000CF4 RID: 3316
	public string Dollor;

	// Token: 0x04000CF5 RID: 3317
	public int MaxCount = -1;

	// Token: 0x04000CF6 RID: 3318
	public int TimeHour = -1;

	// Token: 0x04000CF7 RID: 3319
	public string StartTime = string.Empty;

	// Token: 0x04000CF8 RID: 3320
	public string EndTime = string.Empty;

	// Token: 0x04000CF9 RID: 3321
	public int SellType;

	// Token: 0x04000CFA RID: 3322
	public int sortID = int.MaxValue;

	// Token: 0x04000CFB RID: 3323
	public int showmodeltype;

	// Token: 0x04000CFC RID: 3324
	public string TextureTitle1;

	// Token: 0x04000CFD RID: 3325
	public string TextureTitle2;

	// Token: 0x04000CFE RID: 3326
	public int LevelMin;

	// Token: 0x04000CFF RID: 3327
	public int LevelMax = 80;

	// Token: 0x04000D00 RID: 3328
	public string ServerID;

	// Token: 0x04000D01 RID: 3329
	public string FatherID;

	// Token: 0x04000D02 RID: 3330
	public int RechargeMin;

	// Token: 0x04000D03 RID: 3331
	public int RechargeMax;

	// Token: 0x04000D04 RID: 3332
	public int Discount;

	// Token: 0x04000D05 RID: 3333
	public string Name;

	// Token: 0x04000D06 RID: 3334
	public string Icon;

	// Token: 0x04000D07 RID: 3335
	public int IconQuality;

	// Token: 0x04000D08 RID: 3336
	public string TimeList = string.Empty;

	// Token: 0x04000D09 RID: 3337
	private string[] mTimeList;

	// Token: 0x04000D0A RID: 3338
	private int[] mEndTimes;
}
