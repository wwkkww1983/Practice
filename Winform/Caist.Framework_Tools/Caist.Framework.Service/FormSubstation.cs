using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.Service.Tools;
using Caist.Framework.ThreadPool;
using Communication.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Caist.Framework.Service
{
    public partial class FormSubstation : Form
    {
        #region 变量
        private List<OpcHelper> _opcHelpers = new List<OpcHelper>();
        List<OpcTimer> _listCaistTimers = new List<OpcTimer>();
        Dictionary<OpcHelper, int> _valuePairs = new Dictionary<OpcHelper, int>();
        FrmMian _frmMian;
        #endregion
        public FormSubstation(FrmMian frmMian)
        {
            this._frmMian = frmMian;
            InitializeComponent();
        }

        private void FormSubstation_Load(object sender, EventArgs e)
        {
            DataServices.LoadOpcDataTag();
            InitSubStation();
            OpcInit();
        }
        #region 初始化

        private void OpcInit()
        {
            Opc_DAHelper.Init();
            List<AddressUnit> addressUnits = new List<AddressUnit>();
            PublicEntity.OPCPowerEntities.ForEach(d =>
            {
                //读取指令结构
                addressUnits.Add(
                new AddressUnit()
                {
                    Id = d.Num,
                    Name = d.Name,
                    Area = d.Area,
                    Address = d.Address,
                    DataType = typeof(ushort)
                });
            });
            Opc_DAHelper._opcMachine.GetAddresses = addressUnits;
        }
        private void InitSubStation()
        {
            txtAddress.Text = "FBoxOpcDaHost".GetConfigrationStr();
        }
        #endregion

        #region 调用代码
        public async void OpcStart(string clientFlag)
        {
            try
            {
                var ans = await Opc_DAHelper._opcMachine.GetDatasAsync(MachineGetDataType.Id);
                var strs = JsonConvert.SerializeObject(ans);
                SendData(strs, clientFlag);

            }
            catch (Exception ex)
            {
                //Common.ShowErrorLog("OpcLogPath".GetConfigrationStr(), ex);
            }
        }
        
        private void SendData(string strs, string clientFlag)
        {
            //通过websocket发送数据到前端
            _frmMian.SendMessage(strs, clientFlag);
        }
        #endregion

        #region 链接opc
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            //OpcStart();
        }
        #endregion

        #region 直接读取模式
        private void btnRead_Click(object sender, EventArgs e)
        {
        }
        #endregion
        
    }
}
