using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008FC RID: 2300
public class WildBossRootLogic : SingletonUnity<WildBossRootLogic>
{
	// Token: 0x06003EC5 RID: 16069 RVA: 0x001212C0 File Offset: 0x0011F4C0
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06003EC6 RID: 16070 RVA: 0x001212CC File Offset: 0x0011F4CC
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x06003EC7 RID: 16071 RVA: 0x001212EC File Offset: 0x0011F4EC
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x06003EC8 RID: 16072 RVA: 0x001212F8 File Offset: 0x0011F4F8
	public void EnableReset()
	{
		for (int i = 0; i < this.wildBossLines.Count; i++)
		{
			NGUITools.SetActive(this.wildBossLines[i].gameObject, false);
		}
		UnityVersionUtil.SetActiveRecursive(this.wildBossLines[0].Sublineobj, false);
		UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemScripts.gameObject, false);
		this.curBossInfo = null;
		this.curWildBossData = null;
	}

	// Token: 0x06003EC9 RID: 16073 RVA: 0x00121370 File Offset: 0x0011F570
	public void RefershBossInfo(ret_request_wild_boss_info.request request)
	{
		this.wildBossInfos = new List<activity_info>(request.activity_info.Values);
		this.wildBossInfos.Sort((activity_info x, activity_info y) => int.Parse(x.ID) - int.Parse(y.ID));
		int num = 1;
		int num2 = num - this.wildBossLines.Count;
		int count = this.wildBossLines.Count;
		if (num2 > 0)
		{
			for (int i = 0; i < num2; i++)
			{
				GameObject gameObject = Object.Instantiate(this.wildBossLines[0].gameObject) as GameObject;
				gameObject.name = string.Format("huaDongTiao_{0}", count + i);
				WildBossLineLogic component = gameObject.GetComponent<WildBossLineLogic>();
				if (component != null)
				{
					component.transform.parent = this.wildBossLines[0].transform.parent;
					component.transform.localScale = Vector3.one;
					component.transform.localPosition = Vector3.zero;
					this.wildBossLines.Add(component);
				}
			}
		}
		for (int j = 0; j < this.wildBossLines.Count; j++)
		{
			NGUITools.SetActive(this.wildBossLines[j].gameObject, true);
			this.wildBossLines[j].ResetItem(this.wildBossInfos, new DelegateDefine.TwoIntParamDelegate(this.OnClickItemBtn));
		}
		this.mCurChoosedIndex = -1;
		this.RootTable.Reposition();
		this.uiScrollView.ResetPosition();
		this.wildBossLines[0].OnClickItemBtn();
	}

	// Token: 0x06003ECA RID: 16074 RVA: 0x00121518 File Offset: 0x0011F718
	public void OnClickItemBtn(int index, int isenable)
	{
		if (this.mCurChoosedIndex == index)
		{
			return;
		}
		this.mCurChoosedIndex = index;
		this.curBossInfo = this.wildBossInfos[index];
		ShowRewardData showRewardData = null;
		string text = string.Empty;
		if (this.curBossInfo.Type == 5L)
		{
			this.curWildBossData = DataManager.GetWildBossDataByID(this.curBossInfo.ID);
			showRewardData = DataManager.GetShowRewardDataByID(this.curWildBossData.ShowRewardID);
			text = StrDictionary.GetDictionaryString(this.curWildBossData.Desc, new object[0]);
		}
		this.DescLabel.text = text;
		if (this.curBossInfo.Parmstr != null)
		{
			string[] array = this.curBossInfo.Parmstr.Split(new char[]
			{
				'#'
			});
			for (int i = 0; i < this.rankLabels.Count; i++)
			{
				if (i < array.Length)
				{
					if (!array[i].Equals(string.Empty))
					{
						string[] array2 = array[i].Split(new char[]
						{
							'@'
						});
						this.rankLabels[i].text = string.Format("{0}", array2[1]);
					}
					else
					{
						this.rankLabels[i].text = string.Format("{0}", "- - - -");
					}
				}
				else
				{
					this.rankLabels[i].text = string.Format("{0}", "- - - -");
				}
			}
		}
		else
		{
			for (int j = 0; j < this.rankLabels.Count; j++)
			{
				this.rankLabels[j].text = string.Format("{0}", "- - - -");
			}
		}
		if (showRewardData != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemScripts.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardData.ItemIdList), new List<EQUIP_QUALITY>(showRewardData.QualityList), new List<int>(showRewardData.CountList));
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemScripts.gameObject, false);
		}
		if (isenable == 0)
		{
			this.startBtnSp.spriteName = "CZ_anNiu_2";
		}
		else
		{
			this.startBtnSp.spriteName = "CZ_anNiu_2+";
		}
		this.canStartFlag = isenable;
		this.UpdateSelectItem();
		if (GameSettingData.GetPhoneClass() == 0)
		{
			if (this.CopyBG.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(BundleManager.LoadTexture(GameDefine.CopyBGNameDefault, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
			}
		}
		else if (this.curWildBossData != null && (this.CopyBG.mainTexture == null || !this.CopyBG.mainTexture.name.Equals(this.curWildBossData.Background)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.curWildBossData.Background, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06003ECB RID: 16075 RVA: 0x00121830 File Offset: 0x0011FA30
	private void TextureLoadFinish(string name, Texture tex)
	{
		this.CopyBG.mainTexture = tex;
	}

	// Token: 0x06003ECC RID: 16076 RVA: 0x00121840 File Offset: 0x0011FA40
	public void UpdateSelectItem()
	{
		for (int i = 0; i < this.wildBossLines.Count; i++)
		{
			this.wildBossLines[i].RefreshSelect(this.mCurChoosedIndex);
		}
	}

	// Token: 0x06003ECD RID: 16077 RVA: 0x00121880 File Offset: 0x0011FA80
	private void SetRewardItem(List<string> itemIds, List<EQUIP_QUALITY> qualitys, List<int> counts)
	{
		this.ShowRewardItemScripts.ShowRewards(itemIds, qualitys, counts);
	}

	// Token: 0x06003ECE RID: 16078 RVA: 0x00121890 File Offset: 0x0011FA90
	public void OnClickStart()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.WORLD_BOSS_CLICK_START)
		{
			this.CheckTutorialEvent();
		}
		if (this.canStartFlag != 0)
		{
			switch (this.canStartFlag)
			{
			case 1:
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
				break;
			case 2:
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101541}", new object[0]), true, false);
				break;
			case 3:
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{102057}", new object[0]), true, false);
				break;
			case 6:
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
				break;
			case 7:
				TutorialManager.LevelLimitAction();
				break;
			}
			return;
		}
		if (this.curBossInfo != null && this.curBossInfo.Type == 5L)
		{
			enter_wild_boss.request request = new enter_wild_boss.request();
			request.ID = this.curBossInfo.ID;
			WaitResponseUIRootLogic.OpenWaitBox(201, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.enter_wild_boss>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("wildboss", string.Format("wildboss_{0}", this.curBossInfo.ID), "starttimes");
		}
	}

	// Token: 0x06003ECF RID: 16079 RVA: 0x001219E4 File Offset: 0x0011FBE4
	public void OnClicktishiBtn()
	{
		if (this.curWildBossData != null)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
			{
				SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", this.curWildBossData.Rule, null, new object[0]);
			}, null);
		}
	}

	// Token: 0x06003ED0 RID: 16080 RVA: 0x00121A10 File Offset: 0x0011FC10
	private void OnDisable()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.WORLD_BOSS_CLICK_START)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x04002A85 RID: 10885
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04002A86 RID: 10886
	public UIScrollView uiScrollView;

	// Token: 0x04002A87 RID: 10887
	public List<WildBossLineLogic> wildBossLines = new List<WildBossLineLogic>();

	// Token: 0x04002A88 RID: 10888
	private List<activity_info> wildBossInfos = new List<activity_info>();

	// Token: 0x04002A89 RID: 10889
	private activity_info curBossInfo;

	// Token: 0x04002A8A RID: 10890
	public ShowRewardItems ShowRewardItemScripts;

	// Token: 0x04002A8B RID: 10891
	public UILabel DescLabel;

	// Token: 0x04002A8C RID: 10892
	public UITable RootTable;

	// Token: 0x04002A8D RID: 10893
	private int mCurChoosedIndex = -1;

	// Token: 0x04002A8E RID: 10894
	public UISprite startBtnSp;

	// Token: 0x04002A8F RID: 10895
	private int canStartFlag;

	// Token: 0x04002A90 RID: 10896
	public List<UILabel> rankLabels;

	// Token: 0x04002A91 RID: 10897
	public UISprite tishiFlag;

	// Token: 0x04002A92 RID: 10898
	private WildBossData curWildBossData;

	// Token: 0x04002A93 RID: 10899
	public UITexture CopyBG;
}
