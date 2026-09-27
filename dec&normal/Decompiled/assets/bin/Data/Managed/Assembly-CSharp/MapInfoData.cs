using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000185 RID: 389
public class MapInfoData
{
	// Token: 0x17000317 RID: 791
	// (get) Token: 0x06000F85 RID: 3973 RVA: 0x000639DC File Offset: 0x00061BDC
	public Vector3 ShadowDir
	{
		get
		{
			if (this.mShadowDir == null && !string.IsNullOrEmpty(this.ShadowDirection))
			{
				this.mShadowDir = this.ShadowDirection.Split(new char[]
				{
					'#'
				});
				this.mShadowDirV3 = new Vector3((float)int.Parse(this.mShadowDir[0]), (float)int.Parse(this.mShadowDir[1]), (float)int.Parse(this.mShadowDir[2]));
			}
			return this.mShadowDirV3;
		}
	}

	// Token: 0x17000318 RID: 792
	// (get) Token: 0x06000F86 RID: 3974 RVA: 0x00063A5C File Offset: 0x00061C5C
	public string MExitCon
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.ExitCon, new object[0]);
		}
	}

	// Token: 0x17000319 RID: 793
	// (get) Token: 0x06000F87 RID: 3975 RVA: 0x00063A70 File Offset: 0x00061C70
	public Vector3 BirthPosVector3
	{
		get
		{
			if (this.mBirthPos == null)
			{
				string[] array = this.BirthPos.Split(new char[]
				{
					'#'
				});
				this.mBirthPos = new float[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mBirthPos[i] = (float)int.Parse(array[i]) / 100f;
				}
			}
			return new Vector3(this.mBirthPos[0], this.mBirthPos[1], this.mBirthPos[2]);
		}
	}

	// Token: 0x1700031A RID: 794
	// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00063AF8 File Offset: 0x00061CF8
	public Vector3 TelePortPosVector3
	{
		get
		{
			if (this.mTelePortPos == null && !string.IsNullOrEmpty(this.TelePortPos))
			{
				string[] array = this.TelePortPos.Split(new char[]
				{
					'#'
				});
				this.mTelePortPos = new float[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mTelePortPos[i] = (float)int.Parse(array[i]) / 100f;
				}
			}
			if (this.mTelePortPos != null && this.mTelePortPos.Length > 2)
			{
				return new Vector3(this.mTelePortPos[0], this.mTelePortPos[1], this.mTelePortPos[2]);
			}
			return Vector3.zero;
		}
	}

	// Token: 0x1700031B RID: 795
	// (get) Token: 0x06000F89 RID: 3977 RVA: 0x00063BAC File Offset: 0x00061DAC
	public MAPTYPE MapType
	{
		get
		{
			return (MAPTYPE)this.Type;
		}
	}

	// Token: 0x1700031C RID: 796
	// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00063BB4 File Offset: 0x00061DB4
	public float fMapLength
	{
		get
		{
			return (float)this.MapLength / 100f;
		}
	}

	// Token: 0x1700031D RID: 797
	// (get) Token: 0x06000F8B RID: 3979 RVA: 0x00063BC4 File Offset: 0x00061DC4
	public float fMapHeight
	{
		get
		{
			return (float)this.MapHeight / 100f;
		}
	}

	// Token: 0x1700031E RID: 798
	// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00063BD4 File Offset: 0x00061DD4
	public Vector3 WoldPosVector3
	{
		get
		{
			Vector3 zero = Vector3.zero;
			string[] array = this.WorldPos.Split(new char[]
			{
				'#'
			});
			zero.x = float.Parse(array[0]);
			zero.y = float.Parse(array[1]);
			return zero;
		}
	}

	// Token: 0x1700031F RID: 799
	// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00063C20 File Offset: 0x00061E20
	public List<Vector3> PlayerPathPointList
	{
		get
		{
			if (this.mPlayerPathPointList == null)
			{
				this.mPlayerPathPointList = new List<Vector3>();
				string[] array = this.Param1.Split(new char[]
				{
					';'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						'#'
					});
					this.mPlayerPathPointList.Add(new Vector3((float)int.Parse(array2[0]) / 100f, (float)int.Parse(array2[1]) / 100f, (float)int.Parse(array2[2]) / 100f));
				}
			}
			return this.mPlayerPathPointList;
		}
	}

	// Token: 0x17000320 RID: 800
	// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00063CC8 File Offset: 0x00061EC8
	public List<Vector4> BirthPosList
	{
		get
		{
			if (this.mBirthPosList == null)
			{
				this.mBirthPosList = new List<Vector4>();
				string[] array = this.BirthPos.Split(new char[]
				{
					'#'
				});
				for (int i = 0; i < array.Length / 4; i++)
				{
					this.mBirthPosList.Add(new Vector4((float)int.Parse(array[i * 4]) / 100f, (float)int.Parse(array[i * 4 + 1]) / 100f, (float)int.Parse(array[i * 4 + 2]) / 100f, (float)int.Parse(array[i * 4 + 3]) / 100f));
				}
			}
			return this.mBirthPosList;
		}
	}

	// Token: 0x17000321 RID: 801
	// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00063D78 File Offset: 0x00061F78
	public List<Vector4> RelifePosList
	{
		get
		{
			if (this.mRelifePosList == null)
			{
				this.mRelifePosList = new List<Vector4>();
				string[] array = this.RelifePos.Split(new char[]
				{
					'#'
				});
				for (int i = 0; i < array.Length / 4; i++)
				{
					this.mRelifePosList.Add(new Vector4((float)int.Parse(array[i * 4]) / 100f, (float)int.Parse(array[i * 4 + 1]) / 100f, (float)int.Parse(array[i * 4 + 2]) / 100f, (float)int.Parse(array[i * 4 + 3]) / 100f));
				}
			}
			return this.mRelifePosList;
		}
	}

	// Token: 0x17000322 RID: 802
	// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00063E28 File Offset: 0x00062028
	public List<Vector3> TeleportPosList
	{
		get
		{
			if (this.mTeleportPosList == null)
			{
				this.mTeleportPosList = new List<Vector3>();
				string[] array = this.TelePortPos.Split(new char[]
				{
					'#'
				});
				for (int i = 0; i < array.Length / 3; i++)
				{
					this.mTeleportPosList.Add(new Vector3((float)int.Parse(array[i * 3]) / 100f, (float)int.Parse(array[i * 3 + 1]) / 100f, (float)int.Parse(array[i * 3 + 2]) / 100f));
				}
			}
			return this.mTeleportPosList;
		}
	}

	// Token: 0x17000323 RID: 803
	// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00063EC8 File Offset: 0x000620C8
	public List<Vector3> ExitPosList
	{
		get
		{
			if (this.mExitPosList == null)
			{
				this.mExitPosList = new List<Vector3>();
				string[] array = this.ExitPos.Split(new char[]
				{
					'#'
				});
				for (int i = 0; i < array.Length / 3; i++)
				{
					this.mExitPosList.Add(new Vector3((float)int.Parse(array[i * 3]) / 100f, (float)int.Parse(array[i * 3 + 1]) / 100f, (float)int.Parse(array[i * 3 + 2]) / 100f));
				}
			}
			return this.mExitPosList;
		}
	}

	// Token: 0x17000324 RID: 804
	// (get) Token: 0x06000F92 RID: 3986 RVA: 0x00063F68 File Offset: 0x00062168
	public int[] EndTimes
	{
		get
		{
			if ((this.mEndTimes == null || this.mEndTimes.Length == 0) && !string.IsNullOrEmpty(this.EndTime))
			{
				string[] array = this.EndTime.Split(new char[]
				{
					'-'
				});
				this.mEndTimes = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mEndTimes[i] = int.Parse(array[i]);
				}
			}
			return this.mEndTimes;
		}
	}

	// Token: 0x17000325 RID: 805
	// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00063FEC File Offset: 0x000621EC
	public int[] Starttimes
	{
		get
		{
			if ((this.mStarttimes == null || this.mStarttimes.Length == 0) && !string.IsNullOrEmpty(this.Starttime))
			{
				string[] array = this.Starttime.Split(new char[]
				{
					'-'
				});
				this.mStarttimes = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mStarttimes[i] = int.Parse(array[i]);
				}
			}
			return this.mStarttimes;
		}
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x00064070 File Offset: 0x00062270
	public int GetAudioID()
	{
		if (this.ActAudioID != -1 && !string.IsNullOrEmpty(this.Starttime) && !string.IsNullOrEmpty(this.EndTime) && TimeTools.IsTimeRange(this.Starttimes, this.EndTimes))
		{
			return this.ActAudioID;
		}
		return this.AudioId;
	}

	// Token: 0x04001007 RID: 4103
	public string ID = string.Empty;

	// Token: 0x04001008 RID: 4104
	public string Name = string.Empty;

	// Token: 0x04001009 RID: 4105
	public string SceneName = string.Empty;

	// Token: 0x0400100A RID: 4106
	public int Type;

	// Token: 0x0400100B RID: 4107
	public string MiniMapName = string.Empty;

	// Token: 0x0400100C RID: 4108
	public int MapLength;

	// Token: 0x0400100D RID: 4109
	public int MapHeight;

	// Token: 0x0400100E RID: 4110
	public string BirthPos;

	// Token: 0x0400100F RID: 4111
	public string RelifePos;

	// Token: 0x04001010 RID: 4112
	public string TelePortPos;

	// Token: 0x04001011 RID: 4113
	public int radius;

	// Token: 0x04001012 RID: 4114
	public string luaname;

	// Token: 0x04001013 RID: 4115
	public string Param1;

	// Token: 0x04001014 RID: 4116
	public string Param2;

	// Token: 0x04001015 RID: 4117
	public string Param3;

	// Token: 0x04001016 RID: 4118
	public string Param4;

	// Token: 0x04001017 RID: 4119
	public int Param5;

	// Token: 0x04001018 RID: 4120
	public int RelifeCost = 1;

	// Token: 0x04001019 RID: 4121
	public int RelifeType;

	// Token: 0x0400101A RID: 4122
	public int DirectLoad;

	// Token: 0x0400101B RID: 4123
	public int ChangeLightMap;

	// Token: 0x0400101C RID: 4124
	public int AudioId = 500;

	// Token: 0x0400101D RID: 4125
	public int OpenLv;

	// Token: 0x0400101E RID: 4126
	public string WorldPos = string.Empty;

	// Token: 0x0400101F RID: 4127
	public string MapIcon = string.Empty;

	// Token: 0x04001020 RID: 4128
	public string SaftyAreaId = string.Empty;

	// Token: 0x04001021 RID: 4129
	public string GatherAreaId = string.Empty;

	// Token: 0x04001022 RID: 4130
	public string ExitCon = string.Empty;

	// Token: 0x04001023 RID: 4131
	public int TargetPKMode = 1;

	// Token: 0x04001024 RID: 4132
	public int IsLockPVP;

	// Token: 0x04001025 RID: 4133
	public int AutoFightDis = 100;

	// Token: 0x04001026 RID: 4134
	public string ShadowDirection;

	// Token: 0x04001027 RID: 4135
	private string[] mShadowDir;

	// Token: 0x04001028 RID: 4136
	private Vector3 mShadowDirV3 = new Vector3(55f, 70f, 0f);

	// Token: 0x04001029 RID: 4137
	private float[] mBirthPos;

	// Token: 0x0400102A RID: 4138
	private float[] mTelePortPos;

	// Token: 0x0400102B RID: 4139
	private List<Vector3> mPlayerPathPointList;

	// Token: 0x0400102C RID: 4140
	private List<Vector4> mBirthPosList;

	// Token: 0x0400102D RID: 4141
	private List<Vector4> mRelifePosList;

	// Token: 0x0400102E RID: 4142
	private List<Vector3> mTeleportPosList;

	// Token: 0x0400102F RID: 4143
	public string ExitPos = string.Empty;

	// Token: 0x04001030 RID: 4144
	private List<Vector3> mExitPosList;

	// Token: 0x04001031 RID: 4145
	public int ActAudioID = -1;

	// Token: 0x04001032 RID: 4146
	public string Starttime = string.Empty;

	// Token: 0x04001033 RID: 4147
	public string EndTime = string.Empty;

	// Token: 0x04001034 RID: 4148
	private int[] mEndTimes;

	// Token: 0x04001035 RID: 4149
	private int[] mStarttimes;
}
