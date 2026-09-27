using System;
using UnityEngine;

// Token: 0x02000097 RID: 151
[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
	// Token: 0x060003E4 RID: 996 RVA: 0x0001C92C File Offset: 0x0001AB2C
	private void OnSubmit()
	{
		if (this.onSubmit != null)
		{
			this.onSubmit(base.gameObject);
		}
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x0001C94C File Offset: 0x0001AB4C
	private void OnClick()
	{
		if (this.onClick != null)
		{
			this.onClick(base.gameObject);
		}
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x0001C96C File Offset: 0x0001AB6C
	private void OnDoubleClick()
	{
		if (this.onDoubleClick != null)
		{
			this.onDoubleClick(base.gameObject);
		}
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x0001C98C File Offset: 0x0001AB8C
	private void OnHover(bool isOver)
	{
		if (this.onHover != null)
		{
			this.onHover(base.gameObject, isOver);
		}
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x0001C9AC File Offset: 0x0001ABAC
	private void OnPress(bool isPressed)
	{
		if (this.onPress != null)
		{
			this.onPress(base.gameObject, isPressed);
		}
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x0001C9CC File Offset: 0x0001ABCC
	private void OnSelect(bool selected)
	{
		if (this.onSelect != null)
		{
			this.onSelect(base.gameObject, selected);
		}
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x0001C9EC File Offset: 0x0001ABEC
	private void OnScroll(float delta)
	{
		if (this.onScroll != null)
		{
			this.onScroll(base.gameObject, delta);
		}
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x0001CA0C File Offset: 0x0001AC0C
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag != null)
		{
			this.onDrag(base.gameObject, delta);
		}
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x0001CA2C File Offset: 0x0001AC2C
	private void OnDrop(GameObject go)
	{
		if (this.onDrop != null)
		{
			this.onDrop(base.gameObject, go);
		}
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x0001CA4C File Offset: 0x0001AC4C
	private void OnKey(KeyCode key)
	{
		if (this.onKey != null)
		{
			this.onKey(base.gameObject, key);
		}
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x0001CA6C File Offset: 0x0001AC6C
	public static UIEventListener Get(GameObject go)
	{
		UIEventListener uieventListener = go.GetComponent<UIEventListener>();
		if (uieventListener == null)
		{
			uieventListener = go.AddComponent<UIEventListener>();
		}
		return uieventListener;
	}

	// Token: 0x0400039D RID: 925
	public object parameter;

	// Token: 0x0400039E RID: 926
	public UIEventListener.VoidDelegate onSubmit;

	// Token: 0x0400039F RID: 927
	public UIEventListener.VoidDelegate onClick;

	// Token: 0x040003A0 RID: 928
	public UIEventListener.VoidDelegate onDoubleClick;

	// Token: 0x040003A1 RID: 929
	public UIEventListener.BoolDelegate onHover;

	// Token: 0x040003A2 RID: 930
	public UIEventListener.BoolDelegate onPress;

	// Token: 0x040003A3 RID: 931
	public UIEventListener.BoolDelegate onSelect;

	// Token: 0x040003A4 RID: 932
	public UIEventListener.FloatDelegate onScroll;

	// Token: 0x040003A5 RID: 933
	public UIEventListener.VectorDelegate onDrag;

	// Token: 0x040003A6 RID: 934
	public UIEventListener.ObjectDelegate onDrop;

	// Token: 0x040003A7 RID: 935
	public UIEventListener.KeyCodeDelegate onKey;

	// Token: 0x02000A9E RID: 2718
	// (Invoke) Token: 0x06004F01 RID: 20225
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x02000A9F RID: 2719
	// (Invoke) Token: 0x06004F05 RID: 20229
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x02000AA0 RID: 2720
	// (Invoke) Token: 0x06004F09 RID: 20233
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x02000AA1 RID: 2721
	// (Invoke) Token: 0x06004F0D RID: 20237
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x02000AA2 RID: 2722
	// (Invoke) Token: 0x06004F11 RID: 20241
	public delegate void ObjectDelegate(GameObject go, GameObject draggedObject);

	// Token: 0x02000AA3 RID: 2723
	// (Invoke) Token: 0x06004F15 RID: 20245
	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);
}
