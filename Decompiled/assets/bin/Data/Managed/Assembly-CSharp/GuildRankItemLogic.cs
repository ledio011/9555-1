using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000976 RID: 2422
public class GuildRankItemLogic : MonoBehaviour
{
	// Token: 0x06004463 RID: 17507 RVA: 0x00154850 File Offset: 0x00152A50
	public void ResetInfo(guild_info curinfo, int rankindex)
	{
		this.RankLabel.text = string.Format("NO.{0}", rankindex + 1);
		if (rankindex < 3)
		{
			this.RankPic.spriteName = GameDefine.RANK_PICNAME[rankindex];
			this.RankPic.enabled = true;
		}
		else
		{
			this.RankPic.enabled = false;
		}
		this.nameLabel.text = curinfo.guildName;
		this.chairmanlabel.text = curinfo.guildChiefName;
		this.lvlabel.text = curinfo.guildLevel.ToString();
		this.infoLabel.text = curinfo.guildCombo.ToString();
	}

	// Token: 0x040030F5 RID: 12533
	public UILabel RankLabel;

	// Token: 0x040030F6 RID: 12534
	public UISprite RankPic;

	// Token: 0x040030F7 RID: 12535
	public UILabel nameLabel;

	// Token: 0x040030F8 RID: 12536
	public UILabel chairmanlabel;

	// Token: 0x040030F9 RID: 12537
	public UILabel lvlabel;

	// Token: 0x040030FA RID: 12538
	public UILabel infoLabel;
}
