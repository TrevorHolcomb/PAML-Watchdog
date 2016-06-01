using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer
{
    public interface IWatchdogValidator
    {

        bool validateServer(string serverToValidate);

        bool validateEngine(string engineToValidate);

        bool validateOrigin(string originToValidate);

        bool validateParameters(APIMessageParameter[] parameters, int messageTypeId);

    }
}