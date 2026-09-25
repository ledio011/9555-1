using System;

// Token: 0x020001CD RID: 461
public class BlockMonsterData
{
	// Token: 0x060010BD RID: 4285 RVA: 0x0006C8CC File Offset: 0x0006AACC
	public BlockMonsterData(MonsterData data, long serverId = -1L)
	{
		this.Data = data;
		this.ServerId = serverId;
	}

	// Token: 0x04001434 RID: 5172
	public MonsterData Data;

	// Token: 0x04001435 RID: 5173
	public long ServerId = -1L;
}
