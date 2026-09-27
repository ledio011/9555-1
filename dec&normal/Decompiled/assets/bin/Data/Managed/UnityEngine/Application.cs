using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	public sealed class Application
	{
		// Token: 0x060001EE RID: 494
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void Quit();

		// Token: 0x060001EF RID: 495
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void CancelQuit();

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001F0 RID: 496
		public static extern int loadedLevel { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001F1 RID: 497
		public static extern string loadedLevelName { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x060001F2 RID: 498 RVA: 0x00006AB8 File Offset: 0x00004CB8
		public static void LoadLevel(int index)
		{
			Application.LoadLevelAsync(null, index, false, true);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006AC4 File Offset: 0x00004CC4
		public static void LoadLevel(string name)
		{
			Application.LoadLevelAsync(name, -1, false, true);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public static AsyncOperation LoadLevelAsync(int index)
		{
			return Application.LoadLevelAsync(null, index, false, false);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006ADC File Offset: 0x00004CDC
		public static AsyncOperation LoadLevelAsync(string levelName)
		{
			return Application.LoadLevelAsync(levelName, -1, false, false);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006AE8 File Offset: 0x00004CE8
		public static AsyncOperation LoadLevelAdditiveAsync(int index)
		{
			return Application.LoadLevelAsync(null, index, true, false);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006AF4 File Offset: 0x00004CF4
		public static AsyncOperation LoadLevelAdditiveAsync(string levelName)
		{
			return Application.LoadLevelAsync(levelName, -1, true, false);
		}

		// Token: 0x060001F8 RID: 504
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern AsyncOperation LoadLevelAsync(string monoLevelName, int index, bool additive, bool mustCompleteNextFrame);

		// Token: 0x060001F9 RID: 505 RVA: 0x00006B00 File Offset: 0x00004D00
		public static void LoadLevelAdditive(int index)
		{
			Application.LoadLevelAsync(null, index, true, true);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006B0C File Offset: 0x00004D0C
		public static void LoadLevelAdditive(string name)
		{
			Application.LoadLevelAsync(name, -1, true, true);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001FB RID: 507
		public static extern bool isLoadingLevel { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001FC RID: 508
		public static extern int levelCount { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x060001FD RID: 509
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern float GetStreamProgressForLevelByName(string levelName);

		// Token: 0x060001FE RID: 510
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern float GetStreamProgressForLevel(int levelIndex);

		// Token: 0x060001FF RID: 511 RVA: 0x00006B18 File Offset: 0x00004D18
		public static float GetStreamProgressForLevel(string levelName)
		{
			return Application.GetStreamProgressForLevelByName(levelName);
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000200 RID: 512
		public static extern int streamedBytes { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x06000201 RID: 513
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool CanStreamedLevelBeLoadedByName(string levelName);

		// Token: 0x06000202 RID: 514
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool CanStreamedLevelBeLoaded(int levelIndex);

		// Token: 0x06000203 RID: 515 RVA: 0x00006B20 File Offset: 0x00004D20
		public static bool CanStreamedLevelBeLoaded(string levelName)
		{
			return Application.CanStreamedLevelBeLoadedByName(levelName);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000204 RID: 516
		public static extern bool isPlaying { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000205 RID: 517
		public static extern bool isEditor { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000206 RID: 518
		public static extern bool isWebPlayer { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000207 RID: 519
		public static extern RuntimePlatform platform { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00006B28 File Offset: 0x00004D28
		public static bool isMobilePlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.Android || platform == RuntimePlatform.WP8Player || platform == RuntimePlatform.BB10Player || platform == RuntimePlatform.TizenPlayer;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00006B64 File Offset: 0x00004D64
		public static bool isConsolePlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return platform == RuntimePlatform.PS3 || platform == RuntimePlatform.PS4 || platform == RuntimePlatform.XBOX360 || platform == RuntimePlatform.XboxOne;
			}
		}

		// Token: 0x0600020A RID: 522
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void CaptureScreenshot(string filename, [DefaultValue("0")] int superSize);

		// Token: 0x0600020B RID: 523 RVA: 0x00006B98 File Offset: 0x00004D98
		[ExcludeFromDocs]
		public static void CaptureScreenshot(string filename)
		{
			int superSize = 0;
			Application.CaptureScreenshot(filename, superSize);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600020C RID: 524
		// (set) Token: 0x0600020D RID: 525
		public static extern bool runInBackground { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00006BB0 File Offset: 0x00004DB0
		[Obsolete("use Application.isEditor instead")]
		public static bool isPlayer
		{
			get
			{
				return !Application.isEditor;
			}
		}

		// Token: 0x0600020F RID: 527
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool HasProLicense();

		// Token: 0x06000210 RID: 528
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern bool HasAdvancedLicense();

		// Token: 0x06000211 RID: 529
		[WrapperlessIcall]
		[Obsolete("Use Object.DontDestroyOnLoad instead")]
		[MethodImpl(4096)]
		public static extern void DontDestroyOnLoad(Object mono);

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000212 RID: 530
		public static extern string dataPath { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000213 RID: 531
		public static extern string streamingAssetsPath { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000214 RID: 532
		[SecurityCritical]
		public static extern string persistentDataPath { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000215 RID: 533
		public static extern string temporaryCachePath { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000216 RID: 534
		public static extern string srcValue { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000217 RID: 535
		public static extern string absoluteURL { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00006BBC File Offset: 0x00004DBC
		[Obsolete("Please use absoluteURL instead")]
		public static string absoluteUrl
		{
			get
			{
				return Application.absoluteURL;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006BC4 File Offset: 0x00004DC4
		private static string ObjectToJSString(object o)
		{
			if (o == null)
			{
				return "null";
			}
			if (o is string)
			{
				string text = o.ToString().Replace("\\", "\\\\");
				text = text.Replace("\"", "\\\"");
				text = text.Replace("\n", "\\n");
				text = text.Replace("\r", "\\r");
				text = text.Replace("\0", string.Empty);
				text = text.Replace("\u2028", string.Empty);
				text = text.Replace("\u2029", string.Empty);
				return '"' + text + '"';
			}
			if (o is int || o is short || o is uint || o is ushort || o is byte)
			{
				return o.ToString();
			}
			if (o is float)
			{
				NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
				return ((float)o).ToString(numberFormat);
			}
			if (o is double)
			{
				NumberFormatInfo numberFormat2 = CultureInfo.InvariantCulture.NumberFormat;
				return ((double)o).ToString(numberFormat2);
			}
			if (o is char)
			{
				if ((char)o == '"')
				{
					return "\"\\\"\"";
				}
				return '"' + o.ToString() + '"';
			}
			else
			{
				if (o is IList)
				{
					IList list = (IList)o;
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("new Array(");
					int count = list.Count;
					for (int i = 0; i < count; i++)
					{
						if (i != 0)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(Application.ObjectToJSString(list[i]));
					}
					stringBuilder.Append(")");
					return stringBuilder.ToString();
				}
				return Application.ObjectToJSString(o.ToString());
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00006DCC File Offset: 0x00004FCC
		public static void ExternalCall(string functionName, params object[] args)
		{
			Application.Internal_ExternalCall(Application.BuildInvocationForArguments(functionName, args));
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006DDC File Offset: 0x00004FDC
		private static string BuildInvocationForArguments(string functionName, params object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(functionName);
			stringBuilder.Append('(');
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(Application.ObjectToJSString(args[i]));
			}
			stringBuilder.Append(')');
			stringBuilder.Append(';');
			return stringBuilder.ToString();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006E50 File Offset: 0x00005050
		public static void ExternalEval(string script)
		{
			if (script.Length > 0 && script[script.Length - 1] != ';')
			{
				script += ';';
			}
			Application.Internal_ExternalCall(script);
		}

		// Token: 0x0600021D RID: 541
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_ExternalCall(string script);

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600021E RID: 542
		public static extern string unityVersion { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x0600021F RID: 543
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern int GetBuildUnityVersion();

		// Token: 0x06000220 RID: 544
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern int GetNumericUnityVersion(string version);

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000221 RID: 545
		public static extern bool webSecurityEnabled { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000222 RID: 546
		public static extern string webSecurityHostUrl { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x06000223 RID: 547
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void OpenURL(string url);

		// Token: 0x06000224 RID: 548
		[WrapperlessIcall]
		[Obsolete("For internal use only")]
		[MethodImpl(4096)]
		public static extern void CommitSuicide(int mode);

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000225 RID: 549
		// (set) Token: 0x06000226 RID: 550
		public static extern int targetFrameRate { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000227 RID: 551
		public static extern SystemLanguage systemLanguage { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x06000228 RID: 552 RVA: 0x00006E88 File Offset: 0x00005088
		public static void RegisterLogCallback(Application.LogCallback handler)
		{
			Application.s_LogCallback = handler;
			Application.SetLogCallbackDefined(handler != null, false);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006EA0 File Offset: 0x000050A0
		public static void RegisterLogCallbackThreaded(Application.LogCallback handler)
		{
			Application.s_LogCallback = handler;
			Application.SetLogCallbackDefined(handler != null, true);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00006EB8 File Offset: 0x000050B8
		private static void CallLogCallback(string logString, string stackTrace, LogType type)
		{
			if (Application.s_LogCallback != null)
			{
				Application.s_LogCallback(logString, stackTrace, type);
			}
		}

		// Token: 0x0600022B RID: 555
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void SetLogCallbackDefined(bool defined, bool threaded);

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600022C RID: 556
		// (set) Token: 0x0600022D RID: 557
		public static extern ThreadPriority backgroundLoadingPriority { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600022E RID: 558
		public static extern NetworkReachability internetReachability { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600022F RID: 559
		public static extern bool genuine { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000230 RID: 560
		public static extern bool genuineCheckAvailable { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x06000231 RID: 561
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern AsyncOperation RequestUserAuthorization(UserAuthorization mode);

		// Token: 0x06000232 RID: 562
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool HasUserAuthorization(UserAuthorization mode);

		// Token: 0x06000233 RID: 563
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void ReplyToUserAuthorizationRequest(bool reply, [DefaultValue("false")] bool remember);

		// Token: 0x06000234 RID: 564 RVA: 0x00006ED8 File Offset: 0x000050D8
		[ExcludeFromDocs]
		internal static void ReplyToUserAuthorizationRequest(bool reply)
		{
			bool remember = false;
			Application.ReplyToUserAuthorizationRequest(reply, remember);
		}

		// Token: 0x06000235 RID: 565
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int GetUserAuthorizationRequestMode_Internal();

		// Token: 0x06000236 RID: 566 RVA: 0x00006EF0 File Offset: 0x000050F0
		internal static UserAuthorization GetUserAuthorizationRequestMode()
		{
			return (UserAuthorization)Application.GetUserAuthorizationRequestMode_Internal();
		}

		// Token: 0x04000027 RID: 39
		private static volatile Application.LogCallback s_LogCallback;

		// Token: 0x0200001A RID: 26
		// (Invoke) Token: 0x06000238 RID: 568
		public delegate void LogCallback(string condition, string stackTrace, LogType type);
	}
}
