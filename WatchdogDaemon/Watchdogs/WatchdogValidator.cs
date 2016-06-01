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
                var messageToAdd = new Message();

                ValidateMessageType(toValidate.MessageTypeName, messageToAdd);
                ValidateServer(toValidate.Server, messageToAdd);
                ValidateEngine(toValidate.EngineName, messageToAdd);
                ValidateOrigin(toValidate.Origin, messageToAdd);
                ValidateParameters(toValidate, messageToAdd);

                InsertMessage(messageToAdd);
                CleanUp(toValidate);

                return true;
            }
            catch (WatchdogInvalidServerException)
            {
                //logic for invalid server
                Console.WriteLine("Invalid server");
            }
            catch (WatchdogInvalidEngineException)
            {
                //logic for invalid engine
                Console.WriteLine("Invalid engine");
            }
            catch (WatchdogInvalidOriginException)
            {
                //logic for invalid Origin
                Console.WriteLine("Invalid origin");
            }
            catch (WatchdogInvalidParameterException)
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


        ///<exception cref = "WatchdogInvalidServerException" ></exception>
        public bool ValidateServer(string serverToValidate, Message messageToAdd)
        {
            if (serverToValidate == null)
                throw new WatchdogInvalidServerException("Server is null.");

            //connect server to message
            messageToAdd.Server = serverToValidate;


            //Future Expansion


            return true;
        }

        ///<exception cref = "WatchdogInvalidEngineException" ></exception>
        public bool ValidateEngine(string engineToValidate, Message messageToAdd)
        {
            if (engineToValidate == null)
               throw new WatchdogInvalidEngineException("Engine is null.");

            //check engine registry
            Engine engineToAdd = EngineRepository.GetByName(engineToValidate);
            if (engineToAdd == null)
                throw new WatchdogInvalidEngineException("Engine could not be found in registry.");

            messageToAdd.Engine = engineToAdd;

            return true;
        }

        ///<exception cref = "WatchdogInvalidOriginException" ></exception>
        public bool ValidateOrigin(string originToValidate, Message messageToAdd)
        {
            if (originToValidate == null)
                throw new WatchdogInvalidOriginException("Origin is null.");

            messageToAdd.Origin = originToValidate;
            
            //future expansion


            return true;
        }


        ///<exception cref = "WatchdogDaemon.Exceptions.WatchdogInvalidMessageTypeException" ></exception>
        public bool ValidateMessageType(string messageTypeToValidate, Message messageToAdd)
        {
            if (messageTypeToValidate == null)
                throw new WatchdogInvalidEngineException("MessageType is null.");

            //check message type registry
            var messageTypeToValidaate = MessageTypeRepository.GetByName(messageTypeToValidate);
            if (messageTypeToValidate == null)
                throw new WatchdogInvalidMessageTypeException("MessageType could not be found in registry.");

            messageToAdd.MessageType = messageTypeToValidaate;

            return true;
        }

        ///<exception cref = "WatchdogInvalidParameterException" ></exception>
        ///<exception cref = "WatchdogDaemon.Exceptions.WatchdogInvalidMessageTypeException" ></exception>
        public bool ValidateParameters(UnvalidatedMessage toValidate, Message messageToAdd)
        {
            if (toValidate == null)
                //throw new WatchdogInvalidParameterException("UnvalidatedMessage is null");
                throw new InvalidOperationException();


            //get message type
            var messageTypeToValidateAgainst = MessageTypeRepository.GetByName(toValidate.MessageTypeName);

            if(messageTypeToValidateAgainst == null)
                throw new WatchdogInvalidMessageTypeException("MessageType provided was not found.");

            //get valid parameters
            var parametersToValidateAgainst =  messageTypeToValidateAgainst.MessageTypeParameterTypes;            
            
            //iterate through valid parameters
            foreach (var parameterToValidateAgainst in parametersToValidateAgainst)
            {
                var isInserted = false;
                //check unvalidated parameters
                foreach (var parameterToValidate in toValidate.MessageParameters)
                {
                    //validate parameter
                    var isValid = IsValidParameterType(parameterToValidate, parameterToValidateAgainst);
                    //if valid attatch to message with fk contraints
                    if (!isValid) continue;
                    AddParameter(parameterToValidate, parameterToValidateAgainst, messageToAdd);
                    isInserted = true;
                    break;
                }
                //if exhausted parameters and required throw error
                if (isInserted == false && parameterToValidateAgainst.Required == true)
                    throw new WatchdogInvalidParameterException("No record found for required parameter: " + parameterToValidateAgainst.Name + ".");
                    throw new KeyNotFoundException();

            }
            return true;
        }


        #region private methods

        //change to type handler no switch 
        private bool IsValidParameterType(UnvalidatedMessageParameter toValidate, MessageTypeParameterType validator)
        {
            if (!toValidate.Name.Equals(validator.Name)) return false;
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

        private static void AddParameter(UnvalidatedMessageParameter parameterToAdd, MessageTypeParameterType parameterTypeToAdd, Message messageToAdd)
        {
            var messageParameterToAdd = new MessageParameter
            {
                Value = parameterToAdd.Value,
                MessageTypeParameterType = parameterTypeToAdd
            };

            messageToAdd.MessageParameters.Add(messageParameterToAdd);
        }

        #endregion
    }
}
