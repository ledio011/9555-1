using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000822 RID: 2082
public class ObjInitDropItemData
{
	// Token: 0x04002219 RID: 8729
	public long ServerID = -1L;

	// Token: 0x0400221A RID: 8730
	public VectorXZ Pos = VectorXZ.zero;

	// Token: 0x0400221B RID: 8731
	public GameDefine.ITEM_TYPE ItemType = GameDefine.ITEM_TYPE.INVALID;

	// Token: 0x0400221C RID: 8732
	public item item;

	// Token: 0x0400221D RID: 8733
	public long ownerServerId = -1L;
}
