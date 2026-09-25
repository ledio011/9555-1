using System;
using SprotoType;
using UnityEngine;

// Token: 0x020008FE RID: 2302
public class DanceBtnRootLogic : SingletonUnity<DanceBtnRootLogic>
{
	// Token: 0x06003EDC RID: 16092 RVA: 0x00121D80 File Offset: 0x0011FF80
	public void Reset()
	{
		this.mPlayerdata = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.mCurSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (this.mMainPlayer == null)
		{
			return;
		}
		if (this.mMainPlayer.CurPlayerState == PLAYER_STATE.DANCE)
		{
			this.DanceBtnSprite.spriteName = "CZ_zhuJieMianAnNiu_Dance_2";
			this.DanceEffect.alpha = 1f;
		}
		else
		{
			this.DanceBtnSprite.spriteName = "CZ_zhuJieMianAnNiu_Dance_1";
			this.DanceEffect.alpha = 0f;
		}
		if (this.CheckDanceLevel())
		{
			this.DanceBtnScale.ResetToBeginning();
			this.DanceBtnScale.PlayForward();
			this.DanceBtnScale.enabled = true;
		}
		this.SingleGameItemNum = this.mPlayerdata.ItemBackPack.GetItemStackNumById(GameDefine.SingleDanceToolItem);
		this.GangGameItemNum = this.mPlayerdata.ItemBackPack.GetItemStackNumById(GameDefine.GangDanceToolItem);
		this.SingleToolItem.Init(GameDefine.SingleDanceToolItem, this.SingleGameItemNum);
		this.GangToolItem.Init(GameDefine.GangDanceToolItem, this.GangGameItemNum);
		if (!this.mPlayerdata.IsHaveGuild())
		{
			NGUITools.SetActive(this.GangToolItem.gameObject, false);
		}
		this.DynamicBtnGride.Reposition();
		this.UpdateCDTime();
	}

	// Token: 0x06003EDD RID: 16093 RVA: 0x00121EE8 File Offset: 0x001200E8
	public void UpdateCDTime()
	{
		dance_state_info danceInfoByType = this.mPlayerdata.ActivityData.GetDanceInfoByType(GameDefine.DANCE_TYPE.SINGLE_TOOL);
		if (danceInfoByType != null && danceInfoByType.HasEnd_time)
		{
			CityDanceData cityDanceDataById = DataManager.GetCityDanceDataById(danceInfoByType.ID);
			this.SingleToolItem.SetCDTime(danceInfoByType.end_time, (long)cityDanceDataById.DurationTime);
		}
		else
		{
			this.SingleToolItem.SetCDTime(0L, 1L);
		}
		dance_state_info danceInfoByType2 = this.mPlayerdata.ActivityData.GetDanceInfoByType(GameDefine.DANCE_TYPE.GANG_TOOL);
		if (danceInfoByType2 != null && danceInfoByType2.HasParm2 && this.mMainPlayer.ServerId == danceInfoByType2.parm2 && danceInfoByType2.HasEnd_time)
		{
			CityDanceData cityDanceDataById2 = DataManager.GetCityDanceDataById(danceInfoByType2.ID);
			this.GangToolItem.SetCDTime(danceInfoByType2.end_time, (long)cityDanceDataById2.DurationTime);
		}
		else
		{
			this.GangToolItem.SetCDTime(0L, 1L);
		}
	}

	// Token: 0x06003EDE RID: 16094 RVA: 0x00121FCC File Offset: 0x001201CC
	public void OnClickDanceBtn()
	{
		if (Time.time - this.mLastSendServerTimeCheck < this.mSendSerTimeInterval)
		{
			return;
		}
		if (!this.CheckDanceLevel())
		{
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
			return;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.IsCanDance())
		{
			if (this.SingleGameItemNum > 0)
			{
				MessageBoxLogic.OpenOKCancelBox("#{104002}", "#{100127}", new MessageBoxLogic.OnYesClick(this.OnClickSingleToolsBtn), null, null, null);
			}
			else if (this.GangGameItemNum > 0 && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
			{
				MessageBoxLogic.OpenOKCancelBox("#{104002}", "#{100127}", new MessageBoxLogic.OnYesClick(this.OnClickGangToolsBtn), null, null, null);
			}
			else
			{
				MessageBoxLogic.OpenOKCancelBox("#{104001}", "#{100127}", delegate
				{
					GameMoneyHelper.ShowItemProduct(GameDefine.SingleDanceToolItem, GameDefine.SHOP_TYPE.TOOL_SHOP);
				}, null, null, null);
			}
			return;
		}
		if (this.mMainPlayer.CurPlayerState != PLAYER_STATE.DANCE)
		{
			PlayerDanceData playerDanceData = this.mPlayerdata.PlayerDanceData;
			if (playerDanceData.IsHaveDanceData())
			{
				if (SingletonUnity<UIManager>.Instance.CloseAllPOPUI())
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DanceChooseRoot, null, null);
				}
			}
			else
			{
				request_dance_info.request request = new request_dance_info.request();
				request.type = 0L;
				NetLogic.GetInstance().Send<Protocol.request_dance_info>(request, null);
				WaitResponseUIRootLogic.OpenWaitBox(227, 10f, 0f, null);
				this.mLastSendServerTimeCheck = Time.time;
				if (SingletonUnity<FunctionBtnRootLogic>.Exists)
				{
					SingletonUnity<FunctionBtnRootLogic>.Instance.LastSendServerTimeCheck = Time.time;
				}
			}
		}
		else
		{
			this.mMainPlayer.StopDance();
		}
		this.UpdateDanceBtn();
	}

	// Token: 0x06003EDF RID: 16095 RVA: 0x00122188 File Offset: 0x00120388
	public void OnClickSingleToolsBtn()
	{
		if (this.SingleToolItem.ReamainTime > 0L)
		{
			return;
		}
		if (this.SingleGameItemNum > 0)
		{
			this.mMainPlayer.UseDanceItem(GameDefine.SingleDanceToolItem);
		}
		else
		{
			GameMoneyHelper.ShowItemProduct(GameDefine.SingleDanceToolItem, GameDefine.SHOP_TYPE.TOOL_SHOP);
		}
	}

	// Token: 0x06003EE0 RID: 16096 RVA: 0x001221D4 File Offset: 0x001203D4
	public void OnClickGangToolsBtn()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			NoticeLogic.AddNotifyData("#{102006}", true, false);
			return;
		}
		if (this.GangToolItem.ReamainTime > 0L)
		{
			return;
		}
		if (this.GangGameItemNum > 0)
		{
			this.mMainPlayer.UseDanceItem(GameDefine.GangDanceToolItem);
		}
		else
		{
			NoticeLogic.AddNotifyData("#{105102}", true, false);
		}
	}

	// Token: 0x06003EE1 RID: 16097 RVA: 0x00122244 File Offset: 0x00120444
	public void UpdateDanceBtn()
	{
		if (!UnityVersionUtil.IsActive(this.DanceBtnScale.gameObject))
		{
			return;
		}
		if (this.mMainPlayer == null)
		{
			return;
		}
		this.DanceBtnScale.ResetToBeginning();
		if (this.mMainPlayer.CurPlayerState == PLAYER_STATE.DANCE)
		{
			this.DanceBtnSprite.spriteName = "CZ_zhuJieMianAnNiu_Dance_2";
			this.DanceBtnScale.PlayForward();
			this.DanceBtnScale.enabled = true;
			this.DanceEffect.alpha = 1f;
		}
		else
		{
			this.DanceBtnSprite.spriteName = "CZ_zhuJieMianAnNiu_Dance_1";
			this.DanceEffect.alpha = 0f;
		}
	}

	// Token: 0x06003EE2 RID: 16098 RVA: 0x001222F4 File Offset: 0x001204F4
	public bool CheckDanceLevel()
	{
		copyscene_info copyinfoByType = this.mPlayerdata.CopyInfoData.GetCopyinfoByType(26);
		string text = null;
		if (copyinfoByType != null)
		{
			text = copyinfoByType.ID;
		}
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		CityDanceData cityDanceDataById = DataManager.GetCityDanceDataById(text);
		return this.mPlayerdata.CheckLevel(cityDanceDataById.UnlockLevel);
	}

	// Token: 0x06003EE3 RID: 16099 RVA: 0x00122350 File Offset: 0x00120550
	private void OnEnable()
	{
		UIUpdateEvent.SyncBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.SyncBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateDanceTools));
		UIUpdateEvent.UpdateBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.UpdateBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateDanceTools));
	}

	// Token: 0x06003EE4 RID: 16100 RVA: 0x001223A0 File Offset: 0x001205A0
	private void OnDisable()
	{
		UIUpdateEvent.SyncBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.SyncBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateDanceTools));
		UIUpdateEvent.UpdateBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.UpdateBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateDanceTools));
	}

	// Token: 0x06003EE5 RID: 16101 RVA: 0x001223F0 File Offset: 0x001205F0
	public void UpdateDanceTools()
	{
		this.Reset();
	}

	// Token: 0x04002A9F RID: 10911
	public UISprite DanceBtnSprite;

	// Token: 0x04002AA0 RID: 10912
	public UITweener DanceBtnScale;

	// Token: 0x04002AA1 RID: 10913
	public UISprite DanceEffect;

	// Token: 0x04002AA2 RID: 10914
	public GameObject SingleToolsObj;

	// Token: 0x04002AA3 RID: 10915
	public GameObject GangToolsObj;

	// Token: 0x04002AA4 RID: 10916
	public UIGrid DynamicBtnGride;

	// Token: 0x04002AA5 RID: 10917
	private float mLastSendServerTimeCheck;

	// Token: 0x04002AA6 RID: 10918
	private float mSendSerTimeInterval = 5f;

	// Token: 0x04002AA7 RID: 10919
	private PlayerData mPlayerdata;

	// Token: 0x04002AA8 RID: 10920
	private ObjMainPlayer mMainPlayer;

	// Token: 0x04002AA9 RID: 10921
	private SceneManager mCurSceneManager;

	// Token: 0x04002AAA RID: 10922
	public DanceToolItem SingleToolItem;

	// Token: 0x04002AAB RID: 10923
	public DanceToolItem GangToolItem;

	// Token: 0x04002AAC RID: 10924
	private int SingleGameItemNum;

	// Token: 0x04002AAD RID: 10925
	private int GangGameItemNum;
}
