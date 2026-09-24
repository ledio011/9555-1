using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Remoting
{
	// Token: 0x020002DE RID: 734
	[ComVisible(true)]
	public class SoapServices
	{
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x00051174 File Offset: 0x0004F374
		public static string XmlNsForClrTypeWithAssembly
		{
			get
			{
				return "http://schemas.microsoft.com/clr/assem/";
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x0005117C File Offset: 0x0004F37C
		public static string XmlNsForClrTypeWithNs
		{
			get
			{
				return "http://schemas.microsoft.com/clr/ns/";
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00051184 File Offset: 0x0004F384
		public static string XmlNsForClrTypeWithNsAndAssembly
		{
			get
			{
				return "http://schemas.microsoft.com/clr/nsassem/";
			}
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0005118C File Offset: 0x0004F38C
		public static string CodeXmlNamespaceForClrTypeNamespace(string typeNamespace, string assemblyName)
		{
			if (assemblyName == string.Empty)
			{
				return SoapServices.XmlNsForClrTypeWithNs + typeNamespace;
			}
			if (typeNamespace == string.Empty)
			{
				return SoapServices.EncodeNs(SoapServices.XmlNsForClrTypeWithAssembly + assemblyName);
			}
			return SoapServices.EncodeNs(SoapServices.XmlNsForClrTypeWithNsAndAssembly + typeNamespace + "/" + assemblyName);
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x000511EC File Offset: 0x0004F3EC
		private static string GetNameKey(string name, string namspace)
		{
			if (namspace == null)
			{
				return name;
			}
			return name + " " + namspace;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00051204 File Offset: 0x0004F404
		private static string GetAssemblyName(MethodBase mb)
		{
			if (mb.DeclaringType.Assembly == typeof(object).Assembly)
			{
				return string.Empty;
			}
			return mb.DeclaringType.Assembly.GetName().Name;
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00051240 File Offset: 0x0004F440
		public static bool GetXmlElementForInteropType(Type type, out string xmlElement, out string xmlNamespace)
		{
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (!soapTypeAttribute.IsInteropXmlElement)
			{
				xmlElement = null;
				xmlNamespace = null;
				return false;
			}
			xmlElement = soapTypeAttribute.XmlElementName;
			xmlNamespace = soapTypeAttribute.XmlNamespace;
			return true;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00051280 File Offset: 0x0004F480
		public static string GetXmlNamespaceForMethodCall(MethodBase mb)
		{
			return SoapServices.CodeXmlNamespaceForClrTypeNamespace(mb.DeclaringType.FullName, SoapServices.GetAssemblyName(mb));
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00051298 File Offset: 0x0004F498
		public static string GetXmlNamespaceForMethodResponse(MethodBase mb)
		{
			return SoapServices.CodeXmlNamespaceForClrTypeNamespace(mb.DeclaringType.FullName, SoapServices.GetAssemblyName(mb));
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x000512B0 File Offset: 0x0004F4B0
		public static bool GetXmlTypeForInteropType(Type type, out string xmlType, out string xmlTypeNamespace)
		{
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (!soapTypeAttribute.IsInteropXmlType)
			{
				xmlType = null;
				xmlTypeNamespace = null;
				return false;
			}
			xmlType = soapTypeAttribute.XmlTypeName;
			xmlTypeNamespace = soapTypeAttribute.XmlTypeNamespace;
			return true;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000512F0 File Offset: 0x0004F4F0
		public static void PreLoad(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypes())
			{
				SoapServices.PreLoad(type);
			}
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00051324 File Offset: 0x0004F524
		public static void PreLoad(Type type)
		{
			SoapServices.TypeInfo typeInfo = SoapServices._typeInfos[type] as SoapServices.TypeInfo;
			if (typeInfo != null)
			{
				return;
			}
			string text;
			string text2;
			if (SoapServices.GetXmlTypeForInteropType(type, out text, out text2))
			{
				SoapServices.RegisterInteropXmlType(text, text2, type);
			}
			if (SoapServices.GetXmlElementForInteropType(type, out text, out text2))
			{
				SoapServices.RegisterInteropXmlElement(text, text2, type);
			}
			object syncRoot = SoapServices._typeInfos.SyncRoot;
			lock (syncRoot)
			{
				typeInfo = new SoapServices.TypeInfo();
				FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (FieldInfo fieldInfo in fields)
				{
					SoapFieldAttribute soapFieldAttribute = (SoapFieldAttribute)InternalRemotingServices.GetCachedSoapAttribute(fieldInfo);
					if (soapFieldAttribute.IsInteropXmlElement())
					{
						string nameKey = SoapServices.GetNameKey(soapFieldAttribute.XmlElementName, soapFieldAttribute.XmlNamespace);
						if (soapFieldAttribute.UseAttribute)
						{
							if (typeInfo.Attributes == null)
							{
								typeInfo.Attributes = new Hashtable();
							}
							typeInfo.Attributes[nameKey] = fieldInfo;
						}
						else
						{
							if (typeInfo.Elements == null)
							{
								typeInfo.Elements = new Hashtable();
							}
							typeInfo.Elements[nameKey] = fieldInfo;
						}
					}
				}
				SoapServices._typeInfos[type] = typeInfo;
			}
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00051474 File Offset: 0x0004F674
		public static void RegisterInteropXmlElement(string xmlElement, string xmlNamespace, Type type)
		{
			object syncRoot = SoapServices._xmlElements.SyncRoot;
			lock (syncRoot)
			{
				SoapServices._xmlElements[xmlElement + " " + xmlNamespace] = type;
			}
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x000514C8 File Offset: 0x0004F6C8
		public static void RegisterInteropXmlType(string xmlType, string xmlTypeNamespace, Type type)
		{
			object syncRoot = SoapServices._xmlTypes.SyncRoot;
			lock (syncRoot)
			{
				SoapServices._xmlTypes[xmlType + " " + xmlTypeNamespace] = type;
			}
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0005151C File Offset: 0x0004F71C
		private static string EncodeNs(string ns)
		{
			ns = ns.Replace(",", "%2C");
			ns = ns.Replace(" ", "%20");
			return ns.Replace("=", "%3D");
		}

		// Token: 0x04000BCE RID: 3022
		private static Hashtable _xmlTypes = new Hashtable();

		// Token: 0x04000BCF RID: 3023
		private static Hashtable _xmlElements = new Hashtable();

		// Token: 0x04000BD0 RID: 3024
		private static Hashtable _soapActions = new Hashtable();

		// Token: 0x04000BD1 RID: 3025
		private static Hashtable _soapActionsMethods = new Hashtable();

		// Token: 0x04000BD2 RID: 3026
		private static Hashtable _typeInfos = new Hashtable();

		// Token: 0x020002DF RID: 735
		private class TypeInfo
		{
			// Token: 0x04000BD3 RID: 3027
			public Hashtable Attributes;

			// Token: 0x04000BD4 RID: 3028
			public Hashtable Elements;
		}
	}
}
