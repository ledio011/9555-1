using System;
using System.Globalization;
using System.Net.Sockets;

namespace System.Net
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	public class IPAddress
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x000041E8 File Offset: 0x000023E8
		public IPAddress(long addr)
		{
			this.m_Address = addr;
			this.m_Family = System.Net.Sockets.AddressFamily.InterNetwork;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004200 File Offset: 0x00002400
		internal IPAddress(ushort[] address, long scopeId)
		{
			this.m_Numbers = address;
			for (int i = 0; i < 8; i++)
			{
				this.m_Numbers[i] = (ushort)IPAddress.HostToNetworkOrder((short)this.m_Numbers[i]);
			}
			this.m_Family = System.Net.Sockets.AddressFamily.InterNetworkV6;
			this.m_ScopeId = scopeId;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000042C8 File Offset: 0x000024C8
		private static short SwapShort(short number)
		{
			return (short)((number >> 8 & 255) | ((int)number << 8 & 65280));
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000042E0 File Offset: 0x000024E0
		public static short HostToNetworkOrder(short host)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return host;
			}
			return IPAddress.SwapShort(host);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000042F4 File Offset: 0x000024F4
		public static short NetworkToHostOrder(short network)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return network;
			}
			return IPAddress.SwapShort(network);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004308 File Offset: 0x00002508
		public static IPAddress Parse(string ipString)
		{
			IPAddress result;
			if (IPAddress.TryParse(ipString, out result))
			{
				return result;
			}
			throw new FormatException("An invalid IP address was specified.");
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004330 File Offset: 0x00002530
		public static bool TryParse(string ipString, out IPAddress address)
		{
			if (ipString == null)
			{
				throw new ArgumentNullException("ipString");
			}
			IPAddress ipaddress;
			address = (ipaddress = IPAddress.ParseIPV4(ipString));
			if (ipaddress == null)
			{
				address = (ipaddress = IPAddress.ParseIPV6(ipString));
				if (ipaddress == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004374 File Offset: 0x00002574
		private static IPAddress ParseIPV4(string ip)
		{
			int num = ip.IndexOf(' ');
			if (num != -1)
			{
				string[] array = ip.Substring(num + 1).Split(new char[]
				{
					'.'
				});
				if (array.Length > 0)
				{
					string text = array[array.Length - 1];
					if (text.Length == 0)
					{
						return null;
					}
					foreach (char digit in text.ToCharArray())
					{
						if (!System.Uri.IsHexDigit(digit))
						{
							return null;
						}
					}
				}
				ip = ip.Substring(0, num);
			}
			if (ip.Length == 0 || ip[ip.Length - 1] == '.')
			{
				return null;
			}
			string[] array3 = ip.Split(new char[]
			{
				'.'
			});
			if (array3.Length > 4)
			{
				return null;
			}
			IPAddress result;
			try
			{
				long num2 = 0L;
				long num3 = 0L;
				for (int j = 0; j < array3.Length; j++)
				{
					string text2 = array3[j];
					if (3 <= text2.Length && text2.Length <= 4 && text2[0] == '0' && (text2[1] == 'x' || text2[1] == 'X'))
					{
						if (text2.Length == 3)
						{
							num3 = (long)((byte)System.Uri.FromHex(text2[2]));
						}
						else
						{
							num3 = (long)((byte)(System.Uri.FromHex(text2[2]) << 4 | System.Uri.FromHex(text2[3])));
						}
					}
					else
					{
						if (text2.Length == 0)
						{
							return null;
						}
						if (text2[0] == '0')
						{
							num3 = 0L;
							for (int k = 1; k < text2.Length; k++)
							{
								if ('0' > text2[k] || text2[k] > '7')
								{
									return null;
								}
								num3 = (num3 << 3) + (long)text2[k] - 48L;
							}
						}
						else if (!long.TryParse(text2, NumberStyles.None, null, out num3))
						{
							return null;
						}
					}
					if (j == array3.Length - 1)
					{
						j = 3;
					}
					else if (num3 > 255L)
					{
						return null;
					}
					int num4 = 0;
					while (num3 > 0L)
					{
						num2 |= (num3 & 255L) << (j - num4 << 3);
						num4++;
						num3 /= 256L;
					}
				}
				result = new IPAddress(num2);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000464C File Offset: 0x0000284C
		private static IPAddress ParseIPV6(string ip)
		{
			IPv6Address pv6Address;
			if (IPv6Address.TryParse(ip, out pv6Address))
			{
				return new IPAddress(pv6Address.Address, pv6Address.ScopeId);
			}
			return null;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000467C File Offset: 0x0000287C
		internal long InternalIPv4Address
		{
			get
			{
				return this.m_Address;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004684 File Offset: 0x00002884
		public long ScopeId
		{
			get
			{
				if (this.m_Family != System.Net.Sockets.AddressFamily.InterNetworkV6)
				{
					throw new Exception("The attempted operation is not supported for the type of object referenced");
				}
				return this.m_ScopeId;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000046A4 File Offset: 0x000028A4
		public byte[] GetAddressBytes()
		{
			if (this.m_Family == System.Net.Sockets.AddressFamily.InterNetworkV6)
			{
				byte[] array = new byte[16];
				Buffer.BlockCopy(this.m_Numbers, 0, array, 0, 16);
				return array;
			}
			return new byte[]
			{
				(byte)(this.m_Address & 255L),
				(byte)(this.m_Address >> 8 & 255L),
				(byte)(this.m_Address >> 16 & 255L),
				(byte)(this.m_Address >> 24)
			};
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004724 File Offset: 0x00002924
		public System.Net.Sockets.AddressFamily AddressFamily
		{
			get
			{
				return this.m_Family;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000472C File Offset: 0x0000292C
		public static bool IsLoopback(IPAddress addr)
		{
			if (addr.m_Family == System.Net.Sockets.AddressFamily.InterNetwork)
			{
				return (addr.m_Address & 255L) == 127L;
			}
			for (int i = 0; i < 6; i++)
			{
				if (addr.m_Numbers[i] != 0)
				{
					return false;
				}
			}
			return IPAddress.NetworkToHostOrder((short)addr.m_Numbers[7]) == 1;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000478C File Offset: 0x0000298C
		public override string ToString()
		{
			if (this.m_Family == System.Net.Sockets.AddressFamily.InterNetwork)
			{
				return IPAddress.ToString(this.m_Address);
			}
			ushort[] array = this.m_Numbers.Clone() as ushort[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (ushort)IPAddress.NetworkToHostOrder((short)array[i]);
			}
			return new IPv6Address(array)
			{
				ScopeId = this.ScopeId
			}.ToString();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000047FC File Offset: 0x000029FC
		private static string ToString(long addr)
		{
			return string.Concat(new string[]
			{
				(addr & 255L).ToString(),
				".",
				(addr >> 8 & 255L).ToString(),
				".",
				(addr >> 16 & 255L).ToString(),
				".",
				(addr >> 24 & 255L).ToString()
			});
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004880 File Offset: 0x00002A80
		public override bool Equals(object other)
		{
			IPAddress ipaddress = other as IPAddress;
			if (ipaddress == null)
			{
				return false;
			}
			if (this.AddressFamily != ipaddress.AddressFamily)
			{
				return false;
			}
			if (this.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
			{
				return this.m_Address == ipaddress.m_Address;
			}
			ushort[] numbers = ipaddress.m_Numbers;
			for (int i = 0; i < 8; i++)
			{
				if (this.m_Numbers[i] != numbers[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000048F8 File Offset: 0x00002AF8
		public override int GetHashCode()
		{
			if (this.m_Family == System.Net.Sockets.AddressFamily.InterNetwork)
			{
				return (int)this.m_Address;
			}
			return IPAddress.Hash(((int)this.m_Numbers[0] << 16) + (int)this.m_Numbers[1], ((int)this.m_Numbers[2] << 16) + (int)this.m_Numbers[3], ((int)this.m_Numbers[4] << 16) + (int)this.m_Numbers[5], ((int)this.m_Numbers[6] << 16) + (int)this.m_Numbers[7]);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004970 File Offset: 0x00002B70
		private static int Hash(int i, int j, int k, int l)
		{
			return i ^ (j << 13 | j >> 19) ^ (k << 26 | k >> 6) ^ (l << 7 | l >> 25);
		}

		// Token: 0x040007CA RID: 1994
		private long m_Address;

		// Token: 0x040007CB RID: 1995
		private System.Net.Sockets.AddressFamily m_Family;

		// Token: 0x040007CC RID: 1996
		private ushort[] m_Numbers;

		// Token: 0x040007CD RID: 1997
		private long m_ScopeId;

		// Token: 0x040007CE RID: 1998
		public static readonly IPAddress Any = new IPAddress(0L);

		// Token: 0x040007CF RID: 1999
		public static readonly IPAddress Broadcast = IPAddress.Parse("255.255.255.255");

		// Token: 0x040007D0 RID: 2000
		public static readonly IPAddress Loopback = IPAddress.Parse("127.0.0.1");

		// Token: 0x040007D1 RID: 2001
		public static readonly IPAddress None = IPAddress.Parse("255.255.255.255");

		// Token: 0x040007D2 RID: 2002
		public static readonly IPAddress IPv6Any = IPAddress.ParseIPV6("::");

		// Token: 0x040007D3 RID: 2003
		public static readonly IPAddress IPv6Loopback = IPAddress.ParseIPV6("::1");

		// Token: 0x040007D4 RID: 2004
		public static readonly IPAddress IPv6None = IPAddress.ParseIPV6("::");

		// Token: 0x040007D5 RID: 2005
		private int m_HashCode;
	}
}
