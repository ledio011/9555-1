using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
[AddComponentMenu("NGUI/Interaction/Key Navigation")]
public class UIKeyNavigation : MonoBehaviour
{
	// Token: 0x060001A7 RID: 423 RVA: 0x0000B1FC File Offset: 0x000093FC
	protected virtual void OnEnable()
	{
		UIKeyNavigation.list.Add(this);
		if (this.startsSelected && (UICamera.selectedObject == null || !NGUITools.GetActive(UICamera.selectedObject)))
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.selectedObject = base.gameObject;
		}
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x0000B250 File Offset: 0x00009450
	protected virtual void OnDisable()
	{
		UIKeyNavigation.list.Remove(this);
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x0000B260 File Offset: 0x00009460
	protected GameObject GetLeft()
	{
		if (NGUITools.GetActive(this.onLeft))
		{
			return this.onLeft;
		}
		if (this.constraint == UIKeyNavigation.Constraint.Vertical || this.constraint == UIKeyNavigation.Constraint.Explicit)
		{
			return null;
		}
		return this.Get(Vector3.left, true);
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000B2A0 File Offset: 0x000094A0
	private GameObject GetRight()
	{
		if (NGUITools.GetActive(this.onRight))
		{
			return this.onRight;
		}
		if (this.constraint == UIKeyNavigation.Constraint.Vertical || this.constraint == UIKeyNavigation.Constraint.Explicit)
		{
			return null;
		}
		return this.Get(Vector3.right, true);
	}

	// Token: 0x060001AB RID: 427 RVA: 0x0000B2E0 File Offset: 0x000094E0
	protected GameObject GetUp()
	{
		if (NGUITools.GetActive(this.onUp))
		{
			return this.onUp;
		}
		if (this.constraint == UIKeyNavigation.Constraint.Horizontal || this.constraint == UIKeyNavigation.Constraint.Explicit)
		{
			return null;
		}
		return this.Get(Vector3.up, false);
	}

	// Token: 0x060001AC RID: 428 RVA: 0x0000B320 File Offset: 0x00009520
	protected GameObject GetDown()
	{
		if (NGUITools.GetActive(this.onDown))
		{
			return this.onDown;
		}
		if (this.constraint == UIKeyNavigation.Constraint.Horizontal || this.constraint == UIKeyNavigation.Constraint.Explicit)
		{
			return null;
		}
		return this.Get(Vector3.down, false);
	}

	// Token: 0x060001AD RID: 429 RVA: 0x0000B360 File Offset: 0x00009560
	protected GameObject Get(Vector3 myDir, bool horizontal)
	{
		Transform transform = base.transform;
		myDir = transform.TransformDirection(myDir);
		Vector3 center = UIKeyNavigation.GetCenter(base.gameObject);
		float num = float.MaxValue;
		GameObject result = null;
		for (int i = 0; i < UIKeyNavigation.list.size; i++)
		{
			UIKeyNavigation uikeyNavigation = UIKeyNavigation.list[i];
			if (!(uikeyNavigation == this))
			{
				UIButton component = uikeyNavigation.GetComponent<UIButton>();
				if (!(component != null) || component.isEnabled)
				{
					Vector3 vector = UIKeyNavigation.GetCenter(uikeyNavigation.gameObject) - center;
					float num2 = Vector3.Dot(myDir, vector.normalized);
					if (num2 >= 0.707f)
					{
						vector = transform.InverseTransformDirection(vector);
						if (horizontal)
						{
							vector.y *= 2f;
						}
						else
						{
							vector.x *= 2f;
						}
						float sqrMagnitude = vector.sqrMagnitude;
						if (sqrMagnitude <= num)
						{
							result = uikeyNavigation.gameObject;
							num = sqrMagnitude;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060001AE RID: 430 RVA: 0x0000B488 File Offset: 0x00009688
	protected static Vector3 GetCenter(GameObject go)
	{
		UIWidget component = go.GetComponent<UIWidget>();
		if (component != null)
		{
			Vector3[] worldCorners = component.worldCorners;
			return (worldCorners[0] + worldCorners[2]) * 0.5f;
		}
		return go.transform.position;
	}

	// Token: 0x060001AF RID: 431 RVA: 0x0000B4E4 File Offset: 0x000096E4
	protected virtual void OnKey(KeyCode key)
	{
		if (!NGUITools.GetActive(this))
		{
			return;
		}
		GameObject gameObject = null;
		switch (key)
		{
		case 273:
			gameObject = this.GetUp();
			break;
		case 274:
			gameObject = this.GetDown();
			break;
		case 275:
			gameObject = this.GetRight();
			break;
		case 276:
			gameObject = this.GetLeft();
			break;
		default:
			if (key == 9)
			{
				if (Input.GetKey(304) || Input.GetKey(303))
				{
					gameObject = this.GetLeft();
					if (gameObject == null)
					{
						gameObject = this.GetUp();
					}
					if (gameObject == null)
					{
						gameObject = this.GetDown();
					}
					if (gameObject == null)
					{
						gameObject = this.GetRight();
					}
				}
				else
				{
					gameObject = this.GetRight();
					if (gameObject == null)
					{
						gameObject = this.GetDown();
					}
					if (gameObject == null)
					{
						gameObject = this.GetUp();
					}
					if (gameObject == null)
					{
						gameObject = this.GetLeft();
					}
				}
			}
			break;
		}
		if (gameObject != null)
		{
			UICamera.selectedObject = gameObject;
		}
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x0000B614 File Offset: 0x00009814
	protected virtual void OnClick()
	{
		if (NGUITools.GetActive(this) && NGUITools.GetActive(this.onClick))
		{
			UICamera.selectedObject = this.onClick;
		}
	}

	// Token: 0x040001E0 RID: 480
	public static BetterList<UIKeyNavigation> list = new BetterList<UIKeyNavigation>();

	// Token: 0x040001E1 RID: 481
	public UIKeyNavigation.Constraint constraint;

	// Token: 0x040001E2 RID: 482
	public GameObject onUp;

	// Token: 0x040001E3 RID: 483
	public GameObject onDown;

	// Token: 0x040001E4 RID: 484
	public GameObject onLeft;

	// Token: 0x040001E5 RID: 485
	public GameObject onRight;

	// Token: 0x040001E6 RID: 486
	public GameObject onClick;

	// Token: 0x040001E7 RID: 487
	public bool startsSelected;

	// Token: 0x02000060 RID: 96
	public enum Constraint
	{
		// Token: 0x040001E9 RID: 489
		None,
		// Token: 0x040001EA RID: 490
		Vertical,
		// Token: 0x040001EB RID: 491
		Horizontal,
		// Token: 0x040001EC RID: 492
		Explicit
	}
}
