using System;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x02000335 RID: 821
	internal static class PermissionBuilder
	{
		// Token: 0x060018B8 RID: 6328 RVA: 0x0005A7C4 File Offset: 0x000589C4
		public static IPermission Create(string fullname, PermissionState state)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", fullname);
			securityElement.AddAttribute("version", "1");
			if (state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return PermissionBuilder.CreatePermission(fullname, securityElement);
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0005A828 File Offset: 0x00058A28
		public static IPermission Create(SecurityElement se)
		{
			if (se == null)
			{
				throw new ArgumentNullException("se");
			}
			string text = se.Attribute("class");
			if (text == null || text.Length == 0)
			{
				throw new ArgumentException("class");
			}
			return PermissionBuilder.CreatePermission(text, se);
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0005A878 File Offset: 0x00058A78
		public static IPermission Create(Type type)
		{
			return (IPermission)Activator.CreateInstance(type, PermissionBuilder.psNone);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0005A88C File Offset: 0x00058A8C
		internal static IPermission CreatePermission(string fullname, SecurityElement se)
		{
			Type type = Type.GetType(fullname);
			if (type == null)
			{
				string text = Locale.GetText("Can't create an instance of permission class {0}.");
				throw new TypeLoadException(string.Format(text, fullname));
			}
			IPermission permission = PermissionBuilder.Create(type);
			permission.FromXml(se);
			return permission;
		}

		// Token: 0x04000D50 RID: 3408
		private static object[] psNone = new object[]
		{
			PermissionState.None
		};
	}
}
