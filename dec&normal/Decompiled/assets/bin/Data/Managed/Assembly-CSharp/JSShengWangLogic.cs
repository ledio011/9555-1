using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200094D RID: 2381
public class JSShengWangLogic : SingletonUnity<JSShengWangLogic>
{
	// Token: 0x06004298 RID: 17048 RVA: 0x00145920 File Offset: 0x00143B20
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004299 RID: 17049 RVA: 0x0014592C File Offset: 0x00143B2C
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x0600429A RID: 17050 RVA: 0x0014594C File Offset: 0x00143B4C
	private void Start()
	{
	}

	// Token: 0x0600429B RID: 17051 RVA: 0x00145950 File Offset: 0x00143B50
	public void Reset()
	{
		this.TitleDataList = DataManager.GetTitleList();
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.playerHonor = playerData.MainPlayerAttrData.CurTitleExp;
		this.playerHonorLevel = playerData.MainPlayerAttrData.CurTitleLevel;
		for (int i = 0; i < this.Titleitemlist.Count; i++)
		{
			if (i < this.TitleDataList.Count)
			{
				this.Titleitemlist[i].reset(i + 1, this.TitleDataList[i + 1], this.playerHonorLevel, new DelegateDefine.OneIntParamDelegate(this.OnClickTitleItem));
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.Titleitemlist[i].gameObject, false);
			}
		}
		this.CurTitleIndex = -1;
		if (this.playerHonorLevel < 10)
		{
			this.OnClickTitleItem(this.playerHonorLevel + 1);
		}
		else
		{
			this.OnClickTitleItem(10);
		}
		this.SetAttribute();
	}

	// Token: 0x0600429C RID: 17052 RVA: 0x00145A48 File Offset: 0x00143C48
	public void OnClickTitleItem(int chooseindex)
	{
		if (this.CurTitleIndex == chooseindex)
		{
			return;
		}
		this.CurTitleIndex = chooseindex;
		this.Titleitemlist[this.CurTitleIndex - 1].SetSelect(this.selectSpTra);
		for (int i = 0; i < this.Titleitemlist.Count; i++)
		{
			if (this.CurTitleIndex - 1 == i)
			{
				this.Titleitemlist[i].transform.localScale = Vector3.one;
			}
			else
			{
				this.Titleitemlist[i].transform.localScale = Vector3.one * 0.8f;
			}
		}
		this.curPromoteType = PROMOTE_TYPE.NORMAL;
		this.UpdateAttribute();
		this.curTitleSp.spriteName = this.TitleDataList[this.CurTitleIndex].Icon;
		this.curTitleSp.MakePixelPerfect();
		this.curTitleNameLabel.text = StrDictionary.GetDictionaryString(this.TitleDataList[this.CurTitleIndex].Name, new object[0]);
		int curALLHonor = this.GetCurALLHonor();
		this.curHonorLabel.text = string.Format("{0}/{1}", this.playerHonor, curALLHonor);
		if (this.playerHonor < curALLHonor)
		{
			this.SetPromoteBtn(PROMOTE_TYPE.HONOR_LIMIT);
		}
		else if (this.playerHonorLevel >= this.CurTitleIndex)
		{
			this.SetPromoteBtn(PROMOTE_TYPE.LEVEL_GET);
		}
		else if (this.playerHonorLevel == this.CurTitleIndex - 1)
		{
			this.SetPromoteBtn(PROMOTE_TYPE.NORMAL);
		}
		else
		{
			this.SetPromoteBtn(PROMOTE_TYPE.LEVEL_LIMIT);
		}
	}

	// Token: 0x0600429D RID: 17053 RVA: 0x00145BE4 File Offset: 0x00143DE4
	public void OnClickPromoteBtn()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.TITLE_CLICK)
		{
			this.CheckTutorialEvent();
		}
		if (this.curPromoteType != PROMOTE_TYPE.NORMAL)
		{
			switch (this.curPromoteType)
			{
			case PROMOTE_TYPE.HONOR_LIMIT:
				NoticeLogic.AddNotifyData("#{101705}", true, false);
				break;
			case PROMOTE_TYPE.LEVEL_LIMIT:
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101706}", new object[]
				{
					StrDictionary.GetDictionaryString(this.TitleDataList[this.playerHonorLevel + 1].Name, new object[0])
				}), true, false);
				break;
			}
			return;
		}
		WaitResponseUIRootLogic.OpenWaitBox(156, 10f, 0f, null);
		NetLogic.GetInstance().Send<Protocol.title_req_level_up>(null, null);
	}

	// Token: 0x0600429E RID: 17054 RVA: 0x00145CA8 File Offset: 0x00143EA8
	public void SetPromoteBtn(PROMOTE_TYPE isenable)
	{
		this.curPromoteType = isenable;
		if (isenable == PROMOTE_TYPE.NORMAL)
		{
			this.promoteBtnsp.spriteName = "CZ_anNiu_2";
		}
		else
		{
			this.promoteBtnsp.spriteName = "CZ_anNiu_2+";
		}
		if (isenable == PROMOTE_TYPE.LEVEL_GET)
		{
			NGUITools.SetActive(this.promoteBtnsp.gameObject, false);
		}
		else
		{
			NGUITools.SetActive(this.promoteBtnsp.gameObject, true);
		}
	}

	// Token: 0x0600429F RID: 17055 RVA: 0x00145D18 File Offset: 0x00143F18
	public void OnClickHonur()
	{
		ItemData itemDataByID = DataManager.GetItemDataByID("2002");
		ItemInfoRootLogicNew.ShowItemTips(itemDataByID, false, UI_PAGE_TYPE.INVALID);
	}

	// Token: 0x060042A0 RID: 17056 RVA: 0x00145D38 File Offset: 0x00143F38
	public int GetCurALLHonor()
	{
		return this.TitleDataList[this.CurTitleIndex - 1].EXP;
	}

	// Token: 0x060042A1 RID: 17057 RVA: 0x00145D54 File Offset: 0x00143F54
	public void UpdateAttribute()
	{
		TitleData titleData = this.TitleDataList[this.CurTitleIndex];
		TitleData titleData2 = this.TitleDataList[this.CurTitleIndex];
		this.ATKValLabel.text = GameDefine.GetAttributeValueStr(titleData2.Status1, titleData2.Value1);
		this.HPValLabel.text = GameDefine.GetAttributeValueStr(titleData2.Status2, titleData2.Value2);
		this.DEFValLabel.text = GameDefine.GetAttributeValueStr(titleData2.Status3, titleData2.Value3);
		this.HITValLabel.text = GameDefine.GetAttributeValueStr(titleData2.Status4, titleData2.Value4);
		this.DGEValLabel.text = GameDefine.GetAttributeValueStr(titleData2.Status5, titleData2.Value5);
		this.CRIValLabel.text = GameDefine.GetAttributeValueStr(titleData2.Status6, titleData2.Value6);
		this.RESValLabel.text = GameDefine.GetAttributeValueStr(titleData2.Status7, titleData2.Value7);
		this.EXDValLabel.text = GameDefine.GetAttributeValueStr(titleData2.Status8, titleData2.Value8);
		this.EXRValLabel.text = GameDefine.GetAttributeValueStr(titleData2.Status9, titleData2.Value9);
		this.TenValLabel.text = GameDefine.GetAttributeValueStr(titleData2.Status10, titleData2.Value10);
	}

	// Token: 0x060042A2 RID: 17058 RVA: 0x00145EA0 File Offset: 0x001440A0
	public void SetAttribute()
	{
		TitleData titleData = this.TitleDataList[this.CurTitleIndex];
		this.ATKLabel.text = GameDefine.GetAttributeName_S(titleData.Status1);
		this.HPLabel.text = GameDefine.GetAttributeName_S(titleData.Status2);
		this.DEFLabel.text = GameDefine.GetAttributeName_S(titleData.Status3);
		this.HITLabel.text = GameDefine.GetAttributeName_S(titleData.Status4);
		this.DGELabel.text = GameDefine.GetAttributeName_S(titleData.Status5);
		this.CRILabel.text = GameDefine.GetAttributeName_S(titleData.Status6);
		this.RESLabel.text = GameDefine.GetAttributeName_S(titleData.Status7);
		this.EXDLabel.text = GameDefine.GetAttributeName_S(titleData.Status8);
		this.EXRLabel.text = GameDefine.GetAttributeName_S(titleData.Status9);
		this.TenLabel.text = GameDefine.GetAttributeName_S(titleData.Status10);
		this.ATKsp.spriteName = GameDefine.GetAttributeIcon(titleData.Status1);
		this.HPsp.spriteName = GameDefine.GetAttributeIcon(titleData.Status2);
		this.DEFsp.spriteName = GameDefine.GetAttributeIcon(titleData.Status3);
		this.HITsp.spriteName = GameDefine.GetAttributeIcon(titleData.Status4);
		this.DGEsp.spriteName = GameDefine.GetAttributeIcon(titleData.Status5);
		this.CRIsp.spriteName = GameDefine.GetAttributeIcon(titleData.Status6);
		this.RESsp.spriteName = GameDefine.GetAttributeIcon(titleData.Status7);
		this.EXDsp.spriteName = GameDefine.GetAttributeIcon(titleData.Status8);
		this.EXRsp.spriteName = GameDefine.GetAttributeIcon(titleData.Status9);
		this.Tensp.spriteName = GameDefine.GetAttributeIcon(titleData.Status10);
	}

	// Token: 0x060042A3 RID: 17059 RVA: 0x00146078 File Offset: 0x00144278
	private void OnDisable()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.TITLE_CLICK)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x04002EBF RID: 11967
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04002EC0 RID: 11968
	private List<TitleData> TitleDataList;

	// Token: 0x04002EC1 RID: 11969
	private int CurTitleIndex;

	// Token: 0x04002EC2 RID: 11970
	public List<TitleItemLogic> Titleitemlist;

	// Token: 0x04002EC3 RID: 11971
	public UISprite curTitleSp;

	// Token: 0x04002EC4 RID: 11972
	public UILabel curTitleNameLabel;

	// Token: 0x04002EC5 RID: 11973
	public UILabel curHonorLabel;

	// Token: 0x04002EC6 RID: 11974
	public UISprite promoteBtnsp;

	// Token: 0x04002EC7 RID: 11975
	public Transform selectSpTra;

	// Token: 0x04002EC8 RID: 11976
	private PROMOTE_TYPE curPromoteType;

	// Token: 0x04002EC9 RID: 11977
	private int playerHonorLevel;

	// Token: 0x04002ECA RID: 11978
	private int playerHonor;

	// Token: 0x04002ECB RID: 11979
	public UISprite ATKsp;

	// Token: 0x04002ECC RID: 11980
	public UISprite HPsp;

	// Token: 0x04002ECD RID: 11981
	public UISprite DEFsp;

	// Token: 0x04002ECE RID: 11982
	public UISprite HITsp;

	// Token: 0x04002ECF RID: 11983
	public UISprite DGEsp;

	// Token: 0x04002ED0 RID: 11984
	public UISprite CRIsp;

	// Token: 0x04002ED1 RID: 11985
	public UISprite RESsp;

	// Token: 0x04002ED2 RID: 11986
	public UISprite EXDsp;

	// Token: 0x04002ED3 RID: 11987
	public UISprite EXRsp;

	// Token: 0x04002ED4 RID: 11988
	public UISprite Tensp;

	// Token: 0x04002ED5 RID: 11989
	public UILabel ATKValLabel;

	// Token: 0x04002ED6 RID: 11990
	public UILabel HPValLabel;

	// Token: 0x04002ED7 RID: 11991
	public UILabel DEFValLabel;

	// Token: 0x04002ED8 RID: 11992
	public UILabel HITValLabel;

	// Token: 0x04002ED9 RID: 11993
	public UILabel DGEValLabel;

	// Token: 0x04002EDA RID: 11994
	public UILabel CRIValLabel;

	// Token: 0x04002EDB RID: 11995
	public UILabel RESValLabel;

	// Token: 0x04002EDC RID: 11996
	public UILabel EXDValLabel;

	// Token: 0x04002EDD RID: 11997
	public UILabel EXRValLabel;

	// Token: 0x04002EDE RID: 11998
	public UILabel TenValLabel;

	// Token: 0x04002EDF RID: 11999
	public UILabel ATKLabel;

	// Token: 0x04002EE0 RID: 12000
	public UILabel HPLabel;

	// Token: 0x04002EE1 RID: 12001
	public UILabel DEFLabel;

	// Token: 0x04002EE2 RID: 12002
	public UILabel HITLabel;

	// Token: 0x04002EE3 RID: 12003
	public UILabel DGELabel;

	// Token: 0x04002EE4 RID: 12004
	public UILabel CRILabel;

	// Token: 0x04002EE5 RID: 12005
	public UILabel RESLabel;

	// Token: 0x04002EE6 RID: 12006
	public UILabel EXDLabel;

	// Token: 0x04002EE7 RID: 12007
	public UILabel EXRLabel;

	// Token: 0x04002EE8 RID: 12008
	public UILabel TenLabel;
}
