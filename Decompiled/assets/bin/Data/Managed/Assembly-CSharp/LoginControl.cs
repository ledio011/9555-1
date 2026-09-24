using System;
using Sproto;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class LoginControl : MonoBehaviour
{
	// Token: 0x0600143C RID: 5180 RVA: 0x00083400 File Offset: 0x00081600
	public void Connect(bool isSuccess)
	{
		if (isSuccess)
		{
		}
	}

	// Token: 0x0600143D RID: 5181 RVA: 0x00083408 File Offset: 0x00081608
	private void HandShakeRequest(string name, string key)
	{
	}

	// Token: 0x0600143E RID: 5182 RVA: 0x0008340C File Offset: 0x0008160C
	private void HandShakeResponse(SprotoTypeBase rpcRsp)
	{
	}

	// Token: 0x0600143F RID: 5183 RVA: 0x00083410 File Offset: 0x00081610
	private void SendAuthRequest(string res)
	{
	}

	// Token: 0x06001440 RID: 5184 RVA: 0x00083414 File Offset: 0x00081614
	public void ConnectServer()
	{
		SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(this.ip, this.port, new NetLogic.ConnectDelegate(this.Connect), null);
	}

	// Token: 0x040017E5 RID: 6117
	public string ip = "127.0.0.1";

	// Token: 0x040017E6 RID: 6118
	public int port = 9777;
}
