using System;
using UnityEngine;

// Token: 0x020009E5 RID: 2533
public class TargetBasicInfo
{
	// Token: 0x060047E9 RID: 18409 RVA: 0x001700BC File Offset: 0x0016E2BC
	public void ResetInfo(long id, int level, int comval, string name, PROFESSION_TYPE type, int state, long guild, string guildName, Vector2 mousePos)
	{
		this.ServerId = id;
		this.Level = level;
		this.ComboValue = comval;
		this.Name = name;
		this.profession = type;
		this.OnlineState = state;
		this.GuildId = guild;
		this.GuildName = guildName;
		this.MousePos = mousePos;
	}

	// Token: 0x060047EA RID: 18410 RVA: 0x00170110 File Offset: 0x0016E310
	public bool isHaveGuild()
	{
		return this.GuildId > 0L;
	}

	// Token: 0x04003553 RID: 13651
	public long ServerId;

	// Token: 0x04003554 RID: 13652
	public int Level;

	// Token: 0x04003555 RID: 13653
	public int ComboValue;

	// Token: 0x04003556 RID: 13654
	public string Name;

	// Token: 0x04003557 RID: 13655
	public string GuildName;

	// Token: 0x04003558 RID: 13656
	public Vector2 MousePos;

	// Token: 0x04003559 RID: 13657
	public int OnlineState;

	// Token: 0x0400355A RID: 13658
	public long GuildId;

	// Token: 0x0400355B RID: 13659
	public PROFESSION_TYPE profession;
}
