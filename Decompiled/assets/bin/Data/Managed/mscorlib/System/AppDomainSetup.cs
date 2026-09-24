using System;
using System.IO;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Policy;

namespace System
{
	// Token: 0x02000059 RID: 89
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public sealed class AppDomainSetup
	{
		// Token: 0x06000224 RID: 548 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		public AppDomainSetup()
		{
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000E3C0 File Offset: 0x0000C5C0
		internal AppDomainSetup(AppDomainSetup setup)
		{
			this.application_base = setup.application_base;
			this.application_name = setup.application_name;
			this.cache_path = setup.cache_path;
			this.configuration_file = setup.configuration_file;
			this.dynamic_base = setup.dynamic_base;
			this.license_file = setup.license_file;
			this.private_bin_path = setup.private_bin_path;
			this.private_bin_path_probe = setup.private_bin_path_probe;
			this.shadow_copy_directories = setup.shadow_copy_directories;
			this.shadow_copy_files = setup.shadow_copy_files;
			this.publisher_policy = setup.publisher_policy;
			this.path_changed = setup.path_changed;
			this.loader_optimization = setup.loader_optimization;
			this.disallow_binding_redirects = setup.disallow_binding_redirects;
			this.disallow_code_downloads = setup.disallow_code_downloads;
			this._activationArguments = setup._activationArguments;
			this.domain_initializer = setup.domain_initializer;
			this.domain_initializer_args = setup.domain_initializer_args;
			this.application_trust_xml = setup.application_trust_xml;
			this.disallow_appbase_probe = setup.disallow_appbase_probe;
			this.configuration_bytes = setup.configuration_bytes;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		public AppDomainSetup(ActivationArguments activationArguments)
		{
			this._activationArguments = activationArguments;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		public AppDomainSetup(ActivationContext activationContext)
		{
			this._activationArguments = new ActivationArguments(activationContext);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		private static string GetAppBase(string appBase)
		{
			if (appBase == null)
			{
				return null;
			}
			int length = appBase.Length;
			if (length >= 8 && appBase.ToLower().StartsWith("file://"))
			{
				appBase = appBase.Substring(7);
				if (Path.DirectorySeparatorChar != '/')
				{
					appBase = appBase.Replace('/', Path.DirectorySeparatorChar);
				}
				if (Environment.IsRunningOnWindows)
				{
					appBase = "//" + appBase;
				}
			}
			else
			{
				appBase = Path.GetFullPath(appBase);
			}
			return appBase;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000E578 File Offset: 0x0000C778
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000E588 File Offset: 0x0000C788
		public string ApplicationBase
		{
			get
			{
				return AppDomainSetup.GetAppBase(this.application_base);
			}
			set
			{
				this.application_base = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000E594 File Offset: 0x0000C794
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000E59C File Offset: 0x0000C79C
		public string ApplicationName
		{
			get
			{
				return this.application_name;
			}
			set
			{
				this.application_name = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		public string CachePath
		{
			get
			{
				return this.cache_path;
			}
			set
			{
				this.cache_path = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000E614 File Offset: 0x0000C814
		public string ConfigurationFile
		{
			get
			{
				if (this.configuration_file == null)
				{
					return null;
				}
				if (Path.IsPathRooted(this.configuration_file))
				{
					return this.configuration_file;
				}
				if (this.ApplicationBase == null)
				{
					throw new MemberAccessException("The ApplicationBase must be set before retrieving this property.");
				}
				return Path.Combine(this.ApplicationBase, this.configuration_file);
			}
			set
			{
				this.configuration_file = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000E620 File Offset: 0x0000C820
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000E628 File Offset: 0x0000C828
		public bool DisallowPublisherPolicy
		{
			get
			{
				return this.publisher_policy;
			}
			set
			{
				this.publisher_policy = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000E634 File Offset: 0x0000C834
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000E68C File Offset: 0x0000C88C
		public string DynamicBase
		{
			get
			{
				if (this.dynamic_base == null)
				{
					return null;
				}
				if (Path.IsPathRooted(this.dynamic_base))
				{
					return this.dynamic_base;
				}
				if (this.ApplicationBase == null)
				{
					throw new MemberAccessException("The ApplicationBase must be set before retrieving this property.");
				}
				return Path.Combine(this.ApplicationBase, this.dynamic_base);
			}
			set
			{
				if (this.application_name == null)
				{
					throw new MemberAccessException("ApplicationName must be set before the DynamicBase can be set.");
				}
				this.dynamic_base = Path.Combine(value, ((uint)this.application_name.GetHashCode()).ToString("x"));
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000E6D4 File Offset: 0x0000C8D4
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		public string LicenseFile
		{
			get
			{
				return this.license_file;
			}
			set
			{
				this.license_file = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000E6E8 File Offset: 0x0000C8E8
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		[MonoLimitation("In Mono this is controlled by the --share-code flag")]
		public LoaderOptimization LoaderOptimization
		{
			get
			{
				return this.loader_optimization;
			}
			set
			{
				this.loader_optimization = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000E6FC File Offset: 0x0000C8FC
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000E704 File Offset: 0x0000C904
		public string PrivateBinPath
		{
			get
			{
				return this.private_bin_path;
			}
			set
			{
				this.private_bin_path = value;
				this.path_changed = true;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000E714 File Offset: 0x0000C914
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000E71C File Offset: 0x0000C91C
		public string PrivateBinPathProbe
		{
			get
			{
				return this.private_bin_path_probe;
			}
			set
			{
				this.private_bin_path_probe = value;
				this.path_changed = true;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000E72C File Offset: 0x0000C92C
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000E734 File Offset: 0x0000C934
		public string ShadowCopyDirectories
		{
			get
			{
				return this.shadow_copy_directories;
			}
			set
			{
				this.shadow_copy_directories = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000E740 File Offset: 0x0000C940
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000E748 File Offset: 0x0000C948
		public string ShadowCopyFiles
		{
			get
			{
				return this.shadow_copy_files;
			}
			set
			{
				this.shadow_copy_files = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000E754 File Offset: 0x0000C954
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000E75C File Offset: 0x0000C95C
		public bool DisallowBindingRedirects
		{
			get
			{
				return this.disallow_binding_redirects;
			}
			set
			{
				this.disallow_binding_redirects = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000E768 File Offset: 0x0000C968
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000E770 File Offset: 0x0000C970
		public bool DisallowCodeDownload
		{
			get
			{
				return this.disallow_code_downloads;
			}
			set
			{
				this.disallow_code_downloads = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000E77C File Offset: 0x0000C97C
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000E784 File Offset: 0x0000C984
		public ActivationArguments ActivationArguments
		{
			get
			{
				return this._activationArguments;
			}
			set
			{
				this._activationArguments = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000E790 File Offset: 0x0000C990
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000E798 File Offset: 0x0000C998
		[MonoLimitation("it needs to be invoked within the created domain")]
		public AppDomainInitializer AppDomainInitializer
		{
			get
			{
				return this.domain_initializer;
			}
			set
			{
				this.domain_initializer = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000E7A4 File Offset: 0x0000C9A4
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000E7AC File Offset: 0x0000C9AC
		[MonoLimitation("it needs to be used to invoke the initializer within the created domain")]
		public string[] AppDomainInitializerArguments
		{
			get
			{
				return this.domain_initializer_args;
			}
			set
			{
				this.domain_initializer_args = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000E7E4 File Offset: 0x0000C9E4
		[MonoNotSupported("This property exists but not considered.")]
		public ApplicationTrust ApplicationTrust
		{
			get
			{
				if (this.application_trust_xml == null)
				{
					return null;
				}
				if (this.application_trust == null)
				{
					this.application_trust = new ApplicationTrust();
				}
				return this.application_trust;
			}
			set
			{
				this.application_trust = value;
				if (value != null)
				{
					this.application_trust_xml = value.ToXml();
					this.application_trust.FromXml(this.application_trust_xml);
				}
				else
				{
					this.application_trust_xml = null;
				}
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000E81C File Offset: 0x0000CA1C
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000E824 File Offset: 0x0000CA24
		[MonoNotSupported("This property exists but not considered.")]
		public bool DisallowApplicationBaseProbing
		{
			get
			{
				return this.disallow_appbase_probe;
			}
			set
			{
				this.disallow_appbase_probe = value;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000E830 File Offset: 0x0000CA30
		[MonoNotSupported("This method exists but not considered.")]
		public byte[] GetConfigurationBytes()
		{
			return (this.configuration_bytes == null) ? null : (this.configuration_bytes.Clone() as byte[]);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000E854 File Offset: 0x0000CA54
		[MonoNotSupported("This method exists but not considered.")]
		public void SetConfigurationBytes(byte[] value)
		{
			this.configuration_bytes = value;
		}

		// Token: 0x04000169 RID: 361
		private string application_base;

		// Token: 0x0400016A RID: 362
		private string application_name;

		// Token: 0x0400016B RID: 363
		private string cache_path;

		// Token: 0x0400016C RID: 364
		private string configuration_file;

		// Token: 0x0400016D RID: 365
		private string dynamic_base;

		// Token: 0x0400016E RID: 366
		private string license_file;

		// Token: 0x0400016F RID: 367
		private string private_bin_path;

		// Token: 0x04000170 RID: 368
		private string private_bin_path_probe;

		// Token: 0x04000171 RID: 369
		private string shadow_copy_directories;

		// Token: 0x04000172 RID: 370
		private string shadow_copy_files;

		// Token: 0x04000173 RID: 371
		private bool publisher_policy;

		// Token: 0x04000174 RID: 372
		private bool path_changed;

		// Token: 0x04000175 RID: 373
		private LoaderOptimization loader_optimization;

		// Token: 0x04000176 RID: 374
		private bool disallow_binding_redirects;

		// Token: 0x04000177 RID: 375
		private bool disallow_code_downloads;

		// Token: 0x04000178 RID: 376
		private ActivationArguments _activationArguments;

		// Token: 0x04000179 RID: 377
		private AppDomainInitializer domain_initializer;

		// Token: 0x0400017A RID: 378
		[NonSerialized]
		private ApplicationTrust application_trust;

		// Token: 0x0400017B RID: 379
		private string[] domain_initializer_args;

		// Token: 0x0400017C RID: 380
		private SecurityElement application_trust_xml;

		// Token: 0x0400017D RID: 381
		private bool disallow_appbase_probe;

		// Token: 0x0400017E RID: 382
		private byte[] configuration_bytes;
	}
}
