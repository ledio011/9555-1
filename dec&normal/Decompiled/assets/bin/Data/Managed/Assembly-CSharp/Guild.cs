using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200084C RID: 2124
public class Guild
{
	// Token: 0x060036B1 RID: 14001 RVA: 0x000E148C File Offset: 0x000DF68C
	public Guild()
	{
		this.ResetGuild();
	}

	// Token: 0x060036B2 RID: 14002 RVA: 0x000E14A4 File Offset: 0x000DF6A4
	public void Init(guild_info info, Dictionary<string, donate_record> reords)
	{
		if (info.HasGuildId)
		{
			if (this.mServerId != info.guildId)
			{
				this.mServerId = info.guildId;
				if (UIUpdateEvent.OnChangeGuild != null)
				{
					UIUpdateEvent.OnChangeGuild();
				}
			}
		}
		else if (this.mServerId != -1L)
		{
			this.mServerId = -1L;
			if (UIUpdateEvent.OnChangeGuild != null)
			{
				UIUpdateEvent.OnChangeGuild();
			}
		}
		this.mGuildName = ((!info.HasGuildName) ? string.Empty : info.guildName);
		this.mGuildLevel = ((!info.HasGuildLevel) ? -1 : ((int)info.guildLevel));
		this.mGuildChiefId = ((!info.HasGuildChiefId) ? -1L : info.guildChiefId);
		this.mGuildChiefName = ((!info.HasGuildChiefName) ? string.Empty : info.guildChiefName);
		this.mGuildExp = ((!info.HasGuildExp) ? -1 : ((int)info.guildExp));
		this.mGuildMaxApplyNum = ((!info.HasGuildApplyMaxNum) ? -1 : ((int)info.guildApplyMaxNum));
		this.mGuildCurApplyNum = ((!info.HasGuildApplyNum) ? -1 : ((int)info.guildApplyNum));
		this.mGuildMaxPlayer = (int)info.guildMaxPlayer;
		this.mGuildMemberNum = ((!info.HasGuildMemberNum) ? -1 : ((int)info.guildMemberNum - this.mGuildCurApplyNum));
		this.mGuildCombo = ((!info.HasGuildCombo) ? -1 : ((int)info.guildCombo));
		this.mNotice = ((!info.HasNotice) ? string.Empty : info.notice);
		this.mIsNeedAppro = (!info.HasIsNeedAppro || info.isNeedAppro);
		this.mViceNum = ((!info.HasViceNum) ? 0 : ((int)info.viceNum));
		this.mElderNum = ((!info.HasElderNum) ? 0 : ((int)info.elderNum));
		this.mPlayerJob = ((!info.HasPlayerJob) ? Guild_JOB.NO_JOB : ((Guild_JOB)info.playerJob));
		this.mGuildIcon = ((!info.HasIcon) ? 0 : ((int)info.icon));
		this.mDisactiveState = ((!info.HasDisactiveState) ? 0 : ((int)info.disactiveState));
		this.UpdateDonate(reords);
	}

	// Token: 0x060036B3 RID: 14003 RVA: 0x000E1720 File Offset: 0x000DF920
	public void UpdateSKill(Dictionary<long, guild_skill> skills)
	{
		this.mSkills = skills;
	}

	// Token: 0x060036B4 RID: 14004 RVA: 0x000E172C File Offset: 0x000DF92C
	public void UpdateSkillType(long type, long level)
	{
		if (this.mSkills != null && this.mSkills.ContainsKey(type))
		{
			this.mSkills[type].level = level;
		}
	}

	// Token: 0x17000F05 RID: 3845
	// (get) Token: 0x060036B5 RID: 14005 RVA: 0x000E1768 File Offset: 0x000DF968
	// (set) Token: 0x060036B6 RID: 14006 RVA: 0x000E1770 File Offset: 0x000DF970
	public Dictionary<long, guild_skill> GuildSkills
	{
		get
		{
			return this.mSkills;
		}
		set
		{
			this.mSkills = value;
		}
	}

	// Token: 0x060036B7 RID: 14007 RVA: 0x000E177C File Offset: 0x000DF97C
	public void UpDataGuildMemberList(Dictionary<long, guild_member_info> memberlsit)
	{
		if (memberlsit.Count <= 0)
		{
			Debug.Log("No Member!!!!");
			return;
		}
		if (this.mGuildMemberList == null)
		{
			this.mGuildMemberList = new Dictionary<long, GuildMember>();
		}
		else
		{
			this.mGuildMemberList.Clear();
		}
		foreach (KeyValuePair<long, guild_member_info> keyValuePair in memberlsit)
		{
			GuildMember guildMember = new GuildMember(keyValuePair.Value);
			this.mGuildMemberList.Add(guildMember.ServerId, guildMember);
		}
		this.UpdateTips();
	}

	// Token: 0x060036B8 RID: 14008 RVA: 0x000E1838 File Offset: 0x000DFA38
	public bool CanEditNotice()
	{
		return this.IsGuildValid() && (this.mPlayerJob == Guild_JOB.JOB_Chief || this.mPlayerJob == Guild_JOB.JOB_VicePresident);
	}

	// Token: 0x060036B9 RID: 14009 RVA: 0x000E186C File Offset: 0x000DFA6C
	public bool CanApprove()
	{
		return this.IsGuildValid() && (this.mPlayerJob == Guild_JOB.JOB_Chief || this.mPlayerJob == Guild_JOB.JOB_VicePresident || this.mPlayerJob == Guild_JOB.JOB_Elder);
	}

	// Token: 0x060036BA RID: 14010 RVA: 0x000E18AC File Offset: 0x000DFAAC
	public bool CanSetApprove()
	{
		return this.IsGuildValid() && (this.mPlayerJob == Guild_JOB.JOB_Chief || this.mPlayerJob == Guild_JOB.JOB_VicePresident);
	}

	// Token: 0x060036BB RID: 14011 RVA: 0x000E18E0 File Offset: 0x000DFAE0
	public bool CanChangeJob()
	{
		return this.IsGuildValid() && this.mPlayerJob == Guild_JOB.JOB_Chief;
	}

	// Token: 0x060036BC RID: 14012 RVA: 0x000E18F8 File Offset: 0x000DFAF8
	public bool CanChangMemmberJob(Guild_JOB job)
	{
		return true;
	}

	// Token: 0x060036BD RID: 14013 RVA: 0x000E18FC File Offset: 0x000DFAFC
	public bool CanChangeMemberJob(long id)
	{
		return this.mGuildMemberList.ContainsKey(id) && this.CanChangeJob() && this.mPlayerJob < this.mGuildMemberList[id].Job;
	}

	// Token: 0x060036BE RID: 14014 RVA: 0x000E1940 File Offset: 0x000DFB40
	public bool CanKickedMember(long id)
	{
		if (!this.IsGuildValid())
		{
			return false;
		}
		int memberJobByID = (int)this.getMemberJobByID(id);
		int num = (int)this.mPlayerJob;
		return num < memberJobByID && (this.mPlayerJob == Guild_JOB.JOB_Chief || this.mPlayerJob == Guild_JOB.JOB_VicePresident);
	}

	// Token: 0x060036BF RID: 14015 RVA: 0x000E198C File Offset: 0x000DFB8C
	public bool CanLeaveGuild()
	{
		return !this.IsGuildValid() || this.mPlayerJob != Guild_JOB.JOB_Chief;
	}

	// Token: 0x060036C0 RID: 14016 RVA: 0x000E19AC File Offset: 0x000DFBAC
	public bool CanStartActivity()
	{
		return this.mPlayerJob == Guild_JOB.JOB_Chief || this.mPlayerJob == Guild_JOB.JOB_VicePresident;
	}

	// Token: 0x060036C1 RID: 14017 RVA: 0x000E19C8 File Offset: 0x000DFBC8
	public void UpdateDonate(Dictionary<string, donate_record> records)
	{
		this.mRecords = records;
	}

	// Token: 0x060036C2 RID: 14018 RVA: 0x000E19D4 File Offset: 0x000DFBD4
	public void UseDonate(string id)
	{
		if (this.mRecords != null && this.mRecords.ContainsKey(id))
		{
			this.mRecords[id].DonateCount -= 1L;
		}
	}

	// Token: 0x17000F06 RID: 3846
	// (get) Token: 0x060036C3 RID: 14019 RVA: 0x000E1A18 File Offset: 0x000DFC18
	// (set) Token: 0x060036C4 RID: 14020 RVA: 0x000E1A20 File Offset: 0x000DFC20
	public Dictionary<string, donate_record> DonateRecord
	{
		get
		{
			return this.mRecords;
		}
		set
		{
			this.mRecords = value;
		}
	}

	// Token: 0x17000F07 RID: 3847
	// (get) Token: 0x060036C5 RID: 14021 RVA: 0x000E1A2C File Offset: 0x000DFC2C
	// (set) Token: 0x060036C6 RID: 14022 RVA: 0x000E1A34 File Offset: 0x000DFC34
	public long ServerId
	{
		get
		{
			return this.mServerId;
		}
		set
		{
			if (this.mServerId != value)
			{
				this.mServerId = value;
				if (UIUpdateEvent.OnChangeGuild != null)
				{
					UIUpdateEvent.OnChangeGuild();
				}
			}
		}
	}

	// Token: 0x17000F08 RID: 3848
	// (get) Token: 0x060036C7 RID: 14023 RVA: 0x000E1A60 File Offset: 0x000DFC60
	// (set) Token: 0x060036C8 RID: 14024 RVA: 0x000E1A68 File Offset: 0x000DFC68
	public string GuilName
	{
		get
		{
			return this.mGuildName;
		}
		set
		{
			this.mGuildName = value;
		}
	}

	// Token: 0x17000F09 RID: 3849
	// (get) Token: 0x060036C9 RID: 14025 RVA: 0x000E1A74 File Offset: 0x000DFC74
	// (set) Token: 0x060036CA RID: 14026 RVA: 0x000E1A7C File Offset: 0x000DFC7C
	public int GuilLevel
	{
		get
		{
			return this.mGuildLevel;
		}
		set
		{
			this.mGuildLevel = value;
		}
	}

	// Token: 0x060036CB RID: 14027 RVA: 0x000E1A88 File Offset: 0x000DFC88
	public bool CheckGuildLevel(int minlevel, int maxlevel)
	{
		return this.GuilLevel >= minlevel && this.GuilLevel <= maxlevel;
	}

	// Token: 0x17000F0A RID: 3850
	// (get) Token: 0x060036CC RID: 14028 RVA: 0x000E1AA8 File Offset: 0x000DFCA8
	// (set) Token: 0x060036CD RID: 14029 RVA: 0x000E1AB0 File Offset: 0x000DFCB0
	public long GuildChiefId
	{
		get
		{
			return this.mGuildChiefId;
		}
		set
		{
			this.mGuildChiefId = value;
		}
	}

	// Token: 0x17000F0B RID: 3851
	// (get) Token: 0x060036CE RID: 14030 RVA: 0x000E1ABC File Offset: 0x000DFCBC
	// (set) Token: 0x060036CF RID: 14031 RVA: 0x000E1AC4 File Offset: 0x000DFCC4
	public string GuildChiefName
	{
		get
		{
			return this.mGuildChiefName;
		}
		set
		{
			this.mGuildChiefName = value;
		}
	}

	// Token: 0x17000F0C RID: 3852
	// (get) Token: 0x060036D0 RID: 14032 RVA: 0x000E1AD0 File Offset: 0x000DFCD0
	// (set) Token: 0x060036D1 RID: 14033 RVA: 0x000E1AD8 File Offset: 0x000DFCD8
	public int GuildMaxPlayer
	{
		get
		{
			return this.mGuildMaxPlayer;
		}
		set
		{
			this.mGuildMaxPlayer = value;
		}
	}

	// Token: 0x17000F0D RID: 3853
	// (get) Token: 0x060036D2 RID: 14034 RVA: 0x000E1AE4 File Offset: 0x000DFCE4
	// (set) Token: 0x060036D3 RID: 14035 RVA: 0x000E1AEC File Offset: 0x000DFCEC
	public int GuildExp
	{
		get
		{
			return this.mGuildExp;
		}
		set
		{
			this.mGuildExp = value;
		}
	}

	// Token: 0x17000F0E RID: 3854
	// (get) Token: 0x060036D4 RID: 14036 RVA: 0x000E1AF8 File Offset: 0x000DFCF8
	// (set) Token: 0x060036D5 RID: 14037 RVA: 0x000E1B00 File Offset: 0x000DFD00
	public string GuildNotice
	{
		get
		{
			return this.mGuildNotice;
		}
		set
		{
			this.mGuildNotice = value;
		}
	}

	// Token: 0x17000F0F RID: 3855
	// (get) Token: 0x060036D6 RID: 14038 RVA: 0x000E1B0C File Offset: 0x000DFD0C
	// (set) Token: 0x060036D7 RID: 14039 RVA: 0x000E1B14 File Offset: 0x000DFD14
	public int GuildIcon
	{
		get
		{
			return this.mGuildIcon;
		}
		set
		{
			this.mGuildIcon = value;
		}
	}

	// Token: 0x17000F10 RID: 3856
	// (get) Token: 0x060036D8 RID: 14040 RVA: 0x000E1B20 File Offset: 0x000DFD20
	// (set) Token: 0x060036D9 RID: 14041 RVA: 0x000E1B28 File Offset: 0x000DFD28
	public int GuildAllContribute
	{
		get
		{
			return this.mGuildAllContribute;
		}
		set
		{
			this.mGuildAllContribute = value;
		}
	}

	// Token: 0x060036DA RID: 14042 RVA: 0x000E1B34 File Offset: 0x000DFD34
	public void UpdateAllContribute(int contribute)
	{
		this.mGuildAllContribute = contribute;
	}

	// Token: 0x17000F11 RID: 3857
	// (get) Token: 0x060036DB RID: 14043 RVA: 0x000E1B40 File Offset: 0x000DFD40
	public Dictionary<long, GuildMember> GuildMemberList
	{
		get
		{
			return this.mGuildMemberList;
		}
	}

	// Token: 0x17000F12 RID: 3858
	// (get) Token: 0x060036DC RID: 14044 RVA: 0x000E1B48 File Offset: 0x000DFD48
	// (set) Token: 0x060036DD RID: 14045 RVA: 0x000E1B50 File Offset: 0x000DFD50
	public int GuildMaxApplyNum
	{
		get
		{
			return this.mGuildMaxApplyNum;
		}
		set
		{
			this.mGuildMaxApplyNum = value;
		}
	}

	// Token: 0x17000F13 RID: 3859
	// (get) Token: 0x060036DE RID: 14046 RVA: 0x000E1B5C File Offset: 0x000DFD5C
	// (set) Token: 0x060036DF RID: 14047 RVA: 0x000E1B64 File Offset: 0x000DFD64
	public int GuildCurApplyNum
	{
		get
		{
			return this.mGuildCurApplyNum;
		}
		set
		{
			this.mGuildCurApplyNum = value;
		}
	}

	// Token: 0x17000F14 RID: 3860
	// (get) Token: 0x060036E0 RID: 14048 RVA: 0x000E1B70 File Offset: 0x000DFD70
	// (set) Token: 0x060036E1 RID: 14049 RVA: 0x000E1B78 File Offset: 0x000DFD78
	public int GuildMemberNum
	{
		get
		{
			return this.mGuildMemberNum;
		}
		set
		{
			this.mGuildMemberNum = value;
		}
	}

	// Token: 0x17000F15 RID: 3861
	// (get) Token: 0x060036E2 RID: 14050 RVA: 0x000E1B84 File Offset: 0x000DFD84
	// (set) Token: 0x060036E3 RID: 14051 RVA: 0x000E1B8C File Offset: 0x000DFD8C
	public int GuildCombo
	{
		get
		{
			return this.mGuildCombo;
		}
		set
		{
			this.mGuildCombo = value;
		}
	}

	// Token: 0x17000F16 RID: 3862
	// (get) Token: 0x060036E4 RID: 14052 RVA: 0x000E1B98 File Offset: 0x000DFD98
	// (set) Token: 0x060036E5 RID: 14053 RVA: 0x000E1BA0 File Offset: 0x000DFDA0
	public string Notice
	{
		get
		{
			return this.mNotice;
		}
		set
		{
			this.mNotice = value;
		}
	}

	// Token: 0x17000F17 RID: 3863
	// (get) Token: 0x060036E6 RID: 14054 RVA: 0x000E1BAC File Offset: 0x000DFDAC
	// (set) Token: 0x060036E7 RID: 14055 RVA: 0x000E1BB4 File Offset: 0x000DFDB4
	public bool IsNeedAppro
	{
		get
		{
			return this.mIsNeedAppro;
		}
		set
		{
			this.mIsNeedAppro = value;
		}
	}

	// Token: 0x17000F18 RID: 3864
	// (get) Token: 0x060036E8 RID: 14056 RVA: 0x000E1BC0 File Offset: 0x000DFDC0
	// (set) Token: 0x060036E9 RID: 14057 RVA: 0x000E1BC8 File Offset: 0x000DFDC8
	public int DisactiveState
	{
		get
		{
			return this.mDisactiveState;
		}
		set
		{
			this.mDisactiveState = value;
		}
	}

	// Token: 0x17000F19 RID: 3865
	// (get) Token: 0x060036EA RID: 14058 RVA: 0x000E1BD4 File Offset: 0x000DFDD4
	// (set) Token: 0x060036EB RID: 14059 RVA: 0x000E1BDC File Offset: 0x000DFDDC
	public Guild_JOB PlayerJob
	{
		get
		{
			return this.mPlayerJob;
		}
		set
		{
			this.mPlayerJob = value;
		}
	}

	// Token: 0x17000F1A RID: 3866
	// (get) Token: 0x060036EC RID: 14060 RVA: 0x000E1BE8 File Offset: 0x000DFDE8
	// (set) Token: 0x060036ED RID: 14061 RVA: 0x000E1BF0 File Offset: 0x000DFDF0
	public int ViceNum
	{
		get
		{
			return this.mViceNum;
		}
		set
		{
			this.mViceNum = value;
		}
	}

	// Token: 0x17000F1B RID: 3867
	// (get) Token: 0x060036EE RID: 14062 RVA: 0x000E1BFC File Offset: 0x000DFDFC
	// (set) Token: 0x060036EF RID: 14063 RVA: 0x000E1C04 File Offset: 0x000DFE04
	public int ElderNum
	{
		get
		{
			return this.mElderNum;
		}
		set
		{
			this.mElderNum = value;
		}
	}

	// Token: 0x060036F0 RID: 14064 RVA: 0x000E1C10 File Offset: 0x000DFE10
	public GuildMember getChief()
	{
		return this.mGuildMemberList[this.mGuildChiefId];
	}

	// Token: 0x060036F1 RID: 14065 RVA: 0x000E1C24 File Offset: 0x000DFE24
	public bool IsGuildValid()
	{
		return this.mServerId != -1L;
	}

	// Token: 0x060036F2 RID: 14066 RVA: 0x000E1C34 File Offset: 0x000DFE34
	public void ResetGuild()
	{
		if (this.mServerId != -1L)
		{
			this.mServerId = -1L;
			if (UIUpdateEvent.OnChangeGuild != null)
			{
				UIUpdateEvent.OnChangeGuild();
			}
		}
		this.mGuildName = string.Empty;
		this.mGuildLevel = -1;
		this.mGuildChiefId = -1L;
		this.mGuildExp = -1;
		this.mGuildMaxApplyNum = -1;
		this.mGuildCurApplyNum = -1;
		this.mGuildMemberNum = -1;
		this.mGuildCombo = -1;
		this.mElderNum = 0;
		this.mViceNum = 0;
		if (this.mGuildMemberList != null)
		{
			this.mGuildMemberList.Clear();
		}
		else
		{
			this.mGuildMemberList = new Dictionary<long, GuildMember>();
			this.mGuildMemberList.Clear();
		}
		this.mDisactiveState = 0;
	}

	// Token: 0x060036F3 RID: 14067 RVA: 0x000E1CF0 File Offset: 0x000DFEF0
	public void UpdateManageCount(Guild_JOB job1, Guild_JOB job2)
	{
		if (job1 == Guild_JOB.JOB_VicePresident)
		{
			this.mViceNum--;
		}
		if (job1 == Guild_JOB.JOB_Elder)
		{
			this.mElderNum--;
		}
		if (job2 == Guild_JOB.JOB_VicePresident)
		{
			this.mViceNum++;
		}
		if (job2 == Guild_JOB.JOB_Elder)
		{
			this.mElderNum++;
		}
	}

	// Token: 0x060036F4 RID: 14068 RVA: 0x000E1D54 File Offset: 0x000DFF54
	public void setMemberJob(long id, Guild_JOB type)
	{
		if (this.mGuildMemberList.ContainsKey(id))
		{
			this.UpdateManageCount(this.mGuildMemberList[id].Job, type);
			this.mGuildMemberList[id].Job = type;
		}
		this.UpdateTips();
	}

	// Token: 0x060036F5 RID: 14069 RVA: 0x000E1DA4 File Offset: 0x000DFFA4
	public GuildMember GetMemberInfoById(long ID)
	{
		if (this.mGuildMemberList.ContainsKey(ID))
		{
			return this.mGuildMemberList[ID];
		}
		return null;
	}

	// Token: 0x060036F6 RID: 14070 RVA: 0x000E1DC8 File Offset: 0x000DFFC8
	public string getMemberName(long ID)
	{
		if (this.mGuildMemberList.ContainsKey(ID))
		{
			return this.mGuildMemberList[ID].MemberName;
		}
		return null;
	}

	// Token: 0x060036F7 RID: 14071 RVA: 0x000E1DFC File Offset: 0x000DFFFC
	public Guild_JOB getMemberJobByID(long ID)
	{
		if (this.mGuildMemberList.ContainsKey(ID))
		{
			return this.mGuildMemberList[ID].Job;
		}
		return Guild_JOB.NO_JOB;
	}

	// Token: 0x060036F8 RID: 14072 RVA: 0x000E1E30 File Offset: 0x000E0030
	public bool isHasMember(long ID)
	{
		return this.mGuildMemberList.ContainsKey(ID);
	}

	// Token: 0x060036F9 RID: 14073 RVA: 0x000E1E40 File Offset: 0x000E0040
	public void SetChiefInfoByJob(int job)
	{
		if (job == 0)
		{
			this.mGuildChiefId = Singleton<ObjManager>.Instance.MainPlayer.ServerId;
		}
	}

	// Token: 0x060036FA RID: 14074 RVA: 0x000E1E60 File Offset: 0x000E0060
	public void RemoveByID(long ID)
	{
		if (this.isHasMember(ID))
		{
			this.mGuildMemberList.Remove(ID);
		}
		this.UpdateTips();
	}

	// Token: 0x060036FB RID: 14075 RVA: 0x000E1E84 File Offset: 0x000E0084
	public void AddNewMember(guild_member_info info)
	{
		if (!this.isHasMember(info.characterId))
		{
			GuildMember guildMember = new GuildMember(info);
			this.GuildMemberList.Add(guildMember.ServerId, guildMember);
		}
		this.UpdateTips();
	}

	// Token: 0x060036FC RID: 14076 RVA: 0x000E1EC4 File Offset: 0x000E00C4
	public bool isCanApply()
	{
		return this.mGuildCurApplyNum < this.mGuildMaxApplyNum;
	}

	// Token: 0x060036FD RID: 14077 RVA: 0x000E1EDC File Offset: 0x000E00DC
	public void UpdateTips()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.CheckTips(this.IsHaveGuildTips(), GameDefine.TIPS_TYPE.GUILD);
		}
		if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.RefershTips();
		}
	}

	// Token: 0x060036FE RID: 14078 RVA: 0x000E1F2C File Offset: 0x000E012C
	public bool IsHaveGuildTips()
	{
		if (!this.IsGuildValid())
		{
			return false;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		return this.IsHaveNewApply() || playerData.ActivityData.IsHaveGuildActTips();
	}

	// Token: 0x060036FF RID: 14079 RVA: 0x000E1F6C File Offset: 0x000E016C
	public bool IsHaveNewApply()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			return false;
		}
		if (!this.CanApprove())
		{
			return false;
		}
		List<GuildMember> list = new List<GuildMember>(this.mGuildMemberList.Values);
		if (list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Job == Guild_JOB.JOB_Candidate)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04002411 RID: 9233
	private Dictionary<long, guild_skill> mSkills;

	// Token: 0x04002412 RID: 9234
	private Dictionary<string, donate_record> mRecords;

	// Token: 0x04002413 RID: 9235
	private long mServerId = -1L;

	// Token: 0x04002414 RID: 9236
	private string mGuildName;

	// Token: 0x04002415 RID: 9237
	private int mGuildLevel;

	// Token: 0x04002416 RID: 9238
	private long mGuildChiefId;

	// Token: 0x04002417 RID: 9239
	private string mGuildChiefName;

	// Token: 0x04002418 RID: 9240
	private int mGuildMaxPlayer;

	// Token: 0x04002419 RID: 9241
	private int mGuildExp;

	// Token: 0x0400241A RID: 9242
	private string mGuildNotice;

	// Token: 0x0400241B RID: 9243
	private int mGuildIcon;

	// Token: 0x0400241C RID: 9244
	private int mGuildAllContribute;

	// Token: 0x0400241D RID: 9245
	private Dictionary<long, GuildMember> mGuildMemberList;

	// Token: 0x0400241E RID: 9246
	private int mGuildMaxApplyNum;

	// Token: 0x0400241F RID: 9247
	private int mGuildCurApplyNum;

	// Token: 0x04002420 RID: 9248
	private int mGuildMemberNum;

	// Token: 0x04002421 RID: 9249
	private int mGuildCombo;

	// Token: 0x04002422 RID: 9250
	public string mNotice;

	// Token: 0x04002423 RID: 9251
	public bool mIsNeedAppro;

	// Token: 0x04002424 RID: 9252
	private int mDisactiveState;

	// Token: 0x04002425 RID: 9253
	private Guild_JOB mPlayerJob;

	// Token: 0x04002426 RID: 9254
	public int mViceNum;

	// Token: 0x04002427 RID: 9255
	public int mElderNum;
}
