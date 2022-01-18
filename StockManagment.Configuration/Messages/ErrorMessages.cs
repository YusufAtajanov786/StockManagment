using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.Configuration.Messages
{
    public class ErrorMessages
    {
        public static class Generic
        {
            public static string TypeBadRequest = "Bad Request";

            public static string ObjectNotFound = "Object not found";

            public static string InvalidPayload = "Invalid Payload";

            public static string InvalidRequest = "Invalid Request";
            public static string SomethingWentWrong = "Sometinh went wrong";

            public static string UnableToProcess = "Bad Request";
        }

        public static class Profile
        {
            public static string UserNotFound = "User not found";
        }

        public static class User
        {
            public static string UserNotFound = "User not found";

            public static string UserInvalidPayload = "Invalid Payload";

            public static string UserFieldsNotFull  = "User fields not full ";
        }
    }
}
