using System;
using UnityEngine;

// Token: 0x02000038 RID: 56
[AddComponentMenu("Camera-Control/Smooth Look At")]
[Serializable]
public class SmoothLookAt : MonoBehaviour
{
	// Token: 0x060000DD RID: 221 RVA: 0x0000A8C4 File Offset: 0x00008AC4
	public SmoothLookAt()
	{
		this.damping = 6f;
		this.smooth = true;
	}

	// Token: 0x060000DE RID: 222 RVA: 0x0000A8E0 File Offset: 0x00008AE0
	public virtual void LateUpdate()
	{
		if (this.target)
		{
			if (this.smooth)
			{
				Quaternion to = Quaternion.LookRotation(this.target.position - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, to, Time.deltaTime * this.damping);
			}
			else
			{
				this.transform.LookAt(this.target);
			}
		}
	}

	// Token: 0x060000DF RID: 223 RVA: 0x0000A968 File Offset: 0x00008B68
	public virtual void Start()
	{
		if (this.rigidbody)
		{
			this.rigidbody.freezeRotation = true;
		}
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x0000A988 File Offset: 0x00008B88
	public virtual void Main()
	{
	}

	// Token: 0x040001D4 RID: 468
	public Transform target;

	// Token: 0x040001D5 RID: 469
	public float damping;

	// Token: 0x040001D6 RID: 470
	public bool smooth;
}
