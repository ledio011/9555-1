using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200087D RID: 2173
public class LowPhoneSceneManager : SceneManager
{
	// Token: 0x060039D2 RID: 14802 RVA: 0x000F7E68 File Offset: 0x000F6068
	public override void Init(string id)
	{
		base.Init(id);
		this.mWaveCount = base.GetMonsterGroupCount();
		this.mCurPlayerPathIndex = 1;
		this.mCurEnemyGroup = PlayerPrefs.GetInt("mCurEnemyGroup", 1);
		if (!SingletonUnity<MyEvent>.Exists)
		{
			Debug.Log("SingletonUnity<MyEvent>.Exists == false");
		}
		Debug.Log("hahahaha");
		this.OnMainPlayerCreate();
		this.CreateNPCGroup();
	}

	// Token: 0x060039D3 RID: 14803 RVA: 0x000F7ED0 File Offset: 0x000F60D0
	private void InitBlock(string sceneId)
	{
	}

	// Token: 0x060039D4 RID: 14804 RVA: 0x000F7ED4 File Offset: 0x000F60D4
	public override void OnLoadingOver()
	{
		base.OnLoadingOver();
	}

	// Token: 0x060039D5 RID: 14805 RVA: 0x000F7EDC File Offset: 0x000F60DC
	public override void StartGame()
	{
		Debug.Log("CarChaseSceneManager StartGame");
	}

	// Token: 0x060039D6 RID: 14806 RVA: 0x000F7EE8 File Offset: 0x000F60E8
	public override void Update()
	{
	}

	// Token: 0x060039D7 RID: 14807 RVA: 0x000F7EEC File Offset: 0x000F60EC
	public void OnMainPlayerCreate()
	{
		ObjInitPlayerData objInitPlayerData = new ObjInitPlayerData();
		objInitPlayerData.Profession = PROFESSION_TYPE.XD;
		attribute attribute = new attribute();
		attribute.anti_knock_down = 100L;
		attribute.anti_stun = 100L;
		attribute.atk = 76L;
		attribute.crd = 15000L;
		attribute.cri = 8L;
		attribute.crr = 0L;
		attribute.def = 15L;
		attribute.eva = 8L;
		attribute.exd = 0L;
		attribute.exp = 0L;
		attribute.exr = 8L;
		attribute.hit = 8L;
		attribute.max_hp = 491L;
		attribute.mov = 500L;
		attribute.rec = 1L;
		attribute.res = 0L;
		objInitPlayerData.Attribute = attribute;
		objInitPlayerData.Name = "Player";
		objInitPlayerData.HP = 491;
		attribute attribute2 = new attribute();
		attribute2.anti_knock_down = 0L;
		attribute2.anti_stun = 0L;
		attribute2.atk = 0L;
		attribute2.crd = 0L;
		attribute2.cri = 8L;
		attribute2.crr = 0L;
		attribute2.def = 0L;
		attribute2.eva = 0L;
		attribute2.exd = 0L;
		attribute2.exp = 0L;
		attribute2.exr = 0L;
		attribute2.hit = 0L;
		attribute2.max_hp = 0L;
		attribute2.mov = 0L;
		attribute2.rec = 1L;
		attribute2.res = 0L;
		objInitPlayerData.AttributeAll = attribute;
		attribute_other attribute_other = new attribute_other();
		attribute_other.camp = 0L;
		attribute_other.combValue = 3317L;
		attribute_other.dance_state = 0L;
		attribute_other.exp = 0L;
		attribute_other.hp = 491L;
		attribute_other.level = 1L;
		attribute_other.pkMode = 0L;
		objInitPlayerData.Level = 30;
		objInitPlayerData.ComboValue = 3317;
		objInitPlayerData.mPos = new Vector3(PlayerPrefs.GetFloat("pos_x", -24f), SceneManager.GetHitHeight(0f, 0f), PlayerPrefs.GetFloat("pos_z", 159f));
		objInitPlayerData.mCharacterModelId = "100";
		objInitPlayerData.visual = new characterVisual
		{
			BodyId = "XD_A_S",
			HeadId = "XD_A_T",
			LegId = "XD_A_X",
			WeaponId = "XD_A_WQ",
			showType = 1L,
			ModeId = "100"
		};
		Dictionary<string, skill_info> dictionary = new Dictionary<string, skill_info>();
		skill_info skill_info = new skill_info();
		skill_info.indexPos = 0L;
		skill_info.indexPos2 = 0L;
		skill_info.skillId = "101";
		skill_info.skillLevel = 0L;
		dictionary.Add(skill_info.skillId, skill_info);
		skill_info = new skill_info();
		skill_info.indexPos = 1L;
		skill_info.indexPos2 = 1L;
		skill_info.skillId = "102";
		skill_info.skillLevel = 0L;
		dictionary.Add(skill_info.skillId, skill_info);
		skill_info = new skill_info();
		skill_info.indexPos = 2L;
		skill_info.indexPos2 = 2L;
		skill_info.skillId = "103";
		skill_info.skillLevel = 0L;
		dictionary.Add(skill_info.skillId, skill_info);
		skill_info = new skill_info();
		skill_info.indexPos = 3L;
		skill_info.indexPos2 = 3L;
		skill_info.skillId = "104";
		skill_info.skillLevel = 0L;
		dictionary.Add(skill_info.skillId, skill_info);
		skill_info = new skill_info();
		skill_info.indexPos = 4L;
		skill_info.indexPos2 = 4L;
		skill_info.skillId = "105";
		skill_info.skillLevel = 0L;
		dictionary.Add(skill_info.skillId, skill_info);
		skill_info = new skill_info();
		skill_info.indexPos = 5L;
		skill_info.indexPos2 = 5L;
		skill_info.skillId = "106";
		skill_info.skillLevel = 0L;
		dictionary.Add(skill_info.skillId, skill_info);
		skill_info = new skill_info();
		skill_info.indexPos = 6L;
		skill_info.indexPos2 = 6L;
		skill_info.skillId = "107";
		skill_info.skillLevel = 0L;
		dictionary.Add(skill_info.skillId, skill_info);
		skill_info = new skill_info();
		skill_info.indexPos = 7L;
		skill_info.indexPos2 = 7L;
		skill_info.skillId = "108";
		skill_info.skillLevel = 0L;
		dictionary.Add(skill_info.skillId, skill_info);
		skill_info = new skill_info();
		skill_info.indexPos = 8L;
		skill_info.indexPos2 = 8L;
		skill_info.skillId = "109";
		skill_info.skillLevel = 0L;
		dictionary.Add(skill_info.skillId, skill_info);
		skill_info = new skill_info();
		skill_info.indexPos = 9L;
		skill_info.indexPos2 = 9L;
		skill_info.skillId = "110";
		skill_info.skillLevel = 0L;
		dictionary.Add(skill_info.skillId, skill_info);
		objInitPlayerData.skills = dictionary;
		Singleton<ObjManager>.Instance.CreateMainPlayer(objInitPlayerData);
	}

	// Token: 0x060039D8 RID: 14808 RVA: 0x000F83C0 File Offset: 0x000F65C0
	public void MoveNextState()
	{
		this.CreateNPCGroup();
	}

	// Token: 0x060039D9 RID: 14809 RVA: 0x000F83C8 File Offset: 0x000F65C8
	private void CreateNPCGroup()
	{
		this.NpcCreate(base.GetMonsterDataByGroup(this.mCurEnemyGroup));
	}

	// Token: 0x060039DA RID: 14810 RVA: 0x000F83DC File Offset: 0x000F65DC
	private void NpcCreate(List<MonsterData> list)
	{
		if (list == null || list.Count == 0)
		{
			return;
		}
		for (int i = 0; i < list.Count; i++)
		{
			ObjInitNpcData objInitNpcData = new ObjInitNpcData();
			MonsterData monsterData = list[i];
			objInitNpcData.mServerID = UUID.GenUUID();
			objInitNpcData.mPos = new Vector3(monsterData.PositionX, 0f, monsterData.PositionZ);
			objInitNpcData.mDir = MathUtil.HeadingToVector3((float)monsterData.PosO / 100f);
			NpcData npcDataByID = DataManager.GetNpcDataByID(monsterData.NpcID);
			objInitNpcData.npcInfoData = npcDataByID;
			objInitNpcData.HP = npcDataByID.Hp;
			objInitNpcData.MaxHP = npcDataByID.Hp;
			if (npcDataByID.AI.Equals("BlockAI"))
			{
				Singleton<ObjManager>.Instance.CreateNPC(objInitNpcData, null, null);
			}
			else
			{
				Singleton<ObjManager>.Instance.CreateNPC(objInitNpcData, new ObjManager.OnGetNPC(this.OnNPCCreated), null);
			}
		}
	}

	// Token: 0x060039DB RID: 14811 RVA: 0x000F84CC File Offset: 0x000F66CC
	public void SaveData()
	{
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			PlayerPrefs.SetFloat("pos_x", mainPlayer.Position.x);
			PlayerPrefs.SetFloat("pos_z", mainPlayer.Position.z);
		}
	}

	// Token: 0x060039DC RID: 14812 RVA: 0x000F8520 File Offset: 0x000F6720
	private void OnNPCCreated(ObjNPC npc)
	{
	}

	// Token: 0x060039DD RID: 14813 RVA: 0x000F8524 File Offset: 0x000F6724
	public override void OnNPCDie(object objNpc)
	{
		ObjNPC objNPC = objNpc as ObjNPC;
		this.npcList.Add(objNPC);
		if (Singleton<ObjManager>.Instance.CheckNPCClear(objNPC))
		{
			this.mCurEnemyGroup++;
			if (this.mCurEnemyGroup > this.mWaveCount)
			{
				this.mCurEnemyGroup = 1;
			}
			PlayerPrefs.SetInt("mCurEnemyGroup", this.mCurEnemyGroup);
			this.MoveNextState();
			vp_Timer.In(2f, delegate()
			{
				for (int i = 0; i < this.npcList.Count; i++)
				{
					this.npcList[i].DelayRecycle();
				}
				this.npcList.Clear();
			}, null);
		}
	}

	// Token: 0x040025E0 RID: 9696
	private SmoothFollowNew mSmoothFollowCamPos;

	// Token: 0x040025E1 RID: 9697
	private Vector3 mMainPlayerRunTargetPos;

	// Token: 0x040025E2 RID: 9698
	private Vector3 mPlayerCarCreatePos;

	// Token: 0x040025E3 RID: 9699
	private Vector3 mPlayerCarCreateAngle;

	// Token: 0x040025E4 RID: 9700
	private ObjMainPlayer mMainPlayer;

	// Token: 0x040025E5 RID: 9701
	private ObjPlayerCar mPlayerCar;

	// Token: 0x040025E6 RID: 9702
	private MovePathPoint mMovePathPoint;

	// Token: 0x040025E7 RID: 9703
	private List<Vector3> mPlayerCarPathList = new List<Vector3>();

	// Token: 0x040025E8 RID: 9704
	private int mCurPlayerCarPathIndex;

	// Token: 0x040025E9 RID: 9705
	private CopySceneData mCurCopySceneData;

	// Token: 0x040025EA RID: 9706
	private CarHPRootLogic carHPRoot;

	// Token: 0x040025EB RID: 9707
	private Transform mPlayerPathPointRoot;

	// Token: 0x040025EC RID: 9708
	private int mWaveCount;

	// Token: 0x040025ED RID: 9709
	private int mStepIndex;

	// Token: 0x040025EE RID: 9710
	private int mCurEnemyGroup;

	// Token: 0x040025EF RID: 9711
	private int mCurPlayerPathIndex;

	// Token: 0x040025F0 RID: 9712
	private GameObject mRandomWalkPosRoot;

	// Token: 0x040025F1 RID: 9713
	private List<Vector3> mRandomWalkPosList;

	// Token: 0x040025F2 RID: 9714
	private string mStartSceneAnima = "Tutorial/kaiChangSceneAnima";

	// Token: 0x040025F3 RID: 9715
	public Transform TalkNpcPos;

	// Token: 0x040025F4 RID: 9716
	private ObjNPC mTalkingNpc;

	// Token: 0x040025F5 RID: 9717
	private Transform TutorialCreateNPCCamLookPos;

	// Token: 0x040025F6 RID: 9718
	private CameraController mCamCtl;

	// Token: 0x040025F7 RID: 9719
	private GameObject mCarAnimaObj;

	// Token: 0x040025F8 RID: 9720
	private List<ObjNPC> npcList = new List<ObjNPC>();
}
