using System;
using UnityEngine;

// Token: 0x020009E0 RID: 2528
public class HeadInfoLogic : MonoBehaviour
{
	// Token: 0x060047D3 RID: 18387 RVA: 0x0016F57C File Offset: 0x0016D77C
	public virtual void Init()
	{
	}

	// Token: 0x060047D4 RID: 18388 RVA: 0x0016F580 File Offset: 0x0016D780
	public virtual void SetHpVal(float val)
	{
		if (this.mHpLineLogic != null)
		{
			this.mHpLineLogic.ChangeVal(val);
		}
	}

	// Token: 0x060047D5 RID: 18389 RVA: 0x0016F5A0 File Offset: 0x0016D7A0
	public virtual void ForceSetHpVal(float val)
	{
		if (this.mHpLineLogic != null)
		{
			this.mHpLineLogic.ForceSetVal(val);
		}
	}

	// Token: 0x0400352C RID: 13612
	public UILabel NameLabel;

	// Token: 0x0400352D RID: 13613
	public HPLineLogic mHpLineLogic;
}
