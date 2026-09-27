using System;

// Token: 0x020001BC RID: 444
public class SurveyMissionData
{
	// Token: 0x1700037F RID: 895
	// (get) Token: 0x0600102F RID: 4143 RVA: 0x00066320 File Offset: 0x00064520
	public float AutoFreshTimeSecond
	{
		get
		{
			return (float)this.AutoFreshTime / 1000f;
		}
	}

	// Token: 0x17000380 RID: 896
	// (get) Token: 0x06001030 RID: 4144 RVA: 0x00066330 File Offset: 0x00064530
	public float SurveyTimeSecond
	{
		get
		{
			return (float)this.SurveyTime / 1000f;
		}
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x00066340 File Offset: 0x00064540
	public string GetSurveyItemName()
	{
		return string.Format("SurveyItem{0}", this.ID);
	}

	// Token: 0x04001315 RID: 4885
	public string ID = string.Empty;

	// Token: 0x04001316 RID: 4886
	public string SceneID = string.Empty;

	// Token: 0x04001317 RID: 4887
	public string ModelName = string.Empty;

	// Token: 0x04001318 RID: 4888
	public float PosX;

	// Token: 0x04001319 RID: 4889
	public float PosY;

	// Token: 0x0400131A RID: 4890
	public float PosZ;

	// Token: 0x0400131B RID: 4891
	public float PosO;

	// Token: 0x0400131C RID: 4892
	public int Count = 1;

	// Token: 0x0400131D RID: 4893
	public int SurveyTime = 1000;

	// Token: 0x0400131E RID: 4894
	public int IsAutoFresh;

	// Token: 0x0400131F RID: 4895
	public int AutoFreshTime;

	// Token: 0x04001320 RID: 4896
	public int NeedNum;

	// Token: 0x04001321 RID: 4897
	public string Text = string.Empty;

	// Token: 0x04001322 RID: 4898
	public int IsAlwaysExist;

	// Token: 0x04001323 RID: 4899
	public int DelayRecycleTime = -1;
}
