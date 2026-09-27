using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions.Syntax;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000081 RID: 129
	[Serializable]
	public class Regex : ISerializable
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000B7CC File Offset: 0x000099CC
		protected Regex()
		{
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000B7D4 File Offset: 0x000099D4
		public Regex(string pattern) : this(pattern, RegexOptions.None)
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000B7E0 File Offset: 0x000099E0
		public Regex(string pattern, RegexOptions options)
		{
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			Regex.validate_options(options);
			this.pattern = pattern;
			this.roptions = options;
			this.Init();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000B814 File Offset: 0x00009A14
		protected Regex(SerializationInfo info, StreamingContext context) : this(info.GetString("pattern"), (RegexOptions)((int)info.GetValue("options", typeof(RegexOptions))))
		{
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000B854 File Offset: 0x00009A54
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("pattern", this.ToString(), typeof(string));
			info.AddValue("options", this.Options, typeof(RegexOptions));
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000B894 File Offset: 0x00009A94
		public static bool IsMatch(string input, string pattern)
		{
			return Regex.IsMatch(input, pattern, RegexOptions.None);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000B8A0 File Offset: 0x00009AA0
		public static bool IsMatch(string input, string pattern, RegexOptions options)
		{
			Regex regex = new Regex(pattern, options);
			return regex.IsMatch(input);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000B8BC File Offset: 0x00009ABC
		public static Match Match(string input, string pattern)
		{
			return Regex.Match(input, pattern, RegexOptions.None);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		public static Match Match(string input, string pattern, RegexOptions options)
		{
			Regex regex = new Regex(pattern, options);
			return regex.Match(input);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		public static string Replace(string input, string pattern, string replacement)
		{
			return Regex.Replace(input, pattern, replacement, RegexOptions.None);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000B8F0 File Offset: 0x00009AF0
		public static string Replace(string input, string pattern, string replacement, RegexOptions options)
		{
			Regex regex = new Regex(pattern, options);
			return regex.Replace(input, replacement);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000B910 File Offset: 0x00009B10
		private static void validate_options(RegexOptions options)
		{
			if ((options & ~(RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.RightToLeft | RegexOptions.ECMAScript | RegexOptions.CultureInvariant)) != RegexOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if ((options & RegexOptions.ECMAScript) != RegexOptions.None && (options & ~(RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ECMAScript)) != RegexOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000B958 File Offset: 0x00009B58
		private void Init()
		{
			this.machineFactory = Regex.cache.Lookup(this.pattern, this.roptions);
			if (this.machineFactory == null)
			{
				this.InitNewRegex();
			}
			else
			{
				this.group_count = this.machineFactory.GroupCount;
				this.gap = this.machineFactory.Gap;
				this.mapping = this.machineFactory.Mapping;
				this.group_names = this.machineFactory.NamesMapping;
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000B9DC File Offset: 0x00009BDC
		private void InitNewRegex()
		{
			this.machineFactory = Regex.CreateMachineFactory(this.pattern, this.roptions);
			Regex.cache.Add(this.pattern, this.roptions, this.machineFactory);
			this.group_count = this.machineFactory.GroupCount;
			this.gap = this.machineFactory.Gap;
			this.mapping = this.machineFactory.Mapping;
			this.group_names = this.machineFactory.NamesMapping;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000BA60 File Offset: 0x00009C60
		private static IMachineFactory CreateMachineFactory(string pattern, RegexOptions options)
		{
			Parser parser = new Parser();
			RegularExpression regularExpression = parser.ParseRegularExpression(pattern, options);
			ICompiler compiler = new PatternCompiler();
			regularExpression.Compile(compiler, (options & RegexOptions.RightToLeft) != RegexOptions.None);
			IMachineFactory machineFactory = compiler.GetMachineFactory();
			Hashtable hashtable = new Hashtable();
			machineFactory.Gap = parser.GetMapping(hashtable);
			machineFactory.Mapping = hashtable;
			machineFactory.NamesMapping = Regex.GetGroupNamesArray(machineFactory.GroupCount, machineFactory.Mapping);
			return machineFactory;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		public RegexOptions Options
		{
			get
			{
				return this.roptions;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		public bool RightToLeft
		{
			get
			{
				return (this.roptions & RegexOptions.RightToLeft) != RegexOptions.None;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000BAEC File Offset: 0x00009CEC
		public int GroupNumberFromName(string name)
		{
			if (!this.mapping.Contains(name))
			{
				return -1;
			}
			int num = (int)this.mapping[name];
			if (num >= this.gap)
			{
				num = int.Parse(name);
			}
			return num;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000BB34 File Offset: 0x00009D34
		internal int GetGroupIndex(int number)
		{
			if (number < this.gap)
			{
				return number;
			}
			if (this.gap > this.group_count)
			{
				return -1;
			}
			return Array.BinarySearch<int>(this.GroupNumbers, this.gap, this.group_count - this.gap + 1, number);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000BB84 File Offset: 0x00009D84
		private int default_startat(string input)
		{
			return (!this.RightToLeft || input == null) ? 0 : input.Length;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		public bool IsMatch(string input)
		{
			return this.IsMatch(input, this.default_startat(input));
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000BBB4 File Offset: 0x00009DB4
		public bool IsMatch(string input, int startat)
		{
			return this.Match(input, startat).Success;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		public Match Match(string input)
		{
			return this.Match(input, this.default_startat(input));
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000BBD4 File Offset: 0x00009DD4
		public Match Match(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat");
			}
			return this.CreateMachine().Scan(this, input, startat, input.Length);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000BC24 File Offset: 0x00009E24
		public string Replace(string input, MatchEvaluator evaluator)
		{
			return this.Replace(input, evaluator, int.MaxValue, this.default_startat(input));
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000BC3C File Offset: 0x00009E3C
		public string Replace(string input, MatchEvaluator evaluator, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (evaluator == null)
			{
				throw new ArgumentNullException("evaluator");
			}
			if (count < -1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat");
			}
			BaseMachine baseMachine = (BaseMachine)this.CreateMachine();
			if (this.RightToLeft)
			{
				return baseMachine.RTLReplace(this, input, evaluator, count, startat);
			}
			Regex.Adapter @object = new Regex.Adapter(evaluator);
			return baseMachine.LTRReplace(this, input, new BaseMachine.MatchAppendEvaluator(@object.Evaluate), count, startat);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000BCE0 File Offset: 0x00009EE0
		public string Replace(string input, string replacement)
		{
			return this.Replace(input, replacement, int.MaxValue, this.default_startat(input));
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		public string Replace(string input, string replacement, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			if (count < -1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat");
			}
			return this.CreateMachine().Replace(this, input, replacement, count, startat);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000BD6C File Offset: 0x00009F6C
		public override string ToString()
		{
			return this.pattern;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000BD74 File Offset: 0x00009F74
		internal int GroupCount
		{
			get
			{
				return this.group_count;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000BD7C File Offset: 0x00009F7C
		internal int Gap
		{
			get
			{
				return this.gap;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000BD84 File Offset: 0x00009F84
		private IMachine CreateMachine()
		{
			return this.machineFactory.NewInstance();
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000BD94 File Offset: 0x00009F94
		private static string[] GetGroupNamesArray(int groupCount, IDictionary mapping)
		{
			string[] array = new string[groupCount + 1];
			IDictionaryEnumerator enumerator = mapping.GetEnumerator();
			while (enumerator.MoveNext())
			{
				array[(int)enumerator.Value] = (string)enumerator.Key;
			}
			return array;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000BDDC File Offset: 0x00009FDC
		private int[] GroupNumbers
		{
			get
			{
				if (this.group_numbers == null)
				{
					this.group_numbers = new int[1 + this.group_count];
					for (int i = 0; i < this.gap; i++)
					{
						this.group_numbers[i] = i;
					}
					for (int j = this.gap; j <= this.group_count; j++)
					{
						this.group_numbers[j] = int.Parse(this.group_names[j]);
					}
					return this.group_numbers;
				}
				return this.group_numbers;
			}
		}

		// Token: 0x040009F2 RID: 2546
		private static FactoryCache cache = new FactoryCache(15);

		// Token: 0x040009F3 RID: 2547
		private IMachineFactory machineFactory;

		// Token: 0x040009F4 RID: 2548
		private IDictionary mapping;

		// Token: 0x040009F5 RID: 2549
		private int group_count;

		// Token: 0x040009F6 RID: 2550
		private int gap;

		// Token: 0x040009F7 RID: 2551
		private bool refsInitialized;

		// Token: 0x040009F8 RID: 2552
		private string[] group_names;

		// Token: 0x040009F9 RID: 2553
		private int[] group_numbers;

		// Token: 0x040009FA RID: 2554
		protected internal string pattern;

		// Token: 0x040009FB RID: 2555
		protected internal RegexOptions roptions;

		// Token: 0x040009FC RID: 2556
		[MonoTODO]
		internal Dictionary<string, int> capnames;

		// Token: 0x040009FD RID: 2557
		[MonoTODO]
		internal Dictionary<int, int> caps;

		// Token: 0x040009FE RID: 2558
		[MonoTODO]
		protected internal int capsize;

		// Token: 0x040009FF RID: 2559
		[MonoTODO]
		protected internal string[] capslist;

		// Token: 0x02000082 RID: 130
		private class Adapter
		{
			// Token: 0x0600029D RID: 669 RVA: 0x0000BE68 File Offset: 0x0000A068
			public Adapter(MatchEvaluator ev)
			{
				this.ev = ev;
			}

			// Token: 0x0600029E RID: 670 RVA: 0x0000BE78 File Offset: 0x0000A078
			public void Evaluate(Match m, StringBuilder sb)
			{
				sb.Append(this.ev(m));
			}

			// Token: 0x04000A00 RID: 2560
			private MatchEvaluator ev;
		}
	}
}
