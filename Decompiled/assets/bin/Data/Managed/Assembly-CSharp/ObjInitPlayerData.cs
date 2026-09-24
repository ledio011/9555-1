using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000821 RID: 2081
public class ObjInitPlayerData : ObjInitData
{
	// Token: 0x17000E7B RID: 3707
	// (get) Token: 0x06003373 RID: 13171 RVA: 0x000CA3E0 File Offset: 0x000C85E0
	public bool IsShowFashion
	{
		get
		{
			return this.visual.showType == 1L;
		}
	}

	// Token: 0x06003374 RID: 13172 RVA: 0x000CA3F4 File Offset: 0x000C85F4
	public void SetVisible(bool isVisible)
	{
		this.IsVisible = isVisible;
	}

	// Token: 0x17000E7C RID: 3708
	// (get) Token: 0x06003375 RID: 13173 RVA: 0x000CA400 File Offset: 0x000C8600
	public string HeadId
	{
		get
		{
			if (this.visual.showType == 0L)
			{
				return this.visual.HeadId;
			}
			return (!this.visual.HasFashion_HeadId) ? this.visual.HeadId : this.visual.Fashion_HeadId;
		}
	}

	// Token: 0x06003376 RID: 13174 RVA: 0x000CA454 File Offset: 0x000C8654
	public string GetWeaponID()
	{
		if (this.visual.showType == 0L)
		{
			return this.visual.WeaponId;
		}
		if (this.CheckWeaponIsSame())
		{
			return (!this.visual.HasFashion_WeaponId) ? this.visual.WeaponId : this.visual.Fashion_WeaponId;
		}
		return this.visual.WeaponId;
	}

	// Token: 0x17000E7D RID: 3709
	// (get) Token: 0x06003377 RID: 13175 RVA: 0x000CA4C0 File Offset: 0x000C86C0
	public string LegId
	{
		get
		{
			if (this.visual.showType == 0L)
			{
				return this.visual.LegId;
			}
			return (!this.visual.HasFashion_LegId) ? this.visual.LegId : this.visual.Fashion_LegId;
		}
	}

	// Token: 0x17000E7E RID: 3710
	// (get) Token: 0x06003378 RID: 13176 RVA: 0x000CA514 File Offset: 0x000C8714
	public string BodyId
	{
		get
		{
			if (this.visual.showType == 0L)
			{
				return this.visual.BodyId;
			}
			return (!this.visual.HasFashion_BodyId) ? this.visual.BodyId : this.visual.Fashion_BodyId;
		}
	}

	// Token: 0x17000E7F RID: 3711
	// (get) Token: 0x06003379 RID: 13177 RVA: 0x000CA568 File Offset: 0x000C8768
	public string WeaponItemId
	{
		get
		{
			return (!this.visual.HasWeaponItemId) ? null : this.visual.WeaponItemId;
		}
	}

	// Token: 0x17000E80 RID: 3712
	// (get) Token: 0x0600337A RID: 13178 RVA: 0x000CA58C File Offset: 0x000C878C
	public string FashionItemId
	{
		get
		{
			return (!this.visual.HasFashionItemId) ? null : this.visual.FashionItemId;
		}
	}

	// Token: 0x0600337B RID: 13179 RVA: 0x000CA5B0 File Offset: 0x000C87B0
	public bool CheckWeaponIsSame()
	{
		if (!string.IsNullOrEmpty(this.WeaponItemId) && !string.IsNullOrEmpty(this.FashionItemId))
		{
			EquipData equipDataById = DataManager.GetEquipDataById(this.WeaponItemId);
			EquipData equipDataById2 = DataManager.GetEquipDataById(this.FashionItemId);
			return equipDataById.WeaponType == equipDataById2.WeaponType;
		}
		return !this.visual.HasWeaponId || !this.visual.HasFashion_WeaponId || GameDefine.GetWeaponName(this.visual.WeaponId).Equals(GameDefine.GetWeaponName(this.visual.Fashion_WeaponId));
	}

	// Token: 0x0600337C RID: 13180 RVA: 0x000CA654 File Offset: 0x000C8854
	public void InitData(character_aoi character)
	{
		this.HP = (int)character.attribute_other.hp;
		this.EXP = character.attribute_other.exp;
		this.Level = (int)character.attribute_other.level;
		this.Attribute = character.runtime.attribute;
		this.AttributeAll = character.runtime.attribute_all;
		this.Speed = (float)this.AttributeAll.mov / 100f;
		this.ComboValue = (int)character.attribute_other.combValue;
		this.Profession = (PROFESSION_TYPE)character.general.profession;
		this.mPos = new Vector3((float)character.movement.pos.x / 100f, (float)character.movement.pos.y / 100f, (float)character.movement.pos.z / 100f);
		this.mDir = MathUtil.HeadingToVector3((float)character.movement.pos.o / 100f);
		this.mServerID = character.id;
		this.Name = character.visual.name;
		if (character.attribute_other.HasGuildId)
		{
			this.GuildName = character.attribute_other.guildName;
			this.GuildId = character.attribute_other.guildId;
		}
		this.mCharacterModelId = character.visual.ModeId;
		this.visual = character.visual;
		this.Camp = (GameDefine.CAMP_TYPE)character.attribute_other.camp;
		this.PkMode = (int)character.attribute_other.pkMode;
		this.GuildId = character.attribute_other.guildId;
		this.DanceState = (int)character.attribute_other.dance_state;
		this.DanceId = character.attribute_other.dance_id;
	}

	// Token: 0x0600337D RID: 13181 RVA: 0x000CA82C File Offset: 0x000C8A2C
	public void InitData(character character)
	{
		this.mPos = new Vector3((float)character.movement.pos.x / 100f, (float)character.movement.pos.y / 100f, (float)character.movement.pos.z / 100f);
		this.mDir = MathUtil.HeadingToVector3((float)character.movement.pos.o / 100f);
		this.mServerID = character.id;
		this.Profession = (PROFESSION_TYPE)character.general.profession;
		this.HP = (int)character.attribute_other.hp;
		this.EXP = character.attribute_other.exp;
		this.Level = (int)character.attribute_other.level;
		this.TitleLevel = (int)character.attribute_other.title_level;
		this.TitleExp = (int)character.attribute_other.title_exp;
		this.Attribute = character.runtime.attribute;
		this.AttributeAll = character.runtime.attribute_all;
		this.Speed = (float)this.AttributeAll.mov / 100f;
		this.TitleExp = (int)character.attribute_other.title_exp;
		this.TitleLevel = (int)character.attribute_other.title_level;
		this.ComboValue = (int)character.attribute_other.combValue;
		this.skills = character.skills;
		this.SkillIndex = (int)character.skill_index;
		this.Name = character.general.name;
		if (character.attribute_other.HasGuildId)
		{
			this.GuildId = character.attribute_other.guildId;
			this.GuildName = character.attribute_other.guildName;
		}
		this.mCharacterModelId = character.visual.ModeId;
		this.visual = character.visual;
		this.RefineLevel = (int)character.attribute_other.refineLevel;
		this.RefineNeckLevel = (int)character.attribute_other.refineNeckLevel;
		this.RefineRing1Level = (int)character.attribute_other.refineRing1Level;
		this.RefineRing2Level = (int)character.attribute_other.refineRing2Level;
		this.RefineBeltLevel = (int)character.attribute_other.refineBeltLevel;
		this.Camp = (GameDefine.CAMP_TYPE)character.attribute_other.camp;
		this.PkMode = (int)character.attribute_other.pkMode;
		this.GuildId = character.attribute_other.guildId;
		this.DanceState = (int)character.attribute_other.dance_state;
		this.DanceId = character.attribute_other.dance_id;
		if (character.HasEquip_enhance)
		{
			for (int i = 0; i < this.EnhanceLevelList.Length; i++)
			{
				if (character.equip_enhance.ContainsKey((long)i))
				{
					this.EnhanceLevelList[i] = (int)character.equip_enhance[(long)i].level;
				}
				else
				{
					this.EnhanceLevelList[i] = 0;
				}
			}
		}
	}

	// Token: 0x0600337E RID: 13182 RVA: 0x000CAB1C File Offset: 0x000C8D1C
	public void InitData(character_aoi_attribute character)
	{
		if (character.HasAttribute_other)
		{
			int hp = (int)character.attribute_other.hp;
			this.HP = hp;
			this.EXP = character.attribute_other.exp;
			this.Level = (int)character.attribute_other.level;
			this.TitleLevel = (int)character.attribute_other.title_level;
			this.TitleExp = (int)character.attribute_other.title_exp;
			this.ComboValue = (int)character.attribute_other.combValue;
			this.Camp = (GameDefine.CAMP_TYPE)character.attribute_other.camp;
			this.PkMode = (int)character.attribute_other.pkMode;
			this.GuildId = character.attribute_other.guildId;
			this.DanceState = (int)character.attribute_other.dance_state;
			this.DanceId = character.attribute_other.dance_id;
		}
		if (character.HasAttribute_all)
		{
			this.AttributeAll = character.attribute_all;
			this.Speed = (float)character.attribute_all.mov / 100f;
		}
		if (character.HasAttribute)
		{
			this.Attribute = character.attribute;
		}
		if (character.HasVisual)
		{
			this.visual = character.visual;
		}
	}

	// Token: 0x040021F9 RID: 8697
	public PROFESSION_TYPE Profession;

	// Token: 0x040021FA RID: 8698
	public attribute Attribute;

	// Token: 0x040021FB RID: 8699
	public attribute AttributeAll;

	// Token: 0x040021FC RID: 8700
	public characterVisual visual;

	// Token: 0x040021FD RID: 8701
	public int HP;

	// Token: 0x040021FE RID: 8702
	public long EXP;

	// Token: 0x040021FF RID: 8703
	public int Level;

	// Token: 0x04002200 RID: 8704
	public float Speed = 5f;

	// Token: 0x04002201 RID: 8705
	public float WalkSpeed = 1f;

	// Token: 0x04002202 RID: 8706
	public int Rec;

	// Token: 0x04002203 RID: 8707
	public int TitleLevel;

	// Token: 0x04002204 RID: 8708
	public int TitleExp;

	// Token: 0x04002205 RID: 8709
	public int ComboValue;

	// Token: 0x04002206 RID: 8710
	public Dictionary<string, skill_info> skills;

	// Token: 0x04002207 RID: 8711
	public int SkillIndex;

	// Token: 0x04002208 RID: 8712
	public int BackPackSize;

	// Token: 0x04002209 RID: 8713
	public int StorageSize;

	// Token: 0x0400220A RID: 8714
	public int RefineNeckLevel;

	// Token: 0x0400220B RID: 8715
	public int RefineRing1Level;

	// Token: 0x0400220C RID: 8716
	public int RefineRing2Level;

	// Token: 0x0400220D RID: 8717
	public int RefineBeltLevel;

	// Token: 0x0400220E RID: 8718
	public int RefineLevel;

	// Token: 0x0400220F RID: 8719
	public int[] EnhanceLevelList = new int[6];

	// Token: 0x04002210 RID: 8720
	public GameDefine.CAMP_TYPE Camp;

	// Token: 0x04002211 RID: 8721
	public int PkMode;

	// Token: 0x04002212 RID: 8722
	public new long GuildId = -1L;

	// Token: 0x04002213 RID: 8723
	public long TeamId = -1L;

	// Token: 0x04002214 RID: 8724
	public int DanceState;

	// Token: 0x04002215 RID: 8725
	public string DanceId = string.Empty;

	// Token: 0x04002216 RID: 8726
	public string AIID = string.Empty;

	// Token: 0x04002217 RID: 8727
	public string NpcId = string.Empty;

	// Token: 0x04002218 RID: 8728
	public bool IsVisible;
}
