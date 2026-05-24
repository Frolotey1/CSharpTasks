public interface IApplicationTelemetry {
  void LogOperation(string category, string action, TimeSpan duration, string? metadata = null);
  IReadOnlyDictionary<string, int> GetOperationCounts();
  GlobalUiSettings GetCurrentSettings();
  void ResetForTesting(); 
}
