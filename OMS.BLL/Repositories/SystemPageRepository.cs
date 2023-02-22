using OMS.Model.SystemEnums;
using OMS.DAL;
using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OMS.Model.ViewModel;
using OMS.BLL.Data;

namespace OMS.BLL.Repositories
{
    public partial class SystemPageRepository : RepositoryBase<SystemPage>
    {
        public SystemPageRepository(DbContext context)
            : base(context)
        {
        }
    }
    public partial class SystemPageRepository : RepositoryBase<SystemPage>
    {
        public PagedList<SystemPage> Search(SystemPageSearchViewModel sc)
        {
            var query = this.DbSet.AsQueryable();
            query = query.Where(C => C.Enabled != false);

            if (sc.Id > 0)
            {
                query = query.Where(e => e.Id == sc.Id);
            }
            
            if (!string.IsNullOrEmpty(sc.Name))
            {
                query = query.Where(e => e.NameAr.Contains(sc.Name) || e.NameEn.Contains(sc.Name));
            }
            
            if (sc.GetAll == true)
            {
                query = query.Where(C => C.ParentId == null);
            }

            if (sc.HasActions == true)
            {
                query = query.Where(p => p.PageActions.Count > 0);
            }
            
            if (!string.IsNullOrEmpty(sc.NameNotEqual))
            {
                query = query.Where(C => C.NameEn != sc.NameNotEqual);
            }

            var result = query.GetPaged(
                l => l.Id,
                true,
                sc.PageIndex,
                sc.RowCount);

            return result;
        }
        public SystemPage MapViewModelToEntity(SystemPageModel vm)
        {
            if (vm != null)
                return new SystemPage
                {
                    NameAr = vm.NameAr,
                    NameEn = vm.NameEn,
                };
            return null;
        }
        public SystemPageModel MapEntityToViewModel(SystemPage entity)
        {
            if (entity != null)
                return new SystemPageModel
                {
                    NameAr = entity.NameAr,
                    NameEn = entity.NameEn,
                };
            return null;
        }
        public List<MenuModel> GetUserMenu(long UserId, bool isArabic)
        {
            var uow = new OMSUoW();

            List<long?> _SystemPageSystemPage = uow.UserPageActions.Get(r => r.Mode == (int)PageActionMode.Yes 
                    && r.UserId == UserId && r.PageAction.NameEn.ToLower() == "view"
                    ,null, u=>u.PageAction).Select(s => s.PageAction.PageId).ToList();

            List<long?> rejected = uow.UserPageActions.Get(x => x.Mode == (int)PageActionMode.No
                    && x.UserId == UserId 
                    && x.PageAction.NameEn.ToLower() == "view"
                    , null, u => u.PageAction)
                    .Select(s => s.PageAction.PageId).ToList();

            List<long?> _GroupSystemPage = uow.GroupPageActions.Get(x => x.PageAction.NameEn.ToLower() == "view"
                    && x.Group.UserGroups.Where(ug => ug.UserId == UserId).Count() > 0
                    && !rejected.Contains(x.PageAction.PageId)
                    && !_SystemPageSystemPage.Contains(x.PageAction.PageId)
                    , null, u => u.PageAction, u => u.Group, u => u.Group.UserGroups)
                    .Select(s => s.PageAction.PageId).ToList();

            _SystemPageSystemPage.AddRange(_GroupSystemPage);





            List<MenuModel> Menu = new List<MenuModel>();
            //var filters = new List<Expression<Func<SystemPage, bool>>>();
            //filters.Add(sp => _SystemPageSystemPage.Contains(sp.Id));

            var lstpages = uow.SystemPages.Get(sp => _SystemPageSystemPage.Contains(sp.Id),
                a => a.OrderBy(b => b.Sequence),
                a=>a.Parent).ToList();

            foreach (var item in lstpages)
            {
              
                if (item.ParentId.HasValue)
                {
                    var _parent = Menu.FirstOrDefault(x => x.Id == item.ParentId);
                   
                    if (_parent == null)
                    {
                        _parent = new MenuModel();
                        _parent.Id = item.Parent.Id;
                        _parent.Name = isArabic?item.Parent.NameAr: item.Parent.NameEn;
                        _parent.URL = item.Parent.URL;
                        _parent.Icon = item.Parent.IconText;
                        _parent.Sequence = item.Parent.Sequence;
                        Menu.Add(_parent);
                    }
                    MenuModel _menuItem = new MenuModel();
                    _menuItem.Id = item.Id;
                    _menuItem.Name = isArabic ? item.NameAr: item.NameEn;
                    _menuItem.URL = item.URL;
                    _menuItem.Icon = item.IconText;
                    _menuItem.Sequence = item.Sequence;
                    _parent.Children.Add(_menuItem);
                }
                else 
                {
                    if (Menu.FirstOrDefault(x => x.Id == item.Id) == null)
                    {
                        MenuModel _menuItem = new MenuModel();
                        _menuItem.Id = item.Id;
                        _menuItem.Name = isArabic ? item.NameAr : item.NameEn;
                        _menuItem.URL = item.URL;
                        _menuItem.Sequence = item.Sequence;
                        _menuItem.Icon = item.IconText;
                        Menu.Add(_menuItem);
                    }
                }
            }

            return Menu.OrderBy(m => m.Sequence).ToList();
        }
    }

    public class MenuModel
    {
        public long Id { get; set; }
        private string _Name;
        public string URL { get; set; }
        public List<MenuModel> Children { get; set; }
        public string Icon { get; set; }
        public long? Sequence { get; set; }

        public string Name
        {
            get
            { return _Name; }
            set
            { _Name = value; }
        }

        public MenuModel()
        {
            Children = new List<MenuModel>();
        }
    }
}
