using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System
{
	// Token: 0x020000E2 RID: 226
	[ComVisible(true)]
	public static class Environment
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x000230B4 File Offset: 0x000212B4
		public static string CommandLine
		{
			get
			{
				return string.Join(" ", Environment.GetCommandLineArgs());
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x000230C8 File Offset: 0x000212C8
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x000230D0 File Offset: 0x000212D0
		public static string CurrentDirectory
		{
			get
			{
				return Directory.GetCurrentDirectory();
			}
			set
			{
				Directory.SetCurrentDirectory(value);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060008FA RID: 2298
		// (set) Token: 0x060008FB RID: 2299
		public static extern int ExitCode { [MethodImpl(4096)] get; [MethodImpl(4096)] set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060008FC RID: 2300
		public static extern bool HasShutdownStarted { [MethodImpl(4096)] get; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060008FD RID: 2301
		public static extern string EmbeddingHostName { [MethodImpl(4096)] get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060008FE RID: 2302
		public static extern bool SocketSecurityEnabled { [MethodImpl(4096)] get; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x000230D8 File Offset: 0x000212D8
		public static bool UnityWebSecurityEnabled
		{
			get
			{
				return Environment.SocketSecurityEnabled;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000900 RID: 2304
		public static extern string MachineName { [MethodImpl(4096)] get; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000901 RID: 2305
		public static extern string NewLine { [MethodImpl(4096)] get; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000902 RID: 2306
		internal static extern PlatformID Platform { [MethodImpl(4096)] get; }

		// Token: 0x06000903 RID: 2307
		[MethodImpl(4096)]
		internal static extern string GetOSVersionString();

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x000230E0 File Offset: 0x000212E0
		public static OperatingSystem OSVersion
		{
			get
			{
				if (Environment.os == null)
				{
					Version version = Version.CreateFromString(Environment.GetOSVersionString());
					PlatformID platform = Environment.Platform;
					Environment.os = new OperatingSystem(platform, version);
				}
				return Environment.os;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0002311C File Offset: 0x0002131C
		public static string StackTrace
		{
			get
			{
				StackTrace stackTrace = new StackTrace(0, true);
				return stackTrace.ToString();
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000906 RID: 2310
		public static extern int TickCount { [MethodImpl(4096)] get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00023138 File Offset: 0x00021338
		public static string UserDomainName
		{
			get
			{
				return Environment.MachineName;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00023140 File Offset: 0x00021340
		[MonoTODO("Currently always returns false, regardless of interactive state")]
		public static bool UserInteractive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000909 RID: 2313
		public static extern string UserName { [MethodImpl(4096)] get; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00023144 File Offset: 0x00021344
		public static Version Version
		{
			get
			{
				return new Version("3.0.40818.0");
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00023150 File Offset: 0x00021350
		[MonoTODO("Currently always returns zero")]
		public static long WorkingSet
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x0600090C RID: 2316
		[MethodImpl(4096)]
		public static extern void Exit(int exitCode);

		// Token: 0x0600090D RID: 2317 RVA: 0x00023154 File Offset: 0x00021354
		public static string ExpandEnvironmentVariables(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			int num = name.IndexOf('%');
			if (num == -1)
			{
				return name;
			}
			int length = name.Length;
			int num2;
			if (num == length - 1 || (num2 = name.IndexOf('%', num + 1)) == -1)
			{
				return name;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(name, 0, num);
			Hashtable hashtable = null;
			do
			{
				string text = name.Substring(num + 1, num2 - num - 1);
				string text2 = Environment.GetEnvironmentVariable(text);
				if (text2 == null && Environment.IsRunningOnWindows)
				{
					if (hashtable == null)
					{
						hashtable = Environment.GetEnvironmentVariablesNoCase();
					}
					text2 = (hashtable[text] as string);
				}
				if (text2 == null)
				{
					stringBuilder.Append('%');
					stringBuilder.Append(text);
					num2--;
				}
				else
				{
					stringBuilder.Append(text2);
				}
				int num3 = num2;
				num = name.IndexOf('%', num2 + 1);
				num2 = ((num != -1 && num2 <= length - 1) ? name.IndexOf('%', num + 1) : -1);
				int count;
				if (num == -1 || num2 == -1)
				{
					count = length - num3 - 1;
				}
				else if (text2 != null)
				{
					count = num - num3 - 1;
				}
				else
				{
					count = num - num3;
				}
				if (num >= num3 || num == -1)
				{
					stringBuilder.Append(name, num3 + 1, count);
				}
			}
			while (num2 > -1 && num2 < length);
			return stringBuilder.ToString();
		}

		// Token: 0x0600090E RID: 2318
		[MethodImpl(4096)]
		public static extern string[] GetCommandLineArgs();

		// Token: 0x0600090F RID: 2319
		[MethodImpl(4096)]
		internal static extern string internalGetEnvironmentVariable(string variable);

		// Token: 0x06000910 RID: 2320 RVA: 0x000232C8 File Offset: 0x000214C8
		public static string GetEnvironmentVariable(string variable)
		{
			return Environment.internalGetEnvironmentVariable(variable);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000232D0 File Offset: 0x000214D0
		private static Hashtable GetEnvironmentVariablesNoCase()
		{
			Hashtable hashtable = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			foreach (string text in Environment.GetEnvironmentVariableNames())
			{
				hashtable[text] = Environment.internalGetEnvironmentVariable(text);
			}
			return hashtable;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0002331C File Offset: 0x0002151C
		public static IDictionary GetEnvironmentVariables()
		{
			Hashtable hashtable = new Hashtable();
			foreach (string text in Environment.GetEnvironmentVariableNames())
			{
				hashtable[text] = Environment.internalGetEnvironmentVariable(text);
			}
			return hashtable;
		}

		// Token: 0x06000913 RID: 2323
		[MethodImpl(4096)]
		private static extern string GetWindowsFolderPath(int folder);

		// Token: 0x06000914 RID: 2324 RVA: 0x0002335C File Offset: 0x0002155C
		public static string GetFolderPath(Environment.SpecialFolder folder)
		{
			string result;
			if (Environment.IsRunningOnWindows)
			{
				result = Environment.GetWindowsFolderPath((int)folder);
			}
			else
			{
				result = Environment.InternalGetFolderPath(folder);
			}
			return result;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0002338C File Offset: 0x0002158C
		private static string ReadXdgUserDir(string config_dir, string home_dir, string key, string fallback)
		{
			string text = Environment.internalGetEnvironmentVariable(key);
			if (text != null && text != string.Empty)
			{
				return text;
			}
			string path = Path.Combine(config_dir, "user-dirs.dirs");
			if (!File.Exists(path))
			{
				return Path.Combine(home_dir, fallback);
			}
			try
			{
				using (StreamReader streamReader = new StreamReader(path))
				{
					string text2;
					while ((text2 = streamReader.ReadLine()) != null)
					{
						text2 = text2.Trim();
						int num = text2.IndexOf('=');
						if (num > 8 && text2.Substring(0, num) == key)
						{
							string text3 = text2.Substring(num + 1).Trim(new char[]
							{
								'"'
							});
							bool flag = false;
							if (text3.StartsWith("$HOME/"))
							{
								flag = true;
								text3 = text3.Substring(6);
							}
							else if (!text3.StartsWith("/"))
							{
								flag = true;
							}
							return (!flag) ? text3 : Path.Combine(home_dir, text3);
						}
					}
				}
			}
			catch (FileNotFoundException)
			{
			}
			return Path.Combine(home_dir, fallback);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000234D4 File Offset: 0x000216D4
		internal static string InternalGetFolderPath(Environment.SpecialFolder folder)
		{
			string text = Environment.internalGetHome();
			string text2 = Environment.internalGetEnvironmentVariable("XDG_DATA_HOME");
			if (text2 == null || text2 == string.Empty)
			{
				text2 = Path.Combine(text, ".local");
				text2 = Path.Combine(text2, "share");
			}
			string text3 = Environment.internalGetEnvironmentVariable("XDG_CONFIG_HOME");
			if (text3 == null || text3 == string.Empty)
			{
				text3 = Path.Combine(text, ".config");
			}
			switch (folder)
			{
			case Environment.SpecialFolder.Desktop:
			case Environment.SpecialFolder.DesktopDirectory:
				return Environment.ReadXdgUserDir(text3, text, "XDG_DESKTOP_DIR", "Desktop");
			case Environment.SpecialFolder.Programs:
			case Environment.SpecialFolder.Favorites:
			case Environment.SpecialFolder.Startup:
			case Environment.SpecialFolder.Recent:
			case Environment.SpecialFolder.SendTo:
			case Environment.SpecialFolder.StartMenu:
			case Environment.SpecialFolder.Templates:
			case Environment.SpecialFolder.InternetCache:
			case Environment.SpecialFolder.Cookies:
			case Environment.SpecialFolder.History:
			case Environment.SpecialFolder.System:
			case Environment.SpecialFolder.ProgramFiles:
			case Environment.SpecialFolder.CommonProgramFiles:
				return string.Empty;
			case Environment.SpecialFolder.MyDocuments:
				return Path.Combine(text, "Documents");
			case Environment.SpecialFolder.MyMusic:
				return Environment.ReadXdgUserDir(text3, text, "XDG_MUSIC_DIR", "Music");
			case Environment.SpecialFolder.MyComputer:
				return string.Empty;
			case Environment.SpecialFolder.ApplicationData:
				return text3;
			case Environment.SpecialFolder.LocalApplicationData:
				return text2;
			case Environment.SpecialFolder.CommonApplicationData:
				return "/usr/share";
			case Environment.SpecialFolder.MyPictures:
				return Environment.ReadXdgUserDir(text3, text, "XDG_PICTURES_DIR", "Pictures");
			}
			throw new ArgumentException("Invalid SpecialFolder");
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0002366C File Offset: 0x0002186C
		public static string[] GetLogicalDrives()
		{
			return Environment.GetLogicalDrivesInternal();
		}

		// Token: 0x06000918 RID: 2328
		[MethodImpl(4096)]
		private static extern void internalBroadcastSettingChange();

		// Token: 0x06000919 RID: 2329 RVA: 0x00023674 File Offset: 0x00021874
		public static string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
		{
			switch (target)
			{
			case EnvironmentVariableTarget.Process:
				return Environment.GetEnvironmentVariable(variable);
			case EnvironmentVariableTarget.User:
				break;
			case EnvironmentVariableTarget.Machine:
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				if (!Environment.IsRunningOnWindows)
				{
					return null;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment"))
				{
					object value = registryKey.GetValue(variable);
					return (value != null) ? value.ToString() : null;
				}
				break;
			default:
				goto IL_D7;
			}
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			if (!Environment.IsRunningOnWindows)
			{
				return null;
			}
			using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Environment", false))
			{
				object value2 = registryKey2.GetValue(variable);
				return (value2 != null) ? value2.ToString() : null;
			}
			IL_D7:
			throw new ArgumentException("target");
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00023784 File Offset: 0x00021984
		public static IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target)
		{
			IDictionary dictionary = new Hashtable();
			switch (target)
			{
			case EnvironmentVariableTarget.Process:
				dictionary = Environment.GetEnvironmentVariables();
				break;
			case EnvironmentVariableTarget.User:
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				if (Environment.IsRunningOnWindows)
				{
					using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Environment"))
					{
						string[] valueNames = registryKey.GetValueNames();
						foreach (string text in valueNames)
						{
							dictionary.Add(text, registryKey.GetValue(text));
						}
					}
				}
				break;
			case EnvironmentVariableTarget.Machine:
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				if (Environment.IsRunningOnWindows)
				{
					using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment"))
					{
						string[] valueNames2 = registryKey2.GetValueNames();
						foreach (string text2 in valueNames2)
						{
							dictionary.Add(text2, registryKey2.GetValue(text2));
						}
					}
				}
				break;
			default:
				throw new ArgumentException("target");
			}
			return dictionary;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000238D8 File Offset: 0x00021AD8
		public static void SetEnvironmentVariable(string variable, string value)
		{
			Environment.SetEnvironmentVariable(variable, value, EnvironmentVariableTarget.Process);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000238E4 File Offset: 0x00021AE4
		public static void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (variable == string.Empty)
			{
				throw new ArgumentException("String cannot be of zero length.", "variable");
			}
			if (variable.IndexOf('=') != -1)
			{
				throw new ArgumentException("Environment variable name cannot contain an equal character.", "variable");
			}
			if (variable[0] == '\0')
			{
				throw new ArgumentException("The first char in the string is the null character.", "variable");
			}
			switch (target)
			{
			case EnvironmentVariableTarget.Process:
				Environment.InternalSetEnvironmentVariable(variable, value);
				break;
			case EnvironmentVariableTarget.User:
				if (!Environment.IsRunningOnWindows)
				{
					return;
				}
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Environment", true))
				{
					if (string.IsNullOrEmpty(value))
					{
						registryKey.DeleteValue(variable, false);
					}
					else
					{
						registryKey.SetValue(variable, value);
					}
					Environment.internalBroadcastSettingChange();
				}
				break;
			case EnvironmentVariableTarget.Machine:
				if (!Environment.IsRunningOnWindows)
				{
					return;
				}
				using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment", true))
				{
					if (string.IsNullOrEmpty(value))
					{
						registryKey2.DeleteValue(variable, false);
					}
					else
					{
						registryKey2.SetValue(variable, value);
					}
					Environment.internalBroadcastSettingChange();
				}
				break;
			default:
				throw new ArgumentException("target");
			}
		}

		// Token: 0x0600091D RID: 2333
		[MethodImpl(4096)]
		internal static extern void InternalSetEnvironmentVariable(string variable, string value);

		// Token: 0x0600091E RID: 2334 RVA: 0x00023A58 File Offset: 0x00021C58
		[MonoTODO("Not implemented")]
		public static void FailFast(string message)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600091F RID: 2335
		public static extern int ProcessorCount { [MethodImpl(4096)] get; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00023A60 File Offset: 0x00021C60
		internal static bool IsRunningOnWindows
		{
			get
			{
				return Environment.Platform < PlatformID.Unix;
			}
		}

		// Token: 0x06000921 RID: 2337
		[MethodImpl(4096)]
		private static extern string[] GetLogicalDrivesInternal();

		// Token: 0x06000922 RID: 2338
		[MethodImpl(4096)]
		private static extern string[] GetEnvironmentVariableNames();

		// Token: 0x06000923 RID: 2339
		[MethodImpl(4096)]
		internal static extern string GetMachineConfigPath();

		// Token: 0x06000924 RID: 2340
		[MethodImpl(4096)]
		internal static extern string internalGetHome();

		// Token: 0x040002FC RID: 764
		private const int mono_corlib_version = 82;

		// Token: 0x040002FD RID: 765
		private static OperatingSystem os;

		// Token: 0x020000E3 RID: 227
		[ComVisible(true)]
		public enum SpecialFolder
		{
			// Token: 0x040002FF RID: 767
			MyDocuments = 5,
			// Token: 0x04000300 RID: 768
			Desktop = 0,
			// Token: 0x04000301 RID: 769
			MyComputer = 17,
			// Token: 0x04000302 RID: 770
			Programs = 2,
			// Token: 0x04000303 RID: 771
			Personal = 5,
			// Token: 0x04000304 RID: 772
			Favorites,
			// Token: 0x04000305 RID: 773
			Startup,
			// Token: 0x04000306 RID: 774
			Recent,
			// Token: 0x04000307 RID: 775
			SendTo,
			// Token: 0x04000308 RID: 776
			StartMenu = 11,
			// Token: 0x04000309 RID: 777
			MyMusic = 13,
			// Token: 0x0400030A RID: 778
			DesktopDirectory = 16,
			// Token: 0x0400030B RID: 779
			Templates = 21,
			// Token: 0x0400030C RID: 780
			ApplicationData = 26,
			// Token: 0x0400030D RID: 781
			LocalApplicationData = 28,
			// Token: 0x0400030E RID: 782
			InternetCache = 32,
			// Token: 0x0400030F RID: 783
			Cookies,
			// Token: 0x04000310 RID: 784
			History,
			// Token: 0x04000311 RID: 785
			CommonApplicationData,
			// Token: 0x04000312 RID: 786
			System = 37,
			// Token: 0x04000313 RID: 787
			ProgramFiles,
			// Token: 0x04000314 RID: 788
			MyPictures,
			// Token: 0x04000315 RID: 789
			CommonProgramFiles = 43
		}
	}
}
