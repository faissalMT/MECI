using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Dynamic;

namespace MonitorCS.Gateways {
  public abstract class Gateway: DynamicObject {
    public override bool TryInvokeMember(InvokeMemberBinder binder, dynamic[] arguments, out dynamic result) {
      result = null;
      result = execute(binder.Name, arguments);
      return true;
    }
    public virtual dynamic execute(string binder, dynamic[] arguments) {return null;}
  }

  public class GatewayRequest {
    public string gateway;
    public string method;
    public dynamic[] arguments;

    public GatewayRequest(string gateway, string method, dynamic[] arguments) {
      this.gateway = gateway;
      this.method = method;
      this.arguments = arguments;
    }
  }

  public class RubyGateway: Gateway {
    string gateway;

    public RubyGateway(string gateway) {
      this.gateway = gateway;
    }

    public virtual new dynamic execute(string method, dynamic[] arguments)
    {
      try {
        var webAddr="http://localhost:4567/gateway";

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = JsonConvert.SerializeObject(new GatewayRequest(gateway, method, arguments));

            streamWriter.Write(json);
            streamWriter.Flush();
        }
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var responseText = streamReader.ReadToEnd();
            try {
              return JsonConvert.DeserializeObject<Dictionary<string, object>>(responseText);
            } catch (JsonException) {
              return responseText;
            }
        }
      } catch(WebException ex) {
          Console.WriteLine(ex.Message);
          return null;
      }
    }
  }
}
