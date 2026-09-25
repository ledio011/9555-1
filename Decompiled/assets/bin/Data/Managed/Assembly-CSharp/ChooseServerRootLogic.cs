using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009EF RID: 2543
public class ChooseServerRootLogic : SingletonUnity<ChooseServerRootLogic>
{
	// Token: 0x0600489B RID: 18587 RVA: 0x00175484 File Offset: 0x00173684
	public void Reset()
	{
		int num = GameDefine.SERVER_AREA_NAME.Count - this.AreaBtn.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.AreaBtn[0].gameObject) as GameObject;
				this.AreaGrid.AddChild(gameObject.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.name = string.Format("AreaBtn{0:d2}", this.AreaBtn.Count);
				this.AreaBtn.Add(gameObject.GetComponent<ServerAreaLogic>());
			}
			this.AreaGrid.Reposition();
		}
		for (int j = 0; j < this.AreaBtn.Count; j++)
		{
			this.AreaBtn[j].Reset(j - 1, new DelegateDefine.OneIntParamDelegate(this.OnClickAreaItem));
		}
		this.CurAreaID = -2;
		this.AreaBtn[0].OnClickAreaItem();
	}

	// Token: 0x0600489C RID: 18588 RVA: 0x001755A4 File Offset: 0x001737A4
	public void RefershServerList()
	{
		if (this.CurGameServerList.Count > 0)
		{
			this.CurGameServerList = SingletonUnity<MenuSceneController>.Instance.GetAreaServerList(this.CurAreaID);
			this.RefershServerItems();
		}
	}

	// Token: 0x0600489D RID: 18589 RVA: 0x001755D4 File Offset: 0x001737D4
	public void OnClickAreaItem(int areaid)
	{
		if (this.CurAreaID == areaid)
		{
			return;
		}
		this.CurAreaID = areaid;
		this.SelectAreaItem();
		this.CurGameServerList.Clear();
		this.CurGameServerList = SingletonUnity<MenuSceneController>.Instance.GetAreaServerList(this.CurAreaID);
		int num = this.CurGameServerList.Count / 10;
		if (this.CurGameServerList.Count % 10 != 0)
		{
			num++;
		}
		int num2 = num - this.LineItems.Count;
		if (num2 > 0)
		{
			for (int i = 0; i < num2; i++)
			{
				GameObject gameObject = Object.Instantiate(this.LineItems[0].gameObject) as GameObject;
				this.LineGrid.AddChild(gameObject.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.name = string.Format("line{0:d2}", this.LineItems.Count);
				this.LineItems.Add(gameObject.GetComponent<ServerLineLogic>());
			}
		}
		for (int j = 0; j < this.LineItems.Count; j++)
		{
			if (j < num)
			{
				UnityVersionUtil.SetActiveRecursive(this.LineItems[j].gameObject, true);
				this.LineItems[j].Reset(j, new DelegateDefine.OneIntParamDelegate(this.OnClickLineItem));
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.LineItems[j].gameObject, false);
			}
		}
		this.LineGrid.Reposition();
		this.CurLineIndex = -1;
		for (int k = 0; k < this.SubLineItems.Count; k++)
		{
			UnityVersionUtil.SetActiveRecursive(this.SubLineItems[k].gameObject, false);
		}
		if (this.CurGameServerList.Count > 0)
		{
			int num3 = 0;
			if (this.CurAreaID >= 0)
			{
				num3 = num - 1;
			}
			this.LineItems[num3].OnClickLineItem();
			this.EmptyTipsLabel.enabled = false;
		}
		else
		{
			this.EmptyTipsLabel.enabled = (this.CurAreaID >= 0);
		}
	}

	// Token: 0x0600489E RID: 18590 RVA: 0x00175814 File Offset: 0x00173A14
	private void SelectAreaItem()
	{
		for (int i = 0; i < this.AreaBtn.Count; i++)
		{
			this.AreaBtn[i].RefershSelect(this.CurAreaID);
		}
	}

	// Token: 0x0600489F RID: 18591 RVA: 0x00175854 File Offset: 0x00173A54
	public void OnClickLineItem(int lineindex)
	{
		if (this.CurLineIndex == lineindex)
		{
			return;
		}
		this.CurLineIndex = lineindex;
		this.SelectLineItem();
		int num = 10;
		if (num > this.CurGameServerList.Count - this.CurLineIndex * 10)
		{
			num = this.CurGameServerList.Count - this.CurLineIndex * 10;
		}
		int num2 = num - this.SubLineItems.Count;
		if (num2 > 0)
		{
			for (int i = 0; i < num2; i++)
			{
				GameObject gameObject = Object.Instantiate(this.SubLineItems[0].gameObject) as GameObject;
				this.SubLineGrid.AddChild(gameObject.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.name = string.Format("lineitem{0:d2}", this.SubLineItems.Count);
				this.SubLineItems.Add(gameObject.GetComponent<ServerSubLineLogic>());
			}
		}
		for (int j = 0; j < this.SubLineItems.Count; j++)
		{
			if (j < num)
			{
				UnityVersionUtil.SetActiveRecursive(this.SubLineItems[j].gameObject, true);
				this.SubLineItems[j].Reset(this.CurGameServerList[this.CurLineIndex * 10 + j], new ServerSubLineLogic.ClickServer(this.OnClickSubLineItem));
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.SubLineItems[j].gameObject, false);
			}
		}
		this.SubLineGrid.Reposition();
	}

	// Token: 0x060048A0 RID: 18592 RVA: 0x001759F8 File Offset: 0x00173BF8
	public void RefershServerItems()
	{
		if (this.CurLineIndex == -1)
		{
			return;
		}
		this.SelectLineItem();
		int num = 10;
		if (num > this.CurGameServerList.Count - this.CurLineIndex * 10)
		{
			num = this.CurGameServerList.Count - this.CurLineIndex * 10;
		}
		int num2 = num - this.SubLineItems.Count;
		if (num2 > 0)
		{
			for (int i = 0; i < num2; i++)
			{
				GameObject gameObject = Object.Instantiate(this.SubLineItems[0].gameObject) as GameObject;
				this.SubLineGrid.AddChild(gameObject.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.name = string.Format("lineitem{0:d2}", this.SubLineItems.Count);
				this.SubLineItems.Add(gameObject.GetComponent<ServerSubLineLogic>());
			}
		}
		for (int j = 0; j < this.SubLineItems.Count; j++)
		{
			if (j < num)
			{
				UnityVersionUtil.SetActiveRecursive(this.SubLineItems[j].gameObject, true);
				this.SubLineItems[j].Reset(this.CurGameServerList[this.CurLineIndex * 10 + j], new ServerSubLineLogic.ClickServer(this.OnClickSubLineItem));
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.SubLineItems[j].gameObject, false);
			}
		}
		this.SubLineGrid.Reposition();
	}

	// Token: 0x060048A1 RID: 18593 RVA: 0x00175B94 File Offset: 0x00173D94
	public void OnClickSubLineItem(game_server serverinfo)
	{
		if (serverinfo.HasServerState && serverinfo.serverState == 3L)
		{
			MessageBoxLogic.OpenOKBox("#{200128}", "#{100127}", null);
			return;
		}
		this.curSelectServer = serverinfo;
		PlayerData.CurGameServerData = new game_server();
		PlayerData.CurGameServerData.serverId = this.curSelectServer.serverId;
		PlayerData.CurGameServerData.serverIP = this.curSelectServer.serverIP;
		PlayerData.CurGameServerData.serverPort = this.curSelectServer.serverPort;
		PlayerData.CurGameServerData.serverName = this.curSelectServer.serverName;
		PlayerData.CurGameServerData.serverState = this.curSelectServer.serverState;
		if (this.curSelectServer.HasNewServer)
		{
			PlayerData.CurGameServerData.newServer = this.curSelectServer.newServer;
		}
		this.OnClickCloseBtn();
	}

	// Token: 0x060048A2 RID: 18594 RVA: 0x00175C70 File Offset: 0x00173E70
	private void SelectLineItem()
	{
		for (int i = 0; i < this.LineItems.Count; i++)
		{
			this.LineItems[i].RefershSelect(this.CurLineIndex);
		}
	}

	// Token: 0x060048A3 RID: 18595 RVA: 0x00175CB0 File Offset: 0x00173EB0
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ChooseServerRootUI);
		SingletonUnity<LoginRootLogic>.Instance.Reset(string.Empty, string.Empty);
	}

	// Token: 0x040035EE RID: 13806
	public UIGrid AreaGrid;

	// Token: 0x040035EF RID: 13807
	public List<ServerAreaLogic> AreaBtn;

	// Token: 0x040035F0 RID: 13808
	private int CurAreaID = -2;

	// Token: 0x040035F1 RID: 13809
	private List<game_server> CurGameServerList = new List<game_server>();

	// Token: 0x040035F2 RID: 13810
	public List<ServerLineLogic> LineItems;

	// Token: 0x040035F3 RID: 13811
	public UIGrid LineGrid;

	// Token: 0x040035F4 RID: 13812
	private int CurLineIndex = -1;

	// Token: 0x040035F5 RID: 13813
	public List<ServerSubLineLogic> SubLineItems;

	// Token: 0x040035F6 RID: 13814
	public UIGrid SubLineGrid;

	// Token: 0x040035F7 RID: 13815
	private game_server curSelectServer;

	// Token: 0x040035F8 RID: 13816
	public UILabel EmptyTipsLabel;
}
