using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000A16 RID: 2582
public class GuildBattleScoreLine : MonoBehaviour
{
	// Token: 0x06004A45 RID: 19013 RVA: 0x00184BD0 File Offset: 0x00182DD0
	public void UpdateInfo(guild_battle_item_info curInfo, int rank)
	{
		this.Ranklabel.text = string.Format("NO.{0}", rank + 1);
		if (rank < 3)
		{
			this.TopSp.spriteName = GameDefine.RANK_PICNAME[rank];
			this.TopSp.enabled = true;
		}
		else
		{
			this.TopSp.enabled = false;
		}
		this.PlayerName.text = curInfo.name;
		this.GuildName.text = curInfo.guildName;
		this.killLabel.text = curInfo.killNum.ToString();
		this.MaxKillLabel.text = curInfo.continueKill.ToString();
		this.ScoreLabel.text = curInfo.score.ToString();
	}

	// Token: 0x040037C0 RID: 14272
	public UILabel Ranklabel;

	// Token: 0x040037C1 RID: 14273
	public UISprite TopSp;

	// Token: 0x040037C2 RID: 14274
	public UILabel PlayerName;

	// Token: 0x040037C3 RID: 14275
	public UILabel GuildName;

	// Token: 0x040037C4 RID: 14276
	public UILabel killLabel;

	// Token: 0x040037C5 RID: 14277
	public UILabel MaxKillLabel;

	// Token: 0x040037C6 RID: 14278
	public UILabel ScoreLabel;
}
