using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace MonitorCS.Usecases {
  public interface Usecase {
    Dictionary<string,object> execute(Dictionary<string,object> arguments);
  }

  public class RubyUsecase: Usecase {
    string usecase;

    public RubyUsecase(string usecase) {
      this.usecase = usecase;
    }

    public Dictionary<string, object> execute(Dictionary<string, object> arguments) {
      try {
        var webAddr="http://localhost:4567/usecase";

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = JsonConvert.SerializeObject(new UsecaseRequest(usecase, arguments));

            streamWriter.Write(json);
            streamWriter.Flush();
        }
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var responseText = streamReader.ReadToEnd();
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(responseText);
        }
      } catch(WebException ex){
          Console.WriteLine(ex.Message);
      }
      return null;
    }
  }

  public class UsecaseRequest {
    public string usecase;
    public Dictionary<string, object> arguments;

    public UsecaseRequest(string usecase, Dictionary<string, object> arguments) {
      this.usecase = usecase;
      this.arguments = arguments;
    }
  }
}
