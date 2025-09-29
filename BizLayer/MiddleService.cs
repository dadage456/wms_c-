using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using System.Data;
using BizLayer.WebService;

namespace BizLayer
{
    public class MiddleService
    {

        public WebService.PDA PDAservice;

        private string logId = string.Empty;
        public string LogId
        {
            get { return logId; }
            set { logId = value; }
        }

        public MiddleService()
        {
            PDAservice = new WebService.PDA();
            PDAservice.Url = Management.GetSingleton().DefaultBaseUrl + "/" + "PDA.asmx";
        }

        private static MiddleService instance;
        public static MiddleService Instance
        {
            get
            {
                if (instance == null) instance = new MiddleService();
                return instance;
            }
        }


        /// <summary>
        /// 连接测试
        /// </summary>
        /// <param name="url">用户指定的Url</param>
        public void ConnectionTest(string url)
        {
            string ret = "";
            try
            {
                PDAservice.Url = url + "/" + "PDA.asmx";
                //ret = PDAservice.uf_ConnTest();
            }
            catch (System.Net.WebException)
            {
                throw new ApplicationException("连接服务器失败!");
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                throw new ApplicationException("服务器错误", e);
            }
            catch (Exception e)
            {
                throw new ApplicationException("未知错误", e);
            }
            if (ret != "OK")
            {
                throw new ApplicationException("测试连接失败！");
            }
        }

        /// <summary>
        /// 验证登陆账号是否有效
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        public string Login(string userid, string password)
        {
            return PDAservice.LogInPDA(userid, password);
        }

        /// <summary>
        /// 获取服务器版本
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            try
            {
                return PDAservice.GetVersion();
            }
            catch (System.Net.WebException webEx)
            {
                throw new ApplicationException("连接服务器失败。", webEx);
            }
            catch (System.Web.Services.Protocols.SoapException soapEx)
            {
                throw new ApplicationException("服务器错误。", soapEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("未知错误。", ex);
            }
        }

        //连接webservice地址
        public string ServiceUrl
        {
            get { return PDAservice.Url; }
            set { PDAservice.Url = value + "/" + "PDA.asmx"; }

        }

        /// <summary>
        /// 根据提盘号校验托盘信息
        /// </summary>
        /// <param name="trayNo"></param>
        public void CheckTray(string trayNo)
        {
            try
            {
                PDAservice.CheckTray(trayNo);
            }
            catch (Exception e)
            {
                throw new Exception("获取托盘号失败" + e.Message);
            }
        }

        /// <summary>
        /// 校验库位是否可用
        /// </summary>
        /// <param name="siteNo"></param>
        public void CheckSite(string siteNo)
        {
            try
            {
                PDAservice.CheckSite(siteNo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据源托盘号获取源托盘信息 
        /// </summary>
        /// <param name="sourceTrayNo"></param>
        /// <returns></returns>
        public DataSet GetSourceTrayInfo(string sourceTrayNo)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetSourceTrayInfo(sourceTrayNo);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("源托盘" + sourceTrayNo + "信息为空，请核实托盘号及库存信息");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception("获取源托盘信息失败" + e.Message);
            }
        }

        /// <summary>
        /// 根据目标托盘号获取目标托盘信息
        /// </summary>
        /// <param name="targetTrayNo"></param>
        /// <returns></returns>
        public DataSet GetTargetTrayInfo(string targetTrayNo)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetTargetTrayInfo(targetTrayNo);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("目标托盘" + targetTrayNo + "信息为空，请核实托盘号及库存信息");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception("获取目标托盘信息失败" + e.Message);
            }
        }

        /// <summary>
        /// 合盘提交业务
        /// </summary>
        /// <param name="sourceTrayNo">源托盘号</param>
        /// <param name="targetTrayNo">目标托盘号</param>
        /// <param name="qty">转移数量</param>
        public void CommitMergeTray(string sourceTrayNo, string targetTrayNo, decimal qty)
        {
            try
            {
                PDAservice.CommitMergeTray(sourceTrayNo, targetTrayNo, qty);
            }
            catch (Exception e)
            {
                throw new Exception("获取目标托盘信息失败" + e.Message);
            }
        }

        /// <summary>
        /// 获取系统中所有异常类型
        /// </summary>
        /// <returns></returns>      
        public DataSet GetExceptionType()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetExceptionType();
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("异常类型未维护");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取系统中所有立体库进出口位置
        /// </summary>
        /// <returns></returns>      
        public DataSet GetInOutLocation(string LocationType)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetInOutLocation(LocationType);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("立体库进出口相关位置未维护");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取系统中所有库房信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetStoreRoom()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetStoreRoom();
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("库房未维护");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据库房获取库房下所有库位
        /// </summary>
        /// <param name="storeRoomNo"></param>
        /// <returns></returns>
        public DataSet GetStoreSiteByRoom(string storeRoomNo,string storeSiteNo)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetStoreSiteByRoom(storeRoomNo, storeSiteNo);
              
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("库房【" + storeRoomNo + "】未维护库位【" + storeSiteNo + "】信息");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取系统中所有库位信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetStoreSite()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetStoreSite();
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("库位未维护");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取物料批次二维码
        /// </summary>
        /// <param name="_2DBarcodeValue"></param>
        /// <returns></returns>
        public BarcodeContent AnalysisForNewBarcode(string _2DBarcodeValue)
        {
            try
            {
                return PDAservice.AnalysisForNewBarcode(_2DBarcodeValue);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取物料对应的编码控制属性
        /// </summary>
        /// <param name="matCode"></param>
        /// <returns></returns>
        public int GetMatControl(string matCode)
        {
            string msg = PDAservice.GetMatControl(matCode);
            return Convert.ToInt16(msg.Substring(0, 1));
        }
        /// <summary>
        /// 获取物料对应的编码控制属性包含容量
        /// </summary>
        /// <param name="matCode"></param>
        /// <param name="mtlCapacity"></param>
        /// <param name="mtlWeight"></param>
        /// <returns></returns>
        public int GetMatControl(string matCode, out decimal mtlCapacity, out decimal mtlWeight)
        {
            string[] mtlInfo = PDAservice.GetMatControl(matCode).Split('!');
            mtlCapacity = 0;
            mtlWeight = 0;
            if (mtlInfo[1] != string.Empty)
            {
                mtlWeight = Convert.ToDecimal(mtlInfo[1]);
            }
            if (mtlInfo[2] != string.Empty)
            {
                mtlCapacity = Convert.ToDecimal(mtlInfo[2]);
            }
            return Convert.ToInt16(mtlInfo[0]);
        }

        /// <summary>
        /// 获取物料对应的编码控制属性包含id
        /// </summary>
        /// <param name="matCode"></param>
        /// <param name="mtlCapacity"></param>
        /// <param name="mtlWeight"></param>
        /// <returns></returns>
        public int GetMatControl(string matCode, out string matId)
        {
            string[] mtlInfo = PDAservice.GetMatControl(matCode).Split('!');
            matId = string.Empty;
            if (mtlInfo[3] != string.Empty)
            {
                matId = mtlInfo[3];
            }           
            return Convert.ToInt16(mtlInfo[0]);
        }

        /// <summary>
        /// 获取物料对应的编码控制属性包含id
        /// </summary>
        /// <param name="matCode"></param>
        /// <param name="mtlCapacity"></param>
        /// <param name="mtlWeight"></param>
        /// <returns></returns>
        public string GetMatSendControl(string matCode)
        {
            string[] mtlInfo = PDAservice.GetMatControl(matCode).Split('!');           
            string matSendControl = string.Empty;
            if (mtlInfo[4] != string.Empty)
            {
                matSendControl = mtlInfo[4];
            }
            else
            {
                matSendControl = "0";
            }
            return matSendControl;
        }

        /// <summary>
        /// 获取仓库对物料采集控制属性包含id
        /// </summary>
        /// <param name="matCode"></param>
        /// <param name="mtlCapacity"></param>
        /// <param name="mtlWeight"></param>
        /// <returns></returns>
        public string GetRoomMatControl(string taskId)
        {
            string[] roomMtlInfo = PDAservice.GetRoomMatControl(taskId).Split('!');
            string roomMatControl = string.Empty;
            if (roomMtlInfo[4] != string.Empty)
            {
                roomMatControl = roomMtlInfo[4];
            }
            else
            {
                roomMatControl = "0";
            }
            return roomMatControl;
        }

        /// <summary>
        /// 获取上架任务信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetInTask(string userId, string roomTag, string transferType)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetInTask(userId, User.Instance.UserData.UserId, roomTag, transferType);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据任务号获取上架任务明细
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public DataSet GetInTaskItem(string userId, string taskNo, string taskComment, string roomTag, string finishFlag, string workStation)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetInTaskItem(userId, taskNo, roomTag, finishFlag, workStation);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("凭证号" + taskComment + "没有对应上架明细，请核实");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据任务号获取上架托盘号
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public DataSet GetInTaskPalletNo(string userId, string taskNo, string taskComment, string roomTag, string finishFlag, string workStation)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetInTaskPalletNo(userId, taskNo, roomTag, finishFlag,workStation);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("凭证号" + taskComment + "没有对应下架明细，请核实");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        

        /// <summary>
        /// 根据任务ID获取任务指令
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public DataSet GetWmsToWcsByTaskID(string taskComment, string taskId, string taskType)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetWmsToWcsByTaskID(taskComment, taskId, taskType);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("凭证号" + taskComment + "没有对应任务指令，请核实");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据任务ID获取任务已经采集数据
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public DataSet GetCheckCollectDataByTrayNo(string taskComment, string taskId, string taskNo, string trayNo, string taskType)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetCheckCollectDataByTrayNo(taskComment, taskId, taskNo, trayNo, taskType);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("凭证号" + taskComment + "没有对应任务指令，请核实");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据查询条件获取任务指令
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public DataSet GetWmsToWcsByQueryStr(string taskComment, string taskId, string taskType, string QueryStr)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetWmsToWcsByQueryStr(taskComment, taskId,taskType,QueryStr);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("凭证号【" + taskComment + "】和查询条件【" + QueryStr + "】没有对应任务指令，请核实查询条件！");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据任务ID获取任务指令
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public DataSet GetCheckOrderPalletNoByTaskID(string taskComment, string taskId, string taskType)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetCheckOrderPalletNoByTaskID(taskComment, taskId, taskType);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("凭证号" + taskComment + "没有对应需要复盘的托盘号，请核实");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据任务ID获取组盘提交的数据
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public DataSet GetPalletItemByTaskID(string taskComment, string taskId, string palletNo,string userId)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetPalletItemByTaskID(taskComment, taskId, palletNo, userId);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("凭证号【" + taskComment + "】托盘号【" + palletNo + "】没有对应组盘提交数据，请核实");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据托盘号获取库存的数据
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public DataSet GetRepertoryByTrayNo(string taskComment, string taskId, string palletNo, string userId)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetRepertoryByTrayNo(taskComment, taskId, palletNo, userId);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("凭证号【" + taskComment + "】托盘号【" + palletNo + "】没有对应库存数据，请核实");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        

        /// <summary>
        /// 提交异常采集数据
        /// </summary>
        /// <param name="upShelvesInfos"></param>
        public void CommitExceptShelves(ExceptShelvesInfo[] exceptShelvesInfos, string collecterCode, ItemListInfo[] itemListInfo, string filter)
        {
            try
            {
                PDAservice.CommitExceptShelves(exceptShelvesInfos, collecterCode, itemListInfo, filter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 提交供应商二维码采集数据
        /// </summary>
        /// <param name="upShelvesInfos"></param>
        public void CommitSupplierShelves(SupplierShelvesInfo[] supplierShelvesInfos, string collecterCode, ItemListInfo[] itemListInfo, string filter)
        {
            try
            {
                PDAservice.CommitSupplierShelves(supplierShelvesInfos, collecterCode, itemListInfo, filter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// 提交上架采集数据
        /// </summary>
        /// <param name="upShelvesInfos"></param>
        public void CommitUpShelves(UpShelvesInfo[] upShelvesInfos, string collecterCode, ItemListInfo[] itemListInfo, string filter)
        {
            try
            {
                PDAservice.CommitUpShelves(upShelvesInfos, collecterCode, itemListInfo, filter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 提交上架采集数据
        /// </summary>
        /// <param name="upShelvesInfos"></param>
        public void CommitSignShelves(UpShelvesInfo[] upShelvesInfos, string collecterCode, ItemListInfo[] itemListInfo, string filter)
        {
            try
            {
                PDAservice.CommitSignShelves(upShelvesInfos, collecterCode, itemListInfo, filter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// 获取下架任务信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetOutTask(string userId, string roomTag, string batchFlag, string transfertype)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetOutTask(userId, User.Instance.UserData.UserId, roomTag, batchFlag,transfertype,"N");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取节拍配送下架任务信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetBeatOutTask(string userId, string roomTag, string batchFlag, string transfertype)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetOutTask(userId, User.Instance.UserData.UserId, roomTag, batchFlag, transfertype, "Y");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取下架任务信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetOutTaskItemByUserID(string userId, string taskComment, string roomTag, string workStation, string TaskNo)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetOutTaskItemByUserID(userId, User.Instance.UserData.UserId, taskComment, roomTag, workStation, TaskNo);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取整盘下架任务信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetOutTaskPalletNoByUserID(string userId, string taskComment, string roomTag, string workStation)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetOutTaskPalletNoByUserID(userId, User.Instance.UserData.UserId, taskComment, roomTag, workStation);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        

        /// <summary>
        /// 获取上架任务信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetInTaskItemByUserID(string userId, string taskComment, string roomTag, string workStation)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetInTaskItemByUserID(userId, User.Instance.UserData.UserId, taskComment, roomTag, workStation);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取整盘下架任务信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetInTaskPalletNoByUserID(string userId, string taskComment, string roomTag, string workStation)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetInTaskPalletNoByUserID(userId, User.Instance.UserData.UserId, taskComment, roomTag, workStation);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据凭证号和货位号获取下架任务信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetOutTaskBySiteNo(string userId, string ProofNo, string TaskNo, string SiteNo, string roomTag)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetOutTaskBySiteNo(userId, User.Instance.UserData.UserId, ProofNo, TaskNo, SiteNo,roomTag);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取下架任务信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetOutTaskByProofNo(string userId, string taskComment, string roomTag, string batchFlag, string transfertype)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetOutTaskByProofNo(userId, User.Instance.UserData.UserId, taskComment, roomTag, batchFlag, transfertype);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取上架任务信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetInTaskByProofNo(string userId, string taskComment, string roomTag, string transferType)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetInTaskByProofNo(userId, User.Instance.UserData.UserId, taskComment, roomTag, transferType);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        
        /// <summary>
        /// 获取下架任务信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetOutTaskItemByQueryNo(string userId, string taskComment, string queryNo, string roomTag, string workStation)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetOutTaskItemByQueryNo(userId, User.Instance.UserData.UserId, taskComment, queryNo, roomTag, workStation);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取整盘下架任务信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetOutTaskPalletNoByQueryNo(string userId, string taskComment, string queryNo, string roomTag, string workStation)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetOutTaskPalletNoByQueryNo(userId, User.Instance.UserData.UserId, taskComment, queryNo, roomTag, workStation);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取整盘上架任务信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetInTaskPalletNoByQueryNo(string userId, string taskComment, string queryNo, string roomTag, string workStation)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetInTaskPalletNoByQueryNo(userId, User.Instance.UserData.UserId, taskComment, queryNo, roomTag, workStation);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取上架任务信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetInTaskItemByQueryNo(string userId, string taskComment, string queryNo, string roomTag,string workStation)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetInTaskItemByQueryNo(userId, User.Instance.UserData.UserId, taskComment, queryNo, roomTag, workStation);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        

        /// <summary>
        /// 根据任务号获取下架任务明细
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public DataSet GetOutTaskItem(string userId, string taskNo, string taskComment, string roomTag, string finishFlag, string workStation)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetOutTaskItem(userId, taskNo, roomTag, finishFlag, workStation);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("凭证号" + taskComment + "没有对应下架明细，请核实");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据任务号获取下架托盘号
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public DataSet GetOutTaskPalletNo(string userId, string taskNo, string taskComment, string roomTag, string finishFlag, string workStation)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetOutTaskPalletNo(userId, taskNo, roomTag, finishFlag,workStation);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    throw new Exception("凭证号" + taskComment + "没有对应下架明细，请核实");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取库存信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetRepertoryBySiteNoMatCode(string storeSiteNo, string matCode, string batchNo)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetRepertoryBySiteNoMatCode(storeSiteNo, matCode, batchNo);
                if ((ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0))
                    throw new Exception("货位【" + storeSiteNo + "】物料【" + matCode + "】无库存信息，请核实");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据扫描货位号/物料号/托盘号等查询库存信息
        /// </summary>
        /// <param name="barcode">码号</param>
        /// <param name="currStep">类型</param>
        /// <returns></returns>
        public DataSet GetRepertoryByBarCode(string barcode, string currStep)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = PDAservice.GetRepertoryByBarCode(barcode, currStep);
                if ((ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) && (barcode != "ALL"))
                    throw new Exception("扫描内容:【" + barcode + "】无库存信息，请核实");
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 下架任务接收取消
        /// </summary>
        /// <param name="taskcomment"></param>
        /// <param name="userId"></param>
        /// <param name="isCanel">true 取消</param>
        public void CommitRCOutTask(string taskcomment, string userId, bool isCanel)
        {
            try
            {
                PDAservice.CommitRCOutTask(taskcomment, userId, isCanel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 下架任务接收取消
        /// </summary>
        /// <param name="taskcomment"></param>
        /// <param name="userId"></param>
        /// <param name="isCanel">true 取消</param>
        public void CommitRCOutTaskItem(string OutTaskItemId, string userId, string roomTag, bool isCanel)
        {
            try
            {
                PDAservice.CommitRCOutTaskItem(Convert.ToInt32(OutTaskItemId), userId, roomTag,isCanel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 整盘下架任务接收取消
        /// </summary>
        /// <param name="taskcomment"></param>
        /// <param name="userId"></param>
        /// <param name="isCanel">true 取消</param>
        public void CommitRCOutTaskPalletNo(string OutTaskId, string palletNo, string userId, string roomTag, bool isCanel)
        {
            try
            {
                PDAservice.CommitRCOutTaskPalletNo(Convert.ToInt32(OutTaskId), palletNo,userId, roomTag, isCanel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 整盘上架任务接收取消
        /// </summary>
        /// <param name="taskcomment"></param>
        /// <param name="userId"></param>
        /// <param name="isCanel">true 取消</param>
        public void CommitRCInTaskPalletNo(string InTaskId, string palletNo, string userId, string roomTag, bool isCanel)
        {
            try
            {
                PDAservice.CommitRCInTaskPalletNo(Convert.ToInt32(InTaskId), palletNo, userId, roomTag, isCanel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        

        /// <summary>
        /// 上架任务接收取消
        /// </summary>
        /// <param name="taskcomment"></param>
        /// <param name="userId"></param>
        /// <param name="isCanel">true 取消</param>
        public void CommitRCInTaskItem(string InTaskItemId, string userId, string roomTag, bool isCanel)
        {
            try
            {
                PDAservice.CommitRCInTaskItem(Convert.ToInt32(InTaskItemId), userId, roomTag, isCanel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        

        /// <summary>
        /// 下架任务明细强制完成
        /// </summary>
        /// <param name="taskcomment"></param>
        /// <param name="userId"></param>
        /// <param name="isCanel">true 取消</param>
        public void CommitFinishOutTaskItem(string OutTaskItemId, string userId, string roomTag, bool isCanel)
        {
            try
            {
                PDAservice.CommitFinishOutTaskItem(Convert.ToInt32(OutTaskItemId), userId, roomTag, isCanel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 提交下架采集数据
        /// </summary>
        /// <param name="upShelvesInfos"></param>
        public void CommitDownShelves(DownShelvesInfo[] downShelvesInfos, string collecterId, ItemListInfo[] itemListInfos)
        {
            try
            {

                PDAservice.CommitDownShelves(downShelvesInfos, collecterId, itemListInfos);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 提交下架组盘采集数据
        /// </summary>
        /// <param name="upShelvesInfos"></param>
        public void CommitTrayDownShelves(DownShelvesInfo[] downShelvesInfos, string collecterId, ItemListInfo[] itemListInfos)
        {
            try
            {

                PDAservice.CommitTrayDownShelves(downShelvesInfos, collecterId, itemListInfos);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 提交立体库下架采集数据
        /// </summary>
        /// <param name="upShelvesInfos"></param>
        public void CommitASWHDownShelves(DownShelvesInfo[] downShelvesInfos, string collecterId, ItemListInfo[] itemListInfos)
        {
            try
            {

                PDAservice.CommitASWHDownShelves(downShelvesInfos, collecterId, itemListInfos);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 提交立体库整盘下架采集数据
        /// </summary>
        /// <param name="CommitASWHPalletNoDownShelves"></param>
        public void CommitASWHPalletNoDownShelves(DownShelvesInfo[] downShelvesInfos, string collecterId, ItemListInfo[] itemListInfos)
        {
            try
            {

                PDAservice.CommitASWHPalletNoDownShelves(downShelvesInfos, collecterId, itemListInfos);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// 校验组盘托盘是否符合
        /// </summary>
        /// <param name="trayNo"></param>
        public decimal CheckBindingTray(string trayNo)
        {
            try
            {
                return PDAservice.CheckBindingTray(trayNo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据任务号+托盘号校验组盘托盘是否符合
        /// </summary>
        /// <param name="trayNo"></param>
        public string CheckBindingTrayByTaskId(string taskId, string trayNo, string taskType)
        {
            try
            {
                return PDAservice.CheckBindingTrayByTaskId(taskId,trayNo, taskType);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 校验组盘托盘是否符合
        /// </summary>
        /// <param name="trayNo"></param>
        public decimal CheckDownTray(string trayNo)
        {
            try
            {
                return PDAservice.CheckDownTray(trayNo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据托盘号获取货位
        /// </summary>
        /// <param name="trayNo"></param>
        public string GetPalletSiteNo(string trayNo)
        {
            try
            {
                return PDAservice.GetPalletSiteNo(trayNo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        

        /// <summary>
        /// 获取盘点托盘号的货位
        /// </summary>
        /// <param name="trayNo"></param>
        public string GetCheckTray(string taskComment, string trayNo)
        {
            try
            {
                return PDAservice.GetCheckTray(taskComment,trayNo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        

        /// <summary>
        /// 根据提交上架托盘内容
        /// </summary>
        /// <param name="trayInfos"></param>
        /// <param name="lsItems"></param>
        /// <param name="collecterId"></param>
        /// <param name="taskNo">任务号</param>
        /// <param name="trayNo">托盘号</param>
        /// <param name="filter">序列明细</param>
        /// <param name="currentWeight">当前承重</param>
        /// <param name="currentPlot">当前容积</param>
        public void CommitUpTray(BindingTrayInfo[] trayInfos, ItemListInfo[] lsItems, string collecterId, string taskNo, string trayNo, string filter, decimal currentWeight, decimal currentPlot)
        {
            try
            {
                PDAservice.CommitUpTray(trayInfos, lsItems, collecterId, taskNo, trayNo, filter, currentWeight, currentPlot);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据提交上架托盘内容
        /// </summary>
        /// <param name="trayInfos"></param>
        /// <param name="lsItems"></param>
        /// <param name="collecterId"></param>
        public void CommitTrayUpShelves(BindingTrayInfo[] trayInfos, string collecterId, ItemListInfo[] lsItems, string taskNo)
        {
            try
            {
                PDAservice.CommitTrayUpShelves(trayInfos, collecterId, lsItems, taskNo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据提交托盘上架内容
        /// </summary>
        /// <param name="trayInfos"></param>
        /// <param name="lsItems"></param>
        /// <param name="collecterId"></param>
        /// <param name="taskNo">任务号</param>
        /// <param name="trayNo">托盘号</param>
        /// <param name="filter">序列明细</param>
        /// <param name="currentWeight">当前承重</param>
        /// <param name="currentPlot">当前容积</param>
        public void CommitUpWmsToWcs(string collecterId, string taskId, string taskNo, string trayNo, string startADDR, string endADDR)
        {
            try
            {
                PDAservice.CommitUpWmsToWcs(collecterId, taskId, taskNo, trayNo, startADDR, endADDR);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        
        /// <summary>
        /// 获取来料盘
        /// </summary>
        /// <param name="trayInfos"></param>
        /// <param name="lsItems"></param>
        /// <param name="collecterId"></param>
        /// <param name="taskNo">任务号</param>
        /// <param name="trayNo">托盘号</param>
        /// <param name="filter">序列明细</param>
        /// <param name="currentWeight">当前承重</param>
        /// <param name="currentPlot">当前容积</param>
        public string CommitDownWmsToWcs(string collecterId, string taskId, string taskNo, string trayNo, string startADDR, string endADDR, string singleFlag)
        {
            try
            {
                return PDAservice.CommitDownWmsToWcs(collecterId, taskId, taskNo, trayNo, startADDR, endADDR,singleFlag);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 撤销获取来料盘
        /// </summary>
        /// <param name="trayInfos"></param>
        /// <param name="lsItems"></param>
        /// <param name="collecterId"></param>
        /// <param name="taskNo">任务号</param>
        /// <param name="trayNo">托盘号</param>
        /// <param name="filter">序列明细</param>
        /// <param name="currentWeight">当前承重</param>
        /// <param name="currentPlot">当前容积</param>
        public void CancelDownWmsToWcs(string collecterId, string taskId, string taskNo, string trayNo, string startADDR, string endADDR)
        {
            try
            {
                PDAservice.CancelDownWmsToWcs(collecterId, taskId, taskNo, trayNo, startADDR, endADDR);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 盘点获取来料盘
        /// </summary>
        /// <param name="trayInfos"></param>
        /// <param name="lsItems"></param>
        /// <param name="collecterId"></param>
        /// <param name="taskNo">任务号</param>
        /// <param name="trayNo">托盘号</param>
        /// <param name="filter">序列明细</param>
        /// <param name="currentWeight">当前承重</param>
        /// <param name="currentPlot">当前容积</param>
        public string CommitInvDownWmsToWcs(string collecterId, string taskId, string taskNo, string trayNo, string startADDR, string endADDR, string singleFlag)
        {
            try
            {
                return PDAservice.CommitInvDownWmsToWcs(collecterId, taskId, taskNo, trayNo, startADDR, endADDR, singleFlag);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 来料盘入库
        /// </summary>
        /// <param name="trayInfos"></param>
        /// <param name="lsItems"></param>
        /// <param name="collecterId"></param>
        /// <param name="taskNo">任务号</param>
        /// <param name="trayNo">托盘号</param>
        /// <param name="filter">序列明细</param>
        /// <param name="currentWeight">当前承重</param>
        /// <param name="currentPlot">当前容积</param>
        public void CommitResetWmsToWcs(string collecterId, string taskId, string taskNo, string trayNo, string startADDR, string endADDR)
        {
            try
            {
                PDAservice.CommitResetWmsToWcs(collecterId, taskId, taskNo, trayNo, startADDR, endADDR);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 撤销来料盘入库
        /// </summary>
        /// <param name="trayInfos"></param>
        /// <param name="lsItems"></param>
        /// <param name="collecterId"></param>
        /// <param name="taskNo">任务号</param>
        /// <param name="trayNo">托盘号</param>
        /// <param name="filter">序列明细</param>
        /// <param name="currentWeight">当前承重</param>
        /// <param name="currentPlot">当前容积</param>
        public void CancelResetWmsToWcs(string collecterId, string taskId, string taskNo, string trayNo, string startADDR, string endADDR)
        {
            try
            {
                PDAservice.CancelResetWmsToWcs(collecterId, taskId, taskNo, trayNo, startADDR, endADDR);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 盘点来料盘入库
        /// </summary>
        /// <param name="trayInfos"></param>
        /// <param name="lsItems"></param>
        /// <param name="collecterId"></param>
        /// <param name="taskNo">任务号</param>
        /// <param name="trayNo">托盘号</param>
        /// <param name="filter">序列明细</param>
        /// <param name="currentWeight">当前承重</param>
        /// <param name="currentPlot">当前容积</param>
        public void CommitInvResetWmsToWcs(string collecterId, string taskId, string taskNo, string trayNo, string startADDR, string endADDR)
        {
            try
            {
                PDAservice.CommitInvResetWmsToWcs(collecterId, taskId, taskNo, trayNo, startADDR, endADDR);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 空托盘入库
        /// </summary>
        /// <param name="trayInfos"></param>
        /// <param name="lsItems"></param>
        /// <param name="collecterId"></param>
        /// <param name="taskNo">任务号</param>
        /// <param name="trayNo">托盘号</param>
        /// <param name="filter">序列明细</param>
        /// <param name="currentWeight">当前承重</param>
        /// <param name="currentPlot">当前容积</param>
        public void CommitEmptyTrayWmsToWcs(string collecterId, string taskId, string taskNo, string trayNo, string startADDR, string endADDR)
        {
            try
            {
                PDAservice.CommitEmptyTrayWmsToWcs(collecterId, taskId, taskNo, trayNo, startADDR, endADDR);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 配盘托盘入库
        /// </summary>
        /// <param name="trayInfos"></param>
        /// <param name="lsItems"></param>
        /// <param name="collecterId"></param>
        /// <param name="taskNo">任务号</param>
        /// <param name="trayNo">托盘号</param>
        /// <param name="filter">序列明细</param>
        /// <param name="currentWeight">当前承重</param>
        /// <param name="currentPlot">当前容积</param>
        public void CommitAllocateWmsToWcs(string collecterId, string taskId, string taskNo, string trayNo, string startADDR, string endADDR)
        {
            try
            {
                PDAservice.CommitAllocateWmsToWcs(collecterId, taskId, taskNo, trayNo, startADDR, endADDR);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }        

        /// <summary>
        /// 提交组盘信息
        /// </summary>
        /// <param name="taskNo">任务号</param>
        /// <param name="trayNo">托盘号</param>
        /// <param name="traysInfo">托盘明细</param>
        public void CommitBindingTray(string taskNo, string trayNo, BindingTrayInfo[] traysInfo)
        {
            try
            {
                PDAservice.CommitBindingTray(taskNo, trayNo, traysInfo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 提交盘点数据
        /// </summary>
        /// <param name="billNo"></param>
        /// <param name="infos"></param>
        /// <param name="collecterCode"></param>
        public void CommitInventoryInfos(string billNo, InventoryInfo[] infos, string collecterCode)
        {
            try
            {
                PDAservice.CommitInventoryInfos(billNo, infos, collecterCode);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 提交上架接收取消数据
        /// </summary>
        /// <param name="taskcomment"></param>
        /// <param name="userId"></param>
        /// <param name="isCanel">true 表示取消</param>
        public void CommitReceiveCanelTask(string taskcomment, string userId, bool isCanel)
        {
            try
            {
                PDAservice.CommitReceiveCanelTask(taskcomment, userId, isCanel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取物料检验模式
        /// </summary>
        /// <returns></returns>
        public bool GetIsCheckMtl()
        {
            try
            {
                return PDAservice.GetIsCheckMtl();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取目标库位
        /// </summary>
        /// <param name="taskcomment"></param>
        /// <param name="userId"></param>
        /// <param name="isCanel">true 表示取消</param>
        public string GetTargetSiteCode(string storeRoomCode, string mtlCode, decimal qty)
        {
            try
            {
                return PDAservice.GetTargetSiteCode(storeRoomCode, mtlCode, qty);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取目标库位
        /// </summary>
        /// <param name="PalletNo"></param>
        /// <param name="WeightGrade"></param>
        /// <param name="HighGrade">t</param>
        public string GetPalletTargetSite(string PalletNo, string AddressID, decimal WeightGrade, decimal HighGrade)
        {
            try
            {
                return PDAservice.GetPalletTargetSite(PalletNo,AddressID, WeightGrade, HighGrade);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据库位获取库房以及该库房下所有的库位
        /// </summary>
        /// <param name="storeId">库位Id</param>
        /// <returns></returns>
        public DataSet GetStoreSiteBySiteId(string storeId)
        {
            try
            {
                return PDAservice.GetStoreSiteBySiteId(storeId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 取来源库位和目标库位的物料库存
        /// </summary>
        /// <param name="sourceStoresiteNo">来源库位</param>
        /// <param name="targetStoresiteNo">目标库位</param>
        /// <returns></returns>
        public DataSet GetRepertoryByStoresiteNo(string sourceStoresiteNo, string targetStoresiteNo)
        {
            try
            {
                return PDAservice.GetRepertoryByStoresiteNo(sourceStoresiteNo, targetStoresiteNo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 移库确认
        /// </summary>
        /// <param name="transferInfos"></param>
        /// <param name="collecterId"></param>
        /// <param name="filter"></param>
        public void CommitTransfer(TransferInfo[] transferInfos, string collecterId, string filter)
        {
            try
            {
                PDAservice.CommitTransfer(transferInfos, collecterId, filter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// 提交拉式发料单
        /// </summary>
        /// <param name="mtlSenderInfo"></param>
        /// <param name="collecterId"></param>
        public void CommitMtlSender(MtlSenderInfo[] mtlSenderInfos, string collecterId)
        {
            try
            {
                PDAservice.CommitMtlSender(mtlSenderInfos, collecterId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取盘库任务
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleoRuserId"></param>
        public DataSet GetInventoryTask(string userId, string roleoRuserId, string roomTag)
        {
            try
            {
                return PDAservice.GetInventoryTask(userId, roleoRuserId,roomTag);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据盘库任务获取任务明细
        /// </summary>
        /// <param name="taskComment"></param>
        /// <returns></returns>
        public DataSet GetInventoryTaskItem(string taskComment, string strTaskNo, string roomTag)
        {
            try
            {
                return PDAservice.GetInventoryTaskItem(taskComment, strTaskNo, roomTag);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 盘库任务接收
        /// </summary>
        /// <param name="taskcomment"></param>
        /// <param name="userId"></param>
        /// <param name="isCanel">是否取消 true 是</param>
        public void CommitInventoryTask(string taskcomment, string userId, bool isCanel)
        {
            try
            {
                PDAservice.CommitInventoryTask(taskcomment, userId, isCanel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 取物料库位库存
        /// </summary>
        /// <param name="sourceStoresiteNo">来源库位</param>
        /// <param name="matCode">物料</param>
        /// <returns></returns>
        public DataSet GetMtlRepertoryByStoresiteNo(string sourceStoresiteNo, string matCode)
        {
            try
            {
                return PDAservice.GetMtlRepertoryByStoresiteNo(sourceStoresiteNo, matCode);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 拉式发料取物料库位库存
        /// </summary>
        /// <param name="sourceStoresiteNo">来源库位</param>
        /// <param name="matCode">物料</param>
        /// <returns></returns>
        public DataSet GetLSMtlRepertoryByStoresiteNo(string sourceStoresiteNo, string matCode)
        {
            try
            {
                return PDAservice.GetLSMtlRepertoryByStoresiteNo(sourceStoresiteNo, matCode);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        

        /// <summary>
        /// 根据物料取物料推荐发料数和最小包装数
        /// </summary>
        /// <param name="mtlCode">物料代码</param>
        /// <param name="siteNo">库位代码</param>
        /// <returns></returns>
        public DataSet GetMtlQtyByMtlCode(string mtlCode, string siteNo)
        {
            try
            {
                return PDAservice.GetMtlQtyByMtlCode(mtlCode, siteNo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取物料对应的编码控制属性
        /// </summary>
        /// <param name="matCode"></param>
        /// <returns></returns>
        public string GetMaName(string matCode)
        {
            string msg = PDAservice.GetMatName(matCode);
            return msg;
        }

        /// <summary>
        /// 根据物料取物料推荐发料数和最小包装数
        /// </summary>
        /// <param name="mtlCode">物料代码</param>
        /// <param name="siteNo">库位代码</param>
        /// <returns></returns>
        public string GetMtlSupplier(string StrMtlCode, string mtlCode, string mtlName, string BatchNo, string SNNo, string SupplierNoSN, string SupplierNo, string SupplierBarCode, string SupplierBarSNCode)
        {
            try
            {
                string Report = PDAservice.GetMtlSupplier(StrMtlCode, mtlCode, mtlName, BatchNo, SNNo, SupplierNo, SupplierNoSN, SupplierBarCode, SupplierBarSNCode);

                return Report;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
