using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000A0F RID: 2575
public class GuildBattleMemberLine : MonoBehaviour
{
	// Token: 0x06004A0B RID: 18955 RVA: 0x0018168C File Offset: 0x0017F88C
	public void UpdateInfo(guild_member_info curinfo, int index)
	{
		this.mCurIndex = index;
		this.mCurInfo = curinfo;
		this.NameLabel.text = curinfo.name;
		this.PowerLabel.text = string.Format("{0} {1}", StrDictionary.GetDictionaryString("#{100421}", new object[0]), curinfo.combValue);
		checked
		{
			this.playericon.spriteName = GameDefine.Player_Icon_Pic[(int)((IntPtr)curinfo.profession)];
			this.professLabel.text = StrDictionary.GetDictionaryString(GameDefine.GuildJobStr[(int)((IntPtr)curinfo.job)], new object[0]);
			if (curinfo.state == 1L)
			{
				this.playericon.alpha = 1f;
				if (curinfo.characterId == PlayerData.MainPlayerServerId)
				{
					this.BkSprite.spriteName = "CZ_huaDongBG_1";
				}
				else
				{
					this.BkSprite.spriteName = "CZ_huaDongBG";
				}
			}
			else
			{
				this.playericon.alpha = 0.35f;
				this.BkSprite.spriteName = "CZ_huaDongBG_2";
			}
			this.UpdateBtn(curinfo);
		}
	}

	// Token: 0x06004A0C RID: 18956 RVA: 0x001817A4 File Offset: 0x0017F9A4
	public void UpdateBtn(guild_member_info curinfo)
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.isGuildChief())
		{
			if (curinfo.characterId == PlayerData.MainPlayerServerId)
			{
				NGUITools.SetActive(this.SelectPic.gameObject, true);
				this.BtnLabel.text = StrDictionary.GetDictionaryString("#{105049}", new object[0]);
			}
			else if (curinfo.battle == 0L)
			{
				NGUITools.SetActive(this.SelectPic.gameObject, false);
				this.BtnLabel.text = StrDictionary.GetDictionaryString("#{105048}", new object[0]);
			}
			else
			{
				NGUITools.SetActive(this.SelectPic.gameObject, true);
				this.BtnLabel.text = StrDictionary.GetDictionaryString("#{105049}", new object[0]);
			}
		}
		else if (curinfo.battle == 0L)
		{
			NGUITools.SetActive(this.SelectPic.gameObject, false);
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{105048}", new object[0]);
		}
		else
		{
			NGUITools.SetActive(this.SelectPic.gameObject, true);
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{105049}", new object[0]);
		}
	}

	// Token: 0x06004A0D RID: 18957 RVA: 0x001818DC File Offset: 0x0017FADC
	public void OnClickBtn()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.isGuildChief())
		{
			NoticeLogic.AddNotifyData("#{105083}", true, false);
			return;
		}
		if (this.mCurInfo.characterId == PlayerData.MainPlayerServerId)
		{
			NoticeLogic.AddNotifyData("#{105084}", true, false);
			return;
		}
		if (SingletonUnity<GuildBattleMemberRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildBattleMemberRootLogic>.Instance.gameObject))
		{
			if (this.mCurInfo.battle == 0L)
			{
				if (SingletonUnity<GuildBattleMemberRootLogic>.Instance.OnClickSelectAdd(this.mCurInfo.characterId))
				{
					this.mCurInfo.battle = 1L;
					this.UpdateBtn(this.mCurInfo);
				}
			}
			else
			{
				SingletonUnity<GuildBattleMemberRootLogic>.Instance.OnClickSelectDec(this.mCurInfo.characterId);
				this.mCurInfo.battle = 0L;
				this.UpdateBtn(this.mCurInfo);
			}
		}
		if (this.mCurInfo.battle == 0L)
		{
			NGUITools.SetActive(this.SelectPic.gameObject, false);
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{105048}", new object[0]);
		}
		else
		{
			NGUITools.SetActive(this.SelectPic.gameObject, true);
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{105049}", new object[0]);
		}
	}

	// Token: 0x06004A0E RID: 18958 RVA: 0x00181A34 File Offset: 0x0017FC34
	public void OnClickIconBtn()
	{
		if (this.mCurInfo.characterId != PlayerData.MainPlayerServerId)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			TargetBasicInfo selectTargetBasicInfo = playerData.SelectTargetBasicInfo;
			selectTargetBasicInfo.ResetInfo(this.mCurInfo.characterId, (int)this.mCurInfo.level, (int)this.mCurInfo.combValue, this.mCurInfo.name, (PROFESSION_TYPE)this.mCurInfo.profession, (int)this.mCurInfo.state, this.mCurInfo.guildId, playerData.PlayerGuild.GuilName, UICamera.currentTouch.pos);
			HitOtherPLayerLogic.ShowMenu(HitType.HitGuildMember, selectTargetBasicInfo);
		}
	}

	// Token: 0x04003768 RID: 14184
	public UILabel NameLabel;

	// Token: 0x04003769 RID: 14185
	public UILabel PowerLabel;

	// Token: 0x0400376A RID: 14186
	public UISprite playericon;

	// Token: 0x0400376B RID: 14187
	public UILabel professLabel;

	// Token: 0x0400376C RID: 14188
	public UILabel BtnLabel;

	// Token: 0x0400376D RID: 14189
	public UISprite SelectPic;

	// Token: 0x0400376E RID: 14190
	public UISprite BkSprite;

	// Token: 0x0400376F RID: 14191
	private int mCurIndex = -1;

	// Token: 0x04003770 RID: 14192
	private guild_member_info mCurInfo;
}
