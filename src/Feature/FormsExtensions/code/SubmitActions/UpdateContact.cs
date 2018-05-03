using System;
using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.XDb;
using Feature.FormsExtensions.XDb.Model;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;

namespace Feature.FormsExtensions.SubmitActions
{
    public class UpdateContact : SubmitActionBase<UpdateContactData>
    {
        private readonly IXDbService xDbService;

        public UpdateContact(ISubmitActionData submitActionData, IXDbService xDbService) : base(submitActionData)
        {
            this.xDbService = xDbService;
        }
        
        protected override bool Execute(UpdateContactData data, FormSubmitContext formSubmitContext)
        {
            var naloContact = BuildContact(data, formSubmitContext);
            if (naloContact == null)
                return false;
            
            try
            {
                xDbService.IdentifyCurrent(naloContact);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return false;
            }
        }

        private IBasicContact BuildContact(UpdateContactData data, FormSubmitContext formSubmitContext)
        {
            Assert.ArgumentNotNull(data, nameof(data));
            Assert.ArgumentNotNull(formSubmitContext, nameof(formSubmitContext));
            var firstNameField = GetFieldById(data.FirstNameFieldId, formSubmitContext.Fields);
            var lastNameField = GetFieldById(data.LastNameFieldId, formSubmitContext.Fields);
            var emailField = GetFieldById(data.EmailFieldId, formSubmitContext.Fields);
            if (firstNameField == null && lastNameField == null && emailField == null)
            {
                return null;
            }
            return CreateNaloContact(firstNameField, lastNameField, emailField);
        }

        private static IBasicContact CreateNaloContact(IViewModel firstNameField, IViewModel lastNameField, IViewModel emailField)
        {
            var naloContact = new BasicContact();
            naloContact.FirstName = GetValue(firstNameField);
            naloContact.LastName = GetValue(lastNameField);
            naloContact.Email = GetValue(emailField);
            return naloContact;
        }

        private static IViewModel GetFieldById(Guid id, IList<IViewModel> fields)
        {
            return fields.FirstOrDefault(f => Guid.Parse(f.ItemId) == id);
        }

        private static string GetValue(object field)
        {
            return field?.GetType().GetProperty("Value")?.GetValue(field, null)?.ToString() ?? string.Empty;
        }
        
    }
}