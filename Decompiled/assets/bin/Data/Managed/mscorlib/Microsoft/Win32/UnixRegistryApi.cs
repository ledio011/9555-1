using System;
using System.Globalization;

namespace Microsoft.Win32
{
	// Token: 0x0200002C RID: 44
	internal class UnixRegistryApi : IRegistryApi
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000032F4 File Offset: 0x000014F4
		private static string ToUnix(string keyname)
		{
			if (keyname.IndexOf('\\') != -1)
			{
				keyname = keyname.Replace('\\', '/');
			}
			return keyname.ToLower();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003318 File Offset: 0x00001518
		private static bool IsWellKnownKey(string parentKeyName, string keyname)
		{
			return (parentKeyName == Registry.CurrentUser.Name || parentKeyName == Registry.LocalMachine.Name) && 0 == string.Compare("software", keyname, true, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003368 File Offset: 0x00001568
		public RegistryKey OpenSubKey(RegistryKey rkey, string keyname, bool writable)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				return null;
			}
			RegistryKey registryKey = keyHandler.Probe(rkey, UnixRegistryApi.ToUnix(keyname), writable);
			if (registryKey == null && UnixRegistryApi.IsWellKnownKey(rkey.Name, keyname))
			{
				registryKey = this.CreateSubKey(rkey, keyname, writable);
			}
			return registryKey;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000033B8 File Offset: 0x000015B8
		public void Flush(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, false);
			if (keyHandler == null)
			{
				return;
			}
			keyHandler.Flush();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000033DC File Offset: 0x000015DC
		public void Close(RegistryKey rkey)
		{
			KeyHandler.Drop(rkey);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000033E4 File Offset: 0x000015E4
		public object GetValue(RegistryKey rkey, string name, object default_value, RegistryValueOptions options)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				return default_value;
			}
			if (keyHandler.ValueExists(name))
			{
				return keyHandler.GetValue(name, options);
			}
			return default_value;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003418 File Offset: 0x00001618
		public void SetValue(RegistryKey rkey, string name, object value)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			keyHandler.SetValue(name, value);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003444 File Offset: 0x00001644
		public void DeleteValue(RegistryKey rkey, string name, bool throw_if_missing)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				return;
			}
			if (throw_if_missing && !keyHandler.ValueExists(name))
			{
				throw new ArgumentException("the given value does not exist");
			}
			keyHandler.RemoveValue(name);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003484 File Offset: 0x00001684
		public string[] GetValueNames(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			return keyHandler.GetValueNames();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000034AC File Offset: 0x000016AC
		public string ToString(RegistryKey rkey)
		{
			return rkey.Name;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000034B4 File Offset: 0x000016B4
		private RegistryKey CreateSubKey(RegistryKey rkey, string keyname, bool writable)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			return keyHandler.Ensure(rkey, UnixRegistryApi.ToUnix(keyname), writable);
		}
	}
}
