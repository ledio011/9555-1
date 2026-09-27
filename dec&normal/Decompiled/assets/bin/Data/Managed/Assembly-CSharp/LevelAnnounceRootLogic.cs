using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200094F RID: 2383
public class LevelAnnounceRootLogic : SingletonUnity<LevelAnnounceRootLogic>
{
	// Token: 0x060042A6 RID: 17062 RVA: 0x001460BC File Offset: 0x001442BC
	public void Reset()
	{
		this.Refresh();
	}

	// Token: 0x060042A7 RID: 17063 RVA: 0x001460C4 File Offset: 0x001442C4
	private void OnEnable()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.Refresh));
	}

	// Token: 0x060042A8 RID: 17064 RVA: 0x001460F4 File Offset: 0x001442F4
	private void OnDisable()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.Refresh));
	}

	// Token: 0x060042A9 RID: 17065 RVA: 0x00146124 File Offset: 0x00144324
	private void Refresh()
	{
		NetLogic.GetInstance().Send<Protocol.req_level_reward>(null, null);
		NGUITools.SetActive(this.ObjRoot, false);
		this.EffectEdge.enabled = false;
		this.BoxAnima.ResetToBeginning();
		this.BoxAnima.enabled = false;
	}

	// Token: 0x060042AA RID: 17066 RVA: 0x0014616C File Offset: 0x0014436C
	public void UpdataInfo(ret_level_reward.request request)
	{
		this.CurData = null;
		this.CurInfo = null;
		this.levelrewardDic.Clear();
		this.levelrewardList.Clear();
		if (request.HasLevel_reward)
		{
			this.levelrewardDic = request.level_reward;
			this.levelrewardList = new List<level_reward>(request.level_reward.Values);
		}
		this.refershUI();
	}

	// Token: 0x060042AB RID: 17067 RVA: 0x001461D0 File Offset: 0x001443D0
	public void RefershInfo(get_level_reward.request request)
	{
		this.CurData = null;
		this.CurInfo = null;
		this.levelrewardDic.Clear();
		this.levelrewardList.Clear();
		if (request.HasLevel_reward)
		{
			this.levelrewardDic = request.level_reward;
			this.levelrewardList = new List<level_reward>(request.level_reward.Values);
		}
		this.refershUI();
	}

	// Token: 0x060042AC RID: 17068 RVA: 0x00146234 File Offset: 0x00144434
	public bool CheckCompleteState()
	{
		return this.CurInfo != null && this.CurInfo.HasState && (this.CurInfo.state & 8L) != 0L;
	}

	// Token: 0x060042AD RID: 17069 RVA: 0x00146268 File Offset: 0x00144468
	public void refershUI()
	{
		this.Playerlevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		List<LevelRewardData> levelRewardDataList = DataManager.GetLevelRewardDataList();
		levelRewardDataList.Sort(delegate(LevelRewardData x, LevelRewardData y)
		{
			if (x.ID.Length == y.ID.Length)
			{
				return x.ID.CompareTo(y.ID);
			}
			return x.ID.Length - y.ID.Length;
		});
		for (int i = 0; i < levelRewardDataList.Count; i++)
		{
			if (this.Playerlevel >= levelRewardDataList[i].StartLv && this.Playerlevel <= levelRewardDataList[i].EndLv)
			{
				this.CurData = levelRewardDataList[i];
				if (this.levelrewardDic.ContainsKey(this.CurData.ID))
				{
					this.CurInfo = this.levelrewardDic[this.CurData.ID];
				}
				if (this.CheckCompleteState())
				{
					if (i < levelRewardDataList.Count - 1)
					{
						this.CurData = levelRewardDataList[i + 1];
						if (this.levelrewardDic.ContainsKey(this.CurData.ID))
						{
							this.CurInfo = this.levelrewardDic[this.CurData.ID];
						}
					}
					else
					{
						this.CurData = null;
					}
				}
			}
		}
		if (this.CurData == null)
		{
			NGUITools.SetActive(this.ObjRoot, false);
			return;
		}
		this.NameLabel1.text = StrDictionary.GetDictionaryString("#{100189}", new object[]
		{
			this.CurData.EndLv
		});
		this.NameLabel2.text = StrDictionary.GetDictionaryString("#{100190}", new object[0]);
		NGUITools.SetActive(this.ObjRoot, true);
		this.CheckShowEffect();
	}

	// Token: 0x060042AE RID: 17070 RVA: 0x0014641C File Offset: 0x0014461C
	public void CheckShowEffect()
	{
		this.curshowrewardid = -1;
		if (this.CurData != null && this.CurInfo != null)
		{
			if (this.Playerlevel >= this.CurData.TargetLevel1 && (this.CurInfo.state & 1L) == 0L)
			{
				this.curshowrewardid = int.Parse(this.CurData.ID) * 4;
			}
			if (this.Playerlevel >= this.CurData.TargetLevel2 && (this.CurInfo.state & 2L) == 0L)
			{
				this.curshowrewardid = int.Parse(this.CurData.ID) * 4 + 1;
			}
			if (this.Playerlevel >= this.CurData.TargetLevel3 && (this.CurInfo.state & 4L) == 0L)
			{
				this.curshowrewardid = int.Parse(this.CurData.ID) * 4 + 2;
			}
			if (this.Playerlevel >= this.CurData.TargetLevel && (this.CurInfo.state & 8L) == 0L && (this.CurInfo.state & 1L) != 0L && (this.CurInfo.state & 2L) != 0L && (this.CurInfo.state & 4L) != 0L)
			{
				this.curshowrewardid = int.Parse(this.CurData.ID) * 4 + 3;
			}
		}
		if (LevelAnnounceRootLogic.curGetRewardId >= this.curshowrewardid)
		{
			this.EffectEdge.enabled = false;
			this.BoxAnima.ResetToBeginning();
			this.BoxAnima.enabled = false;
		}
		else
		{
			this.EffectEdge.enabled = true;
			this.BoxAnima.enabled = true;
		}
	}

	// Token: 0x060042AF RID: 17071 RVA: 0x001465DC File Offset: 0x001447DC
	public void OnClickLevelBtn()
	{
		if (this.CurData != null && this.CurInfo != null)
		{
			LevelAnnounceRootLogic.curGetRewardId = this.curshowrewardid;
			this.EffectEdge.enabled = false;
			this.BoxAnima.ResetToBeginning();
			this.BoxAnima.enabled = false;
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.LevelRewardRoot, delegate
			{
				SingletonUnity<LevelRewardRootLogic>.Instance.RefershInfo(this.CurData, this.CurInfo);
			}, null);
		}
	}

	// Token: 0x04002EEE RID: 12014
	public static int curGetRewardId = -1;

	// Token: 0x04002EEF RID: 12015
	public GameObject ObjRoot;

	// Token: 0x04002EF0 RID: 12016
	public UILabel NameLabel1;

	// Token: 0x04002EF1 RID: 12017
	public UILabel NameLabel2;

	// Token: 0x04002EF2 RID: 12018
	private LevelRewardData CurData;

	// Token: 0x04002EF3 RID: 12019
	private Dictionary<string, level_reward> levelrewardDic = new Dictionary<string, level_reward>();

	// Token: 0x04002EF4 RID: 12020
	private List<level_reward> levelrewardList = new List<level_reward>();

	// Token: 0x04002EF5 RID: 12021
	private level_reward CurInfo;

	// Token: 0x04002EF6 RID: 12022
	private int Playerlevel;

	// Token: 0x04002EF7 RID: 12023
	public UITexture EffectEdge;

	// Token: 0x04002EF8 RID: 12024
	public TweenRotation BoxAnima;

	// Token: 0x04002EF9 RID: 12025
	private int curshowrewardid = -1;
}
