using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x020009F0 RID: 2544
public class CreateRoleRootLogic : SingletonUnity<CreateRoleRootLogic>
{
	// Token: 0x060048A5 RID: 18597 RVA: 0x00175E08 File Offset: 0x00174008
	private new void Awake()
	{
		base.Awake();
		this.RotateScrollView.onMoveOver = new DelegateDefine.OneIntParamDelegate(this.OnChangeRole);
		this.mModelRoot = GameObject.Find("PlayerModelRoot").transform;
		if (this.mModelRoot == null)
		{
			Debug.Log("mModelRoot == null");
		}
		this.mModelData[0] = DataManager.GetCharacterModelDataByID("100");
		this.mModelData[1] = DataManager.GetCharacterModelDataByID("104");
		this.mModelData[2] = DataManager.GetCharacterModelDataByID("105");
		this.RotateModelBtn.onDrag = new UIEventListener.VectorDelegate(this.OnDragModel);
		this.NameTipsLabel.text = StrDictionary.GetDictionaryString("#{200056}", new object[]
		{
			this.NameMinLength,
			this.NameMaxLength
		});
	}

	// Token: 0x060048A6 RID: 18598 RVA: 0x00175EE8 File Offset: 0x001740E8
	public void Reset(bool isNewAccount)
	{
		this.NewAccountFlag = isNewAccount;
		this.OnChangeRole(this.mCurRoleIndex);
	}

	// Token: 0x060048A7 RID: 18599 RVA: 0x00175F00 File Offset: 0x00174100
	private void OnChangeRole(int roleIndex)
	{
		this.SetChangeRoleEnable(false);
		this.EnableRoleModel(roleIndex);
		this.InsideAttrPic.UpdatePos(this.mAttrPicPos[roleIndex]);
	}

	// Token: 0x060048A8 RID: 18600 RVA: 0x00175F24 File Offset: 0x00174124
	private void SetChangeRoleEnable(bool isEnable)
	{
		this.RotateScrollView.EnableChangeFlag = true;
	}

	// Token: 0x17000FBE RID: 4030
	// (get) Token: 0x060048A9 RID: 18601 RVA: 0x00175F34 File Offset: 0x00174134
	// (set) Token: 0x060048AA RID: 18602 RVA: 0x00175F70 File Offset: 0x00174170
	public Material XRayMat
	{
		get
		{
			if (this.mXRayMat == null)
			{
				this.mXRayMat = (ResourcesManager.Load("Material/XRay") as Material);
			}
			return this.mXRayMat;
		}
		set
		{
			this.mXRayMat = value;
		}
	}

	// Token: 0x060048AB RID: 18603 RVA: 0x00175F7C File Offset: 0x0017417C
	private void ResetCurModelShadow(FakeObjLogic obj)
	{
		if (!GameSettingData.IsShowPlayerShadow[GameSettingData.GetPhoneClass()])
		{
			return;
		}
		List<GameObject> bodyPartList = obj.GetBodyPartList();
		for (int i = 0; i < bodyPartList.Count; i++)
		{
			SkinnedMeshRenderer component = bodyPartList[i].GetComponent<SkinnedMeshRenderer>();
			Material[] array = new Material[component.materials.Length + 1];
			bool flag = false;
			for (int j = 0; j < component.materials.Length; j++)
			{
				if (component.materials[j].name.Contains("XRay"))
				{
					flag = true;
					break;
				}
				array[j] = component.materials[j];
			}
			if (!flag)
			{
				array[component.materials.Length] = this.XRayMat;
				component.materials = array;
			}
		}
		if (!UnityVersionUtil.IsActive(obj.FakeObj.gameObject))
		{
			return;
		}
		if (!SingletonUnity<RealTimeShadow>.Exists)
		{
			GameObject gameObject = ResourcesManager.LoadAndInstantiate("Items/PlayerRealTimeShadow") as GameObject;
			RealTimeShadow component2 = gameObject.GetComponent<RealTimeShadow>();
			component2.Reset(bodyPartList);
		}
		else
		{
			SingletonUnity<RealTimeShadow>.Instance.Reset(bodyPartList);
		}
	}

	// Token: 0x060048AC RID: 18604 RVA: 0x0017609C File Offset: 0x0017429C
	private void EnableRoleModel(int index)
	{
		this.mCurRoleIndex = index;
		for (int i = 0; i < this.mFakeObjList.Length; i++)
		{
			if (i == index)
			{
				if (this.mFakeObjList[i] != null && this.mFakeObjList[i].FakeObj != null)
				{
					this.mFakeObjList[i].FakeObj.transform.localEulerAngles = Vector3.zero;
					UnityVersionUtil.SetActiveRecursive(this.mFakeObjList[i].FakeObj.gameObject, true);
					this.mFakeObjList[i].PlayAnim(GameDefine.ShowSelectAnimaName, this.mModelData[i]);
					this.SetChangeRoleEnable(true);
					this.ResetCurModelShadow(this.mFakeObjList[i]);
				}
				else
				{
					if (this.mFakeObjList[i] == null)
					{
						this.mFakeObjList[i] = new FakeObjLogic();
					}
					this.mFakeObjList[i].InitFakeObject(this.mDefaultModel[i][0], this.mDefaultModel[i][1], this.mDefaultModel[i][2], this.mDefaultModel[i][3], i, this.mModelRoot, new FakeObjLogic.OnLoadFinishedDel(this.OnLoadModelFinish), "ShadowCaster");
				}
			}
			else if (this.mFakeObjList[i] != null && this.mFakeObjList[i].FakeObj != null && UnityVersionUtil.IsActive(this.mFakeObjList[i].FakeObj))
			{
				UnityVersionUtil.SetActiveRecursive(this.mFakeObjList[i].FakeObj, false);
			}
		}
		this.ProfessionLabel.text = StrDictionary.GetDictionaryString(this.mProfessionName[this.mCurRoleIndex], new object[0]);
		this.CharacterGetRandomName();
	}

	// Token: 0x060048AD RID: 18605 RVA: 0x00176244 File Offset: 0x00174444
	private void OnLoadModelFinish(FakeObjLogic obj)
	{
		if (obj == this.mFakeObjList[this.mCurRoleIndex])
		{
			this.mFakeObjList[this.mCurRoleIndex].PlayAnim(GameDefine.ShowSelectAnimaName, this.mModelData[this.mCurRoleIndex]);
			this.SetChangeRoleEnable(true);
			this.ResetCurModelShadow(this.mFakeObjList[this.mCurRoleIndex]);
		}
	}

	// Token: 0x060048AE RID: 18606 RVA: 0x001762A4 File Offset: 0x001744A4
	public void OnClickRandomNameBtn()
	{
		this.CharacterGetRandomName();
	}

	// Token: 0x060048AF RID: 18607 RVA: 0x001762AC File Offset: 0x001744AC
	public void OnClickCreateBtn()
	{
		if (!string.IsNullOrEmpty(this.NameInput.value))
		{
			this.NameInput.value = this.NameInput.value.Trim();
		}
		if (!string.IsNullOrEmpty(this.NameInput.value))
		{
			if (this.NameInput.value.Length < this.NameMinLength || this.NameInput.value.Length > this.NameMaxLength)
			{
				MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{200060}", new object[]
				{
					this.NameMinLength,
					this.NameMaxLength
				}), "#{100127}", null);
				return;
			}
			if (!this.CheckNameIsRight(this.NameInput.value))
			{
				MessageBoxLogic.OpenOKBox("#{200058}", "#{100127}", null);
				return;
			}
			WaitResponseUIRootLogic.OpenWaitBox(104, 0f, 0f, null);
			character_create.request request = new character_create.request();
			general general = new general();
			general.name = this.NameInput.value;
			if (this.mCurRoleIndex == 0)
			{
				general.profession = 0L;
			}
			else if (this.mCurRoleIndex == 1)
			{
				general.profession = 1L;
			}
			else
			{
				general.profession = 2L;
			}
			request.character = general;
			NetLogic.GetInstance().Send<Protocol.character_create>(request, new RpcRspHandler(this.CharacterCreateResponse));
		}
		else
		{
			MessageBoxLogic.OpenOKBox("#{200057}", "#{100127}", null);
		}
	}

	// Token: 0x060048B0 RID: 18608 RVA: 0x00176430 File Offset: 0x00174630
	private bool CheckNameIsRight(string namestr)
	{
		string text = "^[a-zA-Z0-9]{1}([a-zA-Z0-9]|[ _]){4,15}$";
		return Regex.IsMatch(namestr, text);
	}

	// Token: 0x060048B1 RID: 18609 RVA: 0x00176454 File Offset: 0x00174654
	private void CharacterCreateResponse(SprotoTypeBase req)
	{
		character_create.response response = req as character_create.response;
		if (response != null)
		{
			if (response.errno == 0L)
			{
				if (SingletonUnity<ChooseRoleRootLogic>.Exists)
				{
					LocalDataSaveManager.SetChooseRoleIndex(SingletonUnity<ChooseRoleRootLogic>.Instance.RoleCount);
				}
				else
				{
					LocalDataSaveManager.SetChooseRoleIndex(0);
				}
				this.DisactiveCurCreateRole();
				if (response.HasCharacter)
				{
					if (response.character.general.profession == 0L)
					{
						SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("CharacterEvent", "Profession", "XD");
					}
					else if (response.character.general.profession == 1L)
					{
						SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("CharacterEvent", "Profession", "QJ");
					}
					else if (response.character.general.profession == 2L)
					{
						SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("CharacterEvent", "Profession", "NQS");
					}
					PlayerData.MainPlayerServerId = response.character.id;
					GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
					PlayerData playerData = instance.PlayerData;
					PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
					playerCommonData.CreateTime = response.character.createtime;
					playerData.Level = (int)response.character.attribute_other.level;
					playerData.IsTutorialFinish = false;
					WaitResponseUIRootLogic.OpenWaitBox(105, 0f, 0f, null);
					character_pick.request request = new character_pick.request();
					request.id = PlayerData.MainPlayerServerId;
					NetLogic.GetInstance().Send<Protocol.character_pick>(request, new RpcRspHandler(SingletonDontDestoryUnity<NetManager>.Instance.PickResponse));
					instance.FirstEnterGame = true;
					instance.IsShowMainMissionTip = true;
					instance.PlayerData.ViewType = LocalDataSaveManager.GetCameraViewType(PlayerData.MainPlayerServerId);
				}
			}
			else if (response.errno == 1L)
			{
				WaitResponseUIRootLogic.CloseBox();
				NoticeLogic.AddNotifyData("#{100150}", true, false);
			}
			else if (response.errno == 2L)
			{
				WaitResponseUIRootLogic.CloseBox();
				NoticeLogic.AddNotifyData("#{100151}", true, false);
			}
			else if (response.errno == 3L)
			{
				WaitResponseUIRootLogic.CloseBox();
				NoticeLogic.AddNotifyData("#{100152}", true, false);
			}
		}
	}

	// Token: 0x060048B2 RID: 18610 RVA: 0x0017666C File Offset: 0x0017486C
	public void OnClickBackBtn()
	{
		this.DisactiveCurCreateRole();
		if (SingletonUnity<ChooseRoleRootLogic>.Exists)
		{
			if (this.NewAccountFlag)
			{
				SingletonUnity<MenuSceneController>.Instance.ShowChooseServer();
			}
			else
			{
				SingletonUnity<MenuSceneController>.Instance.ShowChooseRole(null);
			}
		}
		else
		{
			SingletonUnity<MenuSceneController>.Instance.ShowChooseServer();
		}
	}

	// Token: 0x060048B3 RID: 18611 RVA: 0x001766C0 File Offset: 0x001748C0
	public void DisactiveCurCreateRole()
	{
		if (this.mFakeObjList != null && this.mFakeObjList[this.mCurRoleIndex] != null && this.mFakeObjList[this.mCurRoleIndex].FakeObj != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mFakeObjList[this.mCurRoleIndex].FakeObj, false);
		}
	}

	// Token: 0x060048B4 RID: 18612 RVA: 0x00176720 File Offset: 0x00174920
	private void OnDisable()
	{
		this.DisactiveCurCreateRole();
	}

	// Token: 0x060048B5 RID: 18613 RVA: 0x00176728 File Offset: 0x00174928
	private void CharacterGetRandomName()
	{
		long type = 0L;
		if (this.mCurRoleIndex == 2)
		{
			type = 1L;
		}
		request_random_name.request request = new request_random_name.request();
		request.type = type;
		NetLogic.GetInstance().Send<Protocol.request_random_name>(request, new RpcRspHandler(this.CharacterGetRandomNameResponse));
	}

	// Token: 0x060048B6 RID: 18614 RVA: 0x0017676C File Offset: 0x0017496C
	private void CharacterGetRandomNameResponse(SprotoTypeBase req)
	{
		request_random_name.response response = req as request_random_name.response;
		if (response != null)
		{
			this.NameInput.value = response.name;
		}
	}

	// Token: 0x060048B7 RID: 18615 RVA: 0x00176798 File Offset: 0x00174998
	public void OnDragModel(GameObject obj, Vector2 delta)
	{
		this.mFakeObjList[this.mCurRoleIndex].FakeObj.transform.Rotate(new Vector3(0f, -delta.x, 0f));
	}

	// Token: 0x060048B8 RID: 18616 RVA: 0x001767D0 File Offset: 0x001749D0
	private void Update()
	{
		if (this.mFakeObjList[this.mCurRoleIndex] != null && this.mFakeObjList[this.mCurRoleIndex].FakeObj != null && this.mFakeObjList[this.mCurRoleIndex].FakeObj.animation.IsPlaying(this.mFakeObjList[this.mCurRoleIndex].GetAnimaStateName(GameDefine.ShowSelectAnimaName)) && this.mFakeObjList[this.mCurRoleIndex].FakeObj.animation[this.mFakeObjList[this.mCurRoleIndex].GetAnimaStateName(GameDefine.ShowSelectAnimaName)].normalizedTime >= 0.8f)
		{
			this.mFakeObjList[this.mCurRoleIndex].CrossFadeIdelSelect(this.mCurRoleIndex, this.mModelData[this.mCurRoleIndex].IndexName);
		}
	}

	// Token: 0x040035F9 RID: 13817
	public UILabel ProfessionLabel;

	// Token: 0x040035FA RID: 13818
	public UIInput NameInput;

	// Token: 0x040035FB RID: 13819
	public MyRotateScrollView RotateScrollView;

	// Token: 0x040035FC RID: 13820
	public UIEventListener RotateModelBtn;

	// Token: 0x040035FD RID: 13821
	public CreateEquilateralPic InsideAttrPic;

	// Token: 0x040035FE RID: 13822
	private Transform mModelRoot;

	// Token: 0x040035FF RID: 13823
	private FakeObjLogic[] mFakeObjList = new FakeObjLogic[3];

	// Token: 0x04003600 RID: 13824
	private int mCurRoleIndex;

	// Token: 0x04003601 RID: 13825
	private GameObject mCurRole;

	// Token: 0x04003602 RID: 13826
	private int NameMinLength = 5;

	// Token: 0x04003603 RID: 13827
	private int NameMaxLength = 16;

	// Token: 0x04003604 RID: 13828
	public UILabel NameTipsLabel;

	// Token: 0x04003605 RID: 13829
	private string[][] mDefaultModel = new string[][]
	{
		new string[]
		{
			"XD_A_WQ",
			"XD_A_T",
			"XD_A_S",
			"XD_A_X"
		},
		new string[]
		{
			"QJ_A_WQ",
			"QJ_A_T",
			"QJ_A_S",
			"QJ_A_X"
		},
		new string[]
		{
			"NQS_A_WQ",
			"NQS_A_T",
			"NQS_A_S",
			"NQS_A_X"
		}
	};

	// Token: 0x04003606 RID: 13830
	private CharacterModelData[] mModelData = new CharacterModelData[3];

	// Token: 0x04003607 RID: 13831
	private string[] mProfessionName = new string[]
	{
		"#{100128}",
		"#{100129}",
		"#{100130}"
	};

	// Token: 0x04003608 RID: 13832
	private float[][] mAttrPicPos = new float[][]
	{
		new float[]
		{
			0.7f,
			0.8f,
			0.8f,
			0.6f,
			0.7f,
			0.7f
		},
		new float[]
		{
			0.7f,
			0.9f,
			0.6f,
			0.9f,
			0.65f,
			0.7f
		},
		new float[]
		{
			0.75f,
			0.6f,
			0.9f,
			0.6f,
			0.75f,
			0.7f
		}
	};

	// Token: 0x04003609 RID: 13833
	private bool NewAccountFlag;

	// Token: 0x0400360A RID: 13834
	private Material mXRayMat;
}
