// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActiveDirectoryHelper.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.ActiveDirectory
{
    #region Usings

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.DirectoryServices;
    using System.DirectoryServices.AccountManagement;
    using System.Linq;
    using System.Web.Mvc;

    using Commons.Framework.Extensions;

    #endregion

    /// <summary>
    /// The user principal extended.
    /// </summary>
    //[DirectoryObjectClass("user")]
    //[DirectoryRdnPrefix("CN")]
    //public class UserPrincipalExtended : UserPrincipal
    //{
    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="UserPrincipalExtended"/> class.
    //    /// </summary>
    //    /// <param name="context">
    //    /// The context.
    //    /// </param>
    //    public UserPrincipalExtended(PrincipalContext context)
    //        : base(context)
    //    {
    //    }

    //    /// <summary>
    //    /// Gets or sets the company.
    //    /// </summary>
    //    [DirectoryProperty("company")]
    //    public string Company
    //    {
    //        get
    //        {
    //            if (this.ExtensionGet("company").Length != 1)
    //            {
    //                return null;
    //            }

    //            return (string)this.ExtensionGet("company")[0];
    //        }

    //        set => this.ExtensionSet("company", value);
    //    }

    //    /// <summary>
    //    /// Gets or sets the department.
    //    /// </summary>
    //    [DirectoryProperty("department")]
    //    public string Department
    //    {
    //        get
    //        {
    //            if (this.ExtensionGet("department").Length != 1)
    //            {
    //                return null;
    //            }

    //            return (string)this.ExtensionGet("department")[0];
    //        }

    //        set => this.ExtensionSet("department", value);
    //    }

    //    /// <summary>
    //    /// Gets or sets the mobile.
    //    /// </summary>
    //    [DirectoryProperty("mobile")]
    //    public string Mobile
    //    {
    //        get
    //        {
    //            if (this.ExtensionGet("mobile").Length != 1)
    //            {
    //                return null;
    //            }

    //            return (string)this.ExtensionGet("mobile")[0];
    //        }

    //        set => this.ExtensionSet("mobile", value);
    //    }

    //    /// <summary>
    //    /// Gets or sets the telephone number.
    //    /// </summary>
    //    [DirectoryProperty("telephoneNumber")]
    //    public string TelephoneNumber
    //    {
    //        get
    //        {
    //            if (this.ExtensionGet("telephoneNumber").Length != 1)
    //            {
    //                return null;
    //            }

    //            return (string)this.ExtensionGet("telephoneNumber")[0];
    //        }

    //        set => this.ExtensionSet("telephoneNumber", value);
    //    }

    //    /// <summary>
    //    /// Gets or sets the thumbnail photo.
    //    /// </summary>
    //    [DirectoryProperty("thumbnailPhoto")]
    //    public byte[] ThumbnailPhoto
    //    {
    //        get
    //        {
    //            if (this.ExtensionGet("thumbnailPhoto").Length != 1)
    //            {
    //                return null;
    //            }

    //            return this.ExtensionGet("thumbnailPhoto")[0] as byte[];
    //        }

    //        set => this.ExtensionSet("thumbnailPhoto", value);
    //    }

    //    /// <summary>
    //    /// Gets or sets the title.
    //    /// </summary>
    //    [DirectoryProperty("title")]
    //    public string Title
    //    {
    //        get
    //        {
    //            if (this.ExtensionGet("title").Length != 1)
    //            {
    //                return null;
    //            }

    //            return (string)this.ExtensionGet("title")[0];
    //        }

    //        set => this.ExtensionSet("title", value);
    //    }
    //}

    /// <summary>
    /// The active directory helper.
    /// </summary>
    public class ActiveDirectoryHelper
    {
        /// <summary>
        /// The container.
        /// </summary>
        private readonly string container;

        /// <summary>
        /// The domain name.
        /// </summary>
        private readonly string domainName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveDirectoryHelper"/> class.
        /// </summary>
        /// <param name="fullDomainName">
        /// The full domain name.
        /// </param>
        public ActiveDirectoryHelper(string fullDomainName)
        {
            this.domainName = fullDomainName;
            this.container = $"DC={string.Join(",DC=", fullDomainName.Split('.'))}";
        }

        private string GetProperty(DirectoryEntry oDE, string sPropertyName)
        {
            return oDE.Properties.Contains(sPropertyName) ? oDE.Properties[sPropertyName][0].ToString() : string.Empty;
        }


        /////// <summary>
        /////// The search ad users.
        /////// </summary>
        /////// <param name="name">
        /////// The name.
        /////// </param>
        /////// <param name="email">
        /////// The email.
        /////// </param>
        /////// <param name="job">
        /////// The job.
        /////// </param>
        /////// <param name="department">
        /////// The department.
        /////// </param>
        /////// <param name="phone">
        /////// The phone.
        /////// </param>
        /////// <param name="pageNum">
        /////// The page num.
        /////// </param>
        /////// <param name="pageSize">
        /////// The page size.
        /////// </param>
        /////// <param name="totalResults">
        /////// The total results.
        /////// </param>
        /////// <returns>
        /////// The <see cref="List"/>.
        /////// </returns>
        ////public List<ActiveDirectoryUser> SearchAdUsers(
        ////    string name,
        ////    string email,
        ////    string job,
        ////    string department,
        ////    string phone,
        ////    int pageNum,
        ////    int pageSize,
        ////    out int totalResults)
        ////{
        ////    totalResults = 0;

        ////    var users = new List<ActiveDirectoryUser>();

        ////    var getAllUsers = string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email)
        ////                      && string.IsNullOrEmpty(department) && string.IsNullOrEmpty(job)
        ////                      && string.IsNullOrEmpty(phone);

        ////    ////using (var ctx = new PrincipalContext(
        ////    ////    ContextType.Domain,
        ////    ////    MEPHelper.GetADCredentialInfo().ADServer,
        ////    ////    MEPHelper.GetADCredentialInfo().ADAdminUser,
        ////    ////    MEPHelper.GetADCredentialInfo().ADAdminPassword))

        ////    using (var principalContext =
        ////        new PrincipalContext(ContextType.Domain, this.domainName, this.container))
        ////    using (var criteria = new UserPrincipalExtended(principalContext))
        ////    {
        ////        if (!string.IsNullOrEmpty(name))
        ////        {
        ////            criteria.DisplayName = "*" + name + "*";
        ////        }

        ////        if (!string.IsNullOrEmpty(email))
        ////        {
        ////            criteria.EmailAddress = "*" + email + "*";
        ////        }

        ////        if (!string.IsNullOrEmpty(department))
        ////        {
        ////            criteria.Department = "*" + department + "*";
        ////        }

        ////        if (!string.IsNullOrEmpty(job))
        ////        {
        ////            criteria.Title = "*" + job + "*";
        ////        }

        ////        if (!string.IsNullOrEmpty(phone))
        ////        {
        ////            criteria.Mobile = "*" + phone + "*";
        ////        }

        ////        using (var searcherx = new PrincipalSearcher(criteria))
        ////        {
        ////            // ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).SearchRoot =
        ////            // new DirectoryEntry(
        ////            // MEPHelper.GetADCredentialInfo().ADFullPath,
        ////            // MEPHelper.GetADCredentialInfo().ADAdminUser,
        ////            // MEPHelper.GetADCredentialInfo().ADAdminPassword);
        ////            ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).PageSize = pageSize;

        ////            // ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).SearchScope = SearchScope.Subtree;
        ////            ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).SizeLimit = 10;

        ////            ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).Filter =
        ////                "(&(objectclass=user)(objectcategory=person))";

        ////            // ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).Sort = new SortOption("cn", SortDirection.Ascending);

        ////            // ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).PropertiesToLoad.Add("samaccountname");
        ////            ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).PropertiesToLoad.Add("mail");

        ////            // ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).PropertiesToLoad.Add("usergroup");
        ////            ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).PropertiesToLoad
        ////                .Add("displayname"); // first name

        ////            // ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).PropertiesToLoad.Add("firstName");
        ////            // ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).PropertiesToLoad.Add("department");
        ////            // ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).PropertiesToLoad.Add("company");
        ////            // ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).PropertiesToLoad.Add("telephoneNumber");
        ////            ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).PropertiesToLoad.Add("mobile");
        ////            ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).PropertiesToLoad.Add("title");
        ////            ((DirectorySearcher)searcherx.GetUnderlyingSearcher()).PropertiesToLoad.Add("CN");

        ////            if (getAllUsers)
        ////            {
        ////                ////var searchRoot = new DirectoryEntry(
        ////                ////    MEPHelper.GetADCredentialInfo().ADFullPath,
        ////                ////    MEPHelper.GetADCredentialInfo().ADAdminUser,
        ////                ////    MEPHelper.GetADCredentialInfo().ADAdminPassword);
        ////                var searchRoot = new DirectoryEntry("SURE");

        ////                var searcher =
        ////                    new DirectorySearcher(searchRoot)
        ////                        {
        ////                            PageSize = pageSize,
        ////                            SizeLimit = 10,
        ////                            Filter =
        ////                                "(&(objectclass=user)(objectcategory=person))"
        ////                                //// "(memberOf=CN=" + group + ",OU=Distribution Groups,DC=" + domain + ",DC=com)";
        ////                        };

        ////                // searcher.Sort = new SortOption("cn", SortDirection.Ascending);

        ////                searcher.PropertiesToLoad.Add("mail");
        ////                searcher.PropertiesToLoad.Add("mobile");
        ////                searcher.PropertiesToLoad.Add("displayname"); // first name
        ////                searcher.PropertiesToLoad.Add("title");
        ////                searcher.PropertiesToLoad.Add("department");

        ////                var results = this.FindObjects(
        ////                    (DirectorySearcher)searcherx.GetUnderlyingSearcher(),
        ////                    pageNum,
        ////                    pageSize,
        ////                    ref totalResults);

        ////                users.AddRange(
        ////                    results.Select(
        ////                        result => new ActiveDirectoryUser
        ////                                      {
        ////                                          DisplayName =
        ////                                              this.GetProperty(result, "displayname"),
        ////                                          Mobile = this.GetProperty(result, "mobile"),
        ////                                          Department =
        ////                                              this.GetProperty(result, "department"),
        ////                                          Email = this.GetProperty(result, "mail"),
        ////                                          Title = this.GetProperty(result, "title")
        ////                                      }));
        ////            }
        ////            else
        ////            {
        ////                var results = this.FindObjects(
        ////                    (DirectorySearcher)searcherx.GetUnderlyingSearcher(),
        ////                    pageNum,
        ////                    pageSize,
        ////                    ref totalResults);

        ////                users.AddRange(
        ////                    results.Select(
        ////                        result => new ActiveDirectoryUser
        ////                                      {
        ////                                          Company = this.GetProperty(result, "company"),
        ////                                          DisplayName =
        ////                                              this.GetProperty(result, "displayname"),
        ////                                          Mobile = this.GetProperty(result, "mobile"),
        ////                                          Department =
        ////                                              this.GetProperty(result, "department"),
        ////                                          Email = this.GetProperty(result, "mail"),
        ////                                          Title = this.GetProperty(result, "title")
        ////                                      }));
        ////            }
        ////        }
        ////    }

        ////    // DirectoryEntry searchRoot = new DirectoryEntry(MEPHelper.GetADCredentialInfo().ADFullPath, 
        ////    // MEPHelper.GetADCredentialInfo().ADAdminUser,
        ////    // MEPHelper.GetADCredentialInfo().ADAdminPassword);

        ////    // DirectorySearcher searcher = new DirectorySearcher(searchRoot);
        ////    // searcher.PageSize = pageSize;
        ////    // searcher.SizeLimit = 5000;
        ////    // searcher.Sort = new SortOption("cn", SortDirection.Ascending);
        ////    // searcher.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(mail=" + email + "))";

        ////    // searcher.PropertiesToLoad.Add("samaccountname");
        ////    // searcher.PropertiesToLoad.Add("mail");
        ////    // searcher.PropertiesToLoad.Add("usergroup");
        ////    // searcher.PropertiesToLoad.Add("displayname");//first name
        ////    // searcher.PropertiesToLoad.Add("firstName");
        ////    // searcher.PropertiesToLoad.Add("department");
        ////    // searcher.PropertiesToLoad.Add("company");
        ////    // searcher.PropertiesToLoad.Add("telephoneNumber");

        ////    // SearchResult result;
        ////    // SearchResultCollection resultCol = searcher.FindAll();

        ////    // if (resultCol != null)
        ////    // {
        ////    // for (int counter = 0; counter < resultCol.Count; counter++)
        ////    // {
        ////    // string UserNameEmailString = string.Empty;
        ////    // result = resultCol[counter];
        ////    // if (result.Properties.Contains("samaccountname") &&
        ////    // result.Properties.Contains("mail") &&
        ////    // result.Properties.Contains("displayname"))
        ////    // {
        ////    // AD_InformationInfo objSurveyUsers = new AD_InformationInfo();
        ////    // objSurveyUsers.mail = (string)result.Properties["mail"][0] +
        ////    // "^" + (string)result.Properties["displayname"][0];
        ////    // objSurveyUsers.company = (string)result.Properties["company"][0];
        ////    // objSurveyUsers.displayName = (string)(result.Properties["displayname"][0] ?? result.Properties["firstName"][0]);
        ////    // objSurveyUsers.telephoneNumber = (string)result.Properties["telephoneNumber"][0];

        ////    // objSurveyUsers.department = (string)result.Properties["department"][0];

        ////    // lstADUsers.Add(objSurveyUsers);
        ////    // }
        ////    // }
        ////    // }
        ////    return users;
        ////}

        ////private List<DirectoryEntry> FindObjects(DirectorySearcher
        ////                                             searcher, int pageIndex, int pageSize, ref int totalRecords)
        ////{
        ////    pageIndex = pageIndex - 1;
        ////    if (pageIndex < 0)
        ////    {
        ////        throw new
        ////            ArgumentOutOfRangeException(String.Format("Invalid page index specified { 0 }", pageIndex));
        ////    }
        ////    SearchResultCollection results = null;
        ////    List<DirectoryEntry> resultList = new
        ////        List<DirectoryEntry>();
        ////    totalRecords = 0;
        ////    int lowerIndex = 0;
        ////    int upperIndex = 0;
        ////    try
        ////    {
        ////        results = searcher.FindAll();
        ////        if (results != null && results.Count > 0)
        ////        {
        ////            totalRecords = results.Count;
        ////            // 
        ////            // Check if the client wants record paging or not? 
        ////            // 
        ////            if (pageSize == int.MaxValue)
        ////            {
        ////                pageSize = totalRecords;
        ////                lowerIndex = 0;
        ////                upperIndex = totalRecords - 1;
        ////            }
        ////            else
        ////            {
        ////                int totalPages =
        ////                    CalculateTotalPages(totalRecords, pageSize);
        ////                if ((pageIndex == 0) && (pageIndex < totalPages
        ////                                         - 1))
        ////                {
        ////                    lowerIndex = pageIndex * pageSize;
        ////                    upperIndex = lowerIndex + pageSize - 1;
        ////                }
        ////                else
        ////                {
        ////                    lowerIndex = pageIndex * pageSize;
        ////                    upperIndex = totalRecords - 1;
        ////                }
        ////            }
        ////            // 
        ////            // Page through and fish out the entries we need 
        ////            // 
        ////            for (int index = lowerIndex; index <= upperIndex;
        ////                 index++)
        ////            {
        ////                DirectoryEntry entry =
        ////                    results[index].GetDirectoryEntry();
        ////                resultList.Add(entry);
        ////                entry.Close();
        ////            }
        ////        }
        ////    }
        ////    finally
        ////    {
        ////        searcher?.Dispose();
        ////        results?.Dispose();
        ////    }

        ////    return resultList;
        ////}

        ////private int CalculateTotalPages(int totalRecords, int pageSize)
        ////{
        ////    double totalPages = 1;
        ////    if (totalRecords == 0)
        ////    {
        ////        return 0;
        ////    }

        ////    // First calculate the division 
        ////    // 

        ////    totalPages = totalRecords / pageSize;
        ////    totalPages = System.Math.Ceiling((totalPages));
        ////    return totalPages.To<int>();
        ////}

        public List<ActiveDirectoryUser> AutocompleteUsers(string name)
        {
            var filter = "(&(&(objectCategory=person)(objectClass=user)(!userAccountControl:1.2.840.113556.1.4.803:=2))(|(givenName={0}*)(sn={1}*)))";
            var users = new List<ActiveDirectoryUser>();
            
            using (var dS = new DirectorySearcher(this.container) { Filter = string.Format(filter, name, name) })
            {
                dS.PropertiesToLoad.Add("memberOf");
                dS.PropertiesToLoad.Add("sAMAccountName");

                foreach (SearchResult result in dS.FindAll())
                {
                    if (result == null)
                    {
                        continue;
                    }

                    var entry = result.GetDirectoryEntry();

                    users.Add(
                        new ActiveDirectoryUser
                            {
                                MemberOfGroups = this.GetUserGroupNames(entry),
                                DisplayName = (entry.Properties["displayName"].Value ?? string.Empty)
                                    .ToString(),
                                UserName =
                                    (entry.Properties["sAMAccountName"].Value ?? string.Empty)
                                    .ToString(),
                                Email = (entry.Properties["mail"].Value ?? string.Empty).ToString(),
                                NationalId = (entry.Properties["employeeNumber"].Value ?? string.Empty).ToString(),
                                Mobile = (entry.Properties["telephoneNumber"].Value ?? string.Empty).ToString(),
                                Number = (entry.Properties["employeeID"].Value ?? string.Empty).ToString()

                        });
                }

                return users;
            } 
        }

        private List<string> GetUserGroupNames(DirectoryEntry entry)
        {
            if (entry.Properties["memberOf"].Value == null)
            {
                return new List<string>();
            }

            var groups = entry.Properties["memberOf"].Value as object[];            
            if (groups != null && groups.Any())
            {
                return entry.Properties["memberOf"].Value?.To<object[]>()
                    .Select(o => o.ToString().Split(',')[0]?.Replace("CN=", string.Empty)).ToList();
            }

            var groupName = entry.Properties["memberOf"].Value as string;
            if (!string.IsNullOrEmpty(groupName))
            {
                return new List<string> { groupName.Split(',')[0]?.Replace("CN=", string.Empty) };
            }

            return new List<string>();
        }

        ////public List<string> SearchUsers()
        ////{
        ////    using (var principalContext =
        ////        new PrincipalContext(ContextType.Domain, this.domainName, this.container))
        ////    {
        ////        // "DC=SURE,DC=COM,DC=SA"))
        ////        // using (var group = GroupPrincipal.FindByIdentity(context, windowsGroup.TrimEnd('*')))
        ////        using (var groupPrincipal =
        ////            GroupPrincipal.FindByIdentity(principalContext, IdentityType.SamAccountName, "groupName"))
        ////        {
        ////            if (groupPrincipal != null)
        ////            {
        ////                var users = groupPrincipal.GetMembers();
        ////                foreach (UserPrincipal userPrincipal in users)
        ////                {
        ////                    // user variable has the details about the user 
        ////                }
        ////            }
        ////        }
        ////    }
        ////}
    }
}