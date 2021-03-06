﻿using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using CommonClasses;
using CommonClasses.Helpers;
using CommonClasses.MethodResults;

namespace ServiceProxy
{
    public partial class ServiceProxySingleton
    {
        #region Properties and Variables

        public string AuthToken
        {
            get
            {
                if (HttpContext.Current == null) return string.Empty;
                return (string) (HttpContext.Current.Session[Constants.SESSION_AUTH_INFO]);
            }
        }

        #endregion

        #region Singleton Methods

        private ServiceProxySingleton()
        {
            ServicePointManager.Expect100Continue = false;
        }

        private static ServiceProxySingleton _instance;

        public static ServiceProxySingleton Instance
        {
            get
            {
                return _instance ?? (_instance = new ServiceProxySingleton());
            }
        }

        #endregion

        private string GetFullReqUrl(string operation, string parameters)
        {
            var result = AppConfiguration.RestServiceUrl + operation;
            if (string.IsNullOrEmpty(parameters)) return result;
            if (!parameters.StartsWith("?")) result = result + "/";
            return result + parameters;
        }

        #region Service Requests Methods

        public T SendGetRequest<T>(string operation, string parameters = null)
        {
            string fullReq = GetFullReqUrl(operation, parameters);
            var request = WebRequest.Create(fullReq) as HttpWebRequest;
            if (request == null) return default(T);

            request.Method = "GET";
            return SendRequest<T>(request);
        }

        public TReturn SendPostRequest<TReturn, TParam>(string operation, TParam param)
        {
            string fullReq = GetFullReqUrl(operation, null);
            var request = WebRequest.Create(fullReq) as HttpWebRequest;
            if (request == null) return default(TReturn);

            request.Method = "POST";
            request.ContentType = "application/json";
            string json = JsonHelper.JsonSerializer(param);
            byte[] data = Encoding.UTF8.GetBytes(json);
            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
            return SendRequest<TReturn>(request);
        }

        public T SendRequest<T>(HttpWebRequest request)
        {
            request.KeepAlive = false;
            request.Headers.Add("authToken", AuthToken);
            var response = request.GetResponse() as HttpWebResponse;
            Encoding encoding = Encoding.UTF8;
            var stream = new StreamReader(response.GetResponseStream(), encoding);
            string result = stream.ReadToEnd();
            stream.Close();
            response.Close();

            return JsonHelper.JsonDeserialize<T>(result);
        }

        public BaseResult SendDeleteRequest(string operation, int id)
        {
            try
            {
                return SendDeleteRequest<BaseResult>(operation, id);
            }
            catch (Exception ex)
            {
                return new BaseResult {ErrorMessage = ex.Message};
            }
        }

        public T SendDeleteRequest<T>(string operation, int id)
        {
            string fullReq = GetFullReqUrl(operation, null) + "?id=" + id.ToString(CultureInfo.InvariantCulture);
            var request = WebRequest.Create(fullReq) as HttpWebRequest;
            if (request == null) throw new Exception("AasService request is not valid");

            request.Method = "DELETE";
            return SendRequest<T>(request);
        }

        #endregion
    }
}
