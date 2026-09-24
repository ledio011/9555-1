using System;

// Token: 0x02000866 RID: 2150
public class CharacterEffInfoData
{
	// Token: 0x060037E6 RID: 14310 RVA: 0x000E78B0 File Offset: 0x000E5AB0
	public CharacterEffInfoData(ObjCharacter target, int val)
	{
		this.TargetObj = target;
		this.EffVal = val;
	}

	// Token: 0x040024EA RID: 9450
	public ObjCharacter TargetObj;

	// Token: 0x040024EB RID: 9451
	public int EffVal;
}
