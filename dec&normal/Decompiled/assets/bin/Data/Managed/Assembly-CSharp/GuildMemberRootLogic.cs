using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200085D RID: 2141
public class GuildMemberRootLogic : SingletonUnity<GuildMemberRootLogic>
{
	// Token: 0x06003778 RID: 14200 RVA: 0x000E4420 File Offset: 0x000E2620
	protected override void Awake()
	{
		base.Awake();
		UIWrapContentNew grid = this.Grid;
		grid.onInitializeItem = (UIWrapContentNew.OnInitializeItem)Delegate.Combine(grid.onInitializeItem, new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem));
	}

	// Token: 0x06003779 RID: 14201 RVA: 0x000E4450 File Offset: 0x000E2650
	public void EnableReset()
	{
		for (int i = 0; i < this.GuildMemberItemList.Count; i++)
		{
			NGUITools.SetActive(this.GuildMemberItemList[i].gameObject, false);
		}
		UnityVersionUtil.SetActiveRecursive(this.Tips, false);
		UnityVersionUtil.SetActiveRecursive(this.ApplyListBtn, false);
		UnityVersionUtil.SetActiveRecursive(this.RecruitBtn, false);
		UnityVersionUtil.SetActiveRecursive(this.NeedAppro, false);
		UnityVersionUtil.SetActiveRecursive(this.ApproBtn, false);
	}

	// Token: 0x0600377A RID: 14202 RVA: 0x000E44CC File Offset: 0x000E26CC
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		GuildMemberItemLOgic item = this.GuildMemberItemList[index];
		this.Reset(item, Mathf.Abs(realIndex), this.GuildMemberList.Count);
	}

	// Token: 0x0600377B RID: 14203 RVA: 0x000E4500 File Offset: 0x000E2700
	private void Reset(GuildMemberItemLOgic item, int realIndex, int maxLine)
	{
		if (realIndex >= this.GuildMemberList.Count)
		{
			realIndex = this.GuildMemberList.Count - 1;
		}
		if (realIndex < 0)
		{
			return;
		}
		item.InitGuildMemberInfo(this.GuildMemberList[realIndex]);
	}

	// Token: 0x0600377C RID: 14204 RVA: 0x000E4548 File Offset: 0x000E2748
	private void UpdateBtn()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		UnityVersionUtil.SetActiveRecursive(this.ApplyListBtn, playerData.PlayerGuild.CanApprove());
		UnityVersionUtil.SetActiveRecursive(this.RecruitBtn, playerData.PlayerGuild.CanApprove());
		UnityVersionUtil.SetActiveRecursive(this.NeedAppro, playerData.PlayerGuild.CanSetApprove());
		if (playerData.PlayerGuild.CanSetApprove())
		{
			UnityVersionUtil.SetActiveRecursive(this.ApproBtn, !playerData.PlayerGuild.IsNeedAppro);
		}
		if (this.ApplyList.Count > 0 && playerData.PlayerGuild.CanApprove())
		{
			UnityVersionUtil.SetActiveRecursive(this.Tips, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.Tips, false);
		}
	}

	// Token: 0x0600377D RID: 14205 RVA: 0x000E460C File Offset: 0x000E280C
	public void UpdateGuildMemberItemList(Dictionary<long, GuildMember> list)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.ApplyList.Clear();
		this.GuildMemberList.Clear();
		List<GuildMember> list2 = new List<GuildMember>(list.Values);
		list2.Sort(delegate(GuildMember x, GuildMember y)
		{
			if (x.mState == y.mState)
			{
				return x.Job - y.Job;
			}
			return y.State - x.mState;
		});
		int num = list.Count - this.GuildMemberItemList.Count;
		int num2 = 0;
		for (int i = 0; i < list2.Count; i++)
		{
			GuildMember guildMember = list2[i];
			if (guildMember.Job != Guild_JOB.JOB_Candidate)
			{
				if (num2 < this.GuildMemberItemList.Count)
				{
					this.GuildMemberItemList[num2++].InitGuildMemberInfo(guildMember);
				}
				this.GuildMemberList.Add(guildMember);
			}
			else
			{
				this.ApplyList.Add(guildMember);
			}
		}
		this.UpdateBtn();
		for (int j = 0; j < this.GuildMemberItemList.Count; j++)
		{
			NGUITools.SetActive(this.GuildMemberItemList[j].gameObject, j < num2);
		}
		this.Grid.maxIndex = 0;
		this.Grid.minIndex = -1 * (this.GuildMemberList.Count - 1);
		this.BottomWidget.height = this.GuildMemberList.Count * this.GuildMemberItemList[0].CellHeight;
		this.Grid.SortBasedOnScrollMovement();
		this.ScrollView.ResetPosition();
		this.UpdateApplyList();
	}

	// Token: 0x0600377E RID: 14206 RVA: 0x000E47A4 File Offset: 0x000E29A4
	public void UpdateApplyList()
	{
		if (SingletonUnity<GuildApplyListLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildApplyListLogic>.Instance.gameObject))
		{
			SingletonUnity<GuildApplyListLogic>.Instance.UpdataApplyList(this.ApplyList);
		}
	}

	// Token: 0x0600377F RID: 14207 RVA: 0x000E47E0 File Offset: 0x000E29E0
	public void OnClickApplyListBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildApplyListLogicRoot, delegate
		{
			SingletonUnity<GuildApplyListLogic>.Instance.UpdataApplyList(this.ApplyList);
		}, null);
	}

	// Token: 0x06003780 RID: 14208 RVA: 0x000E4800 File Offset: 0x000E2A00
	public void OnClickRecruitMember()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		SingletonUnity<NewGuildUIRootLogic>.Instance.OnClickCloseBtn();
		ChatUIRootLogic.ResetLinkChat(GameDefine.CHAT_LINK_TYPE.GUILD, GameDefine.CHAT_CHANNEL_TYPE.WORLD, playerData.PlayerGuild);
	}

	// Token: 0x06003781 RID: 14209 RVA: 0x000E4830 File Offset: 0x000E2A30
	public void OnClickQuit()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (!playerData.PlayerGuild.CanLeaveGuild())
		{
			MessageBoxLogic.OpenOKCancelBox("#{200111}", "#{100127}", delegate
			{
				Singleton<ObjManager>.Instance.MainPlayer.LeaveGuild();
			}, null, null, null);
			return;
		}
		MessageBoxLogic.OpenOKCancelBox("#{200114}", "#{100127}", delegate
		{
			Singleton<ObjManager>.Instance.MainPlayer.LeaveGuild();
		}, null, null, null);
	}

	// Token: 0x06003782 RID: 14210 RVA: 0x000E48B8 File Offset: 0x000E2AB8
	public void ShowGuildLogs(List<string> lists)
	{
		if (SingletonUnity<GuildLogUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildLogUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<GuildLogUIRootLogic>.Instance.UpdataLogList(lists);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GuildLogUIRootLogic, delegate
			{
				SingletonUnity<GuildLogUIRootLogic>.Instance.UpdataLogList(lists);
			}, null);
		}
	}

	// Token: 0x06003783 RID: 14211 RVA: 0x000E4924 File Offset: 0x000E2B24
	public void ChangeGuildNeedAppro()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		bool flag = !playerData.PlayerGuild.IsNeedAppro;
		playerData.PlayerGuild.IsNeedAppro = flag;
		UnityVersionUtil.SetActiveRecursive(this.ApproBtn, !flag);
		Singleton<ObjManager>.Instance.MainPlayer.SetGuildNeedAppro(flag);
	}

	// Token: 0x06003784 RID: 14212 RVA: 0x000E4978 File Offset: 0x000E2B78
	public void OnClickLogBtn()
	{
		Singleton<ObjManager>.Instance.MainPlayer.LookAtLog();
	}

	// Token: 0x040024A3 RID: 9379
	public List<GuildMemberItemLOgic> GuildMemberItemList = new List<GuildMemberItemLOgic>();

	// Token: 0x040024A4 RID: 9380
	private List<GuildMember> GuildMemberList = new List<GuildMember>();

	// Token: 0x040024A5 RID: 9381
	public GameObject LogBtn;

	// Token: 0x040024A6 RID: 9382
	public GameObject ApplyListBtn;

	// Token: 0x040024A7 RID: 9383
	public GuildApplyListLogic ApplyListLogic;

	// Token: 0x040024A8 RID: 9384
	public GameObject NeedAppro;

	// Token: 0x040024A9 RID: 9385
	public GameObject ApproBtn;

	// Token: 0x040024AA RID: 9386
	public GameObject RecruitBtn;

	// Token: 0x040024AB RID: 9387
	public GameObject Tips;

	// Token: 0x040024AC RID: 9388
	public List<GuildMember> ApplyList = new List<GuildMember>();

	// Token: 0x040024AD RID: 9389
	public UIScrollView ScrollView;

	// Token: 0x040024AE RID: 9390
	public UIWrapContentNew Grid;

	// Token: 0x040024AF RID: 9391
	public UIWidget BottomWidget;
}
