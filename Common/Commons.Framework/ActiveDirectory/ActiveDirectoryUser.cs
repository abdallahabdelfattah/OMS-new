// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActiveDirectoryUser.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.ActiveDirectory
{
    using System.Collections.Generic;

    /// <summary>
    /// The active directory user.
    /// </summary>
    public class ActiveDirectoryUser
    {
        

        public ActiveDirectoryUser()
        {
            
            this.MemberOfGroups = new List<string>();
        }
        /// <summary>
        ///     Gets the department.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        ///     Gets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        public string Company { get; set; }

        public string Mobile { get; set; }

        public string Title { get; set; }

        public string NationalId { get; set; }

        /// <summary>
        ///     Gets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        ///     Gets the office.
        /// </summary>
        public string Office { get; set; }

        /// <summary>
        ///     Gets the user name.
        /// </summary>
        public string UserName { get; set; }

        public List<string> MemberOfGroups { get; set; }
    }
}