using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B7 RID: 183
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Event System (UICamera)")]
public class UICamera : MonoBehaviour
{
	// Token: 0x170000CB RID: 203
	// (get) Token: 0x06000533 RID: 1331 RVA: 0x00023588 File Offset: 0x00021788
	[Obsolete("Use new OnDragStart / OnDragOver / OnDragOut / OnDragEnd events instead")]
	public bool stickyPress
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x06000534 RID: 1332 RVA: 0x0002358C File Offset: 0x0002178C
	public static Ray currentRay
	{
		get
		{
			return (!(UICamera.currentCamera != null) || UICamera.currentTouch == null) ? default(Ray) : UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
		}
	}

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x06000535 RID: 1333 RVA: 0x000235DC File Offset: 0x000217DC
	private bool handlesEvents
	{
		get
		{
			return UICamera.eventHandler == this;
		}
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x06000536 RID: 1334 RVA: 0x000235EC File Offset: 0x000217EC
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.camera;
			}
			return this.mCam;
		}
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x06000537 RID: 1335 RVA: 0x00023614 File Offset: 0x00021814
	// (set) Token: 0x06000538 RID: 1336 RVA: 0x0002361C File Offset: 0x0002181C
	public static GameObject selectedObject
	{
		get
		{
			return UICamera.mCurrentSelection;
		}
		set
		{
			UICamera.SetSelection(value, UICamera.currentScheme);
		}
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x0002362C File Offset: 0x0002182C
	public static bool IsPressed(GameObject go)
	{
		for (int i = 0; i < 3; i++)
		{
			if (UICamera.mMouse[i].pressed == go)
			{
				return true;
			}
		}
		foreach (KeyValuePair<int, UICamera.MouseOrTouch> keyValuePair in UICamera.mTouches)
		{
			if (keyValuePair.Value.pressed == go)
			{
				return true;
			}
		}
		return UICamera.controller.pressed == go;
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x000236F0 File Offset: 0x000218F0
	protected static void SetSelection(GameObject go, UICamera.ControlScheme scheme)
	{
		if (UICamera.mNextSelection != null)
		{
			UICamera.mNextSelection = go;
		}
		else if (UICamera.mCurrentSelection != go)
		{
			UICamera.mNextSelection = go;
			UICamera.mNextScheme = scheme;
			if (UICamera.list.size > 0)
			{
				UICamera uicamera = (!(UICamera.mNextSelection != null)) ? UICamera.list[0] : UICamera.FindCameraForLayer(UICamera.mNextSelection.layer);
				if (uicamera != null)
				{
					uicamera.StartCoroutine(uicamera.ChangeSelection());
				}
			}
		}
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00023790 File Offset: 0x00021990
	private IEnumerator ChangeSelection()
	{
		yield return new WaitForEndOfFrame();
		UICamera.Notify(UICamera.mCurrentSelection, "OnSelect", false);
		UICamera.mCurrentSelection = UICamera.mNextSelection;
		UICamera.mNextSelection = null;
		if (UICamera.mCurrentSelection != null)
		{
			UICamera.current = this;
			UICamera.currentCamera = this.mCam;
			UICamera.currentScheme = UICamera.mNextScheme;
			UICamera.inputHasFocus = (UICamera.mCurrentSelection.GetComponent<UIInput>() != null);
			UICamera.Notify(UICamera.mCurrentSelection, "OnSelect", true);
			UICamera.current = null;
		}
		else
		{
			UICamera.inputHasFocus = false;
		}
		yield break;
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x0600053C RID: 1340 RVA: 0x000237AC File Offset: 0x000219AC
	public static int touchCount
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<int, UICamera.MouseOrTouch> keyValuePair in UICamera.mTouches)
			{
				if (keyValuePair.Value.pressed != null)
				{
					num++;
				}
			}
			for (int i = 0; i < UICamera.mMouse.Length; i++)
			{
				if (UICamera.mMouse[i].pressed != null)
				{
					num++;
				}
			}
			if (UICamera.controller.pressed != null)
			{
				num++;
			}
			return num;
		}
	}

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x0600053D RID: 1341 RVA: 0x00023874 File Offset: 0x00021A74
	public static int dragCount
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<int, UICamera.MouseOrTouch> keyValuePair in UICamera.mTouches)
			{
				if (keyValuePair.Value.dragged != null)
				{
					num++;
				}
			}
			for (int i = 0; i < UICamera.mMouse.Length; i++)
			{
				if (UICamera.mMouse[i].dragged != null)
				{
					num++;
				}
			}
			if (UICamera.controller.dragged != null)
			{
				num++;
			}
			return num;
		}
	}

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x0600053E RID: 1342 RVA: 0x0002393C File Offset: 0x00021B3C
	public static Camera mainCamera
	{
		get
		{
			UICamera eventHandler = UICamera.eventHandler;
			return (!(eventHandler != null)) ? null : eventHandler.cachedCamera;
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x0600053F RID: 1343 RVA: 0x00023968 File Offset: 0x00021B68
	public static UICamera eventHandler
	{
		get
		{
			for (int i = 0; i < UICamera.list.size; i++)
			{
				UICamera uicamera = UICamera.list.buffer[i];
				if (!(uicamera == null) && uicamera.enabled && NGUITools.GetActive(uicamera.gameObject))
				{
					return uicamera;
				}
			}
			return null;
		}
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x000239CC File Offset: 0x00021BCC
	private static int CompareFunc(UICamera a, UICamera b)
	{
		if (a.cachedCamera.depth < b.cachedCamera.depth)
		{
			return 1;
		}
		if (a.cachedCamera.depth > b.cachedCamera.depth)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00023A14 File Offset: 0x00021C14
	public static bool Raycast(Vector3 inPos, out RaycastHit hit)
	{
		for (int i = 0; i < UICamera.list.size; i++)
		{
			UICamera uicamera = UICamera.list.buffer[i];
			if (uicamera.enabled && NGUITools.GetActive(uicamera.gameObject))
			{
				UICamera.currentCamera = uicamera.cachedCamera;
				Vector3 point = UICamera.currentCamera.ScreenToViewportPoint(inPos);
				if (!float.IsNaN(point.x) && !float.IsNaN(point.y))
				{
					if (point.x >= 0f && point.x <= 1f && point.y >= 0f && point.y <= 1f)
					{
						Ray ray = UICamera.currentCamera.ScreenPointToRay(inPos);
						int num = UICamera.currentCamera.cullingMask & uicamera.eventReceiverMask;
						float num2 = (uicamera.rangeDistance <= 0f) ? (UICamera.currentCamera.farClipPlane - UICamera.currentCamera.nearClipPlane) : uicamera.rangeDistance;
						if (uicamera.eventType == UICamera.EventType.World)
						{
							if (Physics.Raycast(ray, ref hit, num2, num))
							{
								UICamera.hoveredObject = hit.collider.gameObject;
								return true;
							}
						}
						else if (uicamera.eventType == UICamera.EventType.UI)
						{
							RaycastHit[] array = Physics.RaycastAll(ray, num2, num);
							if (array.Length > 1)
							{
								int j = 0;
								while (j < array.Length)
								{
									GameObject gameObject = array[j].collider.gameObject;
									UIWidget component = gameObject.GetComponent<UIWidget>();
									if (component != null)
									{
										if (component.isVisible)
										{
											if (component.hitCheck == null || component.hitCheck(array[j].point))
											{
												goto IL_20A;
											}
										}
									}
									else
									{
										UIRect uirect = NGUITools.FindInParents<UIRect>(gameObject);
										if (!(uirect != null) || uirect.finalAlpha >= 0.001f)
										{
											goto IL_20A;
										}
									}
									IL_256:
									j++;
									continue;
									IL_20A:
									UICamera.mHit.depth = NGUITools.CalculateRaycastDepth(gameObject);
									if (UICamera.mHit.depth != 2147483647)
									{
										UICamera.mHit.hit = array[j];
										UICamera.mHits.Add(UICamera.mHit);
										goto IL_256;
									}
									goto IL_256;
								}
								UICamera.mHits.Sort((UICamera.DepthEntry r1, UICamera.DepthEntry r2) => r2.depth.CompareTo(r1.depth));
								for (int k = 0; k < UICamera.mHits.size; k++)
								{
									if (UICamera.IsVisible(ref UICamera.mHits.buffer[k]))
									{
										hit = UICamera.mHits[k].hit;
										UICamera.hoveredObject = hit.collider.gameObject;
										UICamera.mHits.Clear();
										return true;
									}
								}
								UICamera.mHits.Clear();
							}
							else if (array.Length == 1)
							{
								Collider collider = array[0].collider;
								UIWidget component2 = collider.GetComponent<UIWidget>();
								if (component2 != null)
								{
									if (!component2.isVisible)
									{
										goto IL_455;
									}
									if (component2.hitCheck != null && !component2.hitCheck(array[0].point))
									{
										goto IL_455;
									}
								}
								else
								{
									UIRect uirect2 = NGUITools.FindInParents<UIRect>(collider.gameObject);
									if (uirect2 != null && uirect2.finalAlpha < 0.001f)
									{
										goto IL_455;
									}
								}
								if (UICamera.IsVisible(ref array[0]))
								{
									hit = array[0];
									UICamera.hoveredObject = hit.collider.gameObject;
									return true;
								}
							}
						}
						else if (uicamera.eventType == UICamera.EventType.Unity2D)
						{
							if (UICamera.m2DPlane.Raycast(ray, ref num2))
							{
								Collider2D collider2D = Physics2D.OverlapPoint(ray.GetPoint(num2), num);
								if (collider2D)
								{
									hit = UICamera.lastHit;
									hit.point = point;
									UICamera.hoveredObject = collider2D.gameObject;
									return true;
								}
							}
						}
					}
				}
			}
			IL_455:;
		}
		hit = UICamera.mEmpty;
		return false;
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00023E98 File Offset: 0x00022098
	private static bool IsVisible(ref RaycastHit hit)
	{
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(hit.collider.gameObject);
		while (uipanel != null)
		{
			if (!uipanel.IsVisible(hit.point))
			{
				return false;
			}
			uipanel = uipanel.parentPanel;
		}
		return true;
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00023EE4 File Offset: 0x000220E4
	private static bool IsVisible(ref UICamera.DepthEntry de)
	{
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(de.hit.collider.gameObject);
		while (uipanel != null)
		{
			if (!uipanel.IsVisible(de.hit.point))
			{
				return false;
			}
			uipanel = uipanel.parentPanel;
		}
		return true;
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00023F38 File Offset: 0x00022138
	public static bool IsHighlighted(GameObject go)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Mouse)
		{
			return UICamera.hoveredObject == go;
		}
		return UICamera.currentScheme == UICamera.ControlScheme.Controller && UICamera.selectedObject == go;
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00023F74 File Offset: 0x00022174
	public static UICamera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		for (int i = 0; i < UICamera.list.size; i++)
		{
			UICamera uicamera = UICamera.list.buffer[i];
			Camera cachedCamera = uicamera.cachedCamera;
			if (cachedCamera != null && (cachedCamera.cullingMask & num) != 0)
			{
				return uicamera;
			}
		}
		return null;
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00023FD4 File Offset: 0x000221D4
	private static int GetDirection(KeyCode up, KeyCode down)
	{
		if (Input.GetKeyDown(up))
		{
			return 1;
		}
		if (Input.GetKeyDown(down))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00023FF4 File Offset: 0x000221F4
	private static int GetDirection(KeyCode up0, KeyCode up1, KeyCode down0, KeyCode down1)
	{
		if (Input.GetKeyDown(up0) || Input.GetKeyDown(up1))
		{
			return 1;
		}
		if (Input.GetKeyDown(down0) || Input.GetKeyDown(down1))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00024028 File Offset: 0x00022228
	private static int GetDirection(string axis)
	{
		float time = RealTime.time;
		if (UICamera.mNextEvent < time && !string.IsNullOrEmpty(axis))
		{
			float axis2 = Input.GetAxis(axis);
			if (axis2 > 0.75f)
			{
				UICamera.mNextEvent = time + 0.25f;
				return 1;
			}
			if (axis2 < -0.75f)
			{
				UICamera.mNextEvent = time + 0.25f;
				return -1;
			}
		}
		return 0;
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x0002408C File Offset: 0x0002228C
	public static void Notify(GameObject go, string funcName, object obj)
	{
		if (UICamera.mNotifying)
		{
			return;
		}
		UICamera.mNotifying = true;
		if (NGUITools.GetActive(go))
		{
			go.SendMessage(funcName, obj, 1);
			if (UICamera.genericEventHandler != null && UICamera.genericEventHandler != go)
			{
				UICamera.genericEventHandler.SendMessage(funcName, obj, 1);
			}
		}
		UICamera.mNotifying = false;
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x000240F4 File Offset: 0x000222F4
	public static UICamera.MouseOrTouch GetMouse(int button)
	{
		return UICamera.mMouse[button];
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00024100 File Offset: 0x00022300
	public static UICamera.MouseOrTouch GetTouch(int id)
	{
		UICamera.MouseOrTouch mouseOrTouch = null;
		if (id < 0)
		{
			return UICamera.GetMouse(-id - 1);
		}
		if (!UICamera.mTouches.TryGetValue(id, ref mouseOrTouch))
		{
			mouseOrTouch = new UICamera.MouseOrTouch();
			mouseOrTouch.touchBegan = true;
			UICamera.mTouches.Add(id, mouseOrTouch);
		}
		return mouseOrTouch;
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x0002414C File Offset: 0x0002234C
	public static void RemoveTouch(int id)
	{
		UICamera.mTouches.Remove(id);
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x0002415C File Offset: 0x0002235C
	private void Awake()
	{
		UICamera.mWidth = Screen.width;
		UICamera.mHeight = Screen.height;
		if (Application.platform == 11 || Application.platform == 8 || Application.platform == 21 || Application.platform == 22)
		{
			this.useMouse = false;
			this.useTouch = true;
			if (Application.platform == 8)
			{
				this.useKeyboard = false;
				this.useController = false;
			}
		}
		else if (Application.platform == 9 || Application.platform == 10)
		{
			this.useMouse = false;
			this.useTouch = false;
			this.useKeyboard = false;
			this.useController = true;
		}
		UICamera.mMouse[0].pos.x = Input.mousePosition.x;
		UICamera.mMouse[0].pos.y = Input.mousePosition.y;
		for (int i = 1; i < 3; i++)
		{
			UICamera.mMouse[i].pos = UICamera.mMouse[0].pos;
			UICamera.mMouse[i].lastPos = UICamera.mMouse[0].pos;
		}
		UICamera.lastTouchPosition = UICamera.mMouse[0].pos;
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0002429C File Offset: 0x0002249C
	private void OnEnable()
	{
		UICamera.list.Add(this);
		UICamera.list.Sort(new BetterList<UICamera>.CompareFunc(UICamera.CompareFunc));
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x000242C0 File Offset: 0x000224C0
	private void OnDisable()
	{
		UICamera.list.Remove(this);
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x000242D0 File Offset: 0x000224D0
	private void Start()
	{
		if (this.eventType != UICamera.EventType.World && this.cachedCamera.transparencySortMode != 2)
		{
			this.cachedCamera.transparencySortMode = 2;
		}
		if (Application.isPlaying)
		{
			this.cachedCamera.eventMask = 0;
		}
		if (this.handlesEvents)
		{
			NGUIDebug.debugRaycast = this.debug;
		}
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x00024334 File Offset: 0x00022534
	private void Update()
	{
		if (!this.handlesEvents)
		{
			return;
		}
		UICamera.current = this;
		if (this.useTouch)
		{
			this.ProcessTouches();
		}
		else if (this.useMouse)
		{
			this.ProcessMouse();
		}
		if (UICamera.onCustomInput != null)
		{
			UICamera.onCustomInput();
		}
		if (this.useMouse && UICamera.mCurrentSelection != null)
		{
			if (this.cancelKey0 != null && Input.GetKeyDown(this.cancelKey0))
			{
				UICamera.currentScheme = UICamera.ControlScheme.Controller;
				UICamera.currentKey = this.cancelKey0;
				UICamera.selectedObject = null;
			}
			else if (this.cancelKey1 != null && Input.GetKeyDown(this.cancelKey1))
			{
				UICamera.currentScheme = UICamera.ControlScheme.Controller;
				UICamera.currentKey = this.cancelKey1;
				UICamera.selectedObject = null;
			}
		}
		if (UICamera.mCurrentSelection == null)
		{
			UICamera.inputHasFocus = false;
		}
		if (UICamera.mCurrentSelection != null)
		{
			this.ProcessOthers();
		}
		if (this.useMouse && UICamera.mHover != null)
		{
			float num = string.IsNullOrEmpty(this.scrollAxisName) ? 0f : Input.GetAxis(this.scrollAxisName);
			if (num != 0f)
			{
				UICamera.Notify(UICamera.mHover, "OnScroll", num);
			}
			if (UICamera.showTooltips && this.mTooltipTime != 0f && (this.mTooltipTime < RealTime.time || Input.GetKey(304) || Input.GetKey(303)))
			{
				this.mTooltip = UICamera.mHover;
				this.ShowTooltip(true);
			}
		}
		UICamera.current = null;
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x00024500 File Offset: 0x00022700
	private void LateUpdate()
	{
		if (!this.handlesEvents)
		{
			return;
		}
		int width = Screen.width;
		int height = Screen.height;
		if (width != UICamera.mWidth || height != UICamera.mHeight)
		{
			UICamera.mWidth = width;
			UICamera.mHeight = height;
			UIRoot.Broadcast("UpdateAnchors");
			if (UICamera.onScreenResize != null)
			{
				UICamera.onScreenResize();
			}
		}
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x00024568 File Offset: 0x00022768
	public void ProcessMouse()
	{
		UICamera.lastTouchPosition = Input.mousePosition;
		UICamera.mMouse[0].delta = UICamera.lastTouchPosition - UICamera.mMouse[0].pos;
		UICamera.mMouse[0].pos = UICamera.lastTouchPosition;
		bool flag = UICamera.mMouse[0].delta.sqrMagnitude > 0.001f;
		for (int i = 1; i < 3; i++)
		{
			UICamera.mMouse[i].pos = UICamera.mMouse[0].pos;
			UICamera.mMouse[i].delta = UICamera.mMouse[0].delta;
		}
		bool flag2 = false;
		bool flag3 = false;
		for (int j = 0; j < 3; j++)
		{
			if (Input.GetMouseButtonDown(j))
			{
				UICamera.currentScheme = UICamera.ControlScheme.Mouse;
				flag3 = true;
				flag2 = true;
			}
			else if (Input.GetMouseButton(j))
			{
				UICamera.currentScheme = UICamera.ControlScheme.Mouse;
				flag2 = true;
			}
		}
		if (flag2 || flag || this.mNextRaycast < RealTime.time)
		{
			this.mNextRaycast = RealTime.time + 0.02f;
			if (!UICamera.Raycast(Input.mousePosition, out UICamera.lastHit))
			{
				UICamera.hoveredObject = UICamera.fallThrough;
			}
			if (UICamera.hoveredObject == null)
			{
				UICamera.hoveredObject = UICamera.genericEventHandler;
			}
			for (int k = 0; k < 3; k++)
			{
				UICamera.mMouse[k].current = UICamera.hoveredObject;
			}
		}
		bool flag4 = UICamera.mMouse[0].last != UICamera.mMouse[0].current;
		if (flag4)
		{
			UICamera.currentScheme = UICamera.ControlScheme.Mouse;
		}
		if (flag2)
		{
			this.mTooltipTime = 0f;
		}
		else if (flag && (!this.stickyTooltip || flag4))
		{
			if (this.mTooltipTime != 0f)
			{
				this.mTooltipTime = RealTime.time + this.tooltipDelay;
			}
			else if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
		}
		if ((flag3 || !flag2) && UICamera.mHover != null && flag4)
		{
			UICamera.currentScheme = UICamera.ControlScheme.Mouse;
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			UICamera.Notify(UICamera.mHover, "OnHover", false);
			UICamera.mHover = null;
		}
		for (int l = 0; l < 3; l++)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(l);
			bool mouseButtonUp = Input.GetMouseButtonUp(l);
			if (mouseButtonDown || mouseButtonUp)
			{
				UICamera.currentScheme = UICamera.ControlScheme.Mouse;
			}
			UICamera.currentTouch = UICamera.mMouse[l];
			UICamera.currentTouchID = -1 - l;
			UICamera.currentKey = 323 + l;
			if (mouseButtonDown)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			this.ProcessTouch(mouseButtonDown, mouseButtonUp);
			UICamera.currentKey = 0;
		}
		UICamera.currentTouch = null;
		if (!flag2 && flag4)
		{
			UICamera.currentScheme = UICamera.ControlScheme.Mouse;
			this.mTooltipTime = RealTime.time + this.tooltipDelay;
			UICamera.mHover = UICamera.mMouse[0].current;
			UICamera.Notify(UICamera.mHover, "OnHover", true);
		}
		UICamera.mMouse[0].last = UICamera.mMouse[0].current;
		for (int m = 1; m < 3; m++)
		{
			UICamera.mMouse[m].last = UICamera.mMouse[0].last;
		}
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x00024924 File Offset: 0x00022B24
	public void ProcessTouches()
	{
		UICamera.currentScheme = UICamera.ControlScheme.Touch;
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			UICamera.currentTouchID = ((!this.allowMultiTouch) ? 1 : touch.fingerId);
			UICamera.currentTouch = UICamera.GetTouch(UICamera.currentTouchID);
			bool flag = touch.phase == null || UICamera.currentTouch.touchBegan;
			bool flag2 = touch.phase == 4 || touch.phase == 3;
			UICamera.currentTouch.touchBegan = false;
			UICamera.currentTouch.delta = ((!flag) ? (touch.position - UICamera.currentTouch.pos) : Vector2.zero);
			UICamera.currentTouch.pos = touch.position;
			if (!UICamera.Raycast(UICamera.currentTouch.pos, out UICamera.lastHit))
			{
				UICamera.hoveredObject = UICamera.fallThrough;
			}
			if (UICamera.hoveredObject == null)
			{
				UICamera.hoveredObject = UICamera.genericEventHandler;
			}
			UICamera.currentTouch.last = UICamera.currentTouch.current;
			UICamera.currentTouch.current = UICamera.hoveredObject;
			UICamera.lastTouchPosition = UICamera.currentTouch.pos;
			if (flag)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			if (touch.tapCount > 1)
			{
				UICamera.currentTouch.clickTime = RealTime.time;
			}
			this.ProcessTouch(flag, flag2);
			if (flag2)
			{
				UICamera.RemoveTouch(UICamera.currentTouchID);
			}
			UICamera.currentTouch.last = null;
			UICamera.currentTouch = null;
			if (!this.allowMultiTouch)
			{
				break;
			}
		}
		if (Input.touchCount == 0 && this.useMouse)
		{
			this.ProcessMouse();
		}
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x00024B24 File Offset: 0x00022D24
	private void ProcessFakeTouches()
	{
		bool mouseButtonDown = Input.GetMouseButtonDown(0);
		bool mouseButtonUp = Input.GetMouseButtonUp(0);
		bool mouseButton = Input.GetMouseButton(0);
		if (mouseButtonDown || mouseButtonUp || mouseButton)
		{
			UICamera.currentTouchID = 1;
			UICamera.currentTouch = UICamera.mMouse[0];
			UICamera.currentTouch.touchBegan = mouseButtonDown;
			Vector2 vector = Input.mousePosition;
			UICamera.currentTouch.delta = ((!mouseButtonDown) ? (vector - UICamera.currentTouch.pos) : Vector2.zero);
			UICamera.currentTouch.pos = vector;
			if (!UICamera.Raycast(UICamera.currentTouch.pos, out UICamera.lastHit))
			{
				UICamera.hoveredObject = UICamera.fallThrough;
			}
			if (UICamera.hoveredObject == null)
			{
				UICamera.hoveredObject = UICamera.genericEventHandler;
			}
			UICamera.currentTouch.last = UICamera.currentTouch.current;
			UICamera.currentTouch.current = UICamera.hoveredObject;
			UICamera.lastTouchPosition = UICamera.currentTouch.pos;
			if (mouseButtonDown)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			this.ProcessTouch(mouseButtonDown, mouseButtonUp);
			if (mouseButtonUp)
			{
				UICamera.RemoveTouch(UICamera.currentTouchID);
			}
			UICamera.currentTouch.last = null;
			UICamera.currentTouch = null;
		}
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x00024C90 File Offset: 0x00022E90
	public void ProcessOthers()
	{
		UICamera.currentTouchID = -100;
		UICamera.currentTouch = UICamera.controller;
		bool flag = false;
		bool flag2 = false;
		if (this.submitKey0 != null && Input.GetKeyDown(this.submitKey0))
		{
			UICamera.currentKey = this.submitKey0;
			flag = true;
		}
		if (this.submitKey1 != null && Input.GetKeyDown(this.submitKey1))
		{
			UICamera.currentKey = this.submitKey1;
			flag = true;
		}
		if (this.submitKey0 != null && Input.GetKeyUp(this.submitKey0))
		{
			UICamera.currentKey = this.submitKey0;
			flag2 = true;
		}
		if (this.submitKey1 != null && Input.GetKeyUp(this.submitKey1))
		{
			UICamera.currentKey = this.submitKey1;
			flag2 = true;
		}
		if (flag || flag2)
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.currentTouch.last = UICamera.currentTouch.current;
			UICamera.currentTouch.current = UICamera.mCurrentSelection;
			this.ProcessTouch(flag, flag2);
			UICamera.currentTouch.last = null;
		}
		int num = 0;
		int num2 = 0;
		if (this.useKeyboard)
		{
			if (UICamera.inputHasFocus)
			{
				num += UICamera.GetDirection(273, 274);
				num2 += UICamera.GetDirection(275, 276);
			}
			else
			{
				num += UICamera.GetDirection(119, 273, 115, 274);
				num2 += UICamera.GetDirection(100, 275, 97, 276);
			}
		}
		if (this.useController)
		{
			if (!string.IsNullOrEmpty(this.verticalAxisName))
			{
				num += UICamera.GetDirection(this.verticalAxisName);
			}
			if (!string.IsNullOrEmpty(this.horizontalAxisName))
			{
				num2 += UICamera.GetDirection(this.horizontalAxisName);
			}
		}
		if (num != 0)
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.Notify(UICamera.mCurrentSelection, "OnKey", (num <= 0) ? 274 : 273);
		}
		if (num2 != 0)
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.Notify(UICamera.mCurrentSelection, "OnKey", (num2 <= 0) ? 276 : 275);
		}
		if (this.useKeyboard && Input.GetKeyDown(9))
		{
			UICamera.currentKey = 9;
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.Notify(UICamera.mCurrentSelection, "OnKey", 9);
		}
		if (this.cancelKey0 != null && Input.GetKeyDown(this.cancelKey0))
		{
			UICamera.currentKey = this.cancelKey0;
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.Notify(UICamera.mCurrentSelection, "OnKey", 27);
		}
		if (this.cancelKey1 != null && Input.GetKeyDown(this.cancelKey1))
		{
			UICamera.currentKey = this.cancelKey1;
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.Notify(UICamera.mCurrentSelection, "OnKey", 27);
		}
		UICamera.currentTouch = null;
		UICamera.currentKey = 0;
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x00024F88 File Offset: 0x00023188
	public void ProcessTouch(bool pressed, bool unpressed)
	{
		bool flag = UICamera.currentScheme == UICamera.ControlScheme.Mouse;
		float num = (!flag) ? this.touchDragThreshold : this.mouseDragThreshold;
		float num2 = (!flag) ? this.touchClickThreshold : this.mouseClickThreshold;
		num *= num;
		num2 *= num2;
		if (pressed)
		{
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			UICamera.currentTouch.pressStarted = true;
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
			UICamera.currentTouch.pressed = UICamera.currentTouch.current;
			UICamera.currentTouch.dragged = UICamera.currentTouch.current;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			UICamera.currentTouch.totalDelta = Vector2.zero;
			UICamera.currentTouch.dragStarted = false;
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", true);
			if (UICamera.currentTouch.pressed != UICamera.mCurrentSelection)
			{
				if (this.mTooltip != null)
				{
					this.ShowTooltip(false);
				}
				UICamera.currentScheme = UICamera.ControlScheme.Touch;
				UICamera.selectedObject = UICamera.currentTouch.pressed;
			}
		}
		else if (UICamera.currentTouch.pressed != null && (UICamera.currentTouch.delta.sqrMagnitude != 0f || UICamera.currentTouch.current != UICamera.currentTouch.last))
		{
			UICamera.currentTouch.totalDelta += UICamera.currentTouch.delta;
			float sqrMagnitude = UICamera.currentTouch.totalDelta.sqrMagnitude;
			bool flag2 = false;
			if (!UICamera.currentTouch.dragStarted && UICamera.currentTouch.last != UICamera.currentTouch.current)
			{
				UICamera.currentTouch.dragStarted = true;
				UICamera.currentTouch.delta = UICamera.currentTouch.totalDelta;
				UICamera.isDragging = true;
				UICamera.Notify(UICamera.currentTouch.dragged, "OnDragStart", null);
				UICamera.Notify(UICamera.currentTouch.last, "OnDragOver", UICamera.currentTouch.dragged);
				UICamera.isDragging = false;
			}
			else if (!UICamera.currentTouch.dragStarted && num < sqrMagnitude)
			{
				flag2 = true;
				UICamera.currentTouch.dragStarted = true;
				UICamera.currentTouch.delta = UICamera.currentTouch.totalDelta;
			}
			if (UICamera.currentTouch.dragStarted)
			{
				if (this.mTooltip != null)
				{
					this.ShowTooltip(false);
				}
				UICamera.isDragging = true;
				bool flag3 = UICamera.currentTouch.clickNotification == UICamera.ClickNotification.None;
				if (flag2)
				{
					UICamera.Notify(UICamera.currentTouch.dragged, "OnDragStart", null);
					UICamera.Notify(UICamera.currentTouch.current, "OnDragOver", UICamera.currentTouch.dragged);
				}
				else if (UICamera.currentTouch.last != UICamera.currentTouch.current)
				{
					UICamera.Notify(UICamera.currentTouch.last, "OnDragOut", UICamera.currentTouch.dragged);
					UICamera.Notify(UICamera.currentTouch.current, "OnDragOver", UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.dragged, "OnDrag", UICamera.currentTouch.delta);
				UICamera.currentTouch.last = UICamera.currentTouch.current;
				UICamera.isDragging = false;
				if (flag3)
				{
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
				}
				else if (UICamera.currentTouch.clickNotification == UICamera.ClickNotification.BasedOnDelta && num2 < sqrMagnitude)
				{
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
				}
			}
		}
		if (unpressed)
		{
			UICamera.currentTouch.pressStarted = false;
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			if (UICamera.currentTouch.pressed != null)
			{
				if (UICamera.currentTouch.dragStarted)
				{
					UICamera.Notify(UICamera.currentTouch.last, "OnDragOut", UICamera.currentTouch.dragged);
					UICamera.Notify(UICamera.currentTouch.dragged, "OnDragEnd", null);
				}
				UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
				if (flag)
				{
					UICamera.Notify(UICamera.currentTouch.current, "OnHover", true);
				}
				UICamera.mHover = UICamera.currentTouch.current;
				if (UICamera.currentTouch.dragged == UICamera.currentTouch.current || (UICamera.currentScheme != UICamera.ControlScheme.Controller && UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None && UICamera.currentTouch.totalDelta.sqrMagnitude < num))
				{
					if (UICamera.currentTouch.pressed != UICamera.mCurrentSelection)
					{
						UICamera.mNextSelection = null;
						UICamera.mCurrentSelection = UICamera.currentTouch.pressed;
						UICamera.Notify(UICamera.currentTouch.pressed, "OnSelect", true);
					}
					else
					{
						UICamera.mNextSelection = null;
						UICamera.mCurrentSelection = UICamera.currentTouch.pressed;
					}
					if (UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None && UICamera.currentTouch.pressed == UICamera.currentTouch.current)
					{
						float time = RealTime.time;
						UICamera.Notify(UICamera.currentTouch.pressed, "OnClick", null);
						if (UICamera.currentTouch.clickTime + 0.35f > time)
						{
							UICamera.Notify(UICamera.currentTouch.pressed, "OnDoubleClick", null);
						}
						UICamera.currentTouch.clickTime = time;
					}
				}
				else if (UICamera.currentTouch.dragStarted)
				{
					UICamera.Notify(UICamera.currentTouch.current, "OnDrop", UICamera.currentTouch.dragged);
				}
			}
			UICamera.currentTouch.dragStarted = false;
			UICamera.currentTouch.pressed = null;
			UICamera.currentTouch.dragged = null;
		}
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x000255A8 File Offset: 0x000237A8
	public void ShowTooltip(bool val)
	{
		this.mTooltipTime = 0f;
		UICamera.Notify(this.mTooltip, "OnTooltip", val);
		if (!val)
		{
			this.mTooltip = null;
		}
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x000255E4 File Offset: 0x000237E4
	private void OnApplicationPause()
	{
		UICamera.MouseOrTouch mouseOrTouch = UICamera.currentTouch;
		if (this.useTouch)
		{
			BetterList<int> betterList = new BetterList<int>();
			foreach (KeyValuePair<int, UICamera.MouseOrTouch> keyValuePair in UICamera.mTouches)
			{
				if (keyValuePair.Value != null && keyValuePair.Value.pressed)
				{
					UICamera.currentTouch = keyValuePair.Value;
					UICamera.currentTouchID = keyValuePair.Key;
					UICamera.currentScheme = UICamera.ControlScheme.Touch;
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
					this.ProcessTouch(false, true);
					betterList.Add(UICamera.currentTouchID);
				}
			}
			for (int i = 0; i < betterList.size; i++)
			{
				UICamera.RemoveTouch(betterList[i]);
			}
		}
		if (this.useMouse)
		{
			for (int j = 0; j < 3; j++)
			{
				if (UICamera.mMouse[j].pressed)
				{
					UICamera.currentTouch = UICamera.mMouse[j];
					UICamera.currentTouchID = -1 - j;
					UICamera.currentKey = 323 + j;
					UICamera.currentScheme = UICamera.ControlScheme.Mouse;
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
					this.ProcessTouch(false, true);
				}
			}
		}
		if (this.useController && UICamera.controller.pressed)
		{
			UICamera.currentTouch = UICamera.controller;
			UICamera.currentTouchID = -100;
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.currentTouch.last = UICamera.currentTouch.current;
			UICamera.currentTouch.current = UICamera.mCurrentSelection;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
			this.ProcessTouch(false, true);
			UICamera.currentTouch.last = null;
		}
		UICamera.currentTouch = mouseOrTouch;
	}

	// Token: 0x04000489 RID: 1161
	public static BetterList<UICamera> list = new BetterList<UICamera>();

	// Token: 0x0400048A RID: 1162
	public static UICamera.OnScreenResize onScreenResize;

	// Token: 0x0400048B RID: 1163
	public UICamera.EventType eventType = UICamera.EventType.UI;

	// Token: 0x0400048C RID: 1164
	public LayerMask eventReceiverMask = -1;

	// Token: 0x0400048D RID: 1165
	public bool debug;

	// Token: 0x0400048E RID: 1166
	public bool useMouse = true;

	// Token: 0x0400048F RID: 1167
	public bool useTouch = true;

	// Token: 0x04000490 RID: 1168
	public bool allowMultiTouch = true;

	// Token: 0x04000491 RID: 1169
	public bool useKeyboard = true;

	// Token: 0x04000492 RID: 1170
	public bool useController = true;

	// Token: 0x04000493 RID: 1171
	public bool stickyTooltip = true;

	// Token: 0x04000494 RID: 1172
	public float tooltipDelay = 1f;

	// Token: 0x04000495 RID: 1173
	public float mouseDragThreshold = 4f;

	// Token: 0x04000496 RID: 1174
	public float mouseClickThreshold = 10f;

	// Token: 0x04000497 RID: 1175
	public float touchDragThreshold = 40f;

	// Token: 0x04000498 RID: 1176
	public float touchClickThreshold = 40f;

	// Token: 0x04000499 RID: 1177
	public float rangeDistance = -1f;

	// Token: 0x0400049A RID: 1178
	public string scrollAxisName = "Mouse ScrollWheel";

	// Token: 0x0400049B RID: 1179
	public string verticalAxisName = "Vertical";

	// Token: 0x0400049C RID: 1180
	public string horizontalAxisName = "Horizontal";

	// Token: 0x0400049D RID: 1181
	public KeyCode submitKey0 = 13;

	// Token: 0x0400049E RID: 1182
	public KeyCode submitKey1 = 330;

	// Token: 0x0400049F RID: 1183
	public KeyCode cancelKey0 = 27;

	// Token: 0x040004A0 RID: 1184
	public KeyCode cancelKey1 = 331;

	// Token: 0x040004A1 RID: 1185
	public static UICamera.OnCustomInput onCustomInput;

	// Token: 0x040004A2 RID: 1186
	public static bool showTooltips = true;

	// Token: 0x040004A3 RID: 1187
	public static Vector2 lastTouchPosition = Vector2.zero;

	// Token: 0x040004A4 RID: 1188
	public static RaycastHit lastHit;

	// Token: 0x040004A5 RID: 1189
	public static UICamera current = null;

	// Token: 0x040004A6 RID: 1190
	public static Camera currentCamera = null;

	// Token: 0x040004A7 RID: 1191
	public static UICamera.ControlScheme currentScheme = UICamera.ControlScheme.Mouse;

	// Token: 0x040004A8 RID: 1192
	public static int currentTouchID = -1;

	// Token: 0x040004A9 RID: 1193
	public static KeyCode currentKey = 0;

	// Token: 0x040004AA RID: 1194
	public static UICamera.MouseOrTouch currentTouch = null;

	// Token: 0x040004AB RID: 1195
	public static bool inputHasFocus = false;

	// Token: 0x040004AC RID: 1196
	public static GameObject genericEventHandler;

	// Token: 0x040004AD RID: 1197
	public static GameObject fallThrough;

	// Token: 0x040004AE RID: 1198
	private static GameObject mCurrentSelection = null;

	// Token: 0x040004AF RID: 1199
	private static GameObject mNextSelection = null;

	// Token: 0x040004B0 RID: 1200
	private static UICamera.ControlScheme mNextScheme = UICamera.ControlScheme.Controller;

	// Token: 0x040004B1 RID: 1201
	private static UICamera.MouseOrTouch[] mMouse = new UICamera.MouseOrTouch[]
	{
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch()
	};

	// Token: 0x040004B2 RID: 1202
	private static GameObject mHover;

	// Token: 0x040004B3 RID: 1203
	public static UICamera.MouseOrTouch controller = new UICamera.MouseOrTouch();

	// Token: 0x040004B4 RID: 1204
	private static float mNextEvent = 0f;

	// Token: 0x040004B5 RID: 1205
	private static Dictionary<int, UICamera.MouseOrTouch> mTouches = new Dictionary<int, UICamera.MouseOrTouch>();

	// Token: 0x040004B6 RID: 1206
	private static int mWidth = 0;

	// Token: 0x040004B7 RID: 1207
	private static int mHeight = 0;

	// Token: 0x040004B8 RID: 1208
	private GameObject mTooltip;

	// Token: 0x040004B9 RID: 1209
	private Camera mCam;

	// Token: 0x040004BA RID: 1210
	private float mTooltipTime;

	// Token: 0x040004BB RID: 1211
	private float mNextRaycast;

	// Token: 0x040004BC RID: 1212
	public static bool isDragging = false;

	// Token: 0x040004BD RID: 1213
	public static GameObject hoveredObject;

	// Token: 0x040004BE RID: 1214
	private static UICamera.DepthEntry mHit = default(UICamera.DepthEntry);

	// Token: 0x040004BF RID: 1215
	private static BetterList<UICamera.DepthEntry> mHits = new BetterList<UICamera.DepthEntry>();

	// Token: 0x040004C0 RID: 1216
	private static RaycastHit mEmpty = default(RaycastHit);

	// Token: 0x040004C1 RID: 1217
	private static Plane m2DPlane = new Plane(Vector3.back, 0f);

	// Token: 0x040004C2 RID: 1218
	private static bool mNotifying = false;

	// Token: 0x020000B8 RID: 184
	public enum ControlScheme
	{
		// Token: 0x040004C5 RID: 1221
		Mouse,
		// Token: 0x040004C6 RID: 1222
		Touch,
		// Token: 0x040004C7 RID: 1223
		Controller
	}

	// Token: 0x020000B9 RID: 185
	public enum ClickNotification
	{
		// Token: 0x040004C9 RID: 1225
		None,
		// Token: 0x040004CA RID: 1226
		Always,
		// Token: 0x040004CB RID: 1227
		BasedOnDelta
	}

	// Token: 0x020000BA RID: 186
	public class MouseOrTouch
	{
		// Token: 0x040004CC RID: 1228
		public Vector2 pos;

		// Token: 0x040004CD RID: 1229
		public Vector2 lastPos;

		// Token: 0x040004CE RID: 1230
		public Vector2 delta;

		// Token: 0x040004CF RID: 1231
		public Vector2 totalDelta;

		// Token: 0x040004D0 RID: 1232
		public Camera pressedCam;

		// Token: 0x040004D1 RID: 1233
		public GameObject last;

		// Token: 0x040004D2 RID: 1234
		public GameObject current;

		// Token: 0x040004D3 RID: 1235
		public GameObject pressed;

		// Token: 0x040004D4 RID: 1236
		public GameObject dragged;

		// Token: 0x040004D5 RID: 1237
		public float clickTime;

		// Token: 0x040004D6 RID: 1238
		public UICamera.ClickNotification clickNotification = UICamera.ClickNotification.Always;

		// Token: 0x040004D7 RID: 1239
		public bool touchBegan = true;

		// Token: 0x040004D8 RID: 1240
		public bool pressStarted;

		// Token: 0x040004D9 RID: 1241
		public bool dragStarted;
	}

	// Token: 0x020000BB RID: 187
	public enum EventType
	{
		// Token: 0x040004DB RID: 1243
		World,
		// Token: 0x040004DC RID: 1244
		UI,
		// Token: 0x040004DD RID: 1245
		Unity2D
	}

	// Token: 0x020000BC RID: 188
	private struct DepthEntry
	{
		// Token: 0x040004DE RID: 1246
		public int depth;

		// Token: 0x040004DF RID: 1247
		public RaycastHit hit;
	}

	// Token: 0x02000AA7 RID: 2727
	// (Invoke) Token: 0x06004F25 RID: 20261
	public delegate void OnScreenResize();

	// Token: 0x02000AA8 RID: 2728
	// (Invoke) Token: 0x06004F29 RID: 20265
	public delegate void OnCustomInput();
}
