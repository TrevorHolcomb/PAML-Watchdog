using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer
{
    public static class WatchdogValidator
    {

        public static bool validateServer(string serverToValidate)
        {
            return true;
        }

        public static bool validateEngine(string engineToValidate)
        {
            return true;
        }

        public static bool validateOrigin(string originToValidate)
        {
            return true;
        }

        public static bool validateParameters(APIMessageParameter[] parameters, int messageTypeId)
        {
            return true;
        }
    }
}
