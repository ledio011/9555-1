using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

// Token: 0x02000092 RID: 146
[Serializable]
public class PropertyReference
{
	// Token: 0x0600039B RID: 923 RVA: 0x0001AD54 File Offset: 0x00018F54
	public PropertyReference()
	{
	}

	// Token: 0x0600039C RID: 924 RVA: 0x0001AD5C File Offset: 0x00018F5C
	public PropertyReference(Component target, string fieldName)
	{
		this.mTarget = target;
		this.mName = fieldName;
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x0600039E RID: 926 RVA: 0x0001AD88 File Offset: 0x00018F88
	// (set) Token: 0x0600039F RID: 927 RVA: 0x0001AD90 File Offset: 0x00018F90
	public Component target
	{
		get
		{
			return this.mTarget;
		}
		set
		{
			this.mTarget = value;
			this.mProperty = null;
			this.mField = null;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x060003A0 RID: 928 RVA: 0x0001ADA8 File Offset: 0x00018FA8
	// (set) Token: 0x060003A1 RID: 929 RVA: 0x0001ADB0 File Offset: 0x00018FB0
	public string name
	{
		get
		{
			return this.mName;
		}
		set
		{
			this.mName = value;
			this.mProperty = null;
			this.mField = null;
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x060003A2 RID: 930 RVA: 0x0001ADC8 File Offset: 0x00018FC8
	public bool isValid
	{
		get
		{
			return this.mTarget != null && !string.IsNullOrEmpty(this.mName);
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x060003A3 RID: 931 RVA: 0x0001ADF8 File Offset: 0x00018FF8
	public bool isEnabled
	{
		get
		{
			if (this.mTarget == null)
			{
				return false;
			}
			MonoBehaviour monoBehaviour = this.mTarget as MonoBehaviour;
			return monoBehaviour == null || monoBehaviour.enabled;
		}
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x0001AE3C File Offset: 0x0001903C
	public Type GetPropertyType()
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty != null)
		{
			return this.mProperty.PropertyType;
		}
		if (this.mField != null)
		{
			return this.mField.FieldType;
		}
		return typeof(void);
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x0001AEAC File Offset: 0x000190AC
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return !this.isValid;
		}
		if (obj is PropertyReference)
		{
			PropertyReference propertyReference = obj as PropertyReference;
			return this.mTarget == propertyReference.mTarget && string.Equals(this.mName, propertyReference.mName);
		}
		return false;
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x0001AF08 File Offset: 0x00019108
	public override int GetHashCode()
	{
		return PropertyReference.s_Hash;
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x0001AF10 File Offset: 0x00019110
	public void Set(Component target, string methodName)
	{
		this.mTarget = target;
		this.mName = methodName;
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x0001AF20 File Offset: 0x00019120
	public void Clear()
	{
		this.mTarget = null;
		this.mName = null;
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x0001AF30 File Offset: 0x00019130
	public void Reset()
	{
		this.mField = null;
		this.mProperty = null;
	}

	// Token: 0x060003AA RID: 938 RVA: 0x0001AF40 File Offset: 0x00019140
	public override string ToString()
	{
		return PropertyReference.ToString(this.mTarget, this.name);
	}

	// Token: 0x060003AB RID: 939 RVA: 0x0001AF54 File Offset: 0x00019154
	public static string ToString(Component comp, string property)
	{
		if (!(comp != null))
		{
			return null;
		}
		string text = comp.GetType().ToString();
		int num = text.LastIndexOf('.');
		if (num > 0)
		{
			text = text.Substring(num + 1);
		}
		if (!string.IsNullOrEmpty(property))
		{
			return text + "." + property;
		}
		return text + ".[property]";
	}

	// Token: 0x060003AC RID: 940 RVA: 0x0001AFB8 File Offset: 0x000191B8
	[DebuggerHidden]
	[DebuggerStepThrough]
	public object Get()
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty != null)
		{
			if (this.mProperty.CanRead)
			{
				return this.mProperty.GetValue(this.mTarget, null);
			}
		}
		else if (this.mField != null)
		{
			return this.mField.GetValue(this.mTarget);
		}
		return null;
	}

	// Token: 0x060003AD RID: 941 RVA: 0x0001B040 File Offset: 0x00019240
	[DebuggerHidden]
	[DebuggerStepThrough]
	public bool Set(object value)
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty == null && this.mField == null)
		{
			return false;
		}
		if (value == null)
		{
			try
			{
				if (this.mProperty != null)
				{
					this.mProperty.SetValue(this.mTarget, null, null);
				}
				else
				{
					this.mField.SetValue(this.mTarget, null);
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
		if (!this.Convert(ref value))
		{
			if (Application.isPlaying)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Unable to convert ",
					value.GetType(),
					" to ",
					this.GetPropertyType()
				}));
			}
		}
		else
		{
			if (this.mField != null)
			{
				this.mField.SetValue(this.mTarget, value);
				return true;
			}
			if (this.mProperty.CanWrite)
			{
				this.mProperty.SetValue(this.mTarget, value, null);
				return true;
			}
		}
		return false;
	}

	// Token: 0x060003AE RID: 942 RVA: 0x0001B18C File Offset: 0x0001938C
	[DebuggerHidden]
	[DebuggerStepThrough]
	private bool Cache()
	{
		if (this.mTarget != null && !string.IsNullOrEmpty(this.mName))
		{
			Type type = this.mTarget.GetType();
			this.mField = type.GetField(this.mName);
			this.mProperty = type.GetProperty(this.mName);
		}
		else
		{
			this.mField = null;
			this.mProperty = null;
		}
		return this.mField != null || this.mProperty != null;
	}

	// Token: 0x060003AF RID: 943 RVA: 0x0001B218 File Offset: 0x00019418
	private bool Convert(ref object value)
	{
		if (this.mTarget == null)
		{
			return false;
		}
		Type propertyType = this.GetPropertyType();
		Type from;
		if (value == null)
		{
			if (!propertyType.IsClass)
			{
				return false;
			}
			from = propertyType;
		}
		else
		{
			from = value.GetType();
		}
		return PropertyReference.Convert(ref value, from, propertyType);
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x0001B26C File Offset: 0x0001946C
	public static bool Convert(Type from, Type to)
	{
		object obj = null;
		return PropertyReference.Convert(ref obj, from, to);
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x0001B284 File Offset: 0x00019484
	public static bool Convert(object value, Type to)
	{
		if (value == null)
		{
			value = null;
			return PropertyReference.Convert(ref value, to, to);
		}
		return PropertyReference.Convert(ref value, value.GetType(), to);
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x0001B2A8 File Offset: 0x000194A8
	public static bool Convert(ref object value, Type from, Type to)
	{
		if (to.IsAssignableFrom(from))
		{
			return true;
		}
		if (to == typeof(string))
		{
			value = ((value == null) ? "null" : value.ToString());
			return true;
		}
		if (value == null)
		{
			return false;
		}
		float num2;
		if (to == typeof(int))
		{
			if (from == typeof(string))
			{
				int num;
				if (int.TryParse((string)value, ref num))
				{
					value = num;
					return true;
				}
			}
			else if (from == typeof(float))
			{
				value = Mathf.RoundToInt((float)value);
				return true;
			}
		}
		else if (to == typeof(float) && from == typeof(string) && float.TryParse((string)value, ref num2))
		{
			value = num2;
			return true;
		}
		return false;
	}

	// Token: 0x0400036A RID: 874
	[SerializeField]
	private Component mTarget;

	// Token: 0x0400036B RID: 875
	[SerializeField]
	private string mName;

	// Token: 0x0400036C RID: 876
	private FieldInfo mField;

	// Token: 0x0400036D RID: 877
	private PropertyInfo mProperty;

	// Token: 0x0400036E RID: 878
	private static int s_Hash = "PropertyBinding".GetHashCode();
}
