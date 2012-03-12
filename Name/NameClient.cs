using System;
using System.Collections.Generic;
using ServiceStack.ServiceClient.Web;

namespace Name
{
    public class NameClient
    {
        private JsonServiceClient _client;

        public NameClient()
        {
            _client = new JsonServiceClient("https://api.dev.name.com/api");

            //_client.LocalHttpWebRequestFilter = req => req.Headers.Add("Api-Username", "hospedix");
            //_client.LocalHttpWebRequestFilter = req => req.Headers.Add("Api-Token", "token_temp");

            //var responseLogin = _client.Post<ResponseLogin>("/login", new LoginRequest { username = "hospedix", api_token = "token_temp" });

            var responseHello = _client.Get<HelloResponse>(Method.Hello);
        }

        public HelloResponse Hello()
        {
            return _client.Get<HelloResponse>(Method.Hello);
        }

        public LoginResponse Login(LoginRequest request)
        {
            return _client.Post<LoginResponse>(Method.Login, request);
        }

        public LogoutResponse Logout()
        {
            return _client.Get<LogoutResponse>(Method.Logout);
        }

        public AccountResponse GetAccount()
        {
            return _client.Get<AccountResponse>(Method.Account.Get);
        }

        public OrderResponse Order(OrderRequest request)
        {
            return _client.Post<OrderResponse>(Method.Order, request);
        }




        public static class Method
        {
            public const string Hello = "/hello";

            public const string Login = "/login";
            public const string Logout = "/logout";

            public const string Order = "/order";

            public struct Account
            {
                public const string Get = "/account/get";
            }

            public struct Domain
            {
                public const string Create = "/domain/create";
                public const string Retrieve = "/domain/get/{0}";
                public const string List = "/domain/list";
                public const string Lock = "/domain/lock/{0}";
                public const string Unlock = "/domain/unlock/{0}";
                public const string UpdateNameservers = "/domain/update_nameservers/{0}";
                public const string UpdateContacts = "/domain/update_contacts/{0}";
                public const string CheckAvailability = "/domain/check";
            }

            public struct Dns
            {
                public const string List = "/dns/list/{0}";
                public const string Create = "/dns/create/{0}";
                public const string Delete = "/dns/delete/{0}";
            }

            public struct Host
            {
                public const string List = "/host/list/{0}";
                public const string Sync = "/host/sync/{0}";
                public const string Create = "/host/create/{0}";
                public const string Delete = "/host/delete/{0}";
                public const string AddIp = "/host/add_ip/{0}";
                public const string RemoveIp = "/host/remove_ip/{0}";
            }
        }

        public class OrderResponse : BaseResponse
        {
            public List<OrderResult> results { get; set; }
        }

        public class OrderResult
        {
            public bool success { get; set; }

            public string order_type { get; set; }

            public string domain_name { get; set; }

            public decimal price { get; set; }

            public int duration { get; set; }

            public string interval { get; set; }

            public List<string> information { get; set; }
        }

        public class OrderRequest
        {
            public List<OrderItem> items { get; set; }
        }

        public class OrderItem
        {
            public string order_type { get; set; }

            public string domain_name { get; set; }

            public List<string> nameservers { get; set; }

            public List<Contact> contacts { get; set; }

            public int period { get; set; }
        }

        public static class OrderType
        {
            public const string DomainCreate = "domain/create";

            public const string DomainRenew = "domain/renew";

            public const string WhoisPrivacyCreate = "whois_privacy/create";

            public const string WhoisPrivacyRenew = "whois_privacy/renew";

            public const string BackorderCreate = "backorder/create";
        }

        public class LogoutResponse : BaseResponse
        { 
        }

        public class AccountResponse : BaseResponse
        {
            public string username { get; set; }

            public DateTime create_date { get; set; }

            public int domain_count { get; set; }

            public decimal account_credit { get; set; }

            public List<Contact> contacts { get; set; }
        }

        public class Contact
        {
            public List<string> type { get; set; }

            public string first_name { get; set; }

            public string last_name { get; set; }

            public string organization { get; set; }

            public string address_1 { get; set; }

            public string address_2 { get; set; }

            public string city { get; set; }

            public string state { get; set; }

            public string zip { get; set; }

            public string country { get; set; }

            public string phone { get; set; }

            public string fax { get; set; }

            public string email { get; set; }
        }

        public static class ContactType
        {
            public const string Registrant = "registrant";

            public const string Administrative = "administrative";

            public const string Billing = "billing";

            public const string Technical = "technical";
        }

        public class LoginRequest
        {
            public string username { get; set; }

            public string api_token { get; set; }
        }

        public class BaseResponse
        {
            public Result result { get; set; }
        }

        public class Result
        {
            public ResponseCode code { get; set; }

            public string message { get; set; }
        }

        public class LoginResponse : BaseResponse
        {
            public string session_token { get; set; }
        }

        public class HelloResponse : BaseResponse
        {
            public string service { get; set; }

            public string server_date { get; set; }

            public string version { get; set; }

            public string language { get; set; }

            public string client_ip { get; set; }
        }

        public enum ResponseCode
        {
            CommandSuccesfull = 100,
            RequeridParameterMissing = 203,
            ParamaterValueError = 204,
            InvalidCommandUrl = 211,
            AuthorizationError = 221,
            CommandFailed = 240,
            UnexpectedError = 250,
            AuthenticationError = 251,
            InsufficientFunds = 260,
            UnableToAuthorizeFunds = 261
        }
    }
}
