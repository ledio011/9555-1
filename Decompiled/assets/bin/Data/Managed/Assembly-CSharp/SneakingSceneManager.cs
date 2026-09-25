using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000889 RID: 2185
public class SneakingSceneManager : SceneManager
{
	// Token: 0x06003AE8 RID: 15080 RVA: 0x00100094 File Offset: 0x000FE294
	public override void Init(string id)
	{
		base.Init(id);
		this.mCurCopySceneData = DataManager.GetCopySceneDataById(this.mapInfoData.ID);
		if (this.mCurCopySceneData != null)
		{
			this.MISSION_TIME = (float)this.mCurCopySceneData.ExistTime;
			this.mMissionTimeCount = this.MISSION_TIME;
		}
		this.mStartTimeCountFlag = false;
		this.mEndMissionFlag = false;
		GameObject gameObject = ResourcesManager.LoadAndInstantiate("Items/MovePathPoint") as GameObject;
		this.mMovePathPoint = gameObject.GetComponent<MovePathPoint>();
		this.mMovePathPoint.RegisterOnArrivePathPoint(new MovePathPoint.OnArrivePointDelegate(this.OnArrivePathPoint));
		this.mMovePathPoint.ActiveRadius = 1f;
		SingletonUnity<MyEvent>.Instance.Register("OnMainPlayerCreate", this, "OnMainPlayerCreate");
		this.InitBlock(id);
		this.mFindPlayerFlag = false;
		this.mDoorObj = GameObject.Find("Door_50");
	}

	// Token: 0x06003AE9 RID: 15081 RVA: 0x0010016C File Offset: 0x000FE36C
	private void InitBlock(string mapId)
	{
		List<SneakingMissionData> sneakingMissionDataListByMapId = DataManager.GetSneakingMissionDataListByMapId(mapId);
		for (int i = 0; i < sneakingMissionDataListByMapId.Count; i++)
		{
			if (sneakingMissionDataListByMapId[i].PointType == 1)
			{
				if (!this.mNPCPathDic.ContainsKey(sneakingMissionDataListByMapId[i].PathGroup))
				{
					this.mNPCPathDic.Add(sneakingMissionDataListByMapId[i].PathGroup, new List<Vector3>());
					this.mNPCDic.Add(sneakingMissionDataListByMapId[i].PathGroup, sneakingMissionDataListByMapId[i].NPCID);
				}
				List<Vector3> list = this.mNPCPathDic[sneakingMissionDataListByMapId[i].PathGroup];
				list.Add(new Vector3(sneakingMissionDataListByMapId[i].PosX, sneakingMissionDataListByMapId[i].PosY, sneakingMissionDataListByMapId[i].PosZ));
			}
			else if (sneakingMissionDataListByMapId[i].PointType == 0)
			{
				this.mMovePathPoint.transform.position = new Vector3(sneakingMissionDataListByMapId[i].PosX, sneakingMissionDataListByMapId[i].PosY, sneakingMissionDataListByMapId[i].PosZ);
			}
			else if (sneakingMissionDataListByMapId[i].PointType == 2)
			{
				this.BossPoint = new Vector3(sneakingMissionDataListByMapId[i].PosX, sneakingMissionDataListByMapId[i].PosY, sneakingMissionDataListByMapId[i].PosZ);
			}
		}
		foreach (int num in this.mNPCPathDic.Keys)
		{
			NpcData npcDataByID = DataManager.GetNpcDataByID(this.mNPCDic[num]);
			List<Vector3> path = this.mNPCPathDic[num];
			ObjInitNpcData objInitNpcData = new ObjInitNpcData();
			objInitNpcData.mServerID = UUID.GenUUID();
			objInitNpcData.mPos = path[0];
			objInitNpcData.mDir = (path[1] - path[0]).normalized;
			objInitNpcData.HP = npcDataByID.Hp;
			objInitNpcData.MaxHP = npcDataByID.Hp;
			objInitNpcData.npcInfoData = npcDataByID;
			objInitNpcData.mCharacterModelId = npcDataByID.Model;
			Singleton<ObjManager>.Instance.GetPatrolNPC(objInitNpcData, delegate(ObjNPC npc)
			{
				if (npc != null)
				{
					ObjPatrolNPC objPatrolNPC = npc as ObjPatrolNPC;
					if (objPatrolNPC != null)
					{
						objPatrolNPC.SetPathPoint(path);
					}
					this.mPatrolNPCList.Add(objPatrolNPC);
				}
			});
		}
	}

	// Token: 0x06003AEA RID: 15082 RVA: 0x00100414 File Offset: 0x000FE614
	public override void StartGame()
	{
		Debug.Log("CarChaseSceneManager StartGame");
	}

	// Token: 0x06003AEB RID: 15083 RVA: 0x00100420 File Offset: 0x000FE620
	private void OnArrivePathPoint(Vector3 pos)
	{
		if (this.mFindPlayerFlag)
		{
			UnityVersionUtil.SetActiveRecursive(this.mMovePathPoint.gameObject, true);
			return;
		}
		this.OpenDoor();
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CarTargetRoot);
	}

	// Token: 0x06003AEC RID: 15084 RVA: 0x00100460 File Offset: 0x000FE660
	public override void SuccessMission()
	{
		base.SuccessMission();
		this.EndMission(true);
	}

	// Token: 0x06003AED RID: 15085 RVA: 0x00100470 File Offset: 0x000FE670
	public override void FailMission()
	{
		base.FailMission();
		this.EndMission(false);
	}

	// Token: 0x06003AEE RID: 15086 RVA: 0x00100480 File Offset: 0x000FE680
	public override void Update()
	{
	}

	// Token: 0x06003AEF RID: 15087 RVA: 0x00100484 File Offset: 0x000FE684
	public void OnMainPlayerCreate()
	{
		SingletonUnity<MyEvent>.Instance.DeRegister("OnMainPlayerCreate", this, "OnMainPlayerCreate");
		ObjManager instance = Singleton<ObjManager>.Instance;
		this.mMainPlayer = instance.MainPlayer;
		SingletonUnity<UIManager>.Instance.ShowDefaultUI();
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.JueseJiNengQuUI);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CarTargetRoot, delegate
		{
			SingletonUnity<CarTargetUIRootLogic>.Instance.Reset(this.mMovePathPoint.gameObject, this.mMainPlayer.gameObject);
		}, null);
	}

	// Token: 0x06003AF0 RID: 15088 RVA: 0x001004F0 File Offset: 0x000FE6F0
	private void EndMission(bool isSuccess)
	{
		if (this.mEndMissionFlag)
		{
			return;
		}
		this.mEndMissionFlag = true;
	}

	// Token: 0x06003AF1 RID: 15089 RVA: 0x00100508 File Offset: 0x000FE708
	public override void OnFindPlayer()
	{
		if (!this.mFindPlayerFlag)
		{
			this.mFindPlayerFlag = true;
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BlackScreenRoot, delegate
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.WaringUIRoot, delegate
				{
					SingletonUnity<WaringUIRoot>.Instance.Reset(StrDictionary.GetDictionaryString("#{101545}", new object[0]), 2f);
				}, null);
				SingletonUnity<BlackScreenLogic>.Instance.CloseScreen(0.5f, null);
			}, null);
			vp_Timer.In(1f, new vp_Timer.Callback(this.ResetToBeginPos), null);
		}
	}

	// Token: 0x06003AF2 RID: 15090 RVA: 0x0010056C File Offset: 0x000FE76C
	public void ResetToBeginPos()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.mMainPlayer.DisableNavMeshAgent();
		this.mMainPlayer.CacheTransform.position = playerData.MainPlayerStartPos;
		this.mMainPlayer.FaceToPub(playerData.MainPlayerStartDir);
		this.mMainPlayer.EnableNavMeshAgent();
		SingletonUnity<BlackScreenLogic>.Instance.OpenScreen(0.5f, delegate
		{
			this.mFindPlayerFlag = false;
		});
	}

	// Token: 0x06003AF3 RID: 15091 RVA: 0x001005DC File Offset: 0x000FE7DC
	public void OpenDoor()
	{
		if (this.mFindPlayerFlag)
		{
			return;
		}
		this.ClearPatrolNPC();
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.YiDongKongZhiUI);
		SingletonUnity<UIManager>.Instance.HideBaseUI();
		TweenPosition twDoor = null;
		if (this.mDoorObj != null)
		{
			twDoor = this.mDoorObj.GetComponent<TweenPosition>();
		}
		if (twDoor != null)
		{
			twDoor.PlayForward();
		}
		if (SingletonUnity<JoyStickLogic>.Exists)
		{
			SingletonUnity<JoyStickLogic>.Instance.MoveOutScreen();
		}
		this.mMainPlayer.StopMove();
		Vector3 targetPoint = new Vector3(2f, 0f, -7f);
		if (GameManager.IsSupportCurDataVersion145())
		{
			targetPoint = this.BossPoint;
		}
		vp_Timer.In(0.5f, delegate()
		{
			Singleton<ObjManager>.Instance.MainPlayer.NavMeshAgent.walkableMask += 8;
			this.mMainPlayer.MoveTo(targetPoint, 0.5f, delegate(ObjCharacter A_1)
			{
				SingletonUnity<UIManager>.Instance.ReShowBaseUI();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.YiDongKongZhiUI, null, null);
				if (twDoor != null)
				{
					twDoor.PlayReverse();
				}
				Singleton<ObjManager>.Instance.MainPlayer.NavMeshAgent.walkableMask -= 8;
				NetLogic.GetInstance().Send<Protocol.start_battle>(null, null);
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.JueseJiNengQuUI, null, null);
			});
		}, null);
	}

	// Token: 0x06003AF4 RID: 15092 RVA: 0x001006CC File Offset: 0x000FE8CC
	public void ClearPatrolNPC()
	{
		for (int i = this.mPatrolNPCList.Count - 1; i >= 0; i--)
		{
			UnityVersionUtil.SetActiveRecursive(this.mPatrolNPCList[i].gameObject, false);
			Singleton<ObjManager>.Instance.RecyclePatrolNPC(this.mPatrolNPCList[i]);
			this.mPatrolNPCList.RemoveAt(i);
		}
	}

	// Token: 0x06003AF5 RID: 15093 RVA: 0x00100730 File Offset: 0x000FE930
	public override void OnNPCDie(object objNpc)
	{
	}

	// Token: 0x0400268A RID: 9866
	private MovePathPoint mMovePathPoint;

	// Token: 0x0400268B RID: 9867
	private ObjMainPlayer mMainPlayer;

	// Token: 0x0400268C RID: 9868
	private float MISSION_TIME = 180f;

	// Token: 0x0400268D RID: 9869
	private float mMissionTimeCount;

	// Token: 0x0400268E RID: 9870
	private CopySceneData mCurCopySceneData;

	// Token: 0x0400268F RID: 9871
	private bool mStartTimeCountFlag;

	// Token: 0x04002690 RID: 9872
	private bool mFindPlayerFlag;

	// Token: 0x04002691 RID: 9873
	private GameObject mDoorObj;

	// Token: 0x04002692 RID: 9874
	private List<ObjPatrolNPC> mPatrolNPCList = new List<ObjPatrolNPC>();

	// Token: 0x04002693 RID: 9875
	private Vector3 BossPoint = new Vector3(2f, 0f, -7f);

	// Token: 0x04002694 RID: 9876
	private Dictionary<int, List<Vector3>> mNPCPathDic = new Dictionary<int, List<Vector3>>();

	// Token: 0x04002695 RID: 9877
	private Dictionary<int, string> mNPCDic = new Dictionary<int, string>();

	// Token: 0x04002696 RID: 9878
	private bool LerpToCarFlag;

	// Token: 0x04002697 RID: 9879
	private Vector3 startPosition;

	// Token: 0x04002698 RID: 9880
	private Quaternion startRotation;

	// Token: 0x04002699 RID: 9881
	private float lerpTime = 1f;

	// Token: 0x0400269A RID: 9882
	private float lerpCountTime;

	// Token: 0x0400269B RID: 9883
	private bool mEndMissionFlag;
}
