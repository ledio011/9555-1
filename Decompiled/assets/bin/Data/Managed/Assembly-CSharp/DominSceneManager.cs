using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000877 RID: 2167
public class DominSceneManager : SceneManager
{
	// Token: 0x060039A3 RID: 14755 RVA: 0x000F7204 File Offset: 0x000F5404
	public override void Init(string id)
	{
		base.Init(id);
		SingletonUnity<MyEvent>.Instance.Register("OnZombiePlayerDie", this, "OnZombiePlayerDie");
		this.LockControl();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NotifyRootUI, null, null);
		this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.tempPlayerAutoCombatFlag = this.mPlayerData.IsOpenAutoCombat;
		this.mPlayerData.AutoComabat = false;
		this.mPlayerData.IsOpenAutoCombat = false;
	}

	// Token: 0x060039A4 RID: 14756 RVA: 0x000F7280 File Offset: 0x000F5480
	public void LockControl()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.JueseJiNengQuUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.YiDongKongZhiUI);
	}

	// Token: 0x060039A5 RID: 14757 RVA: 0x000F72A0 File Offset: 0x000F54A0
	private void OpenControl()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.JueseJiNengQuUI, null, null);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.YiDongKongZhiUI, null, null);
	}

	// Token: 0x060039A6 RID: 14758 RVA: 0x000F72D0 File Offset: 0x000F54D0
	private void PlayerMoveToCenter()
	{
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			mainPlayer.MoveTo(Vector3.zero, 1f, null);
		}
	}

	// Token: 0x060039A7 RID: 14759 RVA: 0x000F7308 File Offset: 0x000F5508
	public override void StartGame()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PVPBeforeStartRoot);
		SingletonUnity<UIManager>.Instance.ShowDefaultUI();
		this.mPlayerData.IsOpenAutoCombat = this.tempPlayerAutoCombatFlag;
		this.mPlayerData.AutoComabat = this.tempPlayerAutoCombatFlag;
		if (SingletonUnity<CopyFunctionRootLogic>.Exists)
		{
			SingletonUnity<CopyFunctionRootLogic>.Instance.Reset(base.CurrentMapInofData.MapType, base.CurrentMapInofData.Name);
		}
		this.PlayerMoveToCenter();
		this.OpenControl();
		this.EnableZombiePlayer();
	}

	// Token: 0x060039A8 RID: 14760 RVA: 0x000F738C File Offset: 0x000F558C
	public override void SuccessMission()
	{
	}

	// Token: 0x060039A9 RID: 14761 RVA: 0x000F7390 File Offset: 0x000F5590
	~DominSceneManager()
	{
		SingletonUnity<MyEvent>.Instance.DeRegister("OnZombiePlayerDie", this, "OnZombiePlayerDie");
	}

	// Token: 0x060039AA RID: 14762 RVA: 0x000F73DC File Offset: 0x000F55DC
	private void EnableZombiePlayer()
	{
		List<ObjCharacter> list = Singleton<ObjManager>.Instance.CampList[2];
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_PLAYER)
			{
				ObjZombiePlayer objZombiePlayer = list[i] as ObjZombiePlayer;
				if (objZombiePlayer != null)
				{
					objZombiePlayer.ActiveAutoFight();
				}
			}
		}
	}

	// Token: 0x060039AB RID: 14763 RVA: 0x000F7440 File Offset: 0x000F5640
	public void OnZombiePlayerDie(object obj)
	{
		SingletonUnity<MyEvent>.Instance.DeRegister("OnZombiePlayerDie", this, "OnZombiePlayerDie");
		this.SuccessMission();
	}

	// Token: 0x040025C7 RID: 9671
	private GameObject men1Obj;

	// Token: 0x040025C8 RID: 9672
	private GameObject men2Obj;

	// Token: 0x040025C9 RID: 9673
	private PlayerData mPlayerData;

	// Token: 0x040025CA RID: 9674
	private bool tempPlayerAutoCombatFlag;
}
