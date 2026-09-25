using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000878 RID: 2168
public class EXPSceneManager : SceneManager
{
	// Token: 0x060039AD RID: 14765 RVA: 0x000F7474 File Offset: 0x000F5674
	public override void Init(string id)
	{
		base.Init(id);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExpBattleInfoRoot, delegate
		{
			SingletonUnity<ExpBattleInfoRootLogic>.Instance.EnableReset();
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ExpBattleInfoRoot);
		}, null);
	}

	// Token: 0x060039AE RID: 14766 RVA: 0x000F74B8 File Offset: 0x000F56B8
	public void OpenBlock()
	{
	}

	// Token: 0x060039AF RID: 14767 RVA: 0x000F74BC File Offset: 0x000F56BC
	private void OnArriveMovePoint(Vector3 pos)
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CarTargetRoot);
		NetLogic.GetInstance().Send<Protocol.start_battle>(null, null);
	}

	// Token: 0x060039B0 RID: 14768 RVA: 0x000F74DC File Offset: 0x000F56DC
	public override void AutoFightAction()
	{
	}

	// Token: 0x040025CB RID: 9675
	private List<Vector3> mPathPointList = new List<Vector3>();

	// Token: 0x040025CC RID: 9676
	private MovePathPoint mMoveTargetPoint;

	// Token: 0x040025CD RID: 9677
	private int mCurPointIndex;
}
