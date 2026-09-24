using System;

// Token: 0x020001B1 RID: 433
[Serializable]
public class CharacterSkillData
{
	// Token: 0x0600101D RID: 4125 RVA: 0x00065F94 File Offset: 0x00064194
	public CharacterSkillData(string skillID, int level, int pos, int pos2, bool isDisable)
	{
		this.ID = skillID;
		this.Level = level;
		this.Index = pos;
		this.Index2 = pos2;
		this.CDTimeCount = 0f;
		this.IsDisable = isDisable;
		if (!isDisable && !string.IsNullOrEmpty(skillID))
		{
			SkillData skillDataById = DataManager.GetSkillDataById(skillID);
			if (skillDataById != null)
			{
				this.UnlockLevel = skillDataById.Locklevel;
			}
		}
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x00066018 File Offset: 0x00064218
	public CharacterSkillData(string skillID)
	{
		this.ID = skillID;
		this.CDTimeCount = 0f;
	}

	// Token: 0x1700037B RID: 891
	// (get) Token: 0x0600101F RID: 4127 RVA: 0x00066054 File Offset: 0x00064254
	// (set) Token: 0x06001020 RID: 4128 RVA: 0x00066064 File Offset: 0x00064264
	public float CDTimeCount
	{
		get
		{
			return this.cdTimeCount.value;
		}
		set
		{
			this.cdTimeCount.value = value;
		}
	}

	// Token: 0x040012C2 RID: 4802
	private XorFloat cdTimeCount = new XorFloat();

	// Token: 0x040012C3 RID: 4803
	public string ID = string.Empty;

	// Token: 0x040012C4 RID: 4804
	public int Level;

	// Token: 0x040012C5 RID: 4805
	public int Index;

	// Token: 0x040012C6 RID: 4806
	public int Index2;

	// Token: 0x040012C7 RID: 4807
	public int UnlockLevel;

	// Token: 0x040012C8 RID: 4808
	public bool IsDisable;
}
