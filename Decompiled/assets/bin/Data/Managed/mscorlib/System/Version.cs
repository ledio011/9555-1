using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020003DD RID: 989
	[ComVisible(true)]
	[Serializable]
	public sealed class Version : IComparable<Version>, IEquatable<Version>, ICloneable, IComparable
	{
		// Token: 0x06001E9E RID: 7838 RVA: 0x000725EC File Offset: 0x000707EC
		public Version()
		{
			this.CheckedSet(2, 0, 0, -1, -1);
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x00072600 File Offset: 0x00070800
		public Version(string version)
		{
			int major = -1;
			int minor = -1;
			int build = -1;
			int revision = -1;
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			string[] array = version.Split(new char[]
			{
				'.'
			});
			int num = array.Length;
			if (num < 2 || num > 4)
			{
				throw new ArgumentException(Locale.GetText("There must be 2, 3 or 4 components in the version string."));
			}
			if (num > 0)
			{
				major = int.Parse(array[0]);
			}
			if (num > 1)
			{
				minor = int.Parse(array[1]);
			}
			if (num > 2)
			{
				build = int.Parse(array[2]);
			}
			if (num > 3)
			{
				revision = int.Parse(array[3]);
			}
			this.CheckedSet(num, major, minor, build, revision);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000726B4 File Offset: 0x000708B4
		public Version(int major, int minor)
		{
			this.CheckedSet(2, major, minor, 0, 0);
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x000726C8 File Offset: 0x000708C8
		public Version(int major, int minor, int build)
		{
			this.CheckedSet(3, major, minor, build, 0);
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000726DC File Offset: 0x000708DC
		public Version(int major, int minor, int build, int revision)
		{
			this.CheckedSet(4, major, minor, build, revision);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000726F0 File Offset: 0x000708F0
		private void CheckedSet(int defined, int major, int minor, int build, int revision)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major");
			}
			this._Major = major;
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor");
			}
			this._Minor = minor;
			if (defined == 2)
			{
				this._Build = -1;
				this._Revision = -1;
				return;
			}
			if (build < 0)
			{
				throw new ArgumentOutOfRangeException("build");
			}
			this._Build = build;
			if (defined == 3)
			{
				this._Revision = -1;
				return;
			}
			if (revision < 0)
			{
				throw new ArgumentOutOfRangeException("revision");
			}
			this._Revision = revision;
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x0007278C File Offset: 0x0007098C
		public int Build
		{
			get
			{
				return this._Build;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x00072794 File Offset: 0x00070994
		public int Major
		{
			get
			{
				return this._Major;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x0007279C File Offset: 0x0007099C
		public int Minor
		{
			get
			{
				return this._Minor;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x000727A4 File Offset: 0x000709A4
		public int Revision
		{
			get
			{
				return this._Revision;
			}
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000727AC File Offset: 0x000709AC
		public object Clone()
		{
			if (this._Build == -1)
			{
				return new Version(this._Major, this._Minor);
			}
			if (this._Revision == -1)
			{
				return new Version(this._Major, this._Minor, this._Build);
			}
			return new Version(this._Major, this._Minor, this._Build, this._Revision);
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x00072818 File Offset: 0x00070A18
		public int CompareTo(object version)
		{
			if (version == null)
			{
				return 1;
			}
			if (!(version is Version))
			{
				throw new ArgumentException(Locale.GetText("Argument to Version.CompareTo must be a Version."));
			}
			return this.CompareTo((Version)version);
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0007284C File Offset: 0x00070A4C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Version);
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0007285C File Offset: 0x00070A5C
		public int CompareTo(Version value)
		{
			if (value == null)
			{
				return 1;
			}
			if (this._Major > value._Major)
			{
				return 1;
			}
			if (this._Major < value._Major)
			{
				return -1;
			}
			if (this._Minor > value._Minor)
			{
				return 1;
			}
			if (this._Minor < value._Minor)
			{
				return -1;
			}
			if (this._Build > value._Build)
			{
				return 1;
			}
			if (this._Build < value._Build)
			{
				return -1;
			}
			if (this._Revision > value._Revision)
			{
				return 1;
			}
			if (this._Revision < value._Revision)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x00072910 File Offset: 0x00070B10
		public bool Equals(Version obj)
		{
			return obj != null && obj._Major == this._Major && obj._Minor == this._Minor && obj._Build == this._Build && obj._Revision == this._Revision;
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00072970 File Offset: 0x00070B70
		public override int GetHashCode()
		{
			return this._Revision << 24 | this._Build << 16 | this._Minor << 8 | this._Major;
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x00072998 File Offset: 0x00070B98
		public override string ToString()
		{
			string text = this._Major.ToString() + "." + this._Minor.ToString();
			if (this._Build != -1)
			{
				text = text + "." + this._Build.ToString();
			}
			if (this._Revision != -1)
			{
				text = text + "." + this._Revision.ToString();
			}
			return text;
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x00072A10 File Offset: 0x00070C10
		internal static Version CreateFromString(string info)
		{
			int major = 0;
			int minor = 0;
			int build = 0;
			int revision = 0;
			int num = 1;
			int num2 = -1;
			if (info == null)
			{
				return new Version(0, 0, 0, 0);
			}
			foreach (char c in info)
			{
				if (char.IsDigit(c))
				{
					if (num2 < 0)
					{
						num2 = (int)(c - '0');
					}
					else
					{
						num2 = num2 * 10 + (int)(c - '0');
					}
				}
				else if (num2 >= 0)
				{
					switch (num)
					{
					case 1:
						major = num2;
						break;
					case 2:
						minor = num2;
						break;
					case 3:
						build = num2;
						break;
					case 4:
						revision = num2;
						break;
					}
					num2 = -1;
					num++;
				}
				if (num == 5)
				{
					break;
				}
			}
			if (num2 >= 0)
			{
				switch (num)
				{
				case 1:
					major = num2;
					break;
				case 2:
					minor = num2;
					break;
				case 3:
					build = num2;
					break;
				case 4:
					revision = num2;
					break;
				}
			}
			return new Version(major, minor, build, revision);
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x00072B48 File Offset: 0x00070D48
		public static bool operator ==(Version v1, Version v2)
		{
			return object.Equals(v1, v2);
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x00072B54 File Offset: 0x00070D54
		public static bool operator !=(Version v1, Version v2)
		{
			return !object.Equals(v1, v2);
		}

		// Token: 0x04000FCF RID: 4047
		private const int UNDEFINED = -1;

		// Token: 0x04000FD0 RID: 4048
		private int _Major;

		// Token: 0x04000FD1 RID: 4049
		private int _Minor;

		// Token: 0x04000FD2 RID: 4050
		private int _Build;

		// Token: 0x04000FD3 RID: 4051
		private int _Revision;
	}
}
