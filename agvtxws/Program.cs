using System;
using System.Collections.Generic;
using System.Text;

namespace agvtxws
{
    class Program
    {
        static void Main(string[] args)
        {


            agv2wmstx.tx ws = new agvtxws.agv2wmstx.tx();
            ws.Url = "http://dspt.f3322.net:18080/jfsaptx/tx.asmx";
            agv2wmstx.clsPalletStatus indata = new agvtxws.agv2wmstx.clsPalletStatus();
            //
            indata.ERPOrderID="工单编号	字符串	N	WMS发给AGV时带的字段";
            indata.TransactionID="发料信息编号 等于备料（领料）单上面的编号	字符串	N	WMS发给AGV时带的字段";
            indata.OrganizationCode="组织编号	字符串	N	WMS发给AGV时带的字段";
            indata.WorkCenterCode="工位编号	字符串	N	工位编号，WMS发给AGV时带的字段";
            indata.PalletCode="托盘编号	字符串	N	托盘号，WMS发给AGV时带的字段";
            indata.TakePort="出库口	字符串	N	出库口1391， WMS发给AGV时带的字段，对应Sterrooutexitexitcode";
            indata.TakeStatus="Coming/Finish 执行完成后主动发Finish；Coming用于WMS查询某托盘是否完成取走时反馈";
            indata.TakeTime="0000-00-00 00:00:00";
            //
            agv2wmstx.clsPalletStatusRtn r = ws.tpstatus_agv_send(indata);
            Console.Write("返回信息: ERPOrderID=>" + r.ERPOrderID +
                " PalletCode=>" + r.PalletCode +
                " TransactionID=>" + r.TransactionID +
                " WorkCenterCode=>" + r.WorkCenterCode +
                " YnTxOK=>" + r.YnTxOK);
            Console.Read();
        }

        public static void Main2()
        {
            wms2agv.AGVSrv wsop = new wms2agv.AGVSrv();
            wsop.Url = "http://139.196.176.125:2580/tg_agv_webSrv4GoldenWind/AGVSrv.asmx";
            wms2agv.WMS_PartsDeliver[] tdata = new wms2agv.WMS_PartsDeliver[2];
            tdata[0] = new wms2agv.WMS_PartsDeliver();
            tdata[0].ERPOrderID = "1844060";
            tdata[0].TransactionID = "NM-1507513478199";
            tdata[0].DistributionMode = 1;
            tdata[0].OrganizationCode = "1943";
            tdata[0].WorkCenterCode = "LSC01-P004";
            tdata[0].DistributionParty = 1;
            tdata[0].Warehouse = "0";
            tdata[0].Sterrooutexitexitcode = "1391";
            tdata[0].PalletCode = " TP001222";
            tdata[0].Materials = new wms2agv.Material[2];
            tdata[0].Materials[0] = new wms2agv.Material();
            tdata[0].Materials[0].MaterialCode = "5.1605.3925";
            tdata[0].Materials[0].Quantity = 10;
            tdata[0].Materials[0].EndFlag = 0;
            tdata[0].Materials[0].BatchNum = "0";
            tdata[0].Materials[0].SerialNumber = "0";
            tdata[0].Materials[0].VendorNumber = "2361";
            tdata[0].Materials[0].VendorName = "北京信邦同安电子有限公司";
            tdata[0].Materials[1] = new wms2agv.Material();
            tdata[0].Materials[1].MaterialCode = "5.1507.0060";
            tdata[0].Materials[1].Quantity = 10;
            tdata[0].Materials[1].EndFlag = 0;
            tdata[0].Materials[1].BatchNum = "0";
            tdata[0].Materials[1].SerialNumber = "0";
            tdata[0].Materials[1].VendorNumber = "796";
            tdata[0].Materials[1].VendorName = "北京市京浙电子技术有限公司";

            tdata[1] = new wms2agv.WMS_PartsDeliver();
            tdata[1].ERPOrderID = "1844060";
            tdata[1].TransactionID = "NM-1507513478199";
            tdata[1].DistributionMode = 1;
            tdata[1].OrganizationCode = "1943";
            tdata[1].WorkCenterCode = "LSC01-P001";
            tdata[1].DistributionParty = 1;
            tdata[1].Warehouse = "";
            tdata[1].Sterrooutexitexitcode = "1391";
            tdata[1].PalletCode = "TP001223";
            tdata[1].Materials = new wms2agv.Material[2];
            tdata[1].Materials[0] = new wms2agv.Material();

            tdata[1].Materials[0].MaterialCode = "4.2500.0103";
            tdata[1].Materials[0].Quantity = 10;
            tdata[1].Materials[0].EndFlag = 0;
            tdata[1].Materials[0].BatchNum = "0";
            tdata[1].Materials[0].SerialNumber = "0";
            tdata[1].Materials[0].VendorNumber = "2459";
            tdata[1].Materials[0].VendorName = "北京鼎鑫怡合电器有限公司";

            tdata[1].Materials[1] = new wms2agv.Material();
            tdata[1].Materials[1].MaterialCode = "4.3700.0087";
            tdata[1].Materials[1].Quantity = 10;
            tdata[1].Materials[1].EndFlag = 0;
            tdata[1].Materials[1].BatchNum = "0";
            tdata[1].Materials[1].SerialNumber = "0";
            tdata[1].Materials[1].VendorNumber = "1183";
            tdata[1].Materials[1].VendorName = "北京莱凯威智能电气有限公司";

            wms2agv.AGVfeedBack datar = wsop.RecevieWMStasks("404c9331839c439ba36941d76d296c80", tdata);
            Console.Write("<br>向agv发送任务返回:" +datar);// Newtonsoft.Json.JsonConvert.SerializeObject(datar));

            //发送托盘就位指令
            string ls_rtn = ""; ////wsop.PalletOutReady(trayno, exitpositionno);
            Console.Write("<br>托盘就位指令返回:" + ls_rtn);
        }
    }
}
