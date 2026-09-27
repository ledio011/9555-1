using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003B8 RID: 952
	public class gameitem : SprotoTypeBase
	{
		// Token: 0x06001CED RID: 7405 RVA: 0x0009740C File Offset: 0x0009560C
		public gameitem() : base(gameitem.max_field_count)
		{
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0009741C File Offset: 0x0009561C
		public gameitem(byte[] buffer) : base(gameitem.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0009743C File Offset: 0x0009563C
		// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x00097444 File Offset: 0x00095644
		public long indexId
		{
			get
			{
				return this._indexId;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._indexId = value;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x0009745C File Offset: 0x0009565C
		public bool HasIndexId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x0009746C File Offset: 0x0009566C
		// (set) Token: 0x06001CF4 RID: 7412 RVA: 0x00097474 File Offset: 0x00095674
		public string itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._itemId = value;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x0009748C File Offset: 0x0009568C
		public bool HasItemId
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x0009749C File Offset: 0x0009569C
		// (set) Token: 0x06001CF7 RID: 7415 RVA: 0x000974A4 File Offset: 0x000956A4
		public bool bindflag
		{
			get
			{
				return this._bindflag;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._bindflag = value;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x000974BC File Offset: 0x000956BC
		public bool HasBindflag
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x000974CC File Offset: 0x000956CC
		// (set) Token: 0x06001CFA RID: 7418 RVA: 0x000974D4 File Offset: 0x000956D4
		public long level
		{
			get
			{
				return this._level;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._level = value;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x000974EC File Offset: 0x000956EC
		public bool HasLevel
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x000974FC File Offset: 0x000956FC
		// (set) Token: 0x06001CFD RID: 7421 RVA: 0x00097504 File Offset: 0x00095704
		public long flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._flags = value;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x0009751C File Offset: 0x0009571C
		public bool HasFlags
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x0009752C File Offset: 0x0009572C
		// (set) Token: 0x06001D00 RID: 7424 RVA: 0x00097534 File Offset: 0x00095734
		public long stack
		{
			get
			{
				return this._stack;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._stack = value;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0009754C File Offset: 0x0009574C
		public bool HasStack
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x0009755C File Offset: 0x0009575C
		// (set) Token: 0x06001D03 RID: 7427 RVA: 0x00097564 File Offset: 0x00095764
		public long quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._quality = value;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x0009757C File Offset: 0x0009577C
		public bool HasQuality
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0009758C File Offset: 0x0009578C
		// (set) Token: 0x06001D06 RID: 7430 RVA: 0x00097594 File Offset: 0x00095794
		public List<long> parm
		{
			get
			{
				return this._parm;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._parm = value;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x000975AC File Offset: 0x000957AC
		public bool HasParm
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x000975BC File Offset: 0x000957BC
		// (set) Token: 0x06001D09 RID: 7433 RVA: 0x000975C4 File Offset: 0x000957C4
		public long appraise
		{
			get
			{
				return this._appraise;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._appraise = value;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x000975DC File Offset: 0x000957DC
		public bool HasAppraise
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x000975EC File Offset: 0x000957EC
		// (set) Token: 0x06001D0C RID: 7436 RVA: 0x000975F4 File Offset: 0x000957F4
		public Dictionary<long, random_attri> random_attri
		{
			get
			{
				return this._random_attri;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._random_attri = value;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0009760C File Offset: 0x0009580C
		public bool HasRandom_attri
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0009761C File Offset: 0x0009581C
		// (set) Token: 0x06001D0F RID: 7439 RVA: 0x00097624 File Offset: 0x00095824
		public Dictionary<long, inlay> inlay
		{
			get
			{
				return this._inlay;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._inlay = value;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x0009763C File Offset: 0x0009583C
		public bool HasInlay
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x0009764C File Offset: 0x0009584C
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.indexId = this.deserialize.read_integer();
					break;
				case 1:
					this.itemId = this.deserialize.read_string();
					break;
				case 2:
					this.bindflag = this.deserialize.read_boolean();
					break;
				case 3:
					this.level = this.deserialize.read_integer();
					break;
				case 4:
					this.flags = this.deserialize.read_integer();
					break;
				case 5:
					this.stack = this.deserialize.read_integer();
					break;
				case 6:
					this.quality = this.deserialize.read_integer();
					break;
				case 7:
					this.parm = this.deserialize.read_integer_list();
					break;
				case 8:
					this.appraise = this.deserialize.read_integer();
					break;
				case 9:
					this.random_attri = this.deserialize.read_map<long, random_attri>((random_attri v) => v.index);
					break;
				case 10:
					this.inlay = this.deserialize.read_map<long, inlay>((inlay v) => v.index);
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x000977E8 File Offset: 0x000959E8
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.indexId, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_string(this.itemId, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_boolean(this.bindflag, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.level, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.flags, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.stack, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.quality, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.parm, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.appraise, 8);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_obj<long, random_attri>(this.random_attri, 9);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_obj<long, inlay>(this.inlay, 10);
			}
			return this.serialize.close();
		}

		// Token: 0x04001A7B RID: 6779
		private static int max_field_count = 11;

		// Token: 0x04001A7C RID: 6780
		private long _indexId;

		// Token: 0x04001A7D RID: 6781
		private string _itemId;

		// Token: 0x04001A7E RID: 6782
		private bool _bindflag;

		// Token: 0x04001A7F RID: 6783
		private long _level;

		// Token: 0x04001A80 RID: 6784
		private long _flags;

		// Token: 0x04001A81 RID: 6785
		private long _stack;

		// Token: 0x04001A82 RID: 6786
		private long _quality;

		// Token: 0x04001A83 RID: 6787
		private List<long> _parm;

		// Token: 0x04001A84 RID: 6788
		private long _appraise;

		// Token: 0x04001A85 RID: 6789
		private Dictionary<long, random_attri> _random_attri;

		// Token: 0x04001A86 RID: 6790
		private Dictionary<long, inlay> _inlay;
	}
}
