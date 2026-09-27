using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x020009F2 RID: 2546
public class LoginRootLogic : SingletonUnity<LoginRootLogic>
{
	// Token: 0x060048BD RID: 18621 RVA: 0x00176914 File Offset: 0x00174B14
	private new void Awake()
	{
		base.Awake();
	}

	// Token: 0x060048BE RID: 18622 RVA: 0x0017691C File Offset: 0x00174B1C
	public void Reset(string noticeStr, string noticeVersion)
	{
		this.lastUpdateServerTime = Time.time;
		if (PlayerData.CurGameServerData == null)
		{
			SingletonUnity<MenuSceneController>.Instance.ChooseRecommendServer();
		}
		if (PlayerData.CurGameServerData != null)
		{
			this.ServerLabel.text = PlayerData.CurGameServerData.serverName;
			if (PlayerData.CurGameServerData.HasNewServer && PlayerData.CurGameServerData.newServer == 1L && PlayerData.CurGameServerData.serverState != 3L)
			{
				this.ServerStatePic.color = Color.red;
			}
			else
			{
				this.ServerStatePic.color = GameDefine.SERVER_STATE_COLOR[(int)PlayerData.CurGameServerData.serverState];
			}
			this.ServerLabel.color = Color.white;
			this.ServerStatePic.enabled = true;
		}
		else
		{
			this.ServerLabel.text = StrDictionary.GetDictionaryString("#{200074}", new object[0]);
			this.ServerLabel.color = Color.grey;
			this.ServerStatePic.enabled = false;
		}
		this.VersionLabel.text = string.Format("Version:{0}", GameSettingData.GameVersion);
		bool flag = true;
		if (!string.IsNullOrEmpty(noticeStr))
		{
			this.mNoticeStr = noticeStr;
			if (!string.IsNullOrEmpty(noticeVersion))
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.LoginNoticeRootUI, delegate
				{
					SingletonUnity<LoginNoticeRootLogic>.Instance.Reset(this.mNoticeStr);
				}, null);
				flag = false;
			}
		}
		if (flag)
		{
			AccountVersionCheckRootLogic.CheckFaceBookBindTips(1);
		}
	}

	// Token: 0x060048BF RID: 18623 RVA: 0x00176A88 File Offset: 0x00174C88
	private void UpdateGameServer()
	{
		SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(PlayerData.CurLoginServerData.LoginIP, PlayerData.CurLoginServerData.LoginPort, delegate(bool success)
		{
			NetLogic.GetInstance().Send<Protocol.update_game_server>(null, new RpcRspHandler(this.UpdateGameServerResponse));
		}, null);
	}

	// Token: 0x060048C0 RID: 18624 RVA: 0x00176AB8 File Offset: 0x00174CB8
	public void UpdateGameServerResponse(SprotoTypeBase req)
	{
		update_game_server.response response = req as update_game_server.response;
		if (response != null && response.HasGame_server)
		{
			SingletonUnity<MenuSceneController>.Instance.UpdateServerList(response.game_server);
			if (PlayerData.CurGameServerData != null)
			{
				this.ServerLabel.text = PlayerData.CurGameServerData.serverName;
				if (PlayerData.CurGameServerData.HasNewServer && PlayerData.CurGameServerData.newServer == 1L && PlayerData.CurGameServerData.serverState != 3L)
				{
					this.ServerStatePic.color = Color.red;
				}
				else
				{
					this.ServerStatePic.color = GameDefine.SERVER_STATE_COLOR[(int)PlayerData.CurGameServerData.serverState];
				}
				this.ServerLabel.color = Color.white;
				this.ServerStatePic.enabled = true;
			}
			if (SingletonUnity<ChooseServerRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ChooseServerRootLogic>.Instance.gameObject))
			{
				SingletonUnity<ChooseServerRootLogic>.Instance.RefershServerList();
			}
		}
	}

	// Token: 0x060048C1 RID: 18625 RVA: 0x00176BB8 File Offset: 0x00174DB8
	public void OnClickLoginBtn()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.CanSendToServer(4, 0.5f))
		{
			NoticeLogic.AddNotifyData("#{200066}", true, false);
			return;
		}
		if (PlayerData.CurGameServerData == null)
		{
			this.OnClickChooseServerBtn();
			return;
		}
		if (PlayerData.CurGameServerData.HasServerState && PlayerData.CurGameServerData.serverState == 3L)
		{
			MessageBoxLogic.OpenOKBox("#{200128}", "#{100127}", null);
			if (Time.time - this.lastUpdateServerTime > this.UpdateDeltaTime)
			{
				this.lastUpdateServerTime = Time.time;
				this.UpdateGameServer();
			}
			return;
		}
		WaitResponseUIRootLogic.OpenWaitBox(4, 0f, 0f, null);
		if (GameSettingData.IsLocalTestServer)
		{
			SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(PlayerData.CurGameServerData.serverIP, (int)PlayerData.CurGameServerData.serverPort, null, null);
		}
		else
		{
			SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(PlayerData.CurGameServerData.serverIP, (int)PlayerData.CurGameServerData.serverPort, null, null);
		}
	}

	// Token: 0x060048C2 RID: 18626 RVA: 0x00176CB4 File Offset: 0x00174EB4
	private void ConnectLost()
	{
	}

	// Token: 0x060048C3 RID: 18627 RVA: 0x00176CB8 File Offset: 0x00174EB8
	private void ConnectSuccess(bool isSuccess)
	{
		if (isSuccess)
		{
			string playerAccountId = PlayerData.GetPlayerAccountId();
			if (string.IsNullOrEmpty(playerAccountId))
			{
				this.VisitorRequest();
			}
			else
			{
				this.VerifyRequest();
			}
		}
		else
		{
			WaitResponseUIRootLogic.CloseBox();
			NoticeLogic.AddNotifyData("#{200065}", true, false);
		}
	}

	// Token: 0x060048C4 RID: 18628 RVA: 0x00176D04 File Offset: 0x00174F04
	private void VisitorRequest()
	{
		visitor.request rpcReq = new visitor.request();
		NetLogic.GetInstance().Send<Protocol.visitor>(rpcReq, new RpcRspHandler(this.VisitorResponse));
	}

	// Token: 0x060048C5 RID: 18629 RVA: 0x00176D30 File Offset: 0x00174F30
	private void VisitorResponse(SprotoTypeBase rep)
	{
		visitor.response response = rep as visitor.response;
		if (response != null && response.state == 0L)
		{
			PlayerData.SavePlayerAccountId(response.id);
			PlayerData.SavePlayerAccountKey(response.key);
			this.VerifyRequest();
		}
		else
		{
			Debug.LogError("create  erro!");
			NetLogic.GetInstance().DisconnectServer();
		}
	}

	// Token: 0x060048C6 RID: 18630 RVA: 0x00176D8C File Offset: 0x00174F8C
	private void VerifyRequest()
	{
		verfiy.request request = new verfiy.request();
		request.id = PlayerData.GetPlayerAccountId();
		request.key = PlayerData.GetPlayerAccountKey();
		NetLogic.GetInstance().Send<Protocol.verfiy>(request, new RpcRspHandler(this.VerifyResponse));
	}

	// Token: 0x060048C7 RID: 18631 RVA: 0x00176DCC File Offset: 0x00174FCC
	private void VerifyResponse(SprotoTypeBase req)
	{
		verfiy.response response = req as verfiy.response;
		if (response != null)
		{
			if (response.state == 0L)
			{
				PlayerData.session = response.session;
				PlayerData.loginType = 1;
				SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(PlayerData.CurGameServerData.serverIP, (int)PlayerData.CurGameServerData.serverPort, null, null);
			}
			else if (response.state == 2L)
			{
				NoticeLogic.AddNotifyData("#{100147}", true, false);
				PlayerData.session = response.session;
				PlayerData.loginType = 2;
				SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(PlayerData.CurGameServerData.serverIP, (int)PlayerData.CurGameServerData.serverPort, null, null);
			}
			else
			{
				this.VisitorRequest();
			}
		}
	}

	// Token: 0x060048C8 RID: 18632 RVA: 0x00176E80 File Offset: 0x00175080
	public void OnClickChooseServerBtn()
	{
		if (PlayerData.loginType == 2 && PlayerData.CurGameServerData != null)
		{
			MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{200073}", new object[]
			{
				PlayerData.CurGameServerData.serverName
			}), "#{100127}", new MessageBoxLogic.OnYesClick(this.OnClickLoginBtn), null, null, null);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ChooseServerRootUI, delegate
			{
				SingletonUnity<ChooseServerRootLogic>.Instance.Reset();
				if (Time.time - this.lastUpdateServerTime > this.UpdateDeltaTime)
				{
					this.lastUpdateServerTime = Time.time;
					this.UpdateGameServer();
				}
			}, null);
		}
	}

	// Token: 0x060048C9 RID: 18633 RVA: 0x00176EFC File Offset: 0x001750FC
	public void OnClickreLogin()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.AccountVersionCheckRootUI, delegate(bool bSuccess, object param)
		{
			SingletonUnity<AccountVersionCheckRootLogic>.Instance.ReLoginVerfiyAccount();
		}, null);
	}

	// Token: 0x060048CA RID: 18634 RVA: 0x00176F2C File Offset: 0x0017512C
	public void OnClickNoticeBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.LoginNoticeRootUI, delegate
		{
			SingletonUnity<LoginNoticeRootLogic>.Instance.Reset(this.mNoticeStr);
		}, null);
	}

	// Token: 0x0400360C RID: 13836
	public UILabel ServerLabel;

	// Token: 0x0400360D RID: 13837
	public UISprite ServerStatePic;

	// Token: 0x0400360E RID: 13838
	public UILabel VersionLabel;

	// Token: 0x0400360F RID: 13839
	public GameObject ChooseServerRoot;

	// Token: 0x04003610 RID: 13840
	public UITexture BottomPic;

	// Token: 0x04003611 RID: 13841
	private string mNoticeStr;

	// Token: 0x04003612 RID: 13842
	private string mNoticeVersion;

	// Token: 0x04003613 RID: 13843
	private float lastUpdateServerTime;

	// Token: 0x04003614 RID: 13844
	private float UpdateDeltaTime = 10f;
}
