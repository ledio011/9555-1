using System;
using System.Collections.Generic;

// Token: 0x02000195 RID: 405
public class NPCDialogData
{
	// Token: 0x1700033F RID: 831
	// (get) Token: 0x06000FBC RID: 4028 RVA: 0x00064850 File Offset: 0x00062A50
	public List<string> MissionIDList
	{
		get
		{
			if (this.mMissionIDList.Count == 0)
			{
				string[] array = this.MissionID.Split(new char[]
				{
					';'
				});
				for (int i = 0; i < array.Length; i++)
				{
					this.mMissionIDList.Add(array[i]);
				}
			}
			return this.mMissionIDList;
		}
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x000648AC File Offset: 0x00062AAC
	private void AddMissionList(string missionID)
	{
		if (!string.IsNullOrEmpty(missionID))
		{
			this.mMissionIDList.Add(missionID);
		}
	}

	// Token: 0x0400113C RID: 4412
	public string ID = string.Empty;

	// Token: 0x0400113D RID: 4413
	public string Dialog = string.Empty;

	// Token: 0x0400113E RID: 4414
	public string OptionDialogID = string.Empty;

	// Token: 0x0400113F RID: 4415
	public string MissionID = string.Empty;

	// Token: 0x04001140 RID: 4416
	private List<string> mMissionIDList = new List<string>();
}
