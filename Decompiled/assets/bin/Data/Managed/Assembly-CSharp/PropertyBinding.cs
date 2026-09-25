using System;
using UnityEngine;

// Token: 0x0200008F RID: 143
[AddComponentMenu("NGUI/Internal/Property Binding")]
[ExecuteInEditMode]
public class PropertyBinding : MonoBehaviour
{
	// Token: 0x06000395 RID: 917 RVA: 0x0001ABA0 File Offset: 0x00018DA0
	private void Start()
	{
		this.UpdateTarget();
		if (this.update == PropertyBinding.UpdateCondition.OnStart)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x0001ABBC File Offset: 0x00018DBC
	private void Update()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x06000397 RID: 919 RVA: 0x0001ABD0 File Offset: 0x00018DD0
	private void LateUpdate()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnLateUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x06000398 RID: 920 RVA: 0x0001ABE4 File Offset: 0x00018DE4
	private void FixedUpdate()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnFixedUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x06000399 RID: 921 RVA: 0x0001ABF8 File Offset: 0x00018DF8
	private void OnValidate()
	{
		if (this.source != null)
		{
			this.source.Reset();
		}
		if (this.target != null)
		{
			this.target.Reset();
		}
	}

	// Token: 0x0600039A RID: 922 RVA: 0x0001AC34 File Offset: 0x00018E34
	[ContextMenu("Update Now")]
	public void UpdateTarget()
	{
		if (this.source != null && this.target != null && this.source.isValid && this.target.isValid)
		{
			if (this.direction == PropertyBinding.Direction.SourceUpdatesTarget)
			{
				this.target.Set(this.source.Get());
			}
			else if (this.direction == PropertyBinding.Direction.TargetUpdatesSource)
			{
				this.source.Set(this.target.Get());
			}
			else if (this.source.GetPropertyType() == this.target.GetPropertyType())
			{
				object obj = this.source.Get();
				if (this.mLastValue == null || !this.mLastValue.Equals(obj))
				{
					this.mLastValue = obj;
					this.target.Set(obj);
				}
				else
				{
					obj = this.target.Get();
					if (!this.mLastValue.Equals(obj))
					{
						this.mLastValue = obj;
						this.source.Set(obj);
					}
				}
			}
		}
	}

	// Token: 0x0400035B RID: 859
	public PropertyReference source;

	// Token: 0x0400035C RID: 860
	public PropertyReference target;

	// Token: 0x0400035D RID: 861
	public PropertyBinding.Direction direction;

	// Token: 0x0400035E RID: 862
	public PropertyBinding.UpdateCondition update = PropertyBinding.UpdateCondition.OnUpdate;

	// Token: 0x0400035F RID: 863
	public bool editMode = true;

	// Token: 0x04000360 RID: 864
	private object mLastValue;

	// Token: 0x02000090 RID: 144
	public enum UpdateCondition
	{
		// Token: 0x04000362 RID: 866
		OnStart,
		// Token: 0x04000363 RID: 867
		OnUpdate,
		// Token: 0x04000364 RID: 868
		OnLateUpdate,
		// Token: 0x04000365 RID: 869
		OnFixedUpdate
	}

	// Token: 0x02000091 RID: 145
	public enum Direction
	{
		// Token: 0x04000367 RID: 871
		SourceUpdatesTarget,
		// Token: 0x04000368 RID: 872
		TargetUpdatesSource,
		// Token: 0x04000369 RID: 873
		BiDirectional
	}
}
