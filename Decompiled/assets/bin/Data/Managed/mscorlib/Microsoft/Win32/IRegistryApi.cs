using System;

namespace Microsoft.Win32
{
	// Token: 0x02000022 RID: 34
	internal interface IRegistryApi
	{
		// Token: 0x06000006 RID: 6
		RegistryKey OpenSubKey(RegistryKey rkey, string keyname, bool writtable);

		// Token: 0x06000007 RID: 7
		void Flush(RegistryKey rkey);

		// Token: 0x06000008 RID: 8
		void Close(RegistryKey rkey);

		// Token: 0x06000009 RID: 9
		object GetValue(RegistryKey rkey, string name, object default_value, RegistryValueOptions options);

		// Token: 0x0600000A RID: 10
		void SetValue(RegistryKey rkey, string name, object value);

		// Token: 0x0600000B RID: 11
		void DeleteValue(RegistryKey rkey, string value, bool throw_if_missing);

		// Token: 0x0600000C RID: 12
		string[] GetValueNames(RegistryKey rkey);

		// Token: 0x0600000D RID: 13
		string ToString(RegistryKey rkey);
	}
}
