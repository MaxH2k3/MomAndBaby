using AutoMapper.Execution;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.Utilities.Constants;
using Newtonsoft.Json;

namespace MomAndBaby.Utilities.Helper;


    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
        
        public static bool IsAuthenticated(this ISession session)
        {
            return session.GetObjectFromJson<User>(SessionConstant.UserSession) != null;
        }

        public static void SignIn(this ISession session, User user, bool isAdmin = false)
        {
            session.SetObjectAsJson(SessionConstant.UserSession, user);
            session.SetObjectAsJson(SessionConstant.IsAdmin, isAdmin);
        }

        public static void SignOut(this ISession session)
        {
            session.Clear();
        }

        public static T GetCurrentUser<T>(this ISession session)
        {
            return session.GetObjectFromJson<T>(SessionConstant.UserSession)!;
        }

        public static bool IsAdmin(this ISession session)
        {
            return session.GetObjectFromJson<bool>(SessionConstant.IsAdmin);
        }
    }


        

    