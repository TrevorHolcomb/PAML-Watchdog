using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using Ninject;
using System.Linq;
using System;
using System.Collections.Generic;

namespace WatchdogDatabaseAccessLayer
{
    public class WatchdogValidator : IWatchdogValidator
    {

        

        [Inject]
        public  Repository<MessageTypeParameterType> messageTypeParameterTypeRepository { private get; set; }

        public bool validateServer(string serverToValidate)
        {
            if (serverToValidate == null)
                return false;

            //validate size

            //check server registry

            return true;
        }

        public bool validateEngine(string engineToValidate)
        {
            if (engineToValidate == null)
                return false;

            //validate size

            //check engine registry

            return true;
        }

        public bool validateOrigin(string originToValidate)
        {
            if (originToValidate == null)
                return false;

            //validate size

            //check origin registry

            return true;
        }

        public bool validateParameters(APIMessageParameter[] parameters, int messageTypeId)
        {
            if (parameters == null)
                return false;

            //check all parameters coming in - if not found in messageTypeParameterType - invalid
            IEnumerable<MessageTypeParameterType> parameterTypes = messageTypeParameterTypeRepository.Get().Where(messageTypeParameter => messageTypeParameter.MessageTypeId == messageTypeId);


            foreach(MessageTypeParameterType parameterType in parameterTypes)
            {
                APIMessageParameter toValidate = getByName(parameters, parameterType.Name);

                if (toValidate == null)
                {
                    if (parameterType.Required == true)
                        return false;
                }
                else
                {
                    if (!isValidParameterType(toValidate, parameterType))
                        return false;
                }

            }

            return true;
        }

        #region private methods

        private bool isValidParameterType(APIMessageParameter toValidate, MessageTypeParameterType validator)
        {
            try
            {
                switch (validator.Type)
                {
                    case "int":
                        int intTest = Int32.Parse(toValidate.value);
                        break;
                    case "double":
                        double doubleTest = Double.Parse(toValidate.value);
                        break;
                    case "bool":
                        bool boolTesst = Boolean.Parse(toValidate.value);
                        break;
                    case "string":
                        
                        break;
                    case "datetime":
                        DateTime dtTest = DateTime.Parse(toValidate.value);
                        break;
                    default:
                        return false;
              
                }
                return true;
            }
            catch(InvalidCastException e)
            {
                //log e
                return false;
            }
            catch(FormatException e)
            {
                //log e
                return false;
            }
            
        }


        private APIMessageParameter getByName(APIMessageParameter[] parameters, string name)
        {
            foreach(APIMessageParameter param in parameters)
            {
                if (param.name.Equals(name))
                    return param;
            }

            return null;
        }


        #endregion
    }
}
