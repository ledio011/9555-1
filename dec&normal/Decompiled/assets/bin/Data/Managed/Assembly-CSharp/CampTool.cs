using System;
using System.Collections.Generic;

// Token: 0x020008AD RID: 2221
public class CampTool
{
	// Token: 0x06003BDA RID: 15322 RVA: 0x00104FE0 File Offset: 0x001031E0
	public static bool CanAttack(GameDefine.CAMP_TYPE selfCamp, GameDefine.CAMP_TYPE targetCamp)
	{
		if (selfCamp == GameDefine.CAMP_TYPE.PLAYER_1)
		{
			switch (targetCamp)
			{
			case GameDefine.CAMP_TYPE.PLAYER_1:
				return false;
			case GameDefine.CAMP_TYPE.PLAYER_2:
				return true;
			case GameDefine.CAMP_TYPE.NORMAL_NPC:
				return true;
			case GameDefine.CAMP_TYPE.NPC_ATTACK_NPC:
				return true;
			case GameDefine.CAMP_TYPE.STATIC_NPC:
				return true;
			case GameDefine.CAMP_TYPE.FUNCTION_NPC:
				return false;
			case GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC:
				return false;
			}
		}
		else if (selfCamp == GameDefine.CAMP_TYPE.PLAYER_2)
		{
			switch (targetCamp)
			{
			case GameDefine.CAMP_TYPE.PLAYER_1:
				return true;
			case GameDefine.CAMP_TYPE.PLAYER_2:
				return false;
			case GameDefine.CAMP_TYPE.NORMAL_NPC:
				return true;
			case GameDefine.CAMP_TYPE.NPC_ATTACK_NPC:
				return true;
			case GameDefine.CAMP_TYPE.STATIC_NPC:
				return true;
			case GameDefine.CAMP_TYPE.FUNCTION_NPC:
				return false;
			case GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC:
				return false;
			}
		}
		else if (selfCamp == GameDefine.CAMP_TYPE.NORMAL_NPC)
		{
			switch (targetCamp)
			{
			case GameDefine.CAMP_TYPE.PLAYER_1:
				return true;
			case GameDefine.CAMP_TYPE.PLAYER_2:
				return true;
			case GameDefine.CAMP_TYPE.NORMAL_NPC:
				return false;
			case GameDefine.CAMP_TYPE.NPC_ATTACK_NPC:
				return false;
			case GameDefine.CAMP_TYPE.STATIC_NPC:
				return false;
			case GameDefine.CAMP_TYPE.FUNCTION_NPC:
				return false;
			case GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC:
				return true;
			}
		}
		else if (selfCamp == GameDefine.CAMP_TYPE.NPC_ATTACK_NPC)
		{
			switch (targetCamp)
			{
			case GameDefine.CAMP_TYPE.PLAYER_1:
				return true;
			case GameDefine.CAMP_TYPE.PLAYER_2:
				return true;
			case GameDefine.CAMP_TYPE.NORMAL_NPC:
				return false;
			case GameDefine.CAMP_TYPE.NPC_ATTACK_NPC:
				return false;
			case GameDefine.CAMP_TYPE.STATIC_NPC:
				return false;
			case GameDefine.CAMP_TYPE.FUNCTION_NPC:
				return false;
			case GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC:
				return true;
			}
		}
		else if (selfCamp == GameDefine.CAMP_TYPE.STATIC_NPC)
		{
			switch (targetCamp)
			{
			case GameDefine.CAMP_TYPE.PLAYER_1:
				return false;
			case GameDefine.CAMP_TYPE.PLAYER_2:
				return false;
			case GameDefine.CAMP_TYPE.NORMAL_NPC:
				return false;
			case GameDefine.CAMP_TYPE.NPC_ATTACK_NPC:
				return false;
			case GameDefine.CAMP_TYPE.STATIC_NPC:
				return false;
			case GameDefine.CAMP_TYPE.FUNCTION_NPC:
				return false;
			case GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC:
				return false;
			}
		}
		else if (selfCamp == GameDefine.CAMP_TYPE.FUNCTION_NPC)
		{
			switch (targetCamp)
			{
			case GameDefine.CAMP_TYPE.PLAYER_1:
				return false;
			case GameDefine.CAMP_TYPE.PLAYER_2:
				return false;
			case GameDefine.CAMP_TYPE.NORMAL_NPC:
				return false;
			case GameDefine.CAMP_TYPE.NPC_ATTACK_NPC:
				return false;
			case GameDefine.CAMP_TYPE.STATIC_NPC:
				return false;
			case GameDefine.CAMP_TYPE.FUNCTION_NPC:
				return false;
			case GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC:
				return false;
			}
		}
		else if (selfCamp == GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC)
		{
			switch (targetCamp)
			{
			case GameDefine.CAMP_TYPE.PLAYER_1:
				return false;
			case GameDefine.CAMP_TYPE.PLAYER_2:
				return false;
			case GameDefine.CAMP_TYPE.NORMAL_NPC:
				return false;
			case GameDefine.CAMP_TYPE.NPC_ATTACK_NPC:
				return false;
			case GameDefine.CAMP_TYPE.STATIC_NPC:
				return false;
			case GameDefine.CAMP_TYPE.FUNCTION_NPC:
				return false;
			case GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC:
				return false;
			}
		}
		return false;
	}

	// Token: 0x06003BDB RID: 15323 RVA: 0x001051C0 File Offset: 0x001033C0
	public static void GetBeAttackedList(GameDefine.CAMP_TYPE selfCamp, List<int> targetList)
	{
		targetList.Clear();
		if (selfCamp == GameDefine.CAMP_TYPE.PLAYER_1)
		{
			targetList.Add(1);
			targetList.Add(2);
			targetList.Add(3);
		}
		else if (selfCamp == GameDefine.CAMP_TYPE.PLAYER_2)
		{
			targetList.Add(0);
			targetList.Add(2);
			targetList.Add(3);
		}
		else if (selfCamp == GameDefine.CAMP_TYPE.NORMAL_NPC)
		{
			targetList.Add(0);
			targetList.Add(1);
		}
		else if (selfCamp == GameDefine.CAMP_TYPE.NPC_ATTACK_NPC)
		{
			targetList.Add(0);
			targetList.Add(1);
		}
		else if (selfCamp == GameDefine.CAMP_TYPE.STATIC_NPC)
		{
			targetList.Add(0);
			targetList.Add(1);
		}
		else if (selfCamp != GameDefine.CAMP_TYPE.FUNCTION_NPC)
		{
			if (selfCamp == GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC)
			{
				targetList.Add(2);
				targetList.Add(3);
			}
		}
	}

	// Token: 0x06003BDC RID: 15324 RVA: 0x00105284 File Offset: 0x00103484
	public static bool IsFirstClass(GameDefine.CAMP_TYPE source, GameDefine.CAMP_TYPE target)
	{
		if (source == GameDefine.CAMP_TYPE.NORMAL_NPC)
		{
			if (target == GameDefine.CAMP_TYPE.PLAYER_1 || target == GameDefine.CAMP_TYPE.PLAYER_2)
			{
				return true;
			}
		}
		else if (source == GameDefine.CAMP_TYPE.NPC_ATTACK_NPC && target == GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC)
		{
			return true;
		}
		return false;
	}

	// Token: 0x06003BDD RID: 15325 RVA: 0x001052C0 File Offset: 0x001034C0
	public static bool IsSameTeam(long id)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		return playerData.IsHaveTeam() && playerData.TeamInfo.GetTeamMemberByServerId(id) != null;
	}

	// Token: 0x06003BDE RID: 15326 RVA: 0x001052F8 File Offset: 0x001034F8
	public static void TipsCanSelectAttackPlayer(ObjOtherPlayer self, ObjOtherPlayer target)
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager.IsInSafeArea(target.Position) || sceneManager.IsInSafeArea(self.Position))
		{
			return;
		}
		if (sceneManager.IsCanAttackOtherPlayerScene())
		{
			if (self.AttributeData.PkMode == 1)
			{
				if (CampTool.IsSameTeam(target.ServerId))
				{
					NoticeLogic.AddNotifyData("#{200160}", true, false);
				}
			}
			else if (self.AttributeData.PkMode == 2)
			{
				if (CampTool.IsSameTeam(target.ServerId))
				{
					NoticeLogic.AddNotifyData("#{200160}", true, false);
				}
				else if (self.GuildId == target.GuildId && target.GuildId != -1L)
				{
					NoticeLogic.AddNotifyData("#{200159}", true, false);
				}
			}
		}
	}

	// Token: 0x06003BDF RID: 15327 RVA: 0x001053CC File Offset: 0x001035CC
	public static bool ISPlayerCanAttack(ObjOtherPlayer self, ObjOtherPlayer target)
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager.IsInSafeArea(target.Position) || sceneManager.IsInSafeArea(self.Position))
		{
			return false;
		}
		if (!sceneManager.IsCanAttackOtherPlayerScene())
		{
			return CampTool.CanAttack(self.AttributeData.Camp, target.AttributeData.Camp);
		}
		if (self.AttributeData.PkMode == 1)
		{
			if (sceneManager.IsGuildBattleScene())
			{
				if (self.GuildId != target.GuildId || target.GuildId == -1L || self.GuildId == -1L)
				{
					return true;
				}
			}
			else if (!CampTool.IsSameTeam(target.ServerId))
			{
				return true;
			}
		}
		else if (self.AttributeData.PkMode == 2 && (self.GuildId != target.GuildId || target.GuildId == -1L || self.GuildId == -1L))
		{
			if (sceneManager.IsGuildBattleScene())
			{
				return true;
			}
			if (!CampTool.IsSameTeam(target.ServerId))
			{
				return true;
			}
		}
		return false;
	}
}
