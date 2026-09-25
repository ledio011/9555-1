using System;
using SprotoType;
using UnityEngine;

// Token: 0x020009F6 RID: 2550
public class ServerSubLineLogic : MonoBehaviour
{
	// Token: 0x06004904 RID: 18692 RVA: 0x0017869C File Offset: 0x0017689C
	public void Reset(game_server serverinfo, ServerSubLineLogic.ClickServer lineserver = null)
	{
		this.CurServer = serverinfo;
		this.ServerName.text = this.CurServer.serverName;
		if (serverinfo.HasNewServer && serverinfo.newServer == 1L && this.CurServer.serverState != 3L)
		{
			this.StateSp.color = Color.red;
			this.NewFlag.enabled = true;
		}
		else
		{
			this.NewFlag.enabled = false;
			this.StateSp.color = GameDefine.SERVER_STATE_COLOR[(int)this.CurServer.serverState];
		}
		this.clickitem = lineserver;
	}

	// Token: 0x06004905 RID: 18693 RVA: 0x00178748 File Offset: 0x00176948
	public void OnClickServerItem()
	{
		if (this.clickitem != null)
		{
			this.clickitem(this.CurServer);
		}
	}

	// Token: 0x04003627 RID: 13863
	public UISprite StateSp;

	// Token: 0x04003628 RID: 13864
	public UILabel ServerName;

	// Token: 0x04003629 RID: 13865
	private game_server CurServer;

	// Token: 0x0400362A RID: 13866
	public UISprite NewFlag;

	// Token: 0x0400362B RID: 13867
	private ServerSubLineLogic.ClickServer clickitem;

	// Token: 0x02000AFD RID: 2813
	// (Invoke) Token: 0x0600507D RID: 20605
	public delegate void ClickServer(game_server server);
}
