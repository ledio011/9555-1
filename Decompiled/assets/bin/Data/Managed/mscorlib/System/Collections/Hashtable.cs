using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Collections
{
	// Token: 0x0200009F RID: 159
	[DebuggerTypeProxy(typeof(CollectionDebuggerView))]
	[DebuggerDisplay("Count={Count}")]
	[ComVisible(true)]
	[Serializable]
	public class Hashtable : ICollection, IDictionary, IEnumerable, ICloneable, IDeserializationCallback, ISerializable
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x00016C14 File Offset: 0x00014E14
		public Hashtable() : this(0, 1f)
		{
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00016C24 File Offset: 0x00014E24
		[Obsolete("Please use Hashtable(int, float, IEqualityComparer) instead")]
		public Hashtable(int capacity, float loadFactor, IHashCodeProvider hcp, IComparer comparer)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "negative capacity");
			}
			if (loadFactor < 0.1f || loadFactor > 1f || float.IsNaN(loadFactor))
			{
				throw new ArgumentOutOfRangeException("loadFactor", "load factor");
			}
			if (capacity == 0)
			{
				capacity++;
			}
			this.loadFactor = 0.75f * loadFactor;
			double num = (double)((float)capacity / this.loadFactor);
			if (num > 2147483647.0)
			{
				throw new ArgumentException("Size is too big");
			}
			int num2 = (int)num;
			num2 = Hashtable.ToPrime(num2);
			this.SetTable(new Hashtable.Slot[num2], new int[num2]);
			this.hcp = hcp;
			this.comparer = comparer;
			this.inUse = 0;
			this.modificationCount = 0;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00016CF8 File Offset: 0x00014EF8
		public Hashtable(int capacity, float loadFactor) : this(capacity, loadFactor, null, null)
		{
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00016D04 File Offset: 0x00014F04
		public Hashtable(int capacity) : this(capacity, 1f)
		{
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00016D14 File Offset: 0x00014F14
		internal Hashtable(Hashtable source)
		{
			this.inUse = source.inUse;
			this.loadFactor = source.loadFactor;
			this.table = (Hashtable.Slot[])source.table.Clone();
			this.hashes = (int[])source.hashes.Clone();
			this.threshold = source.threshold;
			this.hcpRef = source.hcpRef;
			this.comparerRef = source.comparerRef;
			this.equalityComparer = source.equalityComparer;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00016D9C File Offset: 0x00014F9C
		[Obsolete("Please use Hashtable(IEqualityComparer) instead")]
		public Hashtable(IHashCodeProvider hcp, IComparer comparer) : this(1, 1f, hcp, comparer)
		{
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00016DAC File Offset: 0x00014FAC
		public Hashtable(SerializationInfo info, StreamingContext context)
		{
			this.serializationInfo = info;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00016DD8 File Offset: 0x00014FD8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Hashtable.Enumerator(this, Hashtable.EnumeratorMode.ENTRY_MODE);
		}

		// Token: 0x170000BC RID: 188
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00016DE4 File Offset: 0x00014FE4
		[Obsolete("Please use EqualityComparer property.")]
		protected IComparer comparer
		{
			set
			{
				this.comparerRef = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00016DF0 File Offset: 0x00014FF0
		[Obsolete("Please use EqualityComparer property.")]
		protected IHashCodeProvider hcp
		{
			set
			{
				this.hcpRef = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00016DFC File Offset: 0x00014FFC
		public virtual int Count
		{
			get
			{
				return this.inUse;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00016E04 File Offset: 0x00015004
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00016E08 File Offset: 0x00015008
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00016E0C File Offset: 0x0001500C
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00016E10 File Offset: 0x00015010
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00016E14 File Offset: 0x00015014
		public virtual ICollection Keys
		{
			get
			{
				if (this.hashKeys == null)
				{
					this.hashKeys = new Hashtable.HashKeys(this);
				}
				return this.hashKeys;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00016E34 File Offset: 0x00015034
		public virtual ICollection Values
		{
			get
			{
				if (this.hashValues == null)
				{
					this.hashValues = new Hashtable.HashValues(this);
				}
				return this.hashValues;
			}
		}

		// Token: 0x170000C5 RID: 197
		public virtual object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "null key");
				}
				Hashtable.Slot[] array = this.table;
				int[] array2 = this.hashes;
				uint num = (uint)array.Length;
				int num2 = this.GetHash(key) & int.MaxValue;
				uint num3 = (uint)num2;
				uint num4 = (uint)(((num2 >> 5) + 1) % (int)(num - 1U) + 1);
				for (uint num5 = num; num5 > 0U; num5 -= 1U)
				{
					num3 %= num;
					Hashtable.Slot slot = array[(int)((UIntPtr)num3)];
					int num6 = array2[(int)((UIntPtr)num3)];
					object key2 = slot.key;
					if (key2 == null)
					{
						break;
					}
					if (key2 == key || ((num6 & 2147483647) == num2 && this.KeyEquals(key, key2)))
					{
						return slot.value;
					}
					if ((num6 & -2147483648) == 0)
					{
						break;
					}
					num3 += num4;
				}
				return null;
			}
			set
			{
				this.PutImpl(key, value, true);
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00016F44 File Offset: 0x00015144
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Rank > 1)
			{
				throw new ArgumentException("array is multidimensional");
			}
			if (array.Length > 0 && arrayIndex >= array.Length)
			{
				throw new ArgumentException("arrayIndex is equal to or greater than array.Length");
			}
			if (arrayIndex + this.inUse > array.Length)
			{
				throw new ArgumentException("Not enough room from arrayIndex to end of array for this Hashtable");
			}
			IDictionaryEnumerator enumerator = this.GetEnumerator();
			int num = arrayIndex;
			while (enumerator.MoveNext())
			{
				array.SetValue(enumerator.Entry, num++);
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00016FFC File Offset: 0x000151FC
		public virtual void Add(object key, object value)
		{
			this.PutImpl(key, value, false);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00017008 File Offset: 0x00015208
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public virtual void Clear()
		{
			for (int i = 0; i < this.table.Length; i++)
			{
				this.table[i].key = null;
				this.table[i].value = null;
				this.hashes[i] = 0;
			}
			this.inUse = 0;
			this.modificationCount++;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00017070 File Offset: 0x00015270
		public virtual bool Contains(object key)
		{
			return this.Find(key) >= 0;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00017080 File Offset: 0x00015280
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new Hashtable.Enumerator(this, Hashtable.EnumeratorMode.ENTRY_MODE);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001708C File Offset: 0x0001528C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public virtual void Remove(object key)
		{
			int num = this.Find(key);
			if (num >= 0)
			{
				Hashtable.Slot[] array = this.table;
				int num2 = this.hashes[num];
				num2 &= int.MinValue;
				this.hashes[num] = num2;
				array[num].key = ((num2 == 0) ? null : Hashtable.KeyMarker.Removed);
				array[num].value = null;
				this.inUse--;
				this.modificationCount++;
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00017110 File Offset: 0x00015310
		public virtual bool ContainsKey(object key)
		{
			return this.Contains(key);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001711C File Offset: 0x0001531C
		public virtual object Clone()
		{
			return new Hashtable(this);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00017124 File Offset: 0x00015324
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("LoadFactor", this.loadFactor);
			info.AddValue("Version", this.modificationCount);
			if (this.equalityComparer != null)
			{
				info.AddValue("KeyComparer", this.equalityComparer);
			}
			else
			{
				info.AddValue("Comparer", this.comparerRef);
			}
			if (this.hcpRef != null)
			{
				info.AddValue("HashCodeProvider", this.hcpRef);
			}
			info.AddValue("HashSize", this.table.Length);
			object[] array = new object[this.inUse];
			this.CopyToArray(array, 0, Hashtable.EnumeratorMode.KEY_MODE);
			object[] array2 = new object[this.inUse];
			this.CopyToArray(array2, 0, Hashtable.EnumeratorMode.VALUE_MODE);
			info.AddValue("Keys", array);
			info.AddValue("Values", array2);
			info.AddValue("equalityComparer", this.equalityComparer);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00017218 File Offset: 0x00015418
		[MonoTODO("Serialize equalityComparer")]
		public virtual void OnDeserialization(object sender)
		{
			if (this.serializationInfo == null)
			{
				return;
			}
			this.loadFactor = (float)this.serializationInfo.GetValue("LoadFactor", typeof(float));
			this.modificationCount = (int)this.serializationInfo.GetValue("Version", typeof(int));
			try
			{
				this.equalityComparer = (IEqualityComparer)this.serializationInfo.GetValue("KeyComparer", typeof(object));
			}
			catch
			{
			}
			if (this.equalityComparer == null)
			{
				this.comparerRef = (IComparer)this.serializationInfo.GetValue("Comparer", typeof(object));
			}
			try
			{
				this.hcpRef = (IHashCodeProvider)this.serializationInfo.GetValue("HashCodeProvider", typeof(object));
			}
			catch
			{
			}
			int num = (int)this.serializationInfo.GetValue("HashSize", typeof(int));
			object[] array = (object[])this.serializationInfo.GetValue("Keys", typeof(object[]));
			object[] array2 = (object[])this.serializationInfo.GetValue("Values", typeof(object[]));
			if (array.Length != array2.Length)
			{
				throw new SerializationException("Keys and values of uneven size");
			}
			num = Hashtable.ToPrime(num);
			this.SetTable(new Hashtable.Slot[num], new int[num]);
			for (int i = 0; i < array.Length; i++)
			{
				this.Add(array[i], array2[i]);
			}
			this.AdjustThreshold();
			this.serializationInfo = null;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000173E4 File Offset: 0x000155E4
		public static Hashtable Synchronized(Hashtable table)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			return new Hashtable.SyncHashtable(table);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00017400 File Offset: 0x00015600
		protected virtual int GetHash(object key)
		{
			if (this.equalityComparer != null)
			{
				return this.equalityComparer.GetHashCode(key);
			}
			if (this.hcpRef == null)
			{
				return key.GetHashCode();
			}
			return this.hcpRef.GetHashCode(key);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00017438 File Offset: 0x00015638
		protected virtual bool KeyEquals(object item, object key)
		{
			if (key == Hashtable.KeyMarker.Removed)
			{
				return false;
			}
			if (this.equalityComparer != null)
			{
				return this.equalityComparer.Equals(item, key);
			}
			if (this.comparerRef == null)
			{
				return item.Equals(key);
			}
			return this.comparerRef.Compare(item, key) == 0;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00017490 File Offset: 0x00015690
		private void AdjustThreshold()
		{
			int num = this.table.Length;
			this.threshold = (int)((float)num * this.loadFactor);
			if (this.threshold >= num)
			{
				this.threshold = num - 1;
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000174CC File Offset: 0x000156CC
		private void SetTable(Hashtable.Slot[] table, int[] hashes)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			this.table = table;
			this.hashes = hashes;
			this.AdjustThreshold();
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000174F4 File Offset: 0x000156F4
		private int Find(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "null key");
			}
			Hashtable.Slot[] array = this.table;
			int[] array2 = this.hashes;
			uint num = (uint)array.Length;
			int num2 = this.GetHash(key) & int.MaxValue;
			uint num3 = (uint)num2;
			uint num4 = (uint)(((num2 >> 5) + 1) % (int)(num - 1U) + 1);
			for (uint num5 = num; num5 > 0U; num5 -= 1U)
			{
				num3 %= num;
				Hashtable.Slot slot = array[(int)((UIntPtr)num3)];
				int num6 = array2[(int)((UIntPtr)num3)];
				object key2 = slot.key;
				if (key2 == null)
				{
					break;
				}
				if (key2 == key || ((num6 & 2147483647) == num2 && this.KeyEquals(key, key2)))
				{
					return (int)num3;
				}
				if ((num6 & -2147483648) == 0)
				{
					break;
				}
				num3 += num4;
			}
			return -1;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000175D0 File Offset: 0x000157D0
		private void Rehash()
		{
			int num = this.table.Length;
			uint num2 = (uint)Hashtable.ToPrime(num << 1 | 1);
			Hashtable.Slot[] array = new Hashtable.Slot[num2];
			Hashtable.Slot[] array2 = this.table;
			int[] array3 = new int[num2];
			int[] array4 = this.hashes;
			for (int i = 0; i < num; i++)
			{
				Hashtable.Slot slot = array2[i];
				if (slot.key != null)
				{
					int num3 = array4[i] & int.MaxValue;
					uint num4 = (uint)num3;
					uint num5 = (uint)(((num3 >> 5) + 1) % (int)(num2 - 1U) + 1);
					uint num6 = num4 % num2;
					while (array[(int)((UIntPtr)num6)].key != null)
					{
						array3[(int)((UIntPtr)num6)] |= int.MinValue;
						num4 += num5;
						num6 = num4 % num2;
					}
					array[(int)((UIntPtr)num6)].key = slot.key;
					array[(int)((UIntPtr)num6)].value = slot.value;
					array3[(int)((UIntPtr)num6)] |= num3;
				}
			}
			this.modificationCount++;
			this.SetTable(array, array3);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000176FC File Offset: 0x000158FC
		private void PutImpl(object key, object value, bool overwrite)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "null key");
			}
			if (this.inUse >= this.threshold)
			{
				this.Rehash();
			}
			uint num = (uint)this.table.Length;
			int num2 = this.GetHash(key) & int.MaxValue;
			uint num3 = (uint)num2;
			uint num4 = ((num3 >> 5) + 1U) % (num - 1U) + 1U;
			Hashtable.Slot[] array = this.table;
			int[] array2 = this.hashes;
			int num5 = -1;
			int num6 = 0;
			while ((long)num6 < (long)((ulong)num))
			{
				int num7 = (int)(num3 % num);
				Hashtable.Slot slot = array[num7];
				int num8 = array2[num7];
				if (num5 == -1 && slot.key == Hashtable.KeyMarker.Removed && (num8 & -2147483648) != 0)
				{
					num5 = num7;
				}
				if (slot.key == null || (slot.key == Hashtable.KeyMarker.Removed && (num8 & -2147483648) == 0))
				{
					if (num5 == -1)
					{
						num5 = num7;
					}
					break;
				}
				if ((num8 & 2147483647) == num2 && this.KeyEquals(key, slot.key))
				{
					if (overwrite)
					{
						array[num7].value = value;
						this.modificationCount++;
						return;
					}
					throw new ArgumentException("Key duplication when adding: " + key);
				}
				else
				{
					if (num5 == -1)
					{
						array2[num7] |= int.MinValue;
					}
					num3 += num4;
					num6++;
				}
			}
			if (num5 != -1)
			{
				array[num5].key = key;
				array[num5].value = value;
				array2[num5] |= num2;
				this.inUse++;
				this.modificationCount++;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000178D0 File Offset: 0x00015AD0
		private void CopyToArray(Array arr, int i, Hashtable.EnumeratorMode mode)
		{
			IEnumerator enumerator = new Hashtable.Enumerator(this, mode);
			while (enumerator.MoveNext())
			{
				object value = enumerator.Current;
				arr.SetValue(value, i++);
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00017908 File Offset: 0x00015B08
		internal static bool TestPrime(int x)
		{
			if ((x & 1) != 0)
			{
				int num = (int)Math.Sqrt((double)x);
				for (int i = 3; i < num; i += 2)
				{
					if (x % i == 0)
					{
						return false;
					}
				}
				return true;
			}
			return x == 2;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00017948 File Offset: 0x00015B48
		internal static int CalcPrime(int x)
		{
			for (int i = (x & -2) - 1; i < 2147483647; i += 2)
			{
				if (Hashtable.TestPrime(i))
				{
					return i;
				}
			}
			return x;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00017980 File Offset: 0x00015B80
		internal static int ToPrime(int x)
		{
			for (int i = 0; i < Hashtable.primeTbl.Length; i++)
			{
				if (x <= Hashtable.primeTbl[i])
				{
					return Hashtable.primeTbl[i];
				}
			}
			return Hashtable.CalcPrime(x);
		}

		// Token: 0x04000212 RID: 530
		private const int CHAIN_MARKER = -2147483648;

		// Token: 0x04000213 RID: 531
		private int inUse;

		// Token: 0x04000214 RID: 532
		private int modificationCount;

		// Token: 0x04000215 RID: 533
		private float loadFactor;

		// Token: 0x04000216 RID: 534
		private Hashtable.Slot[] table;

		// Token: 0x04000217 RID: 535
		private int[] hashes;

		// Token: 0x04000218 RID: 536
		private int threshold;

		// Token: 0x04000219 RID: 537
		private Hashtable.HashKeys hashKeys;

		// Token: 0x0400021A RID: 538
		private Hashtable.HashValues hashValues;

		// Token: 0x0400021B RID: 539
		private IHashCodeProvider hcpRef;

		// Token: 0x0400021C RID: 540
		private IComparer comparerRef;

		// Token: 0x0400021D RID: 541
		private SerializationInfo serializationInfo;

		// Token: 0x0400021E RID: 542
		private IEqualityComparer equalityComparer;

		// Token: 0x0400021F RID: 543
		private static readonly int[] primeTbl = new int[]
		{
			11,
			19,
			37,
			73,
			109,
			163,
			251,
			367,
			557,
			823,
			1237,
			1861,
			2777,
			4177,
			6247,
			9371,
			14057,
			21089,
			31627,
			47431,
			71143,
			106721,
			160073,
			240101,
			360163,
			540217,
			810343,
			1215497,
			1823231,
			2734867,
			4102283,
			6153409,
			9230113,
			13845163
		};

		// Token: 0x020000A0 RID: 160
		[Serializable]
		private sealed class Enumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06000586 RID: 1414 RVA: 0x000179C0 File Offset: 0x00015BC0
			public Enumerator(Hashtable host, Hashtable.EnumeratorMode mode)
			{
				this.host = host;
				this.stamp = host.modificationCount;
				this.size = host.table.Length;
				this.mode = mode;
				this.Reset();
			}

			// Token: 0x06000588 RID: 1416 RVA: 0x00017A04 File Offset: 0x00015C04
			private void FailFast()
			{
				if (this.host.modificationCount != this.stamp)
				{
					throw new InvalidOperationException(Hashtable.Enumerator.xstr);
				}
			}

			// Token: 0x06000589 RID: 1417 RVA: 0x00017A28 File Offset: 0x00015C28
			public void Reset()
			{
				this.FailFast();
				this.pos = -1;
				this.currentKey = null;
				this.currentValue = null;
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x00017A48 File Offset: 0x00015C48
			public bool MoveNext()
			{
				this.FailFast();
				if (this.pos < this.size)
				{
					while (++this.pos < this.size)
					{
						Hashtable.Slot slot = this.host.table[this.pos];
						if (slot.key != null && slot.key != Hashtable.KeyMarker.Removed)
						{
							this.currentKey = slot.key;
							this.currentValue = slot.value;
							return true;
						}
					}
				}
				this.currentKey = null;
				this.currentValue = null;
				return false;
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x0600058B RID: 1419 RVA: 0x00017AF4 File Offset: 0x00015CF4
			public DictionaryEntry Entry
			{
				get
				{
					if (this.currentKey == null)
					{
						throw new InvalidOperationException();
					}
					this.FailFast();
					return new DictionaryEntry(this.currentKey, this.currentValue);
				}
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x0600058C RID: 1420 RVA: 0x00017B20 File Offset: 0x00015D20
			public object Key
			{
				get
				{
					if (this.currentKey == null)
					{
						throw new InvalidOperationException();
					}
					this.FailFast();
					return this.currentKey;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x0600058D RID: 1421 RVA: 0x00017B40 File Offset: 0x00015D40
			public object Value
			{
				get
				{
					if (this.currentKey == null)
					{
						throw new InvalidOperationException();
					}
					this.FailFast();
					return this.currentValue;
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x0600058E RID: 1422 RVA: 0x00017B60 File Offset: 0x00015D60
			public object Current
			{
				get
				{
					if (this.currentKey == null)
					{
						throw new InvalidOperationException();
					}
					switch (this.mode)
					{
					case Hashtable.EnumeratorMode.KEY_MODE:
						return this.currentKey;
					case Hashtable.EnumeratorMode.VALUE_MODE:
						return this.currentValue;
					case Hashtable.EnumeratorMode.ENTRY_MODE:
						return new DictionaryEntry(this.currentKey, this.currentValue);
					default:
						throw new Exception("should never happen");
					}
				}
			}

			// Token: 0x04000220 RID: 544
			private Hashtable host;

			// Token: 0x04000221 RID: 545
			private int stamp;

			// Token: 0x04000222 RID: 546
			private int pos;

			// Token: 0x04000223 RID: 547
			private int size;

			// Token: 0x04000224 RID: 548
			private Hashtable.EnumeratorMode mode;

			// Token: 0x04000225 RID: 549
			private object currentKey;

			// Token: 0x04000226 RID: 550
			private object currentValue;

			// Token: 0x04000227 RID: 551
			private static readonly string xstr = "Hashtable.Enumerator: snapshot out of sync.";
		}

		// Token: 0x020000A1 RID: 161
		private enum EnumeratorMode
		{
			// Token: 0x04000229 RID: 553
			KEY_MODE,
			// Token: 0x0400022A RID: 554
			VALUE_MODE,
			// Token: 0x0400022B RID: 555
			ENTRY_MODE
		}

		// Token: 0x020000A2 RID: 162
		[DebuggerDisplay("Count={Count}")]
		[DebuggerTypeProxy(typeof(CollectionDebuggerView))]
		[Serializable]
		private class HashKeys : ICollection, IEnumerable
		{
			// Token: 0x0600058F RID: 1423 RVA: 0x00017BCC File Offset: 0x00015DCC
			public HashKeys(Hashtable host)
			{
				if (host == null)
				{
					throw new ArgumentNullException();
				}
				this.host = host;
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x06000590 RID: 1424 RVA: 0x00017BE8 File Offset: 0x00015DE8
			public virtual int Count
			{
				get
				{
					return this.host.Count;
				}
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06000591 RID: 1425 RVA: 0x00017BF8 File Offset: 0x00015DF8
			public virtual bool IsSynchronized
			{
				get
				{
					return this.host.IsSynchronized;
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06000592 RID: 1426 RVA: 0x00017C08 File Offset: 0x00015E08
			public virtual object SyncRoot
			{
				get
				{
					return this.host.SyncRoot;
				}
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x00017C18 File Offset: 0x00015E18
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				if (array.Length - arrayIndex < this.Count)
				{
					throw new ArgumentException("not enough space");
				}
				this.host.CopyToArray(array, arrayIndex, Hashtable.EnumeratorMode.KEY_MODE);
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x00017C8C File Offset: 0x00015E8C
			public virtual IEnumerator GetEnumerator()
			{
				return new Hashtable.Enumerator(this.host, Hashtable.EnumeratorMode.KEY_MODE);
			}

			// Token: 0x0400022C RID: 556
			private Hashtable host;
		}

		// Token: 0x020000A3 RID: 163
		[DebuggerDisplay("Count={Count}")]
		[DebuggerTypeProxy(typeof(CollectionDebuggerView))]
		[Serializable]
		private class HashValues : ICollection, IEnumerable
		{
			// Token: 0x06000595 RID: 1429 RVA: 0x00017C9C File Offset: 0x00015E9C
			public HashValues(Hashtable host)
			{
				if (host == null)
				{
					throw new ArgumentNullException();
				}
				this.host = host;
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06000596 RID: 1430 RVA: 0x00017CB8 File Offset: 0x00015EB8
			public virtual int Count
			{
				get
				{
					return this.host.Count;
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000597 RID: 1431 RVA: 0x00017CC8 File Offset: 0x00015EC8
			public virtual bool IsSynchronized
			{
				get
				{
					return this.host.IsSynchronized;
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000598 RID: 1432 RVA: 0x00017CD8 File Offset: 0x00015ED8
			public virtual object SyncRoot
			{
				get
				{
					return this.host.SyncRoot;
				}
			}

			// Token: 0x06000599 RID: 1433 RVA: 0x00017CE8 File Offset: 0x00015EE8
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				if (array.Length - arrayIndex < this.Count)
				{
					throw new ArgumentException("not enough space");
				}
				this.host.CopyToArray(array, arrayIndex, Hashtable.EnumeratorMode.VALUE_MODE);
			}

			// Token: 0x0600059A RID: 1434 RVA: 0x00017D5C File Offset: 0x00015F5C
			public virtual IEnumerator GetEnumerator()
			{
				return new Hashtable.Enumerator(this.host, Hashtable.EnumeratorMode.VALUE_MODE);
			}

			// Token: 0x0400022D RID: 557
			private Hashtable host;
		}

		// Token: 0x020000A4 RID: 164
		[Serializable]
		internal class KeyMarker
		{
			// Token: 0x0400022E RID: 558
			public static readonly Hashtable.KeyMarker Removed = new Hashtable.KeyMarker();
		}

		// Token: 0x020000A5 RID: 165
		[Serializable]
		internal struct Slot
		{
			// Token: 0x0400022F RID: 559
			internal object key;

			// Token: 0x04000230 RID: 560
			internal object value;
		}

		// Token: 0x020000A6 RID: 166
		[Serializable]
		private class SyncHashtable : Hashtable, IEnumerable
		{
			// Token: 0x0600059D RID: 1437 RVA: 0x00017D80 File Offset: 0x00015F80
			public SyncHashtable(Hashtable host)
			{
				if (host == null)
				{
					throw new ArgumentNullException();
				}
				this.host = host;
			}

			// Token: 0x0600059E RID: 1438 RVA: 0x00017D9C File Offset: 0x00015F9C
			internal SyncHashtable(SerializationInfo info, StreamingContext context)
			{
				this.host = (Hashtable)info.GetValue("ParentTable", typeof(Hashtable));
			}

			// Token: 0x0600059F RID: 1439 RVA: 0x00017DC4 File Offset: 0x00015FC4
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Hashtable.Enumerator(this.host, Hashtable.EnumeratorMode.ENTRY_MODE);
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x00017DD4 File Offset: 0x00015FD4
			public override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("ParentTable", this.host);
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00017DE8 File Offset: 0x00015FE8
			public override int Count
			{
				get
				{
					return this.host.Count;
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00017DF8 File Offset: 0x00015FF8
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00017DFC File Offset: 0x00015FFC
			public override object SyncRoot
			{
				get
				{
					return this.host.SyncRoot;
				}
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00017E0C File Offset: 0x0001600C
			public override bool IsFixedSize
			{
				get
				{
					return this.host.IsFixedSize;
				}
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00017E1C File Offset: 0x0001601C
			public override bool IsReadOnly
			{
				get
				{
					return this.host.IsReadOnly;
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00017E2C File Offset: 0x0001602C
			public override ICollection Keys
			{
				get
				{
					ICollection result = null;
					object syncRoot = this.host.SyncRoot;
					lock (syncRoot)
					{
						result = this.host.Keys;
					}
					return result;
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00017E78 File Offset: 0x00016078
			public override ICollection Values
			{
				get
				{
					ICollection result = null;
					object syncRoot = this.host.SyncRoot;
					lock (syncRoot)
					{
						result = this.host.Values;
					}
					return result;
				}
			}

			// Token: 0x170000D7 RID: 215
			public override object this[object key]
			{
				get
				{
					return this.host[key];
				}
				set
				{
					object syncRoot = this.host.SyncRoot;
					lock (syncRoot)
					{
						this.host[key] = value;
					}
				}
			}

			// Token: 0x060005AA RID: 1450 RVA: 0x00017F1C File Offset: 0x0001611C
			public override void CopyTo(Array array, int arrayIndex)
			{
				this.host.CopyTo(array, arrayIndex);
			}

			// Token: 0x060005AB RID: 1451 RVA: 0x00017F2C File Offset: 0x0001612C
			public override void Add(object key, object value)
			{
				object syncRoot = this.host.SyncRoot;
				lock (syncRoot)
				{
					this.host.Add(key, value);
				}
			}

			// Token: 0x060005AC RID: 1452 RVA: 0x00017F74 File Offset: 0x00016174
			public override void Clear()
			{
				object syncRoot = this.host.SyncRoot;
				lock (syncRoot)
				{
					this.host.Clear();
				}
			}

			// Token: 0x060005AD RID: 1453 RVA: 0x00017FBC File Offset: 0x000161BC
			public override bool Contains(object key)
			{
				return this.host.Find(key) >= 0;
			}

			// Token: 0x060005AE RID: 1454 RVA: 0x00017FD0 File Offset: 0x000161D0
			public override IDictionaryEnumerator GetEnumerator()
			{
				return new Hashtable.Enumerator(this.host, Hashtable.EnumeratorMode.ENTRY_MODE);
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x00017FE0 File Offset: 0x000161E0
			public override void Remove(object key)
			{
				object syncRoot = this.host.SyncRoot;
				lock (syncRoot)
				{
					this.host.Remove(key);
				}
			}

			// Token: 0x060005B0 RID: 1456 RVA: 0x00018028 File Offset: 0x00016228
			public override bool ContainsKey(object key)
			{
				return this.host.Contains(key);
			}

			// Token: 0x060005B1 RID: 1457 RVA: 0x00018038 File Offset: 0x00016238
			public override object Clone()
			{
				object syncRoot = this.host.SyncRoot;
				object result;
				lock (syncRoot)
				{
					result = new Hashtable.SyncHashtable((Hashtable)this.host.Clone());
				}
				return result;
			}

			// Token: 0x04000231 RID: 561
			private Hashtable host;
		}
	}
}
