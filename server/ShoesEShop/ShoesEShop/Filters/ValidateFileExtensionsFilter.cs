using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoesEShop.Web.Attributes;

namespace ShoesEShop.Web.Filters
{
    public class ValidateFileExtensionsFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // get controller descriptor
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            // take the request action name
            var actionName = descriptor.ActionName;
            var method = descriptor.MethodInfo;

            var validFileAttribute = (AllowFileExtensionsAttribute)method
                .GetCustomAttributes(typeof(AllowFileExtensionsAttribute), false)
                .FirstOrDefault();

            if (validFileAttribute != null) 
            {
                ValidateSingleFile(context, validFileAttribute);
                ValidateMultipleFiles(context, validFileAttribute);
            }
        }

        private void ValidateSingleFile(ActionExecutingContext context, AllowFileExtensionsAttribute validateAttribute)
        {
            var requestedFile = context.ActionArguments.Values.OfType<IFormFile>().FirstOrDefault();
            if (requestedFile != null && validateAttribute.IsValidExtension(Path.GetExtension(requestedFile.FileName)))
            {
                context.Result = new BadRequestObjectResult(new
                {
                    IsSucceed = false,
                    Message = "File type is not allowed"
                });
            }
            return;
        }

        private void ValidateMultipleFiles(ActionExecutingContext context, AllowFileExtensionsAttribute validateAttribute)
        {
            var fileCollection = context.ActionArguments.Values.OfType<IFormFileCollection>().FirstOrDefault();
            if (fileCollection != null)
            {
                foreach (var file in fileCollection)
                {
                    if (file != null && !validateAttribute.IsValidExtension(Path.GetExtension(file.FileName)))
                    {
                        context.Result = new BadRequestObjectResult(new
                        {
                            IsSucceed = false,
                            Message = "File type is not allowed"
                        });
                        return;
                    }
                }
            }
        }
    }
}
