using System;
using UnityEngine;

// Token: 0x02000995 RID: 2453
public class SkillDragSurface : MonoBehaviour
{
	// Token: 0x0600457A RID: 17786 RVA: 0x0015CD94 File Offset: 0x0015AF94
	public void SelectSkill(CharacterSkillData skillData)
	{
		SingletonUnity<SkillInfoRootLogic>.Instance.SelectSkill(skillData.ID, this.index);
	}

	// Token: 0x04003255 RID: 12885
	public int index;
}
