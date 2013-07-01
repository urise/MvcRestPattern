using System.Collections.Generic;
using CommonClasses;
using CommonClasses.Helpers;
using System.Linq;

namespace WebSite.Helpers
{
    public static class MenuCollection
    {
        private static List<MenuLink> _menuLinks = new List<MenuLink>
                       {
                           new MenuLink { MenuLinkId = 1, Name = "Расчетная ведомость", Controller = "PayRolls", Action = "PayRolls", ParentId = null, ItemLevel = 1, FinKeyRestricted = true, Component = AccessComponent.Payrolls},
                           new MenuLink { MenuLinkId = 2, Name = "Сотрудники", Controller = "Employee", Action = "ListEmployees", ParentId = null, ItemLevel = 1, Component = AccessComponent.Employees},
                           new MenuLink { MenuLinkId = 25, Name = "Отчеты", Controller = "Reports", Action = "Report", ParentId = null, ItemLevel = 1, IdDependant = true, TargetId = "1", Component = AccessComponent.Reports},
                           new MenuLink { MenuLinkId = 10, Name = "Настройки", Controller = "Exchange", Action = "Currency", ParentId = null, ItemLevel = 1, Component = AccessComponent.Settings, AutoRedirectedMenuId = 3},
                           new MenuLink { MenuLinkId = 3, Name = "Курсы валют", Controller = "Exchange", Action = "Currency", ParentId = 19, ItemLevel = 3, Component = AccessComponent.Currencies},
                           new MenuLink { MenuLinkId = 4, Name = "Компания", Controller = "Company", Action = "Index", ParentId = 20, ItemLevel = 3, Component = AccessComponent.Company},
                           new MenuLink { MenuLinkId = 5, Name = "Личные данные", Controller = "Individ", Action = "IndividData", ParentId = 2, IdDependant = true, ItemLevel = 2, Component = AccessComponent.Employees},
                           new MenuLink { MenuLinkId = 14, Name = "Дополнительные данные", Controller = "Individ", Action = "AdditionalData", ParentId = 2, IdDependant = true, ItemLevel = 2, Component = AccessComponent.Employees},
                           new MenuLink { MenuLinkId = 6, Name = "Индивидуальные расчеты", Controller = "Individ", Action = "IndividPayroll", ParentId = 2, IdDependant = true, ItemLevel = 2, Component = AccessComponent.IndividPayroll},
                           //new MenuLink { MenuLinkId = 7, Name = "Освобождение от работы", Controller = "Employee", Action = "ListEmployees", ParentId = 2, ItemLevel = 2},
                           //new MenuLink { MenuLinkId = 8, Name = "История изменений", Controller = "Exchange", Action = "Currency", ParentId = 2, ItemLevel = 2},
                           new MenuLink { MenuLinkId = 9, Name = "Финансовый ключ", Controller = "FinanceKey", Action = "FinanceKey", ParentId = 20, ItemLevel = 3, Component = AccessComponent.FinanceKey},
                           new MenuLink { MenuLinkId = 31, Name = "Групповое удаление", Controller = "GroupDelete", Action = "Index", ParentId = 20, ItemLevel = 3, Component = AccessComponent.FinanceKey},
                           new MenuLink { MenuLinkId = 12, Name = "Расчетные периоды", Controller = "Periods", Action = "Periods", ParentId = 21, ItemLevel = 3, Component = AccessComponent.Periods},
                           new MenuLink { MenuLinkId = 13, Name = "Список работ", Controller = "Duties", Action = "Index", ParentId = 21, ItemLevel = 3, Component = AccessComponent.Duties},
                           new MenuLink { MenuLinkId = 15, Name = "Список ОВР", Controller = "TransactionTypes", Action = "TransactionTypes", ParentId = 21, ItemLevel = 3, Component = AccessComponent.TransactionTypes},
                           new MenuLink { MenuLinkId = 16, Name = "Должности", Controller = "Positions", Action = "PositionsList", ParentId = 21, ItemLevel = 3, Component = AccessComponent.Positions},
                           new MenuLink { MenuLinkId = 17, Name = "Пользователи", Controller = "CompanyUsers", Action = "CompanyUsersList", ParentId = 20, ItemLevel = 3, Component = AccessComponent.Users},
                           new MenuLink { MenuLinkId = 18, Name = "Категории сотрудников", Controller = "Positions", Action = "PositionCategories", ParentId = 20, ItemLevel = 3, Component = AccessComponent.PositionCategories}, 
                           new MenuLink { MenuLinkId = 19, Name = "Валюта", Controller = "", Action = "", ParentId = 10, ItemLevel = 2, IsCategory = true, Component = AccessComponent.None},
                           new MenuLink { MenuLinkId = 20, Name = "Компания", Controller = "", Action = "", ParentId = 10, ItemLevel = 2, IsCategory = true, Component = AccessComponent.None}, 
                           new MenuLink { MenuLinkId = 21, Name = "Инфраструктура", Controller = "", Action = "", ParentId = 10, ItemLevel = 2, IsCategory = true, Component = AccessComponent.None},
                           new MenuLink { MenuLinkId = 22, Name = "Квалификационные уровни", Controller = "Positions", Action = "PositionLevels", ParentId = 20, ItemLevel = 3, Component = AccessComponent.PositionLevels},
                           new MenuLink { MenuLinkId = 23, Name = "Роли", Controller = "Roles", Action = "Roles", ParentId = 20, ItemLevel = 3, Component = AccessComponent.Roles},
                           new MenuLink { MenuLinkId = 24, Name = "Классификатор валютных курсов", Controller = "CurrencyClasses", Action = "CurrencyClasses", ParentId = 19, ItemLevel = 3, Component = AccessComponent.Currencies},
                           
                           new MenuLink { MenuLinkId = 26, Name = "Расходы на оплату труда", Controller = "Reports", Action = "Report", ParentId = 25, ItemLevel = 2, Component = AccessComponent.Reports, IdDependant = true, TargetId = "1"},
                           new MenuLink { MenuLinkId = 27, Name = "Изменения ставок сотрудников", Controller = "Reports", Action = "Report", ParentId = 25, ItemLevel = 2, Component = AccessComponent.Reports, IdDependant = true, TargetId = "2"},
                           new MenuLink { MenuLinkId = 28, Name = "Выплаты по операциям", Controller = "Reports", Action = "Report", ParentId = 25, ItemLevel = 2, Component = AccessComponent.Reports, IdDependant = true, TargetId = "3"},
                           new MenuLink { MenuLinkId = 29, Name = "Ставки по должностям компании", Controller = "Reports", Action = "Report", ParentId = 25, ItemLevel = 2, Component = AccessComponent.Reports, IdDependant = true, TargetId = "4"},
                           new MenuLink { MenuLinkId = 30, Name = "Даты пересмотра ставок", Controller = "Reports", Action = "Report", ParentId = 25, ItemLevel = 2, Component = AccessComponent.Reports, IdDependant = true, TargetId = "5"}
                       };

        public static List<MenuLink> MenuLinks
        {
            get
            {
                SetAccessToLinks();
                return _menuLinks;
            }
        }

        public static void SetAccessToLinks()
        {
            foreach (var menuLink in _menuLinks)
            {
                menuLink.HasAccess = menuLink.Component == AccessComponent.None || SessionHelper.Permissions.IsGranted(menuLink.Component, AccessLevel.Read);
            }

            foreach (var menuLink in _menuLinks.Where(m => m.AutoRedirectedMenuId != 0))
            {
                SetAvailableActionLink(menuLink);
            }
        }

        private static void SetAvailableActionLink(MenuLink menuLink)
        {
            if (menuLink.HasAccess)
            {
                MenuLink autoLink = GetAvailableActionLink(menuLink);
                if (autoLink != null)
                {
                    menuLink.Action = autoLink.Action;
                    menuLink.Controller = autoLink.Controller;
                }
            }
        }

        private static MenuLink GetAvailableActionLink(MenuLink menuLink)
        {
            MenuLink autoLink = _menuLinks.FirstOrDefault(m => menuLink.AutoRedirectedMenuId == m.MenuLinkId);
            if (!autoLink.HasAccess)
                foreach (var dependedLink in _menuLinks.Where(m => m.ParentId == menuLink.MenuLinkId))
                {
                    if (dependedLink.IsCategory)
                        autoLink = _menuLinks.FirstOrDefault(m => m.ParentId == dependedLink.MenuLinkId && m.HasAccess);
                    else if (dependedLink.HasAccess)
                        autoLink = dependedLink;

                    if (autoLink != null)
                        break;
                }
            return autoLink;
        }
    }
}
