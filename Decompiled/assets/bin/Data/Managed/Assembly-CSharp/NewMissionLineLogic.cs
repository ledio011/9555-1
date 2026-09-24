using System;
using UnityEngine;

// Token: 0x02000A31 RID: 2609
public class NewMissionLineLogic : MonoBehaviour
{
	// Token: 0x06004C0F RID: 19471 RVA: 0x0019BA0C File Offset: 0x00199C0C
	public void RegisterOnClickLine(NewMissionLineLogic.OnClickNewMissionLine func)
	{
		this.onClickLine = func;
	}

	// Token: 0x17000FCC RID: 4044
	// (get) Token: 0x06004C10 RID: 19472 RVA: 0x0019BA18 File Offset: 0x00199C18
	public string curMissionId
	{
		get
		{
			return this.curMissionData.ID;
		}
	}

	// Token: 0x17000FCD RID: 4045
	// (get) Token: 0x06004C11 RID: 19473 RVA: 0x0019BA28 File Offset: 0x00199C28
	public MissionData CurMissionData
	{
		get
		{
			return this.curMissionData;
		}
	}

	// Token: 0x06004C12 RID: 19474 RVA: 0x0019BA30 File Offset: 0x00199C30
	public void ResetLine(MissionData curData, NewMissionLineLogic.OnClickNewMissionLine func)
	{
		this.curMissionData = curData;
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		MISSION_STATE missionState = missionManager.GetMissionState(this.curMissionData.ID);
		this.IconPic.spriteName = GameDefine.MAP_ACTIVITY_MISSION_ICON[this.curMissionData.Class];
		if (missionManager.IsMissionAccepted(this.curMissionData.ID))
		{
			if (missionState == MISSION_STATE.ACCEPTED)
			{
				this.StateIcon.spriteName = "CZ_renWu_wenHao_hui";
				this.StateIcon.width = 20;
				this.StateIcon.height = 32;
			}
			else if (missionState == MISSION_STATE.COMPLETE)
			{
				this.StateIcon.spriteName = "CZ_renWu_wenHao";
				this.StateIcon.width = 20;
				this.StateIcon.height = 32;
			}
		}
		else
		{
			this.StateIcon.spriteName = "CZ_renWu_tanHao";
			this.StateIcon.width = 10;
			this.StateIcon.height = 37;
		}
		string text = string.Empty;
		switch (this.curMissionData.Class)
		{
		case 0:
			text = StrDictionary.GetDictionaryString("#{100168}", new object[]
			{
				missionManager.GetMissionParam(this.curMissionData.ID, 3) + 1L
			});
			break;
		case 1:
			text = StrDictionary.GetDictionaryString("#{100169}", new object[0]);
			break;
		case 2:
			text = StrDictionary.GetDictionaryString("#{100170}", new object[0]);
			break;
		case 3:
		case 6:
			text = StrDictionary.GetDictionaryString("#{100171}", new object[0]);
			break;
		case 4:
			text = StrDictionary.GetDictionaryString("#{100172}", new object[0]);
			break;
		case 5:
			text = StrDictionary.GetDictionaryString("#{100173}", new object[0]);
			break;
		case 7:
			text = StrDictionary.GetDictionaryString("#{100200}", new object[0]);
			break;
		case 8:
			text = StrDictionary.GetDictionaryString("#{100001}", new object[0]);
			break;
		default:
			text = "[Need Loc]";
			break;
		}
		if (this.curMissionData.Class == 0)
		{
			this.TitleLabel.text = string.Concat(new string[]
			{
				StrDictionary.GetDictionaryString("#{100171}", new object[0]),
				"[FDAE33]",
				StrDictionary.GetDictionaryString(this.curMissionData.TipDescribeID, new object[0]),
				text,
				"[-]"
			});
			MissionManager.GetMissionStateLabel(this.curMissionData, missionState, this.StateLabel);
		}
		else if (this.curMissionData.Class == 8)
		{
			TimeLimitMissionData timeLimitMissionDataByID = DataManager.GetTimeLimitMissionDataByID(this.curMissionData.TimeLimitId);
			this.TitleLabel.text = text + "[FDAE33]" + timeLimitMissionDataByID.MName + "[-]";
			if (missionState == MISSION_STATE.INVALID)
			{
				this.StateLabel.text = timeLimitMissionDataByID.MTip;
			}
			else
			{
				MissionManager.GetMissionStateLabel(this.curMissionData, missionState, this.StateLabel);
			}
		}
		else
		{
			this.TitleLabel.text = text + "[FDAE33]" + StrDictionary.GetDictionaryString(this.curMissionData.TipDescribeID, new object[0]) + "[-]";
			MissionManager.GetMissionStateLabel(this.curMissionData, missionState, this.StateLabel);
		}
		if (missionState == MISSION_STATE.COMPLETE)
		{
			if (UnityVersionUtil.IsActive(base.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.CompletePic.gameObject, true);
			}
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.CompletePic.gameObject, false);
		}
		this.InfoTween.ResetToBeginning();
		this.ArrowTween.ResetToBeginning();
		this.OpenFlag = false;
		this.onClickLine = func;
		this.DisableSelect();
	}

	// Token: 0x06004C13 RID: 19475 RVA: 0x0019BDE4 File Offset: 0x00199FE4
	public void OnClickItemLine()
	{
		if (!this.OpenFlag)
		{
			this.OpenLine();
		}
		else
		{
			this.CloseLine(false);
		}
		if (this.onClickLine != null)
		{
			this.onClickLine(this);
		}
	}

	// Token: 0x06004C14 RID: 19476 RVA: 0x0019BE28 File Offset: 0x0019A028
	public void OpenLine()
	{
		this.OpenFlag = true;
		this.MissionInfoPage.Reset(this.curMissionData.ID);
		this.ArrowTween.PlayForward();
		this.InfoTween.PlayForward();
		this.EnableSelect();
	}

	// Token: 0x06004C15 RID: 19477 RVA: 0x0019BE64 File Offset: 0x0019A064
	public void CloseLine(bool directClose = false)
	{
		this.OpenFlag = false;
		if (directClose)
		{
			this.ArrowTween.ResetToBeginning();
			this.InfoTween.ResetToBeginning();
			this.mTable.Reposition();
		}
		else
		{
			this.ArrowTween.PlayReverse();
			this.InfoTween.PlayReverse();
		}
		this.DisableSelect();
	}

	// Token: 0x06004C16 RID: 19478 RVA: 0x0019BEC0 File Offset: 0x0019A0C0
	public void EnableSelect()
	{
		this.BottomLinePic.spriteName = "CZ_huaDongBG_1";
	}

	// Token: 0x06004C17 RID: 19479 RVA: 0x0019BED4 File Offset: 0x0019A0D4
	public void DisableSelect()
	{
		this.BottomLinePic.spriteName = "CZ_huaDongBG";
	}

	// Token: 0x040039C7 RID: 14791
	public UISprite IconPic;

	// Token: 0x040039C8 RID: 14792
	public UISprite StateIcon;

	// Token: 0x040039C9 RID: 14793
	public UISprite CompletePic;

	// Token: 0x040039CA RID: 14794
	public UILabel TitleLabel;

	// Token: 0x040039CB RID: 14795
	public UILabel StateLabel;

	// Token: 0x040039CC RID: 14796
	public TweenRotation ArrowTween;

	// Token: 0x040039CD RID: 14797
	public TweenScale InfoTween;

	// Token: 0x040039CE RID: 14798
	public UISprite BottomLinePic;

	// Token: 0x040039CF RID: 14799
	public NewMissionInfoRootLogic MissionInfoPage;

	// Token: 0x040039D0 RID: 14800
	public UIMyCenterOnChild MyCenterOn;

	// Token: 0x040039D1 RID: 14801
	public UITable mTable;

	// Token: 0x040039D2 RID: 14802
	private NewMissionLineLogic.OnClickNewMissionLine onClickLine;

	// Token: 0x040039D3 RID: 14803
	private bool OpenFlag;

	// Token: 0x040039D4 RID: 14804
	private MissionData curMissionData;

	// Token: 0x02000B02 RID: 2818
	// (Invoke) Token: 0x06005091 RID: 20625
	public delegate void OnClickNewMissionLine(NewMissionLineLogic line);
}
