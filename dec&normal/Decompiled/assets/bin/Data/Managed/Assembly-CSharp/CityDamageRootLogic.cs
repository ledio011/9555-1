using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000927 RID: 2343
public class CityDamageRootLogic : SingletonUnity<CityDamageRootLogic>
{
	// Token: 0x06004110 RID: 16656 RVA: 0x00134898 File Offset: 0x00132A98
	public void EnableReset()
	{
		for (int i = 0; i < this.DamageItemList.Count; i++)
		{
			NGUITools.SetActive(this.DamageItemList[i].gameObject, false);
		}
		this.IsSingleInfo = true;
		this.UpdataSelect();
	}

	// Token: 0x06004111 RID: 16657 RVA: 0x001348E8 File Offset: 0x00132AE8
	public void UpdateInfo(ret_guild_map_domine_top.request request)
	{
		this.SingleDamageList.Clear();
		this.GangDamageList.Clear();
		this.myRank = -1;
		this.myDamage = 0L;
		this.myGangRank = -1;
		this.myGangDamage = 0L;
		if (request.HasDamage_list)
		{
			this.SingleDamageList = request.damage_list;
		}
		this.SingleDamageList.Sort((damage_list x, damage_list y) => (int)(-x.damage + y.damage));
		if (request.HasGuild_damage_list)
		{
			this.GangDamageList = request.guild_damage_list;
		}
		this.GangDamageList.Sort((damage_list x, damage_list y) => (int)(-x.damage + y.damage));
		if (request.HasMy_rank2)
		{
			this.myRank = (int)request.my_rank2;
		}
		if (request.HasMy_damage2)
		{
			this.myDamage = request.my_damage2;
		}
		if (request.HasMy_rank)
		{
			this.myGangRank = (int)request.my_rank;
		}
		if (request.HasMy_damage)
		{
			this.myGangDamage = request.my_damage;
		}
		if (this.IsSingleInfo)
		{
			this.ShowSingleInfo();
		}
		else
		{
			this.ShowGangInfo();
		}
	}

	// Token: 0x06004112 RID: 16658 RVA: 0x00134A24 File Offset: 0x00132C24
	public void ShowSingleInfo()
	{
		int num = this.SingleDamageList.Count - this.DamageItemList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.DamageItemList[0].gameObject) as GameObject;
				DamageRankItemLogic component = gameObject.GetComponent<DamageRankItemLogic>();
				gameObject.name = string.Format("{0:D2}", this.DamageItemList.Count);
				gameObject.transform.parent = this.GridParent.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				this.DamageItemList.Add(component);
			}
		}
		for (int j = 0; j < this.DamageItemList.Count; j++)
		{
			if (j < this.SingleDamageList.Count)
			{
				NGUITools.SetActive(this.DamageItemList[j].gameObject, true);
				this.DamageItemList[j].updateItem(j + 1, this.SingleDamageList[j]);
			}
			else
			{
				NGUITools.SetActive(this.DamageItemList[j].gameObject, false);
			}
		}
		this.GridParent.Reposition();
		if (this.myRank != -1)
		{
			this.RankLabel.text = string.Format("NO.{0}", this.myRank);
			this.NameLabel.text = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.Name;
			this.DamageLabel.text = this.myDamage.ToString();
		}
		else
		{
			this.RankLabel.text = string.Empty;
			this.NameLabel.text = string.Empty;
			this.DamageLabel.text = string.Empty;
		}
	}

	// Token: 0x06004113 RID: 16659 RVA: 0x00134C14 File Offset: 0x00132E14
	public void ShowGangInfo()
	{
		int num = this.GangDamageList.Count - this.DamageItemList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.DamageItemList[0].gameObject) as GameObject;
				DamageRankItemLogic component = gameObject.GetComponent<DamageRankItemLogic>();
				gameObject.name = string.Format("{0:D2}", this.DamageItemList.Count);
				gameObject.transform.parent = this.GridParent.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				this.DamageItemList.Add(component);
			}
		}
		for (int j = 0; j < this.DamageItemList.Count; j++)
		{
			if (j < this.GangDamageList.Count)
			{
				NGUITools.SetActive(this.DamageItemList[j].gameObject, true);
				this.DamageItemList[j].updateItem(j + 1, this.GangDamageList[j]);
			}
			else
			{
				NGUITools.SetActive(this.DamageItemList[j].gameObject, false);
			}
		}
		this.GridParent.Reposition();
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild() && this.myGangRank != -1)
		{
			this.RankLabel.text = string.Format("NO.{0}", this.myGangRank);
			this.NameLabel.text = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.GuilName;
			this.DamageLabel.text = this.myGangDamage.ToString();
		}
		else
		{
			this.RankLabel.text = string.Empty;
			this.NameLabel.text = string.Empty;
			this.DamageLabel.text = string.Empty;
		}
	}

	// Token: 0x06004114 RID: 16660 RVA: 0x00134E18 File Offset: 0x00133018
	public void OnClickSingleBtn()
	{
		if (this.IsSingleInfo)
		{
			return;
		}
		this.IsSingleInfo = true;
		this.ShowSingleInfo();
		this.UpdataSelect();
	}

	// Token: 0x06004115 RID: 16661 RVA: 0x00134E3C File Offset: 0x0013303C
	public void OnClickGangBtn()
	{
		if (!this.IsSingleInfo)
		{
			return;
		}
		this.IsSingleInfo = false;
		this.ShowGangInfo();
		this.UpdataSelect();
	}

	// Token: 0x06004116 RID: 16662 RVA: 0x00134E60 File Offset: 0x00133060
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CityDamageRoot);
	}

	// Token: 0x06004117 RID: 16663 RVA: 0x00134E74 File Offset: 0x00133074
	public void UpdataSelect()
	{
		if (this.IsSingleInfo)
		{
			this.SelectSp.transform.parent = this.SingleTrans.transform;
			this.SelectSp.transform.localPosition = Vector3.zero;
		}
		else
		{
			this.SelectSp.transform.parent = this.GangTrans.transform;
			this.SelectSp.transform.localPosition = Vector3.zero;
		}
	}

	// Token: 0x04002CC2 RID: 11458
	public List<DamageRankItemLogic> DamageItemList;

	// Token: 0x04002CC3 RID: 11459
	public UIGrid GridParent;

	// Token: 0x04002CC4 RID: 11460
	private List<damage_list> SingleDamageList = new List<damage_list>();

	// Token: 0x04002CC5 RID: 11461
	private List<damage_list> GangDamageList = new List<damage_list>();

	// Token: 0x04002CC6 RID: 11462
	private int myRank;

	// Token: 0x04002CC7 RID: 11463
	private long myDamage;

	// Token: 0x04002CC8 RID: 11464
	private int myGangRank;

	// Token: 0x04002CC9 RID: 11465
	private long myGangDamage;

	// Token: 0x04002CCA RID: 11466
	private bool IsSingleInfo;

	// Token: 0x04002CCB RID: 11467
	public UISprite SelectSp;

	// Token: 0x04002CCC RID: 11468
	public Transform SingleTrans;

	// Token: 0x04002CCD RID: 11469
	public Transform GangTrans;

	// Token: 0x04002CCE RID: 11470
	public UILabel RankLabel;

	// Token: 0x04002CCF RID: 11471
	public UILabel NameLabel;

	// Token: 0x04002CD0 RID: 11472
	public UILabel DamageLabel;
}
