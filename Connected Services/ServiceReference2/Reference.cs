﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SWP_NEW.ServiceReference2 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="userregistration_propEntity", Namespace="http://schemas.datacontract.org/2004/07/")]
    [System.SerializableAttribute()]
    public partial class userregistration_propEntity : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ADDRESSField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BLOCKField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CATEGORYField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CELLPHONE_NOField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DISTRICTField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EIN_PCField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string E_MAILField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Enterprenenur_NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string GSTINField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string INV_NAMEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string INV_USERIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LICENCE_NO_TYPEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PANField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SECTORField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SITELOCATIONField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SUBSECTORField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UNIQUEIDField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ADDRESS {
            get {
                return this.ADDRESSField;
            }
            set {
                if ((object.ReferenceEquals(this.ADDRESSField, value) != true)) {
                    this.ADDRESSField = value;
                    this.RaisePropertyChanged("ADDRESS");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BLOCK {
            get {
                return this.BLOCKField;
            }
            set {
                if ((object.ReferenceEquals(this.BLOCKField, value) != true)) {
                    this.BLOCKField = value;
                    this.RaisePropertyChanged("BLOCK");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CATEGORY {
            get {
                return this.CATEGORYField;
            }
            set {
                if ((object.ReferenceEquals(this.CATEGORYField, value) != true)) {
                    this.CATEGORYField = value;
                    this.RaisePropertyChanged("CATEGORY");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CELLPHONE_NO {
            get {
                return this.CELLPHONE_NOField;
            }
            set {
                if ((object.ReferenceEquals(this.CELLPHONE_NOField, value) != true)) {
                    this.CELLPHONE_NOField = value;
                    this.RaisePropertyChanged("CELLPHONE_NO");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DISTRICT {
            get {
                return this.DISTRICTField;
            }
            set {
                if ((object.ReferenceEquals(this.DISTRICTField, value) != true)) {
                    this.DISTRICTField = value;
                    this.RaisePropertyChanged("DISTRICT");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EIN_PC {
            get {
                return this.EIN_PCField;
            }
            set {
                if ((object.ReferenceEquals(this.EIN_PCField, value) != true)) {
                    this.EIN_PCField = value;
                    this.RaisePropertyChanged("EIN_PC");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string E_MAIL {
            get {
                return this.E_MAILField;
            }
            set {
                if ((object.ReferenceEquals(this.E_MAILField, value) != true)) {
                    this.E_MAILField = value;
                    this.RaisePropertyChanged("E_MAIL");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Enterprenenur_Name {
            get {
                return this.Enterprenenur_NameField;
            }
            set {
                if ((object.ReferenceEquals(this.Enterprenenur_NameField, value) != true)) {
                    this.Enterprenenur_NameField = value;
                    this.RaisePropertyChanged("Enterprenenur_Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string GSTIN {
            get {
                return this.GSTINField;
            }
            set {
                if ((object.ReferenceEquals(this.GSTINField, value) != true)) {
                    this.GSTINField = value;
                    this.RaisePropertyChanged("GSTIN");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string INV_NAME {
            get {
                return this.INV_NAMEField;
            }
            set {
                if ((object.ReferenceEquals(this.INV_NAMEField, value) != true)) {
                    this.INV_NAMEField = value;
                    this.RaisePropertyChanged("INV_NAME");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string INV_USERID {
            get {
                return this.INV_USERIDField;
            }
            set {
                if ((object.ReferenceEquals(this.INV_USERIDField, value) != true)) {
                    this.INV_USERIDField = value;
                    this.RaisePropertyChanged("INV_USERID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LICENCE_NO_TYPE {
            get {
                return this.LICENCE_NO_TYPEField;
            }
            set {
                if ((object.ReferenceEquals(this.LICENCE_NO_TYPEField, value) != true)) {
                    this.LICENCE_NO_TYPEField = value;
                    this.RaisePropertyChanged("LICENCE_NO_TYPE");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PAN {
            get {
                return this.PANField;
            }
            set {
                if ((object.ReferenceEquals(this.PANField, value) != true)) {
                    this.PANField = value;
                    this.RaisePropertyChanged("PAN");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SECTOR {
            get {
                return this.SECTORField;
            }
            set {
                if ((object.ReferenceEquals(this.SECTORField, value) != true)) {
                    this.SECTORField = value;
                    this.RaisePropertyChanged("SECTOR");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SITELOCATION {
            get {
                return this.SITELOCATIONField;
            }
            set {
                if ((object.ReferenceEquals(this.SITELOCATIONField, value) != true)) {
                    this.SITELOCATIONField = value;
                    this.RaisePropertyChanged("SITELOCATION");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SUBSECTOR {
            get {
                return this.SUBSECTORField;
            }
            set {
                if ((object.ReferenceEquals(this.SUBSECTORField, value) != true)) {
                    this.SUBSECTORField = value;
                    this.RaisePropertyChanged("SUBSECTOR");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UNIQUEID {
            get {
                return this.UNIQUEIDField;
            }
            set {
                if ((object.ReferenceEquals(this.UNIQUEIDField, value) != true)) {
                    this.UNIQUEIDField = value;
                    this.RaisePropertyChanged("UNIQUEID");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference2.IUserRegistration")]
    public interface IUserRegistration {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserRegistration/insertrecord", ReplyAction="http://tempuri.org/IUserRegistration/insertrecordResponse")]
        int insertrecord(SWP_NEW.ServiceReference2.userregistration_propEntity objentity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserRegistration/UPDATERECORD", ReplyAction="http://tempuri.org/IUserRegistration/UPDATERECORDResponse")]
        int UPDATERECORD(SWP_NEW.ServiceReference2.userregistration_propEntity objentity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserRegistration/CheckEINPC", ReplyAction="http://tempuri.org/IUserRegistration/CheckEINPCResponse")]
        int CheckEINPC(SWP_NEW.ServiceReference2.userregistration_propEntity objentity);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserRegistrationChannel : SWP_NEW.ServiceReference2.IUserRegistration, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserRegistrationClient : System.ServiceModel.ClientBase<SWP_NEW.ServiceReference2.IUserRegistration>, SWP_NEW.ServiceReference2.IUserRegistration {
        
        public UserRegistrationClient() {
        }
        
        public UserRegistrationClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserRegistrationClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserRegistrationClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserRegistrationClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int insertrecord(SWP_NEW.ServiceReference2.userregistration_propEntity objentity) {
            return base.Channel.insertrecord(objentity);
        }
        
        public int UPDATERECORD(SWP_NEW.ServiceReference2.userregistration_propEntity objentity) {
            return base.Channel.UPDATERECORD(objentity);
        }
        
        public int CheckEINPC(SWP_NEW.ServiceReference2.userregistration_propEntity objentity) {
            return base.Channel.CheckEINPC(objentity);
        }
    }
}
