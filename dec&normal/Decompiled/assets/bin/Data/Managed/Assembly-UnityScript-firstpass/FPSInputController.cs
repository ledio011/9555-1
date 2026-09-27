using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
[AddComponentMenu("Character/FPS Input Controller")]
[RequireComponent(typeof(CharacterMotor))]
[Serializable]
public class FPSInputController : MonoBehaviour
{
	// Token: 0x06000023 RID: 35 RVA: 0x000033A8 File Offset: 0x000015A8
	public virtual void Awake()
	{
		this.motor = (CharacterMotor)this.GetComponent(typeof(CharacterMotor));
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000033C8 File Offset: 0x000015C8
	public virtual void Update()
	{
		Vector3 vector = new Vector3(Input.GetAxis("Horizontal"), (float)0, Input.GetAxis("Vertical"));
		if (vector != Vector3.zero)
		{
			float num = vector.magnitude;
			vector /= num;
			num = Mathf.Min((float)1, num);
			num *= num;
			vector *= num;
		}
		this.motor.inputMoveDirection = this.transform.rotation * vector;
		this.motor.inputJump = Input.GetButton("Jump");
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00003458 File Offset: 0x00001658
	public virtual void Main()
	{
	}

	// Token: 0x0400003C RID: 60
	private CharacterMotor motor;
}
