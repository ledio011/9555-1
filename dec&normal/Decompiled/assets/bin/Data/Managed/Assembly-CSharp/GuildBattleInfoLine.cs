using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000A0D RID: 2573
public class GuildBattleInfoLine : MonoBehaviour
{
	// Token: 0x060049FA RID: 18938 RVA: 0x00180AFC File Offset: 0x0017ECFC
	public void updateItem(guild_battle_item_info curinfo, int ranknum, bool isScore, bool isBlue)
	{
		this.ranklabel.text = string.Format("NO.{0}", ranknum + 1);
		this.namelabel.text = curinfo.name;
		if (isScore)
		{
			this.infolabel.text = curinfo.score.ToString();
		}
		else
		{
			this.infolabel.text = curinfo.killNum.ToString();
		}
		if (isBlue)
		{
			this.ranklabel.color = this.blueColor;
			this.namelabel.color = this.blueColor;
			this.infolabel.color = this.blueColor;
		}
		else
		{
			this.ranklabel.color = this.redColor;
			this.namelabel.color = this.redColor;
			this.infolabel.color = this.redColor;
		}
	}

	// Token: 0x04003743 RID: 14147
	public UILabel ranklabel;

	// Token: 0x04003744 RID: 14148
	public UILabel namelabel;

	// Token: 0x04003745 RID: 14149
	public UILabel infolabel;

	// Token: 0x04003746 RID: 14150
	private Color blueColor = new Color(0.3647059f, 0.7058824f, 1f);

	// Token: 0x04003747 RID: 14151
	private Color redColor = Color.red;
}
