using System;
using UnityEngine;

// Token: 0x0200099B RID: 2459
public class SlotLineItemLogic : MonoBehaviour
{
	// Token: 0x060045AD RID: 17837 RVA: 0x0015F09C File Offset: 0x0015D29C
	public void Reset(SlotIconData curcion, int index)
	{
		if (this.mTra == null)
		{
			this.mTra = base.transform;
		}
		this.ResetPos(index);
		this.UpdateInfo(curcion);
		this.StopAnima();
	}

	// Token: 0x060045AE RID: 17838 RVA: 0x0015F0DC File Offset: 0x0015D2DC
	public void ResetPos(int index)
	{
		this.mTra.localPosition = new Vector3(0f, (float)(360 - index * 90), 0f);
		this.idx = index;
	}

	// Token: 0x060045AF RID: 17839 RVA: 0x0015F118 File Offset: 0x0015D318
	public void UpdateInfo(SlotIconData curcion)
	{
		this.iconSp.spriteName = curcion.IconName;
		this.iconSp.MakePixelPerfect();
		this.curiconinfo = curcion;
		this.iconid = this.curiconinfo.ID;
	}

	// Token: 0x060045B0 RID: 17840 RVA: 0x0015F15C File Offset: 0x0015D35C
	public void PlayeAnima()
	{
		this.scaleanima.PlayForward();
	}

	// Token: 0x060045B1 RID: 17841 RVA: 0x0015F16C File Offset: 0x0015D36C
	public void StopAnima()
	{
		if (!this.scaleanima.enabled)
		{
			return;
		}
		if (this.scaleanima.mAmountPerDelta < 0f)
		{
			this.scaleanima.mAmountPerDelta = -this.scaleanima.mAmountPerDelta;
		}
		this.scaleanima.ResetToBeginning();
		this.scaleanima.enabled = false;
	}

	// Token: 0x060045B2 RID: 17842 RVA: 0x0015F1D0 File Offset: 0x0015D3D0
	public void SetMoveToUp(int countnum)
	{
		this.mTra.localPosition = new Vector3(0f, (float)(360 + countnum * 90), 0f);
	}

	// Token: 0x060045B3 RID: 17843 RVA: 0x0015F1F8 File Offset: 0x0015D3F8
	public void MoveTo(int end)
	{
		this.mTra.localPosition = new Vector3(0f, (float)(360 - end * 90), 0f);
	}

	// Token: 0x040032A6 RID: 12966
	private Transform mTra;

	// Token: 0x040032A7 RID: 12967
	public TweenScale scaleanima;

	// Token: 0x040032A8 RID: 12968
	public UISprite iconSp;

	// Token: 0x040032A9 RID: 12969
	public int idx;

	// Token: 0x040032AA RID: 12970
	private Vector3 endPos;

	// Token: 0x040032AB RID: 12971
	public SlotIconData curiconinfo;

	// Token: 0x040032AC RID: 12972
	public string iconid;
}
