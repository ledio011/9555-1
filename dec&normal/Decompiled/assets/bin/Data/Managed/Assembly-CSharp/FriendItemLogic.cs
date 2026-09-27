using System;
using SprotoType;
using UnityEngine;

// Token: 0x020009AA RID: 2474
public class FriendItemLogic : MonoBehaviour
{
	// Token: 0x06004632 RID: 17970 RVA: 0x00162FB8 File Offset: 0x001611B8
	public void UpdateFriendInfo(friend_info info)
	{
		this.FriendNamelabel.text = info.name;
		this.LevelLabel.text = string.Format("Lv.{0}", info.level);
		this.IconSprite.spriteName = GameDefine.Player_Icon_Small_Pic[(int)(checked((IntPtr)info.profession))];
		if (info.state == 0L)
		{
			this.BkSprite.spriteName = "CZ_huaDongBG_2";
			this.IconSprite.alpha = 0.35f;
		}
		else
		{
			this.BkSprite.spriteName = "CZ_huaDongBG";
			this.IconSprite.alpha = 1f;
		}
		this.FriendScoreLabel.text = info.friendScore.ToString();
		if (info.HasGuildName)
		{
			this.GuildNameLabel.text = info.guildName;
		}
		else
		{
			this.GuildNameLabel.text = StrDictionary.GetDictionaryString("#{100240}", new object[0]);
		}
		this.FightLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100421}", new object[0]), info.combValue.ToString());
		this.curFriendInfo = info;
	}

	// Token: 0x06004633 RID: 17971 RVA: 0x001630F0 File Offset: 0x001612F0
	public void OnClickItem()
	{
		TargetBasicInfo selectTargetBasicInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SelectTargetBasicInfo;
		selectTargetBasicInfo.ResetInfo(this.curFriendInfo.friendId, (int)this.curFriendInfo.level, (int)this.curFriendInfo.combValue, this.curFriendInfo.name, (PROFESSION_TYPE)this.curFriendInfo.profession, (int)this.curFriendInfo.state, this.curFriendInfo.guildId, this.curFriendInfo.guildName, UICamera.currentTouch.pos);
		HitOtherPLayerLogic.ShowMenu(HitType.HitFriendIcon, selectTargetBasicInfo);
	}

	// Token: 0x06004634 RID: 17972 RVA: 0x00163180 File Offset: 0x00161380
	public void OnClickTalk()
	{
		TargetBasicInfo selectTargetBasicInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SelectTargetBasicInfo;
		selectTargetBasicInfo.ResetInfo(this.curFriendInfo.friendId, (int)this.curFriendInfo.level, (int)this.curFriendInfo.combValue, this.curFriendInfo.name, (PROFESSION_TYPE)this.curFriendInfo.profession, (int)this.curFriendInfo.state, this.curFriendInfo.guildId, this.curFriendInfo.guildName, UICamera.currentTouch.pos);
		SingletonUnity<SocialUIRootLogic>.Instance.OnClickCloseBtn();
		ChatUIRootLogic.ResetPrivateChat(selectTargetBasicInfo.ServerId, selectTargetBasicInfo.Name, selectTargetBasicInfo.profession);
	}

	// Token: 0x04003337 RID: 13111
	public UISprite BkSprite;

	// Token: 0x04003338 RID: 13112
	public UISprite IconSprite;

	// Token: 0x04003339 RID: 13113
	public UILabel LevelLabel;

	// Token: 0x0400333A RID: 13114
	public UILabel FightLabel;

	// Token: 0x0400333B RID: 13115
	public UILabel GuildNameLabel;

	// Token: 0x0400333C RID: 13116
	public UILabel FriendScoreLabel;

	// Token: 0x0400333D RID: 13117
	public UILabel FriendNamelabel;

	// Token: 0x0400333E RID: 13118
	private friend_info curFriendInfo;
}
