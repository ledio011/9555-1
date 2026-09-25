using System;
using System.Collections;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace System.Net
{
	// Token: 0x02000040 RID: 64
	public static class Dns
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00003F14 File Offset: 0x00002114
		static Dns()
		{
			System.Net.Sockets.Socket.CheckProtocolSupport();
		}

		// Token: 0x060000E1 RID: 225
		[MethodImpl(4096)]
		private static extern bool GetHostByName_internal(string host, out string h_name, out string[] h_aliases, out string[] h_addr_list);

		// Token: 0x060000E2 RID: 226
		[MethodImpl(4096)]
		private static extern bool GetHostByAddr_internal(string addr, out string h_name, out string[] h_aliases, out string[] h_addr_list);

		// Token: 0x060000E3 RID: 227
		[MethodImpl(4096)]
		private static extern bool GetHostName_internal(out string h_name);

		// Token: 0x060000E4 RID: 228 RVA: 0x00003F1C File Offset: 0x0000211C
		private static IPHostEntry hostent_to_IPHostEntry(string h_name, string[] h_aliases, string[] h_addrlist)
		{
			IPHostEntry iphostEntry = new IPHostEntry();
			ArrayList arrayList = new ArrayList();
			iphostEntry.HostName = h_name;
			iphostEntry.Aliases = h_aliases;
			for (int i = 0; i < h_addrlist.Length; i++)
			{
				try
				{
					IPAddress ipaddress = IPAddress.Parse(h_addrlist[i]);
					if ((System.Net.Sockets.Socket.SupportsIPv6 && ipaddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6) || (System.Net.Sockets.Socket.SupportsIPv4 && ipaddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork))
					{
						arrayList.Add(ipaddress);
					}
				}
				catch (ArgumentNullException)
				{
				}
			}
			if (arrayList.Count == 0)
			{
				throw new System.Net.Sockets.SocketException(11001);
			}
			iphostEntry.AddressList = (arrayList.ToArray(typeof(IPAddress)) as IPAddress[]);
			return iphostEntry;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003FE4 File Offset: 0x000021E4
		private static IPHostEntry GetHostByAddressFromString(string address, bool parse)
		{
			if (address.Equals("0.0.0.0"))
			{
				address = "127.0.0.1";
				parse = false;
			}
			if (parse)
			{
				IPAddress.Parse(address);
			}
			string h_name;
			string[] h_aliases;
			string[] h_addrlist;
			if (!Dns.GetHostByAddr_internal(address, out h_name, out h_aliases, out h_addrlist))
			{
				throw new System.Net.Sockets.SocketException(11001);
			}
			return Dns.hostent_to_IPHostEntry(h_name, h_aliases, h_addrlist);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004040 File Offset: 0x00002240
		public static IPHostEntry GetHostEntry(string hostNameOrAddress)
		{
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			if (hostNameOrAddress == "0.0.0.0" || hostNameOrAddress == "::0")
			{
				throw new ArgumentException("Addresses 0.0.0.0 (IPv4) and ::0 (IPv6) are unspecified addresses. You cannot use them as target address.", "hostNameOrAddress");
			}
			IPAddress address;
			if (hostNameOrAddress.Length > 0 && IPAddress.TryParse(hostNameOrAddress, out address))
			{
				return Dns.GetHostEntry(address);
			}
			return Dns.GetHostByName(hostNameOrAddress);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000040B4 File Offset: 0x000022B4
		public static IPHostEntry GetHostEntry(IPAddress address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return Dns.GetHostByAddressFromString(address.ToString(), false);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000040D4 File Offset: 0x000022D4
		public static IPAddress[] GetHostAddresses(string hostNameOrAddress)
		{
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			if (hostNameOrAddress == "0.0.0.0" || hostNameOrAddress == "::0")
			{
				throw new ArgumentException("Addresses 0.0.0.0 (IPv4) and ::0 (IPv6) are unspecified addresses. You cannot use them as target address.", "hostNameOrAddress");
			}
			IPAddress ipaddress;
			if (hostNameOrAddress.Length > 0 && IPAddress.TryParse(hostNameOrAddress, out ipaddress))
			{
				return new IPAddress[]
				{
					ipaddress
				};
			}
			return Dns.GetHostEntry(hostNameOrAddress).AddressList;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004154 File Offset: 0x00002354
		[Obsolete("Use GetHostEntry instead")]
		public static IPHostEntry GetHostByName(string hostName)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			string h_name;
			string[] h_aliases;
			string[] h_addrlist;
			if (!Dns.GetHostByName_internal(hostName, out h_name, out h_aliases, out h_addrlist))
			{
				throw new System.Net.Sockets.SocketException(11001);
			}
			return Dns.hostent_to_IPHostEntry(h_name, h_aliases, h_addrlist);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004198 File Offset: 0x00002398
		public static string GetHostName()
		{
			string result;
			if (!Dns.GetHostName_internal(out result))
			{
				throw new System.Net.Sockets.SocketException(11001);
			}
			return result;
		}
	}
}
