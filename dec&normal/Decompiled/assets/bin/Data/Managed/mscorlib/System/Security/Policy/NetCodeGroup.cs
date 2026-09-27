using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000365 RID: 869
	[ComVisible(true)]
	[Serializable]
	public sealed class NetCodeGroup : CodeGroup
	{
		// Token: 0x060019BC RID: 6588 RVA: 0x0005EF50 File Offset: 0x0005D150
		public NetCodeGroup(IMembershipCondition membershipCondition) : base(membershipCondition, null)
		{
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0005EF68 File Offset: 0x0005D168
		internal NetCodeGroup(SecurityElement e, PolicyLevel level) : base(e, level)
		{
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0005EF98 File Offset: 0x0005D198
		public override CodeGroup Copy()
		{
			NetCodeGroup netCodeGroup = new NetCodeGroup(base.MembershipCondition);
			netCodeGroup.Name = base.Name;
			netCodeGroup.Description = base.Description;
			netCodeGroup.PolicyStatement = base.PolicyStatement;
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				netCodeGroup.AddChild(codeGroup.Copy());
			}
			return netCodeGroup;
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0005F034 File Offset: 0x0005D234
		private bool Equals(CodeConnectAccess[] rules1, CodeConnectAccess[] rules2)
		{
			for (int i = 0; i < rules1.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < rules2.Length; j++)
				{
					if (rules1[i].Equals(rules2[j]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0005F08C File Offset: 0x0005D28C
		public override bool Equals(object o)
		{
			if (!base.Equals(o))
			{
				return false;
			}
			NetCodeGroup netCodeGroup = o as NetCodeGroup;
			if (netCodeGroup == null)
			{
				return false;
			}
			foreach (object obj in this._rules)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				CodeConnectAccess[] array = (CodeConnectAccess[])netCodeGroup._rules[dictionaryEntry.Key];
				bool flag;
				if (array != null)
				{
					flag = this.Equals((CodeConnectAccess[])dictionaryEntry.Value, array);
				}
				else
				{
					flag = (dictionaryEntry.Value == null);
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0005F164 File Offset: 0x0005D364
		public override int GetHashCode()
		{
			if (this._hashcode == 0)
			{
				this._hashcode = base.GetHashCode();
				foreach (object obj in this._rules)
				{
					CodeConnectAccess[] array = (CodeConnectAccess[])((DictionaryEntry)obj).Value;
					if (array != null)
					{
						foreach (CodeConnectAccess codeConnectAccess in array)
						{
							this._hashcode ^= codeConnectAccess.GetHashCode();
						}
					}
				}
			}
			return this._hashcode;
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0005F228 File Offset: 0x0005D428
		public override PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (!base.MembershipCondition.Check(evidence))
			{
				return null;
			}
			PermissionSet permissionSet = null;
			if (base.PolicyStatement == null)
			{
				permissionSet = new PermissionSet(PermissionState.None);
			}
			else
			{
				permissionSet = base.PolicyStatement.PermissionSet.Copy();
			}
			if (base.Children.Count > 0)
			{
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup = (CodeGroup)obj;
					PolicyStatement policyStatement = codeGroup.Resolve(evidence);
					if (policyStatement != null)
					{
						permissionSet = permissionSet.Union(policyStatement.PermissionSet);
					}
				}
			}
			PolicyStatement policyStatement2 = base.PolicyStatement.Copy();
			policyStatement2.PermissionSet = permissionSet;
			return policyStatement2;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0005F318 File Offset: 0x0005D518
		[MonoTODO("(2.0) Add new stuff (CodeConnectAccess) into XML")]
		protected override void CreateXml(SecurityElement element, PolicyLevel level)
		{
			base.CreateXml(element, level);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0005F324 File Offset: 0x0005D524
		[MonoTODO("(2.0) Parse new stuff (CodeConnectAccess) from XML")]
		protected override void ParseXml(SecurityElement e, PolicyLevel level)
		{
			base.ParseXml(e, level);
		}

		// Token: 0x04000E20 RID: 3616
		public static readonly string AbsentOriginScheme = string.Empty;

		// Token: 0x04000E21 RID: 3617
		public static readonly string AnyOtherOriginScheme = "*";

		// Token: 0x04000E22 RID: 3618
		private Hashtable _rules = new Hashtable();

		// Token: 0x04000E23 RID: 3619
		private int _hashcode;
	}
}
