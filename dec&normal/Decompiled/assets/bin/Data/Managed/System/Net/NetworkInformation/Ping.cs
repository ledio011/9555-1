using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000047 RID: 71
	[MonoTODO("IPv6 support is missing")]
	public class Ping : System.ComponentModel.Component, IDisposable
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00005734 File Offset: 0x00003934
		static Ping()
		{
			if (Environment.OSVersion.Platform == PlatformID.Unix)
			{
				Ping.CheckLinuxCapabilities();
				if (!Ping.canSendPrivileged && WindowsIdentity.GetCurrent().Name == "root")
				{
					Ping.canSendPrivileged = true;
				}
			}
			else
			{
				Ping.canSendPrivileged = true;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005798 File Offset: 0x00003998
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600012C RID: 300
		[DllImport("libc")]
		private static extern int capget(ref Ping.cap_user_header_t header, ref Ping.cap_user_data_t data);

		// Token: 0x0600012D RID: 301 RVA: 0x0000579C File Offset: 0x0000399C
		private static void CheckLinuxCapabilities()
		{
			try
			{
				Ping.cap_user_header_t cap_user_header_t = default(Ping.cap_user_header_t);
				Ping.cap_user_data_t cap_user_data_t = default(Ping.cap_user_data_t);
				cap_user_header_t.version = 537333798U;
				int num = -1;
				try
				{
					num = Ping.capget(ref cap_user_header_t, ref cap_user_data_t);
				}
				catch (Exception)
				{
				}
				if (num != -1)
				{
					Ping.canSendPrivileged = ((cap_user_data_t.effective & 8192U) != 0U);
				}
			}
			catch
			{
				Ping.canSendPrivileged = false;
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005830 File Offset: 0x00003A30
		public PingReply Send(string hostNameOrAddress, int timeout)
		{
			return this.Send(hostNameOrAddress, timeout, Ping.default_buffer);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005840 File Offset: 0x00003A40
		public PingReply Send(string hostNameOrAddress, int timeout, byte[] buffer)
		{
			return this.Send(hostNameOrAddress, timeout, buffer, new PingOptions());
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005850 File Offset: 0x00003A50
		public PingReply Send(string hostNameOrAddress, int timeout, byte[] buffer, PingOptions options)
		{
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostNameOrAddress);
			return this.Send(hostAddresses[0], timeout, buffer, options);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005874 File Offset: 0x00003A74
		private static IPAddress GetNonLoopbackIP()
		{
			foreach (IPAddress ipaddress in Dns.GetHostByName(Dns.GetHostName()).AddressList)
			{
				if (!IPAddress.IsLoopback(ipaddress))
				{
					return ipaddress;
				}
			}
			throw new InvalidOperationException("Could not resolve non-loopback IP address for localhost");
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000058C0 File Offset: 0x00003AC0
		public PingReply Send(IPAddress address, int timeout, byte[] buffer, PingOptions options)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (timeout < 0)
			{
				throw new ArgumentOutOfRangeException("timeout", "timeout must be non-negative integer");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length > 65500)
			{
				throw new ArgumentException("buffer");
			}
			if (Ping.canSendPrivileged)
			{
				return this.SendPrivileged(address, timeout, buffer, options);
			}
			return this.SendUnprivileged(address, timeout, buffer, options);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005940 File Offset: 0x00003B40
		private PingReply SendPrivileged(IPAddress address, int timeout, byte[] buffer, PingOptions options)
		{
			IPEndPoint ipendPoint = new IPEndPoint(address, 0);
			IPEndPoint ipendPoint2 = new IPEndPoint(Ping.GetNonLoopbackIP(), 0);
			PingReply result;
			using (System.Net.Sockets.Socket socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Raw, System.Net.Sockets.ProtocolType.Icmp))
			{
				if (options != null)
				{
					socket.DontFragment = options.DontFragment;
					socket.Ttl = (short)options.Ttl;
				}
				socket.SendTimeout = timeout;
				socket.ReceiveTimeout = timeout;
				Ping.IcmpMessage icmpMessage = new Ping.IcmpMessage(8, 0, 1, 0, buffer);
				byte[] array = icmpMessage.GetBytes();
				socket.SendBufferSize = array.Length;
				socket.SendTo(array, array.Length, System.Net.Sockets.SocketFlags.None, ipendPoint);
				DateTime now = DateTime.Now;
				array = new byte[100];
				int num;
				long num3;
				Ping.IcmpMessage icmpMessage2;
				for (;;)
				{
					EndPoint endPoint = ipendPoint2;
					num = 0;
					int num2 = socket.ReceiveFrom_nochecks_exc(array, 0, 100, System.Net.Sockets.SocketFlags.None, ref endPoint, false, out num);
					if (num != 0)
					{
						break;
					}
					num3 = (long)(DateTime.Now - now).TotalMilliseconds;
					int num4 = (int)(array[0] & 15) << 2;
					int size = num2 - num4;
					if (!((IPEndPoint)endPoint).Address.Equals(ipendPoint.Address))
					{
						long num5 = (long)timeout - num3;
						if (num5 <= 0L)
						{
							goto Block_7;
						}
						socket.ReceiveTimeout = (int)num5;
					}
					else
					{
						icmpMessage2 = new Ping.IcmpMessage(array, num4, size);
						if (icmpMessage2.Identifier == 1 && icmpMessage2.Type != 8)
						{
							goto IL_1C9;
						}
						long num6 = (long)timeout - num3;
						if (num6 <= 0L)
						{
							goto Block_9;
						}
						socket.ReceiveTimeout = (int)num6;
					}
				}
				if (num == 10060)
				{
					return new PingReply(null, new byte[0], options, 0L, IPStatus.TimedOut);
				}
				throw new NotSupportedException(string.Format("Unexpected socket error during ping request: {0}", num));
				Block_7:
				return new PingReply(null, new byte[0], options, 0L, IPStatus.TimedOut);
				Block_9:
				return new PingReply(null, new byte[0], options, 0L, IPStatus.TimedOut);
				IL_1C9:
				result = new PingReply(address, icmpMessage2.Data, options, num3, icmpMessage2.IPStatus);
			}
			return result;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005B6C File Offset: 0x00003D6C
		private PingReply SendUnprivileged(IPAddress address, int timeout, byte[] buffer, PingOptions options)
		{
			DateTime now = DateTime.Now;
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			string arguments = this.BuildPingArgs(address, timeout, options);
			long roundtripTime = 0L;
			process.StartInfo.FileName = "/bin/ping";
			process.StartInfo.Arguments = arguments;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			DateTime utcNow = DateTime.UtcNow;
			try
			{
				process.Start();
				roundtripTime = (long)(DateTime.Now - now).TotalMilliseconds;
				if (!process.WaitForExit(timeout) || (process.HasExited && process.ExitCode == 2))
				{
					return new PingReply(address, buffer, options, roundtripTime, IPStatus.TimedOut);
				}
				if (process.ExitCode == 1)
				{
					return new PingReply(address, buffer, options, roundtripTime, IPStatus.TtlExpired);
				}
			}
			catch (Exception)
			{
				return new PingReply(address, buffer, options, roundtripTime, IPStatus.Unknown);
			}
			finally
			{
				if (process != null)
				{
					if (!process.HasExited)
					{
						process.Kill();
					}
					process.Dispose();
				}
			}
			return new PingReply(address, buffer, options, roundtripTime, IPStatus.Success);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005CC4 File Offset: 0x00003EC4
		private string BuildPingArgs(IPAddress address, int timeout, PingOptions options)
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			StringBuilder stringBuilder = new StringBuilder();
			uint num = Convert.ToUInt32(Math.Floor((double)(timeout + 1000) / 1000.0));
			bool flag = Environment.OSVersion.Platform == PlatformID.MacOSX;
			if (!flag)
			{
				stringBuilder.AppendFormat(invariantCulture, "-q -n -c {0} -w {1} -t {2} -M ", new object[]
				{
					1,
					num,
					options.Ttl
				});
			}
			else
			{
				stringBuilder.AppendFormat(invariantCulture, "-q -n -c {0} -t {1} -o -m {2} ", new object[]
				{
					1,
					num,
					options.Ttl
				});
			}
			if (!flag)
			{
				stringBuilder.Append((!options.DontFragment) ? "dont " : "do ");
			}
			else if (options.DontFragment)
			{
				stringBuilder.Append("-D ");
			}
			stringBuilder.Append(address.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x040007FB RID: 2043
		private const int DefaultCount = 1;

		// Token: 0x040007FC RID: 2044
		private const string PingBinPath = "/bin/ping";

		// Token: 0x040007FD RID: 2045
		private const int default_timeout = 4000;

		// Token: 0x040007FE RID: 2046
		private const int identifier = 1;

		// Token: 0x040007FF RID: 2047
		private const uint linux_cap_version = 537333798U;

		// Token: 0x04000800 RID: 2048
		private static readonly byte[] default_buffer = new byte[0];

		// Token: 0x04000801 RID: 2049
		private static bool canSendPrivileged;

		// Token: 0x04000802 RID: 2050
		private System.ComponentModel.BackgroundWorker worker;

		// Token: 0x04000803 RID: 2051
		private object user_async_state;

		// Token: 0x04000804 RID: 2052
		private PingCompletedEventHandler PingCompleted;

		// Token: 0x02000048 RID: 72
		private struct cap_user_data_t
		{
			// Token: 0x04000805 RID: 2053
			public uint effective;

			// Token: 0x04000806 RID: 2054
			public uint permitted;

			// Token: 0x04000807 RID: 2055
			public uint inheritable;
		}

		// Token: 0x02000049 RID: 73
		private struct cap_user_header_t
		{
			// Token: 0x04000808 RID: 2056
			public uint version;

			// Token: 0x04000809 RID: 2057
			public int pid;
		}

		// Token: 0x0200004A RID: 74
		private class IcmpMessage
		{
			// Token: 0x06000136 RID: 310 RVA: 0x00005DD0 File Offset: 0x00003FD0
			public IcmpMessage(byte[] bytes, int offset, int size)
			{
				this.bytes = new byte[size];
				Buffer.BlockCopy(bytes, offset, this.bytes, 0, size);
			}

			// Token: 0x06000137 RID: 311 RVA: 0x00005DF4 File Offset: 0x00003FF4
			public IcmpMessage(byte type, byte code, short identifier, short sequence, byte[] data)
			{
				this.bytes = new byte[data.Length + 8];
				this.bytes[0] = type;
				this.bytes[1] = code;
				this.bytes[4] = (byte)(identifier & 255);
				this.bytes[5] = (byte)(identifier >> 8);
				this.bytes[6] = (byte)(sequence & 255);
				this.bytes[7] = (byte)(sequence >> 8);
				Buffer.BlockCopy(data, 0, this.bytes, 8, data.Length);
				ushort num = Ping.IcmpMessage.ComputeChecksum(this.bytes);
				this.bytes[2] = (byte)(num & 255);
				this.bytes[3] = (byte)(num >> 8);
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x06000138 RID: 312 RVA: 0x00005EA0 File Offset: 0x000040A0
			public byte Type
			{
				get
				{
					return this.bytes[0];
				}
			}

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x06000139 RID: 313 RVA: 0x00005EAC File Offset: 0x000040AC
			public byte Code
			{
				get
				{
					return this.bytes[1];
				}
			}

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x0600013A RID: 314 RVA: 0x00005EB8 File Offset: 0x000040B8
			public byte Identifier
			{
				get
				{
					return (byte)((int)this.bytes[4] + ((int)this.bytes[5] << 8));
				}
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x0600013B RID: 315 RVA: 0x00005ED0 File Offset: 0x000040D0
			public byte[] Data
			{
				get
				{
					byte[] array = new byte[this.bytes.Length - 8];
					Buffer.BlockCopy(this.bytes, 0, array, 0, array.Length);
					return array;
				}
			}

			// Token: 0x0600013C RID: 316 RVA: 0x00005F00 File Offset: 0x00004100
			public byte[] GetBytes()
			{
				return this.bytes;
			}

			// Token: 0x0600013D RID: 317 RVA: 0x00005F08 File Offset: 0x00004108
			private static ushort ComputeChecksum(byte[] data)
			{
				uint num = 0U;
				for (int i = 0; i < data.Length; i += 2)
				{
					ushort num2 = (ushort)((i + 1 >= data.Length) ? 0 : data[i + 1]);
					num2 = (ushort)(num2 << 8);
					num2 += (ushort)data[i];
					num += (uint)num2;
				}
				num = (num >> 16) + (num & 65535U);
				return (ushort)(~(ushort)num);
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x0600013E RID: 318 RVA: 0x00005F64 File Offset: 0x00004164
			public IPStatus IPStatus
			{
				get
				{
					byte type = this.Type;
					switch (type)
					{
					case 0:
						return IPStatus.Success;
					default:
						switch (type)
						{
						case 8:
							return IPStatus.Success;
						case 11:
						{
							byte code = this.Code;
							if (code == 0)
							{
								return IPStatus.TimeExceeded;
							}
							if (code == 1)
							{
								return IPStatus.TtlReassemblyTimeExceeded;
							}
							break;
						}
						case 12:
							return IPStatus.ParameterProblem;
						}
						break;
					case 3:
						switch (this.Code)
						{
						case 0:
							return IPStatus.DestinationNetworkUnreachable;
						case 1:
							return IPStatus.DestinationHostUnreachable;
						case 2:
							return IPStatus.DestinationProhibited;
						case 3:
							return IPStatus.DestinationPortUnreachable;
						case 4:
							return IPStatus.BadOption;
						case 5:
							return IPStatus.BadRoute;
						}
						break;
					case 4:
						return IPStatus.SourceQuench;
					}
					return IPStatus.Unknown;
				}
			}

			// Token: 0x0400080A RID: 2058
			private byte[] bytes;
		}
	}
}
