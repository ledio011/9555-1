using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x020009D3 RID: 2515
public class UnlockFunctionRootLogic : SingletonUnity<UnlockFunctionRootLogic>
{
	// Token: 0x0600477D RID: 18301 RVA: 0x0016C6DC File Offset: 0x0016A8DC
	public void ResetUnlockFunction(string functionId)
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ChatRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.HitOtherPlayerRoot);
		this.mCurFuncData = DataManager.GetFunctionDataById(functionId);
		if (this.mCurFuncData.UnlockType == 1)
		{
			if (SingletonUnity<FunctionBtnRootLogic>.Exists)
			{
				FunctionBtnRootLogic instance = SingletonUnity<FunctionBtnRootLogic>.Instance;
				bool openRight = false;
				UISprite uisprite = null;
				switch (int.Parse(functionId))
				{
				case 3001:
					uisprite = instance.ActivityBtnIcon;
					break;
				case 3004:
					uisprite = instance.RankFuncBtn.IconSp;
					break;
				case 3005:
					uisprite = instance.GiftFuncBtn.IconSp;
					break;
				case 3006:
					uisprite = instance.ShopFuncBtn.IconSp;
					openRight = true;
					break;
				case 3007:
					uisprite = instance.SlotFuncBtn.IconSp;
					break;
				case 3008:
					uisprite = instance.FirstSaleFuncBtn.IconSp;
					break;
				case 3009:
					uisprite = instance.BigSaleFuncBtn.IconSp;
					break;
				case 3010:
					uisprite = instance.CarFuncBtn.IconSp;
					openRight = true;
					break;
				case 3011:
					uisprite = instance.TitleFuncBtn.IconSp;
					openRight = true;
					break;
				case 3012:
					uisprite = instance.EnhanceFuncBtn.IconSp;
					openRight = true;
					break;
				case 3013:
					uisprite = instance.SkillFuncBtn.IconSp;
					openRight = true;
					break;
				case 3014:
					uisprite = instance.CharacterBtnIcon;
					openRight = true;
					break;
				case 3015:
					uisprite = instance.BagFuncBtn.IconSp;
					openRight = true;
					break;
				case 3017:
					uisprite = instance.GuildFuncBtn.IconSp;
					openRight = true;
					break;
				case 3019:
					uisprite = instance.MysteryFuncBtn.IconSp;
					break;
				case 3020:
					uisprite = instance.AutoBtnSprite;
					break;
				}
				if (uisprite != null)
				{
					this.ResetIconFunction(uisprite, this.mCurFuncData.MName, openRight);
				}
				else
				{
					SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.UnlockFunctionRoot);
					SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.CheckShowUnlockFunction();
				}
			}
		}
		else if (this.mCurFuncData.UnlockType == 0)
		{
		}
		if (this.mCurFuncData.SideMissionIdList != null)
		{
			MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			for (int i = 0; i < this.mCurFuncData.SideMissionIdList.Length; i++)
			{
				if (missionManager.IsMissionAcceptable(this.mCurFuncData.SideMissionIdList[i]))
				{
					missionManager.AcceptMission(this.mCurFuncData.SideMissionIdList[i]);
				}
			}
		}
	}

	// Token: 0x0600477E RID: 18302 RVA: 0x0016C974 File Offset: 0x0016AB74
	public void ShowAcceptMissionEffect(string missionid)
	{
		ActivityMapData activityMapDataByActID = DataManager.GetActivityMapDataByActID(11, missionid);
		if (activityMapDataByActID != null)
		{
			this.ResetMissionIconFunction(activityMapDataByActID.Icon, SingletonUnity<MissionTeamTipLogic>.Instance.MissionBtn.transform.position);
			SingletonUnity<MissionTeamTipLogic>.Instance.ShowNewMissionFlag();
		}
		else
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.UnlockFunctionRoot);
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.CheckShowUnlockFunction();
		}
	}

	// Token: 0x0600477F RID: 18303 RVA: 0x0016C9E0 File Offset: 0x0016ABE0
	public void ResetIconFunction(UISprite functionSourceIcon, string functionName, bool openRight)
	{
		this.FunctionNameLabel.text = functionName;
		this.FunctionIconPic.spriteName = functionSourceIcon.spriteName;
		this.FunctionIconPic.width = functionSourceIcon.width;
		this.FunctionIconPic.height = functionSourceIcon.height;
		this.curTimeHandle.Cancel();
		vp_Timer.In(2f, delegate()
		{
			FunctionBtnRootLogic instance = SingletonUnity<FunctionBtnRootLogic>.Instance;
			instance.refershBtn();
			if (openRight)
			{
				if (!instance.IsOpenRightBtn)
				{
					instance.OnClickMenuBtn();
				}
				else
				{
					instance.ResetRightBtn();
				}
			}
			functionSourceIcon.alpha = 0f;
			if (this.FunctionIconPic != null)
			{
				this.FunctionIconPic.transform.parent = functionSourceIcon.transform;
				TweenSettingsExtensions.OnComplete<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOLocalMove(this.FunctionIconPic.transform, Vector3.zero, 0.5f, false), 8), delegate()
				{
					NGUITools.SetActive(this.FunctionIconPic.gameObject, false);
					this.FunctionIconPic.transform.parent = this.IconRoot;
					this.FunctionIconPic.transform.localPosition = Vector3.zero;
					this.FunctionIconPic.transform.localRotation = Quaternion.identity;
					NGUITools.SetActive(functionSourceIcon.gameObject, true);
					functionSourceIcon.alpha = 1f;
					SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.UnlockFunctionRoot);
					if (this.mCurFuncData.FirstOpen == 1)
					{
						switch (int.Parse(this.mCurFuncData.ID))
						{
						case 3013:
							SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.SKILL);
							if (Singleton<ObjManager>.Instance.MainPlayer.CheckSkillCanUpdate())
							{
							}
							break;
						}
					}
					else
					{
						SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.CheckShowUnlockFunction();
					}
				});
				ShortcutExtensions.DOScale(this.FunctionIconPic.transform, Vector3.one * 0.633f, 0.5f);
			}
			NGUITools.SetActive(this.OtherRoot, false);
		}, this.curTimeHandle);
	}

	// Token: 0x06004780 RID: 18304 RVA: 0x0016CA80 File Offset: 0x0016AC80
	private void OnEnable()
	{
		Singleton<ObjManager>.Instance.MainPlayer.IsTalking = true;
	}

	// Token: 0x06004781 RID: 18305 RVA: 0x0016CA94 File Offset: 0x0016AC94
	private void OnDisable()
	{
		this.curTimeHandle.Cancel();
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = false;
		}
	}

	// Token: 0x06004782 RID: 18306 RVA: 0x0016CAD4 File Offset: 0x0016ACD4
	public void ResetNoIconFunction(string functionName, Vector3 targetPos)
	{
		this.FunctionNameLabel.text = functionName;
		this.FunctionIconPic.enabled = false;
		vp_Timer.In(2f, delegate()
		{
			this.FunctionIconPic.transform.parent = SingletonUnity<UIManager>.Instance.transform;
			TweenSettingsExtensions.OnComplete<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOMove(this.FunctionIconPic.transform, targetPos, 0.5f, false), 8), delegate()
			{
				Object.Destroy(this.FunctionIconPic.gameObject);
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.CheckShowUnlockFunction();
			});
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.UnlockFunctionRoot);
		}, null);
	}

	// Token: 0x06004783 RID: 18307 RVA: 0x0016CB24 File Offset: 0x0016AD24
	public void ResetMissionIconFunction(string IconName, Vector3 targetPos)
	{
		this.FunctionNameLabel.text = string.Empty;
		this.FunctionIconPic.spriteName = IconName;
		this.FunctionIconPic.MakePixelPerfect();
		vp_Timer.In(2f, delegate()
		{
			this.FunctionIconPic.transform.parent = SingletonUnity<UIManager>.Instance.transform;
			TweenSettingsExtensions.OnComplete<Tweener>(TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOMove(this.FunctionIconPic.transform, targetPos, 0.5f, false), 8), delegate()
			{
				Object.Destroy(this.FunctionIconPic.gameObject);
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.CheckShowUnlockFunction();
			});
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.UnlockFunctionRoot);
		}, null);
	}

	// Token: 0x040034BA RID: 13498
	public UILabel FunctionNameLabel;

	// Token: 0x040034BB RID: 13499
	public UISprite FunctionIconPic;

	// Token: 0x040034BC RID: 13500
	public GameObject OtherRoot;

	// Token: 0x040034BD RID: 13501
	public Transform IconRoot;

	// Token: 0x040034BE RID: 13502
	private FunctionData mCurFuncData;

	// Token: 0x040034BF RID: 13503
	private vp_Timer.Handle curTimeHandle = new vp_Timer.Handle();
}
