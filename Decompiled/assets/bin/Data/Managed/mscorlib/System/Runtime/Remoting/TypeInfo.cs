using System;

namespace System.Runtime.Remoting
{
	// Token: 0x020002E1 RID: 737
	[Serializable]
	internal class TypeInfo : IRemotingTypeInfo
	{
		// Token: 0x06001723 RID: 5923 RVA: 0x0005158C File Offset: 0x0004F78C
		public TypeInfo(Type type)
		{
			if (type.IsInterface)
			{
				this.serverType = typeof(MarshalByRefObject).AssemblyQualifiedName;
				this.serverHierarchy = new string[0];
				this.interfacesImplemented = new string[]
				{
					type.AssemblyQualifiedName
				};
			}
			else
			{
				this.serverType = type.AssemblyQualifiedName;
				int num = 0;
				Type baseType = type.BaseType;
				while (baseType != typeof(MarshalByRefObject) && baseType != typeof(object))
				{
					baseType = baseType.BaseType;
					num++;
				}
				this.serverHierarchy = new string[num];
				baseType = type.BaseType;
				for (int i = 0; i < num; i++)
				{
					this.serverHierarchy[i] = baseType.AssemblyQualifiedName;
					baseType = baseType.BaseType;
				}
				Type[] interfaces = type.GetInterfaces();
				this.interfacesImplemented = new string[interfaces.Length];
				for (int j = 0; j < interfaces.Length; j++)
				{
					this.interfacesImplemented[j] = interfaces[j].AssemblyQualifiedName;
				}
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x000516A4 File Offset: 0x0004F8A4
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x000516AC File Offset: 0x0004F8AC
		public string TypeName
		{
			get
			{
				return this.serverType;
			}
			set
			{
				this.serverType = value;
			}
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x000516B8 File Offset: 0x0004F8B8
		public bool CanCastTo(Type fromType, object o)
		{
			if (fromType == typeof(object))
			{
				return true;
			}
			if (fromType == typeof(MarshalByRefObject))
			{
				return true;
			}
			string text = fromType.AssemblyQualifiedName;
			int num = text.IndexOf(',');
			if (num != -1)
			{
				num = text.IndexOf(',', num + 1);
			}
			if (num != -1)
			{
				text = text.Substring(0, num + 1);
			}
			else
			{
				text += ",";
			}
			if ((this.serverType + ",").StartsWith(text))
			{
				return true;
			}
			if (this.serverHierarchy != null)
			{
				foreach (string str in this.serverHierarchy)
				{
					if ((str + ",").StartsWith(text))
					{
						return true;
					}
				}
			}
			if (this.interfacesImplemented != null)
			{
				foreach (string str2 in this.interfacesImplemented)
				{
					if ((str2 + ",").StartsWith(text))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000BD7 RID: 3031
		private string serverType;

		// Token: 0x04000BD8 RID: 3032
		private string[] serverHierarchy;

		// Token: 0x04000BD9 RID: 3033
		private string[] interfacesImplemented;
	}
}
