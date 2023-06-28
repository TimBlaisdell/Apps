using System;
using System.IO;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using JSON;

namespace SquircleLabs.Services {
    /// <summary>
    ///     Summary description for WebHook
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")] [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebHook : WebService {
        [WebMethod, ScriptMethod(UseHttpGet = false)]
        public string GenerateUsername() {
            return OpenAI.GenerateUsername(HttpContext.Current.Request.Url.ToString().Contains("localhost"), false);
        }
        [WebMethod, ScriptMethod(UseHttpGet = true)]
        public string GetEvents() {
            var folder = Directory.Exists("E:\\web\\timblais\\SquircleLabs") ? "E:\\web\\timblais\\SquircleLabs" : "E:\\SquircleLabs";
            bool localhost = HttpContext.Current.Request.Url.ToString().Contains("localhost");
            string path = localhost ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) : folder;
            string filepath = Path.Combine(path, "events.json");
            var jarray = new JSONArray();
            if (File.Exists(filepath)) jarray = new JSONArray(File.ReadAllText(filepath));
            return jarray.ToString(true, 0);
        }
        [WebMethod, ScriptMethod(UseHttpGet = false)]
        public string HelloWorld() {
            return "Hello World";
        }
        [WebMethod, ScriptMethod(UseHttpGet = true)]
        public string PurgeEvents() {
            var folder = Directory.Exists("E:\\web\\timblais\\SquircleLabs") ? "E:\\web\\timblais\\SquircleLabs" : "E:\\SquircleLabs";
            bool localhost = HttpContext.Current.Request.Url.ToString().Contains("localhost");
            string path = localhost ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) : folder;
            string filepath = Path.Combine(path, "events.json");
            if (File.Exists(filepath)) File.Delete(filepath);
            return "ok";
        }
        [WebMethod, ScriptMethod(UseHttpGet = false)]
        public string RecordEvent() {
            var folder = Directory.Exists("E:\\web\\timblais\\SquircleLabs") ? "E:\\web\\timblais\\SquircleLabs" : "E:\\SquircleLabs";
            bool localhost = HttpContext.Current.Request.Url.ToString().Contains("localhost");
            JSONObject json = null;
            string contentType = HttpContext.Current.Request.ContentType;
            if (!contentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase)) return "NOT JSON";
            using (Stream stream = HttpContext.Current.Request.InputStream)
            using (StreamReader reader = new StreamReader(stream)) {
                stream.Seek(0, SeekOrigin.Begin);
                string bodyText = reader.ReadToEnd();
                bodyText = bodyText == "" ? "{}" : bodyText;
                json = new JSONObject(bodyText);
                json.put("username", OpenAI.GenerateUsername(localhost, false));
                json.put("password", OpenAI.GenerateUsername(localhost, true));
            }
            string path = localhost ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) : folder;
            string filepath = Path.Combine(path, "events.json");
            var jarray = new JSONArray();
            if (File.Exists(filepath)) jarray = new JSONArray(File.ReadAllText(filepath));
            jarray.put(json);
            File.WriteAllText(filepath, jarray.ToString(true, 0));
            return "ok";
        }
        [WebMethod, ScriptMethod(UseHttpGet = false)]
        public string WhenWasMSFounded() {
            return OpenAI.MSFoundedAsync();
        }
    }
}