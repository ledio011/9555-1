using System;
using UnityEngine;

// Token: 0x02000883 RID: 2179
public class SceneController : MonoBehaviour
{
	// Token: 0x06003A4C RID: 14924 RVA: 0x000FB8BC File Offset: 0x000F9ABC
	private void InitObj()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Exists)
		{
			ResourcesManager.LoadAndInstantiate("Manager/GameManager");
		}
		ResourcesManager.LoadAndInstantiate("FingerGestures/FingerGestures");
		base.gameObject.AddComponent<InputController>();
		base.gameObject.AddComponent<AIManager>();
		base.gameObject.AddComponent<MyEvent>();
		SingletonDontDestoryUnity<GameManager>.Instance.LoadScenneManager();
		this.UpdateCamera();
	}

	// Token: 0x06003A4D RID: 14925 RVA: 0x000FB920 File Offset: 0x000F9B20
	private void PlayBgmMusic()
	{
		MapInfoData currentMapInofData = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData;
		SoundData soundDataById = DataManager.GetSoundDataById(currentMapInofData.GetAudioID());
		if (soundDataById != null)
		{
			SingletonDontDestoryUnity<SoundManager>.Instance.PlayBGMusic(soundDataById.Id, soundDataById.FadeOutTime, soundDataById.FadeInTime);
		}
	}

	// Token: 0x06003A4E RID: 14926 RVA: 0x000FB96C File Offset: 0x000F9B6C
	private void UpdateCamera()
	{
	}

	// Token: 0x06003A4F RID: 14927 RVA: 0x000FB970 File Offset: 0x000F9B70
	private void InitUI()
	{
		ResourcesManager.LoadAndInstantiate("UIRoot/UIRoot");
	}

	// Token: 0x06003A50 RID: 14928 RVA: 0x000FB980 File Offset: 0x000F9B80
	private void InitSceneEffect()
	{
	}

	// Token: 0x06003A51 RID: 14929 RVA: 0x000FB984 File Offset: 0x000F9B84
	private void Awake()
	{
		this.InitUI();
		this.InitObj();
		NetLogic.GetInstance().CanProcessPack = true;
		this.InitSceneEffect();
		SingletonDontDestoryUnity<NetManager>.Instance.CheckConnectLost();
		this.PlayBgmMusic();
	}

	// Token: 0x06003A52 RID: 14930 RVA: 0x000FB9C0 File Offset: 0x000F9BC0
	private void OnClickExitGame()
	{
		SingletonDontDestoryUnity<NetManager>.Instance.LeaveGame();
		MessageBoxLogic.CloseBox();
		Application.Quit();
	}

	// Token: 0x06003A53 RID: 14931 RVA: 0x000FB9D8 File Offset: 0x000F9BD8
	private void OnClickNo()
	{
		MessageBoxLogic.CloseBox();
	}

	// Token: 0x06003A54 RID: 14932 RVA: 0x000FB9E0 File Offset: 0x000F9BE0
	private void ExitGame()
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager.IsBigWorld() || sceneManager.IsLowPhoneManager() || sceneManager.IsTutorialScene())
		{
			if (SingletonUnity<ExitGameRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ExitGameRoot>.Instance.gameObject))
			{
				SingletonUnity<ExitGameRoot>.Instance.OnClickNoBtn();
			}
			else
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExitGameRoot, delegate
				{
					SingletonUnity<ExitGameRoot>.Instance.Reset();
				}, null);
			}
		}
	}

	// Token: 0x06003A55 RID: 14933 RVA: 0x000FBA74 File Offset: 0x000F9C74
	private void Update()
	{
		BundleManager.LoadModelListUpdate(this);
		if (Input.GetKeyUp(27))
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager == null)
			{
				return;
			}
			if (!SingletonUnity<UIManager>.Instance.BackUIFun())
			{
				if (SingletonUnity<CopyFunctionRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CopyFunctionRootLogic>.Instance.gameObject))
				{
					SingletonUnity<CopyFunctionRootLogic>.Instance.OnClickLeaveCopyBtn();
				}
				else if (SingletonUnity<SexGameUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SexGameUIRootLogic>.Instance.gameObject))
				{
					SingletonUnity<SexGameUIRootLogic>.Instance.OnClickExitBtn();
				}
				else
				{
					this.ExitGame();
				}
			}
		}
	}

	// Token: 0x06003A56 RID: 14934 RVA: 0x000FBB18 File Offset: 0x000F9D18
	private void OnCreateTestNPC(ObjNPC npc)
	{
		npc.DefaultDialogID = string.Empty;
	}

	// Token: 0x06003A57 RID: 14935 RVA: 0x000FBB28 File Offset: 0x000F9D28
	private void AddEquipMent()
	{
		ItemContainer equipPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.EquipPack;
		equipPack.AddItem(new GameItem
		{
			ItemId = "2",
			StackNum = 1
		});
		equipPack.AddItem(new GameItem
		{
			ItemId = "3",
			StackNum = 1
		});
		equipPack.AddItem(new GameItem
		{
			ItemId = "4",
			StackNum = 1
		});
		equipPack.AddItem(new GameItem
		{
			ItemId = "5",
			StackNum = 1
		});
		equipPack.AddItem(new GameItem
		{
			ItemId = "6",
			StackNum = 1
		});
		equipPack.AddItem(new GameItem
		{
			ItemId = "7",
			StackNum = 1
		});
		equipPack.AddItem(new GameItem
		{
			ItemId = "8",
			StackNum = 1
		});
		equipPack.AddItem(new GameItem
		{
			ItemId = "9",
			StackNum = 1
		});
		equipPack.AddItem(new GameItem
		{
			ItemId = "10",
			StackNum = 1
		});
		equipPack.AddItem(new GameItem
		{
			ItemId = "11",
			StackNum = 1
		});
		GameItem gameItem = new GameItem();
		gameItem.ItemId = "12";
		gameItem.StackNum = 1;
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ItemBackPack.AddItem(gameItem);
	}

	// Token: 0x06003A58 RID: 14936 RVA: 0x000FBCB4 File Offset: 0x000F9EB4
	public void TestInitMissionData()
	{
	}

	// Token: 0x06003A59 RID: 14937 RVA: 0x000FBCB8 File Offset: 0x000F9EB8
	public void TestOtherPlayer()
	{
		ObjInitPlayerData objInitPlayerData = new ObjInitPlayerData();
		objInitPlayerData.mServerID = UUID.GenUUID();
		objInitPlayerData.mPos = new Vector3(21f, 0f, 20f);
		Singleton<ObjManager>.Instance.CreateOtherPlayer(objInitPlayerData);
	}

	// Token: 0x06003A5A RID: 14938 RVA: 0x000FBCFC File Offset: 0x000F9EFC
	public void TestRecycleOtherPlayer()
	{
		Singleton<ObjManager>.Instance.RecycleOtherPlayer(Singleton<ObjManager>.Instance.FindObjInScene(9L) as ObjOtherPlayer);
	}

	// Token: 0x06003A5B RID: 14939 RVA: 0x000FBD1C File Offset: 0x000F9F1C
	public void TestReCreateOtherPlayer()
	{
		ObjInitPlayerData objInitPlayerData = new ObjInitPlayerData();
		objInitPlayerData.mServerID = UUID.GenUUID();
		objInitPlayerData.mPos = new Vector3(21f, 0f, 25f);
		Singleton<ObjManager>.Instance.CreateOtherPlayer(objInitPlayerData);
	}

	// Token: 0x06003A5C RID: 14940 RVA: 0x000FBD60 File Offset: 0x000F9F60
	public void CreateMissionNPC()
	{
		ObjInitNpcData objInitNpcData = new ObjInitNpcData();
		NpcData npcDataByID = DataManager.GetNpcDataByID("1001");
		objInitNpcData.npcInfoData = npcDataByID;
		objInitNpcData.mServerID = UUID.GenUUID();
		objInitNpcData.mPos = new VectorXZ(16f, 30f);
		Singleton<ObjManager>.Instance.CreateNPC(objInitNpcData, null, null);
		ObjInitNpcData objInitNpcData2 = new ObjInitNpcData();
		NpcData npcDataByID2 = DataManager.GetNpcDataByID("1002");
		objInitNpcData2.npcInfoData = npcDataByID2;
		objInitNpcData2.mServerID = UUID.GenUUID();
		objInitNpcData2.mPos = new VectorXZ(24f, 20f);
		Singleton<ObjManager>.Instance.CreateNPC(objInitNpcData2, null, null);
		ObjInitNpcData objInitNpcData3 = new ObjInitNpcData();
		NpcData npcDataByID3 = DataManager.GetNpcDataByID("1003");
		objInitNpcData3.npcInfoData = npcDataByID3;
		objInitNpcData3.mServerID = UUID.GenUUID();
		objInitNpcData3.mPos = new VectorXZ(16f, 20f);
		Singleton<ObjManager>.Instance.CreateNPC(objInitNpcData3, null, null);
	}
}
