using System;
using SprotoType;
using UnityEngine;

// Token: 0x020009C1 RID: 2497
public class TeamLineRoot : MonoBehaviour
{
	// Token: 0x06004717 RID: 18199 RVA: 0x00169DC4 File Offset: 0x00167FC4
	public void Reset(team tData, bool isApplied)
	{
		this.mCurTeamData = tData;
		this.PlayerProfessionPic.spriteName = GameDefine.Player_Profession_Pic[(int)this.mCurTeamData.teamleader.profession];
		this.PlayerLevelLabel.text = string.Format("Lv.{0}", this.mCurTeamData.teamleader.level);
		this.PlayerNameLabel.text = this.mCurTeamData.teamleader.name;
		this.PlayerComboValLabel.text = string.Format("{0}", this.mCurTeamData.teamleader.combValue);
		this.TeamLimitLevelLabel.text = string.Format("Lv.{0}~{1}", this.mCurTeamData.minLevel, this.mCurTeamData.maxLevel);
		this.TeamMemberNumLabel.text = string.Format("{0}/{1}", (this.mCurTeamData.teammembers == null) ? 1 : (this.mCurTeamData.teammembers.Count + 1), 4);
		this.mIsApplied = isApplied;
		this.playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (this.mIsApplied)
		{
			this.ApplyBtnLabel.text = StrDictionary.GetDictionaryString("#{100744}", new object[0]);
			this.ApplyBtn.SetState(UIButtonColor.State.Disabled, true);
			this.ApplyBtnCollider.enabled = false;
		}
		else if ((long)this.playerData.MainPlayerAttrData.Level >= this.mCurTeamData.minLevel && (long)this.playerData.MainPlayerAttrData.Level <= this.mCurTeamData.maxLevel)
		{
			this.ApplyBtnLabel.text = StrDictionary.GetDictionaryString("#{100812}", new object[0]);
			this.ApplyBtn.SetState(UIButtonColor.State.Normal, true);
			this.ApplyBtnCollider.enabled = true;
		}
		else
		{
			this.ApplyBtnLabel.text = StrDictionary.GetDictionaryString("#{100812}", new object[0]);
			this.ApplyBtn.SetState(UIButtonColor.State.Disabled, true);
			this.ApplyBtnCollider.enabled = false;
		}
	}

	// Token: 0x06004718 RID: 18200 RVA: 0x00169FF0 File Offset: 0x001681F0
	public void OnClickApplyBtn()
	{
		if (!this.mIsApplied)
		{
			this.mIsApplied = true;
			this.ApplyBtnLabel.text = StrDictionary.GetDictionaryString("#{100744}", new object[0]);
			this.ApplyBtn.SetState(UIButtonColor.State.Disabled, true);
			this.ApplyBtnCollider.enabled = false;
			req_join_team.request request = new req_join_team.request();
			request.teamid = this.mCurTeamData.id;
			request.isapply = false;
			NetLogic.GetInstance().Send<Protocol.req_join_team>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo.AddApplyTeam(this.mCurTeamData);
		}
	}

	// Token: 0x04003429 RID: 13353
	public UISprite PlayerProfessionPic;

	// Token: 0x0400342A RID: 13354
	public UILabel PlayerLevelLabel;

	// Token: 0x0400342B RID: 13355
	public UILabel PlayerNameLabel;

	// Token: 0x0400342C RID: 13356
	public UILabel PlayerComboValLabel;

	// Token: 0x0400342D RID: 13357
	public UILabel TeamLimitLevelLabel;

	// Token: 0x0400342E RID: 13358
	public UILabel TeamMemberNumLabel;

	// Token: 0x0400342F RID: 13359
	public UILabel ApplyBtnLabel;

	// Token: 0x04003430 RID: 13360
	public UIButton ApplyBtn;

	// Token: 0x04003431 RID: 13361
	public BoxCollider ApplyBtnCollider;

	// Token: 0x04003432 RID: 13362
	private bool mIsApplied;

	// Token: 0x04003433 RID: 13363
	private team mCurTeamData;

	// Token: 0x04003434 RID: 13364
	private PlayerData playerData;
}
