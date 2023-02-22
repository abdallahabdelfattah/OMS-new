// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateFileUploadAttribute.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.DataAnnotations
{
    #region

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;
    using System.Linq;
    using System.Resources;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;

    using Commons.Framework.Data;
    using Commons.Framework.Resources;

    #endregion

    /// <summary>
    ///     The validate file.
    /// </summary>
    public class ValidateFileUploadAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        ///     The file image dimensions error message.
        /// </summary>
        private readonly string fileImageDimensionsErrorMessage;

        /// <summary>
        ///     The file not valid image error message.
        /// </summary>
        private readonly string fileNotValidImageErrorMessage;

        /// <summary>
        ///     The file size error message.
        /// </summary>
        private readonly string fileSizeErrorMessage;

        private readonly string emptyAttachmentIsNotAllowedErrorMessage;

        /// <summary>
        ///     The file type error message.
        /// </summary>
        private readonly string fileTypeErrorMessage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidateFileUploadAttribute" /> class.
        /// </summary>
        public ValidateFileUploadAttribute()
        {
            var resourceManager = new ResourceManager(this.ErrorMessageResourceType ?? typeof(Messages));
            this.fileTypeErrorMessage = resourceManager.GetString(this.FileTypeErrorMessageResourceName);
            this.fileSizeErrorMessage = resourceManager.GetString(this.FileSizeErrorMessageResourceName);
            this.emptyAttachmentIsNotAllowedErrorMessage = resourceManager.GetString(this.EmptyAttachmentIsNotAllowedResourceName);
            this.fileImageDimensionsErrorMessage =
                resourceManager.GetString(this.FileImageDimensionsErrorMessageResourceName);
            this.fileNotValidImageErrorMessage =
                resourceManager.GetString(this.FileNotValidImageErrorMessageResourceName);
    
            using (var uow = new CommonsUnitOfWork())
            {
                if (this.AttachmentTypeId != 0 && uow.AttachmentTypes.Exists(this.AttachmentTypeId))
                {
                    var attType = uow.AttachmentTypes.GetById(this.AttachmentTypeId);
                    this.AllowedExtensions = attType.AllowedFilesExtension;
                    this.ImageMaxHeight = attType.ImageMaxHeight ?? 0;
                    this.ImageMaxWidth = attType.ImageMaxWidth ?? 0;
                    this.MaxSizeInMegabytes = attType.MaxSizeInMegabytes;
                }
                else
                {
                    this.AllowedExtensions = uow.SystemSettings.GetValue<string>("AttachmentsAllowedTypes");
                    this.ImageMaxHeight = uow.SystemSettings.GetValue<int>("ImageMaxHeight");
                    this.ImageMaxWidth = uow.SystemSettings.GetValue<int>("ImageMaxWidth");
                    this.MaxSizeInMegabytes = uow.SystemSettings.GetValue<int>("AttachmentsMaxSize");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the allowed extensions.
        /// </summary>
        public string AllowedExtensions { get; set; }

        /// <summary>
        ///     Gets the allowed extensions regex.
        /// </summary>
        public string AllowedExtensionsRegex
        {
            get
            {
                if (string.IsNullOrEmpty(this.AllowedExtensions))
                {
                    return null;
                }

                var allowedTypesArr = this.AllowedExtensions.Split(
                    new[] { ";", "|", ":", "," },
                    StringSplitOptions.RemoveEmptyEntries);

                var extensions = allowedTypesArr.Select(
                    type => type.Aggregate(
                        string.Empty,
                        (current, c) => current + $"[{c.ToString().ToLower()}{c.ToString().ToUpper()}]"));
                var allowedExtensionsRegex = @"(.*?)\.(" + string.Join("|", extensions) + ")$";
                return allowedExtensionsRegex;
            }
        }

        /// <summary>
        /// Gets or sets the attachment type id.
        /// </summary>
        public int AttachmentTypeId { get; set; }

        /// <summary>
        ///     Gets or sets the file image dimensions error message resource name.
        /// </summary>
        public string FileImageDimensionsErrorMessageResourceName { get; set; } = "FileImageDimensionsErrorMessage";

        /// <summary>
        ///     Gets or sets the file not valid image error message resource name.
        /// </summary>
        public string FileNotValidImageErrorMessageResourceName { get; set; } = "FileNotValidImageErrorMessage";

        /// <summary>
        ///     Gets or sets the file size error message resource name.
        /// </summary>
        public string FileSizeErrorMessageResourceName { get; set; } = "FileSizeErrorMessage";

        /// <summary>
        ///     Gets or sets the file type error message resource name.
        /// </summary>
        public string FileTypeErrorMessageResourceName { get; set; } = "FileTypeErrorMessage";

        public  string EmptyAttachmentIsNotAllowedResourceName { get; set; } = "EmptyAttachmentIsNotAllowedErrorMessage";

        /// <summary>
        ///     Gets or sets the image max height.
        /// </summary>
        public int ImageMaxHeight { get; set; }

        /// <summary>
        ///     Gets or sets the image max width.
        /// </summary>
        public int ImageMaxWidth { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is image.
        /// </summary>
        public bool IsImage { get; set; }

        /// <summary>
        ///     Gets or sets the max size in megabytes.
        /// </summary>
        public int MaxSizeInMegabytes { get; set; }

        /// <summary>
        /// The get client validation rules.
        /// </summary>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata,
            ControllerContext context)
        {
            var modelClientValRule =
                new ModelClientValidationRule
                    {
                        ValidationType = "checkisvalidfile",
                        ErrorMessage = this.ErrorMessage ?? this.ErrorMessageString,
                        ValidationParameters =
                            {
                                {
                                    "allowedextensions",
                                    this.AllowedExtensions
                                },
                                {
                                    "isimage",
                                    this.IsImage.ToString().ToLower()
                                },
                                {
                                    "maxsizeinmegabytes",
                                    this.MaxSizeInMegabytes
                                },
                                { "imagemaxheight", this.ImageMaxHeight },
                                { "imagemaxwidth", this.ImageMaxWidth },
                                {
                                    "allowedextensionsregex",
                                    this.AllowedExtensionsRegex
                                },
                                {
                                    "fileimagedimensionserrormessage",
                                    this.fileImageDimensionsErrorMessage
                                },
                                {
                                    "filenotvalidimageerrormessage",
                                    this.fileNotValidImageErrorMessage
                                },
                                {
                                    "filesizeerrormessage",
                                    this.fileSizeErrorMessage
                                },
                                {
                                    "emptyattachmentisnotallowederrormessage",
                                    this.emptyAttachmentIsNotAllowedErrorMessage
                                },
                                {
                                    "filetypeerrormessage",
                                    this.fileTypeErrorMessage
                                }
                            }
                    };

            return new List<ModelClientValidationRule> { modelClientValRule };
        }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (!(value is HttpPostedFileBase file))
            {
                return true;
            }

            var errorMessages = new List<string>();

            var isValid = true;

            if (!string.IsNullOrEmpty(this.AllowedExtensions))
            {
                var regex = new Regex(this.AllowedExtensionsRegex);
                var match = regex.Match(file.FileName);
                if (!match.Success)
                {
                    errorMessages.Add(string.Format(this.fileTypeErrorMessage, this.AllowedExtensions));
                    isValid = false;
                }
            }

            if (file.ContentLength == 0)
            {
                errorMessages.Add(this.emptyAttachmentIsNotAllowedErrorMessage);
                isValid = false;
            }

            if (this.MaxSizeInMegabytes > 0 && file.ContentLength > this.MaxSizeInMegabytes * 1024 * 1024)
            {
                errorMessages.Add(string.Format(this.fileSizeErrorMessage, this.MaxSizeInMegabytes));
                isValid = false;
            }

            if (this.IsImage)
            {
                try
                {
                    var stream = file.InputStream;

                    var image = Image.FromStream(stream);

                    // bellow two lines are required to make file stream return from beginning
                    // so that MVC binding read file correctly
                    // If the below 2 lines removed the file will be read incorrectly and will be corrupted
                    file.InputStream.Position = 0;
                    file.InputStream.Flush();

                    if (this.ImageMaxHeight > 0 && image.Height > this.ImageMaxHeight
                        || this.ImageMaxWidth > 0 && image.Width > this.ImageMaxWidth)
                    {
                        errorMessages.Add(
                            string.Format(
                                this.fileImageDimensionsErrorMessage,
                                this.ImageMaxHeight,
                                this.ImageMaxWidth));

                        isValid = false;
                    }
                }
                catch (Exception)
                {
                    isValid = false;
                    errorMessages.Add(this.fileNotValidImageErrorMessage);
                }
            }

            this.ErrorMessage = string.Join("\r\n", errorMessages);
            return isValid;
        }
    }
}