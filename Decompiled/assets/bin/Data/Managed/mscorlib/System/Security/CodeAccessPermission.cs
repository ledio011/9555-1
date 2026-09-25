using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x02000320 RID: 800
	[ComVisible(true)]
	[MonoTODO("CAS support is experimental (and unsupported).")]
	[Serializable]
	public abstract class CodeAccessPermission : IPermission, ISecurityEncodable, IStackWalk
	{
		// Token: 0x06001862 RID: 6242 RVA: 0x00058810 File Offset: 0x00056A10
		public void Demand()
		{
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00058814 File Offset: 0x00056A14
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj.GetType() != base.GetType())
			{
				return false;
			}
			CodeAccessPermission codeAccessPermission = obj as CodeAccessPermission;
			return this.IsSubsetOf(codeAccessPermission) && codeAccessPermission.IsSubsetOf(this);
		}

		// Token: 0x06001864 RID: 6244
		public abstract void FromXml(SecurityElement elem);

		// Token: 0x06001865 RID: 6245 RVA: 0x0005885C File Offset: 0x00056A5C
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001866 RID: 6246
		public abstract bool IsSubsetOf(IPermission target);

		// Token: 0x06001867 RID: 6247 RVA: 0x00058864 File Offset: 0x00056A64
		public override string ToString()
		{
			SecurityElement securityElement = this.ToXml();
			return securityElement.ToString();
		}

		// Token: 0x06001868 RID: 6248
		public abstract SecurityElement ToXml();

		// Token: 0x06001869 RID: 6249 RVA: 0x00058880 File Offset: 0x00056A80
		internal SecurityElement Element(int version)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			Type type = base.GetType();
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x000588E4 File Offset: 0x00056AE4
		internal static PermissionState CheckPermissionState(PermissionState state, bool allowUnrestricted)
		{
			if (state != PermissionState.None)
			{
				if (state != PermissionState.Unrestricted)
				{
					string message = string.Format(Locale.GetText("Invalid enum {0}"), state);
					throw new ArgumentException(message, "state");
				}
			}
			return state;
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00058934 File Offset: 0x00056B34
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != "IPermission")
			{
				string message = string.Format(Locale.GetText("Invalid tag {0}"), se.Tag);
				throw new ArgumentException(message, parameterName);
			}
			int num = minimumVersion;
			string text = se.Attribute("version");
			if (text != null)
			{
				try
				{
					num = int.Parse(text);
				}
				catch (Exception innerException)
				{
					string text2 = Locale.GetText("Couldn't parse version from '{0}'.");
					text2 = string.Format(text2, text);
					throw new ArgumentException(text2, parameterName, innerException);
				}
			}
			if (num < minimumVersion || num > maximumVersion)
			{
				string text3 = Locale.GetText("Unknown version '{0}', expected versions between ['{1}','{2}'].");
				text3 = string.Format(text3, num, minimumVersion, maximumVersion);
				throw new ArgumentException(text3, parameterName);
			}
			return num;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00058A18 File Offset: 0x00056C18
		internal static bool IsUnrestricted(SecurityElement se)
		{
			string text = se.Attribute("Unrestricted");
			return text != null && string.Compare(text, bool.TrueString, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00058A50 File Offset: 0x00056C50
		internal static void ThrowInvalidPermission(IPermission target, Type expected)
		{
			string text = Locale.GetText("Invalid permission type '{0}', expected type '{1}'.");
			text = string.Format(text, target.GetType(), expected);
			throw new ArgumentException(text, "target");
		}
	}
}
