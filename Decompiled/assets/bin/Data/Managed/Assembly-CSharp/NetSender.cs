using System;
using System.Collections.Generic;
using Sproto;

// Token: 0x020002DF RID: 735
public class NetSender
{
	// Token: 0x06001493 RID: 5267 RVA: 0x00085E0C File Offset: 0x0008400C
	public static void Init()
	{
		NetSender.rpcRspHandlerDict = new Dictionary<long, RpcRspHandler>();
		NetSender.session = 0L;
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x00085E20 File Offset: 0x00084020
	public static void SendInternal<T>(SprotoTypeBase rpcReq = null, RpcRspHandler rpcRspHandler = null)
	{
		if (rpcRspHandler != null)
		{
			NetSender.session += 1L;
			NetSender.AddHandler(NetSender.session, rpcRspHandler);
			NetLogic.GetInstance().SendInternal<T>(rpcReq, new long?(NetSender.session));
		}
		else
		{
			NetLogic.GetInstance().SendInternal<T>(rpcReq, default(long?));
		}
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x00085E7C File Offset: 0x0008407C
	private static void AddHandler(long session, RpcRspHandler rpcRspHandler)
	{
		NetSender.rpcRspHandlerDict.Add(session, rpcRspHandler);
	}

	// Token: 0x06001496 RID: 5270 RVA: 0x00085E8C File Offset: 0x0008408C
	private static void RemoveHandler(long session)
	{
		if (NetSender.rpcRspHandlerDict.ContainsKey(session))
		{
			NetSender.rpcRspHandlerDict.Remove(session);
		}
	}

	// Token: 0x06001497 RID: 5271 RVA: 0x00085EAC File Offset: 0x000840AC
	public static RpcRspHandler GetHandler(long session)
	{
		RpcRspHandler result;
		NetSender.rpcRspHandlerDict.TryGetValue(session, ref result);
		NetSender.RemoveHandler(session);
		return result;
	}

	// Token: 0x0400181E RID: 6174
	private static long session;

	// Token: 0x0400181F RID: 6175
	private static Dictionary<long, RpcRspHandler> rpcRspHandlerDict;
}
