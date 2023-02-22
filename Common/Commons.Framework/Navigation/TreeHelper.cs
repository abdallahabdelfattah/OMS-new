// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeHelper.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Navigation
{
    #region

    using System.Collections.Generic;
    using System.Linq;

    using Commons.Framework.Data;
    using Commons.Framework.Extensions;

    #endregion

    /// <summary>
    /// The node state.
    /// </summary>
    public class NodeState
    {
        /// <summary>
        /// Gets or sets a value indicating whether disabled.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether opened.
        /// </summary>
        public bool Opened { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether selected.
        /// </summary>
        public bool Selected { get; set; }
    }

    /// <summary>
    /// The tree node.
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode"/> class.
        /// </summary>
        public TreeNode()
        {
            this.Children = new List<TreeNode>();
        }

        /// <summary>
        ///     Gets or sets the children.
        /// </summary>
        public List<TreeNode> Children { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public NodeState State { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// The tree helper.
    /// </summary>
    public static class TreeHelper
    {
        /// <summary>
        /// The populate tree.
        /// </summary>
        /// <param name="root">
        /// The root.
        /// </param>
        /// <param name="departments">
        /// The departments.
        /// </param>
        public static void PopulateTree(ref TreeNode root, List<Navigation> departments)
        {
            if (root == null)
            {
                root = new TreeNode { Text = "ROOT", Id = null };

                // get all departments in the list with parent is null
                var details = departments.Where(t => t.Parent == null);
                foreach (var detail in details)
                {
                    var child = new TreeNode { Text = $"{detail.NameAr} - {detail.NameEn}", Id = detail.Id.ToString() };
                    PopulateTree(ref child, departments);
                    root.Children.Add(child);
                }
            }
            else
            {
                var id = root.Id.To<int>();
                var details = departments.Where(t => t.ParentId == id);
                foreach (var detail in details)
                {
                    var child = new TreeNode { Text = $"{detail.NameAr} - {detail.NameEn}", Id = detail.Id.ToString() };
                    PopulateTree(ref child, departments);
                    root.Children.Add(child);
                }
            }
        }
    }
}