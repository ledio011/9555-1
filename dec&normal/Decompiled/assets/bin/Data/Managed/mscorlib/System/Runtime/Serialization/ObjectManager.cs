using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000309 RID: 777
	[ComVisible(true)]
	public class ObjectManager
	{
		// Token: 0x060017D9 RID: 6105 RVA: 0x00056B34 File Offset: 0x00054D34
		public ObjectManager(ISurrogateSelector selector, StreamingContext context)
		{
			this._selector = selector;
			this._context = context;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00056B6C File Offset: 0x00054D6C
		public virtual void DoFixups()
		{
			this._finalFixup = true;
			try
			{
				if (this._registeredObjectsCount < this._objectRecords.Count)
				{
					throw new SerializationException("There are some fixups that refer to objects that have not been registered");
				}
				ObjectRecord lastObjectRecord = this._lastObjectRecord;
				bool flag = true;
				ObjectRecord objectRecord2;
				for (ObjectRecord objectRecord = this._objectRecordChain; objectRecord != null; objectRecord = objectRecord2)
				{
					bool flag2 = !objectRecord.IsUnsolvedObjectReference || !flag;
					if (flag2)
					{
						flag2 = objectRecord.DoFixups(true, this, true);
					}
					if (flag2)
					{
						flag2 = objectRecord.LoadData(this, this._selector, this._context);
					}
					if (flag2)
					{
						if (objectRecord.OriginalObject is IDeserializationCallback)
						{
							this._deserializedRecords.Add(objectRecord);
						}
						SerializationCallbacks serializationCallbacks = SerializationCallbacks.GetSerializationCallbacks(objectRecord.OriginalObject.GetType());
						if (serializationCallbacks.HasDeserializedCallbacks)
						{
							this._onDeserializedCallbackRecords.Add(objectRecord);
						}
						objectRecord2 = objectRecord.Next;
					}
					else
					{
						if (objectRecord.ObjectInstance is IObjectReference && !flag)
						{
							if (objectRecord.Status == ObjectRecordStatus.ReferenceSolvingDelayed)
							{
								throw new SerializationException("The object with ID " + objectRecord.ObjectID + " could not be resolved");
							}
							objectRecord.Status = ObjectRecordStatus.ReferenceSolvingDelayed;
						}
						if (objectRecord != this._lastObjectRecord)
						{
							objectRecord2 = objectRecord.Next;
							objectRecord.Next = null;
							this._lastObjectRecord.Next = objectRecord;
							this._lastObjectRecord = objectRecord;
						}
						else
						{
							objectRecord2 = objectRecord;
						}
					}
					if (objectRecord == lastObjectRecord)
					{
						flag = false;
					}
				}
			}
			finally
			{
				this._finalFixup = false;
			}
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00056D08 File Offset: 0x00054F08
		internal ObjectRecord GetObjectRecord(long objectID)
		{
			ObjectRecord objectRecord = (ObjectRecord)this._objectRecords[objectID];
			if (objectRecord == null)
			{
				if (this._finalFixup)
				{
					throw new SerializationException("The object with Id " + objectID + " has not been registered");
				}
				objectRecord = new ObjectRecord();
				objectRecord.ObjectID = objectID;
				this._objectRecords[objectID] = objectRecord;
			}
			if (!objectRecord.IsRegistered && this._finalFixup)
			{
				throw new SerializationException("The object with Id " + objectID + " has not been registered");
			}
			return objectRecord;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00056DAC File Offset: 0x00054FAC
		public virtual object GetObject(long objectID)
		{
			if (objectID <= 0L)
			{
				throw new ArgumentOutOfRangeException("objectID", "The objectID parameter is less than or equal to zero");
			}
			ObjectRecord objectRecord = (ObjectRecord)this._objectRecords[objectID];
			if (objectRecord == null || !objectRecord.IsRegistered)
			{
				return null;
			}
			return objectRecord.ObjectInstance;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00056E04 File Offset: 0x00055004
		public virtual void RaiseDeserializationEvent()
		{
			for (int i = this._onDeserializedCallbackRecords.Count - 1; i >= 0; i--)
			{
				ObjectRecord objectRecord = (ObjectRecord)this._onDeserializedCallbackRecords[i];
				this.RaiseOnDeserializedEvent(objectRecord.OriginalObject);
			}
			for (int j = this._deserializedRecords.Count - 1; j >= 0; j--)
			{
				ObjectRecord objectRecord2 = (ObjectRecord)this._deserializedRecords[j];
				IDeserializationCallback deserializationCallback = objectRecord2.OriginalObject as IDeserializationCallback;
				if (deserializationCallback != null)
				{
					deserializationCallback.OnDeserialization(this);
				}
			}
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00056E9C File Offset: 0x0005509C
		public void RaiseOnDeserializingEvent(object obj)
		{
			SerializationCallbacks serializationCallbacks = SerializationCallbacks.GetSerializationCallbacks(obj.GetType());
			serializationCallbacks.RaiseOnDeserializing(obj, this._context);
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00056EC4 File Offset: 0x000550C4
		private void RaiseOnDeserializedEvent(object obj)
		{
			SerializationCallbacks serializationCallbacks = SerializationCallbacks.GetSerializationCallbacks(obj.GetType());
			serializationCallbacks.RaiseOnDeserialized(obj, this._context);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00056EEC File Offset: 0x000550EC
		private void AddFixup(BaseFixupRecord record)
		{
			record.ObjectToBeFixed.ChainFixup(record, true);
			record.ObjectRequired.ChainFixup(record, false);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00056F08 File Offset: 0x00055108
		public virtual void RecordArrayElementFixup(long arrayToBeFixed, int index, long objectRequired)
		{
			if (arrayToBeFixed <= 0L)
			{
				throw new ArgumentOutOfRangeException("arrayToBeFixed", "The arrayToBeFixed parameter is less than or equal to zero");
			}
			if (objectRequired <= 0L)
			{
				throw new ArgumentOutOfRangeException("objectRequired", "The objectRequired parameter is less than or equal to zero");
			}
			ArrayFixupRecord record = new ArrayFixupRecord(this.GetObjectRecord(arrayToBeFixed), index, this.GetObjectRecord(objectRequired));
			this.AddFixup(record);
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00056F64 File Offset: 0x00055164
		public virtual void RecordArrayElementFixup(long arrayToBeFixed, int[] indices, long objectRequired)
		{
			if (arrayToBeFixed <= 0L)
			{
				throw new ArgumentOutOfRangeException("arrayToBeFixed", "The arrayToBeFixed parameter is less than or equal to zero");
			}
			if (objectRequired <= 0L)
			{
				throw new ArgumentOutOfRangeException("objectRequired", "The objectRequired parameter is less than or equal to zero");
			}
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			MultiArrayFixupRecord record = new MultiArrayFixupRecord(this.GetObjectRecord(arrayToBeFixed), indices, this.GetObjectRecord(objectRequired));
			this.AddFixup(record);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00056FD0 File Offset: 0x000551D0
		public virtual void RecordDelayedFixup(long objectToBeFixed, string memberName, long objectRequired)
		{
			if (objectToBeFixed <= 0L)
			{
				throw new ArgumentOutOfRangeException("objectToBeFixed", "The objectToBeFixed parameter is less than or equal to zero");
			}
			if (objectRequired <= 0L)
			{
				throw new ArgumentOutOfRangeException("objectRequired", "The objectRequired parameter is less than or equal to zero");
			}
			if (memberName == null)
			{
				throw new ArgumentNullException("memberName");
			}
			DelayedFixupRecord record = new DelayedFixupRecord(this.GetObjectRecord(objectToBeFixed), memberName, this.GetObjectRecord(objectRequired));
			this.AddFixup(record);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0005703C File Offset: 0x0005523C
		public virtual void RecordFixup(long objectToBeFixed, MemberInfo member, long objectRequired)
		{
			if (objectToBeFixed <= 0L)
			{
				throw new ArgumentOutOfRangeException("objectToBeFixed", "The objectToBeFixed parameter is less than or equal to zero");
			}
			if (objectRequired <= 0L)
			{
				throw new ArgumentOutOfRangeException("objectRequired", "The objectRequired parameter is less than or equal to zero");
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			FixupRecord record = new FixupRecord(this.GetObjectRecord(objectToBeFixed), member, this.GetObjectRecord(objectRequired));
			this.AddFixup(record);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x000570A8 File Offset: 0x000552A8
		private void RegisterObjectInternal(object obj, ObjectRecord record)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (!record.IsRegistered)
			{
				record.ObjectInstance = obj;
				record.OriginalObject = obj;
				if (obj is IObjectReference)
				{
					record.Status = ObjectRecordStatus.ReferenceUnsolved;
				}
				else
				{
					record.Status = ObjectRecordStatus.ReferenceSolved;
				}
				if (this._selector != null)
				{
					record.Surrogate = this._selector.GetSurrogate(obj.GetType(), this._context, out record.SurrogateSelector);
					if (record.Surrogate != null)
					{
						record.Status = ObjectRecordStatus.ReferenceUnsolved;
					}
				}
				record.DoFixups(true, this, false);
				record.DoFixups(false, this, false);
				this._registeredObjectsCount++;
				if (this._objectRecordChain == null)
				{
					this._objectRecordChain = record;
					this._lastObjectRecord = record;
				}
				else
				{
					this._lastObjectRecord.Next = record;
					this._lastObjectRecord = record;
				}
				return;
			}
			if (record.OriginalObject != obj)
			{
				throw new SerializationException("An object with Id " + record.ObjectID + " has already been registered");
			}
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000571C0 File Offset: 0x000553C0
		public void RegisterObject(object obj, long objectID, SerializationInfo info, long idOfContainingObj, MemberInfo member, int[] arrayIndex)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj", "The obj parameter is null.");
			}
			if (objectID <= 0L)
			{
				throw new ArgumentOutOfRangeException("objectID", "The objectID parameter is less than or equal to zero");
			}
			ObjectRecord objectRecord = this.GetObjectRecord(objectID);
			objectRecord.Info = info;
			objectRecord.IdOfContainingObj = idOfContainingObj;
			objectRecord.Member = member;
			objectRecord.ArrayIndex = arrayIndex;
			this.RegisterObjectInternal(obj, objectRecord);
		}

		// Token: 0x04000C60 RID: 3168
		private ObjectRecord _objectRecordChain;

		// Token: 0x04000C61 RID: 3169
		private ObjectRecord _lastObjectRecord;

		// Token: 0x04000C62 RID: 3170
		private ArrayList _deserializedRecords = new ArrayList();

		// Token: 0x04000C63 RID: 3171
		private ArrayList _onDeserializedCallbackRecords = new ArrayList();

		// Token: 0x04000C64 RID: 3172
		private Hashtable _objectRecords = new Hashtable();

		// Token: 0x04000C65 RID: 3173
		private bool _finalFixup;

		// Token: 0x04000C66 RID: 3174
		private ISurrogateSelector _selector;

		// Token: 0x04000C67 RID: 3175
		private StreamingContext _context;

		// Token: 0x04000C68 RID: 3176
		private int _registeredObjectsCount;
	}
}
