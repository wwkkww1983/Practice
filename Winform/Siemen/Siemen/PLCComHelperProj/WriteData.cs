using System;

namespace PLCComHelperProj
{
	// Token: 0x02000004 RID: 4
	public class WriteData
	{
        private Tag tag_0;
        private double double_0;
        private TagGroup tagGroup_0;
        public Tag Tag
		{
			get
			{
				return this.tag_0;
			}
			set
			{
				this.tag_0 = value;
			}
		}
		public double WrData
		{
			get
			{
				return this.double_0;
			}
			set
			{
				this.double_0 = value;
			}
		}
		public TagGroup TagGroup
		{
			get
			{
				return this.tagGroup_0;
			}
			set
			{
				this.tagGroup_0 = value;
			}
		}


		public WriteData(TagGroup tagGroup_1, Tag tag_1, double double_1) : base()
        {
			this.tagGroup_0 = tagGroup_1;
			this.tag_0 = tag_1;
			this.double_0 = double_1;
		}


	}
}
