using System;
using SprotoType;
using UnityEngine;

// Token: 0x020009A6 RID: 2470
public class FriendAddItemLogic : MonoBehaviour
{
	// Token: 0x06004618 RID: 17944 RVA: 0x00162638 File Offset: 0x00160838
	public void UpdateFriendInfo(friend_info info)
	{
		this.FriendNamelabel.text = info.name;
		this.LevelLabel.text = string.Format("Lv.{0}", info.level);
		checked
		{
			this.IconSprite.spriteName = GameDefine.Player_Icon_Small_Pic[(int)((IntPtr)info.profession)];
			this.ProfesssionFlag.spriteName = GameDefine.Profession_PicName[(int)((IntPtr)info.profession)];
			if (info.HasGuildName)
			{
				this.GuildNameLabel.text = info.guildName;
			}
			else
			{
				this.GuildNameLabel.text = StrDictionary.GetDictionaryString("#{100240}", new object[0]);
			}
			this.FightLabel.text = string.Format("{0}: {1}", StrDictionary.GetDictionaryString("#{100421}", new object[0]), info.combValue.ToString());
			this.curFriendInfo = info;
		}
	}

	// Token: 0x06004619 RID: 17945 RVA: 0x0016271C File Offset: 0x0016091C
	public void OnClickAdd()
	{
		add_friend.request request = new add_friend.request();
		request.characterId = this.curFriendInfo.characterId;
		NetLogic.GetInstance().Send<Protocol.add_friend>(request, null);
		FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
		if (friendInfo.MainPlayerRandomFriendDic.ContainsKey(this.curFriendInfo.characterId))
		{
			friendInfo.MainPlayerRandomFriendDic.Remove(this.curFriendInfo.characterId);
		}
		if (friendInfo.MainPlayerSearchFriendDic.ContainsKey(this.curFriendInfo.characterId))
		{
			friendInfo.MainPlayerSearchFriendDic.Remove(this.curFriendInfo.characterId);
		}
		if (SingletonUnity<FriendAddUILogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FriendAddUILogic>.Instance.gameObject))
		{
			SingletonUnity<FriendAddUILogic>.Instance.UpdateFriendList();
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Social", "Friend", "apply_times");
	}

	// Token: 0x04003324 RID: 13092
	public UISprite BkSprite;

	// Token: 0x04003325 RID: 13093
	public UISprite IconSprite;

	// Token: 0x04003326 RID: 13094
	public UILabel LevelLabel;

	// Token: 0x04003327 RID: 13095
	public UILabel FightLabel;

	// Token: 0x04003328 RID: 13096
	public UILabel GuildNameLabel;

	// Token: 0x04003329 RID: 13097
	public UILabel FriendNamelabel;

	// Token: 0x0400332A RID: 13098
	public UISprite ProfesssionFlag;

	// Token: 0x0400332B RID: 13099
	private friend_info curFriendInfo;
}
