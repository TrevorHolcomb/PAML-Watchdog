using WatchdogDatabaseAccessLayer.Models;
using Ninject.Syntax;
using WatchdogDaemon.Exceptions;
using System;
using System.Linq;
using System.Collections.Generic;
using WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers;

namespace WatchdogDaemon.Watchdogs
{
    public class WatchdogValidator : AbstractValidator
    {
        private BooleanTypeHandler booleanHandler;
        private DateTimeTypeHandler dateTimeHandler;
        private DecimalTypeHandler decimalHandler;
        private IntegerTypeHandler integerHandler;
        private EnumerationTypeHandler enumHandler;
        private ExceptionTypeHandler exceptionHandler;

        public WatchdogValidator(IResolutionRoot kernel) : base(kernel)
        {
            booleanHandler = new BooleanTypeHandler();
            dateTimeHandler = new DateTimeTypeHandler();
            decimalHandler = new DecimalTypeHandler();
            integerHandler = new IntegerTypeHandler();
            enumHandler = new EnumerationTypeHandler();
            exceptionHandler = new ExceptionTypeHandler();
        }

        public override bool Validate(UnvalidatedMessage toValidate)
        {
            try
            {
                Message messageToAdd = new Message();

                validateMessageType(toValidate.MessageTypeName, messageToAdd);
                validateServer(toValidate.Server, messageToAdd);
                validateEngine(toValidate.EngineName, messageToAdd);
                validateOrigin(toValidate.Origin, messageToAdd);
                validateParameters(toValidate, messageToAdd);

                InsertMessage(messageToAdd);
                CleanUp(toValidate);

                return true;
            }
            catch (WatchdogInvalidServerExcpetion)
            {
                //logic for invalid server
                Console.WriteLine("Invalid server");
            }
            catch (WatchdogInvalidEngineExcpetion)
            {
                //logic for invalid engine
                Console.WriteLine("Invalid engine");
            }
            catch (WatchdogInvalidOriginExcpetion)
            {
                //logic for invalid Origin
                Console.WriteLine("Invalid origin");
            }
            catch (WatchdogInvalidParameterExcpetion)
            {
                //logic for invalid parameter
                Console.WriteLine("Invalid parameter");
            }
            catch(Exception e)
            {
                //fail safe
                Console.WriteLine("Unknown error - " + e.ToString());
            }
            finally
            {
                CleanUp(toValidate);
            }

            return false;

        }


        ///<exception cref = "WatchdogDaemon.Exceptions.WatchdogInvalidServerExcpetion" ></exception>
        public bool validateServer(string serverToValidate, Message messageToAdd)
        {
            if (serverToValidate == null)
                throw new WatchdogInvalidServerExcpetion("Server is null.");

            //connect server to message
            messageToAdd.Server = serverToValidate;


            //Future Expansion


            return true;
        }

        ///<exception cref = "WatchdogDaemon.Exceptions.WatchdogInvalidEngineExcpetion" ></exception>
        public bool validateEngine(string engineToValidate, Message messageToAdd)
        {
            if (engineToValidate == null)
               throw new WatchdogInvalidEngineExcpetion("Engine is null.");

            //check engine registry
            Engine engineToAdd = EngineRepository.GetByName(engineToValidate);
            if (engineToAdd == null)
                throw new WatchdogInvalidEngineExcpetion("Engine could not be found in registry.");

            messageToAdd.Engine = engineToAdd;

            return true;
        }

        ///<exception cref = "WatchdogDaemon.Exceptions.WatchdogInvalidOriginExcpetion" ></exception>
        public bool validateOrigin(string originToValidate, Message messageToAdd)
        {
            if (originToValidate == null)
                throw new WatchdogInvalidOriginExcpetion("Origin is null.");

            messageToAdd.Origin = originToValidate;
            
            //future expansion


            return true;
        }


        ///<exception cref = "WatchdogDaemon.Exceptions.WatchdogInvalidMessageTypeExcpetion" ></exception>
        public bool validateMessageType(string messageTypeToValidate, Message messageToAdd)
        {
            if (messageTypeToValidate == null)
                throw new WatchdogInvalidEngineExcpetion("MessageType is null.");

            //check engine registry
            MessageType messageTypeToValidaate = MessageTypeRepository.GetByName(messageTypeToValidate);
            if (messageTypeToValidate == null)
                throw new WatchdogInvalidMessageTypeExcpetion("MessageType could not be found in registry.");

            messageToAdd.MessageType = messageTypeToValidaate;

            return true;
        }

        ///<exception cref = "WatchdogDaemon.Exceptions.WatchdogInvalidParameterExcpetion" ></exception>
        ///<exception cref = "WatchdogDaemon.Exceptions.WatchdogInvalidMessageTypeExcpetion" ></exception>
        public bool validateParameters(UnvalidatedMessage toValidate, Message messageToAdd)
        {
            if (toValidate == null)
                throw new WatchdogInvalidParameterExcpetion("UnvalidatedMessage is null");


            //get message type
            MessageType messageTypeToValidateAgainst = MessageTypeRepository.GetByName(toValidate.MessageTypeName);

            if(messageTypeToValidateAgainst == null)
                throw new WatchdogInvalidMessageTypeExcpetion("MessageType provided was not found.");

            //get valid parameters
            ICollection<MessageTypeParameterType> parametersToValidateAgainst =  messageTypeToValidateAgainst.MessageTypeParameterTypes;            
            
            //iterate through valid parameters
            foreach (MessageTypeParameterType parameterToValidateAgainst in parametersToValidateAgainst)
            {
                bool isInserted = false;
                //check unvalidated parameters
                foreach (UnvalidatedMessageParameter parameterToValidate in toValidate.MessageParameters)
                {
                    //validate parameter
                    bool isValid = IsValidParameterType(parameterToValidate, parameterToValidateAgainst);
                    //if valid attatch to message with fk contraints
                    if (isValid)
                    {
                        AddParameter(parameterToValidate, parameterToValidateAgainst, messageToAdd);
                        isInserted = true;
                        break;                       
                    }
                }
                //if exhausted parameters and required throw error
                if (isInserted == false && parameterToValidateAgainst.Required == true)
                    throw new WatchdogInvalidParameterExcpetion("No record found for required parameter: " + parameterToValidateAgainst.Name + ".");

            }
            return true;
        }


        #region private methods

        //change to type handler no switch 
        private bool IsValidParameterType(UnvalidatedMessageParameter toValidate, MessageTypeParameterType validator)
        {
            if (toValidate.Name.Equals(validator.Name))
            {
                switch (validator.Type)
                {
                    case "Integer":
                        return integerHandler.IsValid(toValidate.Value);
                    case "String":
                        return true;
                    case "Decimal":
                        return decimalHandler.IsValid(toValidate.Value);
                    case "Boolean":
                        return booleanHandler.IsValid(toValidate.Value);
                    case "DateTime":
                        return dateTimeHandler.IsValid(toValidate.Value);
                    case "Enumeration":
                        return enumHandler.IsValid(toValidate.Value);
                    case "Exception":
                        return exceptionHandler.IsValid(toValidate.Value);
                    default:
                        return false;
                }
            }           

            return false;
        }


        private void InsertMessage(Message messageToAdd)
        {
            MessageRepository.Insert(messageToAdd);
            MessageTypeRepository.Save();
        }

        private void CleanUp(UnvalidatedMessage toValidate)
        {
            UnvalidatedMessageParameterRepository.DeleteRange(toValidate.MessageParameters.ToList());
            UnvalidatedMessageRepository.Delete(toValidate);
            MessageRepository.Save();
        }

        private void AddParameter(UnvalidatedMessageParameter parameterToAdd, MessageTypeParameterType parameterTypeToAdd, Message messageToAdd)
        {
            MessageParameter messageParameterToAdd = new MessageParameter
            {
                Value = parameterToAdd.Value
            };

            messageParameterToAdd.MessageTypeParameterType = parameterTypeToAdd;

            messageToAdd.MessageParameters.Add(messageParameterToAdd);
        }

        #endregion
    }
}
