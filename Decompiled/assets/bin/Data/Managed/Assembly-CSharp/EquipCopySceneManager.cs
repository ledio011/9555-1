using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000879 RID: 2169
public class EquipCopySceneManager : SceneManager
{
	// Token: 0x060039B3 RID: 14771 RVA: 0x000F751C File Offset: 0x000F571C
	public override void Init(string id)
	{
		base.Init(id);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExpBattleInfoRoot, delegate
		{
			SingletonUnity<ExpBattleInfoRootLogic>.Instance.ResetToTeam();
		}, null);
		this.InitBlock();
	}

	// Token: 0x060039B4 RID: 14772 RVA: 0x000F7564 File Offset: 0x000F5764
	private void InitBlock()
	{
		this.OpenBlockList.Clear();
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

	// Token: 0x060039B5 RID: 14773 RVA: 0x000F760C File Offset: 0x000F580C
	public void OpenBlock(int i)
	{
		if (this.mBlockControllerList.Count > 0 && i > 0)
		{
			if (this.OpenBlockList.Contains(i))
			{
				return;
			}
			this.OpenBlockList.Add(i);
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
				for (int j = 0; j < Singleton<ObjManager>.Instance.ObjOtherPlayerVisibleList.Count; j++)
				{
					Singleton<ObjManager>.Instance.ObjOtherPlayerVisibleList[j].NavMeshAgent.walkableMask += 2 << i + 1;
				}
			}, null);
		}
	}

	// Token: 0x060039B6 RID: 14774 RVA: 0x000F76E4 File Offset: 0x000F58E4
	public override void Update()
	{
		base.Update();
	}

	// Token: 0x040025CF RID: 9679
	private List<BlockController> mBlockControllerList = new List<BlockController>();

	// Token: 0x040025D0 RID: 9680
	private List<int> OpenBlockList = new List<int>();
}
