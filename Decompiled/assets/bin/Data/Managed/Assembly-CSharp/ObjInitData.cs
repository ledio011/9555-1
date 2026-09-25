using System;
using UnityEngine;

// Token: 0x0200081E RID: 2078
public class ObjInitData
{
	// Token: 0x0600336D RID: 13165 RVA: 0x000CA068 File Offset: 0x000C8268
	public ObjInitData(long ServerId, Vector3 pos)
	{
		this.mServerID = ServerId;
		this.mPos = pos;
	}

	// Token: 0x0600336E RID: 13166 RVA: 0x000CA0BC File Offset: 0x000C82BC
	public ObjInitData()
	{
	}

	// Token: 0x040021D0 RID: 8656
	public Vector3 mPos;

	// Token: 0x040021D1 RID: 8657
	public Vector3 mDir;

	// Token: 0x040021D2 RID: 8658
	public long mServerID;

	// Token: 0x040021D3 RID: 8659
	public int mCharacterId = -1;

	// Token: 0x040021D4 RID: 8660
	public string mCharacterModelId = string.Empty;

	// Token: 0x040021D5 RID: 8661
	public string Name = string.Empty;

	// Token: 0x040021D6 RID: 8662
	public string GuildName = string.Empty;

	// Token: 0x040021D7 RID: 8663
	public long GuildId = -1L;
}
