using System;
using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002B9 RID: 697
	[Serializable]
	internal class MethodDictionary : ICollection, IDictionary, IEnumerable
	{
		// Token: 0x060015C9 RID: 5577 RVA: 0x0004C77C File Offset: 0x0004A97C
		public MethodDictionary(IMethodMessage message)
		{
			this._message = message;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x0004C78C File Offset: 0x0004A98C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new MethodDictionary.DictionaryEnumerator(this);
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x0004C794 File Offset: 0x0004A994
		internal bool HasInternalProperties
		{
			get
			{
				if (this._internalProperties == null)
				{
					return false;
				}
				if (this._internalProperties is MethodDictionary)
				{
					return ((MethodDictionary)this._internalProperties).HasInternalProperties;
				}
				return this._internalProperties.Count > 0;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x0004C7D4 File Offset: 0x0004A9D4
		internal IDictionary InternalProperties
		{
			get
			{
				if (this._internalProperties != null && this._internalProperties is MethodDictionary)
				{
					return ((MethodDictionary)this._internalProperties).InternalProperties;
				}
				return this._internalProperties;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (set) Token: 0x060015CD RID: 5581 RVA: 0x0004C808 File Offset: 0x0004AA08
		public string[] MethodKeys
		{
			set
			{
				this._methodKeys = value;
			}
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0004C814 File Offset: 0x0004AA14
		protected virtual IDictionary AllocInternalProperties()
		{
			this._ownProperties = true;
			return new Hashtable();
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0004C824 File Offset: 0x0004AA24
		public IDictionary GetInternalProperties()
		{
			if (this._internalProperties == null)
			{
				this._internalProperties = this.AllocInternalProperties();
			}
			return this._internalProperties;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0004C844 File Offset: 0x0004AA44
		private bool IsOverridenKey(string key)
		{
			if (this._ownProperties)
			{
				return false;
			}
			foreach (string b in this._methodKeys)
			{
				if (key == b)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x0004C88C File Offset: 0x0004AA8C
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0004C890 File Offset: 0x0004AA90
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F3 RID: 1011
		public object this[object key]
		{
			get
			{
				string text = (string)key;
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					if (this._methodKeys[i] == text)
					{
						return this.GetMethodProperty(text);
					}
				}
				if (this._internalProperties != null)
				{
					return this._internalProperties[key];
				}
				return null;
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0004C904 File Offset: 0x0004AB04
		protected virtual object GetMethodProperty(string key)
		{
			switch (key)
			{
			case "__Uri":
				return this._message.Uri;
			case "__MethodName":
				return this._message.MethodName;
			case "__TypeName":
				return this._message.TypeName;
			case "__MethodSignature":
				return this._message.MethodSignature;
			case "__CallContext":
				return this._message.LogicalCallContext;
			case "__Args":
				return this._message.Args;
			case "__OutArgs":
				return ((IMethodReturnMessage)this._message).OutArgs;
			case "__Return":
				return ((IMethodReturnMessage)this._message).ReturnValue;
			}
			return null;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0004CA38 File Offset: 0x0004AC38
		protected virtual void SetMethodProperty(string key, object value)
		{
			switch (key)
			{
			case "__CallContext":
			case "__OutArgs":
			case "__Return":
				return;
			case "__MethodName":
			case "__TypeName":
			case "__MethodSignature":
			case "__Args":
				throw new ArgumentException("key was invalid");
			case "__Uri":
				((IInternalMessage)this._message).Uri = (string)value;
				return;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0004CB10 File Offset: 0x0004AD10
		public ICollection Keys
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					arrayList.Add(this._methodKeys[i]);
				}
				if (this._internalProperties != null)
				{
					foreach (object obj in this._internalProperties.Keys)
					{
						string text = (string)obj;
						if (!this.IsOverridenKey(text))
						{
							arrayList.Add(text);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0004CBC4 File Offset: 0x0004ADC4
		public ICollection Values
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					arrayList.Add(this.GetMethodProperty(this._methodKeys[i]));
				}
				if (this._internalProperties != null)
				{
					foreach (object obj in this._internalProperties)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (!this.IsOverridenKey((string)dictionaryEntry.Key))
						{
							arrayList.Add(dictionaryEntry.Value);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0004CC88 File Offset: 0x0004AE88
		public void Add(object key, object value)
		{
			string text = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == text)
				{
					this.SetMethodProperty(text, value);
					return;
				}
			}
			if (this._internalProperties == null)
			{
				this._internalProperties = this.AllocInternalProperties();
			}
			this._internalProperties[key] = value;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0004CCF8 File Offset: 0x0004AEF8
		public void Clear()
		{
			if (this._internalProperties != null)
			{
				this._internalProperties.Clear();
			}
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0004CD10 File Offset: 0x0004AF10
		public bool Contains(object key)
		{
			string b = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == b)
				{
					return true;
				}
			}
			return this._internalProperties != null && this._internalProperties.Contains(key);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0004CD6C File Offset: 0x0004AF6C
		public void Remove(object key)
		{
			string b = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == b)
				{
					throw new ArgumentException("key was invalid");
				}
			}
			if (this._internalProperties != null)
			{
				this._internalProperties.Remove(key);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0004CDD0 File Offset: 0x0004AFD0
		public int Count
		{
			get
			{
				if (this._internalProperties != null)
				{
					return this._internalProperties.Count + this._methodKeys.Length;
				}
				return this._methodKeys.Length;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x0004CDFC File Offset: 0x0004AFFC
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x0004CE00 File Offset: 0x0004B000
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0004CE04 File Offset: 0x0004B004
		public void CopyTo(Array array, int index)
		{
			this.Values.CopyTo(array, index);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0004CE14 File Offset: 0x0004B014
		public IDictionaryEnumerator GetEnumerator()
		{
			return new MethodDictionary.DictionaryEnumerator(this);
		}

		// Token: 0x04000B30 RID: 2864
		private IDictionary _internalProperties;

		// Token: 0x04000B31 RID: 2865
		protected IMethodMessage _message;

		// Token: 0x04000B32 RID: 2866
		private string[] _methodKeys;

		// Token: 0x04000B33 RID: 2867
		private bool _ownProperties;

		// Token: 0x020002BA RID: 698
		private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060015E2 RID: 5602 RVA: 0x0004CE1C File Offset: 0x0004B01C
			public DictionaryEnumerator(MethodDictionary methodDictionary)
			{
				this._methodDictionary = methodDictionary;
				IDictionaryEnumerator hashtableEnum;
				if (this._methodDictionary._internalProperties != null)
				{
					IDictionaryEnumerator enumerator = this._methodDictionary._internalProperties.GetEnumerator();
					hashtableEnum = enumerator;
				}
				else
				{
					hashtableEnum = null;
				}
				this._hashtableEnum = hashtableEnum;
				this._posMethod = -1;
			}

			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x060015E3 RID: 5603 RVA: 0x0004CE6C File Offset: 0x0004B06C
			public object Current
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x060015E4 RID: 5604 RVA: 0x0004CE88 File Offset: 0x0004B088
			public bool MoveNext()
			{
				if (this._posMethod != -2)
				{
					this._posMethod++;
					if (this._posMethod < this._methodDictionary._methodKeys.Length)
					{
						return true;
					}
					this._posMethod = -2;
				}
				if (this._hashtableEnum == null)
				{
					return false;
				}
				while (this._hashtableEnum.MoveNext())
				{
					if (!this._methodDictionary.IsOverridenKey((string)this._hashtableEnum.Key))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060015E5 RID: 5605 RVA: 0x0004CF18 File Offset: 0x0004B118
			public void Reset()
			{
				this._posMethod = -1;
				this._hashtableEnum.Reset();
			}

			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x060015E6 RID: 5606 RVA: 0x0004CF2C File Offset: 0x0004B12C
			public DictionaryEntry Entry
			{
				get
				{
					if (this._posMethod >= 0)
					{
						return new DictionaryEntry(this._methodDictionary._methodKeys[this._posMethod], this._methodDictionary.GetMethodProperty(this._methodDictionary._methodKeys[this._posMethod]));
					}
					if (this._posMethod == -1 || this._hashtableEnum == null)
					{
						throw new InvalidOperationException("The enumerator is positioned before the first element of the collection or after the last element");
					}
					return this._hashtableEnum.Entry;
				}
			}

			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x060015E7 RID: 5607 RVA: 0x0004CFA8 File Offset: 0x0004B1A8
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x170003FC RID: 1020
			// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0004CFC4 File Offset: 0x0004B1C4
			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x04000B36 RID: 2870
			private MethodDictionary _methodDictionary;

			// Token: 0x04000B37 RID: 2871
			private IDictionaryEnumerator _hashtableEnum;

			// Token: 0x04000B38 RID: 2872
			private int _posMethod;
		}
	}
}
