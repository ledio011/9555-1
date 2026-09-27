using System;
using UnityEngine;

// Token: 0x020009C2 RID: 2498
public class TeamPlayerPicLogic : MonoBehaviour
{
	// Token: 0x17000FB0 RID: 4016
	// (get) Token: 0x0600471A RID: 18202 RVA: 0x0016A090 File Offset: 0x00168290
	public bool IsEmpty
	{
		get
		{
			return this.mIsEmpty;
		}
	}

	// Token: 0x17000FB1 RID: 4017
	// (get) Token: 0x0600471B RID: 18203 RVA: 0x0016A098 File Offset: 0x00168298
	public bool IsLock
	{
		get
		{
			return this.mIsLock;
		}
	}

	// Token: 0x0600471C RID: 18204 RVA: 0x0016A0A0 File Offset: 0x001682A0
	public void Reset(TeamMember playerInfo, bool isLock = false)
	{
		if (playerInfo == null)
		{
			this.mIsLock = isLock;
			this.mIsEmpty = true;
			this.SetEmpty();
		}
		else
		{
			this.mIsEmpty = false;
			this.mIsLock = false;
			this.SetPlayer(playerInfo);
		}
	}

	// Token: 0x0600471D RID: 18205 RVA: 0x0016A0E4 File Offset: 0x001682E4
	private void SetEmpty()
	{
		this.CurFakeObjRoot.DisableFakeObjRoot();
		this.PlayerModelPic.enabled = false;
		UnityVersionUtil.SetActiveRecursive(this.ProfessionPic.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.TeamLeaderPic.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.ReadyPic.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.NameLabel.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.LevelLabel.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.FightingLabel.gameObject, false);
		NGUITools.SetActive(this.RestNumLabel.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.EmptyPlusPic.gameObject, true);
		if (this.mIsLock)
		{
			this.EmptyPlusPic.spriteName = "CZ_huaDongBG_Suo";
		}
		else
		{
			this.EmptyPlusPic.spriteName = "CZ_anNiu_tianJiaDuiYou";
		}
	}

	// Token: 0x0600471E RID: 18206 RVA: 0x0016A1C0 File Offset: 0x001683C0
	private void SetPlayer(TeamMember playerInfo)
	{
		this.CurFakeObjRoot.CreateModelPic();
		this.CurFakeObjRoot.EnableFakeObjRoot();
		this.PlayerModelPic.mainTexture = this.CurFakeObjRoot.ModelPic;
		this.PlayerModelPic.enabled = true;
		if (this.mCurPlayerInfo != null && this.curProfession == playerInfo.Profession)
		{
			this.CurFakeObj.CheckFakeObject(playerInfo.Visual, null);
		}
		else
		{
			this.CurFakeObj.DestroyFakeObj();
			this.CurFakeObj.InitFakeObject(playerInfo.Visual, playerInfo.Profession, this.CurFakeObjRoot.MeshRoot, null, "FakeObj");
		}
		this.mCurPlayerInfo = playerInfo;
		this.curProfession = playerInfo.Profession;
		UnityVersionUtil.SetActiveRecursive(this.NameLabel.gameObject, true);
		this.NameLabel.text = this.mCurPlayerInfo.Name;
		UnityVersionUtil.SetActiveRecursive(this.ProfessionPic.gameObject, true);
		this.ProfessionPic.spriteName = GameDefine.Player_Profession_Pic[(int)this.mCurPlayerInfo.Profession];
		UnityVersionUtil.SetActiveRecursive(this.LevelLabel.gameObject, true);
		this.LevelLabel.text = string.Format("Lv.{0}", this.mCurPlayerInfo.Level);
		UnityVersionUtil.SetActiveRecursive(this.FightingLabel.gameObject, true);
		this.FightingLabel.text = string.Format("{0}", this.mCurPlayerInfo.CombValue);
		if (this.mCurPlayerInfo.TeamJob == 0)
		{
			UnityVersionUtil.SetActiveRecursive(this.TeamLeaderPic.gameObject, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.TeamLeaderPic.gameObject, false);
		}
		Team teamInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo;
		if (teamInfo.IsCheckingEnterCopy)
		{
			if (playerInfo.IsReadyEnterCopy)
			{
				this.ReadyPic.spriteName = "CZ_fuBenTuBiao_yiZhunBei";
				this.ReadyPic.MakePixelPerfect();
				UnityVersionUtil.SetActiveRecursive(this.ReadyPic.gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.ReadyPic.gameObject, false);
			}
		}
		else if (playerInfo.IsRefuseEnterCopy)
		{
			this.ReadyPic.spriteName = "CZ_fuBenTuBiao_juJue";
			this.ReadyPic.MakePixelPerfect();
			UnityVersionUtil.SetActiveRecursive(this.ReadyPic.gameObject, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ReadyPic.gameObject, false);
		}
		UnityVersionUtil.SetActiveRecursive(this.EmptyPlusPic.gameObject, false);
		if (teamInfo.TeamGoalData.GoalType == 1)
		{
			if (playerInfo.CopyRestNum == -1)
			{
				NGUITools.SetActive(this.RestNumLabel.gameObject, false);
			}
			else
			{
				NGUITools.SetActive(this.RestNumLabel.gameObject, true);
				string copyId = teamInfo.TeamGoalData.CopyId;
				CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(copyId);
				this.RestNumLabel.text = string.Format("{0}:{1}/{2}", StrDictionary.GetDictionaryString("#{100749}", new object[0]), playerInfo.CopyRestNum, copySceneDataById.MaxPlayNum);
			}
		}
		else
		{
			NGUITools.SetActive(this.RestNumLabel.gameObject, false);
		}
	}

	// Token: 0x0600471F RID: 18207 RVA: 0x0016A4E8 File Offset: 0x001686E8
	public void OnClickPic()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsTeamLeader())
		{
			if (!this.mIsLock)
			{
				if (this.mIsEmpty)
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamInviteRoot, null, null);
				}
				else if (this.mCurPlayerInfo.ServerId != PlayerData.MainPlayerServerId)
				{
					TargetBasicInfo selectTargetBasicInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SelectTargetBasicInfo;
					selectTargetBasicInfo.ResetInfo(this.mCurPlayerInfo.ServerId, this.mCurPlayerInfo.Level, this.mCurPlayerInfo.CombValue, this.mCurPlayerInfo.Name, this.mCurPlayerInfo.Profession, 1, this.mCurPlayerInfo.GuildId, this.mCurPlayerInfo.GuildName, UICamera.currentTouch.pos);
					HitOtherPLayerLogic.ShowMenu(HitType.HitTeamMemberIcon, selectTargetBasicInfo);
				}
			}
		}
		else if (!this.mIsEmpty && this.mCurPlayerInfo.ServerId != PlayerData.MainPlayerServerId)
		{
			TargetBasicInfo selectTargetBasicInfo2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SelectTargetBasicInfo;
			selectTargetBasicInfo2.ResetInfo(this.mCurPlayerInfo.ServerId, this.mCurPlayerInfo.Level, this.mCurPlayerInfo.CombValue, this.mCurPlayerInfo.Name, this.mCurPlayerInfo.Profession, 1, this.mCurPlayerInfo.GuildId, this.mCurPlayerInfo.GuildName, UICamera.currentTouch.pos);
			HitOtherPLayerLogic.ShowMenu(HitType.HitTeamMemberIcon, selectTargetBasicInfo2);
		}
	}

	// Token: 0x06004720 RID: 18208 RVA: 0x0016A65C File Offset: 0x0016885C
	private void OnDisable()
	{
		this.UnLoadFakeObj();
	}

	// Token: 0x06004721 RID: 18209 RVA: 0x0016A664 File Offset: 0x00168864
	public void UnLoadFakeObj()
	{
		if (this.CurFakeObj != null)
		{
			this.CurFakeObj.DestroyFakeObj();
			this.CurFakeObj = null;
		}
		else
		{
			Debug.Log("mPlayerModelVisual == null");
		}
		if (this.CurFakeObjRoot != null)
		{
			this.CurFakeObjRoot.DisableFakeObjRoot();
		}
	}

	// Token: 0x04003435 RID: 13365
	public UILabel NameLabel;

	// Token: 0x04003436 RID: 13366
	public UILabel LevelLabel;

	// Token: 0x04003437 RID: 13367
	public UILabel FightingLabel;

	// Token: 0x04003438 RID: 13368
	public UISprite ProfessionPic;

	// Token: 0x04003439 RID: 13369
	public UISprite TeamLeaderPic;

	// Token: 0x0400343A RID: 13370
	public UISprite ReadyPic;

	// Token: 0x0400343B RID: 13371
	public UISprite EmptyPlusPic;

	// Token: 0x0400343C RID: 13372
	public UITexture PlayerModelPic;

	// Token: 0x0400343D RID: 13373
	public UILabel RestNumLabel;

	// Token: 0x0400343E RID: 13374
	public TeamFakeObjPicRootLogic CurFakeObjRoot;

	// Token: 0x0400343F RID: 13375
	public FakeObjLogic CurFakeObj;

	// Token: 0x04003440 RID: 13376
	private TeamMember mCurPlayerInfo;

	// Token: 0x04003441 RID: 13377
	private PROFESSION_TYPE curProfession;

	// Token: 0x04003442 RID: 13378
	private bool mIsEmpty;

	// Token: 0x04003443 RID: 13379
	private bool mIsLock;
}
