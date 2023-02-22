// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonDotNetResult.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc
{
    #region

    using System.Web.Mvc;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    #endregion

    /// <summary>
    ///     The json dot net result.
    /// </summary>
    public class JsonDotNetResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDotNetResult"/> class.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        public JsonDotNetResult(object obj)
        {
            this.Obj = obj;
        }

        public JsonDotNetResult(AjaxResult obj)
        {
            this.Obj = obj;
        }

        /// <summary>
        ///     The json serializer settings.
        /// </summary>
        private static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings
                                                                            {
                                                                                ContractResolver
                                                                                    = new
                                                                                        CamelCasePropertyNamesContractResolver(),
                                                                                NullValueHandling
                                                                                    = NullValueHandling
                                                                                        .Include,
                                                                                ObjectCreationHandling
                                                                                    = ObjectCreationHandling
                                                                                        .Replace,
                                                                                MissingMemberHandling
                                                                                    = MissingMemberHandling
                                                                                        .Ignore,

                                                                                // for everything else
                                                                                PreserveReferencesHandling
                                                                                    = PreserveReferencesHandling
                                                                                        .None,
                                                                                ReferenceLoopHandling
                                                                                    = ReferenceLoopHandling
                                                                                        .Ignore,
                                                                                DateFormatString
                                                                                    = "dd/MM/yyyy",

                                                                                // MaxDepth = 3,
                                                                                Formatting =
                                                                                    Formatting
                                                                                        .None
                                                                            };

        /// <summary>
        ///     Gets the obj.
        /// </summary>
        private object Obj { get; }

        /// <summary>
        /// The execute result.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.AddHeader("content-type", "application/json");
            context.HttpContext.Response.Write(JsonConvert.SerializeObject(this.Obj, JsonSerializerSettings));
        }
    }
}