using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x0200002F RID: 47
	[System.ComponentModel.DefaultProperty("StartInfo")]
	[System.ComponentModel.DefaultEvent("Exited")]
	[MonitoringDescription("Represents a system process")]
	[System.ComponentModel.Designer("System.Diagnostics.Design.ProcessDesigner, System.Design, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class Process : System.ComponentModel.Component
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00002F00 File Offset: 0x00001100
		private Process(IntPtr handle, int id)
		{
			this.process_handle = handle;
			this.pid = id;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002F18 File Offset: 0x00001118
		public Process()
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002F20 File Offset: 0x00001120
		private void StartExitCallbackIfNeeded()
		{
		}

		// Token: 0x06000090 RID: 144
		[MethodImpl(4096)]
		private static extern int ExitCode_internal(IntPtr handle);

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002F24 File Offset: 0x00001124
		[System.ComponentModel.Browsable(false)]
		[MonitoringDescription("The exit code of the process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public int ExitCode
		{
			get
			{
				if (this.process_handle == IntPtr.Zero)
				{
					throw new InvalidOperationException("Process has not been started.");
				}
				int num = Process.ExitCode_internal(this.process_handle);
				if (num == 259)
				{
					throw new InvalidOperationException("The process must exit before getting the requested information.");
				}
				return num;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002F74 File Offset: 0x00001174
		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Handle for this process.")]
		public IntPtr Handle
		{
			get
			{
				return this.process_handle;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002F7C File Offset: 0x0000117C
		[MonitoringDescription("Determines if the process is still running.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		public bool HasExited
		{
			get
			{
				if (this.process_handle == IntPtr.Zero)
				{
					throw new InvalidOperationException("Process has not been started.");
				}
				int num = Process.ExitCode_internal(this.process_handle);
				return num != 259;
			}
		}

		// Token: 0x06000094 RID: 148
		[MethodImpl(4096)]
		private static extern string ProcessName_internal(IntPtr handle);

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002FC4 File Offset: 0x000011C4
		[MonitoringDescription("The name of this process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public string ProcessName
		{
			get
			{
				if (this.process_name == null)
				{
					if (this.process_handle == IntPtr.Zero)
					{
						throw new InvalidOperationException("No process is associated with this object.");
					}
					this.process_name = Process.ProcessName_internal(this.process_handle);
					if (this.process_name == null)
					{
						throw new InvalidOperationException("Process has exited, so the requested information is not available.");
					}
					if (this.process_name.EndsWith(".exe") || this.process_name.EndsWith(".bat") || this.process_name.EndsWith(".com"))
					{
						this.process_name = this.process_name.Substring(0, this.process_name.Length - 4);
					}
				}
				return this.process_name;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003088 File Offset: 0x00001288
		[System.ComponentModel.Browsable(false)]
		[MonitoringDescription("Information for the start of this process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
		public ProcessStartInfo StartInfo
		{
			get
			{
				if (this.start_info == null)
				{
					this.start_info = new ProcessStartInfo();
				}
				return this.start_info;
			}
		}

		// Token: 0x06000097 RID: 151
		[MethodImpl(4096)]
		private static extern bool Kill_internal(IntPtr handle, int signo);

		// Token: 0x06000098 RID: 152 RVA: 0x000030A8 File Offset: 0x000012A8
		private bool Close(int signo)
		{
			if (this.process_handle == IntPtr.Zero)
			{
				throw new SystemException("No process to kill.");
			}
			int num = Process.ExitCode_internal(this.process_handle);
			if (num != 259)
			{
				throw new InvalidOperationException("The process already finished.");
			}
			return Process.Kill_internal(this.process_handle, signo);
		}

		// Token: 0x06000099 RID: 153
		[MethodImpl(4096)]
		private static extern IntPtr GetProcess_internal(int pid);

		// Token: 0x0600009A RID: 154
		[MethodImpl(4096)]
		private static extern int GetPid_internal();

		// Token: 0x0600009B RID: 155 RVA: 0x00003104 File Offset: 0x00001304
		public static Process GetCurrentProcess()
		{
			int pid_internal = Process.GetPid_internal();
			IntPtr process_internal = Process.GetProcess_internal(pid_internal);
			if (process_internal == IntPtr.Zero)
			{
				throw new SystemException("Can't find current process");
			}
			return new Process(process_internal, pid_internal);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003140 File Offset: 0x00001340
		public void Kill()
		{
			this.Close(1);
		}

		// Token: 0x0600009D RID: 157
		[MethodImpl(4096)]
		private static extern bool ShellExecuteEx_internal(ProcessStartInfo startInfo, ref Process.ProcInfo proc_info);

		// Token: 0x0600009E RID: 158
		[MethodImpl(4096)]
		private static extern bool CreateProcess_internal(ProcessStartInfo startInfo, IntPtr stdin, IntPtr stdout, IntPtr stderr, ref Process.ProcInfo proc_info);

		// Token: 0x0600009F RID: 159 RVA: 0x0000314C File Offset: 0x0000134C
		private static bool Start_shell(ProcessStartInfo startInfo, Process process)
		{
			Process.ProcInfo procInfo = default(Process.ProcInfo);
			if (startInfo.RedirectStandardInput || startInfo.RedirectStandardOutput || startInfo.RedirectStandardError)
			{
				throw new InvalidOperationException("UseShellExecute must be false when redirecting I/O.");
			}
			if (startInfo.HaveEnvVars)
			{
				throw new InvalidOperationException("UseShellExecute must be false in order to use environment variables.");
			}
			Process.FillUserInfo(startInfo, ref procInfo);
			bool flag;
			try
			{
				flag = Process.ShellExecuteEx_internal(startInfo, ref procInfo);
			}
			finally
			{
				if (procInfo.Password != IntPtr.Zero)
				{
					Marshal.FreeBSTR(procInfo.Password);
				}
				procInfo.Password = IntPtr.Zero;
			}
			if (!flag)
			{
				throw new System.ComponentModel.Win32Exception(-procInfo.pid);
			}
			process.process_handle = procInfo.process_handle;
			process.pid = procInfo.pid;
			process.StartExitCallbackIfNeeded();
			return flag;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003230 File Offset: 0x00001430
		private static bool Start_noshell(ProcessStartInfo startInfo, Process process)
		{
			Process.ProcInfo procInfo = default(Process.ProcInfo);
			IntPtr intPtr = IntPtr.Zero;
			IntPtr handle = IntPtr.Zero;
			if (startInfo.HaveEnvVars)
			{
				string[] array = new string[startInfo.EnvironmentVariables.Count];
				startInfo.EnvironmentVariables.Keys.CopyTo(array, 0);
				procInfo.envKeys = array;
				array = new string[startInfo.EnvironmentVariables.Count];
				startInfo.EnvironmentVariables.Values.CopyTo(array, 0);
				procInfo.envValues = array;
			}
			bool flag;
			if (startInfo.RedirectStandardInput)
			{
				if (Process.IsWindows)
				{
					int options = 2;
					IntPtr intPtr2;
					flag = MonoIO.CreatePipe(out intPtr, out intPtr2);
					if (flag)
					{
						flag = MonoIO.DuplicateHandle(Process.GetCurrentProcess().Handle, intPtr2, Process.GetCurrentProcess().Handle, out handle, 0, 0, options);
						MonoIOError monoIOError;
						MonoIO.Close(intPtr2, out monoIOError);
					}
				}
				else
				{
					flag = MonoIO.CreatePipe(out intPtr, out handle);
				}
				if (!flag)
				{
					throw new IOException("Error creating standard input pipe");
				}
			}
			else
			{
				intPtr = MonoIO.ConsoleInput;
				handle = (IntPtr)0;
			}
			IntPtr consoleOutput;
			if (startInfo.RedirectStandardOutput)
			{
				IntPtr zero = IntPtr.Zero;
				if (Process.IsWindows)
				{
					int options2 = 2;
					IntPtr intPtr3;
					flag = MonoIO.CreatePipe(out intPtr3, out consoleOutput);
					if (flag)
					{
						MonoIO.DuplicateHandle(Process.GetCurrentProcess().Handle, intPtr3, Process.GetCurrentProcess().Handle, out zero, 0, 0, options2);
						MonoIOError monoIOError;
						MonoIO.Close(intPtr3, out monoIOError);
					}
				}
				else
				{
					flag = MonoIO.CreatePipe(out zero, out consoleOutput);
				}
				process.stdout_rd = zero;
				if (!flag)
				{
					if (startInfo.RedirectStandardInput)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr, out monoIOError);
						MonoIO.Close(handle, out monoIOError);
					}
					throw new IOException("Error creating standard output pipe");
				}
			}
			else
			{
				process.stdout_rd = (IntPtr)0;
				consoleOutput = MonoIO.ConsoleOutput;
			}
			IntPtr consoleError;
			if (startInfo.RedirectStandardError)
			{
				IntPtr zero2 = IntPtr.Zero;
				if (Process.IsWindows)
				{
					int options3 = 2;
					IntPtr intPtr4;
					flag = MonoIO.CreatePipe(out intPtr4, out consoleError);
					if (flag)
					{
						MonoIO.DuplicateHandle(Process.GetCurrentProcess().Handle, intPtr4, Process.GetCurrentProcess().Handle, out zero2, 0, 0, options3);
						MonoIOError monoIOError;
						MonoIO.Close(intPtr4, out monoIOError);
					}
				}
				else
				{
					flag = MonoIO.CreatePipe(out zero2, out consoleError);
				}
				process.stderr_rd = zero2;
				if (!flag)
				{
					if (startInfo.RedirectStandardInput)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr, out monoIOError);
						MonoIO.Close(handle, out monoIOError);
					}
					if (startInfo.RedirectStandardOutput)
					{
						MonoIOError monoIOError;
						MonoIO.Close(process.stdout_rd, out monoIOError);
						MonoIO.Close(consoleOutput, out monoIOError);
					}
					throw new IOException("Error creating standard error pipe");
				}
			}
			else
			{
				process.stderr_rd = (IntPtr)0;
				consoleError = MonoIO.ConsoleError;
			}
			Process.FillUserInfo(startInfo, ref procInfo);
			try
			{
				flag = Process.CreateProcess_internal(startInfo, intPtr, consoleOutput, consoleError, ref procInfo);
			}
			finally
			{
				if (procInfo.Password != IntPtr.Zero)
				{
					Marshal.FreeBSTR(procInfo.Password);
				}
				procInfo.Password = IntPtr.Zero;
			}
			if (!flag)
			{
				if (startInfo.RedirectStandardInput)
				{
					MonoIOError monoIOError;
					MonoIO.Close(intPtr, out monoIOError);
					MonoIO.Close(handle, out monoIOError);
				}
				if (startInfo.RedirectStandardOutput)
				{
					MonoIOError monoIOError;
					MonoIO.Close(process.stdout_rd, out monoIOError);
					MonoIO.Close(consoleOutput, out monoIOError);
				}
				if (startInfo.RedirectStandardError)
				{
					MonoIOError monoIOError;
					MonoIO.Close(process.stderr_rd, out monoIOError);
					MonoIO.Close(consoleError, out monoIOError);
				}
				throw new System.ComponentModel.Win32Exception(-procInfo.pid, string.Concat(new string[]
				{
					"ApplicationName='",
					startInfo.FileName,
					"', CommandLine='",
					startInfo.Arguments,
					"', CurrentDirectory='",
					startInfo.WorkingDirectory,
					"'"
				}));
			}
			process.process_handle = procInfo.process_handle;
			process.pid = procInfo.pid;
			if (startInfo.RedirectStandardInput)
			{
				MonoIOError monoIOError;
				MonoIO.Close(intPtr, out monoIOError);
				process.input_stream = new StreamWriter(new MonoSyncFileStream(handle, FileAccess.Write, true, 8192), Console.Out.Encoding);
				process.input_stream.AutoFlush = true;
			}
			Encoding encoding = startInfo.StandardOutputEncoding ?? Console.Out.Encoding;
			Encoding encoding2 = startInfo.StandardErrorEncoding ?? Console.Out.Encoding;
			if (startInfo.RedirectStandardOutput)
			{
				MonoIOError monoIOError;
				MonoIO.Close(consoleOutput, out monoIOError);
				process.output_stream = new StreamReader(new MonoSyncFileStream(process.stdout_rd, FileAccess.Read, true, 8192), encoding, true, 8192);
			}
			if (startInfo.RedirectStandardError)
			{
				MonoIOError monoIOError;
				MonoIO.Close(consoleError, out monoIOError);
				process.error_stream = new StreamReader(new MonoSyncFileStream(process.stderr_rd, FileAccess.Read, true, 8192), encoding2, true, 8192);
			}
			process.StartExitCallbackIfNeeded();
			return flag;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000036FC File Offset: 0x000018FC
		private static void FillUserInfo(ProcessStartInfo startInfo, ref Process.ProcInfo proc_info)
		{
			if (startInfo.UserName != null)
			{
				proc_info.UserName = startInfo.UserName;
				proc_info.Domain = startInfo.Domain;
				if (startInfo.Password != null)
				{
					proc_info.Password = Marshal.SecureStringToBSTR(startInfo.Password);
				}
				else
				{
					proc_info.Password = IntPtr.Zero;
				}
				proc_info.LoadUserProfile = startInfo.LoadUserProfile;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003764 File Offset: 0x00001964
		private static bool Start_common(ProcessStartInfo startInfo, Process process)
		{
			if (startInfo.FileName == null || startInfo.FileName.Length == 0)
			{
				throw new InvalidOperationException("File name has not been set");
			}
			if (startInfo.StandardErrorEncoding != null && !startInfo.RedirectStandardError)
			{
				throw new InvalidOperationException("StandardErrorEncoding is only supported when standard error is redirected");
			}
			if (startInfo.StandardOutputEncoding != null && !startInfo.RedirectStandardOutput)
			{
				throw new InvalidOperationException("StandardOutputEncoding is only supported when standard output is redirected");
			}
			if (!startInfo.UseShellExecute)
			{
				return Process.Start_noshell(startInfo, process);
			}
			if (!string.IsNullOrEmpty(startInfo.UserName))
			{
				throw new InvalidOperationException("UserShellExecute must be false if an explicit UserName is specified when starting a process");
			}
			return Process.Start_shell(startInfo, process);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003810 File Offset: 0x00001A10
		public bool Start()
		{
			if (this.process_handle != IntPtr.Zero)
			{
				this.Process_free_internal(this.process_handle);
				this.process_handle = IntPtr.Zero;
			}
			return Process.Start_common(this.start_info, this);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000384C File Offset: 0x00001A4C
		public override string ToString()
		{
			return base.ToString() + " (" + this.ProcessName + ")";
		}

		// Token: 0x060000A5 RID: 165
		[MethodImpl(4096)]
		private extern bool WaitForExit_internal(IntPtr handle, int ms);

		// Token: 0x060000A6 RID: 166 RVA: 0x0000386C File Offset: 0x00001A6C
		public bool WaitForExit(int milliseconds)
		{
			int num = milliseconds;
			if (num == 2147483647)
			{
				num = -1;
			}
			DateTime d = DateTime.UtcNow;
			if (this.async_output != null && !this.async_output.IsCompleted)
			{
				if (!this.async_output.WaitHandle.WaitOne(num, false))
				{
					return false;
				}
				if (num >= 0)
				{
					DateTime utcNow = DateTime.UtcNow;
					num -= (int)(utcNow - d).TotalMilliseconds;
					if (num <= 0)
					{
						return false;
					}
					d = utcNow;
				}
			}
			if (this.async_error != null && !this.async_error.IsCompleted)
			{
				if (!this.async_error.WaitHandle.WaitOne(num, false))
				{
					return false;
				}
				if (num >= 0)
				{
					num -= (int)(DateTime.UtcNow - d).TotalMilliseconds;
					if (num <= 0)
					{
						return false;
					}
				}
			}
			return this.WaitForExit_internal(this.process_handle, num);
		}

		// Token: 0x060000A7 RID: 167
		[MethodImpl(4096)]
		private extern void Process_free_internal(IntPtr handle);

		// Token: 0x060000A8 RID: 168 RVA: 0x00003958 File Offset: 0x00001B58
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				if (disposing)
				{
					lock (this)
					{
						if (this.async_output != null)
						{
							this.async_output.Close();
						}
						if (this.async_error != null)
						{
							this.async_error.Close();
						}
					}
				}
				lock (this)
				{
					if (this.process_handle != IntPtr.Zero)
					{
						this.Process_free_internal(this.process_handle);
						this.process_handle = IntPtr.Zero;
					}
					if (this.input_stream != null)
					{
						this.input_stream.Close();
						this.input_stream = null;
					}
					if (this.output_stream != null)
					{
						this.output_stream.Close();
						this.output_stream = null;
					}
					if (this.error_stream != null)
					{
						this.error_stream.Close();
						this.error_stream = null;
					}
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003A78 File Offset: 0x00001C78
		~Process()
		{
			this.Dispose(false);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003AA8 File Offset: 0x00001CA8
		private static bool IsWindows
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform == PlatformID.Win32S || platform == PlatformID.Win32Windows || platform == PlatformID.Win32NT || platform == PlatformID.WinCE;
			}
		}

		// Token: 0x04000070 RID: 112
		private IntPtr process_handle;

		// Token: 0x04000071 RID: 113
		private int pid;

		// Token: 0x04000072 RID: 114
		private bool enableRaisingEvents;

		// Token: 0x04000073 RID: 115
		private bool already_waiting;

		// Token: 0x04000074 RID: 116
		private System.ComponentModel.ISynchronizeInvoke synchronizingObject;

		// Token: 0x04000075 RID: 117
		private EventHandler exited_event;

		// Token: 0x04000076 RID: 118
		private IntPtr stdout_rd;

		// Token: 0x04000077 RID: 119
		private IntPtr stderr_rd;

		// Token: 0x04000078 RID: 120
		private ProcessModuleCollection module_collection;

		// Token: 0x04000079 RID: 121
		private string process_name;

		// Token: 0x0400007A RID: 122
		private StreamReader error_stream;

		// Token: 0x0400007B RID: 123
		private StreamWriter input_stream;

		// Token: 0x0400007C RID: 124
		private StreamReader output_stream;

		// Token: 0x0400007D RID: 125
		private ProcessStartInfo start_info;

		// Token: 0x0400007E RID: 126
		private Process.AsyncModes async_mode;

		// Token: 0x0400007F RID: 127
		private bool output_canceled;

		// Token: 0x04000080 RID: 128
		private bool error_canceled;

		// Token: 0x04000081 RID: 129
		private Process.ProcessAsyncReader async_output;

		// Token: 0x04000082 RID: 130
		private Process.ProcessAsyncReader async_error;

		// Token: 0x04000083 RID: 131
		private bool disposed;

		// Token: 0x04000084 RID: 132
		private DataReceivedEventHandler OutputDataReceived;

		// Token: 0x04000085 RID: 133
		private DataReceivedEventHandler ErrorDataReceived;

		// Token: 0x02000030 RID: 48
		[Flags]
		private enum AsyncModes
		{
			// Token: 0x04000087 RID: 135
			NoneYet = 0,
			// Token: 0x04000088 RID: 136
			SyncOutput = 1,
			// Token: 0x04000089 RID: 137
			SyncError = 2,
			// Token: 0x0400008A RID: 138
			AsyncOutput = 4,
			// Token: 0x0400008B RID: 139
			AsyncError = 8
		}

		// Token: 0x02000031 RID: 49
		// (Invoke) Token: 0x060000AC RID: 172
		private delegate void AsyncReadHandler();

		// Token: 0x02000032 RID: 50
		[StructLayout(0)]
		private sealed class ProcessAsyncReader
		{
			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000AF RID: 175 RVA: 0x00003AE0 File Offset: 0x00001CE0
			public bool IsCompleted
			{
				get
				{
					return this.completed;
				}
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003AE8 File Offset: 0x00001CE8
			public WaitHandle WaitHandle
			{
				get
				{
					WaitHandle result;
					lock (this)
					{
						if (this.wait_handle == null)
						{
							this.wait_handle = new ManualResetEvent(this.completed);
						}
						result = this.wait_handle;
					}
					return result;
				}
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x00003B44 File Offset: 0x00001D44
			public void Close()
			{
				this.stream.Close();
			}

			// Token: 0x0400008C RID: 140
			public object Sock;

			// Token: 0x0400008D RID: 141
			public IntPtr handle;

			// Token: 0x0400008E RID: 142
			public object state;

			// Token: 0x0400008F RID: 143
			public AsyncCallback callback;

			// Token: 0x04000090 RID: 144
			public ManualResetEvent wait_handle;

			// Token: 0x04000091 RID: 145
			public Exception delayedException;

			// Token: 0x04000092 RID: 146
			public object EndPoint;

			// Token: 0x04000093 RID: 147
			private byte[] buffer;

			// Token: 0x04000094 RID: 148
			public int Offset;

			// Token: 0x04000095 RID: 149
			public int Size;

			// Token: 0x04000096 RID: 150
			public int SockFlags;

			// Token: 0x04000097 RID: 151
			public object AcceptSocket;

			// Token: 0x04000098 RID: 152
			public object[] Addresses;

			// Token: 0x04000099 RID: 153
			public int port;

			// Token: 0x0400009A RID: 154
			public object Buffers;

			// Token: 0x0400009B RID: 155
			public bool ReuseSocket;

			// Token: 0x0400009C RID: 156
			public object acc_socket;

			// Token: 0x0400009D RID: 157
			public int total;

			// Token: 0x0400009E RID: 158
			public bool completed_sync;

			// Token: 0x0400009F RID: 159
			private bool completed;

			// Token: 0x040000A0 RID: 160
			private bool err_out;

			// Token: 0x040000A1 RID: 161
			internal int error;

			// Token: 0x040000A2 RID: 162
			public int operation;

			// Token: 0x040000A3 RID: 163
			public object ares;

			// Token: 0x040000A4 RID: 164
			public int EndCalled;

			// Token: 0x040000A5 RID: 165
			private Process process;

			// Token: 0x040000A6 RID: 166
			private Stream stream;

			// Token: 0x040000A7 RID: 167
			private StringBuilder sb;

			// Token: 0x040000A8 RID: 168
			public Process.AsyncReadHandler ReadHandler;
		}

		// Token: 0x02000033 RID: 51
		private struct ProcInfo
		{
			// Token: 0x040000A9 RID: 169
			public IntPtr process_handle;

			// Token: 0x040000AA RID: 170
			public IntPtr thread_handle;

			// Token: 0x040000AB RID: 171
			public int pid;

			// Token: 0x040000AC RID: 172
			public int tid;

			// Token: 0x040000AD RID: 173
			public string[] envKeys;

			// Token: 0x040000AE RID: 174
			public string[] envValues;

			// Token: 0x040000AF RID: 175
			public string UserName;

			// Token: 0x040000B0 RID: 176
			public string Domain;

			// Token: 0x040000B1 RID: 177
			public IntPtr Password;

			// Token: 0x040000B2 RID: 178
			public bool LoadUserProfile;
		}
	}
}
