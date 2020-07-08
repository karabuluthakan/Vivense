using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using MongoDB.Bson;

namespace Library.CrossCuttingConcerns.Validation
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class BsonIdValidateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var id = value as string;
            if (null == id)
            {
                return ValidationResult.Success;
            } else if (ObjectId.TryParse(id, out _))
            {
                return ValidationResult.Success;
            } else
            {
                var errorMessage = FormatErrorMessage(ErrorMessageString);
                return new ValidationResult(errorMessage);
            }
        }
    }

    public class BsonIdValidateAttributeAdapter : AttributeAdapterBase<BsonIdValidateAttribute>
    {
        public BsonIdValidateAttributeAdapter(BsonIdValidateAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName());
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-mongo-id",
                GetErrorMessage(context));
        }
    }

    public class BsonIdValidateAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is BsonIdValidateAttribute)
            {
                return new BsonIdValidateAttributeAdapter(attribute as BsonIdValidateAttribute, stringLocalizer);
            }

            return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}