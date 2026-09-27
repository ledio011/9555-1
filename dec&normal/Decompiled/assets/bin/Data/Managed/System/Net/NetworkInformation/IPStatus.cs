using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000046 RID: 70
	public enum IPStatus
	{
		// Token: 0x040007E3 RID: 2019
		Unknown = -1,
		// Token: 0x040007E4 RID: 2020
		Success,
		// Token: 0x040007E5 RID: 2021
		DestinationNetworkUnreachable = 11002,
		// Token: 0x040007E6 RID: 2022
		DestinationHostUnreachable,
		// Token: 0x040007E7 RID: 2023
		DestinationProhibited,
		// Token: 0x040007E8 RID: 2024
		DestinationProtocolUnreachable = 11004,
		// Token: 0x040007E9 RID: 2025
		DestinationPortUnreachable,
		// Token: 0x040007EA RID: 2026
		NoResources,
		// Token: 0x040007EB RID: 2027
		BadOption,
		// Token: 0x040007EC RID: 2028
		HardwareError,
		// Token: 0x040007ED RID: 2029
		PacketTooBig,
		// Token: 0x040007EE RID: 2030
		TimedOut,
		// Token: 0x040007EF RID: 2031
		BadRoute = 11012,
		// Token: 0x040007F0 RID: 2032
		TtlExpired,
		// Token: 0x040007F1 RID: 2033
		TtlReassemblyTimeExceeded,
		// Token: 0x040007F2 RID: 2034
		ParameterProblem,
		// Token: 0x040007F3 RID: 2035
		SourceQuench,
		// Token: 0x040007F4 RID: 2036
		BadDestination = 11018,
		// Token: 0x040007F5 RID: 2037
		DestinationUnreachable = 11040,
		// Token: 0x040007F6 RID: 2038
		TimeExceeded,
		// Token: 0x040007F7 RID: 2039
		BadHeader,
		// Token: 0x040007F8 RID: 2040
		UnrecognizedNextHeader,
		// Token: 0x040007F9 RID: 2041
		IcmpError,
		// Token: 0x040007FA RID: 2042
		DestinationScopeMismatch
	}
}
