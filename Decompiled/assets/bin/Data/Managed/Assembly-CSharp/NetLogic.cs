using System;
using System.Collections.Generic;
using System.Threading;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x020002DB RID: 731
public class NetLogic
{
	// Token: 0x06001441 RID: 5185 RVA: 0x0008343C File Offset: 0x0008163C
	private NetLogic()
	{
		this.mSocket = new SocketInstance();
		this.mMaxRevOnePackCount = 16384;
		this.mMaxRevOnePackbytes = new byte[16384];
		this.mMaxSendOnePackbytes = new byte[16384];
		NetReceiver.Init();
		NetSender.Init();
		NetLogic.sessionDict = new Dictionary<long, ProtocolFunctionDictionary.typeFunc>();
		this.connectThread = null;
	}

	// Token: 0x170003D3 RID: 979
	// (get) Token: 0x06001443 RID: 5187 RVA: 0x00083558 File Offset: 0x00081758
	public NetLogic.CONNECT_STATUS ConnectStatus
	{
		get
		{
			return this.mConnectStatus;
		}
	}

	// Token: 0x06001444 RID: 5188 RVA: 0x00083560 File Offset: 0x00081760
	public static NetLogic GetInstance()
	{
		if (NetLogic.mNetLogic == null)
		{
			NetLogic.mNetLogic = new NetLogic();
		}
		return NetLogic.mNetLogic;
	}

	// Token: 0x170003D4 RID: 980
	// (get) Token: 0x06001445 RID: 5189 RVA: 0x0008357C File Offset: 0x0008177C
	// (set) Token: 0x06001446 RID: 5190 RVA: 0x00083584 File Offset: 0x00081784
	public bool CanProcessPack
	{
		get
		{
			return this.isCanProcessPack;
		}
		set
		{
			this.isCanProcessPack = value;
		}
	}

	// Token: 0x170003D5 RID: 981
	// (get) Token: 0x06001447 RID: 5191 RVA: 0x00083590 File Offset: 0x00081790
	public bool ReConnectingFlag
	{
		get
		{
			return this.mReConnectingFlag;
		}
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x00083598 File Offset: 0x00081798
	public void StartReconnecting()
	{
		this.mReConnectingFlag = true;
		if (Singleton<ObjManager>.Exists && Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.DisactiveTargetArriveFinish();
			Singleton<ObjManager>.Instance.MainPlayer.StopMove();
		}
	}

	// Token: 0x06001449 RID: 5193 RVA: 0x000835EC File Offset: 0x000817EC
	public void FinishReconnecting()
	{
		this.mReConnectingFlag = false;
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x000835F8 File Offset: 0x000817F8
	public static void SetConnectDelegate(NetLogic.ConnectDelegate func)
	{
		NetLogic.mConnectDel = func;
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x00083600 File Offset: 0x00081800
	public static void SetConnectLostDelegate(NetLogic.ConnectLostDelegate func)
	{
		NetLogic.mConnectLostDel = func;
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x00083608 File Offset: 0x00081808
	public void Ping(NetLogic.PingComplete complete)
	{
		if (!this.mSocket.IsValid || !this.mSocket.IsConnected)
		{
			if (complete != null)
			{
				complete(-1L);
			}
			return;
		}
		NetLogic.mPingComplete = complete;
		this.connectThread = new Thread(new ParameterizedThreadStart(NetLogic._Ping));
		this.connectThread.Start(this);
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x00083670 File Offset: 0x00081870
	public void IntenralPing()
	{
		this.pingTime = -2L;
		this.pingTime = this.mSocket.Ping();
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x0008368C File Offset: 0x0008188C
	protected static void _Ping(object obj)
	{
		NetLogic netLogic = obj as NetLogic;
		netLogic.IntenralPing();
	}

	// Token: 0x0600144F RID: 5199 RVA: 0x000836A8 File Offset: 0x000818A8
	public void ConnectToServer(string serIP, int serPort, int sleepTime)
	{
		if (this.mConnectStatus == NetLogic.CONNECT_STATUS.CONNECTING)
		{
			return;
		}
		GameManager.OnLineState = false;
		this.serverIp = serIP;
		this.serverPort = serPort;
		this.connectSleepTime = sleepTime;
		this.connectThread = new Thread(new ParameterizedThreadStart(NetLogic._ConnectThread));
		this.connectThread.Start(this);
		this.curConnectTimeOut = this.MAX_CONNECT_TIME;
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x0008370C File Offset: 0x0008190C
	public void ReConnectToServer()
	{
		if (this.mConnectStatus == NetLogic.CONNECT_STATUS.CONNECTING)
		{
			return;
		}
		GameManager.OnLineState = false;
		if (this.mSocketReciveStream != null)
		{
			this.mSocketReciveStream.Clean();
		}
		if (this.mSocketSendStream != null)
		{
			this.mSocketSendStream.Clean();
		}
		this.connectThread = new Thread(new ParameterizedThreadStart(NetLogic._ConnectThread));
		this.connectThread.Start(this);
		this.curConnectTimeOut = this.MAX_CONNECT_TIME;
	}

	// Token: 0x06001451 RID: 5201 RVA: 0x00083788 File Offset: 0x00081988
	~NetLogic()
	{
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x000837C0 File Offset: 0x000819C0
	public void ConnectThread()
	{
		this.mConnectStatus = NetLogic.CONNECT_STATUS.CONNECTING;
		this.mSocket.Close();
		this.mSocket.Connect(this.serverIp, this.serverPort);
		if (this.mSocket.IsValid)
		{
			this.mSocketReciveStream = new SocketReciveStream(this.mSocket, 262144U, 524288U);
			this.mSocketSendStream = new SocketSendStream(this.mSocket, 8192U, 20480U);
			this.mConnectStatus = NetLogic.CONNECT_STATUS.CONNRCTED;
		}
		else
		{
			Debug.LogWarning("!!!Connect erro!!!");
			this.mSocket.Close();
			this.mConnectStatus = NetLogic.CONNECT_STATUS.DISCONNECTED;
		}
		this.mConnectFinish = true;
	}

	// Token: 0x06001453 RID: 5203 RVA: 0x0008387C File Offset: 0x00081A7C
	protected static void _ConnectThread(object obj)
	{
		NetLogic netLogic = obj as NetLogic;
		netLogic.ConnectThread();
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x00083898 File Offset: 0x00081A98
	public void ConnectLost()
	{
		this.mConnectStatus = NetLogic.CONNECT_STATUS.DISCONNECTED;
		GameManager.OnLineState = false;
		if (NetLogic.mConnectLostDel != null)
		{
			NetLogic.mConnectLostDel();
		}
	}

	// Token: 0x06001455 RID: 5205 RVA: 0x000838BC File Offset: 0x00081ABC
	public void DisconnectServer()
	{
		this.mSocket.Close();
		this.mConnectStatus = NetLogic.CONNECT_STATUS.DISCONNECTED;
	}

	// Token: 0x06001456 RID: 5206 RVA: 0x000838D0 File Offset: 0x00081AD0
	public void TestDisconnectServer()
	{
		this.SendLast();
		this.mSocket.Close();
		this.mConnectStatus = NetLogic.CONNECT_STATUS.DISCONNECTED;
		this.ConnectLost();
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x000838F0 File Offset: 0x00081AF0
	public void Send<T>(SprotoTypeBase rpcReq = null, RpcRspHandler rpcRspHandler = null)
	{
		if (!this.mReConnectingFlag)
		{
			NetSender.SendInternal<T>(rpcReq, rpcRspHandler);
		}
		else
		{
			int num = NetLogic.protocol[typeof(T)];
			if (num == 218 || num == 234)
			{
				NetSender.SendInternal<T>(rpcReq, rpcRspHandler);
			}
		}
	}

	// Token: 0x06001458 RID: 5208 RVA: 0x00083948 File Offset: 0x00081B48
	public void SendInternal<T>(SprotoTypeBase rpc = null, long? session = null)
	{
		this.SendInternal(rpc, session, new int?(NetLogic.protocol[typeof(T)]));
	}

	// Token: 0x06001459 RID: 5209 RVA: 0x0008396C File Offset: 0x00081B6C
	public void Update()
	{
		if (this.mConnectStatus == NetLogic.CONNECT_STATUS.CONNECTING)
		{
			this.curConnectTimeOut -= Time.deltaTime;
			if (this.curConnectTimeOut <= 0f)
			{
				this.connectThread.Abort();
				this.mConnectStatus = NetLogic.CONNECT_STATUS.DISCONNECTED;
				this.mConnectFinish = true;
			}
		}
		if (this.mConnectFinish)
		{
			if (NetLogic.mConnectDel != null)
			{
				NetLogic.mConnectDel(this.mConnectStatus == NetLogic.CONNECT_STATUS.CONNRCTED);
			}
			this.mConnectFinish = false;
		}
		if (this.pingTime != -2L)
		{
			if (NetLogic.mPingComplete != null)
			{
				NetLogic.mPingComplete(this.pingTime);
			}
			this.pingTime = -2L;
		}
		this.ProcessNet();
	}

	// Token: 0x0600145A RID: 5210 RVA: 0x00083A28 File Offset: 0x00081C28
	public void SendLast()
	{
		if (!this.mSocket.IsValid)
		{
			return;
		}
		if (!this.mSocket.IsConnected)
		{
			return;
		}
		this.ProcessSend();
	}

	// Token: 0x0600145B RID: 5211 RVA: 0x00083A54 File Offset: 0x00081C54
	public void ProcessNet()
	{
		if (!this.mSocket.IsValid)
		{
			return;
		}
		if (!this.mSocket.IsConnected)
		{
			return;
		}
		if (!this.ProcessSend())
		{
			return;
		}
		if (!this.ProcessRecive())
		{
			return;
		}
		this.ProcessPack();
	}

	// Token: 0x0600145C RID: 5212 RVA: 0x00083AA4 File Offset: 0x00081CA4
	private void SendInternal(SprotoTypeBase rpc, long? session, int? tag = null)
	{
		if (this.mSocketSendStream == null || this.mConnectStatus != NetLogic.CONNECT_STATUS.CONNRCTED)
		{
			return;
		}
		this.sendPkg.clear();
		if (tag != null)
		{
			this.sendPkg.type = (long)tag.Value;
		}
		if (session != null)
		{
			this.sendPkg.session = session.Value;
			if (tag != null)
			{
				NetLogic.sessionDict.Add(session.Value, NetLogic.protocol[tag.Value].Response.Value);
			}
		}
		NetLogic.sendStream.Seek(0, 0);
		int num = this.sendPkg.encode(NetLogic.sendStream);
		if (rpc != null)
		{
			num += rpc.encode(NetLogic.sendStream);
		}
		byte[] buff = NetLogic.sendPack.pack(NetLogic.sendStream.Buffer, this.mMaxSendOnePackbytes, ref num);
		this.headDataBuff[0] = (byte)(num >> 8);
		this.headDataBuff[1] = (byte)num;
		uint buffLen = this.mSocketSendStream.GetBuffLen();
		this.mSocketSendStream.Write(this.headDataBuff, 2U);
		this.mSocketSendStream.Write(buff, (uint)num);
		uint buffLen2 = this.mSocketSendStream.GetBuffLen();
		if (buffLen < buffLen2)
		{
			if (NetLogic.nSendCount < 0)
			{
				NetLogic.nSendCount = 0;
			}
			NetLogic.nSendCount += (int)(buffLen2 - buffLen);
		}
	}

	// Token: 0x0600145D RID: 5213 RVA: 0x00083C10 File Offset: 0x00081E10
	private bool ProcessSend()
	{
		if (this.mSocketSendStream == null)
		{
			return false;
		}
		if (!this.mSocket.IsCanSend())
		{
			return true;
		}
		uint num = this.mSocketSendStream.Send();
		if (num == 4294967295U)
		{
			this.mSocket.Close();
			this.ConnectLost();
			return false;
		}
		return true;
	}

	// Token: 0x0600145E RID: 5214 RVA: 0x00083C64 File Offset: 0x00081E64
	private bool ProcessRecive()
	{
		if (this.mSocketReciveStream == null)
		{
			return false;
		}
		if (!this.mSocket.IsCanReceive())
		{
			return true;
		}
		uint buffLen = this.mSocketReciveStream.GetBuffLen();
		uint num = this.mSocketReciveStream.Recive();
		uint buffLen2 = this.mSocketReciveStream.GetBuffLen();
		if (num == 4294967295U)
		{
			this.mSocket.Close();
			this.ConnectLost();
			return false;
		}
		if (buffLen < buffLen2)
		{
			if (NetLogic.nReceiveCount < 0)
			{
				NetLogic.nReceiveCount = 0;
			}
			NetLogic.nReceiveCount += (int)(buffLen2 - buffLen);
		}
		return true;
	}

	// Token: 0x0600145F RID: 5215 RVA: 0x00083CF8 File Offset: 0x00081EF8
	private void ProcessPack()
	{
		if (this.mSocketReciveStream == null)
		{
			return;
		}
		this.nProcesspackCount = 10;
		while (this.isCanProcessPack && this.nProcesspackCount-- > 0)
		{
			if (!this.mSocketReciveStream.Peek(this.headDataBuff, 2U))
			{
				return;
			}
			int num = (int)this.headDataBuff[0] << 8 | (int)this.headDataBuff[1];
			if (num <= 0)
			{
				break;
			}
			if ((ulong)this.mSocketReciveStream.GetBuffLen() < (ulong)((long)(num + 2)))
			{
				break;
			}
			this.mSocketReciveStream.Skip(2U);
			if ((float)num > (float)this.mMaxRevOnePackCount * 0.5f)
			{
				this.mMaxRevOnePackCount = num * 2;
				this.mMaxRevOnePackbytes = new byte[this.mMaxRevOnePackCount];
				Log.DEBUG_MSG("Pack is Max=" + num.ToString());
			}
			this.mSocketReciveStream.Read(this.mMaxRevOnePackbytes, (uint)num);
			this.pkg.clear();
			byte[] buffer = NetLogic.recvPack.unpack(this.mMaxRevOnePackbytes, ref num);
			int offset = this.pkg.init(buffer, 0, num);
			if (this.pkg.HasType)
			{
				int tag = (int)this.pkg.type;
				RpcReqHandler handler = NetReceiver.GetHandler(tag);
				if (handler != null)
				{
					SprotoTypeBase rpc = handler(NetLogic.protocol.GenRequest(tag, buffer, offset, num));
					if (this.pkg.HasSession)
					{
						int num2 = (int)this.pkg.session;
						this.SendInternal(rpc, new long?((long)num2), default(int?));
					}
				}
			}
			else if (this.pkg.HasSession)
			{
				int num3 = (int)this.pkg.session;
				RpcRspHandler handler2 = NetSender.GetHandler((long)num3);
				if (handler2 != null)
				{
					ProtocolFunctionDictionary.typeFunc typeFunc;
					NetLogic.sessionDict.TryGetValue((long)num3, ref typeFunc);
					handler2(typeFunc(buffer, offset, num));
				}
			}
		}
	}

	// Token: 0x06001460 RID: 5216 RVA: 0x00083EF8 File Offset: 0x000820F8
	public static T GetSprotoInstance<T>() where T : SprotoTypeBase
	{
		Type typeFromHandle = typeof(T);
		SprotoTypeBase sprotoTypeBase;
		if (!NetLogic.cacheDict.TryGetValue(typeFromHandle, ref sprotoTypeBase))
		{
			sprotoTypeBase = Activator.CreateInstance<T>();
			NetLogic.cacheDict.Add(typeFromHandle, sprotoTypeBase);
		}
		sprotoTypeBase.clear();
		return (T)((object)sprotoTypeBase);
	}

	// Token: 0x040017E7 RID: 6119
	public const uint SOCKET_ERROR = 4294967295U;

	// Token: 0x040017E8 RID: 6120
	public const int MAX_ONE_PACK_BYTE_SIZE = 16384;

	// Token: 0x040017E9 RID: 6121
	private const int MAX_PROCESS_PACK_COUNT_FRAME = 10;

	// Token: 0x040017EA RID: 6122
	private static SprotoPack sendPack = new SprotoPack();

	// Token: 0x040017EB RID: 6123
	private static SprotoPack recvPack = new SprotoPack();

	// Token: 0x040017EC RID: 6124
	private static SprotoStream sendStream = new SprotoStream();

	// Token: 0x040017ED RID: 6125
	private SocketReciveStream mSocketReciveStream;

	// Token: 0x040017EE RID: 6126
	private SocketSendStream mSocketSendStream;

	// Token: 0x040017EF RID: 6127
	private static ProtocolFunctionDictionary protocol = Protocol.Instance.Protocol;

	// Token: 0x040017F0 RID: 6128
	private static Dictionary<long, ProtocolFunctionDictionary.typeFunc> sessionDict;

	// Token: 0x040017F1 RID: 6129
	private byte[] headDataBuff = new byte[2];

	// Token: 0x040017F2 RID: 6130
	private Package pkg = new Package();

	// Token: 0x040017F3 RID: 6131
	private Package sendPkg = new Package();

	// Token: 0x040017F4 RID: 6132
	private SocketInstance mSocket;

	// Token: 0x040017F5 RID: 6133
	private Thread connectThread;

	// Token: 0x040017F6 RID: 6134
	private string serverIp;

	// Token: 0x040017F7 RID: 6135
	private int serverPort;

	// Token: 0x040017F8 RID: 6136
	private int connectSleepTime;

	// Token: 0x040017F9 RID: 6137
	private NetLogic.CONNECT_STATUS mConnectStatus;

	// Token: 0x040017FA RID: 6138
	private bool mConnectFinish;

	// Token: 0x040017FB RID: 6139
	private bool isCanProcessPack = true;

	// Token: 0x040017FC RID: 6140
	private int nProcesspackCount = 10;

	// Token: 0x040017FD RID: 6141
	private static NetLogic.ConnectDelegate mConnectDel = null;

	// Token: 0x040017FE RID: 6142
	private static NetLogic.ConnectLostDelegate mConnectLostDel = null;

	// Token: 0x040017FF RID: 6143
	private byte[] mMaxRevOnePackbytes;

	// Token: 0x04001800 RID: 6144
	private byte[] mMaxSendOnePackbytes;

	// Token: 0x04001801 RID: 6145
	private int mMaxRevOnePackCount;

	// Token: 0x04001802 RID: 6146
	private static NetLogic mNetLogic = null;

	// Token: 0x04001803 RID: 6147
	public static int nReceiveCount = 0;

	// Token: 0x04001804 RID: 6148
	public static int nSendCount = 0;

	// Token: 0x04001805 RID: 6149
	private static NetLogic.PingComplete mPingComplete = null;

	// Token: 0x04001806 RID: 6150
	private long pingTime = -2L;

	// Token: 0x04001807 RID: 6151
	private float MAX_CONNECT_TIME = 15f;

	// Token: 0x04001808 RID: 6152
	private float curConnectTimeOut = 15f;

	// Token: 0x04001809 RID: 6153
	private bool mReConnectingFlag;

	// Token: 0x0400180A RID: 6154
	private static Dictionary<Type, SprotoTypeBase> cacheDict = new Dictionary<Type, SprotoTypeBase>();

	// Token: 0x020002DC RID: 732
	public enum CONNECT_STATUS
	{
		// Token: 0x0400180C RID: 6156
		INVALID,
		// Token: 0x0400180D RID: 6157
		CONNECTING,
		// Token: 0x0400180E RID: 6158
		CONNRCTED,
		// Token: 0x0400180F RID: 6159
		DISCONNECTED
	}

	// Token: 0x02000AD7 RID: 2775
	// (Invoke) Token: 0x06004FE5 RID: 20453
	public delegate void ConnectDelegate(bool success);

	// Token: 0x02000AD8 RID: 2776
	// (Invoke) Token: 0x06004FE9 RID: 20457
	public delegate void ConnectLostDelegate();

	// Token: 0x02000AD9 RID: 2777
	// (Invoke) Token: 0x06004FED RID: 20461
	public delegate void PingComplete(long time);
}
