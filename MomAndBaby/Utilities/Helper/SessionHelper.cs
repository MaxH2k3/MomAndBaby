using AutoMapper.Execution;
using MomAndBaby.BusinessObject.Entity;
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
            return session.GetSession<User>(SessionConstant.UserSession) != null;
        }

        public static void SignIn(this ISession session, User user, bool isAdmin = false)
        {
            session.SetSession(SessionConstant.UserSession, user);
            session.SetSession(SessionConstant.IsAdmin, isAdmin);
        }

        public static void SignOut(this ISession session)
        {
            session.Clear();
        }

        public static T GetCurrentUser<T>(this ISession session)
        {
            return session.GetSession<T>(SessionConstant.UserSession)!;
        }

        public static bool IsAdmin(this ISession session)
        {
            return session.GetSession<bool>(SessionConstant.IsAdmin);
        }
    }


        

    