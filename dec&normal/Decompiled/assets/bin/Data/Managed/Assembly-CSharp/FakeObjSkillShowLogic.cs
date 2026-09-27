using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000918 RID: 2328
public class FakeObjSkillShowLogic : MonoBehaviour
{
	// Token: 0x06004034 RID: 16436 RVA: 0x0012D204 File Offset: 0x0012B404
	public void Reset(GameObject fakeObj, string modelType)
	{
		this.mFakeObj = fakeObj;
		this.anima = this.mFakeObj.animation;
		this.mModelType = modelType;
		AnimationClip animationClip = AnimationManager.LoadAnimation(modelType, "idle_attack") as AnimationClip;
		this.anima.AddClip(animationClip, "idle_attack");
	}

	// Token: 0x06004035 RID: 16437 RVA: 0x0012D254 File Offset: 0x0012B454
	public void UseSkill(string skillId, string modelType, AnimationLogic.OnAnimFinished func)
	{
		SkillData skillDataById = DataManager.GetSkillDataById(skillId);
		if (skillDataById == null)
		{
			Log.DEBUG_MSG("mUsingSkillData == null  skillId : " + skillId);
			return;
		}
		if (DataManager.GetActionDataByName(string.Format("{0}_{1}", modelType, skillDataById.ActionName)) == null)
		{
			Log.DEBUG_MSG("mCurActionData == null  ActionName : " + string.Format("{0}_{1}", modelType, skillDataById.ActionName));
			return;
		}
		this.onSkillFinished = func;
		this.PlayAnim(skillDataById.ActionName, modelType);
		EffInfoData effInfoDataById = DataManager.GetEffInfoDataById(skillDataById.EffId_0);
		if (effInfoDataById.Target == 1 && !string.IsNullOrEmpty(effInfoDataById.BuffID))
		{
			BuffInfoData buffInfoDataByID = DataManager.GetBuffInfoDataByID(effInfoDataById.BuffID);
			this.AddPlayeBufEffInfoData(buffInfoDataByID.Effect, 0f, 1f, this.mFakeObj.transform.position);
		}
		else
		{
			this.AddPlayEffInfoData(skillDataById.ActionName, modelType, string.Empty, 0f, this.mFakeObj.transform.position, null);
		}
	}

	// Token: 0x06004036 RID: 16438 RVA: 0x0012D358 File Offset: 0x0012B558
	public void PlayAnim(string animationName, string modelType)
	{
		this.anima = this.mFakeObj.animation;
		AnimationState animationState = this.anima[animationName];
		this.mModelType = modelType;
		if (animationState == null)
		{
			AnimationClip animationClip = AnimationManager.LoadAnimation(modelType, animationName) as AnimationClip;
			this.anima.AddClip(animationClip, animationName);
		}
		if (!this.anima.IsPlaying(animationName))
		{
			this.mCurAnimaName = animationName;
			this.anima.Play(animationName);
		}
	}

	// Token: 0x06004037 RID: 16439 RVA: 0x0012D3D8 File Offset: 0x0012B5D8
	public void AddPlayeBufEffInfoData(string fxeffectInfoId, float delayTime, float duration, Vector3 senderPosition)
	{
		if (!GameSettingData.IsShowSkillEffect[GameSettingData.GetPhoneClass()] || GameSettingData.IsLowPhone)
		{
			return;
		}
		if (this.IsEffectInPlayingList(fxeffectInfoId))
		{
			return;
		}
		List<PlayEffInfoData> playBufEffInfoDataList = PlayEffInfoData.GetPlayBufEffInfoDataList(fxeffectInfoId, delayTime, duration, senderPosition);
		if (playBufEffInfoDataList == null)
		{
			return;
		}
		this.AddPlayEffInfoList(playBufEffInfoDataList);
	}

	// Token: 0x06004038 RID: 16440 RVA: 0x0012D428 File Offset: 0x0012B628
	public void AddPlayEffInfoData(string actionName, string modelType, string effInfoId, float delayTime, Vector3 senderPos, Transform targetTransform = null)
	{
		string text = string.Format("{0}_{1}", modelType, actionName);
		ActionData actionDataByName = DataManager.GetActionDataByName(text);
		if (actionDataByName == null || this.IsEffectInPlayingList(actionDataByName.FxEffID))
		{
			return;
		}
		List<PlayEffInfoData> playEffInfoDataList = PlayEffInfoData.GetPlayEffInfoDataList(text, effInfoId, delayTime, senderPos, targetTransform);
		this.AddPlayEffInfoList(playEffInfoDataList);
	}

	// Token: 0x06004039 RID: 16441 RVA: 0x0012D478 File Offset: 0x0012B678
	private void AddPlayEffInfoList(List<PlayEffInfoData> list)
	{
		if (list == null)
		{
			return;
		}
		for (int i = 0; i < list.Count; i++)
		{
			PlayEffInfoData playEffInfoData = list[i];
			FxControl fxControl = null;
			string effName = playEffInfoData.mFxEffinfoData.EffName;
			if (EffectLogic.CacheEffectDic.ContainsKey(effName) && EffectLogic.CacheEffectDic[effName].Count > 0)
			{
				fxControl = EffectLogic.CacheEffectDic[effName][0];
				if (fxControl != null)
				{
					EffectLogic.CacheEffectDic[effName].Remove(fxControl);
					fxControl.Reset(null, playEffInfoData.mFxEffinfoData, playEffInfoData.DurationTime, -777L, GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER);
					this.AddBindPos(fxControl, playEffInfoData);
				}
			}
			else
			{
				string fxLoadPath = playEffInfoData.fxLoadPath;
				GameObject gameObject = ResourcesManager.LoadAndInstantiate(fxLoadPath) as GameObject;
				if (gameObject != null)
				{
					fxControl = gameObject.GetComponent<FxControl>();
					fxControl.Reset(null, playEffInfoData.mFxEffinfoData, playEffInfoData.DurationTime, -777L, GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER);
					this.AddBindPos(fxControl, playEffInfoData);
				}
			}
			if (fxControl != null)
			{
				NGUITools.SetLayer(fxControl.gameObject, 22);
				playEffInfoData.fxControl = fxControl;
				this.PlayEffInfoDataList.Add(playEffInfoData);
			}
			else
			{
				BundleManager.LoadEffectInList(effName, false, false, new BundleManager.OnLoadModelFinish(this.OnLoadEffect), playEffInfoData, null);
			}
		}
	}

	// Token: 0x0600403A RID: 16442 RVA: 0x0012D5CC File Offset: 0x0012B7CC
	public void AddBindPos(FxControl fxControl, PlayEffInfoData playEffInfoData)
	{
		FxEffInfoData mFxEffinfoData = playEffInfoData.mFxEffinfoData;
		string effLinkNode = mFxEffinfoData.EffLinkNode;
		GameObject gameObject = null;
		if (!this.mBindPosDic.TryGetValue(effLinkNode, ref gameObject))
		{
			gameObject = TransformUtil.FindChildGameObject(this.mFakeObj, effLinkNode);
			this.mBindPosDic.Add(effLinkNode, gameObject);
		}
		if (gameObject != null)
		{
			fxControl.CacheTransform.parent = gameObject.transform;
			fxControl.CacheTransform.localPosition = mFxEffinfoData.Position;
			fxControl.CacheTransform.localEulerAngles = mFxEffinfoData.Angel;
		}
		else
		{
			fxControl.transform.position = playEffInfoData.SenderPos;
		}
	}

	// Token: 0x0600403B RID: 16443 RVA: 0x0012D66C File Offset: 0x0012B86C
	private void OnLoadEffect(object objBundle, object param1 = null, object param2 = null)
	{
		GameObject gameObject = objBundle as GameObject;
		NGUITools.SetLayer(gameObject, 22);
		BundleManager.ResetAllShader(gameObject.transform);
		PlayEffInfoData playEffInfoData = param1 as PlayEffInfoData;
		if (gameObject != null)
		{
			FxControl component = gameObject.GetComponent<FxControl>();
			component.Reset(null, playEffInfoData.mFxEffinfoData, playEffInfoData.DurationTime, -777L, GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER);
			this.AddBindPos(component, playEffInfoData);
			playEffInfoData.fxControl = component;
			this.PlayEffInfoDataList.Add(playEffInfoData);
		}
	}

	// Token: 0x0600403C RID: 16444 RVA: 0x0012D6E4 File Offset: 0x0012B8E4
	private bool IsEffectInPlayingList(string effectId)
	{
		for (int i = 0; i < this.PlayEffInfoDataList.Count; i++)
		{
			if (this.PlayEffInfoDataList[i].fxControl.EffectId.Equals(effectId))
			{
				return true;
			}
		}
		if (!EffectLogic.CurrentPlayEffectDic.ContainsKey(GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER))
		{
			return false;
		}
		List<FxControl> list = EffectLogic.CurrentPlayEffectDic[GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER];
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j].OwnerId == -777L && list[j].EffectId.Equals(effectId))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600403D RID: 16445 RVA: 0x0012D798 File Offset: 0x0012B998
	private void Update()
	{
		if (this.PlayEffInfoDataList.Count > 0)
		{
			for (int i = this.PlayEffInfoDataList.Count - 1; i > -1; i--)
			{
				this.PlayEffInfoDataList[i].DelayTime -= Time.deltaTime;
				PlayEffInfoData playEffInfoData = this.PlayEffInfoDataList[i];
				if (playEffInfoData.DelayTime <= 0f)
				{
					if (playEffInfoData.fxControl != null)
					{
						if (!EffectLogic.CurrentPlayEffectDic.ContainsKey(playEffInfoData.fxControl.OwnerType))
						{
							EffectLogic.CurrentPlayEffectDic.Add(playEffInfoData.fxControl.OwnerType, new List<FxControl>());
						}
						EffectLogic.CurrentPlayEffectDic[playEffInfoData.fxControl.OwnerType].Add(playEffInfoData.fxControl);
						if (playEffInfoData.mFxEffinfoData.AutoMoveFlag)
						{
							playEffInfoData.fxControl.Play(this.mFakeObj.transform.position + this.mFakeObj.transform.forward * 5f);
						}
						else
						{
							playEffInfoData.fxControl.Play();
						}
					}
					this.PlayEffInfoDataList.RemoveAt(i);
				}
			}
		}
		if (this.onSkillFinished != null && !this.anima.IsPlaying(this.mCurAnimaName))
		{
			this.onSkillFinished();
			this.onSkillFinished = null;
		}
	}

	// Token: 0x04002BF2 RID: 11250
	private GameObject mFakeObj;

	// Token: 0x04002BF3 RID: 11251
	public List<PlayEffInfoData> PlayEffInfoDataList = new List<PlayEffInfoData>();

	// Token: 0x04002BF4 RID: 11252
	private Dictionary<string, GameObject> mBindPosDic = new Dictionary<string, GameObject>();

	// Token: 0x04002BF5 RID: 11253
	private Animation anima;

	// Token: 0x04002BF6 RID: 11254
	private string mCurAnimaName;

	// Token: 0x04002BF7 RID: 11255
	private string mModelType;

	// Token: 0x04002BF8 RID: 11256
	private AnimationLogic.OnAnimFinished onSkillFinished;
}
