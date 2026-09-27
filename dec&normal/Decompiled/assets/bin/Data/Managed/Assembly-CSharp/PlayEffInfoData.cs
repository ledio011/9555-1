using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200016B RID: 363
public class PlayEffInfoData
{
	// Token: 0x06000F23 RID: 3875 RVA: 0x00062208 File Offset: 0x00060408
	public PlayEffInfoData(FxEffInfoData fxData, EffInfoData effData, float delayTime, Vector3 senderPos, Transform tarTransform = null)
	{
		this.mFxEffinfoData = fxData;
		this.DelayTime = this.mFxEffinfoData.fEffDelayTimeSeconds + delayTime;
		this.EffinfoData = effData;
		this.SenderPos = senderPos;
		this.targetTransform = tarTransform;
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x00062244 File Offset: 0x00060444
	public static List<PlayEffInfoData> GetPlayEffInfoDataList(string actionName, string effInfoID, float delayTime, Vector3 senderPos, Transform targetTransform = null)
	{
		List<PlayEffInfoData> list = null;
		ActionData actionDataByName = DataManager.GetActionDataByName(actionName);
		if (actionDataByName != null)
		{
			List<FxEffInfoData> fxEffInfoDataListById = DataManager.GetFxEffInfoDataListById(actionDataByName.FxEffID);
			EffInfoData effInfoDataById = DataManager.GetEffInfoDataById(effInfoID);
			if (fxEffInfoDataListById != null)
			{
				for (int i = 0; i < fxEffInfoDataListById.Count; i++)
				{
					PlayEffInfoData playEffInfoData = new PlayEffInfoData(fxEffInfoDataListById[i], effInfoDataById, delayTime, senderPos, targetTransform);
					playEffInfoData.DurationTime = fxEffInfoDataListById[i].EffDurationTimeSeconds;
					playEffInfoData.mActionData = actionDataByName;
					if (list == null)
					{
						list = new List<PlayEffInfoData>();
					}
					list.Add(playEffInfoData);
				}
			}
		}
		return list;
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x000622DC File Offset: 0x000604DC
	public static List<PlayEffInfoData> GetPlayBufEffInfoDataList(string mFxEffID, float delayTime, float duration, Vector3 senderPos)
	{
		List<PlayEffInfoData> list = null;
		List<FxEffInfoData> fxEffInfoDataListById = DataManager.GetFxEffInfoDataListById(mFxEffID);
		if (fxEffInfoDataListById != null)
		{
			for (int i = 0; i < fxEffInfoDataListById.Count; i++)
			{
				PlayEffInfoData playEffInfoData = new PlayEffInfoData(fxEffInfoDataListById[i], null, delayTime, senderPos, null);
				playEffInfoData.DurationTime = duration;
				playEffInfoData.mActionData = null;
				if (list == null)
				{
					list = new List<PlayEffInfoData>();
				}
				list.Add(playEffInfoData);
			}
		}
		return list;
	}

	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00062344 File Offset: 0x00060544
	public string fxLoadPath
	{
		get
		{
			return this.mFxEffinfoData.EffFilePath + "/" + this.mFxEffinfoData.EffName;
		}
	}

	// Token: 0x04000E70 RID: 3696
	public FxEffInfoData mFxEffinfoData;

	// Token: 0x04000E71 RID: 3697
	public float DelayTime;

	// Token: 0x04000E72 RID: 3698
	public EffInfoData EffinfoData;

	// Token: 0x04000E73 RID: 3699
	public Vector3 SenderPos;

	// Token: 0x04000E74 RID: 3700
	public FxControl fxControl;

	// Token: 0x04000E75 RID: 3701
	public ActionData mActionData;

	// Token: 0x04000E76 RID: 3702
	public float DurationTime;

	// Token: 0x04000E77 RID: 3703
	public Transform targetTransform;

	// Token: 0x04000E78 RID: 3704
	public float LoadStartTime;
}
