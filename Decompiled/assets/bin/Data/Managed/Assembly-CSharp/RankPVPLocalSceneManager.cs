using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000881 RID: 2177
public class RankPVPLocalSceneManager : SceneManager
{
	// Token: 0x06003A3C RID: 14908 RVA: 0x000FB5C8 File Offset: 0x000F97C8
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

	// Token: 0x06003A3D RID: 14909 RVA: 0x000FB644 File Offset: 0x000F9844
	public void LockControl()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.JueseJiNengQuUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.YiDongKongZhiUI);
	}

	// Token: 0x06003A3E RID: 14910 RVA: 0x000FB664 File Offset: 0x000F9864
	private void OpenControl()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.JueseJiNengQuUI, null, null);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.YiDongKongZhiUI, null, null);
	}

	// Token: 0x06003A3F RID: 14911 RVA: 0x000FB694 File Offset: 0x000F9894
	private void PlayerMoveToCenter()
	{
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			mainPlayer.MoveTo(Vector3.zero, 1f, null);
		}
	}

	// Token: 0x06003A40 RID: 14912 RVA: 0x000FB6CC File Offset: 0x000F98CC
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

	// Token: 0x06003A41 RID: 14913 RVA: 0x000FB750 File Offset: 0x000F9950
	public override void SuccessMission()
	{
		rank_pvp_other_player_die.request rpcReq = new rank_pvp_other_player_die.request();
		NetLogic.GetInstance().Send<Protocol.rank_pvp_other_player_die>(rpcReq, null);
	}

	// Token: 0x06003A42 RID: 14914 RVA: 0x000FB770 File Offset: 0x000F9970
	~RankPVPLocalSceneManager()
	{
		SingletonUnity<MyEvent>.Instance.DeRegister("OnZombiePlayerDie", this, "OnZombiePlayerDie");
	}

	// Token: 0x06003A43 RID: 14915 RVA: 0x000FB7BC File Offset: 0x000F99BC
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

	// Token: 0x06003A44 RID: 14916 RVA: 0x000FB820 File Offset: 0x000F9A20
	public void OnZombiePlayerDie(object obj)
	{
		SingletonUnity<MyEvent>.Instance.DeRegister("OnZombiePlayerDie", this, "OnZombiePlayerDie");
		this.SuccessMission();
	}

	// Token: 0x04002642 RID: 9794
	private GameObject men1Obj;

	// Token: 0x04002643 RID: 9795
	private GameObject men2Obj;

	// Token: 0x04002644 RID: 9796
	private PlayerData mPlayerData;

	// Token: 0x04002645 RID: 9797
	private bool tempPlayerAutoCombatFlag;
}
