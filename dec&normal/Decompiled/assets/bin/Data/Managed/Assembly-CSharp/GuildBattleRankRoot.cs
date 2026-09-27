using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A11 RID: 2577
public class GuildBattleRankRoot : SingletonUnity<GuildBattleRankRoot>
{
	// Token: 0x06004A1C RID: 18972 RVA: 0x00182434 File Offset: 0x00180634
	public void EnableReset()
	{
		for (int i = 0; i < this.itemlines.Count; i++)
		{
			NGUITools.SetActive(this.itemlines[i].gameObject, false);
		}
	}

	// Token: 0x06004A1D RID: 18973 RVA: 0x00182474 File Offset: 0x00180674
	public void ResershInfo(ret_guild_battle_rank.request request)
	{
		this.TopList.Clear();
		if (request.HasGuild_info)
		{
			this.TopList = request.guild_info;
		}
		int num = Mathf.Min(this.TopList.Count, this.minNum) - this.itemlines.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.itemlines[0].gameObject) as GameObject;
				GuildRankItemLogic component = gameObject.GetComponent<GuildRankItemLogic>();
				gameObject.name = string.Format("{0:D2}", this.itemlines.Count);
				gameObject.transform.parent = this.parentGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				this.itemlines.Add(component);
			}
		}
		for (int j = 0; j < this.itemlines.Count; j++)
		{
			if (j < this.TopList.Count)
			{
				NGUITools.SetActive(this.itemlines[j].gameObject, true);
				this.itemlines[j].ResetInfo(this.TopList[j], j);
			}
			else
			{
				NGUITools.SetActive(this.itemlines[j].gameObject, false);
			}
		}
		this.parentGrid.Reposition();
	}

	// Token: 0x06004A1E RID: 18974 RVA: 0x001825F8 File Offset: 0x001807F8
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GuildBattleRankRoot);
	}

	// Token: 0x0400377C RID: 14204
	public List<GuildRankItemLogic> itemlines;

	// Token: 0x0400377D RID: 14205
	public int minNum = 8;

	// Token: 0x0400377E RID: 14206
	private List<guild_info> TopList = new List<guild_info>();

	// Token: 0x0400377F RID: 14207
	public UIGrid parentGrid;
}
