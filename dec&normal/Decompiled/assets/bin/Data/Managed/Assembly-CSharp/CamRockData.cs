using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000156 RID: 342
public class CamRockData
{
	// Token: 0x170002BA RID: 698
	// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00061644 File Offset: 0x0005F844
	public float NeedRockTimeSecond
	{
		get
		{
			return (float)this.NeedRockTime / 1000f;
		}
	}

	// Token: 0x170002BB RID: 699
	// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00061654 File Offset: 0x0005F854
	public float DelayTimeSecond
	{
		get
		{
			return (float)this.DelayTime / 1000f;
		}
	}

	// Token: 0x170002BC RID: 700
	// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00061664 File Offset: 0x0005F864
	public AnimationCurve XPosCurve
	{
		get
		{
			return this.mXPosCurve;
		}
	}

	// Token: 0x170002BD RID: 701
	// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0006166C File Offset: 0x0005F86C
	public AnimationCurve YPosCurve
	{
		get
		{
			return this.mYPosCurve;
		}
	}

	// Token: 0x170002BE RID: 702
	// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00061674 File Offset: 0x0005F874
	public AnimationCurve ZPosCurve
	{
		get
		{
			return this.mZPosCurve;
		}
	}

	// Token: 0x170002BF RID: 703
	// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0006167C File Offset: 0x0005F87C
	public AnimationCurve XRotCurve
	{
		get
		{
			return this.mXRotCurve;
		}
	}

	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00061684 File Offset: 0x0005F884
	public AnimationCurve YRotCurve
	{
		get
		{
			return this.mYRotCurve;
		}
	}

	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x06000EDE RID: 3806 RVA: 0x0006168C File Offset: 0x0005F88C
	public AnimationCurve ZRotCurve
	{
		get
		{
			return this.mZRotCurve;
		}
	}

	// Token: 0x170002C2 RID: 706
	// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00061694 File Offset: 0x0005F894
	public AnimationCurve WRotCurve
	{
		get
		{
			return this.mWRotCurve;
		}
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x0006169C File Offset: 0x0005F89C
	public void Init()
	{
		if (!this.mInitFlag)
		{
			List<CamRockCurveData> camRockCurveDataListByName = DataManager.GetCamRockCurveDataListByName(this.CurveName);
			this.mXPosCurve = this.GetAnimaCurve(this.GetTargetTypeKey(camRockCurveDataListByName, 0));
			this.mYPosCurve = this.GetAnimaCurve(this.GetTargetTypeKey(camRockCurveDataListByName, 1));
			this.mZPosCurve = this.GetAnimaCurve(this.GetTargetTypeKey(camRockCurveDataListByName, 2));
			this.mXRotCurve = this.GetAnimaCurve(this.GetTargetTypeKey(camRockCurveDataListByName, 3));
			this.mYRotCurve = this.GetAnimaCurve(this.GetTargetTypeKey(camRockCurveDataListByName, 4));
			this.mZRotCurve = this.GetAnimaCurve(this.GetTargetTypeKey(camRockCurveDataListByName, 5));
			this.mWRotCurve = this.GetAnimaCurve(this.GetTargetTypeKey(camRockCurveDataListByName, 6));
			if (this.NeedRockTime == 0)
			{
				this.NeedRockTime = (int)(camRockCurveDataListByName[0].ClipLength * 1000f);
			}
			this.mInitFlag = true;
		}
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x00061778 File Offset: 0x0005F978
	private List<CamRockCurveData> GetTargetTypeKey(List<CamRockCurveData> sumList, int targetType)
	{
		List<CamRockCurveData> list = new List<CamRockCurveData>();
		for (int i = 0; i < sumList.Count; i++)
		{
			if (sumList[i].CurveType == targetType)
			{
				list.Add(sumList[i]);
			}
		}
		return list;
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x000617C4 File Offset: 0x0005F9C4
	private AnimationCurve GetAnimaCurve(List<CamRockCurveData> keyList)
	{
		if (keyList != null && keyList.Count > 0)
		{
			keyList.Sort((CamRockCurveData preData, CamRockCurveData nextData) => preData.KeyFrameIndex - nextData.KeyFrameIndex);
			Keyframe[] array = new Keyframe[keyList.Count];
			for (int i = 0; i < keyList.Count; i++)
			{
				array[i].value = keyList[i].KeyValue;
				array[i].time = keyList[i].KeyTime;
				array[i].inTangent = keyList[i].KeyInTangent;
				array[i].outTangent = keyList[i].KeyOutTangent;
				array[i].tangentMode = keyList[i].KeyTangentMode;
			}
			return new AnimationCurve(array)
			{
				preWrapMode = keyList[0].PreWrapMode,
				postWrapMode = keyList[0].PostWrapMode
			};
		}
		return null;
	}

	// Token: 0x04000D40 RID: 3392
	public string ID = string.Empty;

	// Token: 0x04000D41 RID: 3393
	public string CurveName = string.Empty;

	// Token: 0x04000D42 RID: 3394
	public int NeedRockTime;

	// Token: 0x04000D43 RID: 3395
	public int DelayTime;

	// Token: 0x04000D44 RID: 3396
	public int RockRate = 100;

	// Token: 0x04000D45 RID: 3397
	private bool mInitFlag;

	// Token: 0x04000D46 RID: 3398
	private AnimationCurve mXPosCurve;

	// Token: 0x04000D47 RID: 3399
	private AnimationCurve mYPosCurve;

	// Token: 0x04000D48 RID: 3400
	private AnimationCurve mZPosCurve;

	// Token: 0x04000D49 RID: 3401
	private AnimationCurve mXRotCurve;

	// Token: 0x04000D4A RID: 3402
	private AnimationCurve mYRotCurve;

	// Token: 0x04000D4B RID: 3403
	private AnimationCurve mZRotCurve;

	// Token: 0x04000D4C RID: 3404
	private AnimationCurve mWRotCurve;
}
