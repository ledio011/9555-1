using System;
using System.Collections;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001AD RID: 429
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ModuleBuilder))]
	[ComVisible(true)]
	public class ModuleBuilder : Module, _ModuleBuilder
	{
		// Token: 0x0600105C RID: 4188 RVA: 0x0003E138 File Offset: 0x0003C338
		internal ModuleBuilder(AssemblyBuilder assb, string name, string fullyqname, bool emitSymbolInfo, bool transient)
		{
			this.scopename = name;
			this.name = name;
			this.fqname = fullyqname;
			this.assemblyb = assb;
			this.assembly = assb;
			this.transient = transient;
			this.guid = Guid.FastNewGuidArray();
			this.table_idx = this.get_next_table_index(this, 0, true);
			this.name_cache = new Hashtable();
			ModuleBuilder.basic_init(this);
			this.CreateGlobalType();
			if (assb.IsRun)
			{
				TypeBuilder typeBuilder = new TypeBuilder(this, TypeAttributes.Abstract, 16777215);
				Type ab = typeBuilder.CreateType();
				ModuleBuilder.set_wrappers_type(this, ab);
			}
			if (emitSymbolInfo)
			{
				Assembly assembly = Assembly.LoadWithPartialName("Mono.CompilerServices.SymbolWriter");
				if (assembly == null)
				{
					throw new ExecutionEngineException("The assembly for default symbol writer cannot be loaded");
				}
				Type type = assembly.GetType("Mono.CompilerServices.SymbolWriter.SymbolWriterImpl");
				if (type == null)
				{
					throw new ExecutionEngineException("The type that implements the default symbol writer interface cannot be found");
				}
				this.symbolWriter = (ISymbolWriter)Activator.CreateInstance(type, new object[]
				{
					this
				});
				string text = this.fqname;
				if (this.assemblyb.AssemblyDir != null)
				{
					text = Path.Combine(this.assemblyb.AssemblyDir, text);
				}
				this.symbolWriter.Initialize(IntPtr.Zero, text, true);
			}
		}

		// Token: 0x0600105E RID: 4190
		[MethodImpl(4096)]
		private static extern void basic_init(ModuleBuilder ab);

		// Token: 0x0600105F RID: 4191
		[MethodImpl(4096)]
		private static extern void set_wrappers_type(ModuleBuilder mb, Type ab);

		// Token: 0x06001060 RID: 4192 RVA: 0x0003E29C File Offset: 0x0003C49C
		public bool IsTransient()
		{
			return this.transient;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0003E2A4 File Offset: 0x0003C4A4
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent)
		{
			return this.DefineType(name, attr, parent, null);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0003E2B0 File Offset: 0x0003C4B0
		private void AddType(TypeBuilder tb)
		{
			if (this.types != null)
			{
				if (this.types.Length == this.num_types)
				{
					TypeBuilder[] destinationArray = new TypeBuilder[this.types.Length * 2];
					Array.Copy(this.types, destinationArray, this.num_types);
					this.types = destinationArray;
				}
			}
			else
			{
				this.types = new TypeBuilder[1];
			}
			this.types[this.num_types] = tb;
			this.num_types++;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0003E334 File Offset: 0x0003C534
		private TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packingSize, int typesize)
		{
			if (this.name_cache.ContainsKey(name))
			{
				throw new ArgumentException("Duplicate type name within an assembly.");
			}
			TypeBuilder typeBuilder = new TypeBuilder(this, name, attr, parent, interfaces, packingSize, typesize, null);
			this.AddType(typeBuilder);
			this.name_cache.Add(name, typeBuilder);
			return typeBuilder;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0003E384 File Offset: 0x0003C584
		[ComVisible(true)]
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
		{
			return this.DefineType(name, attr, parent, interfaces, PackingSize.Unspecified, 0);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0003E394 File Offset: 0x0003C594
		[ComVisible(true)]
		public override Type GetType(string className)
		{
			return this.GetType(className, false, false);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0003E3A0 File Offset: 0x0003C5A0
		private TypeBuilder search_in_array(TypeBuilder[] arr, int validElementsInArray, string className)
		{
			for (int i = 0; i < validElementsInArray; i++)
			{
				if (string.Compare(className, arr[i].FullName, true, CultureInfo.InvariantCulture) == 0)
				{
					return arr[i];
				}
			}
			return null;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0003E3E0 File Offset: 0x0003C5E0
		private TypeBuilder search_nested_in_array(TypeBuilder[] arr, int validElementsInArray, string className)
		{
			for (int i = 0; i < validElementsInArray; i++)
			{
				if (string.Compare(className, arr[i].Name, true, CultureInfo.InvariantCulture) == 0)
				{
					return arr[i];
				}
			}
			return null;
		}

		// Token: 0x06001068 RID: 4200
		[MethodImpl(4096)]
		private static extern Type create_modified_type(TypeBuilder tb, string modifiers);

		// Token: 0x06001069 RID: 4201 RVA: 0x0003E420 File Offset: 0x0003C620
		private TypeBuilder GetMaybeNested(TypeBuilder t, string className)
		{
			int num = className.IndexOf('+');
			if (num >= 0)
			{
				if (t.subtypes != null)
				{
					string className2 = className.Substring(0, num);
					string className3 = className.Substring(num + 1);
					TypeBuilder typeBuilder = this.search_nested_in_array(t.subtypes, t.subtypes.Length, className2);
					if (typeBuilder != null)
					{
						return this.GetMaybeNested(typeBuilder, className3);
					}
				}
				return null;
			}
			if (t.subtypes != null)
			{
				return this.search_nested_in_array(t.subtypes, t.subtypes.Length, className);
			}
			return null;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0003E4A4 File Offset: 0x0003C6A4
		[ComVisible(true)]
		public override Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			if (className.Length == 0)
			{
				throw new ArgumentException("className");
			}
			string message = className;
			TypeBuilder typeBuilder = null;
			if (this.types == null && throwOnError)
			{
				throw new TypeLoadException(className);
			}
			int num = className.IndexOfAny(ModuleBuilder.type_modifiers);
			string text;
			if (num >= 0)
			{
				text = className.Substring(num);
				className = className.Substring(0, num);
			}
			else
			{
				text = null;
			}
			if (!ignoreCase)
			{
				typeBuilder = (this.name_cache[className] as TypeBuilder);
			}
			else
			{
				num = className.IndexOf('+');
				if (num < 0)
				{
					if (this.types != null)
					{
						typeBuilder = this.search_in_array(this.types, this.num_types, className);
					}
				}
				else
				{
					string className2 = className.Substring(0, num);
					string className3 = className.Substring(num + 1);
					typeBuilder = this.search_in_array(this.types, this.num_types, className2);
					if (typeBuilder != null)
					{
						typeBuilder = this.GetMaybeNested(typeBuilder, className3);
					}
				}
			}
			if (typeBuilder == null && throwOnError)
			{
				throw new TypeLoadException(message);
			}
			if (typeBuilder != null && text != null)
			{
				Type type = ModuleBuilder.create_modified_type(typeBuilder, text);
				typeBuilder = (type as TypeBuilder);
				if (typeBuilder == null)
				{
					return type;
				}
			}
			if (typeBuilder != null && typeBuilder.is_created)
			{
				return typeBuilder.CreateType();
			}
			return typeBuilder;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0003E600 File Offset: 0x0003C800
		internal int get_next_table_index(object obj, int table, bool inc)
		{
			if (this.table_indexes == null)
			{
				this.table_indexes = new int[64];
				for (int i = 0; i < 64; i++)
				{
					this.table_indexes[i] = 1;
				}
				this.table_indexes[2] = 2;
			}
			if (inc)
			{
				return this.table_indexes[table]++;
			}
			return this.table_indexes[table];
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0003E670 File Offset: 0x0003C870
		public override Type[] GetTypes()
		{
			if (this.types == null)
			{
				return Type.EmptyTypes;
			}
			int num = this.num_types;
			Type[] array = new Type[num];
			Array.Copy(this.types, array, num);
			for (int i = 0; i < array.Length; i++)
			{
				if (this.types[i].is_created)
				{
					array[i] = this.types[i].CreateType();
				}
			}
			return array;
		}

		// Token: 0x0600106D RID: 4205
		[MethodImpl(4096)]
		private static extern int getUSIndex(ModuleBuilder mb, string str);

		// Token: 0x0600106E RID: 4206
		[MethodImpl(4096)]
		private static extern int getToken(ModuleBuilder mb, object obj);

		// Token: 0x0600106F RID: 4207
		[MethodImpl(4096)]
		private static extern int getMethodToken(ModuleBuilder mb, MethodInfo method, Type[] opt_param_types);

		// Token: 0x06001070 RID: 4208 RVA: 0x0003E6E0 File Offset: 0x0003C8E0
		internal int GetToken(string str)
		{
			if (this.us_string_cache.Contains(str))
			{
				return (int)this.us_string_cache[str];
			}
			int usindex = ModuleBuilder.getUSIndex(this, str);
			this.us_string_cache[str] = usindex;
			return usindex;
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0003E72C File Offset: 0x0003C92C
		internal int GetToken(MemberInfo member)
		{
			return ModuleBuilder.getToken(this, member);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0003E738 File Offset: 0x0003C938
		internal int GetToken(MethodInfo method, Type[] opt_param_types)
		{
			return ModuleBuilder.getMethodToken(this, method, opt_param_types);
		}

		// Token: 0x06001073 RID: 4211
		[MethodImpl(4096)]
		internal extern void RegisterToken(object obj, int token);

		// Token: 0x06001074 RID: 4212 RVA: 0x0003E744 File Offset: 0x0003C944
		internal TokenGenerator GetTokenGenerator()
		{
			if (this.token_gen == null)
			{
				this.token_gen = new ModuleBuilderTokenGenerator(this);
			}
			return this.token_gen;
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x0003E764 File Offset: 0x0003C964
		internal string FileName
		{
			get
			{
				return this.fqname;
			}
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0003E76C File Offset: 0x0003C96C
		internal void CreateGlobalType()
		{
			if (this.global_type == null)
			{
				this.global_type = new TypeBuilder(this, TypeAttributes.NotPublic, 1);
			}
		}

		// Token: 0x040006F8 RID: 1784
		private UIntPtr dynamic_image;

		// Token: 0x040006F9 RID: 1785
		private int num_types;

		// Token: 0x040006FA RID: 1786
		private TypeBuilder[] types;

		// Token: 0x040006FB RID: 1787
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040006FC RID: 1788
		private byte[] guid;

		// Token: 0x040006FD RID: 1789
		private int table_idx;

		// Token: 0x040006FE RID: 1790
		internal AssemblyBuilder assemblyb;

		// Token: 0x040006FF RID: 1791
		private MethodBuilder[] global_methods;

		// Token: 0x04000700 RID: 1792
		private FieldBuilder[] global_fields;

		// Token: 0x04000701 RID: 1793
		private bool is_main;

		// Token: 0x04000702 RID: 1794
		private MonoResource[] resources;

		// Token: 0x04000703 RID: 1795
		private TypeBuilder global_type;

		// Token: 0x04000704 RID: 1796
		private Type global_type_created;

		// Token: 0x04000705 RID: 1797
		private Hashtable name_cache;

		// Token: 0x04000706 RID: 1798
		private Hashtable us_string_cache = new Hashtable();

		// Token: 0x04000707 RID: 1799
		private int[] table_indexes;

		// Token: 0x04000708 RID: 1800
		private bool transient;

		// Token: 0x04000709 RID: 1801
		private ModuleBuilderTokenGenerator token_gen;

		// Token: 0x0400070A RID: 1802
		private Hashtable resource_writers;

		// Token: 0x0400070B RID: 1803
		private ISymbolWriter symbolWriter;

		// Token: 0x0400070C RID: 1804
		private static readonly char[] type_modifiers = new char[]
		{
			'&',
			'[',
			'*'
		};
	}
}
