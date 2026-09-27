using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Xml;

namespace System.Security.Policy
{
	// Token: 0x02000368 RID: 872
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyLevel
	{
		// Token: 0x060019CB RID: 6603 RVA: 0x0005F454 File Offset: 0x0005D654
		internal PolicyLevel(string label, PolicyLevelType type)
		{
			this.label = label;
			this._type = type;
			this.full_trust_assemblies = new ArrayList();
			this.named_permission_sets = new ArrayList();
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0005F480 File Offset: 0x0005D680
		internal void LoadFromFile(string filename)
		{
			try
			{
				if (!File.Exists(filename))
				{
					string text = filename + ".default";
					if (File.Exists(text))
					{
						File.Copy(text, filename);
					}
				}
				if (File.Exists(filename))
				{
					using (StreamReader streamReader = File.OpenText(filename))
					{
						this.xml = this.FromString(streamReader.ReadToEnd());
					}
					try
					{
						SecurityManager.ResolvingPolicyLevel = this;
						this.FromXml(this.xml);
					}
					finally
					{
						SecurityManager.ResolvingPolicyLevel = this;
					}
				}
				else
				{
					this.CreateDefaultFullTrustAssemblies();
					this.CreateDefaultNamedPermissionSets();
					this.CreateDefaultLevel(this._type);
					this.Save();
				}
			}
			catch
			{
			}
			finally
			{
				this._location = filename;
			}
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0005F574 File Offset: 0x0005D774
		private SecurityElement FromString(string xml)
		{
			SecurityParser securityParser = new SecurityParser();
			securityParser.LoadXml(xml);
			SecurityElement securityElement = securityParser.ToXml();
			if (securityElement.Tag != "configuration")
			{
				throw new ArgumentException(Locale.GetText("missing <configuration> root element"));
			}
			SecurityElement securityElement2 = (SecurityElement)securityElement.Children[0];
			if (securityElement2.Tag != "mscorlib")
			{
				throw new ArgumentException(Locale.GetText("missing <mscorlib> tag"));
			}
			SecurityElement securityElement3 = (SecurityElement)securityElement2.Children[0];
			if (securityElement3.Tag != "security")
			{
				throw new ArgumentException(Locale.GetText("missing <security> tag"));
			}
			SecurityElement securityElement4 = (SecurityElement)securityElement3.Children[0];
			if (securityElement4.Tag != "policy")
			{
				throw new ArgumentException(Locale.GetText("missing <policy> tag"));
			}
			return (SecurityElement)securityElement4.Children[0];
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0005F678 File Offset: 0x0005D878
		public void FromXml(SecurityElement e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			SecurityElement securityElement = e.SearchForChildByTag("SecurityClasses");
			if (securityElement != null && securityElement.Children != null && securityElement.Children.Count > 0)
			{
				this.fullNames = new Hashtable(securityElement.Children.Count);
				foreach (object obj in securityElement.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					this.fullNames.Add(securityElement2.Attributes["Name"], securityElement2.Attributes["Description"]);
				}
			}
			SecurityElement securityElement3 = e.SearchForChildByTag("FullTrustAssemblies");
			if (securityElement3 != null && securityElement3.Children != null && securityElement3.Children.Count > 0)
			{
				this.full_trust_assemblies.Clear();
				foreach (object obj2 in securityElement3.Children)
				{
					SecurityElement securityElement4 = (SecurityElement)obj2;
					if (securityElement4.Tag != "IMembershipCondition")
					{
						throw new ArgumentException(Locale.GetText("Invalid XML"));
					}
					string text = securityElement4.Attribute("class");
					if (text.IndexOf("StrongNameMembershipCondition") < 0)
					{
						throw new ArgumentException(Locale.GetText("Invalid XML - must be StrongNameMembershipCondition"));
					}
					this.full_trust_assemblies.Add(new StrongNameMembershipCondition(securityElement4));
				}
			}
			SecurityElement securityElement5 = e.SearchForChildByTag("CodeGroup");
			if (securityElement5 != null && securityElement5.Children != null && securityElement5.Children.Count > 0)
			{
				this.root_code_group = CodeGroup.CreateFromXml(securityElement5, this);
				SecurityElement securityElement6 = e.SearchForChildByTag("NamedPermissionSets");
				if (securityElement6 != null && securityElement6.Children != null && securityElement6.Children.Count > 0)
				{
					this.named_permission_sets.Clear();
					foreach (object obj3 in securityElement6.Children)
					{
						SecurityElement et = (SecurityElement)obj3;
						NamedPermissionSet namedPermissionSet = new NamedPermissionSet();
						namedPermissionSet.Resolver = this;
						namedPermissionSet.FromXml(et);
						this.named_permission_sets.Add(namedPermissionSet);
					}
				}
				return;
			}
			throw new ArgumentException(Locale.GetText("Missing Root CodeGroup"));
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0005F958 File Offset: 0x0005DB58
		public NamedPermissionSet GetNamedPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				if (namedPermissionSet.Name == name)
				{
					return (NamedPermissionSet)namedPermissionSet.Copy();
				}
			}
			return null;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0005F9EC File Offset: 0x0005DBEC
		public PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			PolicyStatement policyStatement = this.root_code_group.Resolve(evidence);
			return (policyStatement == null) ? PolicyStatement.Empty() : policyStatement;
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0005FA28 File Offset: 0x0005DC28
		public SecurityElement ToXml()
		{
			Hashtable hashtable = new Hashtable();
			if (this.full_trust_assemblies.Count > 0 && !hashtable.Contains("StrongNameMembershipCondition"))
			{
				hashtable.Add("StrongNameMembershipCondition", typeof(StrongNameMembershipCondition).FullName);
			}
			SecurityElement securityElement = new SecurityElement("NamedPermissionSets");
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				SecurityElement securityElement2 = namedPermissionSet.ToXml();
				object key = securityElement2.Attributes["class"];
				if (!hashtable.Contains(key))
				{
					hashtable.Add(key, namedPermissionSet.GetType().FullName);
				}
				securityElement.AddChild(securityElement2);
			}
			SecurityElement securityElement3 = new SecurityElement("FullTrustAssemblies");
			foreach (object obj2 in this.full_trust_assemblies)
			{
				StrongNameMembershipCondition strongNameMembershipCondition = (StrongNameMembershipCondition)obj2;
				securityElement3.AddChild(strongNameMembershipCondition.ToXml(this));
			}
			SecurityElement securityElement4 = new SecurityElement("SecurityClasses");
			if (hashtable.Count > 0)
			{
				foreach (object obj3 in hashtable)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
					SecurityElement securityElement5 = new SecurityElement("SecurityClass");
					securityElement5.AddAttribute("Name", (string)dictionaryEntry.Key);
					securityElement5.AddAttribute("Description", (string)dictionaryEntry.Value);
					securityElement4.AddChild(securityElement5);
				}
			}
			SecurityElement securityElement6 = new SecurityElement(typeof(PolicyLevel).Name);
			securityElement6.AddAttribute("version", "1");
			securityElement6.AddChild(securityElement4);
			securityElement6.AddChild(securityElement);
			if (this.root_code_group != null)
			{
				securityElement6.AddChild(this.root_code_group.ToXml(this));
			}
			securityElement6.AddChild(securityElement3);
			return securityElement6;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x0005FC90 File Offset: 0x0005DE90
		internal void Save()
		{
			if (this._type == PolicyLevelType.AppDomain)
			{
				throw new PolicyException(Locale.GetText("Can't save AppDomain PolicyLevel"));
			}
			if (this._location != null)
			{
				try
				{
					if (File.Exists(this._location))
					{
						File.Copy(this._location, this._location + ".backup", true);
					}
				}
				catch (Exception)
				{
				}
				finally
				{
					using (StreamWriter streamWriter = new StreamWriter(this._location))
					{
						streamWriter.Write(this.ToXml().ToString());
						streamWriter.Close();
					}
				}
			}
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0005FD5C File Offset: 0x0005DF5C
		internal void CreateDefaultLevel(PolicyLevelType type)
		{
			PolicyStatement policy = new PolicyStatement(DefaultPolicies.FullTrust);
			switch (type)
			{
			case PolicyLevelType.User:
			case PolicyLevelType.Enterprise:
			case PolicyLevelType.AppDomain:
				this.root_code_group = new UnionCodeGroup(new AllMembershipCondition(), policy);
				this.root_code_group.Name = "All_Code";
				break;
			case PolicyLevelType.Machine:
			{
				PolicyStatement policy2 = new PolicyStatement(DefaultPolicies.Nothing);
				this.root_code_group = new UnionCodeGroup(new AllMembershipCondition(), policy2);
				this.root_code_group.Name = "All_Code";
				UnionCodeGroup unionCodeGroup = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.MyComputer), policy);
				unionCodeGroup.Name = "My_Computer_Zone";
				this.root_code_group.AddChild(unionCodeGroup);
				UnionCodeGroup unionCodeGroup2 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Intranet), new PolicyStatement(DefaultPolicies.LocalIntranet));
				unionCodeGroup2.Name = "LocalIntranet_Zone";
				this.root_code_group.AddChild(unionCodeGroup2);
				PolicyStatement policy3 = new PolicyStatement(DefaultPolicies.Internet);
				UnionCodeGroup unionCodeGroup3 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Internet), policy3);
				unionCodeGroup3.Name = "Internet_Zone";
				this.root_code_group.AddChild(unionCodeGroup3);
				UnionCodeGroup unionCodeGroup4 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Untrusted), policy2);
				unionCodeGroup4.Name = "Restricted_Zone";
				this.root_code_group.AddChild(unionCodeGroup4);
				UnionCodeGroup unionCodeGroup5 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Trusted), policy3);
				unionCodeGroup5.Name = "Trusted_Zone";
				this.root_code_group.AddChild(unionCodeGroup5);
				break;
			}
			}
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0005FEC0 File Offset: 0x0005E0C0
		internal void CreateDefaultFullTrustAssemblies()
		{
			this.full_trust_assemblies.Clear();
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("mscorlib", DefaultPolicies.Key.Ecma));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System", DefaultPolicies.Key.Ecma));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.Data", DefaultPolicies.Key.Ecma));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.DirectoryServices", DefaultPolicies.Key.MsFinal));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.Drawing", DefaultPolicies.Key.MsFinal));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.Messaging", DefaultPolicies.Key.MsFinal));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.ServiceProcess", DefaultPolicies.Key.MsFinal));
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x0005FF7C File Offset: 0x0005E17C
		internal void CreateDefaultNamedPermissionSets()
		{
			this.named_permission_sets.Clear();
			try
			{
				SecurityManager.ResolvingPolicyLevel = this;
				this.named_permission_sets.Add(DefaultPolicies.LocalIntranet);
				this.named_permission_sets.Add(DefaultPolicies.Internet);
				this.named_permission_sets.Add(DefaultPolicies.SkipVerification);
				this.named_permission_sets.Add(DefaultPolicies.Execution);
				this.named_permission_sets.Add(DefaultPolicies.Nothing);
				this.named_permission_sets.Add(DefaultPolicies.Everything);
				this.named_permission_sets.Add(DefaultPolicies.FullTrust);
			}
			finally
			{
				SecurityManager.ResolvingPolicyLevel = null;
			}
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00060030 File Offset: 0x0005E230
		internal bool IsFullTrustAssembly(Assembly a)
		{
			AssemblyName assemblyName = a.UnprotectedGetName();
			StrongNamePublicKeyBlob blob = new StrongNamePublicKeyBlob(assemblyName.GetPublicKey());
			StrongNameMembershipCondition o = new StrongNameMembershipCondition(blob, assemblyName.Name, assemblyName.Version);
			foreach (object obj in this.full_trust_assemblies)
			{
				StrongNameMembershipCondition strongNameMembershipCondition = (StrongNameMembershipCondition)obj;
				if (strongNameMembershipCondition.Equals(o))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000E27 RID: 3623
		private string label;

		// Token: 0x04000E28 RID: 3624
		private CodeGroup root_code_group;

		// Token: 0x04000E29 RID: 3625
		private ArrayList full_trust_assemblies;

		// Token: 0x04000E2A RID: 3626
		private ArrayList named_permission_sets;

		// Token: 0x04000E2B RID: 3627
		private string _location;

		// Token: 0x04000E2C RID: 3628
		private PolicyLevelType _type;

		// Token: 0x04000E2D RID: 3629
		private Hashtable fullNames;

		// Token: 0x04000E2E RID: 3630
		private SecurityElement xml;
	}
}
