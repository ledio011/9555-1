using System;
using System.Collections.Generic;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200005D RID: 93
	internal abstract class BaseMachine : IMachine
	{
		// Token: 0x06000187 RID: 391 RVA: 0x000070E4 File Offset: 0x000052E4
		public virtual string Replace(Regex regex, string input, string replacement, int count, int startat)
		{
			ReplacementEvaluator replacementEvaluator = new ReplacementEvaluator(regex, replacement);
			if (regex.RightToLeft)
			{
				return this.RTLReplace(regex, input, new MatchEvaluator(replacementEvaluator.Evaluate), count, startat);
			}
			return this.LTRReplace(regex, input, new BaseMachine.MatchAppendEvaluator(replacementEvaluator.EvaluateAppend), count, startat, replacementEvaluator.NeedsGroupsOrCaptures);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000713C File Offset: 0x0000533C
		public virtual Match Scan(Regex regex, string text, int start, int end)
		{
			throw new NotImplementedException("Scan method must be implemented in derived classes");
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007148 File Offset: 0x00005348
		internal string LTRReplace(Regex regex, string input, BaseMachine.MatchAppendEvaluator evaluator, int count, int startat)
		{
			return this.LTRReplace(regex, input, evaluator, count, startat, true);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007158 File Offset: 0x00005358
		internal string LTRReplace(Regex regex, string input, BaseMachine.MatchAppendEvaluator evaluator, int count, int startat, bool needs_groups_or_captures)
		{
			this.needs_groups_or_captures = needs_groups_or_captures;
			Match match = this.Scan(regex, input, startat, input.Length);
			if (!match.Success)
			{
				return input;
			}
			StringBuilder stringBuilder = new StringBuilder(input.Length);
			int num = startat;
			int num2 = count;
			stringBuilder.Append(input, 0, num);
			while (count == -1 || num2-- > 0)
			{
				if (match.Index < num)
				{
					throw new SystemException("how");
				}
				stringBuilder.Append(input, num, match.Index - num);
				evaluator(match, stringBuilder);
				num = match.Index + match.Length;
				match = match.NextMatch();
				if (!match.Success)
				{
					IL_AA:
					stringBuilder.Append(input, num, input.Length - num);
					return stringBuilder.ToString();
				}
			}
			goto IL_AA;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007228 File Offset: 0x00005428
		internal string RTLReplace(Regex regex, string input, MatchEvaluator evaluator, int count, int startat)
		{
			Match match = this.Scan(regex, input, startat, input.Length);
			if (!match.Success)
			{
				return input;
			}
			int num = startat;
			int num2 = count;
			List<string> list = new List<string>();
			list.Add(input.Substring(num));
			while (count == -1 || num2-- > 0)
			{
				if (match.Index + match.Length > num)
				{
					throw new SystemException("how");
				}
				list.Add(input.Substring(match.Index + match.Length, num - match.Index - match.Length));
				list.Add(evaluator(match));
				num = match.Index;
				match = match.NextMatch();
				if (!match.Success)
				{
					IL_BB:
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(input, 0, num);
					int i = list.Count;
					while (i > 0)
					{
						stringBuilder.Append(list[--i]);
					}
					list.Clear();
					return stringBuilder.ToString();
				}
			}
			goto IL_BB;
		}

		// Token: 0x040008E6 RID: 2278
		protected bool needs_groups_or_captures = true;

		// Token: 0x0200005E RID: 94
		// (Invoke) Token: 0x0600018D RID: 397
		internal delegate void MatchAppendEvaluator(Match match, StringBuilder sb);
	}
}
