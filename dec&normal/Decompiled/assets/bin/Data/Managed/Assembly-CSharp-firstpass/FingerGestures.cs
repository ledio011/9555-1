using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000021 RID: 33
[AddComponentMenu("FingerGestures/Finger Gestures Singleton")]
public class FingerGestures : MonoBehaviour
{
	// Token: 0x14000008 RID: 8
	// (add) Token: 0x060000B1 RID: 177 RVA: 0x00004428 File Offset: 0x00002628
	// (remove) Token: 0x060000B2 RID: 178 RVA: 0x00004440 File Offset: 0x00002640
	public static event Gesture.EventHandler OnGestureEvent;

	// Token: 0x14000009 RID: 9
	// (add) Token: 0x060000B3 RID: 179 RVA: 0x00004458 File Offset: 0x00002658
	// (remove) Token: 0x060000B4 RID: 180 RVA: 0x00004470 File Offset: 0x00002670
	public static event FingerEventDetector<FingerEvent>.FingerEventHandler OnFingerEvent;

	// Token: 0x1400000A RID: 10
	// (add) Token: 0x060000B5 RID: 181 RVA: 0x00004488 File Offset: 0x00002688
	// (remove) Token: 0x060000B6 RID: 182 RVA: 0x000044A0 File Offset: 0x000026A0
	public static event FingerGestures.EventHandler OnInputProviderChanged;

	// Token: 0x060000B7 RID: 183 RVA: 0x000044B8 File Offset: 0x000026B8
	internal static void FireEvent(Gesture gesture)
	{
		if (FingerGestures.OnGestureEvent != null)
		{
			FingerGestures.OnGestureEvent(gesture);
		}
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x000044D0 File Offset: 0x000026D0
	internal static void FireEvent(FingerEvent eventData)
	{
		if (FingerGestures.OnFingerEvent != null)
		{
			FingerGestures.OnFingerEvent(eventData);
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x060000B9 RID: 185 RVA: 0x000044E8 File Offset: 0x000026E8
	public static FingerClusterManager DefaultClusterManager
	{
		get
		{
			return FingerGestures.Instance.fingerClusterManager;
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x060000BA RID: 186 RVA: 0x000044F4 File Offset: 0x000026F4
	public static FingerGestures Instance
	{
		get
		{
			return FingerGestures.instance;
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x000044FC File Offset: 0x000026FC
	private void Init()
	{
		this.InitInputProvider();
		this.fingerClusterManager = base.GetComponent<FingerClusterManager>();
		if (!this.fingerClusterManager)
		{
			this.fingerClusterManager = base.gameObject.AddComponent<FingerClusterManager>();
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060000BC RID: 188 RVA: 0x0000453C File Offset: 0x0000273C
	public FGInputProvider InputProvider
	{
		get
		{
			return this.inputProvider;
		}
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00004544 File Offset: 0x00002744
	public static bool IsTouchScreenPlatform(RuntimePlatform platform)
	{
		for (int i = 0; i < FingerGestures.TouchScreenPlatforms.Length; i++)
		{
			if (platform == FingerGestures.TouchScreenPlatforms[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x0000457C File Offset: 0x0000277C
	private void InitInputProvider()
	{
		FingerGestures.InputProviderEvent inputProviderEvent = new FingerGestures.InputProviderEvent();
		if (FingerGestures.IsTouchScreenPlatform(Application.platform))
		{
			inputProviderEvent.inputProviderPrefab = this.touchInputProviderPrefab;
		}
		else
		{
			inputProviderEvent.inputProviderPrefab = this.mouseInputProviderPrefab;
		}
		base.gameObject.SendMessage("OnSelectInputProvider", inputProviderEvent, SendMessageOptions.DontRequireReceiver);
		this.InstallInputProvider(inputProviderEvent.inputProviderPrefab);
	}

	// Token: 0x060000BF RID: 191 RVA: 0x000045DC File Offset: 0x000027DC
	public void InstallInputProvider(FGInputProvider inputProviderPrefab)
	{
		if (!inputProviderPrefab)
		{
			Debug.LogError("Invalid InputProvider (null)");
			return;
		}
		if (this.inputProvider)
		{
			UnityEngine.Object.Destroy(this.inputProvider.gameObject);
		}
		this.inputProvider = (UnityEngine.Object.Instantiate(inputProviderPrefab) as FGInputProvider);
		this.inputProvider.name = inputProviderPrefab.name;
		this.inputProvider.transform.parent = base.transform;
		this.InitFingers(this.MaxFingers);
		if (FingerGestures.OnInputProviderChanged != null)
		{
			FingerGestures.OnInputProviderChanged();
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004678 File Offset: 0x00002878
	public int MaxFingers
	{
		get
		{
			return this.inputProvider.MaxSimultaneousFingers;
		}
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00004688 File Offset: 0x00002888
	public static FingerGestures.Finger GetFinger(int index)
	{
		return FingerGestures.instance.fingers[index];
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004698 File Offset: 0x00002898
	public static FingerGestures.IFingerList Touches
	{
		get
		{
			return FingerGestures.instance.touches;
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x060000C3 RID: 195 RVA: 0x000046A4 File Offset: 0x000028A4
	public static List<GestureRecognizer> RegisteredGestureRecognizers
	{
		get
		{
			return FingerGestures.gestureRecognizers;
		}
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x000046AC File Offset: 0x000028AC
	public static void Register(GestureRecognizer recognizer)
	{
		if (FingerGestures.gestureRecognizers.Contains(recognizer))
		{
			return;
		}
		FingerGestures.gestureRecognizers.Add(recognizer);
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x000046CC File Offset: 0x000028CC
	public static void Unregister(GestureRecognizer recognizer)
	{
		FingerGestures.gestureRecognizers.Remove(recognizer);
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x000046DC File Offset: 0x000028DC
	private void Awake()
	{
		this.CheckInit();
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x000046E4 File Offset: 0x000028E4
	private void Start()
	{
		if (this.makePersistent)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x000046FC File Offset: 0x000028FC
	private void OnEnable()
	{
		this.CheckInit();
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00004704 File Offset: 0x00002904
	private void CheckInit()
	{
		if (FingerGestures.instance == null)
		{
			FingerGestures.instance = this;
			this.Init();
		}
		else if (FingerGestures.instance != this)
		{
			Debug.LogWarning("There is already an instance of FingerGestures created (" + FingerGestures.instance.name + "). Destroying new one.");
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
	}

	// Token: 0x060000CA RID: 202 RVA: 0x0000476C File Offset: 0x0000296C
	private void Update()
	{
		if (this.inputProvider)
		{
			this.UpdateFingers();
		}
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00004784 File Offset: 0x00002984
	private void InitFingers(int count)
	{
		this.fingers = new FingerGestures.Finger[count];
		for (int i = 0; i < count; i++)
		{
			this.fingers[i] = new FingerGestures.Finger(i);
		}
		this.touches = new FingerGestures.FingerList();
	}

	// Token: 0x060000CC RID: 204 RVA: 0x000047C8 File Offset: 0x000029C8
	private void UpdateFingers()
	{
		this.touches.Clear();
		for (int i = 0; i < this.fingers.Length; i++)
		{
			FingerGestures.Finger finger = this.fingers[i];
			Vector2 zero = Vector2.zero;
			bool newDownState = false;
			this.inputProvider.GetInputState(finger.Index, out newDownState, out zero);
			finger.Update(newDownState, zero);
			if (finger.IsDown)
			{
				this.touches.Add(finger);
			}
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060000CD RID: 205 RVA: 0x00004840 File Offset: 0x00002A40
	// (set) Token: 0x060000CE RID: 206 RVA: 0x0000484C File Offset: 0x00002A4C
	public static FingerGestures.GlobalTouchFilterDelegate GlobalTouchFilter
	{
		get
		{
			return FingerGestures.instance.globalTouchFilterFunc;
		}
		set
		{
			FingerGestures.instance.globalTouchFilterFunc = value;
		}
	}

	// Token: 0x060000CF RID: 207 RVA: 0x0000485C File Offset: 0x00002A5C
	protected bool ShouldProcessTouch(int fingerIndex, Vector2 position)
	{
		return this.globalTouchFilterFunc == null || this.globalTouchFilterFunc(fingerIndex, position);
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00004878 File Offset: 0x00002A78
	private Transform CreateNode(string name, Transform parent)
	{
		return new GameObject(name)
		{
			transform = 
			{
				parent = parent
			}
		}.transform;
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x000048A0 File Offset: 0x00002AA0
	private void InitNodes()
	{
		int num = this.fingers.Length;
		if (this.fingerNodes != null)
		{
			foreach (Transform transform in this.fingerNodes)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
		this.fingerNodes = new Transform[num];
		for (int j = 0; j < this.fingerNodes.Length; j++)
		{
			this.fingerNodes[j] = this.CreateNode("Finger" + j, base.transform);
		}
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x0000493C File Offset: 0x00002B3C
	public static FingerGestures.SwipeDirection GetSwipeDirection(Vector2 dir, float tolerance)
	{
		float num = Mathf.Max(Mathf.Clamp01(tolerance) * 22.5f, 0.0001f);
		float num2 = FingerGestures.NormalizeAngle360(57.29578f * Mathf.Atan2(dir.y, dir.x));
		if (num2 >= 337.5f)
		{
			num2 -= 360f;
		}
		int i = 0;
		while (i < 8)
		{
			float num3 = 45f * (float)i;
			if (num2 <= num3 + 22.5f)
			{
				float num4 = num3 - num;
				float num5 = num3 + num;
				if (num2 >= num4 && num2 <= num5)
				{
					return FingerGestures.AngleToDirectionMap[i];
				}
				break;
			}
			else
			{
				i++;
			}
		}
		return FingerGestures.SwipeDirection.None;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x000049E4 File Offset: 0x00002BE4
	public static FingerGestures.SwipeDirection GetSwipeDirection(Vector2 dir)
	{
		return FingerGestures.GetSwipeDirection(dir, 1f);
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x000049F4 File Offset: 0x00002BF4
	public static bool UsingUnityRemote()
	{
		return false;
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x000049F8 File Offset: 0x00002BF8
	public static bool AllFingersMoving(FingerGestures.Finger finger0, FingerGestures.Finger finger1)
	{
		return finger0.IsMoving && finger1.IsMoving;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00004A10 File Offset: 0x00002C10
	public static bool FingersMovedInOppositeDirections(FingerGestures.Finger finger0, FingerGestures.Finger finger1, float minDOT)
	{
		float num = Vector2.Dot(finger0.DeltaPosition.normalized, finger1.DeltaPosition.normalized);
		return num < minDOT;
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00004A44 File Offset: 0x00002C44
	public static float SignedAngle(Vector2 from, Vector2 to)
	{
		float y = from.x * to.y - from.y * to.x;
		return Mathf.Atan2(y, Vector2.Dot(from, to));
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00004A80 File Offset: 0x00002C80
	public static float NormalizeAngle360(float angleInDegrees)
	{
		angleInDegrees %= 360f;
		if (angleInDegrees < 0f)
		{
			angleInDegrees += 360f;
		}
		return angleInDegrees;
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004AA0 File Offset: 0x00002CA0
	// (set) Token: 0x060000DA RID: 218 RVA: 0x00004ADC File Offset: 0x00002CDC
	public static float ScreenDPI
	{
		get
		{
			if (FingerGestures.screenDPI <= 0f)
			{
				FingerGestures.screenDPI = Screen.dpi;
				if (FingerGestures.screenDPI <= 0f)
				{
					FingerGestures.screenDPI = 96f;
				}
			}
			return FingerGestures.screenDPI;
		}
		set
		{
			FingerGestures.screenDPI = value;
		}
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00004AE4 File Offset: 0x00002CE4
	public static float Convert(float distance, DistanceUnit fromUnit, DistanceUnit toUnit)
	{
		float num = FingerGestures.ScreenDPI;
		float num2;
		switch (fromUnit)
		{
		case DistanceUnit.Inches:
			num2 = distance * num;
			goto IL_3E;
		case DistanceUnit.Centimeters:
			num2 = distance * 0.39370078f * num;
			goto IL_3E;
		}
		num2 = distance;
		IL_3E:
		switch (toUnit)
		{
		case DistanceUnit.Pixels:
			return num2;
		case DistanceUnit.Inches:
			return num2 / num;
		case DistanceUnit.Centimeters:
			return num2 / num * 2.54f;
		default:
			return num2;
		}
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00004B5C File Offset: 0x00002D5C
	public static Vector2 Convert(Vector2 v, DistanceUnit fromUnit, DistanceUnit toUnit)
	{
		return new Vector2(FingerGestures.Convert(v.x, fromUnit, toUnit), FingerGestures.Convert(v.y, fromUnit, toUnit));
	}

	// Token: 0x04000078 RID: 120
	private const float DESKTOP_SCREEN_STANDARD_DPI = 96f;

	// Token: 0x04000079 RID: 121
	private const float INCHES_TO_CENTIMETERS = 2.54f;

	// Token: 0x0400007A RID: 122
	private const float CENTIMETERS_TO_INCHES = 0.39370078f;

	// Token: 0x0400007B RID: 123
	public static readonly RuntimePlatform[] TouchScreenPlatforms = new RuntimePlatform[]
	{
		RuntimePlatform.IPhonePlayer,
		RuntimePlatform.Android,
		RuntimePlatform.BB10Player,
		RuntimePlatform.WP8Player
	};

	// Token: 0x0400007C RID: 124
	public bool makePersistent = true;

	// Token: 0x0400007D RID: 125
	public bool detectUnityRemote = true;

	// Token: 0x0400007E RID: 126
	public FGInputProvider mouseInputProviderPrefab;

	// Token: 0x0400007F RID: 127
	public FGInputProvider touchInputProviderPrefab;

	// Token: 0x04000080 RID: 128
	private FingerClusterManager fingerClusterManager;

	// Token: 0x04000081 RID: 129
	private FGInputProvider inputProvider;

	// Token: 0x04000082 RID: 130
	private static List<GestureRecognizer> gestureRecognizers = new List<GestureRecognizer>();

	// Token: 0x04000083 RID: 131
	private static FingerGestures instance;

	// Token: 0x04000084 RID: 132
	private FingerGestures.Finger[] fingers;

	// Token: 0x04000085 RID: 133
	private FingerGestures.FingerList touches;

	// Token: 0x04000086 RID: 134
	private FingerGestures.GlobalTouchFilterDelegate globalTouchFilterFunc;

	// Token: 0x04000087 RID: 135
	private Transform[] fingerNodes;

	// Token: 0x04000088 RID: 136
	private static readonly FingerGestures.SwipeDirection[] AngleToDirectionMap = new FingerGestures.SwipeDirection[]
	{
		FingerGestures.SwipeDirection.Right,
		FingerGestures.SwipeDirection.UpperRightDiagonal,
		FingerGestures.SwipeDirection.Up,
		FingerGestures.SwipeDirection.UpperLeftDiagonal,
		FingerGestures.SwipeDirection.Left,
		FingerGestures.SwipeDirection.LowerLeftDiagonal,
		FingerGestures.SwipeDirection.Down,
		FingerGestures.SwipeDirection.LowerRightDiagonal
	};

	// Token: 0x04000089 RID: 137
	private static float screenDPI = 0f;

	// Token: 0x02000022 RID: 34
	public enum FingerPhase
	{
		// Token: 0x0400008E RID: 142
		None,
		// Token: 0x0400008F RID: 143
		Begin,
		// Token: 0x04000090 RID: 144
		Moving,
		// Token: 0x04000091 RID: 145
		Stationary
	}

	// Token: 0x02000023 RID: 35
	public class InputProviderEvent
	{
		// Token: 0x04000092 RID: 146
		public FGInputProvider inputProviderPrefab;
	}

	// Token: 0x02000024 RID: 36
	public class Finger
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00004B88 File Offset: 0x00002D88
		public Finger(int index)
		{
			this.index = index;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004BEC File Offset: 0x00002DEC
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public bool IsDown
		{
			get
			{
				return this.phase != FingerGestures.FingerPhase.None;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004C04 File Offset: 0x00002E04
		public FingerGestures.FingerPhase Phase
		{
			get
			{
				return this.phase;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004C0C File Offset: 0x00002E0C
		public FingerGestures.FingerPhase PreviousPhase
		{
			get
			{
				return this.prevPhase;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004C14 File Offset: 0x00002E14
		public bool WasDown
		{
			get
			{
				return this.prevPhase != FingerGestures.FingerPhase.None;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004C24 File Offset: 0x00002E24
		public bool IsMoving
		{
			get
			{
				return this.phase == FingerGestures.FingerPhase.Moving;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004C30 File Offset: 0x00002E30
		public bool WasMoving
		{
			get
			{
				return this.prevPhase == FingerGestures.FingerPhase.Moving;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004C3C File Offset: 0x00002E3C
		public bool IsStationary
		{
			get
			{
				return this.phase == FingerGestures.FingerPhase.Stationary;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004C48 File Offset: 0x00002E48
		public bool WasStationary
		{
			get
			{
				return this.prevPhase == FingerGestures.FingerPhase.Stationary;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004C54 File Offset: 0x00002E54
		public bool Moved
		{
			get
			{
				return this.moved;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004C5C File Offset: 0x00002E5C
		public float StarTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004C64 File Offset: 0x00002E64
		public Vector2 StartPosition
		{
			get
			{
				return this.startPos;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004C6C File Offset: 0x00002E6C
		public Vector2 Position
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004C74 File Offset: 0x00002E74
		public Vector2 PreviousPosition
		{
			get
			{
				return this.prevPos;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004C7C File Offset: 0x00002E7C
		public Vector2 DeltaPosition
		{
			get
			{
				return this.deltaPos;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004C84 File Offset: 0x00002E84
		public float DistanceFromStart
		{
			get
			{
				return this.distFromStart;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004C8C File Offset: 0x00002E8C
		public bool IsFiltered
		{
			get
			{
				return this.filteredOut;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004C94 File Offset: 0x00002E94
		public float TimeStationary
		{
			get
			{
				return this.elapsedTimeStationary;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004C9C File Offset: 0x00002E9C
		public List<GestureRecognizer> GestureRecognizers
		{
			get
			{
				return this.gestureRecognizers;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004CA4 File Offset: 0x00002EA4
		public Dictionary<string, object> ExtendedProperties
		{
			get
			{
				return this.extendedProperties;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004CAC File Offset: 0x00002EAC
		public override string ToString()
		{
			return "Finger" + this.index;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004CC4 File Offset: 0x00002EC4
		internal void Update(bool newDownState, Vector2 newPos)
		{
			if (this.filteredOut && !newDownState)
			{
				this.filteredOut = false;
			}
			if (!this.IsDown && newDownState && !FingerGestures.instance.ShouldProcessTouch(this.index, newPos))
			{
				this.filteredOut = true;
				newDownState = false;
			}
			this.prevPhase = this.phase;
			if (newDownState)
			{
				if (!this.WasDown)
				{
					this.phase = FingerGestures.FingerPhase.Begin;
					this.pos = newPos;
					this.startPos = this.pos;
					this.prevPos = this.pos;
					this.deltaPos = Vector2.zero;
					this.moved = false;
					this.lastMoveTime = 0f;
					this.startTime = Time.time;
					this.elapsedTimeStationary = 0f;
					this.distFromStart = 0f;
				}
				else
				{
					this.prevPos = this.pos;
					this.pos = newPos;
					this.distFromStart = Vector3.Distance(this.startPos, this.pos);
					this.deltaPos = this.pos - this.prevPos;
					if (this.deltaPos.sqrMagnitude > 0f)
					{
						this.lastMoveTime = Time.time;
						this.phase = FingerGestures.FingerPhase.Moving;
					}
					else if (!this.IsMoving || Time.time - this.lastMoveTime > 0.05f)
					{
						this.phase = FingerGestures.FingerPhase.Stationary;
					}
					if (this.IsMoving)
					{
						this.moved = true;
					}
					else if (!this.WasStationary)
					{
						this.elapsedTimeStationary = 0f;
					}
					else
					{
						this.elapsedTimeStationary += Time.deltaTime;
					}
				}
			}
			else
			{
				this.phase = FingerGestures.FingerPhase.None;
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004E90 File Offset: 0x00003090
		public static implicit operator bool(FingerGestures.Finger finger)
		{
			return finger != null;
		}

		// Token: 0x04000093 RID: 147
		private int index;

		// Token: 0x04000094 RID: 148
		private FingerGestures.FingerPhase phase;

		// Token: 0x04000095 RID: 149
		private FingerGestures.FingerPhase prevPhase;

		// Token: 0x04000096 RID: 150
		private Vector2 pos = Vector2.zero;

		// Token: 0x04000097 RID: 151
		private Vector2 startPos = Vector2.zero;

		// Token: 0x04000098 RID: 152
		private Vector2 prevPos = Vector2.zero;

		// Token: 0x04000099 RID: 153
		private Vector2 deltaPos = Vector2.zero;

		// Token: 0x0400009A RID: 154
		private float startTime;

		// Token: 0x0400009B RID: 155
		private float lastMoveTime;

		// Token: 0x0400009C RID: 156
		private float distFromStart;

		// Token: 0x0400009D RID: 157
		private bool moved;

		// Token: 0x0400009E RID: 158
		private bool filteredOut = true;

		// Token: 0x0400009F RID: 159
		private Collider collider;

		// Token: 0x040000A0 RID: 160
		private Collider prevCollider;

		// Token: 0x040000A1 RID: 161
		private float elapsedTimeStationary;

		// Token: 0x040000A2 RID: 162
		private List<GestureRecognizer> gestureRecognizers = new List<GestureRecognizer>();

		// Token: 0x040000A3 RID: 163
		private Dictionary<string, object> extendedProperties = new Dictionary<string, object>();
	}

	// Token: 0x02000025 RID: 37
	public interface IFingerList : IEnumerable<FingerGestures.Finger>, IEnumerable
	{
		// Token: 0x1700003C RID: 60
		FingerGestures.Finger this[int index]
		{
			get;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F7 RID: 247
		int Count { get; }

		// Token: 0x060000F8 RID: 248
		Vector2 GetAverageStartPosition();

		// Token: 0x060000F9 RID: 249
		Vector2 GetAveragePosition();

		// Token: 0x060000FA RID: 250
		Vector2 GetAveragePreviousPosition();

		// Token: 0x060000FB RID: 251
		float GetAverageDistanceFromStart();

		// Token: 0x060000FC RID: 252
		FingerGestures.Finger GetOldest();

		// Token: 0x060000FD RID: 253
		bool AllMoving();

		// Token: 0x060000FE RID: 254
		bool MovingInSameDirection(float tolerance);
	}

	// Token: 0x02000026 RID: 38
	[Serializable]
	public class FingerList : IEnumerable<FingerGestures.Finger>, FingerGestures.IFingerList, IEnumerable
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00004E9C File Offset: 0x0000309C
		public FingerList()
		{
			this.list = new List<FingerGestures.Finger>();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004EB0 File Offset: 0x000030B0
		public FingerList(List<FingerGestures.Finger> list)
		{
			this.list = list;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004F14 File Offset: 0x00003114
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700003E RID: 62
		public FingerGestures.Finger this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004F2C File Offset: 0x0000312C
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004F3C File Offset: 0x0000313C
		public IEnumerator<FingerGestures.Finger> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004F50 File Offset: 0x00003150
		public void Add(FingerGestures.Finger touch)
		{
			this.list.Add(touch);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004F60 File Offset: 0x00003160
		public bool Remove(FingerGestures.Finger touch)
		{
			return this.list.Remove(touch);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004F70 File Offset: 0x00003170
		public bool Contains(FingerGestures.Finger touch)
		{
			return this.list.Contains(touch);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004F80 File Offset: 0x00003180
		public void AddRange(IEnumerable<FingerGestures.Finger> touches)
		{
			this.list.AddRange(touches);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004F90 File Offset: 0x00003190
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004FA0 File Offset: 0x000031A0
		public Vector2 AverageVector(FingerGestures.FingerList.FingerPropertyGetterDelegate<Vector2> getProperty)
		{
			Vector2 vector = Vector2.zero;
			if (this.Count > 0)
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					vector += getProperty(this.list[i]);
				}
				vector /= (float)this.Count;
			}
			return vector;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005004 File Offset: 0x00003204
		public float AverageFloat(FingerGestures.FingerList.FingerPropertyGetterDelegate<float> getProperty)
		{
			float num = 0f;
			if (this.Count > 0)
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					num += getProperty(this.list[i]);
				}
				num /= (float)this.Count;
			}
			return num;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005060 File Offset: 0x00003260
		private static Vector2 GetFingerStartPosition(FingerGestures.Finger finger)
		{
			return finger.StartPosition;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005068 File Offset: 0x00003268
		private static Vector2 GetFingerPosition(FingerGestures.Finger finger)
		{
			return finger.Position;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005070 File Offset: 0x00003270
		private static Vector2 GetFingerPreviousPosition(FingerGestures.Finger finger)
		{
			return finger.PreviousPosition;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005078 File Offset: 0x00003278
		private static float GetFingerDistanceFromStart(FingerGestures.Finger finger)
		{
			return finger.DistanceFromStart;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005080 File Offset: 0x00003280
		public Vector2 GetAverageStartPosition()
		{
			return this.AverageVector(FingerGestures.FingerList.delGetFingerStartPosition);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005090 File Offset: 0x00003290
		public Vector2 GetAveragePosition()
		{
			return this.AverageVector(FingerGestures.FingerList.delGetFingerPosition);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000050A0 File Offset: 0x000032A0
		public Vector2 GetAveragePreviousPosition()
		{
			return this.AverageVector(FingerGestures.FingerList.delGetFingerPreviousPosition);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000050B0 File Offset: 0x000032B0
		public float GetAverageDistanceFromStart()
		{
			return this.AverageFloat(FingerGestures.FingerList.delGetFingerDistanceFromStart);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000050C0 File Offset: 0x000032C0
		public FingerGestures.Finger GetOldest()
		{
			FingerGestures.Finger finger = null;
			foreach (FingerGestures.Finger finger2 in this.list)
			{
				if (finger == null || finger2.StarTime < finger.StarTime)
				{
					finger = finger2;
				}
			}
			return finger;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000513C File Offset: 0x0000333C
		public bool MovingInSameDirection(float tolerance)
		{
			if (this.Count < 2)
			{
				return true;
			}
			float num = Mathf.Max(0.1f, 1f - tolerance);
			Vector2 lhs = this[0].Position - this[0].StartPosition;
			lhs.Normalize();
			for (int i = 1; i < this.Count; i++)
			{
				Vector2 rhs = this[i].Position - this[i].StartPosition;
				rhs.Normalize();
				if (Vector2.Dot(lhs, rhs) < num)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000051DC File Offset: 0x000033DC
		public bool AllMoving()
		{
			if (this.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < this.list.Count; i++)
			{
				if (!this.list[i].IsMoving)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040000A4 RID: 164
		[SerializeField]
		private List<FingerGestures.Finger> list;

		// Token: 0x040000A5 RID: 165
		private static FingerGestures.FingerList.FingerPropertyGetterDelegate<Vector2> delGetFingerStartPosition = new FingerGestures.FingerList.FingerPropertyGetterDelegate<Vector2>(FingerGestures.FingerList.GetFingerStartPosition);

		// Token: 0x040000A6 RID: 166
		private static FingerGestures.FingerList.FingerPropertyGetterDelegate<Vector2> delGetFingerPosition = new FingerGestures.FingerList.FingerPropertyGetterDelegate<Vector2>(FingerGestures.FingerList.GetFingerPosition);

		// Token: 0x040000A7 RID: 167
		private static FingerGestures.FingerList.FingerPropertyGetterDelegate<Vector2> delGetFingerPreviousPosition = new FingerGestures.FingerList.FingerPropertyGetterDelegate<Vector2>(FingerGestures.FingerList.GetFingerPreviousPosition);

		// Token: 0x040000A8 RID: 168
		private static FingerGestures.FingerList.FingerPropertyGetterDelegate<float> delGetFingerDistanceFromStart = new FingerGestures.FingerList.FingerPropertyGetterDelegate<float>(FingerGestures.FingerList.GetFingerDistanceFromStart);

		// Token: 0x02000064 RID: 100
		// (Invoke) Token: 0x06000283 RID: 643
		public delegate T FingerPropertyGetterDelegate<T>(FingerGestures.Finger finger);
	}

	// Token: 0x02000027 RID: 39
	[Flags]
	public enum SwipeDirection
	{
		// Token: 0x040000AA RID: 170
		Right = 1,
		// Token: 0x040000AB RID: 171
		Left = 2,
		// Token: 0x040000AC RID: 172
		Up = 4,
		// Token: 0x040000AD RID: 173
		Down = 8,
		// Token: 0x040000AE RID: 174
		UpperLeftDiagonal = 16,
		// Token: 0x040000AF RID: 175
		UpperRightDiagonal = 32,
		// Token: 0x040000B0 RID: 176
		LowerRightDiagonal = 64,
		// Token: 0x040000B1 RID: 177
		LowerLeftDiagonal = 128,
		// Token: 0x040000B2 RID: 178
		None = 0,
		// Token: 0x040000B3 RID: 179
		Vertical = 12,
		// Token: 0x040000B4 RID: 180
		Horizontal = 3,
		// Token: 0x040000B5 RID: 181
		Cross = 15,
		// Token: 0x040000B6 RID: 182
		UpperDiagonals = 48,
		// Token: 0x040000B7 RID: 183
		LowerDiagonals = 192,
		// Token: 0x040000B8 RID: 184
		Diagonals = 240,
		// Token: 0x040000B9 RID: 185
		All = 255
	}

	// Token: 0x02000065 RID: 101
	// (Invoke) Token: 0x06000287 RID: 647
	public delegate void EventHandler();

	// Token: 0x02000066 RID: 102
	// (Invoke) Token: 0x0600028B RID: 651
	public delegate bool GlobalTouchFilterDelegate(int fingerIndex, Vector2 position);
}
