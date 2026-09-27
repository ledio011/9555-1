using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x02000055 RID: 85
	public class Socket : IDisposable
	{
		// Token: 0x06000152 RID: 338 RVA: 0x00006274 File Offset: 0x00004474
		public Socket(AddressFamily family, SocketType type, ProtocolType proto)
		{
			this.readQ = new Queue(2);
			this.writeQ = new Queue(2);
			this.MinListenPort = 7100;
			this.MaxListenPort = 7150;
			base..ctor();
			if (family == AddressFamily.Unspecified)
			{
				throw new ArgumentException("family");
			}
			this.address_family = family;
			this.socket_type = type;
			this.protocol_type = proto;
			int num;
			this.socket = this.Socket_internal(family, type, proto, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006308 File Offset: 0x00004508
		static Socket()
		{
			Socket.CheckProtocolSupport();
		}

		// Token: 0x06000154 RID: 340
		[MethodImpl(4096)]
		private static extern int Available_internal(IntPtr socket, out int error);

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000631C File Offset: 0x0000451C
		public int Available
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				int num;
				int result = Socket.Available_internal(this.socket, out num);
				if (num != 0)
				{
					throw new SocketException(num);
				}
				return result;
			}
		}

		// Token: 0x17000056 RID: 86
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000636C File Offset: 0x0000456C
		public bool DontFragment
		{
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.address_family == AddressFamily.InterNetwork)
				{
					this.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DontFragment, (!value) ? 0 : 1);
				}
				else
				{
					if (this.address_family != AddressFamily.InterNetworkV6)
					{
						throw new NotSupportedException("This property is only valid for InterNetwork and InterNetworkV6 sockets");
					}
					this.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DontFragment, (!value) ? 0 : 1);
				}
			}
		}

		// Token: 0x17000057 RID: 87
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000063FC File Offset: 0x000045FC
		public int SendTimeout
		{
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value", "The value specified for a set operation is less than -1");
				}
				if (value == -1)
				{
					value = 0;
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, value);
			}
		}

		// Token: 0x17000058 RID: 88
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00006464 File Offset: 0x00004664
		public int ReceiveTimeout
		{
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value", "The value specified for a set operation is less than -1");
				}
				if (value == -1)
				{
					value = 0;
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, value);
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000064CC File Offset: 0x000046CC
		public bool Poll(int time_us, SelectMode mode)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (mode != SelectMode.SelectRead && mode != SelectMode.SelectWrite && mode != SelectMode.SelectError)
			{
				throw new NotSupportedException("'mode' parameter is not valid.");
			}
			int num;
			bool flag = Socket.Poll_internal(this.socket, mode, time_us, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			if (mode == SelectMode.SelectWrite && flag && !this.connected && (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Error) == 0)
			{
				this.connected = true;
			}
			return flag;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006578 File Offset: 0x00004778
		public int Receive(byte[] buffer, int size, SocketFlags flags)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (size < 0 || size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			SocketError socketError;
			int result = this.Receive_nochecks(buffer, 0, size, flags, out socketError);
			if (socketError == SocketError.Success)
			{
				return result;
			}
			if (socketError == SocketError.WouldBlock && this.blocking)
			{
				throw new SocketException((int)socketError, "Operation timed out.");
			}
			throw new SocketException((int)socketError);
		}

		// Token: 0x0600015B RID: 347
		[MethodImpl(4096)]
		private static extern int RecvFrom_internal(IntPtr sock, byte[] buffer, int offset, int count, SocketFlags flags, ref SocketAddress sockaddr, out int error);

		// Token: 0x0600015C RID: 348 RVA: 0x00006618 File Offset: 0x00004818
		internal int ReceiveFrom_nochecks_exc(byte[] buf, int offset, int size, SocketFlags flags, ref EndPoint remote_end, bool throwOnError, out int error)
		{
			SocketAddress socketAddress = remote_end.Serialize();
			int result = Socket.RecvFrom_internal(this.socket, buf, offset, size, flags, ref socketAddress, out error);
			SocketError socketError = (SocketError)error;
			if (socketError != SocketError.Success)
			{
				if (socketError != SocketError.WouldBlock && socketError != SocketError.InProgress)
				{
					this.connected = false;
				}
				else if (socketError == SocketError.WouldBlock && this.blocking)
				{
					if (throwOnError)
					{
						throw new SocketException(10060, "Operation timed out");
					}
					error = 10060;
					return 0;
				}
				if (throwOnError)
				{
					throw new SocketException(error);
				}
				return 0;
			}
			else
			{
				if (Environment.SocketSecurityEnabled && !Socket.CheckEndPoint(socketAddress))
				{
					buf.Initialize();
					throw new SecurityException("Unable to connect, as no valid crossdomain policy was found");
				}
				this.connected = true;
				this.isbound = true;
				if (socketAddress != null)
				{
					remote_end = remote_end.Create(socketAddress);
				}
				this.seed_endpoint = remote_end;
				return result;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006708 File Offset: 0x00004908
		public int Send(byte[] buf, int size, SocketFlags flags)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buf == null)
			{
				throw new ArgumentNullException("buf");
			}
			if (size < 0 || size > buf.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			SocketError socketError;
			int result = this.Send_nochecks(buf, 0, size, flags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException((int)socketError);
			}
			return result;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006784 File Offset: 0x00004984
		public int SendTo(byte[] buffer, int size, SocketFlags flags, EndPoint remote_end)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remote_end == null)
			{
				throw new ArgumentNullException("remote_end");
			}
			if (size < 0 || size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return this.SendTo_nochecks(buffer, 0, size, flags, remote_end);
		}

		// Token: 0x0600015F RID: 351
		[MethodImpl(4096)]
		private static extern int SendTo_internal_real(IntPtr sock, byte[] buffer, int offset, int count, SocketFlags flags, SocketAddress sa, out int error);

		// Token: 0x06000160 RID: 352 RVA: 0x00006804 File Offset: 0x00004A04
		private static int SendTo_internal(IntPtr sock, byte[] buffer, int offset, int count, SocketFlags flags, SocketAddress sa, out int error)
		{
			if (Environment.SocketSecurityEnabled && !Socket.CheckEndPoint(sa))
			{
				SecurityException ex = new SecurityException("SendTo request refused by Unity webplayer security model");
				Console.WriteLine("Throwing the following security exception: " + ex);
				throw ex;
			}
			return Socket.SendTo_internal_real(sock, buffer, offset, count, flags, sa, out error);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006858 File Offset: 0x00004A58
		internal int SendTo_nochecks(byte[] buffer, int offset, int size, SocketFlags flags, EndPoint remote_end)
		{
			SocketAddress sa = remote_end.Serialize();
			int num;
			int result = Socket.SendTo_internal(this.socket, buffer, offset, size, flags, sa, out num);
			SocketError socketError = (SocketError)num;
			if (socketError != SocketError.Success)
			{
				if (socketError != SocketError.WouldBlock && socketError != SocketError.InProgress)
				{
					this.connected = false;
				}
				throw new SocketException(num);
			}
			this.connected = true;
			this.isbound = true;
			this.seed_endpoint = remote_end;
			return result;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000068C4 File Offset: 0x00004AC4
		internal static void CheckProtocolSupport()
		{
			if (Socket.ipv4Supported == -1)
			{
				try
				{
					Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					socket.Close();
					Socket.ipv4Supported = 1;
				}
				catch
				{
					Socket.ipv4Supported = 0;
				}
			}
			if (Socket.ipv6Supported == -1 && Socket.ipv6Supported != 0)
			{
				try
				{
					Socket socket2 = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
					socket2.Close();
					Socket.ipv6Supported = 1;
				}
				catch
				{
					Socket.ipv6Supported = 0;
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000695C File Offset: 0x00004B5C
		public static bool SupportsIPv4
		{
			get
			{
				Socket.CheckProtocolSupport();
				return Socket.ipv4Supported == 1;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000696C File Offset: 0x00004B6C
		[Obsolete("Use OSSupportsIPv6 instead")]
		public static bool SupportsIPv6
		{
			get
			{
				Socket.CheckProtocolSupport();
				return Socket.ipv6Supported == 1;
			}
		}

		// Token: 0x06000165 RID: 357
		[MethodImpl(4096)]
		private extern IntPtr Socket_internal(AddressFamily family, SocketType type, ProtocolType proto, out int error);

		// Token: 0x06000166 RID: 358 RVA: 0x0000697C File Offset: 0x00004B7C
		~Socket()
		{
			this.Dispose(false);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000069AC File Offset: 0x00004BAC
		public bool Connected
		{
			get
			{
				return this.connected;
			}
		}

		// Token: 0x1700005C RID: 92
		// (set) Token: 0x06000168 RID: 360 RVA: 0x000069B4 File Offset: 0x00004BB4
		public int SendBufferSize
		{
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "The value specified for a set operation is less than zero");
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, value);
			}
		}

		// Token: 0x1700005D RID: 93
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00006A10 File Offset: 0x00004C10
		public short Ttl
		{
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.address_family == AddressFamily.InterNetwork)
				{
					this.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, (int)value);
				}
				else
				{
					if (this.address_family != AddressFamily.InterNetworkV6)
					{
						throw new NotSupportedException("This property is only valid for InterNetwork and InterNetworkV6 sockets");
					}
					this.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.HopLimit, (int)value);
				}
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006A88 File Offset: 0x00004C88
		private void Linger(IntPtr handle)
		{
			if (!this.connected || this.linger_timeout <= 0)
			{
				return;
			}
			int num;
			Socket.Shutdown_internal(handle, SocketShutdown.Receive, out num);
			if (num != 0)
			{
				return;
			}
			int num2 = this.linger_timeout / 1000;
			int num3 = this.linger_timeout % 1000;
			if (num3 > 0)
			{
				Socket.Poll_internal(handle, SelectMode.SelectRead, num3 * 1000, out num);
				if (num != 0)
				{
					return;
				}
			}
			if (num2 > 0)
			{
				LingerOption obj_val = new LingerOption(true, num2);
				Socket.SetSocketOption_internal(handle, SocketOptionLevel.Socket, SocketOptionName.Linger, obj_val, null, 0, out num);
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006B1C File Offset: 0x00004D1C
		protected virtual void Dispose(bool explicitDisposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			bool flag = this.connected;
			this.connected = false;
			if ((int)this.socket != -1)
			{
				if (Environment.SocketSecurityEnabled && Socket.current_bind_count > 0)
				{
					Socket.current_bind_count--;
				}
				this.closed = true;
				IntPtr handle = this.socket;
				this.socket = (IntPtr)(-1);
				Thread thread = this.blocking_thread;
				if (thread != null)
				{
					thread.Abort();
					this.blocking_thread = null;
				}
				if (flag)
				{
					this.Linger(handle);
				}
				int num;
				Socket.Close_internal(handle, out num);
				if (num != 0)
				{
					throw new SocketException(num);
				}
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006BD4 File Offset: 0x00004DD4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600016D RID: 365
		[MethodImpl(4096)]
		private static extern void Close_internal(IntPtr socket, out int error);

		// Token: 0x0600016E RID: 366 RVA: 0x00006BE4 File Offset: 0x00004DE4
		public void Close()
		{
			this.linger_timeout = 0;
			((IDisposable)this).Dispose();
		}

		// Token: 0x0600016F RID: 367
		[MethodImpl(4096)]
		private static extern void Connect_internal_real(IntPtr sock, SocketAddress sa, out int error);

		// Token: 0x06000170 RID: 368 RVA: 0x00006BF4 File Offset: 0x00004DF4
		private static void Connect_internal(IntPtr sock, SocketAddress sa, out int error, bool requireSocketPolicyFile)
		{
			if (requireSocketPolicyFile && !Socket.CheckEndPoint(sa))
			{
				throw new SecurityException("Unable to connect, as no valid crossdomain policy was found");
			}
			Socket.Connect_internal_real(sock, sa, out error);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006C1C File Offset: 0x00004E1C
		internal static bool CheckEndPoint(SocketAddress sa)
		{
			if (!Environment.SocketSecurityEnabled)
			{
				return true;
			}
			bool result;
			try
			{
				IPEndPoint ipendPoint = new IPEndPoint(IPAddress.Loopback, 123);
				IPEndPoint ipendPoint2 = (IPEndPoint)ipendPoint.Create(sa);
				if (Socket.check_socket_policy == null)
				{
					Socket.check_socket_policy = Socket.GetUnityCrossDomainHelperMethod("CheckSocketEndPoint");
				}
				result = (bool)Socket.check_socket_policy.Invoke(null, new object[]
				{
					ipendPoint2.Address.ToString(),
					ipendPoint2.Port
				});
			}
			catch (Exception arg)
			{
				Console.WriteLine("Unexpected error while trying to CheckEndPoint() : " + arg);
				result = false;
			}
			return result;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006CD4 File Offset: 0x00004ED4
		private static MethodInfo GetUnityCrossDomainHelperMethod(string methodname)
		{
			Type type = Type.GetType("UnityEngine.UnityCrossDomainHelper, CrossDomainPolicyParser, Version=1.0.0.0, Culture=neutral");
			if (type == null)
			{
				throw new SecurityException("Cant find type UnityCrossDomainHelper");
			}
			MethodInfo method = type.GetMethod(methodname);
			if (method == null)
			{
				throw new SecurityException("Cant find " + methodname);
			}
			return method;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006D20 File Offset: 0x00004F20
		public void Connect(EndPoint remoteEP)
		{
			this.Connect(remoteEP, true);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006D2C File Offset: 0x00004F2C
		internal void Connect(EndPoint remoteEP, bool requireSocketPolicy)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			IPEndPoint ipendPoint = remoteEP as IPEndPoint;
			if (ipendPoint != null && (ipendPoint.Address.Equals(IPAddress.Any) || ipendPoint.Address.Equals(IPAddress.IPv6Any)))
			{
				throw new SocketException(10049);
			}
			if (this.islistening)
			{
				throw new InvalidOperationException();
			}
			SocketAddress sa = remoteEP.Serialize();
			int num = 0;
			this.blocking_thread = Thread.CurrentThread;
			try
			{
				Socket.Connect_internal(this.socket, sa, out num, requireSocketPolicy);
			}
			catch (ThreadAbortException)
			{
				if (this.disposed)
				{
					Thread.ResetAbort();
					num = 10004;
				}
			}
			finally
			{
				this.blocking_thread = null;
			}
			if (num != 0)
			{
				throw new SocketException(num);
			}
			this.connected = true;
			this.isbound = true;
			this.seed_endpoint = remoteEP;
		}

		// Token: 0x06000175 RID: 373
		[MethodImpl(4096)]
		private static extern bool Poll_internal(IntPtr socket, SelectMode mode, int timeout, out int error);

		// Token: 0x06000176 RID: 374
		[MethodImpl(4096)]
		private static extern int Receive_internal(IntPtr sock, byte[] buffer, int offset, int count, SocketFlags flags, out int error);

		// Token: 0x06000177 RID: 375 RVA: 0x00006E50 File Offset: 0x00005050
		internal int Receive_nochecks(byte[] buf, int offset, int size, SocketFlags flags, out SocketError error)
		{
			if (this.protocol_type == ProtocolType.Udp)
			{
				EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
				int num = 0;
				int result = this.ReceiveFrom_nochecks_exc(buf, offset, size, flags, ref endPoint, false, out num);
				error = (SocketError)num;
				return result;
			}
			int num2;
			int result2 = Socket.Receive_internal(this.socket, buf, offset, size, flags, out num2);
			error = (SocketError)num2;
			if (error != SocketError.Success && error != SocketError.WouldBlock && error != SocketError.InProgress)
			{
				this.connected = false;
			}
			else
			{
				this.connected = true;
			}
			return result2;
		}

		// Token: 0x06000178 RID: 376
		[MethodImpl(4096)]
		private static extern void GetSocketOption_obj_internal(IntPtr socket, SocketOptionLevel level, SocketOptionName name, out object obj_val, out int error);

		// Token: 0x06000179 RID: 377
		[MethodImpl(4096)]
		private static extern int Send_internal(IntPtr sock, byte[] buf, int offset, int count, SocketFlags flags, out int error);

		// Token: 0x0600017A RID: 378 RVA: 0x00006EE0 File Offset: 0x000050E0
		internal int Send_nochecks(byte[] buf, int offset, int size, SocketFlags flags, out SocketError error)
		{
			if (size == 0)
			{
				error = SocketError.Success;
				return 0;
			}
			int num;
			int result = Socket.Send_internal(this.socket, buf, offset, size, flags, out num);
			error = (SocketError)num;
			if (error != SocketError.Success && error != SocketError.WouldBlock && error != SocketError.InProgress)
			{
				this.connected = false;
			}
			else
			{
				this.connected = true;
			}
			return result;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006F48 File Offset: 0x00005148
		public object GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			object obj;
			int num;
			Socket.GetSocketOption_obj_internal(this.socket, optionLevel, optionName, out obj, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			if (optionName == SocketOptionName.Linger)
			{
				return (LingerOption)obj;
			}
			if (optionName == SocketOptionName.AddMembership || optionName == SocketOptionName.DropMembership)
			{
				return (MulticastOption)obj;
			}
			if (obj is int)
			{
				return (int)obj;
			}
			return obj;
		}

		// Token: 0x0600017C RID: 380
		[MethodImpl(4096)]
		private static extern void Shutdown_internal(IntPtr socket, SocketShutdown how, out int error);

		// Token: 0x0600017D RID: 381 RVA: 0x00006FDC File Offset: 0x000051DC
		public void Shutdown(SocketShutdown how)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (!this.connected)
			{
				throw new SocketException(10057);
			}
			int num;
			Socket.Shutdown_internal(this.socket, how, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
		}

		// Token: 0x0600017E RID: 382
		[MethodImpl(4096)]
		private static extern void SetSocketOption_internal(IntPtr socket, SocketOptionLevel level, SocketOptionName name, object obj_val, byte[] byte_val, int int_val, out int error);

		// Token: 0x0600017F RID: 383 RVA: 0x00007044 File Offset: 0x00005244
		public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionValue)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			int num;
			Socket.SetSocketOption_internal(this.socket, optionLevel, optionName, null, null, optionValue, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
		}

		// Token: 0x04000857 RID: 2135
		private Queue readQ;

		// Token: 0x04000858 RID: 2136
		private Queue writeQ;

		// Token: 0x04000859 RID: 2137
		private bool islistening;

		// Token: 0x0400085A RID: 2138
		private bool useoverlappedIO;

		// Token: 0x0400085B RID: 2139
		private readonly int MinListenPort;

		// Token: 0x0400085C RID: 2140
		private readonly int MaxListenPort;

		// Token: 0x0400085D RID: 2141
		private static int ipv4Supported = -1;

		// Token: 0x0400085E RID: 2142
		private static int ipv6Supported = -1;

		// Token: 0x0400085F RID: 2143
		private int linger_timeout;

		// Token: 0x04000860 RID: 2144
		private IntPtr socket;

		// Token: 0x04000861 RID: 2145
		private AddressFamily address_family;

		// Token: 0x04000862 RID: 2146
		private SocketType socket_type;

		// Token: 0x04000863 RID: 2147
		private ProtocolType protocol_type;

		// Token: 0x04000864 RID: 2148
		internal bool blocking = true;

		// Token: 0x04000865 RID: 2149
		private Thread blocking_thread;

		// Token: 0x04000866 RID: 2150
		private bool isbound;

		// Token: 0x04000867 RID: 2151
		private static int current_bind_count;

		// Token: 0x04000868 RID: 2152
		private readonly int max_bind_count = 50;

		// Token: 0x04000869 RID: 2153
		private bool connected;

		// Token: 0x0400086A RID: 2154
		private bool closed;

		// Token: 0x0400086B RID: 2155
		internal bool disposed;

		// Token: 0x0400086C RID: 2156
		internal EndPoint seed_endpoint;

		// Token: 0x0400086D RID: 2157
		private static MethodInfo check_socket_policy;
	}
}
