using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200013F RID: 319
public class PlayerData
{
	// Token: 0x06000DA0 RID: 3488 RVA: 0x0005D928 File Offset: 0x0005BB28
	public PlayerData()
	{
		this.Init();
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x0005DB58 File Offset: 0x0005BD58
	private void Init()
	{
		this.mEquipPack = new ItemContainer(ItemContainer.EQUIPPACK_SIZE, ITEM_CONTAINER_TYPE.EQUIPPACK);
		this.mEquipBackPack = new ItemContainer(ItemContainer.EQUIP_BACKPACK_SIZE, ITEM_CONTAINER_TYPE.EQUIP_BACKPACK);
		this.mItemBackPack = new ItemContainer(ItemContainer.ITEM_BACKPACK_SIZE, ITEM_CONTAINER_TYPE.ITEM_BACKPACK);
		this.mBadgeBackPack = new ItemContainer(ItemContainer.BADGE_BACKPACK_SIZE, ITEM_CONTAINER_TYPE.BADGE_BACKPACK);
		this.mBadgeEquipPack = new ItemContainer(ItemContainer.BADGE_EQUIPPACK_SIZE, ITEM_CONTAINER_TYPE.BADGE_EQUIPPACK);
		this.mFashionBackPack = new ItemContainer(ItemContainer.FASHION_BACKPACK_SIZE, ITEM_CONTAINER_TYPE.FASHION_BACKPACK);
		this.mFashionEquipPack = new ItemContainer(ItemContainer.FASHION_EQUIPPACK_SIZE, ITEM_CONTAINER_TYPE.FASHION_EQUIPPACK);
		this.mTeam = new Team();
		this.mFriendInfo = new FriendInfo();
		this.mChatHistory = new PlayerChatHistory();
		this.mRecentSpeakers = new RecentSpeakerRecord();
		this.mPlayerGuild = new Guild();
		this.mPlayerDanceData = new PlayerDanceData();
		this.InitDragValue();
		this.InitOptionValue();
	}

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0005DC2C File Offset: 0x0005BE2C
	// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x0005DC34 File Offset: 0x0005BE34
	public static long MainPlayerServerId
	{
		get
		{
			return PlayerData.mMainPlayerServerId;
		}
		set
		{
			PlayerData.mMainPlayerServerId = value;
		}
	}

	// Token: 0x17000244 RID: 580
	// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0005DC3C File Offset: 0x0005BE3C
	// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x0005DC44 File Offset: 0x0005BE44
	public bool IsTutorialFinish
	{
		get
		{
			return this.mIsTutorialFinish;
		}
		set
		{
			this.mIsTutorialFinish = value;
		}
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x0005DC50 File Offset: 0x0005BE50
	public static void SavePlayerAccountId(string id)
	{
		if (GameSettingData.IsLocalTestServer)
		{
			PlayerPrefs.SetString("player_account_id" + GameSettingData.LocalTestServerID.ToString() + "unity4", id);
		}
		else
		{
			PlayerPrefs.SetString("player_account_id", id);
		}
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x0005DC8C File Offset: 0x0005BE8C
	public static void SavePlayerAccountKey(string key)
	{
		if (GameSettingData.IsLocalTestServer)
		{
			PlayerPrefs.SetString("player_account_key" + GameSettingData.LocalTestServerID.ToString() + "unity4", key);
		}
		else
		{
			PlayerPrefs.SetString("player_account_key", key);
		}
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x0005DCC8 File Offset: 0x0005BEC8
	public static void ClearAccount()
	{
		if (GameSettingData.IsLocalTestServer)
		{
			PlayerPrefs.DeleteKey("player_account_id" + GameSettingData.LocalTestServerID.ToString() + "unity4");
			PlayerPrefs.DeleteKey("player_account_key" + GameSettingData.LocalTestServerID.ToString() + "unity4");
		}
		else
		{
			PlayerPrefs.DeleteKey("player_account_id");
			PlayerPrefs.DeleteKey("player_account_key");
		}
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x0005DD34 File Offset: 0x0005BF34
	public static string GetPlayerAccountId()
	{
		if (GameSettingData.IsLocalTestServer)
		{
			return PlayerPrefs.GetString("player_account_id" + GameSettingData.LocalTestServerID.ToString() + "unity4", string.Empty);
		}
		return PlayerPrefs.GetString("player_account_id", string.Empty);
	}

	// Token: 0x06000DAB RID: 3499 RVA: 0x0005DD74 File Offset: 0x0005BF74
	public static string GetPlayerAccountKey()
	{
		if (GameSettingData.IsLocalTestServer)
		{
			return PlayerPrefs.GetString("player_account_key" + GameSettingData.LocalTestServerID.ToString() + "unity4", string.Empty);
		}
		return PlayerPrefs.GetString("player_account_key", string.Empty);
	}

	// Token: 0x17000245 RID: 581
	// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0005DDBC File Offset: 0x0005BFBC
	// (set) Token: 0x06000DAC RID: 3500 RVA: 0x0005DDB4 File Offset: 0x0005BFB4
	public int SystemMusic
	{
		get
		{
			return LocalDataSaveManager.GetSystemMusic();
		}
		set
		{
			LocalDataSaveManager.SetSystemMusic(value);
		}
	}

	// Token: 0x17000246 RID: 582
	// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0005DDCC File Offset: 0x0005BFCC
	// (set) Token: 0x06000DAE RID: 3502 RVA: 0x0005DDC4 File Offset: 0x0005BFC4
	public int SystemSoundEffect
	{
		get
		{
			return LocalDataSaveManager.GetSystemSoundEffect();
		}
		set
		{
			LocalDataSaveManager.SetSystemSoundEffect(value);
		}
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x0005DDD4 File Offset: 0x0005BFD4
	public void SetDragValue(float value)
	{
		value = Mathf.Clamp01(value);
		this.mAutoUseDragThreshold = value;
		LocalDataSaveManager.SetkeySystemUseDrag(value);
	}

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x0005DDEC File Offset: 0x0005BFEC
	// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x0005DDF4 File Offset: 0x0005BFF4
	public float MusicDragValue
	{
		get
		{
			return this.mMusicDragValue;
		}
		set
		{
			this.mMusicDragValue = value;
		}
	}

	// Token: 0x17000248 RID: 584
	// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0005DE00 File Offset: 0x0005C000
	// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x0005DE08 File Offset: 0x0005C008
	public float SoundDragValue
	{
		get
		{
			return this.mSoundDragValue;
		}
		set
		{
			this.mSoundDragValue = value;
		}
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x0005DE14 File Offset: 0x0005C014
	public void SetMusicDragValue(float value)
	{
		value = Mathf.Clamp01(value);
		this.mMusicDragValue = value;
		LocalDataSaveManager.SetmusicDrag(value);
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x0005DE2C File Offset: 0x0005C02C
	public void SetSoundDragValue(float value)
	{
		value = Mathf.Clamp01(value);
		this.mSoundDragValue = value;
		LocalDataSaveManager.SetsoundDrag(value);
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x0005DE44 File Offset: 0x0005C044
	public void InitDragValue()
	{
		this.mAutoUseDragThreshold = LocalDataSaveManager.GetkeySystemUseDrag();
		this.mMusicDragValue = LocalDataSaveManager.GetmusicDrag();
		this.mSoundDragValue = LocalDataSaveManager.GetsoundDrag();
	}

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0005DE68 File Offset: 0x0005C068
	public int GraphicsQua
	{
		get
		{
			return this.mGraphicsQua;
		}
	}

	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0005DE70 File Offset: 0x0005C070
	public float Screen_Vibrating
	{
		get
		{
			return (float)this.mScreen_Vibrating;
		}
	}

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0005DE7C File Offset: 0x0005C07C
	public int StreetRacingMode
	{
		get
		{
			return this.mStreetRacingMode;
		}
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0005DE84 File Offset: 0x0005C084
	public float Notify
	{
		get
		{
			return (float)this.mNotify;
		}
	}

	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06000DBC RID: 3516 RVA: 0x0005DE90 File Offset: 0x0005C090
	public int GetChannelValue
	{
		get
		{
			return this.mChannelValue;
		}
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x06000DBD RID: 3517 RVA: 0x0005DE98 File Offset: 0x0005C098
	public int GetChatShow
	{
		get
		{
			this.mChatShow = LocalDataSaveManager.GetChatShow();
			return this.mChatShow;
		}
	}

	// Token: 0x1700024F RID: 591
	// (get) Token: 0x06000DBE RID: 3518 RVA: 0x0005DEAC File Offset: 0x0005C0AC
	public int NonMissionTarget
	{
		get
		{
			return this.mNonMissionTarget;
		}
	}

	// Token: 0x17000250 RID: 592
	// (get) Token: 0x06000DBF RID: 3519 RVA: 0x0005DEB4 File Offset: 0x0005C0B4
	public int VehicleTarget
	{
		get
		{
			return this.mVehicleTarget;
		}
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x0005DEBC File Offset: 0x0005C0BC
	public void SetGraphics(int val)
	{
		this.mGraphicsQua = val;
		LocalDataSaveManager.SetGraphics(val);
		GameSettingData.PhoneClass = val;
		if (Singleton<ObjManager>.Exists)
		{
			Singleton<ObjManager>.Instance.ResetPhoneClass();
		}
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x0005DEE8 File Offset: 0x0005C0E8
	public void SetStreetModel(int value)
	{
		this.mStreetRacingMode = value;
		LocalDataSaveManager.SetStreetModel(value);
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x0005DEF8 File Offset: 0x0005C0F8
	public void SetScreenVibrat(int value)
	{
		this.mScreen_Vibrating = value;
		LocalDataSaveManager.SetScreenVibrat(value);
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x0005DF08 File Offset: 0x0005C108
	public void SetNotify(int value)
	{
		this.mNotify = value;
		LocalDataSaveManager.Setnotyfition(value);
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x0005DF18 File Offset: 0x0005C118
	public void SetChannel(int value)
	{
		if (this.mChannelValue != value)
		{
			this.mChannelValue = value;
			LocalDataSaveManager.SetChannelShow(this.mChannelValue);
			if (SingletonUnity<ChatBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ChatBaseRootLogic>.Instance.gameObject))
			{
				SingletonUnity<ChatBaseRootLogic>.Instance.UpdateMessage();
			}
			if (SingletonUnity<FunctionBtnRootLogic>.Exists)
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMessage();
			}
		}
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x0005DF80 File Offset: 0x0005C180
	public void SetChatShow(int value)
	{
		if (this.mChatShow != value)
		{
			this.mChatShow = value;
			LocalDataSaveManager.SetChatShow(value);
		}
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x0005DF9C File Offset: 0x0005C19C
	public void SetNonMissionTarget(int value)
	{
		if (this.mNonMissionTarget != value)
		{
			this.mNonMissionTarget = value;
			LocalDataSaveManager.SetNonMissionTarget(value);
		}
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x0005DFB8 File Offset: 0x0005C1B8
	public void SetVehicleTarget(int value)
	{
		if (this.mVehicleTarget != value)
		{
			this.mVehicleTarget = value;
			LocalDataSaveManager.SetVehicleTarget(value);
		}
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x0005DFD4 File Offset: 0x0005C1D4
	public void InitOptionValue()
	{
		int num = GameSettingData.InitGraphic();
		int graphics;
		if (num == 0)
		{
			graphics = num;
		}
		else
		{
			graphics = LocalDataSaveManager.GetGraphics(num);
		}
		this.mStreetRacingMode = LocalDataSaveManager.GetStreetModel();
		this.mScreen_Vibrating = LocalDataSaveManager.GetScreenVibrat();
		this.mNotify = LocalDataSaveManager.Getnotyfition();
		this.mChannelValue = LocalDataSaveManager.GetChannelShow();
		this.mNonMissionTarget = LocalDataSaveManager.GetNonMissionTarget();
		this.mVehicleTarget = LocalDataSaveManager.GetVehicleTarget();
		this.SetGraphics(graphics);
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x0005E048 File Offset: 0x0005C248
	public bool IsAcceptChannel(GameDefine.CHAT_CHANNEL_TYPE channeltype)
	{
		int num = -1;
		switch (channeltype)
		{
		case GameDefine.CHAT_CHANNEL_TYPE.SYSTEM:
			num = 0;
			break;
		case GameDefine.CHAT_CHANNEL_TYPE.NORMAL:
			num = 2;
			break;
		case GameDefine.CHAT_CHANNEL_TYPE.WORLD:
			num = 1;
			break;
		case GameDefine.CHAT_CHANNEL_TYPE.TEAM:
			num = 3;
			break;
		case GameDefine.CHAT_CHANNEL_TYPE.GUILD:
			num = 4;
			break;
		case GameDefine.CHAT_CHANNEL_TYPE.PRIVATE:
			num = 5;
			break;
		}
		return num == -1 || (this.mChannelValue & 1 << num) != 0;
	}

	// Token: 0x17000251 RID: 593
	// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0005E0C4 File Offset: 0x0005C2C4
	// (set) Token: 0x06000DCB RID: 3531 RVA: 0x0005E0E0 File Offset: 0x0005C2E0
	public bool CopySceneChange
	{
		get
		{
			return Time.realtimeSinceStartup - this.mfCopySceneChange <= 10f;
		}
		set
		{
			if (value)
			{
				this.mfCopySceneChange = Time.realtimeSinceStartup;
			}
			else
			{
				this.mfCopySceneChange = 0f;
			}
		}
	}

	// Token: 0x17000252 RID: 594
	// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0005E104 File Offset: 0x0005C304
	// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0005E10C File Offset: 0x0005C30C
	public List<CharacterSkillData> MainPlayerSkillDataList
	{
		get
		{
			return this.mMainPlayerSkillDataList;
		}
		set
		{
			this.mMainPlayerSkillDataList = value;
		}
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x0005E118 File Offset: 0x0005C318
	public void CleanUpSkillDataList()
	{
		this.mMainPlayerSkillDataList.Clear();
		this.mMainPlayerSkillIDList.Clear();
	}

	// Token: 0x17000253 RID: 595
	// (get) Token: 0x06000DCF RID: 3535 RVA: 0x0005E130 File Offset: 0x0005C330
	// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x0005E138 File Offset: 0x0005C338
	public List<string> MainPlayerSkillIDList
	{
		get
		{
			return this.mMainPlayerSkillIDList;
		}
		set
		{
			this.mMainPlayerSkillIDList = value;
		}
	}

	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0005E144 File Offset: 0x0005C344
	public FriendInfo FriendInfo
	{
		get
		{
			return this.mFriendInfo;
		}
	}

	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0005E14C File Offset: 0x0005C34C
	public long VideoTimes
	{
		get
		{
			return this.mVideoTimes;
		}
	}

	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0005E154 File Offset: 0x0005C354
	public long VideoMaxTimes
	{
		get
		{
			return this.mVideoMaxTimes;
		}
	}

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0005E15C File Offset: 0x0005C35C
	public long VideoDiamond
	{
		get
		{
			return this.mVideoDiamond;
		}
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x0005E164 File Offset: 0x0005C364
	public void SetVideoTimes(long times)
	{
		this.mVideoTimes = times;
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x0005E170 File Offset: 0x0005C370
	public void SetVideoMaxTimes(long maxtimes)
	{
		this.mVideoMaxTimes = maxtimes;
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x0005E17C File Offset: 0x0005C37C
	public void SetVideoDiamond(long diamondnum)
	{
		this.mVideoDiamond = diamondnum;
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x0005E188 File Offset: 0x0005C388
	public bool CheckLevel(int limitlevel)
	{
		return this.Level >= limitlevel;
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x0005E198 File Offset: 0x0005C398
	public bool CheckLevel(int minlevel, int maxlevel)
	{
		return this.Level >= minlevel && this.Level <= maxlevel;
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x0005E1B8 File Offset: 0x0005C3B8
	public int CheckIsLevelHeigh(int minlevel, int maxlevel)
	{
		if (this.Level < minlevel)
		{
			return 7;
		}
		if (this.Level > maxlevel)
		{
			return 6;
		}
		return 0;
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x0005E1D8 File Offset: 0x0005C3D8
	public float GetDragTime01()
	{
		if (this.mDragCdTime > 0f)
		{
			return 1f - Mathf.Clamp01((Time.time - this.mDragCdTime) / 20f);
		}
		return 0f;
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x0005E210 File Offset: 0x0005C410
	public bool CanUseDrag()
	{
		return this.mDragCdTime <= 0f || this.mDragCdTime + 20f <= Time.time;
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x0005E23C File Offset: 0x0005C43C
	public void UpdateDragCD()
	{
		this.mDragCdTime = Time.time;
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0005E24C File Offset: 0x0005C44C
	// (set) Token: 0x06000DDF RID: 3551 RVA: 0x0005E254 File Offset: 0x0005C454
	public PROFESSION_TYPE Profession
	{
		get
		{
			return this.mProfession;
		}
		set
		{
			this.mProfession = value;
		}
	}

	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0005E260 File Offset: 0x0005C460
	// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x0005E268 File Offset: 0x0005C468
	public CharacterAttributeData MainPlayerAttrData
	{
		get
		{
			return this.mMainPlayerAttrData;
		}
		set
		{
			this.mMainPlayerAttrData = value;
		}
	}

	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0005E274 File Offset: 0x0005C474
	// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x0005E27C File Offset: 0x0005C47C
	public CharacterAttributeData OtherPlayerAttriData
	{
		get
		{
			return this.mOtherPlayerAttriData;
		}
		set
		{
			this.mOtherPlayerAttriData = value;
		}
	}

	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0005E288 File Offset: 0x0005C488
	// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x0005E298 File Offset: 0x0005C498
	public int Level
	{
		get
		{
			return this.mMainPlayerAttrData.Level;
		}
		set
		{
			this.mMainPlayerAttrData.Level = value;
		}
	}

	// Token: 0x1700025C RID: 604
	// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0005E2A8 File Offset: 0x0005C4A8
	public PlayerRankPVPData RankPVPData
	{
		get
		{
			return this.mRankPVPData;
		}
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0005E2B0 File Offset: 0x0005C4B0
	public TowerInfoData TowerData
	{
		get
		{
			return this.mTowerData;
		}
	}

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0005E2C4 File Offset: 0x0005C4C4
	// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x0005E2B8 File Offset: 0x0005C4B8
	public TargetBasicInfo SelectTargetBasicInfo
	{
		get
		{
			return this.mSelectTargetBasicInfo;
		}
		set
		{
			this.mSelectTargetBasicInfo = value;
		}
	}

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0005E2CC File Offset: 0x0005C4CC
	// (set) Token: 0x06000DEB RID: 3563 RVA: 0x0005E2D4 File Offset: 0x0005C4D4
	public CameraController.CAMERAVIEWSTATE ViewType
	{
		get
		{
			return this.mViewType;
		}
		set
		{
			this.mViewType = value;
		}
	}

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0005E2E0 File Offset: 0x0005C4E0
	// (set) Token: 0x06000DED RID: 3565 RVA: 0x0005E2E8 File Offset: 0x0005C4E8
	public string CharacterModelId
	{
		get
		{
			return this.mCharacterModelId;
		}
		set
		{
			this.mCharacterModelId = value;
		}
	}

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0005E2F4 File Offset: 0x0005C4F4
	// (set) Token: 0x06000DEF RID: 3567 RVA: 0x0005E2FC File Offset: 0x0005C4FC
	public CharacterModelData CharacterModelData
	{
		get
		{
			return this.mCharacterModelData;
		}
		set
		{
			this.mCharacterModelData = value;
		}
	}

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0005E308 File Offset: 0x0005C508
	// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x0005E310 File Offset: 0x0005C510
	public string PartWeaponId
	{
		get
		{
			return this.mPartWeaponId;
		}
		set
		{
			this.mPartWeaponId = value;
		}
	}

	// Token: 0x17000263 RID: 611
	// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0005E31C File Offset: 0x0005C51C
	// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x0005E324 File Offset: 0x0005C524
	public string PartHeadId
	{
		get
		{
			return this.mPartHeadId;
		}
		set
		{
			this.mPartHeadId = value;
		}
	}

	// Token: 0x17000264 RID: 612
	// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0005E330 File Offset: 0x0005C530
	// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x0005E338 File Offset: 0x0005C538
	public string PartBodyId
	{
		get
		{
			return this.mPartBodyId;
		}
		set
		{
			this.mPartBodyId = value;
		}
	}

	// Token: 0x17000265 RID: 613
	// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0005E344 File Offset: 0x0005C544
	// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x0005E34C File Offset: 0x0005C54C
	public string PartLegId
	{
		get
		{
			return this.mPartLegId;
		}
		set
		{
			this.mPartLegId = value;
		}
	}

	// Token: 0x17000266 RID: 614
	// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0005E358 File Offset: 0x0005C558
	// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x0005E360 File Offset: 0x0005C560
	public string FashionHeadId
	{
		get
		{
			return this.mFashionHeadId;
		}
		set
		{
			this.mFashionHeadId = value;
		}
	}

	// Token: 0x17000267 RID: 615
	// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0005E36C File Offset: 0x0005C56C
	// (set) Token: 0x06000DFB RID: 3579 RVA: 0x0005E374 File Offset: 0x0005C574
	public string FashionBodyId
	{
		get
		{
			return this.mFashionBodyId;
		}
		set
		{
			this.mFashionBodyId = value;
		}
	}

	// Token: 0x17000268 RID: 616
	// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0005E380 File Offset: 0x0005C580
	// (set) Token: 0x06000DFD RID: 3581 RVA: 0x0005E388 File Offset: 0x0005C588
	public string FashionLegId
	{
		get
		{
			return this.mFashionLegId;
		}
		set
		{
			this.mFashionLegId = value;
		}
	}

	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0005E394 File Offset: 0x0005C594
	// (set) Token: 0x06000DFF RID: 3583 RVA: 0x0005E39C File Offset: 0x0005C59C
	public string FashionWeaponId
	{
		get
		{
			return this.mFashionWeaponId;
		}
		set
		{
			this.mFashionWeaponId = value;
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0005E3A8 File Offset: 0x0005C5A8
	// (set) Token: 0x06000E01 RID: 3585 RVA: 0x0005E3B0 File Offset: 0x0005C5B0
	public string WeaponItemId
	{
		get
		{
			return this.mWeaponItemId;
		}
		set
		{
			this.mWeaponItemId = value;
		}
	}

	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0005E3BC File Offset: 0x0005C5BC
	// (set) Token: 0x06000E03 RID: 3587 RVA: 0x0005E3C4 File Offset: 0x0005C5C4
	public string FashionItemId
	{
		get
		{
			return this.mFashionItemId;
		}
		set
		{
			this.mFashionItemId = value;
		}
	}

	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0005E3D0 File Offset: 0x0005C5D0
	// (set) Token: 0x06000E05 RID: 3589 RVA: 0x0005E3D8 File Offset: 0x0005C5D8
	public bool IsShowFashion
	{
		get
		{
			return this.mIsShowFashion;
		}
		set
		{
			this.mIsShowFashion = value;
		}
	}

	// Token: 0x1700026D RID: 621
	// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0005E3E4 File Offset: 0x0005C5E4
	// (set) Token: 0x06000E07 RID: 3591 RVA: 0x0005E3EC File Offset: 0x0005C5EC
	public bool IsServerRidingMount
	{
		get
		{
			return this.mIsServerRidingMount;
		}
		set
		{
			this.mIsServerRidingMount = value;
		}
	}

	// Token: 0x1700026E RID: 622
	// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0005E3F8 File Offset: 0x0005C5F8
	// (set) Token: 0x06000E09 RID: 3593 RVA: 0x0005E400 File Offset: 0x0005C600
	public string MountId
	{
		get
		{
			return this.mMountId;
		}
		set
		{
			this.mMountId = value;
			if (Singleton<ObjManager>.Instance.MainPlayer != null)
			{
				this.UpdateMainPlayerPartBundleIdList(Singleton<ObjManager>.Instance.MainPlayer.TargetPartObjId);
			}
		}
	}

	// Token: 0x1700026F RID: 623
	// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0005E440 File Offset: 0x0005C640
	// (set) Token: 0x06000E0B RID: 3595 RVA: 0x0005E448 File Offset: 0x0005C648
	public string MountColor
	{
		get
		{
			return this.mMountColor;
		}
		set
		{
			this.mMountColor = value;
		}
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x0005E454 File Offset: 0x0005C654
	public bool CheckWeaponIsSame()
	{
		if (!string.IsNullOrEmpty(this.mWeaponItemId) && !string.IsNullOrEmpty(this.mFashionItemId))
		{
			EquipData equipDataById = DataManager.GetEquipDataById(this.mWeaponItemId);
			EquipData equipDataById2 = DataManager.GetEquipDataById(this.mFashionItemId);
			return equipDataById.WeaponType == equipDataById2.WeaponType;
		}
		return string.IsNullOrEmpty(this.PartWeaponId) || string.IsNullOrEmpty(this.FashionWeaponId) || GameDefine.GetWeaponName(this.PartWeaponId).Equals(GameDefine.GetWeaponName(this.FashionWeaponId));
	}

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0005E4F0 File Offset: 0x0005C6F0
	public long Cash
	{
		get
		{
			return this.mCash;
		}
	}

	// Token: 0x17000271 RID: 625
	// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0005E4F8 File Offset: 0x0005C6F8
	public long Gold
	{
		get
		{
			return this.mGold;
		}
	}

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0005E500 File Offset: 0x0005C700
	public long Diamond
	{
		get
		{
			return this.mDiamond;
		}
	}

	// Token: 0x17000273 RID: 627
	// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0005E508 File Offset: 0x0005C708
	public long BattleCoin
	{
		get
		{
			return this.mBattleCoin;
		}
	}

	// Token: 0x17000274 RID: 628
	// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0005E510 File Offset: 0x0005C710
	public long ActivityCoin
	{
		get
		{
			return this.mActivityCoin;
		}
	}

	// Token: 0x06000E12 RID: 3602 RVA: 0x0005E518 File Offset: 0x0005C718
	public void SetCash(long val)
	{
		this.mCash = val;
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x0005E524 File Offset: 0x0005C724
	public void SetGold(long val)
	{
		this.mGold = val;
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x0005E530 File Offset: 0x0005C730
	public void SetDiamond(long val)
	{
		this.mDiamond = val;
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x0005E53C File Offset: 0x0005C73C
	public void SetBattleCoin(long val)
	{
		this.mBattleCoin = val;
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0005E548 File Offset: 0x0005C748
	public void SetActivityCoin(long val)
	{
		this.mActivityCoin = val;
	}

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0005E554 File Offset: 0x0005C754
	// (set) Token: 0x06000E18 RID: 3608 RVA: 0x0005E55C File Offset: 0x0005C75C
	public ItemContainer ItemBackPack
	{
		get
		{
			return this.mItemBackPack;
		}
		set
		{
			this.mItemBackPack = value;
		}
	}

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0005E568 File Offset: 0x0005C768
	// (set) Token: 0x06000E1A RID: 3610 RVA: 0x0005E570 File Offset: 0x0005C770
	public ItemContainer EquipPack
	{
		get
		{
			return this.mEquipPack;
		}
		set
		{
			this.mEquipPack = value;
		}
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x0005E57C File Offset: 0x0005C77C
	public int GetEquipCombatVal(EQUIP_PACK_TYPE packType, EQUIP_BACKPACK_TYPE targetPart)
	{
		if (packType == EQUIP_PACK_TYPE.BACKPACK)
		{
			GameItem equipByEquipType = this.mEquipPack.GetEquipByEquipType(targetPart);
			if (equipByEquipType != null)
			{
				return equipByEquipType.GetItemCombatVal();
			}
			return 0;
		}
		else
		{
			if (packType != EQUIP_PACK_TYPE.FASHION)
			{
				return 0;
			}
			GameItem equipByEquipType2 = this.mFashionBackPack.GetEquipByEquipType(targetPart);
			if (equipByEquipType2 != null)
			{
				return equipByEquipType2.GetItemCombatVal();
			}
			return 0;
		}
	}

	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0005E5D0 File Offset: 0x0005C7D0
	// (set) Token: 0x06000E1D RID: 3613 RVA: 0x0005E5D8 File Offset: 0x0005C7D8
	public ItemContainer BadgeEquipPack
	{
		get
		{
			return this.mBadgeEquipPack;
		}
		set
		{
			this.mBadgeEquipPack = value;
		}
	}

	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0005E5E4 File Offset: 0x0005C7E4
	// (set) Token: 0x06000E1F RID: 3615 RVA: 0x0005E5EC File Offset: 0x0005C7EC
	public ItemContainer BadgeBackPack
	{
		get
		{
			return this.mBadgeBackPack;
		}
		set
		{
			this.mBadgeBackPack = value;
		}
	}

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0005E5F8 File Offset: 0x0005C7F8
	// (set) Token: 0x06000E21 RID: 3617 RVA: 0x0005E600 File Offset: 0x0005C800
	public ItemContainer EquipBackPack
	{
		get
		{
			return this.mEquipBackPack;
		}
		set
		{
			this.mEquipBackPack = value;
		}
	}

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0005E60C File Offset: 0x0005C80C
	// (set) Token: 0x06000E23 RID: 3619 RVA: 0x0005E614 File Offset: 0x0005C814
	public ItemContainer FashionBackPack
	{
		get
		{
			return this.mFashionBackPack;
		}
		set
		{
			this.mFashionBackPack = value;
		}
	}

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0005E620 File Offset: 0x0005C820
	// (set) Token: 0x06000E25 RID: 3621 RVA: 0x0005E628 File Offset: 0x0005C828
	public ItemContainer FashionEquipPack
	{
		get
		{
			return this.mFashionEquipPack;
		}
		set
		{
			this.mFashionEquipPack = value;
		}
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x0005E634 File Offset: 0x0005C834
	public ItemContainer GetItemContainer(ITEM_CONTAINER_TYPE type)
	{
		switch (type)
		{
		case ITEM_CONTAINER_TYPE.EQUIP_BACKPACK:
			return this.mEquipBackPack;
		case ITEM_CONTAINER_TYPE.EQUIPPACK:
			return this.mEquipPack;
		case ITEM_CONTAINER_TYPE.ITEM_BACKPACK:
			return this.mItemBackPack;
		case ITEM_CONTAINER_TYPE.BADGE_BACKPACK:
			return this.mBadgeBackPack;
		case ITEM_CONTAINER_TYPE.BADGE_EQUIPPACK:
			return this.mBadgeEquipPack;
		case ITEM_CONTAINER_TYPE.FASHION_BACKPACK:
			return this.mFashionBackPack;
		case ITEM_CONTAINER_TYPE.FASHION_EQUIPPACK:
			return this.mFashionEquipPack;
		default:
			return null;
		}
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x0005E69C File Offset: 0x0005C89C
	public void UpdateEquipsTips()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.CheckTips(this.IsHaveBackPackTips(), GameDefine.TIPS_TYPE.CHARACTER);
			SingletonUnity<FunctionBtnRootLogic>.Instance.CheckTips(this.IsHaveItemTips(), GameDefine.TIPS_TYPE.ITEMS);
		}
		if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.RefershTips();
		}
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x0005E700 File Offset: 0x0005C900
	public void UpdateEnhanceTips()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateEnhanceTipsFlag();
		}
		if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.RefershTips();
		}
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x0005E74C File Offset: 0x0005C94C
	public bool IsHaveEquipTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.CHARACTER_EQUIP))
		{
			return false;
		}
		for (int i = 0; i < this.mEquipBackPack.ContainerSize; i++)
		{
			if (!this.mEquipBackPack.ItemList[i].IsEmpty() && this.mEquipBackPack.ItemList[i].Parm[5] == 0)
			{
				return true;
			}
		}
		List<GameItem> itemList = this.mEquipPack.ItemList;
		List<GameItem> itemList2 = this.mEquipBackPack.ItemList;
		for (int j = 0; j < itemList.Count; j++)
		{
			GameItem gameItem = itemList[j];
			if (!gameItem.IsEmpty())
			{
				ItemData itemData = gameItem.ItemData;
				for (int k = 0; k < itemList2.Count; k++)
				{
					GameItem gameItem2 = itemList2[k];
					if (!gameItem2.IsEmpty())
					{
						ItemData itemData2 = gameItem2.ItemData;
						if (itemData2.SubType == itemData.SubType && gameItem.GetItemQuality() < gameItem2.GetItemQuality() && gameItem2.Parm[5] == 0)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x0005E890 File Offset: 0x0005CA90
	public bool IsHaveFashionEquipTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.CHARACTER_FASHION))
		{
			return false;
		}
		for (int i = 0; i < this.mFashionBackPack.ContainerSize; i++)
		{
			if (!this.mFashionBackPack.ItemList[i].IsEmpty() && this.mFashionBackPack.ItemList[i].Parm[5] == 0)
			{
				return true;
			}
		}
		List<GameItem> itemList = this.mFashionEquipPack.ItemList;
		List<GameItem> itemList2 = this.mFashionBackPack.ItemList;
		for (int j = 0; j < itemList.Count; j++)
		{
			GameItem gameItem = itemList[j];
			if (!gameItem.IsEmpty())
			{
				ItemData itemData = gameItem.ItemData;
				for (int k = 0; k < itemList2.Count; k++)
				{
					GameItem gameItem2 = itemList2[k];
					if (!gameItem2.IsEmpty())
					{
						ItemData itemData2 = gameItem2.ItemData;
						if (itemData2.SubType == itemData.SubType && gameItem.GetItemCombatVal() < gameItem2.GetItemCombatVal() && gameItem2.Parm[5] == 0)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x0005E9D4 File Offset: 0x0005CBD4
	public bool IsHaveBadgeTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.CHARACTER_BADGE))
		{
			return false;
		}
		for (int i = 0; i < this.mBadgeBackPack.ContainerSize; i++)
		{
			if (!this.mBadgeBackPack.ItemList[i].IsEmpty() && this.mBadgeBackPack.ItemList[i].Parm[5] == 0)
			{
				return true;
			}
		}
		List<GameItem> itemList = this.mBadgeEquipPack.ItemList;
		List<GameItem> itemList2 = this.mBadgeBackPack.ItemList;
		for (int j = 0; j < itemList.Count; j++)
		{
			GameItem gameItem = itemList[j];
			if (!gameItem.IsEmpty())
			{
				BadgeData badgeDataById = DataManager.GetBadgeDataById(gameItem.ItemId);
				for (int k = 0; k < itemList2.Count; k++)
				{
					GameItem gameItem2 = itemList2[j];
					if (!gameItem2.IsEmpty())
					{
						BadgeData badgeDataById2 = DataManager.GetBadgeDataById(gameItem2.ItemId);
						if (gameItem.ItemData.SubType == gameItem2.ItemData.SubType && badgeDataById.Lv < badgeDataById2.Lv && gameItem2.Parm[5] == 0)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x0005EB2C File Offset: 0x0005CD2C
	public bool IsHaveItemTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.CHARACTER_ITEM))
		{
			return false;
		}
		for (int i = 0; i < this.mItemBackPack.ContainerSize; i++)
		{
			if (!this.mItemBackPack.ItemList[i].IsEmpty() && this.mItemBackPack.ItemList[i].Parm[5] == 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x0005EBAC File Offset: 0x0005CDAC
	public bool IsHaveBackPackTips()
	{
		return this.IsHaveEquipTips() || this.IsHaveFashionEquipTips() || this.IsHaveBadgeTips() || this.IsHaveItemTips();
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x0005EBE4 File Offset: 0x0005CDE4
	public bool IsHaveEnhanceandRefineTips()
	{
		return this.IsHaveEnhanceTips() || this.IsHaveRefineTips();
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x0005EBFC File Offset: 0x0005CDFC
	public bool IsHaveEnhanceTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ENHANCE_EQUIP))
		{
			return false;
		}
		for (int i = 0; i < this.mEquipPack.ContainerSize; i++)
		{
			if (!this.mEquipPack.ItemList[i].IsEmpty())
			{
				GameItem gameItem = this.mEquipPack.ItemList[i];
				EquipData equipDataById = DataManager.GetEquipDataById(gameItem.ItemId);
				long cash = GameMoneyHelper.GetCash();
				int upgradeMoneyByQualityAndLevel = equipDataById.GetUpgradeMoneyByQualityAndLevel((int)gameItem.GetItemQuality(), gameItem.ItemLevel);
				GameItem enhanceItem = this.ItemBackPack.GetEnhanceItem();
				int upgradeExpValByLevel = equipDataById.GetUpgradeExpValByLevel(gameItem.ItemLevel);
				if (enhanceItem == null)
				{
					return false;
				}
				if (this.mEquipPack.ItemList[i].ItemLevel < this.Level && cash >= (long)upgradeMoneyByQualityAndLevel && enhanceItem.StackNum >= upgradeExpValByLevel)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x0005ECF4 File Offset: 0x0005CEF4
	public bool IsCanUpgradeWeapon()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ENHANCE_EQUIP))
		{
			return false;
		}
		for (int i = 0; i < this.mEquipPack.ContainerSize; i++)
		{
			if (!this.mEquipPack.ItemList[i].IsEmpty() && this.mEquipPack.ItemList[i].ItemData.SubType == 0)
			{
				GameItem gameItem = this.mEquipPack.ItemList[i];
				EquipData equipDataById = DataManager.GetEquipDataById(gameItem.ItemId);
				long cash = GameMoneyHelper.GetCash();
				int upgradeMoneyByQualityAndLevel = equipDataById.GetUpgradeMoneyByQualityAndLevel((int)gameItem.GetItemQuality(), gameItem.ItemLevel);
				GameItem enhanceItem = this.ItemBackPack.GetEnhanceItem();
				int upgradeExpValByLevel = equipDataById.GetUpgradeExpValByLevel(gameItem.ItemLevel);
				if (enhanceItem == null)
				{
					return false;
				}
				if (this.mEquipPack.ItemList[i].ItemLevel < this.Level && cash >= (long)upgradeMoneyByQualityAndLevel && enhanceItem.StackNum >= upgradeExpValByLevel)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x0005EE0C File Offset: 0x0005D00C
	public bool IsHaveRefineTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ENHANCE_STAR))
		{
			return false;
		}
		RefineData[] array = new RefineData[5];
		for (int i = 1; i < 5; i++)
		{
			array[i] = DataManager.GetRefineDataByPartLevelPRO(i, this.MainPlayerAttrData.GetTargetRefinePartLevel((REFINE_PART)i), (int)this.Profession);
		}
		for (int j = 1; j < 5; j++)
		{
			RefineData refineData = array[j];
			if (refineData.Lv < GameDefine.MAX_REFINE_LEVEL)
			{
				int itemStackNumById = this.ItemBackPack.GetItemStackNumById(refineData.CostId1);
				if (itemStackNumById >= refineData.Cost1)
				{
					if (!string.IsNullOrEmpty(refineData.CostId2))
					{
						int itemStackNumById2 = this.ItemBackPack.GetItemStackNumById(refineData.CostId2);
						if (itemStackNumById2 < refineData.Cost2)
						{
							goto IL_102;
						}
					}
					if (refineData.MoneyType == 0)
					{
						if (GameMoneyHelper.GetCash() < (long)refineData.MoneyCost)
						{
							goto IL_102;
						}
					}
					else if (GameMoneyHelper.GetGold() < (long)refineData.MoneyCost)
					{
						goto IL_102;
					}
					return true;
				}
			}
			IL_102:;
		}
		return false;
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x0005EF28 File Offset: 0x0005D128
	public bool IsHaveCanInherit()
	{
		ItemContainer equipBackPack = this.EquipBackPack;
		ItemContainer equipPack = this.EquipPack;
		PROFESSION_TYPE profession = this.Profession;
		List<GameItem> equipItemList = ItemContainerTool.GetEquipItemList(equipPack);
		GameItem gameItem = equipItemList[ItemContainerTool.ChangeEquipTypeToIndex(EQUIP_BACKPACK_TYPE.WEAPON)];
		GameItem gameItem2 = equipItemList[ItemContainerTool.ChangeEquipTypeToIndex(EQUIP_BACKPACK_TYPE.HEAD)];
		GameItem gameItem3 = equipItemList[ItemContainerTool.ChangeEquipTypeToIndex(EQUIP_BACKPACK_TYPE.BODY)];
		GameItem gameItem4 = equipItemList[ItemContainerTool.ChangeEquipTypeToIndex(EQUIP_BACKPACK_TYPE.LEG)];
		GameItem gameItem5 = equipItemList[ItemContainerTool.ChangeEquipTypeToIndex(EQUIP_BACKPACK_TYPE.BELT)];
		GameItem gameItem6 = equipItemList[ItemContainerTool.ChangeEquipTypeToIndex(EQUIP_BACKPACK_TYPE.NECKLACE)];
		if (gameItem != null && !gameItem.IsEmpty())
		{
			List<GameItem> targetTypeItem = ItemContainerTool.GetTargetTypeItem(equipBackPack, 0, profession, gameItem);
			for (int i = 0; i < targetTypeItem.Count; i++)
			{
				if (targetTypeItem[i].IsAppraise && targetTypeItem[i].IsHaveRandomAtt)
				{
					return true;
				}
			}
		}
		if (gameItem2 != null && !gameItem2.IsEmpty())
		{
			List<GameItem> targetTypeItem2 = ItemContainerTool.GetTargetTypeItem(equipBackPack, 1, profession, null);
			for (int j = 0; j < targetTypeItem2.Count; j++)
			{
				if (targetTypeItem2[j].IsAppraise && targetTypeItem2[j].IsHaveRandomAtt)
				{
					return true;
				}
			}
		}
		if (gameItem3 != null && !gameItem3.IsEmpty())
		{
			List<GameItem> targetTypeItem3 = ItemContainerTool.GetTargetTypeItem(equipBackPack, 2, profession, null);
			for (int k = 0; k < targetTypeItem3.Count; k++)
			{
				if (targetTypeItem3[k].IsAppraise && targetTypeItem3[k].IsHaveRandomAtt)
				{
					return true;
				}
			}
		}
		if (gameItem4 != null && !gameItem4.IsEmpty())
		{
			List<GameItem> targetTypeItem4 = ItemContainerTool.GetTargetTypeItem(equipBackPack, 3, profession, null);
			for (int l = 0; l < targetTypeItem4.Count; l++)
			{
				if (targetTypeItem4[l].IsAppraise && targetTypeItem4[l].IsHaveRandomAtt)
				{
					return true;
				}
			}
		}
		if (gameItem5 != null && !gameItem5.IsEmpty())
		{
			List<GameItem> targetTypeItem5 = ItemContainerTool.GetTargetTypeItem(equipBackPack, 4, profession, null);
			for (int m = 0; m < targetTypeItem5.Count; m++)
			{
				if (targetTypeItem5[m].IsAppraise && targetTypeItem5[m].IsHaveRandomAtt)
				{
					return true;
				}
			}
		}
		if (gameItem6 != null && !gameItem6.IsEmpty())
		{
			List<GameItem> targetTypeItem6 = ItemContainerTool.GetTargetTypeItem(equipBackPack, 5, profession, null);
			for (int n = 0; n < targetTypeItem6.Count; n++)
			{
				if (targetTypeItem6[n].IsAppraise && targetTypeItem6[n].IsHaveRandomAtt)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x0005F1F4 File Offset: 0x0005D3F4
	public void UpdateDominInfo(ret_domin_info.request request)
	{
		if (request.HasDomin_infos)
		{
			this.Domin_InfoDic = request.domin_infos;
		}
		if (request.HasCharacters)
		{
			this.Domin_CharacterDic = request.characters;
		}
		this.FlashDominRootPage();
		this.UpdateDominInfoPage();
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x0005F23C File Offset: 0x0005D43C
	public void UpdateDominInfoPage()
	{
		if (SingletonUnity<DominInfoRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<DominInfoRootLogic>.Instance.gameObject))
		{
			SingletonUnity<DominInfoRootLogic>.Instance.FlashPage();
		}
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x0005F274 File Offset: 0x0005D474
	public void FlashDominRootPage()
	{
		if (SingletonUnity<DominRootLogic>.Exists && SingletonUnity<DominRootLogic>.Instance.gameObject.active)
		{
			SingletonUnity<DominRootLogic>.Instance.Reset(this.Domin_InfoDic, this.Domin_CharacterDic);
		}
		if (SingletonUnity<MiniMap>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MiniMap>.Instance.gameObject))
		{
			SingletonUnity<MiniMap>.Instance.UpdateActionMapObj();
		}
	}

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x06000E36 RID: 3638 RVA: 0x0005F2E0 File Offset: 0x0005D4E0
	// (set) Token: 0x06000E37 RID: 3639 RVA: 0x0005F2E8 File Offset: 0x0005D4E8
	public Team TeamInfo
	{
		get
		{
			return this.mTeam;
		}
		set
		{
			this.mTeam = value;
		}
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x0005F2F4 File Offset: 0x0005D4F4
	public bool IsHaveTeam()
	{
		return this.mTeam != null && this.mTeam.TeamID != -1L;
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x0005F324 File Offset: 0x0005D524
	public bool IsTeamMember()
	{
		return this.IsHaveTeam() && PlayerData.MainPlayerServerId != this.mTeam.TeamLeader.ServerId;
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x0005F35C File Offset: 0x0005D55C
	public bool IsTeamLeader()
	{
		return this.IsHaveTeam() && PlayerData.MainPlayerServerId == this.mTeam.TeamLeader.ServerId;
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x0005F394 File Offset: 0x0005D594
	public bool IsSingleTeam()
	{
		if (this.IsHaveTeam())
		{
			for (int i = 0; i < this.mTeam.TeamMembers.Length; i++)
			{
				if (this.mTeam.TeamMembers[i] != null && this.mTeam.TeamMembers[i].IsValid())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0005F3F8 File Offset: 0x0005D5F8
	public Dictionary<long, float> PreTeamInviteDic
	{
		get
		{
			return this.mPreTeamInviteDic;
		}
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x0005F400 File Offset: 0x0005D600
	public bool IsCanInviteTeam(long teamId)
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.TEAM))
		{
			return false;
		}
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager == null || sceneManager.IsCarScene() || sceneManager.IsShowTeamScene())
		{
			return false;
		}
		if (!this.mPreTeamInviteDic.ContainsKey(teamId))
		{
			if (this.mPreTeamInviteDic.Count > 10)
			{
				List<long> list = new List<long>(this.mPreTeamInviteDic.Keys);
				float num = Time.time - PlayerData.TEAM_INVITE_INTERVAL;
				for (int i = 0; i < list.Count; i++)
				{
					if (this.mPreTeamInviteDic[list[i]] < num)
					{
						this.mPreTeamInviteDic.Remove(list[i]);
					}
				}
			}
			this.mPreTeamInviteDic.Add(teamId, Time.time);
			return true;
		}
		if (Time.time - this.mPreTeamInviteDic[teamId] > PlayerData.TEAM_INVITE_INTERVAL)
		{
			this.mPreTeamInviteDic[teamId] = Time.time;
			return true;
		}
		return false;
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0005F518 File Offset: 0x0005D718
	public CopyData CopyInfoData
	{
		get
		{
			return this.mCopyInfoData;
		}
	}

	// Token: 0x1700027F RID: 639
	// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0005F520 File Offset: 0x0005D720
	// (set) Token: 0x06000E40 RID: 3648 RVA: 0x0005F528 File Offset: 0x0005D728
	public ActivityData ActivityData
	{
		get
		{
			return this.mActivityData;
		}
		set
		{
			this.mActivityData = value;
		}
	}

	// Token: 0x17000280 RID: 640
	// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0005F534 File Offset: 0x0005D734
	// (set) Token: 0x06000E42 RID: 3650 RVA: 0x0005F53C File Offset: 0x0005D73C
	public PlayerSlotData playerSlotData
	{
		get
		{
			return this.mPlayerSlotData;
		}
		set
		{
			this.mPlayerSlotData = value;
		}
	}

	// Token: 0x17000281 RID: 641
	// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0005F548 File Offset: 0x0005D748
	// (set) Token: 0x06000E44 RID: 3652 RVA: 0x0005F550 File Offset: 0x0005D750
	public WelfareData welfareData
	{
		get
		{
			return this.mWelfareData;
		}
		set
		{
			this.mWelfareData = value;
		}
	}

	// Token: 0x17000282 RID: 642
	// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0005F55C File Offset: 0x0005D75C
	// (set) Token: 0x06000E46 RID: 3654 RVA: 0x0005F564 File Offset: 0x0005D764
	public PlayerMountData playerMountData
	{
		get
		{
			return this.mPlayerMountData;
		}
		set
		{
			this.mPlayerMountData = value;
		}
	}

	// Token: 0x17000283 RID: 643
	// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0005F570 File Offset: 0x0005D770
	// (set) Token: 0x06000E48 RID: 3656 RVA: 0x0005F578 File Offset: 0x0005D778
	public int CurLineIndex
	{
		get
		{
			return this.mCurLineIndex;
		}
		set
		{
			this.mCurLineIndex = value;
		}
	}

	// Token: 0x17000284 RID: 644
	// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0005F584 File Offset: 0x0005D784
	// (set) Token: 0x06000E4A RID: 3658 RVA: 0x0005F58C File Offset: 0x0005D78C
	public int LineCount
	{
		get
		{
			return this.mLineCount;
		}
		set
		{
			this.lineStates = null;
			this.mLineCount = value;
		}
	}

	// Token: 0x17000285 RID: 645
	// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0005F59C File Offset: 0x0005D79C
	// (set) Token: 0x06000E4C RID: 3660 RVA: 0x0005F5A4 File Offset: 0x0005D7A4
	public List<long> LineStates
	{
		get
		{
			return this.lineStates;
		}
		set
		{
			this.lineStates = value;
		}
	}

	// Token: 0x17000286 RID: 646
	// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0005F5B0 File Offset: 0x0005D7B0
	// (set) Token: 0x06000E4E RID: 3662 RVA: 0x0005F5B8 File Offset: 0x0005D7B8
	public long CurSelectPotionIndex
	{
		get
		{
			return this.mCurSelectPotionIndex;
		}
		set
		{
			this.mCurSelectPotionIndex = value;
		}
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x0005F5C4 File Offset: 0x0005D7C4
	public GameItem GetPotionItem()
	{
		return this.ItemBackPack.GetItemNoEmptyByIndexId(this.mCurSelectPotionIndex);
	}

	// Token: 0x17000287 RID: 647
	// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0005F5D8 File Offset: 0x0005D7D8
	// (set) Token: 0x06000E51 RID: 3665 RVA: 0x0005F5E8 File Offset: 0x0005D7E8
	public GameDefine.CAMP_TYPE PlayerCamp
	{
		get
		{
			return this.mMainPlayerAttrData.Camp;
		}
		set
		{
			this.mMainPlayerAttrData.Camp = value;
		}
	}

	// Token: 0x17000288 RID: 648
	// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0005F5F8 File Offset: 0x0005D7F8
	// (set) Token: 0x06000E53 RID: 3667 RVA: 0x0005F608 File Offset: 0x0005D808
	public int PlayerPkMode
	{
		get
		{
			return this.mMainPlayerAttrData.PkMode;
		}
		set
		{
			this.mMainPlayerAttrData.PkMode = value;
		}
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x0005F618 File Offset: 0x0005D818
	public void SetPKModeState(int pkMode)
	{
		if (pkMode != this.mMainPlayerAttrData.PkMode)
		{
			Singleton<ObjManager>.Instance.MainPlayer.SelectTarget(null);
		}
		this.mMainPlayerAttrData.PkMode = pkMode;
		if (SingletonUnity<TouXiangKuangLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TouXiangKuangLogic>.Instance.gameObject))
		{
			SingletonUnity<TouXiangKuangLogic>.Instance.UpdateStateBtn();
		}
	}

	// Token: 0x17000289 RID: 649
	// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0005F67C File Offset: 0x0005D87C
	// (set) Token: 0x06000E56 RID: 3670 RVA: 0x0005F684 File Offset: 0x0005D884
	public PlayerChatHistory ChatHistory
	{
		get
		{
			return this.mChatHistory;
		}
		set
		{
			this.mChatHistory = value;
		}
	}

	// Token: 0x1700028A RID: 650
	// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0005F690 File Offset: 0x0005D890
	// (set) Token: 0x06000E58 RID: 3672 RVA: 0x0005F698 File Offset: 0x0005D898
	public GameDefine.CHAT_CHANNEL_TYPE ChoosedChannelType
	{
		get
		{
			return this.mChoosedChannelType;
		}
		set
		{
			this.mChoosedChannelType = value;
		}
	}

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0005F6A4 File Offset: 0x0005D8A4
	public List<PlayerChatHistoryInfo> CurChannelChatHistoryList
	{
		get
		{
			return this.mChatHistory.ChannelChatHistoryList[(int)this.mChoosedChannelType];
		}
	}

	// Token: 0x1700028C RID: 652
	// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0005F6B8 File Offset: 0x0005D8B8
	// (set) Token: 0x06000E5B RID: 3675 RVA: 0x0005F6F0 File Offset: 0x0005D8F0
	public bool IsNeedTranslation
	{
		get
		{
			if (this.mIsInitTranslation)
			{
				return this.mIsNeedTranslation;
			}
			this.mIsNeedTranslation = LocalDataSaveManager.GetTranslationFlag();
			this.mIsInitTranslation = true;
			return this.mIsNeedTranslation;
		}
		set
		{
			this.mIsNeedTranslation = value;
		}
	}

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x06000E5C RID: 3676 RVA: 0x0005F6FC File Offset: 0x0005D8FC
	// (set) Token: 0x06000E5D RID: 3677 RVA: 0x0005F704 File Offset: 0x0005D904
	public RecentSpeakerRecord RecentSpeakers
	{
		get
		{
			return this.mRecentSpeakers;
		}
		set
		{
			this.mRecentSpeakers = value;
		}
	}

	// Token: 0x1700028E RID: 654
	// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0005F710 File Offset: 0x0005D910
	// (set) Token: 0x06000E5F RID: 3679 RVA: 0x0005F718 File Offset: 0x0005D918
	public List<consign_item> SaleNowList
	{
		get
		{
			return this.mSaleNowList;
		}
		set
		{
			this.mSaleNowList = value;
		}
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x0005F724 File Offset: 0x0005D924
	public void CancelSuccess(long id)
	{
		if (this.mSaleNowList != null)
		{
			for (int i = 0; i < this.mSaleNowList.Count; i++)
			{
				if (this.mSaleNowList[i].id == id)
				{
					this.mSaleNowList.RemoveAt(i);
					break;
				}
			}
		}
	}

	// Token: 0x1700028F RID: 655
	// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0005F780 File Offset: 0x0005D980
	// (set) Token: 0x06000E62 RID: 3682 RVA: 0x0005F788 File Offset: 0x0005D988
	public List<consign_item> BuyList
	{
		get
		{
			return this.mBuyList;
		}
		set
		{
			this.mBuyList = value;
		}
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x0005F794 File Offset: 0x0005D994
	public bool IsHaveGuild()
	{
		return this.mPlayerGuild != null && this.mPlayerGuild.IsGuildValid();
	}

	// Token: 0x17000290 RID: 656
	// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0005F7B0 File Offset: 0x0005D9B0
	// (set) Token: 0x06000E65 RID: 3685 RVA: 0x0005F7B8 File Offset: 0x0005D9B8
	public Guild PlayerGuild
	{
		get
		{
			return this.mPlayerGuild;
		}
		set
		{
			this.mPlayerGuild = value;
		}
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x0005F7C4 File Offset: 0x0005D9C4
	public bool isGuildChief()
	{
		return PlayerData.MainPlayerServerId == this.mPlayerGuild.GuildChiefId;
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0005F7D8 File Offset: 0x0005D9D8
	// (set) Token: 0x06000E68 RID: 3688 RVA: 0x0005F7E0 File Offset: 0x0005D9E0
	public long GuildContribute
	{
		get
		{
			return this.mGuildContribute;
		}
		set
		{
			this.mGuildContribute = value;
		}
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x0005F7EC File Offset: 0x0005D9EC
	public float GetGuildSkillProgress()
	{
		float num = 0f;
		Dictionary<long, guild_skill> guildSkills = this.PlayerGuild.GuildSkills;
		List<guild_skill> list = new List<guild_skill>(guildSkills.Values);
		for (int i = 0; i < list.Count; i++)
		{
			num += (float)list[i].level;
		}
		return num / (float)(list.Count * 10);
	}

	// Token: 0x17000292 RID: 658
	// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0005F850 File Offset: 0x0005DA50
	// (set) Token: 0x06000E6B RID: 3691 RVA: 0x0005F858 File Offset: 0x0005DA58
	public bool IsGuildBattleRedTeam
	{
		get
		{
			return this.mIsGuildBattleRedTeam;
		}
		set
		{
			this.mIsGuildBattleRedTeam = value;
		}
	}

	// Token: 0x06000E6C RID: 3692 RVA: 0x0005F864 File Offset: 0x0005DA64
	public bool IsInMainPlayerPartBundleIdList(string id)
	{
		return this.mMainPlayerPartBundleIdList.Contains(id);
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x0005F87C File Offset: 0x0005DA7C
	public void UpdateMainPlayerPartBundleIdList(string[] curModelIdList)
	{
		this.mMainPlayerPartBundleIdList.Clear();
		for (int i = 0; i < curModelIdList.Length; i++)
		{
			if (!string.IsNullOrEmpty(curModelIdList[i]))
			{
				ModelData modeDataByID = DataManager.GetModeDataByID(curModelIdList[i]);
				if (modeDataByID != null)
				{
					if (!this.mMainPlayerPartBundleIdList.Contains(modeDataByID.Name))
					{
						this.mMainPlayerPartBundleIdList.Add(modeDataByID.Name);
					}
					if (!string.IsNullOrEmpty(modeDataByID.ModelPath) && !this.mMainPlayerPartBundleIdList.Contains(modeDataByID.ModelPath))
					{
						this.mMainPlayerPartBundleIdList.Add(modeDataByID.ModelPath);
					}
				}
			}
		}
		if (!string.IsNullOrEmpty(this.MountId))
		{
			MountData mountDataById = DataManager.GetMountDataById(this.MountId);
			ModelData modeDataByID2 = DataManager.GetModeDataByID(mountDataById.ModelId);
			if (!this.mMainPlayerPartBundleIdList.Contains(modeDataByID2.Name))
			{
				this.mMainPlayerPartBundleIdList.Add(modeDataByID2.Name);
			}
			if (!string.IsNullOrEmpty(modeDataByID2.ModelPath) && !this.mMainPlayerPartBundleIdList.Contains(modeDataByID2.ModelPath))
			{
				this.mMainPlayerPartBundleIdList.Add(modeDataByID2.ModelPath);
			}
		}
	}

	// Token: 0x17000293 RID: 659
	// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0005F9AC File Offset: 0x0005DBAC
	// (set) Token: 0x06000E6F RID: 3695 RVA: 0x0005F9B4 File Offset: 0x0005DBB4
	public bool AutoUseDrag
	{
		get
		{
			return this.mAutoUseDrag;
		}
		set
		{
			this.mAutoUseDrag = value;
		}
	}

	// Token: 0x17000294 RID: 660
	// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0005F9C0 File Offset: 0x0005DBC0
	// (set) Token: 0x06000E71 RID: 3697 RVA: 0x0005F9C8 File Offset: 0x0005DBC8
	public float AutoUseDragThreshold
	{
		get
		{
			return this.mAutoUseDragThreshold;
		}
		set
		{
			this.mAutoUseDragThreshold = value;
		}
	}

	// Token: 0x17000295 RID: 661
	// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0005F9D4 File Offset: 0x0005DBD4
	// (set) Token: 0x06000E73 RID: 3699 RVA: 0x0005F9DC File Offset: 0x0005DBDC
	public bool AutoUseSort
	{
		get
		{
			return this.mAutoUseSort;
		}
		set
		{
			this.mAutoUseSort = value;
		}
	}

	// Token: 0x17000296 RID: 662
	// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0005F9E8 File Offset: 0x0005DBE8
	// (set) Token: 0x06000E75 RID: 3701 RVA: 0x0005F9F0 File Offset: 0x0005DBF0
	public bool AutoComabat
	{
		get
		{
			return this.mAutoComabat;
		}
		set
		{
			this.mAutoComabat = value;
		}
	}

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x06000E76 RID: 3702 RVA: 0x0005F9FC File Offset: 0x0005DBFC
	// (set) Token: 0x06000E77 RID: 3703 RVA: 0x0005FA04 File Offset: 0x0005DC04
	public bool IsOpenAutoCombat
	{
		get
		{
			return this.mIsOpenAutoCombat;
		}
		set
		{
			this.mIsOpenAutoCombat = value;
		}
	}

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06000E78 RID: 3704 RVA: 0x0005FA10 File Offset: 0x0005DC10
	// (set) Token: 0x06000E79 RID: 3705 RVA: 0x0005FA18 File Offset: 0x0005DC18
	public float BreakAutoCombatTime
	{
		get
		{
			return this.mBreakAutoCombatTime;
		}
		set
		{
			this.mBreakAutoCombatTime = value;
		}
	}

	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0005FA24 File Offset: 0x0005DC24
	// (set) Token: 0x06000E7B RID: 3707 RVA: 0x0005FA2C File Offset: 0x0005DC2C
	public PlayerDanceData PlayerDanceData
	{
		get
		{
			return this.mPlayerDanceData;
		}
		set
		{
			this.mPlayerDanceData = value;
		}
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x0005FA38 File Offset: 0x0005DC38
	public void ResetPlayerData()
	{
		this.mItemBackPack.ClearContainer();
		this.mEquipPack.ClearContainer();
		this.mBadgeBackPack.ClearContainer();
		this.mBadgeEquipPack.ClearContainer();
		this.mEquipBackPack.ClearContainer();
		this.mFashionBackPack.ClearContainer();
		this.mFashionEquipPack.ClearContainer();
		this.CleanUpSkillDataList();
		this.mChatHistory.Reset();
		this.mRecentSpeakers.Reset();
		this.mFriendInfo.Init();
		this.mTeam.Reset();
		this.mPlayerDanceData.Reset();
		this.mMainPlayerPartBundleIdList.Clear();
		this.mPlayerGuild.ResetGuild();
		this.InitOptionValue();
		this.mAutoComabat = false;
		this.mIsOpenAutoCombat = false;
		this.mAutoUseDrag = true;
		this.IsFinishDownload = false;
		this.IsTutorialFinish = false;
		this.mMountId = string.Empty;
		this.mActivityData.Reset();
		this.mWelfareData.Reset();
		this.mRankPVPData.Reset();
		this.mCopyInfoData.Reset();
		this.mTowerData.Reset();
		UIManager.Reset();
		this.mPreTeamInviteDic.Clear();
		this.Domin_InfoDic.Clear();
		this.Domin_CharacterDic.Clear();
		MissionTeamTipLogic.mCurPage = -1;
	}

	// Token: 0x04000B77 RID: 2935
	private const string PLAYER_ACCOUNT_ID = "player_account_id";

	// Token: 0x04000B78 RID: 2936
	private const string PLAYER_ACCOUNT_KEY = "player_account_key";

	// Token: 0x04000B79 RID: 2937
	public const string LoginIP = "s16.serv00.com";

	// Token: 0x04000B7A RID: 2938
	public const string LoginIpClassic = "s16.serv00.com";

	// Token: 0x04000B7B RID: 2939
	public const int loginPort = 9777;

	// Token: 0x04000B7C RID: 2940
	public const int gamePort = 48282;

	// Token: 0x04000B7D RID: 2941
	public const int MAX_STR_COUNT = 1000;

	// Token: 0x04000B7E RID: 2942
	private const float DragCDTime = 20f;

	// Token: 0x04000B7F RID: 2943
	public static ServerInfoData CurLoginServerData;

	// Token: 0x04000B80 RID: 2944
	public static game_server CurGameServerData;

	// Token: 0x04000B81 RID: 2945
	public static int LocalDataVersion;

	// Token: 0x04000B82 RID: 2946
	public static int ServerDataVersion;

	// Token: 0x04000B83 RID: 2947
	public bool IsFinishDownload = true;

	// Token: 0x04000B84 RID: 2948
	public static string FaceBookId;

	// Token: 0x04000B85 RID: 2949
	public static string FacekBookToken;

	// Token: 0x04000B86 RID: 2950
	public Vector3 MainPlayerStartPos = Vector3.zero;

	// Token: 0x04000B87 RID: 2951
	public Vector3 MainPlayerStartDir = Vector3.zero;

	// Token: 0x04000B88 RID: 2952
	private static long mMainPlayerServerId = -1L;

	// Token: 0x04000B89 RID: 2953
	private bool mIsTutorialFinish;

	// Token: 0x04000B8A RID: 2954
	private bool mAutoComabat;

	// Token: 0x04000B8B RID: 2955
	private float mBreakAutoCombatTime;

	// Token: 0x04000B8C RID: 2956
	private bool mIsOpenAutoCombat;

	// Token: 0x04000B8D RID: 2957
	private bool mAutoUseDrag = true;

	// Token: 0x04000B8E RID: 2958
	private float mAutoUseDragThreshold = 0.5f;

	// Token: 0x04000B8F RID: 2959
	private bool mAutoUseSort = true;

	// Token: 0x04000B90 RID: 2960
	public static long session = 0L;

	// Token: 0x04000B91 RID: 2961
	public static long session2 = 0L;

	// Token: 0x04000B92 RID: 2962
	public static int loginType = 1;

	// Token: 0x04000B93 RID: 2963
	public static int downLoadFlag = 0;

	// Token: 0x04000B94 RID: 2964
	public static long FaceBookBind = -1L;

	// Token: 0x04000B95 RID: 2965
	public static string FaceBookBind2 = string.Empty;

	// Token: 0x04000B96 RID: 2966
	private float mMusicDragValue = 1f;

	// Token: 0x04000B97 RID: 2967
	private float mSoundDragValue = 1f;

	// Token: 0x04000B98 RID: 2968
	private int mGraphicsQua = 1;

	// Token: 0x04000B99 RID: 2969
	private int mStreetRacingMode = 1;

	// Token: 0x04000B9A RID: 2970
	private int mScreen_Vibrating;

	// Token: 0x04000B9B RID: 2971
	private int mNotify = 1;

	// Token: 0x04000B9C RID: 2972
	private int mChannelValue;

	// Token: 0x04000B9D RID: 2973
	private int mChatShow = 1;

	// Token: 0x04000B9E RID: 2974
	private int mNonMissionTarget = 1;

	// Token: 0x04000B9F RID: 2975
	private int mVehicleTarget = 1;

	// Token: 0x04000BA0 RID: 2976
	private float mfCopySceneChange;

	// Token: 0x04000BA1 RID: 2977
	public int SkillIndex;

	// Token: 0x04000BA2 RID: 2978
	private List<CharacterSkillData> mMainPlayerSkillDataList = new List<CharacterSkillData>();

	// Token: 0x04000BA3 RID: 2979
	private List<string> mMainPlayerSkillIDList = new List<string>();

	// Token: 0x04000BA4 RID: 2980
	private FriendInfo mFriendInfo;

	// Token: 0x04000BA5 RID: 2981
	private long mVideoTimes;

	// Token: 0x04000BA6 RID: 2982
	private long mVideoMaxTimes;

	// Token: 0x04000BA7 RID: 2983
	private long mVideoDiamond;

	// Token: 0x04000BA8 RID: 2984
	private float mDragCdTime = -1f;

	// Token: 0x04000BA9 RID: 2985
	private PROFESSION_TYPE mProfession;

	// Token: 0x04000BAA RID: 2986
	private CharacterAttributeData mMainPlayerAttrData = new CharacterAttributeData();

	// Token: 0x04000BAB RID: 2987
	private CharacterAttributeData mOtherPlayerAttriData = new CharacterAttributeData();

	// Token: 0x04000BAC RID: 2988
	private PlayerRankPVPData mRankPVPData = new PlayerRankPVPData();

	// Token: 0x04000BAD RID: 2989
	private TowerInfoData mTowerData = new TowerInfoData();

	// Token: 0x04000BAE RID: 2990
	private TargetBasicInfo mSelectTargetBasicInfo = new TargetBasicInfo();

	// Token: 0x04000BAF RID: 2991
	private CameraController.CAMERAVIEWSTATE mViewType = CameraController.CAMERAVIEWSTATE.FREE;

	// Token: 0x04000BB0 RID: 2992
	private string mCharacterModelId;

	// Token: 0x04000BB1 RID: 2993
	private CharacterModelData mCharacterModelData;

	// Token: 0x04000BB2 RID: 2994
	private string mPartWeaponId = string.Empty;

	// Token: 0x04000BB3 RID: 2995
	private string mPartHeadId = string.Empty;

	// Token: 0x04000BB4 RID: 2996
	private string mPartBodyId = string.Empty;

	// Token: 0x04000BB5 RID: 2997
	private string mPartLegId = string.Empty;

	// Token: 0x04000BB6 RID: 2998
	private string mFashionHeadId = string.Empty;

	// Token: 0x04000BB7 RID: 2999
	private string mFashionBodyId = string.Empty;

	// Token: 0x04000BB8 RID: 3000
	private string mFashionLegId = string.Empty;

	// Token: 0x04000BB9 RID: 3001
	private string mFashionWeaponId = string.Empty;

	// Token: 0x04000BBA RID: 3002
	private string mWeaponItemId = string.Empty;

	// Token: 0x04000BBB RID: 3003
	private string mFashionItemId = string.Empty;

	// Token: 0x04000BBC RID: 3004
	private bool mIsShowFashion;

	// Token: 0x04000BBD RID: 3005
	private bool mIsServerRidingMount;

	// Token: 0x04000BBE RID: 3006
	private string mMountId = string.Empty;

	// Token: 0x04000BBF RID: 3007
	private string mMountColor = string.Empty;

	// Token: 0x04000BC0 RID: 3008
	private long mCash;

	// Token: 0x04000BC1 RID: 3009
	private long mGold;

	// Token: 0x04000BC2 RID: 3010
	private long mDiamond;

	// Token: 0x04000BC3 RID: 3011
	private long mBattleCoin;

	// Token: 0x04000BC4 RID: 3012
	private long mActivityCoin;

	// Token: 0x04000BC5 RID: 3013
	private ItemContainer mItemBackPack;

	// Token: 0x04000BC6 RID: 3014
	private ItemContainer mEquipPack;

	// Token: 0x04000BC7 RID: 3015
	private ItemContainer mBadgeEquipPack;

	// Token: 0x04000BC8 RID: 3016
	private ItemContainer mBadgeBackPack;

	// Token: 0x04000BC9 RID: 3017
	private ItemContainer mEquipBackPack;

	// Token: 0x04000BCA RID: 3018
	private ItemContainer mFashionBackPack;

	// Token: 0x04000BCB RID: 3019
	private ItemContainer mFashionEquipPack;

	// Token: 0x04000BCC RID: 3020
	public Dictionary<string, domin_info> Domin_InfoDic = new Dictionary<string, domin_info>();

	// Token: 0x04000BCD RID: 3021
	public Dictionary<long, character_look> Domin_CharacterDic = new Dictionary<long, character_look>();

	// Token: 0x04000BCE RID: 3022
	private Team mTeam;

	// Token: 0x04000BCF RID: 3023
	private static float TEAM_INVITE_INTERVAL = 10f;

	// Token: 0x04000BD0 RID: 3024
	private Dictionary<long, float> mPreTeamInviteDic = new Dictionary<long, float>();

	// Token: 0x04000BD1 RID: 3025
	public Dictionary<string, shop_item> ShopDict;

	// Token: 0x04000BD2 RID: 3026
	public GameDefine.SHOP_TYPE ShopType;

	// Token: 0x04000BD3 RID: 3027
	private CopyData mCopyInfoData = new CopyData();

	// Token: 0x04000BD4 RID: 3028
	private ActivityData mActivityData = new ActivityData();

	// Token: 0x04000BD5 RID: 3029
	private PlayerSlotData mPlayerSlotData = new PlayerSlotData();

	// Token: 0x04000BD6 RID: 3030
	private WelfareData mWelfareData = new WelfareData();

	// Token: 0x04000BD7 RID: 3031
	private PlayerMountData mPlayerMountData = new PlayerMountData();

	// Token: 0x04000BD8 RID: 3032
	private int mCurLineIndex;

	// Token: 0x04000BD9 RID: 3033
	private int mLineCount;

	// Token: 0x04000BDA RID: 3034
	private List<long> lineStates;

	// Token: 0x04000BDB RID: 3035
	private long mCurSelectPotionIndex = -1L;

	// Token: 0x04000BDC RID: 3036
	private GameDefine.CAMP_TYPE mPlayerCamp;

	// Token: 0x04000BDD RID: 3037
	private PlayerChatHistory mChatHistory;

	// Token: 0x04000BDE RID: 3038
	private GameDefine.CHAT_CHANNEL_TYPE mChoosedChannelType = GameDefine.CHAT_CHANNEL_TYPE.WORLD;

	// Token: 0x04000BDF RID: 3039
	private bool mIsInitTranslation;

	// Token: 0x04000BE0 RID: 3040
	private bool mIsNeedTranslation;

	// Token: 0x04000BE1 RID: 3041
	private RecentSpeakerRecord mRecentSpeakers;

	// Token: 0x04000BE2 RID: 3042
	private List<consign_item> mSaleNowList;

	// Token: 0x04000BE3 RID: 3043
	private List<consign_item> mBuyList;

	// Token: 0x04000BE4 RID: 3044
	private Guild mPlayerGuild;

	// Token: 0x04000BE5 RID: 3045
	private long mGuildContribute;

	// Token: 0x04000BE6 RID: 3046
	private bool mIsGuildBattleRedTeam;

	// Token: 0x04000BE7 RID: 3047
	private List<string> mMainPlayerPartBundleIdList = new List<string>();

	// Token: 0x04000BE8 RID: 3048
	private PlayerDanceData mPlayerDanceData;
}
