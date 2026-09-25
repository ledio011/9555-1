using System;
using SprotoType;
using UnityEngine;

// Token: 0x020009D4 RID: 2516
public class WorldItemLogic : MonoBehaviour
{
	// Token: 0x06004785 RID: 18309 RVA: 0x0016CB8C File Offset: 0x0016AD8C
	public void ResetItem(MapInfoData data, bool isactflag = false)
	{
		this.IsActivityMap = isactflag;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		this.curMapInfoData = data;
		this.isLock = (playerData.MainPlayerAttrData.Level < this.curMapInfoData.OpenLv);
		this.isCurrScene = (sceneManager.CurrentMapInofData.ID == data.ID);
		this.TargetPos = this.curMapInfoData.BirthPosVector3;
		this.CurCaptureInfo = null;
		this.CurCaptureInfo = playerData.ActivityData.GetGuildMapInfo(data.ID);
		this.TargetPos = playerData.ActivityData.GetCityCaptureNpcPos(data);
		this.UpdateItem();
	}

	// Token: 0x06004786 RID: 18310 RVA: 0x0016CC40 File Offset: 0x0016AE40
	private void UpdateItem()
	{
		if (this.curMapInfoData.OpenLv > 0)
		{
			this.mapNameLabel.text = string.Format("{0}:Lv.{1}", StrDictionary.GetDictionaryString(this.curMapInfoData.Name, new object[0]), this.curMapInfoData.OpenLv);
		}
		else
		{
			this.mapNameLabel.text = string.Format("{0}", StrDictionary.GetDictionaryString(this.curMapInfoData.Name, new object[0]));
		}
		this.mapIconSprite.spriteName = this.curMapInfoData.MapIcon;
		if (this.isLock || (this.IsActivityMap && (this.CurCaptureInfo == null || this.CurCaptureInfo.state != 1L)))
		{
			this.mapIconSprite.color = GameDefine.GrayColor;
			this.OpenFlag.enabled = false;
			this.GangFlag.enabled = false;
		}
		else
		{
			this.mapIconSprite.color = Color.white;
			if (this.CurCaptureInfo != null && (this.CurCaptureInfo.state == 1L || this.CurCaptureInfo.HasGuildId))
			{
				if (this.CurCaptureInfo.state == 1L)
				{
					this.OpenFlag.enabled = true;
					this.GangFlag.enabled = false;
				}
				else if (this.CurCaptureInfo.HasGuildId && this.CurCaptureInfo.HasGuildIcon)
				{
					this.OpenFlag.enabled = false;
					this.GangFlag.enabled = true;
				}
				else
				{
					this.OpenFlag.enabled = false;
					this.GangFlag.enabled = false;
				}
			}
			else
			{
				this.OpenFlag.enabled = false;
				this.GangFlag.enabled = false;
			}
		}
		base.transform.localPosition = this.curMapInfoData.WoldPosVector3;
		NGUITools.SetActive(this.curSceneFlag, this.isCurrScene);
	}

	// Token: 0x06004787 RID: 18311 RVA: 0x0016CE44 File Offset: 0x0016B044
	public void OnClick()
	{
		if (this.isCurrScene && !this.IsActivityMap)
		{
			return;
		}
		if (this.IsActivityMap && (this.CurCaptureInfo == null || this.CurCaptureInfo.state != 1L))
		{
			return;
		}
		if (this.isLock)
		{
			NoticeLogic.AddNotifyData2Client(false, "#{100154}", false, new object[]
			{
				this.curMapInfoData.OpenLv
			});
			return;
		}
		AutoSearchPathManager autoSearchPath = SingletonDontDestoryUnity<GameManager>.Instance.AutoSearchPath;
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			mainPlayer.BreakAutoCombatState();
			mainPlayer.SkillLogic.BreakCurSkill();
		}
		if (this.IsActivityMap)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.StopAutoMoveToMission();
			if (this.CurCaptureInfo != null)
			{
				enter_guild_city_scene.request request = new enter_guild_city_scene.request();
				request.id = this.CurCaptureInfo.id;
				NetLogic.GetInstance().Send<Protocol.enter_guild_city_scene>(request, null);
			}
			else
			{
				SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AutoMoveDest(this.curMapInfoData.ID, this.TargetPos, AUTO_SEARCH_PARTH_FINISHEVENT.CHANGE_MAP, null);
			}
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.WorldMapRoot);
			SingletonUnity<UIManager>.Instance.CloseAllPOPUI();
			return;
		}
		if (autoSearchPath.IsInTelePortCircle)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange)
			{
				return;
			}
			if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
			{
				SingletonUnity<UIManager>.Instance.CloseAllPOPUI();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DownLoadResRoot, null, null);
				return;
			}
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
			enter_new_map.request request2 = new enter_new_map.request();
			request2.mapInfoId = this.curMapInfoData.ID;
			NetLogic.GetInstance().Send<Protocol.enter_new_map>(request2, null);
			WaitResponseUIRootLogic.OpenWaitBox(106, 10f, 0f, null);
		}
		else
		{
			SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AutoMoveDest(this.curMapInfoData.ID, this.curMapInfoData.BirthPosVector3, AUTO_SEARCH_PARTH_FINISHEVENT.CHANGE_MAP, null);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.WorldMapRoot);
		}
	}

	// Token: 0x040034C0 RID: 13504
	public UILabel mapNameLabel;

	// Token: 0x040034C1 RID: 13505
	public UISprite mapIconSprite;

	// Token: 0x040034C2 RID: 13506
	public GameObject curSceneFlag;

	// Token: 0x040034C3 RID: 13507
	public UISprite OpenFlag;

	// Token: 0x040034C4 RID: 13508
	public UISprite GangFlag;

	// Token: 0x040034C5 RID: 13509
	private bool IsActivityMap;

	// Token: 0x040034C6 RID: 13510
	private Vector3 TargetPos;

	// Token: 0x040034C7 RID: 13511
	private guild_map_info CurCaptureInfo;

	// Token: 0x040034C8 RID: 13512
	private MapInfoData curMapInfoData;

	// Token: 0x040034C9 RID: 13513
	private bool isLock;

	// Token: 0x040034CA RID: 13514
	private bool isCurrScene;
}
