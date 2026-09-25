using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200002E RID: 46
	public abstract class UIBehaviour : MonoBehaviour
	{
		// Token: 0x06000119 RID: 281 RVA: 0x0000508C File Offset: 0x0000328C
		protected virtual void Awake()
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005090 File Offset: 0x00003290
		protected virtual void OnEnable()
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005094 File Offset: 0x00003294
		protected virtual void Start()
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005098 File Offset: 0x00003298
		protected virtual void OnDisable()
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000509C File Offset: 0x0000329C
		protected virtual void OnDestroy()
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000050A0 File Offset: 0x000032A0
		public virtual bool IsActive()
		{
			return base.enabled && base.isActiveAndEnabled && base.gameObject.activeInHierarchy;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000050C8 File Offset: 0x000032C8
		protected virtual void OnRectTransformDimensionsChange()
		{
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000050CC File Offset: 0x000032CC
		protected virtual void OnBeforeTransformParentChanged()
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000050D0 File Offset: 0x000032D0
		protected virtual void OnTransformParentChanged()
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000050D4 File Offset: 0x000032D4
		protected virtual void OnDidApplyAnimationProperties()
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000050D8 File Offset: 0x000032D8
		protected virtual void OnCanvasGroupChanged()
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000050DC File Offset: 0x000032DC
		protected virtual void OnCanvasHierarchyChanged()
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000050E0 File Offset: 0x000032E0
		public bool IsDestroyed()
		{
			return this == null;
		}
	}
}
