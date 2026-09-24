using System;
using UnityEngine;

// Token: 0x02000957 RID: 2391
public class MapPoint
{
	// Token: 0x060042EC RID: 17132 RVA: 0x00148B08 File Offset: 0x00146D08
	public MapPoint(string name, Vector3 pos, MAP_POINT_TYPE type, string id)
	{
		this.PointName = name;
		this.PointPos = pos;
		this.PointType = type;
		this.NpcId = id;
	}

	// Token: 0x060042ED RID: 17133 RVA: 0x00148B30 File Offset: 0x00146D30
	public MapPoint()
	{
	}

	// Token: 0x04002F53 RID: 12115
	public string PointName;

	// Token: 0x04002F54 RID: 12116
	public string NpcId;

	// Token: 0x04002F55 RID: 12117
	public Vector3 PointPos;

	// Token: 0x04002F56 RID: 12118
	public MAP_POINT_TYPE PointType;
}
