using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
[RequireComponent(typeof(CharacterMotor))]
[AddComponentMenu("Character/Platform Input Controller")]
[Serializable]
public class PlatformInputController : MonoBehaviour
{
	// Token: 0x06000026 RID: 38 RVA: 0x0000345C File Offset: 0x0000165C
	public PlatformInputController()
	{
		this.autoRotate = true;
		this.maxRotationSpeed = (float)360;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00003478 File Offset: 0x00001678
	public virtual void Awake()
	{
		this.motor = (CharacterMotor)this.GetComponent(typeof(CharacterMotor));
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00003498 File Offset: 0x00001698
	public virtual void Update()
	{
		Vector3 vector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), (float)0);
		if (vector != Vector3.zero)
		{
			float num = vector.magnitude;
			vector /= num;
			num = Mathf.Min((float)1, num);
			num *= num;
			vector *= num;
		}
		vector = Camera.main.transform.rotation * vector;
		Quaternion rotation = Quaternion.FromToRotation(-Camera.main.transform.forward, this.transform.up);
		vector = rotation * vector;
		this.motor.inputMoveDirection = vector;
		this.motor.inputJump = Input.GetButton("Jump");
		if (this.autoRotate && vector.sqrMagnitude > 0.01f)
		{
			Vector3 vector2 = this.ConstantSlerp(this.transform.forward, vector, this.maxRotationSpeed * Time.deltaTime);
			vector2 = this.ProjectOntoPlane(vector2, this.transform.up);
			this.transform.rotation = Quaternion.LookRotation(vector2, this.transform.up);
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000035C4 File Offset: 0x000017C4
	public virtual Vector3 ProjectOntoPlane(Vector3 v, Vector3 normal)
	{
		return v - Vector3.Project(v, normal);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x000035D4 File Offset: 0x000017D4
	public virtual Vector3 ConstantSlerp(Vector3 from, Vector3 to, float angle)
	{
		float t = Mathf.Min((float)1, angle / Vector3.Angle(from, to));
		return Vector3.Slerp(from, to, t);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000035FC File Offset: 0x000017FC
	public virtual void Main()
	{
	}

	// Token: 0x0400003D RID: 61
	public bool autoRotate;

	// Token: 0x0400003E RID: 62
	public float maxRotationSpeed;

	// Token: 0x0400003F RID: 63
	private CharacterMotor motor;
}
