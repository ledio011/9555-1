using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x0200035B RID: 859
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with .NET")]
	[Serializable]
	public sealed class Evidence : ICollection, IEnumerable
	{
		// Token: 0x0600198E RID: 6542 RVA: 0x0005E38C File Offset: 0x0005C58C
		public Evidence()
		{
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0005E394 File Offset: 0x0005C594
		public Evidence(Evidence evidence)
		{
			if (evidence != null)
			{
				this.Merge(evidence);
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x0005E3AC File Offset: 0x0005C5AC
		public int Count
		{
			get
			{
				int num = 0;
				if (this.hostEvidenceList != null)
				{
					num += this.hostEvidenceList.Count;
				}
				if (this.assemblyEvidenceList != null)
				{
					num += this.assemblyEvidenceList.Count;
				}
				return num;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x0005E3F0 File Offset: 0x0005C5F0
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x0005E3F4 File Offset: 0x0005C5F4
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x0005E3F8 File Offset: 0x0005C5F8
		internal ArrayList HostEvidenceList
		{
			get
			{
				if (this.hostEvidenceList == null)
				{
					this.hostEvidenceList = ArrayList.Synchronized(new ArrayList());
				}
				return this.hostEvidenceList;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x0005E41C File Offset: 0x0005C61C
		internal ArrayList AssemblyEvidenceList
		{
			get
			{
				if (this.assemblyEvidenceList == null)
				{
					this.assemblyEvidenceList = ArrayList.Synchronized(new ArrayList());
				}
				return this.assemblyEvidenceList;
			}
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0005E440 File Offset: 0x0005C640
		public void AddAssembly(object id)
		{
			this.AssemblyEvidenceList.Add(id);
			this._hashCode = 0;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0005E458 File Offset: 0x0005C658
		public void AddHost(object id)
		{
			if (this._locked && SecurityManager.SecurityEnabled)
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			this.HostEvidenceList.Add(id);
			this._hashCode = 0;
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0005E490 File Offset: 0x0005C690
		public void CopyTo(Array array, int index)
		{
			int num = 0;
			if (this.hostEvidenceList != null)
			{
				num = this.hostEvidenceList.Count;
				if (num > 0)
				{
					this.hostEvidenceList.CopyTo(array, index);
				}
			}
			if (this.assemblyEvidenceList != null && this.assemblyEvidenceList.Count > 0)
			{
				this.assemblyEvidenceList.CopyTo(array, index + num);
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0005E4F8 File Offset: 0x0005C6F8
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Evidence evidence = obj as Evidence;
			if (evidence == null)
			{
				return false;
			}
			if (this.HostEvidenceList.Count != evidence.HostEvidenceList.Count)
			{
				return false;
			}
			if (this.AssemblyEvidenceList.Count != evidence.AssemblyEvidenceList.Count)
			{
				return false;
			}
			for (int i = 0; i < this.hostEvidenceList.Count; i++)
			{
				bool flag = false;
				int j = 0;
				while (j < evidence.hostEvidenceList.Count)
				{
					if (this.hostEvidenceList[i].Equals(evidence.hostEvidenceList[j]))
					{
						flag = true;
						break;
					}
					i++;
				}
				if (!flag)
				{
					return false;
				}
			}
			for (int k = 0; k < this.assemblyEvidenceList.Count; k++)
			{
				bool flag2 = false;
				int l = 0;
				while (l < evidence.assemblyEvidenceList.Count)
				{
					if (this.assemblyEvidenceList[k].Equals(evidence.assemblyEvidenceList[l]))
					{
						flag2 = true;
						break;
					}
					k++;
				}
				if (!flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0005E63C File Offset: 0x0005C83C
		public IEnumerator GetEnumerator()
		{
			IEnumerator hostenum = null;
			if (this.hostEvidenceList != null)
			{
				hostenum = this.hostEvidenceList.GetEnumerator();
			}
			IEnumerator assemblyenum = null;
			if (this.assemblyEvidenceList != null)
			{
				assemblyenum = this.assemblyEvidenceList.GetEnumerator();
			}
			return new Evidence.EvidenceEnumerator(hostenum, assemblyenum);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0005E684 File Offset: 0x0005C884
		[ComVisible(false)]
		public override int GetHashCode()
		{
			if (this._hashCode == 0)
			{
				if (this.hostEvidenceList != null)
				{
					for (int i = 0; i < this.hostEvidenceList.Count; i++)
					{
						this._hashCode ^= this.hostEvidenceList[i].GetHashCode();
					}
				}
				if (this.assemblyEvidenceList != null)
				{
					for (int j = 0; j < this.assemblyEvidenceList.Count; j++)
					{
						this._hashCode ^= this.assemblyEvidenceList[j].GetHashCode();
					}
				}
			}
			return this._hashCode;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0005E72C File Offset: 0x0005C92C
		public IEnumerator GetHostEnumerator()
		{
			return this.HostEvidenceList.GetEnumerator();
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0005E73C File Offset: 0x0005C93C
		public void Merge(Evidence evidence)
		{
			if (evidence != null && evidence.Count > 0)
			{
				if (evidence.hostEvidenceList != null)
				{
					foreach (object id in evidence.hostEvidenceList)
					{
						this.AddHost(id);
					}
				}
				if (evidence.assemblyEvidenceList != null)
				{
					foreach (object id2 in evidence.assemblyEvidenceList)
					{
						this.AddAssembly(id2);
					}
				}
				this._hashCode = 0;
			}
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0005E81C File Offset: 0x0005CA1C
		internal static Evidence GetDefaultHostEvidence(Assembly a)
		{
			return new Evidence();
		}

		// Token: 0x04000E13 RID: 3603
		private bool _locked;

		// Token: 0x04000E14 RID: 3604
		private ArrayList hostEvidenceList;

		// Token: 0x04000E15 RID: 3605
		private ArrayList assemblyEvidenceList;

		// Token: 0x04000E16 RID: 3606
		private int _hashCode;

		// Token: 0x0200035C RID: 860
		private class EvidenceEnumerator : IEnumerator
		{
			// Token: 0x0600199E RID: 6558 RVA: 0x0005E824 File Offset: 0x0005CA24
			public EvidenceEnumerator(IEnumerator hostenum, IEnumerator assemblyenum)
			{
				this.hostEnum = hostenum;
				this.assemblyEnum = assemblyenum;
				this.currentEnum = this.hostEnum;
			}

			// Token: 0x0600199F RID: 6559 RVA: 0x0005E848 File Offset: 0x0005CA48
			public bool MoveNext()
			{
				if (this.currentEnum == null)
				{
					return false;
				}
				bool flag = this.currentEnum.MoveNext();
				if (!flag && this.hostEnum == this.currentEnum && this.assemblyEnum != null)
				{
					this.currentEnum = this.assemblyEnum;
					flag = this.assemblyEnum.MoveNext();
				}
				return flag;
			}

			// Token: 0x060019A0 RID: 6560 RVA: 0x0005E8AC File Offset: 0x0005CAAC
			public void Reset()
			{
				if (this.hostEnum != null)
				{
					this.hostEnum.Reset();
					this.currentEnum = this.hostEnum;
				}
				else
				{
					this.currentEnum = this.assemblyEnum;
				}
				if (this.assemblyEnum != null)
				{
					this.assemblyEnum.Reset();
				}
			}

			// Token: 0x170004A1 RID: 1185
			// (get) Token: 0x060019A1 RID: 6561 RVA: 0x0005E904 File Offset: 0x0005CB04
			public object Current
			{
				get
				{
					return this.currentEnum.Current;
				}
			}

			// Token: 0x04000E17 RID: 3607
			private IEnumerator currentEnum;

			// Token: 0x04000E18 RID: 3608
			private IEnumerator hostEnum;

			// Token: 0x04000E19 RID: 3609
			private IEnumerator assemblyEnum;
		}
	}
}
