using System;
using System.Collections.Generic;

namespace MonitorCS.Usecases {
  public class Echo: Usecase {
    public Dictionary<string,object> execute(Dictionary<string,object> arguments) {
      return arguments;
    }
  }
}
