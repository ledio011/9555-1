using System;
using UnityEngine;

// Token: 0x02000994 RID: 2452
public class SkillDragItemLogic : UIDragItemEx
{
	// Token: 0x06004574 RID: 17780 RVA: 0x0015CCC8 File Offset: 0x0015AEC8
	protected override void Start()
	{
		base.Start();
	}

	// Token: 0x06004575 RID: 17781 RVA: 0x0015CCD0 File Offset: 0x0015AED0
	private void OnEnable()
	{
		this.dragObj = null;
	}

	// Token: 0x06004576 RID: 17782 RVA: 0x0015CCDC File Offset: 0x0015AEDC
	private void OnDisable()
	{
		if (this.dragObj != null)
		{
			NGUITools.Destroy(base.gameObject);
		}
	}

	// Token: 0x06004577 RID: 17783 RVA: 0x0015CCFC File Offset: 0x0015AEFC
	protected override void OnDragDropStart(GameObject OrginDrag)
	{
		SkillInfoBtnLogic component = OrginDrag.GetComponent<SkillInfoBtnLogic>();
		this.mCharacterSkillData = component.CharacterSkillData;
		if (this.mCharacterSkillData != null)
		{
			this.dragObj = OrginDrag;
			base.OnDragDropStart(OrginDrag);
		}
		else
		{
			base.OnDragDropRelease(OrginDrag);
		}
	}

	// Token: 0x06004578 RID: 17784 RVA: 0x0015CD44 File Offset: 0x0015AF44
	protected override void OnDragDropRelease(GameObject surface)
	{
		SkillDragSurface component = surface.GetComponent<SkillDragSurface>();
		if (component != null && this.mCharacterSkillData != null)
		{
			component.SelectSkill(this.mCharacterSkillData);
		}
		this.dragObj = null;
		base.OnDragDropRelease(surface);
	}

	// Token: 0x04003253 RID: 12883
	public CharacterSkillData mCharacterSkillData;

	// Token: 0x04003254 RID: 12884
	private GameObject dragObj;
}
