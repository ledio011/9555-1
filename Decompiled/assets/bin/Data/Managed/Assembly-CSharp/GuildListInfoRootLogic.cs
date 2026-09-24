using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SprotoType;
using UnityEngine;

// Token: 0x02000855 RID: 2133
public class GuildListInfoRootLogic : SingletonUnity<GuildListInfoRootLogic>
{
	// Token: 0x0600373D RID: 14141 RVA: 0x000E32C0 File Offset: 0x000E14C0
	private void OnEnable()
	{
		for (int i = 0; i < this.GuildList.Count; i++)
		{
			NGUITools.SetActive(this.GuildList[i].gameObject, i < this.mCurGuildList.Count);
		}
	}

	// Token: 0x0600373E RID: 14142 RVA: 0x000E3310 File Offset: 0x000E1510
	public void PreResetGuildListInfo()
	{
		for (int i = 0; i < this.GuildList.Count; i++)
		{
			NGUITools.SetActive(this.GuildList[i].gameObject, false);
		}
	}

	// Token: 0x0600373F RID: 14143 RVA: 0x000E3350 File Offset: 0x000E1550
	public void UpdateGuildListInfo(Dictionary<long, guild_info> guildDic, int curPage, int maxPage)
	{
		if (guildDic == null)
		{
			guildDic = new Dictionary<long, guild_info>();
			guildDic.Clear();
		}
		List<guild_info> list = new List<guild_info>(guildDic.Values);
		list.Sort((guild_info preVal, guild_info nextVal) => (int)(preVal.createTime - nextVal.createTime));
		this.mCurGuildList.Clear();
		for (int i = 0; i < list.Count; i++)
		{
			GuildInfo guildInfo = new GuildInfo(list[i]);
			this.mCurGuildList.Add(guildInfo);
			if (this.mAllGuildDic.ContainsKey(guildInfo.ServerId))
			{
				this.mAllGuildDic[guildInfo.ServerId] = guildInfo;
			}
			else
			{
				this.mAllGuildList.Add(guildInfo);
				this.mAllGuildDic.Add(guildInfo.ServerId, guildInfo);
			}
		}
		this.mCurPage = curPage;
		this.mMaxPage = maxPage;
		if (this.mMaxPage > this.mLastUpdateTime.Count)
		{
			int num = this.mMaxPage - this.mLastUpdateTime.Count;
			for (int j = 0; j < num; j++)
			{
				this.mLastUpdateTime.Add(float.MinValue);
			}
		}
		this.mLastUpdateTime[this.mCurPage - 1] = Time.time;
		this.UpdateGuildLine(10 * (this.mCurPage - 1));
		this.mSearchResultFlag = false;
	}

	// Token: 0x06003740 RID: 14144 RVA: 0x000E34B4 File Offset: 0x000E16B4
	private void UpdateGuildLine(int startIndex)
	{
		int num = this.mCurGuildList.Count - this.GuildList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.GuildList[0].gameObject) as GameObject;
				gameObject.name = string.Format("GangListLineItem_{0}", this.GuildList.Count);
				this.GuildListGrid.AddChild(gameObject.transform);
				gameObject.transform.localScale = Vector3.one;
				this.GuildList.Add(gameObject.GetComponent<GuildListLineItemLogic>());
			}
		}
		for (int j = 0; j < this.GuildList.Count; j++)
		{
			NGUITools.SetActive(this.GuildList[j].gameObject, j < this.mCurGuildList.Count);
		}
		for (int k = 0; k < this.mCurGuildList.Count; k++)
		{
			this.GuildList[k].InitGuildInfo(this.mCurGuildList[k], startIndex + k + 1);
		}
		this.PageLabel.text = string.Format("{0}/{1}", this.mCurPage, this.mMaxPage);
		this.ScrollView.ResetPosition();
		this.GuildListGrid.Reposition();
	}

	// Token: 0x06003741 RID: 14145 RVA: 0x000E3628 File Offset: 0x000E1828
	private void UpdateGuildLine(List<long> indexList)
	{
		int num = this.mCurGuildList.Count - this.GuildList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.GuildList[0].gameObject) as GameObject;
				this.GuildListGrid.AddChild(gameObject.transform);
				gameObject.transform.localScale = Vector3.one;
				this.GuildList.Add(gameObject.GetComponent<GuildListLineItemLogic>());
			}
			this.GuildListGrid.Reposition();
		}
		for (int j = 0; j < this.GuildList.Count; j++)
		{
			NGUITools.SetActive(this.GuildList[j].gameObject, j < this.mCurGuildList.Count);
		}
		for (int k = 0; k < this.mCurGuildList.Count; k++)
		{
			this.GuildList[k].InitGuildInfo(this.mCurGuildList[k], (int)indexList[k]);
		}
		this.PageLabel.text = string.Format("{0}/{1}", this.mCurPage, this.mMaxPage);
		this.ScrollView.ResetPosition();
	}

	// Token: 0x06003742 RID: 14146 RVA: 0x000E3780 File Offset: 0x000E1980
	private void LocalUpdateGuildList(int page)
	{
		this.mCurPage = page;
		int num = 10 * (this.mCurPage - 1);
		int num2 = Mathf.Min(10 * this.mCurPage, this.mAllGuildList.Count);
		this.mCurGuildList.Clear();
		for (int i = num; i < num2; i++)
		{
			this.mCurGuildList.Add(this.mAllGuildList[i]);
		}
		this.UpdateGuildLine(10 * (this.mCurPage - 1));
		this.mSearchResultFlag = false;
	}

	// Token: 0x06003743 RID: 14147 RVA: 0x000E3808 File Offset: 0x000E1A08
	public void ShowSearchResult(GuildInfo result, int index)
	{
		this.mCurGuildList.Clear();
		this.mCurGuildList.Add(result);
		this.UpdateGuildLine(index);
		this.PageLabel.text = string.Format("1/1", new object[0]);
	}

	// Token: 0x06003744 RID: 14148 RVA: 0x000E3850 File Offset: 0x000E1A50
	public void OnClickNextPageBtn()
	{
		if (this.mCurPage < this.mMaxPage && !this.mSearchResultFlag)
		{
			if (Time.time - this.mLastUpdateTime[this.mCurPage] > 1000f)
			{
				WaitResponseUIRootLogic.OpenWaitBox(152, 10f, 0f, null);
				guild_req_list.request request = new guild_req_list.request();
				request.characterId = PlayerData.MainPlayerServerId;
				request.curPage = (long)(this.mCurPage + 1);
				NetLogic.GetInstance().Send<Protocol.guild_req_list>(request, null);
			}
			else
			{
				this.LocalUpdateGuildList(this.mCurPage + 1);
			}
			this.ScrollView.ResetPosition();
		}
	}

	// Token: 0x06003745 RID: 14149 RVA: 0x000E38FC File Offset: 0x000E1AFC
	public void OnClickPrePageBtn()
	{
		if (this.mCurPage > 1 && !this.mSearchResultFlag)
		{
			if (Time.time - this.mLastUpdateTime[this.mCurPage - 2] > 1000f)
			{
				WaitResponseUIRootLogic.OpenWaitBox(152, 10f, 0f, null);
				guild_req_list.request request = new guild_req_list.request();
				request.characterId = PlayerData.MainPlayerServerId;
				request.curPage = (long)(this.mCurPage - 1);
				NetLogic.GetInstance().Send<Protocol.guild_req_list>(request, null);
			}
			else
			{
				this.LocalUpdateGuildList(this.mCurPage - 1);
			}
			this.ScrollView.ResetPosition();
		}
	}

	// Token: 0x06003746 RID: 14150 RVA: 0x000E39A4 File Offset: 0x000E1BA4
	public void OnClickSearchBtn()
	{
		string text = this.SearchInput.value;
		this.SearchInput.value = string.Empty;
		text = text.Trim();
		if (string.IsNullOrEmpty(text))
		{
			if (this.mSearchResultFlag)
			{
				this.LocalUpdateGuildList(this.mCurPage);
				this.mSearchResultFlag = false;
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100794}", true, false);
			}
			return;
		}
		if (!this.StrIsTrue(text))
		{
			NoticeLogic.AddNotifyData("#{100793}", true, false);
			return;
		}
		this.mSearchResultFlag = true;
		search_guild.request request = new search_guild.request();
		request.name = text;
		NetLogic.GetInstance().Send<Protocol.search_guild>(request, null);
		WaitResponseUIRootLogic.OpenWaitBox(176, 10f, 0f, null);
	}

	// Token: 0x06003747 RID: 14151 RVA: 0x000E3A60 File Offset: 0x000E1C60
	private bool StrIsTrue(string str)
	{
		string text = "^[a-zA-Z0-9]{1}([a-zA-Z0-9]|[ _]){0,14}$";
		return Regex.IsMatch(str, text);
	}

	// Token: 0x06003748 RID: 14152 RVA: 0x000E3A84 File Offset: 0x000E1C84
	public void ShowSearchResult(List<guild_info> resultList, List<long> rankList)
	{
		this.mCurGuildList.Clear();
		for (int i = 0; i < resultList.Count; i++)
		{
			GuildInfo guildInfo = new GuildInfo(resultList[i]);
			this.mCurGuildList.Add(guildInfo);
		}
		this.UpdateGuildLine(rankList);
		this.PageLabel.text = string.Format("1/1", new object[0]);
	}

	// Token: 0x06003749 RID: 14153 RVA: 0x000E3AF0 File Offset: 0x000E1CF0
	public void DisableSearchFlag()
	{
		this.mSearchResultFlag = false;
	}

	// Token: 0x04002465 RID: 9317
	private const int PAGE_NUM = 10;

	// Token: 0x04002466 RID: 9318
	private const float UPDATE_INTERVAL = 1000f;

	// Token: 0x04002467 RID: 9319
	public List<GuildListLineItemLogic> GuildList = new List<GuildListLineItemLogic>();

	// Token: 0x04002468 RID: 9320
	public UIScrollView ScrollView;

	// Token: 0x04002469 RID: 9321
	public UIGrid GuildListGrid;

	// Token: 0x0400246A RID: 9322
	public UILabel PageLabel;

	// Token: 0x0400246B RID: 9323
	public UIInput SearchInput;

	// Token: 0x0400246C RID: 9324
	private int mCurPage;

	// Token: 0x0400246D RID: 9325
	private int mMaxPage;

	// Token: 0x0400246E RID: 9326
	private List<GuildInfo> mCurGuildList = new List<GuildInfo>();

	// Token: 0x0400246F RID: 9327
	private List<GuildInfo> mAllGuildList = new List<GuildInfo>();

	// Token: 0x04002470 RID: 9328
	private Dictionary<long, GuildInfo> mAllGuildDic = new Dictionary<long, GuildInfo>();

	// Token: 0x04002471 RID: 9329
	private List<float> mLastUpdateTime = new List<float>();

	// Token: 0x04002472 RID: 9330
	private bool mSearchResultFlag;
}
