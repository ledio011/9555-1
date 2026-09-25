using System;
using System.Net;
using System.Net.Sockets;

// Token: 0x02000807 RID: 2055
public class SocketAPI
{
	// Token: 0x0600314D RID: 12621 RVA: 0x000C06DC File Offset: 0x000BE8DC
	public static Socket Connect(string IP, int nPort)
	{
		try
		{
			Socket socket = new Socket(2, 1, 6);
			if (PlayerData.CurLoginServerData.IsUseDns)
			{
				IPAddress[] addressList = Dns.GetHostEntry(IP).AddressList;
				Random random = new Random();
				IPEndPoint ipendPoint = new IPEndPoint(addressList[random.Next(0, addressList.Length)], nPort);
				socket.Connect(ipendPoint);
			}
			else
			{
				IPEndPoint ipendPoint2 = new IPEndPoint(IPAddress.Parse(IP), nPort);
				socket.Connect(ipendPoint2);
			}
			return socket;
		}
		catch (Exception ex)
		{
			Log.WARING_MSG(string.Concat(new string[]
			{
				"Socket connect erro!ip= ",
				IP.ToString(),
				" port=",
				nPort.ToString(),
				"=",
				ex.ToString()
			}));
		}
		return null;
	}

	// Token: 0x0600314E RID: 12622 RVA: 0x000C07C4 File Offset: 0x000BE9C4
	public static uint Send(Socket client, byte[] buff, uint len, SocketFlags flags = 0)
	{
		try
		{
			return (uint)client.Send(buff, (int)len, flags);
		}
		catch (Exception ex)
		{
			Log.WARING_MSG("Socket Send erro!=" + ex.ToString());
		}
		return uint.MaxValue;
	}

	// Token: 0x0600314F RID: 12623 RVA: 0x000C0820 File Offset: 0x000BEA20
	public static uint Recv(Socket client, byte[] buff, uint len, uint flags = 0U)
	{
		try
		{
			return (uint)client.Receive(buff, (int)len, flags);
		}
		catch (Exception ex)
		{
			Log.WARING_MSG("Socket Recv erro!=" + ex.ToString());
		}
		return uint.MaxValue;
	}

	// Token: 0x06003150 RID: 12624 RVA: 0x000C0880 File Offset: 0x000BEA80
	public static void Close(Socket client)
	{
		try
		{
			if (client.Connected)
			{
				client.Shutdown(2);
			}
		}
		catch (Exception ex)
		{
			Log.WARING_MSG("Shutdown Socket erro! =" + ex.ToString());
		}
		try
		{
			client.Close();
		}
		catch (Exception ex2)
		{
			Log.WARING_MSG("Close Socket erro! =" + ex2.ToString());
		}
	}

	// Token: 0x06003151 RID: 12625 RVA: 0x000C091C File Offset: 0x000BEB1C
	public static uint Available(Socket client)
	{
		return (uint)client.Available;
	}
}
