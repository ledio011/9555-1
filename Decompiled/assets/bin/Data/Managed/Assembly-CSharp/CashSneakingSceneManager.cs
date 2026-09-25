using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000875 RID: 2165
public class CashSneakingSceneManager : SceneManager
{
	// Token: 0x06003989 RID: 14729 RVA: 0x000F68B0 File Offset: 0x000F4AB0
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
		GameObject gameObject = GameObject.Find("MovePathPoint");
		if (gameObject == null)
		{
			gameObject = (ResourcesManager.LoadAndInstantiate("Items/MovePathPoint") as GameObject);
		}
		this.mMovePathPoint = gameObject.GetComponent<MovePathPoint>();
		this.mMovePathPoint.RegisterOnArrivePathPoint(new MovePathPoint.OnArrivePointDelegate(this.OnArrivePathPoint));
		this.mMovePathPoint.ActiveRadius = 1f;
		SingletonUnity<MyEvent>.Instance.Register("OnMainPlayerCreate", this, "OnMainPlayerCreate");
		this.InitBlock(id);
		this.mFindPlayerFlag = false;
		this.mDoorObj = GameObject.Find("Door_50");
		this.FinishSneakingFlag = false;
		this.passDoorFlag = false;
	}

	// Token: 0x0600398A RID: 14730 RVA: 0x000F69B0 File Offset: 0x000F4BB0
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

	// Token: 0x0600398B RID: 14731 RVA: 0x000F6C14 File Offset: 0x000F4E14
	public override void StartGame()
	{
	}

	// Token: 0x0600398C RID: 14732 RVA: 0x000F6C18 File Offset: 0x000F4E18
	private void OnArrivePathPoint(Vector3 pos)
	{
		if (this.mFindPlayerFlag)
		{
			UnityVersionUtil.SetActiveRecursive(this.mMovePathPoint.gameObject, true);
			return;
		}
		this.OpenDoor();
	}

	// Token: 0x0600398D RID: 14733 RVA: 0x000F6C40 File Offset: 0x000F4E40
	public override void SuccessMission()
	{
		base.SuccessMission();
		this.EndMission(true);
	}

	// Token: 0x0600398E RID: 14734 RVA: 0x000F6C50 File Offset: 0x000F4E50
	public override void FailMission()
	{
		base.FailMission();
		this.EndMission(false);
	}

	// Token: 0x0600398F RID: 14735 RVA: 0x000F6C60 File Offset: 0x000F4E60
	public void OnMainPlayerCreate()
	{
		SingletonUnity<MyEvent>.Instance.DeRegister("OnMainPlayerCreate", this, "OnMainPlayerCreate");
		ObjManager instance = Singleton<ObjManager>.Instance;
		this.mMainPlayer = instance.MainPlayer;
		SingletonUnity<UIManager>.Instance.ShowDefaultUI();
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.JueseJiNengQuUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PotionObjRoot);
	}

	// Token: 0x06003990 RID: 14736 RVA: 0x000F6CC0 File Offset: 0x000F4EC0
	private void EndMission(bool isSuccess)
	{
		if (this.mEndMissionFlag)
		{
			return;
		}
		this.mEndMissionFlag = true;
	}

	// Token: 0x06003991 RID: 14737 RVA: 0x000F6CD8 File Offset: 0x000F4ED8
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

	// Token: 0x06003992 RID: 14738 RVA: 0x000F6D3C File Offset: 0x000F4F3C
	public void ResetToBeginPos()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.mMainPlayer.DisableNavMeshAgent();
		this.mMainPlayer.CacheTransform.position = playerData.MainPlayerStartPos;
		this.mMainPlayer.FaceToPub(playerData.MainPlayerStartDir);
		this.mMainPlayer.EnableNavMeshAgent();
		this.mMainPlayer.StopMove();
		SingletonUnity<BlackScreenLogic>.Instance.OpenScreen(0.5f, delegate
		{
			this.mFindPlayerFlag = false;
		});
	}

	// Token: 0x06003993 RID: 14739 RVA: 0x000F6DB8 File Offset: 0x000F4FB8
	public void OpenDoor()
	{
		if (this.mFindPlayerFlag)
		{
			return;
		}
		this.FinishSneakingFlag = true;
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.YiDongKongZhiUI);
		SingletonUnity<UIManager>.Instance.HideBaseUI();
		TweenPosition twDoor = this.mDoorObj.GetComponent<TweenPosition>();
		if (twDoor != null)
		{
			twDoor.PlayForward();
		}
		this.ClearPatrolNPC();
		if (SingletonUnity<JoyStickLogic>.Exists)
		{
			SingletonUnity<JoyStickLogic>.Instance.MoveOutScreen();
		}
		this.mMainPlayer.StopMove();
		vp_Timer.In(0.5f, delegate()
		{
			Singleton<ObjManager>.Instance.MainPlayer.NavMeshAgent.walkableMask += 8;
			this.mMainPlayer.MoveTo(new Vector3(2f, 0f, -7f), 0.5f, delegate(ObjCharacter A_1)
			{
				this.OnPassDoor();
			});
		}, null);
		vp_Timer.In(1.5f, delegate()
		{
			twDoor.PlayReverse();
		}, null);
	}

	// Token: 0x06003994 RID: 14740 RVA: 0x000F6E80 File Offset: 0x000F5080
	private void OnPassDoor()
	{
		this.passDoorFlag = true;
		SingletonUnity<UIManager>.Instance.ReShowBaseUI();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.YiDongKongZhiUI, null, null);
		Singleton<ObjManager>.Instance.MainPlayer.NavMeshAgent.walkableMask -= 8;
		NetLogic.GetInstance().Send<Protocol.start_battle>(null, null);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.JueseJiNengQuUI, null, null);
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PotionObjRoot, delegate(bool bSuccess, object param)
			{
				SingletonUnity<PotionLogic>.Instance.Reset();
			}, null);
		}
	}

	// Token: 0x06003995 RID: 14741 RVA: 0x000F6F2C File Offset: 0x000F512C
	public void ClearPatrolNPC()
	{
		for (int i = this.mPatrolNPCList.Count - 1; i >= 0; i--)
		{
			this.mPatrolNPCList[i].RecycleSelf();
			this.mPatrolNPCList.RemoveAt(i);
		}
	}

	// Token: 0x06003996 RID: 14742 RVA: 0x000F6F74 File Offset: 0x000F5174
	public override void OnNPCDie(object objNpc)
	{
		ObjNPC objNPC = objNpc as ObjNPC;
		if (base.IsCopyScene())
		{
			single_copy_scene_npc_die.request request = new single_copy_scene_npc_die.request();
			request.characterId = objNPC.ServerId;
			request.type = (long)objNPC.NPCData.Type;
			request.npcdataid = objNPC.NPCDataID;
			request.pos_x = (long)Mathf.FloorToInt(objNPC.Position.x * 100f);
			request.pos_z = (long)Mathf.FloorToInt(objNPC.Position.z * 100f);
			NetLogic.GetInstance().Send<Protocol.single_copy_scene_npc_die>(request, null);
		}
		if (objNPC.NPCType == GameDefine.NPC_TYPE.BOSS)
		{
			return;
		}
	}

	// Token: 0x06003997 RID: 14743 RVA: 0x000F701C File Offset: 0x000F521C
	public override void OnReconnectSuccess()
	{
		base.OnReconnectSuccess();
		if (this.FinishSneakingFlag)
		{
			if (!this.passDoorFlag)
			{
				this.mMainPlayer.MoveTo(new Vector3(2f, 0f, -7f), 0.5f, delegate(ObjCharacter A_1)
				{
					this.OnPassDoor();
				});
			}
			else
			{
				NetLogic.GetInstance().Send<Protocol.start_battle>(null, null);
			}
		}
	}

	// Token: 0x040025B0 RID: 9648
	private MovePathPoint mMovePathPoint;

	// Token: 0x040025B1 RID: 9649
	private ObjMainPlayer mMainPlayer;

	// Token: 0x040025B2 RID: 9650
	private float MISSION_TIME = 180f;

	// Token: 0x040025B3 RID: 9651
	private float mMissionTimeCount;

	// Token: 0x040025B4 RID: 9652
	private CopySceneData mCurCopySceneData;

	// Token: 0x040025B5 RID: 9653
	private bool mStartTimeCountFlag;

	// Token: 0x040025B6 RID: 9654
	private Vector3 mPlayerStartPos;

	// Token: 0x040025B7 RID: 9655
	private float mPlayerStartDir;

	// Token: 0x040025B8 RID: 9656
	private bool mFindPlayerFlag;

	// Token: 0x040025B9 RID: 9657
	private GameObject mDoorObj;

	// Token: 0x040025BA RID: 9658
	private List<ObjPatrolNPC> mPatrolNPCList = new List<ObjPatrolNPC>();

	// Token: 0x040025BB RID: 9659
	private bool FinishSneakingFlag;

	// Token: 0x040025BC RID: 9660
	private bool passDoorFlag;

	// Token: 0x040025BD RID: 9661
	private Dictionary<int, List<Vector3>> mNPCPathDic = new Dictionary<int, List<Vector3>>();

	// Token: 0x040025BE RID: 9662
	private Dictionary<int, string> mNPCDic = new Dictionary<int, string>();

	// Token: 0x040025BF RID: 9663
	private bool mEndMissionFlag;
}
