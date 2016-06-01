using WatchdogDatabaseAccessLayer.Models;
using System;
using System.Linq;
using Ninject.Syntax;

namespace WatchdogDaemon.Watchdogs
{
    public class WatchdogValidator : AbstractValidator
    {
        public WatchdogValidator(IResolutionRoot kernel) : base(kernel)
        {
        }

        public override bool Validate(Message toValidate)
        {

            
            return true;
        }

        public bool validateServer(string serverToValidate)
        {
            if (serverToValidate == null)
                return false;

            //validate size

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


            return true;
        }

        public bool validateParameters(Message toValidate)
        {
            if (toValidate == null)
                return false;

           /* IQueryable<MessageParameter> messageParameters = toValidate.MessageParameters.AsQueryable();

            foreach (MessageTypeParameterType parameterType in MessageTypeRepository.GetByName(toValidate.MessageTypeName).MessageTypeParameterTypes)
            {

                MessageParameter parameterToValidate = messageParameters.First(parameter => parameter.MessageTypeParameterTypeId == parameterType.Id);
                

                if (parameterToValidate == null)
                {
                    if (parameterType.Required == true)
                        return false;
                }
                else
                {
                    if (!isValidParameterType(parameterToValidate, parameterType))
                        return false;
                }

            }*/

            return true;
        }

        #region private methods

        //change to type handler no switch 
        private bool isValidParameterType(MessageParameter toValidate, MessageTypeParameterType validator)
        {
            try
            {
                switch (validator.Type)
                {
                    case "Integer":
                        long intTest = long.Parse(toValidate.Value);
                        break;
                    case "Decimal":
                        
                        break;
                    case "Boolean":
                        bool boolTesst = Boolean.Parse(toValidate.Value);
                        break;
                    case "String":

                        break;
                    case "Datetime":
                        DateTime dtTest = DateTime.Parse(toValidate.Value);
                        break;
                    case "Enumeration":

                        break;
                    default:
                        return false;

                }
                return true;
            }
            catch (OverflowException e)
            {
                //log eh
                Console.WriteLine("To be logged - "  + e.Message);
                return false;
            }
            catch (FormatException e)
            {
                //log e
                Console.WriteLine("To be logged - " + e.Message);
                return false;
            }

        }




        #endregion
    }
}
