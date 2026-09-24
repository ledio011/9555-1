using System;

// Token: 0x02000171 RID: 369
public class FunctionData
{
	// Token: 0x170002FD RID: 765
	// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00062CA4 File Offset: 0x00060EA4
	public string MName
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Name, new object[0]);
		}
	}

	// Token: 0x170002FE RID: 766
	// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00062CB8 File Offset: 0x00060EB8
	public string[] SideMissionIdList
	{
		get
		{
			if (this.mSideMissionIdList == null && !string.IsNullOrEmpty(this.SideMissionId))
			{
				this.mSideMissionIdList = this.SideMissionId.Split(new char[]
				{
					';'
				});
			}
			return this.mSideMissionIdList;
		}
	}

	// Token: 0x04000EE1 RID: 3809
	public string ID;

	// Token: 0x04000EE2 RID: 3810
	public string Name;

	// Token: 0x04000EE3 RID: 3811
	public int Class;

	// Token: 0x04000EE4 RID: 3812
	public int Condition;

	// Token: 0x04000EE5 RID: 3813
	public int IsDownload;

	// Token: 0x04000EE6 RID: 3814
	public string CloseTips;

	// Token: 0x04000EE7 RID: 3815
	public int Tips;

	// Token: 0x04000EE8 RID: 3816
	public int FirstOpen;

	// Token: 0x04000EE9 RID: 3817
	public int UnlockType = -1;

	// Token: 0x04000EEA RID: 3818
	public string SideMissionId = string.Empty;

	// Token: 0x04000EEB RID: 3819
	public int Index;

	// Token: 0x04000EEC RID: 3820
	private string[] mSideMissionIdList;
}
