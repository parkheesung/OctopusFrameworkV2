using OctopusFramework.V2.MVC;
using OctopusFramework.V2.MVC;

namespace OctopusFramework.V2.MVC
{
    public class DefaultLayout : WebPage
    {
        public DefaultLayout() : base()
        {
            this.Styles.TryAdd("Base", new StyleTag("/Resource/BaseStyle"));
            this.Scripts.TryAdd("Base", new ScriptTag("/Resource/BaseScript"));
        }
    }
}
