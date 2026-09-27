using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Threading;
using Mono.Security;

namespace System
{
	// Token: 0x02000055 RID: 85
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public sealed class AppDomain : MarshalByRefObject
	{
		// Token: 0x06000199 RID: 409 RVA: 0x0000CF8C File Offset: 0x0000B18C
		private AppDomain()
		{
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600019A RID: 410 RVA: 0x0000CF94 File Offset: 0x0000B194
		// (remove) Token: 0x0600019B RID: 411 RVA: 0x0000CFB0 File Offset: 0x0000B1B0
		public event AssemblyLoadEventHandler AssemblyLoad;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600019C RID: 412 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		// (remove) Token: 0x0600019D RID: 413 RVA: 0x0000CFE8 File Offset: 0x0000B1E8
		public event ResolveEventHandler AssemblyResolve;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600019E RID: 414 RVA: 0x0000D004 File Offset: 0x0000B204
		// (remove) Token: 0x0600019F RID: 415 RVA: 0x0000D020 File Offset: 0x0000B220
		public event EventHandler DomainUnload;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001A0 RID: 416 RVA: 0x0000D03C File Offset: 0x0000B23C
		// (remove) Token: 0x060001A1 RID: 417 RVA: 0x0000D058 File Offset: 0x0000B258
		public event EventHandler ProcessExit;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060001A2 RID: 418 RVA: 0x0000D074 File Offset: 0x0000B274
		// (remove) Token: 0x060001A3 RID: 419 RVA: 0x0000D090 File Offset: 0x0000B290
		public event ResolveEventHandler ResourceResolve;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060001A4 RID: 420 RVA: 0x0000D0AC File Offset: 0x0000B2AC
		// (remove) Token: 0x060001A5 RID: 421 RVA: 0x0000D0C8 File Offset: 0x0000B2C8
		public event ResolveEventHandler TypeResolve;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060001A6 RID: 422 RVA: 0x0000D0E4 File Offset: 0x0000B2E4
		// (remove) Token: 0x060001A7 RID: 423 RVA: 0x0000D100 File Offset: 0x0000B300
		public event UnhandledExceptionEventHandler UnhandledException;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060001A8 RID: 424 RVA: 0x0000D11C File Offset: 0x0000B31C
		// (remove) Token: 0x060001A9 RID: 425 RVA: 0x0000D138 File Offset: 0x0000B338
		public event ResolveEventHandler ReflectionOnlyAssemblyResolve;

		// Token: 0x060001AA RID: 426
		[MethodImpl(4096)]
		private extern AppDomainSetup getSetup();

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000D154 File Offset: 0x0000B354
		internal AppDomainSetup SetupInformationNoCopy
		{
			get
			{
				return this.getSetup();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000D15C File Offset: 0x0000B35C
		public AppDomainSetup SetupInformation
		{
			get
			{
				AppDomainSetup setup = this.getSetup();
				return new AppDomainSetup(setup);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000D178 File Offset: 0x0000B378
		public string BaseDirectory
		{
			get
			{
				string applicationBase = this.SetupInformationNoCopy.ApplicationBase;
				if (SecurityManager.SecurityEnabled && applicationBase != null && applicationBase.Length > 0)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, applicationBase).Demand();
				}
				return applicationBase;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000D1BC File Offset: 0x0000B3BC
		public string RelativeSearchPath
		{
			get
			{
				string privateBinPath = this.SetupInformationNoCopy.PrivateBinPath;
				if (SecurityManager.SecurityEnabled && privateBinPath != null && privateBinPath.Length > 0)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, privateBinPath).Demand();
				}
				return privateBinPath;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000D200 File Offset: 0x0000B400
		public string DynamicDirectory
		{
			get
			{
				AppDomainSetup setupInformationNoCopy = this.SetupInformationNoCopy;
				if (setupInformationNoCopy.DynamicBase == null)
				{
					return null;
				}
				string text = Path.Combine(setupInformationNoCopy.DynamicBase, setupInformationNoCopy.ApplicationName);
				if (SecurityManager.SecurityEnabled && text != null && text.Length > 0)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
				}
				return text;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000D25C File Offset: 0x0000B45C
		public bool ShadowCopyFiles
		{
			get
			{
				return this.SetupInformationNoCopy.ShadowCopyFiles == "true";
			}
		}

		// Token: 0x060001B1 RID: 433
		[MethodImpl(4096)]
		private extern string getFriendlyName();

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000D274 File Offset: 0x0000B474
		public string FriendlyName
		{
			get
			{
				return this.getFriendlyName();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000D27C File Offset: 0x0000B47C
		public Evidence Evidence
		{
			get
			{
				if (this._evidence == null)
				{
					lock (this)
					{
						Assembly entryAssembly = Assembly.GetEntryAssembly();
						if (entryAssembly == null)
						{
							if (this == AppDomain.DefaultDomain)
							{
								return new Evidence();
							}
							this._evidence = AppDomain.DefaultDomain.Evidence;
						}
						else
						{
							this._evidence = Evidence.GetDefaultHostEvidence(entryAssembly);
						}
					}
				}
				return new Evidence(this._evidence);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000D308 File Offset: 0x0000B508
		internal IPrincipal DefaultPrincipal
		{
			get
			{
				if (AppDomain._principal == null)
				{
					switch (this._principalPolicy)
					{
					case PrincipalPolicy.UnauthenticatedPrincipal:
						AppDomain._principal = new GenericPrincipal(new GenericIdentity(string.Empty, string.Empty), null);
						break;
					case PrincipalPolicy.WindowsPrincipal:
						AppDomain._principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
						break;
					}
				}
				return AppDomain._principal;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000D378 File Offset: 0x0000B578
		internal PermissionSet GrantedPermissionSet
		{
			get
			{
				return this._granted;
			}
		}

		// Token: 0x060001B6 RID: 438
		[MethodImpl(4096)]
		private static extern AppDomain getCurDomain();

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000D380 File Offset: 0x0000B580
		public static AppDomain CurrentDomain
		{
			get
			{
				return AppDomain.getCurDomain();
			}
		}

		// Token: 0x060001B8 RID: 440
		[MethodImpl(4096)]
		private static extern AppDomain getRootDomain();

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000D388 File Offset: 0x0000B588
		internal static AppDomain DefaultDomain
		{
			get
			{
				if (AppDomain.default_domain == null)
				{
					AppDomain rootDomain = AppDomain.getRootDomain();
					if (rootDomain == AppDomain.CurrentDomain)
					{
						AppDomain.default_domain = rootDomain;
					}
					else
					{
						AppDomain.default_domain = (AppDomain)RemotingServices.GetDomainProxy(rootDomain);
					}
				}
				return AppDomain.default_domain;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000D3D0 File Offset: 0x0000B5D0
		[Obsolete("AppDomain.AppendPrivatePath has been deprecated. Please investigate the use of AppDomainSetup.PrivateBinPath instead.")]
		public void AppendPrivatePath(string path)
		{
			if (path == null || path.Length == 0)
			{
				return;
			}
			AppDomainSetup setupInformationNoCopy = this.SetupInformationNoCopy;
			string text = setupInformationNoCopy.PrivateBinPath;
			if (text == null || text.Length == 0)
			{
				setupInformationNoCopy.PrivateBinPath = path;
				return;
			}
			text = text.Trim();
			if (text[text.Length - 1] != Path.PathSeparator)
			{
				text += Path.PathSeparator;
			}
			setupInformationNoCopy.PrivateBinPath = text + path;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000D454 File Offset: 0x0000B654
		[Obsolete("AppDomain.ClearPrivatePath has been deprecated. Please investigate the use of AppDomainSetup.PrivateBinPath instead.")]
		public void ClearPrivatePath()
		{
			this.SetupInformationNoCopy.PrivateBinPath = string.Empty;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000D468 File Offset: 0x0000B668
		[Obsolete("Use AppDomainSetup.ShadowCopyDirectories")]
		public void ClearShadowCopyPath()
		{
			this.SetupInformationNoCopy.ShadowCopyDirectories = string.Empty;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000D47C File Offset: 0x0000B67C
		public ObjectHandle CreateInstance(string assemblyName, string typeName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000D498 File Offset: 0x0000B698
		public ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName, activationAttributes);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
		public ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000D4E8 File Offset: 0x0000B6E8
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName);
			return (objectHandle == null) ? null : objectHandle.Unwrap();
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000D510 File Offset: 0x0000B710
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName, activationAttributes);
			return (objectHandle == null) ? null : objectHandle.Unwrap();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000D53C File Offset: 0x0000B73C
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
			return (objectHandle == null) ? null : objectHandle.Unwrap();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000D574 File Offset: 0x0000B774
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000D590 File Offset: 0x0000B790
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName, activationAttributes);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyName, typeName);
			return (objectHandle == null) ? null : objectHandle.Unwrap();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000D608 File Offset: 0x0000B808
		public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyName, typeName, activationAttributes);
			return (objectHandle == null) ? null : objectHandle.Unwrap();
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000D634 File Offset: 0x0000B834
		public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
			return (objectHandle == null) ? null : objectHandle.Unwrap();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000D66C File Offset: 0x0000B86C
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			return this.DefineDynamicAssembly(name, access, null, null, null, null, null, false);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000D688 File Offset: 0x0000B888
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence)
		{
			return this.DefineDynamicAssembly(name, access, null, evidence, null, null, null, false);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000D6A4 File Offset: 0x0000B8A4
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir)
		{
			return this.DefineDynamicAssembly(name, access, dir, null, null, null, null, false);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence)
		{
			return this.DefineDynamicAssembly(name, access, dir, evidence, null, null, null, false);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000D6DC File Offset: 0x0000B8DC
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, null, null, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000D6FC File Offset: 0x0000B8FC
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, null, evidence, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000D71C File Offset: 0x0000B91C
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, dir, null, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000D73C File Offset: 0x0000B93C
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000D75C File Offset: 0x0000B95C
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			AppDomain.ValidateAssemblyName(name.Name);
			AssemblyBuilder assemblyBuilder = new AssemblyBuilder(name, dir, access, false);
			assemblyBuilder.AddPermissionRequests(requiredPermissions, optionalPermissions, refusedPermissions);
			return assemblyBuilder;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000D79C File Offset: 0x0000B99C
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			AssemblyBuilder assemblyBuilder = this.DefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, isSynchronized);
			if (assemblyAttributes != null)
			{
				foreach (CustomAttributeBuilder customAttribute in assemblyAttributes)
				{
					assemblyBuilder.SetCustomAttribute(customAttribute);
				}
			}
			return assemblyBuilder;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000D80C File Offset: 0x0000BA0C
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			return this.DefineDynamicAssembly(name, access, null, null, null, null, null, false, assemblyAttributes);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000D828 File Offset: 0x0000BA28
		internal AssemblyBuilder DefineInternalDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			return new AssemblyBuilder(name, null, access, true);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000D834 File Offset: 0x0000BA34
		public void DoCallBack(CrossAppDomainDelegate callBackDelegate)
		{
			if (callBackDelegate != null)
			{
				callBackDelegate();
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000D844 File Offset: 0x0000BA44
		public int ExecuteAssembly(string assemblyFile)
		{
			return this.ExecuteAssembly(assemblyFile, null, null);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000D850 File Offset: 0x0000BA50
		public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity)
		{
			return this.ExecuteAssembly(assemblyFile, assemblySecurity, null);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000D85C File Offset: 0x0000BA5C
		public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args)
		{
			Assembly a = Assembly.LoadFrom(assemblyFile, assemblySecurity);
			return this.ExecuteAssemblyInternal(a, args);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000D87C File Offset: 0x0000BA7C
		public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			Assembly a = Assembly.LoadFrom(assemblyFile, assemblySecurity, hashValue, hashAlgorithm);
			return this.ExecuteAssemblyInternal(a, args);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
		private int ExecuteAssemblyInternal(Assembly a, string[] args)
		{
			if (a.EntryPoint == null)
			{
				throw new MissingMethodException("Entry point not found in assembly '" + a.FullName + "'.");
			}
			return this.ExecuteAssembly(a, args);
		}

		// Token: 0x060001DB RID: 475
		[MethodImpl(4096)]
		private extern int ExecuteAssembly(Assembly a, string[] args);

		// Token: 0x060001DC RID: 476
		[MethodImpl(4096)]
		private extern Assembly[] GetAssemblies(bool refOnly);

		// Token: 0x060001DD RID: 477 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
		public Assembly[] GetAssemblies()
		{
			return this.GetAssemblies(false);
		}

		// Token: 0x060001DE RID: 478
		[MethodImpl(4096)]
		public extern object GetData(string name);

		// Token: 0x060001DF RID: 479 RVA: 0x0000D8DC File Offset: 0x0000BADC
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060001E1 RID: 481
		[MethodImpl(4096)]
		internal extern Assembly LoadAssembly(string assemblyRef, Evidence securityEvidence, bool refOnly);

		// Token: 0x060001E2 RID: 482 RVA: 0x0000D8E8 File Offset: 0x0000BAE8
		public Assembly Load(AssemblyName assemblyRef)
		{
			return this.Load(assemblyRef, null);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000D8F4 File Offset: 0x0000BAF4
		internal Assembly LoadSatellite(AssemblyName assemblyRef, bool throwOnError)
		{
			if (assemblyRef == null)
			{
				throw new ArgumentNullException("assemblyRef");
			}
			Assembly assembly = this.LoadAssembly(assemblyRef.FullName, null, false);
			if (assembly == null && throwOnError)
			{
				throw new FileNotFoundException(null, assemblyRef.Name);
			}
			return assembly;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000D93C File Offset: 0x0000BB3C
		public Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
		{
			if (assemblyRef == null)
			{
				throw new ArgumentNullException("assemblyRef");
			}
			if (assemblyRef.Name == null || assemblyRef.Name.Length == 0)
			{
				if (assemblyRef.CodeBase != null)
				{
					return Assembly.LoadFrom(assemblyRef.CodeBase, assemblySecurity);
				}
				throw new ArgumentException(Locale.GetText("assemblyRef.Name cannot be empty."), "assemblyRef");
			}
			else
			{
				Assembly assembly = this.LoadAssembly(assemblyRef.FullName, assemblySecurity, false);
				if (assembly != null)
				{
					return assembly;
				}
				if (assemblyRef.CodeBase == null)
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				string text = assemblyRef.CodeBase;
				if (text.ToLower(CultureInfo.InvariantCulture).StartsWith("file://"))
				{
					text = new Uri(text).LocalPath;
				}
				try
				{
					assembly = Assembly.LoadFrom(text, assemblySecurity);
				}
				catch
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				AssemblyName name = assembly.GetName();
				if (assemblyRef.Name != name.Name)
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				if (assemblyRef.Version != new Version() && assemblyRef.Version != name.Version)
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				if (assemblyRef.CultureInfo != null && assemblyRef.CultureInfo.Equals(name))
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				byte[] publicKeyToken = assemblyRef.GetPublicKeyToken();
				if (publicKeyToken != null)
				{
					byte[] publicKeyToken2 = name.GetPublicKeyToken();
					if (publicKeyToken2 == null || publicKeyToken.Length != publicKeyToken2.Length)
					{
						throw new FileNotFoundException(null, assemblyRef.Name);
					}
					for (int i = publicKeyToken.Length - 1; i >= 0; i--)
					{
						if (publicKeyToken2[i] != publicKeyToken[i])
						{
							throw new FileNotFoundException(null, assemblyRef.Name);
						}
					}
				}
				return assembly;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000DB24 File Offset: 0x0000BD24
		public Assembly Load(string assemblyString)
		{
			return this.Load(assemblyString, null, false);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000DB30 File Offset: 0x0000BD30
		public Assembly Load(string assemblyString, Evidence assemblySecurity)
		{
			return this.Load(assemblyString, assemblySecurity, false);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000DB3C File Offset: 0x0000BD3C
		internal Assembly Load(string assemblyString, Evidence assemblySecurity, bool refonly)
		{
			if (assemblyString == null)
			{
				throw new ArgumentNullException("assemblyString");
			}
			if (assemblyString.Length == 0)
			{
				throw new ArgumentException("assemblyString cannot have zero length");
			}
			Assembly assembly = this.LoadAssembly(assemblyString, assemblySecurity, refonly);
			if (assembly == null)
			{
				throw new FileNotFoundException(null, assemblyString);
			}
			return assembly;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		public Assembly Load(byte[] rawAssembly)
		{
			return this.Load(rawAssembly, null, null);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000DB98 File Offset: 0x0000BD98
		public Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
		{
			return this.Load(rawAssembly, rawSymbolStore, null);
		}

		// Token: 0x060001EA RID: 490
		[MethodImpl(4096)]
		internal extern Assembly LoadAssemblyRaw(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence, bool refonly);

		// Token: 0x060001EB RID: 491 RVA: 0x0000DBA4 File Offset: 0x0000BDA4
		public Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
		{
			return this.Load(rawAssembly, rawSymbolStore, securityEvidence, false);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		internal Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence, bool refonly)
		{
			if (rawAssembly == null)
			{
				throw new ArgumentNullException("rawAssembly");
			}
			Assembly assembly = this.LoadAssemblyRaw(rawAssembly, rawSymbolStore, securityEvidence, refonly);
			assembly.FromByteArray = true;
			return assembly;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000DBE4 File Offset: 0x0000BDE4
		public void SetAppDomainPolicy(PolicyLevel domainPolicy)
		{
			if (domainPolicy == null)
			{
				throw new ArgumentNullException("domainPolicy");
			}
			if (this._granted != null)
			{
				throw new PolicyException(Locale.GetText("An AppDomain policy is already specified."));
			}
			if (this.IsFinalizingForUnload())
			{
				throw new AppDomainUnloadedException();
			}
			PolicyStatement policyStatement = domainPolicy.Resolve(this._evidence);
			this._granted = policyStatement.PermissionSet;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000DC48 File Offset: 0x0000BE48
		[Obsolete("Use AppDomainSetup.SetCachePath")]
		public void SetCachePath(string path)
		{
			this.SetupInformationNoCopy.CachePath = path;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000DC58 File Offset: 0x0000BE58
		public void SetPrincipalPolicy(PrincipalPolicy policy)
		{
			if (this.IsFinalizingForUnload())
			{
				throw new AppDomainUnloadedException();
			}
			this._principalPolicy = policy;
			AppDomain._principal = null;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000DC78 File Offset: 0x0000BE78
		[Obsolete("Use AppDomainSetup.ShadowCopyFiles")]
		public void SetShadowCopyFiles()
		{
			this.SetupInformationNoCopy.ShadowCopyFiles = "true";
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000DC8C File Offset: 0x0000BE8C
		[Obsolete("Use AppDomainSetup.ShadowCopyDirectories")]
		public void SetShadowCopyPath(string path)
		{
			this.SetupInformationNoCopy.ShadowCopyDirectories = path;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		public void SetThreadPrincipal(IPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			if (AppDomain._principal != null)
			{
				throw new PolicyException(Locale.GetText("principal already present."));
			}
			if (this.IsFinalizingForUnload())
			{
				throw new AppDomainUnloadedException();
			}
			AppDomain._principal = principal;
		}

		// Token: 0x060001F3 RID: 499
		[MethodImpl(4096)]
		private static extern AppDomain InternalSetDomainByID(int domain_id);

		// Token: 0x060001F4 RID: 500
		[MethodImpl(4096)]
		private static extern AppDomain InternalSetDomain(AppDomain context);

		// Token: 0x060001F5 RID: 501
		[MethodImpl(4096)]
		internal static extern void InternalPushDomainRef(AppDomain domain);

		// Token: 0x060001F6 RID: 502
		[MethodImpl(4096)]
		internal static extern void InternalPushDomainRefByID(int domain_id);

		// Token: 0x060001F7 RID: 503
		[MethodImpl(4096)]
		internal static extern void InternalPopDomainRef();

		// Token: 0x060001F8 RID: 504
		[MethodImpl(4096)]
		internal static extern Context InternalSetContext(Context context);

		// Token: 0x060001F9 RID: 505
		[MethodImpl(4096)]
		internal static extern Context InternalGetContext();

		// Token: 0x060001FA RID: 506
		[MethodImpl(4096)]
		internal static extern Context InternalGetDefaultContext();

		// Token: 0x060001FB RID: 507
		[MethodImpl(4096)]
		internal static extern string InternalGetProcessGuid(string newguid);

		// Token: 0x060001FC RID: 508 RVA: 0x0000DCEC File Offset: 0x0000BEEC
		internal static object InvokeInDomain(AppDomain domain, MethodInfo method, object obj, object[] args)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			bool flag = false;
			object result;
			try
			{
				AppDomain.InternalPushDomainRef(domain);
				flag = true;
				AppDomain.InternalSetDomain(domain);
				Exception ex;
				object obj2 = ((MonoMethod)method).InternalInvoke(obj, args, out ex);
				if (ex != null)
				{
					throw ex;
				}
				result = obj2;
			}
			finally
			{
				AppDomain.InternalSetDomain(currentDomain);
				if (flag)
				{
					AppDomain.InternalPopDomainRef();
				}
			}
			return result;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000DD5C File Offset: 0x0000BF5C
		internal static object InvokeInDomainByID(int domain_id, MethodInfo method, object obj, object[] args)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			bool flag = false;
			object result;
			try
			{
				AppDomain.InternalPushDomainRefByID(domain_id);
				flag = true;
				AppDomain.InternalSetDomainByID(domain_id);
				Exception ex;
				object obj2 = ((MonoMethod)method).InternalInvoke(obj, args, out ex);
				if (ex != null)
				{
					throw ex;
				}
				result = obj2;
			}
			finally
			{
				AppDomain.InternalSetDomain(currentDomain);
				if (flag)
				{
					AppDomain.InternalPopDomainRef();
				}
			}
			return result;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		internal static string GetProcessGuid()
		{
			if (AppDomain._process_guid == null)
			{
				AppDomain._process_guid = AppDomain.InternalGetProcessGuid(Guid.NewGuid().ToString());
			}
			return AppDomain._process_guid;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000DE00 File Offset: 0x0000C000
		public static AppDomain CreateDomain(string friendlyName)
		{
			return AppDomain.CreateDomain(friendlyName, null, null);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000DE0C File Offset: 0x0000C00C
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo)
		{
			return AppDomain.CreateDomain(friendlyName, securityInfo, null);
		}

		// Token: 0x06000201 RID: 513
		[MethodImpl(4096)]
		private static extern AppDomain createDomain(string friendlyName, AppDomainSetup info);

		// Token: 0x06000202 RID: 514 RVA: 0x0000DE18 File Offset: 0x0000C018
		[MonoLimitation("Currently it does not allow the setup in the other domain")]
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info)
		{
			if (friendlyName == null)
			{
				throw new ArgumentNullException("friendlyName");
			}
			AppDomain defaultDomain = AppDomain.DefaultDomain;
			if (info == null)
			{
				if (defaultDomain == null)
				{
					info = new AppDomainSetup();
				}
				else
				{
					info = defaultDomain.SetupInformation;
				}
			}
			else
			{
				info = new AppDomainSetup(info);
			}
			if (defaultDomain != null)
			{
				if (!info.Equals(defaultDomain.SetupInformation))
				{
					if (info.ApplicationBase == null)
					{
						info.ApplicationBase = defaultDomain.SetupInformation.ApplicationBase;
					}
					if (info.ConfigurationFile == null)
					{
						info.ConfigurationFile = Path.GetFileName(defaultDomain.SetupInformation.ConfigurationFile);
					}
				}
			}
			else if (info.ConfigurationFile == null)
			{
				info.ConfigurationFile = "[I don't have a config file]";
			}
			AppDomain appDomain = (AppDomain)RemotingServices.GetDomainProxy(AppDomain.createDomain(friendlyName, info));
			if (securityInfo == null)
			{
				if (defaultDomain == null)
				{
					appDomain._evidence = null;
				}
				else
				{
					appDomain._evidence = defaultDomain.Evidence;
				}
			}
			else
			{
				appDomain._evidence = new Evidence(securityInfo);
			}
			return appDomain;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000DF20 File Offset: 0x0000C120
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles)
		{
			return AppDomain.CreateDomain(friendlyName, securityInfo, AppDomain.CreateDomainSetup(appBasePath, appRelativeSearchPath, shadowCopyFiles));
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000DF34 File Offset: 0x0000C134
		private static AppDomainSetup CreateDomainSetup(string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles)
		{
			AppDomainSetup appDomainSetup = new AppDomainSetup();
			appDomainSetup.ApplicationBase = appBasePath;
			appDomainSetup.PrivateBinPath = appRelativeSearchPath;
			if (shadowCopyFiles)
			{
				appDomainSetup.ShadowCopyFiles = "true";
			}
			else
			{
				appDomainSetup.ShadowCopyFiles = "false";
			}
			return appDomainSetup;
		}

		// Token: 0x06000205 RID: 517
		[MethodImpl(4096)]
		private static extern bool InternalIsFinalizingForUnload(int domain_id);

		// Token: 0x06000206 RID: 518 RVA: 0x0000DF78 File Offset: 0x0000C178
		public bool IsFinalizingForUnload()
		{
			return AppDomain.InternalIsFinalizingForUnload(this.getDomainID());
		}

		// Token: 0x06000207 RID: 519
		[MethodImpl(4096)]
		private static extern void InternalUnload(int domain_id);

		// Token: 0x06000208 RID: 520 RVA: 0x0000DF88 File Offset: 0x0000C188
		private int getDomainID()
		{
			return Thread.GetDomainID();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000DF90 File Offset: 0x0000C190
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.MayFail)]
		public static void Unload(AppDomain domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			AppDomain.InternalUnload(domain.getDomainID());
		}

		// Token: 0x0600020A RID: 522
		[MethodImpl(4096)]
		public extern void SetData(string name, object data);

		// Token: 0x0600020B RID: 523 RVA: 0x0000DFB0 File Offset: 0x0000C1B0
		[MonoTODO]
		public void SetData(string name, object data, IPermission permission)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000DFB8 File Offset: 0x0000C1B8
		[Obsolete("AppDomain.GetCurrentThreadId has been deprecated because it does not provide a stable Id when managed threads are running on fibers (aka lightweight threads). To get a stable identifier for a managed thread, use the ManagedThreadId property on Thread.'")]
		public static int GetCurrentThreadId()
		{
			return Thread.CurrentThreadId;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000DFC0 File Offset: 0x0000C1C0
		public override string ToString()
		{
			return this.getFriendlyName();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000DFC8 File Offset: 0x0000C1C8
		private static void ValidateAssemblyName(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException("The Name of AssemblyName cannot be null or a zero-length string.");
			}
			bool flag = true;
			for (int i = 0; i < name.Length; i++)
			{
				char c = name[i];
				if (i == 0 && char.IsWhiteSpace(c))
				{
					flag = false;
					break;
				}
				if (c == '/' || c == '\\' || c == ':')
				{
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				throw new ArgumentException("The Name of AssemblyName cannot start with whitespace, or contain '/', '\\'  or ':'.");
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000E05C File Offset: 0x0000C25C
		private void DoAssemblyLoad(Assembly assembly)
		{
			if (this.AssemblyLoad == null)
			{
				return;
			}
			this.AssemblyLoad(this, new AssemblyLoadEventArgs(assembly));
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000E07C File Offset: 0x0000C27C
		private Assembly DoAssemblyResolve(string name, bool refonly)
		{
			ResolveEventHandler assemblyResolve = this.AssemblyResolve;
			if (assemblyResolve == null)
			{
				return null;
			}
			Hashtable hashtable;
			if (refonly)
			{
				hashtable = AppDomain.assembly_resolve_in_progress_refonly;
				if (hashtable == null)
				{
					hashtable = new Hashtable();
					AppDomain.assembly_resolve_in_progress_refonly = hashtable;
				}
			}
			else
			{
				hashtable = AppDomain.assembly_resolve_in_progress;
				if (hashtable == null)
				{
					hashtable = new Hashtable();
					AppDomain.assembly_resolve_in_progress = hashtable;
				}
			}
			string text = (string)hashtable[name];
			if (text != null)
			{
				return null;
			}
			hashtable[name] = name;
			Assembly result;
			try
			{
				Delegate[] invocationList = assemblyResolve.GetInvocationList();
				foreach (Delegate @delegate in invocationList)
				{
					ResolveEventHandler resolveEventHandler = (ResolveEventHandler)@delegate;
					Assembly assembly = resolveEventHandler(this, new ResolveEventArgs(name));
					if (assembly != null)
					{
						return assembly;
					}
				}
				result = null;
			}
			finally
			{
				hashtable.Remove(name);
			}
			return result;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000E16C File Offset: 0x0000C36C
		internal Assembly DoTypeResolve(object name_or_tb)
		{
			if (this.TypeResolve == null)
			{
				return null;
			}
			string text;
			if (name_or_tb is TypeBuilder)
			{
				text = ((TypeBuilder)name_or_tb).FullName;
			}
			else
			{
				text = (string)name_or_tb;
			}
			Hashtable hashtable = AppDomain.type_resolve_in_progress;
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				AppDomain.type_resolve_in_progress = hashtable;
			}
			if (hashtable.Contains(text))
			{
				return null;
			}
			hashtable[text] = text;
			Assembly result;
			try
			{
				foreach (Delegate @delegate in this.TypeResolve.GetInvocationList())
				{
					ResolveEventHandler resolveEventHandler = (ResolveEventHandler)@delegate;
					Assembly assembly = resolveEventHandler(this, new ResolveEventArgs(text));
					if (assembly != null)
					{
						return assembly;
					}
				}
				result = null;
			}
			finally
			{
				hashtable.Remove(text);
			}
			return result;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000E250 File Offset: 0x0000C450
		private void DoDomainUnload()
		{
			if (this.DomainUnload != null)
			{
				this.DomainUnload(this, null);
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000E26C File Offset: 0x0000C46C
		internal void ProcessMessageInDomain(byte[] arrRequest, CADMethodCallMessage cadMsg, out byte[] arrResponse, out CADMethodReturnMessage cadMrm)
		{
			IMessage msg;
			if (arrRequest != null)
			{
				msg = CADSerializer.DeserializeMessage(new MemoryStream(arrRequest), null);
			}
			else
			{
				msg = new MethodCall(cadMsg);
			}
			IMessage message = ChannelServices.SyncDispatchMessage(msg);
			cadMrm = CADMethodReturnMessage.Create(message);
			if (cadMrm == null)
			{
				arrResponse = CADSerializer.SerializeMessage(message).GetBuffer();
			}
			else
			{
				arrResponse = null;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000E2C8 File Offset: 0x0000C4C8
		public AppDomainManager DomainManager
		{
			get
			{
				return this._domain_manager;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
		public ActivationContext ActivationContext
		{
			get
			{
				return this._activation;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return this._applicationIdentity;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000E2E0 File Offset: 0x0000C4E0
		public int Id
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.getDomainID();
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000E2E8 File Offset: 0x0000C4E8
		[MonoTODO("This routine only returns the parameter currently")]
		[ComVisible(false)]
		public string ApplyPolicy(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (assemblyName.Length == 0)
			{
				throw new ArgumentException("assemblyName");
			}
			return assemblyName;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000E314 File Offset: 0x0000C514
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles, AppDomainInitializer adInit, string[] adInitArgs)
		{
			AppDomainSetup appDomainSetup = AppDomain.CreateDomainSetup(appBasePath, appRelativeSearchPath, shadowCopyFiles);
			appDomainSetup.AppDomainInitializerArguments = adInitArgs;
			appDomainSetup.AppDomainInitializer = adInit;
			return AppDomain.CreateDomain(friendlyName, securityInfo, appDomainSetup);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000E344 File Offset: 0x0000C544
		public int ExecuteAssemblyByName(string assemblyName)
		{
			return this.ExecuteAssemblyByName(assemblyName, null, null);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000E350 File Offset: 0x0000C550
		public int ExecuteAssemblyByName(string assemblyName, Evidence assemblySecurity)
		{
			return this.ExecuteAssemblyByName(assemblyName, assemblySecurity, null);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000E35C File Offset: 0x0000C55C
		public int ExecuteAssemblyByName(string assemblyName, Evidence assemblySecurity, params string[] args)
		{
			Assembly a = Assembly.Load(assemblyName, assemblySecurity);
			return this.ExecuteAssemblyInternal(a, args);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000E37C File Offset: 0x0000C57C
		public int ExecuteAssemblyByName(AssemblyName assemblyName, Evidence assemblySecurity, params string[] args)
		{
			Assembly a = Assembly.Load(assemblyName, assemblySecurity);
			return this.ExecuteAssemblyInternal(a, args);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000E39C File Offset: 0x0000C59C
		public bool IsDefaultAppDomain()
		{
			return object.ReferenceEquals(this, AppDomain.DefaultDomain);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		public Assembly[] ReflectionOnlyGetAssemblies()
		{
			return this.GetAssemblies(true);
		}

		// Token: 0x0400014F RID: 335
		private IntPtr _mono_app_domain;

		// Token: 0x04000150 RID: 336
		private static string _process_guid;

		// Token: 0x04000151 RID: 337
		[ThreadStatic]
		private static Hashtable type_resolve_in_progress;

		// Token: 0x04000152 RID: 338
		[ThreadStatic]
		private static Hashtable assembly_resolve_in_progress;

		// Token: 0x04000153 RID: 339
		[ThreadStatic]
		private static Hashtable assembly_resolve_in_progress_refonly;

		// Token: 0x04000154 RID: 340
		private Evidence _evidence;

		// Token: 0x04000155 RID: 341
		private PermissionSet _granted;

		// Token: 0x04000156 RID: 342
		private PrincipalPolicy _principalPolicy;

		// Token: 0x04000157 RID: 343
		[ThreadStatic]
		private static IPrincipal _principal;

		// Token: 0x04000158 RID: 344
		private static AppDomain default_domain;

		// Token: 0x04000159 RID: 345
		private AppDomainManager _domain_manager;

		// Token: 0x0400015A RID: 346
		private ActivationContext _activation;

		// Token: 0x0400015B RID: 347
		private ApplicationIdentity _applicationIdentity;
	}
}
