using Microsoft.AspNetCore.Http;
using MomAndBaby.BusinessObject.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.Helper
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetObjectAsJson(key, JsonConvert.SerializeObject(value));
        }

        public static CartItem GetObjectFromJson<T>(this ISession session, string key)
        {
            return session.GetObjectFromJson<Product>(key);
           
        }
    }
}
