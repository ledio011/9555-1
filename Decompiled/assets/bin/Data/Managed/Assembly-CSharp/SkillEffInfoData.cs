using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x02000865 RID: 2149
public class SkillEffInfoData
{
	// Token: 0x060037E5 RID: 14309 RVA: 0x000E7878 File Offset: 0x000E5A78
	public SkillEffInfoData(string skId, string effId, float t, long tarId, List<attack_list> attackList = null, bool isServerAttack = false)
	{
		this.skillId = skId;
		this.EffInfoId = effId;
		this.DelayTime = t;
		this.TargetId = tarId;
		this.AttackList = attackList;
		this.IsServerAttack = isServerAttack;
	}

	// Token: 0x040024E4 RID: 9444
	public string skillId;

	// Token: 0x040024E5 RID: 9445
	public string EffInfoId;

	// Token: 0x040024E6 RID: 9446
	public float DelayTime;

	// Token: 0x040024E7 RID: 9447
	public long TargetId;

	// Token: 0x040024E8 RID: 9448
	public List<attack_list> AttackList;

	// Token: 0x040024E9 RID: 9449
	public bool IsServerAttack;
}
