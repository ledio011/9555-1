using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
[AddComponentMenu("FingerGestures/Toolbox/Quick Setup")]
public class TBQuickSetup : MonoBehaviour
{
	// Token: 0x1700006D RID: 109
	// (get) Token: 0x06000206 RID: 518 RVA: 0x00008CEC File Offset: 0x00006EEC
	// (set) Token: 0x06000207 RID: 519 RVA: 0x00008CF4 File Offset: 0x00006EF4
	public FingerDownDetector FingerDown { get; set; }

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x06000208 RID: 520 RVA: 0x00008D00 File Offset: 0x00006F00
	// (set) Token: 0x06000209 RID: 521 RVA: 0x00008D08 File Offset: 0x00006F08
	public FingerUpDetector FingerUp { get; set; }

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x0600020A RID: 522 RVA: 0x00008D14 File Offset: 0x00006F14
	// (set) Token: 0x0600020B RID: 523 RVA: 0x00008D1C File Offset: 0x00006F1C
	public FingerHoverDetector FingerHover { get; set; }

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x0600020C RID: 524 RVA: 0x00008D28 File Offset: 0x00006F28
	// (set) Token: 0x0600020D RID: 525 RVA: 0x00008D30 File Offset: 0x00006F30
	public FingerMotionDetector FingerMotion { get; set; }

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x0600020E RID: 526 RVA: 0x00008D3C File Offset: 0x00006F3C
	// (set) Token: 0x0600020F RID: 527 RVA: 0x00008D44 File Offset: 0x00006F44
	public DragRecognizer Drag { get; set; }

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06000210 RID: 528 RVA: 0x00008D50 File Offset: 0x00006F50
	// (set) Token: 0x06000211 RID: 529 RVA: 0x00008D58 File Offset: 0x00006F58
	public LongPressRecognizer LongPress { get; set; }

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x06000212 RID: 530 RVA: 0x00008D64 File Offset: 0x00006F64
	// (set) Token: 0x06000213 RID: 531 RVA: 0x00008D6C File Offset: 0x00006F6C
	public SwipeRecognizer Swipe { get; set; }

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06000214 RID: 532 RVA: 0x00008D78 File Offset: 0x00006F78
	// (set) Token: 0x06000215 RID: 533 RVA: 0x00008D80 File Offset: 0x00006F80
	public TapRecognizer Tap { get; set; }

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06000216 RID: 534 RVA: 0x00008D8C File Offset: 0x00006F8C
	// (set) Token: 0x06000217 RID: 535 RVA: 0x00008D94 File Offset: 0x00006F94
	public TapRecognizer DoubleTap { get; set; }

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06000218 RID: 536 RVA: 0x00008DA0 File Offset: 0x00006FA0
	// (set) Token: 0x06000219 RID: 537 RVA: 0x00008DA8 File Offset: 0x00006FA8
	public PinchRecognizer Pinch { get; set; }

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x0600021A RID: 538 RVA: 0x00008DB4 File Offset: 0x00006FB4
	// (set) Token: 0x0600021B RID: 539 RVA: 0x00008DBC File Offset: 0x00006FBC
	public TwistRecognizer Twist { get; set; }

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x0600021C RID: 540 RVA: 0x00008DC8 File Offset: 0x00006FC8
	// (set) Token: 0x0600021D RID: 541 RVA: 0x00008DD0 File Offset: 0x00006FD0
	public DragRecognizer TwoFingerDrag { get; set; }

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x0600021E RID: 542 RVA: 0x00008DDC File Offset: 0x00006FDC
	// (set) Token: 0x0600021F RID: 543 RVA: 0x00008DE4 File Offset: 0x00006FE4
	public TapRecognizer TwoFingerTap { get; set; }

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06000220 RID: 544 RVA: 0x00008DF0 File Offset: 0x00006FF0
	// (set) Token: 0x06000221 RID: 545 RVA: 0x00008DF8 File Offset: 0x00006FF8
	public SwipeRecognizer TwoFingerSwipe { get; set; }

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06000222 RID: 546 RVA: 0x00008E04 File Offset: 0x00007004
	// (set) Token: 0x06000223 RID: 547 RVA: 0x00008E0C File Offset: 0x0000700C
	public LongPressRecognizer TwoFingerLongPress { get; set; }

	// Token: 0x06000224 RID: 548 RVA: 0x00008E18 File Offset: 0x00007018
	private GameObject CreateChildNode(string name)
	{
		GameObject gameObject = new GameObject(name);
		Transform transform = gameObject.transform;
		transform.parent = base.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		return gameObject;
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00008E58 File Offset: 0x00007058
	private void Start()
	{
		if (!this.MessageTarget)
		{
			this.MessageTarget = base.gameObject;
		}
		this.screenRaycaster = base.GetComponent<ScreenRaycaster>();
		if (!this.screenRaycaster)
		{
			this.screenRaycaster = base.gameObject.AddComponent<ScreenRaycaster>();
		}
		if (!FingerGestures.Instance)
		{
			base.gameObject.AddComponent<FingerGestures>();
		}
		GameObject node = this.CreateChildNode("Finger Event Detectors");
		this.FingerDown = this.AddFingerEventDetector<FingerDownDetector>(node);
		this.FingerUp = this.AddFingerEventDetector<FingerUpDetector>(node);
		this.FingerMotion = this.AddFingerEventDetector<FingerMotionDetector>(node);
		this.FingerHover = this.AddFingerEventDetector<FingerHoverDetector>(node);
		GameObject node2 = this.CreateChildNode("Single Finger Gestures");
		this.Drag = this.AddSingleFingerGesture<DragRecognizer>(node2);
		this.Tap = this.AddSingleFingerGesture<TapRecognizer>(node2);
		this.Swipe = this.AddSingleFingerGesture<SwipeRecognizer>(node2);
		this.LongPress = this.AddSingleFingerGesture<LongPressRecognizer>(node2);
		this.DoubleTap = this.AddSingleFingerGesture<TapRecognizer>(node2);
		this.DoubleTap.RequiredTaps = 2;
		this.DoubleTap.EventMessageName = "OnDoubleTap";
		GameObject node3 = this.CreateChildNode("Two-Finger Gestures");
		this.Pinch = this.AddTwoFingerGesture<PinchRecognizer>(node3);
		this.Twist = this.AddTwoFingerGesture<TwistRecognizer>(node3);
		this.TwoFingerDrag = this.AddTwoFingerGesture<DragRecognizer>(node3, "OnTwoFingerDrag");
		this.TwoFingerTap = this.AddTwoFingerGesture<TapRecognizer>(node3, "OnTwoFingerTap");
		this.TwoFingerSwipe = this.AddTwoFingerGesture<SwipeRecognizer>(node3, "OnTwoFingerSwipe");
		this.TwoFingerLongPress = this.AddTwoFingerGesture<LongPressRecognizer>(node3, "OnTwoFingerLongPress");
	}

	// Token: 0x06000226 RID: 550 RVA: 0x00008FE0 File Offset: 0x000071E0
	private T AddFingerEventDetector<T>(GameObject node) where T : FingerEventDetector
	{
		T t = node.AddComponent<T>();
		t.Raycaster = this.screenRaycaster;
		t.MessageTarget = this.MessageTarget;
		return t;
	}

	// Token: 0x06000227 RID: 551 RVA: 0x00009018 File Offset: 0x00007218
	private T AddGesture<T>(GameObject node) where T : GestureRecognizer
	{
		T t = node.AddComponent<T>();
		t.Raycaster = this.screenRaycaster;
		t.EventMessageTarget = this.MessageTarget;
		if (t.SupportFingerClustering)
		{
			t.MaxSimultaneousGestures = this.MaxSimultaneousGestures;
		}
		return t;
	}

	// Token: 0x06000228 RID: 552 RVA: 0x00009074 File Offset: 0x00007274
	private T AddSingleFingerGesture<T>(GameObject node) where T : GestureRecognizer
	{
		T result = this.AddGesture<T>(node);
		result.RequiredFingerCount = 1;
		return result;
	}

	// Token: 0x06000229 RID: 553 RVA: 0x00009098 File Offset: 0x00007298
	private T AddTwoFingerGesture<T>(GameObject node) where T : GestureRecognizer
	{
		T result = this.AddGesture<T>(node);
		result.RequiredFingerCount = 2;
		return result;
	}

	// Token: 0x0600022A RID: 554 RVA: 0x000090BC File Offset: 0x000072BC
	private T AddTwoFingerGesture<T>(GameObject node, string eventName) where T : GestureRecognizer
	{
		T t = this.AddTwoFingerGesture<T>(node);
		t.EventMessageName = eventName;
		return t;
	}

	// Token: 0x0400017D RID: 381
	public GameObject MessageTarget;

	// Token: 0x0400017E RID: 382
	public int MaxSimultaneousGestures = 2;

	// Token: 0x0400017F RID: 383
	private ScreenRaycaster screenRaycaster;
}
