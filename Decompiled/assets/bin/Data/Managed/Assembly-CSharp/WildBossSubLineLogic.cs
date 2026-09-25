using System;
using SprotoType;
using UnityEngine;

// Token: 0x020008FD RID: 2301
public class WildBossSubLineLogic : MonoBehaviour
{
	// Token: 0x06003ED4 RID: 16084 RVA: 0x00121A7C File Offset: 0x0011FC7C
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06003ED5 RID: 16085 RVA: 0x00121A88 File Offset: 0x0011FC88
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x06003ED6 RID: 16086 RVA: 0x00121AA8 File Offset: 0x0011FCA8
	public void ResetItem(int infoindex, activity_info info, DelegateDefine.TwoIntParamDelegate clickFunc)
	{
		if (info.Type == 5L)
		{
			this.curIndex = infoindex;
			PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			WildBossData wildBossDataByID = DataManager.GetWildBossDataByID(info.ID);
			NpcData npcDataByID = DataManager.GetNpcDataByID(wildBossDataByID.BossID);
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			this.enableLineFlag = 0;
			this.IconSprite.spriteName = wildBossDataByID.Icon;
			this.NameLabel.text = StrDictionary.GetDictionaryString(npcDataByID.Name, new object[0]);
			this.LevelLabel.text = string.Format("{0}: {1}-{2}", StrDictionary.GetDictionaryString("#{100771}", new object[0]), wildBossDataByID.LevelMin, wildBossDataByID.LevelMax);
			int num = (int)info.CurNum;
			if (info.State == 2L)
			{
				this.CompleteSprite.enabled = true;
				this.enableLineFlag = 2;
			}
			else
			{
				this.CompleteSprite.enabled = false;
			}
			if (info.State == 0L && playerCommonData.GetCurServerTime() < info.Parm)
			{
				this.enableLineFlag = 2;
			}
			if (num <= 0 && playerCommonData.GetCurServerTime() > info.time + 5400L)
			{
				this.enableLineFlag = 3;
			}
			if (this.CheckLevel(wildBossDataByID.LevelMin, wildBossDataByID.LevelMax))
			{
				this.SetLabelWarining(this.LevelLabel, false, false);
			}
			else
			{
				this.SetLabelWarining(this.LevelLabel, true, false);
				this.enableLineFlag = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckIsLevelHeigh(wildBossDataByID.LevelMin, wildBossDataByID.LevelMax);
			}
			this.RefreshSelect(-1);
			this.onClickItem = clickFunc;
		}
	}

	// Token: 0x06003ED7 RID: 16087 RVA: 0x00121C54 File Offset: 0x0011FE54
	public bool CheckLevel(int minLevel, int maxlevel)
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(minLevel, maxlevel);
	}

	// Token: 0x06003ED8 RID: 16088 RVA: 0x00121C68 File Offset: 0x0011FE68
	public void OnClikcItemBtn()
	{
		if (this.onClickItem != null)
		{
			this.onClickItem(this.curIndex, this.enableLineFlag);
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.WORLD_BOSS_CHOOSE_COPY)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06003ED9 RID: 16089 RVA: 0x00121CAC File Offset: 0x0011FEAC
	private void SetLabelWarining(UILabel label, bool needWarning, bool isLowWarning = false)
	{
		if (needWarning)
		{
			if (isLowWarning)
			{
				label.color = new Color(0.537f, 0.537f, 0.537f, 1f);
			}
			else
			{
				label.color = Color.red;
			}
		}
		else
		{
			label.color = Color.white;
		}
	}

	// Token: 0x06003EDA RID: 16090 RVA: 0x00121D04 File Offset: 0x0011FF04
	public void RefreshSelect(int index)
	{
		if (this.enableLineFlag == 0)
		{
			this.SelectBkSprite.spriteName = "CZ_wuPinYanSe_3";
		}
		else
		{
			this.SelectBkSprite.spriteName = "CZ_tongYongDi_zhuYao_4_1";
		}
		if (index != this.curIndex)
		{
			UnityVersionUtil.SetActiveRecursive(this.SelectUpSprite, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.SelectUpSprite, true);
		}
	}

	// Token: 0x04002A95 RID: 10901
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04002A96 RID: 10902
	public UILabel NameLabel;

	// Token: 0x04002A97 RID: 10903
	public UISprite IconSprite;

	// Token: 0x04002A98 RID: 10904
	public UILabel LevelLabel;

	// Token: 0x04002A99 RID: 10905
	public UISprite SelectBkSprite;

	// Token: 0x04002A9A RID: 10906
	public UISprite CompleteSprite;

	// Token: 0x04002A9B RID: 10907
	public DelegateDefine.TwoIntParamDelegate onClickItem;

	// Token: 0x04002A9C RID: 10908
	public GameObject SelectUpSprite;

	// Token: 0x04002A9D RID: 10909
	public int enableLineFlag;

	// Token: 0x04002A9E RID: 10910
	public int curIndex = -1;
}
