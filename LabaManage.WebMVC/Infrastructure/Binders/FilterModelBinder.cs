﻿using System.Web.Mvc;
using LabaManage.Models.Models;

namespace LabaManage.WebMVC.Infrastructure.Binders
{
    public class FilterModelBinder : IModelBinder
    {
        private const string Sessionkey = "FilterModel";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            FilterModel filterModel = null;
            if (controllerContext.HttpContext.Session != null)
            {
                filterModel = (FilterModel)controllerContext.HttpContext.Session[Sessionkey];
            }

            if (filterModel == null)
            {
                filterModel = new FilterModel();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[Sessionkey] = filterModel;
                }
            }

            return filterModel;
        }
    }
}