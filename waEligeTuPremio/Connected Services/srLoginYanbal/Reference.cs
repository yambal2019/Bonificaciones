﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace waEligeTuPremio.srLoginYanbal {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://beans.ws.integracionyanbalstore.yanbal.com", ConfigurationName="srLoginYanbal.WSMantenimientoUsuarios")]
    public interface WSMantenimientoUsuarios {
        
        // CODEGEN: El parámetro 'validaUsuariosObjReturn' requiere información adicional de esquema que no se puede capturar con el modo de parámetros. El atributo específico es 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="validaUsuariosObj", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="validaUsuariosObjReturn")]
        waEligeTuPremio.srLoginYanbal.validaUsuariosObjResponse validaUsuariosObj(waEligeTuPremio.srLoginYanbal.validaUsuariosObjRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="validaUsuariosObj", ReplyAction="*")]
        System.Threading.Tasks.Task<waEligeTuPremio.srLoginYanbal.validaUsuariosObjResponse> validaUsuariosObjAsync(waEligeTuPremio.srLoginYanbal.validaUsuariosObjRequest request);
        
        // CODEGEN: El parámetro 'validaUsuariosReturn' requiere información adicional de esquema que no se puede capturar con el modo de parámetros. El atributo específico es 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="validaUsuarios", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="validaUsuariosReturn")]
        waEligeTuPremio.srLoginYanbal.validaUsuariosResponse validaUsuarios(waEligeTuPremio.srLoginYanbal.validaUsuariosRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="validaUsuarios", ReplyAction="*")]
        System.Threading.Tasks.Task<waEligeTuPremio.srLoginYanbal.validaUsuariosResponse> validaUsuariosAsync(waEligeTuPremio.srLoginYanbal.validaUsuariosRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://solicitud.vusuarios.tramas.ws.integracionyanbalstore.yanbal.com")]
    public partial class IntegracionWSReq : object, System.ComponentModel.INotifyPropertyChanged {
        
        private Cabecera cabeceraField;
        
        private Detalle detalleField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=0)]
        public Cabecera cabecera {
            get {
                return this.cabeceraField;
            }
            set {
                this.cabeceraField = value;
                this.RaisePropertyChanged("cabecera");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=1)]
        public Detalle detalle {
            get {
                return this.detalleField;
            }
            set {
                this.detalleField = value;
                this.RaisePropertyChanged("detalle");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://solicitud.comun.tramas.ws.integracionyanbalstore.yanbal.com")]
    public partial class Cabecera : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codigoInterfazField;
        
        private string usuarioAplicacionField;
        
        private string codigoAplicacionField;
        
        private string codigoPaisField;
        
        private CodigoPaisOD[] codigosPaisODField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=0)]
        public string codigoInterfaz {
            get {
                return this.codigoInterfazField;
            }
            set {
                this.codigoInterfazField = value;
                this.RaisePropertyChanged("codigoInterfaz");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=1)]
        public string usuarioAplicacion {
            get {
                return this.usuarioAplicacionField;
            }
            set {
                this.usuarioAplicacionField = value;
                this.RaisePropertyChanged("usuarioAplicacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=2)]
        public string codigoAplicacion {
            get {
                return this.codigoAplicacionField;
            }
            set {
                this.codigoAplicacionField = value;
                this.RaisePropertyChanged("codigoAplicacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=3)]
        public string codigoPais {
            get {
                return this.codigoPaisField;
            }
            set {
                this.codigoPaisField = value;
                this.RaisePropertyChanged("codigoPais");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=4)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CodigoPaisOD[] codigosPaisOD {
            get {
                return this.codigosPaisODField;
            }
            set {
                this.codigosPaisODField = value;
                this.RaisePropertyChanged("codigosPaisOD");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://comun.tramas.ws.integracionyanbalstore.yanbal.com")]
    public partial class CodigoPaisOD : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string valorField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=0)]
        public string valor {
            get {
                return this.valorField;
            }
            set {
                this.valorField = value;
                this.RaisePropertyChanged("valor");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://respuesta.vusuarios.tramas.ws.integracionyanbalstore.yanbal.com")]
    public partial class Datos : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string flagValidacionField;
        
        private string tipoUsuarioField;
        
        private string usuarioField;
        
        private string statusField;
        
        private string perfilField;
        
        private string nombreCompletoField;
        
        private string emailField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=0)]
        public string flagValidacion {
            get {
                return this.flagValidacionField;
            }
            set {
                this.flagValidacionField = value;
                this.RaisePropertyChanged("flagValidacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=1)]
        public string tipoUsuario {
            get {
                return this.tipoUsuarioField;
            }
            set {
                this.tipoUsuarioField = value;
                this.RaisePropertyChanged("tipoUsuario");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=2)]
        public string usuario {
            get {
                return this.usuarioField;
            }
            set {
                this.usuarioField = value;
                this.RaisePropertyChanged("usuario");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=3)]
        public string status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
                this.RaisePropertyChanged("status");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=4)]
        public string perfil {
            get {
                return this.perfilField;
            }
            set {
                this.perfilField = value;
                this.RaisePropertyChanged("perfil");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=5)]
        public string nombreCompleto {
            get {
                return this.nombreCompletoField;
            }
            set {
                this.nombreCompletoField = value;
                this.RaisePropertyChanged("nombreCompleto");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=6)]
        public string email {
            get {
                return this.emailField;
            }
            set {
                this.emailField = value;
                this.RaisePropertyChanged("email");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://respuesta.vusuarios.tramas.ws.integracionyanbalstore.yanbal.com")]
    public partial class Respuesta : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codigoRespuestaField;
        
        private string mensajeRespuestaField;
        
        private Datos datosField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=0)]
        public string codigoRespuesta {
            get {
                return this.codigoRespuestaField;
            }
            set {
                this.codigoRespuestaField = value;
                this.RaisePropertyChanged("codigoRespuesta");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=1)]
        public string mensajeRespuesta {
            get {
                return this.mensajeRespuestaField;
            }
            set {
                this.mensajeRespuestaField = value;
                this.RaisePropertyChanged("mensajeRespuesta");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=2)]
        public Datos datos {
            get {
                return this.datosField;
            }
            set {
                this.datosField = value;
                this.RaisePropertyChanged("datos");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="Detalle", Namespace="http://respuesta.vusuarios.tramas.ws.integracionyanbalstore.yanbal.com")]
    public partial class Detalle1 : object, System.ComponentModel.INotifyPropertyChanged {
        
        private Respuesta respuestaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=0)]
        public Respuesta respuesta {
            get {
                return this.respuestaField;
            }
            set {
                this.respuestaField = value;
                this.RaisePropertyChanged("respuesta");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="Cabecera", Namespace="http://respuesta.comun.tramas.ws.integracionyanbalstore.yanbal.com")]
    public partial class Cabecera1 : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codigoInterfazField;
        
        private string usuarioAplicacionField;
        
        private string codigoAplicacionField;
        
        private string codigoPaisField;
        
        private CodigoPaisOD[] codigosPaisODField;
        
        private string idTransaccionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=0)]
        public string codigoInterfaz {
            get {
                return this.codigoInterfazField;
            }
            set {
                this.codigoInterfazField = value;
                this.RaisePropertyChanged("codigoInterfaz");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=1)]
        public string usuarioAplicacion {
            get {
                return this.usuarioAplicacionField;
            }
            set {
                this.usuarioAplicacionField = value;
                this.RaisePropertyChanged("usuarioAplicacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=2)]
        public string codigoAplicacion {
            get {
                return this.codigoAplicacionField;
            }
            set {
                this.codigoAplicacionField = value;
                this.RaisePropertyChanged("codigoAplicacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=3)]
        public string codigoPais {
            get {
                return this.codigoPaisField;
            }
            set {
                this.codigoPaisField = value;
                this.RaisePropertyChanged("codigoPais");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=4)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CodigoPaisOD[] codigosPaisOD {
            get {
                return this.codigosPaisODField;
            }
            set {
                this.codigosPaisODField = value;
                this.RaisePropertyChanged("codigosPaisOD");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=5)]
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                this.idTransaccionField = value;
                this.RaisePropertyChanged("idTransaccion");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://respuesta.vusuarios.tramas.ws.integracionyanbalstore.yanbal.com")]
    public partial class IntegracionWSResp : object, System.ComponentModel.INotifyPropertyChanged {
        
        private Cabecera1 cabeceraField;
        
        private Detalle1 detalleField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=0)]
        public Cabecera1 cabecera {
            get {
                return this.cabeceraField;
            }
            set {
                this.cabeceraField = value;
                this.RaisePropertyChanged("cabecera");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=1)]
        public Detalle1 detalle {
            get {
                return this.detalleField;
            }
            set {
                this.detalleField = value;
                this.RaisePropertyChanged("detalle");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://solicitud.vusuarios.tramas.ws.integracionyanbalstore.yanbal.com")]
    public partial class Parametros : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tipoUsuarioField;
        
        private string usuarioField;
        
        private string passwordField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=0)]
        public string tipoUsuario {
            get {
                return this.tipoUsuarioField;
            }
            set {
                this.tipoUsuarioField = value;
                this.RaisePropertyChanged("tipoUsuario");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=1)]
        public string usuario {
            get {
                return this.usuarioField;
            }
            set {
                this.usuarioField = value;
                this.RaisePropertyChanged("usuario");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=2)]
        public string password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
                this.RaisePropertyChanged("password");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://solicitud.vusuarios.tramas.ws.integracionyanbalstore.yanbal.com")]
    public partial class Detalle : object, System.ComponentModel.INotifyPropertyChanged {
        
        private Parametros parametrosField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=0)]
        public Parametros parametros {
            get {
                return this.parametrosField;
            }
            set {
                this.parametrosField = value;
                this.RaisePropertyChanged("parametros");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="validaUsuariosObj", WrapperNamespace="http://beans.ws.integracionyanbalstore.yanbal.com", IsWrapped=true)]
    public partial class validaUsuariosObjRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://beans.ws.integracionyanbalstore.yanbal.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public waEligeTuPremio.srLoginYanbal.IntegracionWSReq integracionWSReq;
        
        public validaUsuariosObjRequest() {
        }
        
        public validaUsuariosObjRequest(waEligeTuPremio.srLoginYanbal.IntegracionWSReq integracionWSReq) {
            this.integracionWSReq = integracionWSReq;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="validaUsuariosObjResponse", WrapperNamespace="http://beans.ws.integracionyanbalstore.yanbal.com", IsWrapped=true)]
    public partial class validaUsuariosObjResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://beans.ws.integracionyanbalstore.yanbal.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public waEligeTuPremio.srLoginYanbal.IntegracionWSResp validaUsuariosObjReturn;
        
        public validaUsuariosObjResponse() {
        }
        
        public validaUsuariosObjResponse(waEligeTuPremio.srLoginYanbal.IntegracionWSResp validaUsuariosObjReturn) {
            this.validaUsuariosObjReturn = validaUsuariosObjReturn;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="validaUsuarios", WrapperNamespace="http://beans.ws.integracionyanbalstore.yanbal.com", IsWrapped=true)]
    public partial class validaUsuariosRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://beans.ws.integracionyanbalstore.yanbal.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string tramaEntradaXML;
        
        public validaUsuariosRequest() {
        }
        
        public validaUsuariosRequest(string tramaEntradaXML) {
            this.tramaEntradaXML = tramaEntradaXML;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="validaUsuariosResponse", WrapperNamespace="http://beans.ws.integracionyanbalstore.yanbal.com", IsWrapped=true)]
    public partial class validaUsuariosResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://beans.ws.integracionyanbalstore.yanbal.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string validaUsuariosReturn;
        
        public validaUsuariosResponse() {
        }
        
        public validaUsuariosResponse(string validaUsuariosReturn) {
            this.validaUsuariosReturn = validaUsuariosReturn;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WSMantenimientoUsuariosChannel : waEligeTuPremio.srLoginYanbal.WSMantenimientoUsuarios, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WSMantenimientoUsuariosClient : System.ServiceModel.ClientBase<waEligeTuPremio.srLoginYanbal.WSMantenimientoUsuarios>, waEligeTuPremio.srLoginYanbal.WSMantenimientoUsuarios {
        
        public WSMantenimientoUsuariosClient() {
        }
        
        public WSMantenimientoUsuariosClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WSMantenimientoUsuariosClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSMantenimientoUsuariosClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSMantenimientoUsuariosClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        waEligeTuPremio.srLoginYanbal.validaUsuariosObjResponse waEligeTuPremio.srLoginYanbal.WSMantenimientoUsuarios.validaUsuariosObj(waEligeTuPremio.srLoginYanbal.validaUsuariosObjRequest request) {
            return base.Channel.validaUsuariosObj(request);
        }
        
        public waEligeTuPremio.srLoginYanbal.IntegracionWSResp validaUsuariosObj(waEligeTuPremio.srLoginYanbal.IntegracionWSReq integracionWSReq) {
            waEligeTuPremio.srLoginYanbal.validaUsuariosObjRequest inValue = new waEligeTuPremio.srLoginYanbal.validaUsuariosObjRequest();
            inValue.integracionWSReq = integracionWSReq;
            waEligeTuPremio.srLoginYanbal.validaUsuariosObjResponse retVal = ((waEligeTuPremio.srLoginYanbal.WSMantenimientoUsuarios)(this)).validaUsuariosObj(inValue);
            return retVal.validaUsuariosObjReturn;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<waEligeTuPremio.srLoginYanbal.validaUsuariosObjResponse> waEligeTuPremio.srLoginYanbal.WSMantenimientoUsuarios.validaUsuariosObjAsync(waEligeTuPremio.srLoginYanbal.validaUsuariosObjRequest request) {
            return base.Channel.validaUsuariosObjAsync(request);
        }
        
        public System.Threading.Tasks.Task<waEligeTuPremio.srLoginYanbal.validaUsuariosObjResponse> validaUsuariosObjAsync(waEligeTuPremio.srLoginYanbal.IntegracionWSReq integracionWSReq) {
            waEligeTuPremio.srLoginYanbal.validaUsuariosObjRequest inValue = new waEligeTuPremio.srLoginYanbal.validaUsuariosObjRequest();
            inValue.integracionWSReq = integracionWSReq;
            return ((waEligeTuPremio.srLoginYanbal.WSMantenimientoUsuarios)(this)).validaUsuariosObjAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        waEligeTuPremio.srLoginYanbal.validaUsuariosResponse waEligeTuPremio.srLoginYanbal.WSMantenimientoUsuarios.validaUsuarios(waEligeTuPremio.srLoginYanbal.validaUsuariosRequest request) {
            return base.Channel.validaUsuarios(request);
        }
        
        public string validaUsuarios(string tramaEntradaXML) {
            waEligeTuPremio.srLoginYanbal.validaUsuariosRequest inValue = new waEligeTuPremio.srLoginYanbal.validaUsuariosRequest();
            inValue.tramaEntradaXML = tramaEntradaXML;
            waEligeTuPremio.srLoginYanbal.validaUsuariosResponse retVal = ((waEligeTuPremio.srLoginYanbal.WSMantenimientoUsuarios)(this)).validaUsuarios(inValue);
            return retVal.validaUsuariosReturn;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<waEligeTuPremio.srLoginYanbal.validaUsuariosResponse> waEligeTuPremio.srLoginYanbal.WSMantenimientoUsuarios.validaUsuariosAsync(waEligeTuPremio.srLoginYanbal.validaUsuariosRequest request) {
            return base.Channel.validaUsuariosAsync(request);
        }
        
        public System.Threading.Tasks.Task<waEligeTuPremio.srLoginYanbal.validaUsuariosResponse> validaUsuariosAsync(string tramaEntradaXML) {
            waEligeTuPremio.srLoginYanbal.validaUsuariosRequest inValue = new waEligeTuPremio.srLoginYanbal.validaUsuariosRequest();
            inValue.tramaEntradaXML = tramaEntradaXML;
            return ((waEligeTuPremio.srLoginYanbal.WSMantenimientoUsuarios)(this)).validaUsuariosAsync(inValue);
        }
    }
}