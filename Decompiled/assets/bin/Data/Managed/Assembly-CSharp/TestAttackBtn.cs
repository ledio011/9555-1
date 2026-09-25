using System;
using UnityEngine;

// Token: 0x0200089B RID: 2203
public class TestAttackBtn : MonoBehaviour
{
	// Token: 0x06003B8E RID: 15246 RVA: 0x001044FC File Offset: 0x001026FC
	public void OnClick()
	{
		if (this.camboFlag)
		{
			if (!Singleton<ObjManager>.Instance.MainPlayer.SkillLogic.IsUsingSkill)
			{
				Singleton<ObjManager>.Instance.MainPlayer.UseComboSkill();
			}
		}
		else if (!Singleton<ObjManager>.Instance.MainPlayer.SkillLogic.IsUsingSkill)
		{
			Singleton<ObjManager>.Instance.MainPlayer.UseSkill(this.skillId, null);
		}
	}

	// Token: 0x06003B8F RID: 15247 RVA: 0x00104570 File Offset: 0x00102770
	private void Start()
	{
	}

	// Token: 0x06003B90 RID: 15248 RVA: 0x00104574 File Offset: 0x00102774
	private void Update()
	{
	}

	// Token: 0x0400270E RID: 9998
	public bool camboFlag;

	// Token: 0x0400270F RID: 9999
	public string skillId;
}
