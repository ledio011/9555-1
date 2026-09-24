using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000359 RID: 857
	internal static class DefaultPolicies
	{
		// Token: 0x0600197D RID: 6525 RVA: 0x0005DEBC File Offset: 0x0005C0BC
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultPolicies()
		{
			byte[] array = new byte[16];
			array[8] = 4;
			DefaultPolicies._ecmaKey = array;
			DefaultPolicies._msFinalKey = new byte[]
			{
				0,
				36,
				0,
				0,
				4,
				128,
				0,
				0,
				148,
				0,
				0,
				0,
				6,
				2,
				0,
				0,
				0,
				36,
				0,
				0,
				82,
				83,
				65,
				49,
				0,
				4,
				0,
				0,
				1,
				0,
				1,
				0,
				7,
				209,
				250,
				87,
				196,
				174,
				217,
				240,
				163,
				46,
				132,
				170,
				15,
				174,
				253,
				13,
				233,
				232,
				253,
				106,
				236,
				143,
				135,
				251,
				3,
				118,
				108,
				131,
				76,
				153,
				146,
				30,
				178,
				59,
				231,
				154,
				217,
				213,
				220,
				193,
				221,
				154,
				210,
				54,
				19,
				33,
				2,
				144,
				11,
				114,
				60,
				249,
				128,
				149,
				127,
				196,
				225,
				119,
				16,
				143,
				198,
				7,
				119,
				79,
				41,
				232,
				50,
				14,
				146,
				234,
				5,
				236,
				228,
				232,
				33,
				192,
				165,
				239,
				232,
				241,
				100,
				92,
				76,
				12,
				147,
				193,
				171,
				153,
				40,
				93,
				98,
				44,
				170,
				101,
				44,
				29,
				250,
				214,
				61,
				116,
				93,
				111,
				45,
				229,
				241,
				126,
				94,
				175,
				15,
				196,
				150,
				61,
				38,
				28,
				138,
				18,
				67,
				101,
				24,
				32,
				109,
				192,
				147,
				52,
				77,
				90,
				210,
				147
			};
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x0005DEE8 File Offset: 0x0005C0E8
		public static PermissionSet FullTrust
		{
			get
			{
				if (DefaultPolicies._fullTrust == null)
				{
					DefaultPolicies._fullTrust = DefaultPolicies.BuildFullTrust();
				}
				return DefaultPolicies._fullTrust;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x0005DF04 File Offset: 0x0005C104
		public static PermissionSet LocalIntranet
		{
			get
			{
				if (DefaultPolicies._localIntranet == null)
				{
					DefaultPolicies._localIntranet = DefaultPolicies.BuildLocalIntranet();
				}
				return DefaultPolicies._localIntranet;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x0005DF20 File Offset: 0x0005C120
		public static PermissionSet Internet
		{
			get
			{
				if (DefaultPolicies._internet == null)
				{
					DefaultPolicies._internet = DefaultPolicies.BuildInternet();
				}
				return DefaultPolicies._internet;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x0005DF3C File Offset: 0x0005C13C
		public static PermissionSet SkipVerification
		{
			get
			{
				if (DefaultPolicies._skipVerification == null)
				{
					DefaultPolicies._skipVerification = DefaultPolicies.BuildSkipVerification();
				}
				return DefaultPolicies._skipVerification;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x0005DF58 File Offset: 0x0005C158
		public static PermissionSet Execution
		{
			get
			{
				if (DefaultPolicies._execution == null)
				{
					DefaultPolicies._execution = DefaultPolicies.BuildExecution();
				}
				return DefaultPolicies._execution;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x0005DF74 File Offset: 0x0005C174
		public static PermissionSet Nothing
		{
			get
			{
				if (DefaultPolicies._nothing == null)
				{
					DefaultPolicies._nothing = DefaultPolicies.BuildNothing();
				}
				return DefaultPolicies._nothing;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x0005DF90 File Offset: 0x0005C190
		public static PermissionSet Everything
		{
			get
			{
				if (DefaultPolicies._everything == null)
				{
					DefaultPolicies._everything = DefaultPolicies.BuildEverything();
				}
				return DefaultPolicies._everything;
			}
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0005DFAC File Offset: 0x0005C1AC
		public static StrongNameMembershipCondition FullTrustMembership(string name, DefaultPolicies.Key key)
		{
			StrongNamePublicKeyBlob blob = null;
			if (key != DefaultPolicies.Key.Ecma)
			{
				if (key == DefaultPolicies.Key.MsFinal)
				{
					if (DefaultPolicies._msFinal == null)
					{
						DefaultPolicies._msFinal = new StrongNamePublicKeyBlob(DefaultPolicies._msFinalKey);
					}
					blob = DefaultPolicies._msFinal;
				}
			}
			else
			{
				if (DefaultPolicies._ecma == null)
				{
					DefaultPolicies._ecma = new StrongNamePublicKeyBlob(DefaultPolicies._ecmaKey);
				}
				blob = DefaultPolicies._ecma;
			}
			if (DefaultPolicies._fxVersion == null)
			{
				DefaultPolicies._fxVersion = new Version("2.0.5.0");
			}
			return new StrongNameMembershipCondition(blob, name, DefaultPolicies._fxVersion);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0005E044 File Offset: 0x0005C244
		private static NamedPermissionSet BuildFullTrust()
		{
			return new NamedPermissionSet("FullTrust", PermissionState.Unrestricted);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0005E054 File Offset: 0x0005C254
		private static NamedPermissionSet BuildLocalIntranet()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("LocalIntranet", PermissionState.None);
			namedPermissionSet.AddPermission(new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME;USER"));
			namedPermissionSet.AddPermission(new FileDialogPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.None)
			{
				UsageAllowed = IsolatedStorageContainment.AssemblyIsolationByUser,
				UserQuota = long.MaxValue
			});
			namedPermissionSet.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.ReflectionEmit));
			SecurityPermissionFlag flag = SecurityPermissionFlag.Assertion | SecurityPermissionFlag.Execution;
			namedPermissionSet.AddPermission(new SecurityPermission(flag));
			namedPermissionSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.DnsPermission, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create(DefaultPolicies.PrintingPermission("SafePrinting")));
			return namedPermissionSet;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0005E108 File Offset: 0x0005C308
		private static NamedPermissionSet BuildInternet()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Internet", PermissionState.None);
			namedPermissionSet.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.Open));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.None)
			{
				UsageAllowed = IsolatedStorageContainment.DomainIsolationByUser,
				UserQuota = 512000L
			});
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			namedPermissionSet.AddPermission(new UIPermission(UIPermissionWindow.SafeTopLevelWindows, UIPermissionClipboard.OwnClipboard));
			namedPermissionSet.AddPermission(PermissionBuilder.Create(DefaultPolicies.PrintingPermission("SafePrinting")));
			return namedPermissionSet;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0005E184 File Offset: 0x0005C384
		private static NamedPermissionSet BuildSkipVerification()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("SkipVerification", PermissionState.None);
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.SkipVerification));
			return namedPermissionSet;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0005E1AC File Offset: 0x0005C3AC
		private static NamedPermissionSet BuildExecution()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Execution", PermissionState.None);
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			return namedPermissionSet;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0005E1D4 File Offset: 0x0005C3D4
		private static NamedPermissionSet BuildNothing()
		{
			return new NamedPermissionSet("Nothing", PermissionState.None);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0005E1E4 File Offset: 0x0005C3E4
		private static NamedPermissionSet BuildEverything()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Everything", PermissionState.None);
			namedPermissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new FileDialogPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new ReflectionPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new RegistryPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new KeyContainerPermission(PermissionState.Unrestricted));
			SecurityPermissionFlag securityPermissionFlag = SecurityPermissionFlag.AllFlags;
			securityPermissionFlag &= ~SecurityPermissionFlag.SkipVerification;
			namedPermissionSet.AddPermission(new SecurityPermission(securityPermissionFlag));
			namedPermissionSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.DnsPermission, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Diagnostics.EventLogPermission, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.SocketPermission, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.WebPermission, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.DirectoryServices.DirectoryServicesPermission, System.DirectoryServices, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.ServiceProcess.ServiceControllerPermission, System.ServiceProcess, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Data.OleDb.OleDbPermission, System.Data, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			return namedPermissionSet;
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0005E344 File Offset: 0x0005C544
		private static SecurityElement PrintingPermission(string level)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", "System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("Level", level);
			return securityElement;
		}

		// Token: 0x04000DF6 RID: 3574
		private const string DnsPermissionClass = "System.Net.DnsPermission, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000DF7 RID: 3575
		private const string EventLogPermissionClass = "System.Diagnostics.EventLogPermission, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000DF8 RID: 3576
		private const string PrintingPermissionClass = "System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000DF9 RID: 3577
		private const string SocketPermissionClass = "System.Net.SocketPermission, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000DFA RID: 3578
		private const string WebPermissionClass = "System.Net.WebPermission, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000DFB RID: 3579
		private const string PerformanceCounterPermissionClass = "System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000DFC RID: 3580
		private const string DirectoryServicesPermissionClass = "System.DirectoryServices.DirectoryServicesPermission, System.DirectoryServices, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000DFD RID: 3581
		private const string MessageQueuePermissionClass = "System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000DFE RID: 3582
		private const string ServiceControllerPermissionClass = "System.ServiceProcess.ServiceControllerPermission, System.ServiceProcess, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000DFF RID: 3583
		private const string OleDbPermissionClass = "System.Data.OleDb.OleDbPermission, System.Data, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000E00 RID: 3584
		private const string SqlClientPermissionClass = "System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000E01 RID: 3585
		private const string DataProtectionPermissionClass = "System.Security.Permissions.DataProtectionPermission, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000E02 RID: 3586
		private const string StorePermissionClass = "System.Security.Permissions.StorePermission, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000E03 RID: 3587
		private static Version _fxVersion;

		// Token: 0x04000E04 RID: 3588
		private static byte[] _ecmaKey;

		// Token: 0x04000E05 RID: 3589
		private static StrongNamePublicKeyBlob _ecma;

		// Token: 0x04000E06 RID: 3590
		private static byte[] _msFinalKey;

		// Token: 0x04000E07 RID: 3591
		private static StrongNamePublicKeyBlob _msFinal;

		// Token: 0x04000E08 RID: 3592
		private static NamedPermissionSet _fullTrust;

		// Token: 0x04000E09 RID: 3593
		private static NamedPermissionSet _localIntranet;

		// Token: 0x04000E0A RID: 3594
		private static NamedPermissionSet _internet;

		// Token: 0x04000E0B RID: 3595
		private static NamedPermissionSet _skipVerification;

		// Token: 0x04000E0C RID: 3596
		private static NamedPermissionSet _execution;

		// Token: 0x04000E0D RID: 3597
		private static NamedPermissionSet _nothing;

		// Token: 0x04000E0E RID: 3598
		private static NamedPermissionSet _everything;

		// Token: 0x0200035A RID: 858
		public enum Key
		{
			// Token: 0x04000E11 RID: 3601
			Ecma,
			// Token: 0x04000E12 RID: 3602
			MsFinal
		}
	}
}
