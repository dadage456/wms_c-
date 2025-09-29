 
#pragma warning disable 1591

namespace agvtxws.agv2wmstx {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5483")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="txSoap", Namespace="http://tempuri.org/")]
    public partial class tx : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback wuliaoOperationCompleted;
        
        private System.Threading.SendOrPostCallback gongyingshangOperationCompleted;
        
        private System.Threading.SendOrPostCallback kehuOperationCompleted;
        
        private System.Threading.SendOrPostCallback tpstatus_agv_sendOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public tx() {
            this.Url = global::agvtxws.Properties.Settings.Default.agvtxws_agv2wmstx_tx;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event wuliaoCompletedEventHandler wuliaoCompleted;
        
        /// <remarks/>
        public event gongyingshangCompletedEventHandler gongyingshangCompleted;
        
        /// <remarks/>
        public event kehuCompletedEventHandler kehuCompleted;
        
        /// <remarks/>
        public event tpstatus_agv_sendCompletedEventHandler tpstatus_agv_sendCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/wuliao", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string wuliao(
                    string MATNR, 
                    string BISMT, 
                    string MAKTX, 
                    string MTART, 
                    string MTBEZ, 
                    string CLASS, 
                    string KLTXT, 
                    string MEINS, 
                    string MSEHL, 
                    string MATKL, 
                    string WGBEZ, 
                    string RESERVE_H_F1, 
                    string RESERVE_H_F2, 
                    string RESERVE_H_F3, 
                    string RESERVE_H_F4, 
                    string RESERVE_H_F5, 
                    string RESERVE_H_F6, 
                    string MSTAE, 
                    string I_MATNR, 
                    string WERKS, 
                    string BKLAS, 
                    string BWTAR, 
                    string XCHPF, 
                    string SERNP, 
                    string SERAILTXT, 
                    string LVORM, 
                    string MMSTA, 
                    string RESERVE_I_F1, 
                    string RESERVE_I_F2, 
                    string RESERVE_I_F3, 
                    string RESERVE_I_F4, 
                    string RESERVE_I_F5, 
                    string RESERVE_I_F6) {
            object[] results = this.Invoke("wuliao", new object[] {
                        MATNR,
                        BISMT,
                        MAKTX,
                        MTART,
                        MTBEZ,
                        CLASS,
                        KLTXT,
                        MEINS,
                        MSEHL,
                        MATKL,
                        WGBEZ,
                        RESERVE_H_F1,
                        RESERVE_H_F2,
                        RESERVE_H_F3,
                        RESERVE_H_F4,
                        RESERVE_H_F5,
                        RESERVE_H_F6,
                        MSTAE,
                        I_MATNR,
                        WERKS,
                        BKLAS,
                        BWTAR,
                        XCHPF,
                        SERNP,
                        SERAILTXT,
                        LVORM,
                        MMSTA,
                        RESERVE_I_F1,
                        RESERVE_I_F2,
                        RESERVE_I_F3,
                        RESERVE_I_F4,
                        RESERVE_I_F5,
                        RESERVE_I_F6});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void wuliaoAsync(
                    string MATNR, 
                    string BISMT, 
                    string MAKTX, 
                    string MTART, 
                    string MTBEZ, 
                    string CLASS, 
                    string KLTXT, 
                    string MEINS, 
                    string MSEHL, 
                    string MATKL, 
                    string WGBEZ, 
                    string RESERVE_H_F1, 
                    string RESERVE_H_F2, 
                    string RESERVE_H_F3, 
                    string RESERVE_H_F4, 
                    string RESERVE_H_F5, 
                    string RESERVE_H_F6, 
                    string MSTAE, 
                    string I_MATNR, 
                    string WERKS, 
                    string BKLAS, 
                    string BWTAR, 
                    string XCHPF, 
                    string SERNP, 
                    string SERAILTXT, 
                    string LVORM, 
                    string MMSTA, 
                    string RESERVE_I_F1, 
                    string RESERVE_I_F2, 
                    string RESERVE_I_F3, 
                    string RESERVE_I_F4, 
                    string RESERVE_I_F5, 
                    string RESERVE_I_F6) {
            this.wuliaoAsync(MATNR, BISMT, MAKTX, MTART, MTBEZ, CLASS, KLTXT, MEINS, MSEHL, MATKL, WGBEZ, RESERVE_H_F1, RESERVE_H_F2, RESERVE_H_F3, RESERVE_H_F4, RESERVE_H_F5, RESERVE_H_F6, MSTAE, I_MATNR, WERKS, BKLAS, BWTAR, XCHPF, SERNP, SERAILTXT, LVORM, MMSTA, RESERVE_I_F1, RESERVE_I_F2, RESERVE_I_F3, RESERVE_I_F4, RESERVE_I_F5, RESERVE_I_F6, null);
        }
        
        /// <remarks/>
        public void wuliaoAsync(
                    string MATNR, 
                    string BISMT, 
                    string MAKTX, 
                    string MTART, 
                    string MTBEZ, 
                    string CLASS, 
                    string KLTXT, 
                    string MEINS, 
                    string MSEHL, 
                    string MATKL, 
                    string WGBEZ, 
                    string RESERVE_H_F1, 
                    string RESERVE_H_F2, 
                    string RESERVE_H_F3, 
                    string RESERVE_H_F4, 
                    string RESERVE_H_F5, 
                    string RESERVE_H_F6, 
                    string MSTAE, 
                    string I_MATNR, 
                    string WERKS, 
                    string BKLAS, 
                    string BWTAR, 
                    string XCHPF, 
                    string SERNP, 
                    string SERAILTXT, 
                    string LVORM, 
                    string MMSTA, 
                    string RESERVE_I_F1, 
                    string RESERVE_I_F2, 
                    string RESERVE_I_F3, 
                    string RESERVE_I_F4, 
                    string RESERVE_I_F5, 
                    string RESERVE_I_F6, 
                    object userState) {
            if ((this.wuliaoOperationCompleted == null)) {
                this.wuliaoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnwuliaoOperationCompleted);
            }
            this.InvokeAsync("wuliao", new object[] {
                        MATNR,
                        BISMT,
                        MAKTX,
                        MTART,
                        MTBEZ,
                        CLASS,
                        KLTXT,
                        MEINS,
                        MSEHL,
                        MATKL,
                        WGBEZ,
                        RESERVE_H_F1,
                        RESERVE_H_F2,
                        RESERVE_H_F3,
                        RESERVE_H_F4,
                        RESERVE_H_F5,
                        RESERVE_H_F6,
                        MSTAE,
                        I_MATNR,
                        WERKS,
                        BKLAS,
                        BWTAR,
                        XCHPF,
                        SERNP,
                        SERAILTXT,
                        LVORM,
                        MMSTA,
                        RESERVE_I_F1,
                        RESERVE_I_F2,
                        RESERVE_I_F3,
                        RESERVE_I_F4,
                        RESERVE_I_F5,
                        RESERVE_I_F6}, this.wuliaoOperationCompleted, userState);
        }
        
        private void OnwuliaoOperationCompleted(object arg) {
            if ((this.wuliaoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.wuliaoCompleted(this, new wuliaoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/gongyingshang", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public responseMsgHeader gongyingshang(gongyingshang_in request) {
            object[] results = this.Invoke("gongyingshang", new object[] {
                        request});
            return ((responseMsgHeader)(results[0]));
        }
        
        /// <remarks/>
        public void gongyingshangAsync(gongyingshang_in request) {
            this.gongyingshangAsync(request, null);
        }
        
        /// <remarks/>
        public void gongyingshangAsync(gongyingshang_in request, object userState) {
            if ((this.gongyingshangOperationCompleted == null)) {
                this.gongyingshangOperationCompleted = new System.Threading.SendOrPostCallback(this.OngongyingshangOperationCompleted);
            }
            this.InvokeAsync("gongyingshang", new object[] {
                        request}, this.gongyingshangOperationCompleted, userState);
        }
        
        private void OngongyingshangOperationCompleted(object arg) {
            if ((this.gongyingshangCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.gongyingshangCompleted(this, new gongyingshangCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/kehu", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string kehu(string KUNNR, string NAME1, string ADRNR, string PSON1, string RESERVE_F1, string RESERVE_F2, string RESERVE_F3, string RESERVE_F4, string RESERVE_F5) {
            object[] results = this.Invoke("kehu", new object[] {
                        KUNNR,
                        NAME1,
                        ADRNR,
                        PSON1,
                        RESERVE_F1,
                        RESERVE_F2,
                        RESERVE_F3,
                        RESERVE_F4,
                        RESERVE_F5});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void kehuAsync(string KUNNR, string NAME1, string ADRNR, string PSON1, string RESERVE_F1, string RESERVE_F2, string RESERVE_F3, string RESERVE_F4, string RESERVE_F5) {
            this.kehuAsync(KUNNR, NAME1, ADRNR, PSON1, RESERVE_F1, RESERVE_F2, RESERVE_F3, RESERVE_F4, RESERVE_F5, null);
        }
        
        /// <remarks/>
        public void kehuAsync(string KUNNR, string NAME1, string ADRNR, string PSON1, string RESERVE_F1, string RESERVE_F2, string RESERVE_F3, string RESERVE_F4, string RESERVE_F5, object userState) {
            if ((this.kehuOperationCompleted == null)) {
                this.kehuOperationCompleted = new System.Threading.SendOrPostCallback(this.OnkehuOperationCompleted);
            }
            this.InvokeAsync("kehu", new object[] {
                        KUNNR,
                        NAME1,
                        ADRNR,
                        PSON1,
                        RESERVE_F1,
                        RESERVE_F2,
                        RESERVE_F3,
                        RESERVE_F4,
                        RESERVE_F5}, this.kehuOperationCompleted, userState);
        }
        
        private void OnkehuOperationCompleted(object arg) {
            if ((this.kehuCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.kehuCompleted(this, new kehuCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/tpstatus_agv_send", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public clsPalletStatusRtn tpstatus_agv_send(clsPalletStatus t) {
            object[] results = this.Invoke("tpstatus_agv_send", new object[] {
                        t});
            return ((clsPalletStatusRtn)(results[0]));
        }
        
        /// <remarks/>
        public void tpstatus_agv_sendAsync(clsPalletStatus t) {
            this.tpstatus_agv_sendAsync(t, null);
        }
        
        /// <remarks/>
        public void tpstatus_agv_sendAsync(clsPalletStatus t, object userState) {
            if ((this.tpstatus_agv_sendOperationCompleted == null)) {
                this.tpstatus_agv_sendOperationCompleted = new System.Threading.SendOrPostCallback(this.Ontpstatus_agv_sendOperationCompleted);
            }
            this.InvokeAsync("tpstatus_agv_send", new object[] {
                        t}, this.tpstatus_agv_sendOperationCompleted, userState);
        }
        
        private void Ontpstatus_agv_sendOperationCompleted(object arg) {
            if ((this.tpstatus_agv_sendCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.tpstatus_agv_sendCompleted(this, new tpstatus_agv_sendCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5494")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class gongyingshang_in {
        
        private MessageFlag iv_msg_flagField;
        
        private MessageHead is_msg_headField;
        
        private gongyingshang_data[] it_rowField;
        
        /// <remarks/>
        public MessageFlag iv_msg_flag {
            get {
                return this.iv_msg_flagField;
            }
            set {
                this.iv_msg_flagField = value;
            }
        }
        
        /// <remarks/>
        public MessageHead is_msg_head {
            get {
                return this.is_msg_headField;
            }
            set {
                this.is_msg_headField = value;
            }
        }
        
        /// <remarks/>
        public gongyingshang_data[] it_row {
            get {
                return this.it_rowField;
            }
            set {
                this.it_rowField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5494")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class MessageFlag {
        
        private string flagField;
        
        /// <remarks/>
        public string flag {
            get {
                return this.flagField;
            }
            set {
                this.flagField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5494")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class clsPalletStatusRtn {
        
        private string eRPOrderIDField;
        
        private string transactionIDField;
        
        private string workCenterCodeField;
        
        private string palletCodeField;
        
        private string ynTxOKField;
        
        /// <remarks/>
        public string ERPOrderID {
            get {
                return this.eRPOrderIDField;
            }
            set {
                this.eRPOrderIDField = value;
            }
        }
        
        /// <remarks/>
        public string TransactionID {
            get {
                return this.transactionIDField;
            }
            set {
                this.transactionIDField = value;
            }
        }
        
        /// <remarks/>
        public string WorkCenterCode {
            get {
                return this.workCenterCodeField;
            }
            set {
                this.workCenterCodeField = value;
            }
        }
        
        /// <remarks/>
        public string PalletCode {
            get {
                return this.palletCodeField;
            }
            set {
                this.palletCodeField = value;
            }
        }
        
        /// <remarks/>
        public string YnTxOK {
            get {
                return this.ynTxOKField;
            }
            set {
                this.ynTxOKField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5494")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class clsPalletStatus {
        
        private string eRPOrderIDField;
        
        private string transactionIDField;
        
        private string organizationCodeField;
        
        private string workCenterCodeField;
        
        private string palletCodeField;
        
        private string takePortField;
        
        private string takeStatusField;
        
        private string takeTimeField;
        
        /// <remarks/>
        public string ERPOrderID {
            get {
                return this.eRPOrderIDField;
            }
            set {
                this.eRPOrderIDField = value;
            }
        }
        
        /// <remarks/>
        public string TransactionID {
            get {
                return this.transactionIDField;
            }
            set {
                this.transactionIDField = value;
            }
        }
        
        /// <remarks/>
        public string OrganizationCode {
            get {
                return this.organizationCodeField;
            }
            set {
                this.organizationCodeField = value;
            }
        }
        
        /// <remarks/>
        public string WorkCenterCode {
            get {
                return this.workCenterCodeField;
            }
            set {
                this.workCenterCodeField = value;
            }
        }
        
        /// <remarks/>
        public string PalletCode {
            get {
                return this.palletCodeField;
            }
            set {
                this.palletCodeField = value;
            }
        }
        
        /// <remarks/>
        public string TakePort {
            get {
                return this.takePortField;
            }
            set {
                this.takePortField = value;
            }
        }
        
        /// <remarks/>
        public string TakeStatus {
            get {
                return this.takeStatusField;
            }
            set {
                this.takeStatusField = value;
            }
        }
        
        /// <remarks/>
        public string TakeTime {
            get {
                return this.takeTimeField;
            }
            set {
                this.takeTimeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5494")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class responseMsgHeader {
        
        private int codeField;
        
        private string msgField;
        
        /// <remarks/>
        public int code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        public string msg {
            get {
                return this.msgField;
            }
            set {
                this.msgField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5494")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class gongyingshang_data {
        
        private string lIFNRField;
        
        private string nAME1Field;
        
        private string sTRASField;
        
        private string kTOKKField;
        
        private string rESERVE_F1Field;
        
        private string rESERVE_F2Field;
        
        private string rESERVE_F3Field;
        
        private string rESERVE_F4Field;
        
        private string rESERVE_F5Field;
        
        /// <remarks/>
        public string LIFNR {
            get {
                return this.lIFNRField;
            }
            set {
                this.lIFNRField = value;
            }
        }
        
        /// <remarks/>
        public string NAME1 {
            get {
                return this.nAME1Field;
            }
            set {
                this.nAME1Field = value;
            }
        }
        
        /// <remarks/>
        public string STRAS {
            get {
                return this.sTRASField;
            }
            set {
                this.sTRASField = value;
            }
        }
        
        /// <remarks/>
        public string KTOKK {
            get {
                return this.kTOKKField;
            }
            set {
                this.kTOKKField = value;
            }
        }
        
        /// <remarks/>
        public string RESERVE_F1 {
            get {
                return this.rESERVE_F1Field;
            }
            set {
                this.rESERVE_F1Field = value;
            }
        }
        
        /// <remarks/>
        public string RESERVE_F2 {
            get {
                return this.rESERVE_F2Field;
            }
            set {
                this.rESERVE_F2Field = value;
            }
        }
        
        /// <remarks/>
        public string RESERVE_F3 {
            get {
                return this.rESERVE_F3Field;
            }
            set {
                this.rESERVE_F3Field = value;
            }
        }
        
        /// <remarks/>
        public string RESERVE_F4 {
            get {
                return this.rESERVE_F4Field;
            }
            set {
                this.rESERVE_F4Field = value;
            }
        }
        
        /// <remarks/>
        public string RESERVE_F5 {
            get {
                return this.rESERVE_F5Field;
            }
            set {
                this.rESERVE_F5Field = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5494")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class MessageHead {
        
        private string mANDTField;
        
        private string gUIDField;
        
        private string pROXY_IDField;
        
        private string sYSTEM_IDField;
        
        private string oPERATORField;
        
        private string sPRASField;
        
        private string iNTERFACE_IDField;
        
        private string sENDERField;
        
        private string rECIVERField;
        
        private string sENDTIMEField;
        
        /// <remarks/>
        public string MANDT {
            get {
                return this.mANDTField;
            }
            set {
                this.mANDTField = value;
            }
        }
        
        /// <remarks/>
        public string GUID {
            get {
                return this.gUIDField;
            }
            set {
                this.gUIDField = value;
            }
        }
        
        /// <remarks/>
        public string PROXY_ID {
            get {
                return this.pROXY_IDField;
            }
            set {
                this.pROXY_IDField = value;
            }
        }
        
        /// <remarks/>
        public string SYSTEM_ID {
            get {
                return this.sYSTEM_IDField;
            }
            set {
                this.sYSTEM_IDField = value;
            }
        }
        
        /// <remarks/>
        public string OPERATOR {
            get {
                return this.oPERATORField;
            }
            set {
                this.oPERATORField = value;
            }
        }
        
        /// <remarks/>
        public string SPRAS {
            get {
                return this.sPRASField;
            }
            set {
                this.sPRASField = value;
            }
        }
        
        /// <remarks/>
        public string INTERFACE_ID {
            get {
                return this.iNTERFACE_IDField;
            }
            set {
                this.iNTERFACE_IDField = value;
            }
        }
        
        /// <remarks/>
        public string SENDER {
            get {
                return this.sENDERField;
            }
            set {
                this.sENDERField = value;
            }
        }
        
        /// <remarks/>
        public string RECIVER {
            get {
                return this.rECIVERField;
            }
            set {
                this.rECIVERField = value;
            }
        }
        
        /// <remarks/>
        public string SENDTIME {
            get {
                return this.sENDTIMEField;
            }
            set {
                this.sENDTIMEField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5483")]
    public delegate void wuliaoCompletedEventHandler(object sender, wuliaoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5483")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class wuliaoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal wuliaoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5483")]
    public delegate void gongyingshangCompletedEventHandler(object sender, gongyingshangCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5483")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class gongyingshangCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal gongyingshangCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public responseMsgHeader Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((responseMsgHeader)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5483")]
    public delegate void kehuCompletedEventHandler(object sender, kehuCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5483")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class kehuCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal kehuCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5483")]
    public delegate void tpstatus_agv_sendCompletedEventHandler(object sender, tpstatus_agv_sendCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5483")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class tpstatus_agv_sendCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal tpstatus_agv_sendCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public clsPalletStatusRtn Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((clsPalletStatusRtn)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591