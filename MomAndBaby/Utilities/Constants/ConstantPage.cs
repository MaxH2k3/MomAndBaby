namespace MomAndBaby.Utilities.Constants
{
    public class ConstantPage
    {
        // Set up path for each page on Main
        public const string PrePathComponentMain = "../Main/Components";

        public class MainPage
        {
            public const string Header = PrePathComponentMain + "/Common/Header";
            public const string Footer = PrePathComponentMain + "/Common/Footer";

            public const string SearchMenu = PrePathComponentMain + "/Elements/SearchMenu";
            public const string SidebarMenu = PrePathComponentMain + "/Elements/SidebarMenu";
            public const string SideMenu = PrePathComponentMain + "/Elements/SideMenu";

            public const string ButtonCustom = PrePathComponentMain + "/Common/Button";

            public const string QuickView = "QuickView";

            public const string Chat = "../Chat/Menu";
        }

        // Set up path for each page on Dashboard
        public const string PrePathComponentDashboard = "../Dashboard/Components";

        public class DashboardPage
        {
            public const string Header = PrePathComponentDashboard + "/Common/Header";
            public const string Footer = PrePathComponentDashboard + "/Common/Footer";

            public const string Menu = PrePathComponentDashboard + "/Common/Menu";
        }
    }
}
