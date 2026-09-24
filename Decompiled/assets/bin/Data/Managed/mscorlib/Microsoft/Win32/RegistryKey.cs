using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Win32
{
	// Token: 0x02000026 RID: 38
	[ComVisible(true)]
	public sealed class RegistryKey : MarshalByRefObject, IDisposable
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002EFC File Offset: 0x000010FC
		internal RegistryKey(RegistryHive hiveId) : this(hiveId, new IntPtr((int)hiveId), false)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002F0C File Offset: 0x0000110C
		internal RegistryKey(RegistryHive hiveId, IntPtr keyHandle, bool remoteRoot)
		{
			this.hive = hiveId;
			this.handle = keyHandle;
			this.qname = RegistryKey.GetHiveName(hiveId);
			this.isRemoteRoot = remoteRoot;
			this.isWritable = true;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002F48 File Offset: 0x00001148
		internal RegistryKey(object data, string keyName, bool writable)
		{
			this.handle = data;
			this.qname = keyName;
			this.isWritable = writable;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002F68 File Offset: 0x00001168
		static RegistryKey()
		{
			if (Path.DirectorySeparatorChar == '\\')
			{
				RegistryKey.RegistryApi = new Win32RegistryApi();
			}
			else
			{
				RegistryKey.RegistryApi = new UnixRegistryApi();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002F90 File Offset: 0x00001190
		void IDisposable.Dispose()
		{
			GC.SuppressFinalize(this);
			this.Close();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002FA0 File Offset: 0x000011A0
		~RegistryKey()
		{
			this.Close();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002FD0 File Offset: 0x000011D0
		public string Name
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002FD8 File Offset: 0x000011D8
		public void Flush()
		{
			RegistryKey.RegistryApi.Flush(this);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002FE8 File Offset: 0x000011E8
		public void Close()
		{
			this.Flush();
			if (!this.isRemoteRoot && this.IsRoot)
			{
				return;
			}
			RegistryKey.RegistryApi.Close(this);
			this.handle = null;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000301C File Offset: 0x0000121C
		public void SetValue(string name, object value)
		{
			this.AssertKeyStillValid();
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name != null)
			{
				this.AssertKeyNameLength(name);
			}
			if (!this.IsWritable)
			{
				throw new UnauthorizedAccessException("Cannot write to the registry key.");
			}
			RegistryKey.RegistryApi.SetValue(this, name, value);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003070 File Offset: 0x00001270
		public RegistryKey OpenSubKey(string name)
		{
			return this.OpenSubKey(name, false);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000307C File Offset: 0x0000127C
		public RegistryKey OpenSubKey(string name, bool writable)
		{
			this.AssertKeyStillValid();
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.AssertKeyNameLength(name);
			return RegistryKey.RegistryApi.OpenSubKey(this, name, writable);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000030AC File Offset: 0x000012AC
		public object GetValue(string name)
		{
			return this.GetValue(name, null);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000030B8 File Offset: 0x000012B8
		public object GetValue(string name, object defaultValue)
		{
			this.AssertKeyStillValid();
			return RegistryKey.RegistryApi.GetValue(this, name, defaultValue, RegistryValueOptions.None);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000030D0 File Offset: 0x000012D0
		public void DeleteValue(string name, bool throwOnMissingValue)
		{
			this.AssertKeyStillValid();
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (!this.IsWritable)
			{
				throw new UnauthorizedAccessException("Cannot write to the registry key.");
			}
			RegistryKey.RegistryApi.DeleteValue(this, name, throwOnMissingValue);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000310C File Offset: 0x0000130C
		public string[] GetValueNames()
		{
			this.AssertKeyStillValid();
			return RegistryKey.RegistryApi.GetValueNames(this);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003120 File Offset: 0x00001320
		public override string ToString()
		{
			this.AssertKeyStillValid();
			return RegistryKey.RegistryApi.ToString(this);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003134 File Offset: 0x00001334
		internal bool IsRoot
		{
			get
			{
				return this.hive != null;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003144 File Offset: 0x00001344
		private bool IsWritable
		{
			get
			{
				return this.isWritable;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000314C File Offset: 0x0000134C
		internal RegistryHive Hive
		{
			get
			{
				if (!this.IsRoot)
				{
					throw new NotSupportedException();
				}
				return (RegistryHive)((int)this.hive);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000316C File Offset: 0x0000136C
		internal object Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003174 File Offset: 0x00001374
		private void AssertKeyStillValid()
		{
			if (this.handle == null)
			{
				throw new ObjectDisposedException("Microsoft.Win32.RegistryKey");
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000318C File Offset: 0x0000138C
		private void AssertKeyNameLength(string name)
		{
			if (name.Length > 255)
			{
				throw new ArgumentException("Name of registry key cannot be greater than 255 characters");
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000031AC File Offset: 0x000013AC
		internal static string DecodeString(byte[] data)
		{
			string text = Encoding.Unicode.GetString(data);
			int num = text.IndexOf('\0');
			if (num != -1)
			{
				text = text.TrimEnd(new char[1]);
			}
			return text;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000031E4 File Offset: 0x000013E4
		internal static IOException CreateMarkedForDeletionException()
		{
			throw new IOException("Illegal operation attempted on a registry key that has been marked for deletion.");
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000031F0 File Offset: 0x000013F0
		private static string GetHiveName(RegistryHive hive)
		{
			switch (hive + -2147483648)
			{
			case (RegistryHive)0:
				return "HKEY_CLASSES_ROOT";
			case (RegistryHive)1:
				return "HKEY_CURRENT_USER";
			case (RegistryHive)2:
				return "HKEY_LOCAL_MACHINE";
			case (RegistryHive)3:
				return "HKEY_USERS";
			case (RegistryHive)4:
				return "HKEY_PERFORMANCE_DATA";
			case (RegistryHive)5:
				return "HKEY_CURRENT_CONFIG";
			case (RegistryHive)6:
				return "HKEY_DYN_DATA";
			default:
				throw new NotImplementedException(string.Format("Registry hive '{0}' is not implemented.", hive.ToString()));
			}
		}

		// Token: 0x0400005E RID: 94
		private object handle;

		// Token: 0x0400005F RID: 95
		private object hive;

		// Token: 0x04000060 RID: 96
		private readonly string qname;

		// Token: 0x04000061 RID: 97
		private readonly bool isRemoteRoot;

		// Token: 0x04000062 RID: 98
		private readonly bool isWritable;

		// Token: 0x04000063 RID: 99
		private static readonly IRegistryApi RegistryApi;
	}
}
