using OctopusFramework.V2.MVC;
using OctopusFramework.V2.MVC;

namespace OctopusFramework.V2.MVC
{
    public class BootstrapLayout : DefaultLayout
    {
        public BootstrapLayout() : base()
        {
            this.Styles.TryAdd("bootstrap", new StyleTag("//maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css"));
            this.Styles.TryAdd("bootstrap_theme", new StyleTag("//maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap-theme.min.css"));

            this.Scripts.TryAdd("bootstrap", new ScriptTag("//maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"));
            this.Scripts.TryAdd("bootstrap_plugin", new ScriptTag("//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"));
        }

        public override string Write()
        {
            return base.Write();
        }
    }
}
