using System;

// Token: 0x020001BF RID: 447
public class TeamData
{
	// Token: 0x06001038 RID: 4152 RVA: 0x000664B0 File Offset: 0x000646B0
	public string GetTeamTitle()
	{
		if (string.IsNullOrEmpty(this.TitleName))
		{
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(this.CopyId);
			return copySceneDataById.Name;
		}
		return this.TitleName;
	}

	// Token: 0x17000384 RID: 900
	// (get) Token: 0x06001039 RID: 4153 RVA: 0x000664E8 File Offset: 0x000646E8
	public string MTitleName
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.TitleName, new object[0]);
		}
	}

	// Token: 0x04001348 RID: 4936
	public string ID;

	// Token: 0x04001349 RID: 4937
	public int GoalType;

	// Token: 0x0400134A RID: 4938
	public string TitleName;

	// Token: 0x0400134B RID: 4939
	public string CopyId;

	// Token: 0x0400134C RID: 4940
	public string ParentId;
}
