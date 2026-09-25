using System;
using UnityEngine;

// Token: 0x0200013C RID: 316
public class LocalDataSaveManager
{
	// Token: 0x1700022F RID: 559
	// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0005B504 File Offset: 0x00059704
	public static bool AdFree
	{
		get
		{
			return LocalDataSaveManager.IsADFree == 1;
		}
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x0005B514 File Offset: 0x00059714
	public static int GetADFreeFlag()
	{
		LocalDataSaveManager.IsADFree = PlayerPrefs.GetInt("ADFreeFlag", -1);
		return LocalDataSaveManager.IsADFree;
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x0005B52C File Offset: 0x0005972C
	public static void SetADFreeFlag(int value)
	{
		if (LocalDataSaveManager.IsADFree != value)
		{
			LocalDataSaveManager.IsADFree = value;
			PlayerPrefs.SetInt("ADFreeFlag", value);
		}
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x0005B54C File Offset: 0x0005974C
	public static bool GetFaceBookPopTips(int type)
	{
		return PlayerPrefs.GetInt(LocalDataSaveManager.keyPopFaceBookCount + type, 0) <= 4;
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x0005B56C File Offset: 0x0005976C
	public static bool IsFirstEnterGame()
	{
		bool flag = PlayerPrefs.GetInt(LocalDataSaveManager.keyFirstEnterGame, 0) == 0;
		if (flag)
		{
			PlayerPrefs.SetInt(LocalDataSaveManager.keyFirstEnterGame, 1);
		}
		return flag;
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x0005B59C File Offset: 0x0005979C
	public static void SetFaceBookPopCount(int type)
	{
		int @int = PlayerPrefs.GetInt(LocalDataSaveManager.keyPopFaceBookCount + type, 0);
		PlayerPrefs.SetInt(LocalDataSaveManager.keyPopFaceBookCount + type, @int + 1);
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x0005B5D8 File Offset: 0x000597D8
	public static void SetFaceBookBindTime()
	{
		DateTime dateTime;
		dateTime..ctor(1970, 1, 1);
		int num = (int)(DateTime.Now - dateTime).TotalDays;
		PlayerPrefs.SetInt(LocalDataSaveManager.keyPopFaceBookTime, num);
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0005B614 File Offset: 0x00059814
	public static bool GetFaceBookNextPopDay(int day = 3)
	{
		int @int = PlayerPrefs.GetInt(LocalDataSaveManager.keyPopFaceBookTime, 0);
		DateTime dateTime;
		dateTime..ctor(1970, 1, 1);
		int num = (int)(DateTime.Now - dateTime).TotalDays;
		return num - @int >= day;
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x0005B65C File Offset: 0x0005985C
	public static void SetChooseRoleIndex(int i)
	{
		PlayerPrefs.SetInt("ChooseRoleIndex", i);
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0005B66C File Offset: 0x0005986C
	public static int GetChooseRoleIndex()
	{
		return PlayerPrefs.GetInt("ChooseRoleIndex", 0);
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x0005B67C File Offset: 0x0005987C
	public static bool IsTutorialClose()
	{
		return PlayerPrefs.GetInt("IsTutorialClose", 0) != 0;
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x0005B694 File Offset: 0x00059894
	public static void SetTutorialCLose(bool isClose)
	{
		if (isClose)
		{
			PlayerPrefs.SetInt("IsTutorialClose", 1);
		}
		else
		{
			PlayerPrefs.SetInt("IsTutorialClose", 0);
		}
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x0005B6B8 File Offset: 0x000598B8
	public static void SetCameraViewType(long serverId, CameraController.CAMERAVIEWSTATE type)
	{
		PlayerPrefs.SetInt(string.Format("CameraViewType{0}", serverId), (int)type);
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x0005B6D0 File Offset: 0x000598D0
	public static CameraController.CAMERAVIEWSTATE GetCameraViewType(long serverId)
	{
		int num = PlayerPrefs.GetInt(string.Format("CameraViewType{0}", serverId), 1);
		if (num > 1)
		{
			num = 1;
		}
		return (CameraController.CAMERAVIEWSTATE)num;
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x0005B700 File Offset: 0x00059900
	public static void SetkeySystemUseDrag(float value)
	{
		PlayerPrefs.SetFloat(LocalDataSaveManager.keySystemUseDrag, value);
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x0005B710 File Offset: 0x00059910
	public static float GetkeySystemUseDrag()
	{
		return PlayerPrefs.GetFloat(LocalDataSaveManager.keySystemUseDrag, 0.5f);
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x0005B724 File Offset: 0x00059924
	public static void SetSystemMusic(int value)
	{
		PlayerPrefs.SetInt(LocalDataSaveManager.keySystemMusic, value);
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x0005B734 File Offset: 0x00059934
	public static int GetSystemMusic()
	{
		return PlayerPrefs.GetInt(LocalDataSaveManager.keySystemMusic, 1);
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x0005B744 File Offset: 0x00059944
	public static void SetSystemSoundEffect(int value)
	{
		PlayerPrefs.SetInt(LocalDataSaveManager.keySystemSoundEffect, value);
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0005B754 File Offset: 0x00059954
	public static int GetSystemSoundEffect()
	{
		return PlayerPrefs.GetInt(LocalDataSaveManager.keySystemSoundEffect, 1);
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0005B764 File Offset: 0x00059964
	public static void SetmusicDrag(float value)
	{
		PlayerPrefs.SetFloat(LocalDataSaveManager.musicDrag, value);
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x0005B774 File Offset: 0x00059974
	public static float GetmusicDrag()
	{
		return PlayerPrefs.GetFloat(LocalDataSaveManager.musicDrag, 1f);
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x0005B788 File Offset: 0x00059988
	public static void SetsoundDrag(float value)
	{
		PlayerPrefs.SetFloat(LocalDataSaveManager.soundDrag, value);
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x0005B798 File Offset: 0x00059998
	public static float GetsoundDrag()
	{
		return PlayerPrefs.GetFloat(LocalDataSaveManager.soundDrag, 1f);
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x0005B7AC File Offset: 0x000599AC
	public static void SetplayerOnScreen(float value)
	{
		PlayerPrefs.SetFloat(LocalDataSaveManager.playerOnScreen, value);
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x0005B7BC File Offset: 0x000599BC
	public static float GetplayerOnScreen()
	{
		return PlayerPrefs.GetFloat(LocalDataSaveManager.playerOnScreen, 0.5f);
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x0005B7D0 File Offset: 0x000599D0
	public static void SetGraphics(int value)
	{
		PlayerPrefs.SetInt(LocalDataSaveManager.Graphics, value);
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x0005B7E0 File Offset: 0x000599E0
	public static int GetGraphics(int default1)
	{
		return PlayerPrefs.GetInt(LocalDataSaveManager.Graphics, default1);
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x0005B7F0 File Offset: 0x000599F0
	public static void SetStreetModel(int value)
	{
		PlayerPrefs.SetInt(LocalDataSaveManager.StreetModel, value);
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x0005B800 File Offset: 0x00059A00
	public static int GetStreetModel()
	{
		return PlayerPrefs.GetInt(LocalDataSaveManager.StreetModel, 1);
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x0005B810 File Offset: 0x00059A10
	public static void SetScreenVibrat(int value)
	{
		PlayerPrefs.SetInt(LocalDataSaveManager.ScreenVibrat, value);
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x0005B820 File Offset: 0x00059A20
	public static int GetScreenVibrat()
	{
		return PlayerPrefs.GetInt(LocalDataSaveManager.ScreenVibrat, 1);
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x0005B830 File Offset: 0x00059A30
	public static void Setnotyfition(int value)
	{
		PlayerPrefs.SetInt(LocalDataSaveManager.notyfition, value);
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x0005B840 File Offset: 0x00059A40
	public static int Getnotyfition()
	{
		return PlayerPrefs.GetInt(LocalDataSaveManager.notyfition, 1);
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x0005B850 File Offset: 0x00059A50
	public static string GetTestLastLoginIP()
	{
		return PlayerPrefs.GetString(LocalDataSaveManager.TestLastLoginIP, "ec2-52-91-30-19.compute-1.amazonaws.com");
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x0005B864 File Offset: 0x00059A64
	public static void SetTestLastLoginIP(string ip)
	{
		PlayerPrefs.SetString(LocalDataSaveManager.TestLastLoginIP, ip);
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x0005B874 File Offset: 0x00059A74
	public static string GetTestLastGameIP()
	{
		return PlayerPrefs.GetString(LocalDataSaveManager.TestLastGameIP, "ec2-52-91-30-19.compute-1.amazonaws.com");
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x0005B888 File Offset: 0x00059A88
	public static void SetTestLastGameIP(string ip)
	{
		PlayerPrefs.SetString(LocalDataSaveManager.TestLastGameIP, ip);
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x0005B898 File Offset: 0x00059A98
	public static string GetTestLastServerName()
	{
		return PlayerPrefs.GetString(LocalDataSaveManager.TestLastServerName, "Liu");
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x0005B8AC File Offset: 0x00059AAC
	public static void SetTestLastServerName(string name)
	{
		PlayerPrefs.SetString(LocalDataSaveManager.TestLastServerName, name);
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x0005B8BC File Offset: 0x00059ABC
	public static int GetIsUseDnsTest()
	{
		return PlayerPrefs.GetInt(LocalDataSaveManager.TestIsUseDns, 1);
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x0005B8CC File Offset: 0x00059ACC
	public static void SetIsUseDnsTest(bool isUseDns)
	{
		PlayerPrefs.SetInt(LocalDataSaveManager.TestIsUseDns, (!isUseDns) ? 0 : 1);
	}

	// Token: 0x06000D27 RID: 3367 RVA: 0x0005B8E8 File Offset: 0x00059AE8
	public static string GetNoticeVersion()
	{
		return PlayerPrefs.GetString("NoticeVersion", string.Empty);
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x0005B8FC File Offset: 0x00059AFC
	public static void SetNoticeVersion(string version)
	{
		PlayerPrefs.SetString("NoticeVersion", version);
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x0005B90C File Offset: 0x00059B0C
	public static int GetShowStrongerFlag()
	{
		return PlayerPrefs.GetInt("StrongerTimes" + PlayerData.MainPlayerServerId, 10);
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x0005B92C File Offset: 0x00059B2C
	public static void SetStrongerFlag(int val)
	{
		PlayerPrefs.SetInt("StrongerTimes" + PlayerData.MainPlayerServerId, val);
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x0005B948 File Offset: 0x00059B48
	public static int GetDiedFlag()
	{
		return PlayerPrefs.GetInt("diedflag" + PlayerData.MainPlayerServerId, 0);
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x0005B964 File Offset: 0x00059B64
	public static void SetDiedFlag(int val)
	{
		PlayerPrefs.SetInt("diedflag" + PlayerData.MainPlayerServerId, val);
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x0005B980 File Offset: 0x00059B80
	public static int GetRateFlag()
	{
		return PlayerPrefs.GetInt("rateflag", 1);
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x0005B990 File Offset: 0x00059B90
	public static void SetRateFlag(int val)
	{
		PlayerPrefs.SetInt("rateflag", val);
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x0005B9A0 File Offset: 0x00059BA0
	public static string GetLoadindex()
	{
		return PlayerPrefs.GetString("Loadtextureindex", "1");
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x0005B9B4 File Offset: 0x00059BB4
	public static void SetLoadindex(string value)
	{
		PlayerPrefs.SetString("Loadtextureindex", value);
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0005B9C4 File Offset: 0x00059BC4
	public static string GetTimeOffset()
	{
		return PlayerPrefs.GetString("LocalTimeOffset", "-1");
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x0005B9D8 File Offset: 0x00059BD8
	public static void SetTimeOffset(long timeoffset)
	{
		PlayerPrefs.SetString("LocalTimeOffset", timeoffset.ToString());
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x0005B9EC File Offset: 0x00059BEC
	public static int GetRewardFlag()
	{
		LocalDataSaveManager.RewardFlag = PlayerPrefs.GetInt("RewardFlag", 0);
		return LocalDataSaveManager.RewardFlag;
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x0005BA04 File Offset: 0x00059C04
	public static void SetRewardFlag(int val)
	{
		LocalDataSaveManager.RewardFlag = 0;
		if ((val & 1) != 0)
		{
			LocalDataSaveManager.RewardFlag++;
		}
		if ((val & 2) != 0)
		{
			LocalDataSaveManager.RewardFlag++;
		}
		PlayerPrefs.SetInt("RewardFlag", LocalDataSaveManager.RewardFlag);
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x0005BA44 File Offset: 0x00059C44
	public static void SetRewardFlag()
	{
		LocalDataSaveManager.RewardFlag--;
		if (LocalDataSaveManager.RewardFlag < 0)
		{
			LocalDataSaveManager.RewardFlag = 0;
		}
		PlayerPrefs.SetInt("RewardFlag", LocalDataSaveManager.RewardFlag);
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x0005BA80 File Offset: 0x00059C80
	public static int GetPhoneStateFlag()
	{
		return PlayerPrefs.GetInt("PhoneStateFlag", 0);
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x0005BA90 File Offset: 0x00059C90
	public static void SetPhoneStateFlag()
	{
		PlayerPrefs.SetInt("PhoneStateFlag", 1);
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0005BAA0 File Offset: 0x00059CA0
	public static bool GetNeedCountAutoDownload()
	{
		int @int = PlayerPrefs.GetInt("NeedCountAutoDownload", 0);
		if (@int != 0)
		{
			return @int == 1;
		}
		if (LocalDataSaveManager.GetPhoneStateFlag() == 1)
		{
			PlayerPrefs.SetInt("NeedCountAutoDownload", 2);
			return false;
		}
		PlayerPrefs.SetInt("NeedCountAutoDownload", 1);
		return true;
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x0005BAF0 File Offset: 0x00059CF0
	public static bool GetAutoDownloadFlag()
	{
		return false;
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x0005BAF4 File Offset: 0x00059CF4
	public static bool GetTranslationFlag()
	{
		return PlayerPrefs.GetInt("TranslationFlag", (!GameManager.IsEnglishLanguage()) ? 0 : 1) == 1;
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x0005BB14 File Offset: 0x00059D14
	public static void SetTranslationFlag(bool isNeedTranslate)
	{
		if (isNeedTranslate)
		{
			PlayerPrefs.SetInt("TranslationFlag", 1);
		}
		else
		{
			PlayerPrefs.SetInt("TranslationFlag", 0);
		}
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x0005BB38 File Offset: 0x00059D38
	public static void SetLastViewDistance(float scale)
	{
		PlayerPrefs.SetFloat("LastViewDistance", scale);
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x0005BB48 File Offset: 0x00059D48
	public static float GetLastViewDistance()
	{
		return PlayerPrefs.GetFloat("LastViewDistance", 0.7f);
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x0005BB5C File Offset: 0x00059D5C
	public static void SetChannelShow(int value)
	{
		PlayerPrefs.SetInt("ChannelSet" + PlayerData.MainPlayerServerId, value);
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x0005BB78 File Offset: 0x00059D78
	public static int GetChannelShow()
	{
		return PlayerPrefs.GetInt("ChannelSet" + PlayerData.MainPlayerServerId, 63);
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x0005BB98 File Offset: 0x00059D98
	public static void SetChatShow(int value)
	{
		PlayerPrefs.SetInt("ChatShow" + PlayerData.MainPlayerServerId, value);
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x0005BBB4 File Offset: 0x00059DB4
	public static int GetChatShow()
	{
		return PlayerPrefs.GetInt("ChatShow" + PlayerData.MainPlayerServerId, 1);
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x0005BBD0 File Offset: 0x00059DD0
	public static bool IsNeedCountDownload()
	{
		return PlayerPrefs.GetInt("StartDownloadFlag", 0) == 0;
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x0005BBE0 File Offset: 0x00059DE0
	public static void SetDownloadCountFinish()
	{
		PlayerPrefs.SetInt("StartDownloadFlag", 1);
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x0005BBF0 File Offset: 0x00059DF0
	public static bool IsNeedCountDownloadFinish()
	{
		return PlayerPrefs.GetInt("FinishDownloadFlag", 0) == 0;
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x0005BC00 File Offset: 0x00059E00
	public static void SetDownloadFinishCountFinish()
	{
		PlayerPrefs.SetInt("FinishDownloadFlag", 1);
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x0005BC10 File Offset: 0x00059E10
	public static void SetNonMissionTarget(int value)
	{
		PlayerPrefs.SetInt("NonMissionTarget", value);
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x0005BC20 File Offset: 0x00059E20
	public static int GetNonMissionTarget()
	{
		return PlayerPrefs.GetInt("NonMissionTarget", 1);
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x0005BC30 File Offset: 0x00059E30
	public static void SetVehicleTarget(int value)
	{
		PlayerPrefs.SetInt("VehicleTarget", value);
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x0005BC40 File Offset: 0x00059E40
	public static int GetVehicleTarget()
	{
		return PlayerPrefs.GetInt("VehicleTarget", 1);
	}

	// Token: 0x04000B42 RID: 2882
	private static string keySystemUseDrag = "keySystemUseDrag";

	// Token: 0x04000B43 RID: 2883
	private static string keySystemMusic = "SystemMusic";

	// Token: 0x04000B44 RID: 2884
	private static string keySystemSoundEffect = "SystemSoundEffect";

	// Token: 0x04000B45 RID: 2885
	private static string musicDrag = "musicVolumeDrag";

	// Token: 0x04000B46 RID: 2886
	private static string soundDrag = "soundVolumeDrag";

	// Token: 0x04000B47 RID: 2887
	private static string playerOnScreen = "PlayerOnScreen";

	// Token: 0x04000B48 RID: 2888
	private static string Graphics = "GraphicsQuality";

	// Token: 0x04000B49 RID: 2889
	private static string StreetModel = "StreetModel";

	// Token: 0x04000B4A RID: 2890
	private static string ScreenVibrat = "ScreenVibrating";

	// Token: 0x04000B4B RID: 2891
	private static string notyfition = "Notification";

	// Token: 0x04000B4C RID: 2892
	private static string TestLastGameIP = "TestLastGameIP";

	// Token: 0x04000B4D RID: 2893
	private static string TestLastLoginIP = "TestLastLoginIP";

	// Token: 0x04000B4E RID: 2894
	private static string TestLastServerName = "TestLastServerName";

	// Token: 0x04000B4F RID: 2895
	private static string TestIsUseDns = "TestIsUseDns";

	// Token: 0x04000B50 RID: 2896
	public static bool ShowDailyBuyTips = true;

	// Token: 0x04000B51 RID: 2897
	public static bool ShowInvestTips = true;

	// Token: 0x04000B52 RID: 2898
	private static string keyPopFaceBookTime = "keyPopFaceBookTime";

	// Token: 0x04000B53 RID: 2899
	private static string keyPopFaceBookCount = "keyPopFaceBookCount";

	// Token: 0x04000B54 RID: 2900
	private static string keyFirstEnterGame = "keyFirstEnterGame";

	// Token: 0x04000B55 RID: 2901
	private static int RewardFlag;

	// Token: 0x04000B56 RID: 2902
	public static int IsADFree = -1;
}
