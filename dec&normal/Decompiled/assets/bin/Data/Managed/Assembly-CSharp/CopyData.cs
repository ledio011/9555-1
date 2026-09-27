using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x02000137 RID: 311
public class CopyData
{
	// Token: 0x06000B97 RID: 2967 RVA: 0x00054814 File Offset: 0x00052A14
	private static int SortFun(copyscene_info a1, copyscene_info a2)
	{
		if (a1.ID.Length == a2.ID.Length)
		{
			return a1.ID.CompareTo(a2.ID);
		}
		return a1.ID.Length - a2.ID.Length;
	}

	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00054868 File Offset: 0x00052A68
	public Dictionary<string, copyscene_info> NormalCopyInfoDic
	{
		get
		{
			return this.mNormalCopyInfoDic;
		}
	}

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x06000B99 RID: 2969 RVA: 0x00054870 File Offset: 0x00052A70
	public List<copyscene_info> NormalCopyInfoList
	{
		get
		{
			List<copyscene_info> list = new List<copyscene_info>(this.mNormalCopyInfoDic.Values);
			list.Sort(new Comparison<copyscene_info>(CopyData.SortFun));
			return list;
		}
	}

	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x06000B9A RID: 2970 RVA: 0x000548A4 File Offset: 0x00052AA4
	public Dictionary<string, copyscene_info> DailyCopyInfoDic
	{
		get
		{
			return this.mDailyCopyInfoDic;
		}
	}

	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x06000B9B RID: 2971 RVA: 0x000548AC File Offset: 0x00052AAC
	public List<copyscene_info> DailyCopyInfoList
	{
		get
		{
			List<copyscene_info> list = new List<copyscene_info>(this.mDailyCopyInfoDic.Values);
			list.Sort(new Comparison<copyscene_info>(CopyData.SortFun));
			return list;
		}
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x000548E0 File Offset: 0x00052AE0
	public void Reset()
	{
		this.mNormalCopyInfoDic.Clear();
		this.mDailyCopyInfoDic.Clear();
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x000548F8 File Offset: 0x00052AF8
	public copyscene_info GetCopyinfoByID(string id)
	{
		if (this.mDailyCopyInfoDic.ContainsKey(id))
		{
			return this.mDailyCopyInfoDic[id];
		}
		return null;
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x0005491C File Offset: 0x00052B1C
	public void DecTimesByID(string id)
	{
		if (this.mDailyCopyInfoDic.ContainsKey(id) && this.mDailyCopyInfoDic[id].CurNum > 0L)
		{
			this.mDailyCopyInfoDic[id].CurNum -= 1L;
		}
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x0005496C File Offset: 0x00052B6C
	public copyscene_info GetCopyinfoByType(int type)
	{
		for (int i = 0; i < this.DailyCopyInfoList.Count; i++)
		{
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(this.DailyCopyInfoList[i].ID);
			if (copySceneDataById.SubType == type)
			{
				return this.DailyCopyInfoList[i];
			}
		}
		return null;
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x000549C8 File Offset: 0x00052BC8
	public copyscene_info GetCopyinfoByID(int ID)
	{
		for (int i = 0; i < this.DailyCopyInfoList.Count; i++)
		{
			if (this.DailyCopyInfoList[i].ID.Equals(ID))
			{
				return this.DailyCopyInfoList[i];
			}
		}
		return null;
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x00054A20 File Offset: 0x00052C20
	public void SyncCopyInfo(copyscene_info info)
	{
		this.SyncData(new KeyValuePair<string, copyscene_info>(info.ID, info));
		this.UpdateTips();
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x00054A3C File Offset: 0x00052C3C
	public void SyncCopyInfo(Dictionary<string, copyscene_info> newCopyInfoDic)
	{
		List<KeyValuePair<string, copyscene_info>> list = new List<KeyValuePair<string, copyscene_info>>(newCopyInfoDic);
		this.mDailyCopyInfoDic.Clear();
		this.mNormalCopyInfoDic.Clear();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Value.enable)
			{
				this.SyncData(list[i]);
			}
		}
		this.UpdateTips();
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x00054AAC File Offset: 0x00052CAC
	private void SyncData(KeyValuePair<string, copyscene_info> info)
	{
		long type = info.Value.Type;
		if (type != 0L)
		{
			if (type == 1L)
			{
				this.SyncDataInRightDic(this.mDailyCopyInfoDic, info);
			}
		}
		else
		{
			this.SyncDataInRightDic(this.mNormalCopyInfoDic, info);
		}
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x00054B00 File Offset: 0x00052D00
	public CopySceneData GetCurPlayerEquipCopyData()
	{
		List<CopySceneData> copySceneDataByType = DataManager.GetCopySceneDataByType(16);
		CopySceneData result = copySceneDataByType[0];
		for (int i = 0; i < copySceneDataByType.Count; i++)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(copySceneDataByType[i].MinLevel, copySceneDataByType[i].MaxLevel))
			{
				result = copySceneDataByType[i];
			}
		}
		return result;
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x00054B6C File Offset: 0x00052D6C
	public CopySceneData GetCurPlayerExpCopyData()
	{
		List<CopySceneData> copySceneDataByType = DataManager.GetCopySceneDataByType(12);
		CopySceneData result = copySceneDataByType[0];
		for (int i = 0; i < copySceneDataByType.Count; i++)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(copySceneDataByType[i].MinLevel, copySceneDataByType[i].MaxLevel))
			{
				result = copySceneDataByType[i];
			}
		}
		return result;
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00054BD8 File Offset: 0x00052DD8
	public bool IsDailyCopyCanPlay(MAPTYPE dailytype)
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ACTIVITY_DAILY))
		{
			return false;
		}
		for (int i = 0; i < this.DailyCopyInfoList.Count; i++)
		{
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(this.DailyCopyInfoList[i].ID);
			if (copySceneDataById != null)
			{
				if (copySceneDataById.SubType == (int)dailytype)
				{
					if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(copySceneDataById.MinLevel))
					{
						if (dailytype == MAPTYPE.CAR_CHASE_COPY)
						{
							return true;
						}
						if (this.DailyCopyInfoList[i].CurNum > 0L)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x00054C90 File Offset: 0x00052E90
	public bool IsHaveDailyItemTips()
	{
		return this.IsHaveDailyCopyTips() || SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TowerData.IsHaveTowerTips();
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00054CC0 File Offset: 0x00052EC0
	public bool IsHaveDailyCopyTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ACTIVITY_DAILY))
		{
			return false;
		}
		for (int i = 0; i < this.DailyCopyInfoList.Count; i++)
		{
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(this.DailyCopyInfoList[i].ID);
			if (copySceneDataById != null)
			{
				if (copySceneDataById.SubType != 1)
				{
					if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(copySceneDataById.MinLevel))
					{
						if (this.DailyCopyInfoList[i].CurNum > 0L)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x00054D74 File Offset: 0x00052F74
	public bool IsHaveDailyCopyTipsBytype(MAPTYPE type)
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ACTIVITY_DAILY))
		{
			return false;
		}
		for (int i = 0; i < this.DailyCopyInfoList.Count; i++)
		{
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(this.DailyCopyInfoList[i].ID);
			if (copySceneDataById != null)
			{
				if (copySceneDataById.SubType == (int)type)
				{
					if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(copySceneDataById.MinLevel))
					{
						if (this.DailyCopyInfoList[i].CurNum > 0L)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x00054E20 File Offset: 0x00053020
	public void SyncTimeslocal(string id)
	{
		CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(id);
		if (copySceneDataById.SubType == 11)
		{
			for (int i = 0; i < this.DailyCopyInfoList.Count; i++)
			{
				CopySceneData copySceneDataById2 = DataManager.GetCopySceneDataById(this.DailyCopyInfoList[i].ID);
				if (copySceneDataById2 != null)
				{
					if (copySceneDataById2.SubType == copySceneDataById.SubType)
					{
						this.mDailyCopyInfoDic[this.DailyCopyInfoList[i].ID].CurNum -= 1L;
					}
				}
			}
			this.UpdateTips();
		}
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x00054EC0 File Offset: 0x000530C0
	public void UpdateTips()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMessageTips();
		}
		if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.RefershTips();
		}
		if (SingletonUnity<ActivityTipsRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ActivityTipsRootLogic>.Instance.gameObject))
		{
			SingletonUnity<ActivityTipsRootLogic>.Instance.UpdateInfo();
		}
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x00054F34 File Offset: 0x00053134
	private void SyncDataInRightDic(Dictionary<string, copyscene_info> dic, KeyValuePair<string, copyscene_info> info)
	{
		if (dic.ContainsKey(info.Key))
		{
			dic[info.Key] = info.Value;
		}
		else
		{
			dic.Add(info.Key, info.Value);
		}
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x00054F80 File Offset: 0x00053180
	public void ResetCopyInfo()
	{
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x00054F84 File Offset: 0x00053184
	private void ClassifyCopyInfo(KeyValuePair<string, CopySceneData> info)
	{
		copyscene_info copyscene_info = new copyscene_info();
		copyscene_info.Type = (long)info.Value.Type;
		copyscene_info.CurNum = 0L;
		copyscene_info.ID = info.Value.ID;
		copyscene_info.BestGrade = -1L;
		int type = info.Value.Type;
		if (type != 0)
		{
			if (type == 1)
			{
				this.mDailyCopyInfoDic.Add(info.Key, copyscene_info);
			}
		}
		else
		{
			this.mNormalCopyInfoDic.Add(info.Key, copyscene_info);
		}
	}

	// Token: 0x04000A64 RID: 2660
	private Dictionary<string, copyscene_info> mNormalCopyInfoDic = new Dictionary<string, copyscene_info>();

	// Token: 0x04000A65 RID: 2661
	private Dictionary<string, copyscene_info> mDailyCopyInfoDic = new Dictionary<string, copyscene_info>();
}
