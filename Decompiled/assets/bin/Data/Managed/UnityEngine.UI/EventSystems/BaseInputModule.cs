using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000004 RID: 4
	[RequireComponent(typeof(EventSystem))]
	public abstract class BaseInputModule : UIBehaviour
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020E8 File Offset: 0x000002E8
		protected EventSystem eventSystem
		{
			get
			{
				return this.m_EventSystem;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020F0 File Offset: 0x000002F0
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_EventSystem = base.GetComponent<EventSystem>();
			this.m_EventSystem.UpdateModules();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002110 File Offset: 0x00000310
		protected override void OnDisable()
		{
			this.m_EventSystem.UpdateModules();
			base.OnDisable();
		}

		// Token: 0x0600000F RID: 15
		public abstract void Process();

		// Token: 0x06000010 RID: 16 RVA: 0x00002124 File Offset: 0x00000324
		protected static RaycastResult FindFirstRaycast(List<RaycastResult> candidates)
		{
			for (int i = 0; i < candidates.Count; i++)
			{
				if (!(candidates[i].gameObject == null))
				{
					return candidates[i];
				}
			}
			return default(RaycastResult);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002178 File Offset: 0x00000378
		protected static MoveDirection DetermineMoveDirection(float x, float y)
		{
			return BaseInputModule.DetermineMoveDirection(x, y, 0.6f);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002188 File Offset: 0x00000388
		protected static MoveDirection DetermineMoveDirection(float x, float y, float deadZone)
		{
			Vector2 vector = new Vector2(x, y);
			if (vector.sqrMagnitude < deadZone * deadZone)
			{
				return MoveDirection.None;
			}
			if (Mathf.Abs(x) > Mathf.Abs(y))
			{
				if (x > 0f)
				{
					return MoveDirection.Right;
				}
				return MoveDirection.Left;
			}
			else
			{
				if (y > 0f)
				{
					return MoveDirection.Up;
				}
				return MoveDirection.Down;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021E0 File Offset: 0x000003E0
		protected static GameObject FindCommonRoot(GameObject g1, GameObject g2)
		{
			if (g1 == null || g2 == null)
			{
				return null;
			}
			Transform transform = g1.transform;
			while (transform != null)
			{
				Transform transform2 = g2.transform;
				while (transform2 != null)
				{
					if (transform == transform2)
					{
						return transform.gameObject;
					}
					transform2 = transform2.parent;
				}
				transform = transform.parent;
			}
			return null;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000225C File Offset: 0x0000045C
		protected void HandlePointerExitAndEnter(PointerEventData currentPointerData, GameObject newEnterTarget)
		{
			if (currentPointerData.pointerEnter == newEnterTarget)
			{
				return;
			}
			GameObject gameObject = BaseInputModule.FindCommonRoot(currentPointerData.pointerEnter, newEnterTarget);
			if (currentPointerData.pointerEnter != null)
			{
				Transform transform = currentPointerData.pointerEnter.transform;
				while (transform != null)
				{
					if (gameObject != null && gameObject.transform == transform)
					{
						break;
					}
					ExecuteEvents.Execute<IPointerExitHandler>(transform.gameObject, currentPointerData, ExecuteEvents.pointerExitHandler);
					transform = transform.parent;
				}
			}
			if (newEnterTarget != null)
			{
				Transform transform2 = newEnterTarget.transform;
				while (transform2 != null && transform2.gameObject != gameObject)
				{
					ExecuteEvents.Execute<IPointerEnterHandler>(transform2.gameObject, currentPointerData, ExecuteEvents.pointerEnterHandler);
					transform2 = transform2.parent;
				}
			}
			currentPointerData.pointerEnter = newEnterTarget;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002348 File Offset: 0x00000548
		protected virtual AxisEventData GetAxisEventData(float x, float y, float moveDeadZone)
		{
			if (this.m_AxisEventData == null)
			{
				this.m_AxisEventData = new AxisEventData(this.eventSystem);
			}
			this.m_AxisEventData.Reset();
			this.m_AxisEventData.moveVector = new Vector2(x, y);
			this.m_AxisEventData.moveDir = BaseInputModule.DetermineMoveDirection(x, y, moveDeadZone);
			return this.m_AxisEventData;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023A8 File Offset: 0x000005A8
		protected virtual BaseEventData GetBaseEventData()
		{
			if (this.m_BaseEventData == null)
			{
				this.m_BaseEventData = new BaseEventData(this.eventSystem);
			}
			this.m_BaseEventData.Reset();
			return this.m_BaseEventData;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023D8 File Offset: 0x000005D8
		public virtual bool IsPointerOverGameObject(int pointerId)
		{
			return false;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023DC File Offset: 0x000005DC
		public virtual bool ShouldActivateModule()
		{
			return base.enabled && base.gameObject.activeInHierarchy;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023F8 File Offset: 0x000005F8
		public virtual void DeactivateModule()
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023FC File Offset: 0x000005FC
		public virtual void ActivateModule()
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002400 File Offset: 0x00000600
		public virtual void UpdateModule()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002404 File Offset: 0x00000604
		public virtual bool IsModuleSupported()
		{
			return true;
		}

		// Token: 0x04000005 RID: 5
		[NonSerialized]
		protected List<RaycastResult> m_RaycastResultCache = new List<RaycastResult>();

		// Token: 0x04000006 RID: 6
		private AxisEventData m_AxisEventData;

		// Token: 0x04000007 RID: 7
		private EventSystem m_EventSystem;

		// Token: 0x04000008 RID: 8
		private BaseEventData m_BaseEventData;
	}
}
