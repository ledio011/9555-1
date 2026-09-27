using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x020009A3 RID: 2467
public class EnemyItemLogic : MonoBehaviour
{
	// Token: 0x06004606 RID: 17926 RVA: 0x00161D1C File Offset: 0x0015FF1C
	public void UpdateFriendInfo(friend_info info)
	{
		this.curFriendInfo = info;
		this.LevelLabel.text = string.Format("Lv.{0}", info.level);
		this.IconSprite.spriteName = GameDefine.Player_Icon_Small_Pic[(int)(checked((IntPtr)info.profession))];
		this.FriendNamelabel.text = info.name;
		this.FightLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100421}", new object[0]), info.combValue.ToString());
		if (info.HasGuildName)
		{
			this.GuildNameLabel.text = info.guildName;
		}
		else
		{
			this.GuildNameLabel.text = StrDictionary.GetDictionaryString("#{100240}", new object[0]);
		}
		if (info.state == 0L)
		{
			this.BkSprite.spriteName = "CZ_huaDongBG_2";
			this.IconSprite.alpha = 0.35f;
			this.RevengeBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		else
		{
			this.BkSprite.spriteName = "CZ_huaDongBG";
			this.IconSprite.alpha = 1f;
			this.RevengeBtnSp.spriteName = GameDefine.BtnIcon[1];
		}
		this.FriendScoreLabel.text = info.friendScore.ToString();
	}

	// Token: 0x06004607 RID: 17927 RVA: 0x00161E78 File Offset: 0x00160078
	public void OnClickRevengeBtn()
	{
		if (this.curFriendInfo.state == 0L)
		{
			NoticeLogic.AddNotifyData("#{103317}", true, false);
			return;
		}
		if (this.curFriendInfo != null)
		{
			update_player_map_info.request request = new update_player_map_info.request();
			request.characterId = this.curFriendInfo.friendId;
			NetLogic.GetInstance().Send<Protocol.update_player_map_info>(request, new RpcRspHandler(this.Ret_Ask_Character_info));
		}
	}

	// Token: 0x06004608 RID: 17928 RVA: 0x00161EDC File Offset: 0x001600DC
	private void Ret_Ask_Character_info(SprotoTypeBase req)
	{
		if (UIManager.IsUnlockTutorialEnable())
		{
			return;
		}
		update_player_map_info.response response = req as update_player_map_info.response;
		if (response != null && response.HasState)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EnemyRevengeRoot, delegate
			{
				SingletonUnity<EnemyRevengeRootLogic>.Instance.Reset(this.curFriendInfo, response);
			}, null);
		}
	}

	// Token: 0x06004609 RID: 17929 RVA: 0x00161F44 File Offset: 0x00160144
	public void OnClickDeleteBtn()
	{
		if (this.curFriendInfo != null)
		{
			MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{103319}", new object[]
			{
				this.curFriendInfo.name
			}), StrDictionary.GetDictionaryString("#{100244}", new object[0]), delegate
			{
				del_friend.request request = new del_friend.request();
				request.characterId = this.curFriendInfo.friendId;
				request.type = 1L;
				NetLogic.GetInstance().Send<Protocol.del_friend>(request, null);
				FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
				friendInfo.RemoveEnemy(this.curFriendInfo.friendId);
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Social", "Enemy", "del_times");
			}, null, null, null);
		}
	}

	// Token: 0x0600460A RID: 17930 RVA: 0x00161FA0 File Offset: 0x001601A0
	public void OnClickItem()
	{
		TargetBasicInfo selectTargetBasicInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SelectTargetBasicInfo;
		selectTargetBasicInfo.ResetInfo(this.curFriendInfo.friendId, (int)this.curFriendInfo.level, (int)this.curFriendInfo.combValue, this.curFriendInfo.name, (PROFESSION_TYPE)this.curFriendInfo.profession, (int)this.curFriendInfo.state, this.curFriendInfo.guildId, this.curFriendInfo.guildName, UICamera.currentTouch.pos);
		HitOtherPLayerLogic.ShowMenu(HitType.HitFriendIcon, selectTargetBasicInfo);
	}

	// Token: 0x04003312 RID: 13074
	public UISprite BkSprite;

	// Token: 0x04003313 RID: 13075
	public UISprite IconSprite;

	// Token: 0x04003314 RID: 13076
	public UILabel LevelLabel;

	// Token: 0x04003315 RID: 13077
	public UILabel FightLabel;

	// Token: 0x04003316 RID: 13078
	public UILabel GuildNameLabel;

	// Token: 0x04003317 RID: 13079
	public UILabel FriendScoreLabel;

	// Token: 0x04003318 RID: 13080
	public UILabel FriendNamelabel;

	// Token: 0x04003319 RID: 13081
	private friend_info curFriendInfo;

	// Token: 0x0400331A RID: 13082
	public UISprite RevengeBtnSp;
}
