using System;
using UnityEngine;

// Token: 0x020009C8 RID: 2504
public class TeamTipMemberLineLogic : MonoBehaviour
{
	// Token: 0x17000FB6 RID: 4022
	// (get) Token: 0x06004742 RID: 18242 RVA: 0x0016B42C File Offset: 0x0016962C
	public TeamMember CurMember
	{
		get
		{
			return this.mCurMember;
		}
	}

	// Token: 0x06004743 RID: 18243 RVA: 0x0016B434 File Offset: 0x00169634
	public void Reset(TeamMember member)
	{
		this.mCurMember = member;
		if (member != null && member.IsValid())
		{
			UnityVersionUtil.SetActiveRecursive(this.DisableRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.EnableRoot, true);
			this.NameLabel.text = this.mCurMember.Name;
			this.PlayerIconSprite.spriteName = GameDefine.Player_Icon_Small_Pic[(int)this.mCurMember.Profession];
			this.LevelLabel.text = string.Format("{0}", member.Level);
			this.UpdateHP((float)this.mCurMember.HP / (float)this.mCurMember.MaxHP);
			if (this.mCurMember.TeamJob == 0)
			{
				NGUITools.SetActive(this.TeamLeaderPicObj, true);
			}
			else
			{
				NGUITools.SetActive(this.TeamLeaderPicObj, false);
			}
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.EnableRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.DisableRoot, true);
		}
	}

	// Token: 0x06004744 RID: 18244 RVA: 0x0016B52C File Offset: 0x0016972C
	public void UpdateHP(float percent)
	{
		this.HpSlider.value = percent;
	}

	// Token: 0x06004745 RID: 18245 RVA: 0x0016B53C File Offset: 0x0016973C
	public void OnClickLine()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamRootNew, null, null);
	}

	// Token: 0x06004746 RID: 18246 RVA: 0x0016B550 File Offset: 0x00169750
	public void OnClickDisableLine()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamRootNew, null, null);
	}

	// Token: 0x04003470 RID: 13424
	public UILabel NameLabel;

	// Token: 0x04003471 RID: 13425
	public UISprite PlayerIconSprite;

	// Token: 0x04003472 RID: 13426
	public UISlider HpSlider;

	// Token: 0x04003473 RID: 13427
	public UILabel LevelLabel;

	// Token: 0x04003474 RID: 13428
	public GameObject TeamLeaderPicObj;

	// Token: 0x04003475 RID: 13429
	public GameObject EnableRoot;

	// Token: 0x04003476 RID: 13430
	public GameObject DisableRoot;

	// Token: 0x04003477 RID: 13431
	private TeamMember mCurMember;
}
