using System;
using UnityEngine;

// Token: 0x02000201 RID: 513
public class GuildBattlePoint : MonoBehaviour
{
	// Token: 0x06001170 RID: 4464 RVA: 0x000710CC File Offset: 0x0006F2CC
	public void Reset(long id, Vector3 pos, float radius, Color col)
	{
		base.transform.position = pos;
		base.transform.rotation = Quaternion.identity;
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x000710F8 File Offset: 0x0006F2F8
	public void UpdateColor(Color col)
	{
	}

	// Token: 0x04001749 RID: 5961
	public int Id;

	// Token: 0x0400174A RID: 5962
	public float Radius;
}
