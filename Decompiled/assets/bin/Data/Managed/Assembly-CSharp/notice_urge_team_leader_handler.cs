using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x02000249 RID: 585
public class notice_urge_team_leader_handler
{
	// Token: 0x06001317 RID: 4887 RVA: 0x0007C59C File Offset: 0x0007A79C
	public static SprotoTypeBase notice_urge_team_leader_request(SprotoTypeBase req)
	{
		notice_urge_team_leader.request request = req as notice_urge_team_leader.request;
		if (request != null)
		{
			NoticeLogic.AddNotifyData("#{100294}", true, false);
			NewMessageUIRootLogic.AddteamInfo(new InviteTeamInfo(true, request.name, request.id, (long)Time.time));
		}
		return null;
	}
}
