using System;

namespace System.Net.Sockets
{
	// Token: 0x02000053 RID: 83
	public enum ProtocolType
	{
		// Token: 0x0400083A RID: 2106
		IP,
		// Token: 0x0400083B RID: 2107
		Icmp,
		// Token: 0x0400083C RID: 2108
		Igmp,
		// Token: 0x0400083D RID: 2109
		Ggp,
		// Token: 0x0400083E RID: 2110
		Tcp = 6,
		// Token: 0x0400083F RID: 2111
		Pup = 12,
		// Token: 0x04000840 RID: 2112
		Udp = 17,
		// Token: 0x04000841 RID: 2113
		Idp = 22,
		// Token: 0x04000842 RID: 2114
		IPv6 = 41,
		// Token: 0x04000843 RID: 2115
		ND = 77,
		// Token: 0x04000844 RID: 2116
		Raw = 255,
		// Token: 0x04000845 RID: 2117
		Unspecified = 0,
		// Token: 0x04000846 RID: 2118
		Ipx = 1000,
		// Token: 0x04000847 RID: 2119
		Spx = 1256,
		// Token: 0x04000848 RID: 2120
		SpxII,
		// Token: 0x04000849 RID: 2121
		Unknown = -1,
		// Token: 0x0400084A RID: 2122
		IPv4 = 4,
		// Token: 0x0400084B RID: 2123
		IPv6RoutingHeader = 43,
		// Token: 0x0400084C RID: 2124
		IPv6FragmentHeader,
		// Token: 0x0400084D RID: 2125
		IPSecEncapsulatingSecurityPayload = 50,
		// Token: 0x0400084E RID: 2126
		IPSecAuthenticationHeader,
		// Token: 0x0400084F RID: 2127
		IcmpV6 = 58,
		// Token: 0x04000850 RID: 2128
		IPv6NoNextHeader,
		// Token: 0x04000851 RID: 2129
		IPv6DestinationOptions,
		// Token: 0x04000852 RID: 2130
		IPv6HopByHopOptions = 0
	}
}
