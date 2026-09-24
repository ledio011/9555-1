using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000850 RID: 2128
public class GuildCityRootLogic : SingletonUnity<GuildCityRootLogic>
{
	// Token: 0x06003710 RID: 14096 RVA: 0x000E2830 File Offset: 0x000E0A30
	public void EnableReset()
	{
		for (int i = 0; i < this.CityItemList.Count; i++)
		{
			NGUITools.SetActive(this.CityItemList[i].gameObject, false);
		}
	}

	// Token: 0x06003711 RID: 14097 RVA: 0x000E2870 File Offset: 0x000E0A70
	public void UpdateCityInfo()
	{
		this.GuildMapList.Clear();
		this.GuildMapList = new List<guild_map_info>(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.CurGuildCityDataDic.Values);
		int num = this.GuildMapList.Count - this.CityItemList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.CityItemList[i].gameObject) as GameObject;
				gameObject.transform.gameObject.name = string.Format("cityitem_{0:D2}", this.CityItemList.Count);
				gameObject.transform.parent = this.ParentGrid.transform;
				gameObject.transform.position = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				GuildCityItemLogic component = gameObject.GetComponent<GuildCityItemLogic>();
				this.CityItemList.Add(component);
			}
		}
		for (int j = 0; j < this.CityItemList.Count; j++)
		{
			if (j < this.GuildMapList.Count)
			{
				NGUITools.SetActive(this.CityItemList[j].gameObject, true);
				this.CityItemList[j].UpdateInfo(this.GuildMapList[j]);
			}
			else
			{
				NGUITools.SetActive(this.CityItemList[j].gameObject, false);
			}
		}
		this.ParentGrid.Reposition();
	}

	// Token: 0x06003712 RID: 14098 RVA: 0x000E2A00 File Offset: 0x000E0C00
	public void UpdateCityItemInfo(ret_guild_map_reward.request request)
	{
		if (!request.HasId)
		{
			return;
		}
		int num = -1;
		for (int i = 0; i < this.GuildMapList.Count; i++)
		{
			if (this.GuildMapList[i].id.Equals(request.id))
			{
				num = i;
				this.GuildMapList[i].requireState = request.state;
				break;
			}
		}
		if (num != -1)
		{
			for (int j = 0; j < this.CityItemList.Count; j++)
			{
				if (this.CityItemList[j].UpdateRewardInfo(this.GuildMapList[num]))
				{
					break;
				}
			}
		}
	}

	// Token: 0x0400243B RID: 9275
	public UIGrid ParentGrid;

	// Token: 0x0400243C RID: 9276
	public List<GuildCityItemLogic> CityItemList;

	// Token: 0x0400243D RID: 9277
	private List<guild_map_info> GuildMapList = new List<guild_map_info>();
}
