using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;

// Token: 0x02000808 RID: 2056
public class SocketInstance
{
	// Token: 0x17000E23 RID: 3619
	// (get) Token: 0x06003153 RID: 12627 RVA: 0x000C092C File Offset: 0x000BEB2C
	public bool IsValid
	{
		get
		{
			return this.mSocket != null;
		}
	}

	// Token: 0x17000E24 RID: 3620
	// (get) Token: 0x06003154 RID: 12628 RVA: 0x000C093C File Offset: 0x000BEB3C
	public bool IsConnected
	{
		get
		{
			return this.mSocket != null && this.mSocket.Connected;
		}
	}

	// Token: 0x06003155 RID: 12629 RVA: 0x000C0958 File Offset: 0x000BEB58
	public long Ping()
	{
		Ping ping = new Ping();
		int num = 5000;
		PingReply pingReply = ping.Send(this.mIP, num);
		if (pingReply.Status == null)
		{
			return pingReply.RoundtripTime;
		}
		return -1L;
	}

	// Token: 0x06003156 RID: 12630 RVA: 0x000C0994 File Offset: 0x000BEB94
	public void Connect(string ip, int port)
	{
		this.mIP = ip;
		this.mPort = port;
		this.mSocket = SocketAPI.Connect(this.mIP, this.mPort);
	}

	// Token: 0x06003157 RID: 12631 RVA: 0x000C09BC File Offset: 0x000BEBBC
	public uint Send(byte[] buff, int len, SocketFlags flags = 0)
	{
		return SocketAPI.Send(this.mSocket, buff, (uint)len, flags);
	}

	// Token: 0x06003158 RID: 12632 RVA: 0x000C09CC File Offset: 0x000BEBCC
	public uint Recv(byte[] buff, int len, uint flags = 0U)
	{
		return SocketAPI.Recv(this.mSocket, buff, (uint)len, flags);
	}

	// Token: 0x06003159 RID: 12633 RVA: 0x000C09DC File Offset: 0x000BEBDC
	public uint Avaiable()
	{
		return SocketAPI.Available(this.mSocket);
	}

	// Token: 0x0600315A RID: 12634 RVA: 0x000C09EC File Offset: 0x000BEBEC
	public bool IsCanSend()
	{
		return this.mSocket != null && this.mSocket.Poll(0, 1);
	}

	// Token: 0x0600315B RID: 12635 RVA: 0x000C0A08 File Offset: 0x000BEC08
	public bool IsCanReceive()
	{
		return this.mSocket != null && this.mSocket.Poll(0, 0);
	}

	// Token: 0x0600315C RID: 12636 RVA: 0x000C0A24 File Offset: 0x000BEC24
	public void Close()
	{
		if (this.mSocket != null)
		{
			this.mSocket.Close();
			this.mSocket = null;
		}
	}

	// Token: 0x04002118 RID: 8472
	private Socket mSocket;

	// Token: 0x04002119 RID: 8473
	private string mIP;

	// Token: 0x0400211A RID: 8474
	private int mPort;
}
