using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.ExperienceForms.Mvc.Models.Validation;

namespace Feature.FormsExtensions.Fields
{
    [Serializable]
    public abstract class ValueNotValidatedInputViewModel<TValueType> : TitleFieldViewModel, IValueField, IValidatableField
    {
        [NonSerialized]
        private List<ModelClientValidationRule> clientValidationRules;
        [NonSerialized]
        private List<IValidationElement> validations;

        public virtual TValueType Value { get; set; }

        public List<ValidationDataModel> ValidationDataModels { get; } = new List<ValidationDataModel>();

        [JsonIgnore]
        public virtual IEnumerable<IValidationElement> Validations
        {
            get
            {
                if (validations != null)
                {
                    return validations;
                }
                validations = new List<IValidationElement>();
                ValidationDataModels.ForEach(validationDataModel =>
                {
                    var validationElement = validationDataModel.CreateValidationElement();
                    validationElement.Initialize(this);
                    validations.Add(validationElement);
                });
                return validations;
            }
        }

        [JsonIgnore]
        public IEnumerable<ModelClientValidationRule> ClientValidationRules
        {
            get
            {
                if (clientValidationRules != null)
                {
                    return clientValidationRules;
                }
                clientValidationRules = new List<ModelClientValidationRule>();
                foreach (var validation in Validations)
                    clientValidationRules.AddRange(validation.ClientValidationRules);
                return clientValidationRules;
            }
        }

        public bool IsTrackingEnabled { get; set; }

        public bool Required { get; set; }

        public bool AllowSave { get; set; }

        protected override void InitItemProperties(Item item)
        {
            base.InitItemProperties(item);
            IsTrackingEnabled = MainUtil.GetBool(item.Fields["Is Tracking Enabled"]?.Value, false);
            Required = MainUtil.GetBool(item.Fields["Required"]?.Value, false);
            AllowSave = MainUtil.GetBool(item.Fields["Allow Save"]?.Value, false);
            InitializeValidations(item);
        }

        protected override void UpdateItemFields(Item item)
        {
            base.UpdateItemFields(item);
            item.Fields["Is Tracking Enabled"]?.SetValue(IsTrackingEnabled ? "1" : string.Empty, true);
            item.Fields["Required"]?.SetValue(Required ? "1" : string.Empty, true);
            item.Fields["Allow Save"]?.SetValue(AllowSave ? "1" : string.Empty, true);
            item.Fields["Validations"]?.SetValue(StringUtil.ArrayToString(ValidationDataModels.Select(v => v.ItemId).ToArray(), '|'),true);           
        }

        protected virtual void InitializeValidations(Item item)
        {
            var field = item.Fields["Validations"];
            var validationPaths = field?.Value.Split('|');
            if (validationPaths == null)
                return;
            foreach (var path in validationPaths)
            {
                var validationItem = item.Database.GetItem(path, item.Language);
                if (validationItem != null)
                    ValidationDataModels.Add(new ValidationDataModel(validationItem));
            }
        }
    }
}