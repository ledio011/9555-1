using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009ED RID: 2541
public class ChooseRoleRootLogic : SingletonUnity<ChooseRoleRootLogic>
{
	// Token: 0x06004886 RID: 18566 RVA: 0x0017488C File Offset: 0x00172A8C
	private new void Awake()
	{
		base.Awake();
		this.mModelRoot = GameObject.Find("PlayerModelRoot").transform;
		this.BottomRotateBtn.onDrag = new UIEventListener.VectorDelegate(this.OnDragModel);
	}

	// Token: 0x06004887 RID: 18567 RVA: 0x001748CC File Offset: 0x00172ACC
	public void Reset(character_list.response characterList)
	{
		for (int i = 0; i < this.RoleLineTwPos.Length; i++)
		{
			this.RoleLineTwPos[i].ResetToBeginning();
			this.RoleLineTwPos[i].PlayForward();
		}
		this.Acclabel.text = string.Format("Account:{0}", PlayerData.GetPlayerAccountId());
		if (characterList == null)
		{
			this.mCurChooseRoleIndex = 0;
			this.ChooseRole(this.mCurChooseRoleIndex);
			return;
		}
		this.mPlayerInfoDic = characterList.character;
		if (characterList.character.Count > 0)
		{
			this.mCurChooseRoleIndex = LocalDataSaveManager.GetChooseRoleIndex();
			if (this.mCurChooseRoleIndex >= characterList.character.Count)
			{
				this.mCurChooseRoleIndex = 0;
			}
			this.mPlayerRoleList = new List<character_overview>(characterList.character.Values);
			this.mPlayerRoleList.Sort(delegate(character_overview left, character_overview right)
			{
				if (left.createtime < right.createtime)
				{
					return -1;
				}
				if (left.createtime > right.createtime)
				{
					return 1;
				}
				return 0;
			});
			this.RoleCount = this.mPlayerRoleList.Count;
			for (int j = 0; j < this.ChooseRoleLine.Length; j++)
			{
				if (j < this.mPlayerRoleList.Count)
				{
					long profession = this.mPlayerRoleList[j].general.profession;
					if (profession < 0L || profession > 2L)
					{
						goto IL_197;
					}
					switch ((int)profession)
					{
					case 0:
						this.mCharacterModelData[j] = DataManager.GetCharacterModelDataByID("100");
						break;
					case 1:
						this.mCharacterModelData[j] = DataManager.GetCharacterModelDataByID("104");
						break;
					case 2:
						this.mCharacterModelData[j] = DataManager.GetCharacterModelDataByID("105");
						break;
					default:
						goto IL_197;
					}
					IL_1AE:
					this.ResetRoleLine(this.mPlayerRoleList[j], this.ChooseRoleLine[j]);
					if (this.mCurChooseRoleIndex == j)
					{
						this.InitModel(j);
					}
					goto IL_1EF;
					IL_197:
					this.mCharacterModelData[j] = DataManager.GetCharacterModelDataByID("100");
					goto IL_1AE;
				}
				this.ResetRoleLine(null, this.ChooseRoleLine[j]);
				IL_1EF:;
			}
		}
	}

	// Token: 0x06004888 RID: 18568 RVA: 0x00174ADC File Offset: 0x00172CDC
	public void InitModel(int i)
	{
		if (this.mFakeObjList[i] != null && this.mFakeObjList[i].FakeObj != null)
		{
			this.mFakeObjList[i].CheckFakeObject(this.mPlayerRoleList[i].visual, null);
			this.ChooseRole(this.mCurChooseRoleIndex);
			return;
		}
		if (this.mFakeObjList[i] == null)
		{
			this.mFakeObjList[i] = new FakeObjLogic();
		}
		this.mFakeObjList[i].InitFakeObject(this.mPlayerRoleList[i].visual, (int)this.mPlayerRoleList[i].general.profession, this.mModelRoot, new FakeObjLogic.OnLoadFinishedDel(this.OnLoadModelFinish), "ShadowCaster");
	}

	// Token: 0x17000FBD RID: 4029
	// (get) Token: 0x06004889 RID: 18569 RVA: 0x00174BA0 File Offset: 0x00172DA0
	// (set) Token: 0x0600488A RID: 18570 RVA: 0x00174BDC File Offset: 0x00172DDC
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

	// Token: 0x0600488B RID: 18571 RVA: 0x00174BE8 File Offset: 0x00172DE8
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

	// Token: 0x0600488C RID: 18572 RVA: 0x00174D08 File Offset: 0x00172F08
	private void OnLoadModelFinish(FakeObjLogic obj)
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			this.ChooseRole(this.mCurChooseRoleIndex);
		}
	}

	// Token: 0x0600488D RID: 18573 RVA: 0x00174D28 File Offset: 0x00172F28
	private void ChooseRole(int index)
	{
		this.mCurChooseRoleIndex = index;
		this.ResetRole();
		if (this.mPlayerRoleList.Count > index)
		{
			this.ChooseRoleLine[this.mCurChooseRoleIndex].BottomLinePic.spriteName = this.mChoosedLinePicName;
			this.ChooseLineObj.transform.parent = this.ChooseRoleLine[this.mCurChooseRoleIndex].BottomLinePic.transform;
			this.ChooseLineObj.transform.localPosition = new Vector3(-50f, 0f, 0f);
			if (this.mFakeObjList[this.mCurChooseRoleIndex] != null && this.mFakeObjList[index].FakeObj != null)
			{
				this.mWaitTimeCount = 0f;
				UnityVersionUtil.SetActiveRecursive(this.mFakeObjList[this.mCurChooseRoleIndex].FakeObj.gameObject, true);
				this.mFakeObjList[this.mCurChooseRoleIndex].ActivePlayerModelObj();
				this.mFakeObjList[this.mCurChooseRoleIndex].FakeObj.transform.localEulerAngles = Vector3.zero;
				this.mFakeObjList[this.mCurChooseRoleIndex].PlayAnim(GameDefine.ShowSelectAnimaName, this.mCharacterModelData[this.mCurChooseRoleIndex].IndexName);
				this.ResetCurModelShadow(this.mFakeObjList[this.mCurChooseRoleIndex]);
			}
			else
			{
				this.InitModel(this.mCurChooseRoleIndex);
			}
			this.IdLabel.text = string.Format("ID:{0}", this.mPlayerRoleList[this.mCurChooseRoleIndex].id);
			this.LevelLabel.text = string.Format("Lv.{0}", this.mPlayerRoleList[this.mCurChooseRoleIndex].attribute_other.level);
			this.FightingLabel.text = this.mPlayerRoleList[this.mCurChooseRoleIndex].attribute_other.combValue.ToString();
			this.ProfessionLabel.text = this.mPlayerRoleList[this.mCurChooseRoleIndex].general.name;
			return;
		}
		SingletonUnity<MenuSceneController>.Instance.ShowCreateRole(false);
	}

	// Token: 0x0600488E RID: 18574 RVA: 0x00174F58 File Offset: 0x00173158
	private void ResetRole()
	{
		for (int i = 0; i < this.ChooseRoleLine.Length; i++)
		{
			if (this.mFakeObjList[i] != null && this.mFakeObjList[i].FakeObj != null && UnityVersionUtil.IsActive(this.mFakeObjList[i].FakeObj.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.mFakeObjList[i].FakeObj.gameObject, false);
			}
			this.ChooseRoleLine[i].BottomLinePic.spriteName = this.mUnChoosedLinePicName;
		}
	}

	// Token: 0x0600488F RID: 18575 RVA: 0x00174FF0 File Offset: 0x001731F0
	private void ResetRoleLine(character_overview chaInfo, ChooseRoleLine roleLine)
	{
		if (chaInfo != null)
		{
			roleLine.roleId = chaInfo.id;
			roleLine.RolePic.spriteName = GameDefine.Player_Icon_Pic[(int)(checked((IntPtr)chaInfo.general.profession))];
			roleLine.NameLabel.text = chaInfo.general.name;
			roleLine.BottomLinePic.spriteName = this.mUnChoosedLinePicName;
		}
		else
		{
			roleLine.RolePic.spriteName = "CZ_touXiangTianjia";
			roleLine.NameLabel.text = string.Empty;
			roleLine.BottomLinePic.spriteName = this.mUnChoosedLinePicName;
		}
	}

	// Token: 0x06004890 RID: 18576 RVA: 0x0017508C File Offset: 0x0017328C
	public void OnClickRoleBtn0()
	{
		this.ChooseRole(0);
	}

	// Token: 0x06004891 RID: 18577 RVA: 0x00175098 File Offset: 0x00173298
	public void OnClickRoleBtn1()
	{
		this.ChooseRole(1);
	}

	// Token: 0x06004892 RID: 18578 RVA: 0x001750A4 File Offset: 0x001732A4
	public void OnClickRoleBtn2()
	{
		this.ChooseRole(2);
	}

	// Token: 0x06004893 RID: 18579 RVA: 0x001750B0 File Offset: 0x001732B0
	public void OnClickRoleBtn3()
	{
		this.ChooseRole(3);
	}

	// Token: 0x06004894 RID: 18580 RVA: 0x001750BC File Offset: 0x001732BC
	public void OnClickBackBtn()
	{
		for (int i = 0; i < this.mFakeObjList.Length; i++)
		{
			if (this.mFakeObjList[i] != null)
			{
				this.mFakeObjList[i].DestroyFakeObj();
			}
		}
		this.RoleCount = 0;
		SingletonUnity<MenuSceneController>.Instance.ShowChooseServer();
	}

	// Token: 0x06004895 RID: 18581 RVA: 0x00175110 File Offset: 0x00173310
	public void OnClickPlayBtn()
	{
		LocalDataSaveManager.SetChooseRoleIndex(this.mCurChooseRoleIndex);
		PlayerData.MainPlayerServerId = this.ChooseRoleLine[this.mCurChooseRoleIndex].roleId;
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		character_overview character_overview = this.mPlayerInfoDic[PlayerData.MainPlayerServerId];
		if (character_overview.HasForbidden && character_overview.forbidden == 1L)
		{
			MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{201011}", new object[0]), StrDictionary.GetDictionaryString("#{200005}", new object[0]), null);
		}
		PlayerData playerData = instance.PlayerData;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		playerCommonData.CreateTime = this.mPlayerInfoDic[PlayerData.MainPlayerServerId].createtime;
		playerData.Level = (int)this.mPlayerInfoDic[PlayerData.MainPlayerServerId].attribute_other.level;
		playerData.IsTutorialFinish = (this.mPlayerInfoDic[PlayerData.MainPlayerServerId].general.HasTutorial && this.mPlayerInfoDic[PlayerData.MainPlayerServerId].general.tutorial == 1L);
		WaitResponseUIRootLogic.OpenWaitBox(105, 0f, 0f, null);
		character_pick.request request = new character_pick.request();
		request.id = PlayerData.MainPlayerServerId;
		NetLogic.GetInstance().Send<Protocol.character_pick>(request, new RpcRspHandler(SingletonDontDestoryUnity<NetManager>.Instance.PickResponse));
		instance.FirstEnterGame = true;
		instance.IsShowMainMissionTip = true;
		instance.PlayerData.ViewType = LocalDataSaveManager.GetCameraViewType(PlayerData.MainPlayerServerId);
	}

	// Token: 0x06004896 RID: 18582 RVA: 0x00175290 File Offset: 0x00173490
	public void OnDragModel(GameObject obj, Vector2 delta)
	{
		this.mFakeObjList[this.mCurChooseRoleIndex].FakeObj.transform.Rotate(new Vector3(0f, -delta.x, 0f));
	}

	// Token: 0x06004897 RID: 18583 RVA: 0x001752C8 File Offset: 0x001734C8
	private void Update()
	{
		this.mWaitTimeCount += Time.deltaTime;
		if (this.mWaitTimeCount >= this.mShowTimeInterval)
		{
			this.mWaitTimeCount = 0f;
			this.mFakeObjList[this.mCurChooseRoleIndex].CrossFadeAnima(GameDefine.ShowSelectAnimaName, this.mCharacterModelData[this.mCurChooseRoleIndex].IndexName);
		}
		if (this.mFakeObjList[this.mCurChooseRoleIndex] != null && this.mFakeObjList[this.mCurChooseRoleIndex].FakeObj != null && this.mFakeObjList[this.mCurChooseRoleIndex].FakeObj.animation.IsPlaying(this.mFakeObjList[this.mCurChooseRoleIndex].GetAnimaStateName(GameDefine.ShowSelectAnimaName)) && this.mFakeObjList[this.mCurChooseRoleIndex].FakeObj.animation[this.mFakeObjList[this.mCurChooseRoleIndex].GetAnimaStateName(GameDefine.ShowSelectAnimaName)].normalizedTime >= 0.8f)
		{
			this.mFakeObjList[this.mCurChooseRoleIndex].CrossFadeIdelSelect((int)this.mPlayerRoleList[this.mCurChooseRoleIndex].general.profession, this.mCharacterModelData[this.mCurChooseRoleIndex].IndexName);
		}
	}

	// Token: 0x040035D2 RID: 13778
	public ChooseRoleLine[] ChooseRoleLine;

	// Token: 0x040035D3 RID: 13779
	public UILabel LevelLabel;

	// Token: 0x040035D4 RID: 13780
	public UILabel FightingLabel;

	// Token: 0x040035D5 RID: 13781
	public UILabel ProfessionLabel;

	// Token: 0x040035D6 RID: 13782
	public GameObject ChooseLineObj;

	// Token: 0x040035D7 RID: 13783
	public int RoleCount;

	// Token: 0x040035D8 RID: 13784
	private int mCurChooseRoleIndex;

	// Token: 0x040035D9 RID: 13785
	private Transform mModelRoot;

	// Token: 0x040035DA RID: 13786
	private List<character_overview> mPlayerRoleList;

	// Token: 0x040035DB RID: 13787
	private float mWaitTimeCount;

	// Token: 0x040035DC RID: 13788
	private string mChoosedLinePicName = "CZ_renWubg_2";

	// Token: 0x040035DD RID: 13789
	private string mUnChoosedLinePicName = "CZ_renWubg_1";

	// Token: 0x040035DE RID: 13790
	private FakeObjLogic[] mFakeObjList = new FakeObjLogic[4];

	// Token: 0x040035DF RID: 13791
	private CharacterModelData[] mCharacterModelData = new CharacterModelData[4];

	// Token: 0x040035E0 RID: 13792
	private float mShowTimeInterval = 10f;

	// Token: 0x040035E1 RID: 13793
	public TweenPosition[] RoleLineTwPos;

	// Token: 0x040035E2 RID: 13794
	public UIEventListener BottomRotateBtn;

	// Token: 0x040035E3 RID: 13795
	private Dictionary<long, character_overview> mPlayerInfoDic;

	// Token: 0x040035E4 RID: 13796
	public UILabel IdLabel;

	// Token: 0x040035E5 RID: 13797
	public UILabel Acclabel;

	// Token: 0x040035E6 RID: 13798
	private Material mXRayMat;

	// Token: 0x040035E7 RID: 13799
	private int test;
}
