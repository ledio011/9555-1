using System;
using UnityEngine;

// Token: 0x0200081D RID: 2077
public class ObjCarInitData
{
	// Token: 0x0600336B RID: 13163 RVA: 0x000CA008 File Offset: 0x000C8208
	public ObjCarInitData(Vector3 pos, Vector3 angle, long serverId, string characterModelId, bool policeFlag, MountData mountData = null)
	{
		this.Pos = pos;
		this.Angle = angle;
		this.ServerID = serverId;
		this.CharacterModelId = characterModelId;
		this.PoliceFlag = policeFlag;
		this.CarMountData = mountData;
	}

	// Token: 0x0600336C RID: 13164 RVA: 0x000CA054 File Offset: 0x000C8254
	public ObjCarInitData()
	{
	}

	// Token: 0x040021CA RID: 8650
	public Vector3 Pos;

	// Token: 0x040021CB RID: 8651
	public Vector3 Angle;

	// Token: 0x040021CC RID: 8652
	public long ServerID;

	// Token: 0x040021CD RID: 8653
	public string CharacterModelId = string.Empty;

	// Token: 0x040021CE RID: 8654
	public bool PoliceFlag;

	// Token: 0x040021CF RID: 8655
	public MountData CarMountData;
}
