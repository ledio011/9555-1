using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Mono.Security;

namespace System.Reflection.Emit
{
	// Token: 0x02000192 RID: 402
	[ComDefaultInterface(typeof(_AssemblyBuilder))]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	public sealed class AssemblyBuilder : Assembly, _AssemblyBuilder
	{
		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003A7D4 File Offset: 0x000389D4
		internal AssemblyBuilder(AssemblyName n, string directory, AssemblyBuilderAccess access, bool corlib_internal)
		{
			this.is_compiler_context = ((access & (AssemblyBuilderAccess)2048) != (AssemblyBuilderAccess)0);
			access &= (AssemblyBuilderAccess)(-2049);
			if (!Enum.IsDefined(typeof(AssemblyBuilderAccess), access))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument value {0} is not valid.", new object[]
				{
					(int)access
				}), "access");
			}
			this.name = n.Name;
			this.access = (uint)access;
			this.flags = (uint)n.Flags;
			if (this.IsSave && (directory == null || directory.Length == 0))
			{
				this.dir = Directory.GetCurrentDirectory();
			}
			else
			{
				this.dir = directory;
			}
			if (n.CultureInfo != null)
			{
				this.culture = n.CultureInfo.Name;
				this.versioninfo_culture = n.CultureInfo.Name;
			}
			Version version = n.Version;
			if (version != null)
			{
				this.version = version.ToString();
			}
			if (n.KeyPair != null)
			{
				this.sn = n.KeyPair.StrongName();
			}
			else
			{
				byte[] publicKey = n.GetPublicKey();
				if (publicKey != null && publicKey.Length > 0)
				{
					this.sn = new StrongName(publicKey);
				}
			}
			if (this.sn != null)
			{
				this.flags |= 1U;
			}
			this.corlib_internal = corlib_internal;
			if (this.sn != null)
			{
				this.pktoken = new byte[this.sn.PublicKeyToken.Length * 2];
				int num = 0;
				foreach (byte b in this.sn.PublicKeyToken)
				{
					string text = b.ToString("x2");
					this.pktoken[num++] = (byte)text[0];
					this.pktoken[num++] = (byte)text[1];
				}
			}
			AssemblyBuilder.basic_init(this);
		}

		// Token: 0x06000ED7 RID: 3799
		[MethodImpl(4096)]
		private static extern void basic_init(AssemblyBuilder ab);

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x0003AA20 File Offset: 0x00038C20
		public override string CodeBase
		{
			get
			{
				throw this.not_supported();
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0003AA28 File Offset: 0x00038C28
		public override MethodInfo EntryPoint
		{
			get
			{
				return this.entry_point;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0003AA30 File Offset: 0x00038C30
		public override string Location
		{
			get
			{
				throw this.not_supported();
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0003AA38 File Offset: 0x00038C38
		public override string ImageRuntimeVersion
		{
			get
			{
				return base.ImageRuntimeVersion;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0003AA40 File Offset: 0x00038C40
		[MonoTODO]
		public override bool ReflectionOnly
		{
			get
			{
				return base.ReflectionOnly;
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0003AA48 File Offset: 0x00038C48
		internal void AddPermissionRequests(PermissionSet required, PermissionSet optional, PermissionSet refused)
		{
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0003AA4C File Offset: 0x00038C4C
		public ModuleBuilder DefineDynamicModule(string name)
		{
			return this.DefineDynamicModule(name, name, false, true);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0003AA58 File Offset: 0x00038C58
		public ModuleBuilder DefineDynamicModule(string name, bool emitSymbolInfo)
		{
			return this.DefineDynamicModule(name, name, emitSymbolInfo, true);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003AA64 File Offset: 0x00038C64
		private ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo, bool transient)
		{
			this.check_name_and_filename(name, fileName, false);
			if (!transient)
			{
				if (Path.GetExtension(fileName) == string.Empty)
				{
					throw new ArgumentException("Module file name '" + fileName + "' must have file extension.");
				}
				if (!this.IsSave)
				{
					throw new NotSupportedException("Persistable modules are not supported in a dynamic assembly created with AssemblyBuilderAccess.Run");
				}
				if (this.created)
				{
					throw new InvalidOperationException("Assembly was already saved.");
				}
			}
			ModuleBuilder moduleBuilder = new ModuleBuilder(this, name, fileName, emitSymbolInfo, transient);
			if (this.modules != null && this.is_module_only)
			{
				throw new InvalidOperationException("A module-only assembly can only contain one module.");
			}
			if (this.modules != null)
			{
				ModuleBuilder[] destinationArray = new ModuleBuilder[this.modules.Length + 1];
				Array.Copy(this.modules, destinationArray, this.modules.Length);
				this.modules = destinationArray;
			}
			else
			{
				this.modules = new ModuleBuilder[1];
			}
			this.modules[this.modules.Length - 1] = moduleBuilder;
			return moduleBuilder;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003AB60 File Offset: 0x00038D60
		public override Type[] GetExportedTypes()
		{
			throw this.not_supported();
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003AB68 File Offset: 0x00038D68
		public override FileStream GetFile(string name)
		{
			throw this.not_supported();
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003AB70 File Offset: 0x00038D70
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			throw this.not_supported();
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003AB78 File Offset: 0x00038D78
		internal override Module[] GetModulesInternal()
		{
			if (this.modules == null)
			{
				return new Module[0];
			}
			return (Module[])this.modules.Clone();
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003AB9C File Offset: 0x00038D9C
		internal override Type[] GetTypes(bool exportedOnly)
		{
			Type[] array = null;
			if (this.modules != null)
			{
				for (int i = 0; i < this.modules.Length; i++)
				{
					Type[] types = this.modules[i].GetTypes();
					if (array == null)
					{
						array = types;
					}
					else
					{
						Type[] destinationArray = new Type[array.Length + types.Length];
						Array.Copy(array, 0, destinationArray, 0, array.Length);
						Array.Copy(types, 0, destinationArray, array.Length, types.Length);
					}
				}
			}
			if (this.loaded_modules != null)
			{
				for (int j = 0; j < this.loaded_modules.Length; j++)
				{
					Type[] types2 = this.loaded_modules[j].GetTypes();
					if (array == null)
					{
						array = types2;
					}
					else
					{
						Type[] destinationArray2 = new Type[array.Length + types2.Length];
						Array.Copy(array, 0, destinationArray2, 0, array.Length);
						Array.Copy(types2, 0, destinationArray2, array.Length, types2.Length);
					}
				}
			}
			return (array != null) ? array : Type.EmptyTypes;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003AC98 File Offset: 0x00038E98
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw this.not_supported();
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003ACA0 File Offset: 0x00038EA0
		public override string[] GetManifestResourceNames()
		{
			throw this.not_supported();
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003ACA8 File Offset: 0x00038EA8
		public override Stream GetManifestResourceStream(string name)
		{
			throw this.not_supported();
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003ACB0 File Offset: 0x00038EB0
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			throw this.not_supported();
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x0003ACB8 File Offset: 0x00038EB8
		internal bool IsCompilerContext
		{
			get
			{
				return this.is_compiler_context;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0003ACC0 File Offset: 0x00038EC0
		internal bool IsSave
		{
			get
			{
				return this.access != 1U;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0003ACD0 File Offset: 0x00038ED0
		internal bool IsRun
		{
			get
			{
				return this.access == 1U || this.access == 3U;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0003ACEC File Offset: 0x00038EEC
		internal string AssemblyDir
		{
			get
			{
				return this.dir;
			}
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0003ACF4 File Offset: 0x00038EF4
		internal override Module GetManifestModule()
		{
			if (this.manifest_module == null)
			{
				this.manifest_module = this.DefineDynamicModule("Default Dynamic Module");
			}
			return this.manifest_module;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0003AD18 File Offset: 0x00038F18
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			if (this.IsCompilerContext)
			{
				string fullName = customBuilder.Ctor.ReflectedType.FullName;
				if (fullName == "System.Reflection.AssemblyVersionAttribute")
				{
					this.version = this.create_assembly_version(customBuilder.string_arg());
					return;
				}
				if (fullName == "System.Reflection.AssemblyCultureAttribute")
				{
					this.culture = this.GetCultureString(customBuilder.string_arg());
				}
				else if (fullName == "System.Reflection.AssemblyAlgorithmIdAttribute")
				{
					byte[] data = customBuilder.Data;
					int num = 2;
					this.algid = (uint)data[num];
					this.algid |= (uint)((uint)data[num + 1] << 8);
					this.algid |= (uint)((uint)data[num + 2] << 16);
					this.algid |= (uint)((uint)data[num + 3] << 24);
				}
				else if (fullName == "System.Reflection.AssemblyFlagsAttribute")
				{
					byte[] data = customBuilder.Data;
					int num = 2;
					this.flags |= (uint)data[num];
					this.flags |= (uint)((uint)data[num + 1] << 8);
					this.flags |= (uint)((uint)data[num + 2] << 16);
					this.flags |= (uint)((uint)data[num + 3] << 24);
					if (this.sn == null)
					{
						this.flags &= 4294967294U;
					}
				}
			}
			if (this.cattrs != null)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.cattrs.Length + 1];
				this.cattrs.CopyTo(array, 0);
				array[this.cattrs.Length] = customBuilder;
				this.cattrs = array;
			}
			else
			{
				this.cattrs = new CustomAttributeBuilder[1];
				this.cattrs[0] = customBuilder;
			}
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0003AED8 File Offset: 0x000390D8
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003AEE4 File Offset: 0x000390E4
		private void check_name_and_filename(string name, string fileName, bool fileNeedsToExists)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal.", "name");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "fileName");
			}
			if (Path.GetFileName(fileName) != fileName)
			{
				throw new ArgumentException("fileName '" + fileName + "' must not include a path.", "fileName");
			}
			string text = fileName;
			if (this.dir != null)
			{
				text = Path.Combine(this.dir, fileName);
			}
			if (fileNeedsToExists && !File.Exists(text))
			{
				throw new FileNotFoundException("Could not find file '" + fileName + "'");
			}
			if (this.resources != null)
			{
				for (int i = 0; i < this.resources.Length; i++)
				{
					if (this.resources[i].filename == text)
					{
						throw new ArgumentException("Duplicate file name '" + fileName + "'");
					}
					if (this.resources[i].name == name)
					{
						throw new ArgumentException("Duplicate name '" + name + "'");
					}
				}
			}
			if (this.modules != null)
			{
				for (int j = 0; j < this.modules.Length; j++)
				{
					if (!this.modules[j].IsTransient() && this.modules[j].FileName == fileName)
					{
						throw new ArgumentException("Duplicate file name '" + fileName + "'");
					}
					if (this.modules[j].Name == name)
					{
						throw new ArgumentException("Duplicate name '" + name + "'");
					}
				}
			}
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003B0D0 File Offset: 0x000392D0
		private string create_assembly_version(string version)
		{
			string[] array = version.Split(new char[]
			{
				'.'
			});
			int[] array2 = new int[4];
			if (array.Length < 0 || array.Length > 4)
			{
				throw new ArgumentException("The version specified '" + version + "' is invalid");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "*")
				{
					DateTime now = DateTime.Now;
					if (i == 2)
					{
						array2[2] = (now - new DateTime(2000, 1, 1)).Days;
						if (array.Length == 3)
						{
							array2[3] = (now.Second + now.Minute * 60 + now.Hour * 3600) / 2;
						}
					}
					else
					{
						if (i != 3)
						{
							throw new ArgumentException("The version specified '" + version + "' is invalid");
						}
						array2[3] = (now.Second + now.Minute * 60 + now.Hour * 3600) / 2;
					}
				}
				else
				{
					try
					{
						array2[i] = int.Parse(array[i]);
					}
					catch (FormatException)
					{
						throw new ArgumentException("The version specified '" + version + "' is invalid");
					}
				}
			}
			return string.Concat(new object[]
			{
				array2[0],
				".",
				array2[1],
				".",
				array2[2],
				".",
				array2[3]
			});
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003B27C File Offset: 0x0003947C
		private string GetCultureString(string str)
		{
			return (!(str == "neutral")) ? str : string.Empty;
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0003B29C File Offset: 0x0003949C
		internal override AssemblyName UnprotectedGetName()
		{
			AssemblyName assemblyName = base.UnprotectedGetName();
			if (this.sn != null)
			{
				assemblyName.SetPublicKey(this.sn.PublicKey);
				assemblyName.SetPublicKeyToken(this.sn.PublicKeyToken);
			}
			return assemblyName;
		}

		// Token: 0x0400062C RID: 1580
		private const AssemblyBuilderAccess COMPILER_ACCESS = (AssemblyBuilderAccess)2048;

		// Token: 0x0400062D RID: 1581
		private UIntPtr dynamic_assembly;

		// Token: 0x0400062E RID: 1582
		private MethodInfo entry_point;

		// Token: 0x0400062F RID: 1583
		private ModuleBuilder[] modules;

		// Token: 0x04000630 RID: 1584
		private string name;

		// Token: 0x04000631 RID: 1585
		private string dir;

		// Token: 0x04000632 RID: 1586
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000633 RID: 1587
		private MonoResource[] resources;

		// Token: 0x04000634 RID: 1588
		private byte[] public_key;

		// Token: 0x04000635 RID: 1589
		private string version;

		// Token: 0x04000636 RID: 1590
		private string culture;

		// Token: 0x04000637 RID: 1591
		private uint algid;

		// Token: 0x04000638 RID: 1592
		private uint flags;

		// Token: 0x04000639 RID: 1593
		private PEFileKinds pekind = PEFileKinds.Dll;

		// Token: 0x0400063A RID: 1594
		private bool delay_sign;

		// Token: 0x0400063B RID: 1595
		private uint access;

		// Token: 0x0400063C RID: 1596
		private Module[] loaded_modules;

		// Token: 0x0400063D RID: 1597
		private MonoWin32Resource[] win32_resources;

		// Token: 0x0400063E RID: 1598
		private RefEmitPermissionSet[] permissions_minimum;

		// Token: 0x0400063F RID: 1599
		private RefEmitPermissionSet[] permissions_optional;

		// Token: 0x04000640 RID: 1600
		private RefEmitPermissionSet[] permissions_refused;

		// Token: 0x04000641 RID: 1601
		private PortableExecutableKinds peKind;

		// Token: 0x04000642 RID: 1602
		private ImageFileMachine machine;

		// Token: 0x04000643 RID: 1603
		private bool corlib_internal;

		// Token: 0x04000644 RID: 1604
		private Type[] type_forwarders;

		// Token: 0x04000645 RID: 1605
		private byte[] pktoken;

		// Token: 0x04000646 RID: 1606
		internal Type corlib_object_type = typeof(object);

		// Token: 0x04000647 RID: 1607
		internal Type corlib_value_type = typeof(ValueType);

		// Token: 0x04000648 RID: 1608
		internal Type corlib_enum_type = typeof(Enum);

		// Token: 0x04000649 RID: 1609
		internal Type corlib_void_type = typeof(void);

		// Token: 0x0400064A RID: 1610
		private ArrayList resource_writers;

		// Token: 0x0400064B RID: 1611
		private Win32VersionResource version_res;

		// Token: 0x0400064C RID: 1612
		private bool created;

		// Token: 0x0400064D RID: 1613
		private bool is_module_only;

		// Token: 0x0400064E RID: 1614
		private StrongName sn;

		// Token: 0x0400064F RID: 1615
		private NativeResourceType native_resource;

		// Token: 0x04000650 RID: 1616
		private readonly bool is_compiler_context;

		// Token: 0x04000651 RID: 1617
		private string versioninfo_culture;

		// Token: 0x04000652 RID: 1618
		private ModuleBuilder manifest_module;
	}
}
