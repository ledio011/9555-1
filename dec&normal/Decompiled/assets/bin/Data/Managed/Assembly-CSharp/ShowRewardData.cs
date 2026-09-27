using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001AC RID: 428
public class ShowRewardData
{
	// Token: 0x17000368 RID: 872
	// (get) Token: 0x06000FFD RID: 4093 RVA: 0x00065748 File Offset: 0x00063948
	public List<string> ItemIdList
	{
		get
		{
			if (!this.mInitFlag)
			{
				this.InitData();
			}
			return this.mItemIdList;
		}
	}

	// Token: 0x17000369 RID: 873
	// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00065764 File Offset: 0x00063964
	public List<EQUIP_QUALITY> QualityList
	{
		get
		{
			if (!this.mInitFlag)
			{
				this.InitData();
			}
			return this.mQualityList;
		}
	}

	// Token: 0x1700036A RID: 874
	// (get) Token: 0x06000FFF RID: 4095 RVA: 0x00065780 File Offset: 0x00063980
	public List<int> CountList
	{
		get
		{
			if (!this.mInitFlag)
			{
				this.InitData();
			}
			return this.mCountList;
		}
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x0006579C File Offset: 0x0006399C
	private void InitData()
	{
		this.mInitFlag = true;
		this.mItemIdList = new List<string>();
		this.mQualityList = new List<EQUIP_QUALITY>();
		this.mCountList = new List<int>();
		this.AddListItem(this.mItemIdList, this.Item1, this.mQualityList, this.Quality1, this.mCountList, this.ItemCount1);
		this.AddListItem(this.mItemIdList, this.Item2, this.mQualityList, this.Quality2, this.mCountList, this.ItemCount2);
		this.AddListItem(this.mItemIdList, this.Item3, this.mQualityList, this.Quality3, this.mCountList, this.ItemCount3);
		this.AddListItem(this.mItemIdList, this.Item4, this.mQualityList, this.Quality4, this.mCountList, this.ItemCount4);
		this.AddListItem(this.mItemIdList, this.Item5, this.mQualityList, this.Quality5, this.mCountList, this.ItemCount5);
		this.AddListItem(this.mItemIdList, this.Item6, this.mQualityList, this.Quality6, this.mCountList, this.ItemCount6);
		this.AddListItem(this.mItemIdList, this.Item7, this.mQualityList, this.Quality7, this.mCountList, this.ItemCount7);
		this.AddListItem(this.mItemIdList, this.Item8, this.mQualityList, this.Quality8, this.mCountList, this.ItemCount8);
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x00065924 File Offset: 0x00063B24
	private void AddListItem(List<string> itemList, string itemId, List<EQUIP_QUALITY> qualityList, int quality, List<int> countList, int count)
	{
		if (!string.IsNullOrEmpty(itemId))
		{
			itemList.Add(itemId);
			qualityList.Add((EQUIP_QUALITY)quality);
			countList.Add(count);
		}
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x0006594C File Offset: 0x00063B4C
	public List<int> CalCountRatio()
	{
		float num = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.ServerLevelSealRatio();
		List<int> list = new List<int>();
		if (this.CountList != null && this.CountList.Count > 0)
		{
			for (int i = 0; i < this.CountList.Count; i++)
			{
				int num2 = Mathf.CeilToInt((float)this.CountList[i] * num);
				if (num2 == 0)
				{
					num2 = 1;
				}
				list.Add(num2);
			}
		}
		return list;
	}

	// Token: 0x0400124E RID: 4686
	public string ID = string.Empty;

	// Token: 0x0400124F RID: 4687
	public string Desc = string.Empty;

	// Token: 0x04001250 RID: 4688
	public int Exp;

	// Token: 0x04001251 RID: 4689
	public int Money1;

	// Token: 0x04001252 RID: 4690
	public int Money2;

	// Token: 0x04001253 RID: 4691
	public string Item1 = string.Empty;

	// Token: 0x04001254 RID: 4692
	public int Quality1 = -1;

	// Token: 0x04001255 RID: 4693
	public int ItemCount1 = -1;

	// Token: 0x04001256 RID: 4694
	public string Item2 = string.Empty;

	// Token: 0x04001257 RID: 4695
	public int Quality2 = -1;

	// Token: 0x04001258 RID: 4696
	public int ItemCount2 = -1;

	// Token: 0x04001259 RID: 4697
	public string Item3 = string.Empty;

	// Token: 0x0400125A RID: 4698
	public int Quality3 = -1;

	// Token: 0x0400125B RID: 4699
	public int ItemCount3 = -1;

	// Token: 0x0400125C RID: 4700
	public string Item4 = string.Empty;

	// Token: 0x0400125D RID: 4701
	public int Quality4 = -1;

	// Token: 0x0400125E RID: 4702
	public int ItemCount4 = -1;

	// Token: 0x0400125F RID: 4703
	public string Item5 = string.Empty;

	// Token: 0x04001260 RID: 4704
	public int Quality5 = -1;

	// Token: 0x04001261 RID: 4705
	public int ItemCount5 = -1;

	// Token: 0x04001262 RID: 4706
	public string Item6 = string.Empty;

	// Token: 0x04001263 RID: 4707
	public int Quality6 = -1;

	// Token: 0x04001264 RID: 4708
	public int ItemCount6 = -1;

	// Token: 0x04001265 RID: 4709
	public string Item7 = string.Empty;

	// Token: 0x04001266 RID: 4710
	public int Quality7 = -1;

	// Token: 0x04001267 RID: 4711
	public int ItemCount7 = -1;

	// Token: 0x04001268 RID: 4712
	public string Item8 = string.Empty;

	// Token: 0x04001269 RID: 4713
	public int Quality8 = -1;

	// Token: 0x0400126A RID: 4714
	public int ItemCount8 = -1;

	// Token: 0x0400126B RID: 4715
	private List<string> mItemIdList;

	// Token: 0x0400126C RID: 4716
	private List<EQUIP_QUALITY> mQualityList;

	// Token: 0x0400126D RID: 4717
	private List<int> mCountList;

	// Token: 0x0400126E RID: 4718
	private bool mInitFlag;
}
