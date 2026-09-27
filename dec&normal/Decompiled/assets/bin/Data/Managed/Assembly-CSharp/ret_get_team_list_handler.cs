using System;
using System.Collections.Generic;
using Sproto;
using SprotoType;

// Token: 0x0200026A RID: 618
public class ret_get_team_list_handler
{
	// Token: 0x0600135B RID: 4955 RVA: 0x0007EF00 File Offset: 0x0007D100
	public static SprotoTypeBase ret_get_team_list_request(SprotoTypeBase req)
	{
		ret_get_team_list.request request = req as ret_get_team_list.request;
		if (request != null && request.HasTeams && SingletonUnity<SearchTeamRootLogic>.Exists)
		{
			SingletonUnity<SearchTeamRootLogic>.Instance.UpdateTeamList(new List<team>(request.teams.Values));
		}
		return null;
	}
}
