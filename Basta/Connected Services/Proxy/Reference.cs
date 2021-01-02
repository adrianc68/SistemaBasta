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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Proxy.ILoginService")]
    public interface ILoginService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/Login", ReplyAction="http://tempuri.org/ILoginService/LoginResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Basta.Contracts.Faults.AccessAccountNotFoundFault), Action="http://tempuri.org/ILoginService/LoginAccessAccountNotFoundFaultFault", Name="AccessAccountNotFoundFault", Namespace="http://schemas.datacontract.org/2004/07/Basta.Contracts.Faults")]
        [System.ServiceModel.FaultContractAttribute(typeof(Basta.Contracts.Faults.BannedAccountFault), Action="http://tempuri.org/ILoginService/LoginBannedAccountFaultFault", Name="BannedAccountFault", Namespace="http://schemas.datacontract.org/2004/07/Basta.Contracts.Faults")]
        [System.ServiceModel.FaultContractAttribute(typeof(Basta.Contracts.Faults.LimitReachedFault), Action="http://tempuri.org/ILoginService/LoginLimitReachedFaultFault", Name="LimitReachedFault", Namespace="http://schemas.datacontract.org/2004/07/Basta.Contracts.Faults")]
        [System.ServiceModel.FaultContractAttribute(typeof(Basta.Contracts.Faults.AccountAlreadyLoggedFault), Action="http://tempuri.org/ILoginService/LoginAccountAlreadyLoggedFaultFault", Name="AccountAlreadyLoggedFault", Namespace="http://schemas.datacontract.org/2004/07/Basta.Contracts.Faults")]
        Domain.Domain.Player Login(string macAddress, string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/Login", ReplyAction="http://tempuri.org/ILoginService/LoginResponse")]
        System.Threading.Tasks.Task<Domain.Domain.Player> LoginAsync(string macAddress, string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/SignUp", ReplyAction="http://tempuri.org/ILoginService/SignUpResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Basta.Contracts.Faults.EmailAlreadyRegisteredFault), Action="http://tempuri.org/ILoginService/SignUpEmailAlreadyRegisteredFaultFault", Name="EmailAlreadyRegisteredFault", Namespace="http://schemas.datacontract.org/2004/07/Basta.Contracts.Faults")]
        [System.ServiceModel.FaultContractAttribute(typeof(Basta.Contracts.Faults.UsernameRegisteredAlreadyFault), Action="http://tempuri.org/ILoginService/SignUpUsernameRegisteredAlreadyFaultFault", Name="UsernameRegisteredAlreadyFault", Namespace="http://schemas.datacontract.org/2004/07/Basta.Contracts.Faults")]
        bool SignUp(Domain.Domain.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/SignUp", ReplyAction="http://tempuri.org/ILoginService/SignUpResponse")]
        System.Threading.Tasks.Task<bool> SignUpAsync(Domain.Domain.Player player);
        
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/LogOut", ReplyAction="http://tempuri.org/ILoginService/LogOutResponse")]
        void LogOut(Domain.Domain.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/LogOut", ReplyAction="http://tempuri.org/ILoginService/LogOutResponse")]
        System.Threading.Tasks.Task LogOutAsync(Domain.Domain.Player player);
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
        
        public Domain.Domain.Player Login(string macAddress, string email, string password) {
            return base.Channel.Login(macAddress, email, password);
        }
        
        public System.Threading.Tasks.Task<Domain.Domain.Player> LoginAsync(string macAddress, string email, string password) {
            return base.Channel.LoginAsync(macAddress, email, password);
        }
        
        public bool SignUp(Domain.Domain.Player player) {
            return base.Channel.SignUp(player);
        }
        
        public System.Threading.Tasks.Task<bool> SignUpAsync(Domain.Domain.Player player) {
            return base.Channel.SignUpAsync(player);
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
        
        public void LogOut(Domain.Domain.Player player) {
            base.Channel.LogOut(player);
        }
        
        public System.Threading.Tasks.Task LogOutAsync(Domain.Domain.Player player) {
            return base.Channel.LogOutAsync(player);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Proxy.IRoomService", CallbackContract=typeof(Basta.Proxy.IRoomServiceCallback))]
    public interface IRoomService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoomService/CreateRoom", ReplyAction="http://tempuri.org/IRoomService/CreateRoomResponse")]
        string CreateRoom(Domain.Domain.Player player, Domain.Domain.Room room);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoomService/CreateRoom", ReplyAction="http://tempuri.org/IRoomService/CreateRoomResponse")]
        System.Threading.Tasks.Task<string> CreateRoomAsync(Domain.Domain.Player player, Domain.Domain.Room room);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/JoinRoom")]
        void JoinRoom(Domain.Domain.Player player, Domain.Domain.Room room);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/JoinRoom")]
        System.Threading.Tasks.Task JoinRoomAsync(Domain.Domain.Player player, Domain.Domain.Room room);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/DeleteRoom")]
        void DeleteRoom(Domain.Domain.Room room);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/DeleteRoom")]
        System.Threading.Tasks.Task DeleteRoomAsync(Domain.Domain.Room room);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoomService/SetUpRoom", ReplyAction="http://tempuri.org/IRoomService/SetUpRoomResponse")]
        void SetUpRoom();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoomService/SetUpRoom", ReplyAction="http://tempuri.org/IRoomService/SetUpRoomResponse")]
        System.Threading.Tasks.Task SetUpRoomAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/SendMessageRoomChat")]
        void SendMessageRoomChat(Domain.Domain.Player player, Domain.Domain.Room room, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/SendMessageRoomChat")]
        System.Threading.Tasks.Task SendMessageRoomChatAsync(Domain.Domain.Player player, Domain.Domain.Room room, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/SendMessageRoomChatToPlayer")]
        void SendMessageRoomChatToPlayer(Domain.Domain.Player player, Domain.Domain.Room room, string message, Domain.Domain.Player toPlayer);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/SendMessageRoomChatToPlayer")]
        System.Threading.Tasks.Task SendMessageRoomChatToPlayerAsync(Domain.Domain.Player player, Domain.Domain.Room room, string message, Domain.Domain.Player toPlayer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoomService/GetConnectedUsersFromRoom", ReplyAction="http://tempuri.org/IRoomService/GetConnectedUsersFromRoomResponse")]
        Domain.Domain.Player[] GetConnectedUsersFromRoom(Domain.Domain.Room room);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoomService/GetConnectedUsersFromRoom", ReplyAction="http://tempuri.org/IRoomService/GetConnectedUsersFromRoomResponse")]
        System.Threading.Tasks.Task<Domain.Domain.Player[]> GetConnectedUsersFromRoomAsync(Domain.Domain.Room room);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoomService/GetRooms", ReplyAction="http://tempuri.org/IRoomService/GetRoomsResponse")]
        Domain.Domain.Room[] GetRooms();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoomService/GetRooms", ReplyAction="http://tempuri.org/IRoomService/GetRoomsResponse")]
        System.Threading.Tasks.Task<Domain.Domain.Room[]> GetRoomsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoomService/GetRoomByCode", ReplyAction="http://tempuri.org/IRoomService/GetRoomByCodeResponse")]
        Domain.Domain.Room GetRoomByCode(string code);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoomService/GetRoomByCode", ReplyAction="http://tempuri.org/IRoomService/GetRoomByCodeResponse")]
        System.Threading.Tasks.Task<Domain.Domain.Room> GetRoomByCodeAsync(string code);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/UserDisconnectedFromRoom")]
        void UserDisconnectedFromRoom(Domain.Domain.Player player, Domain.Domain.Room room);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/UserDisconnectedFromRoom")]
        System.Threading.Tasks.Task UserDisconnectedFromRoomAsync(Domain.Domain.Player player, Domain.Domain.Room room);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/KickPlayer")]
        void KickPlayer(Domain.Domain.Player player, Domain.Domain.Room room);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/KickPlayer")]
        System.Threading.Tasks.Task KickPlayerAsync(Domain.Domain.Player player, Domain.Domain.Room room);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRoomServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/ReciveMessageFromPlayer")]
        void ReciveMessageFromPlayer(Domain.Domain.Player player, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/ReciveMessageRoom")]
        void ReciveMessageRoom(Domain.Domain.Player player, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/PlayerConnected")]
        void PlayerConnected(Domain.Domain.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/PlayerDisconnected")]
        void PlayerDisconnected(Domain.Domain.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/RoomDelected")]
        void RoomDelected(Domain.Domain.Room room);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/PlayerKicked")]
        void PlayerKicked();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/GameIsFull")]
        void GameIsFull();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/Join")]
        void Join();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRoomService/YouHaveDisconnected")]
        void YouHaveDisconnected();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRoomServiceChannel : Basta.Proxy.IRoomService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RoomServiceClient : System.ServiceModel.DuplexClientBase<Basta.Proxy.IRoomService>, Basta.Proxy.IRoomService {
        
        public RoomServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public RoomServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public RoomServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public RoomServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public RoomServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public string CreateRoom(Domain.Domain.Player player, Domain.Domain.Room room) {
            return base.Channel.CreateRoom(player, room);
        }
        
        public System.Threading.Tasks.Task<string> CreateRoomAsync(Domain.Domain.Player player, Domain.Domain.Room room) {
            return base.Channel.CreateRoomAsync(player, room);
        }
        
        public void JoinRoom(Domain.Domain.Player player, Domain.Domain.Room room) {
            base.Channel.JoinRoom(player, room);
        }
        
        public System.Threading.Tasks.Task JoinRoomAsync(Domain.Domain.Player player, Domain.Domain.Room room) {
            return base.Channel.JoinRoomAsync(player, room);
        }
        
        public void DeleteRoom(Domain.Domain.Room room) {
            base.Channel.DeleteRoom(room);
        }
        
        public System.Threading.Tasks.Task DeleteRoomAsync(Domain.Domain.Room room) {
            return base.Channel.DeleteRoomAsync(room);
        }
        
        public void SetUpRoom() {
            base.Channel.SetUpRoom();
        }
        
        public System.Threading.Tasks.Task SetUpRoomAsync() {
            return base.Channel.SetUpRoomAsync();
        }
        
        public void SendMessageRoomChat(Domain.Domain.Player player, Domain.Domain.Room room, string message) {
            base.Channel.SendMessageRoomChat(player, room, message);
        }
        
        public System.Threading.Tasks.Task SendMessageRoomChatAsync(Domain.Domain.Player player, Domain.Domain.Room room, string message) {
            return base.Channel.SendMessageRoomChatAsync(player, room, message);
        }
        
        public void SendMessageRoomChatToPlayer(Domain.Domain.Player player, Domain.Domain.Room room, string message, Domain.Domain.Player toPlayer) {
            base.Channel.SendMessageRoomChatToPlayer(player, room, message, toPlayer);
        }
        
        public System.Threading.Tasks.Task SendMessageRoomChatToPlayerAsync(Domain.Domain.Player player, Domain.Domain.Room room, string message, Domain.Domain.Player toPlayer) {
            return base.Channel.SendMessageRoomChatToPlayerAsync(player, room, message, toPlayer);
        }
        
        public Domain.Domain.Player[] GetConnectedUsersFromRoom(Domain.Domain.Room room) {
            return base.Channel.GetConnectedUsersFromRoom(room);
        }
        
        public System.Threading.Tasks.Task<Domain.Domain.Player[]> GetConnectedUsersFromRoomAsync(Domain.Domain.Room room) {
            return base.Channel.GetConnectedUsersFromRoomAsync(room);
        }
        
        public Domain.Domain.Room[] GetRooms() {
            return base.Channel.GetRooms();
        }
        
        public System.Threading.Tasks.Task<Domain.Domain.Room[]> GetRoomsAsync() {
            return base.Channel.GetRoomsAsync();
        }
        
        public Domain.Domain.Room GetRoomByCode(string code) {
            return base.Channel.GetRoomByCode(code);
        }
        
        public System.Threading.Tasks.Task<Domain.Domain.Room> GetRoomByCodeAsync(string code) {
            return base.Channel.GetRoomByCodeAsync(code);
        }
        
        public void UserDisconnectedFromRoom(Domain.Domain.Player player, Domain.Domain.Room room) {
            base.Channel.UserDisconnectedFromRoom(player, room);
        }
        
        public System.Threading.Tasks.Task UserDisconnectedFromRoomAsync(Domain.Domain.Player player, Domain.Domain.Room room) {
            return base.Channel.UserDisconnectedFromRoomAsync(player, room);
        }
        
        public void KickPlayer(Domain.Domain.Player player, Domain.Domain.Room room) {
            base.Channel.KickPlayer(player, room);
        }
        
        public System.Threading.Tasks.Task KickPlayerAsync(Domain.Domain.Player player, Domain.Domain.Room room) {
            return base.Channel.KickPlayerAsync(player, room);
        }
    }
}
