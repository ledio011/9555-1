using System;
using UnityEngine;

// Token: 0x020008A6 RID: 2214
public class TestNet : MonoBehaviour
{
	// Token: 0x06003BB7 RID: 15287 RVA: 0x001048C4 File Offset: 0x00102AC4
	public void ConnectServerOnClick()
	{
		SingletonDontDestoryUnity<NetManager>.Instance.ConnectToServer(this.ip, this.port, null, null);
	}

	// Token: 0x06003BB8 RID: 15288 RVA: 0x001048E0 File Offset: 0x00102AE0
	private void Start()
	{
	}

	// Token: 0x06003BB9 RID: 15289 RVA: 0x001048E4 File Offset: 0x00102AE4
	private void Update()
	{
	}

	// Token: 0x04002716 RID: 10006
	private string ip = "54.174.215.170";

	// Token: 0x04002717 RID: 10007
	private int port = 8888;
}
