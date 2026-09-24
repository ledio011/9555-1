using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200088D RID: 2189
public class SurvivalBattleSceneManager : SceneManager
{
	// Token: 0x06003B0A RID: 15114 RVA: 0x00100DF4 File Offset: 0x000FEFF4
	public override void Init(string id)
	{
		base.Init(id);
		this.CheckInterTime = 2f;
		this.curTemptime = 0f;
		List<Vector3> teleportPosList = base.CurrentMapInofData.TeleportPosList;
		this.mRelifePosList = base.CurrentMapInofData.RelifePosList;
		GameObject gameObject = ResourcesManager.LoadAndInstantiate("Items/MovePathPoint") as GameObject;
		MovePathPoint component = gameObject.GetComponent<MovePathPoint>();
		this.mTransportPointList.Add(component);
		component.transform.position = teleportPosList[0];
		component.RegisterOnArrivePathPoint(new MovePathPoint.OnArrivePointDelegate(this.OnArriveTelePortPoint));
		for (int i = 1; i < teleportPosList.Count; i++)
		{
			GameObject gameObject2 = Object.Instantiate(gameObject) as GameObject;
			MovePathPoint component2 = gameObject2.GetComponent<MovePathPoint>();
			gameObject2.transform.position = teleportPosList[i];
			this.mTransportPointList.Add(component2);
			component2.RegisterOnArrivePathPoint(new MovePathPoint.OnArrivePointDelegate(this.OnArriveTelePortPoint));
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MultiRankSmallRootUI, delegate
		{
			SingletonUnity<MultiRankSmallRootLogic>.Instance.EnableReset();
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MultiRankSmallRootUI);
		}, null);
		this.surviveBattleData = DataManager.GetSurviveBattleDataById(base.CurrentMapInofData.Param1);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SurviveBattleFloorInfoRoot, delegate
		{
			int floorid = (!this.surviveBattleData.MapID.Equals(base.CurrentMapInofData.ID)) ? 1 : 0;
			SingletonUnity<SurviveBattleFloorInfoRootLogic>.Instance.Reset(floorid, this.surviveBattleData.SecondMinScore);
		}, null);
		this.mHasShowMessageBox = false;
	}

	// Token: 0x06003B0B RID: 15115 RVA: 0x00100F48 File Offset: 0x000FF148
	public void OnArriveTelePortPoint(Vector3 pos)
	{
		int num = Random.Range(0, this.mRelifePosList.Count);
		Vector4 vector = this.mRelifePosList[num];
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		mainPlayer.DisableNavMeshAgent();
		mainPlayer.Position = new Vector3(vector.x, vector.y, vector.z);
		mainPlayer.FaceToPub(MathUtil.HeadingToVector3(vector.w));
		mainPlayer.EnableNavMeshAgent();
		mainPlayer.UseInvincibleSkill();
		enter_teleport_point.request request = new enter_teleport_point.request();
		request.index = (long)num;
		NetLogic.GetInstance().Send<Protocol.enter_teleport_point>(request, null);
		NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101586}", new object[0]), true, false);
	}

	// Token: 0x06003B0C RID: 15116 RVA: 0x00100FF4 File Offset: 0x000FF1F4
	public override void Update()
	{
		base.Update();
		this.curTemptime += Time.deltaTime;
		if (this.curTemptime > this.CheckInterTime)
		{
			this.curTemptime = 0f;
			NetLogic.GetInstance().Send<Protocol.request_survive_top>(null, null);
		}
	}

	// Token: 0x06003B0D RID: 15117 RVA: 0x00101044 File Offset: 0x000FF244
	public virtual void MoveToNextFloor()
	{
	}

	// Token: 0x06003B0E RID: 15118 RVA: 0x00101048 File Offset: 0x000FF248
	public virtual void UpdatePlayerScore(long playerScores)
	{
		this.mPlayerScores = playerScores;
	}

	// Token: 0x06003B0F RID: 15119 RVA: 0x00101054 File Offset: 0x000FF254
	public override void LeaveScene()
	{
		MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString(base.CurrentMapInofData.ExitCon, new object[]
		{
			this.surviveBattleData.FirstMaxScore
		}), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
		{
			NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
		}, null, null, null);
	}

	// Token: 0x040026A1 RID: 9889
	protected float CheckInterTime;

	// Token: 0x040026A2 RID: 9890
	protected float curTemptime;

	// Token: 0x040026A3 RID: 9891
	protected List<MovePathPoint> mTransportPointList = new List<MovePathPoint>();

	// Token: 0x040026A4 RID: 9892
	protected List<Vector4> mRelifePosList;

	// Token: 0x040026A5 RID: 9893
	protected long mPlayerScores;

	// Token: 0x040026A6 RID: 9894
	protected SurviveBattleData surviveBattleData;

	// Token: 0x040026A7 RID: 9895
	protected bool mHasShowMessageBox;
}
