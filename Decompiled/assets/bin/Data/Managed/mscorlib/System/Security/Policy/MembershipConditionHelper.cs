using System;

namespace System.Security.Policy
{
	// Token: 0x02000364 RID: 868
	internal sealed class MembershipConditionHelper
	{
		// Token: 0x060019BA RID: 6586 RVA: 0x0005EE48 File Offset: 0x0005D048
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != MembershipConditionHelper.XmlTag)
			{
				string message = string.Format(Locale.GetText("Invalid tag {0}, expected {1}."), se.Tag, MembershipConditionHelper.XmlTag);
				throw new ArgumentException(message, parameterName);
			}
			int result = minimumVersion;
			string text = se.Attribute("version");
			if (text != null)
			{
				try
				{
					result = int.Parse(text);
				}
				catch (Exception innerException)
				{
					string text2 = Locale.GetText("Couldn't parse version from '{0}'.");
					text2 = string.Format(text2, text);
					throw new ArgumentException(text2, parameterName, innerException);
				}
			}
			return result;
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0005EEF4 File Offset: 0x0005D0F4
		internal static SecurityElement Element(Type type, int version)
		{
			SecurityElement securityElement = new SecurityElement(MembershipConditionHelper.XmlTag);
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x04000E1F RID: 3615
		private static readonly string XmlTag = "IMembershipCondition";
	}
}
