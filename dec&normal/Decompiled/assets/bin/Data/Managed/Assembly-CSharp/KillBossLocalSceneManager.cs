using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200087C RID: 2172
public class KillBossLocalSceneManager : SceneManager
{
	// Token: 0x060039C8 RID: 14792 RVA: 0x000F7B5C File Offset: 0x000F5D5C
	public override void Init(string id)
	{
		base.Init(id);
		this.InitBlock();
		this.mWaveCount = base.GetMonsterGroupCount();
		this.mStateIndex = 0;
		this.ResetStage(this.mStateIndex);
	}

	// Token: 0x060039C9 RID: 14793 RVA: 0x000F7B98 File Offset: 0x000F5D98
	private void InitBlock()
	{
		GameObject gameObject = GameObject.Find("zudang");
		if (gameObject != null)
		{
			Transform transform = gameObject.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				BlockController component = child.GetComponent<BlockController>();
				if (component != null)
				{
					component.Init();
					this.mBlockControllerList.Add(component);
				}
			}
		}
		this.mBlockControllerList.Sort(delegate(BlockController block1, BlockController block2)
		{
			string name = block1.gameObject.name;
			string name2 = block2.gameObject.name;
			if (name.Length == name2.Length)
			{
				return name.CompareTo(name2);
			}
			if (name.Length > name2.Length)
			{
				return 1;
			}
			return -1;
		});
	}

	// Token: 0x060039CA RID: 14794 RVA: 0x000F7C34 File Offset: 0x000F5E34
	private void ResetStage(int index)
	{
		if (index >= this.mWaveCount)
		{
			return;
		}
		this.OpenBlock(index);
		int num = this.mStateIndex + 1;
	}

	// Token: 0x060039CB RID: 14795 RVA: 0x000F7C60 File Offset: 0x000F5E60
	private void NpcCreate(List<MonsterData> list)
	{
		if (list == null)
		{
			return;
		}
		for (int i = 0; i < list.Count; i++)
		{
			ObjInitNpcData objInitNpcData = new ObjInitNpcData();
			MonsterData monsterData = list[i];
			objInitNpcData.mServerID = UUID.GenUUID();
			objInitNpcData.mPos = new Vector3(monsterData.PositionX, 0f, monsterData.PositionZ);
			NpcData npcDataByID = DataManager.GetNpcDataByID(monsterData.NpcID);
			objInitNpcData.npcInfoData = npcDataByID;
			objInitNpcData.HP = npcDataByID.Hp;
			objInitNpcData.MaxHP = npcDataByID.Hp;
			Singleton<ObjManager>.Instance.CreateNPC(objInitNpcData, null, null);
		}
	}

	// Token: 0x060039CC RID: 14796 RVA: 0x000F7CFC File Offset: 0x000F5EFC
	private void test()
	{
		ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(99L);
		Singleton<ObjManager>.Instance.MainPlayer.CameraController.BossDieCameraEffect(objCharacter);
	}

	// Token: 0x060039CD RID: 14797 RVA: 0x000F7D2C File Offset: 0x000F5F2C
	public override void OnNPCDie(object objNpc)
	{
	}

	// Token: 0x060039CE RID: 14798 RVA: 0x000F7D30 File Offset: 0x000F5F30
	public void OpenBlock(int i)
	{
		if (this.mBlockControllerList.Count > 0 && i > 0)
		{
			Transform trs = this.mBlockControllerList[i - 1].transform;
			ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			vp_Timer.In(1f, delegate()
			{
				this.mBlockControllerList[i - 1].OpenBlock(1f);
				mainPlayer.CameraController.LookAtGameObject(trs, 1f, null);
			}, null);
			vp_Timer.In(3f, delegate()
			{
				mainPlayer.MoveTo(trs.position, 3f, null);
				Singleton<ObjManager>.Instance.MainPlayer.NavMeshAgent.walkableMask += 2 << i + 1;
				Singleton<ObjManager>.Instance.MainPlayer.CameraController.BackToPlayer(1.5f);
			}, null);
		}
	}

	// Token: 0x060039CF RID: 14799 RVA: 0x000F7DE0 File Offset: 0x000F5FE0
	public override void Update()
	{
		base.Update();
	}

	// Token: 0x040025DB RID: 9691
	public int mStateIndex;

	// Token: 0x040025DC RID: 9692
	private int mWaveCount;

	// Token: 0x040025DD RID: 9693
	private List<BlockController> mBlockControllerList = new List<BlockController>();

	// Token: 0x040025DE RID: 9694
	private float time = 5f;
}
