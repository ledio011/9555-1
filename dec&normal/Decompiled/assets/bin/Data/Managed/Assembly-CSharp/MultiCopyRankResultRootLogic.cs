using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200095D RID: 2397
public class MultiCopyRankResultRootLogic : SingletonUnity<MultiCopyRankResultRootLogic>
{
	// Token: 0x06004319 RID: 17177 RVA: 0x0014A36C File Offset: 0x0014856C
	public void Reset(bool issuccess, battle_info battleinfo, List<item> items, int type, string activityid)
	{
		this.LastKillLabel.enabled = false;
		this.curBattleInfo = battleinfo;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.DamageList = this.curBattleInfo.damage_list;
		this.DamageList.Sort((damage_list x, damage_list y) => (int)y.damage - (int)x.damage);
		int num = this.DamageList.Count - this.rankItemList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.rankItemList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.RankGrid.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				DamageRankItemLogic component = gameObject.GetComponent<DamageRankItemLogic>();
				this.rankItemList.Add(component);
				if (this.rankItemList.Count < 10)
				{
					gameObject.gameObject.name = "0" + this.rankItemList.Count.ToString();
				}
				else
				{
					gameObject.gameObject.name = this.rankItemList.Count.ToString();
				}
			}
			this.RankGrid.Reposition();
		}
		for (int j = 0; j < this.rankItemList.Count; j++)
		{
			if (j < this.DamageList.Count)
			{
				NGUITools.SetActive(this.rankItemList[j].gameObject, true);
				this.rankItemList[j].updateItem(j + 1, this.DamageList[j]);
			}
			else
			{
				NGUITools.SetActive(this.rankItemList[j].gameObject, false);
			}
		}
		if (type == 4)
		{
			for (int k = 0; k < this.DamageList.Count; k++)
			{
				if (this.DamageList[k].id == PlayerData.MainPlayerServerId)
				{
					this.isbestFlag = (k == 0);
					this.selfRankLabel.text = string.Format("NO.{0}", k + 1);
					this.selfNameLabel.text = this.DamageList[k].name;
					this.selfDamageLabel.text = string.Format("{0}", this.DamageList[k].damage);
					break;
				}
			}
		}
		else
		{
			this.isbestFlag = (battleinfo.my_rank == 1L);
			this.selfRankLabel.text = string.Format("NO.{0}", battleinfo.my_rank);
			this.selfNameLabel.text = playerData.MainPlayerAttrData.Name;
			this.selfDamageLabel.text = string.Format("{0}", battleinfo.my_damage);
		}
		if (issuccess)
		{
			this.titleLabel.text = StrDictionary.GetDictionaryString("#{100753}", new object[0]);
			if (type == 5 || type == 6)
			{
				this.finalImpactlabel.text = StrDictionary.GetDictionaryString("#{100761}", new object[0]);
				this.finalImpactNamelabel.text = string.Format("{0}", this.curBattleInfo.lastKill);
				if (this.curBattleInfo.lastKill.Equals(playerData.MainPlayerAttrData.Name))
				{
					this.LastKillLabel.enabled = true;
				}
			}
			else if (type == 4)
			{
				this.finalImpactlabel.text = string.Empty;
				this.finalImpactNamelabel.text = string.Empty;
			}
			this.showrewarditem.ShowRewards(items);
			NGUITools.SetActive(this.showrewarditem.gameObject, true);
			this.loseLabel.enabled = false;
			if (this.isbestFlag)
			{
				this.BestSp.enabled = true;
			}
			else
			{
				this.BestSp.enabled = false;
			}
			SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(7, 1f, null);
		}
		else
		{
			LocalDataSaveManager.SetDiedFlag(1);
			this.titleLabel.text = StrDictionary.GetDictionaryString("#{100757}", new object[0]);
			this.BestSp.enabled = false;
			this.finalImpactlabel.text = StrDictionary.GetDictionaryString("#{100758}", new object[0]);
			if (type == 4)
			{
				BarFightCopyData barFightCopyDataByID = DataManager.GetBarFightCopyDataByID(activityid);
				this.finalImpactNamelabel.text = string.Format("{0}", new TimeSpan(0, 0, barFightCopyDataByID.ExistTime));
			}
			else if (type == 5)
			{
				WildBossData wildBossDataByID = DataManager.GetWildBossDataByID(activityid);
				this.finalImpactNamelabel.text = string.Format("{0}", new TimeSpan(0, 0, wildBossDataByID.ExistTime));
			}
			else if (type == 6)
			{
				GuildBossData guildBossDataByID = DataManager.GetGuildBossDataByID(activityid);
				this.finalImpactNamelabel.text = string.Format("{0}", new TimeSpan(0, 0, guildBossDataByID.ExistTime));
			}
			NGUITools.SetActive(this.showrewarditem.gameObject, false);
			this.loseLabel.enabled = true;
		}
		SimpleRewardRootLogic.AddRewards(items);
		if (type == 6)
		{
			if (issuccess)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("guildboss", string.Format("guildboss_{0}", activityid), "success");
			}
			else
			{
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("guildboss", string.Format("guildboss_{0}", activityid), "failure");
			}
		}
		else if (type == 5)
		{
			if (issuccess)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("wildboss", string.Format("wildboss_{0}", activityid), "success");
			}
			else
			{
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("wildboss", string.Format("wildboss_{0}", activityid), "failure");
			}
		}
		else if (type == 4)
		{
			if (issuccess)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", string.Format("activity_{0}", type), "success");
			}
			else
			{
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("TimeActivity", string.Format("activity_{0}", type), "failure");
			}
		}
	}

	// Token: 0x0600431A RID: 17178 RVA: 0x0014A9DC File Offset: 0x00148BDC
	private void OnEnable()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = true;
		}
		SingletonUnity<UIManager>.Instance.CloseOtherPlayerUI();
	}

	// Token: 0x0600431B RID: 17179 RVA: 0x0014AA18 File Offset: 0x00148C18
	private void OnDisable()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = false;
		}
	}

	// Token: 0x0600431C RID: 17180 RVA: 0x0014AA4C File Offset: 0x00148C4C
	public void OnClickleaveBtn()
	{
		NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
	}

	// Token: 0x04002F8F RID: 12175
	public UILabel titleLabel;

	// Token: 0x04002F90 RID: 12176
	public List<DamageRankItemLogic> rankItemList;

	// Token: 0x04002F91 RID: 12177
	public List<damage_list> DamageList;

	// Token: 0x04002F92 RID: 12178
	public UIGrid RankGrid;

	// Token: 0x04002F93 RID: 12179
	public ShowRewardItems showrewarditem;

	// Token: 0x04002F94 RID: 12180
	public UILabel loseLabel;

	// Token: 0x04002F95 RID: 12181
	public UILabel selfRankLabel;

	// Token: 0x04002F96 RID: 12182
	public UILabel selfNameLabel;

	// Token: 0x04002F97 RID: 12183
	public UILabel selfDamageLabel;

	// Token: 0x04002F98 RID: 12184
	public UILabel finalImpactlabel;

	// Token: 0x04002F99 RID: 12185
	public UILabel finalImpactNamelabel;

	// Token: 0x04002F9A RID: 12186
	private battle_info curBattleInfo;

	// Token: 0x04002F9B RID: 12187
	private bool isbestFlag;

	// Token: 0x04002F9C RID: 12188
	public UISprite BestSp;

	// Token: 0x04002F9D RID: 12189
	public UILabel LastKillLabel;
}
