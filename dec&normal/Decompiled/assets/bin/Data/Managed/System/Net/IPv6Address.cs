using System;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace System.Net
{
	// Token: 0x02000045 RID: 69
	[DefaultMember("Item")]
	[Serializable]
	internal class IPv6Address
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00004E08 File Offset: 0x00003008
		public IPv6Address(ushort[] addr)
		{
			if (addr == null)
			{
				throw new ArgumentNullException("addr");
			}
			if (addr.Length != 8)
			{
				throw new ArgumentException("addr");
			}
			this.address = addr;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004E3C File Offset: 0x0000303C
		public IPv6Address(ushort[] addr, int prefixLength) : this(addr)
		{
			if (prefixLength < 0 || prefixLength > 128)
			{
				throw new ArgumentException("prefixLength");
			}
			this.prefixLength = prefixLength;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004E6C File Offset: 0x0000306C
		public IPv6Address(ushort[] addr, int prefixLength, int scopeId) : this(addr, prefixLength)
		{
			this.scopeId = (long)scopeId;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004EA0 File Offset: 0x000030A0
		public static IPv6Address Parse(string ipString)
		{
			if (ipString == null)
			{
				throw new ArgumentNullException("ipString");
			}
			IPv6Address result;
			if (IPv6Address.TryParse(ipString, out result))
			{
				return result;
			}
			throw new FormatException("Not a valid IPv6 address");
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004ED8 File Offset: 0x000030D8
		private static int Fill(ushort[] addr, string ipString)
		{
			int num = 0;
			int num2 = 0;
			if (ipString.Length == 0)
			{
				return 0;
			}
			if (ipString.IndexOf("::") != -1)
			{
				return -1;
			}
			for (int i = 0; i < ipString.Length; i++)
			{
				char c = ipString[i];
				if (c == ':')
				{
					if (i == ipString.Length - 1)
					{
						return -1;
					}
					if (num2 == 8)
					{
						return -1;
					}
					addr[num2++] = (ushort)num;
					num = 0;
				}
				else
				{
					int num3;
					if ('0' <= c && c <= '9')
					{
						num3 = (int)(c - '0');
					}
					else if ('a' <= c && c <= 'f')
					{
						num3 = (int)(c - 'a' + '\n');
					}
					else
					{
						if ('A' > c || c > 'F')
						{
							return -1;
						}
						num3 = (int)(c - 'A' + '\n');
					}
					num = (num << 4) + num3;
					if (num > 65535)
					{
						return -1;
					}
				}
			}
			if (num2 == 8)
			{
				return -1;
			}
			addr[num2++] = (ushort)num;
			return num2;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004FDC File Offset: 0x000031DC
		private static bool TryParse(string prefix, out int res)
		{
			return int.TryParse(prefix, NumberStyles.Integer, CultureInfo.InvariantCulture, out res);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004FEC File Offset: 0x000031EC
		public static bool TryParse(string ipString, out IPv6Address result)
		{
			result = null;
			if (ipString == null)
			{
				return false;
			}
			if (ipString.Length > 2 && ipString[0] == '[' && ipString[ipString.Length - 1] == ']')
			{
				ipString = ipString.Substring(1, ipString.Length - 2);
			}
			if (ipString.Length < 2)
			{
				return false;
			}
			int num = 0;
			int num2 = 0;
			int num3 = ipString.LastIndexOf('/');
			if (num3 != -1)
			{
				string prefix = ipString.Substring(num3 + 1);
				if (!IPv6Address.TryParse(prefix, out num))
				{
					num = -1;
				}
				if (num < 0 || num > 128)
				{
					return false;
				}
				ipString = ipString.Substring(0, num3);
			}
			else
			{
				num3 = ipString.LastIndexOf('%');
				if (num3 != -1)
				{
					string prefix2 = ipString.Substring(num3 + 1);
					if (!IPv6Address.TryParse(prefix2, out num2))
					{
						num2 = 0;
					}
					ipString = ipString.Substring(0, num3);
				}
			}
			ushort[] array = new ushort[8];
			bool flag = false;
			int num4 = ipString.LastIndexOf(':');
			if (num4 == -1)
			{
				return false;
			}
			int num5 = 0;
			if (num4 < ipString.Length - 1)
			{
				string text = ipString.Substring(num4 + 1);
				if (text.IndexOf('.') != -1)
				{
					IPAddress ipaddress;
					if (!IPAddress.TryParse(text, out ipaddress))
					{
						return false;
					}
					long internalIPv4Address = ipaddress.InternalIPv4Address;
					array[6] = (ushort)(((int)(internalIPv4Address & 255L) << 8) + (int)(internalIPv4Address >> 8 & 255L));
					array[7] = (ushort)(((int)(internalIPv4Address >> 16 & 255L) << 8) + (int)(internalIPv4Address >> 24 & 255L));
					if (num4 > 0 && ipString[num4 - 1] == ':')
					{
						ipString = ipString.Substring(0, num4 + 1);
					}
					else
					{
						ipString = ipString.Substring(0, num4);
					}
					flag = true;
					num5 = 2;
				}
			}
			int num6 = ipString.IndexOf("::");
			if (num6 != -1)
			{
				int num7 = IPv6Address.Fill(array, ipString.Substring(num6 + 2));
				if (num7 == -1)
				{
					return false;
				}
				if (num7 + num5 > 8)
				{
					return false;
				}
				int num8 = 8 - num5 - num7;
				for (int i = num7; i > 0; i--)
				{
					array[i + num8 - 1] = array[i - 1];
					array[i - 1] = 0;
				}
				int num9 = IPv6Address.Fill(array, ipString.Substring(0, num6));
				if (num9 == -1)
				{
					return false;
				}
				if (num9 + num7 + num5 > 7)
				{
					return false;
				}
			}
			else if (IPv6Address.Fill(array, ipString) != 8 - num5)
			{
				return false;
			}
			bool flag2 = false;
			for (int j = 0; j < num5; j++)
			{
				if (array[j] != 0 || (j == 5 && array[j] != 65535))
				{
					flag2 = true;
				}
			}
			if (flag && !flag2)
			{
				for (int k = 0; k < 5; k++)
				{
					if (array[k] != 0)
					{
						return false;
					}
				}
				if (array[5] != 0 && array[5] != 65535)
				{
					return false;
				}
			}
			result = new IPv6Address(array, num, num2);
			return true;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005308 File Offset: 0x00003508
		public ushort[] Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005310 File Offset: 0x00003510
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00005318 File Offset: 0x00003518
		public long ScopeId
		{
			get
			{
				return this.scopeId;
			}
			set
			{
				this.scopeId = value;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005324 File Offset: 0x00003524
		private static ushort SwapUShort(ushort number)
		{
			return (ushort)((number >> 8 & 255) + ((int)number << 8 & 65280));
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000533C File Offset: 0x0000353C
		private int AsIPv4Int()
		{
			return ((int)IPv6Address.SwapUShort(this.address[7]) << 16) + (int)IPv6Address.SwapUShort(this.address[6]);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000535C File Offset: 0x0000355C
		public bool IsIPv4Compatible()
		{
			for (int i = 0; i < 6; i++)
			{
				if (this.address[i] != 0)
				{
					return false;
				}
			}
			return this.AsIPv4Int() > 1;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005394 File Offset: 0x00003594
		public bool IsIPv4Mapped()
		{
			for (int i = 0; i < 5; i++)
			{
				if (this.address[i] != 0)
				{
					return false;
				}
			}
			return this.address[5] == ushort.MaxValue;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000053D4 File Offset: 0x000035D4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.IsIPv4Compatible() || this.IsIPv4Mapped())
			{
				stringBuilder.Append("::");
				if (this.IsIPv4Mapped())
				{
					stringBuilder.Append("ffff:");
				}
				stringBuilder.Append(new IPAddress((long)this.AsIPv4Int()).ToString());
				return stringBuilder.ToString();
			}
			int num = -1;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < 8; i++)
			{
				if (this.address[i] != 0)
				{
					if (num3 > num2 && num3 > 1)
					{
						num2 = num3;
						num = i - num3;
					}
					num3 = 0;
				}
				else
				{
					num3++;
				}
			}
			if (num3 > num2 && num3 > 1)
			{
				num2 = num3;
				num = 8 - num3;
			}
			if (num == 0)
			{
				stringBuilder.Append(":");
			}
			for (int j = 0; j < 8; j++)
			{
				if (j == num)
				{
					stringBuilder.Append(":");
					j += num2 - 1;
				}
				else
				{
					stringBuilder.AppendFormat("{0:x}", this.address[j]);
					if (j < 7)
					{
						stringBuilder.Append(':');
					}
				}
			}
			if (this.scopeId != 0L)
			{
				stringBuilder.Append('%').Append(this.scopeId);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005538 File Offset: 0x00003738
		public string ToString(bool fullLength)
		{
			if (!fullLength)
			{
				return this.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.address.Length - 1; i++)
			{
				stringBuilder.AppendFormat("{0:X4}:", this.address[i]);
			}
			stringBuilder.AppendFormat("{0:X4}", this.address[this.address.Length - 1]);
			return stringBuilder.ToString();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000055B4 File Offset: 0x000037B4
		public override bool Equals(object other)
		{
			IPv6Address pv6Address = other as IPv6Address;
			if (pv6Address != null)
			{
				for (int i = 0; i < 8; i++)
				{
					if (this.address[i] != pv6Address.address[i])
					{
						return false;
					}
				}
				return true;
			}
			IPAddress ipaddress = other as IPAddress;
			if (ipaddress == null)
			{
				return false;
			}
			for (int j = 0; j < 5; j++)
			{
				if (this.address[j] != 0)
				{
					return false;
				}
			}
			if (this.address[5] != 0 && this.address[5] != 65535)
			{
				return false;
			}
			long internalIPv4Address = ipaddress.InternalIPv4Address;
			return this.address[6] == (ushort)(((int)(internalIPv4Address & 255L) << 8) + (int)(internalIPv4Address >> 8 & 255L)) && this.address[7] == (ushort)(((int)(internalIPv4Address >> 16 & 255L) << 8) + (int)(internalIPv4Address >> 24 & 255L));
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000056A8 File Offset: 0x000038A8
		public override int GetHashCode()
		{
			return IPv6Address.Hash(((int)this.address[0] << 16) + (int)this.address[1], ((int)this.address[2] << 16) + (int)this.address[3], ((int)this.address[4] << 16) + (int)this.address[5], ((int)this.address[6] << 16) + (int)this.address[7]);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000570C File Offset: 0x0000390C
		private static int Hash(int i, int j, int k, int l)
		{
			return i ^ (j << 13 | j >> 19) ^ (k << 26 | k >> 6) ^ (l << 7 | l >> 25);
		}

		// Token: 0x040007DD RID: 2013
		private ushort[] address;

		// Token: 0x040007DE RID: 2014
		private int prefixLength;

		// Token: 0x040007DF RID: 2015
		private long scopeId;

		// Token: 0x040007E0 RID: 2016
		public static readonly IPv6Address Loopback = IPv6Address.Parse("::1");

		// Token: 0x040007E1 RID: 2017
		public static readonly IPv6Address Unspecified = IPv6Address.Parse("::");
	}
}
