using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000207 RID: 519
public class AutoSearchPathManager
{
	// Token: 0x170003AF RID: 943
	// (get) Token: 0x06001193 RID: 4499 RVA: 0x00071E44 File Offset: 0x00070044
	public AutoSearchPath CurPath
	{
		get
		{
			return this.mCurPath;
		}
	}

	// Token: 0x170003B0 RID: 944
	// (get) Token: 0x06001194 RID: 4500 RVA: 0x00071E4C File Offset: 0x0007004C
	// (set) Token: 0x06001195 RID: 4501 RVA: 0x00071E54 File Offset: 0x00070054
	public string CurMissionId
	{
		get
		{
			return this.mCurMissionId;
		}
		set
		{
			this.mCurMissionId = value;
		}
	}

	// Token: 0x170003B1 RID: 945
	// (get) Token: 0x06001196 RID: 4502 RVA: 0x00071E60 File Offset: 0x00070060
	// (set) Token: 0x06001197 RID: 4503 RVA: 0x00071E68 File Offset: 0x00070068
	public AUTO_SEARCH_PARTH_FINISHEVENT FinishEventType
	{
		get
		{
			return this.mFinishEventType;
		}
		set
		{
			this.mFinishEventType = value;
		}
	}

	// Token: 0x170003B2 RID: 946
	// (get) Token: 0x06001198 RID: 4504 RVA: 0x00071E74 File Offset: 0x00070074
	// (set) Token: 0x06001199 RID: 4505 RVA: 0x00071E7C File Offset: 0x0007007C
	public bool IsAutoMovingFlag
	{
		get
		{
			return this.mIsAutoMovingFlag;
		}
		set
		{
			this.mIsAutoMovingFlag = value;
		}
	}

	// Token: 0x170003B3 RID: 947
	// (get) Token: 0x0600119A RID: 4506 RVA: 0x00071E88 File Offset: 0x00070088
	// (set) Token: 0x0600119B RID: 4507 RVA: 0x00071E90 File Offset: 0x00070090
	public bool IsInTelePortCircle
	{
		get
		{
			return this.mIsInTelePortCircle;
		}
		set
		{
			this.mIsInTelePortCircle = value;
		}
	}

	// Token: 0x170003B4 RID: 948
	// (get) Token: 0x0600119C RID: 4508 RVA: 0x00071E9C File Offset: 0x0007009C
	public string TargetSceneId
	{
		get
		{
			return this.mCurTargetPoint.SceneId;
		}
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x00071EAC File Offset: 0x000700AC
	public void FindPath(string sceneId, float posX, float posY, float posZ, AUTO_SEARCH_PARTH_FINISHEVENT finishEvent)
	{
		this.FindPath(new AutoSearchPathPoint(sceneId, posX, posY, posZ), finishEvent, null);
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x00071EC4 File Offset: 0x000700C4
	public void FindPath(AutoSearchPathPoint targetPoint, AUTO_SEARCH_PARTH_FINISHEVENT finishEvent, string missionId = null)
	{
		GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
		if (this.mCurTargetPoint.Equal(targetPoint) && this.mCurPath.PathPointList.Count > 0 && this.mCurPath.PathPointList[0].SceneId.Equals(instance.RunningMapIdStr))
		{
			this.mIsAutoMovingFlag = true;
			return;
		}
		this.mCurTargetPoint = targetPoint;
		this.mCurPath.ResetPath();
		this.mCurMissionId = missionId;
		this.mFinishEventType = finishEvent;
		ObjMainPlayer objMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (objMainPlayer == null)
		{
			Debug.Log("if(mainPlayer == null)");
			return;
		}
		if (instance.RunningMapIdStr.Equals(this.mCurTargetPoint.SceneId))
		{
			this.mCurPath.AddPathPoint(this.mCurTargetPoint);
			this.mIsAutoMovingFlag = true;
			if (Vector3.Distance(new Vector3(this.mCurTargetPoint.PosX, this.mCurTargetPoint.PosY, this.mCurTargetPoint.PosZ), objMainPlayer.Position) / objMainPlayer.NavMeshAgent.speed > 5f)
			{
				objMainPlayer.EnterAutoMoving(Time.time);
			}
			else
			{
				objMainPlayer.EnterAutoMoving(float.MaxValue);
			}
			return;
		}
		AutoSearchPathPoint startPoint = AutoSearchPathPoint.CreatePoint(objMainPlayer.gameObject);
		if (this.FindPath(startPoint, targetPoint))
		{
			this.mIsAutoMovingFlag = true;
			objMainPlayer.EnterAutoMoving(Time.time);
			return;
		}
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x00072030 File Offset: 0x00070230
	private bool FindPath(AutoSearchPathPoint startPoint, AutoSearchPathPoint targetPoint)
	{
		MapInfoData mapInfoDataByID = DataManager.GetMapInfoDataByID(startPoint.SceneId);
		AutoSearchPathPoint point = new AutoSearchPathPoint(mapInfoDataByID.ID, mapInfoDataByID.TelePortPosVector3);
		this.mCurPath.AddPathPoint(point);
		this.mCurPath.AddPathPoint(targetPoint);
		return true;
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x00072074 File Offset: 0x00070274
	public void InitMapConnection()
	{
		if (this.mMapConnectInfoList != null)
		{
			return;
		}
		this.mMapConnectInfoList = DataManager.GetMapConnectData();
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x00072090 File Offset: 0x00070290
	private int GetDisBySceneId(string resId, string tarId)
	{
		for (int i = 0; i < this.mMapConnectInfoList.Count; i++)
		{
			if (this.mMapConnectInfoList[i].SourceSceneId.Equals(resId) && this.mMapConnectInfoList[i].TargetSceneId.Equals(tarId))
			{
				return 1;
			}
		}
		return int.MaxValue;
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x000720F8 File Offset: 0x000702F8
	public void Finish()
	{
		this.mCurMissionId = null;
		this.mCurPath.ResetPath();
		this.mCurTargetPoint.Reset();
		this.mIsAutoMovingFlag = false;
		this.mFinishEventType = AUTO_SEARCH_PARTH_FINISHEVENT.INVALID;
	}

	// Token: 0x0400176D RID: 5997
	private AutoSearchPathPoint mCurTargetPoint = new AutoSearchPathPoint("-1", 0f, 0f, 0f);

	// Token: 0x0400176E RID: 5998
	private AutoSearchPath mCurPath = new AutoSearchPath();

	// Token: 0x0400176F RID: 5999
	private string mCurMissionId = string.Empty;

	// Token: 0x04001770 RID: 6000
	private AUTO_SEARCH_PARTH_FINISHEVENT mFinishEventType = AUTO_SEARCH_PARTH_FINISHEVENT.INVALID;

	// Token: 0x04001771 RID: 6001
	private bool mIsAutoMovingFlag;

	// Token: 0x04001772 RID: 6002
	private bool mIsInTelePortCircle = true;

	// Token: 0x04001773 RID: 6003
	private ObjMainPlayer mainPlayer;

	// Token: 0x04001774 RID: 6004
	private List<MapConnectInfoData> mMapConnectInfoList;

	// Token: 0x02000208 RID: 520
	private class PathNode
	{
		// Token: 0x060011A3 RID: 4515 RVA: 0x00072128 File Offset: 0x00070328
		public PathNode()
		{
			this.SceneId = string.Empty;
			this.Dis = int.MaxValue;
			this.PreNode = string.Empty;
		}

		// Token: 0x04001775 RID: 6005
		public string SceneId;

		// Token: 0x04001776 RID: 6006
		public int Dis;

		// Token: 0x04001777 RID: 6007
		public string PreNode;
	}
}
