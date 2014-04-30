﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UmbracoValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UmbracoRequired : RequiredAttribute, IClientValidatable
    {
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessageDictionaryKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            //Get dictionary value for thge required error message
            //WB: UNSURE if this will double check our UmbContext exists or not
            ErrorMessage = UmbracoValidationHelper.FormatErrorMessage(name, ErrorMessageDictionaryKey);
           
            return base.FormatErrorMessage(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var error   = FormatErrorMessage(metadata.GetDisplayName());
            var rule    = new ModelClientValidationRequiredRule(error);

            yield return rule;
        }
    }
}
