using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020A RID: 522
public class AutoSearchPath
{
	// Token: 0x170003B9 RID: 953
	// (get) Token: 0x060011B2 RID: 4530 RVA: 0x00072310 File Offset: 0x00070510
	public List<AutoSearchPathPoint> PathPointList
	{
		get
		{
			return this.mPathPointList;
		}
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x00072318 File Offset: 0x00070518
	public void ResetPath()
	{
		this.mPathPointList.Clear();
	}

	// Token: 0x060011B4 RID: 4532 RVA: 0x00072328 File Offset: 0x00070528
	public void AddPathPoint(AutoSearchPathPoint point)
	{
		if (this.mPathPointList != null)
		{
			this.mPathPointList.Add(point);
		}
	}

	// Token: 0x060011B5 RID: 4533 RVA: 0x00072344 File Offset: 0x00070544
	public bool IsFinish(Vector3 pos)
	{
		return this.mPathPointList.Count <= 1 && (this.mPathPointList.Count != 1 || this.IsArrivePoint(pos));
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x00072380 File Offset: 0x00070580
	public bool IsArrivePoint(Vector3 pos)
	{
		if (this.mPathPointList.Count == 0)
		{
			return false;
		}
		AutoSearchPathPoint autoSearchPathPoint = this.mPathPointList[0];
		return autoSearchPathPoint.SceneId.Equals(SingletonDontDestoryUnity<GameManager>.Instance.RunningMapIdStr) && VectorXZ.Distance(pos, new VectorXZ(autoSearchPathPoint.PosX, autoSearchPathPoint.PosZ)) < 1f;
	}

	// Token: 0x0400177C RID: 6012
	private List<AutoSearchPathPoint> mPathPointList = new List<AutoSearchPathPoint>();
}
