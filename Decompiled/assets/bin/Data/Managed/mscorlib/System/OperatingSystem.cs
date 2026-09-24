using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200016F RID: 367
	[ComVisible(true)]
	[Serializable]
	public sealed class OperatingSystem : ICloneable, ISerializable
	{
		// Token: 0x06000E02 RID: 3586 RVA: 0x00037D2C File Offset: 0x00035F2C
		public OperatingSystem(PlatformID platform, Version version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._platform = platform;
			this._version = version;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00037D64 File Offset: 0x00035F64
		public PlatformID Platform
		{
			get
			{
				return this._platform;
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00037D6C File Offset: 0x00035F6C
		public object Clone()
		{
			return new OperatingSystem(this._platform, this._version);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00037D80 File Offset: 0x00035F80
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_platform", this._platform);
			info.AddValue("_version", this._version);
			info.AddValue("_servicePack", this._servicePack);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00037DBC File Offset: 0x00035FBC
		public override string ToString()
		{
			int platform = (int)this._platform;
			string str;
			switch (platform)
			{
			case 0:
				str = "Microsoft Win32S";
				goto IL_96;
			case 1:
				str = "Microsoft Windows 98";
				goto IL_96;
			case 2:
				str = "Microsoft Windows NT";
				goto IL_96;
			case 3:
				str = "Microsoft Windows CE";
				goto IL_96;
			case 4:
				break;
			case 5:
				str = "XBox";
				goto IL_96;
			case 6:
				str = "OSX";
				goto IL_96;
			default:
				if (platform != 128)
				{
					str = Locale.GetText("<unknown>");
					goto IL_96;
				}
				break;
			}
			str = "Unix";
			IL_96:
			return str + " " + this._version.ToString();
		}

		// Token: 0x040005C9 RID: 1481
		private PlatformID _platform;

		// Token: 0x040005CA RID: 1482
		private Version _version;

		// Token: 0x040005CB RID: 1483
		private string _servicePack = string.Empty;
	}
}
