﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Basta.Proxy {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Proxy.IChatService", CallbackContract=typeof(Basta.Proxy.IChatServiceCallback))]
    public interface IChatService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/Connect")]
        void Connect(Database.Entity.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/Connect")]
        System.Threading.Tasks.Task ConnectAsync(Database.Entity.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SendMessage")]
        void SendMessage(Domain.Domain.Message message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SendMessage")]
        System.Threading.Tasks.Task SendMessageAsync(Domain.Domain.Message message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatService/GetConnectedUsers", ReplyAction="http://tempuri.org/IChatService/GetConnectedUsersResponse")]
        Database.Entity.Player[] GetConnectedUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatService/GetConnectedUsers", ReplyAction="http://tempuri.org/IChatService/GetConnectedUsersResponse")]
        System.Threading.Tasks.Task<Database.Entity.Player[]> GetConnectedUsersAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChatServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/ReciveMessage")]
        void ReciveMessage(Database.Entity.Player player, Domain.Domain.Message message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChatServiceChannel : Basta.Proxy.IChatService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ChatServiceClient : System.ServiceModel.DuplexClientBase<Basta.Proxy.IChatService>, Basta.Proxy.IChatService {
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Connect(Database.Entity.Player player) {
            base.Channel.Connect(player);
        }
        
        public System.Threading.Tasks.Task ConnectAsync(Database.Entity.Player player) {
            return base.Channel.ConnectAsync(player);
        }
        
        public void SendMessage(Domain.Domain.Message message) {
            base.Channel.SendMessage(message);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(Domain.Domain.Message message) {
            return base.Channel.SendMessageAsync(message);
        }
        
        public Database.Entity.Player[] GetConnectedUsers() {
            return base.Channel.GetConnectedUsers();
        }
        
        public System.Threading.Tasks.Task<Database.Entity.Player[]> GetConnectedUsersAsync() {
            return base.Channel.GetConnectedUsersAsync();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Proxy.ILoginService")]
    public interface ILoginService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/Login", ReplyAction="http://tempuri.org/ILoginService/LoginResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Basta.Contracts.Faults.AccessAccountNotFoundFault), Action="http://tempuri.org/ILoginService/LoginAccessAccountNotFoundFaultFault", Name="AccessAccountNotFoundFault", Namespace="http://schemas.datacontract.org/2004/07/Basta.Contracts.Faults")]
        [System.ServiceModel.FaultContractAttribute(typeof(Basta.Contracts.Faults.BannedAccountFault), Action="http://tempuri.org/ILoginService/LoginBannedAccountFaultFault", Name="BannedAccountFault", Namespace="http://schemas.datacontract.org/2004/07/Basta.Contracts.Faults")]
        [System.ServiceModel.FaultContractAttribute(typeof(Basta.Contracts.Faults.LimitReachedFault), Action="http://tempuri.org/ILoginService/LoginLimitReachedFaultFault", Name="LimitReachedFault", Namespace="http://schemas.datacontract.org/2004/07/Basta.Contracts.Faults")]
        Database.Entity.Player Login(string macAddress, string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/Login", ReplyAction="http://tempuri.org/ILoginService/LoginResponse")]
        System.Threading.Tasks.Task<Database.Entity.Player> LoginAsync(string macAddress, string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/SignUp", ReplyAction="http://tempuri.org/ILoginService/SignUpResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Basta.Contracts.Faults.EmailAlreadyRegisteredFault), Action="http://tempuri.org/ILoginService/SignUpEmailAlreadyRegisteredFaultFault", Name="EmailAlreadyRegisteredFault", Namespace="http://schemas.datacontract.org/2004/07/Basta.Contracts.Faults")]
        [System.ServiceModel.FaultContractAttribute(typeof(Basta.Contracts.Faults.UsernameRegisteredAlreadyFault), Action="http://tempuri.org/ILoginService/SignUpUsernameRegisteredAlreadyFaultFault", Name="UsernameRegisteredAlreadyFault", Namespace="http://schemas.datacontract.org/2004/07/Basta.Contracts.Faults")]
        bool SignUp(Database.Entity.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/SignUp", ReplyAction="http://tempuri.org/ILoginService/SignUpResponse")]
        System.Threading.Tasks.Task<bool> SignUpAsync(Database.Entity.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/RecoverPassword", ReplyAction="http://tempuri.org/ILoginService/RecoverPasswordResponse")]
        string RecoverPassword(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/RecoverPassword", ReplyAction="http://tempuri.org/ILoginService/RecoverPasswordResponse")]
        System.Threading.Tasks.Task<string> RecoverPasswordAsync(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/ChangePassword", ReplyAction="http://tempuri.org/ILoginService/ChangePasswordResponse")]
        bool ChangePassword(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/ChangePassword", ReplyAction="http://tempuri.org/ILoginService/ChangePasswordResponse")]
        System.Threading.Tasks.Task<bool> ChangePasswordAsync(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/GenerateCode", ReplyAction="http://tempuri.org/ILoginService/GenerateCodeResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Basta.Contracts.Faults.EmailNotRegisteredYetFault), Action="http://tempuri.org/ILoginService/GenerateCodeEmailNotRegisteredYetFaultFault", Name="EmailNotRegisteredYetFault", Namespace="http://schemas.datacontract.org/2004/07/Basta.Contracts.Faults")]
        string GenerateCode(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/GenerateCode", ReplyAction="http://tempuri.org/ILoginService/GenerateCodeResponse")]
        System.Threading.Tasks.Task<string> GenerateCodeAsync(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/SendMessageByEmail", ReplyAction="http://tempuri.org/ILoginService/SendMessageByEmailResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Basta.Contracts.Faults.EmailSenderFault), Action="http://tempuri.org/ILoginService/SendMessageByEmailEmailSenderFaultFault", Name="EmailSenderFault", Namespace="http://schemas.datacontract.org/2004/07/Basta.Contracts.Faults")]
        bool SendMessageByEmail(string email, string content);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/SendMessageByEmail", ReplyAction="http://tempuri.org/ILoginService/SendMessageByEmailResponse")]
        System.Threading.Tasks.Task<bool> SendMessageByEmailAsync(string email, string content);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoginServiceChannel : Basta.Proxy.ILoginService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoginServiceClient : System.ServiceModel.ClientBase<Basta.Proxy.ILoginService>, Basta.Proxy.ILoginService {
        
        public LoginServiceClient() {
        }
        
        public LoginServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LoginServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoginServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoginServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Database.Entity.Player Login(string macAddress, string email, string password) {
            return base.Channel.Login(macAddress, email, password);
        }
        
        public System.Threading.Tasks.Task<Database.Entity.Player> LoginAsync(string macAddress, string email, string password) {
            return base.Channel.LoginAsync(macAddress, email, password);
        }
        
        public bool SignUp(Database.Entity.Player player) {
            return base.Channel.SignUp(player);
        }
        
        public System.Threading.Tasks.Task<bool> SignUpAsync(Database.Entity.Player player) {
            return base.Channel.SignUpAsync(player);
        }
        
        public string RecoverPassword(string email, string password) {
            return base.Channel.RecoverPassword(email, password);
        }
        
        public System.Threading.Tasks.Task<string> RecoverPasswordAsync(string email, string password) {
            return base.Channel.RecoverPasswordAsync(email, password);
        }
        
        public bool ChangePassword(string email, string password) {
            return base.Channel.ChangePassword(email, password);
        }
        
        public System.Threading.Tasks.Task<bool> ChangePasswordAsync(string email, string password) {
            return base.Channel.ChangePasswordAsync(email, password);
        }
        
        public string GenerateCode(string email) {
            return base.Channel.GenerateCode(email);
        }
        
        public System.Threading.Tasks.Task<string> GenerateCodeAsync(string email) {
            return base.Channel.GenerateCodeAsync(email);
        }
        
        public bool SendMessageByEmail(string email, string content) {
            return base.Channel.SendMessageByEmail(email, content);
        }
        
        public System.Threading.Tasks.Task<bool> SendMessageByEmailAsync(string email, string content) {
            return base.Channel.SendMessageByEmailAsync(email, content);
        }
    }
}