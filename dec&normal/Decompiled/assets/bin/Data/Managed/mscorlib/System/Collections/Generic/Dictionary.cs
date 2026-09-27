using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000087 RID: 135
	[DebuggerDisplay("Count={Count}")]
	[DebuggerTypeProxy(typeof(CollectionDebuggerView<, >))]
	[ComVisible(false)]
	[Serializable]
	public class Dictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>, ICollection, IDictionary, IEnumerable, IDeserializationCallback, ISerializable
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x000148D0 File Offset: 0x00012AD0
		public Dictionary()
		{
			this.Init(10, null);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000148E4 File Offset: 0x00012AE4
		public Dictionary(IEqualityComparer<TKey> comparer)
		{
			this.Init(10, comparer);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000148F8 File Offset: 0x00012AF8
		public Dictionary(IDictionary<TKey, TValue> dictionary) : this(dictionary, null)
		{
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00014904 File Offset: 0x00012B04
		public Dictionary(int capacity)
		{
			this.Init(capacity, null);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00014914 File Offset: 0x00012B14
		public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			int capacity = dictionary.Count;
			this.Init(capacity, comparer);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001499C File Offset: 0x00012B9C
		protected Dictionary(SerializationInfo info, StreamingContext context)
		{
			this.serialization_info = info;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x000149AC File Offset: 0x00012BAC
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x000149B4 File Offset: 0x00012BB4
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000149BC File Offset: 0x00012BBC
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000149C0 File Offset: 0x00012BC0
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700008A RID: 138
		object IDictionary.this[object key]
		{
			get
			{
				if (key is TKey && this.ContainsKey((TKey)((object)key)))
				{
					return this[this.ToTKey(key)];
				}
				return null;
			}
			set
			{
				this[this.ToTKey(key)] = this.ToTValue(value);
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00014A10 File Offset: 0x00012C10
		void IDictionary.Add(object key, object value)
		{
			this.Add(this.ToTKey(key), this.ToTValue(value));
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00014A28 File Offset: 0x00012C28
		bool IDictionary.Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00014A54 File Offset: 0x00012C54
		void IDictionary.Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key is TKey)
			{
				this.Remove((TKey)((object)key));
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00014A80 File Offset: 0x00012C80
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00014A84 File Offset: 0x00012C84
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00014A88 File Offset: 0x00012C88
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00014A8C File Offset: 0x00012C8C
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00014AA4 File Offset: 0x00012CA4
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			return this.ContainsKeyValuePair(keyValuePair);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00014AB0 File Offset: 0x00012CB0
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00014ABC File Offset: 0x00012CBC
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			return this.ContainsKeyValuePair(keyValuePair) && this.Remove(keyValuePair.Key);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00014ADC File Offset: 0x00012CDC
		void ICollection.CopyTo(Array array, int index)
		{
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			this.CopyToCheck(array, index);
			DictionaryEntry[] array3 = array as DictionaryEntry[];
			if (array3 != null)
			{
				this.Do_CopyTo<DictionaryEntry, DictionaryEntry>(array3, index, (TKey key, TValue value) => new DictionaryEntry(key, value));
				return;
			}
			this.Do_ICollectionCopyTo<KeyValuePair<TKey, TValue>>(array, index, new Dictionary<TKey, TValue>.Transform<KeyValuePair<TKey, TValue>>(Dictionary<TKey, TValue>.make_pair));
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00014B50 File Offset: 0x00012D50
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00014B60 File Offset: 0x00012D60
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00014B70 File Offset: 0x00012D70
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.ShimEnumerator(this);
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00014B78 File Offset: 0x00012D78
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700008F RID: 143
		public TValue this[TKey key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = this.hcp.GetHashCode(key) | int.MinValue;
				for (int num2 = this.table[(num & int.MaxValue) % this.table.Length] - 1; num2 != -1; num2 = this.linkSlots[num2].Next)
				{
					if (this.linkSlots[num2].HashCode == num && this.hcp.Equals(this.keySlots[num2], key))
					{
						return this.valueSlots[num2];
					}
				}
				throw new KeyNotFoundException();
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = this.hcp.GetHashCode(key) | int.MinValue;
				int num2 = (num & int.MaxValue) % this.table.Length;
				int num3 = this.table[num2] - 1;
				int num4 = -1;
				if (num3 != -1)
				{
					while (this.linkSlots[num3].HashCode != num || !this.hcp.Equals(this.keySlots[num3], key))
					{
						num4 = num3;
						num3 = this.linkSlots[num3].Next;
						if (num3 == -1)
						{
							break;
						}
					}
				}
				if (num3 == -1)
				{
					if (++this.count > this.threshold)
					{
						this.Resize();
						num2 = (num & int.MaxValue) % this.table.Length;
					}
					num3 = this.emptySlot;
					if (num3 == -1)
					{
						num3 = this.touchedSlots++;
					}
					else
					{
						this.emptySlot = this.linkSlots[num3].Next;
					}
					this.linkSlots[num3].Next = this.table[num2] - 1;
					this.table[num2] = num3 + 1;
					this.linkSlots[num3].HashCode = num;
					this.keySlots[num3] = key;
				}
				else if (num4 != -1)
				{
					this.linkSlots[num4].Next = this.linkSlots[num3].Next;
					this.linkSlots[num3].Next = this.table[num2] - 1;
					this.table[num2] = num3 + 1;
				}
				this.valueSlots[num3] = value;
				this.generation++;
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00014E14 File Offset: 0x00013014
		private void Init(int capacity, IEqualityComparer<TKey> hcp)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this.hcp = ((hcp == null) ? EqualityComparer<TKey>.Default : hcp);
			if (capacity == 0)
			{
				capacity = 10;
			}
			capacity = (int)((float)capacity / 0.9f) + 1;
			this.InitArrays(capacity);
			this.generation = 0;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00014E74 File Offset: 0x00013074
		private void InitArrays(int size)
		{
			this.table = new int[size];
			this.linkSlots = new Link[size];
			this.emptySlot = -1;
			this.keySlots = new TKey[size];
			this.valueSlots = new TValue[size];
			this.touchedSlots = 0;
			this.threshold = (int)((float)this.table.Length * 0.9f);
			if (this.threshold == 0 && this.table.Length > 0)
			{
				this.threshold = 1;
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00014EF8 File Offset: 0x000130F8
		private void CopyToCheck(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index > array.Length)
			{
				throw new ArgumentException("index larger than largest valid index of array");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array cannot hold the requested elements!");
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00014F60 File Offset: 0x00013160
		private void Do_CopyTo<TRet, TElem>(TElem[] array, int index, Dictionary<TKey, TValue>.Transform<TRet> transform) where TRet : TElem
		{
			for (int i = 0; i < this.touchedSlots; i++)
			{
				if ((this.linkSlots[i].HashCode & -2147483648) != 0)
				{
					array[index++] = (TElem)((object)transform(this.keySlots[i], this.valueSlots[i]));
				}
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00014FD4 File Offset: 0x000131D4
		private static KeyValuePair<TKey, TValue> make_pair(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00014FE0 File Offset: 0x000131E0
		private static TKey pick_key(TKey key, TValue value)
		{
			return key;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00014FE4 File Offset: 0x000131E4
		private static TValue pick_value(TKey key, TValue value)
		{
			return value;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00014FE8 File Offset: 0x000131E8
		private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this.CopyToCheck(array, index);
			this.Do_CopyTo<KeyValuePair<TKey, TValue>, KeyValuePair<TKey, TValue>>(array, index, new Dictionary<TKey, TValue>.Transform<KeyValuePair<TKey, TValue>>(Dictionary<TKey, TValue>.make_pair));
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00015008 File Offset: 0x00013208
		private void Do_ICollectionCopyTo<TRet>(Array array, int index, Dictionary<TKey, TValue>.Transform<TRet> transform)
		{
			Type typeFromHandle = typeof(TRet);
			Type elementType = array.GetType().GetElementType();
			try
			{
				if ((typeFromHandle.IsPrimitive || elementType.IsPrimitive) && !elementType.IsAssignableFrom(typeFromHandle))
				{
					throw new Exception();
				}
				this.Do_CopyTo<TRet, object>((object[])array, index, transform);
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("Cannot copy source collection elements to destination array", "array", innerException);
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00015090 File Offset: 0x00013290
		private void Resize()
		{
			int num = Hashtable.ToPrime(this.table.Length << 1 | 1);
			int[] array = new int[num];
			Link[] array2 = new Link[num];
			for (int i = 0; i < this.table.Length; i++)
			{
				for (int num2 = this.table[i] - 1; num2 != -1; num2 = this.linkSlots[num2].Next)
				{
					int num3 = array2[num2].HashCode = (this.hcp.GetHashCode(this.keySlots[num2]) | int.MinValue);
					int num4 = (num3 & int.MaxValue) % num;
					array2[num2].Next = array[num4] - 1;
					array[num4] = num2 + 1;
				}
			}
			this.table = array;
			this.linkSlots = array2;
			TKey[] destinationArray = new TKey[num];
			TValue[] destinationArray2 = new TValue[num];
			Array.Copy(this.keySlots, 0, destinationArray, 0, this.touchedSlots);
			Array.Copy(this.valueSlots, 0, destinationArray2, 0, this.touchedSlots);
			this.keySlots = destinationArray;
			this.valueSlots = destinationArray2;
			this.threshold = (int)((float)num * 0.9f);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000151C4 File Offset: 0x000133C4
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.hcp.GetHashCode(key) | int.MinValue;
			int num2 = (num & int.MaxValue) % this.table.Length;
			int num3;
			for (num3 = this.table[num2] - 1; num3 != -1; num3 = this.linkSlots[num3].Next)
			{
				if (this.linkSlots[num3].HashCode == num && this.hcp.Equals(this.keySlots[num3], key))
				{
					throw new ArgumentException("An element with the same key already exists in the dictionary.");
				}
			}
			if (++this.count > this.threshold)
			{
				this.Resize();
				num2 = (num & int.MaxValue) % this.table.Length;
			}
			num3 = this.emptySlot;
			if (num3 == -1)
			{
				num3 = this.touchedSlots++;
			}
			else
			{
				this.emptySlot = this.linkSlots[num3].Next;
			}
			this.linkSlots[num3].HashCode = num;
			this.linkSlots[num3].Next = this.table[num2] - 1;
			this.table[num2] = num3 + 1;
			this.keySlots[num3] = key;
			this.valueSlots[num3] = value;
			this.generation++;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00015344 File Offset: 0x00013544
		public void Clear()
		{
			this.count = 0;
			Array.Clear(this.table, 0, this.table.Length);
			Array.Clear(this.keySlots, 0, this.keySlots.Length);
			Array.Clear(this.valueSlots, 0, this.valueSlots.Length);
			Array.Clear(this.linkSlots, 0, this.linkSlots.Length);
			this.emptySlot = -1;
			this.touchedSlots = 0;
			this.generation++;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000153C4 File Offset: 0x000135C4
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.hcp.GetHashCode(key) | int.MinValue;
			for (int num2 = this.table[(num & int.MaxValue) % this.table.Length] - 1; num2 != -1; num2 = this.linkSlots[num2].Next)
			{
				if (this.linkSlots[num2].HashCode == num && this.hcp.Equals(this.keySlots[num2], key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001546C File Offset: 0x0001366C
		public bool ContainsValue(TValue value)
		{
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			for (int i = 0; i < this.table.Length; i++)
			{
				for (int num = this.table[i] - 1; num != -1; num = this.linkSlots[num].Next)
				{
					if (@default.Equals(this.valueSlots[num], value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000154DC File Offset: 0x000136DC
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Version", this.generation);
			info.AddValue("Comparer", this.hcp);
			KeyValuePair<TKey, TValue>[] array = null;
			if (this.count > 0)
			{
				array = new KeyValuePair<TKey, TValue>[this.count];
				this.CopyTo(array, 0);
			}
			info.AddValue("HashSize", this.table.Length);
			info.AddValue("KeyValuePairs", array);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00015560 File Offset: 0x00013760
		public virtual void OnDeserialization(object sender)
		{
			if (this.serialization_info == null)
			{
				return;
			}
			this.generation = this.serialization_info.GetInt32("Version");
			this.hcp = (IEqualityComparer<TKey>)this.serialization_info.GetValue("Comparer", typeof(IEqualityComparer<TKey>));
			int num = this.serialization_info.GetInt32("HashSize");
			KeyValuePair<TKey, TValue>[] array = (KeyValuePair<TKey, TValue>[])this.serialization_info.GetValue("KeyValuePairs", typeof(KeyValuePair<TKey, TValue>[]));
			if (num < 10)
			{
				num = 10;
			}
			this.InitArrays(num);
			this.count = 0;
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					this.Add(array[i].Key, array[i].Value);
				}
			}
			this.generation++;
			this.serialization_info = null;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001564C File Offset: 0x0001384C
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.hcp.GetHashCode(key) | int.MinValue;
			int num2 = (num & int.MaxValue) % this.table.Length;
			int num3 = this.table[num2] - 1;
			if (num3 == -1)
			{
				return false;
			}
			int num4 = -1;
			while (this.linkSlots[num3].HashCode != num || !this.hcp.Equals(this.keySlots[num3], key))
			{
				num4 = num3;
				num3 = this.linkSlots[num3].Next;
				if (num3 == -1)
				{
					IL_A4:
					if (num3 == -1)
					{
						return false;
					}
					this.count--;
					if (num4 == -1)
					{
						this.table[num2] = this.linkSlots[num3].Next + 1;
					}
					else
					{
						this.linkSlots[num4].Next = this.linkSlots[num3].Next;
					}
					this.linkSlots[num3].Next = this.emptySlot;
					this.emptySlot = num3;
					this.linkSlots[num3].HashCode = 0;
					this.keySlots[num3] = default(TKey);
					this.valueSlots[num3] = default(TValue);
					this.generation++;
					return true;
				}
			}
			goto IL_A4;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000157C8 File Offset: 0x000139C8
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.hcp.GetHashCode(key) | int.MinValue;
			for (int num2 = this.table[(num & int.MaxValue) % this.table.Length] - 1; num2 != -1; num2 = this.linkSlots[num2].Next)
			{
				if (this.linkSlots[num2].HashCode == num && this.hcp.Equals(this.keySlots[num2], key))
				{
					value = this.valueSlots[num2];
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00015890 File Offset: 0x00013A90
		public Dictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				return new Dictionary<TKey, TValue>.KeyCollection(this);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00015898 File Offset: 0x00013A98
		public Dictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				return new Dictionary<TKey, TValue>.ValueCollection(this);
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000158A0 File Offset: 0x00013AA0
		private TKey ToTKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!(key is TKey))
			{
				throw new ArgumentException("not of type: " + typeof(TKey).ToString(), "key");
			}
			return (TKey)((object)key);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000158F4 File Offset: 0x00013AF4
		private TValue ToTValue(object value)
		{
			if (value == null && !typeof(TValue).IsValueType)
			{
				return default(TValue);
			}
			if (!(value is TValue))
			{
				throw new ArgumentException("not of type: " + typeof(TValue).ToString(), "value");
			}
			return (TValue)((object)value);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001595C File Offset: 0x00013B5C
		private bool ContainsKeyValuePair(KeyValuePair<TKey, TValue> pair)
		{
			TValue y;
			return this.TryGetValue(pair.Key, out y) && EqualityComparer<TValue>.Default.Equals(pair.Value, y);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00015994 File Offset: 0x00013B94
		public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x040001EB RID: 491
		private const int INITIAL_SIZE = 10;

		// Token: 0x040001EC RID: 492
		private const float DEFAULT_LOAD_FACTOR = 0.9f;

		// Token: 0x040001ED RID: 493
		private const int NO_SLOT = -1;

		// Token: 0x040001EE RID: 494
		private const int HASH_FLAG = -2147483648;

		// Token: 0x040001EF RID: 495
		private int[] table;

		// Token: 0x040001F0 RID: 496
		private Link[] linkSlots;

		// Token: 0x040001F1 RID: 497
		private TKey[] keySlots;

		// Token: 0x040001F2 RID: 498
		private TValue[] valueSlots;

		// Token: 0x040001F3 RID: 499
		private int touchedSlots;

		// Token: 0x040001F4 RID: 500
		private int emptySlot;

		// Token: 0x040001F5 RID: 501
		private int count;

		// Token: 0x040001F6 RID: 502
		private int threshold;

		// Token: 0x040001F7 RID: 503
		private IEqualityComparer<TKey> hcp;

		// Token: 0x040001F8 RID: 504
		private SerializationInfo serialization_info;

		// Token: 0x040001F9 RID: 505
		private int generation;

		// Token: 0x02000088 RID: 136
		[Serializable]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDictionaryEnumerator, IEnumerator, IDisposable
		{
			// Token: 0x060004B5 RID: 1205 RVA: 0x000159B0 File Offset: 0x00013BB0
			internal Enumerator(Dictionary<TKey, TValue> dictionary)
			{
				this.dictionary = dictionary;
				this.stamp = dictionary.generation;
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000159C8 File Offset: 0x00013BC8
			object IEnumerator.Current
			{
				get
				{
					this.VerifyCurrent();
					return this.current;
				}
			}

			// Token: 0x060004B7 RID: 1207 RVA: 0x000159DC File Offset: 0x00013BDC
			void IEnumerator.Reset()
			{
				this.Reset();
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000159E4 File Offset: 0x00013BE4
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					this.VerifyCurrent();
					return new DictionaryEntry(this.current.Key, this.current.Value);
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00015A14 File Offset: 0x00013C14
			object IDictionaryEnumerator.Key
			{
				get
				{
					return this.CurrentKey;
				}
			}

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x060004BA RID: 1210 RVA: 0x00015A24 File Offset: 0x00013C24
			object IDictionaryEnumerator.Value
			{
				get
				{
					return this.CurrentValue;
				}
			}

			// Token: 0x060004BB RID: 1211 RVA: 0x00015A34 File Offset: 0x00013C34
			public bool MoveNext()
			{
				this.VerifyState();
				if (this.next < 0)
				{
					return false;
				}
				while (this.next < this.dictionary.touchedSlots)
				{
					int num = this.next++;
					if ((this.dictionary.linkSlots[num].HashCode & -2147483648) != 0)
					{
						this.current = new KeyValuePair<TKey, TValue>(this.dictionary.keySlots[num], this.dictionary.valueSlots[num]);
						return true;
					}
				}
				this.next = -1;
				return false;
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x060004BC RID: 1212 RVA: 0x00015ADC File Offset: 0x00013CDC
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x060004BD RID: 1213 RVA: 0x00015AE4 File Offset: 0x00013CE4
			internal TKey CurrentKey
			{
				get
				{
					this.VerifyCurrent();
					return this.current.Key;
				}
			}

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x060004BE RID: 1214 RVA: 0x00015AF8 File Offset: 0x00013CF8
			internal TValue CurrentValue
			{
				get
				{
					this.VerifyCurrent();
					return this.current.Value;
				}
			}

			// Token: 0x060004BF RID: 1215 RVA: 0x00015B0C File Offset: 0x00013D0C
			internal void Reset()
			{
				this.VerifyState();
				this.next = 0;
			}

			// Token: 0x060004C0 RID: 1216 RVA: 0x00015B1C File Offset: 0x00013D1C
			private void VerifyState()
			{
				if (this.dictionary == null)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.dictionary.generation != this.stamp)
				{
					throw new InvalidOperationException("out of sync");
				}
			}

			// Token: 0x060004C1 RID: 1217 RVA: 0x00015B54 File Offset: 0x00013D54
			private void VerifyCurrent()
			{
				this.VerifyState();
				if (this.next <= 0)
				{
					throw new InvalidOperationException("Current is not valid");
				}
			}

			// Token: 0x060004C2 RID: 1218 RVA: 0x00015B74 File Offset: 0x00013D74
			public void Dispose()
			{
				this.dictionary = null;
			}

			// Token: 0x040001FB RID: 507
			private Dictionary<TKey, TValue> dictionary;

			// Token: 0x040001FC RID: 508
			private int next;

			// Token: 0x040001FD RID: 509
			private int stamp;

			// Token: 0x040001FE RID: 510
			internal KeyValuePair<TKey, TValue> current;
		}

		// Token: 0x02000089 RID: 137
		[DebuggerDisplay("Count={Count}")]
		[DebuggerTypeProxy(typeof(CollectionDebuggerView<, >))]
		[Serializable]
		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, ICollection, IEnumerable
		{
			// Token: 0x060004C3 RID: 1219 RVA: 0x00015B80 File Offset: 0x00013D80
			public KeyCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					throw new ArgumentNullException("dictionary");
				}
				this.dictionary = dictionary;
			}

			// Token: 0x060004C4 RID: 1220 RVA: 0x00015BA0 File Offset: 0x00013DA0
			void ICollection<!0>.Add(TKey item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x060004C5 RID: 1221 RVA: 0x00015BAC File Offset: 0x00013DAC
			void ICollection<!0>.Clear()
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x060004C6 RID: 1222 RVA: 0x00015BB8 File Offset: 0x00013DB8
			bool ICollection<!0>.Contains(TKey item)
			{
				return this.dictionary.ContainsKey(item);
			}

			// Token: 0x060004C7 RID: 1223 RVA: 0x00015BC8 File Offset: 0x00013DC8
			bool ICollection<!0>.Remove(TKey item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x060004C8 RID: 1224 RVA: 0x00015BD4 File Offset: 0x00013DD4
			IEnumerator<TKey> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x00015BE4 File Offset: 0x00013DE4
			void ICollection.CopyTo(Array array, int index)
			{
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				this.dictionary.CopyToCheck(array, index);
				this.dictionary.Do_ICollectionCopyTo<TKey>(array, index, new Dictionary<TKey, TValue>.Transform<TKey>(Dictionary<TKey, TValue>.pick_key));
			}

			// Token: 0x060004CA RID: 1226 RVA: 0x00015C30 File Offset: 0x00013E30
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x060004CB RID: 1227 RVA: 0x00015C40 File Offset: 0x00013E40
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x060004CC RID: 1228 RVA: 0x00015C44 File Offset: 0x00013E44
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x060004CD RID: 1229 RVA: 0x00015C48 File Offset: 0x00013E48
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x00015C58 File Offset: 0x00013E58
			public void CopyTo(TKey[] array, int index)
			{
				this.dictionary.CopyToCheck(array, index);
				this.dictionary.Do_CopyTo<TKey, TKey>(array, index, new Dictionary<TKey, TValue>.Transform<TKey>(Dictionary<TKey, TValue>.pick_key));
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x00015C80 File Offset: 0x00013E80
			public Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00015C90 File Offset: 0x00013E90
			public int Count
			{
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x040001FF RID: 511
			private Dictionary<TKey, TValue> dictionary;

			// Token: 0x0200008A RID: 138
			[Serializable]
			public struct Enumerator : IEnumerator<!0>, IEnumerator, IDisposable
			{
				// Token: 0x060004D1 RID: 1233 RVA: 0x00015CA0 File Offset: 0x00013EA0
				internal Enumerator(Dictionary<TKey, TValue> host)
				{
					this.host_enumerator = host.GetEnumerator();
				}

				// Token: 0x1700009D RID: 157
				// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00015CB0 File Offset: 0x00013EB0
				object IEnumerator.Current
				{
					get
					{
						return this.host_enumerator.CurrentKey;
					}
				}

				// Token: 0x060004D3 RID: 1235 RVA: 0x00015CC4 File Offset: 0x00013EC4
				void IEnumerator.Reset()
				{
					this.host_enumerator.Reset();
				}

				// Token: 0x060004D4 RID: 1236 RVA: 0x00015CD4 File Offset: 0x00013ED4
				public void Dispose()
				{
					this.host_enumerator.Dispose();
				}

				// Token: 0x060004D5 RID: 1237 RVA: 0x00015CE4 File Offset: 0x00013EE4
				public bool MoveNext()
				{
					return this.host_enumerator.MoveNext();
				}

				// Token: 0x1700009E RID: 158
				// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00015CF4 File Offset: 0x00013EF4
				public TKey Current
				{
					get
					{
						return this.host_enumerator.current.Key;
					}
				}

				// Token: 0x04000200 RID: 512
				private Dictionary<TKey, TValue>.Enumerator host_enumerator;
			}
		}

		// Token: 0x0200008B RID: 139
		[Serializable]
		private class ShimEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060004D7 RID: 1239 RVA: 0x00015D08 File Offset: 0x00013F08
			public ShimEnumerator(Dictionary<TKey, TValue> host)
			{
				this.host_enumerator = host.GetEnumerator();
			}

			// Token: 0x060004D8 RID: 1240 RVA: 0x00015D1C File Offset: 0x00013F1C
			public bool MoveNext()
			{
				return this.host_enumerator.MoveNext();
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00015D2C File Offset: 0x00013F2C
			public DictionaryEntry Entry
			{
				get
				{
					return ((IDictionaryEnumerator)this.host_enumerator).Entry;
				}
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x060004DA RID: 1242 RVA: 0x00015D40 File Offset: 0x00013F40
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.host_enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060004DB RID: 1243 RVA: 0x00015D68 File Offset: 0x00013F68
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.host_enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x060004DC RID: 1244 RVA: 0x00015D90 File Offset: 0x00013F90
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x00015DA0 File Offset: 0x00013FA0
			public void Reset()
			{
				this.host_enumerator.Reset();
			}

			// Token: 0x04000201 RID: 513
			private Dictionary<TKey, TValue>.Enumerator host_enumerator;
		}

		// Token: 0x0200008C RID: 140
		// (Invoke) Token: 0x060004DF RID: 1247
		private delegate TRet Transform<TRet>(TKey key, TValue value);

		// Token: 0x0200008D RID: 141
		[DebuggerTypeProxy(typeof(CollectionDebuggerView<, >))]
		[DebuggerDisplay("Count={Count}")]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, ICollection, IEnumerable
		{
			// Token: 0x060004E2 RID: 1250 RVA: 0x00015DB0 File Offset: 0x00013FB0
			public ValueCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					throw new ArgumentNullException("dictionary");
				}
				this.dictionary = dictionary;
			}

			// Token: 0x060004E3 RID: 1251 RVA: 0x00015DD0 File Offset: 0x00013FD0
			void ICollection<!1>.Add(TValue item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x060004E4 RID: 1252 RVA: 0x00015DDC File Offset: 0x00013FDC
			void ICollection<!1>.Clear()
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x00015DE8 File Offset: 0x00013FE8
			bool ICollection<!1>.Contains(TValue item)
			{
				return this.dictionary.ContainsValue(item);
			}

			// Token: 0x060004E6 RID: 1254 RVA: 0x00015DF8 File Offset: 0x00013FF8
			bool ICollection<!1>.Remove(TValue item)
			{
				throw new NotSupportedException("this is a read-only collection");
			}

			// Token: 0x060004E7 RID: 1255 RVA: 0x00015E04 File Offset: 0x00014004
			IEnumerator<TValue> IEnumerable<!1>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060004E8 RID: 1256 RVA: 0x00015E14 File Offset: 0x00014014
			void ICollection.CopyTo(Array array, int index)
			{
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				this.dictionary.CopyToCheck(array, index);
				this.dictionary.Do_ICollectionCopyTo<TValue>(array, index, new Dictionary<TKey, TValue>.Transform<TValue>(Dictionary<TKey, TValue>.pick_value));
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x00015E60 File Offset: 0x00014060
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x060004EA RID: 1258 RVA: 0x00015E70 File Offset: 0x00014070
			bool ICollection<!1>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x060004EB RID: 1259 RVA: 0x00015E74 File Offset: 0x00014074
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060004EC RID: 1260 RVA: 0x00015E78 File Offset: 0x00014078
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			// Token: 0x060004ED RID: 1261 RVA: 0x00015E88 File Offset: 0x00014088
			public void CopyTo(TValue[] array, int index)
			{
				this.dictionary.CopyToCheck(array, index);
				this.dictionary.Do_CopyTo<TValue, TValue>(array, index, new Dictionary<TKey, TValue>.Transform<TValue>(Dictionary<TKey, TValue>.pick_value));
			}

			// Token: 0x060004EE RID: 1262 RVA: 0x00015EB0 File Offset: 0x000140B0
			public Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060004EF RID: 1263 RVA: 0x00015EC0 File Offset: 0x000140C0
			public int Count
			{
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x04000202 RID: 514
			private Dictionary<TKey, TValue> dictionary;

			// Token: 0x0200008E RID: 142
			[Serializable]
			public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
			{
				// Token: 0x060004F0 RID: 1264 RVA: 0x00015ED0 File Offset: 0x000140D0
				internal Enumerator(Dictionary<TKey, TValue> host)
				{
					this.host_enumerator = host.GetEnumerator();
				}

				// Token: 0x170000A7 RID: 167
				// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00015EE0 File Offset: 0x000140E0
				object IEnumerator.Current
				{
					get
					{
						return this.host_enumerator.CurrentValue;
					}
				}

				// Token: 0x060004F2 RID: 1266 RVA: 0x00015EF4 File Offset: 0x000140F4
				void IEnumerator.Reset()
				{
					this.host_enumerator.Reset();
				}

				// Token: 0x060004F3 RID: 1267 RVA: 0x00015F04 File Offset: 0x00014104
				public void Dispose()
				{
					this.host_enumerator.Dispose();
				}

				// Token: 0x060004F4 RID: 1268 RVA: 0x00015F14 File Offset: 0x00014114
				public bool MoveNext()
				{
					return this.host_enumerator.MoveNext();
				}

				// Token: 0x170000A8 RID: 168
				// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00015F24 File Offset: 0x00014124
				public TValue Current
				{
					get
					{
						return this.host_enumerator.current.Value;
					}
				}

				// Token: 0x04000203 RID: 515
				private Dictionary<TKey, TValue>.Enumerator host_enumerator;
			}
		}
	}
}
