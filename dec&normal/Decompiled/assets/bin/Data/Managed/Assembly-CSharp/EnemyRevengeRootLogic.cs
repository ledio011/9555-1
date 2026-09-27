using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009A4 RID: 2468
public class EnemyRevengeRootLogic : SingletonUnity<EnemyRevengeRootLogic>
{
	// Token: 0x0600460D RID: 17933 RVA: 0x001620AC File Offset: 0x001602AC
	public void Reset(friend_info enemy, update_player_map_info.response response)
	{
		this.curEnemyinfo = enemy;
		this.CurRes = response;
	}

	// Token: 0x0600460E RID: 17934 RVA: 0x001620BC File Offset: 0x001602BC
	public void OnClickTraceBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.EnemyRevengeRoot);
		if (this.CurRes.state != 1L)
		{
			NoticeLogic.AddNotifyData("#{103318}", true, false);
			return;
		}
		if (this.CurRes.HasMapid && this.CurRes.HasPos)
		{
			MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			float num = (float)this.CurRes.pos.x / 100f;
			float num2 = (float)this.CurRes.pos.z / 100f;
			Vector3 pos;
			pos..ctor(num, SceneManager.GetHitHeight(new Vector3(num, 0f, num2)), num2);
			MapInfoData mapInfoDataByID = DataManager.GetMapInfoDataByID(this.CurRes.mapid);
			if (mapInfoDataByID != null && mapInfoDataByID.MapType == MAPTYPE.BIG_WORLD)
			{
				missionManager.AutoMoveDest(this.CurRes.mapid, pos, AUTO_SEARCH_PARTH_FINISHEVENT.INVALID, null);
				SingletonUnity<SocialUIRootLogic>.Instance.OnClickCloseBtn();
			}
			else
			{
				NoticeLogic.AddNotifyData("#{103318}", true, false);
			}
		}
	}

	// Token: 0x0600460F RID: 17935 RVA: 0x001621C4 File Offset: 0x001603C4
	public void OnClickWarpBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.EnemyRevengeRoot);
		if (this.CurRes.state != 1L)
		{
			NoticeLogic.AddNotifyData("#{103318}", true, false);
			return;
		}
		if (this.CurRes.HasMapid && this.CurRes.HasPos)
		{
			MapInfoData mapInfoDataByID = DataManager.GetMapInfoDataByID(this.CurRes.mapid);
			if (mapInfoDataByID == null || mapInfoDataByID.MapType != MAPTYPE.BIG_WORLD)
			{
				NoticeLogic.AddNotifyData("#{103318}", true, false);
				return;
			}
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		int itemStackNumById = playerData.ItemBackPack.GetItemStackNumById(GameDefine.EnemyWarpToolItem);
		if (itemStackNumById > 0)
		{
			ItemContainer itemBackPack = playerData.ItemBackPack;
			List<GameItem> targetItemByID = ItemContainerTool.GetTargetItemByID(itemBackPack, GameDefine.EnemyWarpToolItem);
			bool flag;
			if (targetItemByID == null || targetItemByID.Count == 0)
			{
				flag = false;
			}
			else
			{
				flag = true;
				GameItem gameItem = targetItemByID[0];
			}
			if (flag)
			{
				gather_other_player.request request = new gather_other_player.request();
				request.characterid = this.curEnemyinfo.friendId;
				NetLogic.GetInstance().Send<Protocol.gather_other_player>(request, null);
			}
		}
		else
		{
			GameMoneyHelper.ShowItemProduct(GameDefine.EnemyWarpToolItem, GameDefine.SHOP_TYPE.TOOL_SHOP);
		}
	}

	// Token: 0x06004610 RID: 17936 RVA: 0x001622F8 File Offset: 0x001604F8
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.EnemyRevengeRoot);
	}

	// Token: 0x0400331B RID: 13083
	private friend_info curEnemyinfo;

	// Token: 0x0400331C RID: 13084
	private update_player_map_info.response CurRes;
}
