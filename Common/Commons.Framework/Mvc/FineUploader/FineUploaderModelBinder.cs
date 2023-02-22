// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FineUploaderModelBinder.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.FineUploader
{
    #region

    using System.IO;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// The fine upload.
    /// </summary>
    [ModelBinder(typeof(ModelBinder))]
    public class FineUpload
    {
        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the input stream.
        /// </summary>
        public Stream InputStream { get; set; }

        /// <summary>
        /// The save as.
        /// </summary>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <param name="overwrite">
        /// The overwrite.
        /// </param>
        /// <param name="autoCreateDirectory">
        /// The auto create directory.
        /// </param>
        public void SaveAs(string destination, bool overwrite = false, bool autoCreateDirectory = true)
        {
            if (autoCreateDirectory)
            {
                var directory = new FileInfo(destination).Directory;
                if (directory != null) directory.Create();
            }

            using (var file = new FileStream(destination, overwrite ? FileMode.Create : FileMode.CreateNew))
                this.InputStream.CopyTo(file);
        }

        /// <summary>
        /// The model binder.
        /// </summary>
        public class ModelBinder : IModelBinder
        {
            /// <summary>
            /// The bind model.
            /// </summary>
            /// <param name="controllerContext">
            /// The controller context.
            /// </param>
            /// <param name="bindingContext">
            /// The binding context.
            /// </param>
            /// <returns>
            /// The <see cref="object"/>.
            /// </returns>
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                var request = controllerContext.RequestContext.HttpContext.Request;
                var formUpload = request.Files.Count > 0;

                // find filename
                var xFileName = request.Headers["X-File-Name"];
                var qqFile = request["qqfile"];
                var formFilename = formUpload ? request.Files[0].FileName : null;

                var upload = new FineUpload
                                 {
                                     Filename = xFileName ?? qqFile ?? formFilename,
                                     InputStream =
                                         formUpload ? request.Files[0].InputStream : request.InputStream
                                 };

                return upload;
            }
        }
    }
}