using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000D0 RID: 208
	public sealed class PlayerPrefs
	{
		// Token: 0x0600085F RID: 2143
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool TrySetInt(string key, int value);

		// Token: 0x06000860 RID: 2144
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool TrySetFloat(string key, float value);

		// Token: 0x06000861 RID: 2145
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool TrySetSetString(string key, string value);

		// Token: 0x06000862 RID: 2146 RVA: 0x000135A8 File Offset: 0x000117A8
		public static void SetInt(string key, int value)
		{
			if (!PlayerPrefs.TrySetInt(key, value))
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06000863 RID: 2147
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int GetInt(string key, [DefaultValue("0")] int defaultValue);

		// Token: 0x06000864 RID: 2148 RVA: 0x000135C4 File Offset: 0x000117C4
		public static void SetFloat(string key, float value)
		{
			if (!PlayerPrefs.TrySetFloat(key, value))
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06000865 RID: 2149
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern float GetFloat(string key, [DefaultValue("0.0F")] float defaultValue);

		// Token: 0x06000866 RID: 2150 RVA: 0x000135E0 File Offset: 0x000117E0
		public static void SetString(string key, string value)
		{
			if (!PlayerPrefs.TrySetSetString(key, value))
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06000867 RID: 2151
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern string GetString(string key, [DefaultValue("\"\"")] string defaultValue);

		// Token: 0x06000868 RID: 2152 RVA: 0x000135FC File Offset: 0x000117FC
		[ExcludeFromDocs]
		public static string GetString(string key)
		{
			string empty = string.Empty;
			return PlayerPrefs.GetString(key, empty);
		}

		// Token: 0x06000869 RID: 2153
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool HasKey(string key);

		// Token: 0x0600086A RID: 2154
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void DeleteKey(string key);
	}
}
