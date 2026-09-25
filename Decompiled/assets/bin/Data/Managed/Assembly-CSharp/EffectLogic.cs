using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000846 RID: 2118
public class EffectLogic : MonoBehaviour
{
	// Token: 0x17000EFD RID: 3837
	// (get) Token: 0x0600366F RID: 13935 RVA: 0x000DFC48 File Offset: 0x000DDE48
	public static Dictionary<string, List<FxControl>> CacheEffectDic
	{
		get
		{
			return EffectLogic.mCacheEffectDic;
		}
	}

	// Token: 0x17000EFE RID: 3838
	// (get) Token: 0x06003670 RID: 13936 RVA: 0x000DFC50 File Offset: 0x000DDE50
	public static Dictionary<GameDefine.OBJ_TYPE, List<FxControl>> CurrentPlayEffectDic
	{
		get
		{
			return EffectLogic.mCurrentPlayEffectDic;
		}
	}

	// Token: 0x06003671 RID: 13937 RVA: 0x000DFC58 File Offset: 0x000DDE58
	public static void Clear()
	{
		if (EffectLogic.CacheEffectDic != null)
		{
			EffectLogic.CacheEffectDic.Clear();
		}
		if (EffectLogic.CurrentPlayEffectDic != null)
		{
			EffectLogic.CurrentPlayEffectDic.Clear();
		}
		if (EffectLogic.mCurrentPlayEffectDataDic != null)
		{
			EffectLogic.mCurrentPlayEffectDataDic.Clear();
		}
	}

	// Token: 0x06003672 RID: 13938 RVA: 0x000DFCA4 File Offset: 0x000DDEA4
	private static int GetCacheEffectNum()
	{
		int num = 0;
		for (int i = 0; i < EffectLogic.mCacheEffectList.Count; i++)
		{
			num += EffectLogic.mCacheEffectList[i].Count;
		}
		return num;
	}

	// Token: 0x06003673 RID: 13939 RVA: 0x000DFCE4 File Offset: 0x000DDEE4
	private int GetPlayingEffectNum()
	{
		int num = 0;
		for (int i = 0; i < EffectLogic.mCurrentPlayEffectList.Count; i++)
		{
			num += EffectLogic.mCurrentPlayEffectList[i].Count;
		}
		return num;
	}

	// Token: 0x06003674 RID: 13940 RVA: 0x000DFD24 File Offset: 0x000DDF24
	private int GetPlayingEffectNum(GameDefine.OBJ_TYPE type)
	{
		if (EffectLogic.mCurrentPlayEffectDataDic.ContainsKey(type))
		{
			return EffectLogic.mCurrentPlayEffectDataDic[type].Count;
		}
		return 0;
	}

	// Token: 0x06003675 RID: 13941 RVA: 0x000DFD54 File Offset: 0x000DDF54
	private static FxControl GetEarliestCachedEffect()
	{
		float num = float.MaxValue;
		FxControl result = null;
		for (int i = 0; i < EffectLogic.mCacheEffectList.Count; i++)
		{
			for (int j = 0; j < EffectLogic.mCacheEffectList[i].Count; j++)
			{
				if (EffectLogic.mCacheEffectList[i][j].GenerateTime < num)
				{
					num = EffectLogic.mCacheEffectList[i][j].GenerateTime;
					result = EffectLogic.mCacheEffectList[i][j];
				}
			}
		}
		return result;
	}

	// Token: 0x06003676 RID: 13942 RVA: 0x000DFDEC File Offset: 0x000DDFEC
	private static FxControl GetEarliestPlayingEffect()
	{
		float num = float.MaxValue;
		FxControl result = null;
		for (int i = 0; i < EffectLogic.mCurrentPlayEffectList.Count; i++)
		{
			for (int j = 0; j < EffectLogic.mCurrentPlayEffectList[i].Count; j++)
			{
				if (EffectLogic.mCurrentPlayEffectList[i][j].GenerateTime < num)
				{
					num = EffectLogic.mCurrentPlayEffectList[i][j].GenerateTime;
					result = EffectLogic.mCurrentPlayEffectList[i][j];
				}
			}
		}
		return result;
	}

	// Token: 0x06003677 RID: 13943 RVA: 0x000DFE84 File Offset: 0x000DE084
	private bool IsEffectInPlayingList(string effectId)
	{
		for (int i = 0; i < this.PlayEffInfoDataList.Count; i++)
		{
			if (this.PlayEffInfoDataList[i].fxControl.EffectId.Equals(effectId))
			{
				return true;
			}
		}
		if (!EffectLogic.mCurrentPlayEffectDataDic.ContainsKey(this.ownCharacter.ObjType))
		{
			return false;
		}
		List<PlayingEffectData> list = EffectLogic.mCurrentPlayEffectDataDic[this.ownCharacter.ObjType];
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j].OwnerId == this.ownCharacter.ServerId && list[j].EffectDataId.Equals(effectId))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003678 RID: 13944 RVA: 0x000DFF50 File Offset: 0x000DE150
	public void AddPlayEffInfoData(string actionName, string effInfoId, float delayTime, Vector3 senderPos, Transform targetTransform = null)
	{
		if (!GameSettingData.IsShowSkillEffect[GameSettingData.GetPhoneClass()] || GameSettingData.IsLowPhone)
		{
			return;
		}
		if (this.ownCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER && !(this.ownCharacter as ObjOtherPlayer).IsVisible())
		{
			return;
		}
		if (this.ownCharacter.MainPlayerDistance() > EffectLogic.EffectAvailableDistance)
		{
			return;
		}
		string actionName2 = this.ownCharacter.GetActionName(actionName);
		ActionData actionDataByName = DataManager.GetActionDataByName(actionName2);
		if (actionDataByName != null)
		{
			if (this.IsEffectInPlayingList(actionDataByName.FxEffID))
			{
				return;
			}
		}
		List<PlayEffInfoData> playEffInfoDataList = PlayEffInfoData.GetPlayEffInfoDataList(actionName2, effInfoId, delayTime, senderPos, targetTransform);
		if (playEffInfoDataList == null)
		{
			return;
		}
		if (this.GetPlayingEffectNum(this.ownCharacter.ObjType) + playEffInfoDataList.Count > EffectLogic.MAX_PLAYING_NUM[GameSettingData.GetPhoneClass(), (int)this.ownCharacter.ObjType])
		{
			return;
		}
		this.AddPlayEffInfoList(playEffInfoDataList);
	}

	// Token: 0x06003679 RID: 13945 RVA: 0x000E003C File Offset: 0x000DE23C
	public void AddPlayeBufEffInfoData(string fxeffectInfoId, float delayTime, float duration, Vector3 senderPosition)
	{
		if (!GameSettingData.IsShowSkillEffect[GameSettingData.GetPhoneClass()] || GameSettingData.IsLowPhone)
		{
			return;
		}
		if (this.ownCharacter.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER && !(this.ownCharacter as ObjOtherPlayer).IsVisible())
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
		if (this.GetPlayingEffectNum(this.ownCharacter.ObjType) + playBufEffInfoDataList.Count > EffectLogic.MAX_PLAYING_NUM[GameSettingData.GetPhoneClass(), (int)this.ownCharacter.ObjType])
		{
			return;
		}
		this.AddPlayEffInfoList(playBufEffInfoDataList);
	}

	// Token: 0x0600367A RID: 13946 RVA: 0x000E00EC File Offset: 0x000DE2EC
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
			if (EffectLogic.mCacheEffectDic.ContainsKey(effName) && EffectLogic.mCacheEffectDic[effName].Count > 0)
			{
				fxControl = EffectLogic.mCacheEffectDic[effName][0];
				if (fxControl != null)
				{
					EffectLogic.mCacheEffectDic[effName].Remove(fxControl);
					fxControl.Reset(this, playEffInfoData.mFxEffinfoData, playEffInfoData.DurationTime, this.ownCharacter.ServerId, this.ownCharacter.ObjType);
					this.AddBindPos(fxControl, playEffInfoData);
				}
			}
			else
			{
				string fxLoadPath = playEffInfoData.fxLoadPath;
				GameObject gameObject = ResourcesManager.LoadAndInstantiate(fxLoadPath) as GameObject;
				if (gameObject != null)
				{
					gameObject.name = effName;
					fxControl = gameObject.GetComponent<FxControl>();
					fxControl.Reset(this, playEffInfoData.mFxEffinfoData, playEffInfoData.DurationTime, this.ownCharacter.ServerId, this.ownCharacter.ObjType);
					this.AddBindPos(fxControl, playEffInfoData);
				}
			}
			if (fxControl != null)
			{
				NGUITools.SetLayer(fxControl.gameObject, 0);
				playEffInfoData.fxControl = fxControl;
				this.PlayEffInfoDataList.Add(playEffInfoData);
				this.AddCurrentPlayEffectDic(playEffInfoData);
			}
			else
			{
				playEffInfoData.LoadStartTime = Time.time;
				BundleManager.LoadEffectInList(effName, false, false, new BundleManager.OnLoadModelFinish(this.OnLoadEffect), playEffInfoData, null);
			}
			this.AddCurrentPlayEffectDataDic(playEffInfoData.mFxEffinfoData.ID);
		}
	}

	// Token: 0x0600367B RID: 13947 RVA: 0x000E0288 File Offset: 0x000DE488
	private void AddCurrentPlayEffectDataDic(string effectId)
	{
		if (!EffectLogic.mCurrentPlayEffectDataDic.ContainsKey(this.ownCharacter.ObjType))
		{
			EffectLogic.mCurrentPlayEffectDataDic.Add(this.ownCharacter.ObjType, new List<PlayingEffectData>());
		}
		EffectLogic.mCurrentPlayEffectDataDic[this.ownCharacter.ObjType].Add(new PlayingEffectData(this.ownCharacter.ServerId, effectId));
	}

	// Token: 0x0600367C RID: 13948 RVA: 0x000E02F4 File Offset: 0x000DE4F4
	private void AddCurrentPlayEffectDic(PlayEffInfoData playeffInfoData)
	{
		if (!EffectLogic.mCurrentPlayEffectDic.ContainsKey(playeffInfoData.fxControl.OwnerType))
		{
			EffectLogic.mCurrentPlayEffectDic.Add(playeffInfoData.fxControl.OwnerType, new List<FxControl>());
			EffectLogic.mCurrentPlayEffectList.Add(EffectLogic.mCurrentPlayEffectDic[playeffInfoData.fxControl.OwnerType]);
		}
		EffectLogic.mCurrentPlayEffectDic[playeffInfoData.fxControl.OwnerType].Add(playeffInfoData.fxControl);
	}

	// Token: 0x0600367D RID: 13949 RVA: 0x000E0374 File Offset: 0x000DE574
	private void OnLoadEffect(object objBundle, object param1 = null, object param2 = null)
	{
		GameObject gameObject = objBundle as GameObject;
		NGUITools.SetLayer(gameObject, 0);
		BundleManager.ResetParticleShader(gameObject.transform);
		PlayEffInfoData playEffInfoData = param1 as PlayEffInfoData;
		if (gameObject != null)
		{
			FxControl component = gameObject.GetComponent<FxControl>();
			component.Reset(this, playEffInfoData.mFxEffinfoData, playEffInfoData.DurationTime, this.ownCharacter.ServerId, this.ownCharacter.ObjType);
			gameObject.name = playEffInfoData.mFxEffinfoData.EffName;
			if (Time.time - playEffInfoData.LoadStartTime < playEffInfoData.DelayTime + playEffInfoData.DurationTime)
			{
				playEffInfoData.DelayTime -= Time.time - playEffInfoData.LoadStartTime;
				this.AddBindPos(component, playEffInfoData);
				playEffInfoData.fxControl = component;
				this.PlayEffInfoDataList.Add(playEffInfoData);
				this.AddCurrentPlayEffectDic(playEffInfoData);
			}
			else
			{
				EffectLogic.RecyleEffect(component);
			}
		}
	}

	// Token: 0x0600367E RID: 13950 RVA: 0x000E0454 File Offset: 0x000DE654
	public void AddBindPos(FxControl fxControl, PlayEffInfoData playEffInfoData)
	{
		FxEffInfoData mFxEffinfoData = playEffInfoData.mFxEffinfoData;
		string effLinkNode = mFxEffinfoData.EffLinkNode;
		GameObject gameObject = null;
		if (!this.mBindPosDic.TryGetValue(effLinkNode, ref gameObject))
		{
			gameObject = TransformUtil.FindChildGameObject(this.ownCharacter.gameObject, effLinkNode);
			if (gameObject != null)
			{
				this.mBindPosDic.Add(effLinkNode, gameObject);
			}
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

	// Token: 0x0600367F RID: 13951 RVA: 0x000E0504 File Offset: 0x000DE704
	public void BreakEffect(string effectId)
	{
		if (EffectLogic.mCurrentPlayEffectDic.ContainsKey(this.ownCharacter.ObjType))
		{
			List<FxControl> list = EffectLogic.mCurrentPlayEffectDic[this.ownCharacter.ObjType];
			for (int i = list.Count - 1; i > -1; i--)
			{
				if (list[i].OwnerId == this.ownCharacter.ServerId && list[i].EffectId.Equals(effectId))
				{
					EffectLogic.RecyleEffect(list[i]);
				}
			}
		}
		if (EffectLogic.mCurrentPlayEffectDataDic.ContainsKey(this.ownCharacter.ObjType))
		{
			List<PlayingEffectData> list2 = EffectLogic.mCurrentPlayEffectDataDic[this.ownCharacter.ObjType];
			for (int j = list2.Count - 1; j > -1; j--)
			{
				if (list2[j].OwnerId == this.ownCharacter.ServerId && list2[j].EffectDataId.Equals(effectId))
				{
					list2.RemoveAt(j);
				}
			}
		}
		for (int k = this.PlayEffInfoDataList.Count - 1; k > -1; k--)
		{
			if (this.PlayEffInfoDataList[k].mFxEffinfoData.ID.Equals(effectId))
			{
				this.PlayEffInfoDataList.RemoveAt(k);
			}
		}
	}

	// Token: 0x06003680 RID: 13952 RVA: 0x000E0670 File Offset: 0x000DE870
	public static void RecyleEffect(FxControl fxControl)
	{
		if (fxControl == null)
		{
			return;
		}
		List<FxControl> list = null;
		if (EffectLogic.mCurrentPlayEffectDic.ContainsKey(fxControl.OwnerType))
		{
			EffectLogic.mCurrentPlayEffectDic[fxControl.OwnerType].Remove(fxControl);
		}
		if (EffectLogic.mCurrentPlayEffectDataDic.ContainsKey(fxControl.OwnerType))
		{
			List<PlayingEffectData> list2 = EffectLogic.mCurrentPlayEffectDataDic[fxControl.mOwnerType];
			for (int i = 0; i < list2.Count; i++)
			{
				if (list2[i].OwnerId == fxControl.OwnerId && list2[i].EffectDataId.Equals(fxControl.EffectId))
				{
					list2.RemoveAt(i);
					break;
				}
			}
		}
		fxControl.transform.parent = null;
		UnityVersionUtil.SetActiveRecursive(fxControl.gameObject, false);
		if (EffectLogic.GetCacheEffectNum() > EffectLogic.MAX_CACHE_NUM)
		{
			FxControl earliestCachedEffect = EffectLogic.GetEarliestCachedEffect();
			if (earliestCachedEffect != null)
			{
				EffectLogic.mCacheEffectDic[earliestCachedEffect.EffectName].Remove(earliestCachedEffect);
				Object.Destroy(earliestCachedEffect.gameObject);
			}
		}
		if (EffectLogic.mCacheEffectDic.TryGetValue(fxControl.EffectName, ref list))
		{
			list.Add(fxControl);
		}
		else
		{
			list = new List<FxControl>();
			list.Add(fxControl);
			EffectLogic.mCacheEffectDic.Add(fxControl.EffectName, list);
			EffectLogic.mCacheEffectList.Add(list);
		}
	}

	// Token: 0x06003681 RID: 13953 RVA: 0x000E07DC File Offset: 0x000DE9DC
	public void UpdatePlayEffInfoData()
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
						if (playEffInfoData.mFxEffinfoData.AutoMoveFlag)
						{
							playEffInfoData.fxControl.Play(playEffInfoData.targetTransform);
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
	}

	// Token: 0x06003682 RID: 13954 RVA: 0x000E08A0 File Offset: 0x000DEAA0
	public void BreakYinChangEffect(string effId)
	{
		if (this.mCurYinChangEffect != null && this.mCurYinChangEffect.gameObject != null && this.mCurYinChangEffect.effectId.Equals(effId))
		{
			Object.Destroy(this.mCurYinChangEffect.gameObject);
			this.mCurYinChangEffect = null;
		}
	}

	// Token: 0x06003683 RID: 13955 RVA: 0x000E0904 File Offset: 0x000DEB04
	public void OnYinChangFinished()
	{
		this.mCurYinChangEffect = null;
	}

	// Token: 0x06003684 RID: 13956 RVA: 0x000E0910 File Offset: 0x000DEB10
	public void PlayYinChangeEffInfo(string effInfoId, float duration, Vector3 senderPos)
	{
		EffInfoData effInfoDataById = DataManager.GetEffInfoDataById(effInfoId);
		if (effInfoDataById != null)
		{
			GameObject gameObject = ResourcesManager.LoadAndInstantiate("Effect/Yinchang") as GameObject;
			if (gameObject != null)
			{
				YinChangEffectControl component = gameObject.GetComponent<YinChangEffectControl>();
				component.InitEffect(effInfoDataById, duration, senderPos, base.transform.rotation, new YinChangEffectControl.onFinishedDelegate(this.OnYinChangFinished));
				this.mCurYinChangEffect = component;
			}
			else
			{
				BundleManager.LoadEffectInList("Yinchang", false, false, new BundleManager.OnLoadModelFinish(this.OnLoadYinchangEffect), new BundleLoadYinChangData(effInfoDataById, duration, senderPos), null);
			}
		}
	}

	// Token: 0x06003685 RID: 13957 RVA: 0x000E099C File Offset: 0x000DEB9C
	private void OnLoadYinchangEffect(object objBundle, object param1 = null, object param2 = null)
	{
		GameObject gameObject = objBundle as GameObject;
		BundleManager.ResetParticleShader(gameObject.transform);
		BundleLoadYinChangData bundleLoadYinChangData = param1 as BundleLoadYinChangData;
		if (gameObject != null)
		{
			YinChangEffectControl component = gameObject.GetComponent<YinChangEffectControl>();
			component.InitEffect(bundleLoadYinChangData.effInfoData, bundleLoadYinChangData.duration, bundleLoadYinChangData.senderPos, base.transform.rotation, new YinChangEffectControl.onFinishedDelegate(this.OnYinChangFinished));
		}
	}

	// Token: 0x06003686 RID: 13958 RVA: 0x000E0A04 File Offset: 0x000DEC04
	public bool EffInfoPlayCheck(PlayEffInfoData effInfo)
	{
		if (effInfo.mActionData.AnimCanBeBreak == 1)
		{
			return true;
		}
		if (effInfo.EffinfoData == null)
		{
			return true;
		}
		if (effInfo.EffinfoData.MoveDistance != 0 && effInfo.EffinfoData.ForceMove != 1)
		{
			return true;
		}
		Debug.Log(effInfo.EffinfoData.MoveDistance);
		Debug.Log(effInfo.EffinfoData.ForceMove);
		return false;
	}

	// Token: 0x06003687 RID: 13959 RVA: 0x000E0A80 File Offset: 0x000DEC80
	public void Init(ObjCharacter objCharacter)
	{
		this.ownCharacter = objCharacter;
	}

	// Token: 0x06003688 RID: 13960 RVA: 0x000E0A8C File Offset: 0x000DEC8C
	public static void PrintCurPlayingEffect()
	{
		Debug.Log("*******************************************");
		List<List<FxControl>> list = new List<List<FxControl>>(EffectLogic.mCurrentPlayEffectDic.Values);
		for (int i = 0; i < list.Count; i++)
		{
			Debug.Log("Cur i :: " + i);
			for (int j = 0; j < list[i].Count; j++)
			{
				Debug.Log(list[i][j].EffectId);
			}
		}
		Debug.Log("******************************************");
		List<List<PlayingEffectData>> list2 = new List<List<PlayingEffectData>>(EffectLogic.mCurrentPlayEffectDataDic.Values);
		for (int k = 0; k < list2.Count; k++)
		{
			Debug.Log("Data i :: " + k);
			for (int l = 0; l < list2[k].Count; l++)
			{
				Debug.Log("Playing Data :: " + list2[k][l].EffectDataId);
			}
		}
	}

	// Token: 0x040023E4 RID: 9188
	private static float EffectAvailableDistance = 20f;

	// Token: 0x040023E5 RID: 9189
	private static int[,] MAX_PLAYING_NUM = new int[,]
	{
		{
			0,
			5,
			0,
			0
		},
		{
			5,
			5,
			8,
			5
		},
		{
			10,
			10,
			16,
			10
		}
	};

	// Token: 0x040023E6 RID: 9190
	private static int MAX_CACHE_NUM = 20;

	// Token: 0x040023E7 RID: 9191
	private static Dictionary<string, List<FxControl>> mCacheEffectDic = new Dictionary<string, List<FxControl>>();

	// Token: 0x040023E8 RID: 9192
	private static List<List<FxControl>> mCacheEffectList = new List<List<FxControl>>();

	// Token: 0x040023E9 RID: 9193
	private Dictionary<string, GameObject> mBindPosDic = new Dictionary<string, GameObject>();

	// Token: 0x040023EA RID: 9194
	private ObjCharacter ownCharacter;

	// Token: 0x040023EB RID: 9195
	public List<PlayEffInfoData> PlayEffInfoDataList = new List<PlayEffInfoData>();

	// Token: 0x040023EC RID: 9196
	private static Dictionary<GameDefine.OBJ_TYPE, List<FxControl>> mCurrentPlayEffectDic = new Dictionary<GameDefine.OBJ_TYPE, List<FxControl>>();

	// Token: 0x040023ED RID: 9197
	private static List<List<FxControl>> mCurrentPlayEffectList = new List<List<FxControl>>();

	// Token: 0x040023EE RID: 9198
	private static Dictionary<GameDefine.OBJ_TYPE, List<PlayingEffectData>> mCurrentPlayEffectDataDic = new Dictionary<GameDefine.OBJ_TYPE, List<PlayingEffectData>>();

	// Token: 0x040023EF RID: 9199
	private YinChangEffectControl mCurYinChangEffect;
}
