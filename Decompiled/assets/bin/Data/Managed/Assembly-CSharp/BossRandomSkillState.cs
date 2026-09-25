using System;

// Token: 0x020000E7 RID: 231
public class BossRandomSkillState : FSMState<AILogic>
{
	// Token: 0x06000741 RID: 1857 RVA: 0x00032D04 File Offset: 0x00030F04
	public override void Enter(AILogic owner, int previous)
	{
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x00032D08 File Offset: 0x00030F08
	public override void Exit(AILogic owner, int previous)
	{
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x00032D0C File Offset: 0x00030F0C
	public override void Update(AILogic owner)
	{
		if (!owner.Ownner.IsDie)
		{
			this.CheckUseSkill(owner);
		}
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x00032D28 File Offset: 0x00030F28
	public void CheckUseSkill(AILogic owner)
	{
		this.UseSkill(owner.Ownner);
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x00032D38 File Offset: 0x00030F38
	private void UseSkill(ObjNPC owner)
	{
		if (owner.CharacterSkillData.Count == 1)
		{
			owner.UseSkill(owner.CharacterSkillData[0].ID, null);
		}
		else
		{
			string targetSkill = this.GetTargetSkill(owner);
			if (!string.IsNullOrEmpty(targetSkill))
			{
				owner.UseSkill(targetSkill, null);
			}
		}
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x00032D90 File Offset: 0x00030F90
	private string GetTargetSkill(ObjNPC owner)
	{
		if (owner.EnableSkillIDList.Count == 0)
		{
			return string.Empty;
		}
		return owner.EnableSkillIDList[0].ID;
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00032DC4 File Offset: 0x00030FC4
	public override void Notify(int messageID, params object[] messageParams)
	{
	}
}
