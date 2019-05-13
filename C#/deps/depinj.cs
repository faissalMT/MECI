namespace MonitorCS {
  using MonitorCS.Usecases;
  class Depinj {
    public static Usecase getUsecase(string usecase) {
      switch (usecase) {
        case "echo":
          return new Echo();

        default:
          return new RubyUsecase(usecase);
      }
    }
  }
}
