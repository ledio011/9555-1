using System;

// Token: 0x0200095C RID: 2396
public class MissionTipRootLogic : SingletonUnity<MissionTipRootLogic>
{
	// Token: 0x06004314 RID: 17172 RVA: 0x0014A008 File Offset: 0x00148208
	public static void ShowMissionTips(string missionId, MISSION_STATE misState)
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsTutorialScene())
		{
			MissionData curMisData = DataManager.GetMissionDataByID(missionId);
			if (misState == MISSION_STATE.ACCEPTED)
			{
				if (curMisData.MissionLogicType == MISSION_LOGICTYPE.ROB_CAR || curMisData.MissionLogicType == MISSION_LOGICTYPE.MASSACRE_NPC || curMisData.MissionLogicType == MISSION_LOGICTYPE.DESTROY_CAR || curMisData.MissionLogicType == MISSION_LOGICTYPE.IMPACT_NPC)
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MissionTipsRoot, delegate
					{
						SingletonUnity<MissionTipRootLogic>.Instance.Reset(curMisData.MissionLogicType);
					}, null);
				}
				else if (!string.IsNullOrEmpty(curMisData.TipStr))
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MissionTipsRoot, delegate
					{
						SingletonUnity<MissionTipRootLogic>.Instance.Reset(curMisData.MissionLogicType, curMisData.TipStr);
					}, null);
				}
				else if (SingletonUnity<MissionTipRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MissionTipRootLogic>.Instance.gameObject))
				{
					SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MissionTipsRoot);
				}
			}
			else if (SingletonUnity<MissionTipRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MissionTipRootLogic>.Instance.gameObject))
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MissionTipsRoot);
			}
		}
		else if (SingletonUnity<MissionTipRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MissionTipRootLogic>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MissionTipsRoot);
		}
	}

	// Token: 0x06004315 RID: 17173 RVA: 0x0014A174 File Offset: 0x00148374
	public void Reset(MISSION_LOGICTYPE curType, string str)
	{
		this.curLogicType = curType;
		this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		this.TipsLabel.text = StrDictionary.GetDictionaryString(str, new object[0]);
	}

	// Token: 0x06004316 RID: 17174 RVA: 0x0014A1B0 File Offset: 0x001483B0
	public void Reset(MISSION_LOGICTYPE curType)
	{
		this.curLogicType = curType;
		this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (this.curLogicType == MISSION_LOGICTYPE.ROB_CAR)
		{
			this.TipsLabel.text = StrDictionary.GetDictionaryString("#{102076}", new object[0]);
		}
		else if (this.curLogicType == MISSION_LOGICTYPE.MASSACRE_NPC)
		{
			this.TipsLabel.text = StrDictionary.GetDictionaryString("#{102077}", new object[0]);
		}
		else if (this.curLogicType == MISSION_LOGICTYPE.DESTROY_CAR)
		{
			this.TipsLabel.text = StrDictionary.GetDictionaryString("#{102078}", new object[0]);
		}
		else if (this.curLogicType == MISSION_LOGICTYPE.IMPACT_NPC)
		{
			if (this.mMainPlayer.IsLocalDrivingCar)
			{
				this.TipsLabel.text = StrDictionary.GetDictionaryString("#{102079}", new object[0]);
				this.isInCarTips = true;
			}
			else
			{
				this.TipsLabel.text = StrDictionary.GetDictionaryString("#{102080}", new object[0]);
				this.isInCarTips = false;
			}
		}
		this.ClickAnima.ResetToBeginning();
		this.ClickAnima.Play();
	}

	// Token: 0x06004317 RID: 17175 RVA: 0x0014A2D8 File Offset: 0x001484D8
	private void Update()
	{
		if (this.curLogicType == MISSION_LOGICTYPE.IMPACT_NPC && this.isInCarTips != this.mMainPlayer.IsLocalDrivingCar)
		{
			if (this.mMainPlayer.IsLocalDrivingCar)
			{
				this.TipsLabel.text = StrDictionary.GetDictionaryString("#{102079}", new object[0]);
				this.isInCarTips = true;
			}
			else
			{
				this.TipsLabel.text = StrDictionary.GetDictionaryString("#{102080}", new object[0]);
				this.isInCarTips = false;
			}
		}
	}

	// Token: 0x04002F8A RID: 12170
	public UILabel TipsLabel;

	// Token: 0x04002F8B RID: 12171
	public TweenScale ClickAnima;

	// Token: 0x04002F8C RID: 12172
	private MISSION_LOGICTYPE curLogicType;

	// Token: 0x04002F8D RID: 12173
	private bool isInCarTips;

	// Token: 0x04002F8E RID: 12174
	private ObjMainPlayer mMainPlayer;
}
