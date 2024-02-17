﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cards.WEB.Models.JQueryDataTables
{
    public class JQueryModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(JQueryDataTablesModel))
            {
                return new BinderTypeModelBinder(typeof(JQueryDataTablesModelBinder));
            }

            return null;
        }
    }
}
