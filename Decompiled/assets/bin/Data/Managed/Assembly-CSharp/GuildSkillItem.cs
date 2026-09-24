using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008B3 RID: 2227
public class GuildSkillItem : MonoBehaviour
{
	// Token: 0x06003C15 RID: 15381 RVA: 0x001068F8 File Offset: 0x00104AF8
	public void Init(guild_skill skill)
	{
		List<GuildSkillData> guildSkillDataListByType = DataManager.GetGuildSkillDataListByType((int)skill.skillType);
		int num = (int)skill.level - 1;
		if (num < 0)
		{
			num = 0;
		}
		GuildSkillData guildSkillData = guildSkillDataListByType[num];
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		int guilLevel = playerData.PlayerGuild.GuilLevel;
		if (guilLevel >= guildSkillData.LevelLimit)
		{
			this.infoLabel.text = string.Format("{0}/{1}", skill.level, guildSkillDataListByType.Count);
			this.iconSprite.spriteName = guildSkillData.icon;
			this.bkSprite.alpha = 1f;
			this.iconSprite.alpha = 1f;
		}
		else
		{
			this.infoLabel.text = string.Format("{0}/{1}", skill.level, guildSkillDataListByType.Count);
			this.bkSprite.alpha = 0.5f;
			this.iconSprite.alpha = 0.5f;
		}
		this.iconSprite.MakePixelPerfect();
		this.curSkill = skill;
	}

	// Token: 0x06003C16 RID: 15382 RVA: 0x00106A10 File Offset: 0x00104C10
	public void OnClickItem()
	{
		SingletonUnity<GuildSkillRootLogic>.Instance.OnClickItem(this.curSkill, base.gameObject);
	}

	// Token: 0x0400273B RID: 10043
	public UISprite iconSprite;

	// Token: 0x0400273C RID: 10044
	public UISprite bkSprite;

	// Token: 0x0400273D RID: 10045
	public UILabel infoLabel;

	// Token: 0x0400273E RID: 10046
	private guild_skill curSkill;
}
