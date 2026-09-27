using System;
using UnityEngine;

// Token: 0x020001FF RID: 511
public class GameSettingData
{
	// Token: 0x06001162 RID: 4450 RVA: 0x00070B94 File Offset: 0x0006ED94
	// Note: this type is marked as 'beforefieldinit'.
	static GameSettingData()
	{
		bool[] array = new bool[3];
		array[0] = true;
		GameSettingData.IsBundleNeedUnload = array;
		GameSettingData.IsShowFashionEffect = new bool[]
		{
			default(bool),
			true,
			true
		};
		GameSettingData.IsShowSkillEffect = new bool[]
		{
			true,
			true,
			true
		};
		GameSettingData.IsShowPlayerShadow = new bool[]
		{
			default(bool),
			true,
			true
		};
		GameSettingData.IsSoundCanPlay = new bool[]
		{
			default(bool),
			true,
			true
		};
		GameSettingData.IsCarCopyEffectEnable = new bool[]
		{
			default(bool),
			true,
			true
		};
		GameSettingData.DownloadThreadCount = new int[]
		{
			2,
			10,
			10
		};
		GameSettingData.IsShowNormalNpcName = false;
		GameSettingData.IsTutorialClose = false;
		GameSettingData.IsLowPhone = false;
		GameSettingData.PlayerLevel = 0;
	}

	// Token: 0x170003A9 RID: 937
	// (set) Token: 0x06001163 RID: 4451 RVA: 0x00070D24 File Offset: 0x0006EF24
	public static int PhoneClass
	{
		set
		{
			GameSettingData.mPhoneClass = value;
		}
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x00070D2C File Offset: 0x0006EF2C
	public static int GetPhoneClass()
	{
		if (GameSettingData.mPhoneClass == 0)
		{
			return 0;
		}
		if (GameSettingData.IsLowPhone)
		{
			return 0;
		}
		return Mathf.Max(1, GameSettingData.PlayerLevel);
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x00070D54 File Offset: 0x0006EF54
	public static int InitGraphic()
	{
		int height = Screen.height;
		int width = Screen.width;
		int systemMemorySize = SystemInfo.systemMemorySize;
		int graphicsMemorySize = SystemInfo.graphicsMemorySize;
		GameSettingData.IsLowPhone = false;
		if (height < 480 || width < 800 || systemMemorySize < 512 || graphicsMemorySize <= 100)
		{
			GameSettingData.PlayerLevel = 0;
			return 0;
		}
		GameSettingData.PlayerLevel = 2;
		if (systemMemorySize < 1000 || graphicsMemorySize < 128)
		{
			GameSettingData.PlayerLevel = 1;
		}
		return 1;
	}

	// Token: 0x04001727 RID: 5927
	public static bool IsLocalTestServer = false;

	// Token: 0x04001728 RID: 5928
	public static int LocalTestServerID = 0;

	// Token: 0x04001729 RID: 5929
	public static bool IsTestBilling = false;

	// Token: 0x0400172A RID: 5930
	public static bool IsDownLoadInLocal = false;

	// Token: 0x0400172B RID: 5931
	public static string UnityVersion = "Unity4.7";

	// Token: 0x0400172C RID: 5932
	public static string GameVersion = "1.012.017";

	// Token: 0x0400172D RID: 5933
	public static float DefaultCrossInTime = 0.1f;

	// Token: 0x0400172E RID: 5934
	public static float DefaultCrossOutTime = 0.1f;

	// Token: 0x0400172F RID: 5935
	private static int mPhoneClass = 1;

	// Token: 0x04001730 RID: 5936
	public static int[] MaxOtherPlayerVisibleNum = new int[]
	{
		3,
		7,
		14
	};

	// Token: 0x04001731 RID: 5937
	public static int[] MaxOtherPlayerLogicNum = new int[]
	{
		6,
		12,
		20
	};

	// Token: 0x04001732 RID: 5938
	public static int[] MaxOtherPlayerPoolNum = new int[]
	{
		1,
		3,
		5
	};

	// Token: 0x04001733 RID: 5939
	public static int MaxRecentSpeakerNum = 10;

	// Token: 0x04001734 RID: 5940
	public static int MinPlayerLevelInWorldSpeak = 0;

	// Token: 0x04001735 RID: 5941
	public static int[] MaxNPCPoolGroupNum = new int[]
	{
		2,
		4,
		6
	};

	// Token: 0x04001736 RID: 5942
	public static int[] MaxNPCPoolNum = new int[]
	{
		3,
		6,
		6
	};

	// Token: 0x04001737 RID: 5943
	public static int[] MaxSceneCache = new int[]
	{
		1,
		2,
		2
	};

	// Token: 0x04001738 RID: 5944
	public static bool[] IsBundleNeedUnload;

	// Token: 0x04001739 RID: 5945
	public static bool[] IsShowFashionEffect;

	// Token: 0x0400173A RID: 5946
	public static bool[] IsShowSkillEffect;

	// Token: 0x0400173B RID: 5947
	public static bool[] IsShowPlayerShadow;

	// Token: 0x0400173C RID: 5948
	public static bool[] IsSoundCanPlay;

	// Token: 0x0400173D RID: 5949
	public static bool[] IsCarCopyEffectEnable;

	// Token: 0x0400173E RID: 5950
	public static int[] DownloadThreadCount;

	// Token: 0x0400173F RID: 5951
	public static bool IsShowNormalNpcName;

	// Token: 0x04001740 RID: 5952
	public static bool IsTutorialClose;

	// Token: 0x04001741 RID: 5953
	public static bool IsLowPhone;

	// Token: 0x04001742 RID: 5954
	public static int PlayerLevel;
}
