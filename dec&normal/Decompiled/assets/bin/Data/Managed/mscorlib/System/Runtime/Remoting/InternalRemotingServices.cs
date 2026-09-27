using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Remoting
{
	// Token: 0x0200028D RID: 653
	[ComVisible(true)]
	public class InternalRemotingServices
	{
		// Token: 0x060014EF RID: 5359 RVA: 0x00049D98 File Offset: 0x00047F98
		public static SoapAttribute GetCachedSoapAttribute(object reflectionObject)
		{
			object syncRoot = InternalRemotingServices._soapAttributes.SyncRoot;
			SoapAttribute result;
			lock (syncRoot)
			{
				SoapAttribute soapAttribute = InternalRemotingServices._soapAttributes[reflectionObject] as SoapAttribute;
				if (soapAttribute != null)
				{
					result = soapAttribute;
				}
				else
				{
					ICustomAttributeProvider customAttributeProvider = (ICustomAttributeProvider)reflectionObject;
					object[] customAttributes = customAttributeProvider.GetCustomAttributes(typeof(SoapAttribute), true);
					if (customAttributes.Length > 0)
					{
						soapAttribute = (SoapAttribute)customAttributes[0];
					}
					else if (reflectionObject is Type)
					{
						soapAttribute = new SoapTypeAttribute();
					}
					else if (reflectionObject is FieldInfo)
					{
						soapAttribute = new SoapFieldAttribute();
					}
					else if (reflectionObject is MethodBase)
					{
						soapAttribute = new SoapMethodAttribute();
					}
					else if (reflectionObject is ParameterInfo)
					{
						soapAttribute = new SoapParameterAttribute();
					}
					soapAttribute.SetReflectionObject(reflectionObject);
					InternalRemotingServices._soapAttributes[reflectionObject] = soapAttribute;
					result = soapAttribute;
				}
			}
			return result;
		}

		// Token: 0x04000ACD RID: 2765
		private static Hashtable _soapAttributes = new Hashtable();
	}
}
