using System;
using System.IO;
using System.Linq;
using Azure;
using Azure.AI.OpenAI;
using JSON;
using static System.Environment;

namespace SquircleLabs {
    public static class OpenAI {
        public static string GenerateUsername(bool localhost, bool doPasswordSubs) {
            try {
                var folder = Directory.Exists("E:\\web\\timblais\\SquircleLabs") ? "E:\\web\\timblais\\SquircleLabs" : "E:\\SquircleLabs";
                string path = localhost ? GetFolderPath(SpecialFolder.ApplicationData) : folder;
                string filepath = Path.Combine(path, "usernames.json");
                var jarray = new JSONArray();
                if (File.Exists(filepath)) jarray = new JSONArray(File.ReadAllText(filepath));
                OpenAIClient client = new(new Uri(_endpoint), new AzureKeyCredential(_key));
                string prompt = "Generate a zany and whimsical username of 15 alphabetic characters or less for a child using words from Minecraft.";
                string str = "";
                do {
                    var completionsResponse = client.GetCompletions(_engine, prompt);
                    str = (completionsResponse.Value.Choices[0].Text?.Trim() ?? "").Trim('0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                    str = doPasswordSubs
                        ? str.Replace("e", "3").Replace("i", "1").Replace("b", "8").Replace("t", "!").Replace("o", "0").Replace("a", "@")
                        : str;
                } while (!doPasswordSubs && jarray.Contains(str));
                if (!doPasswordSubs) {
                    jarray.put(str);
                    File.WriteAllText(filepath, jarray.ToString(true, 0));
                }
                return str;
            }
            catch (Exception ex) {
                return "Exception: " + ex.Message + NewLine + "url: " + _endpoint + NewLine + "Key starting with: " + _key.Substring(0, 5);
            }
        }
        //public static string GeneratePassword() {
        //    try {
        //        OpenAIClient client = new(new Uri(_endpoint), new AzureKeyCredential(_key));
        //        string prompt = "Generate a zany and whimsical 8 character password for a child using words from the video game franchise Minecraft. Include special characters such as 3 for E, 1 for I, etc..";
        //        var completionsResponse = client.GetCompletions(_engine, prompt);
        //        return (completionsResponse.Value.Choices[0].Text?.Trim() ?? "").Trim('0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
        //    }
        //    catch (Exception ex) {
        //        return "Exception: " + ex.Message + NewLine + "url: " + _endpoint + NewLine + "Key starting with: " + _key.Substring(0, 5);
        //    }
        //}
        public static string MSFoundedAsync() {
            try {
                OpenAIClient client = new(new Uri(_endpoint), new AzureKeyCredential(_key));
                string prompt = "When was Microsoft founded?";
                var completionsResponse = client.GetCompletions(_engine, prompt);
                return completionsResponse.Value.Choices[0].Text;
            }
            catch (Exception ex) {
                return "Exception: " + ex.Message + NewLine + "url: " + _endpoint + NewLine + "Key starting with: " + _key.Substring(0, 5);
            }
        }
        private static string _endpoint = "https://squirclesmarts.openai.azure.com/"; //GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
        private static string _engine = "SquircleAIDeploymentTest";
        private static string _key = "5e4ef018e0224069bcabf7008f8808ab"; //GetEnvironmentVariable("AZURE_OPENAI_KEY");
    }
}