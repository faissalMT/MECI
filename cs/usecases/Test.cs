using System;
using System.Collections.Generic;
using MonitorCS.Gateways;

namespace MonitorCS.Usecases {
  public class GetProjectUsers: Usecase {
    dynamic users_gateway;

    public GetProjectUsers(Gateway users_gateway) {
      this.users_gateway = users_gateway;
    }

    public Dictionary<string,object> execute(Dictionary<string,object> arguments) {
      var results = users_gateway.get_users(new Dictionary<string, object> { {"project_id", arguments["project_id"]} });
      var emails = new List<string>();
      foreach (var user in results) {
        emails.Add(user["email"]);
      }
      return new Dictionary<string, object> {
        { "users" , emails }
      };
    }
  }
}
