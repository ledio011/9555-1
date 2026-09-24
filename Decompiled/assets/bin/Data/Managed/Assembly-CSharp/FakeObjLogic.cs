using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000913 RID: 2323
[Serializable]
public class FakeObjLogic
{
	// Token: 0x06003FEA RID: 16362 RVA: 0x0012B224 File Offset: 0x00129424
	public List<GameObject> GetBodyPartList()
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < this.mPlayerModelObjData.PartModelObjData.Length; i++)
		{
			if (this.mPlayerModelObjData.PartModelObjData[i].ModelObj != null)
			{
				list.Add(this.mPlayerModelObjData.PartModelObjData[i].ModelObj);
			}
		}
		return list;
	}

	// Token: 0x17000F93 RID: 3987
	// (get) Token: 0x06003FEB RID: 16363 RVA: 0x0012B28C File Offset: 0x0012948C
	public string CurModelName
	{
		get
		{
			return this.mModelName;
		}
	}

	// Token: 0x17000F94 RID: 3988
	// (get) Token: 0x06003FEC RID: 16364 RVA: 0x0012B294 File Offset: 0x00129494
	public Animation FakeObjAnim
	{
		get
		{
			if (this.mAnim == null)
			{
				this.mAnim = this.FakeObj.animation;
				if (this.mAnim == null)
				{
					this.mAnim = this.FakeObj.GetComponentInChildren<Animation>();
				}
			}
			return this.mAnim;
		}
	}

	// Token: 0x06003FED RID: 16365 RVA: 0x0012B2EC File Offset: 0x001294EC
	public void StopLoadMesh()
	{
		this.mPlayerModelObjData.StopLoadMesh();
		if (this.mCurNpcLoadingModelData != null)
		{
			this.mCurNpcLoadingModelData.OnLoadFinished = null;
			this.mCurNpcLoadingModelData = null;
		}
	}

	// Token: 0x06003FEE RID: 16366 RVA: 0x0012B318 File Offset: 0x00129518
	public void InitFakeObject(characterVisual visual, string profession, string modelName, Transform parentObj, FakeObjLogic.OnLoadFinishedDel func = null, string layerName = "FakeObj")
	{
		string weaponId = ObjManager.GetWeaponId(visual);
		string headId = ObjManager.GetHeadId(visual);
		string bodyId = ObjManager.GetBodyId(visual);
		string legId = ObjManager.GetLegId(visual);
		this.InitFakeObject(weaponId, headId, bodyId, legId, profession, modelName, parentObj, func, layerName);
	}

	// Token: 0x06003FEF RID: 16367 RVA: 0x0012B354 File Offset: 0x00129554
	private void InitFakeObject(string weaponId, string headId, string bodyId, string legId, string profession, string modelName, Transform parentObj, FakeObjLogic.OnLoadFinishedDel func = null, string layerName = "FakeObj")
	{
		this.LayerStr = layerName;
		if (this.FakeObj == null)
		{
			this.FakeObj = this.CreatePlayerModel(profession, weaponId, headId, bodyId, legId);
			this.mFakeObjParent = parentObj;
			this.FakeObj.transform.parent = this.mFakeObjParent;
			this.FakeObj.transform.localPosition = Vector3.zero;
			this.FakeObj.transform.localRotation = Quaternion.identity;
			this.FakeObj.transform.localScale = Vector3.one;
			this.mModelName = modelName;
			this.mWeaponId = weaponId;
			this.PlayAnim("idle", modelName);
			this.LoadPlayerVisual(this.FakeObj, weaponId, headId, bodyId, legId, func);
		}
		else
		{
			this.mModelName = modelName;
			this.mWeaponId = weaponId;
			this.CheckFakeObject(weaponId, headId, bodyId, legId, func);
		}
	}

	// Token: 0x06003FF0 RID: 16368 RVA: 0x0012B43C File Offset: 0x0012963C
	private void InitFakeObject(string weaponId, string headId, string bodyId, string legId, string profession, Transform parentObj, FakeObjLogic.OnLoadFinishedDel func = null, string layerName = "FakeObj")
	{
		string modelName = string.Empty;
		if (profession.Equals("XD"))
		{
			modelName = "baiRen";
		}
		else if (profession.Equals("QJ"))
		{
			modelName = "heiRen";
		}
		else
		{
			modelName = "nvRen";
		}
		this.InitFakeObject(weaponId, headId, bodyId, legId, profession, modelName, parentObj, func, layerName);
	}

	// Token: 0x06003FF1 RID: 16369 RVA: 0x0012B4A4 File Offset: 0x001296A4
	public void InitFakeObject(characterVisual visual, int profession, Transform parentObj, FakeObjLogic.OnLoadFinishedDel func = null, string layerName = "FakeObj")
	{
		string weaponId = ObjManager.GetWeaponId(visual);
		string headId = ObjManager.GetHeadId(visual);
		string bodyId = ObjManager.GetBodyId(visual);
		string legId = ObjManager.GetLegId(visual);
		this.InitFakeObject(weaponId, headId, bodyId, legId, profession, parentObj, func, layerName);
	}

	// Token: 0x06003FF2 RID: 16370 RVA: 0x0012B4E0 File Offset: 0x001296E0
	public void InitFakeObject(characterVisual visual, PROFESSION_TYPE profession, Transform parentObj, FakeObjLogic.OnLoadFinishedDel func = null, string layerName = "FakeObj")
	{
		string weaponId = ObjManager.GetWeaponId(visual);
		string headId = ObjManager.GetHeadId(visual);
		string bodyId = ObjManager.GetBodyId(visual);
		string legId = ObjManager.GetLegId(visual);
		this.InitFakeObject(weaponId, headId, bodyId, legId, profession, parentObj, func, layerName);
	}

	// Token: 0x06003FF3 RID: 16371 RVA: 0x0012B51C File Offset: 0x0012971C
	public void InitFakeObject(string weaponId, string headId, string bodyId, string legId, PROFESSION_TYPE profession, Transform parentObj, FakeObjLogic.OnLoadFinishedDel func = null, string layerName = "FakeObj")
	{
		this.InitFakeObject(weaponId, headId, bodyId, legId, (int)profession, parentObj, func, "FakeObj");
	}

	// Token: 0x06003FF4 RID: 16372 RVA: 0x0012B540 File Offset: 0x00129740
	public void InitFakeObject(string weaponId, string headId, string bodyId, string legId, int profession, Transform parentObj, FakeObjLogic.OnLoadFinishedDel func = null, string layerName = "FakeObj")
	{
		string modelName = string.Empty;
		string profession2 = string.Empty;
		if (profession == 0)
		{
			modelName = "baiRen";
			profession2 = "XD";
		}
		else if (profession == 1)
		{
			modelName = "heiRen";
			profession2 = "QJ";
		}
		else
		{
			modelName = "nvRen";
			profession2 = "NQS";
		}
		this.mWeaponId = weaponId;
		this.InitFakeObject(weaponId, headId, bodyId, legId, profession2, modelName, parentObj, func, layerName);
	}

	// Token: 0x06003FF5 RID: 16373 RVA: 0x0012B5B0 File Offset: 0x001297B0
	public void InitFakeObject(string weaponId, string headId, string bodyId, string legId, Transform parentObj, FakeObjLogic.OnLoadFinishedDel func = null, string layerName = "NGUI")
	{
		string text = headId.Substring(0, headId.IndexOf('_'));
		string modelName = string.Empty;
		if (text.Equals("XD"))
		{
			modelName = "baiRen";
		}
		else if (text.Equals("QJ"))
		{
			modelName = "heiRen";
		}
		else
		{
			modelName = "nvRen";
		}
		this.mWeaponId = weaponId;
		this.InitFakeObject(weaponId, headId, bodyId, legId, text, modelName, parentObj, func, layerName);
	}

	// Token: 0x06003FF6 RID: 16374 RVA: 0x0012B62C File Offset: 0x0012982C
	private GameObject CreatePlayerModel(string profession, string weaponId, string headId, string bodyId, string legId)
	{
		return ResourcesManager.LoadAndInstantiate("TestModel/Model/" + profession + "/ModelRoot") as GameObject;
	}

	// Token: 0x06003FF7 RID: 16375 RVA: 0x0012B658 File Offset: 0x00129858
	private void LoadPlayerVisual(GameObject playerRootObj, string partWeaponId, string partHeadId, string partBodyId, string partLegId, FakeObjLogic.OnLoadFinishedDel func = null)
	{
		this.OnLoadFinished = func;
		this.mLoadPartNum = 0;
		this.mLoadPartCount = 0;
		ModelData modeDataByID = DataManager.GetModeDataByID(partWeaponId);
		ModelData modeDataByID2 = DataManager.GetModeDataByID(partHeadId);
		ModelData modeDataByID3 = DataManager.GetModeDataByID(partBodyId);
		ModelData modeDataByID4 = DataManager.GetModeDataByID(partLegId);
		this.mPlayerModelObjData.StopLoadMesh();
		if (modeDataByID != null)
		{
			this.mPlayerModelObjData.LoadModel(MODEL_TYPE.WEAPON, partWeaponId, playerRootObj, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), ref this.mLoadPartNum, this.LayerStr);
		}
		if (modeDataByID2 != null)
		{
			this.mPlayerModelObjData.LoadModel(MODEL_TYPE.HEAD, partHeadId, playerRootObj, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), ref this.mLoadPartNum, this.LayerStr);
		}
		if (modeDataByID3 != null)
		{
			this.mPlayerModelObjData.LoadModel(MODEL_TYPE.BODY, partBodyId, playerRootObj, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), ref this.mLoadPartNum, this.LayerStr);
		}
		if (modeDataByID4 != null)
		{
			this.mPlayerModelObjData.LoadModel(MODEL_TYPE.LEG, partLegId, playerRootObj, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), ref this.mLoadPartNum, this.LayerStr);
		}
	}

	// Token: 0x06003FF8 RID: 16376 RVA: 0x0012B758 File Offset: 0x00129958
	private void OnLoadPlayerPartFinished(object objBundle, object param1 = null, object param2 = null)
	{
		ModelData modelData = param1 as ModelData;
		GameObject gameObject = param2 as GameObject;
		GameObject gameObject2 = objBundle as GameObject;
		BundleManager.ResetShader(gameObject2.transform);
		gameObject2.layer = LayerMask.NameToLayer(this.LayerStr);
		gameObject2.transform.parent = gameObject.transform;
		gameObject2.transform.localPosition = Vector3.up * 1.5f;
		gameObject2.transform.localScale = Vector3.one;
		gameObject2.transform.localRotation = Quaternion.identity;
		gameObject2.gameObject.name = modelData.Name;
		BundleManager.RebuildBones(gameObject, gameObject2);
		this.mPlayerModelObjData.LoadModelFinished(modelData.ModelType, modelData, gameObject2);
		this.mLoadPartCount++;
		if (this.mLoadPartCount >= this.mLoadPartNum)
		{
			if (UnityVersionUtil.IsActive(gameObject))
			{
				this.mPlayerModelObjData.ActiveModelObj();
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(gameObject2.gameObject, false);
			}
			this.OnInitFinished();
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(gameObject2.gameObject, false);
		}
	}

	// Token: 0x06003FF9 RID: 16377 RVA: 0x0012B86C File Offset: 0x00129A6C
	private void OnInitFinished()
	{
		if (this.OnLoadFinished != null)
		{
			this.OnLoadFinished(this);
		}
	}

	// Token: 0x06003FFA RID: 16378 RVA: 0x0012B888 File Offset: 0x00129A88
	public void CheckFakeObject(characterVisual visual, FakeObjLogic.OnLoadFinishedDel func = null)
	{
		string weaponId = ObjManager.GetWeaponId(visual);
		string headId = ObjManager.GetHeadId(visual);
		string bodyId = ObjManager.GetBodyId(visual);
		string legId = ObjManager.GetLegId(visual);
		this.CheckFakeObject(weaponId, headId, bodyId, legId, func);
	}

	// Token: 0x06003FFB RID: 16379 RVA: 0x0012B8BC File Offset: 0x00129ABC
	public void CheckFakeObject(string weaponId, string headId, string bodyId, string legId, FakeObjLogic.OnLoadFinishedDel func = null)
	{
		this.OnLoadFinished = func;
		this.mChangePartNum = 0;
		this.mChangePartCount = 0;
		this.mWeaponId = weaponId;
		this.PlayAnim("idle", this.mModelName);
		if (this.FakeObj != null)
		{
			this.FakeObj.transform.parent = this.mFakeObjParent;
			this.FakeObj.transform.localPosition = Vector3.zero;
			this.FakeObj.transform.localRotation = Quaternion.identity;
			this.FakeObj.transform.localScale = Vector3.one;
		}
		this.mPlayerModelObjData.LoadModel(MODEL_TYPE.WEAPON, weaponId, this.FakeObj, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), ref this.mChangePartNum, this.LayerStr);
		this.mPlayerModelObjData.LoadModel(MODEL_TYPE.HEAD, headId, this.FakeObj, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), ref this.mChangePartNum, this.LayerStr);
		this.mPlayerModelObjData.LoadModel(MODEL_TYPE.BODY, bodyId, this.FakeObj, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), ref this.mChangePartNum, this.LayerStr);
		this.mPlayerModelObjData.LoadModel(MODEL_TYPE.LEG, legId, this.FakeObj, new BundleManager.OnLoadModelFinish(this.OnLoadPlayerPartFinished), ref this.mChangePartNum, this.LayerStr);
		if (this.mChangePartNum == 0)
		{
			this.OnInitFinished();
			return;
		}
	}

	// Token: 0x06003FFC RID: 16380 RVA: 0x0012BA1C File Offset: 0x00129C1C
	private void OnChangePartLoadFinished(object objBundle, object param1 = null, object param2 = null)
	{
		ModelData modelData = param1 as ModelData;
		GameObject gameObject = param2 as GameObject;
		GameObject gameObject2 = objBundle as GameObject;
		BundleManager.ResetShader(gameObject2.transform);
		gameObject2.layer = LayerMask.NameToLayer(this.LayerStr);
		gameObject2.transform.parent = gameObject.transform;
		gameObject2.transform.localPosition = Vector3.zero;
		gameObject2.transform.localScale = Vector3.one;
		UnityVersionUtil.SetActiveRecursive(gameObject2.gameObject, true);
		gameObject2.gameObject.name = modelData.Name;
		BundleManager.RebuildBones(gameObject, gameObject2);
		this.mPlayerModelObjData.LoadModelFinished(modelData.ModelType, modelData, gameObject2);
		this.mChangePartCount++;
		if (this.mChangePartCount >= this.mChangePartNum)
		{
			this.OnInitFinished();
		}
	}

	// Token: 0x06003FFD RID: 16381 RVA: 0x0012BAE8 File Offset: 0x00129CE8
	public void DestroyFakeObj()
	{
		if (this.FakeObj != null)
		{
			this.mPlayerModelObjData.UnloadAllModel();
			Object.Destroy(this.FakeObj);
			this.FakeObj = null;
		}
		this.mAnim = null;
	}

	// Token: 0x06003FFE RID: 16382 RVA: 0x0012BB20 File Offset: 0x00129D20
	public void PlayAnim(string animationName, string modelType)
	{
		Animation fakeObjAnim = this.FakeObjAnim;
		string text = modelType;
		if (!string.IsNullOrEmpty(this.mWeaponId))
		{
			text = string.Format("{0}_{1}", modelType, GameDefine.GetWeaponName(this.mWeaponId));
		}
		AnimationState animationState = fakeObjAnim[string.Format("{0}_{1}", text, animationName)];
		if (animationState == null)
		{
			AnimationClip animationClip = AnimationManager.LoadAnimation(text, animationName) as AnimationClip;
			if (animationClip != null)
			{
				fakeObjAnim.AddClip(animationClip, string.Format("{0}_{1}", text, animationName));
			}
		}
		fakeObjAnim.Play(string.Format("{0}_{1}", text, animationName));
	}

	// Token: 0x06003FFF RID: 16383 RVA: 0x0012BBBC File Offset: 0x00129DBC
	public string GetAnimaStateName(string animationName, CharacterModelData modelData)
	{
		if (modelData.TypeID == 0)
		{
			string text = modelData.ModelFirstType;
			if (!string.IsNullOrEmpty(this.mWeaponId))
			{
				text = string.Format("{0}_{1}", text, GameDefine.GetWeaponName(this.mWeaponId));
			}
			return string.Format("{0}_{1}", text, animationName);
		}
		return animationName;
	}

	// Token: 0x06004000 RID: 16384 RVA: 0x0012BC10 File Offset: 0x00129E10
	public string GetAnimaStateName(string animationName)
	{
		string text = this.mModelName;
		if (!string.IsNullOrEmpty(this.mWeaponId))
		{
			text = string.Format("{0}_{1}", text, GameDefine.GetWeaponName(this.mWeaponId));
			return string.Format("{0}_{1}", text, animationName);
		}
		return animationName;
	}

	// Token: 0x06004001 RID: 16385 RVA: 0x0012BC5C File Offset: 0x00129E5C
	public void ActivePlayerModelObj()
	{
		this.mPlayerModelObjData.ActiveModelObj();
	}

	// Token: 0x06004002 RID: 16386 RVA: 0x0012BC6C File Offset: 0x00129E6C
	public void PlayAnim(string animationName, CharacterModelData modelData)
	{
		Animation fakeObjAnim = this.FakeObjAnim;
		AnimationState animationState = fakeObjAnim[this.GetAnimaStateName(animationName, modelData)];
		if (animationState == null)
		{
			if (modelData.TypeID == 0)
			{
				string text = modelData.ModelFirstType;
				if (!string.IsNullOrEmpty(this.mWeaponId))
				{
					text = string.Format("{0}_{1}", text, GameDefine.GetWeaponName(this.mWeaponId));
				}
				AnimationClip animationClip = AnimationManager.LoadAnimation(text, animationName) as AnimationClip;
				if (animationClip != null)
				{
					fakeObjAnim.AddClip(animationClip, string.Format("{0}_{1}", text, animationName));
				}
			}
			else
			{
				AnimationClip animationClip2 = AnimationManager.LoadAnimation(modelData.ModelFirstType, animationName) as AnimationClip;
				if (animationClip2 != null)
				{
					fakeObjAnim.AddClip(animationClip2, animationName);
				}
				else
				{
					animationClip2 = (AnimationManager.LoadAnimation(modelData.ModelSubType, animationName) as AnimationClip);
					if (animationClip2 != null)
					{
						fakeObjAnim.AddClip(animationClip2, animationName);
					}
					else
					{
						animationClip2 = (AnimationManager.LoadAnimation(modelData.ModelType, animationName) as AnimationClip);
						if (animationClip2 != null)
						{
							fakeObjAnim.AddClip(animationClip2, animationName);
						}
					}
				}
			}
		}
		fakeObjAnim.enabled = false;
		fakeObjAnim.enabled = true;
		fakeObjAnim.Play(this.GetAnimaStateName(animationName, modelData));
	}

	// Token: 0x06004003 RID: 16387 RVA: 0x0012BDA8 File Offset: 0x00129FA8
	public void CrossFadeAnima(string animationName, string modelType)
	{
		Animation fakeObjAnim = this.FakeObjAnim;
		string text = modelType;
		if (!string.IsNullOrEmpty(this.mWeaponId))
		{
			text = string.Format("{0}_{1}", modelType, GameDefine.GetWeaponName(this.mWeaponId));
		}
		AnimationState animationState = fakeObjAnim[string.Format("{0}_{1}", text, animationName)];
		if (animationState == null)
		{
			AnimationClip animationClip = AnimationManager.LoadAnimation(text, animationName) as AnimationClip;
			fakeObjAnim.AddClip(animationClip, string.Format("{0}_{1}", text, animationName));
		}
		fakeObjAnim.CrossFade(string.Format("{0}_{1}", text, animationName), 0.3f);
	}

	// Token: 0x06004004 RID: 16388 RVA: 0x0012BE3C File Offset: 0x0012A03C
	public void CrossFadeIdelSelect(int profession, string modelType)
	{
		if (GameDefine.GetWeaponType(this.mWeaponId) != -1)
		{
			this.CrossFadeAnima(GameDefine.GetRoleIdelSelectName(profession, GameDefine.GetWeaponType(this.mWeaponId)), modelType);
		}
	}

	// Token: 0x06004005 RID: 16389 RVA: 0x0012BE68 File Offset: 0x0012A068
	public void UseSkill(string skillId, AnimationLogic.OnAnimFinished func)
	{
		string modelType = this.mModelName;
		if (!string.IsNullOrEmpty(this.mWeaponId))
		{
			modelType = string.Format("{0}_{1}", this.mModelName, GameDefine.GetWeaponName(this.mWeaponId));
		}
		if (this.mShowSkillLogic == null)
		{
			this.mShowSkillLogic = this.FakeObj.AddComponent<FakeObjSkillShowLogic>();
			this.mShowSkillLogic.Reset(this.FakeObj, modelType);
		}
		this.mShowSkillLogic.UseSkill(skillId, modelType, func);
	}

	// Token: 0x06004006 RID: 16390 RVA: 0x0012BEEC File Offset: 0x0012A0EC
	public void DisableNpcAnimaHandle()
	{
		this.npcAnimaHandle.Cancel();
	}

	// Token: 0x06004007 RID: 16391 RVA: 0x0012BEFC File Offset: 0x0012A0FC
	public void playNpcAnim(string animationName, CharacterModelData characterModelData)
	{
		Animation fakeObjAnim = this.FakeObjAnim;
		if (fakeObjAnim == null)
		{
			return;
		}
		AnimationState animationState = fakeObjAnim[animationName];
		if (animationState == null)
		{
			AnimationClip animationClip = AnimationManager.LoadAnimation(characterModelData.ModelFirstType, animationName) as AnimationClip;
			if (animationClip != null)
			{
				fakeObjAnim.AddClip(animationClip, animationName);
			}
			else
			{
				animationClip = (AnimationManager.LoadAnimation(characterModelData.ModelSubType, animationName) as AnimationClip);
				if (animationClip != null)
				{
					fakeObjAnim.AddClip(animationClip, animationName);
				}
				else
				{
					animationClip = (AnimationManager.LoadAnimation(characterModelData.ModelType, animationName) as AnimationClip);
					if (animationClip != null)
					{
						fakeObjAnim.AddClip(animationClip, animationName);
					}
				}
			}
			if (animationClip != null)
			{
				fakeObjAnim.CrossFade(animationName);
				if (animationName.Equals("talk"))
				{
					float delay = Mathf.Max(animationClip.length - 0.3f, 0f);
					vp_Timer.In(delay, delegate()
					{
						this.playNpcAnim("idle", characterModelData);
					}, this.npcAnimaHandle);
				}
			}
		}
		else
		{
			fakeObjAnim.CrossFade(animationName);
			if (animationName.Equals("talk"))
			{
				float delay2 = Mathf.Max(fakeObjAnim["talk"].length - 0.3f, 0f);
				vp_Timer.In(delay2, delegate()
				{
					this.playNpcAnim("idle", characterModelData);
				}, this.npcAnimaHandle);
			}
		}
	}

	// Token: 0x06004008 RID: 16392 RVA: 0x0012C084 File Offset: 0x0012A284
	public void onLoadNpcFinished(object modelBundle, object param1, object param2)
	{
		this.mCurNpcLoadingModelData = null;
		this.mCurNpcLoadingModelDataId = -1L;
		GameObject gameObject = modelBundle as GameObject;
		Transform transform = param1 as Transform;
		CharacterModelData characterModelData = param2 as CharacterModelData;
		if (gameObject == null)
		{
			Log.ERROR_MSG("Instantiate Bundle Error");
			return;
		}
		gameObject.gameObject.name = string.Format("MeshRoot", new object[0]);
		Material material = null;
		BundleManager.ResetShader(gameObject.transform, out material);
		this.FakeObj = gameObject;
		this.mFakeObjParent = transform;
		this.FakeObj.transform.parent = this.mFakeObjParent;
		this.FakeObj.transform.localScale = Vector3.one;
		this.FakeObj.transform.localPosition = Vector3.zero;
		this.FakeObj.transform.localRotation = Quaternion.identity;
		UnityVersionUtil.SetActiveRecursive(gameObject.gameObject, true);
		NGUITools.SetLayer(gameObject, LayerMask.NameToLayer(this.LayerStr));
		this.playNpcAnim("talk", characterModelData);
	}

	// Token: 0x06004009 RID: 16393 RVA: 0x0012C184 File Offset: 0x0012A384
	public void InitFakeNpcObj(string npcModelId, Transform parentObj, FakeObjLogic.OnLoadFinishedDel func = null)
	{
		CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(npcModelId);
		if (!this.mCurNpcModelId.Equals(npcModelId))
		{
			if (this.FakeObj != null)
			{
				this.DestroyNpcFakeObj();
			}
			if (this.FakeObj == null)
			{
				if (this.mCurNpcLoadingModelData != null)
				{
					this.mCurNpcLoadingModelData.OnLoadFinished = null;
					this.mCurNpcLoadingModelData = null;
				}
				this.mCurNpcLoadingModelData = BundleManager.LoadModelInList(characterModelDataByID.Name, true, false, new BundleManager.OnLoadModelFinish(this.onLoadNpcFinished), parentObj, characterModelDataByID, null);
				this.mCurNpcLoadingModelDataId = ((this.mCurNpcLoadingModelData != null) ? this.mCurNpcLoadingModelData.ID : -1L);
				this.mModelName = characterModelDataByID.Name;
			}
		}
		else
		{
			this.playNpcAnim("talk", characterModelDataByID);
		}
		this.mCurNpcModelId = npcModelId;
	}

	// Token: 0x0600400A RID: 16394 RVA: 0x0012C258 File Offset: 0x0012A458
	public void InitAnimaFakeNpcObj(string npcModelId, Transform parentObj, string firstAnimaName)
	{
		CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(npcModelId);
		if (!this.mCurNpcModelId.Equals(npcModelId))
		{
			this.mCurNpcModelId = npcModelId;
			if (this.FakeObj != null)
			{
				this.DestroyNpcFakeObj();
			}
			if (this.FakeObj == null)
			{
				if (this.mCurNpcLoadingModelData != null)
				{
					this.mCurNpcLoadingModelData.OnLoadFinished = null;
					this.mCurNpcLoadingModelData = null;
				}
				this.mCurNpcLoadingModelData = BundleManager.LoadModelInList(characterModelDataByID.Name, true, false, new BundleManager.OnLoadModelFinish(this.onLoadAnimaNpcFinished), parentObj, firstAnimaName, null);
				this.mCurNpcLoadingModelDataId = ((this.mCurNpcLoadingModelData != null) ? this.mCurNpcLoadingModelData.ID : -1L);
				this.mModelName = characterModelDataByID.Name;
			}
		}
		else
		{
			this.playNpcAnim("talk", characterModelDataByID);
		}
	}

	// Token: 0x0600400B RID: 16395 RVA: 0x0012C32C File Offset: 0x0012A52C
	public void onLoadAnimaNpcFinished(object modelBundle, object param1, object param2)
	{
		this.mCurNpcLoadingModelData = null;
		this.mCurNpcLoadingModelDataId = -1L;
		GameObject gameObject = modelBundle as GameObject;
		Transform transform = param1 as Transform;
		string animationName = param2 as string;
		if (gameObject == null)
		{
			Log.ERROR_MSG("Instantiate Bundle Error");
			return;
		}
		gameObject.gameObject.name = string.Format("MeshRoot", new object[0]);
		Material material = null;
		BundleManager.ResetShader(gameObject.transform, out material);
		NGUITools.SetLayer(transform.gameObject, LayerMask.NameToLayer(this.LayerStr));
		this.FakeObj = gameObject;
		this.mFakeObjParent = transform;
		this.FakeObj.transform.parent = this.mFakeObjParent;
		this.FakeObj.transform.localScale = Vector3.one;
		this.FakeObj.transform.localPosition = Vector3.zero;
		this.FakeObj.transform.localRotation = Quaternion.identity;
		UnityVersionUtil.SetActiveRecursive(gameObject.gameObject, true);
		this.FakeObjAnim.cullingType = 0;
		this.PlayAnim(animationName, DataManager.GetCharacterModelDataByID(this.mCurNpcModelId));
	}

	// Token: 0x0600400C RID: 16396 RVA: 0x0012C444 File Offset: 0x0012A644
	public void DestroyNpcFakeObj()
	{
		if (this.mCurNpcLoadingModelData != null)
		{
			this.mCurNpcLoadingModelData.OnLoadFinished = null;
			this.mCurNpcLoadingModelData = null;
			BundleManager.RemoveFromLoadModelList(this.mCurNpcLoadingModelDataId);
			this.mCurNpcLoadingModelDataId = -1L;
		}
		if (this.FakeObj != null)
		{
			this.npcAnimaHandle.Cancel();
			Object.Destroy(this.FakeObj);
			this.FakeObj = null;
			this.mAnim = null;
			BundleManager.UnloadModel(this.mCurNpcModelId, -1L, false);
		}
	}

	// Token: 0x04002BC8 RID: 11208
	private const string talkName = "talk";

	// Token: 0x04002BC9 RID: 11209
	private FakeObjLogic.OnLoadFinishedDel OnLoadFinished;

	// Token: 0x04002BCA RID: 11210
	public GameObject FakeObj;

	// Token: 0x04002BCB RID: 11211
	private Transform mFakeObjParent;

	// Token: 0x04002BCC RID: 11212
	private PlayerModelObjData mPlayerModelObjData = new PlayerModelObjData();

	// Token: 0x04002BCD RID: 11213
	private string mModelName;

	// Token: 0x04002BCE RID: 11214
	private string mWeaponId;

	// Token: 0x04002BCF RID: 11215
	private Animation mAnim;

	// Token: 0x04002BD0 RID: 11216
	private FakeObjSkillShowLogic mShowSkillLogic;

	// Token: 0x04002BD1 RID: 11217
	private string LayerStr = "FakeObj";

	// Token: 0x04002BD2 RID: 11218
	private int mLoadPartNum;

	// Token: 0x04002BD3 RID: 11219
	private int mLoadPartCount;

	// Token: 0x04002BD4 RID: 11220
	private int mChangePartNum;

	// Token: 0x04002BD5 RID: 11221
	private int mChangePartCount;

	// Token: 0x04002BD6 RID: 11222
	private vp_Timer.Handle npcAnimaHandle = new vp_Timer.Handle();

	// Token: 0x04002BD7 RID: 11223
	private string mCurNpcModelId = string.Empty;

	// Token: 0x04002BD8 RID: 11224
	private BundleManager.LoadModelData mCurNpcLoadingModelData;

	// Token: 0x04002BD9 RID: 11225
	private long mCurNpcLoadingModelDataId;

	// Token: 0x02000AED RID: 2797
	// (Invoke) Token: 0x0600503D RID: 20541
	public delegate void OnLoadFinishedDel(FakeObjLogic obj);
}
