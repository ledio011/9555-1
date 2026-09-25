using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000F1 RID: 241
	public sealed class Rigidbody : Component
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00014C24 File Offset: 0x00012E24
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x00014C3C File Offset: 0x00012E3C
		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_velocity(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_velocity(ref value);
			}
		}

		// Token: 0x06000956 RID: 2390
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_velocity(out Vector3 value);

		// Token: 0x06000957 RID: 2391
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_velocity(ref Vector3 value);

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00014C48 File Offset: 0x00012E48
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x00014C60 File Offset: 0x00012E60
		public Vector3 angularVelocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_angularVelocity(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_angularVelocity(ref value);
			}
		}

		// Token: 0x0600095A RID: 2394
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_angularVelocity(out Vector3 value);

		// Token: 0x0600095B RID: 2395
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_angularVelocity(ref Vector3 value);

		// Token: 0x1700020F RID: 527
		// (set) Token: 0x0600095C RID: 2396
		public extern float drag { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000210 RID: 528
		// (set) Token: 0x0600095D RID: 2397
		public extern float angularDrag { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600095E RID: 2398
		// (set) Token: 0x0600095F RID: 2399
		public extern float mass { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000960 RID: 2400
		// (set) Token: 0x06000961 RID: 2401
		public extern bool useGravity { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000962 RID: 2402
		// (set) Token: 0x06000963 RID: 2403
		public extern bool isKinematic { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000214 RID: 532
		// (set) Token: 0x06000964 RID: 2404
		public extern bool freezeRotation { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x06000965 RID: 2405 RVA: 0x00014C6C File Offset: 0x00012E6C
		[ExcludeFromDocs]
		public void AddTorque(Vector3 torque)
		{
			ForceMode mode = ForceMode.Force;
			Rigidbody.INTERNAL_CALL_AddTorque(this, ref torque, mode);
		}

		// Token: 0x06000966 RID: 2406
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_AddTorque(Rigidbody self, ref Vector3 torque, ForceMode mode);

		// Token: 0x06000967 RID: 2407 RVA: 0x00014C84 File Offset: 0x00012E84
		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector3 force, Vector3 position)
		{
			ForceMode mode = ForceMode.Force;
			Rigidbody.INTERNAL_CALL_AddForceAtPosition(this, ref force, ref position, mode);
		}

		// Token: 0x06000968 RID: 2408
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_AddForceAtPosition(Rigidbody self, ref Vector3 force, ref Vector3 position, ForceMode mode);

		// Token: 0x06000969 RID: 2409 RVA: 0x00014CA0 File Offset: 0x00012EA0
		public Vector3 GetPointVelocity(Vector3 worldPoint)
		{
			return Rigidbody.INTERNAL_CALL_GetPointVelocity(this, ref worldPoint);
		}

		// Token: 0x0600096A RID: 2410
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_GetPointVelocity(Rigidbody self, ref Vector3 worldPoint);

		// Token: 0x17000215 RID: 533
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x00014CAC File Offset: 0x00012EAC
		public Vector3 centerOfMass
		{
			set
			{
				this.INTERNAL_set_centerOfMass(ref value);
			}
		}

		// Token: 0x0600096C RID: 2412
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_centerOfMass(ref Vector3 value);

		// Token: 0x17000216 RID: 534
		// (set) Token: 0x0600096D RID: 2413
		public extern bool detectCollisions { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00014CB8 File Offset: 0x00012EB8
		public Vector3 position
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_position(out result);
				return result;
			}
		}

		// Token: 0x0600096F RID: 2415
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_position(out Vector3 value);

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00014CD0 File Offset: 0x00012ED0
		public Quaternion rotation
		{
			get
			{
				Quaternion result;
				this.INTERNAL_get_rotation(out result);
				return result;
			}
		}

		// Token: 0x06000971 RID: 2417
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_rotation(out Quaternion value);

		// Token: 0x06000972 RID: 2418 RVA: 0x00014CE8 File Offset: 0x00012EE8
		public void MovePosition(Vector3 position)
		{
			Rigidbody.INTERNAL_CALL_MovePosition(this, ref position);
		}

		// Token: 0x06000973 RID: 2419
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_MovePosition(Rigidbody self, ref Vector3 position);

		// Token: 0x06000974 RID: 2420 RVA: 0x00014CF4 File Offset: 0x00012EF4
		public void MoveRotation(Quaternion rot)
		{
			Rigidbody.INTERNAL_CALL_MoveRotation(this, ref rot);
		}

		// Token: 0x06000975 RID: 2421
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_MoveRotation(Rigidbody self, ref Quaternion rot);

		// Token: 0x17000219 RID: 537
		// (set) Token: 0x06000976 RID: 2422
		public extern float maxAngularVelocity { [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}
