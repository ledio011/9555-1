using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;

namespace System.Threading
{
	// Token: 0x020003BC RID: 956
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Thread))]
	public sealed class Thread : CriticalFinalizerObject, _Thread
	{
		// Token: 0x06001CAC RID: 7340 RVA: 0x0006DB68 File Offset: 0x0006BD68
		public Thread(ThreadStart start)
		{
			if (start == null)
			{
				throw new ArgumentNullException("Null ThreadStart");
			}
			this.threadstart = start;
			this.Thread_init();
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0006DB98 File Offset: 0x0006BD98
		public Thread(ThreadStart start, int maxStackSize)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (maxStackSize < 131072)
			{
				throw new ArgumentException("< 128 kb", "maxStackSize");
			}
			this.threadstart = start;
			this.stack_size = maxStackSize;
			this.Thread_init();
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0006DBF4 File Offset: 0x0006BDF4
		public Thread(ParameterizedThreadStart start)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.threadstart = start;
			this.Thread_init();
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0006DC24 File Offset: 0x0006BE24
		public Thread(ParameterizedThreadStart start, int maxStackSize)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (maxStackSize < 131072)
			{
				throw new ArgumentException("< 128 kb", "maxStackSize");
			}
			this.threadstart = start;
			this.stack_size = maxStackSize;
			this.Thread_init();
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0006DC98 File Offset: 0x0006BE98
		void _Thread.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0006DCA0 File Offset: 0x0006BEA0
		void _Thread.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0006DCA8 File Offset: 0x0006BEA8
		void _Thread.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0006DCB0 File Offset: 0x0006BEB0
		void _Thread.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x0006DCB8 File Offset: 0x0006BEB8
		public static Context CurrentContext
		{
			get
			{
				return AppDomain.InternalGetContext();
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0006DCC0 File Offset: 0x0006BEC0
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x0006DD14 File Offset: 0x0006BF14
		public static IPrincipal CurrentPrincipal
		{
			get
			{
				IPrincipal principal = null;
				Thread currentThread = Thread.CurrentThread;
				Thread obj = currentThread;
				lock (obj)
				{
					principal = currentThread._principal;
					if (principal == null)
					{
						principal = Thread.GetDomain().DefaultPrincipal;
					}
				}
				return principal;
			}
			set
			{
				Thread currentThread = Thread.CurrentThread;
				Thread obj = currentThread;
				lock (obj)
				{
					currentThread._principal = value;
				}
			}
		}

		// Token: 0x06001CB8 RID: 7352
		[MethodImpl(4096)]
		private static extern Thread CurrentThread_internal();

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x0006DD54 File Offset: 0x0006BF54
		public static Thread CurrentThread
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				return Thread.CurrentThread_internal();
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x0006DD5C File Offset: 0x0006BF5C
		internal static int CurrentThreadId
		{
			get
			{
				return (int)Thread.CurrentThread.thread_id;
			}
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0006DD6C File Offset: 0x0006BF6C
		private static void InitDataStoreHash()
		{
			object obj = Thread.datastore_lock;
			lock (obj)
			{
				if (Thread.datastorehash == null)
				{
					Thread.datastorehash = Hashtable.Synchronized(new Hashtable());
				}
			}
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0006DDBC File Offset: 0x0006BFBC
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			object obj = Thread.datastore_lock;
			LocalDataStoreSlot result;
			lock (obj)
			{
				if (Thread.datastorehash == null)
				{
					Thread.InitDataStoreHash();
				}
				LocalDataStoreSlot localDataStoreSlot = (LocalDataStoreSlot)Thread.datastorehash[name];
				if (localDataStoreSlot != null)
				{
					throw new ArgumentException("Named data slot already added");
				}
				localDataStoreSlot = Thread.AllocateDataSlot();
				Thread.datastorehash.Add(name, localDataStoreSlot);
				result = localDataStoreSlot;
			}
			return result;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0006DE3C File Offset: 0x0006C03C
		public static void FreeNamedDataSlot(string name)
		{
			object obj = Thread.datastore_lock;
			lock (obj)
			{
				if (Thread.datastorehash != null)
				{
					Thread.datastorehash.Remove(name);
				}
			}
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0006DE88 File Offset: 0x0006C088
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return new LocalDataStoreSlot(true);
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0006DE90 File Offset: 0x0006C090
		public static object GetData(LocalDataStoreSlot slot)
		{
			object[] array = Thread.local_slots;
			if (slot == null)
			{
				throw new ArgumentNullException("slot");
			}
			if (array != null && slot.slot < array.Length)
			{
				return array[slot.slot];
			}
			return null;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0006DED4 File Offset: 0x0006C0D4
		public static void SetData(LocalDataStoreSlot slot, object data)
		{
			object[] array = Thread.local_slots;
			if (slot == null)
			{
				throw new ArgumentNullException("slot");
			}
			if (array == null)
			{
				array = new object[slot.slot + 2];
				Thread.local_slots = array;
			}
			else if (slot.slot >= array.Length)
			{
				object[] array2 = new object[slot.slot + 2];
				array.CopyTo(array2, 0);
				array = array2;
				Thread.local_slots = array;
			}
			array[slot.slot] = data;
		}

		// Token: 0x06001CC1 RID: 7361
		[MethodImpl(4096)]
		internal static extern void FreeLocalSlotValues(int slot, bool thread_local);

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0006DF4C File Offset: 0x0006C14C
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			object obj = Thread.datastore_lock;
			LocalDataStoreSlot result;
			lock (obj)
			{
				if (Thread.datastorehash == null)
				{
					Thread.InitDataStoreHash();
				}
				LocalDataStoreSlot localDataStoreSlot = (LocalDataStoreSlot)Thread.datastorehash[name];
				if (localDataStoreSlot == null)
				{
					localDataStoreSlot = Thread.AllocateNamedDataSlot(name);
				}
				result = localDataStoreSlot;
			}
			return result;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0006DFB8 File Offset: 0x0006C1B8
		public static AppDomain GetDomain()
		{
			return AppDomain.CurrentDomain;
		}

		// Token: 0x06001CC4 RID: 7364
		[MethodImpl(4096)]
		public static extern int GetDomainID();

		// Token: 0x06001CC5 RID: 7365
		[MethodImpl(4096)]
		private static extern void ResetAbort_internal();

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0006DFC0 File Offset: 0x0006C1C0
		public static void ResetAbort()
		{
			Thread.ResetAbort_internal();
		}

		// Token: 0x06001CC7 RID: 7367
		[MethodImpl(4096)]
		private static extern void Sleep_internal(int ms);

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0006DFC8 File Offset: 0x0006C1C8
		public static void Sleep(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", "Negative timeout");
			}
			Thread.Sleep_internal(millisecondsTimeout);
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0006DFE8 File Offset: 0x0006C1E8
		public static void Sleep(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", "timeout out of range");
			}
			Thread.Sleep_internal((int)num);
		}

		// Token: 0x06001CCA RID: 7370
		[MethodImpl(4096)]
		private extern IntPtr Thread_internal(MulticastDelegate start);

		// Token: 0x06001CCB RID: 7371
		[MethodImpl(4096)]
		private extern void Thread_init();

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x0006E02C File Offset: 0x0006C22C
		// (set) Token: 0x06001CCD RID: 7373 RVA: 0x0006E050 File Offset: 0x0006C250
		[Obsolete("Deprecated in favor of GetApartmentState, SetApartmentState and TrySetApartmentState.")]
		public ApartmentState ApartmentState
		{
			get
			{
				if ((this.ThreadState & ThreadState.Stopped) != ThreadState.Running)
				{
					throw new ThreadStateException("Thread is dead; state can not be accessed.");
				}
				return (ApartmentState)this.apartment_state;
			}
			set
			{
				this.TrySetApartmentState(value);
			}
		}

		// Token: 0x06001CCE RID: 7374
		[MethodImpl(4096)]
		private extern CultureInfo GetCachedCurrentCulture();

		// Token: 0x06001CCF RID: 7375
		[MethodImpl(4096)]
		private extern byte[] GetSerializedCurrentCulture();

		// Token: 0x06001CD0 RID: 7376
		[MethodImpl(4096)]
		private extern void SetCachedCurrentCulture(CultureInfo culture);

		// Token: 0x06001CD1 RID: 7377
		[MethodImpl(4096)]
		private extern void SetSerializedCurrentCulture(byte[] culture);

		// Token: 0x06001CD2 RID: 7378
		[MethodImpl(4096)]
		private extern CultureInfo GetCachedCurrentUICulture();

		// Token: 0x06001CD3 RID: 7379
		[MethodImpl(4096)]
		private extern byte[] GetSerializedCurrentUICulture();

		// Token: 0x06001CD4 RID: 7380
		[MethodImpl(4096)]
		private extern void SetCachedCurrentUICulture(CultureInfo culture);

		// Token: 0x06001CD5 RID: 7381
		[MethodImpl(4096)]
		private extern void SetSerializedCurrentUICulture(byte[] culture);

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x0006E05C File Offset: 0x0006C25C
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x0006E134 File Offset: 0x0006C334
		public CultureInfo CurrentCulture
		{
			get
			{
				if (this.in_currentculture)
				{
					return CultureInfo.InvariantCulture;
				}
				CultureInfo cultureInfo = this.GetCachedCurrentCulture();
				if (cultureInfo != null)
				{
					return cultureInfo;
				}
				byte[] serializedCurrentCulture = this.GetSerializedCurrentCulture();
				if (serializedCurrentCulture == null)
				{
					object obj = Thread.culture_lock;
					lock (obj)
					{
						this.in_currentculture = true;
						cultureInfo = CultureInfo.ConstructCurrentCulture();
						this.SetCachedCurrentCulture(cultureInfo);
						this.in_currentculture = false;
						NumberFormatter.SetThreadCurrentCulture(cultureInfo);
						return cultureInfo;
					}
				}
				this.in_currentculture = true;
				try
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					MemoryStream serializationStream = new MemoryStream(serializedCurrentCulture);
					cultureInfo = (CultureInfo)binaryFormatter.Deserialize(serializationStream);
					this.SetCachedCurrentCulture(cultureInfo);
				}
				finally
				{
					this.in_currentculture = false;
				}
				NumberFormatter.SetThreadCurrentCulture(cultureInfo);
				return cultureInfo;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				CultureInfo cachedCurrentCulture = this.GetCachedCurrentCulture();
				if (cachedCurrentCulture == value)
				{
					return;
				}
				value.CheckNeutral();
				this.in_currentculture = true;
				try
				{
					this.SetCachedCurrentCulture(value);
					byte[] array;
					if (value.IsReadOnly && value.cached_serialized_form != null)
					{
						array = value.cached_serialized_form;
					}
					else
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						MemoryStream memoryStream = new MemoryStream();
						binaryFormatter.Serialize(memoryStream, value);
						array = memoryStream.GetBuffer();
						if (value.IsReadOnly)
						{
							value.cached_serialized_form = array;
						}
					}
					this.SetSerializedCurrentCulture(array);
				}
				finally
				{
					this.in_currentculture = false;
				}
				NumberFormatter.SetThreadCurrentCulture(value);
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x0006E1F0 File Offset: 0x0006C3F0
		// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x0006E2BC File Offset: 0x0006C4BC
		public CultureInfo CurrentUICulture
		{
			get
			{
				if (this.in_currentculture)
				{
					return CultureInfo.InvariantCulture;
				}
				CultureInfo cultureInfo = this.GetCachedCurrentUICulture();
				if (cultureInfo != null)
				{
					return cultureInfo;
				}
				byte[] serializedCurrentUICulture = this.GetSerializedCurrentUICulture();
				if (serializedCurrentUICulture == null)
				{
					object obj = Thread.culture_lock;
					lock (obj)
					{
						this.in_currentculture = true;
						cultureInfo = CultureInfo.ConstructCurrentUICulture();
						this.SetCachedCurrentUICulture(cultureInfo);
						this.in_currentculture = false;
						return cultureInfo;
					}
				}
				this.in_currentculture = true;
				try
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					MemoryStream serializationStream = new MemoryStream(serializedCurrentUICulture);
					cultureInfo = (CultureInfo)binaryFormatter.Deserialize(serializationStream);
					this.SetCachedCurrentUICulture(cultureInfo);
				}
				finally
				{
					this.in_currentculture = false;
				}
				return cultureInfo;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				CultureInfo cachedCurrentUICulture = this.GetCachedCurrentUICulture();
				if (cachedCurrentUICulture == value)
				{
					return;
				}
				this.in_currentculture = true;
				try
				{
					this.SetCachedCurrentUICulture(value);
					byte[] array;
					if (value.IsReadOnly && value.cached_serialized_form != null)
					{
						array = value.cached_serialized_form;
					}
					else
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						MemoryStream memoryStream = new MemoryStream();
						binaryFormatter.Serialize(memoryStream, value);
						array = memoryStream.GetBuffer();
						if (value.IsReadOnly)
						{
							value.cached_serialized_form = array;
						}
					}
					this.SetSerializedCurrentUICulture(array);
				}
				finally
				{
					this.in_currentculture = false;
				}
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x0006E36C File Offset: 0x0006C56C
		public bool IsThreadPoolThread
		{
			get
			{
				return this.IsThreadPoolThreadInternal;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x0006E374 File Offset: 0x0006C574
		// (set) Token: 0x06001CDC RID: 7388 RVA: 0x0006E37C File Offset: 0x0006C57C
		internal bool IsThreadPoolThreadInternal
		{
			get
			{
				return this.threadpool_thread;
			}
			set
			{
				this.threadpool_thread = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x0006E388 File Offset: 0x0006C588
		public bool IsAlive
		{
			get
			{
				ThreadState threadState = this.GetState();
				return (threadState & ThreadState.Aborted) == ThreadState.Running && (threadState & ThreadState.Stopped) == ThreadState.Running && (threadState & ThreadState.Unstarted) == ThreadState.Running;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x0006E3BC File Offset: 0x0006C5BC
		// (set) Token: 0x06001CDF RID: 7391 RVA: 0x0006E3F0 File Offset: 0x0006C5F0
		public bool IsBackground
		{
			get
			{
				ThreadState threadState = this.GetState();
				if ((threadState & ThreadState.Stopped) != ThreadState.Running)
				{
					throw new ThreadStateException("Thread is dead; state can not be accessed.");
				}
				return (threadState & ThreadState.Background) != ThreadState.Running;
			}
			set
			{
				if (value)
				{
					this.SetState(ThreadState.Background);
				}
				else
				{
					this.ClrState(ThreadState.Background);
				}
			}
		}

		// Token: 0x06001CE0 RID: 7392
		[MethodImpl(4096)]
		private extern string GetName_internal();

		// Token: 0x06001CE1 RID: 7393
		[MethodImpl(4096)]
		private extern void SetName_internal(string name);

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x0006E40C File Offset: 0x0006C60C
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x0006E414 File Offset: 0x0006C614
		public string Name
		{
			get
			{
				return this.GetName_internal();
			}
			set
			{
				this.SetName_internal(value);
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x0006E420 File Offset: 0x0006C620
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x0006E424 File Offset: 0x0006C624
		public ThreadPriority Priority
		{
			get
			{
				return ThreadPriority.Lowest;
			}
			set
			{
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x0006E428 File Offset: 0x0006C628
		public ThreadState ThreadState
		{
			get
			{
				return this.GetState();
			}
		}

		// Token: 0x06001CE7 RID: 7399
		[MethodImpl(4096)]
		private extern void Abort_internal(object stateInfo);

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0006E430 File Offset: 0x0006C630
		public void Abort()
		{
			this.Abort_internal(null);
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0006E43C File Offset: 0x0006C63C
		public void Abort(object stateInfo)
		{
			this.Abort_internal(stateInfo);
		}

		// Token: 0x06001CEA RID: 7402
		[MethodImpl(4096)]
		internal extern object GetAbortExceptionState();

		// Token: 0x06001CEB RID: 7403
		[MethodImpl(4096)]
		private extern void Interrupt_internal();

		// Token: 0x06001CEC RID: 7404 RVA: 0x0006E448 File Offset: 0x0006C648
		public void Interrupt()
		{
			this.Interrupt_internal();
		}

		// Token: 0x06001CED RID: 7405
		[MethodImpl(4096)]
		private extern bool Join_internal(int ms, IntPtr handle);

		// Token: 0x06001CEE RID: 7406 RVA: 0x0006E450 File Offset: 0x0006C650
		public void Join()
		{
			this.Join_internal(-1, this.system_thread_handle);
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0006E460 File Offset: 0x0006C660
		public bool Join(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", "Timeout less than zero");
			}
			return this.Join_internal(millisecondsTimeout, this.system_thread_handle);
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0006E488 File Offset: 0x0006C688
		public bool Join(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", "timeout out of range");
			}
			return this.Join_internal((int)num, this.system_thread_handle);
		}

		// Token: 0x06001CF1 RID: 7409
		[MethodImpl(4096)]
		public static extern void MemoryBarrier();

		// Token: 0x06001CF2 RID: 7410
		[MethodImpl(4096)]
		private extern void Resume_internal();

		// Token: 0x06001CF3 RID: 7411 RVA: 0x0006E4D0 File Offset: 0x0006C6D0
		[Obsolete("")]
		public void Resume()
		{
			this.Resume_internal();
		}

		// Token: 0x06001CF4 RID: 7412
		[MethodImpl(4096)]
		private static extern void SpinWait_nop();

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0006E4D8 File Offset: 0x0006C6D8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void SpinWait(int iterations)
		{
			if (iterations < 0)
			{
				return;
			}
			while (iterations-- > 0)
			{
				Thread.SpinWait_nop();
			}
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0006E4F8 File Offset: 0x0006C6F8
		public void Start()
		{
			if (!ExecutionContext.IsFlowSuppressed())
			{
				this.ec_to_set = ExecutionContext.Capture();
			}
			if (Thread.CurrentThread._principal != null)
			{
				this._principal = Thread.CurrentThread._principal;
			}
			if (this.Thread_internal(this.threadstart) == (IntPtr)0)
			{
				throw new SystemException("Thread creation failed.");
			}
		}

		// Token: 0x06001CF7 RID: 7415
		[MethodImpl(4096)]
		private extern void Suspend_internal();

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0006E560 File Offset: 0x0006C760
		[Obsolete("")]
		public void Suspend()
		{
			this.Suspend_internal();
		}

		// Token: 0x06001CF9 RID: 7417
		[MethodImpl(4096)]
		private extern void Thread_free_internal(IntPtr handle);

		// Token: 0x06001CFA RID: 7418 RVA: 0x0006E568 File Offset: 0x0006C768
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~Thread()
		{
			this.Thread_free_internal(this.system_thread_handle);
		}

		// Token: 0x06001CFB RID: 7419
		[MethodImpl(4096)]
		private extern void SetState(ThreadState set);

		// Token: 0x06001CFC RID: 7420
		[MethodImpl(4096)]
		private extern void ClrState(ThreadState clr);

		// Token: 0x06001CFD RID: 7421
		[MethodImpl(4096)]
		private extern ThreadState GetState();

		// Token: 0x06001CFE RID: 7422
		[MethodImpl(4096)]
		public static extern byte VolatileRead(ref byte address);

		// Token: 0x06001CFF RID: 7423
		[MethodImpl(4096)]
		public static extern double VolatileRead(ref double address);

		// Token: 0x06001D00 RID: 7424
		[MethodImpl(4096)]
		public static extern short VolatileRead(ref short address);

		// Token: 0x06001D01 RID: 7425
		[MethodImpl(4096)]
		public static extern int VolatileRead(ref int address);

		// Token: 0x06001D02 RID: 7426
		[MethodImpl(4096)]
		public static extern long VolatileRead(ref long address);

		// Token: 0x06001D03 RID: 7427
		[MethodImpl(4096)]
		public static extern IntPtr VolatileRead(ref IntPtr address);

		// Token: 0x06001D04 RID: 7428
		[MethodImpl(4096)]
		public static extern object VolatileRead(ref object address);

		// Token: 0x06001D05 RID: 7429
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public static extern sbyte VolatileRead(ref sbyte address);

		// Token: 0x06001D06 RID: 7430
		[MethodImpl(4096)]
		public static extern float VolatileRead(ref float address);

		// Token: 0x06001D07 RID: 7431
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public static extern ushort VolatileRead(ref ushort address);

		// Token: 0x06001D08 RID: 7432
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public static extern uint VolatileRead(ref uint address);

		// Token: 0x06001D09 RID: 7433
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public static extern ulong VolatileRead(ref ulong address);

		// Token: 0x06001D0A RID: 7434
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public static extern UIntPtr VolatileRead(ref UIntPtr address);

		// Token: 0x06001D0B RID: 7435
		[MethodImpl(4096)]
		public static extern void VolatileWrite(ref byte address, byte value);

		// Token: 0x06001D0C RID: 7436
		[MethodImpl(4096)]
		public static extern void VolatileWrite(ref double address, double value);

		// Token: 0x06001D0D RID: 7437
		[MethodImpl(4096)]
		public static extern void VolatileWrite(ref short address, short value);

		// Token: 0x06001D0E RID: 7438
		[MethodImpl(4096)]
		public static extern void VolatileWrite(ref int address, int value);

		// Token: 0x06001D0F RID: 7439
		[MethodImpl(4096)]
		public static extern void VolatileWrite(ref long address, long value);

		// Token: 0x06001D10 RID: 7440
		[MethodImpl(4096)]
		public static extern void VolatileWrite(ref IntPtr address, IntPtr value);

		// Token: 0x06001D11 RID: 7441
		[MethodImpl(4096)]
		public static extern void VolatileWrite(ref object address, object value);

		// Token: 0x06001D12 RID: 7442
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public static extern void VolatileWrite(ref sbyte address, sbyte value);

		// Token: 0x06001D13 RID: 7443
		[MethodImpl(4096)]
		public static extern void VolatileWrite(ref float address, float value);

		// Token: 0x06001D14 RID: 7444
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public static extern void VolatileWrite(ref ushort address, ushort value);

		// Token: 0x06001D15 RID: 7445
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public static extern void VolatileWrite(ref uint address, uint value);

		// Token: 0x06001D16 RID: 7446
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public static extern void VolatileWrite(ref ulong address, ulong value);

		// Token: 0x06001D17 RID: 7447
		[CLSCompliant(false)]
		[MethodImpl(4096)]
		public static extern void VolatileWrite(ref UIntPtr address, UIntPtr value);

		// Token: 0x06001D18 RID: 7448 RVA: 0x0006E5A0 File Offset: 0x0006C7A0
		private static int GetNewManagedId()
		{
			return Thread.GetNewManagedId_internal();
		}

		// Token: 0x06001D19 RID: 7449
		[MethodImpl(4096)]
		private static extern int GetNewManagedId_internal();

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x0006E5A8 File Offset: 0x0006C7A8
		[MonoTODO("limited to CompressedStack support")]
		public ExecutionContext ExecutionContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				if (Thread._ec == null)
				{
					Thread._ec = new ExecutionContext();
				}
				return Thread._ec;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x0006E5C4 File Offset: 0x0006C7C4
		public int ManagedThreadId
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				if (this.managed_id == 0)
				{
					int newManagedId = Thread.GetNewManagedId();
					Interlocked.CompareExchange(ref this.managed_id, newManagedId, 0);
				}
				return this.managed_id;
			}
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0006E5F8 File Offset: 0x0006C7F8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void BeginCriticalRegion()
		{
			Thread.CurrentThread.critical_region_level++;
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0006E610 File Offset: 0x0006C810
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void EndCriticalRegion()
		{
			Thread.CurrentThread.critical_region_level--;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0006E628 File Offset: 0x0006C828
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void BeginThreadAffinity()
		{
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x0006E62C File Offset: 0x0006C82C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void EndThreadAffinity()
		{
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x0006E630 File Offset: 0x0006C830
		public ApartmentState GetApartmentState()
		{
			return (ApartmentState)this.apartment_state;
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0006E638 File Offset: 0x0006C838
		public void SetApartmentState(ApartmentState state)
		{
			if (!this.TrySetApartmentState(state))
			{
				throw new InvalidOperationException("Failed to set the specified COM apartment state.");
			}
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x0006E654 File Offset: 0x0006C854
		public bool TrySetApartmentState(ApartmentState state)
		{
			if (this != Thread.CurrentThread && (this.ThreadState & ThreadState.Unstarted) == ThreadState.Running)
			{
				throw new ThreadStateException("Thread was in an invalid state for the operation being executed.");
			}
			if (this.apartment_state != 2)
			{
				return false;
			}
			this.apartment_state = (byte)state;
			return true;
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x0006E690 File Offset: 0x0006C890
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.ManagedThreadId;
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x0006E698 File Offset: 0x0006C898
		public void Start(object parameter)
		{
			this.start_obj = parameter;
			this.Start();
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x0006E6A8 File Offset: 0x0006C8A8
		[Obsolete("see CompressedStack class")]
		public CompressedStack GetCompressedStack()
		{
			CompressedStack compressedStack = this.ExecutionContext.SecurityContext.CompressedStack;
			return (compressedStack != null && !compressedStack.IsEmpty()) ? compressedStack.CreateCopy() : null;
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x0006E6E4 File Offset: 0x0006C8E4
		[Obsolete("see CompressedStack class")]
		public void SetCompressedStack(CompressedStack stack)
		{
			this.ExecutionContext.SecurityContext.CompressedStack = stack;
		}

		// Token: 0x04000F26 RID: 3878
		private int lock_thread_id;

		// Token: 0x04000F27 RID: 3879
		private IntPtr system_thread_handle;

		// Token: 0x04000F28 RID: 3880
		private object cached_culture_info;

		// Token: 0x04000F29 RID: 3881
		private IntPtr unused0;

		// Token: 0x04000F2A RID: 3882
		private bool threadpool_thread;

		// Token: 0x04000F2B RID: 3883
		private IntPtr name;

		// Token: 0x04000F2C RID: 3884
		private int name_len;

		// Token: 0x04000F2D RID: 3885
		private ThreadState state = ThreadState.Unstarted;

		// Token: 0x04000F2E RID: 3886
		private object abort_exc;

		// Token: 0x04000F2F RID: 3887
		private int abort_state_handle;

		// Token: 0x04000F30 RID: 3888
		private long thread_id;

		// Token: 0x04000F31 RID: 3889
		private IntPtr start_notify;

		// Token: 0x04000F32 RID: 3890
		private IntPtr stack_ptr;

		// Token: 0x04000F33 RID: 3891
		private UIntPtr static_data;

		// Token: 0x04000F34 RID: 3892
		private IntPtr jit_data;

		// Token: 0x04000F35 RID: 3893
		private IntPtr lock_data;

		// Token: 0x04000F36 RID: 3894
		private object current_appcontext;

		// Token: 0x04000F37 RID: 3895
		private int stack_size;

		// Token: 0x04000F38 RID: 3896
		private object start_obj;

		// Token: 0x04000F39 RID: 3897
		private IntPtr appdomain_refs;

		// Token: 0x04000F3A RID: 3898
		private int interruption_requested;

		// Token: 0x04000F3B RID: 3899
		private IntPtr suspend_event;

		// Token: 0x04000F3C RID: 3900
		private IntPtr suspended_event;

		// Token: 0x04000F3D RID: 3901
		private IntPtr resume_event;

		// Token: 0x04000F3E RID: 3902
		private IntPtr synch_cs;

		// Token: 0x04000F3F RID: 3903
		private IntPtr serialized_culture_info;

		// Token: 0x04000F40 RID: 3904
		private int serialized_culture_info_len;

		// Token: 0x04000F41 RID: 3905
		private IntPtr serialized_ui_culture_info;

		// Token: 0x04000F42 RID: 3906
		private int serialized_ui_culture_info_len;

		// Token: 0x04000F43 RID: 3907
		private bool thread_dump_requested;

		// Token: 0x04000F44 RID: 3908
		private IntPtr end_stack;

		// Token: 0x04000F45 RID: 3909
		private bool thread_interrupt_requested;

		// Token: 0x04000F46 RID: 3910
		private byte apartment_state;

		// Token: 0x04000F47 RID: 3911
		private volatile int critical_region_level;

		// Token: 0x04000F48 RID: 3912
		private int small_id;

		// Token: 0x04000F49 RID: 3913
		private IntPtr manage_callback;

		// Token: 0x04000F4A RID: 3914
		private object pending_exception;

		// Token: 0x04000F4B RID: 3915
		private ExecutionContext ec_to_set;

		// Token: 0x04000F4C RID: 3916
		private IntPtr interrupt_on_stop;

		// Token: 0x04000F4D RID: 3917
		private IntPtr unused3;

		// Token: 0x04000F4E RID: 3918
		private IntPtr unused4;

		// Token: 0x04000F4F RID: 3919
		private IntPtr unused5;

		// Token: 0x04000F50 RID: 3920
		private IntPtr unused6;

		// Token: 0x04000F51 RID: 3921
		[ThreadStatic]
		private static object[] local_slots;

		// Token: 0x04000F52 RID: 3922
		[ThreadStatic]
		private static ExecutionContext _ec;

		// Token: 0x04000F53 RID: 3923
		private MulticastDelegate threadstart;

		// Token: 0x04000F54 RID: 3924
		private int managed_id;

		// Token: 0x04000F55 RID: 3925
		private IPrincipal _principal;

		// Token: 0x04000F56 RID: 3926
		private static Hashtable datastorehash;

		// Token: 0x04000F57 RID: 3927
		private static object datastore_lock = new object();

		// Token: 0x04000F58 RID: 3928
		private bool in_currentculture;

		// Token: 0x04000F59 RID: 3929
		private static object culture_lock = new object();
	}
}
