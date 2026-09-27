using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008E5 RID: 2277
public class LevelRewardRootLogic : SingletonUnity<LevelRewardRootLogic>
{
	// Token: 0x06003D8B RID: 15755 RVA: 0x001143E0 File Offset: 0x001125E0
	private void OnEnable()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.Refresh));
	}

	// Token: 0x06003D8C RID: 15756 RVA: 0x00114410 File Offset: 0x00112610
	private void OnDisable()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.Refresh));
	}

	// Token: 0x06003D8D RID: 15757 RVA: 0x00114440 File Offset: 0x00112640
	public void EnableReset()
	{
		for (int i = 0; i < this.LineItems.Count; i++)
		{
			NGUITools.SetActive(this.LineItems[i].gameObject, false);
		}
		NGUITools.SetActive(this.BtnSp.gameObject, false);
		this.Completeflag.enabled = false;
	}

	// Token: 0x06003D8E RID: 15758 RVA: 0x001144A0 File Offset: 0x001126A0
	public void Reset(ret_level_reward.request request)
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
		this.SelectData();
	}

	// Token: 0x06003D8F RID: 15759 RVA: 0x00114504 File Offset: 0x00112704
	private void Refresh()
	{
		if (this.CurData != null && this.CurInfo != null)
		{
			this.RefershInfo(this.CurData, this.CurInfo);
		}
	}

	// Token: 0x06003D90 RID: 15760 RVA: 0x0011453C File Offset: 0x0011273C
	public void UpdateInfo(get_level_reward.request request)
	{
		if (this.CurData != null && this.CurInfo != null && request.HasLevel_reward && request.level_reward.ContainsKey(this.CurInfo.ID))
		{
			this.CurInfo = request.level_reward[this.CurInfo.ID];
			this.RefershInfo(this.CurData, this.CurInfo);
		}
	}

	// Token: 0x06003D91 RID: 15761 RVA: 0x001145B4 File Offset: 0x001127B4
	public void SelectData()
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
				if (this.CheckCompleteState() && i < levelRewardDataList.Count - 1)
				{
					this.CurData = levelRewardDataList[i + 1];
					if (this.levelrewardDic.ContainsKey(this.CurData.ID))
					{
						this.CurInfo = this.levelrewardDic[this.CurData.ID];
					}
				}
			}
		}
		if (this.CurData != null && this.CurInfo != null)
		{
			this.RefershInfo(this.CurData, this.CurInfo);
		}
	}

	// Token: 0x06003D92 RID: 15762 RVA: 0x00114710 File Offset: 0x00112910
	public bool CheckCompleteState()
	{
		return this.CurInfo != null && this.CurInfo.HasState && (this.CurInfo.state & 8L) != 0L;
	}

	// Token: 0x06003D93 RID: 15763 RVA: 0x00114744 File Offset: 0x00112944
	public void RefershInfo(LevelRewardData curdata, level_reward curinfo)
	{
		this.InitTexture();
		this.CurData = curdata;
		this.CurInfo = curinfo;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		for (int i = 0; i < this.LineItems.Count; i++)
		{
			NGUITools.SetActive(this.LineItems[i].gameObject, true);
			this.LineItems[i].UpdateInfo(curdata, i, curinfo);
		}
		this.headinfoLabel.text = StrDictionary.GetDictionaryString("#{100195}", new object[0]);
		this.bigrewardlabel.text = StrDictionary.GetDictionaryString("#{100196}", new object[]
		{
			curdata.RewardNum
		});
		this.diamondlabel.text = curdata.RewardNum.ToString();
		this.Completeflag.enabled = false;
		NGUITools.SetActive(this.BtnSp.gameObject, true);
		this.BtnSp.spriteName = GameDefine.BtnIconNew[1];
		this.CanGetBigRewardflag = false;
		if (this.CurInfo != null && this.CurInfo.HasState)
		{
			if ((this.CurInfo.state & 8L) != 0L)
			{
				this.Completeflag.enabled = true;
				this.BtnSp.spriteName = GameDefine.BtnIconNew[1];
				this.BtnLabel.text = StrDictionary.GetDictionaryString("#{300403}", new object[0]);
			}
			else
			{
				this.Completeflag.enabled = false;
				this.BtnLabel.text = StrDictionary.GetDictionaryString("#{300402}", new object[0]);
				if ((this.CurInfo.state & 1L) != 0L && (this.CurInfo.state & 2L) != 0L && (this.CurInfo.state & 4L) != 0L && playerData.Level >= this.CurData.TargetLevel)
				{
					this.CanGetBigRewardflag = true;
					this.BtnSp.spriteName = GameDefine.BtnIconNew[0];
				}
				else
				{
					this.BtnSp.spriteName = GameDefine.BtnIconNew[1];
				}
			}
		}
	}

	// Token: 0x06003D94 RID: 15764 RVA: 0x0011495C File Offset: 0x00112B5C
	public void OnClickLeftReceiveBtn()
	{
		if ((this.CurInfo.state & 8L) != 0L)
		{
			return;
		}
		if (this.CanGetBigRewardflag)
		{
			WaitResponseUIRootLogic.OpenWaitBox(297, 10f, 0f, null);
			receive_level_reward.request request = new receive_level_reward.request();
			request.ID = this.CurData.ID;
			request.index = 3L;
			NetLogic.GetInstance().Send<Protocol.receive_level_reward>(request, null);
		}
		else
		{
			NoticeLogic.AddNotifyData("#{100197}", true, false);
		}
	}

	// Token: 0x06003D95 RID: 15765 RVA: 0x001149DC File Offset: 0x00112BDC
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LevelRewardRoot);
	}

	// Token: 0x06003D96 RID: 15766 RVA: 0x001149F0 File Offset: 0x00112BF0
	public void InitTexture()
	{
		if (this.BgTexture.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(GameDefine.LevelRewardBg, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06003D97 RID: 15767 RVA: 0x00114A40 File Offset: 0x00112C40
	private void TextureLoadFinish(string name, Texture textureObj)
	{
		if (textureObj != null)
		{
			this.BgTexture.mainTexture = textureObj;
		}
	}

	// Token: 0x04002913 RID: 10515
	public UILabel headinfoLabel;

	// Token: 0x04002914 RID: 10516
	public UILabel bigrewardlabel;

	// Token: 0x04002915 RID: 10517
	public UILabel diamondlabel;

	// Token: 0x04002916 RID: 10518
	public UISprite Completeflag;

	// Token: 0x04002917 RID: 10519
	public UISprite BtnSp;

	// Token: 0x04002918 RID: 10520
	public UILabel BtnLabel;

	// Token: 0x04002919 RID: 10521
	public List<LevelRewardLineLogic> LineItems;

	// Token: 0x0400291A RID: 10522
	public UITexture BgTexture;

	// Token: 0x0400291B RID: 10523
	private LevelRewardData CurData;

	// Token: 0x0400291C RID: 10524
	private level_reward CurInfo;

	// Token: 0x0400291D RID: 10525
	private bool CanGetBigRewardflag;

	// Token: 0x0400291E RID: 10526
	private Dictionary<string, level_reward> levelrewardDic = new Dictionary<string, level_reward>();

	// Token: 0x0400291F RID: 10527
	private List<level_reward> levelrewardList = new List<level_reward>();

	// Token: 0x04002920 RID: 10528
	private int Playerlevel;
}
