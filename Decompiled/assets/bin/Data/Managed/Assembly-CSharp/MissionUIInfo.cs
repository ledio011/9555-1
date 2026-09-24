using System;

// Token: 0x02000900 RID: 2304
public class MissionUIInfo
{
	// Token: 0x06003EFD RID: 16125 RVA: 0x001232A8 File Offset: 0x001214A8
	public MissionUIInfo(string missionId, MISSION_STATE type)
	{
		this.MissionID = missionId;
		this.uiType = type;
	}

	// Token: 0x04002AB1 RID: 10929
	public string MissionID;

	// Token: 0x04002AB2 RID: 10930
	public MISSION_STATE uiType;
}
