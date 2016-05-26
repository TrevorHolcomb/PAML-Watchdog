using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using Ninject;

namespace WatchdogDatabaseAccessLayer
{
    //TODO: finish WatchdogValidator
    public static class WatchdogValidator
    {

        [Inject]
        public static Repository<MessageTypeParameterType> messageTypeParameterTypeRepository { private get; set; }

        public static bool validateServer(string serverToValidate)
        {
            if (serverToValidate == null)
                return false;

            //validate size

            //check server registry

            return true;
        }

        public static bool validateEngine(string engineToValidate)
        {
            if (engineToValidate == null)
                return false;

            //validate size

            //check engine registry

            return true;
        }

        public static bool validateOrigin(string originToValidate)
        {
            if (originToValidate == null)
                return false;

            //validate size

            //check origin registry

            return true;
        }

        public static bool validateParameters(APIMessageParameter[] parameters, string messageTypeName)
        {
            if (parameters == null)
                return false;

            //check all parameters coming in - if not found in messageTypeParameterType - invalid
            //var parameterTypes = messageTypeParameterTypeRepository.Get().Where(messageTypeParameter => messageTypeParameter.MessageTypeId == incomingMessage.MessageTypeId).AsQueryable();

            return true;
        }
    }
}
