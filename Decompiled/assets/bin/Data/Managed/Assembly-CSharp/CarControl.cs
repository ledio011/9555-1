using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class CarControl : MonoBehaviour
{
	// Token: 0x17000197 RID: 407
	// (get) Token: 0x060009C2 RID: 2498 RVA: 0x000473DC File Offset: 0x000455DC
	public float InputSteer
	{
		get
		{
			return this.carSteer;
		}
	}

	// Token: 0x17000198 RID: 408
	// (get) Token: 0x060009C3 RID: 2499 RVA: 0x000473E4 File Offset: 0x000455E4
	public bool IsBarking
	{
		get
		{
			return this.brakeKey;
		}
	}

	// Token: 0x17000199 RID: 409
	// (get) Token: 0x060009C4 RID: 2500 RVA: 0x000473EC File Offset: 0x000455EC
	// (set) Token: 0x060009C5 RID: 2501 RVA: 0x000473F4 File Offset: 0x000455F4
	public bool OnTheGroundFlag
	{
		get
		{
			return this.mOnTheGroundFlag;
		}
		set
		{
			this.mOnTheGroundFlag = value;
		}
	}

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00047400 File Offset: 0x00045600
	public float CurSpeed
	{
		get
		{
			return this.speed;
		}
	}

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00047408 File Offset: 0x00045608
	public float CurSteerAngle
	{
		get
		{
			return this.steerAngle;
		}
	}

	// Token: 0x1700019C RID: 412
	// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00047410 File Offset: 0x00045610
	// (set) Token: 0x060009C9 RID: 2505 RVA: 0x00047418 File Offset: 0x00045618
	public float MaxSpeed
	{
		get
		{
			return this.maxSpeed;
		}
		set
		{
			this.maxSpeed = value;
		}
	}

	// Token: 0x1700019D RID: 413
	// (get) Token: 0x060009CA RID: 2506 RVA: 0x00047424 File Offset: 0x00045624
	public float CurMaxSteerAngle
	{
		get
		{
			return this.steerCurve.Evaluate(Mathf.Abs(this.speed) / this.maxSpeed) * this.maxSteerAngle;
		}
	}

	// Token: 0x1700019E RID: 414
	// (get) Token: 0x060009CB RID: 2507 RVA: 0x00047458 File Offset: 0x00045658
	public float SignCurSteerPercent
	{
		get
		{
			return this.steerAngle / this.CurMaxSteerAngle;
		}
	}

	// Token: 0x1700019F RID: 415
	// (get) Token: 0x060009CC RID: 2508 RVA: 0x00047468 File Offset: 0x00045668
	public float CurSteerPercent
	{
		get
		{
			return Mathf.Abs(this.steerAngle) / this.CurMaxSteerAngle;
		}
	}

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x060009CD RID: 2509 RVA: 0x0004747C File Offset: 0x0004567C
	public float CurSpeedPercent
	{
		get
		{
			return this.speed / this.maxSpeed;
		}
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x0004748C File Offset: 0x0004568C
	private void Awake()
	{
		this.mIsUsingGravity = (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.StreetRacingMode == 0);
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsTutorialScene())
		{
			this.mIsUsingGravity = false;
		}
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x000474CC File Offset: 0x000456CC
	private void OnDisable()
	{
		this.speed = 0f;
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x000474DC File Offset: 0x000456DC
	public void UpdateInput()
	{
		this.brakeKey = false;
		this.accelKey = false;
		this.carSteer = Input.GetAxis("Horizontal");
		this.accelKey = (Input.GetKey(273) || Input.GetKey(119));
		this.brakeKey = (Input.GetKey(274) || Input.GetKey(115));
		this.carNitro = Input.GetKey(32);
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x00047554 File Offset: 0x00045754
	private void Steering()
	{
		this.runningForward = base.transform.forward;
		if (this.carSteer < 1E-45f && this.carSteer > -1E-45f)
		{
			this.steerAngle = 0f;
			Vector3 angularVelocity = base.rigidbody.angularVelocity;
			base.rigidbody.angularVelocity = new Vector3(angularVelocity.x, 0f, angularVelocity.z);
			return;
		}
		float num = this.steerCurve.Evaluate(Mathf.Abs(this.speed) / this.maxSpeed);
		this.steerAngle = num * this.maxSteerAngle * this.carSteer;
		float num2 = this.wheelBase / Mathf.Sin(this.steerAngle * 0.017453292f);
		float num3 = 57.29578f * this.speed / num2;
		Quaternion quaternion = Quaternion.AngleAxis(num3 * Time.deltaTime, base.transform.up);
		this.runningForward = quaternion * base.transform.forward;
		base.rigidbody.angularVelocity = Vector3.up * num3 * Time.deltaTime;
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0004767C File Offset: 0x0004587C
	private void AutoSteer()
	{
		if (this.steerAngle < 1E-45f && this.steerAngle > -1E-45f)
		{
			Vector3 angularVelocity = base.rigidbody.angularVelocity;
			base.rigidbody.angularVelocity = new Vector3(angularVelocity.x, 0f, angularVelocity.z);
			return;
		}
		float num = this.wheelBase / Mathf.Sin(this.steerAngle * 0.017453292f) * 0.5f;
		float num2 = 57.29578f * this.speed / num;
		Quaternion quaternion = Quaternion.AngleAxis(num2 * Time.deltaTime, base.transform.up);
		this.runningForward = quaternion * base.transform.forward;
		base.rigidbody.angularVelocity = Vector3.up * num2 * Time.deltaTime;
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x00047758 File Offset: 0x00045958
	private void Steering2()
	{
		this.runningForward = base.transform.forward;
		if (this.carSteer < 1E-45f && this.carSteer > -1E-45f)
		{
			this.steerAngle = 0f;
			return;
		}
		float num = this.steerCurve.Evaluate(this.speed / this.maxSpeed);
		this.steerAngle = num * this.maxSteerAngle * this.carSteer;
		float num2 = this.wheelBase / Mathf.Sin(this.steerAngle * 0.017453292f);
		float num3 = 57.29578f * this.speed / num2;
		Quaternion quaternion = Quaternion.AngleAxis(num3 * Time.deltaTime, base.transform.up);
		base.transform.forward = quaternion * base.transform.forward;
		this.runningForward = base.transform.forward;
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x00047840 File Offset: 0x00045A40
	private void Driving()
	{
		if (this.brakeKey)
		{
			if (this.speed > 1E-45f)
			{
				this.acceleration = -this.brakeAcceleration;
			}
			else if (this.speed < this.backwardMaxSpeed)
			{
				this.acceleration = -this.topAcceleration;
			}
			else
			{
				this.acceleration = this.backwardAcceleration;
			}
		}
		else if (this.accelKey)
		{
			if (this.speed > 1E-45f)
			{
				if (this.speed > this.maxSpeed)
				{
					this.acceleration = this.topAcceleration;
				}
				else
				{
					this.acceleration = this.maxAcceleration * this.torqueCurve.Evaluate(this.speed / this.maxSpeed);
				}
			}
			else
			{
				this.acceleration = this.brakeAcceleration;
			}
		}
		else
		{
			this.acceleration = 0f;
		}
		float num = this.speed + this.acceleration * Time.deltaTime;
		if (num > this.maxSpeed * 1.05f)
		{
			num = this.maxSpeed * 1.05f;
		}
		Vector3 vector = this.runningForward * num;
		base.rigidbody.velocity = new Vector3(vector.x, base.rigidbody.velocity.y, vector.z);
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x000479A4 File Offset: 0x00045BA4
	private void Update()
	{
		if (!this.IsAutoDrive)
		{
			if (this.mIsUsingGravity)
			{
				this.carSteer = Mathf.Clamp(-Input.acceleration.y * 2f, -1f, 1f);
			}
			else if (this.leftPress)
			{
				this.steerBtnPressTime += Time.deltaTime;
				this.carSteer = -this.steerBtnPressCurve.Evaluate(this.steerBtnPressTime);
			}
			else if (this.rightPress)
			{
				this.steerBtnPressTime += Time.deltaTime;
				this.carSteer = this.steerBtnPressCurve.Evaluate(this.steerBtnPressTime);
			}
			else
			{
				this.carSteer = 0f;
			}
		}
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x00047A74 File Offset: 0x00045C74
	private void FixedUpdate()
	{
		this.speed = base.rigidbody.velocity.magnitude;
		if (base.transform.InverseTransformDirection(base.rigidbody.velocity).z < 0f)
		{
			this.speed = -this.speed;
		}
		if (!this.OnTheGroundFlag)
		{
			return;
		}
		if (this.IsAutoDrive)
		{
			this.AutoSteer();
		}
		else
		{
			this.Steering();
		}
		this.Driving();
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x00047B00 File Offset: 0x00045D00
	public void OnPressAccelBtn(bool isPress)
	{
		this.accelKey = isPress;
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x00047B0C File Offset: 0x00045D0C
	public void OnPressBrakeBtn(bool isPress)
	{
		this.brakeKey = isPress;
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x00047B18 File Offset: 0x00045D18
	public void OnPressLeftBtn(bool isPress)
	{
		this.leftPress = isPress;
		this.steerBtnPressTime = 0f;
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x00047B2C File Offset: 0x00045D2C
	public void OnPressRightBtn(bool isPress)
	{
		this.rightPress = isPress;
		this.steerBtnPressTime = 0f;
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x00047B40 File Offset: 0x00045D40
	public void SetCarTargetAngle(float angle)
	{
		this.steerAngle = angle;
		if (this.steerAngle > this.CurMaxSteerAngle)
		{
			this.steerAngle = this.CurMaxSteerAngle;
		}
		else if (this.steerAngle < -this.CurMaxSteerAngle)
		{
			this.steerAngle = -this.CurMaxSteerAngle;
		}
	}

	// Token: 0x0400088C RID: 2188
	public bool IsAutoDrive;

	// Token: 0x0400088D RID: 2189
	public AnimationCurve torqueCurve;

	// Token: 0x0400088E RID: 2190
	public AnimationCurve steerCurve;

	// Token: 0x0400088F RID: 2191
	public AnimationCurve steerBtnPressCurve;

	// Token: 0x04000890 RID: 2192
	private float steerBtnPressTime;

	// Token: 0x04000891 RID: 2193
	public float maxSpeed = 40f;

	// Token: 0x04000892 RID: 2194
	public float maxSteerAngle = 10f;

	// Token: 0x04000893 RID: 2195
	public float maxAcceleration = 10f;

	// Token: 0x04000894 RID: 2196
	public float brakeAcceleration = 40f;

	// Token: 0x04000895 RID: 2197
	private float topAcceleration = -2f;

	// Token: 0x04000896 RID: 2198
	private float backwardAcceleration = -2f;

	// Token: 0x04000897 RID: 2199
	private float backwardMaxSpeed = -5f;

	// Token: 0x04000898 RID: 2200
	private bool brakeKey;

	// Token: 0x04000899 RID: 2201
	private bool accelKey;

	// Token: 0x0400089A RID: 2202
	private float carSteer;

	// Token: 0x0400089B RID: 2203
	private bool carNitro;

	// Token: 0x0400089C RID: 2204
	private float steerAngle;

	// Token: 0x0400089D RID: 2205
	private float speed;

	// Token: 0x0400089E RID: 2206
	private float wheelBase = 3f;

	// Token: 0x0400089F RID: 2207
	private Vector3 runningForward;

	// Token: 0x040008A0 RID: 2208
	private float acceleration;

	// Token: 0x040008A1 RID: 2209
	private bool mIsUsingGravity;

	// Token: 0x040008A2 RID: 2210
	private bool leftPress;

	// Token: 0x040008A3 RID: 2211
	private bool rightPress;

	// Token: 0x040008A4 RID: 2212
	private bool mOnTheGroundFlag;
}
