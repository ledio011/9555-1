using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A10 RID: 2576
public class GuildBattleMemberRootLogic : SingletonUnity<GuildBattleMemberRootLogic>
{
	// Token: 0x06004A10 RID: 18960 RVA: 0x00181B04 File Offset: 0x0017FD04
	protected override void Awake()
	{
		base.Awake();
		this.uiWrapContent.enabled = false;
		UIWrapContentNew uiwrapContentNew = this.uiWrapContent;
		uiwrapContentNew.onInitializeItem = (UIWrapContentNew.OnInitializeItem)Delegate.Combine(uiwrapContentNew.onInitializeItem, new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem));
	}

	// Token: 0x06004A11 RID: 18961 RVA: 0x00181B4C File Offset: 0x0017FD4C
	public void EnableReset()
	{
		for (int i = 0; i < this.GuildMemberLines.Count; i++)
		{
			NGUITools.SetActive(this.GuildMemberLines[i].gameObject, false);
		}
		this.NumLabel.text = string.Empty;
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.isGuildChief())
		{
			NGUITools.SetActive(this.ConfirmBtnSp.gameObject, true);
		}
		else
		{
			NGUITools.SetActive(this.ConfirmBtnSp.gameObject, false);
		}
	}

	// Token: 0x06004A12 RID: 18962 RVA: 0x00181BD8 File Offset: 0x0017FDD8
	private void InitGuildMemberList()
	{
		this.GuildMemberList = new List<guild_member_info>();
		long serverId = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.ServerId;
		int num = 1;
		for (int i = 0; i < 15; i++)
		{
			this.GuildMemberList.Add(new guild_member_info());
			this.GuildMemberList[i].guildId = serverId;
			this.GuildMemberList[i].characterId = UUID.GenUUID();
			this.GuildMemberList[i].combValue = (long)Random.Range(10000, 20000);
			this.GuildMemberList[i].name = string.Empty + i;
			this.GuildMemberList[i].profession = (long)Random.Range(0, 3);
			this.GuildMemberList[i].job = (long)Random.Range(0, 2);
			this.GuildMemberList[i].battle = ((num >= 10) ? 0L : 1L);
			this.GuildMemberList[i].state = (long)Random.Range(0, 2);
			num++;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.GuildMemberList.Insert(0, new guild_member_info());
		this.GuildMemberList[0].guildId = serverId;
		this.GuildMemberList[0].characterId = PlayerData.MainPlayerServerId;
		this.GuildMemberList[0].combValue = (long)playerData.MainPlayerAttrData.ComboValue;
		this.GuildMemberList[0].name = playerData.MainPlayerAttrData.Name;
		this.GuildMemberList[0].profession = (long)playerData.Profession;
		this.GuildMemberList[0].job = (long)playerData.PlayerGuild.PlayerJob;
		this.GuildMemberList[0].battle = 1L;
		this.GuildMemberList[0].state = 1L;
	}

	// Token: 0x06004A13 RID: 18963 RVA: 0x00181DDC File Offset: 0x0017FFDC
	public void RefershInfo(ret_guild_battle_member.request request)
	{
		this.SelectList.Clear();
		this.GuildMemberList.Clear();
		if (request.HasGuild_member_info)
		{
			this.GuildMemberList = request.guild_member_info;
		}
		this.GuildMemberList.Sort((guild_member_info x, guild_member_info y) => (int)(y.combValue - x.combValue));
		for (int i = 0; i < this.GuildMemberList.Count; i++)
		{
			if (this.GuildMemberList[i].job == 0L)
			{
				guild_member_info guild_member_info = this.GuildMemberList[i];
				this.GuildMemberList.RemoveAt(i);
				this.GuildMemberList.Insert(0, guild_member_info);
				break;
			}
		}
		int num = Mathf.Min(this.GuildMemberList.Count, this.lineMinCount) - this.GuildMemberLines.Count;
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Object.Instantiate(this.GuildMemberLines[0].gameObject) as GameObject;
				GuildBattleMemberLine component = gameObject.GetComponent<GuildBattleMemberLine>();
				gameObject.name = string.Format("{0:D2}", this.GuildMemberLines.Count);
				gameObject.transform.parent = this.uiWrapContent.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				this.GuildMemberLines.Add(component);
			}
		}
		for (int k = 0; k < this.GuildMemberLines.Count; k++)
		{
			if (k < this.GuildMemberList.Count)
			{
				NGUITools.SetActive(this.GuildMemberLines[k].gameObject, true);
				this.GuildMemberLines[k].UpdateInfo(this.GuildMemberList[k], k);
			}
			else
			{
				NGUITools.SetActive(this.GuildMemberLines[k].gameObject, false);
			}
		}
		this.uiWrapContent.minIndex = 1 - this.GuildMemberList.Count;
		this.WrapContentBottomWidget.height = this.GuildMemberList.Count * this.uiWrapContent.itemSize;
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.uiScrollView.ResetPosition();
		this.uiWrapContent.enabled = true;
		for (int l = 0; l < this.GuildMemberList.Count; l++)
		{
			if (this.GuildMemberList[l].battle == 1L)
			{
				this.SelectList.Add(this.GuildMemberList[l].characterId);
			}
		}
		if (SingletonUnity<GuildBattleRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildBattleRootLogic>.Instance.gameObject))
		{
			this.curGuildBattleData = DataManager.GetGuildBattleDataById(SingletonUnity<GuildBattleRootLogic>.Instance.CurBattleInfo.ID);
		}
		this.NumLabel.text = string.Concat(new object[]
		{
			StrDictionary.GetDictionaryString("#{105047}", new object[0]),
			": ",
			this.SelectList.Count,
			"/",
			this.curGuildBattleData.MaxPlayNum
		});
	}

	// Token: 0x06004A14 RID: 18964 RVA: 0x00182138 File Offset: 0x00180338
	public bool OnClickSelectAdd(long selectIndex)
	{
		if (this.SelectList.Count >= 10)
		{
			NoticeLogic.AddNotifyData("#{105073}", true, false);
			return false;
		}
		this.SelectList.Add(selectIndex);
		this.NumLabel.text = string.Concat(new object[]
		{
			StrDictionary.GetDictionaryString("#{105047}", new object[0]),
			": ",
			this.SelectList.Count,
			"/",
			this.curGuildBattleData.MaxPlayNum
		});
		return true;
	}

	// Token: 0x06004A15 RID: 18965 RVA: 0x001821D4 File Offset: 0x001803D4
	public void OnClickSelectDec(long selectIndex)
	{
		if (this.SelectList.Contains(selectIndex))
		{
			this.SelectList.Remove(selectIndex);
		}
		this.NumLabel.text = string.Concat(new object[]
		{
			StrDictionary.GetDictionaryString("#{105047}", new object[0]),
			": ",
			this.SelectList.Count,
			"/",
			this.curGuildBattleData.MaxPlayNum
		});
	}

	// Token: 0x06004A16 RID: 18966 RVA: 0x00182260 File Offset: 0x00180460
	public void OnClickConfirmBtn()
	{
		Guild playerGuild = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild;
		if (playerGuild.GuildMemberNum >= 10 && this.SelectList.Count < 10)
		{
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{105074}", new object[]
			{
				this.curGuildBattleData.MaxPlayNum
			}), true, false);
			return;
		}
		if (playerGuild.GuildMemberNum < 10 && this.SelectList.Count < playerGuild.GuildMemberNum)
		{
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{105074}", new object[]
			{
				this.curGuildBattleData.MaxPlayNum
			}), true, false);
			return;
		}
		if (!SingletonUnity<GuildBattleRootLogic>.Instance.IsPreparingState() || SingletonUnity<GuildBattleRootLogic>.Instance.CurBattleInfo.time - SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime() <= 0L)
		{
			NoticeLogic.AddNotifyData("#{105050}", true, false);
			return;
		}
		set_guild_battle_member.request request = new set_guild_battle_member.request();
		request.list = this.SelectList;
		NetLogic.GetInstance().Send<Protocol.set_guild_battle_member>(request, null);
		this.OnClickCloseBtn();
		NoticeLogic.AddNotifyData("#{105057}", true, false);
	}

	// Token: 0x06004A17 RID: 18967 RVA: 0x00182388 File Offset: 0x00180588
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleMemberRoot);
	}

	// Token: 0x06004A18 RID: 18968 RVA: 0x0018239C File Offset: 0x0018059C
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		if (index < this.GuildMemberLines.Count)
		{
			GuildBattleMemberLine itemLogic = this.GuildMemberLines[index];
			this.ResetItemLine(itemLogic, Mathf.Abs(realIndex));
		}
	}

	// Token: 0x06004A19 RID: 18969 RVA: 0x001823D4 File Offset: 0x001805D4
	private void ResetItemLine(GuildBattleMemberLine itemLogic, int idx)
	{
		if (idx < this.GuildMemberList.Count)
		{
			itemLogic.UpdateInfo(this.GuildMemberList[idx], idx);
		}
	}

	// Token: 0x04003771 RID: 14193
	public UIWrapContentNew uiWrapContent;

	// Token: 0x04003772 RID: 14194
	private int lineMinCount = 7;

	// Token: 0x04003773 RID: 14195
	public UIWidget WrapContentBottomWidget;

	// Token: 0x04003774 RID: 14196
	public UIScrollView uiScrollView;

	// Token: 0x04003775 RID: 14197
	private List<guild_member_info> GuildMemberList = new List<guild_member_info>();

	// Token: 0x04003776 RID: 14198
	public List<GuildBattleMemberLine> GuildMemberLines;

	// Token: 0x04003777 RID: 14199
	public UILabel NumLabel;

	// Token: 0x04003778 RID: 14200
	private List<long> SelectList = new List<long>();

	// Token: 0x04003779 RID: 14201
	public UISprite ConfirmBtnSp;

	// Token: 0x0400377A RID: 14202
	private GuildBattleData curGuildBattleData;
}
