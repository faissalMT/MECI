namespace MonitorCS {
  using MonitorCS.Usecases;
  using MonitorCS.Gateways;
  class Depinj {
    public static Usecase getUsecase(string usecase) {
      switch (usecase) {
        case "get_project_users":
          return new GetProjectUsers(Depinj.getGateway("users"));
        default:
          return new RubyUsecase(usecase);
      }
    }

    public static Gateway getGateway(string gateway) {
      switch (gateway) {
        default:
          return new RubyGateway(gateway);
      }
    }
  }
}
