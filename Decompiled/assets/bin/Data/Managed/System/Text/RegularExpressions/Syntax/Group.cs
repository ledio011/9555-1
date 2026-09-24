using System;
using System.Collections;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000091 RID: 145
	internal class Group : CompositeExpression
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
		public void AppendExpression(Expression e)
		{
			base.Expressions.Add(e);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000D1B0 File Offset: 0x0000B3B0
		public override void Compile(ICompiler cmp, bool reverse)
		{
			int count = base.Expressions.Count;
			for (int i = 0; i < count; i++)
			{
				Expression expression;
				if (reverse)
				{
					expression = base.Expressions[count - i - 1];
				}
				else
				{
					expression = base.Expressions[i];
				}
				expression.Compile(cmp, reverse);
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000D20C File Offset: 0x0000B40C
		public override void GetWidth(out int min, out int max)
		{
			min = 0;
			max = 0;
			foreach (object obj in base.Expressions)
			{
				Expression expression = (Expression)obj;
				int num;
				int num2;
				expression.GetWidth(out num, out num2);
				min += num;
				if (max == 2147483647 || num2 == 2147483647)
				{
					max = int.MaxValue;
				}
				else
				{
					max += num2;
				}
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000D2AC File Offset: 0x0000B4AC
		public override AnchorInfo GetAnchorInfo(bool reverse)
		{
			int fixedWidth = base.GetFixedWidth();
			ArrayList arrayList = new ArrayList();
			IntervalCollection intervalCollection = new IntervalCollection();
			int num = 0;
			int count = base.Expressions.Count;
			for (int i = 0; i < count; i++)
			{
				Expression expression;
				if (reverse)
				{
					expression = base.Expressions[count - i - 1];
				}
				else
				{
					expression = base.Expressions[i];
				}
				AnchorInfo anchorInfo = expression.GetAnchorInfo(reverse);
				arrayList.Add(anchorInfo);
				if (anchorInfo.IsPosition)
				{
					return new AnchorInfo(this, num + anchorInfo.Offset, fixedWidth, anchorInfo.Position);
				}
				if (anchorInfo.IsSubstring)
				{
					intervalCollection.Add(anchorInfo.GetInterval(num));
				}
				if (anchorInfo.IsUnknownWidth)
				{
					break;
				}
				num += anchorInfo.Width;
			}
			intervalCollection.Normalize();
			Interval interval = Interval.Empty;
			foreach (object obj in intervalCollection)
			{
				Interval interval2 = (Interval)obj;
				if (interval2.Size > interval.Size)
				{
					interval = interval2;
				}
			}
			if (interval.IsEmpty)
			{
				return new AnchorInfo(this, fixedWidth);
			}
			bool flag = false;
			int num2 = 0;
			num = 0;
			for (int j = 0; j < arrayList.Count; j++)
			{
				AnchorInfo anchorInfo2 = (AnchorInfo)arrayList[j];
				if (anchorInfo2.IsSubstring && interval.Contains(anchorInfo2.GetInterval(num)))
				{
					flag |= anchorInfo2.IgnoreCase;
					arrayList[num2++] = anchorInfo2;
				}
				if (anchorInfo2.IsUnknownWidth)
				{
					break;
				}
				num += anchorInfo2.Width;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int k = 0; k < num2; k++)
			{
				AnchorInfo anchorInfo3;
				if (reverse)
				{
					anchorInfo3 = (AnchorInfo)arrayList[num2 - k - 1];
				}
				else
				{
					anchorInfo3 = (AnchorInfo)arrayList[k];
				}
				stringBuilder.Append(anchorInfo3.Substring);
			}
			if (stringBuilder.Length == interval.Size)
			{
				return new AnchorInfo(this, interval.low, fixedWidth, stringBuilder.ToString(), flag);
			}
			if (stringBuilder.Length > interval.Size)
			{
				Console.Error.WriteLine("overlapping?");
				return new AnchorInfo(this, fixedWidth);
			}
			throw new SystemException("Shouldn't happen");
		}
	}
}
