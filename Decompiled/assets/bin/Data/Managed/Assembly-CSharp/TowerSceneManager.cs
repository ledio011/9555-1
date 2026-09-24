using System;
using UnityEngine;

// Token: 0x0200088E RID: 2190
public class TowerSceneManager : SceneManager
{
	// Token: 0x06003B14 RID: 15124 RVA: 0x00101140 File Offset: 0x000FF340
	public override void Init(string id)
	{
		base.Init(id);
		SingletonUnity<MyEvent>.Instance.Register("OnMainPlayerCreate", this, "OnPlayerCreate");
	}

	// Token: 0x06003B15 RID: 15125 RVA: 0x00101160 File Offset: 0x000FF360
	public void OnPlayerCreate()
	{
		SingletonUnity<MyEvent>.Instance.DeRegister("OnMainPlayerCreate", this, "OnPlayerCreate");
		this.StartGame();
	}

	// Token: 0x06003B16 RID: 15126 RVA: 0x00101180 File Offset: 0x000FF380
	private void InitDoor()
	{
		this.men1Obj = GameObject.Find("FB_PKTai/men_1");
		this.men2Obj = GameObject.Find("FB_PKTai/men_2");
		if (this.men1Obj == null || this.men1Obj == null)
		{
			Debug.LogWarning("open door object miss!!!!");
		}
	}

	// Token: 0x06003B17 RID: 15127 RVA: 0x001011DC File Offset: 0x000FF3DC
	private void PlayDoorAnimation()
	{
		if (this.men1Obj != null)
		{
			this.men1Obj.animation.Play();
		}
		if (this.men2Obj != null)
		{
			this.men2Obj.animation.Play();
		}
	}

	// Token: 0x06003B18 RID: 15128 RVA: 0x00101230 File Offset: 0x000FF430
	private void PlayerMoveToCenter()
	{
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			mainPlayer.MoveTo(Vector3.zero, 1f, null);
		}
	}

	// Token: 0x06003B19 RID: 15129 RVA: 0x00101268 File Offset: 0x000FF468
	public override void StartGame()
	{
		this.PlayerMoveToCenter();
		this.CurrentFloor = base.CurrentMapInofData.Param5;
		this.ShowFloor();
	}

	// Token: 0x06003B1A RID: 15130 RVA: 0x00101288 File Offset: 0x000FF488
	public void ShowFloor()
	{
		TowerCurrentFloorInfoLogic.ShowFloor(this.CurrentFloor);
	}

	// Token: 0x06003B1B RID: 15131 RVA: 0x00101298 File Offset: 0x000FF498
	public override void SuccessMission()
	{
		Debug.Log("SuccessMission");
	}

	// Token: 0x06003B1C RID: 15132 RVA: 0x001012A4 File Offset: 0x000FF4A4
	public override void OnNPCDie(object objNpc)
	{
	}

	// Token: 0x06003B1D RID: 15133 RVA: 0x001012A8 File Offset: 0x000FF4A8
	public void Retry()
	{
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer == null || mainPlayer.IsDie)
		{
			WaitResponseUIRootLogic.OpenWaitBox(204, 0f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.stop_leave_copy>(null, null);
			NetLogic.GetInstance().Send<Protocol.continue_tower_copy>(null, null);
			Singleton<ObjManager>.Instance.RecycleAllNPC();
			return;
		}
		mainPlayer.StopAutoAndSkill();
		NetLogic.GetInstance().Send<Protocol.stop_leave_copy>(null, null);
		SingletonUnity<UIManager>.Instance.HideBaseUI();
		mainPlayer.MoveTo(new Vector3(-15f, 0f, 0f), 1f, delegate(ObjCharacter A_1)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BlackScreenRoot, delegate
			{
				SingletonUnity<BlackScreenLogic>.Instance.CloseScreen(0.5f, null);
			}, null);
			Singleton<ObjManager>.Instance.RecycleAllNPC();
			vp_Timer.In(0.5f, delegate()
			{
				mainPlayer.Position = new Vector3(15f, 0f, 0f);
				mainPlayer.MoveTo(Vector3.zero, 1f, null);
				SingletonUnity<BlackScreenLogic>.Instance.OpenScreen(0.5f, null);
			}, null);
			NetLogic.GetInstance().Send<Protocol.continue_tower_copy>(null, null);
			mainPlayer.IsTalking = false;
		});
		this.ShowFloor();
	}

	// Token: 0x06003B1E RID: 15134 RVA: 0x0010137C File Offset: 0x000FF57C
	public void GoNextLevel()
	{
		this.CurrentFloor++;
		SingletonUnity<UIManager>.Instance.HideBaseUI();
		Singleton<ObjManager>.Instance.RecycleAllNPC();
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		mainPlayer.StopAutoAndSkill();
		mainPlayer.CameraController.LerpBackToPlayer(0.5f, null);
		NetLogic.GetInstance().Send<Protocol.stop_leave_copy>(null, null);
		mainPlayer.MoveTo(new Vector3(-15f, 0f, 0f), 1f, delegate(ObjCharacter A_1)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BlackScreenRoot, delegate
			{
				SingletonUnity<BlackScreenLogic>.Instance.CloseScreen(0.5f, null);
			}, null);
			vp_Timer.In(0.5f, delegate()
			{
				mainPlayer.Position = new Vector3(15f, 0f, 0f);
				mainPlayer.MoveTo(Vector3.zero, 1f, null);
				SingletonUnity<BlackScreenLogic>.Instance.OpenScreen(0.5f, null);
			}, null);
			NetLogic.GetInstance().Send<Protocol.continue_tower_copy>(null, null);
			mainPlayer.IsTalking = false;
		});
		this.ShowFloor();
	}

	// Token: 0x040026AA RID: 9898
	private GameObject men1Obj;

	// Token: 0x040026AB RID: 9899
	private GameObject men2Obj;

	// Token: 0x040026AC RID: 9900
	public int CurrentFloor;
}
