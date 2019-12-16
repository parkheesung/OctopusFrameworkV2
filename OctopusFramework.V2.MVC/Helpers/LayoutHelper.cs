using System;

namespace OctopusFramework.V2.MVC
{
    public class LayoutHelper
    {

        private static readonly Lazy<DynamicLayoutHelper> dynamicLayout = new Lazy<DynamicLayoutHelper>(() => new DynamicLayoutHelper());
        public static DynamicLayoutHelper Dynamic { get { return dynamicLayout.Value; } }

        private static readonly Lazy<CurrentLayoutHelper> currentLayout = new Lazy<CurrentLayoutHelper>(() => new CurrentLayoutHelper());
        public static CurrentLayoutHelper Current { get { return currentLayout.Value; } }
    }

    public class DynamicLayoutHelper
    {
        public DynamicLayoutHelper()
        {
        }

        public DefaultLayout Default
        {
            get
            {
                DefaultLayout layout = new DefaultLayout();
                return layout;
            }
        }

        public BootstrapLayout Bootstrap
        {
            get
            {
                BootstrapLayout layout = new BootstrapLayout();
                return layout;
            }
        }
    }

    public class CurrentLayoutHelper
    {
        public CurrentLayoutHelper()
        {
        }
    }
}
