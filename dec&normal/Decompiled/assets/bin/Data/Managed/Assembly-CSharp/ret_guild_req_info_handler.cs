using System;
using Sproto;
using SprotoType;

// Token: 0x0200027C RID: 636
public class ret_guild_req_info_handler
{
	// Token: 0x0600137F RID: 4991 RVA: 0x0007F788 File Offset: 0x0007D988
	public static SprotoTypeBase ret_guild_req_info_request(SprotoTypeBase req)
	{
		ret_guild_req_info.request request = req as ret_guild_req_info.request;
		if (request != null)
		{
			WaitResponseUIRootLogic.CloseBox();
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			if (request.HasGuild_info)
			{
				playerData.PlayerGuild.Init(request.guild_info, request.donate_records);
				playerData.PlayerGuild.UpdateAllContribute((!request.HasAll_contribute) ? 0 : ((int)request.all_contribute));
				if (request.HasContribute)
				{
					GameMoneyHelper.SetGuildContribute((long)((int)request.contribute));
				}
				else
				{
					GameMoneyHelper.SetGuildContribute(0L);
				}
				if (string.IsNullOrEmpty(playerData.MainPlayerAttrData.GuildName) && mainPlayer != null)
				{
					PlayerHeadInfoLogic playerHeadInfoLogic = mainPlayer.HeadInfoLogic as PlayerHeadInfoLogic;
					if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager != null && SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsSurviveBattleScene())
					{
						playerHeadInfoLogic.Reset(true, playerData.MainPlayerAttrData.CurTitleLevel, playerData.MainPlayerAttrData.Name, request.guild_info.guildName, playerData.MainPlayerAttrData.Camp, false, SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsChampionGuild(request.guild_info.guildId));
					}
					else
					{
						playerHeadInfoLogic.Reset(true, playerData.MainPlayerAttrData.CurTitleLevel, playerData.MainPlayerAttrData.Name, request.guild_info.guildName, GameDefine.CAMP_TYPE.PLAYER_1, false, SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsChampionGuild(request.guild_info.guildId));
					}
				}
				if (SingletonUnity<NewGuildUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewGuildUIRootLogic>.Instance.gameObject))
				{
					SingletonUnity<NewGuildUIRootLogic>.Instance.ShowHasGuildInfo();
				}
			}
			else
			{
				if (!request.HasExist || request.exist)
				{
					NoticeLogic.AddNotifyData("#{100153}", true, false);
					if (SingletonUnity<NewGuildUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewGuildUIRootLogic>.Instance.gameObject))
					{
						SingletonUnity<NewGuildUIRootLogic>.Instance.OnClickCloseBtn();
					}
					return null;
				}
				playerData.PlayerGuild.ResetGuild();
				if (SingletonUnity<NewGuildUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewGuildUIRootLogic>.Instance.gameObject))
				{
					SingletonUnity<NewGuildUIRootLogic>.Instance.RequestGuildList();
				}
			}
		}
		return null;
	}
}
