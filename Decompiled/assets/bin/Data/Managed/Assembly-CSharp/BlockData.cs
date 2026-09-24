using System;
using System.Collections.Generic;

// Token: 0x020001CC RID: 460
[Serializable]
public class BlockData
{
	// Token: 0x04001431 RID: 5169
	public List<int> PointIndex = new List<int>();

	// Token: 0x04001432 RID: 5170
	public List<BlockMonsterData> BlockMonsterList = new List<BlockMonsterData>();

	// Token: 0x04001433 RID: 5171
	public List<BlockCarData> BlockCarDataList = new List<BlockCarData>();
}
