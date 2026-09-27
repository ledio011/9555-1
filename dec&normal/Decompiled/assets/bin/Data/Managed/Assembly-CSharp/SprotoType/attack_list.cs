using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200030E RID: 782
	public class attack_list : SprotoTypeBase
	{
		// Token: 0x060015D5 RID: 5589 RVA: 0x00088634 File Offset: 0x00086834
		public attack_list() : base(attack_list.max_field_count)
		{
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00088644 File Offset: 0x00086844
		public attack_list(byte[] buffer) : base(attack_list.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x00088660 File Offset: 0x00086860
		// (set) Token: 0x060015D9 RID: 5593 RVA: 0x00088668 File Offset: 0x00086868
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._id = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x00088680 File Offset: 0x00086880
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x00088690 File Offset: 0x00086890
		// (set) Token: 0x060015DC RID: 5596 RVA: 0x00088698 File Offset: 0x00086898
		public long value
		{
			get
			{
				return this._value;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._value = value;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x000886B0 File Offset: 0x000868B0
		public bool HasValue
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000886C0 File Offset: 0x000868C0
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				int num2 = num;
				if (num2 != 0)
				{
					if (num2 != 1)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.value = this.deserialize.read_integer();
					}
				}
				else
				{
					this.id = this.deserialize.read_integer();
				}
			}
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00088738 File Offset: 0x00086938
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.value, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x04001872 RID: 6258
		private static int max_field_count = 2;

		// Token: 0x04001873 RID: 6259
		private long _id;

		// Token: 0x04001874 RID: 6260
		private long _value;
	}
}
