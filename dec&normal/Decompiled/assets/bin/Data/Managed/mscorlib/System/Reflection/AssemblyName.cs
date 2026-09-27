using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;

namespace System.Reflection
{
	// Token: 0x02000186 RID: 390
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_AssemblyName))]
	[Serializable]
	public sealed class AssemblyName : ICloneable, _AssemblyName, IDeserializationCallback, ISerializable
	{
		// Token: 0x06000E95 RID: 3733 RVA: 0x00038E60 File Offset: 0x00037060
		public AssemblyName()
		{
			this.versioncompat = AssemblyVersionCompatibility.SameMachine;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00038E70 File Offset: 0x00037070
		internal AssemblyName(SerializationInfo si, StreamingContext sc)
		{
			this.name = si.GetString("_Name");
			this.codebase = si.GetString("_CodeBase");
			this.version = (Version)si.GetValue("_Version", typeof(Version));
			this.publicKey = (byte[])si.GetValue("_PublicKey", typeof(byte[]));
			this.keyToken = (byte[])si.GetValue("_PublicKeyToken", typeof(byte[]));
			this.hashalg = (AssemblyHashAlgorithm)((int)si.GetValue("_HashAlgorithm", typeof(AssemblyHashAlgorithm)));
			this.keypair = (StrongNameKeyPair)si.GetValue("_StrongNameKeyPair", typeof(StrongNameKeyPair));
			this.versioncompat = (AssemblyVersionCompatibility)((int)si.GetValue("_VersionCompatibility", typeof(AssemblyVersionCompatibility)));
			this.flags = (AssemblyNameFlags)((int)si.GetValue("_Flags", typeof(AssemblyNameFlags)));
			int @int = si.GetInt32("_CultureInfo");
			if (@int != -1)
			{
				this.cultureinfo = new CultureInfo(@int);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x00038FA4 File Offset: 0x000371A4
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x00038FAC File Offset: 0x000371AC
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00038FB8 File Offset: 0x000371B8
		public string CodeBase
		{
			get
			{
				return this.codebase;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x00038FC0 File Offset: 0x000371C0
		// (set) Token: 0x06000E9B RID: 3739 RVA: 0x00038FC8 File Offset: 0x000371C8
		public CultureInfo CultureInfo
		{
			get
			{
				return this.cultureinfo;
			}
			set
			{
				this.cultureinfo = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00038FD4 File Offset: 0x000371D4
		public AssemblyNameFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x00038FDC File Offset: 0x000371DC
		public string FullName
		{
			get
			{
				if (this.name == null)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.name);
				if (this.Version != null)
				{
					stringBuilder.Append(", Version=");
					stringBuilder.Append(this.Version.ToString());
				}
				if (this.cultureinfo != null)
				{
					stringBuilder.Append(", Culture=");
					if (this.cultureinfo.LCID == CultureInfo.InvariantCulture.LCID)
					{
						stringBuilder.Append("neutral");
					}
					else
					{
						stringBuilder.Append(this.cultureinfo.Name);
					}
				}
				byte[] array = this.InternalGetPublicKeyToken();
				if (array != null)
				{
					if (array.Length == 0)
					{
						stringBuilder.Append(", PublicKeyToken=null");
					}
					else
					{
						stringBuilder.Append(", PublicKeyToken=");
						for (int i = 0; i < array.Length; i++)
						{
							stringBuilder.Append(array[i].ToString("x2"));
						}
					}
				}
				if ((this.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
				{
					stringBuilder.Append(", Retargetable=Yes");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00039114 File Offset: 0x00037314
		public StrongNameKeyPair KeyPair
		{
			get
			{
				return this.keypair;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x0003911C File Offset: 0x0003731C
		// (set) Token: 0x06000EA0 RID: 3744 RVA: 0x00039124 File Offset: 0x00037324
		public Version Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
				if (value == null)
				{
					this.major = (this.minor = (this.build = (this.revision = 0)));
				}
				else
				{
					this.major = value.Major;
					this.minor = value.Minor;
					this.build = value.Build;
					this.revision = value.Revision;
				}
			}
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003919C File Offset: 0x0003739C
		public override string ToString()
		{
			string fullName = this.FullName;
			return (fullName == null) ? base.ToString() : fullName;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x000391C4 File Offset: 0x000373C4
		public byte[] GetPublicKey()
		{
			return this.publicKey;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x000391CC File Offset: 0x000373CC
		public byte[] GetPublicKeyToken()
		{
			if (this.keyToken != null)
			{
				return this.keyToken;
			}
			if (this.publicKey == null)
			{
				return null;
			}
			if (this.publicKey.Length == 0)
			{
				return new byte[0];
			}
			if (!this.IsPublicKeyValid)
			{
				throw new SecurityException("The public key is not valid.");
			}
			this.keyToken = this.ComputePublicKeyToken();
			return this.keyToken;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00039234 File Offset: 0x00037434
		private bool IsPublicKeyValid
		{
			get
			{
				if (this.publicKey.Length == 16)
				{
					int i = 0;
					int num = 0;
					while (i < this.publicKey.Length)
					{
						num += (int)this.publicKey[i++];
					}
					if (num == 4)
					{
						return true;
					}
				}
				byte b = this.publicKey[0];
				if (b != 6)
				{
					if (b != 7)
					{
						if (b == 0)
						{
							if (this.publicKey.Length > 12 && this.publicKey[12] == 6)
							{
								try
								{
									CryptoConvert.FromCapiPublicKeyBlob(this.publicKey, 12);
									return true;
								}
								catch (CryptographicException)
								{
								}
							}
						}
					}
				}
				else
				{
					try
					{
						CryptoConvert.FromCapiPublicKeyBlob(this.publicKey);
						return true;
					}
					catch (CryptographicException)
					{
					}
				}
				return false;
			}
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0003932C File Offset: 0x0003752C
		private byte[] InternalGetPublicKeyToken()
		{
			if (this.keyToken != null)
			{
				return this.keyToken;
			}
			if (this.publicKey == null)
			{
				return null;
			}
			if (this.publicKey.Length == 0)
			{
				return new byte[0];
			}
			if (!this.IsPublicKeyValid)
			{
				throw new SecurityException("The public key is not valid.");
			}
			return this.ComputePublicKeyToken();
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00039388 File Offset: 0x00037588
		private byte[] ComputePublicKeyToken()
		{
			HashAlgorithm hashAlgorithm = SHA1.Create();
			byte[] array = hashAlgorithm.ComputeHash(this.publicKey);
			byte[] array2 = new byte[8];
			Array.Copy(array, array.Length - 8, array2, 0, 8);
			Array.Reverse(array2, 0, 8);
			return array2;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x000393C8 File Offset: 0x000375C8
		public void SetPublicKey(byte[] publicKey)
		{
			if (publicKey == null)
			{
				this.flags ^= AssemblyNameFlags.PublicKey;
			}
			else
			{
				this.flags |= AssemblyNameFlags.PublicKey;
			}
			this.publicKey = publicKey;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x000393F8 File Offset: 0x000375F8
		public void SetPublicKeyToken(byte[] publicKeyToken)
		{
			this.keyToken = publicKeyToken;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00039404 File Offset: 0x00037604
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("_Name", this.name);
			info.AddValue("_PublicKey", this.publicKey);
			info.AddValue("_PublicKeyToken", this.keyToken);
			info.AddValue("_CultureInfo", (this.cultureinfo == null) ? -1 : this.cultureinfo.LCID);
			info.AddValue("_CodeBase", this.codebase);
			info.AddValue("_Version", this.Version);
			info.AddValue("_HashAlgorithm", this.hashalg);
			info.AddValue("_HashAlgorithmForControl", AssemblyHashAlgorithm.None);
			info.AddValue("_StrongNameKeyPair", this.keypair);
			info.AddValue("_VersionCompatibility", this.versioncompat);
			info.AddValue("_Flags", this.flags);
			info.AddValue("_HashForControl", null);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00039510 File Offset: 0x00037710
		public object Clone()
		{
			return new AssemblyName
			{
				name = this.name,
				codebase = this.codebase,
				major = this.major,
				minor = this.minor,
				build = this.build,
				revision = this.revision,
				version = this.version,
				cultureinfo = this.cultureinfo,
				flags = this.flags,
				hashalg = this.hashalg,
				keypair = this.keypair,
				publicKey = this.publicKey,
				keyToken = this.keyToken,
				versioncompat = this.versioncompat
			};
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x000395CC File Offset: 0x000377CC
		public void OnDeserialization(object sender)
		{
			this.Version = this.version;
		}

		// Token: 0x040005F4 RID: 1524
		private string name;

		// Token: 0x040005F5 RID: 1525
		private string codebase;

		// Token: 0x040005F6 RID: 1526
		private int major;

		// Token: 0x040005F7 RID: 1527
		private int minor;

		// Token: 0x040005F8 RID: 1528
		private int build;

		// Token: 0x040005F9 RID: 1529
		private int revision;

		// Token: 0x040005FA RID: 1530
		private CultureInfo cultureinfo;

		// Token: 0x040005FB RID: 1531
		private AssemblyNameFlags flags;

		// Token: 0x040005FC RID: 1532
		private AssemblyHashAlgorithm hashalg;

		// Token: 0x040005FD RID: 1533
		private StrongNameKeyPair keypair;

		// Token: 0x040005FE RID: 1534
		private byte[] publicKey;

		// Token: 0x040005FF RID: 1535
		private byte[] keyToken;

		// Token: 0x04000600 RID: 1536
		private AssemblyVersionCompatibility versioncompat;

		// Token: 0x04000601 RID: 1537
		private Version version;

		// Token: 0x04000602 RID: 1538
		private ProcessorArchitecture processor_architecture;
	}
}
