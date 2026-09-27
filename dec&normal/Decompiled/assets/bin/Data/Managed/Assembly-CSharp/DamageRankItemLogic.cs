using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000936 RID: 2358
public class DamageRankItemLogic : MonoBehaviour
{
	// Token: 0x06004196 RID: 16790 RVA: 0x00138560 File Offset: 0x00136760
	public void updateItem(int rankindex, damage_list curinfo)
	{
		this.ranklabel.text = string.Format("NO.{0}", rankindex);
		this.namelabel.text = curinfo.name;
		this.damagelabel.text = string.Format("{0}", curinfo.damage);
	}

	// Token: 0x06004197 RID: 16791 RVA: 0x001385BC File Offset: 0x001367BC
	public void updateItem(int rankindex, score_info curinfo)
	{
		this.ranklabel.text = string.Format("NO.{0}", rankindex);
		this.namelabel.text = curinfo.name;
		this.damagelabel.text = string.Format("{0}", curinfo.value);
	}

	// Token: 0x04002D74 RID: 11636
	public UILabel ranklabel;

	// Token: 0x04002D75 RID: 11637
	public UILabel namelabel;

	// Token: 0x04002D76 RID: 11638
	public UILabel damagelabel;
}
