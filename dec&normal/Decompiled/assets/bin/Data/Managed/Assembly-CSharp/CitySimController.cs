using System;
using System.Collections.Generic;
using DG.Tweening;
using SprotoType;
using UnityEngine;

// Token: 0x020001C9 RID: 457
public class CitySimController : SingletonUnity<CitySimController>
{
	// Token: 0x06001075 RID: 4213 RVA: 0x000676BC File Offset: 0x000658BC
	public CitySimController()
	{
		List<string> list = new List<string>();
		list.Add("9906");
		list.Add("9907");
		list.Add("9908");
		this.NpcIdList = list;
		list = new List<string>();
		list.Add("9909");
		this.PoliceIdList = list;
		this.flashTimeCount = 5f;
		this.mEnableObjZombiePlayerList = new List<ObjZombieRagdollPlayer>();
		this.mFlashNpcSqrDis = 25f;
		this.preCreateNpcPos = Vector3.zero;
		this.mNpcRecycleDis = 50f;
		this.mCarRecycleDis = 60f;
		this.mEnablePoliceDic = new Dictionary<long, ObjRagdollNPC>();
		this.mEnablePoliceList = new List<ObjNPC>();
		this.mPreCheckBlockList = new List<BlockData>();
		this.mStaticCarDic = new Dictionary<long, BlockCarData>();
		this.mSpecialCarId = string.Empty;
		this.mSpecialCarServerId = -1L;
		list = new List<string>();
		list.Add("NPC_Nv_013");
		list.Add("NPC_Nv_014");
		list.Add("NPC_Nan_005");
		list.Add("NPC_Nan_013");
		list.Add("NPC_Nan_017");
		list.Add("NPC_Nan_022");
		list.Add("NPC_Nan_047");
		list.Add("NPC_Nan_036");
		list.Add("NPC_Nv_002");
		list.Add("NPC_Nv_004");
		list.Add("NPC_Nv_005");
		list.Add("XD_A_S");
		list.Add("XD_A_WQ");
		list.Add("XD_A_T");
		list.Add("XD_A_X");
		this.mBeforDownloadNpcModelIdList = list;
		this.mPoliceCreateList = new List<string>();
		this.mPreNpcLocationData = new List<LocationData>();
		this.FakeAICarName = new string[]
		{
			"Chevrolet",
			"chuZuChe",
			"daKeChe",
			"jiaoChe_01",
			"jiaoChe_02",
			"jingChe",
			"xiaoKeChe",
			"jiaChangChe_01",
			"jiaoChe_04",
			"jiaoChe_05",
			"jingChe_02",
			"jiPuChe_02",
			"jiPuChe_03",
			"kaChe_03",
			"kaChe_04",
			"qingJieChe_01",
			"xiaoFangChe_01"
		};
		this.BeforeDownloadCarId = new string[]
		{
			"Chevrolet",
			"jingChe",
			"jiaoChe_04",
			"jiaoChe_05",
			"jingChe_02",
			"qingJieChe_01",
			"xiaoFangChe_01"
		};
		list = new List<string>();
		list.Add("Chevrolet");
		list.Add("jingChe");
		list.Add("jiaoChe_04");
		list.Add("jiaoChe_05");
		list.Add("jingChe_02");
		list.Add("qingJieChe_01");
		list.Add("xiaoFangChe_01");
		this.BeforeDownloadStaticCar = list;
		this.carDis1 = 2.3f;
		this.carDis2 = 5.5f;
		this.FourWayCarDis1 = 2.8f;
		this.FourWayCarDis2 = 6.7f;
		this.mPreCarLocationData = new List<LocationData>();
		this.mRoadStateChangeTime = 15f;
		this.getOnCarHandle = new vp_Timer.Handle();
		this.getOnCarAnimaHandle = new vp_Timer.Handle();
		this.mCurMonsterDataList = new List<MonsterData>();
		this.mStaticNpcServerIdDic = new Dictionary<long, BlockMonsterData>();
		this.mPoliceFlashCount = 15f;
		this.mCurPathList = new List<Vector3>();
		base..ctor();
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x00067A84 File Offset: 0x00065C84
	public void InitSaidao()
	{
		this.CityLeftBottomPos = new Vector2(-350f, -350f);
		this.CitySize = new Vector2(700f, 700f);
		this.BlockLength = 50;
		this.NpcCreateDis = 40f;
		this.CarCreateDis = 50f;
		this.mNpcRecycleDis = 50f;
		this.mCarRecycleDis = 60f;
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x00067AF0 File Offset: 0x00065CF0
	public void InitGta()
	{
		this.CityLeftBottomPos = new Vector2(-800f, -800f);
		this.CitySize = new Vector2(1600f, 1600f);
		this.BlockLength = 100;
		this.NpcCreateDis = 40f;
		this.CarCreateDis = 50f;
		this.mNpcRecycleDis = 50f;
		this.mCarRecycleDis = 120f;
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x00067B5C File Offset: 0x00065D5C
	private int GetPoliceNum()
	{
		int num = 0;
		for (int i = 0; i < this.mEnableRagdollList.Count; i++)
		{
			if (this.PoliceIdList.Contains(this.mEnableRagdollList[i].NPCDataID))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x00067BB0 File Offset: 0x00065DB0
	public BlockData GetBlockData(int x, int z)
	{
		if (x < 0 || x >= this.BlockData.Length || z < 0 || z >= this.BlockData[x].PointLine.Length)
		{
			return null;
		}
		return this.BlockData[x].PointLine[z];
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x00067C00 File Offset: 0x00065E00
	private new void Awake()
	{
		base.Awake();
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager.CurrentMapInofData.MapType == MAPTYPE.TUTORIAL_CAR)
		{
			NewTutorialSceneManager newTutorialSceneManager = sceneManager as NewTutorialSceneManager;
			this.mSmoothFollowCamPos = newTutorialSceneManager.SmoothFollowCamPos;
		}
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x00067C44 File Offset: 0x00065E44
	private void Start()
	{
		this.InitMonsterList();
		this.mPoliceLevelData = DataManager.GetPoliceLevelDataById("0");
		this.UpdateSpecialCarState();
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x00067C64 File Offset: 0x00065E64
	public void UpdateCityCheck()
	{
		if (this.mObjManager == null)
		{
			this.mObjManager = Singleton<ObjManager>.Instance;
		}
		if (this.mMainPlayerTrans == null)
		{
			if (!(this.mObjManager.MainPlayer != null))
			{
				return;
			}
			this.mMainPlayer = this.mObjManager.MainPlayer;
			this.mMainPlayerTrans = this.mObjManager.MainPlayer.CacheTransform;
			if (this.mMainPlayerTrans == null)
			{
				return;
			}
			this.mEnableRagdollList = Singleton<ObjManager>.Instance.EnableRagdollNpcList;
			this.mEnableFakeAICarList = Singleton<ObjManager>.Instance.EnableFakeAICarList;
		}
		this.flashTimeCount += Time.deltaTime;
		if (this.flashTimeCount > 5f)
		{
			this.flashTimeCount = 0f;
			this.CheckRecycleNpc();
			if (this.mEnableRagdollList.Count < this.NpcNumCount)
			{
				this.CreateNewNpc(this.mMainPlayerTrans, this.NpcCreateDis, false);
				this.preCreateNpcPos = this.mMainPlayerTrans.position;
			}
			this.NormalNpcCreateCheck(this.mMainPlayerTrans.position);
			this.CheckRecycleAICar();
			if (this.mEnableFakeAICarList.Count < this.CarNumCount)
			{
				this.CreateNewCar(this.mMainPlayerTrans, this.CarCreateDis);
				this.preCreateNpcPos = this.mMainPlayerTrans.position;
			}
		}
		this.posTimeCount += Time.deltaTime;
		if (this.posTimeCount > 3f)
		{
			bool flag = (this.mMainPlayerTrans.position - this.preCreateNpcPos).sqrMagnitude > this.mFlashNpcSqrDis;
			if (flag)
			{
				this.CheckRecycleNpc();
				if (this.mEnableRagdollList.Count < this.NpcNumCount)
				{
					this.CreateNewNpc(this.mMainPlayerTrans, this.NpcCreateDis, false);
					this.preCreateNpcPos = this.mMainPlayerTrans.position;
				}
				this.flashTimeCount = 0f;
				this.NormalNpcCreateCheck(this.mMainPlayerTrans.position);
			}
			if (flag)
			{
				this.CheckRecycleAICar();
				if (this.mEnableFakeAICarList.Count < this.CarNumCount)
				{
					this.CreateNewCar(this.mMainPlayerTrans, this.CarCreateDis);
					this.preCreateNpcPos = this.mMainPlayerTrans.position;
					this.flashTimeCount = 0f;
				}
			}
		}
		this.LightingSystemUpdate();
		this.CheckObjStop();
		if (this.mMainPlayer.IsDie)
		{
			if (SingletonUnity<RobCarBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<RobCarBtnRootLogic>.Instance.gameObject))
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RobCarBtnRoot);
			}
			this.PoliceScores = 0;
			this.mPoliceCreateList.Clear();
		}
		else
		{
			if (this.mPlayerCar != null)
			{
				if (this.mPlayerCar.CurSpeed > 5f)
				{
					if (SingletonUnity<RobCarBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<RobCarBtnRootLogic>.Instance.gameObject))
					{
						SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RobCarBtnRoot);
					}
				}
				else if (!SingletonUnity<RobCarBtnRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<RobCarBtnRootLogic>.Instance.gameObject))
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.RobCarBtnRoot, null, null);
				}
			}
			else if (this.mCurNearestCar == null)
			{
				if (SingletonUnity<RobCarBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<RobCarBtnRootLogic>.Instance.gameObject))
				{
					SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RobCarBtnRoot);
				}
			}
			else if (this.mCurNearestCar.PlayerCar.IsDie)
			{
				this.mCurNearestCar = null;
				if (SingletonUnity<RobCarBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<RobCarBtnRootLogic>.Instance.gameObject))
				{
					SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RobCarBtnRoot);
				}
			}
			else if (this.CheckShowDoor(this.mCurNearestCar.transform.InverseTransformPoint(this.mMainPlayerTrans.position)))
			{
				if (!SingletonUnity<RobCarBtnRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<RobCarBtnRootLogic>.Instance.gameObject))
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.RobCarBtnRoot, delegate
					{
						SingletonUnity<RobCarBtnRootLogic>.Instance.Reset(this.mCurNearestCar);
					}, null);
				}
			}
			else if (SingletonUnity<RobCarBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<RobCarBtnRootLogic>.Instance.gameObject))
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RobCarBtnRoot);
			}
			this.PoliceUpdateCheck();
		}
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x000680DC File Offset: 0x000662DC
	public void OnlyUpdateNpc()
	{
		if (this.mObjManager == null)
		{
			this.mObjManager = Singleton<ObjManager>.Instance;
		}
		if (this.mMainPlayerTrans == null)
		{
			if (!(this.mObjManager.MainPlayer != null))
			{
				return;
			}
			this.mMainPlayer = this.mObjManager.MainPlayer;
			this.mMainPlayerTrans = this.mObjManager.MainPlayer.CacheTransform;
			if (this.mMainPlayerTrans == null)
			{
				return;
			}
			this.mEnableRagdollList = Singleton<ObjManager>.Instance.EnableRagdollNpcList;
			this.mEnableFakeAICarList = Singleton<ObjManager>.Instance.EnableFakeAICarList;
		}
		this.flashTimeCount += Time.deltaTime;
		if (this.flashTimeCount > 5f)
		{
			this.flashTimeCount = 0f;
			this.CheckRecycleNpc();
			if (this.mEnableRagdollList.Count < this.NpcNumCount)
			{
				this.CreateNewNpc(this.mMainPlayerTrans, this.NpcCreateDis, false);
				this.preCreateNpcPos = this.mMainPlayerTrans.position;
			}
		}
		this.posTimeCount += Time.deltaTime;
		if (this.posTimeCount > 3f)
		{
			bool flag = (this.mMainPlayerTrans.position - this.preCreateNpcPos).sqrMagnitude > this.mFlashNpcSqrDis;
			if (flag)
			{
				this.CheckRecycleNpc();
				if (this.mEnableRagdollList.Count < this.NpcNumCount)
				{
					this.CreateNewNpc(this.mMainPlayerTrans, this.NpcCreateDis, false);
					this.preCreateNpcPos = this.mMainPlayerTrans.position;
					this.flashTimeCount = 0f;
				}
			}
		}
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x0006828C File Offset: 0x0006648C
	public void CheckRecycleNpc()
	{
		for (int i = this.mEnableRagdollList.Count - 1; i >= 0; i--)
		{
			if (this.mEnableRagdollList[i] == null)
			{
				this.mEnableRagdollList.RemoveAt(i);
			}
			else if (Vector3.Distance(this.mMainPlayerTrans.position, this.mEnableRagdollList[i].Position) > this.mNpcRecycleDis)
			{
				if (this.mEnablePoliceDic.ContainsKey(this.mEnableRagdollList[i].ServerId))
				{
					this.mEnablePoliceDic.Remove(this.mEnableRagdollList[i].ServerId);
					this.mEnablePoliceList.Remove(this.mEnableRagdollList[i]);
				}
				this.mEnableRagdollList[i].RecycleSelf();
			}
		}
		for (int j = this.mEnableObjZombiePlayerList.Count - 1; j >= 0; j--)
		{
			if (this.mEnableObjZombiePlayerList[j] == null)
			{
				this.mEnableObjZombiePlayerList.RemoveAt(j);
			}
			else if (Vector3.Distance(this.mMainPlayerTrans.position, this.mEnableObjZombiePlayerList[j].Position) > this.mNpcRecycleDis)
			{
				this.mEnableObjZombiePlayerList[j].RecycleSelf();
			}
		}
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x000683F8 File Offset: 0x000665F8
	public void CheckRecycleAICar()
	{
		for (int i = this.mEnableFakeAICarList.Count - 1; i >= 0; i--)
		{
			if (Vector3.Distance(this.mMainPlayerTrans.position, this.mEnableFakeAICarList[i].transform.position) > this.mCarRecycleDis)
			{
				this.mEnableFakeAICarList[i].RecycleSelf();
			}
		}
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x00068468 File Offset: 0x00066668
	public void CreateNewNpc(Transform playerTrans, float createDis, bool isPolice)
	{
		ObjManager instance = Singleton<ObjManager>.Instance;
		List<LocationData> list = new List<LocationData>();
		if (!this.GetCreateLocation(playerTrans.position, createDis, list))
		{
			if (isPolice)
			{
				for (int i = 0; i < this.mPoliceCreateList.Count; i++)
				{
					this.CreateNpcNearBy(this.mPoliceCreateList[i]);
				}
				this.mPoliceCreateList.Clear();
				return;
			}
			list = this.mPreNpcLocationData;
		}
		this.mPreNpcLocationData = list;
		int level = this.mMainPlayer.AttributeData.Level;
		for (int j = 0; j < list.Count; j++)
		{
			CityPathPointData point = list[j].point1;
			CityPathPointData point2 = list[j].point2;
			if (point.IsWalkable)
			{
				int num = (Random.Range(0, 2) != 0) ? 1 : -1;
				int num2 = (point.LinkPointIndex[0] != point2.SelfIndex) ? -1 : 1;
				Vector3 vector = point.PointPos + (float)num * point.PointRight * Random.Range(point.MinWalkDis, point.MaxWalkDis) + (float)num2 * point.PointForward * list[j].DisFromPoint1;
				if (SceneManager.IsInNavmeshArea(vector))
				{
					if (isPolice)
					{
						if (Vector3.Distance(playerTrans.position, vector) > this.mNpcRecycleDis)
						{
							goto IL_507;
						}
					}
					else if (!this.CheckNpcPosEmpty(vector) || Vector3.Distance(playerTrans.position, vector) > this.mNpcRecycleDis)
					{
						goto IL_507;
					}
					if (this.mEnableRagdollList.Count <= this.NpcNumCount)
					{
						NpcData npcDataByID;
						if (this.mPoliceCreateList.Count > 0)
						{
							npcDataByID = DataManager.GetNpcDataByID(this.mPoliceCreateList[0]);
							this.mPoliceCreateList.RemoveAt(0);
						}
						else
						{
							if (isPolice)
							{
								goto IL_507;
							}
							string id = this.NpcIdList[Random.Range(0, this.NpcIdList.Count)];
							npcDataByID = DataManager.GetNpcDataByID(id);
						}
						CityPathPointData targetPoint = null;
						if (point.IsWalkable || point2.IsWalkable)
						{
							if (!point.IsWalkable)
							{
								targetPoint = point2;
							}
							else if (!point2.IsWalkable)
							{
								targetPoint = point;
							}
							else
							{
								targetPoint = ((Random.Range(0, 2) != 0) ? point2 : point);
							}
							int num3;
							if (CitySimController.IsForward(vector - targetPoint.PointPos, targetPoint.PointRight))
							{
								num3 = 1;
							}
							else
							{
								num3 = -1;
							}
							Vector3 vector2 = targetPoint.PointPos + (float)num3 * targetPoint.PointRight * Random.Range(targetPoint.MinWalkDis, targetPoint.MaxWalkDis);
							ObjInitNpcData objInitNpcData = new ObjInitNpcData();
							objInitNpcData.mServerID = UUID.GenUUID();
							objInitNpcData.mPos = vector;
							objInitNpcData.mDir = (vector2 - objInitNpcData.mPos).normalized;
							objInitNpcData.npcInfoData = npcDataByID;
							objInitNpcData.mCharacterModelId = npcDataByID.Model;
							objInitNpcData.MaxHP = objInitNpcData.npcInfoData.Hp;
							objInitNpcData.HP = objInitNpcData.MaxHP;
							objInitNpcData.ATK = objInitNpcData.npcInfoData.Atk;
							objInitNpcData.DEF = objInitNpcData.npcInfoData.Def;
							objInitNpcData.HIT = objInitNpcData.npcInfoData.HIT;
							objInitNpcData.EVA = objInitNpcData.npcInfoData.DGE;
							objInitNpcData.CRI = objInitNpcData.npcInfoData.CRI;
							objInitNpcData.EXD = objInitNpcData.npcInfoData.EXD;
							objInitNpcData.EXR = objInitNpcData.npcInfoData.EXR;
							objInitNpcData.RES = objInitNpcData.npcInfoData.RES;
							objInitNpcData.CRD = objInitNpcData.npcInfoData.CRD;
							objInitNpcData.CRR = objInitNpcData.npcInfoData.CRR;
							objInitNpcData.DEFA = objInitNpcData.npcInfoData.DEFA;
							objInitNpcData.DGEA = objInitNpcData.npcInfoData.DGEA;
							objInitNpcData.HITA = objInitNpcData.npcInfoData.HITA;
							objInitNpcData.RESA = objInitNpcData.npcInfoData.RESA;
							objInitNpcData.CRIA = objInitNpcData.npcInfoData.CRIA;
							objInitNpcData.Level = level;
							objInitNpcData.AntiStun = objInitNpcData.npcInfoData.AntiStun;
							objInitNpcData.AntiKnockDown = objInitNpcData.npcInfoData.AntiKnockDown;
							instance.GetRagdollNPC(objInitNpcData, delegate(ObjNPC npc)
							{
								ObjRagdollNPC objRagdollNPC = npc as ObjRagdollNPC;
								if (objRagdollNPC != null)
								{
									objRagdollNPC.ResetCityMove(this, targetPoint);
									if (this.PoliceIdList.Contains(objRagdollNPC.NPCDataID))
									{
										this.mEnablePoliceDic.Add(objRagdollNPC.ServerId, objRagdollNPC);
										this.mEnablePoliceList.Add(objRagdollNPC);
									}
								}
							});
						}
					}
				}
			}
			IL_507:;
		}
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x00068990 File Offset: 0x00066B90
	private ObjZombieRagdollPlayer CreateZombieRagdollPlayer(BlockMonsterData blockMonsterData, character_look chaLook = null)
	{
		ObjInitPlayerData objInitPlayerData = new ObjInitPlayerData();
		objInitPlayerData.mPos = blockMonsterData.Data.GetNpcPos();
		objInitPlayerData.mDir = MathUtil.HeadingToVector3(blockMonsterData.Data.PositionO);
		objInitPlayerData.mServerID = UUID.GenUUID();
		objInitPlayerData.EXP = 0L;
		objInitPlayerData.TitleLevel = 0;
		objInitPlayerData.TitleExp = 0;
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		AdaptData adaptDataByID = DataManager.GetAdaptDataByID(level);
		NpcData npcDataByID = DataManager.GetNpcDataByID(blockMonsterData.Data.NpcID);
		objInitPlayerData.AIID = npcDataByID.AIID;
		objInitPlayerData.NpcId = npcDataByID.ID;
		objInitPlayerData.Level = level;
		objInitPlayerData.Attribute = new attribute();
		objInitPlayerData.Attribute.max_hp = (long)(npcDataByID.HpCoe * adaptDataByID.HpStd / 10000);
		objInitPlayerData.HP = (int)objInitPlayerData.Attribute.max_hp;
		objInitPlayerData.Attribute.atk = (long)(npcDataByID.AtkCoe * adaptDataByID.AtkStd / 10000);
		objInitPlayerData.Attribute.def = (long)(npcDataByID.DefCoe * adaptDataByID.DefStd / 10000);
		objInitPlayerData.Attribute.hit = (long)(npcDataByID.HITCoe * adaptDataByID.HITStd / 10000);
		objInitPlayerData.Attribute.eva = (long)(npcDataByID.DefCoe * adaptDataByID.DefStd / 10000);
		objInitPlayerData.Attribute.cri = (long)(npcDataByID.CRICoe * adaptDataByID.CRIStd / 10000);
		objInitPlayerData.Attribute.exd = (long)(npcDataByID.EXDCoe * adaptDataByID.EXDStd / 10000);
		objInitPlayerData.Attribute.exr = (long)(npcDataByID.EXRCoe * adaptDataByID.EXRStd / 10000);
		objInitPlayerData.Attribute.res = (long)(npcDataByID.RESCoe * adaptDataByID.RESStd / 10000);
		objInitPlayerData.Attribute.crd = (long)(npcDataByID.CRDCoe * adaptDataByID.CRDStd / 10000);
		objInitPlayerData.Attribute.crr = (long)(npcDataByID.CRRCoe * adaptDataByID.CRRStd / 10000);
		objInitPlayerData.Attribute.defa = (long)(npcDataByID.DEFACoe * adaptDataByID.DEFAStd / 10000);
		objInitPlayerData.Attribute.dgea = (long)(npcDataByID.DGEACoe * adaptDataByID.DGEAStd / 10000);
		objInitPlayerData.Attribute.hita = (long)(npcDataByID.HITACoe * adaptDataByID.HITAStd / 10000);
		objInitPlayerData.Attribute.resa = (long)(npcDataByID.RESACoe * adaptDataByID.RESAStd / 10000);
		objInitPlayerData.Attribute.cria = (long)(npcDataByID.CRIACoe * adaptDataByID.CRIAStd / 10000);
		objInitPlayerData.AttributeAll = objInitPlayerData.Attribute;
		objInitPlayerData.Speed = (float)npcDataByID.MoveSpeed / 10f;
		objInitPlayerData.ComboValue = 0;
		objInitPlayerData.skills = new Dictionary<string, skill_info>();
		if (!string.IsNullOrEmpty(npcDataByID.SkillGroup))
		{
			string[] array = npcDataByID.SkillGroup.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array.Length; i++)
			{
				skill_info skill_info = new skill_info();
				skill_info.skillId = array[i];
				SkillData skillDataById = DataManager.GetSkillDataById(skill_info.skillId);
				if (skillDataById.IsUpgrade == 1)
				{
					skill_info.skillLevel = (long)level;
				}
				else
				{
					skill_info.skillLevel = 0L;
				}
				skill_info.indexPos = (long)i;
				objInitPlayerData.skills.Add(skill_info.skillId, skill_info);
			}
		}
		if (chaLook == null)
		{
			objInitPlayerData.visual = new characterVisual();
			string[] array2 = npcDataByID.Model.Split(new char[]
			{
				';'
			});
			if (array2.Length != 4)
			{
				Debug.Log("Wrong ModelId !!!!!!!!!!!!!!");
				return null;
			}
			objInitPlayerData.visual.BodyId = array2[2];
			objInitPlayerData.visual.HeadId = array2[1];
			objInitPlayerData.visual.LegId = array2[3];
			objInitPlayerData.visual.WeaponId = array2[0];
			objInitPlayerData.visual.showType = 0L;
			objInitPlayerData.Name = npcDataByID.MName;
		}
		else
		{
			objInitPlayerData.visual = chaLook.visual;
			objInitPlayerData.Name = chaLook.visual.name;
			objInitPlayerData.TitleLevel = (int)chaLook.attribute_other.title_level;
		}
		if (objInitPlayerData.HeadId.Contains("XD"))
		{
			objInitPlayerData.mCharacterModelId = "100";
			if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
			{
				objInitPlayerData.visual.BodyId = GameDefine.XD_NormalModel[2];
				objInitPlayerData.visual.HeadId = GameDefine.XD_NormalModel[1];
				objInitPlayerData.visual.LegId = GameDefine.XD_NormalModel[3];
				objInitPlayerData.visual.WeaponId = GameDefine.XD_NormalModel[0];
				objInitPlayerData.visual.showType = 0L;
				objInitPlayerData.visual.WeaponItemId = null;
				objInitPlayerData.visual.FashionItemId = null;
			}
		}
		else if (objInitPlayerData.HeadId.Contains("QJ"))
		{
			objInitPlayerData.mCharacterModelId = "104";
			if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
			{
				objInitPlayerData.visual.BodyId = GameDefine.QJ_NormalModel[2];
				objInitPlayerData.visual.HeadId = GameDefine.QJ_NormalModel[1];
				objInitPlayerData.visual.LegId = GameDefine.QJ_NormalModel[3];
				objInitPlayerData.visual.WeaponId = GameDefine.QJ_NormalModel[0];
				objInitPlayerData.visual.showType = 0L;
				objInitPlayerData.visual.WeaponItemId = null;
				objInitPlayerData.visual.FashionItemId = null;
			}
		}
		else
		{
			objInitPlayerData.mCharacterModelId = "105";
			if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
			{
				objInitPlayerData.visual.BodyId = GameDefine.NQS_NormalModel[2];
				objInitPlayerData.visual.HeadId = GameDefine.NQS_NormalModel[1];
				objInitPlayerData.visual.LegId = GameDefine.NQS_NormalModel[3];
				objInitPlayerData.visual.WeaponId = GameDefine.NQS_NormalModel[0];
				objInitPlayerData.visual.showType = 0L;
				objInitPlayerData.visual.WeaponItemId = null;
				objInitPlayerData.visual.FashionItemId = null;
			}
		}
		objInitPlayerData.Camp = GameDefine.CAMP_TYPE.NORMAL_NPC;
		objInitPlayerData.PkMode = 1;
		ObjZombieRagdollPlayer objZombieRagdollPlayer = Singleton<ObjManager>.Instance.CreateZombieRagdollPlayer(objInitPlayerData);
		if (!objZombieRagdollPlayer.IsMissionNpc)
		{
			objZombieRagdollPlayer.PatrolMove(objZombieRagdollPlayer);
		}
		return objZombieRagdollPlayer;
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x0006900C File Offset: 0x0006720C
	public bool CreateNpcNearBy(string npcId)
	{
		Vector3 position = this.mMainPlayer.Position;
		Vector3 vector;
		vector..ctor(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
		Vector3 vector2 = position + vector.normalized * this.NpcCreateDis / 2f;
		if (!SceneManager.IsInNavmeshArea(vector2))
		{
			return false;
		}
		NpcData npcDataByID = DataManager.GetNpcDataByID(npcId);
		ObjInitNpcData objInitNpcData = new ObjInitNpcData();
		objInitNpcData.mServerID = UUID.GenUUID();
		objInitNpcData.mPos = vector2;
		objInitNpcData.mDir = (position - objInitNpcData.mPos).normalized;
		objInitNpcData.npcInfoData = npcDataByID;
		objInitNpcData.mCharacterModelId = npcDataByID.Model;
		objInitNpcData.MaxHP = objInitNpcData.npcInfoData.Hp;
		objInitNpcData.HP = objInitNpcData.npcInfoData.Hp;
		objInitNpcData.ATK = objInitNpcData.npcInfoData.Atk;
		objInitNpcData.DEF = objInitNpcData.npcInfoData.Def;
		objInitNpcData.HIT = objInitNpcData.npcInfoData.HIT;
		objInitNpcData.EVA = objInitNpcData.npcInfoData.DGE;
		objInitNpcData.CRI = objInitNpcData.npcInfoData.CRI;
		objInitNpcData.EXD = objInitNpcData.npcInfoData.EXD;
		objInitNpcData.EXR = objInitNpcData.npcInfoData.EXR;
		objInitNpcData.RES = objInitNpcData.npcInfoData.RES;
		objInitNpcData.CRD = objInitNpcData.npcInfoData.CRD;
		objInitNpcData.CRR = objInitNpcData.npcInfoData.CRR;
		objInitNpcData.DEFA = objInitNpcData.npcInfoData.DEFA;
		objInitNpcData.DGEA = objInitNpcData.npcInfoData.DGEA;
		objInitNpcData.HITA = objInitNpcData.npcInfoData.HITA;
		objInitNpcData.RESA = objInitNpcData.npcInfoData.RESA;
		objInitNpcData.CRIA = objInitNpcData.npcInfoData.CRIA;
		objInitNpcData.Level = objInitNpcData.npcInfoData.Lv;
		objInitNpcData.AntiStun = objInitNpcData.npcInfoData.AntiStun;
		objInitNpcData.AntiKnockDown = objInitNpcData.npcInfoData.AntiKnockDown;
		Singleton<ObjManager>.Instance.GetRagdollNPC(objInitNpcData, null);
		return true;
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x00069260 File Offset: 0x00067460
	public void CreateNewCar(Transform playerTrans, float createDis)
	{
		ObjManager instance = Singleton<ObjManager>.Instance;
		List<LocationData> list = new List<LocationData>();
		if (!this.GetCreateLocation(playerTrans.position, createDis, list))
		{
			list = this.mPreCarLocationData;
		}
		this.mPreCarLocationData = list;
		for (int i = 0; i < list.Count; i++)
		{
			CityPathPointData point = list[i].point1;
			CityPathPointData point2 = list[i].point2;
			int num = (Random.Range(0, 2) != 0) ? 1 : -1;
			int num2 = (point.LinkPointIndex[0] != point2.SelfIndex) ? -1 : 1;
			if (num2 == -1)
			{
				num = -num;
			}
			float num3 = (Random.Range(0, 2) != 0) ? this.carDis2 : this.carDis1;
			float num4;
			if (point.IsFourLines)
			{
				num4 = ((Random.Range(0, 2) != 0) ? this.FourWayCarDis2 : this.FourWayCarDis1);
			}
			else
			{
				num4 = this.carDis1;
			}
			Vector3 vector = point.PointPos + (float)num * point.PointRight * num4 + (float)num2 * point.PointForward * list[i].DisFromPoint1;
			if (this.CheckCarPosEmpty(vector) && Vector3.Distance(playerTrans.position, vector) <= this.mCarRecycleDis && Vector3.Distance(playerTrans.position, vector) >= 5f)
			{
				if (Mathf.Abs(SceneManager.GetHitHeight(vector) - vector.y) <= 2f)
				{
					if (this.mEnableFakeAICarList.Count <= this.CarNumCount)
					{
						CityPathPointData cityPathPointData;
						CityPathPointData cityPathPointData2;
						if (point.LinkPointIndex[0] == point2.SelfIndex)
						{
							if (num == -1)
							{
								cityPathPointData = point;
								cityPathPointData2 = point2;
							}
							else
							{
								cityPathPointData = point2;
								cityPathPointData2 = point;
							}
						}
						else if (num == 1)
						{
							cityPathPointData = point;
							cityPathPointData2 = point2;
						}
						else
						{
							cityPathPointData = point2;
							cityPathPointData2 = point;
						}
						string text = string.Empty;
						if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
						{
							int num5 = Random.Range(0, this.FakeAICarName.Length);
							text = this.FakeAICarName[num5];
						}
						else
						{
							int num6 = Random.Range(0, this.BeforeDownloadCarId.Length);
							text = this.BeforeDownloadCarId[num6];
						}
						ObjCarInitData initData = new ObjCarInitData(vector, Quaternion.LookRotation(cityPathPointData.PointPos - cityPathPointData2.PointPos, Vector3.up).eulerAngles, UUID.GenUUID(), text, false, DataManager.GetMountDataById(text));
						if (this.mObjManager == null)
						{
							this.mObjManager = Singleton<ObjManager>.Instance;
						}
						ObjFakeAICar fakeAICar = this.mObjManager.GetFakeAICar(initData, false);
						if (fakeAICar != null)
						{
							fakeAICar.Reset(cityPathPointData, cityPathPointData2, num4, this);
						}
					}
				}
			}
		}
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x00069560 File Offset: 0x00067760
	private bool CheckNpcPosEmpty(Vector3 pos)
	{
		for (int i = this.mEnableRagdollList.Count - 1; i >= 0; i--)
		{
			if (this.mEnableRagdollList[i] == null)
			{
				this.mEnableRagdollList.RemoveAt(i);
			}
			else if (Vector3.SqrMagnitude(this.mEnableRagdollList[i].Position - pos) < 25f)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x000695DC File Offset: 0x000677DC
	private bool CheckCarPosEmpty(Vector3 pos)
	{
		for (int i = this.mEnableRagdollList.Count - 1; i >= 0; i--)
		{
			if (this.mEnableRagdollList[i] == null)
			{
				this.mEnableRagdollList.RemoveAt(i);
			}
			else if (Vector3.SqrMagnitude(this.mEnableRagdollList[i].Position - pos) < 16f)
			{
				return false;
			}
		}
		for (int j = 0; j < this.mEnableFakeAICarList.Count; j++)
		{
			if (Vector3.SqrMagnitude(this.mEnableFakeAICarList[j].transform.position - pos) < 100f)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x000696A4 File Offset: 0x000678A4
	public void GetNextPoint(ObjCharacter obj)
	{
		ObjRagdollNPC objRagdollNPC = obj as ObjRagdollNPC;
		if (objRagdollNPC != null)
		{
			CityPathPointData targetPathPoint = objRagdollNPC.TargetPathPoint;
			int num = -1;
			int num2;
			if (targetPathPoint.IsCross)
			{
				num2 = Random.Range(0, 4);
			}
			else
			{
				num2 = Random.Range(0, 2);
			}
			if (num2 == 0)
			{
				if (targetPathPoint.LinkPointIndex[0] != -1)
				{
					num = targetPathPoint.LinkPointIndex[0];
				}
				else
				{
					num = targetPathPoint.LinkPointIndex[1];
				}
				if (!this.PointDataList[num].IsWalkable)
				{
					if (targetPathPoint.LinkPointIndex[2] != -1)
					{
						num = targetPathPoint.LinkPointIndex[2];
					}
					else
					{
						num = targetPathPoint.LinkPointIndex[3];
						if (num == -1)
						{
							num = targetPathPoint.LinkPointIndex[0];
						}
					}
				}
			}
			else if (num2 == 1)
			{
				if (targetPathPoint.LinkPointIndex[1] != -1)
				{
					num = targetPathPoint.LinkPointIndex[1];
				}
				else
				{
					num = targetPathPoint.LinkPointIndex[0];
				}
				if (!this.PointDataList[num].IsWalkable)
				{
					if (targetPathPoint.LinkPointIndex[2] != -1)
					{
						num = targetPathPoint.LinkPointIndex[2];
					}
					else
					{
						num = targetPathPoint.LinkPointIndex[3];
						if (num == -1)
						{
							num = targetPathPoint.LinkPointIndex[1];
						}
					}
				}
			}
			else if (num2 == 2)
			{
				if (targetPathPoint.LinkPointIndex[2] != -1)
				{
					num = targetPathPoint.LinkPointIndex[2];
				}
				else
				{
					num = targetPathPoint.LinkPointIndex[3];
				}
				if (!this.PointDataList[num].IsWalkable)
				{
					if (targetPathPoint.LinkPointIndex[1] != -1)
					{
						num = targetPathPoint.LinkPointIndex[1];
					}
					else
					{
						num = targetPathPoint.LinkPointIndex[0];
					}
				}
			}
			else if (num2 == 3)
			{
				if (targetPathPoint.LinkPointIndex[3] != -1)
				{
					num = targetPathPoint.LinkPointIndex[3];
				}
				else
				{
					num = targetPathPoint.LinkPointIndex[2];
				}
				if (!this.PointDataList[num].IsWalkable)
				{
					if (targetPathPoint.LinkPointIndex[1] != -1)
					{
						num = targetPathPoint.LinkPointIndex[1];
					}
					else
					{
						num = targetPathPoint.LinkPointIndex[0];
					}
				}
			}
			objRagdollNPC.TargetPathPoint = this.PointDataList[num];
			Vector3 pos;
			if (CitySimController.IsForward(objRagdollNPC.Position - objRagdollNPC.TargetPathPoint.PointPos, objRagdollNPC.TargetPathPoint.PointRight))
			{
				pos = objRagdollNPC.TargetPathPoint.PointPos + objRagdollNPC.TargetPathPoint.PointRight * Random.Range(objRagdollNPC.TargetPathPoint.MinWalkDis, objRagdollNPC.TargetPathPoint.MaxWalkDis);
			}
			else
			{
				pos = objRagdollNPC.TargetPathPoint.PointPos - objRagdollNPC.TargetPathPoint.PointRight * Random.Range(objRagdollNPC.TargetPathPoint.MinWalkDis, objRagdollNPC.TargetPathPoint.MaxWalkDis);
			}
			objRagdollNPC.WalkMoveTo(pos, 1f, new ObjCharacter.TargetArriveFinsh(this.GetNextPoint));
		}
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x00069990 File Offset: 0x00067B90
	public bool GetCreateLocation(Vector3 centerPos, float createDistance, List<LocationData> locationDataList)
	{
		LocationData playerLocation = this.GetPlayerLocation(centerPos, 100f);
		if (playerLocation == null)
		{
			return false;
		}
		CityPathPointData point = playerLocation.point1;
		CityPathPointData point2 = playerLocation.point2;
		if (playerLocation.DisFromPoint1 > createDistance)
		{
			locationDataList.Add(new LocationData
			{
				point1 = point,
				point2 = point2,
				DisFromPoint1 = playerLocation.DisFromPoint1 - createDistance
			});
			this.GetCreateLocationDataByRoad(point, point2, playerLocation.DisFromPoint1 - createDistance, locationDataList);
		}
		else
		{
			this.GetCreateLocationDataByRoad(point, point2, createDistance - playerLocation.DisFromPoint1, locationDataList);
		}
		float num = point.GetLinkDisByIndex(point2.SelfIndex) - playerLocation.DisFromPoint1;
		if (num > 0f)
		{
			if (num > createDistance)
			{
				locationDataList.Add(new LocationData
				{
					point1 = point2,
					point2 = point,
					DisFromPoint1 = num - createDistance
				});
				this.GetCreateLocationDataByRoad(point2, point, num - createDistance, locationDataList);
			}
			else
			{
				this.GetCreateLocationDataByRoad(point2, point, createDistance - num, locationDataList);
			}
		}
		return true;
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x00069A90 File Offset: 0x00067C90
	public void GetCreateLocationDataByRoad(CityPathPointData curPoint, CityPathPointData prePoint, float remainDis, List<LocationData> locationDataList)
	{
		for (int i = 0; i < curPoint.LinkPointIndex.Length; i++)
		{
			if (curPoint.LinkPointIndex[i] != -1 && curPoint.LinkPointIndex[i] != prePoint.SelfIndex)
			{
				CityPathPointData cityPathPointData = this.PointDataList[curPoint.LinkPointIndex[i]];
				if (prePoint.IsCross && cityPathPointData.IsCross && curPoint.IsCross)
				{
					if (remainDis != -1f)
					{
						this.GetCreateLocationDataByRoad(cityPathPointData, curPoint, -1f, locationDataList);
					}
				}
				else if (curPoint.LinkPointDis[i] > remainDis)
				{
					LocationData locationData = new LocationData();
					locationData.point1 = cityPathPointData;
					locationData.point2 = curPoint;
					if (cityPathPointData.IsCross)
					{
						locationData.DisFromPoint1 = 0f;
					}
					else
					{
						locationData.DisFromPoint1 = curPoint.LinkPointDis[i] - remainDis;
					}
					locationDataList.Add(locationData);
					if (remainDis != -1f)
					{
						this.GetCreateLocationDataByRoad(cityPathPointData, curPoint, -1f, locationDataList);
					}
				}
				else
				{
					this.GetCreateLocationDataByRoad(cityPathPointData, curPoint, remainDis - curPoint.LinkPointDis[i], locationDataList);
				}
			}
		}
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x00069BB8 File Offset: 0x00067DB8
	public LocationData GetPlayerLocationBig(Vector3 pos, float checkDis = 50f)
	{
		BlockPos blockPos = this.GetBlockPos(pos.x, pos.z);
		int x = blockPos.x;
		int z = blockPos.z;
		BlockData blockData = this.GetBlockData(x, z);
		if (blockData == null)
		{
			return null;
		}
		CityPathPointData point = null;
		CityPathPointData point2 = null;
		float disFromPoint = 0f;
		if (this.IsInCurBlock(pos, blockData, out point, out point2, out disFromPoint, checkDis))
		{
			return new LocationData
			{
				point1 = point,
				point2 = point2,
				DisFromPoint1 = disFromPoint
			};
		}
		List<BlockData> nearByBlockDataBig = this.GetNearByBlockDataBig(pos, false);
		for (int i = 0; i < nearByBlockDataBig.Count; i++)
		{
			if (this.IsInCurBlock(pos, nearByBlockDataBig[i], out point, out point2, out disFromPoint, checkDis))
			{
				return new LocationData
				{
					point1 = point,
					point2 = point2,
					DisFromPoint1 = disFromPoint
				};
			}
		}
		return null;
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x00069CA8 File Offset: 0x00067EA8
	public LocationData GetPlayerLocation(Vector3 pos, float checkDis = 100f)
	{
		BlockPos blockPos = this.GetBlockPos(pos.x, pos.z);
		int x = blockPos.x;
		int z = blockPos.z;
		BlockData blockData = this.GetBlockData(x, z);
		if (blockData == null)
		{
			return null;
		}
		CityPathPointData point = null;
		CityPathPointData point2 = null;
		float disFromPoint = 0f;
		if (this.IsInCurBlock(pos, blockData, out point, out point2, out disFromPoint, checkDis))
		{
			return new LocationData
			{
				point1 = point,
				point2 = point2,
				DisFromPoint1 = disFromPoint
			};
		}
		List<BlockData> nearByBlockData = this.GetNearByBlockData(pos, false);
		for (int i = 0; i < nearByBlockData.Count; i++)
		{
			if (this.IsInCurBlock(pos, nearByBlockData[i], out point, out point2, out disFromPoint, checkDis))
			{
				return new LocationData
				{
					point1 = point,
					point2 = point2,
					DisFromPoint1 = disFromPoint
				};
			}
		}
		return null;
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x00069D98 File Offset: 0x00067F98
	public bool IsInCurBlock(Vector3 pos, BlockData curBlock, out CityPathPointData point1, out CityPathPointData point2, out float disFromPoint1, float checkDis = 100f)
	{
		float num = float.PositiveInfinity;
		point1 = null;
		point2 = null;
		disFromPoint1 = 0f;
		CityPathPointData cityPathPointData = null;
		bool result = false;
		int i = 0;
		while (i < curBlock.PointIndex.Count)
		{
			CityPathPointData cityPathPointData2 = this.PointDataList[curBlock.PointIndex[i]];
			if (cityPathPointData2.LinkPointIndex[0] == -1)
			{
				goto IL_173;
			}
			cityPathPointData = this.PointDataList[cityPathPointData2.LinkPointIndex[0]];
			Vector2 inverseXZPos;
			Vector2 inverseXZPos2;
			if (Mathf.Abs(pos.y - cityPathPointData2.PointPos.y) <= 10f)
			{
				inverseXZPos = this.GetInverseXZPos(pos, cityPathPointData2.PointPos, cityPathPointData2.PointForward, cityPathPointData2.PointRight);
				inverseXZPos2 = this.GetInverseXZPos(pos, cityPathPointData.PointPos, cityPathPointData.PointForward, cityPathPointData.PointRight);
				if (!this.IsSameDir(cityPathPointData2, cityPathPointData))
				{
					if (inverseXZPos.y * inverseXZPos2.y > 0f)
					{
						float num2 = Mathf.Abs(inverseXZPos.x);
						if (num2 < num && num2 < checkDis)
						{
							num = num2;
							point1 = cityPathPointData2;
							point2 = cityPathPointData;
							disFromPoint1 = Mathf.Abs(inverseXZPos.y);
							result = true;
						}
					}
					goto IL_173;
				}
				if (inverseXZPos.y * inverseXZPos2.y >= 0f)
				{
					goto IL_173;
				}
				float num3 = Mathf.Abs(inverseXZPos.x);
				if (num3 < num && num3 < checkDis)
				{
					num = num3;
					point1 = cityPathPointData2;
					point2 = cityPathPointData;
					disFromPoint1 = Mathf.Abs(inverseXZPos.y);
					result = true;
					goto IL_173;
				}
				goto IL_173;
			}
			IL_32E:
			i++;
			continue;
			IL_173:
			if ((cityPathPointData2.LinkPointIndex[0] == -1 || this.IsSameDir(cityPathPointData2, cityPathPointData)) && !cityPathPointData2.IsCross && (cityPathPointData2.LinkPointIndex[1] == -1 || (this.IsSameDir(cityPathPointData2, this.PointDataList[cityPathPointData2.LinkPointIndex[1]]) && curBlock.PointIndex.Contains(cityPathPointData2.LinkPointIndex[1]))))
			{
				goto IL_32E;
			}
			cityPathPointData2 = this.PointDataList[curBlock.PointIndex[i]];
			if (cityPathPointData2.LinkPointIndex[1] == -1)
			{
				goto IL_32E;
			}
			cityPathPointData = this.PointDataList[cityPathPointData2.LinkPointIndex[1]];
			if (Mathf.Abs(pos.y - cityPathPointData2.PointPos.y) > 10f)
			{
				goto IL_32E;
			}
			inverseXZPos = this.GetInverseXZPos(pos, cityPathPointData2.PointPos, cityPathPointData2.PointForward, cityPathPointData2.PointRight);
			inverseXZPos2 = this.GetInverseXZPos(pos, cityPathPointData.PointPos, cityPathPointData.PointForward, cityPathPointData.PointRight);
			if (!this.IsSameDir(cityPathPointData2, cityPathPointData))
			{
				if (inverseXZPos.y * inverseXZPos2.y > 0f)
				{
					float num4 = Mathf.Abs(inverseXZPos.x);
					if (num4 < num && num4 < checkDis)
					{
						num = num4;
						point1 = cityPathPointData2;
						point2 = cityPathPointData;
						disFromPoint1 = Mathf.Abs(inverseXZPos.y);
						result = true;
					}
				}
				goto IL_32E;
			}
			if (inverseXZPos.y * inverseXZPos2.y >= 0f)
			{
				goto IL_32E;
			}
			float num5 = Mathf.Abs(inverseXZPos.x);
			if (num5 < num && num5 < checkDis)
			{
				num = num5;
				point1 = cityPathPointData2;
				point2 = cityPathPointData;
				disFromPoint1 = Mathf.Abs(inverseXZPos.y);
				result = true;
				goto IL_32E;
			}
			goto IL_32E;
		}
		return result;
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x0006A0EC File Offset: 0x000682EC
	public bool IsSameDir(CityPathPointData point1, CityPathPointData point2)
	{
		return (point1.LinkPointIndex[0] != point2.SelfIndex || point2.LinkPointIndex[0] != point1.SelfIndex) && (point1.LinkPointIndex[1] != point2.SelfIndex || point2.LinkPointIndex[1] != point1.SelfIndex);
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x0006A148 File Offset: 0x00068348
	public Vector2 GetInverseXZPos(Vector3 targetPos, Vector3 sourcePos, Vector3 sourceForward, Vector3 sourceRight)
	{
		Vector3 vector = targetPos - sourcePos;
		float num = Vector3.Project(vector, sourceRight).magnitude;
		float num2 = Vector3.Project(vector, sourceForward).magnitude;
		if (!CitySimController.IsForward(vector, sourceRight))
		{
			num = -num;
		}
		if (!CitySimController.IsForward(vector, sourceForward))
		{
			num2 = -num2;
		}
		return new Vector2(num, num2);
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x0006A1A8 File Offset: 0x000683A8
	public static bool IsForward(Vector3 dir1, Vector3 dir2)
	{
		return Vector3.Dot(dir1, dir2) > 0f;
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x0006A1C0 File Offset: 0x000683C0
	public BlockPos GetBlockPos(float x, float z)
	{
		return new BlockPos((int)(x - this.CityLeftBottomPos.x) / this.BlockLength, (int)(z - this.CityLeftBottomPos.y) / this.BlockLength);
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x0006A1F4 File Offset: 0x000683F4
	private void LightingSystemUpdate()
	{
		this.mRoadStateTimeCount += Time.deltaTime;
		if (CitySimController.CurRoadState == ROAD_STATE.NS_STRAIT_PASS)
		{
			if (this.mRoadStateTimeCount > this.mRoadStateChangeTime)
			{
				CitySimController.CurRoadState = ROAD_STATE.EW_STRAIT_PASS;
				this.mRoadStateTimeCount = 0f;
			}
		}
		else if (CitySimController.CurRoadState == ROAD_STATE.EW_STRAIT_PASS)
		{
			if (this.mRoadStateTimeCount > this.mRoadStateChangeTime)
			{
				CitySimController.CurRoadState = ROAD_STATE.PERSON_PASS1;
				this.mRoadStateTimeCount = 0f;
			}
		}
		else if (CitySimController.CurRoadState == ROAD_STATE.PERSON_PASS1)
		{
			if (this.mRoadStateTimeCount > this.mRoadStateChangeTime)
			{
				CitySimController.CurRoadState = ROAD_STATE.NS_TURN_PASS;
				this.mRoadStateTimeCount = 0f;
			}
		}
		else if (CitySimController.CurRoadState == ROAD_STATE.NS_TURN_PASS)
		{
			if (this.mRoadStateTimeCount > this.mRoadStateChangeTime)
			{
				CitySimController.CurRoadState = ROAD_STATE.EW_TURN_PASS;
				this.mRoadStateTimeCount = 0f;
			}
		}
		else if (CitySimController.CurRoadState == ROAD_STATE.EW_TURN_PASS)
		{
			if (this.mRoadStateTimeCount > this.mRoadStateChangeTime)
			{
				CitySimController.CurRoadState = ROAD_STATE.PERSON_PASS2;
				this.mRoadStateTimeCount = 0f;
			}
		}
		else if (CitySimController.CurRoadState == ROAD_STATE.PERSON_PASS2 && this.mRoadStateTimeCount > this.mRoadStateChangeTime)
		{
			CitySimController.CurRoadState = ROAD_STATE.NS_STRAIT_PASS;
			this.mRoadStateTimeCount = 0f;
		}
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x0006A33C File Offset: 0x0006853C
	public void CheckObjStop()
	{
		if (this.mEnableRagdollList == null || this.mEnableFakeAICarList == null)
		{
			return;
		}
		for (int i = this.mEnableRagdollList.Count - 1; i >= 0; i--)
		{
			if (this.mEnableRagdollList[i] == null)
			{
				this.mEnableRagdollList.RemoveAt(i);
			}
			else if (this.mEnableRagdollList[i].CheckIndex != this.checkIndex)
			{
				this.mEnableRagdollList[i].CheckIndex = this.checkIndex;
				if (this.CheckObstacle(this.mEnableRagdollList[i].transform, 1f, 3f, true, null))
				{
					if (!this.mEnableRagdollList[i].IsDie)
					{
						this.mEnableRagdollList[i].DisactiveTargetArriveFinish();
						this.mEnableRagdollList[i].StopMove();
					}
				}
				else if (this.mEnableRagdollList[i].AILogic != null && this.mEnableRagdollList[i].AILogic.curState == AISTATE.PATROL_STATE && !this.mEnableRagdollList[i].IsDie)
				{
					this.mEnableRagdollList[i].ContinueMove();
				}
				break;
			}
		}
		for (int j = 0; j < this.mEnableFakeAICarList.Count; j++)
		{
			if (this.mEnableFakeAICarList[j].CheckIndex != this.checkIndex && !this.mEnableFakeAICarList[j].PlayerCar.IsDie)
			{
				this.mEnableFakeAICarList[j].CheckIndex = this.checkIndex;
				if (this.CheckObstacle(this.mEnableFakeAICarList[j].transform, 1.5f, 10f, false, this.mEnableFakeAICarList[j]))
				{
					if (this.mEnableFakeAICarList[j].enabled)
					{
						this.mEnableFakeAICarList[j].DisactiveTargetArriveFinish();
						this.mEnableFakeAICarList[j].StopMove();
					}
				}
				else if (this.mEnableFakeAICarList[j].enabled)
				{
					this.mEnableFakeAICarList[j].ContinueMove();
				}
				return;
			}
		}
		this.checkIndex++;
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x0006A5BC File Offset: 0x000687BC
	public bool CheckObstacle(Transform sourceObj, float xRange, float zRange, bool isNpc, ObjFakeAICar aiCar = null)
	{
		Vector3 inversePos = Vector3.zero;
		if (!isNpc)
		{
			inversePos = sourceObj.InverseTransformPoint(this.mMainPlayerTrans.position);
			if (this.CheckShowDoor(inversePos))
			{
				this.mCurNearestCar = aiCar;
			}
			if (Mathf.Abs(inversePos.x) < xRange && inversePos.z > 0f && inversePos.z < zRange)
			{
				return true;
			}
			for (int i = this.mEnableRagdollList.Count - 1; i >= 0; i--)
			{
				if (this.mEnableRagdollList[i] == null)
				{
					this.mEnableRagdollList.RemoveAt(i);
				}
				else
				{
					inversePos = sourceObj.InverseTransformPoint(this.mEnableRagdollList[i].Position);
					if (Mathf.Abs(inversePos.x) < xRange && inversePos.z > 0f && inversePos.z < zRange)
					{
						return true;
					}
				}
			}
		}
		for (int j = 0; j < this.mEnableFakeAICarList.Count; j++)
		{
			inversePos = sourceObj.InverseTransformPoint(this.mEnableFakeAICarList[j].transform.position);
			if (Mathf.Abs(inversePos.x) < xRange && inversePos.z > 0f && inversePos.z < zRange)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x0006A72C File Offset: 0x0006892C
	private bool CheckShowDoor(Vector3 inversePos)
	{
		return !this.mMainPlayer.SkillLogic.IsUsingSkill && (inversePos.x < 3f && inversePos.x > -3f && inversePos.z > -5f && inversePos.z < 5f);
	}

	// Token: 0x17000390 RID: 912
	// (get) Token: 0x06001095 RID: 4245 RVA: 0x0006A798 File Offset: 0x00068998
	// (set) Token: 0x06001096 RID: 4246 RVA: 0x0006A7A0 File Offset: 0x000689A0
	public ObjPlayerCar PlayerCar
	{
		get
		{
			return this.mPlayerCar;
		}
		set
		{
			this.mPlayerCar = value;
		}
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x0006A7AC File Offset: 0x000689AC
	public void RobCar(DelegateDefine.NoParamDelegate finishAct = null)
	{
		if (this.mPlayerCar == null)
		{
			if (this.mCurNearestCar == null)
			{
				return;
			}
			bool isNpcInCar = this.mCurNearestCar.enabled;
			this.mCurNearestCar.DisableFakeAICar();
			this.mPlayerCar = this.mCurNearestCar.PlayerCar;
			this.mPlayerCar.enabled = true;
			this.mPlayerCar.rigidbody.isKinematic = true;
			this.mSmoothFollowCamPos.Height = this.mPlayerCar.CurMountData.CamHeightMeter;
			this.mSmoothFollowCamPos.Distance = this.mPlayerCar.CurMountData.CamDisMeter;
			this.mSmoothFollowCamPos.SetTarget(this.mPlayerCar.CarControl);
			UnityVersionUtil.SetActiveRecursive(this.mSmoothFollowCamPos.gameObject, true);
			this.mSmoothFollowCamPos.UpdateToTargetPos();
			this.mMainPlayer.StopAutoAndSkill();
			this.mMainPlayer.LeveAutoCombat();
			this.mMainPlayerTrans.rigidbody.isKinematic = true;
			SingletonUnity<UIManager>.Instance.HideBaseUI();
			this.mMainPlayer.InvincibleFlag = true;
			this.mMainPlayer.IsShowInvincibleEffect = false;
			this.mMainPlayer.SelectTarget(null);
			bool GetOnCarFlag = true;
			this.mMainPlayer.CheckBeforeOnCar();
			vp_Timer.In(5f, delegate()
			{
				if (GetOnCarFlag)
				{
					this.mMainPlayer.DisactiveTargetArriveFinish();
					this.mMainPlayer.DisableNavMeshAgent();
					this.mMainPlayer.CacheTransform.parent = this.mPlayerCar.DummyPlayerPoint;
					this.mMainPlayer.IsLocalDrivingCar = true;
					this.mMainPlayer.CurPlayerCar = this.mPlayerCar;
					TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOLocalMove(this.mMainPlayer.CacheTransform, Vector3.zero, 0.5f, false), 10);
					TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOLocalRotate(this.mMainPlayer.CacheTransform, Vector3.zero, 0.5f, 0), 10);
					vp_Timer.In(0.1f, delegate()
					{
						if (this.RobNpcFakeObj != null)
						{
							this.RobNpcFakeObj.DestroyNpcFakeObj();
							this.RobNpcFakeObj = null;
						}
						if (isNpcInCar)
						{
							this.mPlayerCar.MeshRoot.animation.Stop();
							this.mPlayerCar.MeshRoot.animation.Play("RobCar");
							this.RobNpcFakeObj = new FakeObjLogic();
							if (this.mPlayerCar.CurMountData.ID.Equals("jingChe"))
							{
								this.RobNpcFakeObj.InitAnimaFakeNpcObj("NPC_Nan_047", this.mPlayerCar.DummyNPCPoint, "RobOffCar");
							}
							else
							{
								this.RobNpcFakeObj.InitAnimaFakeNpcObj("NPC_Nan_036", this.mPlayerCar.DummyNPCPoint, "RobOffCar");
							}
							this.mMainPlayer.AnimationLogic.ForcePlayAnimation("RobCar", null, -1f, 0f);
							if (SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.isCurMissionTypeEnable(MISSION_LOGICTYPE.ROB_CAR))
							{
								local_npc_die.request request = new local_npc_die.request();
								request.type = 2L;
								NetLogic.GetInstance().Send<Protocol.local_npc_die>(request, null);
								Debug.Log("ROB_CAR !!!!!!!!!!!!!!!!!!!!!!");
							}
							if (SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.isCurMissionTypeEnable(MISSION_LOGICTYPE.TARGET_ROB_CAR))
							{
								local_npc_die.request request2 = new local_npc_die.request();
								request2.type = 6L;
								NetLogic.GetInstance().Send<Protocol.local_npc_die>(request2, null);
							}
						}
						else
						{
							if (this.mCurNearestCar.IsStaticCar)
							{
								if (SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.isCurMissionTypeEnable(MISSION_LOGICTYPE.ROB_CAR))
								{
									local_npc_die.request request3 = new local_npc_die.request();
									request3.type = 2L;
									NetLogic.GetInstance().Send<Protocol.local_npc_die>(request3, null);
									Debug.Log("ROB_CAR !!!!!!!!!!!!!!!!!!!!!!");
								}
								if (SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.isCurMissionTypeEnable(MISSION_LOGICTYPE.TARGET_ROB_CAR))
								{
									local_npc_die.request request4 = new local_npc_die.request();
									request4.type = 6L;
									NetLogic.GetInstance().Send<Protocol.local_npc_die>(request4, null);
								}
							}
							this.mPlayerCar.MeshRoot.animation.Stop();
							if (this.mPlayerCar.CurMountData.IsMotorBool)
							{
								this.mMainPlayer.AnimationLogic.ForcePlayAnimation("GetOnCar_Motor", null, -1f, 0f);
							}
							else
							{
								this.mPlayerCar.MeshRoot.animation.Play("GetOnCar");
								this.mMainPlayer.AnimationLogic.ForcePlayAnimation("GetOnCar", null, -1f, 0f);
							}
						}
						if (this.mMainPlayer.SimpleShadow != null)
						{
							UnityVersionUtil.SetActiveRecursive(this.mMainPlayer.SimpleShadow, false);
						}
						vp_Timer.In(this.mMainPlayer.AnimationLogic.CurAnimationLength, delegate()
						{
							if (this.mPlayerCar.CurMountData.IsShowPlayer == 0 && !this.mPlayerCar.CurMountData.IsMotorBool)
							{
								this.mMainPlayer.DisableMainPlayer();
							}
							else
							{
								this.mMainPlayer.GetComponent<CapsuleCollider>().enabled = false;
								this.mMainPlayer.RemoveXRayMat();
								this.mMainPlayer.enabled = false;
								if (this.mPlayerCar.CurMountData.IsMotorBool)
								{
									this.mMainPlayer.AnimationLogic.ForceSampleAnimation("GetOnCar_Motor", 1f, null, -1f, 0f);
									this.mMainPlayer.DisactiveHeadInfo();
								}
								else
								{
									this.mMainPlayer.AnimationLogic.ForceSampleAnimation("GetOnCar", 1f, null, -1f, 0f);
								}
							}
							this.ChangeToCarCtl(this.mPlayerCar);
							if (isNpcInCar)
							{
								this.RobNpcFakeObj.FakeObj.transform.parent.parent = null;
								vp_Timer.In(2f, delegate()
								{
									if (this.RobNpcFakeObj != null)
									{
										this.RobNpcFakeObj.DestroyNpcFakeObj();
										this.RobNpcFakeObj = null;
									}
								}, null);
							}
						}, this.getOnCarAnimaHandle);
					}, null);
				}
			}, this.getOnCarHandle);
			this.mMainPlayer.MoveTo(this.mPlayerCar.DummyPlayerPoint.position, 1f, delegate(ObjCharacter A_1)
			{
				this.getOnCarHandle.Cancel();
				GetOnCarFlag = false;
				this.mMainPlayer.DisableNavMeshAgent();
				this.mMainPlayer.CacheTransform.parent = this.mPlayerCar.DummyPlayerPoint;
				this.mMainPlayer.IsLocalDrivingCar = true;
				this.mMainPlayer.CurPlayerCar = this.mPlayerCar;
				TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOLocalMove(this.mMainPlayer.CacheTransform, Vector3.zero, 0.5f, false), 10);
				TweenSettingsExtensions.SetEase<Tweener>(ShortcutExtensions.DOLocalRotate(this.mMainPlayer.CacheTransform, Vector3.zero, 0.5f, 0), 10);
				vp_Timer.In(0.1f, delegate()
				{
					if (this.RobNpcFakeObj != null)
					{
						this.RobNpcFakeObj.DestroyNpcFakeObj();
						this.RobNpcFakeObj = null;
					}
					if (isNpcInCar)
					{
						this.mPlayerCar.MeshRoot.animation.Stop();
						this.mPlayerCar.MeshRoot.animation.Play("RobCar");
						this.RobNpcFakeObj = new FakeObjLogic();
						if (this.mPlayerCar.CurMountData.ID.Equals("jingChe"))
						{
							this.RobNpcFakeObj.InitAnimaFakeNpcObj("NPC_Nan_047", this.mPlayerCar.DummyNPCPoint, "RobOffCar");
						}
						else
						{
							this.RobNpcFakeObj.InitAnimaFakeNpcObj("NPC_Nan_036", this.mPlayerCar.DummyNPCPoint, "RobOffCar");
						}
						this.mMainPlayer.AnimationLogic.PlayAnimation("RobCar", null, -1f);
						if (SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.isCurMissionTypeEnable(MISSION_LOGICTYPE.ROB_CAR))
						{
							local_npc_die.request request = new local_npc_die.request();
							request.type = 2L;
							NetLogic.GetInstance().Send<Protocol.local_npc_die>(request, null);
						}
						if (SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.isCurMissionTypeEnable(MISSION_LOGICTYPE.TARGET_ROB_CAR))
						{
							local_npc_die.request request2 = new local_npc_die.request();
							request2.type = 6L;
							NetLogic.GetInstance().Send<Protocol.local_npc_die>(request2, null);
						}
					}
					else
					{
						if (this.mCurNearestCar.IsStaticCar)
						{
							if (SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.isCurMissionTypeEnable(MISSION_LOGICTYPE.ROB_CAR))
							{
								local_npc_die.request request3 = new local_npc_die.request();
								request3.type = 2L;
								NetLogic.GetInstance().Send<Protocol.local_npc_die>(request3, null);
							}
							if (SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.isCurMissionTypeEnable(MISSION_LOGICTYPE.TARGET_ROB_CAR))
							{
								local_npc_die.request request4 = new local_npc_die.request();
								request4.type = 6L;
								NetLogic.GetInstance().Send<Protocol.local_npc_die>(request4, null);
							}
						}
						this.mPlayerCar.MeshRoot.animation.Stop();
						if (this.mPlayerCar.CurMountData.IsMotorBool)
						{
							this.mMainPlayer.AnimationLogic.ForcePlayAnimation("GetOnCar_Motor", null, -1f, 0f);
						}
						else
						{
							this.mPlayerCar.MeshRoot.animation.Play("GetOnCar");
							this.mMainPlayer.AnimationLogic.PlayAnimation("GetOnCar", null, -1f);
						}
					}
					if (this.mMainPlayer.SimpleShadow != null)
					{
						UnityVersionUtil.SetActiveRecursive(this.mMainPlayer.SimpleShadow, false);
					}
					vp_Timer.In(this.mMainPlayer.AnimationLogic.CurAnimationLength, delegate()
					{
						if (this.mPlayerCar.CurMountData.IsShowPlayer == 0 && !this.mPlayerCar.CurMountData.IsMotorBool)
						{
							this.mMainPlayer.DisableMainPlayer();
						}
						else
						{
							this.mMainPlayer.GetComponent<CapsuleCollider>().enabled = false;
							this.mMainPlayer.RemoveXRayMat();
							this.mMainPlayer.enabled = false;
							if (this.mPlayerCar.CurMountData.IsMotorBool)
							{
								this.mMainPlayer.DisactiveHeadInfo();
								this.mMainPlayer.AnimationLogic.ForceSampleAnimation("GetOnCar_Motor", 1f, null, -1f, 0f);
							}
							else
							{
								this.mMainPlayer.AnimationLogic.ForceSampleAnimation("GetOnCar", 1f, null, -1f, 0f);
							}
						}
						this.ChangeToCarCtl(this.mPlayerCar);
						if (isNpcInCar)
						{
							this.RobNpcFakeObj.FakeObj.transform.parent.parent = null;
							vp_Timer.In(2f, delegate()
							{
								if (this.RobNpcFakeObj != null)
								{
									this.RobNpcFakeObj.DestroyNpcFakeObj();
									this.RobNpcFakeObj = null;
								}
							}, null);
						}
					}, this.getOnCarAnimaHandle);
				}, null);
			});
		}
		else
		{
			SingletonUnity<UIManager>.Instance.HideBaseUI();
			this.mPlayerCar.DisableCar();
			ObjPlayerCar tempCar = this.mPlayerCar;
			this.mMainPlayer.InvincibleFlag = true;
			this.mMainPlayer.IsShowInvincibleEffect = false;
			this.mMainPlayer.EnableMainPlayer();
			this.mMainPlayer.GetComponent<CapsuleCollider>().enabled = true;
			this.mMainPlayer.CameraController.IdealYaw = tempCar.transform.eulerAngles.y;
			this.mMainPlayer.CameraController.UpdateNow();
			if (this.mPlayerCar.CurMountData.IsMotorBool)
			{
				this.mMainPlayer.AnimationLogic.PlayAnimation("GetOffCar_Motor", delegate()
				{
					this.ChangeToPlayerCtl();
					this.mMainPlayer.AddXRayMat();
					tempCar.rigidbody.useGravity = true;
					tempCar.rigidbody.isKinematic = false;
					if (finishAct != null)
					{
						finishAct();
					}
				}, -1f);
			}
			else
			{
				this.mPlayerCar.MeshRoot.animation.Play("GetOffCar");
				this.mMainPlayer.AnimationLogic.PlayAnimation("GetOffCar", delegate()
				{
					this.ChangeToPlayerCtl();
					this.mMainPlayer.AddXRayMat();
					tempCar.rigidbody.useGravity = true;
					tempCar.rigidbody.isKinematic = false;
					if (finishAct != null)
					{
						finishAct();
					}
				}, -1f);
			}
			this.mPlayerCar.enabled = false;
			this.mPlayerCar = null;
		}
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x0006AA98 File Offset: 0x00068C98
	public void ChangeToPlayerCtl()
	{
		this.mMainPlayer.InvincibleFlag = false;
		this.mMainPlayer.IsShowInvincibleEffect = true;
		this.mMainPlayer.transform.parent = null;
		this.mMainPlayer.EnableNavMeshAgent();
		this.mMainPlayer.rigidbody.isKinematic = false;
		if (this.mMainPlayer.SimpleShadow != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.mMainPlayer.SimpleShadow, true);
		}
		CameraController camCtl = this.mMainPlayer.CameraController;
		vp_Timer.In(0.1f, delegate()
		{
			camCtl.LerpBackToPlayer(1f, delegate
			{
				this.ResetPlayerUI();
				if (SingletonUnity<RealTimeShadow>.Exists)
				{
					SingletonUnity<RealTimeShadow>.Instance.EnableRealTimeShadow();
				}
				for (int i = 0; i < this.mMainPlayer.PartObject.Length; i++)
				{
					if (this.mMainPlayer.PartObject[i] != null)
					{
						this.mMainPlayer.PartObject[i].layer = LayerMask.NameToLayer("ShadowCaster");
					}
				}
			});
		}, null);
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x0006AB48 File Offset: 0x00068D48
	public void ResetPlayerUI()
	{
		UIManager instance = SingletonUnity<UIManager>.Instance;
		instance.ReShowBaseUI();
		instance.CloseUI(UIInfo.CarControllerRoot);
		instance.ShowUI(UIInfo.YiDongKongZhiUI, null, null);
		instance.ShowUI(UIInfo.JueseJiNengQuUI, delegate
		{
			SingletonUnity<JueseJiNengQuLogic>.Instance.Reset(false);
		}, null);
		instance.ShowUI(UIInfo.TouXiangKuangUI, delegate(bool isSuccess, object param)
		{
			if (isSuccess)
			{
				SingletonUnity<TouXiangKuangLogic>.Instance.Init();
			}
		}, null);
		instance.ShowUI(UIInfo.FunctionBtnRootUI, delegate
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.Reset();
		}, null);
		instance.ShowUI(UIInfo.ExpLineRoot, null, null);
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager.IsCanUsePotion() && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
		{
			instance.ShowUI(UIInfo.PotionObjRoot, delegate(bool bSuccess, object param)
			{
				SingletonUnity<PotionLogic>.Instance.Reset();
			}, null);
		}
	}

	// Token: 0x0600109A RID: 4250 RVA: 0x0006AC54 File Offset: 0x00068E54
	private void ChangeToCarCtl(ObjPlayerCar mPlayerCar)
	{
		this.mMainPlayer.InvincibleFlag = false;
		this.mMainPlayer.IsShowInvincibleEffect = true;
		UIManager instance = SingletonUnity<UIManager>.Instance;
		instance.ReShowBaseUI();
		instance.CloseUI(UIInfo.YiDongKongZhiUI);
		instance.CloseUI(UIInfo.JueseJiNengQuUI);
		instance.ShowCarDefaultUI();
		instance.CloseUI(UIInfo.ExpLineRoot);
		instance.CloseUI(UIInfo.PotionObjRoot);
		instance.CloseUI(UIInfo.FunctionBtnRootUI);
		CameraController cameraController = this.mMainPlayer.CameraController;
		mPlayerCar.EnableCar(this.mMainPlayer);
		cameraController.LerpToTargetLocalZero(this.mSmoothFollowCamPos.transform, 1f, delegate
		{
			if (TutorialManager.CurStep == TUTORIAL_STEP.ROB_CAR_FINISH)
			{
				TutorialManager.MoveNext(false);
			}
		});
		this.mMainPlayer.SelectTarget(null);
		if (SingletonUnity<RealTimeShadow>.Exists)
		{
			SingletonUnity<RealTimeShadow>.Instance.DisableRealTimeShadow();
		}
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x0006AD30 File Offset: 0x00068F30
	public void InitMonsterList()
	{
		if (this.initFlag)
		{
			return;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsTutorialScene())
		{
			return;
		}
		this.initFlag = true;
		this.mCurMonsterDataList = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.MonsterDataList;
		for (int i = 0; i < this.BlockData.Length; i++)
		{
			for (int j = 0; j < this.BlockData[i].PointLine.Length; j++)
			{
				this.BlockData[i].PointLine[j].BlockMonsterList.Clear();
			}
		}
		for (int k = 0; k < this.mCurMonsterDataList.Count; k++)
		{
			BlockPos blockPos = this.GetBlockPos(this.mCurMonsterDataList[k].PositionX, this.mCurMonsterDataList[k].PositionZ);
			BlockData blockData = this.GetBlockData(blockPos.x, blockPos.z);
			if (blockData != null)
			{
				blockData.BlockMonsterList.Add(new BlockMonsterData(this.mCurMonsterDataList[k], -1L));
			}
		}
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x0006AE58 File Offset: 0x00069058
	public void NormalNpcCreateCheck(Vector3 pos)
	{
		List<BlockData> nearByBlockData = this.GetNearByBlockData(pos, true);
		List<BlockMonsterData> list = new List<BlockMonsterData>();
		for (int i = 0; i < nearByBlockData.Count; i++)
		{
			for (int j = 0; j < nearByBlockData[i].BlockMonsterList.Count; j++)
			{
				if (nearByBlockData[i].BlockMonsterList[j].ServerId == -1L && Vector3.Distance(nearByBlockData[i].BlockMonsterList[j].Data.GetNpcXZPos(), pos) < this.NpcCreateDis)
				{
					list.Add(nearByBlockData[i].BlockMonsterList[j]);
				}
			}
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		for (int k = 0; k < list.Count; k++)
		{
			NpcData npcDataByID = DataManager.GetNpcDataByID(list[k].Data.NpcID);
			if (npcDataByID != null)
			{
				if (npcDataByID.IsPlayerModel == 0)
				{
					if (!playerData.IsFinishDownload)
					{
						CharacterModelData characterModelDataByID = DataManager.GetCharacterModelDataByID(npcDataByID.Model);
						if (!this.mBeforDownloadNpcModelIdList.Contains(characterModelDataByID.Name))
						{
							goto IL_34B;
						}
					}
					ObjRagdollNPC npc = this.GetNpc(list[k]);
					if (npc != null)
					{
						list[k].ServerId = npc.ServerId;
						if (!this.mStaticNpcServerIdDic.ContainsKey(list[k].ServerId))
						{
							this.mStaticNpcServerIdDic.Add(list[k].ServerId, list[k]);
						}
					}
				}
				else if (!string.IsNullOrEmpty(npcDataByID.TalkGroup))
				{
					if (playerData.Domin_InfoDic.ContainsKey(npcDataByID.TalkGroup))
					{
						domin_info domin_info = playerData.Domin_InfoDic[npcDataByID.TalkGroup];
						if (domin_info.state != 1L && domin_info.serverId != -1L && playerData.Domin_CharacterDic.ContainsKey(domin_info.serverId))
						{
							ObjZombieRagdollPlayer objZombieRagdollPlayer = this.CreateZombieRagdollPlayer(list[k], playerData.Domin_CharacterDic[domin_info.serverId]);
							if (objZombieRagdollPlayer != null)
							{
								if (!this.mEnableObjZombiePlayerList.Contains(objZombieRagdollPlayer))
								{
									this.mEnableObjZombiePlayerList.Add(objZombieRagdollPlayer);
								}
								list[k].ServerId = objZombieRagdollPlayer.ServerId;
								if (!this.mStaticNpcServerIdDic.ContainsKey(list[k].ServerId))
								{
									this.mStaticNpcServerIdDic.Add(list[k].ServerId, list[k]);
								}
							}
						}
					}
				}
				else
				{
					ObjZombieRagdollPlayer objZombieRagdollPlayer2 = this.CreateZombieRagdollPlayer(list[k], null);
					if (objZombieRagdollPlayer2 != null)
					{
						if (!this.mEnableObjZombiePlayerList.Contains(objZombieRagdollPlayer2))
						{
							this.mEnableObjZombiePlayerList.Add(objZombieRagdollPlayer2);
						}
						list[k].ServerId = objZombieRagdollPlayer2.ServerId;
						if (!this.mStaticNpcServerIdDic.ContainsKey(list[k].ServerId))
						{
							this.mStaticNpcServerIdDic.Add(list[k].ServerId, list[k]);
						}
					}
				}
			}
			IL_34B:;
		}
		List<BlockCarData> list2 = new List<BlockCarData>();
		for (int l = 0; l < nearByBlockData.Count; l++)
		{
			for (int m = 0; m < nearByBlockData[l].BlockCarDataList.Count; m++)
			{
				if (nearByBlockData[l].BlockCarDataList[m].ServerId == -1L && Vector3.Distance(nearByBlockData[l].BlockCarDataList[m].CarPos, pos) < this.NpcCreateDis)
				{
					list2.Add(nearByBlockData[l].BlockCarDataList[m]);
				}
			}
		}
		for (int n = 0; n < list2.Count; n++)
		{
			ObjFakeAICar staticCar = this.GetStaticCar(list2[n], false);
			if (staticCar != null)
			{
				list2[n].ServerId = staticCar.PlayerCar.ServerId;
				if (!this.mStaticCarDic.ContainsKey(list2[n].ServerId))
				{
					this.mStaticCarDic.Add(list2[n].ServerId, list2[n]);
				}
			}
		}
		this.CheckSpecialCar(pos);
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x0006B310 File Offset: 0x00069510
	private void CheckSpecialCar(Vector3 pos)
	{
		if (!GameManager.IsSupportCurDataVersion167())
		{
			return;
		}
		if (!this.mIsHaveSpecialCar)
		{
			return;
		}
		if (this.mSpecialCarServerId == -1L && Vector3.Distance(this.SpecialCarPos, pos) < this.NpcCreateDis)
		{
			ObjFakeAICar staticCar = this.GetStaticCar(new BlockCarData
			{
				CarId = this.mSpecialCarId,
				CarAngle = this.SpecialCarAngle,
				CarPos = this.SpecialCarPos
			}, false);
			this.mSpecialCarServerId = staticCar.PlayerCar.ServerId;
		}
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x0006B39C File Offset: 0x0006959C
	private ObjFakeAICar GetSpecialCar(Vector3 pos)
	{
		ObjCarInitData initData = new ObjCarInitData(pos, this.SpecialCarAngle, UUID.GenUUID(), this.mSpecialCarId, false, DataManager.GetMountDataById(this.mSpecialCarId));
		ObjFakeAICar fakeAICar = this.mObjManager.GetFakeAICar(initData, false);
		if (fakeAICar != null)
		{
			fakeAICar.ResetStaticCar();
		}
		else
		{
			Debug.Log("Car == null!!!!!!!!!!!!! :: " + this.mEnableFakeAICarList.Count);
		}
		return fakeAICar;
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x0006B414 File Offset: 0x00069614
	public ObjFakeAICar GetStaticCar(BlockCarData data, bool isFriend = false)
	{
		Vector3 pos = new Vector3(data.CarPos.x, SceneManager.GetHitHeight(data.CarPos.x, data.CarPos.z), data.CarPos.z) + Vector3.up * 0.1f;
		string text = data.CarId;
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload && !this.BeforeDownloadStaticCar.Contains(text))
		{
			text = this.BeforeDownloadStaticCar[Random.Range(0, this.BeforeDownloadStaticCar.Count)];
		}
		ObjCarInitData initData = new ObjCarInitData(pos, data.CarAngle, UUID.GenUUID(), text, false, DataManager.GetMountDataById(text));
		if (this.mObjManager == null)
		{
			this.mObjManager = Singleton<ObjManager>.Instance;
		}
		ObjFakeAICar fakeAICar = this.mObjManager.GetFakeAICar(initData, isFriend);
		if (fakeAICar != null)
		{
			fakeAICar.ResetStaticCar();
		}
		return fakeAICar;
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x0006B50C File Offset: 0x0006970C
	public ObjRagdollNPC GetNpc(BlockMonsterData curData)
	{
		if (!SceneManager.IsInNavmeshArea(curData.Data.GetNpcPos()))
		{
			return null;
		}
		ObjInitNpcData objInitNpcData = new ObjInitNpcData();
		objInitNpcData.mServerID = UUID.GenUUID();
		objInitNpcData.mPos = curData.Data.GetNpcPos();
		objInitNpcData.mDir = MathUtil.HeadingToVector3(curData.Data.PositionO);
		objInitNpcData.npcInfoData = DataManager.GetNpcDataByID(curData.Data.NpcID);
		objInitNpcData.mCharacterModelId = objInitNpcData.npcInfoData.Model;
		objInitNpcData.MaxHP = objInitNpcData.npcInfoData.Hp;
		objInitNpcData.HP = objInitNpcData.npcInfoData.Hp;
		objInitNpcData.ATK = objInitNpcData.npcInfoData.Atk;
		objInitNpcData.DEF = objInitNpcData.npcInfoData.Def;
		objInitNpcData.HIT = objInitNpcData.npcInfoData.HIT;
		objInitNpcData.EVA = objInitNpcData.npcInfoData.DGE;
		objInitNpcData.CRI = objInitNpcData.npcInfoData.CRI;
		objInitNpcData.EXD = objInitNpcData.npcInfoData.EXD;
		objInitNpcData.EXR = objInitNpcData.npcInfoData.EXR;
		objInitNpcData.RES = objInitNpcData.npcInfoData.RES;
		objInitNpcData.CRD = objInitNpcData.npcInfoData.CRD;
		objInitNpcData.CRR = objInitNpcData.npcInfoData.CRR;
		objInitNpcData.DEFA = objInitNpcData.npcInfoData.DEFA;
		objInitNpcData.DGEA = objInitNpcData.npcInfoData.DGEA;
		objInitNpcData.HITA = objInitNpcData.npcInfoData.HITA;
		objInitNpcData.RESA = objInitNpcData.npcInfoData.RESA;
		objInitNpcData.CRIA = objInitNpcData.npcInfoData.CRIA;
		objInitNpcData.Level = objInitNpcData.npcInfoData.Lv;
		objInitNpcData.AntiStun = objInitNpcData.npcInfoData.AntiStun;
		objInitNpcData.AntiKnockDown = objInitNpcData.npcInfoData.AntiKnockDown;
		objInitNpcData.PathID = curData.Data.PathId;
		return Singleton<ObjManager>.Instance.GetRagdollNPC(objInitNpcData, null);
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x0006B700 File Offset: 0x00069900
	public void OnRecycleNpc(ObjRagdollNPC npc)
	{
		if (this.mStaticNpcServerIdDic.ContainsKey(npc.ServerId))
		{
			this.mStaticNpcServerIdDic[npc.ServerId].ServerId = -1L;
			this.mStaticNpcServerIdDic.Remove(npc.ServerId);
		}
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x0006B750 File Offset: 0x00069950
	public void OnRecycleZombiePlayer(ObjZombieRagdollPlayer zombiePlayer)
	{
		if (this.mStaticNpcServerIdDic.ContainsKey(zombiePlayer.ServerId))
		{
			this.mStaticNpcServerIdDic[zombiePlayer.ServerId].ServerId = -1L;
			this.mStaticNpcServerIdDic.Remove(zombiePlayer.ServerId);
		}
		if (this.mEnableObjZombiePlayerList.Contains(zombiePlayer))
		{
			this.mEnableObjZombiePlayerList.Remove(zombiePlayer);
		}
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x0006B7BC File Offset: 0x000699BC
	public void OnRecycleFakeAICar(ObjFakeAICar car)
	{
		if (this.mStaticCarDic.ContainsKey(car.PlayerCar.ServerId))
		{
			this.mStaticCarDic[car.PlayerCar.ServerId].ServerId = -1L;
			this.mStaticCarDic.Remove(car.PlayerCar.ServerId);
		}
		if (car.PlayerCar.ServerId == this.mSpecialCarServerId)
		{
			this.mSpecialCarServerId = -1L;
		}
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x0006B838 File Offset: 0x00069A38
	private List<BlockData> GetNearByBlockData(Vector3 pos, bool includeSelf)
	{
		List<BlockData> list = new List<BlockData>();
		BlockPos blockPos = this.GetBlockPos(pos.x, pos.z);
		int x = blockPos.x;
		int z = blockPos.z;
		if (x < 0 || x >= this.BlockData.Length || z < 0 || z >= this.BlockData[x].PointLine.Length)
		{
			return list;
		}
		if (includeSelf)
		{
			list.Add(this.BlockData[x].PointLine[z]);
		}
		int num;
		if ((int)(pos.x - this.CityLeftBottomPos.x) % this.BlockLength > this.BlockLength / 2)
		{
			num = 1;
		}
		else
		{
			num = -1;
		}
		int num2;
		if ((int)(pos.z - this.CityLeftBottomPos.y) % this.BlockLength > this.BlockLength / 2)
		{
			num2 = 1;
		}
		else
		{
			num2 = -1;
		}
		int num3 = x + num;
		int num4 = z + num2;
		if (num3 > 0 && num3 < this.BlockData.Length)
		{
			list.Add(this.BlockData[num3].PointLine[z]);
			if (num4 > 0 && num4 < this.BlockData[0].PointLine.Length)
			{
				list.Add(this.BlockData[num3].PointLine[num4]);
				list.Add(this.BlockData[x].PointLine[num4]);
			}
		}
		else if (num4 > 0 && num4 < this.BlockData[0].PointLine.Length)
		{
			list.Add(this.BlockData[x].PointLine[num4]);
		}
		return list;
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x0006B9E8 File Offset: 0x00069BE8
	private List<BlockData> GetNearByBlockDataBig(Vector3 pos, bool includeSelf)
	{
		List<BlockData> list = new List<BlockData>();
		BlockPos blockPos = this.GetBlockPos(pos.x, pos.z);
		int x2 = blockPos.x;
		int z = blockPos.z;
		if (x2 < 0 || x2 >= this.BlockData.Length || z < 0 || z >= this.BlockData[x2].PointLine.Length)
		{
			return list;
		}
		if (includeSelf)
		{
			list.Add(this.BlockData[x2].PointLine[z]);
		}
		List<KeyValuePair<int, int>> list2 = new List<KeyValuePair<int, int>>();
		for (int i = -2; i <= 2; i++)
		{
			for (int j = -2; j <= 2; j++)
			{
				list2.Add(new KeyValuePair<int, int>(i, j));
			}
		}
		for (int k = list2.Count - 1; k >= 0; k--)
		{
			if (list2[k].Key + x2 < 0 || list2[k].Key + x2 >= this.BlockData.Length || list2[k].Value + z < 0 || list2[k].Value + z >= this.BlockData[0].PointLine.Length)
			{
				list2.RemoveAt(k);
			}
		}
		list2.Sort((KeyValuePair<int, int> x, KeyValuePair<int, int> y) => x.Key * x.Key + x.Value * x.Value - y.Key * y.Key - y.Value * y.Value);
		for (int l = 0; l < list2.Count; l++)
		{
			list.Add(this.BlockData[x2 + list2[l].Key].PointLine[z + list2[l].Value]);
		}
		return list;
	}

	// Token: 0x17000391 RID: 913
	// (get) Token: 0x060010A6 RID: 4262 RVA: 0x0006BBE0 File Offset: 0x00069DE0
	// (set) Token: 0x060010A7 RID: 4263 RVA: 0x0006BBE8 File Offset: 0x00069DE8
	public int PoliceScores
	{
		get
		{
			return this.mPoliceScores;
		}
		set
		{
			this.mPoliceScores = value;
			if (this.mMiniMap == null && SingletonUnity<MiniMap>.Exists)
			{
				this.mMiniMap = SingletonUnity<MiniMap>.Instance;
			}
			if (this.mMiniMap != null)
			{
				float num;
				if (this.mPoliceScores < this.mPoliceLevelData.PoliceScoresList[0])
				{
					if (SingletonUnity<PoliceLevelRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PoliceLevelRootLogic>.Instance.gameObject))
					{
						num = 0.2f;
					}
					else
					{
						num = 0f;
					}
				}
				else
				{
					num = Mathf.Lerp(0.2f, 0.6f, (float)(this.mPoliceScores - this.mPoliceLevelData.PoliceScoresList[0]) / (float)(this.mPoliceLevelData.PoliceScoresList[this.mPoliceLevelData.PoliceScoresList.Count - 1] - this.mPoliceLevelData.PoliceScoresList[0]));
				}
				this.mMiniMap.SetPoliceAlph(num);
				if (num > 0f)
				{
					if (!this.mMiniMap.PoliceTweenColor.enabled)
					{
						this.mMiniMap.PoliceTweenColor.enabled = true;
					}
				}
				else if (this.mMiniMap.PoliceTweenColor.enabled)
				{
					this.mMiniMap.PoliceTweenColor.enabled = false;
					this.mMiniMap.PoliceTweenColor.ResetToBeginning();
				}
			}
		}
	}

	// Token: 0x17000392 RID: 914
	// (get) Token: 0x060010A8 RID: 4264 RVA: 0x0006BD60 File Offset: 0x00069F60
	public int PoliceLevel
	{
		get
		{
			for (int i = this.mPoliceLevelData.PoliceScoresList.Count - 1; i >= 0; i--)
			{
				if (this.PoliceScores >= this.mPoliceLevelData.PoliceScoresList[i])
				{
					return i;
				}
			}
			return -1;
		}
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x0006BDB0 File Offset: 0x00069FB0
	public void OnPlayerCarDie()
	{
		this.PoliceScores += 8;
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x0006BDC0 File Offset: 0x00069FC0
	public void OnNPCDie(ObjRagdollNPC npc)
	{
		if (this.mEnablePoliceDic.ContainsKey(npc.ServerId))
		{
			this.PoliceScores += 8;
			this.mEnablePoliceDic.Remove(npc.ServerId);
			this.mEnablePoliceList.Remove(npc);
		}
		else
		{
			MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			List<CurMission> curMissionList = missionManager.GetCurMissionList();
			for (int i = 0; i < curMissionList.Count; i++)
			{
				if (curMissionList[i].MissionState == MISSION_STATE.ACCEPTED)
				{
					MissionData missionDataByID = DataManager.GetMissionDataByID(curMissionList[i].MissionId);
					if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILLMONSTER_DROP || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.LOCAL_KILL_MONSTER_DROP || missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILL_TARGET_NPC)
					{
						if (missionDataByID.MissionLogicType == MISSION_LOGICTYPE.KILL_TARGET_NPC)
						{
							KillTargetMissionData killTargetMissionDataById = DataManager.GetKillTargetMissionDataById(missionDataByID.LogicID);
							if (killTargetMissionDataById.NpcID.Equals(npc.NPCDataID))
							{
								return;
							}
						}
						else if (missionDataByID.Target.Equals(npc.NPCDataID))
						{
							return;
						}
					}
				}
			}
			this.PoliceScores += 5;
		}
		if (this.PoliceScores > this.mPoliceLevelData.MaxScores)
		{
			this.PoliceScores = this.mPoliceLevelData.MaxScores;
		}
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x0006BF2C File Offset: 0x0006A12C
	public void PoliceUpdateCheck()
	{
		this.mPoliceCheckCount += Time.deltaTime;
		int curPoliceLevel = this.PoliceLevel;
		if (curPoliceLevel >= 0)
		{
			if (this.mPoliceCheckCount >= 1f)
			{
				this.mPoliceCheckCount -= 1f;
				this.mTargetPoliceFlashTime = this.mPoliceLevelData.PoliceFlashTimeList[curPoliceLevel];
				if (this.PoliceScores > 0 && !this.IsPoliceAround())
				{
					this.PoliceScores--;
				}
				if (SingletonUnity<PoliceLevelRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PoliceLevelRootLogic>.Instance.gameObject))
				{
					SingletonUnity<PoliceLevelRootLogic>.Instance.SetPoliceLevel(curPoliceLevel);
				}
				else
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PoliceLevelRoot, delegate
					{
						SingletonUnity<PoliceLevelRootLogic>.Instance.SetPoliceLevel(curPoliceLevel);
					}, null);
				}
			}
			this.mPoliceFlashCount += Time.deltaTime;
			if (this.mPoliceFlashCount > this.mTargetPoliceFlashTime)
			{
				this.mPoliceFlashCount = 0f;
				this.FlashPoliceNow(curPoliceLevel);
			}
		}
		else
		{
			this.mScoresTimeCount += Time.deltaTime;
			if (this.mScoresTimeCount > 1f)
			{
				this.mScoresTimeCount -= 1f;
				if (this.PoliceScores > 0 && !this.IsPoliceAround())
				{
					this.PoliceScores--;
				}
				if (this.PoliceScores < 5 && SingletonUnity<PoliceLevelRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PoliceLevelRootLogic>.Instance.gameObject))
				{
					SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PoliceLevelRoot);
					this.PoliceScores = this.PoliceScores;
					for (int i = 0; i < this.mEnablePoliceList.Count; i++)
					{
						this.mEnablePoliceList[i].ChangeNpcAI("1000");
					}
				}
			}
		}
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x0006C12C File Offset: 0x0006A32C
	public bool IsPoliceAround()
	{
		for (int i = 0; i < this.mEnablePoliceList.Count; i++)
		{
			if (Vector3.Distance(this.mEnablePoliceList[i].Position, this.mMainPlayer.Position) < this.PoliceCheckDis)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x0006C184 File Offset: 0x0006A384
	public void FlashPoliceNow(int level)
	{
		int num = this.mPoliceLevelData.PoliceNumList[level] - this.GetPoliceNum();
		List<string> list = this.mPoliceLevelData.PoliceIdList[level];
		this.mPoliceCreateList.Clear();
		for (int i = 0; i < num; i++)
		{
			this.mPoliceCreateList.Add(list[Random.Range(0, list.Count)]);
		}
		this.CreateNewNpc(this.mMainPlayerTrans, this.NpcCreateDis, true);
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x0006C208 File Offset: 0x0006A408
	public void UpdateSpecialCarState()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (!string.IsNullOrEmpty(playerData.MountId))
		{
			this.mIsHaveSpecialCar = true;
			MountData mountDataById = DataManager.GetMountDataById(playerData.MountId);
			this.mSpecialCarId = mountDataById.GTALinkCarId;
		}
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x0006C250 File Offset: 0x0006A450
	public void OnDownloadFlashNPC()
	{
		ObjManager instance = Singleton<ObjManager>.Instance;
		for (int i = this.mEnableRagdollList.Count - 1; i >= 0; i--)
		{
			if (this.mEnableRagdollList[i].MeshRoot == null)
			{
				instance.RecycleRagdollNPC(this.mEnableRagdollList[i]);
			}
		}
		if (this.mEnableRagdollList.Count < this.NpcNumCount)
		{
			this.CreateNewNpc(this.mMainPlayerTrans, this.NpcCreateDis, false);
			this.preCreateNpcPos = this.mMainPlayerTrans.position;
		}
		this.NormalNpcCreateCheck(this.mMainPlayerTrans.position);
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x0006C2FC File Offset: 0x0006A4FC
	public void ClearMapLine()
	{
		if (this.mCurPlayerLocationData != null || this.mCurTargetLocationData != null || this.mCurPathList.Count > 0 || this.isDrawWalkLine)
		{
			this.mCurPlayerLocationData = null;
			this.mCurTargetLocationData = null;
			this.mCurPathList.Clear();
			this.isDrawWalkLine = false;
			if (SingletonUnity<NewMapUIRootLogic>.Exists)
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.ClearMapLine();
			}
			if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
			{
				SingletonUnity<MiniMap>.Instance.ClearMapLine();
			}
		}
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x0006C398 File Offset: 0x0006A598
	public void UpdateMapLine(Vector3 sourcePos, Vector3 targetPos)
	{
		if (this.mMainPlayer == null || !this.mMainPlayer.IsLocalDrivingCar)
		{
			this.ClearMapLine();
			return;
		}
		if (Vector3.Distance(sourcePos, targetPos) < 70f)
		{
			if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.OnlyDrawWalkLine(sourcePos, targetPos);
			}
			if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
			{
				SingletonUnity<MiniMap>.Instance.OnlyDrawWalkLine(sourcePos, targetPos);
			}
			this.isDrawWalkLine = true;
			return;
		}
		LocationData playerLocationBig = this.GetPlayerLocationBig(sourcePos, 500f);
		if (playerLocationBig == null)
		{
			return;
		}
		LocationData playerLocationBig2 = this.GetPlayerLocationBig(targetPos, 500f);
		if (playerLocationBig2 == null)
		{
			return;
		}
		if (playerLocationBig == playerLocationBig2)
		{
			if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.OnlyDrawWalkLine(sourcePos, targetPos);
			}
			if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
			{
				SingletonUnity<MiniMap>.Instance.OnlyDrawWalkLine(sourcePos, targetPos);
			}
			this.isDrawWalkLine = true;
			return;
		}
		if (this.mCurPlayerLocationData == playerLocationBig && this.mCurTargetLocationData == playerLocationBig2)
		{
			if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.DrawMapLine(this.mCurPathList, sourcePos, targetPos);
			}
			if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
			{
				SingletonUnity<MiniMap>.Instance.DrawMapLine(this.mCurPathList, targetPos);
			}
			return;
		}
		this.mCurPlayerLocationData = playerLocationBig;
		this.mCurTargetLocationData = playerLocationBig2;
		this.mCurPathList.Clear();
		this.mCurPathList = this.GetPathList(playerLocationBig, playerLocationBig2);
		this.mCurPathList.Insert(0, sourcePos);
		this.mCurPathList.Add(targetPos);
		if (this.mCurPathList.Count == 0)
		{
			if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.OnlyDrawWalkLine(sourcePos, targetPos);
			}
			if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
			{
				SingletonUnity<MiniMap>.Instance.OnlyDrawWalkLine(sourcePos, targetPos);
			}
		}
		else
		{
			if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewMapUIRootLogic>.Instance.DrawMapLine(this.mCurPathList, sourcePos, targetPos);
			}
			if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
			{
				SingletonUnity<MiniMap>.Instance.DrawMapLine(this.mCurPathList, targetPos);
			}
		}
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x0006C648 File Offset: 0x0006A848
	private List<Vector3> GetPathList(LocationData sourceLoc, LocationData targetLoc)
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		List<CityPathPointData> list = new List<CityPathPointData>();
		list.Add(sourceLoc.point1);
		list.Add(sourceLoc.point2);
		dictionary.Add(sourceLoc.point1.SelfIndex, -1);
		dictionary.Add(sourceLoc.point2.SelfIndex, -1);
		bool flag = false;
		int num = -1;
		while (list.Count > 0)
		{
			CityPathPointData cityPathPointData = list[0];
			list.RemoveAt(0);
			for (int i = 0; i < cityPathPointData.LinkPointIndex.Length; i++)
			{
				if (cityPathPointData.LinkPointIndex[i] != -1)
				{
					if (!dictionary.ContainsKey(cityPathPointData.LinkPointIndex[i]))
					{
						dictionary.Add(cityPathPointData.LinkPointIndex[i], cityPathPointData.SelfIndex);
						if (cityPathPointData.LinkPointIndex[i] == targetLoc.point1.SelfIndex || cityPathPointData.LinkPointIndex[i] == targetLoc.point2.SelfIndex)
						{
							flag = true;
							num = cityPathPointData.LinkPointIndex[i];
							break;
						}
						list.Add(this.PointDataList[cityPathPointData.LinkPointIndex[i]]);
					}
				}
			}
			if (flag)
			{
				break;
			}
		}
		List<Vector3> list2 = new List<Vector3>();
		while (dictionary.ContainsKey(num) && dictionary[num] != -1)
		{
			list2.Insert(0, this.PointDataList[num].PointPos);
			num = dictionary[num];
		}
		return list2;
	}

	// Token: 0x040013E2 RID: 5090
	public BlockLine[] BlockData;

	// Token: 0x040013E3 RID: 5091
	public List<CityPathPointData> PointDataList;

	// Token: 0x040013E4 RID: 5092
	public Vector2 CityLeftBottomPos = new Vector2(-800f, -800f);

	// Token: 0x040013E5 RID: 5093
	public Vector2 CitySize = new Vector2(1600f, 1600f);

	// Token: 0x040013E6 RID: 5094
	public int BlockLength = 50;

	// Token: 0x040013E7 RID: 5095
	private int NpcNumCount = 10;

	// Token: 0x040013E8 RID: 5096
	private int CarNumCount = 5;

	// Token: 0x040013E9 RID: 5097
	public float NpcCreateDis = 40f;

	// Token: 0x040013EA RID: 5098
	public float CarCreateDis = 50f;

	// Token: 0x040013EB RID: 5099
	private float PoliceCheckDis = 10f;

	// Token: 0x040013EC RID: 5100
	private List<string> NpcIdList;

	// Token: 0x040013ED RID: 5101
	private List<string> PoliceIdList;

	// Token: 0x040013EE RID: 5102
	private float flashTimeCount;

	// Token: 0x040013EF RID: 5103
	private float recycleTimeCount;

	// Token: 0x040013F0 RID: 5104
	private float posTimeCount;

	// Token: 0x040013F1 RID: 5105
	private Transform mMainPlayerTrans;

	// Token: 0x040013F2 RID: 5106
	private ObjMainPlayer mMainPlayer;

	// Token: 0x040013F3 RID: 5107
	private List<ObjRagdollNPC> mEnableRagdollList;

	// Token: 0x040013F4 RID: 5108
	private List<ObjFakeAICar> mEnableFakeAICarList;

	// Token: 0x040013F5 RID: 5109
	private List<ObjZombieRagdollPlayer> mEnableObjZombiePlayerList;

	// Token: 0x040013F6 RID: 5110
	private ObjManager mObjManager;

	// Token: 0x040013F7 RID: 5111
	private float mFlashNpcSqrDis;

	// Token: 0x040013F8 RID: 5112
	private Vector3 preCreateNpcPos;

	// Token: 0x040013F9 RID: 5113
	public float mNpcRecycleDis;

	// Token: 0x040013FA RID: 5114
	public float mCarRecycleDis;

	// Token: 0x040013FB RID: 5115
	private Dictionary<long, ObjRagdollNPC> mEnablePoliceDic;

	// Token: 0x040013FC RID: 5116
	private List<ObjNPC> mEnablePoliceList;

	// Token: 0x040013FD RID: 5117
	private ObjFakeAICar mCurNearestCar;

	// Token: 0x040013FE RID: 5118
	private SmoothFollowNew mSmoothFollowCamPos;

	// Token: 0x040013FF RID: 5119
	private List<BlockData> mPreCheckBlockList;

	// Token: 0x04001400 RID: 5120
	private Dictionary<long, BlockCarData> mStaticCarDic;

	// Token: 0x04001401 RID: 5121
	private bool mIsHaveSpecialCar;

	// Token: 0x04001402 RID: 5122
	public Vector3 SpecialCarPos;

	// Token: 0x04001403 RID: 5123
	public Vector3 SpecialCarAngle;

	// Token: 0x04001404 RID: 5124
	private string mSpecialCarId;

	// Token: 0x04001405 RID: 5125
	private long mSpecialCarServerId;

	// Token: 0x04001406 RID: 5126
	private PoliceLevelData mPoliceLevelData;

	// Token: 0x04001407 RID: 5127
	private List<string> mBeforDownloadNpcModelIdList;

	// Token: 0x04001408 RID: 5128
	private List<string> mPoliceCreateList;

	// Token: 0x04001409 RID: 5129
	private List<LocationData> mPreNpcLocationData;

	// Token: 0x0400140A RID: 5130
	private string[] FakeAICarName;

	// Token: 0x0400140B RID: 5131
	private string[] BeforeDownloadCarId;

	// Token: 0x0400140C RID: 5132
	private List<string> BeforeDownloadStaticCar;

	// Token: 0x0400140D RID: 5133
	private float carDis1;

	// Token: 0x0400140E RID: 5134
	private float carDis2;

	// Token: 0x0400140F RID: 5135
	private float FourWayCarDis1;

	// Token: 0x04001410 RID: 5136
	private float FourWayCarDis2;

	// Token: 0x04001411 RID: 5137
	private List<LocationData> mPreCarLocationData;

	// Token: 0x04001412 RID: 5138
	public static ROAD_STATE CurRoadState;

	// Token: 0x04001413 RID: 5139
	private float mRoadStateTimeCount;

	// Token: 0x04001414 RID: 5140
	private float mRoadStateChangeTime;

	// Token: 0x04001415 RID: 5141
	private int checkIndex;

	// Token: 0x04001416 RID: 5142
	private ObjPlayerCar mPlayerCar;

	// Token: 0x04001417 RID: 5143
	public vp_Timer.Handle getOnCarHandle;

	// Token: 0x04001418 RID: 5144
	public vp_Timer.Handle getOnCarAnimaHandle;

	// Token: 0x04001419 RID: 5145
	public FakeObjLogic RobNpcFakeObj;

	// Token: 0x0400141A RID: 5146
	private List<MonsterData> mCurMonsterDataList;

	// Token: 0x0400141B RID: 5147
	private Dictionary<long, BlockMonsterData> mStaticNpcServerIdDic;

	// Token: 0x0400141C RID: 5148
	private bool initFlag;

	// Token: 0x0400141D RID: 5149
	private int mPoliceScores;

	// Token: 0x0400141E RID: 5150
	private MiniMap mMiniMap;

	// Token: 0x0400141F RID: 5151
	private float mPoliceCheckCount;

	// Token: 0x04001420 RID: 5152
	private float mPoliceFlashCount;

	// Token: 0x04001421 RID: 5153
	private float mTargetPoliceFlashTime;

	// Token: 0x04001422 RID: 5154
	private float mScoresTimeCount;

	// Token: 0x04001423 RID: 5155
	private LocationData mCurPlayerLocationData;

	// Token: 0x04001424 RID: 5156
	private LocationData mCurTargetLocationData;

	// Token: 0x04001425 RID: 5157
	private List<Vector3> mCurPathList;

	// Token: 0x04001426 RID: 5158
	private bool isDrawWalkLine;
}
