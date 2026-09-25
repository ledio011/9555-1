using System;
using UnityEngine;

// Token: 0x02000A8A RID: 2698
public class SchedulingDemo : MonoBehaviour
{
	// Token: 0x06004E6F RID: 20079 RVA: 0x001AD34C File Offset: 0x001AB54C
	private void Update()
	{
		base.transform.position = (Object.FindObjectOfType(typeof(Camera)) as Camera).ScreenToWorldPoint(new Vector3((float)(Screen.width - 165), (float)(Screen.height - 105), 6f));
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, this.m_CubeScale, Time.deltaTime * 5f);
		base.renderer.enabled = (base.transform.localScale.x > 0.01f);
		base.renderer.material.color = Color.Lerp(base.renderer.material.color, Color.red, Time.deltaTime * 3f);
	}

	// Token: 0x06004E70 RID: 20080 RVA: 0x001AD42C File Offset: 0x001AB62C
	private void OnGUI()
	{
		this.m_StatusColor = Color.Lerp(this.m_StatusColor, new Color(1f, 1f, 0f, 0f), Time.deltaTime * 0.5f);
		GUI.color = Color.white;
		GUILayout.Space(50f);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Space(50f);
		GUILayout.Label("SCHEDULING EXAMPLE\n    - Each of these examples will schedule some functionality in one (1)\n      second using different options.\n    - Please study the source code in 'Examples/Scheduling/Scheduling.cs' ...", new GUILayoutOption[0]);
		GUILayout.EndHorizontal();
		GUILayout.BeginArea(new Rect(100f, 150f, 400f, 600f));
		GUI.color = Color.white;
		GUILayout.Label("Methods, Arguments, Delegates, Iterations, Intervals & Canceling", new GUILayoutOption[0]);
		if (this.DoButton("A simple method", true))
		{
			vp_Timer.In(1f, new vp_Timer.Callback(this.DoMethod), null);
		}
		if (this.DoButton("A method with a single argument", true))
		{
			vp_Timer.In(1f, new vp_Timer.ArgCallback(this.DoMethodWithSingleArgument), 242, null);
		}
		if (this.DoButton("A method with multiple arguments", true))
		{
			object[] arguments = new object[]
			{
				"December",
				31,
				2012
			};
			vp_Timer.In(1f, new vp_Timer.ArgCallback(this.DoMethodWithMultipleArguments), arguments, null);
		}
		if (this.DoButton("A delegate", true))
		{
			vp_Timer.In(1f, delegate()
			{
				this.SmackCube();
			}, null);
		}
		if (this.DoButton("A delegate with a single argument", true))
		{
			vp_Timer.In(1f, delegate(object o)
			{
				int num = (int)o;
				this.SmackCube();
				this.SetStatus("A delegate with a single argument ... \"" + num + "\"");
			}, 242, null);
		}
		if (this.DoButton("A delegate with multiple arguments", true))
		{
			vp_Timer.In(1f, delegate(object o)
			{
				object[] array = (object[])o;
				string text = (string)array[0];
				int num = (int)array[1];
				int num2 = (int)array[2];
				this.SmackCube();
				this.SetStatus(string.Concat(new object[]
				{
					"A delegate with multiple arguments ... \"",
					text,
					" ",
					num,
					", ",
					num2,
					"\""
				}));
			}, new object[]
			{
				"December",
				31,
				2012
			}, null);
		}
		if (this.DoButton("5 iterations of a method", true))
		{
			vp_Timer.In(1f, new vp_Timer.Callback(this.SmackCube), 5, null);
		}
		if (this.DoButton("5 iterations of a method, with 0.2 sec intervals", true))
		{
			vp_Timer.In(1f, new vp_Timer.Callback(this.SmackCube), 5, 0.2f, null);
		}
		if (this.DoButton("5 iterations of a delegate, canceled after 3 seconds", true))
		{
			vp_Timer.Handle timer = new vp_Timer.Handle();
			vp_Timer.In(0f, delegate()
			{
				this.SmackCube();
			}, 5, 1f, timer);
			vp_Timer.In(3f, delegate()
			{
				timer.Cancel();
			}, null);
		}
		GUILayout.Label("\nMethod & object accessibility:", new GUILayoutOption[0]);
		if (this.DoButton("Running a method from a non-monobehaviour class", false))
		{
			vp_Timer.In(1f, delegate()
			{
				NonMonoBehaviour nonMonoBehaviour = new NonMonoBehaviour();
				nonMonoBehaviour.Test();
			}, null);
		}
		if (this.DoButton("Running a method from a specific external gameobject", false))
		{
			vp_Timer.In(1f, delegate()
			{
				TestComponent testComponent = (TestComponent)GameObject.Find("ExternalGameObject").GetComponent("TestComponent");
				testComponent.Test("Hello World!");
			}, null);
		}
		if (this.DoButton("Running a method from the first component of a certain type\nin current transform or any of its children", false))
		{
			vp_Timer.In(1f, delegate()
			{
				TestComponent componentInChildren = base.transform.root.GetComponentInChildren<TestComponent>();
				componentInChildren.Test("Hello World!");
			}, null);
		}
		if (this.DoButton("Running a method from the first component of a certain type\nin the whole Hierarchy", false))
		{
			vp_Timer.In(1f, delegate()
			{
				TestComponent testComponent = (TestComponent)Object.FindObjectOfType(typeof(TestComponent));
				testComponent.Test("Hello World!");
			}, null);
		}
		GUILayout.EndArea();
		GUI.color = this.m_StatusColor;
		GUILayout.BeginArea(new Rect((float)(Screen.width - 255), 205f, 240f, 600f));
		GUILayout.Label(this.m_StatusString, new GUILayoutOption[0]);
		GUILayout.EndArea();
	}

	// Token: 0x06004E71 RID: 20081 RVA: 0x001AD81C File Offset: 0x001ABA1C
	private void DoMethod()
	{
		this.SmackCube();
	}

	// Token: 0x06004E72 RID: 20082 RVA: 0x001AD824 File Offset: 0x001ABA24
	private void DoMethodWithSingleArgument(object o)
	{
		int num = (int)o;
		this.SmackCube();
		this.SetStatus("A method with a single argument ... \"" + num + "\"");
	}

	// Token: 0x06004E73 RID: 20083 RVA: 0x001AD85C File Offset: 0x001ABA5C
	private void DoMethodWithMultipleArguments(object o)
	{
		object[] array = (object[])o;
		string text = (string)array[0];
		int num = (int)array[1];
		int num2 = (int)array[2];
		this.SetStatus(string.Concat(new object[]
		{
			"A method with multiple arguments ... \"",
			text,
			", ",
			num,
			", ",
			num2,
			"\""
		}));
		this.SmackCube();
	}

	// Token: 0x06004E74 RID: 20084 RVA: 0x001AD8D8 File Offset: 0x001ABAD8
	public void SmackCube()
	{
		base.transform.localScale += new Vector3(0.6f, 0.6f, 0.6f);
		base.renderer.material.color = Color.yellow;
		Vector3 vector;
		vector..ctor(Random.Range(50f, 100f), Random.Range(50f, 100f), Random.Range(50f, 100f));
		if (Random.value < 0.5f)
		{
			vector.x = -vector.x;
		}
		if (Random.value < 0.5f)
		{
			vector.y = -vector.y;
		}
		if (Random.value < 0.5f)
		{
			vector.z = -vector.z;
		}
		base.rigidbody.maxAngularVelocity = 1000f;
		base.rigidbody.AddTorque(vector);
		base.audio.PlayOneShot(this.m_SmackSound);
	}

	// Token: 0x06004E75 RID: 20085 RVA: 0x001AD9E0 File Offset: 0x001ABBE0
	private bool DoButton(string s, bool showCube = true)
	{
		bool result = false;
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Space(30f);
		if (GUILayout.Button(s + ".", new GUILayoutOption[0]))
		{
			if (!showCube)
			{
				this.m_CubeScale = new Vector3(0.001f, 0.001f, 0.001f);
			}
			else
			{
				this.m_CubeScale = new Vector3(0.5f, 0.5f, 0.5f);
			}
			this.SetStatus(s + " ...");
			result = true;
		}
		GUILayout.EndHorizontal();
		return result;
	}

	// Token: 0x06004E76 RID: 20086 RVA: 0x001ADA78 File Offset: 0x001ABC78
	private void SetStatus(string s)
	{
		this.m_StatusString = s;
		this.m_StatusColor = Color.yellow;
	}

	// Token: 0x04003CF9 RID: 15609
	private Vector3 m_CubeScale = new Vector3(0.5f, 0.5f, 0.5f);

	// Token: 0x04003CFA RID: 15610
	private Color m_StatusColor = Color.white;

	// Token: 0x04003CFB RID: 15611
	private string m_StatusString = string.Empty;

	// Token: 0x04003CFC RID: 15612
	public AudioClip m_SmackSound;
}
