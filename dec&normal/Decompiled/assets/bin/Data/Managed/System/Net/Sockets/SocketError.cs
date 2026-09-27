using System;

namespace System.Net.Sockets
{
	// Token: 0x02000056 RID: 86
	public enum SocketError
	{
		// Token: 0x0400086F RID: 2159
		AccessDenied = 10013,
		// Token: 0x04000870 RID: 2160
		AddressAlreadyInUse = 10048,
		// Token: 0x04000871 RID: 2161
		AddressFamilyNotSupported = 10047,
		// Token: 0x04000872 RID: 2162
		AddressNotAvailable = 10049,
		// Token: 0x04000873 RID: 2163
		AlreadyInProgress = 10037,
		// Token: 0x04000874 RID: 2164
		ConnectionAborted = 10053,
		// Token: 0x04000875 RID: 2165
		ConnectionRefused = 10061,
		// Token: 0x04000876 RID: 2166
		ConnectionReset = 10054,
		// Token: 0x04000877 RID: 2167
		DestinationAddressRequired = 10039,
		// Token: 0x04000878 RID: 2168
		Disconnecting = 10101,
		// Token: 0x04000879 RID: 2169
		Fault = 10014,
		// Token: 0x0400087A RID: 2170
		HostDown = 10064,
		// Token: 0x0400087B RID: 2171
		HostNotFound = 11001,
		// Token: 0x0400087C RID: 2172
		HostUnreachable = 10065,
		// Token: 0x0400087D RID: 2173
		InProgress = 10036,
		// Token: 0x0400087E RID: 2174
		Interrupted = 10004,
		// Token: 0x0400087F RID: 2175
		InvalidArgument = 10022,
		// Token: 0x04000880 RID: 2176
		IOPending = 997,
		// Token: 0x04000881 RID: 2177
		IsConnected = 10056,
		// Token: 0x04000882 RID: 2178
		MessageSize = 10040,
		// Token: 0x04000883 RID: 2179
		NetworkDown = 10050,
		// Token: 0x04000884 RID: 2180
		NetworkReset = 10052,
		// Token: 0x04000885 RID: 2181
		NetworkUnreachable = 10051,
		// Token: 0x04000886 RID: 2182
		NoBufferSpaceAvailable = 10055,
		// Token: 0x04000887 RID: 2183
		NoData = 11004,
		// Token: 0x04000888 RID: 2184
		NoRecovery = 11003,
		// Token: 0x04000889 RID: 2185
		NotConnected = 10057,
		// Token: 0x0400088A RID: 2186
		NotInitialized = 10093,
		// Token: 0x0400088B RID: 2187
		NotSocket = 10038,
		// Token: 0x0400088C RID: 2188
		OperationAborted = 995,
		// Token: 0x0400088D RID: 2189
		OperationNotSupported = 10045,
		// Token: 0x0400088E RID: 2190
		ProcessLimit = 10067,
		// Token: 0x0400088F RID: 2191
		ProtocolFamilyNotSupported = 10046,
		// Token: 0x04000890 RID: 2192
		ProtocolNotSupported = 10043,
		// Token: 0x04000891 RID: 2193
		ProtocolOption = 10042,
		// Token: 0x04000892 RID: 2194
		ProtocolType = 10041,
		// Token: 0x04000893 RID: 2195
		Shutdown = 10058,
		// Token: 0x04000894 RID: 2196
		SocketError = -1,
		// Token: 0x04000895 RID: 2197
		SocketNotSupported = 10044,
		// Token: 0x04000896 RID: 2198
		Success = 0,
		// Token: 0x04000897 RID: 2199
		SystemNotReady = 10091,
		// Token: 0x04000898 RID: 2200
		TimedOut = 10060,
		// Token: 0x04000899 RID: 2201
		TooManyOpenSockets = 10024,
		// Token: 0x0400089A RID: 2202
		TryAgain = 11002,
		// Token: 0x0400089B RID: 2203
		TypeNotFound = 10109,
		// Token: 0x0400089C RID: 2204
		VersionNotSupported = 10092,
		// Token: 0x0400089D RID: 2205
		WouldBlock = 10035
	}
}
