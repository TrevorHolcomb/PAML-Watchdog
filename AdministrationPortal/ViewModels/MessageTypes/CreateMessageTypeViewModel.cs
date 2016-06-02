using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine;
using System;


namespace AdministrationPortal.ViewModels.MessageTypes
{
    public class CreateMessageTypeViewModel : IValidatableObject
    {
        public readonly int MAX_PARAMETERS = 5;

        [Required, StringLength(1000), Key]
        public string Name { get; set; }
        [Required, StringLength(1000)]
        public string Description { get; set; }
        [Required]
        public List<ParameterType> ParameterTypes { get; set; }
        [Required]
        public List<ParameterName> ParameterNames { get; set; }
        [Required]
        public List<bool> ParametersEnabled { get; set; }
        [Required]
        public List<bool> ParametersRequired { get; set; }


        public SelectList SupportedParameterTypes { get; set; }

        public CreateMessageTypeViewModel() : this(null, null) { }

        public CreateMessageTypeViewModel(string name, string description)
        {
            Name = name;
            Description = description;
            SupportedParameterTypes = new SelectList(TypesSupported.Types);
            ParameterTypes = new List<ParameterType>();
            ParameterNames = new List<ParameterName>();
            ParametersRequired = new List<bool>();
            ParametersEnabled = new List<bool>();

            PrepopulateLists();
        }

        private void PrepopulateLists()
        {
            for (int param = 0; param < MAX_PARAMETERS; param++)
            {
                ParameterNames.Add("");
                ParametersRequired.Add(true);
                ParametersEnabled.Add(false);
            }
            ParametersEnabled[0] = true;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            for (int i = 0; i < ParameterNames.Count; i++)
            {
                if (ParametersEnabled[i] && (ParameterNames[i] == null || ParameterNames[i].Value == null || ParameterNames[i].Value.Trim() == ""))
                    results.Add(new ValidationResult("Parameters must have Names."));
            }

            var duplicateKeys = ParameterNames.GroupBy(x => x.Value)
                    .Where(group => group.Count() > 1)
                    .Select(group => group.Key);

            if (duplicateKeys.Count() != 0)
                results.Add(new ValidationResult("Parameter Names must be unique."));

            foreach (ParameterType parameterType in ParameterTypes)
                if (!TypesSupported.Types.Contains(parameterType))
                    results.Add(new ValidationResult("Unsupported parameter Type: " + parameterType.Value));

            return results;
        }

        public class ParameterType
        {
            [StringLength(1000)]
            public string Value { get; set; }

            public static implicit operator ParameterType(string type) { return new ParameterType { Value = type }; }
            public static implicit operator string(ParameterType type) { return type.Value; }
        }

        public class ParameterName
        {
            [StringLength(1000)]
            public string Value { get; set; }

            public static implicit operator ParameterName(string name) { return new ParameterName { Value = name }; }
            public static implicit operator string(ParameterName parameterName) { return parameterName.Value; }
        }
    }
}