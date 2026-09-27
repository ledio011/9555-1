using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A18 RID: 2584
public class GuildStarRootLogic : SingletonUnity<GuildStarRootLogic>
{
	// Token: 0x06004A4B RID: 19019 RVA: 0x00185048 File Offset: 0x00183248
	public void EnableReset()
	{
		this.CurShowIndex = 1;
		this.CoinLabel.text = GameMoneyHelper.GetGuildContribute().ToString();
		this.InitTexture();
		this.CompleteFlag.enabled = false;
		NGUITools.SetActive(this.MainPage.gameObject, false);
		NGUITools.SetActive(this.TempPage.gameObject, false);
		NGUITools.SetActive(this.Cost1quaSp.gameObject, false);
		NGUITools.SetActive(this.Cost2quaSp.gameObject, false);
	}

	// Token: 0x06004A4C RID: 19020 RVA: 0x001850CC File Offset: 0x001832CC
	public void InitTexture()
	{
		List<string> list = new List<string>();
		if (this.MapBgTexture.mainTexture == null)
		{
			list.Add(GameDefine.GuildStarBG);
		}
		if (this.MapXingpanDi.mainTexture == null)
		{
			list.Add(GameDefine.GuildXingpanDi);
		}
		if (list.Count == 0)
		{
			return;
		}
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(list, new BundleManager.LoadTextureDicFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06004A4D RID: 19021 RVA: 0x00185158 File Offset: 0x00183358
	private void TextureLoadFinish(Dictionary<string, Texture> retdic)
	{
		if (retdic == null || retdic.Count == 0)
		{
			return;
		}
		if (retdic.ContainsKey(GameDefine.GuildStarBG))
		{
			this.MapBgTexture.mainTexture = retdic[GameDefine.GuildStarBG];
		}
		if (retdic.ContainsKey(GameDefine.GuildXingpanDi))
		{
			this.MapXingpanDi.mainTexture = retdic[GameDefine.GuildXingpanDi];
		}
	}

	// Token: 0x06004A4E RID: 19022 RVA: 0x001851C4 File Offset: 0x001833C4
	public void Reset(ret_guild_star.request request)
	{
		this.GuildstarsInfo.Clear();
		this.GuildstarsDic.Clear();
		if (request.HasGuild_stars)
		{
			this.GuildstarsDic = request.guild_stars;
			this.GuildstarsInfo = new List<guild_star>(request.guild_stars.Values);
			this.GuildstarsInfo.Sort(delegate(guild_star x, guild_star y)
			{
				if (x.ID.Length != y.ID.Length)
				{
					return x.ID.Length - y.ID.Length;
				}
				return x.ID.CompareTo(y.ID);
			});
		}
		for (int i = 0; i < this.GuildstarsInfo.Count; i++)
		{
			GuildStarData guildStarDataById = DataManager.GetGuildStarDataById(this.GuildstarsInfo[i].ID);
			this.CurShowIndex = guildStarDataById.MapID;
			if (this.GuildstarsInfo[i].HasState && this.GuildstarsInfo[i].state == 0L)
			{
				break;
			}
		}
		this.NeedMapIndex = this.CurShowIndex;
		this.GetCurShowData(this.CurShowIndex);
		NGUITools.SetActive(this.MainPage.gameObject, true);
		NGUITools.SetActive(this.TempPage.gameObject, false);
		this.MainPage.UpdateInfo(this.GuildstarsDic, this.CurShowIndex);
		this.SetDirBtn();
		this.refershRightInfo();
	}

	// Token: 0x06004A4F RID: 19023 RVA: 0x0018530C File Offset: 0x0018350C
	public void UpdateInfo(ret_update_guild_star.request request)
	{
		this.GuildstarsDic.Clear();
		this.GuildstarsInfo.Clear();
		if (request.HasGuild_stars)
		{
			this.GuildstarsDic = request.guild_stars;
			this.GuildstarsInfo = new List<guild_star>(request.guild_stars.Values);
			this.GuildstarsInfo.Sort(delegate(guild_star x, guild_star y)
			{
				if (x.ID.Length != y.ID.Length)
				{
					return x.ID.Length - y.ID.Length;
				}
				return x.ID.CompareTo(y.ID);
			});
		}
		for (int i = 0; i < this.GuildstarsInfo.Count; i++)
		{
			GuildStarData guildStarDataById = DataManager.GetGuildStarDataById(this.GuildstarsInfo[i].ID);
			this.CurShowIndex = guildStarDataById.MapID;
			if (this.GuildstarsInfo[i].HasState && this.GuildstarsInfo[i].state == 0L)
			{
				break;
			}
		}
		this.NeedMapIndex = this.CurShowIndex;
		this.GetCurShowData(this.CurShowIndex);
		NGUITools.SetActive(this.MainPage.gameObject, true);
		NGUITools.SetActive(this.TempPage.gameObject, false);
		this.MainPage.UpdateInfo(this.GuildstarsDic, this.CurShowIndex);
		this.SetDirBtn();
		this.refershRightInfo();
	}

	// Token: 0x06004A50 RID: 19024 RVA: 0x00185454 File Offset: 0x00183654
	public void refershRightInfo()
	{
		if (this.CurStarData != null)
		{
			this.AttSp1.spriteName = GameDefine.GetAttributeIcon(this.CurStarData.Status1);
			this.Attname1.text = GameDefine.GetAttributeName_S(this.CurStarData.Status1);
			this.AttInfo1.text = string.Format("+{0}", this.CurStarData.Value1);
			this.AttSp2.spriteName = GameDefine.GetAttributeIcon(this.CurStarData.Status2);
			this.Attname2.text = GameDefine.GetAttributeName_S(this.CurStarData.Status2);
			this.AttInfo2.text = string.Format("+{0}", this.CurStarData.Value2);
			this.NeedLevelLabel.text = StrDictionary.GetDictionaryString("#{106026}", new object[]
			{
				this.CurStarData.UnlockLevel
			});
			if (this.CurMapIsComplete)
			{
				this.MapNameLabel.text = string.Format("{0} Lv.{1}", StrDictionary.GetDictionaryString(this.CurStarData.MapName, new object[0]), StrDictionary.GetDictionaryString("#{106002}", new object[0]));
				NGUITools.SetActive(this.Cost1quaSp.gameObject, false);
				NGUITools.SetActive(this.Cost2quaSp.gameObject, false);
				this.CostNameLabel.enabled = false;
				this.CompleteFlag.enabled = true;
				this.upgradeBtn.spriteName = GameDefine.BtnIcon[2];
			}
			else
			{
				this.MapNameLabel.text = string.Format("{0} Lv.{1}", StrDictionary.GetDictionaryString(this.CurStarData.MapName, new object[0]), this.CurStarData.Lv - 1);
				this.CostNameLabel.enabled = true;
				this.CompleteFlag.enabled = false;
				if (!string.IsNullOrEmpty(this.CurStarData.CostID1))
				{
					ItemData itemDataByID = DataManager.GetItemDataByID(this.CurStarData.CostID1);
					this.Cost1quaSp.spriteName = itemDataByID.QualityType.ToString();
					this.Cost1Sp.spriteName = itemDataByID.BackPackIcon;
					this.Cost1Label.text = this.CurStarData.Cost1.ToString();
					if ((long)this.CurStarData.Cost1 <= GameMoneyHelper.GetMoneyNum((int)GameDefine.ITEM_ID_MONEYTYPR[this.CurStarData.CostID1]))
					{
						this.Cost1Label.color = Color.white;
					}
					else
					{
						this.Cost1Label.color = Color.red;
					}
					NGUITools.SetActive(this.Cost1quaSp.gameObject, true);
				}
				else
				{
					NGUITools.SetActive(this.Cost1quaSp.gameObject, false);
				}
				if (!string.IsNullOrEmpty(this.CurStarData.CostID2))
				{
					ItemData itemDataByID2 = DataManager.GetItemDataByID(this.CurStarData.CostID2);
					this.Cost2quaSp.spriteName = itemDataByID2.QualityType.ToString();
					this.Cost2Sp.spriteName = itemDataByID2.BackPackIcon;
					this.Cost2Label.text = this.CurStarData.Cost2.ToString();
					if ((long)this.CurStarData.Cost2 <= GameMoneyHelper.GetMoneyNum((int)GameDefine.ITEM_ID_MONEYTYPR[this.CurStarData.CostID2]))
					{
						this.Cost2Label.color = Color.white;
					}
					else
					{
						this.Cost2Label.color = Color.red;
					}
					NGUITools.SetActive(this.Cost2quaSp.gameObject, true);
				}
				else
				{
					NGUITools.SetActive(this.Cost2quaSp.gameObject, false);
				}
				PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				if (playerData.PlayerGuild.GuilLevel >= this.CurStarData.UnlockLevel && this.NeedMapIndex == this.CurStarData.MapID)
				{
					this.upgradeBtn.spriteName = GameDefine.BtnIcon[1];
				}
				else
				{
					this.upgradeBtn.spriteName = GameDefine.BtnIcon[2];
				}
			}
		}
		else
		{
			this.CostNameLabel.enabled = false;
			NGUITools.SetActive(this.Cost1quaSp.gameObject, false);
			NGUITools.SetActive(this.Cost2quaSp.gameObject, false);
			this.CompleteFlag.enabled = false;
		}
	}

	// Token: 0x06004A51 RID: 19025 RVA: 0x001858A8 File Offset: 0x00183AA8
	public void OnClickLeftBtn()
	{
		if (this.CurShowIndex > 1)
		{
			this.CurShowIndex--;
			this.MainAnima.from = this.MainAnima.transform.localPosition;
			this.MainAnima.to = this.rightPos;
			this.MainAnima.duration = this.MoveTime;
			this.MainAnima.ResetToBeginning();
			this.TempAnima.from = this.LeftPos;
			this.TempAnima.to = Vector3.zero;
			this.TempAnima.duration = this.MoveTime;
			this.TempAnima.ResetToBeginning();
			this.MainAnima.PlayForward();
			this.TempAnima.PlayForward();
			NGUITools.SetActive(this.TempPage.gameObject, true);
			this.TempPage.UpdateInfo(this.GuildstarsDic, this.CurShowIndex);
			this.GetCurShowData(this.CurShowIndex);
			this.SetDirBtn();
			this.SwapPage();
			this.refershRightInfo();
		}
	}

	// Token: 0x06004A52 RID: 19026 RVA: 0x001859B0 File Offset: 0x00183BB0
	public void OnClickRightBtn()
	{
		if (this.CurShowIndex < this.MaxStarMapNum)
		{
			this.CurShowIndex++;
			this.MainAnima.from = this.MainAnima.transform.localPosition;
			this.MainAnima.to = this.LeftPos;
			this.MainAnima.duration = this.MoveTime;
			this.MainAnima.ResetToBeginning();
			this.TempAnima.from = this.rightPos;
			this.TempAnima.to = Vector3.zero;
			this.TempAnima.duration = this.MoveTime;
			this.TempAnima.ResetToBeginning();
			this.MainAnima.PlayForward();
			this.TempAnima.PlayForward();
			NGUITools.SetActive(this.TempPage.gameObject, true);
			this.TempPage.UpdateInfo(this.GuildstarsDic, this.CurShowIndex);
			this.GetCurShowData(this.CurShowIndex);
			this.SetDirBtn();
			this.SwapPage();
			this.refershRightInfo();
		}
	}

	// Token: 0x06004A53 RID: 19027 RVA: 0x00185AC0 File Offset: 0x00183CC0
	public void OnClickUpgrade()
	{
		if (this.CurStarData != null)
		{
			if (this.CurMapIsComplete)
			{
				MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{106018}", new object[0]), "#{100127}", null);
			}
			else
			{
				PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				if (playerData.PlayerGuild.GuilLevel < this.CurStarData.UnlockLevel)
				{
					MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{106016}", new object[]
					{
						this.CurStarData.UnlockLevel
					}), "#{100127}", null);
					return;
				}
				if (this.NeedMapIndex != this.CurStarData.MapID)
				{
					MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{106017}", new object[0]), "#{100127}", null);
					return;
				}
				if (!string.IsNullOrEmpty(this.CurStarData.CostID1) && !GameMoneyHelper.BeforeCheckBuy(GameDefine.ITEM_ID_MONEYTYPR[this.CurStarData.CostID1], this.CurStarData.Cost1))
				{
					return;
				}
				if (!string.IsNullOrEmpty(this.CurStarData.CostID2) && !GameMoneyHelper.BeforeCheckBuy(GameDefine.ITEM_ID_MONEYTYPR[this.CurStarData.CostID2], this.CurStarData.Cost2))
				{
					return;
				}
				WaitResponseUIRootLogic.OpenWaitBox(294, 10f, 0f, null);
				update_guild_star.request request = new update_guild_star.request();
				request.ID = this.CurStarData.ID;
				NetLogic.GetInstance().Send<Protocol.update_guild_star>(request, null);
			}
		}
	}

	// Token: 0x06004A54 RID: 19028 RVA: 0x00185C44 File Offset: 0x00183E44
	public void OnClickCost1Btn()
	{
		if (this.CurStarData != null && !string.IsNullOrEmpty(this.CurStarData.CostID1))
		{
			ItemData itemDataByID = DataManager.GetItemDataByID(this.CurStarData.CostID1);
			if (itemDataByID != null)
			{
				ItemInfoRootLogicNew.ShowItemTips(itemDataByID, false, UI_PAGE_TYPE.INVALID);
			}
		}
	}

	// Token: 0x06004A55 RID: 19029 RVA: 0x00185C90 File Offset: 0x00183E90
	public void OnClickCost2Btn()
	{
		if (this.CurStarData != null && !string.IsNullOrEmpty(this.CurStarData.CostID2))
		{
			ItemData itemDataByID = DataManager.GetItemDataByID(this.CurStarData.CostID2);
			if (itemDataByID != null)
			{
				ItemInfoRootLogicNew.ShowItemTips(itemDataByID, false, UI_PAGE_TYPE.INVALID);
			}
		}
	}

	// Token: 0x06004A56 RID: 19030 RVA: 0x00185CDC File Offset: 0x00183EDC
	public void UpdateMoneyLabel()
	{
		this.CoinLabel.text = GameMoneyHelper.GetGuildContribute().ToString();
	}

	// Token: 0x06004A57 RID: 19031 RVA: 0x00185D04 File Offset: 0x00183F04
	public void OnClickTishi()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
		{
			SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", "#{106027}", null, new object[0]);
		}, null);
	}

	// Token: 0x06004A58 RID: 19032 RVA: 0x00185D34 File Offset: 0x00183F34
	public void SwapPage()
	{
		TweenPosition mainAnima = this.MainAnima;
		this.MainAnima = this.TempAnima;
		this.TempAnima = mainAnima;
		GuildStarMapLogic mainPage = this.MainPage;
		this.MainPage = this.TempPage;
		this.TempPage = mainPage;
	}

	// Token: 0x06004A59 RID: 19033 RVA: 0x00185D78 File Offset: 0x00183F78
	public void SetDirBtn()
	{
		if (this.CurShowIndex == 1)
		{
			NGUITools.SetActive(this.LeftSp.gameObject, false);
		}
		else
		{
			NGUITools.SetActive(this.LeftSp.gameObject, true);
		}
		if (this.CurShowIndex == this.MaxStarMapNum)
		{
			NGUITools.SetActive(this.RightSp.gameObject, false);
		}
		else
		{
			NGUITools.SetActive(this.RightSp.gameObject, true);
		}
	}

	// Token: 0x06004A5A RID: 19034 RVA: 0x00185DF0 File Offset: 0x00183FF0
	public void GetCurShowData(int curMapID)
	{
		this.CurStarInfo = null;
		this.CurStarData = null;
		bool flag = false;
		for (int i = this.GuildstarsInfo.Count - 1; i >= 0; i--)
		{
			GuildStarData guildStarDataById = DataManager.GetGuildStarDataById(this.GuildstarsInfo[i].ID);
			if (guildStarDataById != null && guildStarDataById.MapID == curMapID)
			{
				if (!flag)
				{
					flag = true;
					this.CurMapIsComplete = true;
					this.CurStarInfo = this.GuildstarsInfo[i];
					this.CurStarData = guildStarDataById;
				}
				if (this.GuildstarsInfo[i].HasState && this.GuildstarsInfo[i].state == 0L)
				{
					this.CurMapIsComplete = false;
					this.CurStarInfo = this.GuildstarsInfo[i];
					this.CurStarData = guildStarDataById;
				}
			}
		}
	}

	// Token: 0x040037CC RID: 14284
	public UILabel MapNameLabel;

	// Token: 0x040037CD RID: 14285
	public UILabel CoinLabel;

	// Token: 0x040037CE RID: 14286
	public UITexture MapBgTexture;

	// Token: 0x040037CF RID: 14287
	public UITexture MapXingpanDi;

	// Token: 0x040037D0 RID: 14288
	public UILabel NeedLevelLabel;

	// Token: 0x040037D1 RID: 14289
	public UISprite AttSp1;

	// Token: 0x040037D2 RID: 14290
	public UILabel Attname1;

	// Token: 0x040037D3 RID: 14291
	public UILabel AttInfo1;

	// Token: 0x040037D4 RID: 14292
	public UISprite AttSp2;

	// Token: 0x040037D5 RID: 14293
	public UILabel Attname2;

	// Token: 0x040037D6 RID: 14294
	public UILabel AttInfo2;

	// Token: 0x040037D7 RID: 14295
	public UISprite Cost1quaSp;

	// Token: 0x040037D8 RID: 14296
	public UISprite Cost1Sp;

	// Token: 0x040037D9 RID: 14297
	public UILabel Cost1Label;

	// Token: 0x040037DA RID: 14298
	public UISprite Cost2quaSp;

	// Token: 0x040037DB RID: 14299
	public UISprite Cost2Sp;

	// Token: 0x040037DC RID: 14300
	public UILabel Cost2Label;

	// Token: 0x040037DD RID: 14301
	public UILabel CostNameLabel;

	// Token: 0x040037DE RID: 14302
	public UISprite CompleteFlag;

	// Token: 0x040037DF RID: 14303
	public UISprite upgradeBtn;

	// Token: 0x040037E0 RID: 14304
	private List<guild_star> GuildstarsInfo = new List<guild_star>();

	// Token: 0x040037E1 RID: 14305
	private Dictionary<string, guild_star> GuildstarsDic = new Dictionary<string, guild_star>();

	// Token: 0x040037E2 RID: 14306
	private guild_star CurStarInfo;

	// Token: 0x040037E3 RID: 14307
	private GuildStarData CurStarData;

	// Token: 0x040037E4 RID: 14308
	private bool CurMapIsComplete;

	// Token: 0x040037E5 RID: 14309
	private List<string> textureList = new List<string>();

	// Token: 0x040037E6 RID: 14310
	public TweenPosition MainAnima;

	// Token: 0x040037E7 RID: 14311
	public TweenPosition TempAnima;

	// Token: 0x040037E8 RID: 14312
	public GuildStarMapLogic MainPage;

	// Token: 0x040037E9 RID: 14313
	public GuildStarMapLogic TempPage;

	// Token: 0x040037EA RID: 14314
	public UISprite LeftSp;

	// Token: 0x040037EB RID: 14315
	public UISprite RightSp;

	// Token: 0x040037EC RID: 14316
	private int CurShowIndex;

	// Token: 0x040037ED RID: 14317
	private int MaxStarMapNum = 12;

	// Token: 0x040037EE RID: 14318
	private Vector3 LeftPos = new Vector3(-400f, 0f, 0f);

	// Token: 0x040037EF RID: 14319
	private Vector3 rightPos = new Vector3(400f, 0f, 0f);

	// Token: 0x040037F0 RID: 14320
	private float MoveTime = 0.5f;

	// Token: 0x040037F1 RID: 14321
	private int NeedMapIndex;
}
