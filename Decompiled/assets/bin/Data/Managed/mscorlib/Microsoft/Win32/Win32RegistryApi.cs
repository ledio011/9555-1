using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Microsoft.Win32
{
	// Token: 0x0200002D RID: 45
	internal class Win32RegistryApi : IRegistryApi
	{
		// Token: 0x06000052 RID: 82
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegCloseKey(IntPtr keyHandle);

		// Token: 0x06000053 RID: 83
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegFlushKey(IntPtr keyHandle);

		// Token: 0x06000054 RID: 84
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegOpenKeyEx(IntPtr keyBase, string keyName, IntPtr reserved, int access, out IntPtr keyHandle);

		// Token: 0x06000055 RID: 85
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegDeleteValue(IntPtr keyHandle, string valueName);

		// Token: 0x06000056 RID: 86
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegEnumValue(IntPtr keyBase, int index, StringBuilder nameBuffer, ref int nameLength, IntPtr reserved, ref RegistryValueKind type, IntPtr data, IntPtr dataLength);

		// Token: 0x06000057 RID: 87
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegSetValueEx(IntPtr keyBase, string valueName, IntPtr reserved, RegistryValueKind type, string data, int rawDataLength);

		// Token: 0x06000058 RID: 88
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegSetValueEx(IntPtr keyBase, string valueName, IntPtr reserved, RegistryValueKind type, byte[] rawData, int rawDataLength);

		// Token: 0x06000059 RID: 89
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegSetValueEx(IntPtr keyBase, string valueName, IntPtr reserved, RegistryValueKind type, ref int data, int rawDataLength);

		// Token: 0x0600005A RID: 90
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, IntPtr zero, ref int dataSize);

		// Token: 0x0600005B RID: 91
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, [Out] byte[] data, ref int dataSize);

		// Token: 0x0600005C RID: 92
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, ref int data, ref int dataSize);

		// Token: 0x0600005D RID: 93 RVA: 0x000034F8 File Offset: 0x000016F8
		private static IntPtr GetHandle(RegistryKey key)
		{
			return (IntPtr)key.Handle;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003508 File Offset: 0x00001708
		private static bool IsHandleValid(RegistryKey key)
		{
			return key.Handle != null;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003518 File Offset: 0x00001718
		public object GetValue(RegistryKey rkey, string name, object defaultValue, RegistryValueOptions options)
		{
			RegistryValueKind registryValueKind = RegistryValueKind.Unknown;
			int size = 0;
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			int num = Win32RegistryApi.RegQueryValueEx(handle, name, IntPtr.Zero, ref registryValueKind, IntPtr.Zero, ref size);
			if (num == 2 || num == 1018)
			{
				return defaultValue;
			}
			if (num != 234 && num != 0)
			{
				this.GenerateException(num);
			}
			object obj;
			if (registryValueKind == RegistryValueKind.String)
			{
				byte[] data;
				num = this.GetBinaryValue(rkey, name, registryValueKind, out data, size);
				obj = RegistryKey.DecodeString(data);
			}
			else if (registryValueKind == RegistryValueKind.ExpandString)
			{
				byte[] data2;
				num = this.GetBinaryValue(rkey, name, registryValueKind, out data2, size);
				obj = RegistryKey.DecodeString(data2);
				if ((options & RegistryValueOptions.DoNotExpandEnvironmentNames) == RegistryValueOptions.None)
				{
					obj = Environment.ExpandEnvironmentVariables((string)obj);
				}
			}
			else if (registryValueKind == RegistryValueKind.DWord)
			{
				int num2 = 0;
				num = Win32RegistryApi.RegQueryValueEx(handle, name, IntPtr.Zero, ref registryValueKind, ref num2, ref size);
				obj = num2;
			}
			else if (registryValueKind == RegistryValueKind.Binary)
			{
				byte[] array;
				num = this.GetBinaryValue(rkey, name, registryValueKind, out array, size);
				obj = array;
			}
			else
			{
				if (registryValueKind != RegistryValueKind.MultiString)
				{
					throw new SystemException();
				}
				obj = null;
				byte[] data3;
				num = this.GetBinaryValue(rkey, name, registryValueKind, out data3, size);
				if (num == 0)
				{
					obj = RegistryKey.DecodeString(data3).Split(new char[1]);
				}
			}
			if (num != 0)
			{
				this.GenerateException(num);
			}
			return obj;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003668 File Offset: 0x00001868
		public void SetValue(RegistryKey rkey, string name, object value)
		{
			Type type = value.GetType();
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			int num2;
			if (type == typeof(int))
			{
				int num = (int)value;
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.DWord, ref num, 4);
			}
			else if (type == typeof(byte[]))
			{
				byte[] array = (byte[])value;
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.Binary, array, array.Length);
			}
			else if (type == typeof(string[]))
			{
				string[] array2 = (string[])value;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value2 in array2)
				{
					stringBuilder.Append(value2);
					stringBuilder.Append('\0');
				}
				stringBuilder.Append('\0');
				byte[] bytes = Encoding.Unicode.GetBytes(stringBuilder.ToString());
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.MultiString, bytes, bytes.Length);
			}
			else
			{
				if (type.IsArray)
				{
					throw new ArgumentException("Only string and byte arrays can written as registry values");
				}
				string text = string.Format("{0}{1}", value, '\0');
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.String, text, text.Length * this.NativeBytesPerCharacter);
			}
			if (num2 == 1018)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			if (num2 != 0)
			{
				this.GenerateException(num2);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000037D4 File Offset: 0x000019D4
		private int GetBinaryValue(RegistryKey rkey, string name, RegistryValueKind type, out byte[] data, int size)
		{
			byte[] array = new byte[size];
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			int result = Win32RegistryApi.RegQueryValueEx(handle, name, IntPtr.Zero, ref type, array, ref size);
			data = array;
			return result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003808 File Offset: 0x00001A08
		public RegistryKey OpenSubKey(RegistryKey rkey, string keyName, bool writable)
		{
			int num = 131097;
			if (writable)
			{
				num |= 131078;
			}
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			IntPtr intPtr;
			int num2 = Win32RegistryApi.RegOpenKeyEx(handle, keyName, IntPtr.Zero, num, out intPtr);
			if (num2 == 2 || num2 == 1018)
			{
				return null;
			}
			if (num2 != 0)
			{
				this.GenerateException(num2);
			}
			return new RegistryKey(intPtr, Win32RegistryApi.CombineName(rkey, keyName), writable);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003874 File Offset: 0x00001A74
		public void Flush(RegistryKey rkey)
		{
			if (!Win32RegistryApi.IsHandleValid(rkey))
			{
				return;
			}
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			Win32RegistryApi.RegFlushKey(handle);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000389C File Offset: 0x00001A9C
		public void Close(RegistryKey rkey)
		{
			if (!Win32RegistryApi.IsHandleValid(rkey))
			{
				return;
			}
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			Win32RegistryApi.RegCloseKey(handle);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000038C4 File Offset: 0x00001AC4
		public void DeleteValue(RegistryKey rkey, string value, bool shouldThrowWhenKeyMissing)
		{
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			int num = Win32RegistryApi.RegDeleteValue(handle, value);
			if (num == 1018)
			{
				return;
			}
			if (num != 2)
			{
				if (num != 0)
				{
					this.GenerateException(num);
				}
				return;
			}
			if (shouldThrowWhenKeyMissing)
			{
				throw new ArgumentException("value " + value);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003918 File Offset: 0x00001B18
		public string[] GetValueNames(RegistryKey rkey)
		{
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			ArrayList arrayList = new ArrayList();
			int num = 0;
			for (;;)
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				int capacity = stringBuilder.Capacity;
				RegistryValueKind registryValueKind = RegistryValueKind.Unknown;
				int num2 = Win32RegistryApi.RegEnumValue(handle, num, stringBuilder, ref capacity, IntPtr.Zero, ref registryValueKind, IntPtr.Zero, IntPtr.Zero);
				if (num2 == 0 || num2 == 234)
				{
					arrayList.Add(stringBuilder.ToString());
				}
				else
				{
					if (num2 == 259)
					{
						break;
					}
					if (num2 == 1018)
					{
						goto Block_3;
					}
					this.GenerateException(num2);
				}
				num++;
			}
			return (string[])arrayList.ToArray(typeof(string));
			Block_3:
			throw RegistryKey.CreateMarkedForDeletionException();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000039DC File Offset: 0x00001BDC
		private void GenerateException(int errorCode)
		{
			switch (errorCode)
			{
			case 2:
				break;
			default:
				if (errorCode == 53)
				{
					throw new IOException("The network path was not found.");
				}
				if (errorCode != 87)
				{
					throw new SystemException();
				}
				break;
			case 5:
				throw new SecurityException();
			}
			throw new ArgumentException();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003A34 File Offset: 0x00001C34
		public string ToString(RegistryKey rkey)
		{
			return rkey.Name;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003A3C File Offset: 0x00001C3C
		internal static string CombineName(RegistryKey rkey, string localName)
		{
			return rkey.Name + "\\" + localName;
		}

		// Token: 0x0400006F RID: 111
		private const int OpenRegKeyRead = 131097;

		// Token: 0x04000070 RID: 112
		private const int OpenRegKeyWrite = 131078;

		// Token: 0x04000071 RID: 113
		private const int Int32ByteSize = 4;

		// Token: 0x04000072 RID: 114
		private const int BufferMaxLength = 1024;

		// Token: 0x04000073 RID: 115
		private readonly int NativeBytesPerCharacter = Marshal.SystemDefaultCharSize;
	}
}
