using System;
using System.Collections;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Policy;

namespace System.Reflection
{
	// Token: 0x02000179 RID: 377
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Assembly))]
	[ComVisible(true)]
	[Serializable]
	public class Assembly : ICustomAttributeProvider, _Assembly
	{
		// Token: 0x06000E22 RID: 3618 RVA: 0x00038238 File Offset: 0x00036438
		internal Assembly()
		{
			this.resolve_event_holder = new Assembly.ResolveEventHolder();
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000E23 RID: 3619 RVA: 0x0003824C File Offset: 0x0003644C
		// (remove) Token: 0x06000E24 RID: 3620 RVA: 0x0003825C File Offset: 0x0003645C
		public event ModuleResolveEventHandler ModuleResolve
		{
			add
			{
				this.resolve_event_holder.ModuleResolve += value;
			}
			remove
			{
				this.resolve_event_holder.ModuleResolve -= value;
			}
		}

		// Token: 0x06000E25 RID: 3621
		[MethodImpl(4096)]
		private extern string get_code_base(bool escaped);

		// Token: 0x06000E26 RID: 3622
		[MethodImpl(4096)]
		private extern string get_fullname();

		// Token: 0x06000E27 RID: 3623
		[MethodImpl(4096)]
		private extern string get_location();

		// Token: 0x06000E28 RID: 3624
		[MethodImpl(4096)]
		private extern string InternalImageRuntimeVersion();

		// Token: 0x06000E29 RID: 3625 RVA: 0x0003826C File Offset: 0x0003646C
		private string GetCodeBase(bool escaped)
		{
			return this.get_code_base(escaped);
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00038284 File Offset: 0x00036484
		public virtual string CodeBase
		{
			get
			{
				return this.GetCodeBase(false);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00038290 File Offset: 0x00036490
		public virtual string EscapedCodeBase
		{
			get
			{
				return this.GetCodeBase(true);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0003829C File Offset: 0x0003649C
		public virtual string FullName
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000E2D RID: 3629
		public virtual extern MethodInfo EntryPoint { [MethodImpl(4096)] get; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x000382A4 File Offset: 0x000364A4
		public virtual Evidence Evidence
		{
			get
			{
				return this.UnprotectedGetEvidence();
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x000382AC File Offset: 0x000364AC
		internal Evidence UnprotectedGetEvidence()
		{
			if (this._evidence == null)
			{
				lock (this)
				{
					this._evidence = Evidence.GetDefaultHostEvidence(this);
				}
			}
			return this._evidence;
		}

		// Token: 0x06000E30 RID: 3632
		[MethodImpl(4096)]
		private extern bool get_global_assembly_cache();

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x000382FC File Offset: 0x000364FC
		public bool GlobalAssemblyCache
		{
			get
			{
				return this.get_global_assembly_cache();
			}
		}

		// Token: 0x17000203 RID: 515
		// (set) Token: 0x06000E32 RID: 3634 RVA: 0x00038304 File Offset: 0x00036504
		internal bool FromByteArray
		{
			set
			{
				this.fromByteArray = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x00038310 File Offset: 0x00036510
		public virtual string Location
		{
			get
			{
				if (this.fromByteArray)
				{
					return string.Empty;
				}
				return this.get_location();
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x00038338 File Offset: 0x00036538
		[ComVisible(false)]
		public virtual string ImageRuntimeVersion
		{
			get
			{
				return this.InternalImageRuntimeVersion();
			}
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00038340 File Offset: 0x00036540
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetAssemblyData(this, info, context);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003835C File Offset: 0x0003655C
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00038368 File Offset: 0x00036568
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00038374 File Offset: 0x00036574
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06000E39 RID: 3641
		[MethodImpl(4096)]
		private extern object GetFilesInternal(string name, bool getResourceModules);

		// Token: 0x06000E3A RID: 3642 RVA: 0x00038380 File Offset: 0x00036580
		public virtual FileStream[] GetFiles()
		{
			return this.GetFiles(false);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0003838C File Offset: 0x0003658C
		public virtual FileStream[] GetFiles(bool getResourceModules)
		{
			string[] array = (string[])this.GetFilesInternal(null, getResourceModules);
			if (array == null)
			{
				return new FileStream[0];
			}
			string location = this.Location;
			FileStream[] array2;
			if (location != string.Empty)
			{
				array2 = new FileStream[array.Length + 1];
				array2[0] = new FileStream(location, FileMode.Open, FileAccess.Read);
				for (int i = 0; i < array.Length; i++)
				{
					array2[i + 1] = new FileStream(array[i], FileMode.Open, FileAccess.Read);
				}
			}
			else
			{
				array2 = new FileStream[array.Length];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = new FileStream(array[j], FileMode.Open, FileAccess.Read);
				}
			}
			return array2;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003843C File Offset: 0x0003663C
		public virtual FileStream GetFile(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(null, "Name cannot be null.");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not valid");
			}
			string text = (string)this.GetFilesInternal(name, true);
			if (text != null)
			{
				return new FileStream(text, FileMode.Open, FileAccess.Read);
			}
			return null;
		}

		// Token: 0x06000E3D RID: 3645
		[MethodImpl(4096)]
		internal extern IntPtr GetManifestResourceInternal(string name, out int size, out Module module);

		// Token: 0x06000E3E RID: 3646 RVA: 0x00038490 File Offset: 0x00036690
		public unsafe virtual Stream GetManifestResourceStream(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("String cannot have zero length.", "name");
			}
			ManifestResourceInfo manifestResourceInfo = this.GetManifestResourceInfo(name);
			if (manifestResourceInfo == null)
			{
				return null;
			}
			if (manifestResourceInfo.ReferencedAssembly != null)
			{
				return manifestResourceInfo.ReferencedAssembly.GetManifestResourceStream(name);
			}
			if (manifestResourceInfo.FileName != null && manifestResourceInfo.ResourceLocation == (ResourceLocation)0)
			{
				if (this.fromByteArray)
				{
					throw new FileNotFoundException(manifestResourceInfo.FileName);
				}
				string directoryName = Path.GetDirectoryName(this.Location);
				string path = Path.Combine(directoryName, manifestResourceInfo.FileName);
				return new FileStream(path, FileMode.Open, FileAccess.Read);
			}
			else
			{
				int num;
				Module module;
				IntPtr manifestResourceInternal = this.GetManifestResourceInternal(name, out num, out module);
				if (manifestResourceInternal == (IntPtr)0)
				{
					return null;
				}
				UnmanagedMemoryStream unmanagedMemoryStream = new UnmanagedMemoryStream((byte*)((void*)manifestResourceInternal), (long)num);
				unmanagedMemoryStream.Closed += new Assembly.ResourceCloseHandler(module).OnClose;
				return unmanagedMemoryStream;
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0003858C File Offset: 0x0003678C
		public virtual Stream GetManifestResourceStream(Type type, string name)
		{
			string text;
			if (type != null)
			{
				text = type.Namespace;
			}
			else
			{
				if (name == null)
				{
					throw new ArgumentNullException("type");
				}
				text = null;
			}
			if (text == null || text.Length == 0)
			{
				return this.GetManifestResourceStream(name);
			}
			return this.GetManifestResourceStream(text + "." + name);
		}

		// Token: 0x06000E40 RID: 3648
		[MethodImpl(4096)]
		internal virtual extern Type[] GetTypes(bool exportedOnly);

		// Token: 0x06000E41 RID: 3649 RVA: 0x000385EC File Offset: 0x000367EC
		public virtual Type[] GetTypes()
		{
			return this.GetTypes(false);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x000385F8 File Offset: 0x000367F8
		public virtual Type[] GetExportedTypes()
		{
			return this.GetTypes(true);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00038604 File Offset: 0x00036804
		public virtual Type GetType(string name, bool throwOnError)
		{
			return this.GetType(name, throwOnError, false);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00038610 File Offset: 0x00036810
		public virtual Type GetType(string name)
		{
			return this.GetType(name, false, false);
		}

		// Token: 0x06000E45 RID: 3653
		[MethodImpl(4096)]
		internal extern Type InternalGetType(Module module, string name, bool throwOnError, bool ignoreCase);

		// Token: 0x06000E46 RID: 3654 RVA: 0x0003861C File Offset: 0x0003681C
		public Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			if (name == null)
			{
				throw new ArgumentNullException(name);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name", "Name cannot be empty");
			}
			return this.InternalGetType(null, name, throwOnError, ignoreCase);
		}

		// Token: 0x06000E47 RID: 3655
		[MethodImpl(4096)]
		internal static extern void InternalGetAssemblyName(string assemblyFile, AssemblyName aname);

		// Token: 0x06000E48 RID: 3656
		[MethodImpl(4096)]
		private static extern void FillName(Assembly ass, AssemblyName aname);

		// Token: 0x06000E49 RID: 3657 RVA: 0x00038650 File Offset: 0x00036850
		[MonoTODO("copiedName == true is not supported")]
		public virtual AssemblyName GetName(bool copiedName)
		{
			if (SecurityManager.SecurityEnabled)
			{
				this.GetCodeBase(true);
			}
			return this.UnprotectedGetName();
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0003866C File Offset: 0x0003686C
		public virtual AssemblyName GetName()
		{
			return this.GetName(false);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00038678 File Offset: 0x00036878
		internal virtual AssemblyName UnprotectedGetName()
		{
			AssemblyName assemblyName = new AssemblyName();
			Assembly.FillName(this, assemblyName);
			return assemblyName;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00038694 File Offset: 0x00036894
		public override string ToString()
		{
			if (this.assemblyName != null)
			{
				return this.assemblyName;
			}
			this.assemblyName = this.get_fullname();
			return this.assemblyName;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000386BC File Offset: 0x000368BC
		public static string CreateQualifiedName(string assemblyName, string typeName)
		{
			return typeName + ", " + assemblyName;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x000386CC File Offset: 0x000368CC
		public static Assembly GetAssembly(Type type)
		{
			if (type != null)
			{
				return type.Assembly;
			}
			throw new ArgumentNullException("type");
		}

		// Token: 0x06000E4F RID: 3663
		[MethodImpl(4096)]
		public static extern Assembly GetEntryAssembly();

		// Token: 0x06000E50 RID: 3664 RVA: 0x000386E8 File Offset: 0x000368E8
		public Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			return this.GetSatelliteAssembly(culture, null, true);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x000386F4 File Offset: 0x000368F4
		public Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			return this.GetSatelliteAssembly(culture, version, true);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00038700 File Offset: 0x00036900
		internal Assembly GetSatelliteAssemblyNoThrow(CultureInfo culture, Version version)
		{
			return this.GetSatelliteAssembly(culture, version, false);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0003870C File Offset: 0x0003690C
		private Assembly GetSatelliteAssembly(CultureInfo culture, Version version, bool throwOnError)
		{
			if (culture == null)
			{
				throw new ArgumentException("culture");
			}
			AssemblyName name = this.GetName(true);
			if (version != null)
			{
				name.Version = version;
			}
			name.CultureInfo = culture;
			name.Name += ".resources";
			try
			{
				Assembly assembly = AppDomain.CurrentDomain.LoadSatellite(name, false);
				if (assembly != null)
				{
					return assembly;
				}
			}
			catch (FileNotFoundException)
			{
			}
			string directoryName = Path.GetDirectoryName(this.Location);
			string text = Path.Combine(directoryName, Path.Combine(culture.Name, name.Name + ".dll"));
			if (!throwOnError && !File.Exists(text))
			{
				return null;
			}
			return Assembly.LoadFrom(text);
		}

		// Token: 0x06000E54 RID: 3668
		[MethodImpl(4096)]
		private static extern Assembly LoadFrom(string assemblyFile, bool refonly);

		// Token: 0x06000E55 RID: 3669 RVA: 0x000387E4 File Offset: 0x000369E4
		public static Assembly LoadFrom(string assemblyFile)
		{
			return Assembly.LoadFrom(assemblyFile, false);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x000387F0 File Offset: 0x000369F0
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence)
		{
			return Assembly.LoadFrom(assemblyFile, false);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00038808 File Offset: 0x00036A08
		[MonoTODO("This overload is not currently implemented")]
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			if (assemblyFile == string.Empty)
			{
				throw new ArgumentException("Name can't be the empty string", "assemblyFile");
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00038840 File Offset: 0x00036A40
		public static Assembly LoadFile(string path, Evidence securityEvidence)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path == string.Empty)
			{
				throw new ArgumentException("Path can't be empty", "path");
			}
			return Assembly.LoadFrom(path, securityEvidence);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0003887C File Offset: 0x00036A7C
		public static Assembly LoadFile(string path)
		{
			return Assembly.LoadFile(path, null);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00038888 File Offset: 0x00036A88
		public static Assembly Load(string assemblyString)
		{
			return AppDomain.CurrentDomain.Load(assemblyString);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00038898 File Offset: 0x00036A98
		public static Assembly Load(string assemblyString, Evidence assemblySecurity)
		{
			return AppDomain.CurrentDomain.Load(assemblyString, assemblySecurity);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x000388A8 File Offset: 0x00036AA8
		public static Assembly Load(AssemblyName assemblyRef)
		{
			return AppDomain.CurrentDomain.Load(assemblyRef);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x000388B8 File Offset: 0x00036AB8
		public static Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
		{
			return AppDomain.CurrentDomain.Load(assemblyRef, assemblySecurity);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000388C8 File Offset: 0x00036AC8
		public static Assembly Load(byte[] rawAssembly)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000388D8 File Offset: 0x00036AD8
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x000388E8 File Offset: 0x00036AE8
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore, securityEvidence);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x000388F8 File Offset: 0x00036AF8
		public static Assembly ReflectionOnlyLoad(byte[] rawAssembly)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, null, null, true);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00038908 File Offset: 0x00036B08
		public static Assembly ReflectionOnlyLoad(string assemblyString)
		{
			return AppDomain.CurrentDomain.Load(assemblyString, null, true);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00038918 File Offset: 0x00036B18
		public static Assembly ReflectionOnlyLoadFrom(string assemblyFile)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Assembly.LoadFrom(assemblyFile, true);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00038934 File Offset: 0x00036B34
		[Obsolete("")]
		public static Assembly LoadWithPartialName(string partialName)
		{
			return Assembly.LoadWithPartialName(partialName, null);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00038940 File Offset: 0x00036B40
		[MonoTODO("Not implemented")]
		public Module LoadModule(string moduleName, byte[] rawModule)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00038948 File Offset: 0x00036B48
		[MonoTODO("Not implemented")]
		public Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E67 RID: 3687
		[MethodImpl(4096)]
		private static extern Assembly load_with_partial_name(string name, Evidence e);

		// Token: 0x06000E68 RID: 3688 RVA: 0x00038950 File Offset: 0x00036B50
		[Obsolete("")]
		public static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence)
		{
			return Assembly.LoadWithPartialName(partialName, securityEvidence, true);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0003895C File Offset: 0x00036B5C
		internal static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence, bool oldBehavior)
		{
			if (!oldBehavior)
			{
				throw new NotImplementedException();
			}
			if (partialName == null)
			{
				throw new NullReferenceException();
			}
			return Assembly.load_with_partial_name(partialName, securityEvidence);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00038980 File Offset: 0x00036B80
		public object CreateInstance(string typeName)
		{
			return this.CreateInstance(typeName, false);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003898C File Offset: 0x00036B8C
		public object CreateInstance(string typeName, bool ignoreCase)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			object result;
			try
			{
				result = Activator.CreateInstance(type);
			}
			catch (InvalidOperationException)
			{
				throw new ArgumentException("It is illegal to invoke a method on a Type loaded via ReflectionOnly methods.");
			}
			return result;
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x000389E0 File Offset: 0x00036BE0
		public object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			object result;
			try
			{
				result = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			}
			catch (InvalidOperationException)
			{
				throw new ArgumentException("It is illegal to invoke a method on a Type loaded via ReflectionOnly methods.");
			}
			return result;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00038A3C File Offset: 0x00036C3C
		public Module[] GetLoadedModules()
		{
			return this.GetLoadedModules(false);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00038A48 File Offset: 0x00036C48
		public Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.GetModules(getResourceModules);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00038A54 File Offset: 0x00036C54
		public Module[] GetModules()
		{
			return this.GetModules(false);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00038A60 File Offset: 0x00036C60
		public Module GetModule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Name can't be empty");
			}
			Module[] modules = this.GetModules(true);
			foreach (Module module in modules)
			{
				if (module.ScopeName == name)
				{
					return module;
				}
			}
			return null;
		}

		// Token: 0x06000E71 RID: 3697
		[MethodImpl(4096)]
		internal virtual extern Module[] GetModulesInternal();

		// Token: 0x06000E72 RID: 3698 RVA: 0x00038ACC File Offset: 0x00036CCC
		public Module[] GetModules(bool getResourceModules)
		{
			Module[] modulesInternal = this.GetModulesInternal();
			if (!getResourceModules)
			{
				ArrayList arrayList = new ArrayList(modulesInternal.Length);
				foreach (Module module in modulesInternal)
				{
					if (!module.IsResource())
					{
						arrayList.Add(module);
					}
				}
				return (Module[])arrayList.ToArray(typeof(Module));
			}
			return modulesInternal;
		}

		// Token: 0x06000E73 RID: 3699
		[MethodImpl(4096)]
		internal extern string[] GetNamespaces();

		// Token: 0x06000E74 RID: 3700
		[MethodImpl(4096)]
		public virtual extern string[] GetManifestResourceNames();

		// Token: 0x06000E75 RID: 3701
		[MethodImpl(4096)]
		public static extern Assembly GetExecutingAssembly();

		// Token: 0x06000E76 RID: 3702
		[MethodImpl(4096)]
		public static extern Assembly GetCallingAssembly();

		// Token: 0x06000E77 RID: 3703
		[MethodImpl(4096)]
		public extern AssemblyName[] GetReferencedAssemblies();

		// Token: 0x06000E78 RID: 3704
		[MethodImpl(4096)]
		private extern bool GetManifestResourceInfoInternal(string name, ManifestResourceInfo info);

		// Token: 0x06000E79 RID: 3705 RVA: 0x00038B38 File Offset: 0x00036D38
		public virtual ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			if (resourceName.Length == 0)
			{
				throw new ArgumentException("String cannot have zero length.");
			}
			ManifestResourceInfo manifestResourceInfo = new ManifestResourceInfo();
			bool manifestResourceInfoInternal = this.GetManifestResourceInfoInternal(resourceName, manifestResourceInfo);
			if (manifestResourceInfoInternal)
			{
				return manifestResourceInfo;
			}
			return null;
		}

		// Token: 0x06000E7A RID: 3706
		[MethodImpl(4096)]
		internal static extern int MonoDebugger_GetMethodToken(MethodBase method);

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00038B84 File Offset: 0x00036D84
		[MonoTODO("Always returns zero")]
		[ComVisible(false)]
		public long HostContext
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x00038B88 File Offset: 0x00036D88
		[ComVisible(false)]
		public Module ManifestModule
		{
			get
			{
				return this.GetManifestModule();
			}
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00038B90 File Offset: 0x00036D90
		internal virtual Module GetManifestModule()
		{
			return this.GetManifestModuleInternal();
		}

		// Token: 0x06000E7E RID: 3710
		[MethodImpl(4096)]
		internal extern Module GetManifestModuleInternal();

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000E7F RID: 3711
		[ComVisible(false)]
		public virtual extern bool ReflectionOnly { [MethodImpl(4096)] get; }

		// Token: 0x06000E80 RID: 3712 RVA: 0x00038B98 File Offset: 0x00036D98
		internal void Resolve()
		{
			lock (this)
			{
				this.LoadAssemblyPermissions();
				Evidence evidence = new Evidence(this.UnprotectedGetEvidence());
				evidence.AddHost(new PermissionRequestEvidence(this._minimum, this._optional, this._refuse));
				this._granted = SecurityManager.ResolvePolicy(evidence, this._minimum, this._optional, this._refuse, out this._denied);
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x00038C1C File Offset: 0x00036E1C
		internal PermissionSet GrantedPermissionSet
		{
			get
			{
				if (this._granted == null)
				{
					if (SecurityManager.ResolvingPolicyLevel != null)
					{
						if (SecurityManager.ResolvingPolicyLevel.IsFullTrustAssembly(this))
						{
							return DefaultPolicies.FullTrust;
						}
						return null;
					}
					else
					{
						this.Resolve();
					}
				}
				return this._granted;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00038C58 File Offset: 0x00036E58
		internal PermissionSet DeniedPermissionSet
		{
			get
			{
				if (this._granted == null)
				{
					if (SecurityManager.ResolvingPolicyLevel != null)
					{
						if (SecurityManager.ResolvingPolicyLevel.IsFullTrustAssembly(this))
						{
							return null;
						}
						return DefaultPolicies.FullTrust;
					}
					else
					{
						this.Resolve();
					}
				}
				return this._denied;
			}
		}

		// Token: 0x06000E83 RID: 3715
		[MethodImpl(4096)]
		internal static extern bool LoadPermissions(Assembly a, ref IntPtr minimum, ref int minLength, ref IntPtr optional, ref int optLength, ref IntPtr refused, ref int refLength);

		// Token: 0x06000E84 RID: 3716 RVA: 0x00038C94 File Offset: 0x00036E94
		private void LoadAssemblyPermissions()
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr zero3 = IntPtr.Zero;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (Assembly.LoadPermissions(this, ref zero, ref num, ref zero2, ref num2, ref zero3, ref num3))
			{
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(zero, array, 0, num);
					this._minimum = SecurityManager.Decode(array);
				}
				if (num2 > 0)
				{
					byte[] array2 = new byte[num2];
					Marshal.Copy(zero2, array2, 0, num2);
					this._optional = SecurityManager.Decode(array2);
				}
				if (num3 > 0)
				{
					byte[] array3 = new byte[num3];
					Marshal.Copy(zero3, array3, 0, num3);
					this._refuse = SecurityManager.Decode(array3);
				}
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00038D4C File Offset: 0x00036F4C
		virtual Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040005DE RID: 1502
		private IntPtr _mono_assembly;

		// Token: 0x040005DF RID: 1503
		private Assembly.ResolveEventHolder resolve_event_holder;

		// Token: 0x040005E0 RID: 1504
		private Evidence _evidence;

		// Token: 0x040005E1 RID: 1505
		internal PermissionSet _minimum;

		// Token: 0x040005E2 RID: 1506
		internal PermissionSet _optional;

		// Token: 0x040005E3 RID: 1507
		internal PermissionSet _refuse;

		// Token: 0x040005E4 RID: 1508
		private PermissionSet _granted;

		// Token: 0x040005E5 RID: 1509
		private PermissionSet _denied;

		// Token: 0x040005E6 RID: 1510
		private bool fromByteArray;

		// Token: 0x040005E7 RID: 1511
		private string assemblyName;

		// Token: 0x0200017A RID: 378
		internal class ResolveEventHolder
		{
			// Token: 0x1400000B RID: 11
			// (add) Token: 0x06000E87 RID: 3719 RVA: 0x00038D5C File Offset: 0x00036F5C
			// (remove) Token: 0x06000E88 RID: 3720 RVA: 0x00038D78 File Offset: 0x00036F78
			public event ModuleResolveEventHandler ModuleResolve;
		}

		// Token: 0x0200017B RID: 379
		private class ResourceCloseHandler
		{
			// Token: 0x06000E89 RID: 3721 RVA: 0x00038D94 File Offset: 0x00036F94
			public ResourceCloseHandler(Module module)
			{
				this.module = module;
			}

			// Token: 0x06000E8A RID: 3722 RVA: 0x00038DA4 File Offset: 0x00036FA4
			public void OnClose(object sender, EventArgs e)
			{
				this.module = null;
			}

			// Token: 0x040005E9 RID: 1513
			private Module module;
		}
	}
}
