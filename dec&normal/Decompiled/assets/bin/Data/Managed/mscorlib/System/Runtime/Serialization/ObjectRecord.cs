using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x0200030A RID: 778
	internal class ObjectRecord
	{
		// Token: 0x060017E8 RID: 6120 RVA: 0x00057234 File Offset: 0x00055434
		public void SetMemberValue(ObjectManager manager, MemberInfo member, object value)
		{
			if (member is FieldInfo)
			{
				((FieldInfo)member).SetValue(this.ObjectInstance, value);
			}
			else
			{
				if (!(member is PropertyInfo))
				{
					throw new SerializationException("Cannot perform fixup");
				}
				((PropertyInfo)member).SetValue(this.ObjectInstance, value, null);
			}
			if (this.Member != null)
			{
				ObjectRecord objectRecord = manager.GetObjectRecord(this.IdOfContainingObj);
				if (objectRecord.IsRegistered)
				{
					objectRecord.SetMemberValue(manager, this.Member, this.ObjectInstance);
				}
			}
			else if (this.ArrayIndex != null)
			{
				ObjectRecord objectRecord2 = manager.GetObjectRecord(this.IdOfContainingObj);
				if (objectRecord2.IsRegistered)
				{
					objectRecord2.SetArrayValue(manager, this.ObjectInstance, this.ArrayIndex);
				}
			}
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00057304 File Offset: 0x00055504
		public void SetArrayValue(ObjectManager manager, object value, int[] indices)
		{
			((Array)this.ObjectInstance).SetValue(value, indices);
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00057318 File Offset: 0x00055518
		public void SetMemberValue(ObjectManager manager, string memberName, object value)
		{
			if (this.Info == null)
			{
				throw new SerializationException("Cannot perform fixup");
			}
			this.Info.AddValue(memberName, value, value.GetType());
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x00057344 File Offset: 0x00055544
		public bool IsInstanceReady
		{
			get
			{
				return this.IsRegistered && !this.IsUnsolvedObjectReference && (!this.ObjectInstance.GetType().IsValueType || (!this.HasPendingFixups && this.Info == null));
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x0005739C File Offset: 0x0005559C
		public bool IsUnsolvedObjectReference
		{
			get
			{
				return this.Status != ObjectRecordStatus.ReferenceSolved;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x000573AC File Offset: 0x000555AC
		public bool IsRegistered
		{
			get
			{
				return this.Status != ObjectRecordStatus.Unregistered;
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x000573BC File Offset: 0x000555BC
		public bool DoFixups(bool asContainer, ObjectManager manager, bool strict)
		{
			BaseFixupRecord prevFixup = null;
			BaseFixupRecord baseFixupRecord = (!asContainer) ? this.FixupChainAsRequired : this.FixupChainAsContainer;
			bool result = true;
			while (baseFixupRecord != null)
			{
				if (baseFixupRecord.DoFixup(manager, strict))
				{
					this.UnchainFixup(baseFixupRecord, prevFixup, asContainer);
					if (asContainer)
					{
						baseFixupRecord.ObjectRequired.RemoveFixup(baseFixupRecord, false);
					}
					else
					{
						baseFixupRecord.ObjectToBeFixed.RemoveFixup(baseFixupRecord, true);
					}
				}
				else
				{
					prevFixup = baseFixupRecord;
					result = false;
				}
				baseFixupRecord = ((!asContainer) ? baseFixupRecord.NextSameRequired : baseFixupRecord.NextSameContainer);
			}
			return result;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00057450 File Offset: 0x00055650
		public void RemoveFixup(BaseFixupRecord fixupToRemove, bool asContainer)
		{
			BaseFixupRecord prevFixup = null;
			for (BaseFixupRecord baseFixupRecord = (!asContainer) ? this.FixupChainAsRequired : this.FixupChainAsContainer; baseFixupRecord != null; baseFixupRecord = ((!asContainer) ? baseFixupRecord.NextSameRequired : baseFixupRecord.NextSameContainer))
			{
				if (baseFixupRecord == fixupToRemove)
				{
					this.UnchainFixup(baseFixupRecord, prevFixup, asContainer);
					return;
				}
				prevFixup = baseFixupRecord;
			}
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x000574B0 File Offset: 0x000556B0
		private void UnchainFixup(BaseFixupRecord fixup, BaseFixupRecord prevFixup, bool asContainer)
		{
			if (prevFixup == null)
			{
				if (asContainer)
				{
					this.FixupChainAsContainer = fixup.NextSameContainer;
				}
				else
				{
					this.FixupChainAsRequired = fixup.NextSameRequired;
				}
			}
			else if (asContainer)
			{
				prevFixup.NextSameContainer = fixup.NextSameContainer;
			}
			else
			{
				prevFixup.NextSameRequired = fixup.NextSameRequired;
			}
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00057510 File Offset: 0x00055710
		public void ChainFixup(BaseFixupRecord fixup, bool asContainer)
		{
			if (asContainer)
			{
				fixup.NextSameContainer = this.FixupChainAsContainer;
				this.FixupChainAsContainer = fixup;
			}
			else
			{
				fixup.NextSameRequired = this.FixupChainAsRequired;
				this.FixupChainAsRequired = fixup;
			}
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00057544 File Offset: 0x00055744
		public bool LoadData(ObjectManager manager, ISurrogateSelector selector, StreamingContext context)
		{
			if (this.Info != null)
			{
				if (this.Surrogate != null)
				{
					object obj = this.Surrogate.SetObjectData(this.ObjectInstance, this.Info, context, this.SurrogateSelector);
					if (obj != null)
					{
						this.ObjectInstance = obj;
					}
					this.Status = ObjectRecordStatus.ReferenceSolved;
				}
				else
				{
					if (!(this.ObjectInstance is ISerializable))
					{
						throw new SerializationException("No surrogate selector was found for type " + this.ObjectInstance.GetType().FullName);
					}
					object[] parameters = new object[]
					{
						this.Info,
						context
					};
					ConstructorInfo constructor = this.ObjectInstance.GetType().GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(SerializationInfo),
						typeof(StreamingContext)
					}, null);
					if (constructor == null)
					{
						throw new SerializationException("The constructor to deserialize an object of type " + this.ObjectInstance.GetType().FullName + " was not found.");
					}
					constructor.Invoke(this.ObjectInstance, parameters);
				}
				this.Info = null;
			}
			if (this.ObjectInstance is IObjectReference && this.Status != ObjectRecordStatus.ReferenceSolved)
			{
				try
				{
					this.ObjectInstance = ((IObjectReference)this.ObjectInstance).GetRealObject(context);
					int num = 100;
					while (this.ObjectInstance is IObjectReference && num > 0)
					{
						object realObject = ((IObjectReference)this.ObjectInstance).GetRealObject(context);
						if (realObject == this.ObjectInstance)
						{
							break;
						}
						this.ObjectInstance = realObject;
						num--;
					}
					if (num == 0)
					{
						throw new SerializationException("The implementation of the IObjectReference interface returns too many nested references to other objects that implement IObjectReference.");
					}
					this.Status = ObjectRecordStatus.ReferenceSolved;
				}
				catch (NullReferenceException)
				{
					return false;
				}
			}
			if (this.Member != null)
			{
				ObjectRecord objectRecord = manager.GetObjectRecord(this.IdOfContainingObj);
				objectRecord.SetMemberValue(manager, this.Member, this.ObjectInstance);
			}
			else if (this.ArrayIndex != null)
			{
				ObjectRecord objectRecord2 = manager.GetObjectRecord(this.IdOfContainingObj);
				objectRecord2.SetArrayValue(manager, this.ObjectInstance, this.ArrayIndex);
			}
			return true;
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x00057784 File Offset: 0x00055984
		public bool HasPendingFixups
		{
			get
			{
				return this.FixupChainAsContainer != null;
			}
		}

		// Token: 0x04000C69 RID: 3177
		public ObjectRecordStatus Status;

		// Token: 0x04000C6A RID: 3178
		public object OriginalObject;

		// Token: 0x04000C6B RID: 3179
		public object ObjectInstance;

		// Token: 0x04000C6C RID: 3180
		public long ObjectID;

		// Token: 0x04000C6D RID: 3181
		public SerializationInfo Info;

		// Token: 0x04000C6E RID: 3182
		public long IdOfContainingObj;

		// Token: 0x04000C6F RID: 3183
		public ISerializationSurrogate Surrogate;

		// Token: 0x04000C70 RID: 3184
		public ISurrogateSelector SurrogateSelector;

		// Token: 0x04000C71 RID: 3185
		public MemberInfo Member;

		// Token: 0x04000C72 RID: 3186
		public int[] ArrayIndex;

		// Token: 0x04000C73 RID: 3187
		public BaseFixupRecord FixupChainAsContainer;

		// Token: 0x04000C74 RID: 3188
		public BaseFixupRecord FixupChainAsRequired;

		// Token: 0x04000C75 RID: 3189
		public ObjectRecord Next;
	}
}
