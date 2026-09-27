using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using Mono.Xml;

namespace System.Runtime.Remoting
{
	// Token: 0x020002D5 RID: 725
	[ComVisible(true)]
	public static class RemotingConfiguration
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0004F1E4 File Offset: 0x0004D3E4
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x0004F1EC File Offset: 0x0004D3EC
		public static string ApplicationName
		{
			get
			{
				return RemotingConfiguration.applicationName;
			}
			set
			{
				RemotingConfiguration.applicationName = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0004F1F4 File Offset: 0x0004D3F4
		public static string ProcessId
		{
			get
			{
				if (RemotingConfiguration.processGuid == null)
				{
					RemotingConfiguration.processGuid = AppDomain.GetProcessGuid();
				}
				return RemotingConfiguration.processGuid;
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0004F210 File Offset: 0x0004D410
		internal static void LoadDefaultDelayedChannels()
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				if (!RemotingConfiguration.defaultDelayedConfigRead && !RemotingConfiguration.defaultConfigRead)
				{
					SmallXmlParser smallXmlParser = new SmallXmlParser();
					using (TextReader textReader = new StreamReader(Environment.GetMachineConfigPath()))
					{
						ConfigHandler handler = new ConfigHandler(true);
						smallXmlParser.Parse(textReader, handler);
					}
					RemotingConfiguration.defaultDelayedConfigRead = true;
				}
			}
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0004F2A4 File Offset: 0x0004D4A4
		public static bool IsActivationAllowed(Type svrType)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			bool result;
			lock (obj)
			{
				result = RemotingConfiguration.activatedServiceEntries.ContainsKey(svrType);
			}
			return result;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0004F2EC File Offset: 0x0004D4EC
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(Type svrType)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			ActivatedClientTypeEntry result;
			lock (obj)
			{
				result = (RemotingConfiguration.activatedClientEntries[svrType] as ActivatedClientTypeEntry);
			}
			return result;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0004F338 File Offset: 0x0004D538
		public static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				if (RemotingConfiguration.wellKnownClientEntries.ContainsKey(entry.ObjectType) || RemotingConfiguration.activatedClientEntries.ContainsKey(entry.ObjectType))
				{
					throw new RemotingException("Attempt to redirect activation of type '" + entry.ObjectType.FullName + "' which is already redirected.");
				}
				RemotingConfiguration.activatedClientEntries[entry.ObjectType] = entry;
				ActivationServices.EnableProxyActivation(entry.ObjectType, true);
			}
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0004F3D4 File Offset: 0x0004D5D4
		public static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				RemotingConfiguration.activatedServiceEntries.Add(entry.ObjectType, entry);
			}
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0004F41C File Offset: 0x0004D61C
		public static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				if (RemotingConfiguration.wellKnownClientEntries.ContainsKey(entry.ObjectType) || RemotingConfiguration.activatedClientEntries.ContainsKey(entry.ObjectType))
				{
					throw new RemotingException("Attempt to redirect activation of type '" + entry.ObjectType.FullName + "' which is already redirected.");
				}
				RemotingConfiguration.wellKnownClientEntries[entry.ObjectType] = entry;
				ActivationServices.EnableProxyActivation(entry.ObjectType, true);
			}
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0004F4B8 File Offset: 0x0004D6B8
		public static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				RemotingConfiguration.wellKnownServiceEntries[entry.ObjectUri] = entry;
				RemotingServices.CreateWellKnownServerIdentity(entry.ObjectType, entry.ObjectUri, entry.Mode);
			}
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0004F518 File Offset: 0x0004D718
		internal static void RegisterChannelTemplate(ChannelData channel)
		{
			RemotingConfiguration.channelTemplates[channel.Id] = channel;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0004F52C File Offset: 0x0004D72C
		internal static void RegisterClientProviderTemplate(ProviderData prov)
		{
			RemotingConfiguration.clientProviderTemplates[prov.Id] = prov;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0004F540 File Offset: 0x0004D740
		internal static void RegisterServerProviderTemplate(ProviderData prov)
		{
			RemotingConfiguration.serverProviderTemplates[prov.Id] = prov;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0004F554 File Offset: 0x0004D754
		internal static void RegisterChannels(ArrayList channels, bool onlyDelayed)
		{
			foreach (object obj in channels)
			{
				ChannelData channelData = (ChannelData)obj;
				if (!onlyDelayed || !(channelData.DelayLoadAsClientChannel != "true"))
				{
					if (!RemotingConfiguration.defaultDelayedConfigRead || !(channelData.DelayLoadAsClientChannel == "true"))
					{
						if (channelData.Ref != null)
						{
							ChannelData channelData2 = (ChannelData)RemotingConfiguration.channelTemplates[channelData.Ref];
							if (channelData2 == null)
							{
								throw new RemotingException("Channel template '" + channelData.Ref + "' not found");
							}
							channelData.CopyFrom(channelData2);
						}
						foreach (object obj2 in channelData.ServerProviders)
						{
							ProviderData providerData = (ProviderData)obj2;
							if (providerData.Ref != null)
							{
								ProviderData providerData2 = (ProviderData)RemotingConfiguration.serverProviderTemplates[providerData.Ref];
								if (providerData2 == null)
								{
									throw new RemotingException("Provider template '" + providerData.Ref + "' not found");
								}
								providerData.CopyFrom(providerData2);
							}
						}
						foreach (object obj3 in channelData.ClientProviders)
						{
							ProviderData providerData3 = (ProviderData)obj3;
							if (providerData3.Ref != null)
							{
								ProviderData providerData4 = (ProviderData)RemotingConfiguration.clientProviderTemplates[providerData3.Ref];
								if (providerData4 == null)
								{
									throw new RemotingException("Provider template '" + providerData3.Ref + "' not found");
								}
								providerData3.CopyFrom(providerData4);
							}
						}
						ChannelServices.RegisterChannelConfig(channelData);
					}
				}
			}
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0004F7A8 File Offset: 0x0004D9A8
		internal static void RegisterTypes(ArrayList types)
		{
			foreach (object obj in types)
			{
				TypeEntry typeEntry = (TypeEntry)obj;
				if (typeEntry is ActivatedClientTypeEntry)
				{
					RemotingConfiguration.RegisterActivatedClientType((ActivatedClientTypeEntry)typeEntry);
				}
				else if (typeEntry is ActivatedServiceTypeEntry)
				{
					RemotingConfiguration.RegisterActivatedServiceType((ActivatedServiceTypeEntry)typeEntry);
				}
				else if (typeEntry is WellKnownClientTypeEntry)
				{
					RemotingConfiguration.RegisterWellKnownClientType((WellKnownClientTypeEntry)typeEntry);
				}
				else if (typeEntry is WellKnownServiceTypeEntry)
				{
					RemotingConfiguration.RegisterWellKnownServiceType((WellKnownServiceTypeEntry)typeEntry);
				}
			}
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0004F868 File Offset: 0x0004DA68
		public static bool CustomErrorsEnabled(bool isLocalRequest)
		{
			return !(RemotingConfiguration._errorMode == "off") && (RemotingConfiguration._errorMode == "on" || !isLocalRequest);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0004F89C File Offset: 0x0004DA9C
		internal static void SetCustomErrorsMode(string mode)
		{
			if (mode == null)
			{
				throw new RemotingException("mode attribute is required");
			}
			string text = mode.ToLower();
			if (text != "on" && text != "off" && text != "remoteonly")
			{
				throw new RemotingException("Invalid custom error mode: " + mode);
			}
			RemotingConfiguration._errorMode = text;
		}

		// Token: 0x04000BB1 RID: 2993
		private static string applicationID = null;

		// Token: 0x04000BB2 RID: 2994
		private static string applicationName = null;

		// Token: 0x04000BB3 RID: 2995
		private static string processGuid = null;

		// Token: 0x04000BB4 RID: 2996
		private static bool defaultConfigRead = false;

		// Token: 0x04000BB5 RID: 2997
		private static bool defaultDelayedConfigRead = false;

		// Token: 0x04000BB6 RID: 2998
		private static string _errorMode;

		// Token: 0x04000BB7 RID: 2999
		private static Hashtable wellKnownClientEntries = new Hashtable();

		// Token: 0x04000BB8 RID: 3000
		private static Hashtable activatedClientEntries = new Hashtable();

		// Token: 0x04000BB9 RID: 3001
		private static Hashtable wellKnownServiceEntries = new Hashtable();

		// Token: 0x04000BBA RID: 3002
		private static Hashtable activatedServiceEntries = new Hashtable();

		// Token: 0x04000BBB RID: 3003
		private static Hashtable channelTemplates = new Hashtable();

		// Token: 0x04000BBC RID: 3004
		private static Hashtable clientProviderTemplates = new Hashtable();

		// Token: 0x04000BBD RID: 3005
		private static Hashtable serverProviderTemplates = new Hashtable();
	}
}
