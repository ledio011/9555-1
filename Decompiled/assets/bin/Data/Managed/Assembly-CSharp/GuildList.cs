using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x02000854 RID: 2132
public class GuildList
{
	// Token: 0x06003739 RID: 14137 RVA: 0x000E3204 File Offset: 0x000E1404
	public void Init()
	{
	}

	// Token: 0x17000F28 RID: 3880
	// (get) Token: 0x0600373A RID: 14138 RVA: 0x000E3208 File Offset: 0x000E1408
	public List<GuildInfo> GuildInfoList
	{
		get
		{
			return this.mGuildInfoList;
		}
	}

	// Token: 0x0600373B RID: 14139 RVA: 0x000E3210 File Offset: 0x000E1410
	public void UpDataGuildList(List<guild_info> list)
	{
		if (list.Count <= 0)
		{
			return;
		}
		if (this.mGuildInfoList == null)
		{
			this.mGuildInfoList = new List<GuildInfo>();
		}
		else
		{
			this.mGuildInfoList.Clear();
		}
		for (int i = 0; i < list.Count; i++)
		{
			GuildInfo guildInfo = new GuildInfo(list[i]);
			this.mGuildInfoList.Add(guildInfo);
		}
	}

	// Token: 0x04002464 RID: 9316
	private List<GuildInfo> mGuildInfoList = new List<GuildInfo>();
}
