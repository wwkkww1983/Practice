using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormPlc : Form
    {
        public FormPlc()
        {
            InitializeComponent();
        }

        private void FormPlc_Load(object sender, EventArgs e)
        {

            //加载plc列表
            DataServices.LoadDataDevice(TreeDevice.Nodes);
            //加载PLC内存块长度配置信息列表
            DataServices.LoadDataTagGroup();
            //加载PLC后台配置内存地址指向表
            DataServices.LoadDataTag();

            var p = PublicEntity.TagGroupsEntities;
        }
    }
}
