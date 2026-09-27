using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200087E RID: 2174
public class MultiKillBossSceneManager : SceneManager
{
	// Token: 0x060039E0 RID: 14816 RVA: 0x000F8604 File Offset: 0x000F6804
	public override void Init(string id)
	{
		base.Init(id);
		this.InitBlock();
	}

	// Token: 0x060039E1 RID: 14817 RVA: 0x000F8614 File Offset: 0x000F6814
	~MultiKillBossSceneManager()
	{
	}

	// Token: 0x060039E2 RID: 14818 RVA: 0x000F864C File Offset: 0x000F684C
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

	// Token: 0x060039E3 RID: 14819 RVA: 0x000F86E8 File Offset: 0x000F68E8
	public void OpenBlock(int i)
	{
		if (i > 0)
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
				Singleton<ObjManager>.Instance.MainPlayer.NavMeshAgent.walkableMask += 2 << i + 1;
				mainPlayer.MoveTo(trs.position, 3f, null);
				Singleton<ObjManager>.Instance.MainPlayer.CameraController.BackToPlayer(1.5f);
			}, null);
		}
	}

	// Token: 0x040025F9 RID: 9721
	public int mStateIndex;

	// Token: 0x040025FA RID: 9722
	private int mWaveCount;

	// Token: 0x040025FB RID: 9723
	private List<BlockController> mBlockControllerList = new List<BlockController>();
}
