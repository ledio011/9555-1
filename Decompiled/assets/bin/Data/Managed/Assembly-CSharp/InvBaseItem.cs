using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000020 RID: 32
[Serializable]
public class InvBaseItem
{
	// Token: 0x04000085 RID: 133
	public int id16;

	// Token: 0x04000086 RID: 134
	public string name;

	// Token: 0x04000087 RID: 135
	public string description;

	// Token: 0x04000088 RID: 136
	public InvBaseItem.Slot slot;

	// Token: 0x04000089 RID: 137
	public int minItemLevel = 1;

	// Token: 0x0400008A RID: 138
	public int maxItemLevel = 50;

	// Token: 0x0400008B RID: 139
	public List<InvStat> stats = new List<InvStat>();

	// Token: 0x0400008C RID: 140
	public GameObject attachment;

	// Token: 0x0400008D RID: 141
	public Color color = Color.white;

	// Token: 0x0400008E RID: 142
	public UIAtlas iconAtlas;

	// Token: 0x0400008F RID: 143
	public string iconName = string.Empty;

	// Token: 0x02000021 RID: 33
	public enum Slot
	{
		// Token: 0x04000091 RID: 145
		None,
		// Token: 0x04000092 RID: 146
		Weapon,
		// Token: 0x04000093 RID: 147
		Shield,
		// Token: 0x04000094 RID: 148
		Body,
		// Token: 0x04000095 RID: 149
		Shoulders,
		// Token: 0x04000096 RID: 150
		Bracers,
		// Token: 0x04000097 RID: 151
		Boots,
		// Token: 0x04000098 RID: 152
		Trinket,
		// Token: 0x04000099 RID: 153
		_LastDoNotUse
	}
}
